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
                   ByRef prmRefStatus As String,
                   Optional ByRef prmRefCompany As String = "",
                   Optional ByRef prmRefAccount As String = "")
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
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
        Dim dtToday As String = formatDatetime(DateTime.Now)
        Try
            Dim Sql As String = ""

            If _status = "ADD" Then

                Sql = "INSERT INTO "
                Sql += "Public."
                Sql += "m92_kanjo("
                Sql += "会社コード, 勘定科目コード, 勘定科目名称１, 勘定科目名称２, 勘定科目名称３, 会計用勘定科目コード, 備考, 有効区分, 更新者, 更新日)"
                Sql += " VALUES('"
                Sql += frmC01F10_Login.loginValue.BumonNM
                Sql += "', '"
                Sql += TxtAccountCode.Text
                Sql += "', '"
                Sql += TxtAccountName1.Text
                Sql += "', '"
                Sql += TxtAccountName2.Text
                Sql += "', '"
                Sql += TxtAccountName3.Text
                Sql += "', '"
                Sql += TxtAccountingAccountCode.Text
                Sql += "', '"
                Sql += TxtRemarks.Text
                Sql += "', '"
                Sql += cmbEffectiveClassification.SelectedValue.ToString
                Sql += "', '"
                Sql += frmC01F10_Login.loginValue.TantoNM
                Sql += "', '"
                Sql += dtToday
                Sql += "')"

                _db.executeDB(Sql)
            Else

                Sql = "UPDATE "
                Sql += "Public."
                Sql += "m92_kanjo "
                Sql += "SET "
                Sql += "勘定科目名称１"
                Sql += " = '"
                Sql += TxtAccountName1.Text
                Sql += "', "
                Sql += "勘定科目名称２"
                Sql += " = '"
                Sql += TxtAccountName2.Text
                Sql += "', "
                Sql += "勘定科目名称３"
                Sql += " = '"
                Sql += TxtAccountName3.Text
                Sql += "', "
                Sql += "会計用勘定科目コード"
                Sql += " = '"
                Sql += TxtAccountingAccountCode.Text
                Sql += "', "
                Sql += "備考"
                Sql += " = '"
                Sql += TxtRemarks.Text
                Sql += "', "
                Sql += "有効区分"
                Sql += " = '"
                Sql += cmbEffectiveClassification.SelectedValue.ToString
                Sql += "', "
                Sql += "更新者"
                Sql += " = '"
                Sql += frmC01F10_Login.loginValue.TantoNM
                Sql += "', "
                Sql += "更新日"
                Sql += " = '"
                Sql += dtToday
                Sql += "' "
                Sql += "WHERE"
                Sql += " 会社コード"
                Sql += "='"
                Sql += _companyCode
                Sql += "'"
                Sql += " AND"
                Sql += " 勘定科目コード"
                Sql += "='"
                Sql += _AccountCode
                Sql += "' "

                _db.executeDB(Sql)
            End If

            Dim frmMC As MstAccount
            frmMC = New MstAccount(_msgHd, _db, _langHd)
            frmMC.Show()
            Me.Close()

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
        Dim MstAccount As MstAccount
        MstAccount = New MstAccount(_msgHd, _db, _langHd)
        MstAccount.Show()
        Me.Close()
    End Sub

    '画面表示時
    Private Sub Account_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblAccountCode.Text = "CustomerCode"
            LblAccountName1.Text = "CustomerName"
            LblAccountName2.Text = "CustomerCode"
            LblAccountName3.Text = "CustomerName"
            LblAccountingAccountCode.Text = "ShortName"
            LblRemarks.Text = "PostalCode"
            LblEffectiveClassification.Text = "EffectiveClassification"
            'ExEffectiveClassification.Text = "(0:True 1:False)"
            btnAddAccount.Text = "Registration"
            btnBack.Text = "Back"
        End If

        If _status = "EDIT" Then

            Dim Sql As String = ""

            Sql = " AND "
            Sql += "勘定科目コード"
            Sql += " ILIKE "
            Sql += "'"
            Sql += _AccountCode
            Sql += "'"

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
        tb.Rows.Add(CommonConst.FLAG_ENABLED_TXT, CommonConst.FLAG_ENABLED)
        tb.Rows.Add(CommonConst.FLAG_DISABLED_TXT, CommonConst.FLAG_DISABLED)
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

        Sql += "SELECT"
        Sql += " *"
        Sql += " FROM "

        Sql += "public." & tableName
        Sql += " WHERE "
        Sql += "会社コード"
        Sql += " ILIKE  "
        Sql += "'" & frmC01F10_Login.loginValue.BumonNM & "'"
        Sql += txtParam
        Return _db.selectDB(Sql, RS, reccnt)
    End Function

    'どんなカルチャーであっても、日本の形式に変換する
    Private Function formatDatetime(ByVal prmDatetime As DateTime) As String

        'PCのカルチャーを取得し、それに応じてStringからDatetimeを作成
        Dim ciCurrent As New System.Globalization.CultureInfo(CultureInfo.CurrentCulture.Name.ToString)
        Dim dateFormat As DateTime = DateTime.Parse(prmDatetime.ToString, ciCurrent, System.Globalization.DateTimeStyles.AssumeLocal)

        Dim changeFormat As String = dateFormat.ToString("yyyy/MM/dd HH:mm:ss")

        Dim ciJP As New System.Globalization.CultureInfo(CommonConst.CI_JP)
        Dim rtnDatetime As DateTime = DateTime.Parse(changeFormat, ciJP, System.Globalization.DateTimeStyles.AssumeLocal)


        '日本の形式に書き換える
        Return changeFormat
    End Function

End Class