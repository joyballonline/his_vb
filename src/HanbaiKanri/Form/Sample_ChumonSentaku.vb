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

    Public Class Sample_ChumonSentaku
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

        ''-------------------------------------------------------------------------------
        ''計画対象品一覧出力用定数
        ''-------------------------------------------------------------------------------
        'Private Const RS2 As String = "RecSetM12ForxLS"             'レコードセットテーブル

        ''汎用マスタ固定キー
        'Private Const M01KOTEI_JUYOUSAKI As String = "01"           '需要先

        ''汎用マスタ可変キー
        'Private Const M01KAHEN_KURIKAESI As String = "9"            '他繰返品

        ''M11エイリアス
        'Private Const COLDT_HINMEICD As String = "dtHinmeiCD"       '品名コード
        'Private Const COLDT_HINMEI As String = "dtHinmei"           '品名
        'Private Const COLDT_LOTTYOU As String = "dtLottyou"         '標準ロット長
        'Private Const COLDT_TANTYOU As String = "dtSeisakuTantyou"  '単長
        'Private Const COLDT_JOSU As String = "dtJosu"               '条数
        'Private Const COLDT_KIJUNTUKISU As String = "dtKijunTuki"   '基準月数
        'Private Const COLDT_ABC As String = "dtABC"                 'ABC区分

        ''M12エイリアス
        'Private Const COLDT_M12KHINMEICD As String = "KHINMEICD"    '計画品名コード
        'Private Const COLDT_M12HINMEICD As String = "HINMEICD"      '実品名コード

        ''EXCEL
        'Private Const START_PRINT_ROW As Integer = 7                'EXCEL出力開始行数
        'Private Const START_PRINT_COL As Integer = 1                'EXCEL出力開始列数
        'Private Const XLSSHEETNM_HINSYU As String = "Ver01-00"      '計画対商品一覧雛形シート名
        'Private Const XLS_TITLE As String = "計画対象品一覧表"      'EXCELタイトル

        ''計画対象品一覧表のPGID
        'Private Const ZM130P_PGID As String = "ZM130P"

        '-------------------------------------------------------------------------------
        '   変数定義
        '-------------------------------------------------------------------------------
        Private _msgHd As UtilMsgHandler
        Private _db As UtilDBIf
        'Private updFlg As UpdatableType

        ''-->2010.12/12 add by takagi 
        ''-------------------------------------------------------------------------------
        ''   オーバーライドプロパティで×ボタンだけを無効にする(ControlBoxはTrueのまま使用可能)
        ''-------------------------------------------------------------------------------
        'Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
        '    Get
        '        Const CS_NOCLOSE As Integer = &H200

        '        Dim tmpCreateParams As System.Windows.Forms.CreateParams = MyBase.CreateParams
        '        tmpCreateParams.ClassStyle = tmpCreateParams.ClassStyle Or CS_NOCLOSE

        '        Return tmpCreateParams
        '    End Get
        'End Property
        ''<--2010.12/12 add by takagi 

        '-------------------------------------------------------------------------------
        'デフォルトコンストラクタ（隠蔽）
        '-------------------------------------------------------------------------------
        Private Sub New()
            ' この呼び出しは、Windows フォーム デザイナで必要です。
            InitializeComponent()

            'フォーム上のキーイベントを拾う
            Me.KeyPreview = True

            '
            '売上か委託かの判定
            Dim tokuisakiBri As Integer
            tokuisakiBri = 1    '得意先分類を格納
            If tokuisakiBri = 1 Then
                Label3.Text = "売上"
                Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
            ElseIf tokuisakiBri = 2 Then
                Label3.Text = "委託"
                Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
            End If

            '
            ' 曜日を取得します
            Dim strDenpyoDate As String = dteDenpyoDate.Text
            Label33.Text = YobiReturn(strDenpyoDate)

            Dim strChakuDate As String = dteChakuDate.Text
            Label35.Text = YobiReturn(strChakuDate)

            Dim dataViewRowCnt As Integer = DataGridView1.Rows.Count
            Label37.Text = dataViewRowCnt


            'テスト用データ
            dteDenpyoDate.Text = "2017/12/26"
            dteChakuDate.Text = "2017/12/27"
            ' ユーザ操作による行追加を無効(禁止)
            DataGridView1.AllowUserToAddRows = False

            ' DataGridViewの行追加(1行目) サンプル用
            DataGridView1.Rows.Add()
            'idx = DataGridView1.Rows.Count - 1
            DataGridView1.Rows(0).Cells(0).Value = "1"
            DataGridView1.Rows(0).Cells(1).Value = "123456"
            DataGridView1.Rows(0).Cells(2).Value = "志津川産ぶっかけめかぶ(宮城県産)"
            DataGridView1.Rows(0).Cells(3).Value = "1kg×10"
            DataGridView1.Rows(0).Cells(4).Value = "外"
            DataGridView1.Rows(0).Cells(5).Value = "999.00"
            DataGridView1.Rows(0).Cells(6).Value = "99,999"
            DataGridView1.Rows(0).Cells(7).Value = "99,999.00"
            DataGridView1.Rows(0).Cells(8).Value = "個"
            DataGridView1.Rows(0).Cells(9).Value = "999,999.00"
            DataGridView1.Rows(0).Cells(10).Value = "99,999,999"
            DataGridView1.Rows(0).Cells(11).Value = "伝票番号：12345678"
            DataGridView1.Rows(0).Cells(12).Value = "発泡"
            DataGridView1.Rows.Add()
        End Sub

        'Stringのyyyy/mm/ddを引数に渡すと曜日を返す
        Public Function YobiReturn(ByRef strDenpyoDate As String)

            Dim dteDenpyo As DateTime
            Dim strWeek1 As String ' 短縮表記の曜日を取得します（例：日）

            If DateTime.TryParse(strDenpyoDate, dteDenpyo) Then
                Dim week As DayOfWeek = dteDenpyo.DayOfWeek           ' 現在の曜日をDayOfWeek型で取得します
                Dim weekNumber As Integer = CInt(dteDenpyo.DayOfWeek) ' Int32型にキャストして曜日を数値に変換します
                strWeek1 = dteDenpyo.ToString("ddd")
            Else
                strWeek1 = ""
            End If

            Return strWeek1
        End Function

        '-------------------------------------------------------------------------------
        'コンストラクタ　メニューから呼ばれる
        '-------------------------------------------------------------------------------
        Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf)
            Call Me.New()

            '初期処理
            _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
            _db = prmRefDbHd                                                    'DBハンドラの設定
            StartPosition = FormStartPosition.CenterScreen                      '画面中央表示

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

                '画面タイトル設定
                'Dim optionStr As String = ComBiz.getFormTitleOption(_db, _msgHd)
                'If Not "".Equals(optionStr) Then Me.Text = Me.Text & " - " & optionStr 'タイトルオプション表示

                'バージョン表記
                'lblVersion.Text = UtilClass.getAppVersion(StartUp.assembly)

                '画面初期化
                'Call initForm()

            Catch ue As UsrDefException
                ue.dspMsg()
            Catch ex As Exception
                'Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
            End Try

        End Sub

        Private Sub InitializeComponent()
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"業務"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer)), Nothing)
        Dim ListViewItem2 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"処理名"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer)), Nothing)
        Dim ListViewItem3 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"My前回操作日時"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer)), Nothing)
        Dim ListViewItem4 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"操作者"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer)), Nothing)
        Dim ListViewItem5 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"前回操作日時"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer)), New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte)))
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel34 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.TableLayoutPanel33 = New System.Windows.Forms.TableLayoutPanel()
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.TableLayoutPanel32 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel31 = New System.Windows.Forms.TableLayoutPanel()
        Me.TextBox19 = New System.Windows.Forms.TextBox()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.TextBox9 = New System.Windows.Forms.TextBox()
        Me.TextBox17 = New System.Windows.Forms.TextBox()
        Me.TextBox18 = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel30 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel29 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel27 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel28 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel34.SuspendLayout
        Me.TableLayoutPanel33.SuspendLayout
        Me.TableLayoutPanel32.SuspendLayout
        Me.TableLayoutPanel31.SuspendLayout
        Me.TableLayoutPanel30.SuspendLayout
        Me.TableLayoutPanel29.SuspendLayout
        Me.TableLayoutPanel27.SuspendLayout
        Me.TableLayoutPanel28.SuspendLayout
        Me.SuspendLayout()
        '
        'Button5
        '
        Me.Button5.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Button5.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button5.Location = New System.Drawing.Point(824, 57)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(102, 36)
        Me.Button5.TabIndex = 5
        Me.Button5.Text = "検索(&S)"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Label45
        '
        Me.Label45.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label45.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label45.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label45.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label45.Location = New System.Drawing.Point(891, 17)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(84, 22)
        Me.Label45.TabIndex = 5
        Me.Label45.Text = "0"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel34
        '
        Me.TableLayoutPanel34.ColumnCount = 3
        Me.TableLayoutPanel34.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel34.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.9896!))
        Me.TableLayoutPanel34.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.74636!))
        Me.TableLayoutPanel34.Controls.Add(Me.Label45, 2, 0)
        Me.TableLayoutPanel34.Controls.Add(Me.Label46, 1, 0)
        Me.TableLayoutPanel34.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel34.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel34.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel34.Name = "TableLayoutPanel34"
        Me.TableLayoutPanel34.RowCount = 1
        Me.TableLayoutPanel34.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel34.Size = New System.Drawing.Size(978, 39)
        Me.TableLayoutPanel34.TabIndex = 5
        '
        'Label46
        '
        Me.Label46.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label46.AutoSize = True
        Me.Label46.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label46.Location = New System.Drawing.Point(830, 24)
        Me.Label46.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(29, 12)
        Me.Label46.TabIndex = 3
        Me.Label46.Text = "件数"
        '
        'Button4
        '
        Me.Button4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button4.Location = New System.Drawing.Point(217, 20)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(102, 36)
        Me.Button4.TabIndex = 5
        Me.Button4.Text = "戻る(&B)"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'btnSelect
        '
        Me.btnSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSelect.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSelect.Location = New System.Drawing.Point(82, 20)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(102, 36)
        Me.btnSelect.TabIndex = 4
        Me.btnSelect.Text = "選択(&G)"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel33
        '
        Me.TableLayoutPanel33.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel33.ColumnCount = 2
        Me.TableLayoutPanel33.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.07454!))
        Me.TableLayoutPanel33.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.92546!))
        Me.TableLayoutPanel33.Controls.Add(Me.Button4, 0, 1)
        Me.TableLayoutPanel33.Controls.Add(Me.btnSelect, 0, 1)
        Me.TableLayoutPanel33.Location = New System.Drawing.Point(659, 519)
        Me.TableLayoutPanel33.Name = "TableLayoutPanel33"
        Me.TableLayoutPanel33.RowCount = 2
        Me.TableLayoutPanel33.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.25424!))
        Me.TableLayoutPanel33.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 84.74577!))
        Me.TableLayoutPanel33.Size = New System.Drawing.Size(322, 59)
        Me.TableLayoutPanel33.TabIndex = 5
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "前回操作日時"
        Me.ColumnHeader5.Width = 120
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "操作者"
        Me.ColumnHeader4.Width = 80
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "My前回操作日時"
        Me.ColumnHeader3.Width = 120
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "処理名"
        Me.ColumnHeader2.Width = 200
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "業務"
        '
        'ListView1
        '
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5})
        Me.ListView1.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1, ListViewItem2, ListViewItem3, ListViewItem4, ListViewItem5})
        Me.ListView1.Location = New System.Drawing.Point(0, 39)
        Me.ListView1.Margin = New System.Windows.Forms.Padding(0)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(978, 330)
        Me.ListView1.TabIndex = 4
        Me.ListView1.UseCompatibleStateImageBehavior = False
        '
        'TableLayoutPanel32
        '
        Me.TableLayoutPanel32.ColumnCount = 1
        Me.TableLayoutPanel32.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel32.Controls.Add(Me.TableLayoutPanel34, 0, 0)
        Me.TableLayoutPanel32.Controls.Add(Me.ListView1, 0, 1)
        Me.TableLayoutPanel32.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel32.Location = New System.Drawing.Point(3, 129)
        Me.TableLayoutPanel32.Name = "TableLayoutPanel32"
        Me.TableLayoutPanel32.RowCount = 2
        Me.TableLayoutPanel32.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.81081!))
        Me.TableLayoutPanel32.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.18919!))
        Me.TableLayoutPanel32.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel32.Size = New System.Drawing.Size(978, 369)
        Me.TableLayoutPanel32.TabIndex = 1
        '
        'Label48
        '
        Me.Label48.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label48.AutoSize = True
        Me.Label48.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label48.Location = New System.Drawing.Point(3, 70)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(113, 15)
        Me.Label48.TabIndex = 7
        Me.Label48.Text = "（前方一致検索）"
        '
        'Label47
        '
        Me.Label47.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label47.AutoSize = True
        Me.Label47.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label47.Location = New System.Drawing.Point(3, 3)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(113, 15)
        Me.Label47.TabIndex = 4
        Me.Label47.Text = "（一部一致検索）"
        '
        'Label43
        '
        Me.Label43.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label43.AutoSize = True
        Me.Label43.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label43.Location = New System.Drawing.Point(3, 25)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(113, 15)
        Me.Label43.TabIndex = 5
        Me.Label43.Text = "（一部一致検索）"
        '
        'Label44
        '
        Me.Label44.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label44.AutoSize = True
        Me.Label44.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label44.Location = New System.Drawing.Point(3, 47)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(113, 15)
        Me.Label44.TabIndex = 6
        Me.Label44.Text = "（一部一致検索）"
        '
        'TableLayoutPanel31
        '
        Me.TableLayoutPanel31.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TableLayoutPanel31.ColumnCount = 1
        Me.TableLayoutPanel31.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.11765!))
        Me.TableLayoutPanel31.Controls.Add(Me.Label48, 0, 3)
        Me.TableLayoutPanel31.Controls.Add(Me.Label47, 0, 0)
        Me.TableLayoutPanel31.Controls.Add(Me.Label43, 0, 1)
        Me.TableLayoutPanel31.Controls.Add(Me.Label44, 0, 2)
        Me.TableLayoutPanel31.Location = New System.Drawing.Point(468, 0)
        Me.TableLayoutPanel31.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel31.Name = "TableLayoutPanel31"
        Me.TableLayoutPanel31.RowCount = 4
        Me.TableLayoutPanel31.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel31.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel31.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel31.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel31.Size = New System.Drawing.Size(304, 89)
        Me.TableLayoutPanel31.TabIndex = 1
        '
        'TextBox19
        '
        Me.TextBox19.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox19.Location = New System.Drawing.Point(101, 66)
        Me.TextBox19.Margin = New System.Windows.Forms.Padding(0)
        Me.TextBox19.Name = "TextBox19"
        Me.TextBox19.Size = New System.Drawing.Size(120, 22)
        Me.TextBox19.TabIndex = 2
        '
        'Label39
        '
        Me.Label39.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label39.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label39.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label39.Location = New System.Drawing.Point(1, 0)
        Me.Label39.Margin = New System.Windows.Forms.Padding(0)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(100, 22)
        Me.Label39.TabIndex = 0
        Me.Label39.Text = "出荷先名"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label40
        '
        Me.Label40.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label40.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label40.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label40.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label40.Location = New System.Drawing.Point(1, 22)
        Me.Label40.Margin = New System.Windows.Forms.Padding(0)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(100, 22)
        Me.Label40.TabIndex = 1
        Me.Label40.Text = "住所"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label41
        '
        Me.Label41.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label41.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label41.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label41.Location = New System.Drawing.Point(1, 44)
        Me.Label41.Margin = New System.Windows.Forms.Padding(0)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(100, 22)
        Me.Label41.TabIndex = 2
        Me.Label41.Text = "電話番号"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label42
        '
        Me.Label42.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label42.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label42.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label42.Location = New System.Drawing.Point(1, 66)
        Me.Label42.Margin = New System.Windows.Forms.Padding(0)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(100, 22)
        Me.Label42.TabIndex = 3
        Me.Label42.Text = "出荷先CD"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TextBox9
        '
        Me.TextBox9.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox9.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox9.Location = New System.Drawing.Point(101, 0)
        Me.TextBox9.Margin = New System.Windows.Forms.Padding(0)
        Me.TextBox9.Name = "TextBox9"
        Me.TextBox9.Size = New System.Drawing.Size(350, 22)
        Me.TextBox9.TabIndex = 1
        '
        'TextBox17
        '
        Me.TextBox17.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox17.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox17.Location = New System.Drawing.Point(101, 22)
        Me.TextBox17.Margin = New System.Windows.Forms.Padding(0)
        Me.TextBox17.Name = "TextBox17"
        Me.TextBox17.Size = New System.Drawing.Size(350, 22)
        Me.TextBox17.TabIndex = 4
        '
        'TextBox18
        '
        Me.TextBox18.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBox18.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox18.Location = New System.Drawing.Point(101, 44)
        Me.TextBox18.Margin = New System.Windows.Forms.Padding(0)
        Me.TextBox18.Name = "TextBox18"
        Me.TextBox18.Size = New System.Drawing.Size(120, 22)
        Me.TextBox18.TabIndex = 5
        '
        'TableLayoutPanel30
        '
        Me.TableLayoutPanel30.ColumnCount = 2
        Me.TableLayoutPanel30.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.39468!))
        Me.TableLayoutPanel30.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.60532!))
        Me.TableLayoutPanel30.Controls.Add(Me.TextBox19, 1, 3)
        Me.TableLayoutPanel30.Controls.Add(Me.Label39, 0, 0)
        Me.TableLayoutPanel30.Controls.Add(Me.Label40, 0, 1)
        Me.TableLayoutPanel30.Controls.Add(Me.Label41, 0, 2)
        Me.TableLayoutPanel30.Controls.Add(Me.Label42, 0, 3)
        Me.TableLayoutPanel30.Controls.Add(Me.TextBox9, 1, 0)
        Me.TableLayoutPanel30.Controls.Add(Me.TextBox17, 1, 1)
        Me.TableLayoutPanel30.Controls.Add(Me.TextBox18, 1, 2)
        Me.TableLayoutPanel30.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel30.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel30.Name = "TableLayoutPanel30"
        Me.TableLayoutPanel30.RowCount = 4
        Me.TableLayoutPanel30.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel30.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel30.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel30.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel30.Size = New System.Drawing.Size(451, 89)
        Me.TableLayoutPanel30.TabIndex = 0
        '
        'Label38
        '
        Me.Label38.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(3, 19)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(65, 12)
        Me.Label38.TabIndex = 0
        Me.Label38.Text = "■抽出条件"
        '
        'TableLayoutPanel29
        '
        Me.TableLayoutPanel29.ColumnCount = 2
        Me.TableLayoutPanel29.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.65789!))
        Me.TableLayoutPanel29.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.34211!))
        Me.TableLayoutPanel29.Controls.Add(Me.TableLayoutPanel31, 1, 0)
        Me.TableLayoutPanel29.Controls.Add(Me.TableLayoutPanel30, 0, 0)
        Me.TableLayoutPanel29.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel29.Location = New System.Drawing.Point(0, 31)
        Me.TableLayoutPanel29.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel29.Name = "TableLayoutPanel29"
        Me.TableLayoutPanel29.RowCount = 1
        Me.TableLayoutPanel29.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel29.Size = New System.Drawing.Size(772, 89)
        Me.TableLayoutPanel29.TabIndex = 1
        '
        'TableLayoutPanel27
        '
        Me.TableLayoutPanel27.ColumnCount = 1
        Me.TableLayoutPanel27.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel27.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel27.Controls.Add(Me.TableLayoutPanel33, 0, 2)
        Me.TableLayoutPanel27.Controls.Add(Me.TableLayoutPanel28, 0, 0)
        Me.TableLayoutPanel27.Controls.Add(Me.TableLayoutPanel32, 0, 1)
        Me.TableLayoutPanel27.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel27.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel27.Name = "TableLayoutPanel27"
        Me.TableLayoutPanel27.RowCount = 3
        Me.TableLayoutPanel27.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.82131!))
        Me.TableLayoutPanel27.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 64.60481!))
        Me.TableLayoutPanel27.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.57388!))
        Me.TableLayoutPanel27.Size = New System.Drawing.Size(984, 581)
        Me.TableLayoutPanel27.TabIndex = 1
        '
        'TableLayoutPanel28
        '
        Me.TableLayoutPanel28.ColumnCount = 2
        Me.TableLayoutPanel28.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 79.00208!))
        Me.TableLayoutPanel28.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.99792!))
        Me.TableLayoutPanel28.Controls.Add(Me.Label38, 0, 0)
        Me.TableLayoutPanel28.Controls.Add(Me.TableLayoutPanel29, 0, 1)
        Me.TableLayoutPanel28.Controls.Add(Me.Button5, 1, 1)
        Me.TableLayoutPanel28.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel28.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel28.Name = "TableLayoutPanel28"
        Me.TableLayoutPanel28.RowCount = 2
        Me.TableLayoutPanel28.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.44628!))
        Me.TableLayoutPanel28.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 73.55372!))
        Me.TableLayoutPanel28.Size = New System.Drawing.Size(978, 120)
        Me.TableLayoutPanel28.TabIndex = 0
        '
        'Sample_ChumonSentaku
        '
        Me.ClientSize = New System.Drawing.Size(984, 581)
        Me.Controls.Add(Me.TableLayoutPanel27)
        Me.Name = "Sample_ChumonSentaku"
        Me.Text = "注文選択（H01F50）[カネキ吉田商店][鴫原　牧人]"
        Me.TableLayoutPanel34.ResumeLayout(False)
        Me.TableLayoutPanel34.PerformLayout
        Me.TableLayoutPanel33.ResumeLayout(False)
        Me.TableLayoutPanel32.ResumeLayout(False)
        Me.TableLayoutPanel31.ResumeLayout(False)
        Me.TableLayoutPanel31.PerformLayout
        Me.TableLayoutPanel30.ResumeLayout(False)
        Me.TableLayoutPanel30.PerformLayout
        Me.TableLayoutPanel29.ResumeLayout(False)
        Me.TableLayoutPanel27.ResumeLayout(False)
        Me.TableLayoutPanel28.ResumeLayout(False)
        Me.TableLayoutPanel28.PerformLayout
        Me.ResumeLayout(False)

    End Sub

    Public Event CellEnter As DataGridViewCellEventHandler

        Private Const COMBOBOX_COLUMN As Integer = 1

        Public clickColumnIndex As Integer
        Public clickRowIndex As Integer
        Dim strReVal1 As String = ""
        Dim strReVal2 As String = ""
        Dim strReVal3 As String = ""
        Dim strReVal As String = ""

        'セルをダブルクリックしたとき
        Private Sub DataGridView1_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick

            '検索ウインドウオープン
            SelectWindowOpen()

        End Sub

        '検索ウインドウオープン処理
        Private Sub SelectWindowOpen()
            clickColumnIndex = DataGridView1.CurrentCell.ColumnIndex
            clickRowIndex = DataGridView1.CurrentCell.RowIndex

            Select Case clickColumnIndex
                Case 1      '商品ＣＤ
                    Dim openForm As Sample_Shohin = New Sample_Shohin()      '画面遷移
                    openForm.ShowDialog()                      '画面表示
                    strReVal1 = openForm.selectVal1               'セルに表示する内容を取得
                    strReVal2 = openForm.selectVal2               'セルに表示する内容を取得
                    strReVal3 = openForm.selectVal3               'セルに表示する内容を取得
                Case 4      '税
                    Dim openForm As Sample_Hanyo = New Sample_Hanyo(clickColumnIndex, clickRowIndex)      '画面遷移
                    openForm.ShowDialog()                      '画面表示
                    strReVal = openForm.selectVal               'セルに表示する内容を取得
                Case 8      '単位
                    Dim openForm As Sample_Hanyo = New Sample_Hanyo(clickColumnIndex, clickRowIndex)      '画面遷移
                    openForm.ShowDialog()                      '画面表示
                    strReVal = openForm.selectVal               'セルに表示する内容を取得
                Case 12     '梱包
                    Dim openForm As Sample_Hanyo = New Sample_Hanyo(clickColumnIndex, clickRowIndex)      '画面遷移
                    openForm.ShowDialog()                      '画面表示
                    strReVal = openForm.selectVal               'セルに表示する内容を取得
            End Select


            If clickColumnIndex = 1 And strReVal1 <> "" Then
                DataGridView1.Rows(clickRowIndex).Cells(1).Value = strReVal1
                DataGridView1.Rows(clickRowIndex).Cells(2).Value = strReVal2
                DataGridView1.Rows(clickRowIndex).Cells(3).Value = strReVal3
            ElseIf (clickColumnIndex = 4 Or clickColumnIndex = 8 Or clickColumnIndex = 12) And strReVal <> "" Then
                DataGridView1.Rows(clickRowIndex).Cells(clickColumnIndex).Value = strReVal
            End If

        End Sub
        'キーを押下したら
        Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles DataGridView1.KeyDown

            'tabの場合は別制御
            'If e.KeyCode = Keys.Tab Then
            '    e.Handled = True
            '    Me.SelectNextControl(Me.DataGridView1, True, True, True, True)
            '    Exit Sub
            'End If

            Select Case e.KeyCode
                Case Keys.Space
                    e.Handled = True
                    '検索ウインドウオープン
                    Select Case DataGridView1.CurrentCell.ColumnIndex
                        Case 1, 4, 8, 12
                            SelectWindowOpen()
                    End Select
                    'Case Keys.Enter
                    '    'Enterキーを押下したらTabキーと同じ動作をさせる
                    '    e.Handled = True
                    '    'SendKeys.Send("{TAB}")
                    '    If DataGridView1.ColumnCount > DataGridView1.CurrentCell.ColumnIndex + 1 Then
                    '        DataGridView1.CurrentCell = DataGridView1(DataGridView1.CurrentCell.ColumnIndex + 1, DataGridView1.CurrentCell.RowIndex)
                    '    Else
                    '        If DataGridView1.RowCount > DataGridView1.CurrentCell.RowIndex + 1 Then
                    '            DataGridView1.CurrentCell = DataGridView1(1, DataGridView1.CurrentCell.RowIndex + 1)
                    '        Else
                    '            DataGridView1.CurrentCell = DataGridView1(1, DataGridView1.CurrentCell.RowIndex)
                    '        End If
                    '    End If
            End Select
            DataGridView1.FirstDisplayedScrollingColumnIndex = DataGridView1.CurrentCell.ColumnIndex


        End Sub


        '伝票日付からフォーカスが外れたら、曜日再取得
        Private Sub dteDenpyoDate_Leave(sender As Object, e As System.EventArgs) Handles dteDenpyoDate.Leave
            Dim strDenpyoDate As String = dteDenpyoDate.Text
            Label33.Text = YobiReturn(strDenpyoDate)
        End Sub

        '着日からフォーカスが外れたら、曜日再取得
        Private Sub dteChakuDate_Leave(sender As Object, e As System.EventArgs) Handles dteChakuDate.Leave
            Dim strChakuDate As String = dteChakuDate.Text
            Label35.Text = YobiReturn(strChakuDate)
        End Sub


        Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
        Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
        Friend WithEvents Label4 As Label
        Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
        Friend WithEvents lblZaiko As Label
        Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
        Friend WithEvents Label1 As Label
        Friend WithEvents dteDenpyoDate As CustomControl.TextBoxDate
        Friend WithEvents Label2 As Label
        Friend WithEvents cboHinsyuKbn As ComboBox

        Friend WithEvents TableLayoutPanel6 As TableLayoutPanel
        Friend WithEvents TableLayoutPanel7 As TableLayoutPanel
        Friend WithEvents Label3 As Label
        Friend WithEvents TableLayoutPanel8 As TableLayoutPanel
        Friend WithEvents btnPrint As Button
        Friend WithEvents btnTouroku As Button
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

        Friend WithEvents TableLayoutPanel20 As TableLayoutPanel
        Friend WithEvents Label22 As Label
        Friend WithEvents Label21 As Label
        Friend WithEvents TextBox16 As TextBox

        Friend WithEvents btnModoru As Button
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



        Friend WithEvents Column13 As DataGridViewTextBoxColumn
        Friend WithEvents Column14 As DataGridViewTextBoxColumn
        Friend WithEvents DataGridView1 As Windows.Forms.DataGridView


        Friend WithEvents lblJoutai As Label
        Friend WithEvents TableLayoutPanel23 As TableLayoutPanel
        Friend WithEvents Label33 As Label
        Friend WithEvents TableLayoutPanel24 As TableLayoutPanel
        Friend WithEvents dteChakuDate As CustomControl.TextBoxDate
        Friend WithEvents Label34 As Label
        Friend WithEvents Label35 As Label
        Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
        Friend WithEvents TableLayoutPanel25 As TableLayoutPanel
        Friend WithEvents Label36 As Label
        Friend WithEvents Label37 As Label

        '登録ボタン押下時
        Private Sub btnTouroku_Click(sender As Object, e As EventArgs) Handles btnTouroku.Click
            Me.Close()
        End Sub

        '戻るボタン押下時
        Private Sub btnModoru_Click(sender As Object, e As EventArgs) Handles btnModoru.Click
            Me.Close()
        End Sub

        Friend WithEvents TableLayoutPanel26 As TableLayoutPanel
        Friend WithEvents btnDeleteRow As Button
        Friend WithEvents btnAddRow As Button
        Friend WithEvents btnTopRow As Button

        '再印刷ボタン押下時
        Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
            Me.Close()
        End Sub

        '先頭へボタン押下時
        Private Sub btnTopRow_Click(sender As Object, e As EventArgs) Handles btnTopRow.Click
            DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex).Cells(1)
        End Sub
        '行追加ボタン押下時
        Private Sub btnAddRow_Click(sender As Object, e As EventArgs) Handles btnAddRow.Click
            DataGridView1.Rows.Add()
            DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex + 1).Cells(1)
        End Sub
        '行削除ボタン押下時
        Private Sub btnDeleteRow_Click(sender As Object, e As EventArgs) Handles btnDeleteRow.Click
            If DataGridView1.RowCount > 0 Then
                clickRowIndex = DataGridView1.CurrentCell.RowIndex
                DataGridView1.Rows.RemoveAt(clickRowIndex)
            End If
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
        Friend WithEvents Column15 As DataGridViewTextBoxColumn

        Private Sub TableLayoutPanel8_Paint(sender As Object, e As PaintEventArgs) Handles TableLayoutPanel8.Paint

        End Sub


        Private Sub Sample_Chumon_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
            If e.KeyCode = Keys.Enter Then
                e.Handled = True
                SendKeys.Send("{TAB}")
                Exit Sub
            End If

        End Sub

        Friend WithEvents Button3 As Button
        Friend WithEvents Button2 As Button
        Friend WithEvents Button1 As Button


        '-------------------------------------------------------------------------------
        '   画面初期化
        '   （処理概要）画面項目を初期設定する
        '   ●入力パラメタ  ：なし
        '   ●出力パラメタ  ：なし
        '   ●メソッド戻り値：なし
        '-------------------------------------------------------------------------------
        'Private Sub initForm()

        '    '計画年月/処理年月表示
        '    Call getKeikakuKanriTblRec(lblKeikaku.Text, lblSyori.Text)

        '    '実行日時表示
        '    With updFlg
        '        '１シート目-----
        '        Call getExecuteDt(lblSyokisettei, btnSyokisettei, .updFlgSyokisettei)              '初期設定
        '        Call getExecuteDt(lblSetteitisyuusei, btnSetteitisyuusei, .updFlgSetteitisyuusei)  '希望出来日修正
        '        Call getExecuteDt(lblTDTorikomi, btnTDTorikomi, .updFlgTDTorikomi)                 '手配済ﾃﾞｰﾀ取込
        '        Call getExecuteDt(lblNDTorikomi, btnNDTorikomi, .updFlgNDTorikomi)                 '入庫済ﾃﾞｰﾀ取込
        '        Call getExecuteDt(lblSDSyuusei, btnSDSyuusei, .updFlgSDSyuusei)                    '生産量ﾃﾞｰﾀ修正
        '        Call getExecuteDt(lblSeisanKakutei, btnSeisanKakutei, .updFlgSeisanKakutei)        '生産量確定
        '        Call getExecuteDt(lblHKNyuryoku, btnHKNyuryoku, .updFlgHKNyuryoku)                 '品種別計画入力
        '        Call getExecuteDt(lblKKNyuroku, btnKKNyuroku, .updFlgKKNyuroku)                    '個別計画入力
        '        Call getExecuteDt(lblHJTorikomi, btnHJTorikomi, .updFlgHJTorikomi)                 '販売実績取込
        '        Call getExecuteDt(lblSyuukeiTenkai, btnSyuukeiTenkai, .updFlgSyuukeiTenkai)        '販売計画集計展開
        '        Call getExecuteDt(lblHKSyuusei, btnHKSyuusei, .updFlgHKSyuusei)                    '販売計画量修正
        '        Call getExecuteDt(lblSKakutei, btnSKakutei, .updFlgSKakutei)                       '販売計画確定
        '        Call getExecuteDt(lblZaikoTorikomi, btnZaikoTorikomi, .updFlgZaikoTorikomi)        '在庫実績取込
        '        Call getExecuteDt(lblSHZTorikomi, btnSHZTorikomi, .updFlgSHZTorikomi)              '生産販売在庫取込
        '        Call getExecuteDt(lblSKSyuusei, btnSKSyuusei, .updFlgSKSyuusei)                    '生産計画数量修正
        '        Call getExecuteDt(lblKakutei, btnKakutei, .updFlgKakutei)                          '生産計画確定
        '        Call getExecuteDt(lblTDSakusei, btnTDSakusei, .updFlgTDSakusei)                    '手配ﾃﾞｰﾀ作成
        '        Call getExecuteDt(lblTDSyuusei, btnTDSyuusei, .updFlgTDSyuusei)                    '手配ﾃﾞｰﾀ修正・出力
        '        Call getExecuteDt(lblTDSousin, btnTDSousin, .updFlgTDSousin)                       '手配ﾃﾞｰﾀ作成(生産管理ｼｽﾃﾑ送信用								)
        '        Call getExecuteDt(lblFYamadumi, btnFYamadumi, .updFlgFYamadumi)                    '負荷山積データ取込
        '        Call getExecuteDt(lblKKakunin, btnKKakunin, .updFlgKKakunin)                       '負荷山積集計結果確認
        '        Call getExecuteDt(lblSTDB, btnSTDB, .updFlgSTDB)                                   '製作手配DB登録

        '        '-->2010.12.17 add by takagi #16
        '        '同一タブ内での最新日付を取得する
        '        Dim latestDt As String = ""
        '        For Each ctl As Control In tabGeturei.Controls                               '同一タブ内のコントロールをループ
        '            Dim l As System.Windows.Forms.Label = TryCast(ctl, System.Windows.Forms.Label)
        '            If l IsNot Nothing Then                                                         '取得コントロールがLabelか判断
        '                If IsDate(l.Text) Then                                                      '日付以外のコントロールはスキップ
        '                    If "".Equals(latestDt) Then latestDt = l.Text '                          初回ループ時は無条件格納
        '                    If CDate(latestDt) < CDate(l.Text) Then                                 '保持している最新日付よりも新しいか？
        '                        latestDt = l.Text                                                   'その場合はより新しい日付を保持
        '                    End If
        '                End If
        '            End If
        '        Next
        '        For Each ctl As Control In tabGeturei.Controls                               '同一タブ内のコントロールをループ
        '            Dim l As System.Windows.Forms.Label = TryCast(ctl, System.Windows.Forms.Label)
        '            If l IsNot Nothing Then                                                         '取得コントロールがLabelか判断
        '                'ラベル
        '                If NON_EXECUTE.Equals(l.Text) OrElse IsDate(l.Text) Then
        '                    '日付ラベル
        '                    Select Case True
        '                        Case NON_EXECUTE.Equals(l.Text) : l.ForeColor = Color.Black
        '                        Case CDate(latestDt) <= CDate(l.Text) : l.ForeColor = Color.Red
        '                        Case Else : l.ForeColor = Color.Blue
        '                    End Select
        '                End If
        '            End If
        '        Next
        '        '<--2010.12.17 add by takagi #16

        '        '２シート目-----                                            
        '        '-->2010.12.02 upd by takagi
        '        'Call getExecuteDt(lblTSakujo, btnSinki, .updFlgSinki)                              '新規登録
        '        Call getExecuteDt(lblShinki, btnSinki, .updFlgSinki)                              '新規登録
        '        '<--2010.12.02 upd by takagi
        '        Call getExecuteDt(lblSyuusei, btnSyuusei, .updFlgSyuusei)                          '修正・EXCEL出力
        '        '-->2010.12.02 upd by takagi
        '        'Call getExecuteDt(lblTSakujo, btnSakujo, .updFlgSakujo)                            '削除
        '        Call getExecuteDt(lblM11Del, btnSakujo, .updFlgSakujo)                            '削除
        '        '<--2010.12.02 upd by takagi
        '        Call getExecuteDt(lblKExcel, btnKExcel, .updFlgKExcel)                             '計画対象品一覧表印刷
        '        Call getExecuteDt(lblABC, btnABC, .updFlgABC)                                      'ABC分析
        '        Call getExecuteDt(lblHMstMente, btnHMstMente, .updFlgHMstMente)                    '品種区分マスタメンテ
        '        Call getExecuteDt(lblHanyoMst, btnHanyoMst, .updFlgHanyoMst)                       '汎用マスタメンテ
        '        Call getExecuteDt(lblSNouryokuMst, btnSNouryokuMst, .updFlgSNouryokuMst)           '生産能力マスタメンテ
        '        Call getExecuteDt(lblGRenkei, btnGRenkei, .updFlgGRenkei)                          '外部システム連携

        '    End With

        'End Sub

        '-------------------------------------------------------------------------------
        '   計画/処理年月の取得
        '   （処理概要）計画管理ＴＢＬから計画年月と処理年月を取得する
        '   ●入力パラメタ  ：なし
        '   ●出力パラメタ  ：prmRefSyoriYM     取得済処理年月
        '                     prmRefKeikakuYM   取得済計画年月
        '   ●メソッド戻り値：なし
        '-------------------------------------------------------------------------------
        'Private Sub getKeikakuKanriTblRec(ByRef prmRefSyoriYM As String, ByRef prmRefKeikakuYM As String)
        '    Try
        '        '初期化
        '        prmRefSyoriYM = ""              '処理年月
        '        prmRefKeikakuYM = ""            '計画年月

        '        '計画管理TBL検索
        '        Dim sql As String = ""
        '        Dim iRecCnt As Integer = 0
        '        sql = sql & N & " SELECT "
        '        sql = sql & N & "  SNENGETU "
        '        sql = sql & N & " ,KNENGETU "
        '        sql = sql & N & " FROM T01KEIKANRI "
        '        Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)
        '        If iRecCnt <> 1 Then Throw New UsrDefException("計画管理ＴＢＬのレコード構成が不正です。(" & iRecCnt & "件)")

        '        '返却値編集
        '        prmRefSyoriYM = _db.rmNullStr(ds.Tables(RS).Rows(0)("SNENGETU"))
        '        If Not "".Equals(prmRefSyoriYM) Then prmRefSyoriYM = prmRefSyoriYM.Substring(0, 4) & "/" & prmRefSyoriYM.Substring(4)
        '        prmRefKeikakuYM = _db.rmNullStr(ds.Tables(RS).Rows(0)("KNENGETU"))
        '        If Not "".Equals(prmRefKeikakuYM) Then prmRefKeikakuYM = prmRefKeikakuYM.Substring(0, 4) & "/" & prmRefKeikakuYM.Substring(4)

        '    Catch ue As UsrDefException         'ユーザー定義例外
        '        Call ue.dspMsg()
        '        Throw ue                        'キャッチした例外をそのままスロー
        '    Catch ex As Exception               'システム例外
        '        Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        '    End Try

        'End Sub

        '-------------------------------------------------------------------------------
        '   計画/処理年月の取得
        '   （処理概要）処理制御TBLから機能IDの処理終了日時を取得すると共に、その機能の使用可否と更新可否を判定する
        '   ●入力パラメタ  ：prmExecBtn        押下ボタン(Tagプロパティに該当する機能IDを設定していること)
        '   ●出力パラメタ  ：prmRefUpdatable   更新可否(該当ボタンより起動される機能が更新権限を保有するか否か
        '   ●メソッド戻り値：処理終了日時
        '-------------------------------------------------------------------------------
        'Private Sub getExecuteDt(ByRef prmRefLabel As Label, ByVal prmExecBtn As Button, ByRef prmRefUpdatable As Boolean)
        '    Dim ret As String = ""
        '    Try
        '        'パラメタチェック
        '        If "".Equals(prmExecBtn.Tag) Then Throw New UsrDefException("押下ボタンのTagプロパティが未設定です。" & N & "Tagプロパティに機能IDを正しく設定してください。")

        '        '初期化
        '        prmExecBtn.Enabled = False
        '        prmRefUpdatable = False

        '        '処理終了日時の取得-----
        '        Dim iRecCnt As Integer = 0
        '        Dim ds As DataSet = _db.selectDB("SELECT SDATEEND FROM T02SEIGYO WHERE PGID = '" & _db.rmSQ(prmExecBtn.Tag) & "'", RS, iRecCnt)
        '        If iRecCnt <> 1 Then Throw New UsrDefException("処理制御ＴＢＬに該当機能のレコードが見つかりません。(" & prmExecBtn.Tag & ")")

        '        '-->2010.12.17 chg by takagi #16
        '        'ret = _db.rmNullDate(ds.Tables(RS).Rows(0)("SDATEEND"), "yyyy/MM/dd HH:mm")
        '        ret = _db.rmNullDate(ds.Tables(RS).Rows(0)("SDATEEND"), "yyyy/MM/dd HH:mm:ss")
        '        '<--2010.12.17 chg by takagi #16
        '        If "".Equals(ret) Then ret = NON_EXECUTE


        '        '先行ジョブの判定-------
        '        ds = _db.selectDB("SELECT BEFOREJOB_ID FROM M81BEFOREJOB WHERE PGID = '" & _db.rmSQ(prmExecBtn.Tag) & "'", RS, iRecCnt)
        '        If iRecCnt <= 0 Then
        '            '先行ジョブ定義なし→起動可能
        '            prmExecBtn.Enabled = True
        '        Else
        '            '先行ジョブ定義あり
        '            Dim wkCnt As Integer = 0
        '            Dim wkDs As DataSet = Nothing
        '            Dim wkPgId As String = ""
        '            Dim execCnt As Integer = 0
        '            For i As Integer = 0 To iRecCnt - 1
        '                '先行ジョブごとに実行済か判定
        '                wkPgId = _db.rmNullStr(ds.Tables(RS).Rows(i)("BEFOREJOB_ID"))
        '                wkDs = _db.selectDB("SELECT SDATEEND FROM T02SEIGYO WHERE PGID = '" & _db.rmSQ(wkPgId) & "'", RS, wkCnt)
        '                If Not "".Equals(_db.rmNullDate(wkDs.Tables(RS).Rows(0)("SDATEEND"))) Then
        '                    execCnt += 1                            '処理済
        '                End If
        '            Next
        '            If execCnt = iRecCnt Then
        '                prmExecBtn.Enabled = True                   '全て処理済→起動可能
        '            End If
        '        End If

        '        '後続ジョブの判定-------
        '        If prmExecBtn.Enabled Then                          '起動可能の場合のみ更新可否を判断する
        '            ds = _db.selectDB("SELECT AFTERJOB_ID FROM M82AFTERJOB WHERE PGID = '" & _db.rmSQ(prmExecBtn.Tag) & "'", RS, iRecCnt)
        '            If iRecCnt <= 0 Then
        '                '後続ジョブ定義なし→更新可能
        '                prmRefUpdatable = True
        '            Else
        '                '後続ジョブ定義あり
        '                prmRefUpdatable = True
        '                Dim wkCnt As Integer = 0
        '                Dim wkDs As DataSet = Nothing
        '                Dim wkPgId As String = ""
        '                Dim execCnt As Integer = 0
        '                For i As Integer = 0 To iRecCnt - 1
        '                    '後続ジョブごとに実行済か判定
        '                    wkPgId = _db.rmNullStr(ds.Tables(RS).Rows(i)("AFTERJOB_ID"))
        '                    wkDs = _db.selectDB("SELECT SDATEEND FROM T02SEIGYO WHERE PGID = '" & _db.rmSQ(wkPgId) & "'", RS, wkCnt)
        '                    If Not "".Equals(_db.rmNullDate(wkDs.Tables(RS).Rows(0)("SDATEEND"))) Then
        '                        prmRefUpdatable = False             '処理済
        '                        Exit For                            '一つでも処理済があればその時点で更新不可
        '                    End If
        '                Next
        '            End If
        '        End If

        '        'デバッグ用に更新権限をボタンテキストに表示
        '        If StartUp.DebugMode Then
        '            lblDebugDsp.Visible = True
        '            prmExecBtn.Text = System.Text.RegularExpressions.Regex.Replace(prmExecBtn.Text, "\[.*\]", "") & "[" & prmRefUpdatable & "]"
        '        End If

        '    Catch ue As UsrDefException         'ユーザー定義例外
        '        Call ue.dspMsg()
        '        Throw ue                        'キャッチした例外をそのままスロー
        '    Catch ex As Exception               'システム例外
        '        Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        '    End Try
        '    prmRefLabel.Text = ret
        '    '-->2010.12.17 add by takagi #16
        '    'If Not prmExecBtn.Enabled Then prmRefLabel.Text = NON_EXECUTE
        '    '<--2010.12.17 add by takagi #16
        '    If Not NON_EXECUTE.Equals(prmRefLabel.Text) Then prmRefLabel.ForeColor = Color.Blue

        'End Sub

        '-------------------------------------------------------------------------------
        '   処理制御TBL更新
        '   （処理概要）処理制御TBL(T02SEIGYO)の処理実行日時を設定する
        '   ●入力パラメタ  ：prmPgId       処理制御TBLのレコード特定に使用する機能ID
        '                     prmRunFlg     実行かキャンセルかを示すフラグ(キャンセル：確定解除等に使用)
        '                     [prmStartDt]  処理開始日時(キャンセル時は未使用)
        '                     [prmEndDt]    処理終了日時(キャンセル時は未使用)
        '   ●出力パラメタ  ：なし
        '   ●メソッド戻り値：なし
        '-------------------------------------------------------------------------------
        'Public Sub updateSeigyoTbl(ByVal prmPgId As String, ByVal prmRunFlg As Boolean, Optional ByVal prmStartDt As Date = Nothing, Optional ByVal prmEndDt As Date = Nothing)
        '    Try
        '        'パラメタチェック
        '        If prmRunFlg AndAlso (prmStartDt = #12:00:00 AM# OrElse prmEndDt = #12:00:00 AM#) Then
        '            Throw New UsrDefException("実行処理(prmRunFlg=True)の場合は処理開始日時(prmStartDt)・処理終了日時(prmEndDt)が必須です。")
        '        End If

        '        '制御テーブル更新
        '        Dim sql As String = ""
        '        Dim affectedRows As Integer = 0
        '        sql = sql & N & "UPDATE T02SEIGYO SET "
        '        If prmRunFlg Then
        '            sql = sql & N & "SDATESTART = TO_DATE('" & Format(prmStartDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS'), "
        '            sql = sql & N & "SDATEEND   = TO_DATE('" & Format(prmEndDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS'), "
        '        Else
        '            sql = sql & N & "SDATESTART = NULL, "
        '            sql = sql & N & "SDATEEND   = NULL, "
        '        End If
        '        sql = sql & N & "UPDNAME = '" & _db.rmSQ(UtilClass.getComputerName()) & "', "
        '        sql = sql & N & "UPDDATE = TO_DATE('" & Format(Now, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "
        '        sql = sql & N & "WHERE PGID = '" & _db.rmSQ(prmPgId) & "' "
        '        _db.executeDB(sql, affectedRows)
        '        If affectedRows <= 0 Then
        '            Throw New UsrDefException("制御TBLのレコード構成が不正です。(" & prmPgId & "非存在)")
        '        End If

        '        'メニュー画面再描画
        '        Call initForm()

        '    Catch ue As UsrDefException         'ユーザー定義例外
        '        Call ue.dspMsg()
        '        Throw ue                        'キャッチした例外をそのままスロー
        '    Catch ex As Exception               'システム例外
        '        Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        '    End Try
        'End Sub

        ''------------------------------------------------------------------------------------------------------
        ''　終了ボタン押下
        ''------------------------------------------------------------------------------------------------------
        'Private Sub btnEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnd.Click

        '    '画面クローズ
        '    Me.Close()

        'End Sub

        '    '------------------------------------------------------------------------------------------------------
        '    '　初期設定ボタン押下
        '    '------------------------------------------------------------------------------------------------------
        '    Private Sub btnSyokisettei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSyokisettei.Click

        '        Dim openForm As ZG110B_Junbi = New ZG110B_Junbi(_msgHd, _db, Me, ZG110B_Junbi.BOOTMODE_INIT, updFlg.updFlgSyokisettei)      '画面遷移
        '        openForm.Show()                                                             '画面表示
        '        Me.Hide()

        '    End Sub

        '    '------------------------------------------------------------------------------------------------------
        '    '　希望出来日ボタン押下
        '    '------------------------------------------------------------------------------------------------------
        '    Private Sub btnSetteitisyuusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetteitisyuusei.Click

        '        Dim openForm As ZG110B_Junbi = New ZG110B_Junbi(_msgHd, _db, Me, ZG110B_Junbi.BOOTMODE_UPD, updFlg.updFlgSetteitisyuusei)      '画面遷移
        '        openForm.Show()                                                             '画面表示
        '        Me.Hide()

        '    End Sub

        '    '------------------------------------------------------------------------------------------------------
        '    '　手配済データ登録ボタン押下
        '    '------------------------------------------------------------------------------------------------------
        '    Private Sub btnTDTorikomi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTDTorikomi.Click

        '        Dim openForm As ZG210E_SeisanHanei = New ZG210E_SeisanHanei(_msgHd, _db, Me, ZG210E_SeisanHanei.TEHAI, updFlg.updFlgTDTorikomi)      '画面遷移
        '        openForm.Show()                                                             '画面表示
        '        Me.Hide()

        '    End Sub

        '    '------------------------------------------------------------------------------------------------------
        '    '　入庫済データ登録ボタン押下
        '    '------------------------------------------------------------------------------------------------------
        '    Private Sub btnNDTorikomi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNDTorikomi.Click

        '        Dim openForm As ZG210E_SeisanHanei = New ZG210E_SeisanHanei(_msgHd, _db, Me, ZG210E_SeisanHanei.NYUKO, updFlg.updFlgNDTorikomi)      '画面遷移
        '        openForm.Show()                                                             '画面表示
        '        Me.Hide()

        '    End Sub

        '    '------------------------------------------------------------------------------------------------------
        '    '　生産量データ修正ボタン押下
        '    '------------------------------------------------------------------------------------------------------
        '    Private Sub btnSDSyuusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSDSyuusei.Click

        '        Dim openForm As ZG220E_SeisanSyusei = New ZG220E_SeisanSyusei(_msgHd, _db, Me, updFlg.updFlgSDSyuusei)      '画面遷移
        '        openForm.Show()                                                             '画面表示
        '        Me.Hide()

        '    End Sub

        '    '------------------------------------------------------------------------------------------------------
        '    '　生産量確定ボタン押下
        '    '------------------------------------------------------------------------------------------------------
        '    Private Sub btnSeisanKakutei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeisanKakutei.Click

        '        Dim openForm As ZG230B_SeisanryouKakutei = New ZG230B_SeisanryouKakutei(_msgHd, _db, Me, updFlg.updFlgSeisanKakutei)      '画面遷移
        '        openForm.Show()                                                             '画面表示
        '        Me.Hide()

        '    End Sub

        '    '------------------------------------------------------------------------------------------------------
        '    '　品種別計画入力ボタン押下
        '    '------------------------------------------------------------------------------------------------------
        '    Private Sub btnHKNyuryoku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHKNyuryoku.Click

        '        Dim openForm As ZG310E_Hinsyubetu = New ZG310E_Hinsyubetu(_msgHd, _db, Me, updFlg.updFlgHKNyuryoku)      '画面遷移
        '        openForm.Show()                                                             '画面表示
        '        Me.Hide()

        '    End Sub

        '    '------------------------------------------------------------------------------------------------------
        '    '　個別計画入力ボタン押下
        '    '------------------------------------------------------------------------------------------------------
        '    Private Sub btnKKNyuroku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKKNyuroku.Click

        '        Dim openForm As ZG320E_KobetuNyuuroku = New ZG320E_KobetuNyuuroku(_msgHd, _db, Me, updFlg.updFlgKKNyuroku)      '画面遷移
        '        openForm.Show()                                                             '画面表示
        '        Me.Hide()

        '    End Sub

        '    '------------------------------------------------------------------------------------------------------
        '    '　販売実績取込ボタン押下
        '    '------------------------------------------------------------------------------------------------------
        '    Private Sub btnHJTorikomi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHJTorikomi.Click

        '        Dim openForm As ZG330B_HJissekiTorikomi = New ZG330B_HJissekiTorikomi(_msgHd, _db, Me, updFlg.updFlgHJTorikomi)      '画面遷移
        '        openForm.Show()                                                             '画面表示
        '        Me.Hide()

        '    End Sub

        '    '------------------------------------------------------------------------------------------------------
        '    '　販売計画集計展開ボタン押下
        '    '------------------------------------------------------------------------------------------------------
        '    Private Sub btnSyuukeiTenkai_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSyuukeiTenkai.Click

        '        Dim openForm As ZG340B_HJissekiTenkai = New ZG340B_HJissekiTenkai(_msgHd, _db, Me, updFlg.updFlgSyuukeiTenkai)      '画面遷移
        '        openForm.Show()                                                             '画面表示
        '        Me.Hide()

        '    End Sub

        '    '------------------------------------------------------------------------------------------------------
        '    '　販売計画量修正ボタン押下
        '    '------------------------------------------------------------------------------------------------------
        '    Private Sub btnHKSyuusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHKSyuusei.Click

        '        Dim openForm As ZG350E_KeikakuryouHosei = New ZG350E_KeikakuryouHosei(_msgHd, _db, Me, updFlg.updFlgHKSyuusei)      '画面遷移
        '        openForm.Show()                                                             '画面表示
        '        Me.Hide()

        '    End Sub

        '    '------------------------------------------------------------------------------------------------------
        '    '　販売計画量確定ボタン押下
        '    '------------------------------------------------------------------------------------------------------
        '    Private Sub btnSKakutei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSKakutei.Click

        '        Dim openForm As ZG360B_HKeikakuKakutei = New ZG360B_HKeikakuKakutei(_msgHd, _db, Me, updFlg.updFlgSKakutei)      '画面遷移
        '        openForm.Show()                                                             '画面表示
        '        Me.Hide()

        '    End Sub

        '    '------------------------------------------------------------------------------------------------------
        '    '　在庫実績取込ボタン押下
        '    '------------------------------------------------------------------------------------------------------
        '    Private Sub btnZaikoTorikomi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZaikoTorikomi.Click

        '        Dim openForm As ZG410B_ZJissekiTorikomi = New ZG410B_ZJissekiTorikomi(_msgHd, _db, Me, updFlg.updFlgZaikoTorikomi)      '画面遷移
        '        openForm.Show()                                                             '画面表示
        '        Me.Hide()

        '    End Sub

        '    '------------------------------------------------------------------------------------------------------
        '    '　生産販売在庫取込ボタン押下
        '    '------------------------------------------------------------------------------------------------------
        '    Private Sub btnSHZTorikomi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSHZTorikomi.Click

        '        Dim openForm As ZG510B_SHZTorikomiIkkatu = New ZG510B_SHZTorikomiIkkatu(_msgHd, _db, Me, updFlg.updFlgSHZTorikomi)      '画面遷移
        '        openForm.Show()                                                             '画面表示
        '        Me.Hide()

        '    End Sub

        '    '------------------------------------------------------------------------------------------------------
        '    '　生産計画数量修正ボタン押下
        '    '------------------------------------------------------------------------------------------------------
        '    Private Sub btnSKSyuusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSKSyuusei.Click

        '        Dim openForm As ZG530E_SeisanSuuryouSyuusei = New ZG530E_SeisanSuuryouSyuusei(_msgHd, _db, Me, updFlg.updFlgSKSyuusei)      '画面遷移
        '        openForm.Show()                                                             '画面表示
        '        Me.Hide()

        '    End Sub

        '    '------------------------------------------------------------------------------------------------------
        '    '　生産計画確定ボタン押下
        '    '------------------------------------------------------------------------------------------------------
        '    Private Sub btnKakutei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKakutei.Click

        '        Dim openForm As ZG540B_SKeikakuKakutei = New ZG540B_SKeikakuKakutei(_msgHd, _db, Me, updFlg.updFlgKakutei)      '画面遷移
        '        openForm.Show()                                                             '画面表示
        '        Me.Hide()

        '    End Sub

        '    '------------------------------------------------------------------------------------------------------
        '    '　手配データ作成ボタン押下
        '    '------------------------------------------------------------------------------------------------------
        '    Private Sub btnTDSakusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTDSakusei.Click

        '        Dim openForm As ZG610B_TehaiDateSakusei = New ZG610B_TehaiDateSakusei(_msgHd, _db, Me, updFlg.updFlgTDSakusei)      '画面遷移
        '        openForm.Show()                                                             '画面表示
        '        Me.Hide()

        '    End Sub

        '    '------------------------------------------------------------------------------------------------------
        '    '　手配データ修正・出力ボタン押下
        '    '------------------------------------------------------------------------------------------------------
        '    Private Sub btnTDSyuusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTDSyuusei.Click

        '        Dim openForm As ZG620E_TehaiSyuuseiItiran = New ZG620E_TehaiSyuuseiItiran(_msgHd, _db, Me, updFlg.updFlgTDSyuusei)      '画面遷移
        '        openForm.Show()                                                             '画面表示
        '        Me.Hide()

        '    End Sub

        '    '------------------------------------------------------------------------------------------------------
        '    '　手配データ作成(生産管理システム送信用)ボタン押下
        '    '------------------------------------------------------------------------------------------------------
        '    Private Sub btnTDSousin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTDSousin.Click

        '        Dim openForm As ZG630B_TehaiSakuseiSeisan = New ZG630B_TehaiSakuseiSeisan(_msgHd, _db, Me, updFlg.updFlgTDSousin)      '画面遷移
        '        openForm.Show()                                                             '画面表示
        '        Me.Hide()

        '    End Sub

        '    '------------------------------------------------------------------------------------------------------
        '    '　負荷山積データ取込ボタン押下
        '    '------------------------------------------------------------------------------------------------------
        '    Private Sub btnFYamadumi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFYamadumi.Click

        '        Dim openForm As ZG720B_FukaYamadumiTorikomi = New ZG720B_FukaYamadumiTorikomi(_msgHd, _db, Me, updFlg.updFlgFYamadumi)      '画面遷移
        '        openForm.Show()                                                             '画面表示
        '        Me.Hide()

        '    End Sub

        '    '------------------------------------------------------------------------------------------------------
        '    '　負荷山積集計結果確認ボタン押下
        '    '------------------------------------------------------------------------------------------------------
        '    Private Sub btnKKakunin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKKakunin.Click

        '        Dim openForm As ZG730Q_FukaYamadumiKoutei = New ZG730Q_FukaYamadumiKoutei(_msgHd, _db, Me, updFlg.updFlgKKakunin)      '画面遷移
        '        openForm.Show()                                                             '画面表示
        '        Me.Hide()

        '    End Sub

        '    '------------------------------------------------------------------------------------------------------
        '    '　製作手配DB登録ボタン押下
        '    '------------------------------------------------------------------------------------------------------
        '    Private Sub btnSTDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSTDB.Click

        '        Dim openForm As ZG640B_SeisakuTehaiDB = New ZG640B_SeisakuTehaiDB(_msgHd, _db, Me, updFlg.updFlgSTDB)      '画面遷移
        '        openForm.Show()                                                             '画面表示
        '        Me.Hide()

        '    End Sub

        '    '------------------------------------------------------------------------------------------------------
        '    '　新規登録ボタン押下
        '    '------------------------------------------------------------------------------------------------------
        '    Private Sub btnTSakujo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSinki.Click

        '        '-->2010.12.02 add by takagi
        '        'Dim openForm As ZM110E_Sinki = New ZM110E_Sinki(_msgHd, _db, Me, updFlg.updFlgSinki, True)      'パラメタを遷移先画面へ渡す
        '        Dim openForm As ZM110E_Sinki = New ZM110E_Sinki(_msgHd, _db, Me, updFlg.updFlgSinki, True, btnSinki.Tag)      'パラメタを遷移先画面へ渡す
        '        '<--2010.12.02 add by takagi
        '        openForm.Show()                                                             '画面表示
        '        Me.Hide()

        '    End Sub

        '    '------------------------------------------------------------------------------------------------------
        '    '　修正・EXCEL出力押下
        '    '------------------------------------------------------------------------------------------------------
        '    Private Sub btnSyuusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSyuusei.Click

        '        Dim openForm As ZM120E_Syuusei = New ZM120E_Syuusei(_msgHd, _db, Me, updFlg.updFlgSyuusei)      'パラメタを遷移先画面へ渡す
        '        openForm.Show()                                                             '画面表示
        '        Me.Hide()

        '    End Sub

        '    '------------------------------------------------------------------------------------------------------
        '    '　削除ボタン押下
        '    '------------------------------------------------------------------------------------------------------
        '    Private Sub btnSakujo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSakujo.Click

        '        '-->2010.12.02 add by takagi
        '        'Dim openForm As ZM110E_Sinki = New ZM110E_Sinki(_msgHd, _db, Me, updFlg.updFlgSinki, False)      'パラメタを遷移先画面へ渡す
        '        Dim openForm As ZM110E_Sinki = New ZM110E_Sinki(_msgHd, _db, Me, updFlg.updFlgSinki, False, btnSakujo.Tag)      'パラメタを遷移先画面へ渡す
        '        '<--2010.12.02 add by takagi
        '        openForm.Show()                                                             '画面表示
        '        Me.Hide()

        '    End Sub

        '#Region "計画対象品一覧表印刷ボタン押下　計画対商品一覧印刷"
        '    '------------------------------------------------------------------------------------------------------
        '    '　計画対象品一覧表印刷ボタン押下
        '    '------------------------------------------------------------------------------------------------------
        '    Private Sub btnKExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKExcel.Click
        '        Try
        '            'マウスカーソル砂時計
        '            Me.Cursor = Cursors.WaitCursor

        '            '印刷
        '            Dim startPrintTime As Date = Now

        '            'EXCEL出力
        '            Call printExcel()

        '            Dim endPrintTime As Date = Now

        '            '制御テーブル更新
        '            Call updateSeigyoTbl(ZM130P_PGID, True, startPrintTime, endPrintTime)

        '        Catch ue As UsrDefException
        '            ue.dspMsg()
        '        Catch ex As Exception
        '            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        '        Finally
        '            'マウスカーソル元に戻す
        '            Me.Cursor = Cursors.Default
        '        End Try
        '    End Sub

        '    '------------------------------------------------------------------------------------------------------
        '    '　計画対象品一覧出力
        '    '------------------------------------------------------------------------------------------------------
        '    Private Sub printExcel()
        '        Try
        '            Dim pb As UtilProgressBar = New UtilProgressBar(Me)
        '            Try
        '                pb.Show()

        '                'プログレスバー設定
        '                pb.jobName = "出力を準備しています。"
        '                pb.status = "初期化中．．．"

        '                '雛形ファイル(品名別販売計画と同じ雛形)
        '                Dim openFilePath As String = StartUp.iniValue.BaseXlsPath & "\" & StartUp.iniValue.ExcelZM130R1_Base
        '                '雛形ファイルが開かれていないかチェック
        '                Dim fh As UtilFile = New UtilFile()
        '                Try
        '                    fh.move(openFilePath, openFilePath & 1)
        '                    fh.move(openFilePath & 1, openFilePath)
        '                Catch ioe As System.IO.IOException
        '                    Throw New UsrDefException("ファイルが開かれています。ファイルを閉じてから再実行してください。",
        '                                              _msgHd.getMSG("fileOpenErr", "【ファイル名】：" & openFilePath))
        '                End Try

        '                '出力用ファイル
        '                'ファイル名取得-----------------------------------------------------
        '                Dim wkEditFile As String = StartUp.iniValue.OutXlsPath & "\" & StartUp.iniValue.ExcelZM130R1_Out     'コピー先ファイル

        '                'コピー先ファイルが存在する場合、コピー先ファイルを削除----------------
        '                If UtilClass.isFileExists(wkEditFile) Then
        '                    Try
        '                        fh.delete(wkEditFile)
        '                    Catch ioe As System.IO.IOException
        '                        Throw New UsrDefException("ファイルが開かれています。ファイルを閉じてから再実行してください。",
        '                                                  _msgHd.getMSG("fileOpenErr", "【ファイル名】：" & wkEditFile))
        '                    End Try
        '                End If

        '                Try
        '                    '出力用ファイルへ雛型ファイルコピー
        '                    FileCopy(openFilePath, wkEditFile)
        '                Catch ioe As System.IO.IOException
        '                    Throw New UsrDefException(ioe, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ioe)))
        '                End Try

        '                Dim eh As xls.UtilExcelHandler = New xls.UtilExcelHandler(wkEditFile)
        '                Try
        '                    eh.open()
        '                    Try
        '                        '汎用マスタから需要先情報を取得
        '                        Dim sql As String = ""
        '                        sql = sql & N & " SELECT KAHENKEY, NAME1 FROM M01HANYO "
        '                        sql = sql & N & " WHERE KOTEIKEY = '" & M01KOTEI_JUYOUSAKI & "'"
        '                        sql = sql & N & " ORDER BY KAHENKEY "
        '                        'SQL発行
        '                        Dim iRecCnt As Integer          'データセットの行数
        '                        Dim dsHanyo As DataSet = _db.selectDB(sql, RS, iRecCnt)

        '                        If iRecCnt <= 0 Then                    'M01汎用マスタ抽出レコードが１件もない場合
        '                            Throw New UsrDefException("登録されていません。", _msgHd.getMSG("noData"))
        '                        End If

        '                        For i As Integer = 0 To iRecCnt - 1

        '                            'M11の値をデータセットに保持
        '                            Dim dsM11 As DataSet = Nothing
        '                            Dim rowCntM11 As Integer = 0
        '                            '需要先ごとにM11のデータを抽出
        '                            Call getM11DataForXls(_db.rmNullStr(dsHanyo.Tables(RS).Rows(i)("KAHENKEY")), dsM11, rowCntM11)

        '                            'M12の値をデータセットに保持
        '                            Dim dsM12 As DataSet = Nothing
        '                            Dim rowCntM12 As Integer = 0
        '                            '需要先ごとにM12のデータを抽出
        '                            Call getM12DataForXls(_db.rmNullStr(dsHanyo.Tables(RS).Rows(i)("KAHENKEY")), dsM12, rowCntM12)

        '                            If rowCntM11 > 0 Then

        '                                'シート(雛形)を複製保存
        '                                Dim baseName As String = XLSSHEETNM_HINSYU  '雛形シート名
        '                                Dim newName As String = _db.rmNullStr(dsHanyo.Tables(RS).Rows(i)("NAME1"))    '新たに作成するシート
        '                                Try
        '                                    eh.targetSheet = baseName               '雛形シート選択
        '                                    eh.copySheetOnLast(newName)             '雛形シートコピー
        '                                Catch ex As Exception
        '                                    Throw New UsrDefException("シートの複製に失敗しました。", _msgHd.getMSG("failCopySheet"))
        '                                End Try

        '                                'プログレスバー設定
        '                                pb.jobName = newName & "出力中．．．"
        '                                pb.status = ""

        '                                'コピーしたシートに出力
        '                                eh.targetSheet = newName

        '                                '作成日時編集
        '                                Dim printDate As String = Now.ToString("yyyy/MM/dd HH:mm")
        '                                eh.setValue("作成日時 ： " & printDate, 1, 8)   'H1

        '                                'タイトル・需要品名編集
        '                                eh.setValue(XLS_TITLE & "      (" & _db.rmNullStr(dsHanyo.Tables(RS).Rows(i)("NAME1")) & ")", 3, 1)      'A3

        '                                Dim startPrintRow As Integer = START_PRINT_ROW          '出力開始行数

        '                                Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder
        '                                pb.maxVal = rowCntM11

        '                                Dim k As Integer = 0        'M11ループカウンター
        '                                Dim m As Integer = 0        'M12のレコード数カウンター
        '                                Dim xlsRow As Integer = 0
        '                                For k = 0 To rowCntM11 - 1

        '                                    pb.status = (k + 1) & "/" & rowCntM11 & "件"
        '                                    pb.oneStep = 10
        '                                    pb.value = k + 1

        '                                    xlsRow = startPrintRow + k

        '                                    '行を1行追加
        '                                    eh.copyRow(xlsRow)
        '                                    eh.insertPasteRow(xlsRow)

        '                                    '一覧データ出力
        '                                    With dsM11.Tables(RS)
        '                                        Dim sHinmeiCD As String = ""        '出力する計画品名コードを保持する変数
        '                                        For n As Integer = m To rowCntM12 - 1
        '                                            '実品名コードが等しい場合
        '                                            If _db.rmNullStr(.Rows(k)(COLDT_HINMEICD)).Equals _
        '                                                            (_db.rmNullStr(dsM12.Tables(RS2).Rows(n)(COLDT_M12KHINMEICD))) Then
        '                                                If "".Equals(sHinmeiCD) Then
        '                                                    sHinmeiCD = _db.rmNullStr(dsM12.Tables(RS2).Rows(n)(COLDT_M12HINMEICD))
        '                                                Else
        '                                                    '該当する実品名コードをカンマ区切りでつなげる
        '                                                    sHinmeiCD = sHinmeiCD & "," & _db.rmNullStr(dsM12.Tables(RS2).Rows(n)(COLDT_M12HINMEICD))
        '                                                End If
        '                                                m = n + 1
        '                                            Else
        '                                                Exit For
        '                                            End If
        '                                        Next

        '                                        sb.Append(_db.rmNullStr(.Rows(k)(COLDT_HINMEICD)) & ControlChars.Tab)       '品名コード
        '                                        sb.Append(_db.rmNullStr(.Rows(k)(COLDT_HINMEI)) & ControlChars.Tab)         '品名
        '                                        sb.Append(_db.rmNullStr(.Rows(k)(COLDT_LOTTYOU)) & ControlChars.Tab)        'ロット長
        '                                        sb.Append(_db.rmNullStr(.Rows(k)(COLDT_TANTYOU)) & ControlChars.Tab)        '単長
        '                                        sb.Append(_db.rmNullStr(.Rows(k)(COLDT_JOSU)) & ControlChars.Tab)           '条数
        '                                        sb.Append(_db.rmNullStr(.Rows(k)(COLDT_KIJUNTUKISU)) & ControlChars.Tab)    '基準月数
        '                                        sb.Append(_db.rmNullStr(.Rows(k)(COLDT_ABC)) & ControlChars.Tab)            'ABC区分
        '                                        sb.Append(sHinmeiCD)                                                        '集計品名数

        '                                        sb.Append(ControlChars.CrLf)
        '                                    End With
        '                                Next

        '                                Clipboard.SetText(sb.ToString)
        '                                eh.paste(START_PRINT_ROW, START_PRINT_COL) '一括貼り付け

        '                                '罫線を再設定
        '                                Dim lineV As LineVO = New LineVO()
        '                                lineV.Bottom = LineVO.LineType.NomalL
        '                                eh.drawRuledLine(lineV, xlsRow, START_PRINT_COL, , 8)

        '                                eh.deleteRow(xlsRow + 1)    '余分な行を削除
        '                            End If
        '                        Next

        '                        eh.deleteSheet(XLSSHEETNM_HINSYU)   '余分なシートを削除

        '                        '-->2010.12.25 add by takagi #43
        '                        '先頭シートを選択
        '                        eh.targetSheetByIdx = 1
        '                        eh.selectSheet(eh.targetSheet)
        '                        eh.selectCell(1, 1)
        '                        '<--2010.12.25 add by takagi #43
        '                    Finally
        '                        eh.close()
        '                    End Try

        '                    'EXCELファイル開く
        '                    eh.display()

        '                Catch ue As UsrDefException
        '                    ue.dspMsg()
        '                    Throw ue
        '                Catch ex As Exception
        '                    'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
        '                    Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)), , StartUp.iniValue.LogFilePath)
        '                Finally
        '                    eh.endUse()
        '                    eh = Nothing
        '                End Try
        '            Finally
        '                '画面消去
        '                pb.Close()
        '            End Try
        '        Catch ue As UsrDefException         'ユーザー定義例外
        '            Call ue.dspMsg()
        '            Throw ue                        'キャッチした例外をそのままスロー
        '        Catch ex As Exception               'システム例外
        '            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        '        End Try
        '    End Sub

        '    '-------------------------------------------------------------------------------
        '    '   エクセル出力用データ抽出
        '    '　（処理概要）エクセル出力用のデータをM11から抽出する。
        '    '   ●入力パラメタ  ：prmJJuyousaki     需要先の値
        '    '   ●出力パラメタ  ：prmDs             抽出結果のデータセット
        '    '   ●出力パラメタ  ：prmRecCnt         抽出結果件数
        '    '-------------------------------------------------------------------------------
        '    Private Sub getM11DataForXls(ByVal prmJuyousaki As String, ByRef prmDs As DataSet, ByRef prmRecCnt As Integer)
        '        Try

        '            'EXCEL用のデータ取得
        '            Dim SQL As String = ""
        '            SQL = "SELECT "
        '            SQL = SQL & N & " (TT_H_SIYOU_CD "
        '            SQL = SQL & N & "  || TT_H_HIN_CD "
        '            SQL = SQL & N & "  || TT_H_SENSIN_CD "
        '            SQL = SQL & N & "  || TT_H_SIZE_CD "
        '            SQL = SQL & N & "  || TT_H_COLOR_CD)   " & COLDT_HINMEICD       '品名コード
        '            SQL = SQL & N & " ,TT_HINMEI           " & COLDT_HINMEI         '品名
        '            SQL = SQL & N & " ,TT_LOT              " & COLDT_LOTTYOU        '標準ロット長
        '            SQL = SQL & N & " ,TT_TANCYO           " & COLDT_TANTYOU        '製作単長
        '            SQL = SQL & N & " ,TT_JYOSU            " & COLDT_JOSU           '入庫本数 全体
        '            SQL = SQL & N & " ,TT_KZAIKOTUKISU     " & COLDT_KIJUNTUKISU    '基準月数
        '            SQL = SQL & N & " ,TT_ABCKBN           " & COLDT_ABC            'ABC区分
        '            SQL = SQL & N & " FROM M11KEIKAKUHIN "
        '            SQL = SQL & N & "   WHERE "
        '            '需要先
        '            SQL = SQL & "   TT_JUYOUCD = '" & _db.rmSQ(prmJuyousaki) & "'"
        '            SQL = SQL & "   ORDER BY TT_JUYOUCD, TT_H_HIN_CD, TT_H_SENSIN_CD,  "
        '            SQL = SQL & "   TT_H_SIZE_CD, TT_H_SIYOU_CD, TT_H_COLOR_CD, TT_TEHAI_KBN "

        '            'SQL発行
        '            prmDs = _db.selectDB(SQL, RS, prmRecCnt)

        '        Catch ue As UsrDefException         'ユーザー定義例外
        '            Call ue.dspMsg()
        '            Throw ue                        'キャッチした例外をそのままスロー
        '        Catch ex As Exception               'システム例外
        '            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        '        End Try
        '    End Sub

        '    '-------------------------------------------------------------------------------
        '    '   エクセル出力用データ抽出
        '    '　（処理概要）エクセル出力用のデータをM12から抽出する。
        '    '   ●入力パラメタ  ：prmJuyousaki      需要先の値
        '    '   ●出力パラメタ  ：prmDs             抽出結果のデータセット
        '    '   ●出力パラメタ  ：prmRecCnt         抽出結果件数
        '    '-------------------------------------------------------------------------------
        '    Private Sub getM12DataForXls(ByVal prmJuyousaki As String, ByRef prmDs As DataSet, ByRef prmRecCnt As Integer)
        '        Try

        '            'EXCEL用のデータ取得
        '            Dim SQL As String = ""
        '            SQL = "SELECT "
        '            SQL = SQL & N & "  M12.HINMEICD " & COLDT_M12HINMEICD       '実品名コード
        '            SQL = SQL & N & "  ,M12.KHINMEICD " & COLDT_M12KHINMEICD    '計画品名コード
        '            SQL = SQL & N & " FROM  M12SYUYAKU M12 "
        '            SQL = SQL & N & "   LEFT JOIN  M11KEIKAKUHIN M11 "
        '            SQL = SQL & N & "   ON M11.TT_KHINMEICD = M12.KHINMEICD "
        '            SQL = SQL & N & "   WHERE "
        '            SQL = SQL & N & "   NOT M12.KHINMEICD = M12.HINMEICD "

        '            '需要先
        '            SQL = SQL & N & "   AND "
        '            SQL = SQL & "   M11.TT_JUYOUCD = '" & _db.rmSQ(prmJuyousaki) & "'"

        '            SQL = SQL & "   ORDER BY TT_JUYOUCD, TT_H_HIN_CD, TT_H_SENSIN_CD,  "
        '            SQL = SQL & "   TT_H_SIZE_CD, TT_H_SIYOU_CD, TT_H_COLOR_CD, TT_TEHAI_KBN "

        '            'SQL発行
        '            prmDs = _db.selectDB(SQL, RS2, prmRecCnt)


        '        Catch ue As UsrDefException         'ユーザー定義例外
        '            Call ue.dspMsg()
        '            Throw ue                        'キャッチした例外をそのままスロー
        '        Catch ex As Exception               'システム例外
        '            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        '        End Try
        '    End Sub

        '#End Region

        '    '------------------------------------------------------------------------------------------------------
        '    '　ABC分析ボタン押下
        '    '------------------------------------------------------------------------------------------------------
        '    Private Sub btnABC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnABC.Click

        '        Dim openForm As ZM410B_ABCBunseki = New ZM410B_ABCBunseki(_msgHd, _db, Me, updFlg.updFlgABC)      '画面遷移
        '        openForm.Show()                                                             '画面表示
        '        Me.Hide()

        '    End Sub

        '    '------------------------------------------------------------------------------------------------------
        '    '　品種区分マスタメンテボタン押下
        '    '------------------------------------------------------------------------------------------------------
        '    Private Sub btnHMstMente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHMstMente.Click

        '        Dim openForm As ZM210E_HinsyuKbn = New ZM210E_HinsyuKbn(_msgHd, _db, Me, updFlg.updFlgHMstMente)      'パラメタを遷移先画面へ渡す
        '        openForm.Show()                                                             '画面表示
        '        Me.Hide()

        '    End Sub

        '    '------------------------------------------------------------------------------------------------------
        '    '　汎用マスタメンテボタン押下
        '    '------------------------------------------------------------------------------------------------------
        '    Private Sub btnHanyoMst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHanyoMst.Click

        '        Me.Hide()
        '        Dim openForm As ZM310E_HanyouMstMente = New ZM310E_HanyouMstMente(_msgHd, _db, Me, updFlg.updFlgHanyoMst)      '画面遷移
        '        openForm.Show()                                                             '画面表示

        '    End Sub

        '    '------------------------------------------------------------------------------------------------------
        '    '　生産能力マスタメンテボタン押下
        '    '------------------------------------------------------------------------------------------------------
        '    Private Sub btnSNouryokuMst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSNouryokuMst.Click

        '        Dim openForm As ZM610E_SeisanMstMente = New ZM610E_SeisanMstMente(_msgHd, _db, Me, updFlg.updFlgSNouryokuMst)      '画面遷移
        '        openForm.Show()                                                             '画面表示
        '        Me.Hide()

        '    End Sub

        '    '------------------------------------------------------------------------------------------------------
        '    '　外部システム連携ボタン押下
        '    '------------------------------------------------------------------------------------------------------
        '    Private Sub btnGRenkei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGRenkei.Click

        '        Dim openForm As ZM510B_GaibuSystem = New ZM510B_GaibuSystem(_msgHd, _db, Me)      '画面遷移
        '        openForm.Show()                                                             '画面表示
        '        Me.Hide()

        '    End Sub

        '    '------------------------------------------------------------------------------------------------------
        '    '　販売実績照会ボタン押下
        '    '------------------------------------------------------------------------------------------------------
        '    Private Sub btnHSyoukai_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHSyoukai.Click

        '        Dim openForm As ZE110Q_HanbaiJisseki = New ZE110Q_HanbaiJisseki(_msgHd, _db, Me)      '画面遷移
        '        openForm.Show()                                                             '画面表示
        '        Me.Hide()

        '    End Sub

        '    '------------------------------------------------------------------------------------------------------
        '    '　在庫実績照会ボタン押下
        '    '------------------------------------------------------------------------------------------------------
        '    Private Sub btnZSyoukai_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZSyoukai.Click

        '        Dim openForm As ZE210Q_ZaikoJisseki = New ZE210Q_ZaikoJisseki(_msgHd, _db, Me)      '画面遷移
        '        openForm.Show()                                                             '画面表示
        '        Me.Hide()

        '    End Sub

        '    Private Sub tabGeturei_Click(sender As Object, e As EventArgs) Handles tabGeturei.Click

        '    End Sub
    End Class