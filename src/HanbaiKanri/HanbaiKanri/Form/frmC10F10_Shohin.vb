'===============================================================================
'
'　北日本電線株式会社
'　　（システム名）在庫計画システム
'　　（処理機能名）メニュー画面
'    （フォームID）ZC110M_Menu
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   高木        2010/10/15                 新規              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls

Public Class frmC10F10_Shohin
    Inherits System.Windows.Forms.Form

    ''-------------------------------------------------------------------------------
    ''   構造体定義
    ''-------------------------------------------------------------------------------
    'Private Structure UpdatableType
    '    Public updFlgSyokisettei As Boolean         '初期設定
    '    Public updFlgSetteitisyuusei As Boolean     '希望出来日修正
    '    Public updFlgTDTorikomi As Boolean          '手配済ﾃﾞｰﾀ取込
    '    Public updFlgNDTorikomi As Boolean          '入庫済ﾃﾞｰﾀ取込
    '    Public updFlgSDSyuusei As Boolean           '生産量ﾃﾞｰﾀ修正
    '    Public updFlgSeisanKakutei As Boolean       '生産量確定
    '    Public updFlgHKNyuryoku As Boolean          '品種別計画入力
    '    Public updFlgKKNyuroku As Boolean           '個別計画入力
    '    Public updFlgHJTorikomi As Boolean          '販売実績取込
    '    Public updFlgSyuukeiTenkai As Boolean       '販売計画集計展開
    '    Public updFlgHKSyuusei As Boolean           '販売計画量修正
    '    Public updFlgSKakutei As Boolean            '販売計画確定
    '    Public updFlgZaikoTorikomi As Boolean       '在庫実績取込
    '    Public updFlgSHZTorikomi As Boolean         '生産販売在庫取込
    '    Public updFlgSKSyuusei As Boolean           '生産計画数量修正
    '    Public updFlgKakutei As Boolean             '生産計画確定
    '    Public updFlgTDSakusei As Boolean           '手配ﾃﾞｰﾀ作成
    '    Public updFlgTDSyuusei As Boolean           '手配ﾃﾞｰﾀ修正・出力
    '    Public updFlgTDSousin As Boolean            '手配ﾃﾞｰﾀ作成(生産管理ｼｽﾃﾑ送信用)
    '    Public updFlgFYamadumi As Boolean           '負荷山積データ取込
    '    Public updFlgKKakunin As Boolean            '負荷山積集計結果確認
    '    Public updFlgSTDB As Boolean                '製作手配DB登録
    '    Public updFlgSinki As Boolean               '新規登録
    '    Public updFlgSyuusei As Boolean             '修正・EXCEL出力
    '    Public updFlgSakujo As Boolean              '削除
    '    Public updFlgKExcel As Boolean              '計画対象品一覧表印刷
    '    Public updFlgABC As Boolean                 'ABC分析
    '    Public updFlgHMstMente As Boolean           '品種区分マスタメンテ
    '    Public updFlgHanyoMst As Boolean            '汎用マスタメンテ
    '    Public updFlgSNouryokuMst As Boolean        '生産能力マスタメンテ
    '    Public updFlgGRenkei As Boolean             '外部システム連携
    'End Structure

    ''-------------------------------------------------------------------------------
    ''   定数定義
    ''-------------------------------------------------------------------------------
    'Private Const N As String = ControlChars.NewLine            '改行文字
    'Private Const RS As String = "RecSet"                       'レコードセットテーブル
    'Public Const NON_EXECUTE As String = "- - -"

    'PG制御文字 
    Private Const RS As String = "RecSet"                               'レコードセットテーブル



    '-------------------------------------------------------------------------------
    '   変数定義
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _BunruiSetFlg As Boolean = False   '商品分類セット完了フラグ
    Private _ShohinSetFlg As Boolean = False   '商品セット完了フラグ

    Public _selected As Boolean     'フォームからの戻り値用　選択状態　True:選択された　False:選択されなかった
    Public _selectShohinCD As String = ""   'フォームからの戻り値用　商品コード
    Public _selectShohinNM As String = ""   'フォームからの戻り値用　商品名称
    Public _selectNisugata As String = ""   'フォームからの戻り値用　荷姿


    '-------------------------------------------------------------------------------
    'デフォルトコンストラクタ（隠蔽）
    '-------------------------------------------------------------------------------
    Public Sub New()
        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()


        'フォーム上のキーイベントを拾う
        Me.KeyPreview = True

    End Sub


    'リストボックスに表示する値と内部的に持たせる値を作るクラス
    Public Class SyohinBunrui
        Private myVal As String
        Private myDispVal As String

        Public Sub New(ByVal prmVal As String, ByVal prmDispVal As String)
            Me.myVal = prmVal
            Me.myDispVal = prmDispVal
        End Sub

        Public ReadOnly Property DispVal() As String
            Get
                Return myDispVal
            End Get
        End Property

        Public ReadOnly Property Val() As String
            Get
                Return myVal
            End Get
        End Property

    End Class 'HanyoData


    '-------------------------------------------------------------------------------
    'コンストラクタ　メニューから呼ばれる
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByRef prmHanbaiSiireKbn As String)
        Call Me.New()

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint  'フォームタイトル表示

        _selected = False       '選択状態リセット


        Dim strSql As String = ""
        Try
            '販売仕入区分名
            strSql = "SELECT 文字１ FROM m90_hanyo"
            strSql = strSql & "   WHERE 会社コード= '" & frmC01F10_Login.loginValue.BumonCD & "' and 固定キー ='" & CommonConst.HANYO_HANBAISIIRE_KBN & "' and 可変キー = '" & prmHanbaiSiireKbn & "' "

            Dim reccnt As Integer = 0
            Dim ds2 As DataSet = _db.selectDB(strSql, RS, reccnt)
            If reccnt = 0 Then
                lblKBN.Text = ""
            Else
                lblKBN.Text = _db.rmNullStr(ds2.Tables(RS).Rows(0)("文字１"))
            End If
            '商品分類リスト作成
            strSql = "SELECT DISTINCT "
            strSql = strSql & "    g.大分類 as 商品分類 ,h.文字１ as 商品分類名 ,h.表示順"
            strSql = strSql & " FROM m20_goods g "
            strSql = strSql & "   inner join m90_hanyo h on h.会社コード= '" & frmC01F10_Login.loginValue.BumonCD & "' and h.固定キー ='" & CommonConst.HANYO_SHOHIN_BUNRUI & "' and h.可変キー = g.大分類 "
            strSql = strSql & " order by h.表示順 "

            reccnt = 0
            Dim ds As DataSet = _db.selectDB(strSql, RS, reccnt)

            '描画の前にすべてクリアする
            ListBox1.Items.Clear()

            Dim shohinBunruiList = New ArrayList()
            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                shohinBunruiList.Add(New SyohinBunrui(_db.rmNullStr(ds.Tables(RS).Rows(index)("商品分類")), _db.rmNullStr(ds.Tables(RS).Rows(index)("商品分類名"))))
            Next

            ListBox1.DataSource = shohinBunruiList
            ListBox1.ValueMember = "Val"
            ListBox1.DisplayMember = "DispVal"
            ListBox1.SelectedIndex = 0

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
        '商品リスト表示
        _BunruiSetFlg = True
        DispShohin()
    End Sub
    '-------------------------------------------------------------------------------
    '　商品リスト表示
    '-------------------------------------------------------------------------------
    Private Sub DispShohin()

        If _BunruiSetFlg = False Then
            Exit Sub
        End If
        '商品名リスト
        Dim strSql As String = ""
        Try
            strSql = "SELECT DISTINCT "
            strSql = strSql & "    商品コード, 商品名 ,表示順 "
            strSql = strSql & " FROM m20_goods  "
            strSql = strSql & " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' "
            strSql = strSql & "  and 大分類 = '" & Me.ListBox1.SelectedValue & "' "
            strSql = strSql & " order by 表示順 "

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(strSql, RS, reccnt)

            '描画の前にすべてクリアする
            If ds.Tables(RS).Rows.Count = 0 Then
                lblShohinCount.Text = 0
                Exit Sub
            End If
            Dim shohinBunruiList = New ArrayList()
            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                shohinBunruiList.Add(New SyohinBunrui(_db.rmNullStr(ds.Tables(RS).Rows(index)("商品コード")), _db.rmNullStr(ds.Tables(RS).Rows(index)("商品名"))))
            Next

            ListBox2.DataSource = shohinBunruiList
            ListBox2.ValueMember = "Val"
            ListBox2.DisplayMember = "DispVal"
            ListBox2.SelectedIndex = 0

            '件数表示
            lblShohinCount.Text = ListBox2.Items.Count

            '商品セット完了フラグ
            _ShohinSetFlg = True

            '荷姿リスト表示
            DispNisugata()

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub
    '-------------------------------------------------------------------------------
    '　荷姿リスト表示
    '-------------------------------------------------------------------------------
    Private Sub DispNisugata()

        If _ShohinSetFlg = False Then
            Exit Sub
        End If
        '荷姿形状リスト
        Dim strSql As String = ""
        Try
            strSql = strSql & "SELECT 0 AS 連番,'" + CommonConst.SELECT_DATA_NOT_SHITEI + "' AS 詳細情報,0 AS 表示順 "
            strSql = strSql & " UNION "
            strSql = strSql & "SELECT DISTINCT "
            strSql = strSql & "    連番, 詳細情報 ,表示順 "
            strSql = strSql & " FROM m21_goodsdtl  "
            strSql = strSql & " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' "
            strSql = strSql & "  and 商品コード = '" & Me.ListBox2.SelectedValue & "' "
            strSql = strSql & " order by 表示順 "

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(strSql, RS, reccnt)

            '描画の前にすべてクリアする
            If ds.Tables(RS).Rows.Count = 0 Then
                ListBox3.DataSource = Nothing
                ListBox3.Items.Clear()
                lblNisugataCount.Text = 0
                Exit Sub
            End If
            Dim NisugataList = New ArrayList()
            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                NisugataList.Add(New SyohinBunrui(_db.rmNullStr(ds.Tables(RS).Rows(index)("連番")), _db.rmNullStr(ds.Tables(RS).Rows(index)("詳細情報"))))
            Next

            ListBox3.DataSource = NisugataList
            ListBox3.ValueMember = "Val"
            ListBox3.DisplayMember = "DispVal"
            ListBox3.SelectedIndex = 0

            '件数表示
            lblNisugataCount.Text = ListBox3.Items.Count
        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '商品データ選択処理
    Private Sub selectShohin()

        If ListBox2.Text <> "" Then
            _selectShohinCD = ListBox2.SelectedValue       '商品コード
            _selectShohinNM = ListBox2.Text                '商品名称

            If (ListBox3.Text <> "") AndAlso (ListBox3.Text <> CommonConst.SELECT_DATA_NOT_SHITEI) Then
                _selectNisugata = ListBox3.Text           '荷姿
            Else
                _selectNisugata = ""      '荷姿
            End If
            _selected = True        '選択状態
        End If

        Me.Hide()

    End Sub

    '-------------------------------------------------------------------------------
    '　フォームロードイベント
    '-------------------------------------------------------------------------------
    Private Sub Sample_Chumon_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            '描画関係の設定
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           'サイズが変更されたときに、コントロールがコントロール自体を再描画するかどうかを示す値を設定
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '描画はバッファで実行され、完了後に、結果が画面に出力されるよう設定
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  'コントロールは、画面に直接ではなく、まずバッファに描画されます。これにより、ちらつきを抑えることができます。
            Me.SetStyle(ControlStyles.UserPaint, True)              'コントロールは、オペレーティング システムによってではなく、独自に描画されるよう設定
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   'コントロールはウィンドウ メッセージ WM_ERASEBKGND を無視するように設定

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    Private Sub InitializeComponent()
        Me.TableLayoutPanel23 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel25 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel28 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.lblShohinCount = New System.Windows.Forms.Label()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.ListBox2 = New System.Windows.Forms.ListBox()
        Me.TableLayoutPanel26 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel29 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblNisugataCount = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.ListBox3 = New System.Windows.Forms.ListBox()
        Me.TableLayoutPanel24 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.lblKBN = New System.Windows.Forms.Label()
        Me.TableLayoutPanel27 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.btnModoru = New System.Windows.Forms.Button()
        Me.TableLayoutPanel23.SuspendLayout()
        Me.TableLayoutPanel25.SuspendLayout()
        Me.TableLayoutPanel28.SuspendLayout()
        Me.TableLayoutPanel26.SuspendLayout()
        Me.TableLayoutPanel29.SuspendLayout()
        Me.TableLayoutPanel24.SuspendLayout()
        Me.TableLayoutPanel27.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel23
        '
        Me.TableLayoutPanel23.ColumnCount = 3
        Me.TableLayoutPanel23.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel23.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel23.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel23.Controls.Add(Me.TableLayoutPanel25, 1, 0)
        Me.TableLayoutPanel23.Controls.Add(Me.ListBox1, 0, 1)
        Me.TableLayoutPanel23.Controls.Add(Me.ListBox2, 1, 1)
        Me.TableLayoutPanel23.Controls.Add(Me.TableLayoutPanel26, 2, 0)
        Me.TableLayoutPanel23.Controls.Add(Me.ListBox3, 2, 1)
        Me.TableLayoutPanel23.Controls.Add(Me.TableLayoutPanel24, 0, 0)
        Me.TableLayoutPanel23.Controls.Add(Me.TableLayoutPanel27, 2, 2)
        Me.TableLayoutPanel23.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel23.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel23.Name = "TableLayoutPanel23"
        Me.TableLayoutPanel23.RowCount = 3
        Me.TableLayoutPanel23.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.45455!))
        Me.TableLayoutPanel23.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 74.54546!))
        Me.TableLayoutPanel23.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64.0!))
        Me.TableLayoutPanel23.Size = New System.Drawing.Size(860, 395)
        Me.TableLayoutPanel23.TabIndex = 0
        '
        'TableLayoutPanel25
        '
        Me.TableLayoutPanel25.ColumnCount = 1
        Me.TableLayoutPanel25.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel25.Controls.Add(Me.TableLayoutPanel28, 0, 1)
        Me.TableLayoutPanel25.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel25.Location = New System.Drawing.Point(175, 3)
        Me.TableLayoutPanel25.Name = "TableLayoutPanel25"
        Me.TableLayoutPanel25.RowCount = 2
        Me.TableLayoutPanel25.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel25.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel25.Size = New System.Drawing.Size(338, 78)
        Me.TableLayoutPanel25.TabIndex = 4
        '
        'TableLayoutPanel28
        '
        Me.TableLayoutPanel28.ColumnCount = 3
        Me.TableLayoutPanel28.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel28.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.82353!))
        Me.TableLayoutPanel28.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.958477!))
        Me.TableLayoutPanel28.Controls.Add(Me.Label35, 0, 0)
        Me.TableLayoutPanel28.Controls.Add(Me.Label37, 2, 0)
        Me.TableLayoutPanel28.Controls.Add(Me.lblShohinCount, 1, 0)
        Me.TableLayoutPanel28.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel28.Location = New System.Drawing.Point(0, 39)
        Me.TableLayoutPanel28.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel28.Name = "TableLayoutPanel28"
        Me.TableLayoutPanel28.RowCount = 1
        Me.TableLayoutPanel28.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel28.Size = New System.Drawing.Size(338, 39)
        Me.TableLayoutPanel28.TabIndex = 3
        '
        'Label35
        '
        Me.Label35.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label35.AutoSize = True
        Me.Label35.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label35.Location = New System.Drawing.Point(3, 24)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(52, 15)
        Me.Label35.TabIndex = 1
        Me.Label35.Text = "■商品"
        '
        'Label37
        '
        Me.Label37.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label37.AutoSize = True
        Me.Label37.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label37.Location = New System.Drawing.Point(313, 24)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(22, 15)
        Me.Label37.TabIndex = 2
        Me.Label37.Text = "件"
        '
        'lblShohinCount
        '
        Me.lblShohinCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblShohinCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblShohinCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblShohinCount.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblShohinCount.Location = New System.Drawing.Point(223, 17)
        Me.lblShohinCount.Name = "lblShohinCount"
        Me.lblShohinCount.Size = New System.Drawing.Size(84, 22)
        Me.lblShohinCount.TabIndex = 4
        Me.lblShohinCount.Text = "0"
        Me.lblShohinCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ListBox1
        '
        Me.ListBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBox1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 15
        Me.ListBox1.Items.AddRange(New Object() {"めかぶ", "かき", "鮑", "うに", "その他"})
        Me.ListBox1.Location = New System.Drawing.Point(3, 87)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(166, 240)
        Me.ListBox1.TabIndex = 2
        '
        'ListBox2
        '
        Me.ListBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBox2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ListBox2.FormattingEnabled = True
        Me.ListBox2.ItemHeight = 15
        Me.ListBox2.Items.AddRange(New Object() {"32345　三陸産丸カップ", "33456　ブレンド丸カップ", "34001　シャキ3P", "34002　きざみ3P", "34003　韓国3P", "34004　三昧", "34005　三陸トップ", "34006　シャキ1P", "34007　ブレンドトップ", "32345　三陸産丸カップ", "33456　ブレンド丸カップ", "34001　シャキ3P", "34002　きざみ3P", "34003　韓国3P", "34004　三昧", "34005　三陸トップ", "34006　シャキ1P", "34007　ブレンドトップ", "32345　三陸産丸カップ", "33456　ブレンド丸カップ", "34001　シャキ3P", "34002　きざみ3P", "34003　韓国3P", "34004　三昧", "34005　三陸トップ", "34006　シャキ1P", "34007　ブレンドトップ", "32345　三陸産丸カップ", "33456　ブレンド丸カップ", "34001　シャキ3P", "34002　きざみ3P", "34003　韓国3P", "34004　三昧", "34005　三陸トップ", "34006　シャキ1P", "34007　ブレンドトップ"})
        Me.ListBox2.Location = New System.Drawing.Point(175, 87)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.Size = New System.Drawing.Size(338, 240)
        Me.ListBox2.TabIndex = 2
        '
        'TableLayoutPanel26
        '
        Me.TableLayoutPanel26.ColumnCount = 1
        Me.TableLayoutPanel26.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel26.Controls.Add(Me.TableLayoutPanel29, 0, 1)
        Me.TableLayoutPanel26.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel26.Location = New System.Drawing.Point(519, 3)
        Me.TableLayoutPanel26.Name = "TableLayoutPanel26"
        Me.TableLayoutPanel26.RowCount = 2
        Me.TableLayoutPanel26.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel26.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel26.Size = New System.Drawing.Size(338, 78)
        Me.TableLayoutPanel26.TabIndex = 2
        '
        'TableLayoutPanel29
        '
        Me.TableLayoutPanel29.ColumnCount = 3
        Me.TableLayoutPanel29.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel29.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.41924!))
        Me.TableLayoutPanel29.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.591065!))
        Me.TableLayoutPanel29.Controls.Add(Me.lblNisugataCount, 1, 0)
        Me.TableLayoutPanel29.Controls.Add(Me.Label36, 0, 0)
        Me.TableLayoutPanel29.Controls.Add(Me.Label38, 2, 0)
        Me.TableLayoutPanel29.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel29.Location = New System.Drawing.Point(0, 39)
        Me.TableLayoutPanel29.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel29.Name = "TableLayoutPanel29"
        Me.TableLayoutPanel29.RowCount = 1
        Me.TableLayoutPanel29.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel29.Size = New System.Drawing.Size(338, 39)
        Me.TableLayoutPanel29.TabIndex = 4
        '
        'lblNisugataCount
        '
        Me.lblNisugataCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNisugataCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblNisugataCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNisugataCount.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNisugataCount.Location = New System.Drawing.Point(221, 17)
        Me.lblNisugataCount.Name = "lblNisugataCount"
        Me.lblNisugataCount.Size = New System.Drawing.Size(84, 22)
        Me.lblNisugataCount.TabIndex = 5
        Me.lblNisugataCount.Text = "0"
        Me.lblNisugataCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label36
        '
        Me.Label36.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label36.AutoSize = True
        Me.Label36.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label36.Location = New System.Drawing.Point(3, 24)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(90, 15)
        Me.Label36.TabIndex = 2
        Me.Label36.Text = "■荷姿・形状"
        '
        'Label38
        '
        Me.Label38.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label38.AutoSize = True
        Me.Label38.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label38.Location = New System.Drawing.Point(313, 24)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(22, 15)
        Me.Label38.TabIndex = 3
        Me.Label38.Text = "件"
        '
        'ListBox3
        '
        Me.ListBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBox3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ListBox3.FormattingEnabled = True
        Me.ListBox3.ItemHeight = 15
        Me.ListBox3.Location = New System.Drawing.Point(519, 87)
        Me.ListBox3.Name = "ListBox3"
        Me.ListBox3.Size = New System.Drawing.Size(338, 240)
        Me.ListBox3.TabIndex = 3
        '
        'TableLayoutPanel24
        '
        Me.TableLayoutPanel24.ColumnCount = 1
        Me.TableLayoutPanel24.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel24.Controls.Add(Me.Label34, 0, 1)
        Me.TableLayoutPanel24.Controls.Add(Me.lblKBN, 0, 0)
        Me.TableLayoutPanel24.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel24.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel24.Name = "TableLayoutPanel24"
        Me.TableLayoutPanel24.RowCount = 2
        Me.TableLayoutPanel24.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel24.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel24.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel24.Size = New System.Drawing.Size(166, 78)
        Me.TableLayoutPanel24.TabIndex = 0
        '
        'Label34
        '
        Me.Label34.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label34.AutoSize = True
        Me.Label34.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label34.Location = New System.Drawing.Point(3, 63)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(82, 15)
        Me.Label34.TabIndex = 0
        Me.Label34.Text = "■商品分類"
        '
        'lblKBN
        '
        Me.lblKBN.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblKBN.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblKBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblKBN.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKBN.Location = New System.Drawing.Point(3, 8)
        Me.lblKBN.Name = "lblKBN"
        Me.lblKBN.Size = New System.Drawing.Size(160, 22)
        Me.lblKBN.TabIndex = 1
        Me.lblKBN.Text = "販売商品"
        Me.lblKBN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TableLayoutPanel27
        '
        Me.TableLayoutPanel27.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.TableLayoutPanel27.ColumnCount = 3
        Me.TableLayoutPanel27.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.0!))
        Me.TableLayoutPanel27.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel27.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.0!))
        Me.TableLayoutPanel27.Controls.Add(Me.btnSelect, 0, 1)
        Me.TableLayoutPanel27.Controls.Add(Me.btnModoru, 2, 1)
        Me.TableLayoutPanel27.Location = New System.Drawing.Point(617, 333)
        Me.TableLayoutPanel27.Name = "TableLayoutPanel27"
        Me.TableLayoutPanel27.RowCount = 2
        Me.TableLayoutPanel27.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel27.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel27.Size = New System.Drawing.Size(240, 59)
        Me.TableLayoutPanel27.TabIndex = 3
        '
        'btnSelect
        '
        Me.btnSelect.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSelect.Location = New System.Drawing.Point(3, 20)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(102, 36)
        Me.btnSelect.TabIndex = 3
        Me.btnSelect.Text = "選択(&G)"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'btnModoru
        '
        Me.btnModoru.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnModoru.Location = New System.Drawing.Point(135, 20)
        Me.btnModoru.Name = "btnModoru"
        Me.btnModoru.Size = New System.Drawing.Size(102, 36)
        Me.btnModoru.TabIndex = 4
        Me.btnModoru.Text = "戻る(&B)"
        Me.btnModoru.UseVisualStyleBackColor = True
        '
        'frmC10F10_Shohin
        '
        Me.ClientSize = New System.Drawing.Size(860, 395)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel23)
        Me.Name = "frmC10F10_Shohin"
        Me.Text = "商品選択(C10F10)"
        Me.TableLayoutPanel23.ResumeLayout(False)
        Me.TableLayoutPanel25.ResumeLayout(False)
        Me.TableLayoutPanel28.ResumeLayout(False)
        Me.TableLayoutPanel28.PerformLayout()
        Me.TableLayoutPanel26.ResumeLayout(False)
        Me.TableLayoutPanel29.ResumeLayout(False)
        Me.TableLayoutPanel29.PerformLayout()
        Me.TableLayoutPanel24.ResumeLayout(False)
        Me.TableLayoutPanel24.PerformLayout()
        Me.TableLayoutPanel27.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Public Event CellEnter As DataGridViewCellEventHandler

    Private Const COMBOBOX_COLUMN As Integer = 1

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        'F1キーが押されたか調べる
        If e.KeyData = Keys.F1 Then
        End If
    End Sub


    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents Label4 As Label
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents lblZaiko As Label
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents Label1 As Label
    Friend WithEvents dteKonkaiJissekiFrom As CustomControl.TextBoxDate
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents Label2 As Label
    Friend WithEvents cboHinsyuKbn As ComboBox

    Friend WithEvents TableLayoutPanel6 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel7 As TableLayoutPanel
    Friend WithEvents lblJoutai As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents TableLayoutPanel8 As TableLayoutPanel
    Friend WithEvents btnCancel As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents TableLayoutPanel9 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel10 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel11 As TableLayoutPanel
    Friend WithEvents Label7 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents TableLayoutPanel12 As TableLayoutPanel
    Friend WithEvents Label11 As Label
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents txtSiyousyo As TextBox
    Friend WithEvents TableLayoutPanel13 As TableLayoutPanel
    Friend WithEvents Label13 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label14 As Label

    Friend WithEvents Label15 As Label
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents TableLayoutPanel14 As TableLayoutPanel
    Friend WithEvents Label17 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents TextBox6 As TextBox
    Friend WithEvents TextBox7 As TextBox
    Friend WithEvents TextBox8 As TextBox
    Friend WithEvents TableLayoutPanel15 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel17 As TableLayoutPanel
    Friend WithEvents Label20 As Label
    Friend WithEvents TableLayoutPanel18 As TableLayoutPanel
    Friend WithEvents TextBox12 As TextBox
    Friend WithEvents TextBox11 As TextBox
    Friend WithEvents TextBox10 As TextBox
    Friend WithEvents TableLayoutPanel19 As TableLayoutPanel
    Friend WithEvents TextBox15 As TextBox
    Friend WithEvents TextBox13 As TextBox
    Friend WithEvents TextBox14 As TextBox

    Private Sub TableLayoutPanel16_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub TextBox9_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub TableLayoutPanel20_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Friend WithEvents TableLayoutPanel20 As TableLayoutPanel
    Friend WithEvents Label22 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents TextBox16 As TextBox

    Private Sub TableLayoutPanel17_Paint(sender As Object, e As PaintEventArgs)

    End Sub
    Friend WithEvents Button2 As Button
    Friend WithEvents TableLayoutPanel16 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel21 As TableLayoutPanel
    Friend WithEvents Label23 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents Label24 As Label
    Friend WithEvents Label25 As Label
    Friend WithEvents Label26 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents TableLayoutPanel22 As TableLayoutPanel
    Friend WithEvents Label27 As Label
    Friend WithEvents Label28 As Label
    Friend WithEvents Label29 As Label
    Friend WithEvents Label30 As Label
    Friend WithEvents Label31 As Label
    Friend WithEvents Label32 As Label


    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub
    Friend WithEvents Column13 As DataGridViewTextBoxColumn
    Friend WithEvents Column14 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridView1 As Windows.Forms.DataGridView
    Friend WithEvents Label33 As Label

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
    Friend WithEvents Column7 As DataGridViewTextBoxColumn
    Friend WithEvents Column8 As DataGridViewTextBoxColumn
    Friend WithEvents Column9 As DataGridViewTextBoxColumn
    Friend WithEvents Column10 As DataGridViewTextBoxColumn
    Friend WithEvents Column11 As DataGridViewTextBoxColumn
    Friend WithEvents Column12 As DataGridViewTextBoxColumn
    '選択した商品コード
    Public ReadOnly Property GettShohinCD() As String
        Get
            Return _selectShohinCD
        End Get
    End Property

    '選択した商品名称
    Public ReadOnly Property GetShohinNM() As String
        Get
            Return _selectShohinNM
        End Get
    End Property
    '選択した荷姿
    Public ReadOnly Property GetNisugata() As String
        Get
            Return _selectNisugata
        End Get
    End Property

    '選択状態   True:選択状態 False:非選択状態
    Public ReadOnly Property Selected() As String
        Get
            Return _selected
        End Get
    End Property

    '荷姿形状一覧一覧キーダウン
    Private Sub ListBox3_KeyDown(sender As Object, e As EventArgs) Handles ListBox3.KeyDown
        Try
            Dim keyEventArgs As KeyEventArgs = TryCast(e, KeyEventArgs)

            If keyEventArgs.KeyData = Keys.Enter Then
                '押下キーがEnterの場合

                '商品データ選択処理
                selectShohin()

                'Enterキー処理無効化
                keyEventArgs.Handled = True

            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '選択ボタンクリック、荷姿形状一覧ダブルクリック
    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click, ListBox3.DoubleClick
        Try
            '商品データ選択処理
            selectShohin()

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

    '商品分類が変更になった時のイベント
    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged

        DispShohin()

    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged
        '荷姿リスト表示
        DispNisugata()

    End Sub

End Class