'===============================================================================
'
'　カネキ吉田商店様
'　　（システム名）販売管理
'　　（処理機能名）仕入一覧
'    （フォームID）H06F10
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   菅野雄      2018/03/08                 新規              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL

Public Class frmH06F10_SelectShiire
    Inherits System.Windows.Forms.Form

#Region "宣言"
    '-------------------------------------------------------------------------------
    '   変数定義
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _comLogc As CommonLogic                             '共通処理用
    Private _gh As UtilDataGridViewHandler                      'DataGridViewユーティリティクラス
    Private _parentForm As Form                                 '親フォーム
    Private _SelectID As String                                 'メニュー選択処理ID
    Private _userId As String                                   'ログインユーザＩＤ

    Private _ShoriMode As Integer                               '処理モード（登録、変更、取消、照会)
    Private _selected As Boolean                                'フォームからの戻り値用　選択状態　True:選択された　False:選択されなかった
    Private _selectValShiireNo As String                        'フォームからの戻り値用　仕入伝番

    '-------------------------------------------------------------------------------
    '   定数定義
    '-------------------------------------------------------------------------------
    Private Const N As String = ControlChars.NewLine            '改行文字
    Private Const RS As String = "RecSet"                       'レコードセットテーブル
    'グリッド列№
    Private Const COLNO_SHIIREYMD = 0                           '01:仕入日
    Private Const COLNO_DENPYONO_DISP = 1                       '02:伝票番号（表示用）
    Private Const COLNO_DENPYONO = 2                            '03:伝票番号
    Private Const COLNO_SHIIRECD = 3                            '04:仕入先コード
    Private Const COLNO_SHIIRENM = 4                            '05:仕入先名
    Private Const COLNO_SHOHINCD = 5                            '06:商品コード
    Private Const COLNO_SHOHINNM = 6                            '07:商品名
    Private Const COLNO_SUURYO = 7                              '08:数量
    Private Const COLNO_TANKA = 8                               '09:単価
    Private Const COLNO_KINGAKU = 9                             '10:金額
    Private Const COLNO_SHIHARAIUMU = 10                        '11:支払(支払有無)
    Private Const COLNO_TORIKESHI = 11                          '12:取消区分
    '編集モードの名称
    Private Const MODEP_ADDNEW = "複写元選択モード"             '複写新規
    Private Const MODEP_EditStatus = "変更選択モード"           '変更
    Private Const MODEP_CancelStatus = "取消選択モード"         '取消
    Private Const MODEP_InquiryStatus = "参照選択モード"        '参照

    Private Const TORIKESHI = "取消"                            '取消データの表示
#End Region

    '選択状態   True:選択状態 False:非選択状態
    Public ReadOnly Property Selected() As String
        Get
            Return _selected
        End Get
    End Property

    '選択した仕入伝番
    Public ReadOnly Property GetValShiireNo() As String
        Get
            Return _selectValShiireNo
        End Get
    End Property

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

        Try
            '初期処理
            _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
            _db = prmRefDbHd                                                    'DBハンドラの設定
            _comLogc = New CommonLogic(_db, _msgHd)                             '共通処理用
            _gh = New UtilDataGridViewHandler(dgvList)                          'DataGridViewユーティリティクラス
            _parentForm = prmParentForm                                         '親フォーム
            _SelectID = prmSelectID                                             'メニュー選択処理ID
            _userId = frmC01F10_Login.loginValue.TantoCD                        'ユーザＩＤ

            StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
            Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                'フォームタイトル表示

            '操作履歴ログ作成（初期処理）
            _comLogc.Insert_L01_ProcLog(frmC01F10_Login.loginValue.BumonCD, DBNull.Value, _SelectID, CommonConst.PROGRAM_START, CommonConst.STATUS_NORMAL,
                                        DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value,
                                        DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _userId)

            _selected = False         '選択状態リセット

            '仕入日表示
            dtpShiireDateFrom.Text = DateAdd("m", -1, Now).ToString("yyyy/MM/dd")   '仕入日自 システム日付-1ヶ月
            dtpShiireDateTo.Text = DateAdd("m", 0, Now).ToString("yyyy/MM/dd")      '仕入日至 システム日付

            'モード設定
            Select Case prmSelectID
                Case CommonConst.MENU_H0610   '複写新規
                    _ShoriMode = CommonConst.MODE_ADDNEW

                Case CommonConst.MENU_H0620   '変更
                    _ShoriMode = CommonConst.MODE_EditStatus

                Case CommonConst.MENU_H0630   '取消
                    _ShoriMode = CommonConst.MODE_CancelStatus

                Case CommonConst.MENU_H0640   '参照
                    _ShoriMode = CommonConst.MODE_InquiryStatus

                Case Else                     '上記以外
                    _ShoriMode = CommonConst.MODE_InquiryStatus

            End Select

            'モードによる分岐処理
            Select Case _ShoriMode
                Case CommonConst.MODE_ADDNEW         '複写新規
                    Me.lblMode.Text = MODEP_ADDNEW
                    Me.chkTorikesi.Checked = True
                Case CommonConst.MODE_EditStatus     '変更
                    Me.lblMode.Text = MODEP_EditStatus
                    Me.chkTorikesi.Checked = False
                Case CommonConst.MODE_CancelStatus   '取消
                    Me.lblMode.Text = MODEP_CancelStatus
                    Me.chkTorikesi.Checked = False
                Case CommonConst.MODE_InquiryStatus  '参照
                    Me.lblMode.Text = MODEP_InquiryStatus
                    Me.chkTorikesi.Checked = False
                Case Else
                    Me.lblMode.Text = ""
                    Me.chkTorikesi.Checked = False
            End Select

            '一覧初期表示
            DispList()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '検索ボタンクリック
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            'リスト表示
            DispList()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '選択ボタンクリック時
    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        Try
            '仕入データ選択処理
            selectShiire()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '戻るボタンクリック
    Private Sub cmdBack_Click(sender As Object, e As EventArgs) Handles cmdBack.Click
        Try
            _parentForm.Show()                                              ' 前画面を表示
            _parentForm.Enabled = True                                      ' 前画面の使用を可能にする
            _parentForm.Activate()                                          ' 前画面をアクティブにする

            Me.Dispose()                                                    ' 自画面を閉じる

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    'テキストボックスのキープレスイベント
    Private Sub txtSyukkasakiName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtShiiresakiName.KeyPress, txtShiiresakiCd.KeyPress, txtSyohinName.KeyPress, txtSyohinCd.KeyPress,
                                                                                             dtpShiireDateFrom.KeyPress, dtpShiireDateTo.KeyPress, txtDenpyoNoFrom.KeyPress, txtDenpyoNoTo.KeyPress
        UtilMDL.UtilClass.moveNextFocus(Me, e)
    End Sub

    '-------------------------------------------------------------------------------
    '　フォーカス取得イベント
    '-------------------------------------------------------------------------------
    Private Sub ctl_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtShiiresakiName.GotFocus, txtShiiresakiCd.GotFocus, txtSyohinName.GotFocus, txtSyohinCd.GotFocus,
                                                                                             dtpShiireDateFrom.GotFocus, dtpShiireDateTo.GotFocus, txtDenpyoNoFrom.GotFocus, txtDenpyoNoTo.GotFocus

        'フォーカス取得時、入力パラメタのコントロールを全選択状態とする
        Call UtilClass.selAll(sender)

    End Sub

    '伝票番号(From,To)からフォーカスが外れたとき
    Private Sub txtDenpyoNo_Leave(sender As Object, e As EventArgs) Handles txtDenpyoNoFrom.Leave, txtDenpyoNoTo.Leave
        Try
            '伝票番号からフォーカスアウト時処理
            denpyoNoLeave(sender)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '一覧セルダブルクリック
    Private Sub dgvLIST_CellDoubleClick(sender As Object, e As EventArgs) Handles dgvList.CellDoubleClick
        Try
            'ヘッダー行ダブルクリックの場合、処理終了
            If TryCast(e, DataGridViewCellEventArgs).RowIndex < 0 Then
                Exit Sub
            End If

            '仕入データ選択処理
            selectShiire()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '一覧キーダウン
    Private Sub dgvLIST_KeyDown(sender As Object, e As EventArgs) Handles dgvList.KeyDown
        Try
            Dim keyEventArgs As KeyEventArgs = TryCast(e, KeyEventArgs)

            If keyEventArgs.KeyData = Keys.Enter Then
                '押下キーがEnterの場合

                '仕入データ選択処理
                selectShiire()

                'Enterキー処理無効化
                keyEventArgs.Handled = True

            Else
                'タブキー押下時制御 タブキー押下時、行移動する
                _gh.gridTabKeyDown(Me, e)
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '伝票番号フォーカスアウト時処理
    Private Sub denpyoNoLeave(textbox As TextBox)

        '入力がある場合
        If (textbox.Text <> String.Empty) Then

            '伝票番号の連番桁数で前ゼロ埋め
            textbox.Text = textbox.Text.PadLeft(CommonConst.KETA_DEN_SHIIRE, "0"c)
        End If
    End Sub

    '仕入データをもとにグリッドへ一覧表示
    Private Sub DispList()

        Dim strSQL As String = ""
        strSQL &= N & " SELECT "
        strSQL &= N & "        DISTINCT SRD2.仕入伝番 "
        strSQL &= N & " FROM   T41_SIREDT              SRD2 "                                               '仕入明細
        strSQL &= N & " INNER  JOIN T40_SIREHD         SRH2 "                                               '仕入基本
        strSQL &= N & "    ON  SRH2.会社コード       = SRD2.会社コード "
        strSQL &= N & "   AND  SRH2.仕入伝番         = SRD2.仕入伝番 "
        strSQL &= N & " WHERE  SRD2.会社コード       = '" & frmC01F10_Login.loginValue.BumonCD & "' "

        '仕入先名
        If Me.txtShiiresakiName.Text <> String.Empty Then
            strSQL &= N & "   AND  SRH2.仕入先名      LIKE '%" & _db.rmSQ(Me.txtShiiresakiName.Text) & "%' "
        End If
        '仕入先コード
        If Me.txtShiiresakiCd.Text <> String.Empty Then
            strSQL &= N & "   AND  SRH2.仕入先コード  LIKE '" & _db.rmSQ(Me.txtShiiresakiCd.Text) & "%' "
        End If
        '商品名
        If Me.txtSyohinName.Text <> String.Empty Then
            strSQL &= N & "   AND  SRD2.商品名        LIKE '%" & _db.rmSQ(Me.txtSyohinName.Text) & "%' "
        End If
        '商品コード
        If Me.txtSyohinCd.Text <> String.Empty Then
            strSQL &= N & "   AND  SRD2.商品コード    LIKE '" & _db.rmSQ(Me.txtSyohinCd.Text) & "%' "
        End If
        '仕入日From
        If Me.dtpShiireDateFrom.Value.ToString <> String.Empty Then
            strSQL &= N & "   AND  SRH2.仕入日          >= '" & _db.rmSQ(Me.dtpShiireDateFrom.Value.ToString("yyyy/MM/dd")) & "' "
        End If
        '仕入日To
        If Me.dtpShiireDateTo.Value.ToString <> String.Empty Then
            strSQL &= N & "   AND  SRH2.仕入日          <= '" & _db.rmSQ(Me.dtpShiireDateTo.Value.ToString("yyyy/MM/dd")) & "' "
        End If
        '伝票番号From
        If Me.txtDenpyoNoFrom.Text <> String.Empty Then
            strSQL &= N & "   AND  SRH2.仕入伝番        >= '" & _db.rmSQ(CommonConst.CONNECT_DEN_SHIIRE & Me.txtDenpyoNoFrom.Text) & "' "
        End If
        '伝票番号To
        If Me.txtDenpyoNoTo.Text <> String.Empty Then
            strSQL &= N & "   AND  SRH2.仕入伝番        <= '" & _db.rmSQ(CommonConst.CONNECT_DEN_SHIIRE & Me.txtDenpyoNoTo.Text) & "' "
        End If
        '仕入単価（未設定のみ表示チェック。チェックON時、仕入単価 = 0 のみ検索対象）
        If Me.chkTankaMiSettei.Checked Then
            strSQL &= N & "   AND  SRD2.仕入単価         = 0 "
        End If
        '支払（未払のみ表示チェック。チェックON時、支払有無 = 0（未払い） のみ検索対象）
        If Me.chkMiharai.Checked Then
            strSQL &= N & "   AND  SRD2.支払有無         = 0 "
        End If
        '取消区分（取消データ含むチェック。チェックOFF時、0（有効データ）のみ検索対象）
        If Not Me.chkTorikesi.Checked Then
            strSQL &= N & "   AND  SRH2.取消区分         = 0 "
        End If


        '支払有無
        Dim strSQLShiharai As String = ""

        If rdbMeisai.Checked Then
            '表示形式が明細単位の場合
            strSQLShiharai &= N & "       ,SRD.支払有無 "
        Else
            '表示形式が伝票単位の場合
            '全ての明細が0(未払い)    → 未
            '全ての明細が1(支払済)    → 済
            '0(未払い)と1(支払済)混在 → 一部
            strSQLShiharai &= N & "      ,( SELECT CASE SUM(SRD3.支払有無) WHEN 0             THEN '" & CommonConst.SHIHARAIUMUNM_MI & "' "
            strSQLShiharai &= N & "                                        WHEN COUNT(SRD3.*) THEN '" & CommonConst.SHIHARAIUMUNM_SUMI & "' "
            strSQLShiharai &= N & "                                                           ELSE '" & CommonConst.SHIHARAIUMUNM_ICHIBU & "' "
            strSQLShiharai &= N & "                END "
            strSQLShiharai &= N & "         FROM   T41_SIREDT          SRD3 "
            strSQLShiharai &= N & "         WHERE  SRD3.会社コード   = SRH.会社コード "
            strSQLShiharai &= N & "           AND  SRD3.仕入伝番     = SRH.仕入伝番 "
            strSQLShiharai &= N & "       )                         AS 支払有無 "
        End If

        Dim strSQLALL As String = ""
        strSQLALL &= N & " SELECT "
        strSQLALL &= N & "        SRH.会社コード "
        strSQLALL &= N & "       ,SRH.仕入日 "
        strSQLALL &= N & "       ,SRH.仕入伝番 "
        strSQLALL &= N & "       ,SRH.仕入先コード "
        strSQLALL &= N & "       ,SRH.仕入先名 "
        strSQLALL &= N & "       ,SRD.商品コード "
        strSQLALL &= N & "       ,SRD.商品名 "
        strSQLALL &= N & "       ,SRD.仕入数量 "
        strSQLALL &= N & "       ,SRD.仕入単価 "
        strSQLALL &= N & "       ,SRD.仕入金額 "
        strSQLALL &= N & strSQLShiharai                                                                       '支払有無
        strSQLALL &= N & "       ,SRD.行番 "
        strSQLALL &= N & "       ,SRH.取消区分 "
        strSQLALL &= N & "       ,SRD.単位 "
        strSQLALL &= N & " FROM   T41_SIREDT              SRD "                                               '仕入明細
        strSQLALL &= N & " INNER  JOIN T40_SIREHD         SRH "                                               '仕入基本
        strSQLALL &= N & "    ON  SRH.会社コード        = SRD.会社コード "
        strSQLALL &= N & "   AND  SRH.仕入伝番          = SRD.仕入伝番 "
        strSQLALL &= N & " WHERE  SRD.会社コード        = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        strSQLALL &= N & "   AND  SRD.仕入伝番         IN (" & strSQL & ") "

        '表示形式が伝票単位の場合
        If rdbDenpyo.Checked Then
            strSQLALL &= N & " AND SRD.行番            = 1 "
        End If
        strSQLALL &= N & " ORDER BY "
        strSQLALL &= N & "        SRH.仕入日 DESC "
        strSQLALL &= N & "       ,SRH.仕入伝番 "
        strSQLALL &= N & "       ,SRD.行番 "

        Dim reccnt As Integer = 0
        Dim ds As DataSet = _db.selectDB(strSQLALL, RS, reccnt)

        '画面入力の条件値をもとに、対象データを検索し、一覧表示する。	
        '検索にあたっては、次の手順を行う。	
        '①対象データの件数を取得する
        If reccnt > StartUp.iniValue.GamenSelUpperCnt Then
            '②件数が規定件数を超える場合、表示有無の確認を行う
            '　規定件数以内の場合は③に進む
            '確認メッセージを表示する
            Dim piRtn As Integer
            piRtn = _msgHd.dspMSG("MaxDataCnt")  '抽出したデータが上限を超えています。表示してよろしいですか？
            If piRtn = vbCancel Then
                Exit Sub
            End If
        End If

        '③画面を表示する

        '描画の前にすべてクリアする
        dgvList.Rows.Clear()

        '一覧の生成
        For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
            dgvList.Rows.Add()

            dgvList.Rows(index).Cells(COLNO_SHIIREYMD).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("仕入日"))       '仕入日

            '伝票番号
            Dim denpyoNo As String = _db.rmNullStr(ds.Tables(RS).Rows(index)("仕入伝番"))

            '表示形式が明細単位の場合
            If rdbMeisai.Checked Then
                '伝票番号に行番を追加
                denpyoNo &= "-" & _db.rmNullStr(ds.Tables(RS).Rows(index)("行番"))
            End If

            '取消データ（取消区分=1）の場合
            If _db.rmNullStr(ds.Tables(RS).Rows(index)("取消区分")) = "1" Then
                '「取消」を追加
                denpyoNo &= TORIKESHI
            End If

            dgvList.Rows(index).Cells(COLNO_DENPYONO_DISP).Value = denpyoNo                                                            '伝票番号（表示用）
            dgvList.Rows(index).Cells(COLNO_DENPYONO).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("仕入伝番"))                     '伝票番号
            dgvList.Rows(index).Cells(COLNO_SHIIRECD).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("仕入先コード"))                 '仕入先コード
            dgvList.Rows(index).Cells(COLNO_SHIIRENM).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("仕入先名"))                     '仕入先名
            dgvList.Rows(index).Cells(COLNO_SHOHINCD).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("商品コード"))                   '商品コード
            dgvList.Rows(index).Cells(COLNO_SHOHINNM).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("商品名"))                       '商品名
            dgvList.Rows(index).Cells(COLNO_SUURYO).Value = _db.rmNullDouble(ds.Tables(RS).Rows(index)("仕入数量")).ToString("N2") &   '数量
                                                            _db.rmNullStr(ds.Tables(RS).Rows(index)("単位"))
            dgvList.Rows(index).Cells(COLNO_TANKA).Value = _db.rmNullDouble(ds.Tables(RS).Rows(index)("仕入単価"))                     '単価
            dgvList.Rows(index).Cells(COLNO_KINGAKU).Value = _db.rmNullDouble(ds.Tables(RS).Rows(index)("仕入金額"))                   '金額
            dgvList.Rows(index).Cells(COLNO_SHIHARAIUMU).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("支払有無"))                  '支払
            dgvList.Rows(index).Cells(COLNO_TORIKESHI).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("取消区分"))                    '取消区分
        Next

        '件数表示
        Me.lblListCount.Text = dgvList.RowCount

        '対象データの有無チェック
        If ds.Tables(RS).Rows.Count = 0 Then
            '選択ボタンを非活性
            Me.btnSelect.Enabled = False
            Throw New UsrDefException("対象データがありません。", _msgHd.getMSG("NonData"))
        Else
            '選択ボタンを活性
            Me.btnSelect.Enabled = True
        End If

    End Sub

    '仕入データ選択処理
    Private Sub selectShiire()

        Dim idx As Integer

        '一覧選択行インデックスの取得
        For Each c As DataGridViewRow In dgvList.SelectedRows
            idx = c.Index
            Exit For
        Next c

        '複写新規モード以外の場合
        If _ShoriMode <> CommonConst.MODE_ADDNEW Then

            '取消区分
            Dim torikeshiKbn = dgvList.Rows(idx).Cells(COLNO_TORIKESHI).Value

            '取消データ（取消区分=1）の場合
            If torikeshiKbn = "1" Then
                Throw New UsrDefException("取消データは選択できません。", _msgHd.getMSG("cannotSelectTorikeshiData"))
            End If
        End If

        '仕入伝番
        Dim shiireNo = dgvList.Rows(idx).Cells(COLNO_DENPYONO).Value

        If _ShoriMode <> CommonConst.MODE_ADDNEW Then
            '複写新規モード以外の場合

            '仕入入力フォームオープン
            Dim openForm As Form = Nothing
            openForm = New frmH06F20_Shiire(_msgHd, _db, _SelectID, _SelectID, Me, shiireNo)
            openForm.Show()

            Me.Hide()   ' 自分は隠れる

        Else
            '複写新規モードの場合

            '選択行の仕入伝番をセット
            _selectValShiireNo = shiireNo

            _selected = True        '選択状態

            Me.Hide()               ' 自分は隠れる
        End If

    End Sub

End Class