'===============================================================================
'
'　カネキ吉田商店様
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
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示

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
        Me.TableLayoutPanel30 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.chkM01 = New System.Windows.Forms.CheckBox()
        Me.chkG01 = New System.Windows.Forms.CheckBox()
        Me.chkH07 = New System.Windows.Forms.CheckBox()
        Me.chkH06 = New System.Windows.Forms.CheckBox()
        Me.chkH05 = New System.Windows.Forms.CheckBox()
        Me.chkH01 = New System.Windows.Forms.CheckBox()
        Me.TableLayoutPanel28 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel23 = New System.Windows.Forms.TableLayoutPanel()
        Me.chkH03 = New System.Windows.Forms.CheckBox()
        Me.TableLayoutPanel27 = New System.Windows.Forms.TableLayoutPanel()
        Me.dgvLIST = New System.Windows.Forms.DataGridView()
        Me.処理ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.業務 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.処理名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.説明 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.My前回操作日時 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.操作者 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.前回操作日時 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TableLayoutPanel30.SuspendLayout()
        Me.TableLayoutPanel28.SuspendLayout()
        Me.TableLayoutPanel23.SuspendLayout()
        Me.TableLayoutPanel27.SuspendLayout()
        CType(Me.dgvLIST, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnSelect
        '
        Me.btnSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSelect.Enabled = False
        Me.btnSelect.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSelect.Location = New System.Drawing.Point(98, 9)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(102, 33)
        Me.btnSelect.TabIndex = 4
        Me.btnSelect.Text = "選択(&G)"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel30
        '
        Me.TableLayoutPanel30.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel30.ColumnCount = 3
        Me.TableLayoutPanel30.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.07454!))
        Me.TableLayoutPanel30.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.92546!))
        Me.TableLayoutPanel30.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.TableLayoutPanel30.Controls.Add(Me.cmdExit, 0, 1)
        Me.TableLayoutPanel30.Controls.Add(Me.btnSelect, 0, 1)
        Me.TableLayoutPanel30.Location = New System.Drawing.Point(593, 488)
        Me.TableLayoutPanel30.Name = "TableLayoutPanel30"
        Me.TableLayoutPanel30.RowCount = 2
        Me.TableLayoutPanel30.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.25424!))
        Me.TableLayoutPanel30.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 84.74577!))
        Me.TableLayoutPanel30.Size = New System.Drawing.Size(388, 45)
        Me.TableLayoutPanel30.TabIndex = 1
        '
        'cmdExit
        '
        Me.cmdExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdExit.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdExit.Location = New System.Drawing.Point(245, 9)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(102, 33)
        Me.cmdExit.TabIndex = 5
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
        Me.chkM01.Location = New System.Drawing.Point(777, 4)
        Me.chkM01.Name = "chkM01"
        Me.chkM01.Size = New System.Drawing.Size(88, 19)
        Me.chkM01.TabIndex = 6
        Me.chkM01.Text = "マスタ保守"
        Me.chkM01.UseVisualStyleBackColor = True
        '
        'chkG01
        '
        Me.chkG01.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkG01.AutoSize = True
        Me.chkG01.Checked = True
        Me.chkG01.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkG01.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkG01.Location = New System.Drawing.Point(635, 4)
        Me.chkG01.Name = "chkG01"
        Me.chkG01.Size = New System.Drawing.Size(116, 19)
        Me.chkG01.TabIndex = 5
        Me.chkG01.Text = "原価管理業務"
        Me.chkG01.UseVisualStyleBackColor = True
        '
        'chkH07
        '
        Me.chkH07.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkH07.AutoSize = True
        Me.chkH07.Checked = True
        Me.chkH07.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkH07.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkH07.Location = New System.Drawing.Point(524, 4)
        Me.chkH07.Name = "chkH07"
        Me.chkH07.Size = New System.Drawing.Size(86, 19)
        Me.chkH07.TabIndex = 4
        Me.chkH07.Text = "支払業務"
        Me.chkH07.UseVisualStyleBackColor = True
        '
        'chkH06
        '
        Me.chkH06.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkH06.AutoSize = True
        Me.chkH06.Checked = True
        Me.chkH06.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkH06.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkH06.Location = New System.Drawing.Point(398, 4)
        Me.chkH06.Name = "chkH06"
        Me.chkH06.Size = New System.Drawing.Size(86, 19)
        Me.chkH06.TabIndex = 3
        Me.chkH06.Text = "仕入業務"
        Me.chkH06.UseVisualStyleBackColor = True
        '
        'chkH05
        '
        Me.chkH05.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkH05.AutoSize = True
        Me.chkH05.Checked = True
        Me.chkH05.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkH05.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkH05.Location = New System.Drawing.Point(272, 4)
        Me.chkH05.Name = "chkH05"
        Me.chkH05.Size = New System.Drawing.Size(86, 19)
        Me.chkH05.TabIndex = 2
        Me.chkH05.Text = "入金業務"
        Me.chkH05.UseVisualStyleBackColor = True
        '
        'chkH01
        '
        Me.chkH01.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkH01.AutoSize = True
        Me.chkH01.Checked = True
        Me.chkH01.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkH01.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkH01.Location = New System.Drawing.Point(20, 4)
        Me.chkH01.Name = "chkH01"
        Me.chkH01.Size = New System.Drawing.Size(86, 19)
        Me.chkH01.TabIndex = 0
        Me.chkH01.Text = "注文業務"
        Me.chkH01.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel28
        '
        Me.TableLayoutPanel28.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TableLayoutPanel28.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TableLayoutPanel28.ColumnCount = 1
        Me.TableLayoutPanel28.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel28.Controls.Add(Me.TableLayoutPanel23, 0, 0)
        Me.TableLayoutPanel28.Location = New System.Drawing.Point(3, 8)
        Me.TableLayoutPanel28.Name = "TableLayoutPanel28"
        Me.TableLayoutPanel28.RowCount = 1
        Me.TableLayoutPanel28.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel28.Size = New System.Drawing.Size(899, 36)
        Me.TableLayoutPanel28.TabIndex = 2
        '
        'TableLayoutPanel23
        '
        Me.TableLayoutPanel23.ColumnCount = 7
        Me.TableLayoutPanel23.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel23.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel23.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel23.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel23.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel23.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel23.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel23.Controls.Add(Me.chkH03, 1, 0)
        Me.TableLayoutPanel23.Controls.Add(Me.chkM01, 6, 0)
        Me.TableLayoutPanel23.Controls.Add(Me.chkH01, 0, 0)
        Me.TableLayoutPanel23.Controls.Add(Me.chkG01, 5, 0)
        Me.TableLayoutPanel23.Controls.Add(Me.chkH05, 2, 0)
        Me.TableLayoutPanel23.Controls.Add(Me.chkH07, 4, 0)
        Me.TableLayoutPanel23.Controls.Add(Me.chkH06, 3, 0)
        Me.TableLayoutPanel23.Location = New System.Drawing.Point(4, 4)
        Me.TableLayoutPanel23.Name = "TableLayoutPanel23"
        Me.TableLayoutPanel23.RowCount = 1
        Me.TableLayoutPanel23.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel23.Size = New System.Drawing.Size(886, 28)
        Me.TableLayoutPanel23.TabIndex = 0
        '
        'chkH03
        '
        Me.chkH03.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkH03.AutoSize = True
        Me.chkH03.Checked = True
        Me.chkH03.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkH03.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkH03.Location = New System.Drawing.Point(131, 4)
        Me.chkH03.Name = "chkH03"
        Me.chkH03.Size = New System.Drawing.Size(116, 19)
        Me.chkH03.TabIndex = 1
        Me.chkH03.Text = "委託売上業務"
        Me.chkH03.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel27
        '
        Me.TableLayoutPanel27.ColumnCount = 1
        Me.TableLayoutPanel27.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel27.Controls.Add(Me.TableLayoutPanel30, 0, 2)
        Me.TableLayoutPanel27.Controls.Add(Me.dgvLIST, 0, 1)
        Me.TableLayoutPanel27.Controls.Add(Me.TableLayoutPanel28, 0, 0)
        Me.TableLayoutPanel27.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel27.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel27.Name = "TableLayoutPanel27"
        Me.TableLayoutPanel27.RowCount = 3
        Me.TableLayoutPanel27.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel27.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.78358!))
        Me.TableLayoutPanel27.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.328359!))
        Me.TableLayoutPanel27.Size = New System.Drawing.Size(984, 536)
        Me.TableLayoutPanel27.TabIndex = 1
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
        Me.dgvLIST.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvLIST.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvLIST.Location = New System.Drawing.Point(3, 56)
        Me.dgvLIST.MultiSelect = False
        Me.dgvLIST.Name = "dgvLIST"
        Me.dgvLIST.RowHeadersVisible = False
        Me.dgvLIST.RowHeadersWidth = 25
        Me.dgvLIST.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvLIST.Size = New System.Drawing.Size(978, 426)
        Me.dgvLIST.TabIndex = 0
        '
        '処理ID
        '
        DataGridViewCellStyle2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.処理ID.DefaultCellStyle = DataGridViewCellStyle2
        Me.処理ID.HeaderText = "処理ID"
        Me.処理ID.Name = "処理ID"
        Me.処理ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.処理ID.Visible = False
        Me.処理ID.Width = 130
        '
        '業務
        '
        DataGridViewCellStyle3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.業務.DefaultCellStyle = DataGridViewCellStyle3
        Me.業務.HeaderText = "業務"
        Me.業務.Name = "業務"
        Me.業務.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        '処理名
        '
        DataGridViewCellStyle4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.処理名.DefaultCellStyle = DataGridViewCellStyle4
        Me.処理名.HeaderText = "処理名"
        Me.処理名.Name = "処理名"
        Me.処理名.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.処理名.Width = 140
        '
        '説明
        '
        DataGridViewCellStyle5.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.説明.DefaultCellStyle = DataGridViewCellStyle5
        Me.説明.HeaderText = "説明"
        Me.説明.Name = "説明"
        Me.説明.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.説明.Width = 355
        '
        'My前回操作日時
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.My前回操作日時.DefaultCellStyle = DataGridViewCellStyle6
        Me.My前回操作日時.HeaderText = " My前回操作日時"
        Me.My前回操作日時.Name = "My前回操作日時"
        Me.My前回操作日時.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.My前回操作日時.Width = 135
        '
        '操作者
        '
        DataGridViewCellStyle7.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.操作者.DefaultCellStyle = DataGridViewCellStyle7
        Me.操作者.HeaderText = "　操作者"
        Me.操作者.Name = "操作者"
        Me.操作者.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.操作者.Width = 90
        '
        '前回操作日時
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle8.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.前回操作日時.DefaultCellStyle = DataGridViewCellStyle8
        Me.前回操作日時.HeaderText = "前回操作日時"
        Me.前回操作日時.Name = "前回操作日時"
        Me.前回操作日時.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.前回操作日時.Width = 135
        '
        'frmC01F30_Menu
        '
        Me.ClientSize = New System.Drawing.Size(984, 536)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel27)
        Me.Name = "frmC01F30_Menu"
        Me.Text = "処理メニュー（C01F30）"
        Me.TableLayoutPanel30.ResumeLayout(False)
        Me.TableLayoutPanel28.ResumeLayout(False)
        Me.TableLayoutPanel23.ResumeLayout(False)
        Me.TableLayoutPanel23.PerformLayout()
        Me.TableLayoutPanel27.ResumeLayout(False)
        CType(Me.dgvLIST, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

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

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Dim intRet As Integer
        intRet = _msgHd.dspMSG("SystemExit")
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
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
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
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
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
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    Sub Check_Clicked(sender As Object, e As EventArgs) _
        Handles chkH01.CheckedChanged, chkH03.CheckedChanged, chkH05.CheckedChanged, chkH06.CheckedChanged, chkH07.CheckedChanged, chkG01.CheckedChanged, chkM01.CheckedChanged
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
            strSql = strSql & likeSql
            strSql = strSql & " order by m.表示順 "

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
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    Function strcheckMenu() As String
        '各チェックボックスのチェック状態でlike作成
        Dim checkMenu As String = ""
        If chkH01.Checked Then
            checkMenu += " m.処理ＩＤ like 'H01%' "
        End If
        If chkH03.Checked Then
            If checkMenu IsNot "" Then checkMenu += " or "
            checkMenu += " m.処理ＩＤ like 'H03%' "
        End If
        If chkH05.Checked Then
            If checkMenu IsNot "" Then checkMenu += " or "
            checkMenu += " m.処理ＩＤ like 'H05%' "
        End If
        If chkH06.Checked Then
            If checkMenu IsNot "" Then checkMenu += " or "
            checkMenu += " m.処理ＩＤ like 'H06%' or "
            checkMenu += " m.処理ＩＤ like 'H11%' "
        End If
        If chkH07.Checked Then
            If checkMenu IsNot "" Then checkMenu += " or "
            checkMenu += " m.処理ＩＤ like 'H07%' "
        End If
        If chkG01.Checked Then
            If checkMenu IsNot "" Then checkMenu += " or "
            checkMenu += " m.処理ＩＤ like 'G01%' "
        End If
        If chkH01.Checked Then
            If checkMenu IsNot "" Then checkMenu += " or "
            checkMenu += " m.処理ＩＤ like 'H10%' "
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

        'Select Case selectID
        '    '-----------------------------------注文業務（H01）
        '    Case HAIFUN_ID               'ハイフン
        '        Exit Sub
        '    '-----------------------------------注文業務（H01）
        '    Case CommonConst.MENU_H0110  '注文登録
        '        Dim openForm As Form = Nothing
        '        openForm = New frmH01F10_SyoriSentaku(_msgHd, _db, selectID, Me)   '処理選択
        '        openForm.Show()
        '    Case CommonConst.MENU_H0120  '注文変更
        '        '注文変更フォームオープン
        '        Dim openForm As New frmH01F50_SelectRireki(_msgHd, _db, selectID, Me, CommonConst.MODE_EditStatus)
        '        openForm.Show()
        '    Case CommonConst.MENU_H0130  '注文取消
        '        '注文取消フォームオープン
        '        Dim openForm As New frmH01F50_SelectRireki(_msgHd, _db, selectID, Me, CommonConst.MODE_CancelStatus)
        '        openForm.Show()
        '    Case CommonConst.MENU_H0140  '注文照会
        '        '注文照会フォームオープン
        '        Dim openForm As New frmH01F50_SelectRireki(_msgHd, _db, selectID, Me, CommonConst.MODE_InquiryStatus)
        '        openForm.Show()
        '    '-----------------------------------注文帳照会（H02）
        '    Case CommonConst.MENU_H0210  '注文帳照会
        '        '注文帳照会フォームオープン
        '    '-----------------------------------委託売上業務（H03）
        '    Case CommonConst.MENU_H0310  '委託売上登録
        '        Dim openForm As Form = Nothing
        '        openForm = New frmH03F10_SelectItakuList(_msgHd, _db, selectID, Me, CommonConst.MODE_ADDNEW)
        '        openForm.Show()
        '    Case CommonConst.MENU_H0320  '委託売上変更
        '        Dim openForm As Form = Nothing
        '        openForm = New frmH03F30_SelectItakuList(_msgHd, _db, selectID, Me, CommonConst.MODE_EditStatus)
        '        openForm.Show()
        '    Case CommonConst.MENU_H0330  '委託売上取消
        '        Dim openForm As Form = Nothing
        '        openForm = New frmH03F30_SelectItakuList(_msgHd, _db, selectID, Me, CommonConst.MODE_CancelStatus)
        '        openForm.Show()
        '    Case CommonConst.MENU_H0340  '委託売上照会
        '        Dim openForm As Form = Nothing
        '        openForm = New frmH03F30_SelectItakuList(_msgHd, _db, selectID, Me, CommonConst.MODE_InquiryStatus)
        '        openForm.Show()
        '    '-----------------------------------請求書発行業務（H04）
        '    'Case CommonConst.MENU_H0410  '請求書発行
        '    '    '請求書発行フォームオープン
        '    ''-----------------------------------入金業務（H05）
        '    Case CommonConst.MENU_H0510  '入金登録
        '        '入金登録フォームオープン
        '        Dim openForm As Form = Nothing
        '        openForm = New frmH05F20_Nyukin(_msgHd, _db, selectID, Me, CommonConst.MODE_ADDNEW)
        '        openForm.Show()

        '    Case CommonConst.MENU_H0520  '入金変更
        '        '入金変更フォームオープン
        '        Dim openForm As Form = Nothing
        '        openForm = New frmH05F10_NyukinList(_msgHd, _db, selectID, Me, CommonConst.MODE_EditStatus)
        '        openForm.Show()

        '    Case CommonConst.MENU_H0530  '入金取消
        '        '入金取消フォームオープン
        '        Dim openForm As Form = Nothing
        '        openForm = New frmH05F10_NyukinList(_msgHd, _db, selectID, Me, CommonConst.MODE_CancelStatus)
        '        openForm.Show()

        '    Case CommonConst.MENU_H0540  '入金照会
        '        '入金照会フォームオープン
        '        Dim openForm As Form = Nothing
        '        openForm = New frmH05F10_NyukinList(_msgHd, _db, selectID, Me, CommonConst.MODE_InquiryStatus)
        '        openForm.Show()
        '    '-----------------------------------仕入業務（H06）
        '    Case CommonConst.MENU_H0610  '仕入登録
        '        '仕入登録フォームオープン
        '        Dim openForm As Form = Nothing
        '        openForm = New frmH06F20_Shiire(_msgHd, _db, selectID, CommonConst.STARTUPID_MENU, Me)
        '        openForm.Show()

        '    Case CommonConst.MENU_H0620  '仕入変更
        '        '仕入一覧フォームオープン
        '        Dim openForm As Form = Nothing
        '        openForm = New frmH06F10_SelectShiire(_msgHd, _db, selectID, CommonConst.STARTUPID_MENU, Me)
        '        openForm.Show()

        '    Case CommonConst.MENU_H0630  '仕入取消
        '        '仕入一覧フォームオープン
        '        Dim openForm As Form = Nothing
        '        openForm = New frmH06F10_SelectShiire(_msgHd, _db, selectID, CommonConst.STARTUPID_MENU, Me)
        '        openForm.Show()

        '    Case CommonConst.MENU_H0640  '仕入照会
        '        '仕入一覧フォームオープン
        '        Dim openForm As Form = Nothing
        '        openForm = New frmH06F10_SelectShiire(_msgHd, _db, selectID, CommonConst.STARTUPID_MENU, Me)
        '        openForm.Show()

        '    '-----------------------------------支払業務（H07）
        '    'Case CommonConst.MENU_H0710  '支払登録
        '    '    '支払登録フォームオープン

        '    'Case CommonConst.MENU_H0720  '支払変更
        '    '    '支払変更フォームオープン

        '    'Case CommonConst.MENU_H0730  '支払取消
        '    '    '支払取消フォームオープン

        '    'Case CommonConst.MENU_H0740  '支払照会
        '        '支払照会フォームオープン

        '    '-----------------------------------注文明細表（H10）
        '    Case CommonConst.MENU_H1001  '注文明細表
        '        Dim openForm As Form = Nothing
        '        openForm = New frmH10F01_ChumonList(_msgHd, _db, selectID, CommonConst.STARTUPID_MENU, Me)   '処理選択
        '        openForm.Show()

        '    Case CommonConst.MENU_H1002  '売上未計上一覧表
        '        Dim openForm As Form = Nothing
        '        openForm = New frmH10F02_MikeijyoList(_msgHd, _db, selectID, CommonConst.STARTUPID_MENU, Me)   '処理選択
        '        openForm.Show()

        '    Case CommonConst.MENU_H1003  '売掛金一覧表
        '        Dim openForm As Form = Nothing
        '        openForm = New frmH10F03_UrikakeKinList(_msgHd, _db, selectID, CommonConst.STARTUPID_MENU, Me)   '処理選択
        '        openForm.Show()

        '    Case CommonConst.MENU_H1004  '得意先元帳
        '        Dim openForm As Form = Nothing
        '        openForm = New frmH10F04_TokuisakiMotoList(_msgHd, _db, selectID, CommonConst.STARTUPID_MENU, Me)   '処理選択
        '        openForm.Show()

        '    Case CommonConst.MENU_H1005  '出荷数一覧表
        '        Dim openForm As Form = Nothing
        '        openForm = New frmH10F05_ShukkaSuList(_msgHd, _db, selectID, CommonConst.STARTUPID_MENU, Me)   '処理選択
        '        openForm.Show()

        '    '-----------------------------------仕入明細表（H11）
        '    Case CommonConst.MENU_H1101  '注文明細表
        '        Dim openForm As Form = Nothing
        '        openForm = New frmH11F01_ShiireList(_msgHd, _db, selectID, CommonConst.STARTUPID_MENU, Me)   '処理選択
        '        openForm.Show()

        '    Case CommonConst.MENU_H1102  '買掛金一覧表
        '        Dim openForm As Form = Nothing
        '        openForm = New frmH11F02_KaikakeList(_msgHd, _db, selectID, CommonConst.STARTUPID_MENU, Me)   '処理選択
        '        openForm.Show()

        '    Case CommonConst.MENU_H1103  '仕入先元帳
        '        Dim openForm As Form = Nothing
        '        openForm = New frmH11F03_SiiresakiMotoList(_msgHd, _db, selectID, CommonConst.STARTUPID_MENU, Me)   '処理選択
        '        openForm.Show()

        '    Case CommonConst.MENU_H1121  '仕入総括表
        '        Dim openForm As Form = Nothing
        '        openForm = New frmH11F21_ShiireSokatuList(_msgHd, _db, selectID, CommonConst.STARTUPID_MENU, Me)   '処理選択
        '        openForm.Show()

        '        '-----------------------------------
        '    Case CommonConst.MENU_M0170    '汎用マスタ一覧
        '        Dim openForm As Form = Nothing
        '        openForm = New frmM70F10_HanyoList(_msgHd, _db, selectID, Me)   '処理選択
        '        openForm.Show()

        '    Case CommonConst.MENU_M0120    '取引先マスタ一覧
        '        Dim openForm As Form = Nothing
        '        openForm = New frmM20F10_TorihikisakiList(_msgHd, _db, selectID, Me)   '処理選択
        '        openForm.Show()

        '    Case CommonConst.MENU_M0130    '商品マスタ一覧
        '        Dim openForm As Form = Nothing
        '        openForm = New frmM30F10_ShohinList(_msgHd, _db, selectID, Me)   '処理選択
        '        openForm.Show()

        '    Case CommonConst.MENU_M0110    'ユーザーマスタ一覧
        '        Dim openForm As Form = Nothing
        '        openForm = New frmM10F10_UserList(_msgHd, _db, selectID, Me)   '処理選択
        '        openForm.Show()

        '    Case CommonConst.MENU_M0140    '販売単価マスタ一覧
        '        Dim openForm As Form = Nothing
        '        openForm = New frmM40F10_HanbaiTankaList(_msgHd, _db, selectID, Me)   '処理選択
        '        openForm.Show()

        '    Case CommonConst.MENU_M0150    '仕入単価マスタ一覧
        '        Dim openForm As Form = Nothing
        '        openForm = New frmM50F10_SiireTankaList(_msgHd, _db, selectID, Me)   '処理選択
        '        openForm.Show()

        '    Case CommonConst.MENU_M0180    '消費税マスタ一覧
        '        Dim openForm As Form = Nothing
        '        openForm = New frmM80F10_ShohizeiList(_msgHd, _db, selectID, Me)   '処理選択
        '        openForm.Show()

        '    Case "M0199"
        '        Dim openForm As Form = Nothing
        '        openForm = New ReportSample(_msgHd, _db)
        '        openForm.Show()

        '        '-----------------------------------
        '    Case Else                      'マスタ設定されていない場合
        '        Exit Sub
        'End Select

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

            dt.Rows.Add(newRow)
        Next

        'DataSetにdtを追加
        ds.Tables.Add(dt)

        Return ds

    End Function


End Class