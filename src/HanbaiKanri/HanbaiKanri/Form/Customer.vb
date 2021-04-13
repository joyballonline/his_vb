Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class Customer
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
    'Private _companyCode As String = ""
    Private _customerCode As String = ""

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
                   Optional ByRef prmRefCustomer As String = "")
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _parentForm = prmRefForm
        _status = prmRefStatus
        '_companyCode = prmRefCompany
        _customerCode = prmRefCustomer
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True

    End Sub

    Private Sub Customer_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblCustomerCode.Text = "CustomerCode"
            LblCustomerName.Text = "CustomerName"
            LblCustomerShortName.Text = "ShortName"
            LblPostalCode.Text = "PostalCode"
            LblAddress1.Text = "Address1"
            LblAddress2.Text = "Address2"
            LblAddress3.Text = "Address3"
            LblTEL.Text = "TEL"
            LblTelSearch.Text = "TEL(Search)"
            LblFax.Text = "FAX"
            LblPerson.Text = "Person"
            LblPosition.Text = "Position"
            LblPaymentTerms.Text = "PaymentTerms"
            LblMemo.Text = "Memo"
            LblAccountingVendorCode.Text = "AccountingVendorCode"
            LblDomesticKbn.Text = "DomesticDivision"

            LblCustomerCodeRemarks.Text = "(Non-Overlapping string)"
            LblPostalCodeEx.Text = "(Example:0123456)"
            LblTelEx.Text = "(Example:0123456789)"
            LblTelSearchEx.Text = "(Example:0123456789)"
            LblFaxEx.Text = "(Example:0123456789)"

            btnRegistrarion.Text = "Registration"
            btnBack.Text = "Back"
        End If

        createCombobox() '国内区分コンボボックスを設定

        '編集時
        If _status = CommonConst.STATUS_EDIT Then

            TxtCustomerCode.Enabled = False '編集不可

            Dim Sql As String = ""
            Sql += "SELECT "
            Sql += "会社コード, "
            Sql += "得意先コード, "
            Sql += "得意先名, "
            Sql += "得意先名略称, "
            Sql += "郵便番号, "
            Sql += "住所１, "
            Sql += "住所２, "
            Sql += "住所３, "
            Sql += "電話番号, "
            Sql += "電話番号検索用, "
            Sql += "ＦＡＸ番号, "
            Sql += "担当者名, "
            Sql += "担当者役職, "
            Sql += "既定支払条件, "
            Sql += "メモ, "
            Sql += "会計用得意先コード, "
            Sql += "国内区分, "
            Sql += "更新者, "
            Sql += "更新日, "
            Sql += "is_active "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "m10_customer"
            Sql += " WHERE "
            Sql += "会社コード"
            Sql += " ILIKE "
            Sql += "'"
            Sql += frmC01F10_Login.loginValue.BumonCD
            Sql += "'"
            Sql += " AND "
            Sql += "得意先コード"
            Sql += " ILIKE "
            Sql += "'"
            Sql += _customerCode
            Sql += "'"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            If ds.Tables(RS).Rows(0)("会社コード") Is DBNull.Value Then
            Else
                TxtCompanyCode.Text = ds.Tables(RS).Rows(0)("会社コード")
            End If

            If ds.Tables(RS).Rows(0)("得意先コード") Is DBNull.Value Then
            Else
                TxtCustomerCode.Text = ds.Tables(RS).Rows(0)("得意先コード")
            End If

            If ds.Tables(RS).Rows(0)("得意先名") Is DBNull.Value Then
            Else
                TxtCustomerName.Text = ds.Tables(RS).Rows(0)("得意先名")
            End If

            If ds.Tables(RS).Rows(0)("得意先名略称") Is DBNull.Value Then
            Else
                TxtCustomerShortName.Text = ds.Tables(RS).Rows(0)("得意先名略称")
            End If

            If ds.Tables(RS).Rows(0)("郵便番号") Is DBNull.Value Then
            Else
                TxtPostalCode.Text = ds.Tables(RS).Rows(0)("郵便番号")
            End If

            If ds.Tables(RS).Rows(0)("住所１") Is DBNull.Value Then
            Else
                TxtAddress1.Text = ds.Tables(RS).Rows(0)("住所１")
            End If

            If ds.Tables(RS).Rows(0)("住所２") Is DBNull.Value Then
            Else
                TxtAddress2.Text = ds.Tables(RS).Rows(0)("住所２")
            End If

            If ds.Tables(RS).Rows(0)("住所３") Is DBNull.Value Then
            Else
                TxtAddress3.Text = ds.Tables(RS).Rows(0)("住所３")
            End If

            If ds.Tables(RS).Rows(0)("電話番号") Is DBNull.Value Then
            Else
                TxtTel.Text = ds.Tables(RS).Rows(0)("電話番号")
            End If

            If ds.Tables(RS).Rows(0)("電話番号検索用") Is DBNull.Value Then
            Else
                TxtTelSearch.Text = ds.Tables(RS).Rows(0)("電話番号検索用")
            End If

            If ds.Tables(RS).Rows(0)("ＦＡＸ番号") Is DBNull.Value Then
            Else
                TxtFax.Text = ds.Tables(RS).Rows(0)("ＦＡＸ番号")
            End If

            If ds.Tables(RS).Rows(0)("担当者名") Is DBNull.Value Then
            Else
                TxtPerson.Text = ds.Tables(RS).Rows(0)("担当者名")
            End If

            If ds.Tables(RS).Rows(0)("担当者役職") Is DBNull.Value Then
            Else
                TxtPosition.Text = ds.Tables(RS).Rows(0)("担当者役職")
            End If

            If ds.Tables(RS).Rows(0)("既定支払条件") Is DBNull.Value Then
            Else
                TxtPaymentTerms.Text = ds.Tables(RS).Rows(0)("既定支払条件")
            End If

            If ds.Tables(RS).Rows(0)("メモ") Is DBNull.Value Then
            Else
                TxtMemo.Text = ds.Tables(RS).Rows(0)("メモ")
            End If

            If ds.Tables(RS).Rows(0)("会計用得意先コード") Is DBNull.Value Then
            Else
                TxtAccountingVendorCode.Text = ds.Tables(RS).Rows(0)("会計用得意先コード")
            End If

            If ds.Tables(RS).Rows(0)("国内区分") Is DBNull.Value Then
            Else
                createCombobox(ds.Tables(RS).Rows(0)("国内区分"))
            End If
            If ds.Tables(RS).Rows(0)("is_active") Is DBNull.Value Then
            Else
                ChkActive.Checked = IIf(ds.Tables(RS).Rows(0)("is_active") = 0, True, False)
            End If
        End If
    End Sub

    Private Sub btnAddCustomer_Click(sender As Object, e As EventArgs) Handles btnRegistrarion.Click
        '項目チェック
        Dim strMessage As String = ""    'メッセージ本文
        Dim strMessageTitle As String = ""      'メッセージタイトル
        '得意先コードは必須
        If TxtCustomerCode.Text = "" Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                strMessage = "Please enter Customer Code. "
                strMessageTitle = "CustomerCode Error"
            Else
                strMessage = "得意先コードを入力してください。"
                strMessageTitle = "得意先コード入力エラー"
            End If
            Dim result As DialogResult = MessageBox.Show(strMessage, strMessageTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        '登録処理はここから
        Dim dtToday As String = UtilClass.formatDatetime(DateTime.Now)
        Try
            If _status = CommonConst.STATUS_ADD Then
                Dim Sql As String = ""

                Sql = ""
                Sql += "INSERT INTO "
                Sql += "Public."
                Sql += "m10_customer("
                Sql += "会社コード, 得意先コード, 得意先名, 得意先名略称, 郵便番号, 住所１, 住所２, 住所３, 電話番号, 電話番号検索用"
                Sql += ", ＦＡＸ番号, 担当者名, 担当者役職, 既定支払条件, メモ, 会計用得意先コード, 国内区分, 更新者, 更新日)"
                Sql += " VALUES('"
                Sql += frmC01F10_Login.loginValue.BumonCD '会社コード
                Sql += "', '"
                Sql += TxtCustomerCode.Text '得意先コード
                Sql += "', '"
                Sql += TxtCustomerName.Text '得意先名
                Sql += "', '"
                Sql += TxtCustomerShortName.Text '得意先名略称
                Sql += "', '"
                Sql += TxtPostalCode.Text '郵便番号
                Sql += "', '"
                Sql += TxtAddress1.Text '住所１
                Sql += "', '"
                Sql += TxtAddress2.Text '住所２
                Sql += "', '"
                Sql += TxtAddress3.Text '住所３
                Sql += "', '"
                Sql += TxtTel.Text '電話番号
                Sql += "', '"
                Sql += TxtTelSearch.Text '電話番号検索用
                Sql += "', '"
                Sql += TxtFax.Text 'ＦＡＸ番号
                Sql += "', '"
                Sql += TxtPerson.Text '担当者名
                Sql += "', '"
                Sql += TxtPosition.Text '担当者役職
                Sql += "', '"
                Sql += TxtPaymentTerms.Text '既定支払条件
                Sql += "', '"
                Sql += TxtMemo.Text 'メモ
                Sql += "', '"
                Sql += TxtAccountingVendorCode.Text '会計用得意先コード
                Sql += "', '"
                Sql += cmDomesticKbn.SelectedValue.ToString '国内区分
                Sql += "', '"
                Sql += frmC01F10_Login.loginValue.TantoNM '更新者
                Sql += "', '"
                Sql += dtToday '更新日
                Sql += "')"

                _db.executeDB(Sql)

            Else

                Dim Sql As String = ""

                Sql = ""
                Sql += "UPDATE "
                Sql += "Public."
                Sql += "m10_customer "
                Sql += "SET "
                Sql += " 会社コード"
                Sql += " = '"
                Sql += TxtCompanyCode.Text
                Sql += "', "
                Sql += "得意先コード"
                Sql += " = '"
                Sql += TxtCustomerCode.Text
                Sql += "', "
                Sql += "得意先名"
                Sql += " = '"
                Sql += TxtCustomerName.Text
                Sql += "', "
                Sql += "得意先名略称"
                Sql += " = '"
                Sql += TxtCustomerShortName.Text
                Sql += "', "
                Sql += "郵便番号"
                Sql += " = '"
                Sql += TxtPostalCode.Text
                Sql += "', "
                Sql += "住所１"
                Sql += " = '"
                Sql += TxtAddress1.Text
                Sql += "', "
                Sql += "住所２"
                Sql += " = '"
                Sql += TxtAddress2.Text
                Sql += "', "
                Sql += "住所３"
                Sql += " = '"
                Sql += TxtAddress3.Text
                Sql += "', "
                Sql += "電話番号"
                Sql += " = '"
                Sql += TxtTel.Text
                Sql += "', "
                Sql += "電話番号検索用"
                Sql += " = '"
                Sql += TxtTelSearch.Text
                Sql += "', "
                Sql += "ＦＡＸ番号"
                Sql += " = '"
                Sql += TxtFax.Text
                Sql += "', "
                Sql += "担当者名"
                Sql += " = '"
                Sql += TxtPerson.Text
                Sql += "', "
                Sql += "担当者役職"
                Sql += " = '"
                Sql += TxtPosition.Text
                Sql += "', "
                Sql += "既定支払条件"
                Sql += " = '"
                Sql += TxtPaymentTerms.Text
                Sql += "', "
                Sql += "メモ"
                Sql += " = '"
                Sql += TxtMemo.Text
                Sql += "', "
                Sql += "会計用得意先コード"
                Sql += " = '"
                Sql += TxtAccountingVendorCode.Text
                Sql += "', "
                Sql += "国内区分"
                Sql += " = '"
                Sql += cmDomesticKbn.SelectedValue.ToString
                Sql += "', "
                Sql += "更新者"
                Sql += " = '"
                Sql += frmC01F10_Login.loginValue.TantoNM
                Sql += "', "
                Sql += "更新日"
                Sql += " = '"
                Sql += dtToday
                Sql += "', is_active="
                Sql += (IIf(ChkActive.Checked, 0, 1)).ToString & " "
                Sql += "WHERE"
                Sql += " 会社コード"
                Sql += "='"
                Sql += frmC01F10_Login.loginValue.BumonCD
                Sql += "'"
                Sql += " AND"
                Sql += " 得意先コード"
                Sql += "='"
                Sql += _customerCode
                Sql += "' "

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
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    '国内区分のコンボボックスを作成
    '編集モードの時は値を渡してセットさせる
    Private Sub createCombobox(Optional ByRef prmVal As String = "")

        cmDomesticKbn.DisplayMember = "Text"
        cmDomesticKbn.ValueMember = "Value"

        Dim dsHanyo As DataSet = getDsHanyoData(CommonConst.DD_CODE)

        Dim dtDD As New DataTable("Table")
        dtDD.Columns.Add("Text", GetType(String))
        dtDD.Columns.Add("Value", GetType(Integer))

        For i As Integer = 0 To dsHanyo.Tables(RS).Rows.Count - 1
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                dtDD.Rows.Add(dsHanyo.Tables(RS).Rows(i)("文字２"), dsHanyo.Tables(RS).Rows(i)("可変キー"))
            Else
                dtDD.Rows.Add(dsHanyo.Tables(RS).Rows(i)("文字１"), dsHanyo.Tables(RS).Rows(i)("可変キー"))
            End If
        Next

        cmDomesticKbn.DataSource = dtDD

        If prmVal IsNot "" Then
            cmDomesticKbn.SelectedValue = prmVal
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
        Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += txtParam
        Return _db.selectDB(Sql, RS, reccnt)
    End Function

    '汎用マスタから固定キー、可変キーに応じた結果を返す
    'param1：String 固定キー
    'param2：String 可変キー
    'Return: DataSet
    Private Function getDsHanyoData(ByVal prmFixed As String, Optional ByVal prmVariable As String = "") As DataSet
        Dim Sql As String = ""

        Sql = " AND "
        Sql += "固定キー ILIKE '" & prmFixed & "'"

        If prmVariable IsNot "" Then
            Sql += " AND "
            Sql += "可変キー ILIKE '" & prmVariable & "'"
        End If

        'リードタイムのリストを汎用マスタから取得
        Return getDsData("m90_hanyo", Sql)
    End Function

End Class