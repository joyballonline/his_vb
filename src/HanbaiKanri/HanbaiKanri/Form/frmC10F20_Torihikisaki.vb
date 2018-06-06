'===============================================================================
'
'　カネキ吉田商店様
'　　（システム名）販売管理システム
'　　（処理機能名）仕入入力画面
'    （フォームID）C10F20
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   菅野雄      2018/3/15                  新規              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL.MSG
Imports UtilMDL.DataGridView
Imports UtilMDL.DB
Imports UtilMDL

Public Class frmC10F20_Torihikisaki
    Inherits System.Windows.Forms.Form

#Region "宣言"
    '-------------------------------------------------------------------------------
    '   変数定義
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _gh As UtilDataGridViewHandler                          'DataGridViewユーティリティクラス
    Private _comLogc As CommonLogic                                 '共通処理用

    Private _targetKbn As String                                    '対象区分（請求先、取引先、取引先Ｇ、仕入先、支払先）
    Private _selected As Boolean                                    'フォームからの戻り値用　選択状態　True:選択された　False:選択されなかった

    Private _selectValTorihikisakiCd As String                      'フォームからの戻り値用　取引先コード
    Private _selectValTorihikisakiName As String                    'フォームからの戻り値用　取引先名
    Private _selectValBunrui As String                              'フォームからの戻り値用　分類
    Private _selectValAddress As String                             'フォームからの戻り値用　住所
    Private _selectValTel As String                                 'フォームからの戻り値用　電話番号
    Private _selectValMemo As String                                'フォームからの戻り値用　備考

    '------------------------------------------------------------------------------------------------------
    'メンバー定数宣言
    '------------------------------------------------------------------------------------------------------
    'PG制御文字 
    Private Const RS As String = "RecSet"                           'レコードセットテーブル
    'グリッド列№
    Private Const COLNO_TorihikisakiCD = 0                          '01:取引先CD
    Private Const COLNO_TorihikisakiNM = 1                          '02:取引先名
    Private Const COLNO_BUNRUI = 2                                  '03:出荷分類
    Private Const COLNO_ADDRESS = 3                                 '04:住所
    Private Const COLNO_TEL = 4                                     '05:電話番号
    Private Const COLNO_MEMO = 5                                    '06:備考

    '対象区分 画面表示名称
    Private Const TARGET_KBN_DISP_SEIKYU = "請求先"
    Private Const TARGET_KBN_DISP_SHUKKA = "出荷先"
    Private Const TARGET_KBN_DISP_SHUKKAG = "出荷先Ｇ"
    Private Const TARGET_KBN_DISP_SHIIRE = "仕入先"
    Private Const TARGET_KBN_DISP_SHIHARAI = "支払先"

#End Region

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
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByVal prmTargetKbn As String)
        Call Me.New()

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _gh = New UtilDataGridViewHandler(dgvList)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示

        ' 共通処理使用の準備
        _comLogc = New CommonLogic(_db, _msgHd)

        _targetKbn = prmTargetKbn                                           '対象区分

        _selected = False                                                   '選択状態リセット

        '初期処理
        initProcess()

    End Sub

#Region "選択情報"

    '選択状態   True:選択状態 False:非選択状態
    Public ReadOnly Property Selected() As String
        Get
            Return _selected
        End Get
    End Property

    '選択した取引先コード
    Public ReadOnly Property GetValTorihikisakiCd() As String
        Get
            Return _selectValTorihikisakiCd
        End Get
    End Property

    '選択した取引先名
    Public ReadOnly Property GetValTorihikisakiName() As String
        Get
            Return _selectValTorihikisakiName
        End Get
    End Property

    '選択した分類
    Public ReadOnly Property GetValBunrui() As String
        Get
            Return _selectValBunrui
        End Get
    End Property

    '選択した住所
    Public ReadOnly Property GetValAddress() As String
        Get
            Return _selectValAddress
        End Get
    End Property

    '選択した電話番号
    Public ReadOnly Property GetValTel() As String
        Get
            Return _selectValTel
        End Get
    End Property

    '選択した備考
    Public ReadOnly Property GetValMemo() As String
        Get
            Return _selectValMemo
        End Get
    End Property

#End Region

#Region "ボタン"

    '検索ボタンクリック時
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            '一覧データを取得
            getTorihikisakiList()

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
            '取引先選択処理
            selectTorihikisaki()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '戻るボタンクリック時
    Private Sub btnModoru_Click(sender As Object, e As EventArgs) Handles btnModoru.Click
        Me.Hide()
    End Sub

#End Region

#Region "コントロールハンドル"

    '一覧セルダブルクリック
    Private Sub dgvLIST_CellDoubleClick(sender As Object, e As EventArgs) Handles dgvList.CellDoubleClick

        Try
            'ヘッダー行ダブルクリックの場合、処理終了
            If TryCast(e, DataGridViewCellEventArgs).RowIndex < 0 Then
                Exit Sub
            End If

            '取引先選択処理
            selectTorihikisaki()

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
                selectTorihikisaki()

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

    '-------------------------------------------------------------------------------
    '　キープレスイベント
    '-------------------------------------------------------------------------------
    Private Sub ctl_KeyPress(sender As Object, e As KeyPressEventArgs) Handles _
                             txtTorihikisakiName.KeyPress,
                             txtTorihikisakiCd.KeyPress
        '押下キーがEnterの場合、次のコントロールへフォーカス移動
        UtilMDL.UtilClass.moveNextFocus(Me, e)
    End Sub

    '-------------------------------------------------------------------------------
    '　フォーカス取得イベント
    '-------------------------------------------------------------------------------
    Private Sub ctl_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
                             txtTorihikisakiName.GotFocus,
                             txtTorihikisakiCd.GotFocus
        'フォーカス取得時、入力パラメタのコントロールを全選択状態とする
        Call UtilClass.selAll(sender)
    End Sub

#End Region

    '初期処理
    '
    Private Sub initProcess()

        '対象区分名の表示
        Select Case _targetKbn
            Case CommonConst.TORIHIKISAKI_TARGET_KBN_SEIKYU
                Me.lblTargetKbnName.Text = TARGET_KBN_DISP_SEIKYU

            Case CommonConst.TORIHIKISAKI_TARGET_KBN_SHUKKA
                Me.lblTargetKbnName.Text = TARGET_KBN_DISP_SHUKKA

            Case CommonConst.TORIHIKISAKI_TARGET_KBN_SHUKKAG
                Me.lblTargetKbnName.Text = TARGET_KBN_DISP_SHUKKAG

            Case CommonConst.TORIHIKISAKI_TARGET_KBN_SHIIRE
                Me.lblTargetKbnName.Text = TARGET_KBN_DISP_SHIIRE

            Case CommonConst.TORIHIKISAKI_TARGET_KBN_SHIHARAI
                Me.lblTargetKbnName.Text = TARGET_KBN_DISP_SHIHARAI

        End Select

    End Sub

    Private Sub getTorihikisakiList()

        '入力取引先コード
        Dim torihikisakiCd As String = Me.txtTorihikisakiCd.Text
        '入力取引先名
        Dim torihikisakiName As String = Me.txtTorihikisakiName.Text

        '取引先データを取得
        Dim ds As DataSet = _comLogc.getTorihikisaki(frmC01F10_Login.loginValue.BumonCD,
                                                     _targetKbn,
                                                     torihikisakiCd,
                                                     torihikisakiName,
                                                     False
                                                    )

        'データカウント
        Dim dataCount As Integer = ds.Tables(RS).Rows.Count

        '描画の前にすべてクリアする
        dgvList.Rows.Clear()

        '抽出データ件数を表示
        txtTotal.Text = dataCount

        '対象データの有無チェック
        If dataCount = 0 Then
            '選択ボタンを非活性
            Me.btnSelect.Enabled = False
            Throw New UsrDefException("対象データがありません。", _msgHd.getMSG("NonData"))
        Else
            '選択ボタンを活性
            Me.btnSelect.Enabled = True
        End If

        '一覧描画
        For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
            dgvList.Rows.Add()
            dgvList.Rows(index).Cells(COLNO_TorihikisakiCD).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("取引先コード"))     '取引先ＣＤ
            dgvList.Rows(index).Cells(COLNO_TorihikisakiNM).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("取引先名"))         '取引先名
            dgvList.Rows(index).Cells(COLNO_BUNRUI).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("分類"))                     '分類
            dgvList.Rows(index).Cells(COLNO_ADDRESS).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("住所"))                    '住所
            dgvList.Rows(index).Cells(COLNO_TEL).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("電話番号"))                    '電話番号
            dgvList.Rows(index).Cells(COLNO_MEMO).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("備考"))                       '備考
        Next

    End Sub

    '取引先選択処理
    Private Sub selectTorihikisaki()

        Dim idx As Integer

        '一覧選択行インデックスの取得
        For Each c As DataGridViewRow In dgvList.SelectedRows
            idx = c.Index
            Exit For
        Next c

        '選択行
        Dim cell As DataGridViewCellCollection = dgvList.Rows(idx).Cells

        '選択行データをセット
        _selectValTorihikisakiCd = cell(COLNO_TorihikisakiCD).Value
        _selectValTorihikisakiName = cell(COLNO_TorihikisakiNM).Value
        _selectValBunrui = cell(COLNO_BUNRUI).Value
        _selectValAddress = cell(COLNO_ADDRESS).Value
        _selectValTel = cell(COLNO_TEL).Value
        _selectValMemo = cell(COLNO_MEMO).Value

        _selected = True        '選択状態

        Me.Hide()

    End Sub

End Class