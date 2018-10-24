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
                   ByRef prmRefStatus As String,
                   Optional ByRef prmRefCompany As String = "")
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
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

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        Dim MC As MstCompany
        MC = New MstCompany(_msgHd, _db, _langHd)
        MC.Show()
        Me.Close()
    End Sub

    Private Sub btnAddCompany_Click_1(sender As Object, e As EventArgs) Handles BtnRegistration.Click
        Dim dtToday As DateTime = DateTime.Now
        Try
            If _status = "ADD" Then
                Dim Sql As String = ""

                Sql = ""
                Sql += "INSERT INTO "
                Sql += "Public."
                Sql += "m01_company("
                Sql += "会社コード, 会社名, 会社略称, 郵便番号, 住所１, 住所２, 住所３, 電話番号, ＦＡＸ番号, 代表者役職, 代表者名, 表示順, 備考, 銀行名, 銀行コード, 支店名, 支店コード, 預金種目, 口座番号, 口座名義, 在庫単価評価法, 前払法人税率, 更新者, 更新日)"
                Sql += " VALUES('"
                Sql += TxtCompanyCode.Text
                Sql += "', '"
                Sql += TxtCompanyName.Text
                Sql += "', '"
                Sql += TxtCompanyShortName.Text
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
                Sql += TxtFax.Text
                Sql += "', '"
                Sql += TxtRepresentativePosition.Text
                Sql += "', '"
                Sql += TxtRepresentativeName.Text
                Sql += "', '"
                If TxtDisplayOrder.Text = "" Then
                    Sql += "0"
                Else
                    Sql += TxtDisplayOrder.Text
                End If

                Sql += "', '"
                Sql += TxtRemarks.Text
                Sql += "', '"
                Sql += TxtBankName.Text
                Sql += "', '"
                Sql += TxtBankCode.Text
                Sql += "', '"
                Sql += TxtBranchName.Text
                Sql += "', '"
                Sql += TxtBranchOfficeCode.Text
                Sql += "', '"
                Sql += TxtDepositCategory.Text
                Sql += "', '"
                Sql += TxtAccountNumber.Text
                Sql += "', '"
                Sql += TxtAccountName.Text
                Sql += "', '"
                Sql += CbEvaluation.SelectedValue.ToString
                Sql += "', '"
                Sql += TxtPph.Text
                Sql += "', '"
                Sql += frmC01F10_Login.loginValue.TantoNM
                Sql += "', '"
                Sql += dtToday
                Sql += " ')"
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
                Sql += "代表者役職"
                Sql += ", "
                Sql += "代表者名"
                Sql += ", "
                Sql += "表示順"
                Sql += ", "
                Sql += "備考"
                Sql += ", "
                Sql += "銀行名"
                Sql += ", "
                Sql += "銀行コード"
                Sql += ", "
                Sql += "支店名"
                Sql += ", "
                Sql += "支店コード"
                Sql += ", "
                Sql += "預金種目"
                Sql += ", "
                Sql += "口座番号"
                Sql += ", "
                Sql += "口座名義"
                Sql += ", "
                Sql += "在庫単価評価法"
                Sql += ", "
                Sql += "前払法人税率"
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
                Sql += "m01_company "
                Sql += "SET "
                Sql += " 会社コード"
                Sql += " = '"
                Sql += TxtCompanyCode.Text
                Sql += "', "
                Sql += "会社名"
                Sql += " = '"
                Sql += TxtCompanyName.Text
                Sql += "', "
                Sql += "会社略称"
                Sql += " = '"
                Sql += TxtCompanyShortName.Text
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
                Sql += "ＦＡＸ番号"
                Sql += " = '"
                Sql += TxtFax.Text
                Sql += "', "
                Sql += "代表者役職"
                Sql += " = '"
                Sql += TxtRepresentativePosition.Text
                Sql += "', "
                Sql += "代表者名"
                Sql += " = '"
                Sql += TxtRepresentativeName.Text
                Sql += "', "
                Sql += "表示順"
                Sql += " = '"
                Sql += TxtDisplayOrder.Text
                Sql += "', "
                Sql += "備考"
                Sql += " = '"
                Sql += TxtRemarks.Text
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
                Sql += "在庫単価評価法"
                Sql += " = '"
                Sql += CbEvaluation.SelectedValue.ToString
                Sql += "', "
                Sql += "前払法人税率"
                Sql += " = '"
                Sql += TxtPph.Text
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
                Sql += "銀行名"
                Sql += ", "
                Sql += "銀行コード"
                Sql += ", "
                Sql += "支店名"
                Sql += ", "
                Sql += "支店コード"
                Sql += ", "
                Sql += "預金種目"
                Sql += ", "
                Sql += "口座番号"
                Sql += ", "
                Sql += "口座名義"
                Sql += ", "
                Sql += "在庫単価評価法"
                Sql += ", "
                Sql += "前払法人税率"
                Sql += ", "
                Sql += "更新者"
                Sql += ", "
                Sql += "更新日"

                _db.executeDB(Sql)
            End If

            Dim frmMC As MstCompany
            frmMC = New MstCompany(_msgHd, _db, _langHd)
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

    Private Sub Company_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Sql1 As String = ""

        Sql1 += "SELECT "
        Sql1 += "* "
        Sql1 += "FROM "
        Sql1 += "public"
        Sql1 += "."
        Sql1 += "m90_hanyo"
        Sql1 += " WHERE "
        Sql1 += "会社コード"
        Sql1 += " ILIKE "
        Sql1 += "'"
        Sql1 += frmC01F10_Login.loginValue.BumonNM
        Sql1 += "'"
        Sql1 += " AND "
        Sql1 += "固定キー"
        Sql1 += " ILIKE "
        Sql1 += "'"
        Sql1 += "3"
        Sql1 += "'"

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

        If _status = "EDIT" Then
            Dim Sql As String = ""

            Sql += "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "m01_company"
            Sql += " WHERE "
            Sql += "会社コード"
            Sql += " ILIKE "
            Sql += "'"
            Sql += _companyCode
            Sql += "'"


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
                TxtDepositCategory.Text = ds.Tables(RS).Rows(0)("預金種目")
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


        End If
    End Sub
End Class