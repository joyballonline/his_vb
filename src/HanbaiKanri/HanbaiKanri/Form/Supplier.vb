Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class Supplier
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
    Private _status As String
    Private _companyCode As String
    Private _supplierCode As String

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
                   Optional ByRef prmRefSupplier As String = "")
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _parentForm = prmRefForm
        _status = prmRefStatus
        _companyCode = prmRefCompany
        _supplierCode = prmRefSupplier

        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True

    End Sub

    Private Sub Supplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblSupplierCode.Text = "SupplierCode"
            LblSupplierName.Text = "SupplierName"
            LblShortName.Text = "ShortName"
            LblPostalCode.Text = "PostalCode"
            LblAddress1.Text = "Address1"
            LblAddress2.Text = "Address2"
            Lbladdress3.Text = "Address3"
            LblTel.Text = "TEL"
            LblFax.Text = "FAX"
            LblPerson.Text = "Person"
            LblPosition.Text = "Position"
            LblTariffRate.Text = "CustomsDutyRate"
            LblPph.Text = "PPH"
            LblTransportationCost.Text = "ShippingCost"
            LblMemo.Text = "Memo"
            LblBankName.Text = "BankName"
            LblBankCode.Text = "BankCode"
            LblBranchCode.Text = "BranchCode"
            LblBranchName.Text = "BranchName"
            LblDepositCategory.Text = "DepositCategory"
            LblAccountNumber.Text = "AccountNumber"
            LblAccountHolder.Text = "AccountHolder"
            LblAccountingVendorCode.Text = "AccountingVendorCode"

            LblSupplierCodeText.Text = "(Non-Overlapping string)"
            LblPostalCodeText.Text = "(Example:0123456)"
            LblTelText.Text = "(Example:0123456789)"
            LblFaxText.Text = "(Example:0123456789)"
            LblTariffRateText.Text = "(Example:0.01)"
            LblTransportationCostText.Text = "(Example:0.1)"
            LblPphText.Text = "(Example:0.025)"
            LblBankCodeText.Text = "(Example:012)"
            LblBranchCodeText.Text = "(Example:012)"
            LblAccountNumberText.Text = "(Example:0123456)"

            BtnRegistration.Text = "Registration"
            BtnBack.Text = "Back"
        End If

        createCombobox()

        If _status = CommonConst.STATUS_EDIT Then

            TxtSupplierCode.Enabled = False '変更不可

            Dim Sql As String = ""

            Sql += "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "m11_supplier"
            Sql += " WHERE "
            Sql += "会社コード"
            Sql += " ILIKE "
            Sql += "'"
            Sql += _companyCode
            Sql += "'"
            Sql += " AND "
            Sql += "仕入先コード"
            Sql += " ILIKE "
            Sql += "'"
            Sql += _supplierCode
            Sql += "'"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            If ds.Tables(RS).Rows(0)("会社コード") Is DBNull.Value Then
            Else
                TxtCompanyCode.Text = ds.Tables(RS).Rows(0)("会社コード")
            End If

            If ds.Tables(RS).Rows(0)("仕入先コード") Is DBNull.Value Then
            Else
                TxtSupplierCode.Text = ds.Tables(RS).Rows(0)("仕入先コード")
            End If

            If ds.Tables(RS).Rows(0)("仕入先名") Is DBNull.Value Then
            Else
                TxtSupplierName.Text = ds.Tables(RS).Rows(0)("仕入先名")
            End If

            If ds.Tables(RS).Rows(0)("仕入先名略称") Is DBNull.Value Then
            Else
                TxtSupplierShortName.Text = ds.Tables(RS).Rows(0)("仕入先名略称")
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

            If ds.Tables(RS).Rows(0)("関税率") Is DBNull.Value Then
            Else
                TxtTariffRate.Text = ds.Tables(RS).Rows(0)("関税率")
            End If

            If ds.Tables(RS).Rows(0)("前払法人税率") Is DBNull.Value Then
            Else
                TxtPph.Text = ds.Tables(RS).Rows(0)("前払法人税率")
            End If

            If ds.Tables(RS).Rows(0)("輸送費率") Is DBNull.Value Then
            Else
                TxtTransportationCost.Text = ds.Tables(RS).Rows(0)("輸送費率")
            End If

            If ds.Tables(RS).Rows(0)("メモ") Is DBNull.Value Then
            Else
                TxtMemo.Text = ds.Tables(RS).Rows(0)("メモ")
            End If

            If ds.Tables(RS).Rows(0)("銀行名") Is DBNull.Value Then
            Else
                TxtBankName.Text = ds.Tables(RS).Rows(0)("銀行名")
            End If

            If ds.Tables(RS).Rows(0)("銀行コード") Is DBNull.Value Then
            Else
                TxtBankCode.Text = ds.Tables(RS).Rows(0)("銀行コード")
            End If

            If ds.Tables(RS).Rows(0)("支店名") Is DBNull.Value Then
            Else
                TxtBranchName.Text = ds.Tables(RS).Rows(0)("支店名")
            End If

            If ds.Tables(RS).Rows(0)("支店コード") Is DBNull.Value Then
            Else
                TxtBranchOfficeCode.Text = ds.Tables(RS).Rows(0)("支店コード")
            End If

            If ds.Tables(RS).Rows(0)("預金種目") Is DBNull.Value Then
            Else
                createCombobox(ds.Tables(RS).Rows(0)("預金種目"))
            End If

            If ds.Tables(RS).Rows(0)("口座番号") Is DBNull.Value Then
            Else
                TxtAccountNumber.Text = ds.Tables(RS).Rows(0)("口座番号")
            End If

            If ds.Tables(RS).Rows(0)("口座名義") Is DBNull.Value Then
            Else
                TxtAccountName.Text = ds.Tables(RS).Rows(0)("口座名義")
            End If

            If ds.Tables(RS).Rows(0)("会計用仕入先コード") Is DBNull.Value Then
            Else
                TxtAccountingVendorCode.Text = ds.Tables(RS).Rows(0)("会計用仕入先コード")
            End If

        End If
    End Sub

    Private Sub btnAddSupplier_Click(sender As Object, e As EventArgs) Handles BtnRegistration.Click
        '項目チェック
        Dim strMessage As String = ""    'メッセージ本文
        Dim strMessageTitle As String = ""      'メッセージタイトル
        ''得意先コードは必須
        If TxtSupplierCode.Text = "" Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                strMessage = "Please enter Supplier Code. "
                strMessageTitle = "SupplierCode Error"
            Else
                strMessage = "仕入先コードを入力してください。"
                strMessageTitle = "仕入先コード入力エラー"
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
                Sql += "m11_supplier("
                Sql += "会社コード, 仕入先コード, 仕入先名, 仕入先名略称, 郵便番号, 住所１, 住所２, 住所３, 電話番号, 電話番号検索用, ＦＡＸ番号, 担当者名, 担当者役職, 関税率, 前払法人税率, 輸送費率, メモ, 銀行名, 銀行コード, 支店名, 支店コード, 預金種目, 口座番号, 口座名義, 会計用仕入先コード, 更新者, 更新日)"
                Sql += " VALUES('"
                Sql += frmC01F10_Login.loginValue.BumonCD
                Sql += "', '"
                Sql += TxtSupplierCode.Text
                Sql += "', '"
                Sql += TxtSupplierName.Text
                Sql += "', '"
                Sql += TxtSupplierShortName.Text
                Sql += "', '"
                Sql += TxtPostalCode.Text
                Sql += "', '"
                Sql += TxtAddress1.Text
                Sql += "', '"
                Sql += TxtAddress2.Text
                Sql += "', '"
                Sql += TxtAddress3.Text
                Sql += "', '"
                Sql += TxtTel.Text
                Sql += "', '"
                Sql += TxtTelSearch.Text
                Sql += "', '"
                Sql += TxtFax.Text
                Sql += "', '"
                Sql += TxtPerson.Text
                Sql += "', '"
                Sql += TxtPosition.Text
                Sql += "', '"
                If TxtTariffRate.Text = "" Then
                    Sql += "0"
                Else
                    Sql += UtilClass.formatNumber(TxtTariffRate.Text)
                End If
                Sql += "', '"
                If TxtPph.Text = "" Then
                    Sql += "0"
                Else
                    Sql += UtilClass.formatNumber(TxtPph.Text)
                End If
                Sql += "', '"
                If TxtTransportationCost.Text = "" Then
                    Sql += "0"
                Else
                    Sql += UtilClass.formatNumber(TxtTransportationCost.Text)
                End If
                Sql += "', '"
                Sql += TxtMemo.Text
                Sql += "', '"
                Sql += TxtBankName.Text
                Sql += "', '"
                Sql += TxtBankCode.Text
                Sql += "', '"
                Sql += TxtBranchName.Text
                Sql += "', '"
                Sql += TxtBranchOfficeCode.Text
                Sql += "', '"
                Sql += cmDCKbn.SelectedValue.ToString
                Sql += "', '"
                Sql += TxtAccountNumber.Text
                Sql += "', '"
                Sql += TxtAccountName.Text
                Sql += "', '"
                Sql += TxtAccountingVendorCode.Text
                Sql += "', '"
                Sql += frmC01F10_Login.loginValue.TantoNM
                Sql += "', '"
                Sql += dtToday
                Sql += "')"

                _db.executeDB(Sql)
            Else
                Dim Sql As String = ""

                Sql = ""
                Sql += "UPDATE "
                Sql += "Public."
                Sql += "m11_Supplier "
                Sql += "SET "
                Sql += " 会社コード"
                Sql += " = '"
                Sql += TxtCompanyCode.Text
                Sql += "', "
                Sql += "仕入先コード"
                Sql += " = '"
                Sql += TxtSupplierCode.Text
                Sql += "', "
                Sql += "仕入先名"
                Sql += " = '"
                Sql += TxtSupplierName.Text
                Sql += "', "
                Sql += "仕入先名略称"
                Sql += " = '"
                Sql += TxtSupplierShortName.Text
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
                Sql += "関税率"
                Sql += " = '"
                Sql += UtilClass.formatNumber(TxtTariffRate.Text)
                Sql += "', "
                Sql += "前払法人税率"
                Sql += " = '"
                Sql += UtilClass.formatNumber(TxtPph.Text)
                Sql += "', "
                Sql += "輸送費率"
                Sql += " = '"
                Sql += UtilClass.formatNumber(TxtTransportationCost.Text)
                Sql += "', "
                Sql += "メモ"
                Sql += " = '"
                Sql += TxtMemo.Text
                Sql += "', "
                Sql += "銀行名"
                Sql += " = '"
                Sql += TxtBankName.Text
                Sql += "', "
                Sql += "銀行コード"
                Sql += " = '"
                Sql += TxtBankCode.Text
                Sql += "', "
                Sql += "支店名"
                Sql += " = '"
                Sql += TxtBranchName.Text
                Sql += "', "
                Sql += "支店コード"
                Sql += " = '"
                Sql += TxtBranchOfficeCode.Text
                Sql += "', "
                Sql += "預金種目"
                Sql += " = '"
                Sql += cmDCKbn.SelectedValue.ToString
                Sql += "', "
                Sql += "口座番号"
                Sql += " = '"
                Sql += TxtAccountNumber.Text
                Sql += "', "
                Sql += "口座名義"
                Sql += " = '"
                Sql += TxtAccountName.Text
                Sql += "', "
                Sql += "会計用仕入先コード"
                Sql += " = '"
                Sql += TxtAccountingVendorCode.Text
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
                Sql += " 仕入先コード"
                Sql += "='"
                Sql += _supplierCode
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

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    '有効無効のコンボボックスを作成
    '編集モードの時は値を渡してセットさせる
    Private Sub createCombobox(Optional ByRef prmVal As String = "")

        cmDCKbn.DisplayMember = "Text"
        cmDCKbn.ValueMember = "Value"

        Dim dsHanyo As DataSet = getDsHanyoData(CommonConst.DC_CODE)

        Dim dtDC As New DataTable("Table")
        dtDC.Columns.Add("Text", GetType(String))
        dtDC.Columns.Add("Value", GetType(Integer))

        For i As Integer = 0 To dsHanyo.Tables(RS).Rows.Count - 1
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                dtDC.Rows.Add(dsHanyo.Tables(RS).Rows(i)("文字２"), dsHanyo.Tables(RS).Rows(i)("可変キー"))
            Else
                dtDC.Rows.Add(dsHanyo.Tables(RS).Rows(i)("文字１"), dsHanyo.Tables(RS).Rows(i)("可変キー"))
            End If
        Next

        cmDCKbn.DataSource = dtDC

        If prmVal IsNot "" Then
            cmDCKbn.SelectedValue = prmVal
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