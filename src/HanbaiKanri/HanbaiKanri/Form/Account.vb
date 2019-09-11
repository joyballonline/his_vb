Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports System.Globalization

Public Class Account

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
    Private _db As UtilDBIf
    Private _langHd As UtilLangHandler
    Private _parentForm As Form
    'Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ
    Private _status As String = ""
    Private _companyCode As String = ""
    Private _AccountCode As String = ""

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
                   ByRef prmRefStatus As String,
                   Optional ByRef prmRefCompany As String = "",
                   Optional ByRef prmRefAccount As String = "")
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _parentForm = prmRefForm
        _status = prmRefStatus
        _companyCode = prmRefCompany
        _AccountCode = prmRefAccount
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True

    End Sub

    Private Sub btnAddAccount_Click(sender As Object, e As EventArgs) Handles btnAddAccount.Click
        Dim dtToday As String = UtilClass.formatDatetime(DateTime.Now)
        Try
            Dim Sql As String = ""

            If _status = CommonConst.STATUS_ADD Then

                Sql = "INSERT INTO Public.m92_kanjo("
                Sql += "会社コード, 勘定科目コード, 勘定科目名称１, 勘定科目名称２, 勘定科目名称３, 会計用勘定科目コード, 備考, 有効区分, 更新者, 更新日)"
                Sql += " VALUES("
                Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql += ", '" & TxtAccountCode.Text & "'"
                Sql += ", '" & TxtAccountName1.Text & "'"
                Sql += ", '" & TxtAccountName2.Text & "'"
                Sql += ", '" & TxtAccountName3.Text & "'"
                Sql += ", '" & TxtAccountingAccountCode.Text & "'"
                Sql += ", '" & TxtRemarks.Text & "'"
                Sql += ", '" & cmbEffectiveClassification.SelectedValue.ToString & "'"
                Sql += ", '" & frmC01F10_Login.loginValue.TantoNM & "'"
                Sql += ", '" & dtToday & "'"
                Sql += ")"

                _db.executeDB(Sql)
            Else

                Sql = "UPDATE Public.m92_kanjo "
                Sql += "SET "
                Sql += "勘定科目名称１ = '" & TxtAccountName1.Text & "'"
                Sql += ", 勘定科目名称２ = '" & TxtAccountName2.Text & "'"
                Sql += ", 勘定科目名称３ = '" & TxtAccountName3.Text & "'"
                Sql += ", 会計用勘定科目コード = '" & TxtAccountingAccountCode.Text & "'"
                Sql += ", 備考 = '" & TxtRemarks.Text & "'"
                Sql += ", 有効区分 = '" & cmbEffectiveClassification.SelectedValue.ToString & "'"
                Sql += ", 更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
                Sql += ", 更新日 = '" & dtToday & "'"
                Sql += " WHERE 会社コード = '" & _companyCode & "'"
                Sql += " AND 勘定科目コード = '" & _AccountCode & "'"

                _db.executeDB(Sql)
            End If

            _parentForm.Enabled = True
            _parentForm.Show()
            Me.Dispose()

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '戻るボタン押下時
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    '画面表示時
    Private Sub Account_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblAccountCode.Text = "AccountCode"
            LblAccountName1.Text = "AccountName1"
            LblAccountName2.Text = "AccountName2"
            LblAccountName3.Text = "AccountName3"
            LblAccountingAccountCode.Text = "AccountingAccountCode"
            LblRemarks.Text = "Remarks"
            LblEffectiveClassification.Text = "EffectiveClassification"
            'ExEffectiveClassification.Text = "(0:True 1:False)"
            btnAddAccount.Text = "Registration"
            btnBack.Text = "Back"
        End If

        If _status = CommonConst.STATUS_EDIT Then

            Dim Sql As String = ""

            Sql = " AND 勘定科目コード = '" & _AccountCode & "'"

            Dim ds As DataSet = getDsData("m92_kanjo", Sql)

            If ds.Tables(RS).Rows(0)("勘定科目コード") IsNot DBNull.Value Then
                TxtAccountCode.Text = ds.Tables(RS).Rows(0)("勘定科目コード")
            End If

            If ds.Tables(RS).Rows(0)("勘定科目名称１") IsNot DBNull.Value Then
                TxtAccountName1.Text = ds.Tables(RS).Rows(0)("勘定科目名称１")
            End If

            If ds.Tables(RS).Rows(0)("勘定科目名称２") IsNot DBNull.Value Then
                TxtAccountName2.Text = ds.Tables(RS).Rows(0)("勘定科目名称２")
            End If

            If ds.Tables(RS).Rows(0)("勘定科目名称３") IsNot DBNull.Value Then
                TxtAccountName3.Text = ds.Tables(RS).Rows(0)("勘定科目名称３")
            End If

            If ds.Tables(RS).Rows(0)("会計用勘定科目コード") IsNot DBNull.Value Then
                TxtAccountingAccountCode.Text = ds.Tables(RS).Rows(0)("会計用勘定科目コード")
            End If

            If ds.Tables(RS).Rows(0)("備考") IsNot DBNull.Value Then
                TxtRemarks.Text = ds.Tables(RS).Rows(0)("備考")
            End If

            If ds.Tables(RS).Rows(0)("有効区分") IsNot DBNull.Value Then
                createCombobox(ds.Tables(RS).Rows(0)("有効区分"))
                'TxtEffectiveClassification.Text = ds.Tables(RS).Rows(0)("有効区分")
            End If

        Else

            createCombobox()

        End If
    End Sub

    '有効無効のコンボボックスを作成
    '編集モードの時は値を渡してセットさせる
    Private Sub createCombobox(Optional ByRef prmVal As String = "")
        cmbEffectiveClassification.DisplayMember = "Text"
        cmbEffectiveClassification.ValueMember = "Value"

        Dim tb As New DataTable
        tb.Columns.Add("Text", GetType(String))
        tb.Columns.Add("Value", GetType(Integer))
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            tb.Rows.Add(CommonConst.FLAG_ENABLED_TXT_ENG, CommonConst.FLAG_ENABLED)
            tb.Rows.Add(CommonConst.FLAG_DISABLED_TXT_ENG, CommonConst.FLAG_DISABLED)
        Else
            tb.Rows.Add(CommonConst.FLAG_ENABLED_TXT, CommonConst.FLAG_ENABLED)
            tb.Rows.Add(CommonConst.FLAG_DISABLED_TXT, CommonConst.FLAG_DISABLED)
        End If

        cmbEffectiveClassification.DataSource = tb

        If prmVal IsNot Nothing Then
            cmbEffectiveClassification.SelectedValue = prmVal
        End If

    End Sub

    'param1：String テーブル名
    'param2：String 詳細条件
    'Return: DataSet
    Private Function getDsData(ByVal tableName As String, Optional ByRef txtParam As String = "") As DataSet
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        Sql += "SELECT * FROM public." & tableName
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += txtParam
        Return _db.selectDB(Sql, RS, reccnt)
    End Function

End Class