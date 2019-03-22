Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class SalesSearch
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
    Private _parentForm As Form
    Private _status As String
    Private _companyCode As String = frmC01F10_Login.loginValue.BumonCD
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
        _status = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True

    End Sub

    Private Sub UserMaintenance_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblString.Text = "String"
            BtnSearch.Text = "Search"
            BtnSelect.Text = "Select"
            BtnBack.Text = "Back"

            '英語用見出し
            DgvUser.Columns("ユーザID").HeaderText = "UserID"
            DgvUser.Columns("氏名").HeaderText = "Name"
            DgvUser.Columns("略名").HeaderText = "ShortName"
            DgvUser.Columns("備考").HeaderText = "Remarks"
            DgvUser.Columns("権限").HeaderText = "Authority"
            DgvUser.Columns("言語").HeaderText = "Language"
            DgvUser.Columns("更新者").HeaderText = "ModifiedBy"
            DgvUser.Columns("更新日").HeaderText = "UpdateDate"

        End If

        setList() '一覧再表示
    End Sub

    Private Sub setList()
        DgvUser.Rows.Clear() '一覧クリア

        Dim Sql As String = ""
        Try
            Sql += "SELECT * FROM public.m02_user"
            Sql += " WHERE "
            Sql += "会社コード ='" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " and (ユーザＩＤ  ILIKE '%" & UtilClass.escapeSql(Search.Text) & "%'"
            Sql += " OR 氏名  ILIKE '%" & UtilClass.escapeSql(Search.Text) & "%'"
            Sql += " OR 略名  ILIKE '%" & UtilClass.escapeSql(Search.Text) & "%'"
            Sql += " OR 備考  ILIKE '%" & UtilClass.escapeSql(Search.Text) & "%'"
            Sql += " OR 更新者  ILIKE '%" & UtilClass.escapeSql(Search.Text) & "%'"
            Sql += " ) "
            Sql += " and 無効フラグ ='" & CommonConst.FLAG_ENABLED & "'"
            Sql += " order by 会社コード, ユーザＩＤ "

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvUser.Rows.Add()
                DgvUser.Rows(i).Cells("ユーザID").Value = ds.Tables(RS).Rows(i)("ユーザＩＤ")        'ユーザＩＤ
                DgvUser.Rows(i).Cells("氏名").Value = ds.Tables(RS).Rows(i)("氏名")              '氏名
                DgvUser.Rows(i).Cells("略名").Value = ds.Tables(RS).Rows(i)("略名")              '略名
                DgvUser.Rows(i).Cells("備考").Value = ds.Tables(RS).Rows(i)("備考")              '備考
                DgvUser.Rows(i).Cells("権限").Value = setAuthText(ds.Tables(RS).Rows(i)("権限"))              '権限
                DgvUser.Rows(i).Cells("言語").Value = setLangText(ds.Tables(RS).Rows(i)("言語"))              '言語
                DgvUser.Rows(i).Cells("更新者").Value = ds.Tables(RS).Rows(i)("更新者")              '更新者
                DgvUser.Rows(i).Cells("更新日").Value = ds.Tables(RS).Rows(i)("更新日")              '更新日
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    Private Sub BtnSelect_Click(sender As Object, e As EventArgs) Handles BtnSelect.Click, DgvUser.DoubleClick

        If _status = "PURCHASE" Then
            '発注
            Dim frm As OrderingAdd = CType(Me.Owner, OrderingAdd)
            Dim RowIndex As Integer = DgvUser.CurrentCell.RowIndex

            frm.TxtSales.Text = DgvUser.Rows(RowIndex).Cells("氏名").Value
            frm.TxtSales.Tag = DgvUser.Rows(RowIndex).Cells("ユーザID").Value

        ElseIf _status = CommonConst.STATUS_CLONE Then
            '複製
            Dim frm As Ordering = CType(Me.Owner, Ordering)
            Dim RowIndex As Integer = DgvUser.CurrentCell.RowIndex

            frm.TxtSales.Text = DgvUser.Rows(RowIndex).Cells("氏名").Value
            frm.TxtSales.Tag = DgvUser.Rows(RowIndex).Cells("ユーザID").Value
        Else
            '見積
            Dim frm As Quote = CType(Me.Owner, Quote)
            Dim RowIndex As Integer = DgvUser.CurrentCell.RowIndex

            frm.TxtSales.Text = DgvUser.Rows(RowIndex).Cells("氏名").Value
            frm.TxtSales.Tag = DgvUser.Rows(RowIndex).Cells("ユーザID").Value
        End If


        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()


    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    '検索ボタン押下時
    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        setList() '一覧再表示
    End Sub

    Private Function setAuthText(ByVal prmVal As String) As String
        Dim reVal As String

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            reVal = IIf(Integer.Parse(prmVal) = CommonConst.Auth_KBN_GENERAL, CommonConst.Auth_KBN_GENERAL_TXT_ENG, CommonConst.Auth_KBN_ADMIN_TXT_ENG)
        Else
            reVal = IIf(Integer.Parse(prmVal) = CommonConst.Auth_KBN_GENERAL, CommonConst.Auth_KBN_GENERAL_TXT, CommonConst.Auth_KBN_ADMIN_TXT)
        End If

        Return reVal
    End Function

    Private Function setLangText(ByVal prmVal As String) As String
        Dim reVal As String

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            reVal = IIf(prmVal = CommonConst.LANG_KBN_JPN, CommonConst.LANG_KBN_JPN, CommonConst.LANG_KBN_ENG)
        Else
            reVal = IIf(prmVal = CommonConst.LANG_KBN_JPN, CommonConst.LANG_KBN_JPN_TXT, CommonConst.LANG_KBN_ENG_TXT)
        End If

        Return reVal
    End Function

End Class