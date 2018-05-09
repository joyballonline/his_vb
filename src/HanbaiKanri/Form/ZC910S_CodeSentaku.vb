'===============================================================================
'
'　北日本電線株式会社
'　　（システム名）在庫計画システム
'　　（処理機能名）コード選択子画面
'    （フォームID）ZC910S_CodeSentaku
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   中澤        2010/09/01                 新規              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView

Public Class ZC910S_CodeSentaku
    Inherits System.Windows.Forms.Form

#Region "リテラル値定義"

    'PG制御文字
    Private Const RS As String = "RecSet"                       'データセットテーブル名
    Private Const N As String = ControlChars.NewLine            '改行文字

    '一覧データセットバインド列名
    Private Const COLDT_CD As String = "dtCD"           '可変キー
    Private Const COLDT_MEISYO As String = "dtMeisyou"  '名称

    '一覧列名
    Private Const COLCN_CD As String = "cnCD"           '可変キー
    Private Const COLCN_MAISYO As String = "cnMeisyou"  '名称

#End Region

#Region "メンバ変数定義"

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As IfRturnKahenKey

    Dim _dgv As UtilDataGridViewHandler             'グリッドハンドラー

    Private _oldRowIndex As Integer = -1            '選択行の背景色を変更するためのフラグを宣言
    Private _colorCtlFlg As Boolean = False         '選択行の背景色を変更するためのフラグを宣言

    Private _koteiKey As String = ""                '親画面から受け取った固定キー保持変数
    Private _kahenKey As String = ""                '親画面から受け取った可変キー保持変数

    Private _backName As String = ""                '汎用マスタ.需要先は名称2を返すのでここに設定する
    Private _hinsyuKbnFlg As Boolean = False        '品種区分マスタの値を表示するフラグ

    '-->2010.12/12 add by takagi 
    '-------------------------------------------------------------------------------
    '   オーバーライドプロパティで×ボタンだけを無効にする(ControlBoxはTrueのまま使用可能)
    '-------------------------------------------------------------------------------
    Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
        Get
            Const CS_NOCLOSE As Integer = &H200

            Dim tmpCreateParams As System.Windows.Forms.CreateParams = MyBase.CreateParams
            tmpCreateParams.ClassStyle = tmpCreateParams.ClassStyle Or CS_NOCLOSE

            Return tmpCreateParams
        End Get
    End Property
    '<--2010.12/12 add by takagi 

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
    'コンストラクタ　ZM110E_Sinki、ZM120E_Syuuseiなどから呼ばれる
    '   ●入力パラメタ  ：prmRefMsgHd       メッセージハンドラ
    '                     prmRefDbHd        DBハンドラ
    '                     prmForm           呼出元親フォーム
    '                     prmKoteiKey       汎用マスタ固定キー(M02品種区分を検索の場合は需要先CD)
    '                     prmKahenKey       汎用マスタ可変キー(M02品種区分を検索の場合は品種区分)
    '                     prmBackName       汎用マスタ返却値(需要先の場合は名称2を返すのでここで設定。デフォルトは名称1)
    '                     prmHinsyuKbnFlg   検索対象TB判定(True=品種区分マスタ、False=汎用マスタ。デフォルトはFalse)
    '   ●出力パラメタ  ：なし
    '   ●メソッド戻り値：なし
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByVal prmForm As Form, ByVal prmKoteiKey As String, _
                           ByVal prmKahenKey As String, Optional ByVal prmBackName As String = "", Optional ByVal prmHinsyuKbnFlg As Boolean = False)
        Call Me.New()

        '初期処理
        _msgHd = prmRefMsgHd                                    'MSGハンドラの設定
        _db = prmRefDbHd                                        'DBハンドラの設定
        _parentForm = prmForm                                   '親フォーム
        StartPosition = FormStartPosition.CenterScreen          '画面中央表示

        _koteiKey = prmKoteiKey             '親画面から受け取った固定キー
        _kahenKey = prmKahenKey             '親画面から受け取った可変キー
        _backName = prmBackName             '親画面に返す値の判定用変数
        _hinsyuKbnFlg = prmHinsyuKbnFlg     'Trueの場合は品種区分マスタを検索して値を返す

    End Sub

#End Region

#Region "Formイベント"

    '-------------------------------------------------------------------------------
    '　処理開始イベント
    '-------------------------------------------------------------------------------
    Private Sub ZC910S_CodeSentaku_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            '描画関係の設定
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           'サイズが変更されたときに、コントロールがコントロール自体を再描画するかどうかを示す値を設定
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '描画はバッファで実行され、完了後に、結果が画面に出力されるよう設定
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  'コントロールは、画面に直接ではなく、まずバッファに描画されます。これにより、ちらつきを抑えることができます。
            Me.SetStyle(ControlStyles.UserPaint, True)              'コントロールは、オペレーティング システムによってではなく、独自に描画されるよう設定
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   'コントロールはウィンドウ メッセージ WM_ERASEBKGND を無視するように設定

            Dim optionStr As String = ComBiz.getFormTitleOption(_db, _msgHd)
            If Not "".Equals(optionStr) Then Me.Text = Me.Text & " - " & optionStr 'タイトルオプション表示

            '初期値設定
            Call dispDGV()

            _colorCtlFlg = True

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

#End Region

#Region "ボタンイベント"

    '------------------------------------------------------------------------------------------------------
    '選択ボタン押下処理
    '------------------------------------------------------------------------------------------------------
    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        Try
            If "".Equals(lblMeisyo.Text) And "".Equals(lblKahenKey.Text) Then
                Throw New UsrDefException("コードを選択してください。", _msgHd.getMSG("ErrCodeChk"))
            End If

            '親フォームに値渡し
            _parentForm.setKahenKey(lblKahenKey.Text, lblMeisyo.Text)

            '■親フォーム表示
            _parentForm.myShow()
            _parentForm.myActivate()
            Me.Close()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '　戻るボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnModoru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModoru.Click

        '親フォームに値渡し
        _parentForm.setKahenKey("", "")     '親フォームが受け取る可変キーをリセットする
        '■親フォーム表示
        _parentForm.myShow()
        _parentForm.myActivate()
        Me.Close()

    End Sub

#End Region

#Region "ユーザ定義関数:DGV関連"

    '-------------------------------------------------------------------------------
    '　一覧クリック時処理
    '   （処理概要）選択された名称をラベルに表示する。
    '-------------------------------------------------------------------------------
    Private Sub dgvJuyousaki_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvJuyousaki.CellClick

        Try

            Call showDGV(e.RowIndex)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　DGV表示
    '   （処理概要）DGVより選択されたデータをラベルに表示する。
    '-------------------------------------------------------------------------------
    Private Sub showDGV(ByVal prmRowIndex As Integer)
        Try

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvJuyousaki)

            'クリックされたセルの内容をラベルに表示
            lblMeisyo.Text = gh.getCellData(COLDT_MEISYO, prmRowIndex)
            '親画面へ返却する可変キーを非表示ラベルに保持
            lblKahenKey.Text = gh.getCellData(COLDT_CD, prmRowIndex)

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　一覧行移動時の処理
    '　(処理内容)①移動後のセルの内容をラベルに表示する
    '　　　　　　②列に着色する
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvJuyousaki_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvJuyousaki.CellEnter
        Call showDGV(dgvJuyousaki.CurrentCellAddress.Y)

        If _colorCtlFlg Then
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvJuyousaki)
            gh.setSelectionRowColor(dgvJuyousaki.CurrentCellAddress.Y, _oldRowIndex, StartUp.lCOLOR_BLUE)
        End If
        _oldRowIndex = dgvJuyousaki.CurrentCellAddress.Y
    End Sub

#End Region

#Region "ユーザ定義関数:DB関連"

    '------------------------------------------------------------------------------------------------------
    '　一覧表示処理
    '　(処理内容)親画面から渡された固定キーを元に一覧表示を行う
    '------------------------------------------------------------------------------------------------------
    Private Sub dispDGV()

        Try

            Dim sql As String = ""
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = Nothing

            'M01汎用マスタの値を表示する場合
            If Not _hinsyuKbnFlg Then
                sql = "SELECT "
                sql = sql & N & " KAHENKEY " & COLDT_CD                 '可変キー

                '表示する列は需要先が名称2、それ以外が名称1
                '_backNameに検索する列が指定されている場合はその値を返す
                Select Case _backName
                    Case StartUp.HANYO_BACK_NAME1
                        sql = sql & N & " ,NAME1 " & COLDT_MEISYO       '名称
                    Case StartUp.HANYO_BACK_NAME2
                        sql = sql & N & " ,NAME2 " & COLDT_MEISYO
                    Case StartUp.HANYO_BACK_NAME3
                        sql = sql & N & " ,NAME3 " & COLDT_MEISYO
                    Case StartUp.HANYO_BACK_NAME4
                        sql = sql & N & " ,NAME4 " & COLDT_MEISYO
                    Case StartUp.HANYO_BACK_NAME5
                        sql = sql & N & " ,NAME5 " & COLDT_MEISYO
                    Case Else
                        sql = sql & N & " ,NAME1 " & COLDT_MEISYO
                End Select
                sql = sql & N & " FROM M01HANYO "
                sql = sql & N & " WHERE KOTEIKEY = '" & _koteiKey & "'"
                sql = sql & N & " ORDER BY KAHENKEY "

                'M02品種区分マスタの値を表示する場合
            Else
                sql = "SELECT "
                sql = sql & N & "  HINSYUKBN " & COLDT_CD               '品種区分
                sql = sql & N & " ,HINSYUKBNNM " & COLDT_MEISYO         '品種区分名
                sql = sql & N & " FROM M02HINSYUKBN "
                sql = sql & N & " WHERE JUYOUCD = '" & _koteiKey & "'"
                sql = sql & N & " ORDER BY HINSYUKBN "

            End If

            'SQL発行
            ds = _db.selectDB(sql, RS, iRecCnt)
            If iRecCnt <= 0 Then                    '抽出レコードが１件もない場合
                Throw New UsrDefException("登録されていません。", _msgHd.getMSG("noData"))
            End If

            '抽出データを一覧に表示する
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(Me.dgvJuyousaki)
            gh.clearRow()
            dgvJuyousaki.DataSource = ds
            dgvJuyousaki.DataMember = RS

            lblKensu.Text = CStr(iRecCnt) & "件"

            '先頭行着色
            gh.setSelectionRowColor(0, 0, StartUp.lCOLOR_BLUE)

            '親画面から可変キー(品種区分)も渡された場合、一致する一覧行にフォーカスする
            Dim dispCnt As Integer = 0
            If Not "".Equals(_kahenKey) Then
                '渡された可変キー(品種区分)と一覧の可変キー(品種区分)が一致するなら
                For i As Integer = 0 To gh.getMaxRow - 1
                    If _kahenKey.Equals(gh.getCellData(COLDT_CD, i)) Then
                        dispCnt = i
                        Exit For
                    End If
                Next
                'フォーカスを移動し、その行を着色する
                gh.setCurrentCell(COLCN_CD, dispCnt)
                gh.setSelectionRowColor(dispCnt, 0, StartUp.lCOLOR_BLUE)
                _oldRowIndex = dispCnt
            End If

            '現在フォーカスがある行の値をラベルに表示する。
            lblKahenKey.Text = gh.getCellData(COLDT_CD, dispCnt)
            lblMeisyo.Text = gh.getCellData(COLDT_MEISYO, dispCnt)

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

#End Region

    '' 2010/12/15 add start sugano
    Private Sub dgvJuyousaki_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvJuyousaki.CellDoubleClick
        Call btnSelect_Click(sender, e)
    End Sub
    '' 2010/12/15 add end sugano

End Class