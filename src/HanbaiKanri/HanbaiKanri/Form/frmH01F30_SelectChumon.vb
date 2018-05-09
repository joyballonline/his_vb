Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL


Public Class frmH01F30_SelectChumon

    '------------------------------------------------------------------------------------------------------
    'メンバー定数宣言
    '------------------------------------------------------------------------------------------------------
    'PG制御文字 
    Private Const N As String = ControlChars.NewLine                    '改行文字
    Private Const RS As String = "RecSet"                               'レコードセットテーブル
    'グリッド列№
    Private Const COLNO_ICHI = 0        '01:位置
    Private Const COLNO_SYUKKACD = 1    '02:出荷先CD
    Private Const COLNO_SYUKKANM = 2    '03:出荷先名
    Private Const COLNO_BUNRUI = 3      '04:出荷分類
    Private Const COLNO_SHOHINNM = 4    '05:商品
    Private Const COLNO_MEMO = 5        '06:注文帳備考

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
            Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]"                                  'フォームタイトル表示

            '操作履歴ログ作成（初期処理）
            _comLogc.Insert_L01_ProcLog(frmC01F10_Login.loginValue.BumonCD, DBNull.Value, _SelectID, CommonConst.PROGRAM_START, CommonConst.STATUS_NORMAL,
                                        DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value,
                                        DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _userId)

            'コンボボックスの初期値表示
            clearCmbChumon()
            cboGroup.SelectedIndex = 0

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

            '注文データ選択処理
            selectChumon()

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

                '注文データ選択処理
                selectChumon()

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

    'リスト表示
    Private Sub getSyukkasakiList()
        '--------------------------------
        '抽出条件からデータ取得
        '--------------------------------
        Dim strSql As String = ""

        '--------------------------------
        ' ユーザ操作による行追加を無効(禁止)
        '--------------------------------
        dgvList.AllowUserToAddRows = False
        '表示のセンター寄せ
        dgvList.Columns(COLNO_ICHI).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvList.Columns(COLNO_SYUKKACD).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvList.Columns(COLNO_BUNRUI).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        '取得したデータをDataGrdViewに反映
        '商品名を取得する
        strSql = ""
        strSql = "SELECT "
        strSql = strSql & "  c.注文帳横名称 "
        strSql = strSql & " FROM m31_cmngods c "
        strSql = strSql & " Where c.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and 注文帳番号 ='" & _db.rmSQ(cboGroup.SelectedValue) & "'"
        strSql = strSql & " order by 横位置 "
        Dim reccnt As Integer = 0
        Dim dsA As DataSet = _db.selectDB(strSql, RS, reccnt)
        Dim strShohinNM As String = ""       '商品名
        For index As Integer = 0 To dsA.Tables(RS).Rows.Count - 1
            If strShohinNM <> "" Then
                strShohinNM = strShohinNM & ","
            End If
            strShohinNM = strShohinNM & _db.rmNullStr(dsA.Tables(RS).Rows(index)("注文帳横名称"))
        Next



        strSql = ""
        strSql = "SELECT "
        strSql = strSql & "   c.縦位置, c.出荷先コード, c.注文帳縦名称, c.出荷先分類,h.文字１ as 出荷先分類名 , c.メモ "
        strSql = strSql & " FROM m30_cmnship c "
        strSql = strSql & "    inner join m90_hanyo h on h.会社コード= c.会社コード and h.固定キー ='" & CommonConst.HANYO_KOTEI_SKBUNRUI & "' and h.可変キー = c.出荷先分類 "
        strSql = strSql & " Where c.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and 注文帳番号 ='" & _db.rmSQ(cboGroup.SelectedValue) & "'"
        strSql = strSql & " order by c.縦位置, c.出荷先コード "

        reccnt = 0
        Dim ds As DataSet = _db.selectDB(strSql, RS, reccnt)

        '描画の前にすべてクリアする
        dgvList.Rows.Clear()
        For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1

            dgvList.Rows.Add()
            dgvList.Rows(index).Cells(COLNO_ICHI).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("縦位置"))              '縦位置
            dgvList.Rows(index).Cells(COLNO_SYUKKACD).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("出荷先コード"))    '出荷先コード
            dgvList.Rows(index).Cells(COLNO_SYUKKANM).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("注文帳縦名称"))    '注文帳縦名称
            dgvList.Rows(index).Cells(COLNO_BUNRUI).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("出荷先分類名"))      '分類
            dgvList.Rows(index).Cells(COLNO_SHOHINNM).Value = strShohinNM                                                 '商品
            dgvList.Rows(index).Cells(COLNO_MEMO).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("メモ"))                '備考
        Next


        '抽出データ件数を取得、表示
        lblCount.Text = ds.Tables(RS).Rows.Count

        '対象データの有無チェック
        If ds.Tables(RS).Rows.Count = 0 Then
            '選択ボタンを非活性
            Me.btnSelect.Enabled = False
        Else
            '選択ボタンを活性
            Me.btnSelect.Enabled = True
        End If

    End Sub

    '-------------------------------------------------------------------------------
    '   注文帳グループコンボボックスを初期化
    '   （処理概要）　会社マスタよりデータを取得し、コンボボックスにセットする
    '-------------------------------------------------------------------------------
    Private Sub clearCmbChumon()

        Dim strSql As String = ""
        '汎用マスタをコンボボックスにセット
        Try
            strSql = "SELECT "
            strSql = strSql & "    可変キー, 文字１ "
            strSql = strSql & " FROM m90_hanyo "
            strSql = strSql & "   WHERE 会社コード= '" & frmC01F10_Login.loginValue.BumonCD & "' and 固定キー ='" & CommonConst.HANYO_KOTEI_CMNNO & "' "
            strSql = strSql & " order by 表示順 "
            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(strSql, RS, reccnt)

            cboGroup.DisplayMember = "文字１"
            cboGroup.ValueMember = "可変キー"
            cboGroup.DataSource = ds.Tables(RS)
        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '戻るボタンクリック
    Private Sub cmdReturn_Click(sender As Object, e As EventArgs) Handles cmdReturn.Click
        _parentForm.Show()                                              ' 前画面を表示
        _parentForm.Enabled = True                                      ' 前画面の使用を可能にする
        _parentForm.Activate()                                          ' 前画面をアクティブにする

        Me.Dispose()                                                    ' 自画面を閉じる


    End Sub
    '選択ボタンクリック
    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        Try
            '注文データ選択処理
            selectChumon()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '注文帳グループ変更時
    Private Sub cboGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboGroup.SelectedIndexChanged
        Try
            'リスト表示
            getSyukkasakiList()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '注文データ選択処理
    Private Sub selectChumon()

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
End Class