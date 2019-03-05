'===============================================================================
'
'　SPIN
'　　（システム名）販売管理
'　　（処理機能名）メニュー画面
'    （フォームID）C01F30
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   田頭        2018/01/27                 新規              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
    Imports UtilMDL.DataGridView
    Imports UtilMDL.FileDirectory
    Imports UtilMDL.xls

Public Class frmC01F30_Menu
    Inherits System.Windows.Forms.Form

    '------------------------------------------------------------------------------------------------------
    'メンバー定数宣言
    '------------------------------------------------------------------------------------------------------
    'PG制御文字 
    Private Const RS As String = "RecSet"                               'レコードセットテーブル
    Private Const HAIFUN_ID As String = "H@@@@@"
    Private Const HAIFUN_GYOMU1 As String = "-----------"
    Private Const HAIFUN_SHORI As String = "----------------"
    Private Const HAIFUN_SETUMEI As String = "-------------------------------------------"
    Private Const HAIFUN_MYSOUSANICHIJI As String = "---------------"
    Private Const HAIFUN_SOUSA As String = "----------"
    Private Const HAIFUN_ZENKAI As String = "---------------"

    '-------------------------------------------------------------------------------
    '   変数定義
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _langHd As UtilLangHandler
    Private _db As UtilDBIf
    Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ

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
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefLangHd As UtilLangHandler, ByRef prmRefDbHd As UtilDBIf)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _langHd = prmRefLangHd
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Dim Title As String = _langHd.getLANG("MENU", frmC01F10_Login.loginValue.Language)
        Me.Text = Title & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示

        Me.btnSelect.Text = _langHd.getLANG("SelectG", frmC01F10_Login.loginValue.Language)
        'Me.btnUserMaintenance.Text = _langHd.getLANG("UserMST", frmC01F10_Login.loginValue.Language)
        'Me.btnLanguageMaster.Text = _langHd.getLANG("LangMST", frmC01F10_Login.loginValue.Language)
        'Me.btnSupplierMaster.Text = _langHd.getLANG("SupplierMST", frmC01F10_Login.loginValue.Language)
        'Me.btnCostmerMaster.Text = _langHd.getLANG("CustomerMST", frmC01F10_Login.loginValue.Language)
        'Me.btnHanyouMaster.Text = _langHd.getLANG("CommonMST", frmC01F10_Login.loginValue.Language)
        'Me.BtnCompanyMaster.Text = _langHd.getLANG("CompanyMST", frmC01F10_Login.loginValue.Language)
        'Me.Button3.Text = _langHd.getLANG("Quotation", frmC01F10_Login.loginValue.Language)
        'Me.BtnOrder.Text = _langHd.getLANG("Order", frmC01F10_Login.loginValue.Language)
        'Me.cmdExit.Text = _langHd.getLANG("Exit(B)", frmC01F10_Login.loginValue.Language)
        'Me.BtnPurchase.Text = _langHd.getLANG("Purchase", frmC01F10_Login.loginValue.Language)


        _init = True

    End Sub

    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.chkM01 = New System.Windows.Forms.CheckBox()
        Me.chkH07 = New System.Windows.Forms.CheckBox()
        Me.chkH05 = New System.Windows.Forms.CheckBox()
        Me.chkH04 = New System.Windows.Forms.CheckBox()
        Me.chkH02 = New System.Windows.Forms.CheckBox()
        Me.chkH01 = New System.Windows.Forms.CheckBox()
        Me.dgvLIST = New System.Windows.Forms.DataGridView()
        Me.処理ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.業務 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.処理名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.説明 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.My前回操作日時 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.操作者 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.前回操作日時 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chkH06 = New System.Windows.Forms.CheckBox()
        Me.chkH03 = New System.Windows.Forms.CheckBox()
        CType(Me.dgvLIST, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnSelect
        '
        Me.btnSelect.Enabled = False
        Me.btnSelect.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSelect.Location = New System.Drawing.Point(1144, 501)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(94, 48)
        Me.btnSelect.TabIndex = 20
        Me.btnSelect.Text = "選択(&G)"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'cmdExit
        '
        Me.cmdExit.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdExit.Location = New System.Drawing.Point(1244, 501)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(94, 48)
        Me.cmdExit.TabIndex = 19
        Me.cmdExit.Text = "終了(&B)"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'chkM01
        '
        Me.chkM01.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkM01.AutoSize = True
        Me.chkM01.Checked = True
        Me.chkM01.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkM01.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkM01.Location = New System.Drawing.Point(660, -72)
        Me.chkM01.Name = "chkM01"
        Me.chkM01.Size = New System.Drawing.Size(88, 19)
        Me.chkM01.TabIndex = 18
        Me.chkM01.Text = "マスタ保守"
        Me.chkM01.UseVisualStyleBackColor = True
        Me.chkM01.Visible = False
        '
        'chkH07
        '
        Me.chkH07.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkH07.AutoSize = True
        Me.chkH07.Checked = True
        Me.chkH07.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkH07.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkH07.Location = New System.Drawing.Point(568, -72)
        Me.chkH07.Name = "chkH07"
        Me.chkH07.Size = New System.Drawing.Size(86, 19)
        Me.chkH07.TabIndex = 16
        Me.chkH07.Text = "入庫業務"
        Me.chkH07.UseVisualStyleBackColor = True
        Me.chkH07.Visible = False
        '
        'chkH05
        '
        Me.chkH05.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkH05.AutoSize = True
        Me.chkH05.Checked = True
        Me.chkH05.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkH05.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkH05.Location = New System.Drawing.Point(382, -72)
        Me.chkH05.Name = "chkH05"
        Me.chkH05.Size = New System.Drawing.Size(86, 19)
        Me.chkH05.TabIndex = 15
        Me.chkH05.Text = "発注業務"
        Me.chkH05.UseVisualStyleBackColor = True
        Me.chkH05.Visible = False
        '
        'chkH04
        '
        Me.chkH04.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkH04.AutoSize = True
        Me.chkH04.Checked = True
        Me.chkH04.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkH04.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkH04.Location = New System.Drawing.Point(290, -72)
        Me.chkH04.Name = "chkH04"
        Me.chkH04.Size = New System.Drawing.Size(86, 19)
        Me.chkH04.TabIndex = 14
        Me.chkH04.Text = "出庫業務"
        Me.chkH04.UseVisualStyleBackColor = True
        Me.chkH04.Visible = False
        '
        'chkH02
        '
        Me.chkH02.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkH02.AutoSize = True
        Me.chkH02.Checked = True
        Me.chkH02.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkH02.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkH02.Location = New System.Drawing.Point(104, -72)
        Me.chkH02.Name = "chkH02"
        Me.chkH02.Size = New System.Drawing.Size(86, 19)
        Me.chkH02.TabIndex = 13
        Me.chkH02.Text = "受注業務"
        Me.chkH02.UseVisualStyleBackColor = True
        Me.chkH02.Visible = False
        '
        'chkH01
        '
        Me.chkH01.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkH01.AutoSize = True
        Me.chkH01.Checked = True
        Me.chkH01.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkH01.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkH01.Location = New System.Drawing.Point(12, -72)
        Me.chkH01.Name = "chkH01"
        Me.chkH01.Size = New System.Drawing.Size(86, 19)
        Me.chkH01.TabIndex = 12
        Me.chkH01.Text = "注文業務"
        Me.chkH01.UseVisualStyleBackColor = True
        Me.chkH01.Visible = False
        '
        'dgvLIST
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvLIST.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvLIST.ColumnHeadersHeight = 25
        Me.dgvLIST.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.処理ID, Me.業務, Me.処理名, Me.説明, Me.My前回操作日時, Me.操作者, Me.前回操作日時})
        Me.dgvLIST.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvLIST.Location = New System.Drawing.Point(12, 37)
        Me.dgvLIST.MultiSelect = False
        Me.dgvLIST.Name = "dgvLIST"
        Me.dgvLIST.RowHeadersVisible = False
        Me.dgvLIST.RowHeadersWidth = 25
        Me.dgvLIST.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvLIST.Size = New System.Drawing.Size(1326, 457)
        Me.dgvLIST.TabIndex = 11
        '
        '処理ID
        '
        DataGridViewCellStyle2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.処理ID.DefaultCellStyle = DataGridViewCellStyle2
        Me.処理ID.HeaderText = "処理ID"
        Me.処理ID.Name = "処理ID"
        Me.処理ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.処理ID.Visible = False
        Me.処理ID.Width = 300
        '
        '業務
        '
        DataGridViewCellStyle3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.業務.DefaultCellStyle = DataGridViewCellStyle3
        Me.業務.HeaderText = "業務"
        Me.業務.Name = "業務"
        Me.業務.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.業務.Width = 300
        '
        '処理名
        '
        DataGridViewCellStyle4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.処理名.DefaultCellStyle = DataGridViewCellStyle4
        Me.処理名.HeaderText = "処理名"
        Me.処理名.Name = "処理名"
        Me.処理名.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.処理名.Width = 200
        '
        '説明
        '
        DataGridViewCellStyle5.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.説明.DefaultCellStyle = DataGridViewCellStyle5
        Me.説明.HeaderText = "説明"
        Me.説明.Name = "説明"
        Me.説明.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.説明.Width = 640
        '
        'My前回操作日時
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.My前回操作日時.DefaultCellStyle = DataGridViewCellStyle6
        Me.My前回操作日時.HeaderText = " My前回操作日時"
        Me.My前回操作日時.Name = "My前回操作日時"
        Me.My前回操作日時.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.My前回操作日時.Visible = False
        Me.My前回操作日時.Width = 200
        '
        '操作者
        '
        DataGridViewCellStyle7.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.操作者.DefaultCellStyle = DataGridViewCellStyle7
        Me.操作者.HeaderText = "　操作者"
        Me.操作者.Name = "操作者"
        Me.操作者.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.操作者.Visible = False
        Me.操作者.Width = 200
        '
        '前回操作日時
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle8.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.前回操作日時.DefaultCellStyle = DataGridViewCellStyle8
        Me.前回操作日時.HeaderText = "前回操作日時"
        Me.前回操作日時.Name = "前回操作日時"
        Me.前回操作日時.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.前回操作日時.Visible = False
        Me.前回操作日時.Width = 200
        '
        'chkH06
        '
        Me.chkH06.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkH06.AutoSize = True
        Me.chkH06.Checked = True
        Me.chkH06.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkH06.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkH06.Location = New System.Drawing.Point(474, -72)
        Me.chkH06.Name = "chkH06"
        Me.chkH06.Size = New System.Drawing.Size(86, 19)
        Me.chkH06.TabIndex = 21
        Me.chkH06.Text = "仕入業務"
        Me.chkH06.UseVisualStyleBackColor = True
        Me.chkH06.Visible = False
        '
        'chkH03
        '
        Me.chkH03.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkH03.AutoSize = True
        Me.chkH03.Checked = True
        Me.chkH03.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkH03.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkH03.Location = New System.Drawing.Point(196, -72)
        Me.chkH03.Name = "chkH03"
        Me.chkH03.Size = New System.Drawing.Size(86, 19)
        Me.chkH03.TabIndex = 22
        Me.chkH03.Text = "売上業務"
        Me.chkH03.UseVisualStyleBackColor = True
        Me.chkH03.Visible = False
        '
        'frmC01F30_Menu
        '
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.ControlBox = False
        Me.Controls.Add(Me.chkH03)
        Me.Controls.Add(Me.chkH06)
        Me.Controls.Add(Me.btnSelect)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.chkM01)
        Me.Controls.Add(Me.chkH07)
        Me.Controls.Add(Me.chkH05)
        Me.Controls.Add(Me.chkH04)
        Me.Controls.Add(Me.chkH02)
        Me.Controls.Add(Me.chkH01)
        Me.Controls.Add(Me.dgvLIST)
        Me.Name = "frmC01F30_Menu"
        Me.Text = "処理メニュー（C01F30）"
        CType(Me.dgvLIST, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public Event CellEnter As DataGridViewCellEventHandler

    Private Const COMBOBOX_COLUMN As Integer = 1
    '-------------------------------------------------------------------------------
    '   画面ロード処理
    '   （処理概要） 画面が起動したときの処理を行う。
    '-------------------------------------------------------------------------------
    Private Sub frmC01F30_Menu_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '一覧クリア
        dgvLIST.Rows.Clear()

        ' ユーザ操作による行追加を無効(禁止)
        dgvLIST.AllowUserToAddRows = False

        '列の幅をユーザー変更可
        dgvLIST.AllowUserToResizeColumns = True

        '行の高さをユーザー変更不可
        dgvLIST.AllowUserToResizeRows = False

        '列ヘッダーの高さ変更不可
        dgvLIST.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

        'メニュー表示
        getMenuList(strcheckMenu())
        If frmC01F10_Login.loginValue.Language = "ENG" Then
            dgvLIST.Columns("業務").HeaderText = "Business"
            dgvLIST.Columns("処理名").HeaderText = "ProcessingName"
            dgvLIST.Columns("説明").HeaderText = "Description"
            btnSelect.Text = "Select(&G)"
            cmdExit.Text = "Exit(&B)"
        End If

    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles DataGridView1.KeyDown
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

    Friend WithEvents TableLayoutPanel20 As TableLayoutPanel
    Friend WithEvents Label22 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents TextBox16 As TextBox
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

    Public Sub New(ByVal clickColumnIndex As Integer, ByVal clickRowIndex As Integer)
        'Form1から受け取ったデータをForm2インスタンスのメンバに格納
        Me.clickColumnIndex = clickColumnIndex

        InitializeComponent()

        'フォーム上のキーイベントを拾う
        Me.KeyPreview = True

        Me.Label21 = New System.Windows.Forms.Label()


    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs)
        Dim intRet As Integer
        intRet = _msgHd.dspMSG("SystemExit", frmC01F10_Login.loginValue.Language)
        If intRet = vbOK Then
            Application.Exit()
        End If
    End Sub

    '選択ボタンクリック
    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click

        Try
            'メニュー選択処理
            selectMenu()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '一覧セルダブルクリック
    Private Sub dgvLIST_CellDoubleClick(sender As Object, e As EventArgs) Handles dgvLIST.CellDoubleClick

        Try
            'ヘッダー行ダブルクリックの場合、処理終了
            If TryCast(e, DataGridViewCellEventArgs).RowIndex < 0 Then
                Exit Sub
            End If

            'メニュー選択処理
            selectMenu()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '一覧キーダウン
    Private Sub dgvLIST_KeyDown(sender As Object, e As EventArgs) Handles dgvLIST.KeyDown

        Try
            Dim keyEventArgs As KeyEventArgs = TryCast(e, KeyEventArgs)

            If keyEventArgs.KeyData = Keys.Enter Then
                '押下キーがEnterの場合

                'メニュー選択処理
                selectMenu()
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
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    Sub Check_Clicked(sender As Object, e As EventArgs) _

        'チェックされているメニューをもとに表示条件を作成
        getMenuList(strcheckMenu())

    End Sub

    'メニューリスト表示処理
    Private Sub getMenuList(Optional ByVal getMenuSql As String = "")

        '初期処理前の場合は処理終了
        If Not _init Then
            '処理終了
            Exit Sub
        End If

        '一覧をクリア
        dgvLIST.Rows.Clear()

        'チェックボックス未選択だったら処理終了
        If getMenuSql Is "" Then
            '選択ボタンを非活性
            Me.btnSelect.Enabled = False
            '処理終了
            Exit Sub
        End If

        Dim likeSql As String = ""
        likeSql += " and "
        likeSql += getMenuSql

        Dim strSql As String = ""

        'メニューを読み込み
        Try
            If frmC01F10_Login.loginValue.Language = "ENG" Then
                strSql = "SELECT "
                strSql = strSql & "    m.会社コード "
                strSql = strSql & "  , m.処理ＩＤ "
                strSql = strSql & "  , m.英語用処理名 "
                strSql = strSql & "  , m.業務ＩＤ "
                strSql = strSql & "  , m.英語用業務名"
                strSql = strSql & "  , m.表示順 "
                strSql = strSql & "  , m.英語用説明 "
                strSql = strSql & "  , m.削除フラグ "
                strSql = strSql & "  , to_char(p.更新日, 'yyyy/mm/dd hh24:mi') My前回操作日時 "
                strSql = strSql & "  , p2.更新者 "
                strSql = strSql & "  , to_char(p2.更新日, 'yyyy/mm/dd hh24:mi') 前回操作日時"
                strSql = strSql & " FROM m04_menu m "
                strSql = strSql & " left join l10_proclog p on "
                strSql = strSql & "           p.会社コード = m.会社コード "
                strSql = strSql & "      and  p.処理ＩＤ   = m.処理ＩＤ "
                strSql = strSql & "      and  p.更新日     = (SELECT MAX(更新日) "
                strSql = strSql & "                           FROM l10_proclog "
                strSql = strSql & "                           WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                strSql = strSql & "                             and 処理ＩＤ   = m.処理ＩＤ "
                strSql = strSql & "                             and 更新者     = '" & frmC01F10_Login.loginValue.TantoCD & "'"
                strSql = strSql & "                          ) "
                strSql = strSql & " left join l10_proclog p2 on "
                strSql = strSql & "           p2.会社コード = m.会社コード "
                strSql = strSql & "      and  p2.処理ＩＤ   = m.処理ＩＤ "
                strSql = strSql & "      and  p2.更新日     = (SELECT MAX(更新日) "
                strSql = strSql & "                            FROM l10_proclog "
                strSql = strSql & "                            WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                strSql = strSql & "                              and 処理ＩＤ   = m.処理ＩＤ "
                strSql = strSql & "                           ) "
                strSql = strSql & " Where m.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                strSql = strSql & " and "
                strSql = strSql & " m.削除フラグ = 0 "
                strSql = strSql & likeSql
                strSql = strSql & " order by m.表示順 "
            Else
                strSql = "SELECT "
                strSql = strSql & "    m.会社コード "
                strSql = strSql & "  , m.処理ＩＤ "
                strSql = strSql & "  , m.処理名 "
                strSql = strSql & "  , m.業務ＩＤ "
                strSql = strSql & "  , m.業務名"
                strSql = strSql & "  , m.表示順 "
                strSql = strSql & "  , m.説明 "
                strSql = strSql & "  , m.削除フラグ "
                strSql = strSql & "  , to_char(p.更新日, 'yyyy/mm/dd hh24:mi') My前回操作日時 "
                strSql = strSql & "  , p2.更新者 "
                strSql = strSql & "  , to_char(p2.更新日, 'yyyy/mm/dd hh24:mi') 前回操作日時"
                strSql = strSql & " FROM m04_menu m "
                strSql = strSql & " left join l10_proclog p on "
                strSql = strSql & "           p.会社コード = m.会社コード "
                strSql = strSql & "      and  p.処理ＩＤ   = m.処理ＩＤ "
                strSql = strSql & "      and  p.更新日     = (SELECT MAX(更新日) "
                strSql = strSql & "                           FROM l10_proclog "
                strSql = strSql & "                           WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                strSql = strSql & "                             and 処理ＩＤ   = m.処理ＩＤ "
                strSql = strSql & "                             and 更新者     = '" & frmC01F10_Login.loginValue.TantoCD & "'"
                strSql = strSql & "                          ) "
                strSql = strSql & " left join l10_proclog p2 on "
                strSql = strSql & "           p2.会社コード = m.会社コード "
                strSql = strSql & "      and  p2.処理ＩＤ   = m.処理ＩＤ "
                strSql = strSql & "      and  p2.更新日     = (SELECT MAX(更新日) "
                strSql = strSql & "                            FROM l10_proclog "
                strSql = strSql & "                            WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                strSql = strSql & "                              and 処理ＩＤ   = m.処理ＩＤ "
                strSql = strSql & "                           ) "
                strSql = strSql & " Where m.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                strSql = strSql & " and "
                strSql = strSql & " m.削除フラグ = 0 "
                strSql = strSql & likeSql
                strSql = strSql & " order by m.表示順 "
            End If


            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(strSql, RS, reccnt)

            '業務IDの間にハイフンレコード追加
            Dim ds2 As DataSet = add_Haifun(ds)

            '対象データの有無チェック
            If reccnt = 0 Then
                '選択ボタンを非活性
                Me.btnSelect.Enabled = False
                '処理終了
                Exit Sub
            Else
                '選択ボタンを活性
                Me.btnSelect.Enabled = True
            End If

            For index As Integer = 0 To ds2.Tables(RS).Rows.Count - 1
                dgvLIST.Rows.Add()
                dgvLIST.Rows(index).Cells(0).Value = _db.rmNullStr(ds2.Tables(RS).Rows(index)("処理ＩＤ"))          '処理ID
                dgvLIST.Rows(index).Cells(1).Value = _db.rmNullStr(ds2.Tables(RS).Rows(index)("業務名"))            '業務
                dgvLIST.Rows(index).Cells(2).Value = _db.rmNullStr(ds2.Tables(RS).Rows(index)("処理名"))            '処理名
                dgvLIST.Rows(index).Cells(3).Value = _db.rmNullStr(ds2.Tables(RS).Rows(index)("説明"))              '説明
                dgvLIST.Rows(index).Cells(4).Value = _db.rmNullStr(ds2.Tables(RS).Rows(index)("My前回操作日時"))    'My前回操作日時
                dgvLIST.Rows(index).Cells(5).Value = _db.rmNullStr(ds2.Tables(RS).Rows(index)("更新者"))            '操作者
                dgvLIST.Rows(index).Cells(6).Value = _db.rmNullStr(ds2.Tables(RS).Rows(index)("前回操作日時"))      '前回操作日時
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    Function strcheckMenu() As String
        '各チェックボックスのチェック状態でlike作成
        Dim checkMenu As String = ""
        If chkH01.Checked Then
            checkMenu += " m.処理ＩＤ like 'H01%' "
        End If
        checkMenu += " or "
        checkMenu += " m.処理ＩＤ like 'H08%' "

        checkMenu += " or "
        checkMenu += " m.処理ＩＤ like 'H09%' "

        checkMenu += " or "
        checkMenu += " m.処理ＩＤ like 'H10%' "

        checkMenu += " or "
        checkMenu += " m.処理ＩＤ like 'H11%' "

        checkMenu += " or "
        checkMenu += " m.処理ＩＤ like 'H12%' "

        checkMenu += " or "
        checkMenu += " m.処理ＩＤ like 'H13%' "

        If chkH02.Checked Then
            If checkMenu IsNot "" Then checkMenu += " or "
            checkMenu += " m.処理ＩＤ like 'H02%' "
        End If
        If chkH03.Checked Then
            If checkMenu IsNot "" Then checkMenu += " or "
            checkMenu += " m.処理ＩＤ like 'H03%' "
        End If
        If chkH04.Checked Then
            If checkMenu IsNot "" Then checkMenu += " or "
            checkMenu += " m.処理ＩＤ like 'H04%' "
        End If
        If chkH05.Checked Then
            If checkMenu IsNot "" Then checkMenu += " or "
            checkMenu += " m.処理ＩＤ like 'H05%' "
        End If
        If chkH06.Checked Then
            If checkMenu IsNot "" Then checkMenu += " or "
            checkMenu += " m.処理ＩＤ like 'H06%' "
        End If
        If chkH07.Checked Then
            If checkMenu IsNot "" Then checkMenu += " or "
            checkMenu += " m.処理ＩＤ like 'H07%' "
        End If
        If chkM01.Checked Then
            If checkMenu IsNot "" Then checkMenu += " or "
            checkMenu += " m.処理ＩＤ like 'M01%' "
        End If

        Dim getMenuSql As String = ""

        If checkMenu IsNot "" Then
            getMenuSql += "("
            getMenuSql += checkMenu
            getMenuSql += ")"
        End If
        Return getMenuSql
    End Function

    'メニュー選択処理
    Private Sub selectMenu()

        Dim idx As Integer

        '一覧選択行インデックスの取得
        For Each c As DataGridViewRow In dgvLIST.SelectedRows
            idx = c.Index
            Exit For
        Next c

        Dim selectID As String = dgvLIST.Rows(idx).Cells(0).Value   '選択処理ＩＤ

        Select Case selectID
            '-----------------------------------注文業務（H01）
            Case HAIFUN_ID               'ハイフン
                Exit Sub
            '-----------------------------------注文業務（H01）
            Case CommonConst.MENU_H0110  '見積登録
                Dim Status As String = CommonConst.STATUS_ADD
                Dim openForm As Form = Nothing
                openForm = New Quote(_msgHd, _db, _langHd, Me, , , Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H0120  '仕入単価入力
                Dim Status As String = CommonConst.STATUS_PRICE
                Dim openForm As Form = Nothing
                openForm = New QuoteList(_msgHd, _db, _langHd, Me, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H0130  '見積修正
                Dim Status As String = CommonConst.STATUS_EDIT
                Dim openForm As Form = Nothing
                openForm = New QuoteList(_msgHd, _db, _langHd, Me, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H0140  '見積複写
                Dim Status As String = CommonConst.STATUS_CLONE
                Dim openForm As Form = Nothing
                openForm = New QuoteList(_msgHd, _db, _langHd, Me, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H0150  '見積参照
                Dim Status As String = CommonConst.STATUS_VIEW
                Dim openForm As Form = Nothing
                openForm = New QuoteList(_msgHd, _db, _langHd, Me, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H0160  '見積取消
                Dim Status As String = CommonConst.STATUS_CANCEL
                Dim openForm As Form = Nothing
                openForm = New QuoteList(_msgHd, _db, _langHd, Me, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H0810  '受発注登録
                Dim Status As String = CommonConst.STATUS_ORDER_PURCHASE
                Dim openForm As Form = Nothing
                openForm = New QuoteList(_msgHd, _db, _langHd, Me, Status)
                openForm.Show()
                Me.Hide()
            '-----------------------------------受注業務（H02）
            Case CommonConst.MENU_H0210  '受注登録
                Dim Status As String = CommonConst.STATUS_ORDER_NEW
                Dim openForm As Form = Nothing
                openForm = New QuoteList(_msgHd, _db, _langHd, Me, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H0220  '受注編集
                Dim Status As String = CommonConst.STATUS_EDIT
                Dim openForm As Form = Nothing
                openForm = New OrderList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H0230  '受注複写
                Dim Status As String = CommonConst.STATUS_CLONE
                Dim openForm As Form = Nothing
                openForm = New OrderList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H0240  '受注取消
                Dim Status As String = CommonConst.STATUS_CANCEL
                Dim openForm As Form = Nothing
                openForm = New OrderList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H0250  '受注参照
                Dim Status As String = CommonConst.STATUS_VIEW
                Dim openForm As Form = Nothing
                openForm = New OrderList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H0260  '受注残一覧
                Dim Status As String = CommonConst.STATUS_VIEW
                Dim openForm As Form = Nothing
                openForm = New OrderRemainingList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H0270  '受注一覧
                Dim Status As String = CommonConst.STATUS_VIEW
                Dim openForm As Form = Nothing
                openForm = New JobOrderList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
           '-----------------------------------売上業務（H03）
            Case CommonConst.MENU_H0310  '売上登録
                Dim Status As String = CommonConst.STATUS_SALES
                Dim openForm As Form = Nothing
                openForm = New OrderList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H0320  '売上編集
                Dim Status As String = CommonConst.STATUS_SALES
                Dim openForm As Form = Nothing
                openForm = New OrderList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H0330  '売上取消
                Dim Status As String = CommonConst.STATUS_CANCEL
                Dim openForm As Form = Nothing
                openForm = New SalesList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H0340  '売上参照
                Dim Status As String = CommonConst.STATUS_VIEW
                Dim openForm As Form = Nothing
                openForm = New SalesList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H0350  '売上利益一覧
                Dim Status As String = CommonConst.STATUS_VIEW
                Dim openForm As Form = Nothing
                openForm = New SalesProfitList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H0360  '売上金・ＶＡＴ一覧
                Dim Status As String = CommonConst.STATUS_VIEW
                Dim openForm As Form = Nothing
                openForm = New SalesVATList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            '-----------------------------------出庫業務（H04）
            Case CommonConst.MENU_H0410  '出庫登録
                Dim Status As String = CommonConst.STATUS_GOODS_ISSUE
                Dim openForm As Form = Nothing
                openForm = New OrderList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H0420  '出庫編集
                Dim Status As String = CommonConst.STATUS_GOODS_ISSUE
                Dim openForm As Form = Nothing
                openForm = New OrderList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H0430  '出庫取消
                Dim Status As String = CommonConst.STATUS_CANCEL
                Dim openForm As Form = Nothing
                openForm = New GoodsIssueList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H0440  '出庫参照
                Dim Status As String = CommonConst.STATUS_VIEW
                Dim openForm As Form = Nothing
                openForm = New GoodsIssueList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            '-----------------------------------発注業務（H05）
            Case CommonConst.MENU_H0510  '発注登録
                Dim openForm As Form = Nothing
                openForm = New OrderingAdd(_msgHd, _db, _langHd, Me)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H0520  '発注編集
                Dim Status As String = CommonConst.STATUS_EDIT
                Dim openForm As Form = Nothing
                openForm = New OrderingList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H0530  '発注複写
                Dim Status As String = CommonConst.STATUS_CLONE
                Dim openForm As Form = Nothing
                openForm = New OrderingList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H0540  '発注取消
                Dim Status As String = CommonConst.STATUS_CANCEL
                Dim openForm As Form = Nothing
                openForm = New OrderingList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H0550  '発注参照
                Dim Status As String = CommonConst.STATUS_VIEW
                Dim openForm As Form = Nothing
                openForm = New OrderingList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            '-----------------------------------仕入業務（H05）
            Case CommonConst.MENU_H0610  '仕入登録
                Dim Status As String = CommonConst.STATUS_ORDING
                Dim openForm As Form = Nothing
                openForm = New OrderingList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H0620  '仕入編集
                Dim Status As String = CommonConst.STATUS_ORDING
                Dim openForm As Form = Nothing
                openForm = New OrderingList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H0630  '仕入取消
                Dim Status As String = CommonConst.STATUS_CANCEL
                Dim openForm As Form = Nothing
                openForm = New PurchaseList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H0640  '仕入参照
                Dim Status As String = CommonConst.STATUS_VIEW
                Dim openForm As Form = Nothing
                openForm = New PurchaseList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            '-----------------------------------入庫業務（H05）
            Case CommonConst.MENU_H0710  '入庫登録
                Dim Status As String = CommonConst.STATUS_RECEIPT
                Dim openForm As Form = Nothing
                openForm = New OrderingList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H0720  '入庫編集
                Dim Status As String = CommonConst.STATUS_RECEIPT
                Dim openForm As Form = Nothing
                openForm = New OrderingList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H0730  '入庫取消
                Dim Status As String = CommonConst.STATUS_CANCEL
                Dim openForm As Form = Nothing
                openForm = New ReceiptList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H0740  '入庫参照
                Dim Status As String = CommonConst.STATUS_VIEW
                Dim openForm As Form = Nothing
                openForm = New ReceiptList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            '-----------------------------------請求業務（H05）
            Case CommonConst.MENU_H0910  '請求登録
                Dim Status As String = CommonConst.STATUS_BILL
                Dim openForm As Form = Nothing
                openForm = New OrderList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H0920  '請求編集
                Dim Status As String = CommonConst.STATUS_BILL
                Dim openForm As Form = Nothing
                openForm = New OrderList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H0930  '請求取消
                Dim Status As String = CommonConst.STATUS_CANCEL
                Dim openForm As Form = Nothing
                openForm = New BillingList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H0940  '請求参照
                Dim Status As String = CommonConst.STATUS_VIEW
                Dim openForm As Form = Nothing
                openForm = New BillingList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H0950  '請求計算
                Dim openForm As Form = Nothing
                openForm = New CustomerList(_msgHd, _db, _langHd)
                openForm.Show()
                Me.Hide()
                '-----------------------------------入金業務（H05）
            Case CommonConst.MENU_H1010  '入金登録
                Dim openForm As Form = Nothing
                openForm = New DepositList(_msgHd, _db, _langHd)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H1020  '入金取消
                Dim openForm As Form = Nothing
                Dim Status As String = CommonConst.STATUS_CANCEL
                openForm = New DepositDetailList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H1030  '入金参照
                Dim openForm As Form = Nothing
                Dim Status As String = CommonConst.STATUS_VIEW
                openForm = New DepositDetailList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            '-----------------------------------売掛業務（H05）
            Case CommonConst.MENU_H1110  '売掛登録
                Dim Status As String = CommonConst.STATUS_AP
                Dim openForm As Form = Nothing
                openForm = New OrderingList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H1120  '売掛編集
                Dim Status As String = CommonConst.STATUS_AP
                Dim openForm As Form = Nothing
                openForm = New OrderingList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H1130  '売掛取消
                Dim Status As String = CommonConst.STATUS_CANCEL
                Dim openForm As Form = Nothing
                openForm = New AccountsPayableList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H1140  '売掛参照
                Dim Status As String = CommonConst.STATUS_VIEW
                Dim openForm As Form = Nothing
                openForm = New AccountsPayableList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            '-----------------------------------入金業務（H05）
            Case CommonConst.MENU_H1210  '支払登録
                Dim openForm As Form = Nothing
                openForm = New PaymentList(_msgHd, _db, _langHd)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H1220  '支払取消
                Dim openForm As Form = Nothing
                Dim Status As String = CommonConst.STATUS_CANCEL
                openForm = New PaidList(_msgHd, _db, _langHd, Status)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H1230  '支払登録
                Dim openForm As Form = Nothing
                openForm = New PaidList(_msgHd, _db, _langHd)
                openForm.Show()
                Me.Hide()
            '-----------------------------------締処理業務（H05）
            Case CommonConst.MENU_H1310  '締処理ログ参照
                Dim openForm As Form = Nothing
                openForm = New ClosingLog(_msgHd, _db, _langHd)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_H1320  '仕訳処理
                Dim openForm As Form = Nothing
                openForm = New Shiwake(_msgHd, _db, _langHd)
                openForm.Show()
                Me.Hide()
            '-----------------------------------マスタ管理（M01）
            Case CommonConst.MENU_M0110    '汎用マスタ一覧
                Dim openForm As Form = Nothing
                openForm = New MstHanyou(_msgHd, _db, _langHd)
                openForm.Show()
                Me.Hide()

            Case CommonConst.MENU_M0120    '取引先マスタ一覧
                Dim openForm As Form = Nothing
                openForm = New MstCustomer(_msgHd, _db, _langHd)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_M0130    '仕入先マスタ一覧
                Dim openForm As Form = Nothing
                openForm = New MstSupplier(_msgHd, _db, _langHd)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_M0140    '会社マスタ一覧
                Dim openForm As Form = Nothing
                openForm = New MstCompany(_msgHd, _db, _langHd)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_M0150    'ユーザマスタ一覧
                Dim openForm As Form = Nothing
                openForm = New MstUser(_msgHd, _db, _langHd)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_M0160    '言語マスタ一覧
                Dim openForm As Form = Nothing
                openForm = New MstLanguage(_msgHd, _db, _langHd)
                openForm.Show()
                Me.Hide()
            Case CommonConst.MENU_M0170    '在庫マスタ一覧
                Dim openForm As Form = Nothing
                openForm = New StockList(_msgHd, _db, _langHd)
                openForm.Show()
                Me.Hide()
                '-----------------------------------
            Case CommonConst.MENU_M0180    '勘定科目マスタ一覧
                Dim openForm As Form = Nothing
                openForm = New MstAccount(_msgHd, _db, _langHd)
                openForm.Show()
                Me.Hide()
                '-----------------------------------
            Case Else                      'マスタ設定されていない場合
                Exit Sub
        End Select

        Me.Hide()   ' 自分は隠れる

    End Sub

    '-------------------------------------------------------------------------------
    '　業務IDの間にハイフンレコード追加する
    '-------------------------------------------------------------------------------
    Private Function add_Haifun(ByVal paraDs As DataSet) As DataSet

        Dim ds As New DataSet
        Dim dsIdx As Integer = 0
        Dim dt As DataTable = New DataTable(RS)

        '列追加
        dt.Columns.Add("会社コード", GetType(String))
        dt.Columns.Add("処理ＩＤ", GetType(String))
        dt.Columns.Add("処理名", GetType(String))
        dt.Columns.Add("業務ＩＤ", GetType(String))
        dt.Columns.Add("業務名", GetType(String))
        dt.Columns.Add("表示順", GetType(Integer))
        dt.Columns.Add("説明", GetType(String))
        dt.Columns.Add("削除フラグ", GetType(String))
        dt.Columns.Add("My前回操作日時", GetType(String))
        dt.Columns.Add("更新者", GetType(String))
        dt.Columns.Add("前回操作日時", GetType(String))

        Dim sGyomuId As String = ""
        For index As Integer = 0 To paraDs.Tables(RS).Rows.Count - 1
            Dim newRow As DataRow = dt.NewRow

            '請求先コードがkeyブレイクで債権残高(t30)レコードがない場合
            If Not sGyomuId.Equals(_db.rmNullStr(paraDs.Tables(RS).Rows(index)("業務ＩＤ"))) AndAlso
               index > 0 Then

                newRow("会社コード") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("会社コード"))
                newRow("処理ＩＤ") = HAIFUN_ID
                newRow("業務名") = HAIFUN_GYOMU1
                newRow("処理名") = HAIFUN_SHORI
                newRow("業務ＩＤ") = "H00"
                newRow("表示順") = 0
                newRow("説明") = HAIFUN_SETUMEI
                newRow("削除フラグ") = ""
                newRow("My前回操作日時") = HAIFUN_MYSOUSANICHIJI
                newRow("更新者") = HAIFUN_SOUSA
                newRow("前回操作日時") = HAIFUN_ZENKAI
                '追加
                dt.Rows.Add(newRow)
                newRow = dt.NewRow
            End If
            'keyブレイク用
            sGyomuId = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("業務ＩＤ"))

            If frmC01F10_Login.loginValue.Language = "ENG" Then
                newRow("会社コード") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("会社コード"))
                newRow("処理ＩＤ") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("処理ＩＤ"))
                newRow("処理名") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("英語用処理名"))
                newRow("業務ＩＤ") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("業務ＩＤ"))
                newRow("業務名") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("英語用業務名"))
                newRow("表示順") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("表示順"))
                newRow("説明") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("英語用説明"))
                newRow("削除フラグ") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("削除フラグ"))
                newRow("My前回操作日時") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("My前回操作日時"))
                newRow("更新者") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("更新者"))
                newRow("前回操作日時") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("前回操作日時"))
            Else
                newRow("会社コード") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("会社コード"))
                newRow("処理ＩＤ") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("処理ＩＤ"))
                newRow("処理名") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("処理名"))
                newRow("業務ＩＤ") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("業務ＩＤ"))
                newRow("業務名") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("業務名"))
                newRow("表示順") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("表示順"))
                newRow("説明") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("説明"))
                newRow("削除フラグ") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("削除フラグ"))
                newRow("My前回操作日時") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("My前回操作日時"))
                newRow("更新者") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("更新者"))
                newRow("前回操作日時") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("前回操作日時"))
            End If


            dt.Rows.Add(newRow)
        Next

        'DataSetにdtを追加
        ds.Tables.Add(dt)

        Return ds

    End Function

    Private Sub cmdExit_Click_1(sender As Object, e As EventArgs) Handles cmdExit.Click
        Dim intRet As Integer
        intRet = _msgHd.dspMSG("SystemExit", frmC01F10_Login.loginValue.Language)
        If intRet = vbOK Then
            Application.Exit()
        End If
    End Sub
End Class