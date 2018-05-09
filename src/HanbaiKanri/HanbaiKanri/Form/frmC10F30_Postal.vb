'===============================================================================
'
'　株式会社 全備様
'　　（システム名）販売管理
'　　（処理機能名）郵便番号検索
'    （フォームID）C10F30
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   菅野雄      2018/03/05                 新規             
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB

Public Class frmC10F30_Postal
    Inherits System.Windows.Forms.Form

    ''-------------------------------------------------------------------------------
    ''   定数定義
    ''-------------------------------------------------------------------------------
    'PG制御文字 
    Private Const N As String = ControlChars.NewLine                      '改行文字
    Private Const RS As String = "RecSet"                                 'レコードセットテーブル
    'グリッド列№
    Private Const COLNO_ADDRESS1 = 0                                      '01:都道府県名
    Private Const COLNO_ADDRESS2 = 1                                      '02:市区町村名
    Private Const COLNO_ADDRESS3 = 2                                      '03:町域名

    '-------------------------------------------------------------------------------
    '   変数定義
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _comLogc As CommonLogic                         '共通処理用

    Private _selected As Boolean                            'フォームからの戻り値用　選択状態　True:選択された　False:選択されなかった
    Private _selectValPostalCd1 As String                   'フォームからの戻り値用　郵便番号１
    Private _selectValPostalCd2 As String                   'フォームからの戻り値用　郵便番号２
    Private _selectValAddress As String                     'フォームからの戻り値用　住所

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
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByVal prmPostalCd1 As String, ByVal prmPostalCd2 As String)

        Call Me.New()

        Try
            '初期処理
            _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
            _db = prmRefDbHd                                                    'DBハンドラの設定
            StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
            Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示

            ' 共通処理使用の準備
            _comLogc = New CommonLogic(_db, _msgHd)                             ' 共通処理用

            '選択ボタンを非活性
            Me.btnSelect.Enabled = False

            '渡された郵便番号を画面にセット
            Me.txtPostalCd1.Text = prmPostalCd1
            Me.txtPostalCd2.Text = prmPostalCd2

            _selected = False         '選択状態リセット

            '郵便番号１、２とも入力がある場合
            If (Not ("".Equals(Me.txtPostalCd1.Text))) AndAlso (Not ("".Equals(Me.txtPostalCd2.Text))) Then
                '住所一覧を表示
                Call dispAddressList()
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '選択状態   True:選択状態 False:非選択状態
    Public ReadOnly Property Selected() As String
        Get
            Return _selected
        End Get
    End Property

    '検索時の郵便番号１
    Public ReadOnly Property GetValPostalCd1() As String
        Get
            Return _selectValPostalCd1
        End Get
    End Property

    '検索時の郵便番号２
    Public ReadOnly Property GetValPostalCd2() As String
        Get
            Return _selectValPostalCd2
        End Get
    End Property

    '選択した住所
    Public ReadOnly Property GetValAddress() As String
        Get
            Return _selectValAddress
        End Get
    End Property

    '検索ボタンクリック
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        Try
            '入力チェック
            Call checkInput()

            '住所一覧データを取得
            Call dispAddressList()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '選択ボタンクリック
    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click

        Try
            '住所選択処理
            selectAddress()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '戻るボタンクリック
    Private Sub btnModoru_Click(sender As Object, e As EventArgs) Handles btnModoru.Click
        Me.Hide()
    End Sub

    '一覧セルダブルクリック
    Private Sub dgvList_CellDoubleClick(sender As Object, e As EventArgs) Handles dgvList.CellDoubleClick

        Try
            'ヘッダー行ダブルクリックの場合、処理終了
            If TryCast(e, DataGridViewCellEventArgs).RowIndex < 0 Then
                Exit Sub
            End If

            '住所選択処理
            selectAddress()

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
    Private Sub ctl_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles _
                             txtPostalCd1.KeyPress,
                             txtPostalCd2.KeyPress

        '押下キーがEnterの場合、次のコントロールへフォーカス移動
        Call UtilClass.moveNextFocus(Me, e)

    End Sub

    '-------------------------------------------------------------------------------
    '　フォーカス取得イベント
    '-------------------------------------------------------------------------------
    Private Sub ctl_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
                             txtPostalCd1.GotFocus,
                             txtPostalCd2.GotFocus

        'フォーカス取得時、入力パラメタのコントロールを全選択状態とする
        Call UtilClass.selAll(sender)

    End Sub

    '入力チェック
    Private Sub checkInput()

        '郵便番号１
        If "".Equals(Me.txtPostalCd1.Text) Then
            Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", ""), Me.txtPostalCd1)
        End If

        '郵便番号２
        If "".Equals(Me.txtPostalCd2.Text) Then
            Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", ""), Me.txtPostalCd2)
        End If

    End Sub

    '住所一覧を表示
    Private Sub dispAddressList()

        '入力郵便番号
        Dim strPostalCd As String = Me.txtPostalCd1.Text & Me.txtPostalCd2.Text

        '住所データを取得
        Dim ds As DataSet = _comLogc.getAddress(strPostalCd)

        '検索時の郵便番号を保持
        _selectValPostalCd1 = Me.txtPostalCd1.Text
        _selectValPostalCd2 = Me.txtPostalCd2.Text

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
        For index As Integer = 0 To dataCount - 1
            dgvList.Rows.Add()
            dgvList.Rows(index).Cells(COLNO_ADDRESS1).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("都道府県名"))
            dgvList.Rows(index).Cells(COLNO_ADDRESS2).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("市区町村名"))
            dgvList.Rows(index).Cells(COLNO_ADDRESS3).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("町域名"))
        Next

    End Sub

    '住所選択処理
    Private Sub selectAddress()

        Dim idx As Integer

        '一覧選択行インデックスの取得
        For Each c As DataGridViewRow In dgvList.SelectedRows
            idx = c.Index
            Exit For
        Next c

        '選択行
        Dim cell As DataGridViewCellCollection = dgvList.Rows(idx).Cells

        '選択行の住所(１～３)をセット
        _selectValAddress = cell(COLNO_ADDRESS1).Value &
                            cell(COLNO_ADDRESS2).Value &
                            cell(COLNO_ADDRESS3).Value

        _selected = True        '選択状態

        Me.Hide()

    End Sub

End Class