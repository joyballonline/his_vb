Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports System.Globalization

Public Class ClosingAdmin
    Inherits System.Windows.Forms.Form

    '------------------------------------------------------------------------------------------------------
    'メンバー定数宣言
    '------------------------------------------------------------------------------------------------------
    'PG制御文字 
    Private Const N As String = ControlChars.NewLine            '改行文字
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
    Private _parentForm As Form
    'Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ
    Private CompanyCode As String = ""
    Private OrderNo As String()
    Private OrderStatus As String = ""


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
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler,
                   ByRef prmRefDbHd As UtilDBIf,
                   ByRef prmRefLang As UtilLangHandler,
                   ByRef prmRefForm As Form,
                   Optional ByRef prmRefStatus As String = "")
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _parentForm = prmRefForm
        OrderStatus = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True
        'DgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.DisplayedCells
    End Sub

    '画面表示時
    Private Sub MstHanyoue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""


        '締処理日
        Sql = "SELECT 今回締日"
        Sql += " FROM m01_company"
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

        Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)


        '今回締日を判定
        Dim strShime As String = Convert.ToString(ds.Tables(RS).Rows(0)("今回締日"))
        If strShime = vbNullString Then
            '今回締日がテーブルに登録されていない場合は前月末の日付をセットする
            Dim dtmShime As Date = DateSerial(Now.Year, Now.Month, 1)
            dtmSime.Text = DateAdd("d", -1, dtmShime)
        Else
            'テーブルの値をセットする
            dtmSime.Text = ds.Tables(RS).Rows(0)("今回締日")
        End If


        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then

            LblMode.Text = "AdminMode"
            LblShime.Text = "Closing Date"

            BtnRegistration.Text = "Closing"
            BtnBack.Text = "Back"
            BtnOutput.Text = "JournalOutput"

        End If

    End Sub

    '登録ボタン押下時
    Private Sub BtnRegistration_Click(sender As Object, e As EventArgs) Handles BtnRegistration.Click

        Dim frmNew As New ClosingLog(_msgHd, _db, _langHd, Me)
        frmNew.Closing_btn(1, dtmSime.Value)

        frmNew.Refresh()

        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()

    End Sub


    '戻るボタン押下時
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    Private Sub BtnOutput_Click(sender As Object, e As EventArgs) Handles BtnOutput.Click

        Dim frmNew As New ClosingLog(_msgHd, _db, _langHd, Me)
        Cursor.Current = Cursors.WaitCursor
        _db.executeDB("truncate table t67_swkhd")
        frmNew.getShiwakeData(dtmSime.Value.ToString("yyyyMM"))
        '(1, dtmSime.Value)
        frmNew.SiwakeConvertDataTableToCsv()
        'frmNew.Refresh()
        Cursor.Current = Cursors.Default

        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()

    End Sub
End Class