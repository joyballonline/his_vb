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
                   ByRef prmRefStatus As String,
                   Optional ByRef prmRefCompany As String = "",
                   Optional ByRef prmRefSupplier As String = "")
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _status = prmRefStatus
        _companyCode = prmRefCompany
        _supplierCode = prmRefSupplier

        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示

        _init = True

    End Sub

    Private Sub btnAddSupplier_Click(sender As Object, e As EventArgs) Handles BtnRegistration.Click
        Dim dtToday As DateTime = DateTime.Now


        Try
            If _status = "ADD" Then
                Dim Sql As String = ""

                Sql = ""
                Sql += "INSERT INTO "
                Sql += "Public."
                Sql += "m11_supplier("
                Sql += "会社コード, 仕入先コード, 仕入先名, 仕入先名略称, 郵便番号, 住所１, 住所２, 住所３, 電話番号, 電話番号検索用, ＦＡＸ番号, 担当者名, 担当者役職, 既定間接費率, メモ, 銀行コード, 支店コード, 預金種目, 口座番号, 口座名義,  更新者, 更新日)"
                Sql += " VALUES('"
                Sql += frmC01F10_Login.loginValue.BumonNM
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
                If TxtCosts.Text = "" Then
                    Sql += "0"
                Else
                    Sql += TxtCosts.Text
                End If
                Sql += "', '"
                Sql += TxtMemo.Text
                Sql += "', '"
                Sql += TxtBankCode.Text
                Sql += "', '"
                Sql += TxtBranchOfficeCode.Text
                Sql += "', '"
                Sql += TxtDepositCategory.Text
                Sql += "', '"
                Sql += TxtAccountNumber.Text
                Sql += "', '"
                Sql += TxtAccountName.Text
                Sql += "', '"
                Sql += frmC01F10_Login.loginValue.TantoNM
                Sql += "', '"
                Sql += dtToday
                Sql += " ')"
                Sql += "RETURNING 会社コード"
                Sql += ", "
                Sql += "仕入先コード"
                Sql += ", "
                Sql += "仕入先名"
                Sql += ", "
                Sql += "仕入先名略称"
                Sql += ", "
                Sql += "郵便番号"
                Sql += ", "
                Sql += "住所１"
                Sql += ", "
                Sql += "住所２"
                Sql += ", "
                Sql += "住所３"
                Sql += ", "
                Sql += "電話番号"
                Sql += ", "
                Sql += "電話番号検索用"
                Sql += ", "
                Sql += "ＦＡＸ番号"
                Sql += ", "
                Sql += "担当者名"
                Sql += ", "
                Sql += "担当者役職"
                Sql += ", "
                Sql += "既定間接費率"
                Sql += ", "
                Sql += "メモ"
                Sql += ", "
                Sql += "銀行コード"
                Sql += ", "
                Sql += "支店コード"
                Sql += ", "
                Sql += "預金種目"
                Sql += ", "
                Sql += "口座番号"
                Sql += ", "
                Sql += "口座名義"
                Sql += ", "
                Sql += "更新者"
                Sql += ", "
                Sql += "更新日"

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
                Sql += "既定間接費率"
                Sql += " = '"
                Sql += TxtCosts.Text
                Sql += "', "
                Sql += "メモ"
                Sql += " = '"
                Sql += TxtMemo.Text
                Sql += "', "
                Sql += "銀行コード"
                Sql += " = '"
                Sql += TxtBankCode.Text
                Sql += "', "
                Sql += "支店コード"
                Sql += " = '"
                Sql += TxtBranchOfficeCode.Text
                Sql += "', "
                Sql += "預金種目"
                Sql += " = '"
                Sql += TxtDepositCategory.Text
                Sql += "', "
                Sql += "口座番号"
                Sql += " = '"
                Sql += TxtAccountNumber.Text
                Sql += "', "
                Sql += "口座名義"
                Sql += " = '"
                Sql += TxtAccountName.Text
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
                Sql += "RETURNING 会社コード"
                Sql += ", "
                Sql += "仕入先コード"
                Sql += ", "
                Sql += "仕入先名"
                Sql += ", "
                Sql += "仕入先名略称"
                Sql += ", "
                Sql += "郵便番号"
                Sql += ", "
                Sql += "住所１"
                Sql += ", "
                Sql += "住所２"
                Sql += ", "
                Sql += "住所３"
                Sql += ", "
                Sql += "電話番号"
                Sql += ", "
                Sql += "電話番号検索用"
                Sql += ", "
                Sql += "ＦＡＸ番号"
                Sql += ", "
                Sql += "担当者名"
                Sql += ", "
                Sql += "担当者役職"
                Sql += ", "
                Sql += "既定間接費率"
                Sql += ", "
                Sql += "メモ"
                Sql += ", "
                Sql += "銀行コード"
                Sql += ", "
                Sql += "支店コード"
                Sql += ", "
                Sql += "預金種目"
                Sql += ", "
                Sql += "口座番号"
                Sql += ", "
                Sql += "口座名義"
                Sql += ", "
                Sql += "更新者"
                Sql += ", "
                Sql += "更新日"

                _db.executeDB(Sql)
            End If

            Dim frmMC As MstSupplier
            frmMC = New MstSupplier(_msgHd, _db, _langHd)
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

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        Dim MstSupplier As MstSupplier
        MstSupplier = New MstSupplier(_msgHd, _db, _langHd)
        MstSupplier.Show()
        Me.Close()
    End Sub

    Private Sub Supplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If _status = "EDIT" Then
            Dim Sql As String = ""

            Sql += "SELECT "
            Sql += "会社コード, "
            Sql += "仕入先コード, "
            Sql += "仕入先名, "
            Sql += "仕入先名略称, "
            Sql += "郵便番号, "
            Sql += "住所１, "
            Sql += "住所２, "
            Sql += "住所３, "
            Sql += "電話番号, "
            Sql += "電話番号検索用, "
            Sql += "ＦＡＸ番号, "
            Sql += "担当者名, "
            Sql += "担当者役職, "
            Sql += "既定間接費率, "
            Sql += "メモ, "
            Sql += "銀行コード, "
            Sql += "支店コード, "
            Sql += "預金種目, "
            Sql += "口座番号, "
            Sql += "口座名義, "
            Sql += "更新者, "
            Sql += "更新日 "
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

            TxtCompanyCode.Text = ds.Tables(RS).Rows(0)("会社コード")
            TxtSupplierCode.Text = ds.Tables(RS).Rows(0)("仕入先コード")
            TxtSupplierName.Text = ds.Tables(RS).Rows(0)("仕入先名")
            TxtSupplierShortName.Text = ds.Tables(RS).Rows(0)("仕入先名略称")
            TxtPostalCode.Text = ds.Tables(RS).Rows(0)("郵便番号")
            TxtAddress1.Text = ds.Tables(RS).Rows(0)("住所１")
            TxtAddress2.Text = ds.Tables(RS).Rows(0)("住所２")
            TxtAddress3.Text = ds.Tables(RS).Rows(0)("住所３")
            TxtTel.Text = ds.Tables(RS).Rows(0)("電話番号")
            TxtTelSearch.Text = ds.Tables(RS).Rows(0)("電話番号検索用")
            TxtFax.Text = ds.Tables(RS).Rows(0)("ＦＡＸ番号")
            TxtPerson.Text = ds.Tables(RS).Rows(0)("担当者名")
            TxtPosition.Text = ds.Tables(RS).Rows(0)("担当者役職")
            TxtCosts.Text = ds.Tables(RS).Rows(0)("既定間接費率")
            TxtMemo.Text = ds.Tables(RS).Rows(0)("メモ")
            TxtBankCode.Text = ds.Tables(RS).Rows(0)("銀行コード")
            TxtBranchOfficeCode.Text = ds.Tables(RS).Rows(0)("支店コード")
            TxtDepositCategory.Text = ds.Tables(RS).Rows(0)("預金種目")
            TxtAccountNumber.Text = ds.Tables(RS).Rows(0)("口座番号")
            TxtAccountName.Text = ds.Tables(RS).Rows(0)("口座名義")
        End If
    End Sub
End Class