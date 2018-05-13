'===============================================================================
'
'　北日本電線株式会社
'　　（システム名）在庫計画システム
'　　（処理機能名）手配データ修正(詳細)
'    （フォームID）ZG621E_TehaiSyuuseiSyousai
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   橋本        2010/10/22                 新規    
'　(2)   中澤        2010/11/17                 変更(項目「納期」削除対応)    
'　(3)   中澤        2010/12/02                 変更(対象外が親画面に反映されないバグ修正)  
'                                               変更(手配数量の自動計算処理追加)
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.Combo

Public Class ZG621E_TehaiSyuuseiSyousai
    Inherits System.Windows.Forms.Form

#Region "リテラル値定義"

    'PG制御文字
    Private Const RS As String = "RecSet"                       'データセットテーブル名
    Private Const N As String = ControlChars.NewLine            '改行文字

    '一覧データバインド名
    Private Const COLDT_TEHAINO As String = "dtTehaiNo"             '手配№
    Private Const COLDT_SYUTTAIBI As String = "dtSyuttaibi"         '希望出来日
    '2010/11/17 delete start Nakazawa
    'Private Const COLDT_NOUKI As String = "dtNouki"                 '納期
    '2010/11/17 delete end Nakazawa
    Private Const COLDT_TEHAIKBN As String = "dtTehaikbn"           '手配区分"
    Private Const COLDT_SEISAKU As String = "dtSeisaku"             '製作区分
    Private Const COLDT_SEIZOUBMN As String = "dtSeizoubmn"         '製造部門
    Private Const COLDT_TYUMONSAKI As String = "dtTyumonsaki"       '注文先
    Private Const COLDT_HINMEICD As String = "dtHinmeiCD"           '品名コード
    Private Const COLDT_HINMEI As String = "dtHinmei"               '注文品名
    Private Const COLDT_TEHAISUURYOU As String = "dtTehaiSuuryou"   '手配数料量
    Private Const COLDT_SIYOUSYONO As String = "dtSiyousyoNo"       '仕様書番号
    Private Const COLDT_TANTYOU As String = "dtTantyou"             '単長
    Private Const COLDT_JOUSUU As String = "dtJousuu"               '条数
    Private Const COLDT_MAKIWAKU As String = "dtMakiwaku"           '巻枠コード
    Private Const COLDT_MAKIWAKUMEI As String = "dtMakiwakuMei"     '巻枠名
    Private Const COLDT_HOUSOU As String = "dtHousou"               '包装/表示区分
    Private Const COLDT_HOUSOUTYPE As String = "dtHoousoutype"      '包装/表示種類
    Private Const COLDT_KSUU As String = "dtKsuu"                   'K本数
    Private Const COLDT_SHSUU As String = "dtShsuu"                 'S本数
    Private Const COLDT_GAICYUSAKI As String = "dtGaicyusaki"       '外注先
    Private Const COLDT_CYUMONBI As String = "dtCyumonbi"           '注文日
    Private Const COLDT_NYUKABI As String = "dtNyukabi"             '入荷日
    Private Const COLDT_KAMOKUCD As String = "dtKamokuCd"           '科目コード
    Private Const COLDT_CYUMONNO As String = "dtcyumonNo"           '注文№
    Private Const COLDT_TOKKI As String = "dtTokki"                 '特記事項
    Private Const COLDT_BIKO As String = "dtBiko"                   '備考
    Private Const COLDT_HENKO As String = "dtHenko"                 '変更内容
    Private Const COLDT_TENKAIKBN As String = "dtTenkaikbn"         '展開区分
    Private Const COLDT_BBNKOUTEI As String = "dtBbnkoutei"         '部分展開指定工程
    Private Const COLDT_HINSITUKBN As String = "dtHinsitukbn"       '品質試験区分
    Private Const COLDT_KEISANKBN As String = "dtKeisankbn"         '加工長計算区分
    Private Const COLDT_TATIAIUM As String = "dtTatiaium"           '立会有無
    Private Const COLDT_TACIAIBI As String = "dtTaciaibi"           '立会予定日
    Private Const COLDT_TAISYOGAI As String = "dtTaisyogai"         '対象外

    '固定キー
    Private Const COTEI_TEHAI As String = "02"                  '手配区分
    Private Const COTEI_SEISAKU As String = "03"                '作成区分
    Private Const COTEI_TENKAI As String = "04"                 '展開区分
    Private Const COTEI_KAKOU As String = "05"                  '加工長計算
    Private Const COTEI_TATIAI As String = "06"                 '立会有無
    Private Const COTEI_HINSHITSU As String = "08"              '品質試験区分
    Private Const COTEI_SEIZOU As String = "09"                 '製造部門

    'プログラムID（T91実行履歴テーブル登録用）
    Private Const PGID As String = "ZG620E"

    '更新件数
    Private Const UPDATECOUNT As Integer = 1

    '対象外フラグ
    Private Const TAISYO_ARI As String = "1"
    Private Const TAISYO_GAI As String = ""

    '製作区分
    Private Const SEISAKU_NAI As String = "1"    '内作
    Private Const SEISAKU_GAI As String = "2"    '外注

    '展開区分
    Private Const TENKAI_ALL As String = "1"
    Private Const TENKAI_POINT As String = "2"

    '品質試験区分
    Private Const HINSITU_YOTYO As String = "0"
    Private Const HINSITU_LOT As String = "2"

    '立会区分
    Private Const TACHIAI_NASI As String = "1"
    Private Const TACHIAI_ARI As String = "2"

    '特記事項文言
    Private Const TOKKI_WORD As String = "ﾆｭｳｺﾌﾘﾜｹ"
    Private Const TOKKI_WORD_K As String = " K:"
    Private Const TOKKI_WORD_S As String = " S:"


#End Region

#Region "メンバ変数定義"

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As IfRturnUpDateData
    Private _menuForm As ZC110M_Menu

    Private _koteiKey As String
    Private _Syori As String
    Private _Keikaku As String

    Private _changeFlg As Boolean = False           '変更フラグ
    Private _beforeChange As String = ""            '変更前のデータ
    Private _updFlg As Boolean = False

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
    'コンストラクタ　メニューから呼ばれる
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByVal prmForm As Form, ByVal prmKoteiKey As String, ByVal prmSyori As String, ByVal prmKeikaku As String, ByVal prmUpdFlg As Boolean, ByVal prmMenuForm As ZC110M_Menu)
        Call Me.New()

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _parentForm = prmForm                                               '親フォーム
        _koteiKey = prmKoteiKey                                             '親から受け取った固定キー
        _Syori = prmSyori                                                   '親から受け取った処理年月
        _Keikaku = prmKeikaku                                               '親から受け取った計画年月
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        _updFlg = prmUpdFlg
        _menuForm = prmMenuForm
    End Sub

#End Region

#Region "Formイベント"

    '-------------------------------------------------------------------------------
    '　処理開始イベント
    '-------------------------------------------------------------------------------
    Private Sub ZG621E_TehaiSyuuseiSyousai_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            '描画関係の設定
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           'サイズが変更されたときに、コントロールがコントロール自体を再描画するかどうかを示す値を設定
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '描画はバッファで実行され、完了後に、結果が画面に出力されるよう設定
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  'コントロールは、画面に直接ではなく、まずバッファに描画されます。これにより、ちらつきを抑えることができます。
            Me.SetStyle(ControlStyles.UserPaint, True)              'コントロールは、オペレーティング システムによってではなく、独自に描画されるよう設定
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   'コントロールはウィンドウ メッセージ WM_ERASEBKGND を無視するように設定

            'タイトルオプション表示
            Dim optionStr As String = ComBiz.getFormTitleOption(_db, _msgHd)
            If Not "".Equals(optionStr) Then Me.Text = Me.Text & " - " & optionStr

            '画面表示
            Call initForm()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

#End Region

#Region "ボタンイベント"

    '------------------------------------------------------------------------------------------------------
    '　戻るボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnModoru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModoru.Click
        Try
            '警告メッセージ
            If _changeFlg Then
                Dim rtn As DialogResult = _msgHd.dspMSG("confirmDgvEdit")   '編集中の内容が破棄されます。よろしいですか？
                If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If
            
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
    '　更新ボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnTouroku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKousin.Click
        Try

            '必須入力チェック
            Call checkTouroku()

            '登録確認メッセージ
            Dim rtn As DialogResult = _msgHd.dspMSG("confirmInsert")    '登録します。
            If rtn <> Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If

            ' 元のWaitカーソルを保持
            Dim preCursor As Cursor = Me.Cursor
            ' カーソルを待機カーソルに変更
            Me.Cursor = Cursors.WaitCursor
            Try
                '処理開始時間と端末IDの取得
                Dim dStartSysdate As Date = Now()                           '処理開始日時
                Dim sPCName As String = UtilClass.getComputerName           '端末ID
                Dim dtkibou As String = ""
                '2010/11/17 delete start Nakazawa
                'Dim dtnouki As String = ""
                '2010/11/17 delete end Nakazawa
                Dim dttehaisuuryou As String = ""
                Dim dttantyou As String = ""
                Dim dtjyusuu As String = ""
                Dim dtsiyousyono As String = ""

                'トランザクション開始
                _db.beginTran()

                '手配テーブルの更新処理
                Call UpdateT51TEHAI(dStartSysdate, sPCName)

                '処理終了日時の取得
                Dim dFinishSysdate As Date = Now()

                '実行履歴テーブルの更新処理
                Call updT91Rireki(_koteiKey, sPCName, dStartSysdate, dFinishSysdate)

                'T02処理制御テーブル更新
                _menuForm.updateSeigyoTbl(PGID, True, dStartSysdate, dFinishSysdate)

                'トランザクション終了
                _db.commitTran()

                '更新内容反映
                If Not "".Equals(Trim(Replace(txtKibou.Text, "/", ""))) Then
                    dtkibou = txtKibou.Text.Substring(2, 8) '希望出来日
                End If
                '2010/11/17 delete start Nakazawa
                'If Not "".Equals(Trim(Replace(txtNouki.Text, "/", ""))) Then
                '    dtnouki = txtNouki.Text.Substring(2, 8) '納期
                'End If
                '2010/11/17 delete end Nakazawa
                dttehaisuuryou = txtTehaiSuuryou.Value      '手配数量
                dttantyou = txtTantyou.Text                 '単長
                dtjyusuu = txtJousuu.Value                  '条数
                dtsiyousyono = txtSiyousyoNo.Text           '仕様書番号


                '2010/12/02 add start Nakazawa
                Dim taisyougaiFlg As Boolean = False        '対象外チェック用フラグ
                If chktaisyogai.Checked Then
                    taisyougaiFlg = True
                End If
                '2010/12/02 add end Nakazawa

                '親フォームに値渡し
                '2010/12/02 update start Nakazawa---
                '2010/11/17 update start Nakazawa
                '_parentForm.setUpDateData(dtkibou, dtnouki, dttehaisuuryou, dttantyou, dtjyusuu, dtsiyousyono)
                '_parentForm.setUpDateData(dtkibou, dttehaisuuryou, dttantyou, dtjyusuu, dtsiyousyono)
                '2010/11/17 update end Nakazawa
                _parentForm.setUpDateData(dtkibou, dttehaisuuryou, dttantyou, dtjyusuu, dtsiyousyono, taisyougaiFlg)
                '2010/12/02 update end Nakazawa---

                '完了メッセージ
                _msgHd.dspMSG("completeInsert")

                '変更フラグを無効にする
                _changeFlg = False

                '■親フォーム表示
                _parentForm.myShow()
                _parentForm.myActivate()
                Me.Close()

            Finally
                If _db.isTransactionOpen = True Then
                    _db.rollbackTran()                          'ロールバック
                End If
                ' カーソルを元に戻す
                Me.Cursor = preCursor
            End Try

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

#End Region

#Region "ユーザ定義関数:画面制御"
    '-------------------------------------------------------------------------------
    '　画面起動時
    '-------------------------------------------------------------------------------
    Private Sub initForm()
        Try
            'コンボボックス
            Call setCbo(cboSeisakuKbn, COTEI_SEISAKU)           '作成区分
            Call setCbo(cboTenkaiKbn, COTEI_TENKAI)             '展開区分
            Call setCbo(cboHinsituKbn, COTEI_HINSHITSU)         '品質試験区分
            Call setCbo(cboTachiai, COTEI_TATIAI)               '立会有無

            '画面表示
            Call dispFrom(_koteiKey)

            btnKousin.Enabled = _updFlg

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　コントロールキー押下イベント
    '　(処理概要)エンターボタン押下時に次のコントロールに移る
    '-------------------------------------------------------------------------------
    '2010/11/17 update start Nakazawa
    'Private Sub txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtKibou.KeyPress, _
    '                                                                                                            txtNouki.KeyPress, _
    '                                                                                                            cboSeisakuKbn.KeyPress, _
    '                                                                                                            txtTehaiSuuryou.KeyPress, _
    '                                                                                                            txtSiyousyoNo.KeyPress, _
    '                                                                                                            txtTantyou.KeyPress, _
    '                                                                                                            txtJousuu.KeyPress, _
    '                                                                                                            txtMakiwaku.KeyPress, _
    '                                                                                                            txtHousou.KeyPress, _
    '                                                                                                            txtKHonsuu.KeyPress, _
    '                                                                                                            txtSHonsuu.KeyPress, _
    '                                                                                                            txtBikou.KeyPress, _
    '                                                                                                            txtGaicyusaki.KeyPress, _
    '                                                                                                            txtCyumonbi.KeyPress, _
    '                                                                                                            txtNyukabi.KeyPress, _
    '                                                                                                            txtKamoku.KeyPress, _
    '                                                                                                            txtCyumonno.KeyPress, _
    '                                                                                                            txtTokki1.KeyPress, _
    '                                                                                                            txtTokki2.KeyPress, _
    '                                                                                                            txtTokki3.KeyPress, _
    '                                                                                                            txtHenkouNaiyou.KeyPress, _
    '                                                                                                            cboTenkaiKbn.KeyPress, _
    '                                                                                                            txtBubunTenkai.KeyPress, _
    '                                                                                                            cboHinsituKbn.KeyPress, _
    '                                                                                                            cboTachiai.KeyPress, _
    '                                                                                                            chktaisyogai.KeyPress
    Private Sub txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtKibou.KeyPress, _
                                                                                                                cboSeisakuKbn.KeyPress, _
                                                                                                                txtTehaiSuuryou.KeyPress, _
                                                                                                                txtSiyousyoNo.KeyPress, _
                                                                                                                txtTantyou.KeyPress, _
                                                                                                                txtJousuu.KeyPress, _
                                                                                                                txtMakiwaku.KeyPress, _
                                                                                                                txtHousou.KeyPress, _
                                                                                                                txtKHonsuu.KeyPress, _
                                                                                                                txtSHonsuu.KeyPress, _
                                                                                                                txtBikou.KeyPress, _
                                                                                                                txtGaicyusaki.KeyPress, _
                                                                                                                txtCyumonbi.KeyPress, _
                                                                                                                txtNyukabi.KeyPress, _
                                                                                                                txtKamoku.KeyPress, _
                                                                                                                txtCyumonno.KeyPress, _
                                                                                                                txtTokki1.KeyPress, _
                                                                                                                txtTokki2.KeyPress, _
                                                                                                                txtTokki3.KeyPress, _
                                                                                                                txtHenkouNaiyou.KeyPress, _
                                                                                                                cboTenkaiKbn.KeyPress, _
                                                                                                                txtBubunTenkai.KeyPress, _
                                                                                                                cboHinsituKbn.KeyPress, _
                                                                                                                cboTachiai.KeyPress, _
                                                                                                                chktaisyogai.KeyPress
        '2010/11/17 update end Nakazawa

        Try
            '次のコントロールへ移動する
            UtilClass.moveNextFocus(Me, e)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　コントロールキーフォーカス取得イベント
    '　(処理概要)フォーカス取得時、全選択状態にする
    '-------------------------------------------------------------------------------
    '2010/11/17 update start Nakazawa
    'Private Sub txt_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtKibou.GotFocus, _
    '                                                                                      txtNouki.GotFocus, _
    '                                                                                      cboSeisakuKbn.GotFocus, _
    '                                                                                      txtTehaiSuuryou.GotFocus, _
    '                                                                                      txtSiyousyoNo.GotFocus, _
    '                                                                                      txtTantyou.GotFocus, _
    '                                                                                      txtJousuu.GotFocus, _
    '                                                                                      txtMakiwaku.GotFocus, _
    '                                                                                      txtHousou.GotFocus, _
    '                                                                                      txtKHonsuu.GotFocus, _
    '                                                                                      txtSHonsuu.GotFocus, _
    '                                                                                      txtBikou.GotFocus, _
    '                                                                                      txtGaicyusaki.GotFocus, _
    '                                                                                      txtCyumonbi.GotFocus, _
    '                                                                                      txtNyukabi.GotFocus, _
    '                                                                                      txtKamoku.GotFocus, _
    '                                                                                      txtCyumonno.GotFocus, _
    '                                                                                      txtTokki1.GotFocus, _
    '                                                                                      txtTokki2.GotFocus, _
    '                                                                                      txtTokki3.GotFocus, _
    '                                                                                      txtHenkouNaiyou.GotFocus, _
    '                                                                                      cboTenkaiKbn.GotFocus, _
    '                                                                                      txtBubunTenkai.GotFocus, _
    '                                                                                      cboHinsituKbn.GotFocus, _
    '                                                                                      cboTachiai.GotFocus
    Private Sub txt_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtKibou.GotFocus, _
                                                                                          cboSeisakuKbn.GotFocus, _
                                                                                          txtTehaiSuuryou.GotFocus, _
                                                                                          txtSiyousyoNo.GotFocus, _
                                                                                          txtTantyou.GotFocus, _
                                                                                          txtJousuu.GotFocus, _
                                                                                          txtMakiwaku.GotFocus, _
                                                                                          txtHousou.GotFocus, _
                                                                                          txtKHonsuu.GotFocus, _
                                                                                          txtSHonsuu.GotFocus, _
                                                                                          txtBikou.GotFocus, _
                                                                                          txtGaicyusaki.GotFocus, _
                                                                                          txtCyumonbi.GotFocus, _
                                                                                          txtNyukabi.GotFocus, _
                                                                                          txtKamoku.GotFocus, _
                                                                                          txtCyumonno.GotFocus, _
                                                                                          txtTokki1.GotFocus, _
                                                                                          txtTokki2.GotFocus, _
                                                                                          txtTokki3.GotFocus, _
                                                                                          txtHenkouNaiyou.GotFocus, _
                                                                                          cboTenkaiKbn.GotFocus, _
                                                                                          txtBubunTenkai.GotFocus, _
                                                                                          cboHinsituKbn.GotFocus, _
                                                                                          cboTachiai.GotFocus
        '2010/11/17 update end Nakazawa

        Try
            '全選択状態にする
            UtilClass.selAll(sender)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　テキストボックスコントロールのフォーカス取得時イベント
    '　(処理概要)テキストボックスコントロールのフォーカス取得時の処理
    '-------------------------------------------------------------------------------
    '2010/11/17 update start Nakazawa
    'Private Sub txt_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtKibou.Enter, _
    '                                                                                      txtNouki.Enter, _
    '                                                                                      txtTehaiSuuryou.Enter, _
    '                                                                                      txtSiyousyoNo.Enter, _
    '                                                                                      txtTantyou.Enter, _
    '                                                                                      txtJousuu.Enter, _
    '                                                                                      txtMakiwaku.Enter, _
    '                                                                                      txtHousou.Enter, _
    '                                                                                      txtKHonsuu.Enter, _
    '                                                                                      txtSHonsuu.Enter, _
    '                                                                                      txtBikou.Enter, _
    '                                                                                      txtGaicyusaki.Enter, _
    '                                                                                      txtCyumonbi.Enter, _
    '                                                                                      txtNyukabi.Enter, _
    '                                                                                      txtKamoku.Enter, _
    '                                                                                      txtCyumonno.Enter, _
    '                                                                                      txtTokki2.Enter, _
    '                                                                                      txtTokki3.Enter, _
    '                                                                                      txtHenkouNaiyou.Enter, _
    '                                                                                      txtBubunTenkai.Enter
    Private Sub txt_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtKibou.Enter, _
                                                                                        txtTehaiSuuryou.Enter, _
                                                                                        txtSiyousyoNo.Enter, _
                                                                                        txtTantyou.Enter, _
                                                                                        txtJousuu.Enter, _
                                                                                        txtMakiwaku.Enter, _
                                                                                        txtHousou.Enter, _
                                                                                        txtKHonsuu.Enter, _
                                                                                        txtSHonsuu.Enter, _
                                                                                        txtBikou.Enter, _
                                                                                        txtGaicyusaki.Enter, _
                                                                                        txtCyumonbi.Enter, _
                                                                                        txtNyukabi.Enter, _
                                                                                        txtKamoku.Enter, _
                                                                                        txtCyumonno.Enter, _
                                                                                        txtTokki2.Enter, _
                                                                                        txtTokki3.Enter, _
                                                                                        txtHenkouNaiyou.Enter, _
                                                                                        txtBubunTenkai.Enter
        '2010/11/17 update end Nakazawa
        Try
            '既に変更フラグが立っている場合は何も行わない
            If _changeFlg = False Then
                If sender.GetType Is GetType(TextBox) Then
                    Dim txt As TextBox = DirectCast(sender, TextBox)
                    _beforeChange = txt.Text
                ElseIf sender.GetType Is GetType(CustomControl.TextBoxNum) Then
                    Dim txtnum As CustomControl.TextBoxNum = DirectCast(sender, CustomControl.TextBoxNum)
                    _beforeChange = txtnum.Value
                ElseIf sender.GetType Is GetType(CustomControl.TextBoxDate) Then
                    Dim txtdate As CustomControl.TextBoxDate = DirectCast(sender, CustomControl.TextBoxDate)
                    _beforeChange = txtdate.Text
                End If
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　テキストボックスコントロールのフォーカス消失時イベント
    '　(処理概要)テキストコントロールのフォーカス消失時の処理
    '-------------------------------------------------------------------------------
    '2010/11/17 update start Nakazawa
    'Private Sub txt_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtKibou.Leave, _
    '                                                                                          txtNouki.Leave, _
    '                                                                                          txtTehaiSuuryou.Leave, _
    '                                                                                          txtSiyousyoNo.Leave, _
    '                                                                                          txtTantyou.Leave, _
    '                                                                                          txtJousuu.Leave, _
    '                                                                                          txtMakiwaku.Leave, _
    '                                                                                          txtHousou.Leave, _
    '                                                                                          txtKHonsuu.Leave, _
    '                                                                                          txtSHonsuu.Leave, _
    '                                                                                          txtBikou.Leave, _
    '                                                                                          txtGaicyusaki.Leave, _
    '                                                                                          txtCyumonbi.Leave, _
    '                                                                                          txtNyukabi.Leave, _
    '                                                                                          txtKamoku.Leave, _
    '                                                                                          txtCyumonno.Leave, _
    '                                                                                          txtTokki2.Leave, _
    '                                                                                          txtTokki3.Leave, _
    '                                                                                          txtHenkouNaiyou.Leave, _
    '                                                                                          txtBubunTenkai.Leave
    Private Sub txt_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtKibou.Leave, _
                                                                                              txtTehaiSuuryou.Leave, _
                                                                                              txtSiyousyoNo.Leave, _
                                                                                              txtTantyou.Leave, _
                                                                                              txtJousuu.Leave, _
                                                                                              txtMakiwaku.Leave, _
                                                                                              txtHousou.Leave, _
                                                                                              txtKHonsuu.Leave, _
                                                                                              txtSHonsuu.Leave, _
                                                                                              txtBikou.Leave, _
                                                                                              txtGaicyusaki.Leave, _
                                                                                              txtCyumonbi.Leave, _
                                                                                              txtNyukabi.Leave, _
                                                                                              txtKamoku.Leave, _
                                                                                              txtCyumonno.Leave, _
                                                                                              txtTokki2.Leave, _
                                                                                              txtTokki3.Leave, _
                                                                                              txtHenkouNaiyou.Leave, _
                                                                                              txtBubunTenkai.Leave
        '2010/11/17 update end Nakazawa

        Try

            '編集前と値が変わっていた場合、フラグを立てる
            If sender.GetType Is GetType(TextBox) Then
                Dim txt As TextBox = DirectCast(sender, TextBox)
                If Not _beforeChange.Equals(txt.Text) Then
                    _changeFlg = True
                End If
                Select Case txt.Name
                    Case txtMakiwaku.Name
                        '巻枠コードの場合
                        If "".Equals(txtMakiwaku.Text) Then
                            '巻枠名クリア
                            lblMakiwakumei.Text = ""
                        Else
                            '巻枠名表示
                            Call dispMakiwakuName(txtMakiwaku.Text)
                        End If

                    Case txtHousou.Name
                        '包装/表示区分の場合
                        If "".Equals(txtHousou.Text) Then
                            '包装/表示区分種類クリア
                            lblHousouSyurui.Text = ""
                        Else
                            '包装/表示区分種類表示
                            Call dispHousouType(txtHousou.Text)
                        End If

                    Case txtGaicyusaki.Name
                    Case txtKamoku.Name
                    Case txtCyumonno.Name
                        '外注情報テキストボックスの場合
                        '特記事項２、３編集
                        txtTokki2.Text = txtGaicyusaki.Text.PadRight(10) & Strings.Right(Trim(Replace(txtCyumonbi.Text, "/", "")), 6) & Strings.Right(Trim(Replace(txtNyukabi.Text, "/", "")), 6)
                        txtTokki3.Text = txtKamoku.Text.PadRight(16) & txtCyumonno.Text.PadLeft(6)
                End Select
            ElseIf sender.GetType Is GetType(CustomControl.TextBoxNum) Then
                Dim txtnum As CustomControl.TextBoxNum = DirectCast(sender, CustomControl.TextBoxNum)
                If Not _beforeChange.Equals(txtnum.Text) Then
                    _changeFlg = True
                End If

                '数値テキストボックスが未入力の場合、初期化する
                If "".Equals(txtnum.Value) Then
                    txtnum.Value = "0"
                End If

                Select Case txtnum.Name
                    Case txtJousuu.Name
                        '条数テキストボックスの場合
                        'K本数再計算
                        txtKHonsuu.Value = (Integer.Parse(txtJousuu.Value) - Integer.Parse(txtSHonsuu.Value)).ToString

                    Case txtKHonsuu.Name
                        'K本数テキストボックスの場合
                        'S本数再計算
                        txtSHonsuu.Value = (Integer.Parse(txtJousuu.Value) - Integer.Parse(txtKHonsuu.Value)).ToString
                        '特記記事１編集
                        If Integer.Parse(txtJousuu.Value) < Integer.Parse(txtKHonsuu.Value) + Integer.Parse(txtSHonsuu.Value) Then
                            txtTokki1.Text = ""
                        Else
                            txtTokki1.Text = TOKKI_WORD & TOKKI_WORD_K & txtKHonsuu.Value & TOKKI_WORD_S & txtSHonsuu.Value
                        End If

                    Case txtSHonsuu.Name
                        'S本数テキストボックスの場合
                        'K本数再計算
                        txtKHonsuu.Value = (Integer.Parse(txtJousuu.Value) - Integer.Parse(txtSHonsuu.Value)).ToString
                        '特記記事１編集
                        If Integer.Parse(txtJousuu.Value) < Integer.Parse(txtKHonsuu.Value) + Integer.Parse(txtSHonsuu.Value) Then
                            txtTokki1.Text = ""
                        Else
                            txtTokki1.Text = TOKKI_WORD & TOKKI_WORD_K & txtKHonsuu.Value & TOKKI_WORD_S & txtSHonsuu.Value
                        End If
                End Select

            ElseIf sender.GetType Is GetType(CustomControl.TextBoxDate) Then
                Dim txtdate As CustomControl.TextBoxDate = DirectCast(sender, CustomControl.TextBoxDate)
                If Not _beforeChange.Equals(txtdate.Text) Then
                    _changeFlg = True
                End If
                Select Case txtdate.Name
                    Case txtCyumonbi.Name
                    Case txtNyukabi.Name
                        '外注情報日付テキストボックスの場合
                        '特記事項２、３編集
                        txtTokki2.Text = txtGaicyusaki.Text.PadRight(10) & Strings.Right(Trim(Replace(txtCyumonbi.Text, "/", "")), 6) & Strings.Right(Trim(Replace(txtNyukabi.Text, "/", "")), 6)
                        txtTokki3.Text = txtKamoku.Text.PadRight(16) & txtCyumonno.Text.PadLeft(6)
                End Select
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　製作区分コンボボックス変更時のイベント
    '　(処理概要)製作区分コンボボックスの選択内容によってコントロールの状態を変更する
    '-------------------------------------------------------------------------------
    Private Sub cboSeisakuKbn_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSeisakuKbn.SelectedIndexChanged
        Try
            Dim ch1 As UtilComboBoxHandler = New UtilComboBoxHandler(cboSeisakuKbn)
            Dim ch2 As UtilComboBoxHandler = New UtilComboBoxHandler(cboTenkaiKbn)
            Dim ch3 As UtilComboBoxHandler = New UtilComboBoxHandler(cboHinsituKbn)
            '-->2010.12.27 add by takagi #54
            txtGaicyusaki.Text = ""
            txtCyumonbi.Text = ""
            txtNyukabi.Text = ""
            txtKamoku.Text = ""
            txtCyumonno.Text = ""
            txtTokki1.Text = ""
            txtTokki2.Text = ""
            txtTokki3.Text = ""
            '<--2010.12.27 add by takagi #54
            If ch1.getCode.Equals(SEISAKU_NAI) Then
                '製作区分が="1"(内作)の場合
                grpGaicyu.Enabled = False
                txtTokki1.Enabled = False
                '-->2010.12.27 chg by takagi 
                'txtTokki1.BackColor = Color.FromArgb(255, 255, 192)
                txtTokki1.BackColor = StartUp.lCOLOR_YELLOW
                '<--2010.12.27 chg by takagi 
                txtTokki2.Enabled = True
                '-->2010.12.27 chg by takagi 
                'txtTokki2.BackColor = Color.FromKnownColor(KnownColor.Window)
                txtTokki2.BackColor = StartUp.lCOLOR_WHITE
                '<--2010.12.27 chg by takagi 
                txtTokki3.Enabled = True
                '-->2010.12.27 chg by takagi 
                'txtTokki3.BackColor = Color.FromKnownColor(KnownColor.Window)
                txtTokki3.BackColor = StartUp.lCOLOR_WHITE
                '<--2010.12.27 chg by takagi 
                ch2.selectItem(TENKAI_ALL)
                ch3.selectItem(HINSITU_LOT)
            Else
                '製作区分が="2"(外注)の場合
                grpGaicyu.Enabled = True
                txtTokki1.Enabled = True
                txtTokki1.BackColor = Color.FromKnownColor(KnownColor.Window)
                txtTokki2.Enabled = False
                '-->2010.12.27 chg by takagi 
                'txtTokki2.BackColor = Color.FromArgb(255, 255, 192)
                txtTokki2.BackColor = StartUp.lCOLOR_YELLOW
                '<--2010.12.27 chg by takagi 
                txtTokki3.Enabled = False
                '-->2010.12.27 chg by takagi 
                'txtTokki3.BackColor = Color.FromArgb(255, 255, 192)
                txtTokki3.BackColor = StartUp.lCOLOR_YELLOW
                '<--2010.12.27 chg by takagi 
                ch2.selectItem(TENKAI_POINT)
                ch3.selectItem(HINSITU_YOTYO)
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　展開区分コンボボックス変更時のイベント
    '　(処理概要)展開区分コンボボックスの選択内容によってコントロールの状態を変更する
    '-------------------------------------------------------------------------------
    Private Sub cboTenkaiKbn_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTenkaiKbn.TextChanged
        Try
            Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(cboTenkaiKbn)
            If ch.getCode.Equals(TENKAI_ALL) Then
                '展開区分="1"(全展開)の場合
                txtBubunTenkai.Clear()
                txtBubunTenkai.Enabled = False
                txtBubunTenkai.BackColor = Color.FromArgb(255, 255, 192)
            Else
                '展開区分="2"(部分展開)の場合
                txtBubunTenkai.Enabled = True
                txtBubunTenkai.BackColor = Color.FromKnownColor(KnownColor.Window)
            End If
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　立会有無コンボボックス変更時のイベント
    '　(処理概要)立会有無コンボボックスの選択内容によってコントロールの状態を変更する
    '-------------------------------------------------------------------------------
    Private Sub cboTachiai_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTachiai.SelectedIndexChanged
        Try
            Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(cboTachiai)
            If ch.getCode.Equals(TACHIAI_NASI) Then
                '立会有無="1"(ナシ)の場合
                txtTachiaibi.Text = ""
                txtTachiaibi.Enabled = False
                txtTachiaibi.BackColor = Color.FromArgb(255, 255, 192)
            Else
                '立会有無="2"(アリ)の場合
                txtTachiaibi.Enabled = True
                txtTachiaibi.BackColor = Color.FromKnownColor(KnownColor.Window)
            End If
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　コンボボックスコントロールのフォーカス取得時イベント
    '　(処理概要)コンボボックスコントロールのフォーカス取得時の処理
    '-------------------------------------------------------------------------------
    Private Sub cbo_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSeisakuKbn.Enter, _
                                                                                              cboTenkaiKbn.Enter, _
                                                                                              cboHinsituKbn.Enter, _
                                                                                              cboTachiai.Enter
        Try

            '既に変更フラグが立っている場合は何も行わない
            If _changeFlg = False Then
                Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(sender)
                _beforeChange = ch.getCode()
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　コンボボックスコントロールのフォーカス消失時イベント
    '　(処理概要)コンボボックスコントロールのフォーカス消失時の処理
    '-------------------------------------------------------------------------------
    Private Sub cbo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSeisakuKbn.Leave, _
                                                                                              cboTenkaiKbn.Leave, _
                                                                                              cboHinsituKbn.Leave, _
                                                                                              cboTachiai.Leave
        Try
            If _changeFlg = False Then
                '編集前と値が変わっていた場合、フラグを立てる
                Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(sender)
                If Not _beforeChange.Equals(ch.getCode()) Then
                    _changeFlg = True
                End If
            End If
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　チェックボックスコントロールのフォーカス取得時イベント
    '　(処理概要)チェックボックスコントロールのフォーカス取得時の処理
    '-------------------------------------------------------------------------------
    Private Sub chk_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chktaisyogai.Enter
        Try

            Dim chk As CheckBox = DirectCast(sender, CheckBox)
            _beforeChange = chk.Checked.ToString

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　チェックボックスコントロールのフォーカス消失時イベント
    '　(処理概要)チェックボックスコントロールのフォーカス消失時の処理
    '-------------------------------------------------------------------------------
    Private Sub chk_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chktaisyogai.Leave
        Try
            If _changeFlg = False Then
                '編集前と値が変わっていた場合、フラグを立てる
                Dim chk As CheckBox = DirectCast(sender, CheckBox)
                If Not _beforeChange.Equals(chk.Checked.ToString) Then
                    _changeFlg = True
                End If
            End If
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　コンボボックスの選択
    '　(処理概要)コンボボックスのリストを選択する
    '　　I　：　prmsender       対象オブジェクト
    '　　I　：　prmsetdata      コンボボックスで選択させる内容
    '-------------------------------------------------------------------------------
    Private Sub selectCbo(ByVal prmsender As Object, ByVal prmsetdata As String)
        Try

            Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(prmsender)
            'コンボボックスを選択する
            ch.selectItem(prmsetdata)

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　コンボボックスのコード取得
    '　(処理概要)現在選択されているコンボボックスのコードを取得する
    '　　I　：　prmsender      コンボボックスで選択させる内容
    '-------------------------------------------------------------------------------
    Private Function GetCboCode(ByVal prmsender As Object) As String
        Try

            Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(prmsender)
            'コード取得
            GetCboCode = ch.getCode()

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Function


#End Region

#Region "ユーザ定義関数:DB関連"
    '-------------------------------------------------------------------------------
    '　画面表示
    '　(処理概要)画面を表示する
    '-------------------------------------------------------------------------------
    Private Sub dispFrom(ByVal prmSql As String)
        Try
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & " T51.TEHAI_NO " & COLDT_TEHAINO
            sql = sql & N & " ,TO_CHAR(TO_DATE(T51.KIBOU_DATE,'YYYYMMDD'),'yyyy/mm/dd') " & COLDT_SYUTTAIBI
            '2010/11/17 delete start Nakazawa
            'sql = sql & N & " ,TO_CHAR(TO_DATE(T51.NOUKI,'YYYYMMDD'),'yyyy/mm/dd') " & COLDT_NOUKI
            '2010/11/17 delete end Nakazawa
            sql = sql & N & " ,M011.NAME1 " & COLDT_TEHAIKBN
            sql = sql & N & " ,T51.SEISAKU_KBN " & COLDT_SEISAKU
            sql = sql & N & " ,M012.NAME1 " & COLDT_SEIZOUBMN
            sql = sql & N & " ,T51.TYUMONSAKI " & COLDT_TYUMONSAKI
            sql = sql & N & " ,T51.HINMEI_CD " & COLDT_HINMEICD
            sql = sql & N & " ,T51.HINMEI " & COLDT_HINMEI
            sql = sql & N & " ,T51.TEHAI_SUU " & COLDT_TEHAISUURYOU
            sql = sql & N & " ,T51.SIYOUSYO_NO " & COLDT_SIYOUSYONO
            sql = sql & N & " ,T51.TANCYO " & COLDT_TANTYOU
            sql = sql & N & " ,T51.JYOSU " & COLDT_JOUSUU
            sql = sql & N & " ,T51.MAKI_CD " & COLDT_MAKIWAKU
            sql = sql & N & " ,ZEA.ZE_NAME " & COLDT_MAKIWAKUMEI
            sql = sql & N & " ,T51.HOSO_KBN " & COLDT_HOUSOU
            sql = sql & N & " ,HOS.HN_NAME " & COLDT_HOUSOUTYPE
            sql = sql & N & " ,T51.N_K_SUU " & COLDT_KSUU
            sql = sql & N & " ,T51.N_SH_SUU " & COLDT_SHSUU
            sql = sql & N & " ,T51.GAICYUSAKI " & COLDT_GAICYUSAKI
            sql = sql & N & " ,TO_CHAR(TO_DATE(T51.CYUMONBI,'RRMMDD'),'yyyy/mm/dd') " & COLDT_CYUMONBI
            sql = sql & N & " ,TO_CHAR(TO_DATE(T51.NYUKABI,'RRMMDD'),'yyyy/mm/dd') " & COLDT_NYUKABI
            sql = sql & N & " ,T51.KAMOKU_CD " & COLDT_KAMOKUCD
            sql = sql & N & " ,T51.CYUMONNO " & COLDT_CYUMONNO
            sql = sql & N & " ,T51.TOKKI " & COLDT_TOKKI
            sql = sql & N & " ,T51.BIKO " & COLDT_BIKO
            sql = sql & N & " ,T51.HENKO " & COLDT_HENKO
            sql = sql & N & " ,T51.TENKAI_KBN " & COLDT_TENKAIKBN
            sql = sql & N & " ,T51.BBNKOUTEI " & COLDT_BBNKOUTEI
            sql = sql & N & " ,T51.HINSITU_KBN " & COLDT_HINSITUKBN
            sql = sql & N & " ,M013.NAME1 " & COLDT_KEISANKBN
            sql = sql & N & " ,T51.TATIAI_UM " & COLDT_TATIAIUM
            sql = sql & N & " ,TO_CHAR(TO_DATE(T51.TACIAIBI,'YYYYMMDD'),'yyyy/mm/dd') " & COLDT_TACIAIBI
            sql = sql & N & " ,T51.GAI_FLG " & COLDT_TAISYOGAI
            sql = sql & N & " FROM T51TEHAI T51 "
            sql = sql & N & "   LEFT OUTER JOIN M01HANYO M011 ON "
            sql = sql & N & "   T51.TEHAI_KBN = M011.KAHENKEY "
            sql = sql & N & "   AND M011.KOTEIKEY = '" & COTEI_TEHAI & "'"
            sql = sql & N & "   LEFT OUTER JOIN M01HANYO M012 ON "
            sql = sql & N & "   T51.SEIZOU_BMN = M012.KAHENKEY "
            sql = sql & N & "   AND M012.KOTEIKEY = '" & COTEI_SEIZOU & "'"
            sql = sql & N & "   LEFT OUTER JOIN M01HANYO M013 ON "
            sql = sql & N & "   T51.KEISAN_KBN = M013.KAHENKEY "
            sql = sql & N & "   AND M013.KOTEIKEY = '" & COTEI_KAKOU & "'"
            sql = sql & N & "   LEFT OUTER JOIN M01HANYO M014 ON "
            sql = sql & N & "   T51.TATIAI_UM = M014.KAHENKEY "
            sql = sql & N & "   AND M014.KOTEIKEY = '" & COTEI_TATIAI & "'"
            sql = sql & N & "   LEFT OUTER JOIN ZEASYCODE_TB ZEA ON "
            sql = sql & N & "   T51.MAKI_CD = ZEA.ZE_CODE "
            sql = sql & N & "   LEFT OUTER JOIN HOSONAME_TB HOS ON "
            sql = sql & N & "   T51.HOSO_KBN = HOS.HN_KUBUN "
            sql = sql & N & " WHERE T51.TEHAI_NO = '" & prmSql & "'"
            sql = sql & N & " ORDER BY T51.TEHAI_NO "

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '抽出レコードが１件もない場合
                Throw New UsrDefException("対象データがありません。", _msgHd.getMSG("NonData"))
            End If

            '検索結果表示
            lblTehaiNo.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_TEHAINO))                 '手配№
            txtKibou.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_SYUTTAIBI))                 '希望出来日
            '2010/11/17 delete start Nakazawa
            'txtNouki.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_NOUKI))                     '納期
            '2010/11/17 delete end Nakazawa
            lblTehaiKbn.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_TEHAIKBN))               '手配区分
            Call selectCbo(cboSeisakuKbn, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_SEISAKU)))    '製作区分
            lblSeizouBmn.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_SEIZOUBMN))             '製造部門
            lblTyuumonsaki.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_TYUMONSAKI))          '注文先
            lblHinmecd.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_HINMEICD))                '品名コード
            lblHinmei.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_HINMEI))                   '注文品名
            txtTehaiSuuryou.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_TEHAISUURYOU))       '手配数料量
            txtSiyousyoNo.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_SIYOUSYONO))           '仕様書番号
            txtTantyou.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_TANTYOU))                 '単長
            txtJousuu.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_JOUSUU))                   '条数
            txtMakiwaku.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_MAKIWAKU))               '巻枠コード
            lblMakiwakumei.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_MAKIWAKUMEI))         '巻枠名
            txtHousou.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_HOUSOU))                   '包装/表示区分
            lblHousouSyurui.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_HOUSOUTYPE))         '包装/表示種類
            txtKHonsuu.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_KSUU))                    'K本数
            txtSHonsuu.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_SHSUU))                   'S本数
            txtGaicyusaki.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_GAICYUSAKI))           '外注先
            txtCyumonbi.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_CYUMONBI))               '注文日
            txtNyukabi.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_NYUKABI))                 '入荷日
            txtKamoku.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_KAMOKUCD))                 '科目コード
            txtCyumonno.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_CYUMONNO))               '注文№
            '特記事項
            Dim tokki As String = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_TOKKI))
            If tokki.Length <= 22 Then
                txtTokki1.Text = Trim(tokki)
            ElseIf tokki.Length > 22 And tokki.Length <= 44 Then
                txtTokki1.Text = Trim(tokki.Substring(0, 22))
                txtTokki2.Text = Trim(tokki.Substring(22))
            Else
                txtTokki1.Text = Trim(tokki.Substring(0, 22))
                txtTokki2.Text = Trim(tokki.Substring(22, 22))
                txtTokki3.Text = Trim(tokki.Substring(44))
            End If
            txtBikou.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_BIKO))                      '備考
            txtHenkouNaiyou.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_HENKO))              '変更内容
            Call selectCbo(cboTenkaiKbn, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_TENKAIKBN)))   '展開区分
            txtBubunTenkai.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_BBNKOUTEI))           '部分展開指定
            Call selectCbo(cboHinsituKbn, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_HINSITUKBN))) '品質試験区分
            lblKakoutyouKbn.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_KEISANKBN))          '加工長計算区
            Call selectCbo(cboTachiai, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_TATIAIUM)))      '立会有無
            txtTachiaibi.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_TACIAIBI))              '立会予定日
            '対象外
            If "1".Equals(_db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_TAISYOGAI))) Then
                chktaisyogai.Checked = True
            Else
                chktaisyogai.Checked = False
            End If

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '　手配テーブルの更新処理
    '　(処理概要)変更された内容にて手配テーブルを更新する
    '　　I　：　prmSysdate       処理開始日時
    '　　I　：　prmPCName      　端末ID
    '------------------------------------------------------------------------------------------------------
    Private Sub UpdateT51TEHAI(ByVal prmSysdate As Date, ByVal prmPCName As String)
        Try

            'SQL文発行
            Dim sql As String = ""
            sql = "UPDATE T51TEHAI SET"
            sql = sql & N & " KIBOU_DATE = '" & Trim(Replace(txtKibou.Text, "/", "")) & "'"
            '2010/11/17 delete start Nakazawa
            'sql = sql & N & " ,NOUKI = '" & Trim(Replace(txtNouki.Text, "/", "")) & "'"
            '2010/11/17 delete end Nakazawa
            sql = sql & N & " ,SEISAKU_KBN = '" & GetCboCode(cboSeisakuKbn) & "'"
            sql = sql & N & " ,TEHAI_SUU = " & txtTehaiSuuryou.Value
            sql = sql & N & " ,SIYOUSYO_NO = '" & txtSiyousyoNo.Text & "'"
            sql = sql & N & " ,TANCYO = " & txtTantyou.Value
            sql = sql & N & " ,JYOSU = " & txtJousuu.Value
            sql = sql & N & " ,MAKI_CD = '" & txtMakiwaku.Text & "'"
            sql = sql & N & " ,HOSO_KBN = '" & txtHousou.Text & "'"
            sql = sql & N & " ,N_K_SUU = " & txtKHonsuu.Value
            sql = sql & N & " ,N_SH_SUU = " & txtSHonsuu.Value
            sql = sql & N & " ,GAICYUSAKI = '" & txtGaicyusaki.Text & "'"
            '-->2010.12.27 chg by takagi #52
            'sql = sql & N & " ,CYUMONBI = '" & Trim(Replace(txtCyumonbi.Text, "/", "")) & "'"
            'sql = sql & N & " ,NYUKABI = '" & Trim(Replace(txtNyukabi.Text, "/", "")) & "'"
            Dim tmp As String = ""
            tmp = Trim(Replace(txtCyumonbi.Text, "/", ""))
            If tmp.Length > 0 Then tmp = tmp.Substring(2, 6) '入力があればYYMMDDへ
            sql = sql & N & " ,CYUMONBI = '" & tmp & "'"
            tmp = Trim(Replace(txtNyukabi.Text, "/", ""))
            If tmp.Length > 0 Then tmp = tmp.Substring(2, 6) '入力があればYYMMDDへ
            sql = sql & N & " ,NYUKABI = '" & tmp & "'"
            '<--2010.12.27 chg by takagi #52
            sql = sql & N & " ,KAMOKU_CD = '" & txtKamoku.Text & "'"
            sql = sql & N & " ,CYUMONNO = '" & txtCyumonno.Text & "'"
            sql = sql & N & " ,TOKKI = '" & txtTokki1.Text.PadRight(22) & txtTokki2.Text.PadRight(22) & txtTokki3.Text.PadRight(22) & "'"
            sql = sql & N & " ,BIKO = '" & txtBikou.Text & "'"
            sql = sql & N & " ,HENKO = '" & txtHenkouNaiyou.Text & "'"
            sql = sql & N & " ,TENKAI_KBN = '" & GetCboCode(cboTenkaiKbn) & "'"
            sql = sql & N & " ,BBNKOUTEI = '" & txtBubunTenkai.Text & "'"
            sql = sql & N & " ,HINSITU_KBN = '" & GetCboCode(cboHinsituKbn) & "'"
            sql = sql & N & " ,TATIAI_UM = '" & GetCboCode(cboTachiai) & "'"
            sql = sql & N & " ,TACIAIBI = '" & Trim(Replace(txtTachiaibi.Text, "/", "")) & "'"
            If chktaisyogai.Checked Then
                sql = sql & N & " ,GAI_FLG = '" & TAISYO_ARI & "'"
            Else
                sql = sql & N & " ,GAI_FLG = '" & TAISYO_GAI & "'"
            End If

            sql = sql & N & ", UPDNAME = '" & prmPCName & "'"                                         '端末ID
            sql = sql & N & ", UPDDATE = TO_DATE('" & prmSysdate & "', 'YYYY/MM/DD HH24:MI:SS') "     '更新日時
            sql = sql & N & " WHERE TEHAI_NO = '" & _koteiKey & "'"
            _db.executeDB(sql)

        Catch ex As Exception
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　コンボボックスのセット
    '　(処理概要)M01汎用マスタから作成区分,展開区分,品質試験区分,立会有無を抽出して表示する。
    '　　I　：　prmsender       設定対象オブジェクト
    '　　I　：　prmWhere      　検索条件
    '-------------------------------------------------------------------------------
    Private Sub setCbo(ByVal prmsender As Object, ByVal prmWhere As String)
        Try
            'コンボボックス
            Dim sql = ""
            sql = sql & N & " SELECT KAHENKEY "
            sql = sql & N & " ,NAME1 "
            sql = sql & N & " FROM M01HANYO "
            sql = sql & N & " WHERE KOTEIKEY = '" & prmWhere & "' "
            sql = sql & N & " ORDER BY KAHENKEY "

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '抽出レコードが１件もない場合
                btnKousin.Enabled = False
                Throw New UsrDefException("登録されていません。", _msgHd.getMSG("noData"))
            End If

            Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(prmsender)

            '検索結果をコンボボックスに設定
            For i As Integer = 0 To iRecCnt - 1
                ch.addItem(New UtilCboVO(ds.Tables(RS).Rows(i)(0).ToString, ds.Tables(RS).Rows(i)(1).ToString))
            Next

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　巻枠名表示
    '　(処理概要)巻枠名を表示する
    '　　I　：　prmWhere      　検索条件
    '-------------------------------------------------------------------------------
    Private Sub dispMakiwakuName(ByVal prmWhere As String)
        Try
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & " ZE_NAME " & "NAME"          '巻枠名
            sql = sql & N & " FROM ZEASYCODE_TB "
            sql = sql & N & " WHERE ZE_CODE = '" & prmWhere & "'"

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '抽出レコードが１件もない場合
                Throw New UsrDefException("巻枠名がマスタに登録されていません。", _msgHd.getMSG("noMakiwakuName"), txtMakiwaku)
            End If

            '検索結果を表示
            lblMakiwakumei.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("NAME"))

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub
    '-------------------------------------------------------------------------------
    '　包装/表示区分種類表示
    '　(処理概要)包装/表示区分種類を表示する
    '　　I　：　prmWhere      　検索条件
    '-------------------------------------------------------------------------------
    Private Sub dispHousouType(ByVal prmWhere As String)
        Try
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & " HN_NAME " & "NAME"          '包装/表示種類
            sql = sql & N & " FROM HOSONAME_TB "
            sql = sql & N & " WHERE HN_KUBUN = '" & prmWhere & "'"

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '抽出レコードが１件もない場合
                Throw New UsrDefException("包装/表示種類がマスタに登録されていません。", _msgHd.getMSG("noHousouType"), txtHousou)
            End If
            '検索結果を表示
            lblHousouSyurui.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("NAME"))

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '　実行履歴テーブルの更新処理
    '　(処理概要)実行履テーブルを更新する
    '　　I　：　prmTehai       　　手配№
    '　　I　：　prmPCName      　　端末ID
    '　　I　：　prmStartDate       処理開始日時
    '　　I　：　prmFinishDate      処理終了日時
    '------------------------------------------------------------------------------------------------------
    Private Sub updT91Rireki(ByVal prmTehai As String, ByVal prmPCName As String, ByVal prmStartDate As Date, ByVal prmFinishDate As Date)
        Try
            '登録処理
            Dim sql As String = ""
            sql = "INSERT INTO T91RIREKI ("
            sql = sql & N & "  SNENGETU"                                                    '処理年月
            sql = sql & N & ", KNENGETU"                                                    '計画年月
            sql = sql & N & ", PGID"                                                        '機能ID
            sql = sql & N & ", SDATESTART"                                                  '処理開始日時
            sql = sql & N & ", SDATEEND"                                                    '処理終了日時
            sql = sql & N & ", KENNSU1"                                                     '件数１（削除件数）
            sql = sql & N & ", NAME1"                                                       '文字１（手配№）
            sql = sql & N & ", UPDNAME"                                                     '端末ID
            sql = sql & N & ", UPDDATE"                                                     '更新日時
            sql = sql & N & ") VALUES ("
            sql = sql & N & "  '" & _Syori & "'"                                            '処理年月
            sql = sql & N & ", '" & _Keikaku & "'"                                          '計画年月
            sql = sql & N & ", '" & PGID & "'"                                              '機能ID
            sql = sql & N & ", TO_DATE('" & prmStartDate & "', 'YYYY/MM/DD HH24:MI:SS') "   '処理開始日時
            sql = sql & N & ", TO_DATE('" & prmFinishDate & "', 'YYYY/MM/DD HH24:MI:SS') "  '処理終了日時
            sql = sql & N & ", " & UPDATECOUNT                                              '件数１（更新件数）
            sql = sql & N & ", '" & prmTehai & "'"                                          '文字１（手配№）
            sql = sql & N & ", '" & prmPCName & "'"                                         '端末ID
            sql = sql & N & ", TO_DATE('" & prmStartDate & "', 'YYYY/MM/DD HH24:MI:SS') "   '更新日時
            sql = sql & N & " )"
            _db.executeDB(sql)

        Catch ex As Exception
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '2010/12/02 add start nakazawa
    '2010/12/02 del start sugano
    ''-------------------------------------------------------------------------------
    ''　単長(ｍ)編集後処理
    ''　(処理概要)手配数量の自動計算を行う。
    ''-------------------------------------------------------------------------------
    'Private Sub txtTantyou_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTantyou.LostFocus
    '    Try

    '        Call culcTehaiSuryo()

    '    Catch ue As UsrDefException
    '        ue.dspMsg()
    '    Catch ex As Exception
    '        Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
    '    End Try

    'End Sub

    ''-------------------------------------------------------------------------------
    ''　条数編集後処理
    ''　(処理概要)手配数量の自動計算を行う。
    ''-------------------------------------------------------------------------------
    'Private Sub txtJousuu_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtJousuu.LostFocus
    '    Try

    '        Call culcTehaiSuryo()

    '    Catch ue As UsrDefException
    '        ue.dspMsg()
    '    Catch ex As Exception
    '        Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
    '    End Try

    'End Sub

    ''-------------------------------------------------------------------------------
    ''　手配数量の自動計算
    ''　(処理概要)単長と条数から手配数量を自動計算する。
    ''-------------------------------------------------------------------------------
    'Private Sub culcTehaiSuryo()
    '    Try

    '        '単長と条数が両方入力されている場合のみ行う
    '        If Not "".Equals(txtTantyou.Text) And Not "".Equals(txtJousuu.Text) Then

    '            txtTehaiSuuryou.Text = CStr(CInt(txtTantyou.Text) * CInt(txtJousuu.Text))

    '        End If

    '    Catch ue As UsrDefException         'ユーザー定義例外
    '        Call ue.dspMsg()
    '        Throw ue                        'キャッチした例外をそのままスロー
    '    Catch ex As Exception               'システム例外
    '        Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
    '    End Try

    'End Sub
    ''2010/12/02 del end sugano
    '2010/12/02 add end nakazawa

#End Region

#Region "ユーザ定義関数:チェック処理"
    '------------------------------------------------------------------------------------------------------
    '  登録チェック
    '　(処理概要)各項目の入力チェックを行う
    '------------------------------------------------------------------------------------------------------
    Private Sub checkTouroku()
        Try

            Dim ch1 As UtilComboBoxHandler = New UtilComboBoxHandler(cboSeisakuKbn)
            Dim ch2 As UtilComboBoxHandler = New UtilComboBoxHandler(cboTenkaiKbn)
            Dim ch3 As UtilComboBoxHandler = New UtilComboBoxHandler(cboHinsituKbn)

            '必須入力チェック
            Call chekuHissu(txtKibou, Trim(Replace(txtKibou.Text, "/", "")), "希望出来日")
            '2010/11/17 delete start Nakazawa
            'Call chekuHissu(txtNouki, Trim(Replace(txtNouki.Text, "/", "")), "納期")
            '2010/11/17 delete end Nakazawa
            Call chekuHissu(txtTehaiSuuryou, txtTehaiSuuryou.Value, "手配数量")
            Call chekuHissu(txtSiyousyoNo, txtSiyousyoNo.Text, "仕様書番号")
            Call chekuHissu(txtTantyou, txtTantyou.Value, "単長")
            Call chekuHissu(txtJousuu, txtJousuu.Value, "条数")
            Call chekuHissu(txtKHonsuu, txtKHonsuu.Value, "K本数")
            Call chekuHissu(txtSHonsuu, txtSHonsuu.Value, "S本数")
            Call chekuHissu(cboSeisakuKbn, cboSeisakuKbn.Text, "製作区分")
            Call chekuHissu(cboTenkaiKbn, cboTenkaiKbn.Text, "展開区分")
            Call chekuHissu(cboHinsituKbn, cboHinsituKbn.Text, "品質試験区分")
            Call chekuHissu(txtMakiwaku, txtMakiwaku.Text, "巻枠コード")
            Call chekuHissu(txtHousou, txtHousou.Text, "包装/表示区分")
            If SEISAKU_GAI.Equals(ch1.getCode) Then
                Call chekuHissu(txtGaicyusaki, txtGaicyusaki.Text, "外注先")
                Call chekuHissu(txtCyumonbi, Trim(Replace(txtCyumonbi.Text, "/", "")), "注文日")
                Call chekuHissu(txtNyukabi, Trim(Replace(txtNyukabi.Text, "/", "")), "入荷日")
                Call chekuHissu(txtKamoku, txtKamoku.Text, "科目コード")
                Call chekuHissu(txtCyumonno, txtCyumonno.Text, "注文番号")
            End If

            'マイナス値チェック
            If Integer.Parse(txtKHonsuu.Value) < 0 Then
                'エラーメッセージの表示
                Throw New UsrDefException("プラスの値を入力してください。", _msgHd.getMSG("NoPositiveNo", "【 K本数 】"), txtKHonsuu)
            End If
            If Integer.Parse(txtSHonsuu.Value) < 0 Then
                'エラーメッセージの表示
                Throw New UsrDefException("プラスの値を入力してください。", _msgHd.getMSG("NoPositiveNo", "【 S本数 】"), txtSHonsuu)
            End If

            '大小関係チェック
            If Integer.Parse(txtKHonsuu.Value) > Integer.Parse(txtJousuu.Value) Then
                'エラーメッセージの表示
                Throw New UsrDefException("条数を超える値が入力されています。", _msgHd.getMSG("JousuuOver", "【 K本数 】"), txtKHonsuu)
            End If
            If Integer.Parse(txtSHonsuu.Value) > Integer.Parse(txtJousuu.Value) Then
                'エラーメッセージの表示
                Throw New UsrDefException("条数を超える値が入力されています。", _msgHd.getMSG("JousuuOver", "【 S本数 】"), txtSHonsuu)
            End If

            '桁数チェック
            If Not txtMakiwaku.Text.Length.Equals(6) Then
                'エラーメッセージの表示
                Throw New UsrDefException("巻枠コードは６桁で入力してください。", _msgHd.getMSG("MakiwakuCDLength", "【 巻枠コード 】"), txtMakiwaku)
            End If
            '-->2010.12.27 del by takagi #55
            'If SEISAKU_GAI.Equals(ch1.getCode) And Not txtKamoku.Text.Length.Equals(6) Then
            '    'エラーメッセージの表示
            '    Throw New UsrDefException("科目コードは６桁で入力してください。", _msgHd.getMSG("KamokuCDLength", "【 科目コード 】"), txtKamoku)
            'End If
            '<--2010.12.27 del by takagi #55
            If Long.Parse(txtTantyou.Value) * Long.Parse(txtJousuu.Value) >= 10000000 Then
                'エラーメッセージの表示
                Throw New UsrDefException("単長×条数の値が７ケタを超えています。", _msgHd.getMSG("InputLenOver", "【 単長×条数 】"), txtTantyou)
            End If

            '空欄チェック
            If "".Equals(lblMakiwakumei.Text) Then
                'エラーメッセージの表示
                Throw New UsrDefException("巻枠名がマスタに登録されていません。", _msgHd.getMSG("noMakiwakuName"))
            End If
            If "".Equals(lblHousouSyurui.Text) Then
                'エラーメッセージの表示
                Throw New UsrDefException("包装/表示種類がマスタに登録されていません。", _msgHd.getMSG("noHousouType"))
            End If

            '半角文字チェック
            If UtilClass.isOnlyNStr(txtSiyousyoNo.Text) = False Then
                Throw New UsrDefException("半角英数のみ入力可能です。", _msgHd.getMSG("onlyAcceptHankakuEisu", "【 仕様書番号 】"), txtSiyousyoNo)
            End If
            If UtilClass.isOnlyNStr(txtGaicyusaki.Text) = False Then
                Throw New UsrDefException("半角英数のみ入力可能です。", _msgHd.getMSG("onlyAcceptHankakuEisu", "【 外注先 】"), txtGaicyusaki)
            End If
            If UtilClass.isOnlyNStr(txtKamoku.Text) = False Then
                Throw New UsrDefException("半角英数のみ入力可能です。", _msgHd.getMSG("onlyAcceptHankakuEisu", "【 科目コード 】"), txtKamoku)
            End If
            If UtilClass.isOnlyNStr(txtCyumonno.Text) = False Then
                Throw New UsrDefException("半角英数のみ入力可能です。", _msgHd.getMSG("onlyAcceptHankakuEisu", "【 注文№ 】"), txtCyumonno)
            End If
            If UtilClass.isOnlyNStr(txtTokki1.Text) = False Then
                Throw New UsrDefException("半角英数のみ入力可能です。", _msgHd.getMSG("onlyAcceptHankakuEisu", "【 特記事項１ 】"), txtTokki1)
            End If
            If UtilClass.isOnlyNStr(txtTokki2.Text) = False Then
                Throw New UsrDefException("半角英数のみ入力可能です。", _msgHd.getMSG("onlyAcceptHankakuEisu", "【 特記事項２ 】"), txtTokki2)
            End If
            If UtilClass.isOnlyNStr(txtTokki3.Text) = False Then
                Throw New UsrDefException("半角英数のみ入力可能です。", _msgHd.getMSG("onlyAcceptHankakuEisu", "【 特記事項３ 】"), txtTokki3)
            End If
            If UtilClass.isOnlyNStr(txtBubunTenkai.Text) = False Then
                Throw New UsrDefException("半角英数のみ入力可能です。", _msgHd.getMSG("onlyAcceptHankakuEisu", "【 部分展開指定工程 】"), txtBubunTenkai)
            End If

            '入力値チェック
            If Not Integer.Parse(txtJousuu.Value).Equals(Integer.Parse(txtKHonsuu.Value) + Integer.Parse(txtSHonsuu.Value)) Then
                'エラーメッセージの表示
                Throw New UsrDefException("K本数とS本数の合計が、条数と一致していません。", _msgHd.getMSG("notEqualKSSum", "【 条数 】"), txtJousuu)
            End If
            If Not Long.Parse(txtTehaiSuuryou.Value).Equals(Long.Parse(txtTantyou.Value) * Long.Parse(txtJousuu.Value)) Then
                'エラーメッセージの表示
                Throw New UsrDefException("手配数量が単長×条数と一致していません。", _msgHd.getMSG("notEqualTehaiSuuryou", "【 手配数量 】"), txtTehaiSuuryou)
            End If
            If SEISAKU_GAI.Equals(ch1.getCode) And Not TENKAI_POINT.Equals(ch2.getCode) Then
                'エラーメッセージの表示
                Throw New UsrDefException("製作区分「外注」時は展開区分「部分展開」以外選択出来ません。", _msgHd.getMSG("nonGaicyuSelect", "【 展開区分 】"), cboTenkaiKbn)
            End If
            If TENKAI_POINT.Equals(ch2.getCode) And "".Equals(txtBubunTenkai.Text) Then
                'エラーメッセージの表示
                Throw New UsrDefException("展開区分「部分展開」時は部分展開指定工程は省略できません。", _msgHd.getMSG("nonBubunOmit", "【 部分展開指定工程 】"), txtBubunTenkai)
            End If
            If Not "".Equals(txtBubunTenkai.Text) Then
                If Not "1".Equals(txtBubunTenkai.Text.Substring(0, 1)) And Not "3".Equals(txtBubunTenkai.Text.Substring(0, 1)) Then
                    'エラーメッセージの表示
                    Throw New UsrDefException("１または３から始まる工程を入力してください。", _msgHd.getMSG("ErrStartKoutei", "【 部分展開指定工程 】"), txtBubunTenkai)
                End If
            End If


        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '  必須入力チェック
    '　(処理概要)テキストが入力されているかチェックする
    '　　I　：　prmSender              チェックするオブジェクト
    '　　I　：　prmControlName         チェックするオブジェクト名
    '------------------------------------------------------------------------------------------------------
    Private Sub chekuHissu(ByVal prmSender As System.Object, ByVal prmChktxt As String, ByVal prmControlName As String)
        Try
            '必須入力チェック
            If "".Equals(prmChktxt) Then
                'エラーメッセージの表示
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【 " & prmControlName & "】"), prmSender)
            End If

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub
#End Region

    
End Class