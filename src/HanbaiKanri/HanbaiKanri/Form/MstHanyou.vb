Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class MstHanyou
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
    'Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ

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
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示

        _init = True

    End Sub

    Private Sub MstHanyoue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Sql As String = ""
        Try
            Sql += "SELECT "
            Sql += "会社コード, "
            Sql += "会社名, "
            Sql += "会社略称, "
            Sql += "郵便番号, "
            Sql += "住所１, "
            Sql += "住所２, "
            Sql += "住所３, "
            Sql += "電話番号, "
            Sql += "ＦＡＸ番号, "
            Sql += "代表者役職, "
            Sql += "代表者名, "
            Sql += "表示順, "
            Sql += "備考, "
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
            Sql += "m01_company"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                Dgv_Company.Rows.Add()
                Dgv_Company.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)(0)        '会社コード
                Dgv_Company.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)(1)        '言語コード
                Dgv_Company.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)(2)        '氏名
                Dgv_Company.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)(3)      '略名
                Dgv_Company.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)(4)      '備考
                Dgv_Company.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)(5)      '無効フラグ
                Dgv_Company.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)(6)      '更新者
                Dgv_Company.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)(7)      '更新日
                Dgv_Company.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)(8)        '会社コード
                Dgv_Company.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)(9)        '言語コード
                Dgv_Company.Rows(index).Cells(10).Value = ds.Tables(RS).Rows(index)(10)        '氏名
                Dgv_Company.Rows(index).Cells(11).Value = ds.Tables(RS).Rows(index)(11)      '略名
                Dgv_Company.Rows(index).Cells(12).Value = ds.Tables(RS).Rows(index)(12)      '備考
                Dgv_Company.Rows(index).Cells(13).Value = ds.Tables(RS).Rows(index)(13)      '無効フラグ
                Dgv_Company.Rows(index).Cells(14).Value = ds.Tables(RS).Rows(index)(14)      '更新者
                Dgv_Company.Rows(index).Cells(15).Value = ds.Tables(RS).Rows(index)(15)      '更新日
                Dgv_Company.Rows(index).Cells(16).Value = ds.Tables(RS).Rows(index)(16)      '備考
                Dgv_Company.Rows(index).Cells(17).Value = ds.Tables(RS).Rows(index)(17)      '無効フラグ
                Dgv_Company.Rows(index).Cells(18).Value = ds.Tables(RS).Rows(index)(18)      '更新者
                Dgv_Company.Rows(index).Cells(19).Value = ds.Tables(RS).Rows(index)(19)      '更新日
            Next

            CompanyCode.Text = Dgv_Company.Rows(0).Cells(0).Value
            CompanyName.Text = Dgv_Company.Rows(0).Cells(1).Value
            CompanyShortName.Text = Dgv_Company.Rows(0).Cells(2).Value
            PostalCode.Text = Dgv_Company.Rows(0).Cells(3).Value
            Address1.Text = Dgv_Company.Rows(0).Cells(4).Value
            Address2.Text = Dgv_Company.Rows(0).Cells(5).Value
            Address3.Text = Dgv_Company.Rows(0).Cells(6).Value
            Tel.Text = Dgv_Company.Rows(0).Cells(7).Value
            Fax.Text = Dgv_Company.Rows(0).Cells(8).Value
            RepresentativePosition.Text = Dgv_Company.Rows(0).Cells(9).Value
            RepresentativeName.Text = Dgv_Company.Rows(0).Cells(10).Value
            DisplayOrder.Text = Dgv_Company.Rows(0).Cells(11).Value
            Remarks.Text = Dgv_Company.Rows(0).Cells(12).Value
            BankCode.Text = Dgv_Company.Rows(0).Cells(13).Value
            BranchOfficeCode.Text = Dgv_Company.Rows(0).Cells(14).Value
            DepositCategory.Text = Dgv_Company.Rows(0).Cells(15).Value
            AccountNumber.Text = Dgv_Company.Rows(0).Cells(16).Value
            AccountName.Text = Dgv_Company.Rows(0).Cells(17).Value

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    Private Sub btnEditCompany_Click(sender As Object, e As EventArgs) Handles btnEditCompany.Click
        Dim dtToday As DateTime = DateTime.Now
        Try
            Dim Sql As String = ""

            Sql = ""
            Sql += "UPDATE "
            Sql += "Public."
            Sql += "m01_Company "
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
            Sql += CompanyCode.Text
            Sql += "' "
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

            Dim frmUM As frmC01F30_Menu
            frmUM = New frmC01F30_Menu(_msgHd, _db)
            frmUM.Show()
            Me.Close()

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub
End Class