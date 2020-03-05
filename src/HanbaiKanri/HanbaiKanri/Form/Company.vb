Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class Company
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
                   Optional ByRef prmRefCompany As String = "")
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _parentForm = prmRefForm
        _status = prmRefStatus
        _companyCode = prmRefCompany
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True

    End Sub

    Public Class ComboBoxItem

        Private m_id As String = ""
        Private m_name As String = ""

        '実際の値
        '（ValueMemberに設定する文字列と同名にする）
        Public Property ID() As String
            Set(ByVal value As String)
                m_id = value
            End Set
            Get
                Return m_id
            End Get
        End Property

        '表示名称
        '（DisplayMemberに設定する文字列と同名にする）
        Public Property NAME() As String
            Set(ByVal value As String)
                m_name = value
            End Set
            Get
                Return m_name
            End Get
        End Property

    End Class

    Private Sub Company_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then

            Label1.Text = "CompanyCode"
            Label2.Text = "CompanyName"
            Label3.Text = "ShortName"
            Label5.Text = "PostalCode"
            Label6.Text = "Address1"
            Label7.Text = "Address2"
            Label8.Text = "Address3"
            Label9.Text = "PhoneNumber"
            Label11.Text = "FAX"
            Label4.Text = "Position"
            Label12.Text = "Name"
            Label13.Text = "DisplayOrder"
            Label14.Text = "Remarks"
            Label32.Text = "BankName"
            Label34.Text = "BankCode"
            Label18.Text = "BranchName"
            Label17.Text = "BranchCode"
            Label16.Text = "Category"
            Label15.Text = "AccountNumber"
            Label19.Text = "AccountHolder"
            Label30.Text = "EvaluationMethod"
            Label29.Text = "PPH"
            Label28.Text = "AccountingCode"

            Label26.Text = "(Non-Overlapping string)"
            Label21.Text = "(Example:123456)"
            Label10.Text = "(Example:123456789)"
            Label20.Text = "(Example:123456789)"
            Label22.Text = "(Example:0123)"
            Label23.Text = "(Example:012)"
            Label25.Text = "(Example:123456)"
            Label24.Text = "(Example:0.025)"
            Label31.Text = "(Company code in accurate)"

            BtnRegistration.Text = "Registration"
            BtnBack.Text = "Back"
        End If

        Dim Sql1 As String = ""

        Sql1 += "SELECT * FROM public.m90_hanyo"
        Sql1 += " WHERE 会社コード ='" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql1 += " AND 固定キー ='" & CommonConst.IP_CODE & "'"

        Dim reccnt As Integer = 0
        Dim ds1 As DataSet = _db.selectDB(Sql1, RS, reccnt)

        Dim EvaluationCount As Integer = ds1.Tables(RS).Rows.Count - 1
        Dim Evaluation(EvaluationCount) As ComboBoxItem

        For i As Integer = 0 To ds1.Tables(RS).Rows.Count - 1
            Evaluation(i) = New ComboBoxItem
            Evaluation(i).ID = (ds1.Tables(RS).Rows(i)("可変キー"))
            Evaluation(i).NAME = (ds1.Tables(RS).Rows(i)("文字１"))
        Next

        CbEvaluation.DisplayMember = "NAME"
        CbEvaluation.ValueMember = "ID"

        CbEvaluation.DataSource = Evaluation
        CbEvaluation.SelectedIndex = 0

        createCombobox()

        If _status = CommonConst.STATUS_EDIT Then
            TxtCompanyCode.Enabled = False

            Dim Sql As String = ""

            Sql += "SELECT * FROM public.m01_company"
            Sql += " WHERE 会社コード = '" & _companyCode & "'"


            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)
            If ds.Tables(RS).Rows(0)("会社コード") Is DBNull.Value Then
            Else
                TxtCompanyCode.Text = ds.Tables(RS).Rows(0)("会社コード")
            End If

            If ds.Tables(RS).Rows(0)("会社名") Is DBNull.Value Then
            Else
                TxtCompanyName.Text = ds.Tables(RS).Rows(0)("会社名")
            End If

            If ds.Tables(RS).Rows(0)("会社略称") Is DBNull.Value Then
            Else
                TxtCompanyShortName.Text = ds.Tables(RS).Rows(0)("会社略称")
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

            If ds.Tables(RS).Rows(0)("ＦＡＸ番号") Is DBNull.Value Then
            Else
                TxtFax.Text = ds.Tables(RS).Rows(0)("ＦＡＸ番号")
            End If

            If ds.Tables(RS).Rows(0)("代表者役職") Is DBNull.Value Then
            Else
                TxtRepresentativePosition.Text = ds.Tables(RS).Rows(0)("代表者役職")
            End If

            If ds.Tables(RS).Rows(0)("代表者名") Is DBNull.Value Then
            Else
                TxtRepresentativeName.Text = ds.Tables(RS).Rows(0)("代表者名")
            End If

            If ds.Tables(RS).Rows(0)("表示順") Is DBNull.Value Then
            Else
                TxtDisplayOrder.Text = ds.Tables(RS).Rows(0)("表示順")
            End If

            If ds.Tables(RS).Rows(0)("備考") Is DBNull.Value Then
            Else
                TxtRemarks.Text = ds.Tables(RS).Rows(0)("備考")
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

            If ds.Tables(RS).Rows(0)("前払法人税率") Is DBNull.Value Then
            Else
                TxtPph.Text = ds.Tables(RS).Rows(0)("前払法人税率")
            End If

            If ds.Tables(RS).Rows(0)("在庫単価評価法") Is DBNull.Value Then
            Else
                Dim tmp = ds.Tables(RS).Rows(0)("在庫単価評価法").ToString
                CbEvaluation.SelectedValue = tmp
            End If

            If ds.Tables(RS).Rows(0)("会計用コード") Is DBNull.Value Then
            Else
                Dim tmp = ds.Tables(RS).Rows(0)("会計用コード").ToString
                TxtBranchCode.Text = tmp
            End If

        End If
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    Private Sub btnAddCompany_Click_1(sender As Object, e As EventArgs) Handles BtnRegistration.Click

        '入力チェック  2020.03.05
        Dim blnFlg As Boolean = mCheck
        If blnFlg = False Then
            Exit Sub
        End If


        Dim dtToday As String = UtilClass.formatDatetime(DateTime.Now)
        Try
            If _status = CommonConst.STATUS_ADD Then
                Dim Sql As String = ""

                Sql += "INSERT INTO Public.m01_company ( "
                Sql += "会社コード, 会社名, 会社略称, 郵便番号, 住所１, 住所２, 住所３, 電話番号, ＦＡＸ番号, 代表者役職, 代表者名, 表示順, 備考, 銀行名, 銀行コード, 支店名, 支店コード, 預金種目, 口座番号, 口座名義, 在庫単価評価法, 前払法人税率, 会計用コード, 更新者, 更新日"
                Sql += " ) VALUES("
                Sql += " '" & TxtCompanyCode.Text & "'"     '会社コード
                Sql += ", '" & TxtCompanyName.Text & "'"    '会社名
                Sql += ", '" & TxtCompanyShortName.Text & "'"   '会社略称
                Sql += ", '" & TxtPostalCode.Text & "'"     '郵便番号
                Sql += ", '" & TxtAddress1.Text & "'"       '住所１
                Sql += ", '" & TxtAddress2.Text & "'"       '住所２
                Sql += ", '" & TxtAddress3.Text & "'"       '住所３
                Sql += ", '" & TxtTel.Text & "'"            '電話番号
                Sql += ", '" & TxtFax.Text & "'"            'ＦＡＸ番号
                Sql += ", '" & TxtRepresentativePosition.Text & "'"     '代表者役職
                Sql += ", '" & TxtRepresentativeName.Text & "'"     '代表者名
                Sql += ", "        '表示順
                If TxtDisplayOrder.Text = "" Then
                    Sql += "0"
                Else
                    Sql += TxtDisplayOrder.Text
                End If
                Sql += ", '" & TxtRemarks.Text & "'"        '備考
                Sql += ", '" & TxtBankName.Text & "'"      '銀行名
                Sql += ", '" & TxtBankCode.Text & "'"      '銀行コード
                Sql += ", '" & TxtBranchName.Text & "'"    '支店名
                Sql += ", '" & TxtBranchOfficeCode.Text & "'"       '支店コード
                Sql += ", '" & cmDCKbn.SelectedValue.ToString & "'"        '預金種目
                Sql += ", '" & TxtAccountNumber.Text & "'"          '口座番号
                Sql += ", '" & TxtAccountName.Text & "'"            '口座名義
                Sql += ", '" & CbEvaluation.SelectedValue.ToString & "'"        '在庫単価評価法
                Sql += ", "        '前払法人税率
                If TxtPph.Text = "" Then
                    Sql += "0"
                Else
                    Sql += UtilClass.formatNumber(TxtPph.Text)
                End If
                Sql += ", '" & TxtBranchCode.Text & "'"     '会計用コード
                Sql += ", '" & frmC01F10_Login.loginValue.TantoNM & "'"     '更新者
                Sql += ", '" & dtToday & "'"        '更新日
                Sql += " )"

                _db.executeDB(Sql)
            Else
                Dim Sql As String = ""
                Sql += "UPDATE Public.m01_company "
                Sql += "SET "
                Sql += " 会社コード = '" & TxtCompanyCode.Text & "'"
                Sql += ", 会社名 = '" & TxtCompanyName.Text & "'"
                Sql += ", 会社略称 = '" & TxtCompanyShortName.Text & "'"
                Sql += ", 郵便番号 = '" & TxtPostalCode.Text & "'"
                Sql += ", 住所１ = '" & TxtAddress1.Text & "'"
                Sql += ", 住所２ = '" & TxtAddress2.Text & "'"
                Sql += ", 住所３ = '" & TxtAddress3.Text & "'"
                Sql += ", 電話番号 = '" & TxtTel.Text & "'"
                Sql += ", ＦＡＸ番号 = '" & TxtFax.Text & "'"
                Sql += ", 代表者役職 = '" & TxtRepresentativePosition.Text & "'"
                Sql += ", 代表者名 = '" & TxtRepresentativeName.Text & "'"
                Sql += ", 表示順 = "
                If TxtDisplayOrder.Text = "" Then
                    Sql += "0"
                Else
                    Sql += TxtDisplayOrder.Text
                End If
                Sql += ", 備考 = '" & TxtRemarks.Text & "'"
                Sql += ", 銀行名 = '" & TxtBankName.Text & "'"
                Sql += ", 銀行コード = '" & TxtBankCode.Text & "'"
                Sql += ", 支店名 = '" & TxtBranchName.Text & "'"
                Sql += ", 支店コード = '" & TxtBranchOfficeCode.Text & "'"
                Sql += ", 預金種目 = '" & cmDCKbn.SelectedValue.ToString & "'"
                Sql += ", 口座番号 = '" & TxtAccountNumber.Text & "'"
                Sql += ", 口座名義 = '" & TxtAccountName.Text & "'"
                Sql += ", 在庫単価評価法 = " & CbEvaluation.SelectedValue.ToString
                Sql += ", 前払法人税率 = " & UtilClass.formatNumber(TxtPph.Text)
                Sql += ", 会計用コード = '" & TxtBranchCode.Text & "'"
                Sql += ", 更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
                Sql += ", 更新日 = '" & dtToday & "'"
                Sql += "WHERE 会社コード ='" & _companyCode & "'"

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


    Private Function mCheck() As Boolean  '2020.03.05

        Dim strMessage As String = ""    'メッセージ本文
        Dim strMessageTitle As String = ""      'メッセージタイトル
        ''会社コードは必須
        If TxtCompanyCode.Text = "" Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                strMessage = "Please enter Company Code. "
                strMessageTitle = "CompanyCode Error"
            Else
                strMessage = "会社コードを入力してください。"
                strMessageTitle = "会社コード入力エラー"
            End If
            Dim result As DialogResult = MessageBox.Show(strMessage, strMessageTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Function
        End If
        '表示順の属性チェック（表示順のみ数値項目）
        If Not IsNumeric(TxtDisplayOrder.Text) And Not TxtDisplayOrder.Text = "" Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                strMessage = "Please enter with numeric value. "
                strMessageTitle = "DisplayOrder Error"
            Else
                strMessage = "数値で入力してください。"
                strMessageTitle = "表示順入力エラー"
            End If
            Dim result As DialogResult = MessageBox.Show(strMessage, strMessageTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Function
        End If

        '前払法人税率
        If IsNumeric(TxtPph.Text) = False Then

            _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
            TxtPph.Select()
            Exit Function
        End If

        mCheck = True

    End Function


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