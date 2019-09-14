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

        txtRate1.ReadOnly = True
        txtRate1.BackColor = Color.FromArgb(255, 255, 192)
        txtRate1.TabStop = False

        txtRate2.ReadOnly = True
        txtRate2.BackColor = Color.FromArgb(255, 255, 192)
        txtRate2.TabStop = False

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then

            LblStandardDate.Text = "StandardDate"
            LblBaseCurrency.Text = "BaseCurrency"
            LblRate.Text = "ExchangeRate(IDR)"

            BtnRegistration.Text = "Registration"
            BtnBack.Text = "Back"

            Label1.Text = "Rate"
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
            LblJPY.Text = "1" & ds.Tables(RS).Rows(0)("通貨コード") & "     = "
            NudForeignCurrency1.Tag = ds.Tables(RS).Rows(0)("採番キー")
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
            LblUSD.Text = "1" & ds.Tables(RS).Rows(0)("通貨コード") & "     = "
            NudForeignCurrency2.Tag = ds.Tables(RS).Rows(0)("採番キー")
        Else
            '操作できないアラートを出す
            _msgHd.dspMSG("chkCurrencyError", frmC01F10_Login.loginValue.Language)
        End If


        '拡張_外貨
        For i As Integer = 4 To 6

            Sql = " AND "
            Sql += "採番キー"
            Sql += " = " & i

            ds = getDsData("m25_currency", Sql)

            If ds.Tables(RS).Rows.Count > 0 Then  'データあり

                '可視
                If i = 4 Then
                    LblCurrency4.Visible = True
                    NudForeignCurrency4.Visible = True

                    LblCurrency4.Text = "1" & ds.Tables(RS).Rows(0)("通貨コード") & "     = "
                    NudForeignCurrency4.Tag = ds.Tables(RS).Rows(0)("採番キー")

                ElseIf i = 5 Then
                    LblCurrency5.Visible = True
                    NudForeignCurrency5.Visible = True

                    LblCurrency5.Text = "1" & ds.Tables(RS).Rows(0)("通貨コード") & "     = "
                    NudForeignCurrency5.Tag = ds.Tables(RS).Rows(0)("採番キー")

                ElseIf i = 6 Then
                    LblCurrency6.Visible = True
                    NudForeignCurrency6.Visible = True

                    LblCurrency6.Text = "1" & ds.Tables(RS).Rows(0)("通貨コード") & "     = "
                    NudForeignCurrency6.Tag = ds.Tables(RS).Rows(0)("採番キー")

                End If

            Else  'データなし
                'Me.("LblCurrency4").Visible = False

                '非可視
                If i = 4 Then
                    LblCurrency4.Visible = False
                    NudForeignCurrency4.Visible = False

                    NudForeignCurrency4.Tag = 0
                ElseIf i = 5 Then
                    LblCurrency5.Visible = False
                    NudForeignCurrency5.Visible = False

                    NudForeignCurrency5.Tag = 0
                ElseIf i = 6 Then
                    LblCurrency6.Visible = False
                    NudForeignCurrency6.Visible = False

                    NudForeignCurrency6.Tag = 0
                End If

            End If
        Next

    End Sub

    '戻るボダン押下時
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    '登録ボタン押下時
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles BtnRegistration.Click

        Dim reccnt As Integer = 0
        Dim Sql As String = ""
        Dim ds As DataSet


        'チェック
        If mCheck = False Then
            Exit Sub
        End If


        '重複データのチェック
        Sql = " SELECT count(*) as 件数"
        Sql += " FROM public.t71_exchangerate"

        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += "   and 基準日 = '" & UtilClass.strFormatDate(DtpStandardDate.Text) & "'"
        Sql += "   and 採番キー = " & NudForeignCurrency1.Tag.ToString

        ds = _db.selectDB(Sql, RS, reccnt)

        '件数があればエラー
        If ds.Tables(RS).Rows(0)("件数") > 0 Then
            _msgHd.dspMSG("RegDupKeyData", frmC01F10_Login.loginValue.Language)
            Exit Sub
        End If


        '登録処理
        Dim dtToday As String = UtilClass.formatDatetime(DateTime.Now)
        If _status = CommonConst.STATUS_ADD Then
        End If

        Try
            Sql = "INSERT INTO "
            Sql += "Public."
            Sql += "t71_exchangerate("
            Sql += "会社コード, 基準日, 採番キー, レート, 更新者, 更新日)"
            Sql += " VALUES('"
            Sql += frmC01F10_Login.loginValue.BumonCD '会社コード
            Sql += "', '"
            Sql += UtilClass.strFormatDate(DtpStandardDate.Text) '基準日
            Sql += "', "
            Sql += NudForeignCurrency1.Tag.ToString '採番キー
            Sql += ", '"
            Sql += UtilClass.formatNumberF10(lblBaseCurrency1.Text / NudForeignCurrency1.Text) 'レート
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
            Sql += NudForeignCurrency2.Tag.ToString '採番キー
            Sql += ", '"
            Sql += UtilClass.formatNumberF10(lblBaseCurrency2.Text / NudForeignCurrency2.Text) 'レート
            Sql += "', '"
            Sql += frmC01F10_Login.loginValue.TantoNM
            Sql += "', '"
            Sql += dtToday
            Sql += "')"

            _db.executeDB(Sql)


            '拡張_外貨
            For i As Integer = 4 To 6

                Dim tag As String = vbNullString
                Dim NudForeignCurrency As Decimal

                If i = 4 Then
                    tag = NudForeignCurrency4.Tag.ToString
                    NudForeignCurrency = NudForeignCurrency4.Text
                ElseIf i = 5 Then
                    tag = NudForeignCurrency5.Tag.ToString
                    NudForeignCurrency = NudForeignCurrency5.Text
                ElseIf i = 6 Then
                    tag = NudForeignCurrency6.Tag.ToString
                    NudForeignCurrency = NudForeignCurrency6.Text
                End If

                If tag = 0 Then
                Else
                    Call mSet_t71(tag, NudForeignCurrency)
                End If
            Next


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

    Private Function mCheck() As Boolean


        If lblBaseCurrency1.Text = "" Or NudForeignCurrency1.Text = "" Or lblBaseCurrency2.Text = "" Or NudForeignCurrency2.Text = "" Then
            '登録できないアラートを出す
            _msgHd.dspMSG("chkInputError", frmC01F10_Login.loginValue.Language)
            Exit Function
        End If

        If NudForeignCurrency1.Text = 0 Or NudForeignCurrency2.Text = 0 Then
            '登録できないアラートを出す
            _msgHd.dspMSG("chkInputError", frmC01F10_Login.loginValue.Language)
            Exit Function
        End If

        If NudForeignCurrency4.Visible = True Then
            If NudForeignCurrency4.Text = "" OrElse NudForeignCurrency4.Text = 0 Then
                '登録できないアラートを出す
                _msgHd.dspMSG("chkInputError", frmC01F10_Login.loginValue.Language)
                Exit Function
            End If
        End If

        If NudForeignCurrency5.Visible = True Then
            If NudForeignCurrency5.Text = "" OrElse NudForeignCurrency5.Text = 0 Then
                '登録できないアラートを出す
                _msgHd.dspMSG("chkInputError", frmC01F10_Login.loginValue.Language)
                Exit Function
            End If
        End If

        If NudForeignCurrency6.Visible = True Then
            If NudForeignCurrency6.Text = "" OrElse NudForeignCurrency6.Text = 0 Then
                '登録できないアラートを出す
                _msgHd.dspMSG("chkInputError", frmC01F10_Login.loginValue.Language)
                Exit Function
            End If
        End If

        mCheck = True

    End Function

    Private Sub mSet_t71(ByVal Saiban As String, ByVal NudForeignCurrency As Decimal)

        Dim dtToday As String = UtilClass.formatDatetime(DateTime.Now)
        Dim sql As String


        sql = "INSERT INTO "
        sql += "Public."
        sql += "t71_exchangerate("
        sql += "会社コード, 基準日, 採番キー, レート, 更新者, 更新日)"
        sql += " VALUES('"
        sql += frmC01F10_Login.loginValue.BumonCD '会社コード
        sql += "', '"
        sql += UtilClass.strFormatDate(DtpStandardDate.Text) '基準日
        sql += "', "
        sql += Saiban.ToString '採番キー
        sql += ", '"
        sql += UtilClass.formatNumberF10(1 / NudForeignCurrency) 'レート
        sql += "', '"
        sql += frmC01F10_Login.loginValue.TantoNM
        sql += "', '"
        sql += dtToday
        sql += "')"

        _db.executeDB(sql)


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
    Private Sub lblBaseCurrency1_Validated(sender As Object, e As EventArgs)
        If TxtForeignCurrency1.Text <> "" And NudForeignCurrency1.Value > 0 Then
            'Dim rateVal As Decimal = lblBaseCurrency1.Text / NudForeignCurrency1.Value
            Dim rateVal As Decimal = Decimal.Parse(NudForeignCurrency1.Value) / Decimal.Parse(lblBaseCurrency1.Text)
            NudForeignCurrency1.Text = rateVal.ToString("F10")
        End If

    End Sub

    Private Sub TxtForeignCurrency1_Validated(sender As Object, e As EventArgs) Handles TxtForeignCurrency1.Validated
        'If lblBaseCurrency1.Text <> "" And TxtForeignCurrency1.Text <> "" Then
        '    Dim rateVal As Decimal = lblBaseCurrency1.Text / TxtForeignCurrency1.Text
        '    TxtRate1.Text = rateVal.ToString("F10")
        'End If
    End Sub

    Private Sub NudForeignCurrency1_Validated(sender As Object, e As EventArgs) Handles NudForeignCurrency1.Validated
        If lblBaseCurrency1.Text <> "" And NudForeignCurrency1.Value > 0 Then
            Dim rateVal As Decimal = Decimal.Parse(NudForeignCurrency1.Value) / Decimal.Parse(lblBaseCurrency1.Text)
            NudForeignCurrency1.Text = rateVal.ToString("F10")
            rateVal = Decimal.Parse(lblBaseCurrency1.Text) / Decimal.Parse(NudForeignCurrency1.Text)
            txtRate1.Text = rateVal.ToString("F10")
        End If
    End Sub

    'IDR → USD
    Private Sub lblBaseCurrency2_Validated(sender As Object, e As EventArgs)
        If lblBaseCurrency2.Text <> "" And NudForeignCurrency2.Value > 0 Then
            'Dim rateVal As Decimal = Decimal.Parse(lblBaseCurrency2.Text) / Decimal.Parse(UtilClass.formatNumberF10(NudForeignCurrency2.Value))
            Dim rateVal As Decimal = Decimal.Parse(NudForeignCurrency2.Value) / Decimal.Parse(lblBaseCurrency2.Text)
            NudForeignCurrency2.Text = rateVal.ToString("F10")
        End If
    End Sub

    Private Sub TxtForeignCurrency2_Validated(sender As Object, e As EventArgs) Handles TxtForeignCurrency2.Validated
        'If lblBaseCurrency2.Text <> "" And NudForeignCurrency2.Value <> 0 Then
        '    Dim rateVal As Decimal = lblBaseCurrency2.Text / TxtForeignCurrency2.Text
        '    TxtRate2.Text = rateVal.ToString("F10")
        'End If
    End Sub

    Private Sub NudForeignCurrency2_Validated(sender As Object, e As EventArgs) Handles NudForeignCurrency2.Validated
        If lblBaseCurrency2.Text <> "" And NudForeignCurrency2.Value > 0 Then
            Dim rateVal As Decimal = Decimal.Parse(NudForeignCurrency2.Value) / Decimal.Parse(lblBaseCurrency2.Text)
            NudForeignCurrency2.Text = rateVal.ToString("F10")
            rateVal = Decimal.Parse(lblBaseCurrency2.Text) / Decimal.Parse(NudForeignCurrency2.Text)
            txtRate2.Text = rateVal.ToString("F10")
        End If
    End Sub

    Private Sub NudForeignCurrency1_Enter(sender As Object, e As EventArgs) Handles NudForeignCurrency1.Enter
        sender.Select(0, sender.Text.Length)
    End Sub

    Private Sub NudForeignCurrency2_Enter(sender As Object, e As EventArgs) Handles NudForeignCurrency2.Enter
        sender.Select(0, sender.Text.Length)
    End Sub

    Private Sub NudForeignCurrency1_Click(sender As Object, e As EventArgs) Handles NudForeignCurrency1.Click

    End Sub

    Private Sub NudForeignCurrency2_Click(sender As Object, e As EventArgs) Handles NudForeignCurrency2.Click
        sender.Select(0, sender.Text.Length)
    End Sub

    Private Sub NudForeignCurrency4_Enter(sender As Object, e As EventArgs) Handles NudForeignCurrency4.Enter
        sender.Select(0, sender.Text.Length)
    End Sub

    Private Sub NudForeignCurrency5_Enter(sender As Object, e As EventArgs) Handles NudForeignCurrency5.Enter
        sender.Select(0, sender.Text.Length)
    End Sub

    Private Sub NudForeignCurrency6_Enter(sender As Object, e As EventArgs) Handles NudForeignCurrency6.Enter
        sender.Select(0, sender.Text.Length)
    End Sub
End Class