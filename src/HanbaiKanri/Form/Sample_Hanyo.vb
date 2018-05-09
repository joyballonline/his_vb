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

Public Class Sample_Hanyo
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
    End Sub

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
        Me.TableLayoutPanel23 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel24 = New System.Windows.Forms.TableLayoutPanel()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel25 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.btnModoru = New System.Windows.Forms.Button()
        Me.TableLayoutPanel28 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel23.SuspendLayout()
        Me.TableLayoutPanel24.SuspendLayout()
        Me.TableLayoutPanel25.SuspendLayout()
        Me.TableLayoutPanel28.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel23
        '
        Me.TableLayoutPanel23.ColumnCount = 3
        Me.TableLayoutPanel23.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel23.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90.0!))
        Me.TableLayoutPanel23.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel23.Controls.Add(Me.TableLayoutPanel24, 1, 1)
        Me.TableLayoutPanel23.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel23.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel23.Name = "TableLayoutPanel23"
        Me.TableLayoutPanel23.RowCount = 3
        Me.TableLayoutPanel23.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel23.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.0!))
        Me.TableLayoutPanel23.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel23.Size = New System.Drawing.Size(456, 323)
        Me.TableLayoutPanel23.TabIndex = 0
        '
        'TableLayoutPanel24
        '
        Me.TableLayoutPanel24.ColumnCount = 1
        Me.TableLayoutPanel24.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel24.Controls.Add(Me.TableLayoutPanel28, 0, 0)
        Me.TableLayoutPanel24.Controls.Add(Me.ListBox1, 0, 1)
        Me.TableLayoutPanel24.Controls.Add(Me.TableLayoutPanel25, 0, 2)
        Me.TableLayoutPanel24.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel24.Location = New System.Drawing.Point(25, 19)
        Me.TableLayoutPanel24.Name = "TableLayoutPanel24"
        Me.TableLayoutPanel24.RowCount = 3
        Me.TableLayoutPanel24.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel24.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel24.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel24.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel24.Size = New System.Drawing.Size(404, 284)
        Me.TableLayoutPanel24.TabIndex = 0
        '
        'ListBox1
        '
        Me.ListBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 12
        Me.ListBox1.Items.AddRange(New Object() {"1:外税", "2:内税", "3:非課税"})
        Me.ListBox1.Location = New System.Drawing.Point(0, 28)
        Me.ListBox1.Margin = New System.Windows.Forms.Padding(0)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(404, 198)
        Me.ListBox1.TabIndex = 1243
        '
        'Label34
        '
        Me.Label34.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label34.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label34.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label34.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.Label34.Location = New System.Drawing.Point(0, 6)
        Me.Label34.Margin = New System.Windows.Forms.Padding(0)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(127, 22)
        Me.Label34.TabIndex = 0
        Me.Label34.Text = "汎用選択ラベル"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TableLayoutPanel25
        '
        Me.TableLayoutPanel25.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel25.ColumnCount = 3
        Me.TableLayoutPanel25.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.0!))
        Me.TableLayoutPanel25.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel25.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.0!))
        Me.TableLayoutPanel25.Controls.Add(Me.btnSelect, 0, 1)
        Me.TableLayoutPanel25.Controls.Add(Me.btnModoru, 2, 1)
        Me.TableLayoutPanel25.Location = New System.Drawing.Point(157, 229)
        Me.TableLayoutPanel25.Name = "TableLayoutPanel25"
        Me.TableLayoutPanel25.RowCount = 2
        Me.TableLayoutPanel25.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel25.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel25.Size = New System.Drawing.Size(244, 52)
        Me.TableLayoutPanel25.TabIndex = 1244
        '
        'btnSelect
        '
        Me.btnSelect.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSelect.Location = New System.Drawing.Point(3, 13)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(102, 36)
        Me.btnSelect.TabIndex = 3
        Me.btnSelect.Text = "選択(&G)"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'btnModoru
        '
        Me.btnModoru.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnModoru.Location = New System.Drawing.Point(136, 13)
        Me.btnModoru.Name = "btnModoru"
        Me.btnModoru.Size = New System.Drawing.Size(102, 36)
        Me.btnModoru.TabIndex = 4
        Me.btnModoru.Text = "戻る(&B)"
        Me.btnModoru.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel28
        '
        Me.TableLayoutPanel28.ColumnCount = 3
        Me.TableLayoutPanel28.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel28.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.83168!))
        Me.TableLayoutPanel28.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel28.Controls.Add(Me.Label37, 1, 0)
        Me.TableLayoutPanel28.Controls.Add(Me.Label34, 0, 0)
        Me.TableLayoutPanel28.Controls.Add(Me.Label39, 2, 0)
        Me.TableLayoutPanel28.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel28.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel28.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel28.Name = "TableLayoutPanel28"
        Me.TableLayoutPanel28.RowCount = 1
        Me.TableLayoutPanel28.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel28.Size = New System.Drawing.Size(404, 28)
        Me.TableLayoutPanel28.TabIndex = 1245
        '
        'Label37
        '
        Me.Label37.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label37.AutoSize = True
        Me.Label37.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label37.Location = New System.Drawing.Point(270, 16)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(29, 12)
        Me.Label37.TabIndex = 2
        Me.Label37.Text = "件数"
        '
        'Label39
        '
        Me.Label39.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label39.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label39.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label39.Location = New System.Drawing.Point(317, 6)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(84, 22)
        Me.Label39.TabIndex = 4
        Me.Label39.Text = "0"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Sample_Hanyo
        '
        Me.ClientSize = New System.Drawing.Size(456, 323)
        Me.Controls.Add(Me.TableLayoutPanel23)
        Me.Name = "Sample_Hanyo"
        Me.Text = "汎用選択"
        Me.TableLayoutPanel23.ResumeLayout(False)
        Me.TableLayoutPanel24.ResumeLayout(False)
        Me.TableLayoutPanel25.ResumeLayout(False)
        Me.TableLayoutPanel28.ResumeLayout(False)
        Me.TableLayoutPanel28.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Public Event CellEnter As DataGridViewCellEventHandler

    Private Const COMBOBOX_COLUMN As Integer = 1

    'Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
    '    Dim clickIndex As Integer
    '    clickIndex = e.ColumnIndex
    '    If e.ColumnIndex = 1 Then
    '        SendKeys.SendWait("{F4}")
    '        Dim openForm As Form1 = New Form1()      '画面遷移
    '        openForm.Show()                      '画面表示
    '    ElseIf e.ColumnIndex = 4 Then
    '        SendKeys.SendWait("{F4}")
    '        Dim openForm As Sample_HanyoSelect = New Sample_HanyoSelect()      '画面遷移
    '        openForm.Show()                      '画面表示
    '    ElseIf e.ColumnIndex = 8 Then
    '        SendKeys.SendWait("{F4}")
    '        Dim openForm As Sample_HanyoSelect2 = New Sample_HanyoSelect2()      '画面遷移
    '        openForm.Show()                      '画面表示
    '    End If
    'End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles DataGridView1.KeyDown
        'F1キーが押されたか調べる
        If e.KeyData = Keys.F1 Then
        End If
    End Sub

    'Private Sub DataGridView1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellEnter
    '    r = e.RowIndex
    '    c = e.ColumnIndex
    'End Sub



    Private Sub Sample_Chumon_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub cboKubun_SelectedIndexChanged(sender As Object, e As EventArgs)

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

    Private Sub TableLayoutPanel1_Paint(sender As Object, e As PaintEventArgs) Handles TableLayoutPanel1.Paint

    End Sub

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

    Private Sub TableLayoutPanel13_Paint(sender As Object, e As PaintEventArgs) Handles TableLayoutPanel13.Paint

    End Sub

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

    Private Sub TableLayoutPanel17_Paint(sender As Object, e As PaintEventArgs) Handles TableLayoutPanel17.Paint

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


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    Friend WithEvents Column13 As DataGridViewTextBoxColumn
    Friend WithEvents Column14 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridView1 As Windows.Forms.DataGridView
    Friend WithEvents Label33 As Label

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

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

    Dim clickColumnIndex As Integer
    Dim clickRowIndex As Integer

    Public selectVal As String
    Public ReadOnly Property ReturnVal() As String
        Get
            Return selectVal
        End Get
    End Property

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        Dim selectValue As String = ListBox1.Text
        Dim selectValueIndex As Integer = selectValue.IndexOf(":")
        If selectValueIndex <> -1 Then
            '税区分
            If Me.clickColumnIndex = 4 Then
                selectVal = selectValue.Substring(selectValueIndex + 1, 1)
                '単位
            ElseIf Me.clickColumnIndex = 8 Then
                selectVal = selectValue.Substring(selectValueIndex + 1)

            ElseIf Me.clickColumnIndex = 12 Then
                selectVal = selectValue.Substring(selectValueIndex + 1, 2)
            End If

        End If
        Me.Hide()
    End Sub

    Private Sub btnModoru_Click(sender As Object, e As EventArgs) Handles btnModoru.Click
        Me.Hide()
    End Sub

    Public Sub New(ByVal clickColumnIndex As Integer, ByVal clickRowIndex As Integer)
        'Form1から受け取ったデータをForm2インスタンスのメンバに格納
        Me.clickColumnIndex = clickColumnIndex

        InitializeComponent()

        'フォーム上のキーイベントを拾う
        Me.KeyPreview = True

        Me.Label21 = New System.Windows.Forms.Label()

        If Me.clickColumnIndex = 4 Then
            Label34.Text = "税区分"
            ListBox1.Items.Clear()
            ListBox1.Items.Add("1:外税")
            ListBox1.Items.Add("2:内税")
            ListBox1.Items.Add("3:非課税")

        ElseIf Me.clickColumnIndex = 8 Then
            Label34.Text = "単位"
            ListBox1.Items.Clear()
            ListBox1.Items.Add("1:Kg")
            ListBox1.Items.Add("2:個")
            ListBox1.Items.Add("3:枚")

        ElseIf Me.clickColumnIndex = 12 Then
            Label34.Text = "包装"
            ListBox1.Items.Clear()
            ListBox1.Items.Add("1:発砲")
            ListBox1.Items.Add("2:ダンボール")
        End If

        Dim hanyoListCnt As Integer = ListBox1.Items.Count
        Label39.Text = hanyoListCnt


    End Sub

    'フォーム上のキーイベント取得
    Private Sub Sample_Shohin_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown

        'フォーム上で Alt + Gを押下したら
        If e.KeyCode = Keys.G AndAlso e.Alt Then
            Me.btnSelect_Click(sender, e)
        End If

        'フォーム上で Alt + Gを押下したら
        If e.KeyCode = Keys.B AndAlso e.Alt Then
            Me.Hide()
        End If
    End Sub



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