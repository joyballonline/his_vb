Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL

Public Class frmH01F20_SelectSyukka
    '-------------------------------------------------------------------------------
    '   変数定義
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _comLogc As CommonLogic                             '共通処理用
    Private _gh As UtilDataGridViewHandler                      'DataGridViewユーティリティクラス
    Private _parentForm As Form                                 '親フォーム
    Private _SelectID As String
    Private _userId As String                                   'ログインユーザＩＤ

    '------------------------------------------------------------------------------------------------------
    'メンバー定数宣言
    '------------------------------------------------------------------------------------------------------
    'PG制御文字 
    Private Const RS As String = "RecSet"                               'レコードセットテーブル
    'グリッド列№
    Private Const COLNO_SYUKKACD = 0                                '01:出荷先CD
    Private Const COLNO_SYUKKANM = 1                                '02:出荷先名
    Private Const COLNO_BUNRUI = 2                                  '03:出荷分類
    Private Const COLNO_ADDRESS = 3                                 '04:住所
    Private Const COLNO_TEL = 4                                     '05:電話番号
    Private Const COLNO_MEMO = 5                                    '06:備考

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
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByRef prmSelectID As String, ByRef prmParentForm As Form)
        Call Me.New()

        Try
            '初期処理
            _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
            _db = prmRefDbHd                                                    'DBハンドラの設定
            _comLogc = New CommonLogic(_db, _msgHd)                             '共通処理用
            _gh = New UtilDataGridViewHandler(dgvList)                          'DataGridViewユーティリティクラス
            _parentForm = prmParentForm
            _SelectID = prmSelectID
            _userId = frmC01F10_Login.loginValue.TantoCD                        'ユーザＩＤ

            StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
            Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示

            '操作履歴ログ作成（初期処理）
            _comLogc.Insert_L01_ProcLog(frmC01F10_Login.loginValue.BumonCD, DBNull.Value, _SelectID, CommonConst.PROGRAM_START, CommonConst.STATUS_NORMAL,
                                        DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value,
                                        DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _userId)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub
    '閉じるボタンクリック時
    Private Sub cmdBack_Click(sender As Object, e As EventArgs) Handles cmdBack.Click

        _parentForm.Show()                                              ' 前画面を表示
        _parentForm.Enabled = True                                      ' 前画面の使用を可能にする
        _parentForm.Activate()                                          ' 前画面をアクティブにする

        Me.Dispose()                                                    ' 自画面を閉じる

    End Sub
    '選択ボタンクリック時
    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click

        Try
            '出荷先選択処理
            selectSyukkasaki()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '検索ボタンクリック時
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            '一覧データを取得
            getSyukkasakiList()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '一覧セルダブルクリック
    Private Sub dgvLIST_CellDoubleClick(sender As Object, e As EventArgs) Handles dgvLIST.CellDoubleClick

        Try
            'ヘッダー行ダブルクリックの場合、処理終了
            If TryCast(e, DataGridViewCellEventArgs).RowIndex < 0 Then
                Exit Sub
            End If

            '出荷先選択処理
            selectSyukkasaki()

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

                '出荷先選択処理
                selectSyukkasaki()

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

    Private Sub getSyukkasakiList()
        '--------------------------------
        '抽出条件からデータ取得
        'functionにする
        '--------------------------------
        Dim strSql As String = ""

        '取得したデータをDataGrdViewに反映
        strSql = "SELECT "
        strSql = strSql & "  c.会社コード, c.取引先コード, c.取引先名, c.出荷先分類,h.文字１ as 出荷先分類名 , c.住所１, c.電話番号, c.メモ  "
        strSql = strSql & " FROM m10_customer c "
        strSql = strSql & "    inner join m90_hanyo h on h.会社コード= c.会社コード and h.固定キー ='" & CommonConst.HANYO_KOTEI_SKBUNRUI & "' and h.可変キー = c.出荷先分類 "
        strSql = strSql & " Where c.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' "
        strSql = strSql & makeLikeSql()                                     '抽出条件の取得、SQLwhere句の作成
        strSql = strSql & " order by c.会社コード, c.取引先コード "

        Dim reccnt As Integer = 0
        Dim ds As DataSet = _db.selectDB(strSql, RS, reccnt)

        '描画の前にすべてクリアする
        dgvList.Rows.Clear()
        For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
            dgvList.Rows.Add()
            dgvList.Rows(index).Cells(COLNO_SYUKKACD).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("取引先コード"))     '出荷先ＣＤ
            dgvList.Rows(index).Cells(COLNO_SYUKKANM).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("取引先名"))         '出荷先名
            dgvList.Rows(index).Cells(COLNO_BUNRUI).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("出荷先分類名"))     '分類
            dgvList.Rows(index).Cells(COLNO_ADDRESS).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("住所１"))           '住所
            dgvList.Rows(index).Cells(COLNO_TEL).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("電話番号"))         '電話番号
            dgvList.Rows(index).Cells(COLNO_MEMO).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("メモ"))             '備考
        Next


        '抽出データ件数を取得、表示
        txtTotal.Text = ds.Tables(RS).Rows.Count

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


    Function makeLikeSql() As String
        '--------------------------------
        '抽出条件の取得、SQLwhere句の作成
        '--------------------------------
        Dim syukkasakiName As String = If(txtSyukkasakiName.Text IsNot "", txtSyukkasakiName.Text, "")     '出荷先名
        Dim address As String = If(txtAddress.Text IsNot "", txtAddress.Text, "")                   '住所
        Dim tel As String = If(txtTel.Text IsNot "", txtTel.Text, "")                           '電話番号
        Dim syukkasakiCd As String = If(txtSyukkasakiCd.Text IsNot "", txtSyukkasakiCd.Text, "")         '出荷先ＣＤ

        Dim termsSql As String = ""
        termsSql += " and ( c.取引先名 like  '%" & syukkasakiName & "%' "
        termsSql += " and c.住所１ like  '%" & address & "%' "
        termsSql += " and c.電話番号 like  '%" & tel & "%' "
        termsSql += " and c.取引先コード like  '" & syukkasakiCd & "%' ) "

        Return termsSql
    End Function

    '出荷先選択処理
    Private Sub selectSyukkasaki()

        Dim idx As Integer
        For Each c As DataGridViewRow In dgvList.SelectedRows
            idx = c.Index
            Exit For
        Next c

        Dim openForm As Form = Nothing
        openForm = New frmH01F60_Chumon(_msgHd, _db, _SelectID, CommonConst.MODE_ADDNEW, dgvList.Rows(idx).Cells(COLNO_SYUKKACD).Value, Me)
        openForm.Show()
        Me.Hide()
        
    End Sub

    'テキストボックスのキープレスイベント
    Private Sub txtSyukkasakiName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSyukkasakiName.KeyPress, txtAddress.KeyPress, txtTel.KeyPress, txtSyukkasakiCd.KeyPress
        UtilMDL.UtilClass.moveNextFocus(Me, e)
    End Sub

    '-------------------------------------------------------------------------------
    '　フォーカス取得イベント
    '-------------------------------------------------------------------------------
    Private Sub ctl_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
                            txtSyukkasakiName.GotFocus, txtAddress.GotFocus, txtTel.GotFocus, txtSyukkasakiCd.GotFocus

        'フォーカス取得時、入力パラメタのコントロールを全選択状態とする
        Call UtilClass.selAll(sender)

    End Sub

End Class