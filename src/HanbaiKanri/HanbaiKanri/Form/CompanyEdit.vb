Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls

Public Class CompanyEdit
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
    Private srArr() As String
    Private key1 As String
    Private key2 As String

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
                   ByRef prmRefsrArr() As String,
                   ByRef prmRefkey1 As String,
                   ByRef prmRefkey2 As String)
        Call Me.New()

        _init = False

        '初期処理
        srArr = prmRefsrArr
        key1 = prmRefkey1
        key2 = prmRefkey2
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True

    End Sub

    Private Sub SupplierEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CompanyCode.Text = srArr(0)
        CompanyName.Text = srArr(1)
        CompanyShortName.Text = srArr(2)
        PostalCode.Text = srArr(3)
        Address1.Text = srArr(4)
        Address2.Text = srArr(5)
        Address3.Text = srArr(6)
        Tel.Text = srArr(7)
        Fax.Text = srArr(8)
        RepresentativePosition.Text = srArr(9)
        RepresentativeName.Text = srArr(10)
        DisplayOrder.Text = srArr(11)
        Remarks.Text = srArr(12)
        BankCode.Text = srArr(13)
        BranchOfficeCode.Text = srArr(14)
        DepositCategory.Text = srArr(15)
        AccountNumber.Text = srArr(16)
        AccountName.Text = srArr(17)
    End Sub

    Private Sub BtnEditCompany_Click(sender As Object, e As EventArgs) Handles BtnEditCompany.Click
        Dim dtToday As DateTime = DateTime.Now
        Try
            Dim Sql As String = ""

            Sql = ""
            Sql += "UPDATE "
            Sql += "Public."
            Sql += "m01_company "
            Sql += "SET "
            Sql += " 会社コード"
            Sql += " = '"
            Sql += CompanyCode.Text
            Sql += "', "
            Sql += "会社名"
            Sql += " = '"
            Sql += CompanyName.Text
            Sql += "', "
            Sql += "会社略称"
            Sql += " = '"
            Sql += CompanyShortName.Text
            Sql += "', "
            Sql += "郵便番号"
            Sql += " = '"
            Sql += PostalCode.Text
            Sql += "', "
            Sql += "住所１"
            Sql += " = '"
            Sql += Address1.Text
            Sql += "', "
            Sql += "住所２"
            Sql += " = '"
            Sql += Address2.Text
            Sql += "', "
            Sql += "住所３"
            Sql += " = '"
            Sql += Address3.Text
            Sql += "', "
            Sql += "電話番号"
            Sql += " = '"
            Sql += Tel.Text
            Sql += "', "
            Sql += "ＦＡＸ番号"
            Sql += " = '"
            Sql += Fax.Text
            Sql += "', "
            Sql += "代表者役職"
            Sql += " = '"
            Sql += RepresentativePosition.Text
            Sql += "', "
            Sql += "代表者名"
            Sql += " = '"
            Sql += RepresentativeName.Text
            Sql += "', "
            Sql += "表示順"
            Sql += " = '"
            Sql += DisplayOrder.Text
            Sql += "', "
            Sql += "備考"
            Sql += " = '"
            Sql += Remarks.Text
            Sql += "', "
            Sql += "銀行コード"
            Sql += " = '"
            Sql += BankCode.Text
            Sql += "', "
            Sql += "支店コード"
            Sql += " = '"
            Sql += BranchOfficeCode.Text
            Sql += "', "
            Sql += "預金種目"
            Sql += " = '"
            Sql += DepositCategory.Text
            Sql += "', "
            Sql += "口座番号"
            Sql += " = '"
            Sql += AccountNumber.Text
            Sql += "', "
            Sql += "口座名義"
            Sql += " = '"
            Sql += AccountName.Text
            Sql += "', "
            Sql += "更新者"
            Sql += " = '"
            Sql += "Admin"
            Sql += "', "
            Sql += "更新日"
            Sql += " = '"
            Sql += dtToday
            Sql += "' "
            Sql += "WHERE"
            Sql += " 会社コード"
            Sql += "='"
            Sql += key1
            Sql += "'"
            Sql += "RETURNING 会社コード"
            Sql += ", "
            Sql += "会社名"
            Sql += ", "
            Sql += "会社略称"
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
            Sql += "ＦＡＸ番号"
            Sql += ", "
            Sql += "代表者役職"
            Sql += ", "
            Sql += "代表者名"
            Sql += ", "
            Sql += "表示順"
            Sql += ", "
            Sql += "備考"
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

            Dim MstCompany As MstCompany
            MstCompany = New MstCompany(_msgHd, _db, _langHd)
            MstCompany.Show()
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
        Dim MstCompany As MstCompany
        MstCompany = New MstCompany(_msgHd, _db, _langHd)
        MstCompany.Show()
        Me.Close()
    End Sub
End Class