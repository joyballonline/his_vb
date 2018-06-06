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

Public Class frmC10F90_Hanyo
    Inherits System.Windows.Forms.Form


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
    Private _hanyoKbn As String     '汎用マスタ　固定キー　親フォームより渡される StartUp.HAN_xxxxxx
    '
    Public _selected As Boolean     'フォームからの戻り値用　選択状態　True:選択された　False:選択されなかった
    Public _selectValCD As String   'フォームからの戻り値用　コード
    Public _selectValNM As String   'フォームからの戻り値用　名称

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
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByRef prmHanyoKbn As String)
        Call Me.New()

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示

        _hanyoKbn = prmHanyoKbn '汎用マスタ　固定キーセット
        _selected = False       '選択状態リセット

        Me.Label21 = New System.Windows.Forms.Label()

        lblTitle.Text = _hanyoKbn.Substring(4) 'パラメタ)固定キーの5文字目以降

        Dim strSql As String = ""
        Try
            strSql = "SELECT "
            strSql = strSql & "    h.固定キー, h.可変キー, h.表示順, h.文字１, h.文字２ "
            strSql = strSql & " FROM m90_hanyo h "
            strSql = strSql & " WHERE h.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and h.固定キー = '" & _hanyoKbn & "'"
            strSql = strSql & " order by h.表示順 "

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(strSql, RS, reccnt)

            '描画の前にすべてクリアする
            ListBox1.Items.Clear()

            Dim hanyoDataList = New ArrayList()
            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                hanyoDataList.Add(New HanyoData(_db.rmNullStr(ds.Tables(RS).Rows(index)("可変キー")), _db.rmNullStr(ds.Tables(RS).Rows(index)("可変キー")) & " " & _db.rmNullStr(ds.Tables(RS).Rows(index)("文字１"))))
            Next

            ListBox1.DataSource = hanyoDataList
            ListBox1.ValueMember = "Val"
            ListBox1.DisplayMember = "DispVal"

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

        Dim hanyoListCnt As Integer = ListBox1.Items.Count
        Label39.Text = hanyoListCnt
        ListBox1.SelectedIndex = 0


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

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    Private Sub InitializeComponent()
        Me.TableLayoutPanel23 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel24 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel28 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.TableLayoutPanel25 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.btnModoru = New System.Windows.Forms.Button()
        Me.TableLayoutPanel23.SuspendLayout()
        Me.TableLayoutPanel24.SuspendLayout()
        Me.TableLayoutPanel28.SuspendLayout()
        Me.TableLayoutPanel25.SuspendLayout()
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
        'TableLayoutPanel28
        '
        Me.TableLayoutPanel28.ColumnCount = 3
        Me.TableLayoutPanel28.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel28.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 61.13861!))
        Me.TableLayoutPanel28.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.693069!))
        Me.TableLayoutPanel28.Controls.Add(Me.lblTitle, 0, 0)
        Me.TableLayoutPanel28.Controls.Add(Me.Label39, 1, 0)
        Me.TableLayoutPanel28.Controls.Add(Me.Label37, 2, 0)
        Me.TableLayoutPanel28.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel28.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel28.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel28.Name = "TableLayoutPanel28"
        Me.TableLayoutPanel28.RowCount = 1
        Me.TableLayoutPanel28.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel28.Size = New System.Drawing.Size(404, 28)
        Me.TableLayoutPanel28.TabIndex = 1245
        '
        'lblTitle
        '
        Me.lblTitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTitle.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.lblTitle.Location = New System.Drawing.Point(0, 6)
        Me.lblTitle.Margin = New System.Windows.Forms.Padding(0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(127, 22)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "汎用選択ラベル"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label39
        '
        Me.Label39.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label39.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label39.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label39.Location = New System.Drawing.Point(293, 6)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(84, 22)
        Me.Label39.TabIndex = 4
        Me.Label39.Text = "0"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label37
        '
        Me.Label37.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label37.AutoSize = True
        Me.Label37.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label37.Location = New System.Drawing.Point(383, 13)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(18, 15)
        Me.Label37.TabIndex = 2
        Me.Label37.Text = "件"
        '
        'ListBox1
        '
        Me.ListBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBox1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 15
        Me.ListBox1.Items.AddRange(New Object() {"1:外税", "2:内税", "3:非課税"})
        Me.ListBox1.Location = New System.Drawing.Point(0, 28)
        Me.ListBox1.Margin = New System.Windows.Forms.Padding(0)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(404, 198)
        Me.ListBox1.TabIndex = 1243
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
        Me.btnSelect.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSelect.Location = New System.Drawing.Point(3, 13)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(102, 36)
        Me.btnSelect.TabIndex = 3
        Me.btnSelect.Text = "選択(&G)"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'btnModoru
        '
        Me.btnModoru.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnModoru.Location = New System.Drawing.Point(136, 13)
        Me.btnModoru.Name = "btnModoru"
        Me.btnModoru.Size = New System.Drawing.Size(102, 36)
        Me.btnModoru.TabIndex = 4
        Me.btnModoru.Text = "戻る(&B)"
        Me.btnModoru.UseVisualStyleBackColor = True
        '
        'frmC10F90_Hanyo
        '
        Me.ClientSize = New System.Drawing.Size(456, 323)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel23)
        Me.Name = "frmC10F90_Hanyo"
        Me.Text = "汎用選択(C10F90)"
        Me.TableLayoutPanel23.ResumeLayout(False)
        Me.TableLayoutPanel24.ResumeLayout(False)
        Me.TableLayoutPanel28.ResumeLayout(False)
        Me.TableLayoutPanel28.PerformLayout()
        Me.TableLayoutPanel25.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Public Event CellEnter As DataGridViewCellEventHandler

    Private Const COMBOBOX_COLUMN As Integer = 1


    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles DataGridView1.KeyDown
        'F1キーが押されたか調べる
        If e.KeyData = Keys.F1 Then
        End If
    End Sub

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

    '選択したコード
    Public ReadOnly Property GetValCD() As String
        Get
            Return _selectValCD
        End Get
    End Property

    '選択した名称
    Public ReadOnly Property GetValNM() As String
        Get
            Return _selectValNM
        End Get
    End Property

    '選択状態   True:選択状態 False:非選択状態
    Public ReadOnly Property Selected() As String
        Get
            Return _selected
        End Get
    End Property

    '一覧キーダウン
    Private Sub ListBox1_KeyDown(sender As Object, e As EventArgs) Handles ListBox1.KeyDown
        Try
            Dim keyEventArgs As KeyEventArgs = TryCast(e, KeyEventArgs)

            If keyEventArgs.KeyData = Keys.Enter Then
                '押下キーがEnterの場合

                '汎用データ選択処理
                selectHanyo()

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

    '選択ボタンクリック、一覧タブルクリック
    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click, ListBox1.DoubleClick
        Try
            '汎用データ選択処理
            selectHanyo()

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

    '汎用データ選択処理
    Private Sub selectHanyo()

        If ListBox1.Text <> "" Then
            _selectValCD = ListBox1.SelectedValue       'コード
            Dim strSql As String = ""

            strSql = "SELECT "
            strSql = strSql & "    h.固定キー, h.可変キー, h.表示順, h.文字１, h.文字２ "
            strSql = strSql & " FROM m90_hanyo h "
            strSql = strSql & " WHERE h.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and h.固定キー = '" & _hanyoKbn & "' and h.可変キー ='" & _selectValCD & "'"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(strSql, RS, reccnt)

            _selectValNM = _db.rmNullStr(ds.Tables(RS).Rows(0)("文字２"))                '名称

            _selected = True        '選択状態
        End If

        Me.Hide()

    End Sub

    'リストボックスに表示する値と内部的に持たせる値を作るクラス
    Public Class HanyoData
        Private myVal As String
        Private myDispVal As String

        Public Sub New(ByVal prmDispVal As String, ByVal prmVal As String)
            Me.myVal = prmVal
            Me.myDispVal = prmDispVal
        End Sub 'NewNew

        Public ReadOnly Property DispVal() As String
            Get
                Return myVal
            End Get
        End Property

        Public ReadOnly Property Val() As String
            Get
                Return myDispVal
            End Get
        End Property

    End Class 'HanyoData

End Class