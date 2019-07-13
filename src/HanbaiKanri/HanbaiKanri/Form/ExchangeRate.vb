Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class ExchangeRate
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
    Private _key1 As String = ""
    Private _key2 As String = ""


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
                   Optional ByRef prmRefCode As String = Nothing,
                   Optional ByRef prmRefKey1 As String = Nothing,
                   Optional ByRef prmRefKey2 As String = Nothing)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _parentForm = prmRefForm
        _status = prmRefStatus
        _companyCode = prmRefCode
        _key1 = prmRefKey1
        _key2 = prmRefKey2

        If _key1 <> Nothing Then
            _key1 = UtilClass.strFormatDate(_key1)
        End If
        If _key2 <> Nothing Then
            _key2 = UtilClass.strFormatDate(_key2)
        End If

        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True

    End Sub

    '初期表示時
    Private Sub ExchangeRate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim reccnt As Integer = 0
        Dim Sql As String = ""
        Dim ds As DataSet

        DtpStandardDate.Text = DateTime.Today

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then

            LblStandardDate.Text = "StandardDate"
            LblBaseCurrency.Text = "BaseCurrency"
            LblForeignCurrency.Text = "ForeignCurrency"
            LblRate.Text = "Rate"

            BtnRegistration.Text = "Registration"
            BtnBack.Text = "Back"

        End If

        'IDRテキスト取得
        Sql = " AND "
        Sql += "採番キー"
        Sql += " = "
        Sql += CommonConst.CURRENCY_CD_IDR.ToString

        ds = getDsData("m25_currency", Sql)

        If ds.Tables(RS).Rows.Count > 0 Then
            LblIDR1.Text = ds.Tables(RS).Rows(0)("通貨コード")
            LblIDR2.Text = ds.Tables(RS).Rows(0)("通貨コード")
        Else
            '操作できないアラートを出す
            _msgHd.dspMSG("chkCurrencyError", frmC01F10_Login.loginValue.Language)
        End If

        'JPYテキスト取得
        Sql = " AND "
        Sql += "採番キー"
        Sql += " = "
        Sql += CommonConst.CURRENCY_CD_JPY.ToString

        ds = getDsData("m25_currency", Sql)

        If ds.Tables(RS).Rows.Count > 0 Then
            LblJPY.Text = ds.Tables(RS).Rows(0)("通貨コード") & "     = "
            TxtRate1.Tag = ds.Tables(RS).Rows(0)("採番キー")
        Else
            '操作できないアラートを出す
            _msgHd.dspMSG("chkCurrencyError", frmC01F10_Login.loginValue.Language)
        End If

        'USDテキスト取得
        Sql = " AND "
        Sql += "採番キー"
        Sql += " = "
        Sql += CommonConst.CURRENCY_CD_USD.ToString

        ds = getDsData("m25_currency", Sql)

        If ds.Tables(RS).Rows.Count > 0 Then
            LblUSD.Text = ds.Tables(RS).Rows(0)("通貨コード") & "     = "
            TxtRate2.Tag = ds.Tables(RS).Rows(0)("採番キー")
        Else
            '操作できないアラートを出す
            _msgHd.dspMSG("chkCurrencyError", frmC01F10_Login.loginValue.Language)
        End If

    End Sub

    '戻るボダン押下時
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    '登録ボタン押下時
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles BtnRegistration.Click

        If TxtBaseCurrency1.Text = "" Or NudForeignCurrency1.Text = "" Or TxtBaseCurrency2.Text = "" Or NudForeignCurrency2.Text = "" Then
            '登録できないアラートを出す
            _msgHd.dspMSG("chkInputError", frmC01F10_Login.loginValue.Language)
            Exit Sub
        End If

        Dim dtToday As String = UtilClass.formatDatetime(DateTime.Now)
        If _status = CommonConst.STATUS_ADD Then
        End If

        Try
            Dim Sql As String = ""

            Sql = "INSERT INTO "
            Sql += "Public."
            Sql += "t71_exchangerate("
            Sql += "会社コード, 基準日, 採番キー, レート, 更新者, 更新日)"
            Sql += " VALUES('"
            Sql += frmC01F10_Login.loginValue.BumonCD '会社コード
            Sql += "', '"
            Sql += UtilClass.strFormatDate(DtpStandardDate.Text) '基準日
            Sql += "', "
            Sql += TxtRate1.Tag.ToString '採番キー
            Sql += ", '"
            Sql += UtilClass.formatNumberF10(TxtRate1.Text) 'レート
            Sql += "', '"
            Sql += frmC01F10_Login.loginValue.TantoNM
            Sql += "', '"
            Sql += dtToday
            Sql += "')"

            _db.executeDB(Sql)

            Sql = "INSERT INTO "
            Sql += "Public."
            Sql += "t71_exchangerate("
            Sql += "会社コード, 基準日, 採番キー, レート, 更新者, 更新日)"
            Sql += " VALUES('"
            Sql += frmC01F10_Login.loginValue.BumonCD '会社コード
            Sql += "', '"
            Sql += UtilClass.strFormatDate(DtpStandardDate.Text) '基準日
            Sql += "', "
            Sql += TxtRate2.Tag.ToString '採番キー
            Sql += ", '"
            Sql += UtilClass.formatNumberF10(TxtRate2.Text) 'レート
            Sql += "', '"
            Sql += frmC01F10_Login.loginValue.TantoNM
            Sql += "', '"
            Sql += dtToday
            Sql += "')"

            _db.executeDB(Sql)

            _parentForm.Enabled = True
            _parentForm.Show()
            Me.Dispose()

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try
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
        Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += txtParam

        Return _db.selectDB(Sql, RS, reccnt)
    End Function

    'IDR → JPY
    Private Sub TxtBaseCurrency1_Validated(sender As Object, e As EventArgs) Handles TxtBaseCurrency1.Validated
        If TxtForeignCurrency1.Text <> "" And NudForeignCurrency1.Value > 0 Then
            'Dim rateVal As Decimal = TxtBaseCurrency1.Text / NudForeignCurrency1.Value
            Dim rateVal As Decimal = Decimal.Parse(NudForeignCurrency1.Value) / Decimal.Parse(TxtBaseCurrency1.Text)
            TxtRate1.Text = rateVal.ToString("F10")
        End If

    End Sub

    Private Sub TxtForeignCurrency1_Validated(sender As Object, e As EventArgs) Handles TxtForeignCurrency1.Validated
        'If TxtBaseCurrency1.Text <> "" And TxtForeignCurrency1.Text <> "" Then
        '    Dim rateVal As Decimal = TxtBaseCurrency1.Text / TxtForeignCurrency1.Text
        '    TxtRate1.Text = rateVal.ToString("F10")
        'End If
    End Sub

    Private Sub NudForeignCurrency1_Validated(sender As Object, e As EventArgs) Handles NudForeignCurrency1.Validated
        If TxtBaseCurrency1.Text <> "" And NudForeignCurrency1.Value > 0 Then
            Dim rateVal As Decimal = Decimal.Parse(NudForeignCurrency1.Value) / Decimal.Parse(TxtBaseCurrency1.Text)
            TxtRate1.Text = rateVal.ToString("F10")
        End If
    End Sub

    'IDR → USD
    Private Sub TxtBaseCurrency2_Validated(sender As Object, e As EventArgs) Handles TxtBaseCurrency2.Validated
        If TxtBaseCurrency2.Text <> "" And NudForeignCurrency2.Value > 0 Then
            'Dim rateVal As Decimal = Decimal.Parse(TxtBaseCurrency2.Text) / Decimal.Parse(UtilClass.formatNumberF10(NudForeignCurrency2.Value))
            Dim rateVal As Decimal = Decimal.Parse(NudForeignCurrency2.Value) / Decimal.Parse(TxtBaseCurrency2.Text)
            TxtRate2.Text = rateVal.ToString("F10")
        End If
    End Sub

    Private Sub TxtForeignCurrency2_Validated(sender As Object, e As EventArgs) Handles TxtForeignCurrency2.Validated
        'If TxtBaseCurrency2.Text <> "" And NudForeignCurrency2.Value <> 0 Then
        '    Dim rateVal As Decimal = TxtBaseCurrency2.Text / TxtForeignCurrency2.Text
        '    TxtRate2.Text = rateVal.ToString("F10")
        'End If
    End Sub

    Private Sub NudForeignCurrency2_Validated(sender As Object, e As EventArgs) Handles NudForeignCurrency2.Validated
        If TxtBaseCurrency2.Text <> "" And NudForeignCurrency2.Value > 0 Then
            Dim rateVal As Decimal = Decimal.Parse(NudForeignCurrency2.Value) / Decimal.Parse(TxtBaseCurrency2.Text)
            TxtRate2.Text = rateVal.ToString("F10")
        End If
    End Sub

    Private Sub NudForeignCurrency1_Enter(sender As Object, e As EventArgs) Handles NudForeignCurrency1.Enter
        sender.Select(0, sender.Text.Length)
    End Sub

    Private Sub NudForeignCurrency2_Enter(sender As Object, e As EventArgs) Handles NudForeignCurrency2.Enter
        sender.Select(0, sender.Text.Length)
    End Sub

    Private Sub NudForeignCurrency1_Click(sender As Object, e As EventArgs) Handles NudForeignCurrency1.Click
        sender.Select(0, sender.Text.Length)
    End Sub

    Private Sub NudForeignCurrency2_Click(sender As Object, e As EventArgs) Handles NudForeignCurrency2.Click
        sender.Select(0, sender.Text.Length)
    End Sub

    Private Sub checkDecimalPoint()

    End Sub
End Class