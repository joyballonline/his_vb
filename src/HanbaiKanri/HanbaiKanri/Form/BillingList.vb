Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class BillingList
    Inherits System.Windows.Forms.Form

    '------------------------------------------------------------------------------------------------------
    'メンバー定数宣言
    '------------------------------------------------------------------------------------------------------
    'PG制御文字 
    Dim ds As DataSet
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
    Private _langHd As UtilLangHandler
    Private _db As UtilDBIf
    'Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ
    Private CompanyCode As String = ""
    Private OrderingNo As String()
    Private _status As String = ""

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
                   Optional ByRef prmRefStatus As String = "")
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _status = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True
        DgvBilling.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.DisplayedCells
    End Sub
    Private Sub PurchaseListLoad(Optional ByRef Status As String = "")
        If Status = "EXCLUSION" Then
            Dim Sql As String = ""
            Try
                Sql += "SELECT "
                Sql += "* "
                Sql += "FROM "
                Sql += "public"
                Sql += "."
                Sql += "t23_skyuhd"
                Sql += " WHERE "
                Sql += "取消区分"
                Sql += " = "
                Sql += "'"
                Sql += "0"
                Sql += "'"
                Sql += " ORDER BY "
                Sql += "登録日 DESC"

                Dim reccnt As Integer = 0
                ds = _db.selectDB(Sql, RS, reccnt)
                If frmC01F10_Login.loginValue.Language = "ENG" Then
                    DgvBilling.Columns.Add("請求番号", "BillingNumber")
                    DgvBilling.Columns.Add("請求区分", "BillingClassification")
                    DgvBilling.Columns.Add("請求日", "BillingDate")
                    DgvBilling.Columns.Add("客先番号", "CustomerNumber")
                    DgvBilling.Columns.Add("受注番号", "OrderNumber")
                    DgvBilling.Columns.Add("受注番号枝番", "BranchNumber")
                    DgvBilling.Columns.Add("得意先コード", "CustomerCode")
                    DgvBilling.Columns.Add("得意先名", "CustomerName")
                    DgvBilling.Columns.Add("請求金額計", "TotalBillingAmount")
                    DgvBilling.Columns.Add("売掛残高", "AccountsReceivableBalance")
                    DgvBilling.Columns.Add("備考1", "Remarks1")
                    DgvBilling.Columns.Add("備考2", "remarks2")
                    DgvBilling.Columns.Add("登録日", "RegistrationDate")
                    DgvBilling.Columns.Add("更新者", "Changer")
                Else
                    DgvBilling.Columns.Add("請求番号", "請求番号")
                    DgvBilling.Columns.Add("請求区分", "請求区分")
                    DgvBilling.Columns.Add("請求日", "請求日")
                    DgvBilling.Columns.Add("客先番号", "客先番号")
                    DgvBilling.Columns.Add("受注番号", "受注番号")
                    DgvBilling.Columns.Add("受注番号枝番", "受注番号枝番")
                    DgvBilling.Columns.Add("得意先コード", "得意先コード")
                    DgvBilling.Columns.Add("得意先名", "得意先名")
                    DgvBilling.Columns.Add("請求金額計", "請求金額計")
                    DgvBilling.Columns.Add("売掛残高", "売掛残高")
                    DgvBilling.Columns.Add("備考1", "備考1")
                    DgvBilling.Columns.Add("備考2", "備考2")
                    DgvBilling.Columns.Add("登録日", "登録日")
                    DgvBilling.Columns.Add("更新者", "更新者")
                End If


                DgvBilling.Columns("請求金額計").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvBilling.Columns("売掛残高").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                    DgvBilling.Rows.Add()
                    DgvBilling.Rows(index).Cells("請求番号").Value = ds.Tables(RS).Rows(index)("請求番号")
                    If ds.Tables(RS).Rows(index)("請求区分") = "1" Then
                        DgvBilling.Rows(index).Cells("請求区分").Value = "前受金請求"
                    Else
                        DgvBilling.Rows(index).Cells("請求区分").Value = "通常請求"
                    End If
                    DgvBilling.Rows(index).Cells("請求日").Value = ds.Tables(RS).Rows(index)("請求日")
                    DgvBilling.Rows(index).Cells("客先番号").Value = ds.Tables(RS).Rows(index)("客先番号")
                    DgvBilling.Rows(index).Cells("受注番号").Value = ds.Tables(RS).Rows(index)("受注番号")
                    DgvBilling.Rows(index).Cells("受注番号枝番").Value = ds.Tables(RS).Rows(index)("受注番号枝番")
                    DgvBilling.Rows(index).Cells("得意先コード").Value = ds.Tables(RS).Rows(index)("得意先コード")
                    DgvBilling.Rows(index).Cells("得意先名").Value = ds.Tables(RS).Rows(index)("得意先名")
                    DgvBilling.Rows(index).Cells("請求金額計").Value = ds.Tables(RS).Rows(index)("請求金額計")
                    DgvBilling.Rows(index).Cells("売掛残高").Value = ds.Tables(RS).Rows(index)("売掛残高")
                    DgvBilling.Rows(index).Cells("備考1").Value = ds.Tables(RS).Rows(index)("備考1")
                    DgvBilling.Rows(index).Cells("備考2").Value = ds.Tables(RS).Rows(index)("備考2")
                    DgvBilling.Rows(index).Cells("登録日").Value = ds.Tables(RS).Rows(index)("登録日")
                    DgvBilling.Rows(index).Cells("更新者").Value = ds.Tables(RS).Rows(index)("更新者")
                Next

            Catch ue As UsrDefException
                ue.dspMsg()
                Throw ue
            Catch ex As Exception
                'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
            End Try
        Else
            Dim Sql As String = ""
            Try
                Sql += "SELECT "
                Sql += "* "
                Sql += "FROM "
                Sql += "public"
                Sql += "."
                Sql += "t23_skyuhd"
                Sql += " ORDER BY "
                Sql += "登録日 DESC"

                Dim reccnt As Integer = 0
                ds = _db.selectDB(Sql, RS, reccnt)
                If frmC01F10_Login.loginValue.Language = "ENG" Then
                    DgvBilling.Columns.Add("請求番号", "BillingNumber")
                    DgvBilling.Columns.Add("請求区分", "BillingClassification")
                    DgvBilling.Columns.Add("請求日", "BillingDate")
                    DgvBilling.Columns.Add("客先番号", "CustomerNumber")
                    DgvBilling.Columns.Add("受注番号", "OrderNumber")
                    DgvBilling.Columns.Add("受注番号枝番", "BranchNumber")
                    DgvBilling.Columns.Add("得意先コード", "CustomerCode")
                    DgvBilling.Columns.Add("得意先名", "CustomerName")
                    DgvBilling.Columns.Add("請求金額計", "TotalBillingAmount")
                    DgvBilling.Columns.Add("売掛残高", "AccountsReceivableBalance")
                    DgvBilling.Columns.Add("備考1", "Remarks1")
                    DgvBilling.Columns.Add("備考2", "remarks2")
                    DgvBilling.Columns.Add("登録日", "RegistrationDate")
                    DgvBilling.Columns.Add("更新者", "Changer")
                Else
                    DgvBilling.Columns.Add("請求番号", "請求番号")
                    DgvBilling.Columns.Add("請求区分", "請求区分")
                    DgvBilling.Columns.Add("請求日", "請求日")
                    DgvBilling.Columns.Add("客先番号", "客先番号")
                    DgvBilling.Columns.Add("受注番号", "受注番号")
                    DgvBilling.Columns.Add("受注番号枝番", "受注番号枝番")
                    DgvBilling.Columns.Add("得意先コード", "得意先コード")
                    DgvBilling.Columns.Add("得意先名", "得意先名")
                    DgvBilling.Columns.Add("請求金額計", "請求金額計")
                    DgvBilling.Columns.Add("売掛残高", "売掛残高")
                    DgvBilling.Columns.Add("備考1", "備考1")
                    DgvBilling.Columns.Add("備考2", "備考2")
                    DgvBilling.Columns.Add("登録日", "登録日")
                    DgvBilling.Columns.Add("更新者", "更新者")
                End If

                DgvBilling.Columns("請求金額計").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvBilling.Columns("売掛残高").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                    DgvBilling.Rows.Add()
                    DgvBilling.Rows(index).Cells("請求番号").Value = ds.Tables(RS).Rows(index)("請求番号")
                    If ds.Tables(RS).Rows(index)("請求区分") = "1" Then
                        DgvBilling.Rows(index).Cells("請求区分").Value = "前受金請求"
                    Else
                        DgvBilling.Rows(index).Cells("請求区分").Value = "通常請求"
                    End If
                    DgvBilling.Rows(index).Cells("請求日").Value = ds.Tables(RS).Rows(index)("請求日")
                        DgvBilling.Rows(index).Cells("客先番号").Value = ds.Tables(RS).Rows(index)("客先番号")
                    DgvBilling.Rows(index).Cells("受注番号").Value = ds.Tables(RS).Rows(index)("受注番号")
                    DgvBilling.Rows(index).Cells("受注番号枝番").Value = ds.Tables(RS).Rows(index)("受注番号枝番")
                    DgvBilling.Rows(index).Cells("得意先コード").Value = ds.Tables(RS).Rows(index)("得意先コード")
                    DgvBilling.Rows(index).Cells("得意先名").Value = ds.Tables(RS).Rows(index)("得意先名")
                    DgvBilling.Rows(index).Cells("請求金額計").Value = ds.Tables(RS).Rows(index)("請求金額計")
                    DgvBilling.Rows(index).Cells("売掛残高").Value = ds.Tables(RS).Rows(index)("売掛残高")
                    DgvBilling.Rows(index).Cells("備考1").Value = ds.Tables(RS).Rows(index)("備考1")
                    DgvBilling.Rows(index).Cells("備考2").Value = ds.Tables(RS).Rows(index)("備考2")
                    DgvBilling.Rows(index).Cells("登録日").Value = ds.Tables(RS).Rows(index)("登録日")
                    DgvBilling.Rows(index).Cells("更新者").Value = ds.Tables(RS).Rows(index)("更新者")
                Next

            Catch ue As UsrDefException
                ue.dspMsg()
                Throw ue
            Catch ex As Exception
                'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
            End Try
        End If

    End Sub
    Private Sub MstHanyoue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If _status = "VIEW" Then
            If frmC01F10_Login.loginValue.Language = "ENG" Then
                LblMode.Text = "ViewMode"
            Else
                LblMode.Text = "参照モード"
            End If

            'BtnBillingView.Visible = True
            'BtnBillingView.Location = New Point(997, 677)
        ElseIf _status = "CANCEL" Then
            If frmC01F10_Login.loginValue.Language = "ENG" Then
                LblMode.Text = "CancelMode"
            Else
                LblMode.Text = "取消モード"
            End If

            BtnBillingCancel.Visible = True
            BtnBillingCancel.Location = New Point(997, 509)
        End If

        Dim Status As String = "EXCLUSION"
        PurchaseListLoad(Status)
        If frmC01F10_Login.loginValue.Language = "ENG" Then
            LblConditions.Text = "ExtractionCondition"
            Label1.Text = "CustomerName"
            Label4.Text = "CustomerCode"
            Label8.Text = "BillingDate"
            Label7.Text = "BillingNumber"
            Label11.Text = "CustomerNumber"
            Label10.Text = "DisplayFormat"

            ChkCancelData.Text = "IncludeCancelData"

            BtnBillingSearch.Text = "Search"
            BtnBillingCancel.Text = "CancelOfBilling"
            BtnBillingView.Text = "BillingView"
            BtnBack.Text = "Back"
        End If
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        Dim openForm As Form = Nothing
        openForm = New frmC01F30_Menu(_msgHd, _langHd, _db)
        openForm.Show()
        Me.Close()
    End Sub

    'Private Sub RbtnDetails_CheckedChanged(sender As Object, e As EventArgs) Handles RbtnDetails.CheckedChanged
    '    DgvBilling.Rows.Clear()
    '    DgvBilling.Columns.Clear()

    '    If RbtnSlip.Checked Then
    '        Dim Sql As String = ""
    '        Sql += "SELECT "
    '        Sql += " * "
    '        Sql += "FROM "
    '        Sql += "public"
    '        Sql += "."
    '        Sql += "t40_sirehd"
    '        If OrderingNo IsNot Nothing Then
    '            For i As Integer = 0 To OrderingNo.Length - 1
    '                If i = 0 Then
    '                    Sql += " WHERE "
    '                    Sql += "仕入番号"
    '                    Sql += " ILIKE "
    '                    Sql += "'%"
    '                    Sql += OrderingNo(i)
    '                    Sql += "%'"
    '                Else
    '                    Sql += " OR "
    '                    Sql += "仕入番号"
    '                    Sql += " ILIKE "
    '                    Sql += "'%"
    '                    Sql += OrderingNo(i)
    '                    Sql += "%'"
    '                End If
    '            Next
    '        End If



    '        Dim reccnt As Integer = 0
    '        ds = _db.selectDB(Sql, RS, reccnt)

    '        DgvBilling.Columns.Add("仕入番号", "仕入番号")
    '        DgvBilling.Columns.Add("仕入日", "仕入日")
    '        DgvBilling.Columns.Add("発注番号", "発注番号")
    '        DgvBilling.Columns.Add("発注番号枝番", "発注番号枝番")
    '        DgvBilling.Columns.Add("仕入先コード", "仕入先コード")
    '        DgvBilling.Columns.Add("仕入先名", "仕入先名")
    '        DgvBilling.Columns.Add("仕入先郵便番号", "仕入先郵便番号")
    '        DgvBilling.Columns.Add("仕入先住所", "仕入先住所")
    '        DgvBilling.Columns.Add("仕入先電話番号", "仕入先電話番号")
    '        DgvBilling.Columns.Add("仕入先ＦＡＸ", "仕入先ＦＡＸ")
    '        DgvBilling.Columns.Add("仕入先担当者名", "仕入先担当者名")
    '        DgvBilling.Columns.Add("仕入先担当者役職", "仕入先担当者役職")
    '        DgvBilling.Columns.Add("仕入金額", "仕入金額")
    '        DgvBilling.Columns.Add("支払条件", "支払条件")
    '        DgvBilling.Columns.Add("営業担当者", "営業担当者")
    '        DgvBilling.Columns.Add("入力担当者", "入力担当者")
    '        DgvBilling.Columns.Add("備考", "備考")
    '        DgvBilling.Columns.Add("登録日", "登録日")

    '        DgvBilling.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    '        For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
    '            DgvBilling.Rows.Add()
    '            DgvBilling.Rows(index).Cells("仕入番号").Value = ds.Tables(RS).Rows(index)("仕入番号")
    '            DgvBilling.Rows(index).Cells("発注番号").Value = ds.Tables(RS).Rows(index)("発注番号")
    '            DgvBilling.Rows(index).Cells("発注番号枝番").Value = ds.Tables(RS).Rows(index)("発注番号枝番")
    '            DgvBilling.Rows(index).Cells("仕入日").Value = ds.Tables(RS).Rows(index)("仕入日")
    '            DgvBilling.Rows(index).Cells("仕入先コード").Value = ds.Tables(RS).Rows(index)("仕入先コード")
    '            DgvBilling.Rows(index).Cells("仕入先名").Value = ds.Tables(RS).Rows(index)("仕入先名")
    '            DgvBilling.Rows(index).Cells("仕入先郵便番号").Value = ds.Tables(RS).Rows(index)("仕入先郵便番号")
    '            DgvBilling.Rows(index).Cells("仕入先住所").Value = ds.Tables(RS).Rows(index)("仕入先住所")
    '            DgvBilling.Rows(index).Cells("仕入先電話番号").Value = ds.Tables(RS).Rows(index)("仕入先電話番号")
    '            DgvBilling.Rows(index).Cells("仕入先ＦＡＸ").Value = ds.Tables(RS).Rows(index)("仕入先ＦＡＸ")
    '            DgvBilling.Rows(index).Cells("仕入先担当者名").Value = ds.Tables(RS).Rows(index)("仕入先担当者名")
    '            DgvBilling.Rows(index).Cells("仕入先担当者役職").Value = ds.Tables(RS).Rows(index)("仕入先担当者役職")
    '            DgvBilling.Rows(index).Cells("仕入金額").Value = ds.Tables(RS).Rows(index)("仕入金額")
    '            DgvBilling.Rows(index).Cells("支払条件").Value = ds.Tables(RS).Rows(index)("支払条件")
    '            DgvBilling.Rows(index).Cells("営業担当者").Value = ds.Tables(RS).Rows(index)("営業担当者")
    '            DgvBilling.Rows(index).Cells("入力担当者").Value = ds.Tables(RS).Rows(index)("入力担当者")
    '            DgvBilling.Rows(index).Cells("備考").Value = ds.Tables(RS).Rows(index)("備考")
    '            DgvBilling.Rows(index).Cells("登録日").Value = ds.Tables(RS).Rows(index)("登録日")
    '        Next
    '    Else
    '        Dim Sql As String = ""

    '        Sql += "SELECT "
    '        Sql += " * "
    '        Sql += "FROM "
    '        Sql += "public"
    '        Sql += "."
    '        Sql += "t41_siredt"

    '        If OrderingNo IsNot Nothing Then
    '            For i As Integer = 0 To OrderingNo.Length - 1
    '                If i = 0 Then
    '                    Sql += " WHERE "
    '                    Sql += "仕入番号"
    '                    Sql += " ILIKE "
    '                    Sql += "'%"
    '                    Sql += OrderingNo(i)
    '                    Sql += "%'"
    '                Else
    '                    Sql += " OR "
    '                    Sql += "仕入番号"
    '                    Sql += " ILIKE "
    '                    Sql += "'%"
    '                    Sql += OrderingNo(i)
    '                    Sql += "%'"
    '                End If
    '            Next
    '        End If

    '        Dim reccnt As Integer = 0
    '        ds = _db.selectDB(Sql, RS, reccnt)

    '        DgvBilling.Columns.Add("仕入番号", "仕入番号")
    '        DgvBilling.Columns.Add("行番号", "行番号")
    '        DgvBilling.Columns.Add("仕入区分", "仕入区分")
    '        DgvBilling.Columns.Add("メーカー", "メーカー")
    '        DgvBilling.Columns.Add("品名", "品名")
    '        DgvBilling.Columns.Add("型式", "型式")
    '        DgvBilling.Columns.Add("仕入先名", "仕入先名")
    '        DgvBilling.Columns.Add("仕入値", "仕入値")
    '        DgvBilling.Columns.Add("発注数量", "発注数量")
    '        DgvBilling.Columns.Add("仕入数量", "仕入数量")
    '        DgvBilling.Columns.Add("発注残数", "発注残数")
    '        DgvBilling.Columns.Add("単位", "単位")
    '        DgvBilling.Columns.Add("間接費", "間接費")
    '        DgvBilling.Columns.Add("仕入金額", "仕入金額")
    '        DgvBilling.Columns.Add("リードタイム", "リードタイム")
    '        DgvBilling.Columns.Add("備考", "備考")
    '        DgvBilling.Columns.Add("更新者", "更新者")



    '        DgvBilling.Columns("仕入値").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    '        DgvBilling.Columns("発注数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    '        DgvBilling.Columns("仕入数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    '        DgvBilling.Columns("発注残数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    '        DgvBilling.Columns("間接費").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    '        DgvBilling.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    '        DgvBilling.Columns("リードタイム").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    '        For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
    '            DgvBilling.Rows.Add()
    '            DgvBilling.Rows(index).Cells("仕入番号").Value = ds.Tables(RS).Rows(index)("仕入番号")
    '            DgvBilling.Rows(index).Cells("行番号").Value = ds.Tables(RS).Rows(index)("行番号")
    '            DgvBilling.Rows(index).Cells("仕入区分").Value = ds.Tables(RS).Rows(index)("仕入区分")
    '            DgvBilling.Rows(index).Cells("メーカー").Value = ds.Tables(RS).Rows(index)("メーカー")
    '            DgvBilling.Rows(index).Cells("品名").Value = ds.Tables(RS).Rows(index)("品名")
    '            DgvBilling.Rows(index).Cells("型式").Value = ds.Tables(RS).Rows(index)("型式")
    '            DgvBilling.Rows(index).Cells("仕入先名").Value = ds.Tables(RS).Rows(index)("仕入先名")
    '            DgvBilling.Rows(index).Cells("仕入値").Value = ds.Tables(RS).Rows(index)("仕入値")
    '            DgvBilling.Rows(index).Cells("発注数量").Value = ds.Tables(RS).Rows(index)("発注数量")
    '            DgvBilling.Rows(index).Cells("仕入数量").Value = ds.Tables(RS).Rows(index)("仕入数量")
    '            DgvBilling.Rows(index).Cells("発注残数").Value = ds.Tables(RS).Rows(index)("発注残数")
    '            DgvBilling.Rows(index).Cells("単位").Value = ds.Tables(RS).Rows(index)("単位")
    '            DgvBilling.Rows(index).Cells("間接費").Value = ds.Tables(RS).Rows(index)("間接費")
    '            DgvBilling.Rows(index).Cells("仕入金額").Value = ds.Tables(RS).Rows(index)("仕入金額")
    '            DgvBilling.Rows(index).Cells("リードタイム").Value = ds.Tables(RS).Rows(index)("リードタイム")
    '            DgvBilling.Rows(index).Cells("備考").Value = ds.Tables(RS).Rows(index)("備考")
    '            DgvBilling.Rows(index).Cells("更新者").Value = ds.Tables(RS).Rows(index)("更新者")
    '        Next
    '    End If
    'End Sub

    Private Sub BtnPurchaseSearch_Click(sender As Object, e As EventArgs) Handles BtnBillingSearch.Click
        DgvBilling.Rows.Clear()
        DgvBilling.Columns.Clear()

        Dim count As Integer = 0
        Dim Sql As String = ""
        Try
            Sql += "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t23_skyuhd"
            If TxtCustomerName.Text = "" Then
            Else
                Sql += " WHERE "
                Sql += "得意先名"
                Sql += " ILIKE "
                Sql += "'%"
                Sql += TxtCustomerName.Text
                Sql += "%'"
                count += 1
            End If
            If TxtCustomerCode.Text = "" Then
            Else
                If count > 0 Then
                    Sql += " AND "
                    Sql += "得意先コード"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtCustomerCode.Text
                    Sql += "%'"
                Else
                    Sql += " WHERE "
                    Sql += "得意先コード"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtCustomerCode.Text
                    Sql += "%'"
                    count += 1
                End If
            End If
            If TxtBillingDate1.Text = "" Then
                If TxtBillingDate2.Text = "" Then
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "請求日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtBillingDate2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "請求日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtBillingDate2.Text
                        Sql += "'"
                        count += 1
                    End If
                End If
            Else
                If TxtBillingDate2.Text = "" Then
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "請求日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtBillingDate1.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "請求日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtBillingDate1.Text
                        Sql += "'"
                        count += 1
                    End If
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "請求日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtBillingDate1.Text
                        Sql += "' "
                        Sql += "AND  "
                        Sql += "請求日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtBillingDate2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "請求日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtBillingDate1.Text
                        Sql += "' "
                        Sql += "AND  "
                        Sql += "請求日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtBillingDate2.Text
                        Sql += "'"
                        count += 1
                    End If
                End If
            End If
            If TxtBillingNo1.Text = "" Then
                If TxtBillingNo2.Text = "" Then
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "請求番号"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtBillingNo2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "請求番号"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtBillingNo2.Text
                        Sql += "'"
                        count += 1
                    End If
                End If
            Else
                If TxtBillingNo2.Text = "" Then
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "請求番号"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtBillingNo1.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "請求番号"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtBillingNo1.Text
                        Sql += "'"
                        count += 1
                    End If
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "請求番号"
                        Sql += " >= "
                        Sql += "'"
                        Sql += TxtBillingNo1.Text
                        Sql += "' "
                        Sql += "AND "
                        Sql += "請求番号"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtBillingNo2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "請求番号"
                        Sql += " >= "
                        Sql += "'"
                        Sql += TxtBillingNo1.Text
                        Sql += "' "
                        Sql += "AND  "
                        Sql += "請求番号"
                        Sql += " <= "
                        Sql += "'"
                        Sql += TxtBillingNo2.Text
                        Sql += "'"
                        count += 1
                    End If
                End If
            End If
            If TxtCustomerPO.Text = "" Then
            Else
                If count > 0 Then
                    Sql += " AND "
                    Sql += "客先番号"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtCustomerPO.Text
                    Sql += "%'"
                Else
                    Sql += " WHERE "
                    Sql += "客先番号"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtCustomerPO.Text
                    Sql += "%'"
                    count += 1
                End If
            End If
            Sql += " ORDER BY "
            Sql += "登録日 DESC"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            If frmC01F10_Login.loginValue.Language = "ENG" Then
                DgvBilling.Columns.Add("請求番号", "BillingNumber")
                DgvBilling.Columns.Add("請求区分", "BillingClassification")
                DgvBilling.Columns.Add("請求日", "BillingDate")
                DgvBilling.Columns.Add("客先番号", "CustomerNumber")
                DgvBilling.Columns.Add("受注番号", "OrderNumber")
                DgvBilling.Columns.Add("受注番号枝番", "BranchNumber")
                DgvBilling.Columns.Add("得意先コード", "CustomerCode")
                DgvBilling.Columns.Add("得意先名", "CustomerName")
                DgvBilling.Columns.Add("請求金額計", "TotalBillingAmount")
                DgvBilling.Columns.Add("売掛残高", "AccountsReceivableBalance")
                DgvBilling.Columns.Add("備考1", "Remarks1")
                DgvBilling.Columns.Add("備考2", "remarks2")
                DgvBilling.Columns.Add("登録日", "RegistrationDate")
                DgvBilling.Columns.Add("更新者", "Changer")
            Else
                DgvBilling.Columns.Add("請求番号", "請求番号")
                DgvBilling.Columns.Add("請求区分", "請求区分")
                DgvBilling.Columns.Add("請求日", "請求日")
                DgvBilling.Columns.Add("客先番号", "客先番号")
                DgvBilling.Columns.Add("受注番号", "受注番号")
                DgvBilling.Columns.Add("受注番号枝番", "受注番号枝番")
                DgvBilling.Columns.Add("得意先コード", "得意先コード")
                DgvBilling.Columns.Add("得意先名", "得意先名")
                DgvBilling.Columns.Add("請求金額計", "請求金額計")
                DgvBilling.Columns.Add("売掛残高", "売掛残高")
                DgvBilling.Columns.Add("備考1", "備考1")
                DgvBilling.Columns.Add("備考2", "備考2")
                DgvBilling.Columns.Add("登録日", "登録日")
                DgvBilling.Columns.Add("更新者", "更新者")
            End If

            DgvBilling.Columns("請求金額計").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvBilling.Columns("売掛残高").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'Dim tmp As Integer = ds.Tables(RS).Rows.Count - 1
            'ReDim OrderingNo(tmp)

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvBilling.Rows.Add()
                DgvBilling.Rows(index).Cells("請求番号").Value = ds.Tables(RS).Rows(index)("請求番号")
                DgvBilling.Rows(index).Cells("請求区分").Value = ds.Tables(RS).Rows(index)("請求区分")
                DgvBilling.Rows(index).Cells("請求日").Value = ds.Tables(RS).Rows(index)("請求日")
                DgvBilling.Rows(index).Cells("客先番号").Value = ds.Tables(RS).Rows(index)("客先番号")
                DgvBilling.Rows(index).Cells("受注番号").Value = ds.Tables(RS).Rows(index)("受注番号")
                DgvBilling.Rows(index).Cells("受注番号枝番").Value = ds.Tables(RS).Rows(index)("受注番号枝番")
                DgvBilling.Rows(index).Cells("得意先コード").Value = ds.Tables(RS).Rows(index)("得意先コード")
                DgvBilling.Rows(index).Cells("得意先名").Value = ds.Tables(RS).Rows(index)("得意先名")
                DgvBilling.Rows(index).Cells("請求金額計").Value = ds.Tables(RS).Rows(index)("請求金額計")
                DgvBilling.Rows(index).Cells("売掛残高").Value = ds.Tables(RS).Rows(index)("売掛残高")
                DgvBilling.Rows(index).Cells("備考1").Value = ds.Tables(RS).Rows(index)("備考1")
                DgvBilling.Rows(index).Cells("備考2").Value = ds.Tables(RS).Rows(index)("備考2")
                DgvBilling.Rows(index).Cells("登録日").Value = ds.Tables(RS).Rows(index)("登録日")
                DgvBilling.Rows(index).Cells("更新者").Value = ds.Tables(RS).Rows(index)("更新者")
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    Private Sub BtnBillingView_Click(sender As Object, e As EventArgs) Handles BtnBillingView.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvBilling.CurrentCell.RowIndex
        Dim No As String = DgvBilling.Rows(RowIdx).Cells("受注番号").Value
        Dim Suffix As String = DgvBilling.Rows(RowIdx).Cells("受注番号枝番").Value
        Dim Status As String = "VIEW"
        Dim openForm As Form = Nothing
        openForm = New BillingManagement(_msgHd, _db, _langHd, Me, No, Suffix, Status)   '処理選択
        openForm.Show(Me)
    End Sub

    Private Sub ChkCancelData_CheckedChanged(sender As Object, e As EventArgs) Handles ChkCancelData.CheckedChanged
        DgvBilling.Rows.Clear()
        DgvBilling.Columns.Clear()

        If ChkCancelData.Checked = False Then
            Dim Status As String = "EXCLUSION"
            PurchaseListLoad(Status)
        Else
            PurchaseListLoad()
        End If
    End Sub

    Private Sub BtnBillingCancel_Click(sender As Object, e As EventArgs) Handles BtnBillingCancel.Click
        Dim dtNow As DateTime = DateTime.Now
        Dim Sql1 As String = ""
        Sql1 = ""
        Sql1 += "UPDATE "
        Sql1 += "Public."
        Sql1 += "t23_skyuhd "
        Sql1 += "SET "

        Sql1 += "取消区分"
        Sql1 += " = '"
        Sql1 += "1"
        Sql1 += "', "
        Sql1 += "取消日"
        Sql1 += " = '"
        Sql1 += dtNow
        Sql1 += "', "
        Sql1 += "更新者"
        Sql1 += " = '"
        Sql1 += frmC01F10_Login.loginValue.TantoNM
        Sql1 += " ' "

        Sql1 += "WHERE"
        Sql1 += " 会社コード"
        Sql1 += "='"
        Sql1 += frmC01F10_Login.loginValue.BumonNM
        Sql1 += "'"
        Sql1 += " AND"
        Sql1 += " 請求番号"
        Sql1 += "='"
        Sql1 += DgvBilling.Rows(DgvBilling.CurrentCell.RowIndex).Cells("請求番号").Value
        Sql1 += "' "
        Sql1 += "RETURNING 会社コード"
        Sql1 += ", "
        Sql1 += "請求番号"
        Sql1 += ", "
        Sql1 += "請求区分"
        Sql1 += ", "
        Sql1 += "請求日"
        Sql1 += ", "
        Sql1 += "受注番号"
        Sql1 += ", "
        Sql1 += "受注番号枝番"
        Sql1 += ", "
        Sql1 += "得意先コード"
        Sql1 += ", "
        Sql1 += "得意先名"
        Sql1 += ", "
        Sql1 += "請求金額計"
        Sql1 += ", "
        Sql1 += "売掛残高"
        Sql1 += ", "
        Sql1 += "備考1"
        Sql1 += ", "
        Sql1 += "備考2"
        Sql1 += ", "
        Sql1 += "取消日"
        Sql1 += ", "
        Sql1 += "取消区分"
        Sql1 += ", "
        Sql1 += "登録日"
        Sql1 += ", "
        Sql1 += "更新者"

        If frmC01F10_Login.loginValue.Language = "ENG" Then
            Dim result As DialogResult = MessageBox.Show("Would yoou like to cancel the billing？",
                                             "Question",
                                             MessageBoxButtons.YesNoCancel,
                                             MessageBoxIcon.Exclamation,
                                             MessageBoxDefaultButton.Button2)

            If result = DialogResult.Yes Then
                _db.executeDB(Sql1)
                DgvBilling.Rows.Clear()
                DgvBilling.Columns.Clear()
                Dim Status As String = "EXCLUSION"
                PurchaseListLoad(Status)
            ElseIf result = DialogResult.No Then

            ElseIf result = DialogResult.Cancel Then

            End If
        Else
            Dim result As DialogResult = MessageBox.Show("請求を取り消しますか？",
                                             "質問",
                                             MessageBoxButtons.YesNoCancel,
                                             MessageBoxIcon.Exclamation,
                                             MessageBoxDefaultButton.Button2)

            If result = DialogResult.Yes Then
                _db.executeDB(Sql1)
                DgvBilling.Rows.Clear()
                DgvBilling.Columns.Clear()
                Dim Status As String = "EXCLUSION"
                PurchaseListLoad(Status)
            ElseIf result = DialogResult.No Then

            ElseIf result = DialogResult.Cancel Then

            End If
        End If
    End Sub
End Class