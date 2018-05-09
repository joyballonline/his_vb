'===============================================================================
'
'　北日本電線株式会社
'　　（システム名）在庫計画システム
'　　（処理機能名）手配データ一覧
'    （フォームID）ZG620E_TehaiSyuuseiItran
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   橋本        2010/10/19                 新規              
'　(2)   中澤        2010/11/17                 変更(項目「納期」削除対応)    
'　(3)   中澤        2010/12/02                 変更(詳細画面で変更した対象外が反映されないバグ修正)    
'　(4)   菅野        2011/01/17                 変更(処理制御テーブルの更新をやめる）    
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls

Public Class ZG620E_TehaiSyuuseiItiran
    Inherits System.Windows.Forms.Form
    Implements IfRturnUpDateData

#Region "リテラル値定義"
    '-------------------------------------------------------------------------------
    '   定数定義
    '-------------------------------------------------------------------------------

    'PG制御文字
    Private Const RS As String = "RecSet"                       'データセットテーブル名
    Private Const N As String = ControlChars.NewLine            '改行文字
    Private Const PGID As String = "ZG620E"

    '一覧データバインド名
    Private Const COLDT_TAISYOGAI As String = "dtTaisyogai"         '対象外
    Private Const COLDT_TEHAINO As String = "dtTehaiNo"             '手配№
    Private Const COLDT_SYUTTAIBI As String = "dtSyuttaibi"         '希望出来日
    '2010/11/17 delete start Nakazawa
    'Private Const COLDT_NOUKI As String = "dtNouki"                 '納期
    '2010/11/17 delete end Nakazawa
    Private Const COLDT_HINMEICD As String = "dtHinmeiCD"           '品名コード
    Private Const COLDT_HINMEI As String = "dtHinmei"               '品名
    Private Const COLDT_TEHAISUURYOU As String = "dtTehaiSuuryou"   '手配数料量
    Private Const COLDT_TANTYOU As String = "dtTantyou"             '単長
    Private Const COLDT_JOUSUU As String = "dtJousuu"               '条数
    Private Const COLDT_SIYOUSYONO As String = "dtSiyousyoNo"       '仕様書番号
    Private Const COLDT_MAKIWAKU As String = "dtMakiwaku"           '巻枠コード
    Private Const COLDT_HOUSOU As String = "dtHousou"               '包装/表示区分
    Private Const COLDT_JUYOU As String = "dtJuyou"                 '需要先
    Private Const COLDT_SEISAKU As String = "dtSeisaku"             '製作区分
    Private Const COLDT_SEIZOUBMN As String = "dtSeizoubmn"         '製造部門
    Private Const COLDT_TEHAISORT As String = "dtTehaiSort"         '手配№表示順

    '一覧グリッド名
    Private Const COLCN_TAISYOGAI As String = "cnTaisyogai"         '対象外
    Private Const COLCN_TEHAINO As String = "cnTehaiNo"             '手配№
    Private Const COLCN_SYUTTAIBI As String = "cnSyuttaibi"         '希望出来日
    '2010/11/17 delete start Nakazawa
    'Private Const COLCN_NOUKI As String = "cnNouki"                 '納期
    '2010/11/17 delete end Nakazawa
    Private Const COLCN_HINMEICD As String = "cnHinmeiCD"           '品名コード
    Private Const COLCN_HINMEI As String = "cnHinmei"               '品名
    Private Const COLCN_TEHAISUURYOU As String = "cnTehaiSuuryou"   '手配数料量
    Private Const COLCN_TANTYOU As String = "cnTantyou"             '単長
    Private Const COLCN_JOUSUU As String = "cnJousuu"               '条数
    Private Const COLCN_SIYOUSYONO As String = "cnSiyousyoNo"       '仕様書番号
    Private Const COLCN_MAKIWAKU As String = "cnMakiwaku"           '巻枠コード
    Private Const COLCN_HOUSOU As String = "cnHousou"               '包装/表示区分
    Private Const COLCN_JUYOU As String = "cnJuyou"                 '需要先
    Private Const COLCN_SEISAKU As String = "cnSeisaku"             '製作区分
    Private Const COLCN_SEIZOUBMN As String = "cnSeizoubmn"         '製造部門
    Private Const COLCN_TEHAISORT As String = "cnTehaiSort"         '手配№表示順

    'M01汎用マスタ固定ｷｰ
    Private Const COTEI_JUYOU As String = "01"                      '需要先名
    Private Const COTEI_SEISAKU As String = "03"                    '製作区分
    Private Const COTEI_SEIZOUBMN As String = "09"                  '製造部門
    Private Const COTEI_GAIFLG As String = "17"                     '対象外フラグ

    'EXCEL
    Private Const START_PRINT As Integer = 11        'EXCEL出力開始行数
    Private Const XLSSHEET_DENSEN As String = "電線" 'シート判断用固定文字
    Private Const XLSSHEET_TSUSIN As String = "通信" 'シート判断用固定文字

    'EXCEL列番号
    Private Const XLSCOL_TEHAINO As Integer = 1      '手配№
    Private Const XLSCOL_JUYOU As Integer = 2        '需要先名
    Private Const XLSCOL_SEISAKU As Integer = 3      '製作区分
    Private Const XLSCOL_HINMEICD As Integer = 4     '品名コード
    Private Const XLSCOL_HINMEI As Integer = 5       '品名・サイズ・色
    Private Const XLSCOL_SYUTTAIBI As Integer = 6    '希望出来日
    Private Const XLSCOL_TEHAISUURYOU As Integer = 7 '手配数量
    Private Const XLSCOLTANTYOU As Integer = 8       '単長
    Private Const XLSCOL_JOUSUU As Integer = 9       '条数
    Private Const XLSCOL_MAKIWAKU As Integer = 10    '巻枠コード
    Private Const XLSCOL_HOUSOU As Integer = 11      '包装/表示区分
    Private Const XLSCOL_SIYOUSYONO As Integer = 12  '仕様書番号
    Private Const XLSCOL_GAICYU As Integer = 13      '外注
    Private Const XLSCOL_ENKI As Integer = 14        '延期
    Private Const XLSCOL_CYUSI As Integer = 15       '中止
    

#End Region

#Region "メンバ変数定義"

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As ZC110M_Menu

    Private _oldRowIndex As Integer = -1            '選択行の背景色を変更するためのフラグを宣言
    Private _colorCtlFlg As Boolean = False         '選択行の背景色を変更するためのフラグを宣言

    '検索条件格納変数
    Private _SerchCriteria As SerchCriteria
    Private Structure SerchCriteria
        Private _serchTehaiNo As String            '手配№
        Private _serchSiyoCd As String             '仕様コード
        Private _serchHinsyuCd As String           '品種コード
        Private _serchSensinCd As String           '線心数コード
        Private _serchSizeCd As String             'サイズコード
        Private _serchColorCd As String            '色コード
        Private _serchHinmei As String             '品名
        Private _serchKibouFrom As String          '希望出来日(From)
        Private _serchKibouTo As String            '希望出来日(To)
        '2010/11/17 delete start Nakazawa
        'Private _serchNoukiFrom As String          '納期(From)
        'Private _serchNoukiTo As String            '納期(To)
        '2010/11/17 delete end Nakazawa

        Public Property serchTehaiNo() As String
            Get
                Return _serchTehaiNo
            End Get
            Set(ByVal Value As String)
                _serchTehaiNo = Value
            End Set
        End Property
        Public Property serchSiyoCd() As String
            Get
                Return _serchSiyoCd
            End Get
            Set(ByVal Value As String)
                _serchSiyoCd = Value
            End Set
        End Property
        Public Property serchHinsyuCd() As String
            Get
                Return _serchHinsyuCd
            End Get
            Set(ByVal Value As String)
                _serchHinsyuCd = Value
            End Set
        End Property
        Public Property serchSensinCd() As String
            Get
                Return _serchSensinCd
            End Get
            Set(ByVal Value As String)
                _serchSensinCd = Value
            End Set
        End Property
        Public Property serchSizeCd() As String
            Get
                Return _serchSizeCd
            End Get
            Set(ByVal Value As String)
                _serchSizeCd = Value
            End Set
        End Property
        Public Property serchColorCd() As String
            Get
                Return _serchColorCd
            End Get
            Set(ByVal Value As String)
                _serchColorCd = Value
            End Set
        End Property
        Public Property serchHinmei() As String
            Get
                Return _serchHinmei
            End Get
            Set(ByVal Value As String)
                _serchHinmei = Value
            End Set
        End Property
        Public Property serchKibouFrom() As String
            Get
                Return _serchKibouFrom
            End Get
            Set(ByVal Value As String)
                _serchKibouFrom = Value
            End Set
        End Property
        Public Property serchKibouTo() As String
            Get
                Return _serchKibouTo
            End Get
            Set(ByVal Value As String)
                _serchKibouTo = Value
            End Set
        End Property
        '2010/11/17 delete start Nakazawa
        'Public Property serchNoukiFrom() As String
        '    Get
        '        Return _serchNoukiFrom
        '    End Get
        '    Set(ByVal Value As String)
        '        _serchNoukiFrom = Value
        '    End Set
        'End Property
        'Public Property serchNoukiTo() As String
        '    Get
        '        Return _serchNoukiTo
        '    End Get
        '    Set(ByVal Value As String)
        '        _serchNoukiTo = Value
        '    End Set
        'End Property
        '2010/11/17 delete end Nakazawa
    End Structure

    '検索条件
    Private _sqlWhere As String = ""
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
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByVal prmForm As ZC110M_Menu, ByVal prmUpdFlg As Boolean)
        Call Me.New()

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _parentForm = prmForm                                               '親フォーム
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        _updFlg = prmUpdFlg
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

            '自画面を終了し、メニュー画面に戻る。
            _parentForm.Show()
            _parentForm.Activate()

            Me.Close()
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '　検索ボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnKensaku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKensaku.Click
        Try

            '日付チェック
            Call checkDate()

            ' 元のWaitカーソルを保持
            Dim preCursor As Cursor = Me.Cursor
            ' カーソルを待機カーソルに変更
            Me.Cursor = Cursors.WaitCursor

            Try
                '検索条件の作成
                _sqlWhere = createSerchStr()

                '列着色フラグ無効
                _colorCtlFlg = False

                '一覧表示
                Call dispDGV(_sqlWhere)

                ''2011/01/17 del start sugano
                ''処理制御テーブルを更新する
                '_parentForm.updateSeigyoTbl(PGID, True, Now(), Now())
                ''2011/01/17 del end sugano

                '一覧行着色フラグを有効にする
                _colorCtlFlg = True

                '一覧の最初の入力可能セルへフォーカスする
                setForcusCol(dgvTehaiData.CurrentCellAddress.Y, 0)
            Finally
                ' カーソルを元に戻す
                Me.Cursor = preCursor
            End Try
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '在庫補充リスト出力ボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnInsatu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsatu.Click
        Try

            ' 元のWaitカーソルを保持
            Dim preCursor As Cursor = Me.Cursor
            ' カーソルを待機カーソルに変更
            Me.Cursor = Cursors.WaitCursor
            Try

                'EXCEL出力
                Call printExcel(_sqlWhere)

            Finally
                ' カーソルを元に戻す
                Me.Cursor = preCursor
            End Try
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '修正ボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnSyuusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSyuusei.Click
        Try
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvTehaiData)
            Dim rowcnt As Integer = dgvTehaiData.CurrentCellAddress.Y

            Dim kahenKey As String = gh.getCellData(COLDT_TEHAINO, rowcnt)    '現在カーソルがある行の可変キーを取得
            Dim syori As String = Trim(Replace(lblSyori.Text, "/", ""))       '処理年月
            Dim keikaku As String = Trim(Replace(lblKeikaku.Text, "/", ""))   '計画年月

            Dim openForm As ZG621E_TehaiSyuuseiSyousai = New ZG621E_TehaiSyuuseiSyousai(_msgHd, _db, Me, kahenKey, syori, keikaku, _updFlg, _parentForm)      '画面遷移
            openForm.ShowDialog(Me)                                                                       '画面表示
            openForm.Dispose()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-->2010.12.12 add by takagi 
    Private Sub dgvTehaiData_CellContentDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTehaiData.CellContentDoubleClick
        If e.RowIndex < 0 Then Exit Sub
        Call btnSyuusei_Click(Nothing, Nothing) '修正ボタン押下転送
    End Sub
    '<--2010.12.12 add by takagi 

#End Region

#Region "ユーザ定義関数:画面制御"
    '-------------------------------------------------------------------------------
    '　画面起動時
    '-------------------------------------------------------------------------------
    Private Sub initForm()
        Try
            '修正ボタン・在庫補充リスト出力ボタン使用不可
            btnSyuusei.Enabled = False
            btnInsatu.Enabled = False

            '処理年月、計画年月表示
            Call dispDate()

            '-->2010.12.27 chg by takagi #53
            ' '' 2010/12/22 add start sugano
            ''列着色フラグ無効
            '_colorCtlFlg = False

            ''一覧表示
            'Call dispDGV("")

            ''処理制御テーブルを更新する
            '_parentForm.updateSeigyoTbl(PGID, True, Now(), Now())

            ''一覧行着色フラグを有効にする
            '_colorCtlFlg = True

            ''一覧の最初の入力可能セルへフォーカスする
            'setForcusCol(dgvTehaiData.CurrentCellAddress.Y, 0)
            ' '' 2010/12/22 add end sugano
            Call btnKensaku_Click(Nothing, Nothing)
            ''<--2010.12.27 chg by takagi #53

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　検索条件SQL作成
    '　(処理概要)画面に入力された検索条件をSQL文にする
    '　　I　：　なし
    '　　R　：　createSerchStr      '検索条件
    '-------------------------------------------------------------------------------
    Private Function createSerchStr() As String
        Try
            
            createSerchStr = ""

            '検索条件の保持
            _SerchCriteria.serchTehaiNo = txtTehaiNo.Text        '手配№
            _SerchCriteria.serchSiyoCd = txtSiyouCD.Text         '仕様コード
            _SerchCriteria.serchHinsyuCd = txtHinsyuCD.Text      '品種コード
            _SerchCriteria.serchSensinCd = txtSensinsuu.Text     '線心数コード
            _SerchCriteria.serchSizeCd = txtSize.Text            'サイズコード
            _SerchCriteria.serchColorCd = txtColor.Text          '色コード
            _SerchCriteria.serchHinmei = txtHinmei.Text          '品名
            _SerchCriteria.serchKibouFrom = Trim(Replace(txtKibouFrom.Text, "/", ""))    '希望出来日(From)
            _SerchCriteria.serchKibouTo = Trim(Replace(txtKibouTo.Text, "/", ""))        '希望出来日(To)
            '2010/11/17 delete start Nakazawa
            '_SerchCriteria.serchNoukiFrom = Trim(Replace(txtNoukiFrom.Text, "/", ""))    '納期(From)
            '_SerchCriteria.serchNoukiTo = Trim(Replace(txtNoukiTo.Text, "/", ""))        '納期(To)
            '2010/11/17 delete end Nakazawa

            '手配№
            If Not "".Equals(_SerchCriteria.serchTehaiNo) Then
                If Not "".Equals(createSerchStr) Then
                    createSerchStr = createSerchStr & N & " AND "
                End If
                createSerchStr = createSerchStr & " TEHAI_NO LIKE '" & _SerchCriteria.serchTehaiNo & "%'"
            End If

            '品名コード仕様
            If Not "".Equals(_SerchCriteria.serchSiyoCd) Then
                If Not "".Equals(createSerchStr) Then
                    createSerchStr = createSerchStr & N & " AND "
                End If
                createSerchStr = createSerchStr & " H_SIYOU_CD LIKE '" & _SerchCriteria.serchSiyoCd & "%'"
            End If

            '品名コード品種
            If Not "".Equals(_SerchCriteria.serchHinsyuCd) Then
                If Not "".Equals(createSerchStr) Then
                    createSerchStr = createSerchStr & N & " AND "
                End If
                createSerchStr = createSerchStr & " H_HIN_CD LIKE '" & _SerchCriteria.serchHinsyuCd & "%'"
            End If

            '品名コード線心数
            If Not "".Equals(_SerchCriteria.serchSensinCd) Then
                If Not "".Equals(createSerchStr) Then
                    createSerchStr = createSerchStr & N & " AND "
                End If
                createSerchStr = createSerchStr & " H_SENSIN_CD LIKE '" & _SerchCriteria.serchSensinCd & "%'"
            End If

            '品名コードサイズ
            If Not "".Equals(_SerchCriteria.serchSizeCd) Then
                If Not "".Equals(createSerchStr) Then
                    createSerchStr = createSerchStr & N & " AND "
                End If
                createSerchStr = createSerchStr & " H_SIZE_CD LIKE '" & _SerchCriteria.serchSizeCd & "%'"
            End If

            '品名コード品種
            If Not "".Equals(_SerchCriteria.serchColorCd) Then
                If Not "".Equals(createSerchStr) Then
                    createSerchStr = createSerchStr & N & " AND "
                End If
                createSerchStr = createSerchStr & " H_COLOR_CD LIKE '" & _SerchCriteria.serchColorCd & "%'"
            End If

            '品名
            If Not "".Equals(_SerchCriteria.serchHinmei) Then
                If Not "".Equals(createSerchStr) Then
                    createSerchStr = createSerchStr & N & " AND "
                End If
                createSerchStr = createSerchStr & " HINMEI LIKE '%" & _SerchCriteria.serchHinmei & "%'"
            End If

            '希望出来日(From)
            If Not "".Equals(_SerchCriteria.serchKibouFrom) Then
                If Not "".Equals(createSerchStr) Then
                    createSerchStr = createSerchStr & N & " AND "
                End If
                createSerchStr = createSerchStr & " KIBOU_DATE >= '" & _SerchCriteria.serchKibouFrom & "'"
            End If
            '希望出来日(To)
            If Not "".Equals(_SerchCriteria.serchKibouTo) Then
                If Not "".Equals(createSerchStr) Then
                    createSerchStr = createSerchStr & N & " AND "
                End If
                createSerchStr = createSerchStr & " KIBOU_DATE <= '" & _SerchCriteria.serchKibouTo & "'"
            End If
            '2010/11/17 delete start Nakazawa
            '納期(From)
            'If Not "".Equals(_SerchCriteria.serchNoukiFrom) Then
            '    If Not "".Equals(createSerchStr) Then
            '        createSerchStr = createSerchStr & N & " AND "
            '    End If
            '    createSerchStr = createSerchStr & " NOUKI >= '" & _SerchCriteria.serchNoukiFrom & "'"
            'End If
            ''納期(To)
            'If Not "".Equals(_SerchCriteria.serchNoukiTo) Then
            '    If Not "".Equals(createSerchStr) Then
            '        createSerchStr = createSerchStr & N & " AND "
            '    End If
            '    createSerchStr = createSerchStr & " NOUKI <= '" & _SerchCriteria.serchNoukiTo & "'"
            'End If
            '2010/11/17 delete end Nakazawa

            If Not "".Equals(createSerchStr) Then
                createSerchStr = " WHERE " & createSerchStr
            End If
        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Function

    '-------------------------------------------------------------------------------
    '　コントロールキー押下イベント
    '　(処理概要)エンターボタン押下時に次のコントロールに移る
    '-------------------------------------------------------------------------------
    Private Sub txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTehaiNo.KeyPress, _
                                                                                                                txtHinsyuCD.KeyPress, _
                                                                                                                txtSiyouCD.KeyPress, _
                                                                                                                txtSensinsuu.KeyPress, _
                                                                                                                txtSize.KeyPress, _
                                                                                                                txtColor.KeyPress, _
                                                                                                                txtHinmei.KeyPress, _
                                                                                                                txtKibouFrom.KeyPress, _
                                                                                                                txtKibouTo.KeyPress
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
    Private Sub txt_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTehaiNo.GotFocus, _
                                                                                            txtHinsyuCD.GotFocus, _
                                                                                            txtSiyouCD.GotFocus, _
                                                                                            txtSensinsuu.GotFocus, _
                                                                                            txtSize.GotFocus, _
                                                                                            txtColor.GotFocus, _
                                                                                            txtHinmei.GotFocus, _
                                                                                            txtKibouFrom.GotFocus, _
                                                                                            txtKibouTo.GotFocus
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
    ' 　更新データの受け取り
    '   (処理概要)子画面で更新されたデータを受け取る
    '　　I　：　prmKibou     　　 希望出来日
    '　　I　：　prmTehaiSuuryou   手配数量
    '　　I　：　prmTantyou        単長
    '　　I　：　prmJousuu         条数
    '　　I　：　prmSiyousyoNo     仕様書番号
    '-------------------------------------------------------------------------------
    '2010/12/02 update start Nakazawa---
    '2010/11/17 update start Nakazawa
    'Sub setUpDateData(ByVal prmKibou As String, ByVal prmNouki As String, ByVal prmTehaiSuuryou As String, _
    '                ByVal prmTantyou As String, ByVal prmJousuu As String, ByVal prmSiyousyoNo As String) Implements IfRturnUpDateData.setUpDateData
    'Sub setUpDateData(ByVal prmKibou As String, ByVal prmTehaiSuuryou As String, _
    '                     ByVal prmTantyou As String, ByVal prmJousuu As String, ByVal prmSiyousyoNo As String) Implements IfRturnUpDateData.setUpDateData
    '2010/11/17 update end Nakazawa
    Sub setUpDateData(ByVal prmKibou As String, ByVal prmTehaiSuuryou As String, _
                ByVal prmTantyou As String, ByVal prmJousuu As String, ByVal prmSiyousyoNo As String, ByVal prmTaisyogaiFlg As Boolean) Implements IfRturnUpDateData.setUpDateData
        '2010/12/02 update end Nakazawa---

        Try
            '子画面で入力された内容を一覧に反映する
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvTehaiData)
            gh.setCellData(COLDT_SYUTTAIBI, dgvTehaiData.CurrentRow.Index, prmKibou)                '希望日
            '2010/11/17 delete start Nakazawa
            'gh.setCellData(COLDT_NOUKI, dgvTehaiData.CurrentRow.Index, prmNouki)                  '納期
            '2010/11/17 delete end Nakazawa
            gh.setCellData(COLDT_TEHAISUURYOU, dgvTehaiData.CurrentRow.Index, prmTehaiSuuryou)      '手配数量
            gh.setCellData(COLDT_TANTYOU, dgvTehaiData.CurrentRow.Index, prmTantyou)                '単長
            gh.setCellData(COLDT_JOUSUU, dgvTehaiData.CurrentRow.Index, prmJousuu)                  '条数
            gh.setCellData(COLDT_SIYOUSYONO, dgvTehaiData.CurrentRow.Index, prmSiyousyoNo)          '仕様書番号

            '2010/12/02 add start Nakazawa
            Dim taisyogai As String = ""        '対象外の列に表示する値
            If prmTaisyogaiFlg Then
                '詳細画面で対象外にチェックされていた場合は、汎用マスタから表示内容を取得する
                Dim sql As String = ""
                sql = "SELECT NAME1 FROM M01HANYO WHERE KOTEIKEY = '" & COTEI_GAIFLG & "'"
                'SQL発行
                Dim iRecCnt As Integer          'データセットの行数
                Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)
                If iRecCnt > 0 Then
                    taisyogai = _db.rmNullStr(ds.Tables(RS).Rows(0)("NAME1")).ToString
                End If
            Else
                'チェックされていない場合は空を表示する
                taisyogai = ""
            End If
            gh.setCellData(COLDT_TAISYOGAI, dgvTehaiData.CurrentRow.Index, taisyogai)          '対象外
            '2010/12/02 add end Nakazawa

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   myShowメソッド
    '-------------------------------------------------------------------------------
    Public Sub myShow() Implements IfRturnUpDateData.myShow
        Me.Show()
    End Sub

    '-------------------------------------------------------------------------------
    '   myActivateメソッド
    '-------------------------------------------------------------------------------
    Public Sub myActivate() Implements IfRturnUpDateData.myActivate
        Me.Activate()
    End Sub

#End Region

#Region "ユーザ定義関数:EXCEL関連"
    '------------------------------------------------------------------------------------------------------
    '　EXCEL出力処理
    '------------------------------------------------------------------------------------------------------
    Private Sub printExcel(ByVal prmSql As String)
        Try
            '雛形ファイル
            Dim openFilePath As String = StartUp.iniValue.BaseXlsPath & "\" & StartUp.iniValue.ExcelZG640R1_Base
            '雛形ファイルが開かれていないかチェック
            Dim fh As UtilFile = New UtilFile()
            Try
                fh.move(openFilePath, openFilePath & 1)
                fh.move(openFilePath & 1, openFilePath)
            Catch ioe As System.IO.IOException
                Throw New UsrDefException("ファイルが開かれています。ファイルを閉じてから再実行してください。", _
                                          _msgHd.getMSG("fileOpenErr", "【ファイル名】：" & openFilePath))
            End Try

            '出力用ファイル
            'ファイル名取得-----------------------------------------------------
            Dim wkEditFile As String = StartUp.iniValue.OutXlsPath & "\" & StartUp.iniValue.ExcelZG640R1_Out     'コピー先ファイル

            'コピー先ファイルが存在する場合、コピー先ファイルを削除----------------
            If UtilClass.isFileExists(wkEditFile) Then
                Try
                    fh.delete(wkEditFile)
                Catch ioe As System.IO.IOException
                    Throw New UsrDefException("ファイルが開かれています。ファイルを閉じてから再実行してください。", _
                                              _msgHd.getMSG("fileOpenErr", "【ファイル名】：" & wkEditFile))
                End Try
            End If

            Try
                '出力用ファイルへ雛型ファイルコピー
                FileCopy(openFilePath, wkEditFile)
            Catch ioe As System.IO.IOException
                Throw New UsrDefException(ioe, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ioe)))
            End Try

            Dim eh As xls.UtilExcelHandler = New xls.UtilExcelHandler(wkEditFile)
            Try
                'コピー先ファイル開く
                eh.open()
                Try
                    Dim startPrintRow As Integer = START_PRINT                                           '出力開始行数
                    Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvTehaiData)        'DGVハンドラの設定
                    Dim rowCnt As Integer = gh.getMaxRow
                    Dim i As Integer
                    Dim j As Integer = 0
                    Dim k As Integer = 0
                    Dim sql As String = ""
                    sql = "SELECT "
                    sql = sql & N & " T51.TEHAI_NO " & COLDT_TEHAINO
                    sql = sql & N & " ,M011.NAME2 " & COLDT_JUYOU
                    sql = sql & N & " ,M012.NAME1 " & COLDT_SEISAKU
                    sql = sql & N & " ,T51.HINMEI_CD " & COLDT_HINMEICD
                    sql = sql & N & " ,T51.HINMEI " & COLDT_HINMEI
                    sql = sql & N & " ,TO_CHAR(TO_DATE(T51.KIBOU_DATE,'YYYYMMDD'),'mm/dd') " & COLDT_SYUTTAIBI
                    sql = sql & N & " ,T51.TEHAI_SUU " & COLDT_TEHAISUURYOU
                    sql = sql & N & " ,T51.TANCYO " & COLDT_TANTYOU
                    sql = sql & N & " ,T51.JYOSU " & COLDT_JOUSUU
                    sql = sql & N & " ,T51.MAKI_CD " & COLDT_MAKIWAKU
                    sql = sql & N & " ,T51.HOSO_KBN " & COLDT_HOUSOU
                    sql = sql & N & " ,T51.SIYOUSYO_NO " & COLDT_SIYOUSYONO
                    sql = sql & N & " ,M013.NAME1 " & COLDT_SEIZOUBMN
                    sql = sql & N & " FROM T51TEHAI T51 "
                    '' 2010/12/22 upd start sugano
                    'sql = sql & N & "   LEFT OUTER JOIN M01HANYO M011 ON "
                    sql = sql & N & "   LEFT OUTER JOIN (SELECT * FROM M01HANYO WHERE KOTEIKEY = '01') M011 ON "
                    '' 2010/12/22 upd end sugano
                    sql = sql & N & "   T51.JUYOUCD = M011.KAHENKEY "
                    sql = sql & N & "   AND M011.KOTEIKEY = '" & COTEI_JUYOU & "'"
                    sql = sql & N & "   LEFT OUTER JOIN M01HANYO M012 ON "
                    sql = sql & N & "   T51.SEISAKU_KBN = M012.KAHENKEY "
                    sql = sql & N & "   AND M012.KOTEIKEY = '" & COTEI_SEISAKU & "'"
                    sql = sql & N & "   LEFT OUTER JOIN M01HANYO M013 ON "
                    sql = sql & N & "   T51.SEIZOU_BMN = M013.KAHENKEY "
                    sql = sql & N & "   AND M013.KOTEIKEY = '" & COTEI_SEIZOUBMN & "'"
                    '' 2010/12/22 add start sugano
                    sql = sql & N & " WHERE GAI_FLG IS NULL "   '対象外フラグが1のデータを除く
                    '' 2010/12/22 add start sugano

                    If Not "".Equals(prmSql) Then
                        '' 2010/12/22 upd start sugano
                        'sql = sql & N & prmSql
                        sql = sql & N & " AND " & prmSql.Replace("WHERE", "")
                        '' 2010/12/22 upd end sugano
                    End If
                    sql = sql & N & " ORDER BY T51.TEHAI_NO "

                    'SQL発行
                    Dim iRecCnt As Integer          'データセットの行数
                    Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

                    If iRecCnt <= 0 Then                    '抽出レコードが１件もない場合
                        Throw New UsrDefException("対象データがありません。", _msgHd.getMSG("NonData"))
                    End If

                    '取得したデータをEXCELに展開する
                    For i = 0 To iRecCnt - 1
                        If XLSSHEET_DENSEN.Equals(_db.rmNullStr(ds.Tables(RS).Rows(i)(COLDT_SEIZOUBMN))) Then
                            '製造部門が電線の場合
                            printDetails(ds, eh, XLSSHEET_DENSEN, i, startPrintRow, j)
                            j = j + 1    '電線カウントアップ
                        Else
                            '製造部門が通信の場合
                            printDetails(ds, eh, XLSSHEET_TSUSIN, i, startPrintRow, k)
                            k = k + 1    '通信カウントアップ
                        End If
                    Next

                    '電線の出力件数が存在する場合、ヘッダーを編集し、存在しない場合はシートを削除
                    If j > 0 Then
                        'EXCELヘッダー編集
                        printHeader(eh, XLSSHEET_DENSEN, startPrintRow, j)
                    Else
                        'シートを削除
                        eh.deleteSheet(XLSSHEET_DENSEN)
                    End If
                    '通信の出力件数が存在する場合、ヘッダーを編集し、存在しない場合はシートを削除
                    If k > 0 Then
                        'EXCELヘッダー編集
                        printHeader(eh, XLSSHEET_TSUSIN, startPrintRow, k)
                    Else
                        'シートを削除
                        eh.deleteSheet(XLSSHEET_TSUSIN)
                    End If

                    Clipboard.Clear()         'クリップボードの初期化
                Finally
                    eh.close()
                End Try

                'EXCELファイル開く
                eh.display()

            Catch ue As UsrDefException
                ue.dspMsg()
                Throw ue
            Catch ex As Exception
                'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)), , StartUp.iniValue.LogFilePath)
            Finally
                eh.endUse()
                eh = Nothing
            End Try

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　EXCEL明細編集
    '　(処理概要)EXCELに出力する明細を編集する
    '　　I　：　prmds     　　　データセット
    '　　I　：　prmeh     　　　EXCELハンドラー
    '　　I　：　prmSeizoubmn 　 製造区分
    '　　I　：　prmDbRows     　データセットインデックス
    '　　I　：　prmStartRow     出力開始行数
    '　　I　：　prmRow　　　　  出力件数
    '-------------------------------------------------------------------------------
    Private Sub printDetails(ByVal prmds As DataSet, ByVal prmeh As xls.UtilExcelHandler, ByVal prmSeizoubmn As String, ByVal prmDbRows As Integer, ByVal prmStartRow As Integer, ByVal prmRow As Integer)
        Try
            prmeh.selectSheet(prmSeizoubmn)
            '列を1行追加
            prmeh.copyRow(prmStartRow + prmRow)
            prmeh.insertPasteRow(prmStartRow + prmRow)
            '一覧データ出力
            prmeh.setValue(_db.rmNullStr(prmds.Tables(RS).Rows(prmDbRows)(COLDT_TEHAINO)), prmStartRow + prmRow, XLSCOL_TEHAINO)              '手配№
            prmeh.setValue(_db.rmNullStr(prmds.Tables(RS).Rows(prmDbRows)(COLDT_JUYOU)), prmStartRow + prmRow, XLSCOL_JUYOU)                  '需要先名
            prmeh.setValue(_db.rmNullStr(prmds.Tables(RS).Rows(prmDbRows)(COLDT_SEISAKU)), prmStartRow + prmRow, XLSCOL_SEISAKU)              '製作区分
            prmeh.setValue(_db.rmNullStr(prmds.Tables(RS).Rows(prmDbRows)(COLDT_HINMEICD)), prmStartRow + prmRow, XLSCOL_HINMEICD)            '品名コード
            prmeh.setValue(_db.rmNullStr(prmds.Tables(RS).Rows(prmDbRows)(COLDT_HINMEI)), prmStartRow + prmRow, XLSCOL_HINMEI)                '品名・サイズ・色
            prmeh.setValue(_db.rmNullStr(prmds.Tables(RS).Rows(prmDbRows)(COLDT_SYUTTAIBI)), prmStartRow + prmRow, XLSCOL_SYUTTAIBI)          '希望出来日
            prmeh.setValue(_db.rmNullStr(prmds.Tables(RS).Rows(prmDbRows)(COLDT_TEHAISUURYOU)), prmStartRow + prmRow, XLSCOL_TEHAISUURYOU)    '手配数量
            prmeh.setValue(_db.rmNullStr(prmds.Tables(RS).Rows(prmDbRows)(COLDT_TANTYOU)), prmStartRow + prmRow, XLSCOLTANTYOU)               '単長
            prmeh.setValue(_db.rmNullStr(prmds.Tables(RS).Rows(prmDbRows)(COLDT_JOUSUU)), prmStartRow + prmRow, XLSCOL_JOUSUU)                '条数
            prmeh.setValue(_db.rmNullStr(prmds.Tables(RS).Rows(prmDbRows)(COLDT_MAKIWAKU)), prmStartRow + prmRow, XLSCOL_MAKIWAKU)            '巻枠コード
            prmeh.setValue(_db.rmNullStr(prmds.Tables(RS).Rows(prmDbRows)(COLDT_HOUSOU)), prmStartRow + prmRow, XLSCOL_HOUSOU)                '包装/表示区分
            prmeh.setValue(_db.rmNullStr(prmds.Tables(RS).Rows(prmDbRows)(COLDT_SIYOUSYONO)), prmStartRow + prmRow, XLSCOL_SIYOUSYONO)        '仕様書番号
        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)), , StartUp.iniValue.LogFilePath)
        End Try
    End Sub
    '-------------------------------------------------------------------------------
    '　EXCELヘッダー編集
    '　(処理概要)EXCELに出力するヘッダーを編集する
    '　　I　：　prmeh     　　　EXCELハンドラー
    '　　I　：　prmSeizoubmn 　 製造区分
    '　　I　：　prmStartRow     出力開始行数
    '　　I　：　prmRow　　　　  出力件数
    '-------------------------------------------------------------------------------
    Private Sub printHeader(ByVal prmeh As xls.UtilExcelHandler, ByVal prmSeizoubmn As String, ByVal prmStartRow As Integer, ByVal prmRow As Integer)
        Try
            Dim startPrintRow As Integer = START_PRINT                                           '出力開始行数

            prmeh.selectSheet(prmSeizoubmn)
            '余分な空行を削除
            prmeh.deleteRow(prmStartRow + prmRow)

            '作成日時編集
            Dim printDate As String = Now.ToString("yyyy/MM/dd HH:mm")
            prmeh.setValue("作成日時 ： " & printDate, 1, 15)   'O1
            '処理年月、計画年月編集
            prmeh.setValue("処理年月：" & lblSyori.Text & "　　計画年月：" & lblKeikaku.Text, 4, 1)    'A4

            '検索条件編集
            Dim dtKibouFrom As String = Trim(Replace(txtKibouFrom.Text, "/", ""))
            Dim dtKibouTo As String = Trim(Replace(txtKibouTo.Text, "/", ""))
            '2010/11/17 delete start Nakazawa
            'Dim dtNoukiFrom As String = Trim(Replace(txtNoukiFrom.Text, "/", ""))
            'Dim dtNoukiTo As String = Trim(Replace(txtNoukiTo.Text, "/", ""))
            '2010/11/17 delete end Nakazawa
            Dim serchKibou As String = ""
            Dim serchNouki As String = ""
            If Not "".Equals(dtKibouFrom) Or Not "".Equals(dtKibouTo) Then
                serchKibou = dtKibouFrom & "～" & dtKibouTo
            End If
            '2010/11/17 delete start Nakazawa
            'If Not "".Equals(dtNoukiFrom) Or Not "".Equals(dtNoukiTo) Then
            '    serchNouki = dtNoukiFrom & "～" & dtNoukiTo
            'End If
            '2010/11/17 delete end Nakazawa

            '検索条件
            '2010/11/17 update start Nakazawa
            'prmeh.setValue("手配№：" & _SerchCriteria.serchTehaiNo & "　　品名ｺｰﾄﾞ：" & createHinmeiCd() & "　　品名：" & _SerchCriteria.serchHinmei & _
            '            "　　希望出来日：" & serchKibou & "　　納期：" & serchNouki, 6, 1) 'A6
            prmeh.setValue("手配№：" & _SerchCriteria.serchTehaiNo & "　　品名ｺｰﾄﾞ：" & createHinmeiCd() & "　　品名：" & _SerchCriteria.serchHinmei & _
                                    "　　希望出来日：" & serchKibou, 6, 1) 'A6

            '2010/11/17 update end Nakazawa
            '左上のセルにフォーカス当てる
            prmeh.selectCell(11, 1)     'A11

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)), , StartUp.iniValue.LogFilePath)
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　品名コード作成
    '　(処理概要)EXCELに出力する品名コードを編集して返す。
    '　　I　：　なし
    '　　R　：　createHinmeiCd      '編集した品名コード
    '-------------------------------------------------------------------------------
    Private Function createHinmeiCd() As String
        createHinmeiCd = ""

        '仕様コード
        If _SerchCriteria.serchSiyoCd.Length = 2 Then
            createHinmeiCd = _SerchCriteria.serchSiyoCd & "-"
        ElseIf _SerchCriteria.serchSiyoCd.Length = 1 Then
            createHinmeiCd = _SerchCriteria.serchSiyoCd.Substring(0, 1) & "*-"
        Else
            createHinmeiCd = "**-"
        End If

        '品種コード
        If _SerchCriteria.serchHinsyuCd.Length = 3 Then
            createHinmeiCd = createHinmeiCd & _SerchCriteria.serchHinsyuCd & "-"
        ElseIf _SerchCriteria.serchHinsyuCd.Length = 2 Then
            createHinmeiCd = createHinmeiCd & _SerchCriteria.serchHinsyuCd.Substring(0, 2) & "*-"
        ElseIf _SerchCriteria.serchHinsyuCd.Length = 1 Then
            createHinmeiCd = createHinmeiCd & _SerchCriteria.serchHinsyuCd.Substring(0, 1) & "**-"
        Else
            createHinmeiCd = createHinmeiCd & "***-"
        End If

        '線心数コード
        If _SerchCriteria.serchSensinCd.Length = 3 Then
            createHinmeiCd = createHinmeiCd & _SerchCriteria.serchSensinCd & "-"
        ElseIf _SerchCriteria.serchSensinCd.Length = 2 Then
            createHinmeiCd = createHinmeiCd & _SerchCriteria.serchSensinCd.Substring(0, 2) & "*-"
        ElseIf _SerchCriteria.serchSensinCd.Length = 1 Then
            createHinmeiCd = createHinmeiCd & _SerchCriteria.serchSensinCd.Substring(0, 1) & "**-"
        Else
            createHinmeiCd = createHinmeiCd & "***-"
        End If

        'サイズコード
        If _SerchCriteria.serchSizeCd.Length = 2 Then
            createHinmeiCd = createHinmeiCd & _SerchCriteria.serchSizeCd & "-"
        ElseIf _SerchCriteria.serchSizeCd.Length = 1 Then
            createHinmeiCd = createHinmeiCd & _SerchCriteria.serchSizeCd.Substring(0, 1) & "*-"
        Else
            createHinmeiCd = createHinmeiCd & "**-"
        End If

        '色コード
        If _SerchCriteria.serchColorCd.Length = 3 Then
            createHinmeiCd = createHinmeiCd & _SerchCriteria.serchColorCd
        ElseIf _SerchCriteria.serchColorCd.Length = 2 Then
            createHinmeiCd = createHinmeiCd & _SerchCriteria.serchColorCd.Substring(0, 2) & "*"
        ElseIf _SerchCriteria.serchColorCd.Length = 1 Then
            createHinmeiCd = createHinmeiCd & _SerchCriteria.serchColorCd.Substring(0, 1) & "**"
        Else
            createHinmeiCd = createHinmeiCd & "***"
        End If

    End Function
#End Region

#Region "ユーザ定義関数:DGV関連"

    '------------------------------------------------------------------------------------------------------
    '選択行に着色する処理
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvTehaiData_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTehaiData.CellEnter
        If _colorCtlFlg Then
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvTehaiData)
            gh.setSelectionRowColor(dgvTehaiData.CurrentCellAddress.Y, _oldRowIndex, StartUp.lCOLOR_BLUE)
        End If
        _oldRowIndex = dgvTehaiData.CurrentCellAddress.Y

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　背景色の設定処理
    '　(処理概要)行の背景色を青に着色する。
    '　　I　：　prmRowIndex     現在フォーカスがある行数
    '　　I　：　prmOldRowIndex  現在の行に移る前の行数
    '------------------------------------------------------------------------------------------------------
    Private Sub setBackcolor(ByVal prmRowIndex As Integer, ByVal prmOldRowIndex As Integer)

        Dim _dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvTehaiData)

        '指定した行の背景色を青にする
        _dgv.setSelectionRowColor(prmRowIndex, prmOldRowIndex, StartUp.lCOLOR_BLUE)

        _oldRowIndex = prmRowIndex

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　指定列へのフォーカス設定処理
    '　(処理概要)指定されたセルにフォーカスする。
    '　　I　：　prmCoIndex      フォーカスさせるセルの列数
    '　　I　：　prmRowIndex     フォーカスさせるセルの行数
    '------------------------------------------------------------------------------------------------------
    Private Sub setForcusCol(ByVal prmColIndex As Integer, ByVal prmRowIndex As Integer)

        'フォーカスをあてる
        dgvTehaiData.Focus()
        dgvTehaiData.CurrentCell = dgvTehaiData(prmColIndex, prmRowIndex)

        '背景色の設定
        Call setBackcolor(prmRowIndex, _oldRowIndex)

    End Sub
#End Region

#Region "ユーザ定義関数:DB関連"
    '-------------------------------------------------------------------------------
    '　処理年月、計画年月表示
    '　(処理概要)処理年月、計画年月を表示する
    '-------------------------------------------------------------------------------
    Private Sub dispDate()
        Try
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & " SNENGETU " & "SYORI"          '処理年月
            sql = sql & N & " ,KNENGETU " & "KEIKAKU"       '計画年月
            sql = sql & N & " FROM T01KEIKANRI "

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '抽出レコードが１件もない場合
                Throw New UsrDefException("登録されていません。", _msgHd.getMSG("noData"))
            End If

            Dim syoriDate As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("SYORI"))
            Dim keikakuDate As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("KEIKAKU"))

            '「YYYY/MM」形式で表示
            lblSyori.Text = syoriDate.Substring(0, 4) & "/" & syoriDate.Substring(4, 2)
            lblKeikaku.Text = keikakuDate.Substring(0, 4) & "/" & keikakuDate.Substring(4, 2)

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　一覧表示
    '　(処理概要)一覧を表示する
    '　　I　：　prmSql      検索条件
    '-------------------------------------------------------------------------------
    Private Sub dispDGV(Optional ByVal prmSql As String = "")
        Try
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & "  M01.NAME1 " & COLDT_TAISYOGAI
            sql = sql & N & " ,T51.TEHAI_NO " & COLDT_TEHAINO
            sql = sql & N & " ,TO_CHAR(TO_DATE(T51.KIBOU_DATE,'YYYYMMDD'),'yy/mm/dd') " & COLDT_SYUTTAIBI
            '2010/11/17 delete start Nakazawa
            'sql = sql & N & " ,TO_CHAR(TO_DATE(T51.NOUKI,'YYYYMMDD'),'yy/mm/dd') " & COLDT_NOUKI
            '2010/11/17 delete end Nakazawa
            sql = sql & N & " ,T51.HINMEI_CD " & COLDT_HINMEICD
            sql = sql & N & " ,T51.HINMEI " & COLDT_HINMEI
            sql = sql & N & " ,T51.TEHAI_SUU " & COLDT_TEHAISUURYOU
            sql = sql & N & " ,T51.TANCYO " & COLDT_TANTYOU
            sql = sql & N & " ,T51.JYOSU " & COLDT_JOUSUU
            sql = sql & N & " ,T51.SIYOUSYO_NO " & COLDT_SIYOUSYONO
            sql = sql & N & " FROM T51TEHAI T51 "
            sql = sql & N & "   LEFT OUTER JOIN M01HANYO M01 ON "
            sql = sql & N & "   T51.GAI_FLG = M01.KAHENKEY "
            sql = sql & N & "   AND M01.KOTEIKEY = '" & COTEI_GAIFLG & "'"
            If Not "".Equals(prmSql) Then
                sql = sql & N & prmSql
            End If
            sql = sql & N & " ORDER BY T51.TEHAI_NO "

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            '抽出データを一覧に表示する
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvTehaiData)
            gh.clearRow()

            If iRecCnt <= 0 Then                    '抽出レコードが１件もない場合

                '修正ボタン・在庫補充リスト出力ボタン使用不可
                btnSyuusei.Enabled = False
                btnInsatu.Enabled = False

                '検索件数を表示()
                lblKensuu.Text = CStr(iRecCnt) & "件"

                Throw New UsrDefException("対象データがありません。", _msgHd.getMSG("NonData"))
            Else
                '抽出データがある場合、登録ボタン・EXCELボタンを有効にする
                btnSyuusei.Enabled = True
                btnInsatu.Enabled = True
            End If

            dgvTehaiData.DataSource = ds
            dgvTehaiData.DataMember = RS

            '検索件数を表示
            lblKensuu.Text = CStr(iRecCnt) & "件"

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub
#End Region

#Region "ユーザ定義関数:チェック処理"
    '------------------------------------------------------------------------------------------------------
    '  日付チェック
    '　(処理概要)入力された日付の大小関係をチェックする
    '------------------------------------------------------------------------------------------------------
    Private Sub checkDate()
        Try
            Dim dtKibouFrom As String = Trim(Replace(txtKibouFrom.Text, "/", ""))
            Dim dtKibouTo As String = Trim(Replace(txtKibouTo.Text, "/", ""))
            '2010/11/17 delete start Nakazawa
            'Dim dtNoukiFrom As String = Trim(Replace(txtNoukiFrom.Text, "/", ""))
            'Dim dtNoukiTo As String = Trim(Replace(txtNoukiTo.Text, "/", ""))
            '2010/11/17 delete end Nakazawa

            '希望出来日
            If Not "".Equals(dtKibouFrom) And Not "".Equals(dtKibouTo) Then
                If Date.Compare(Date.Parse(Format(CInt(dtKibouFrom), "0000/00/00")), Date.Parse(Format(CInt(dtKibouTo), "0000/00/00"))) > 0 Then
                    Throw New UsrDefException("希望出来日の大小関係が不正です。", _msgHd.getMSG("ErrHaniChk", "希望出来日"), txtKibouFrom)
                End If
            End If

            '2010/11/17 delete start Nakazawa
            '納期
            'If Not "".Equals(dtNoukiFrom) And Not "".Equals(dtNoukiTo) Then
            '    If Date.Compare(Date.Parse(Format(CInt(dtNoukiFrom), "0000/00/00")), Date.Parse(Format(CInt(dtNoukiTo), "0000/00/00"))) > 0 Then
            '        Throw New UsrDefException("納期の大小関係が不正です。", _msgHd.getMSG("ErrHaniChk", "納期"), txtNoukiFrom)
            '    End If
            'End If
            '2010/11/17 delete end Nakazawa

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub
#End Region

    '-->2010.12.27 add by takagi
    Private Sub dgvTehaiData_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTehaiData.CellDoubleClick
        Call btnSyuusei_Click(Nothing, Nothing)
    End Sub
    '<--2010.12.27 add by takagi

End Class