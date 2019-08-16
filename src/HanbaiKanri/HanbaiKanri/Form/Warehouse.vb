Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports System.Globalization

Public Class Warehouse

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

                Sql = "INSERT INTO "
                Sql += "Public."
                Sql += "m20_warehouse("
                Sql += "会社コード, 倉庫コード, 名称, 略称, 郵便番号, 住所１, 住所２, 住所３, 電話番号, ＦＡＸ番号, 保税有無, 備考, 無効フラグ, 更新者, 更新日)"
                Sql += " VALUES('"
                Sql += frmC01F10_Login.loginValue.BumonCD '会社コード
                Sql += "', '"
                Sql += TxtWarehouseCode.Text '倉庫コード
                Sql += "', '"
                Sql += TxtName.Text '名称
                Sql += "', '"
                Sql += TxtShortName.Text '略称
                Sql += "', '"
                Sql += TxtPostalCode.Text '郵便番号
                Sql += "', '"
                Sql += TxtAddress1.Text '住所１
                Sql += "', '"
                Sql += TxtAddress2.Text '住所２
                Sql += "', '"
                Sql += TxtAddress3.Text '住所３
                Sql += "', '"
                Sql += TxtPhone.Text '電話番号
                Sql += "', '"
                Sql += TxtFax.Text 'ＦＡＸ番号
                Sql += "', '"
                Sql += cmCustomsBondKbn.SelectedValue.ToString '保税有無
                Sql += "', '"
                Sql += TxtRemarks.Text '備考
                Sql += "', '"
                Sql += cmbInvalidFlag.SelectedValue.ToString '無効フラグ
                Sql += "', '"
                Sql += frmC01F10_Login.loginValue.TantoNM '更新者
                Sql += "', '"
                Sql += dtToday '更新日
                Sql += "')"

                _db.executeDB(Sql)
            Else

                Sql = "UPDATE "
                Sql += "Public."
                Sql += "m20_warehouse "
                Sql += "SET "
                Sql += "名称"
                Sql += " = '"
                Sql += TxtName.Text
                Sql += "', "
                Sql += "略称"
                Sql += " = '"
                Sql += TxtShortName.Text
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
                Sql += TxtPhone.Text
                Sql += "', "
                Sql += "ＦＡＸ番号"
                Sql += " = '"
                Sql += TxtFax.Text
                Sql += "', "
                Sql += "保税有無"
                Sql += " = '"
                Sql += cmCustomsBondKbn.SelectedValue.ToString
                Sql += "', "
                Sql += "備考"
                Sql += " = '"
                Sql += TxtRemarks.Text
                Sql += "', "
                Sql += "無効フラグ"
                Sql += " = '"
                Sql += cmbInvalidFlag.SelectedValue.ToString
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
                Sql += " 倉庫コード"
                Sql += "='"
                Sql += _AccountCode
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
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '画面表示時
    Private Sub Account_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblWarehouseCode.Text = "WarehouseCode"
            LblName.Text = "Name"
            LblShortName.Text = "ShortName"
            LblPostalCode.Text = "PostalCode"
            LblAddress1.Text = "Address1"
            LblAddress2.Text = "Address2"
            LblAddress3.Text = "Address3"
            LblPhone.Text = "PhoneNumber"
            LblFax.Text = "FAX"
            LblCustomsBondKbn.Text = "PresenceOfBondedBonds"
            cmCustomsBondKbn.Text = "(0:保税なし, 1:保税あり)"
            LblInvalidFlag.Text = "InvalidFlag"
            'InvalidFlag.Text = "(0:True 1:False)"
            LblRemarks.Text = "Remarks"
            btnAddAccount.Text = "Registration"
            btnBack.Text = "Back"
        End If

        If _status = CommonConst.STATUS_EDIT Then

            Dim Sql As String = ""

            Sql = " AND "
            Sql += "倉庫コード"
            Sql += " = "
            Sql += "'"
            Sql += _AccountCode
            Sql += "'"

            Dim ds As DataSet = getDsData("m20_warehouse", Sql)

            If ds.Tables(RS).Rows(0)("倉庫コード") IsNot DBNull.Value Then
                TxtWarehouseCode.Text = ds.Tables(RS).Rows(0)("倉庫コード")
            End If

            If ds.Tables(RS).Rows(0)("名称") IsNot DBNull.Value Then
                TxtName.Text = ds.Tables(RS).Rows(0)("名称")
            End If

            If ds.Tables(RS).Rows(0)("略称") IsNot DBNull.Value Then
                TxtShortName.Text = ds.Tables(RS).Rows(0)("略称")
            End If

            If ds.Tables(RS).Rows(0)("郵便番号") IsNot DBNull.Value Then
                TxtPostalCode.Text = ds.Tables(RS).Rows(0)("郵便番号")
            End If

            If ds.Tables(RS).Rows(0)("住所１") IsNot DBNull.Value Then
                TxtAddress1.Text = ds.Tables(RS).Rows(0)("住所１")
            End If

            If ds.Tables(RS).Rows(0)("住所２") IsNot DBNull.Value Then
                TxtAddress2.Text = ds.Tables(RS).Rows(0)("住所２")
            End If

            If ds.Tables(RS).Rows(0)("住所３") IsNot DBNull.Value Then
                TxtAddress3.Text = ds.Tables(RS).Rows(0)("住所３")
            End If

            If ds.Tables(RS).Rows(0)("電話番号") IsNot DBNull.Value Then
                TxtPhone.Text = ds.Tables(RS).Rows(0)("電話番号")
            End If

            If ds.Tables(RS).Rows(0)("ＦＡＸ番号") IsNot DBNull.Value Then
                TxtFax.Text = ds.Tables(RS).Rows(0)("ＦＡＸ番号")
            End If

            If ds.Tables(RS).Rows(0)("保税有無") IsNot DBNull.Value Then
                customsBondKbnCombobox(ds.Tables(RS).Rows(0)("保税有無"))
            End If

            If ds.Tables(RS).Rows(0)("備考") IsNot DBNull.Value Then
                TxtRemarks.Text = ds.Tables(RS).Rows(0)("備考")
            End If

            If ds.Tables(RS).Rows(0)("無効フラグ") IsNot DBNull.Value Then
                createCombobox(ds.Tables(RS).Rows(0)("無効フラグ"))
            End If

            TxtWarehouseCode.Enabled = False
        Else

            createCombobox()
            customsBondKbnCombobox()

        End If
    End Sub

    '有効無効のコンボボックスを作成
    '編集モードの時は値を渡してセットさせる
    Private Sub createCombobox(Optional ByRef prmVal As String = "")
        cmbInvalidFlag.DisplayMember = "Text"
        cmbInvalidFlag.ValueMember = "Value"

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

        cmbInvalidFlag.DataSource = tb

        If prmVal IsNot "" Then
            cmbInvalidFlag.SelectedValue = prmVal
        End If

    End Sub

    '保税区分のコンボボックスを作成
    '編集モードの時は値を渡してセットさせる
    Private Sub customsBondKbnCombobox(Optional ByRef prmVal As String = "")
        cmCustomsBondKbn.DisplayMember = "Text"
        cmCustomsBondKbn.ValueMember = "Value"

        Dim tb As New DataTable
        tb.Columns.Add("Text", GetType(String))
        tb.Columns.Add("Value", GetType(Integer))

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            tb.Rows.Add(CommonConst.CB_KBN_MOT_AVAILABLE_TXT_ENG, CommonConst.CB_KBN_MOT_AVAILABLE)
            tb.Rows.Add(CommonConst.CB_KBN_AVAILABLE_TXT_ENG, CommonConst.CB_KBN_AVAILABLE)

        Else
            tb.Rows.Add(CommonConst.CB_KBN_MOT_AVAILABLE_TXT, CommonConst.CB_KBN_MOT_AVAILABLE)
            tb.Rows.Add(CommonConst.CB_KBN_AVAILABLE_TXT, CommonConst.CB_KBN_AVAILABLE)

        End If

        cmCustomsBondKbn.DataSource = tb

        If prmVal IsNot "" Then
            cmCustomsBondKbn.SelectedValue = prmVal
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


    '戻るボタン押下時
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

End Class