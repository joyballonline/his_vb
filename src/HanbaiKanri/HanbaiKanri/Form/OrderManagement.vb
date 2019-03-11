Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports System.Globalization

Public Class OrderManagement
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
    Private CompanyCode As String = ""
    Private No As String = ""
    Private Suffix As String = ""
    Private _langHd As UtilLangHandler
    Private _status As String = ""
    Private Input As String = frmC01F10_Login.loginValue.TantoNM

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
                   ByRef prmRefNo As String,
                   ByRef prmRefSuffix As String,
                   Optional ByRef prmRefStatus As String = "")
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        No = prmRefNo
        Suffix = prmRefSuffix
        _status = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        DtpOrderDate.Value = Date.Now
        DtpDepositDate.Value = Date.Now
        _init = True

    End Sub

    '画面表示時
    Private Sub OrderManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then

            LblOrderNo.Text = "OrderNumber"
            LblCutomerNo.Text = "CustomerNumber"
            LblOrderDate.Text = "OrderDate"
            LblCustomer.Text = "CustomerName"
            LblOrder.Text = "JobOrder"
            LblHistory.Text = "SalesHistoryData"
            LblAdd.Text = "SalesThisTime"
            LblSalesDate.Text = "SalesDate"
            LblDepositDate.Text = "DepositDate"
            LblRemarks.Text = "Remarks"
            LblNo1.Text = "Record"
            LblNo1.Location = New Point(1272, 82)
            LblNo1.Size = New Size(66, 22)
            LblNo2.Text = "Record"
            LblNo2.Location = New Point(1272, 212)
            LblNo2.Size = New Size(66, 22)
            LblNo3.Text = "Record"
            LblNo3.Location = New Point(1272, 343)
            LblNo3.Size = New Size(66, 22)

            TxtCount1.Location = New Point(1228, 82)
            TxtCount2.Location = New Point(1228, 212)
            TxtCount3.Location = New Point(1228, 343)
            'TxtRemarks.Size = New Size(600, 22)

            BtnRegist.Text = "Registration"
            BtnBack.Text = "Back"
        End If
        If _status = CommonConst.STATUS_VIEW Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "ViewMode"
            Else
                LblMode.Text = "参照モード"
            End If

            LblNo1.Visible = False
            LblNo2.Visible = False
            LblNo3.Visible = False
            LblOrder.Visible = False
            LblAdd.Visible = False
            'LblOrderDate.Visible = False
            'LblRemarks.Visible = False
            'DtpOrderDate.Visible = False
            TxtCount1.Visible = False
            TxtCount2.Visible = False
            TxtCount3.Visible = False
            'TxtRemarks.Visible = False
            DgvOrder.Visible = False
            DgvAdd.Visible = False
            DgvHistory.ReadOnly = False
            LblHistory.Location = New Point(12, 82)
            DgvHistory.Location = New Point(12, 106)
            LblSalesDate.Location = New Point(172, 82)
            DtpOrderDate.Location = New Point(292, 82)
            LblDepositDate.Location = New Point(447, 82)
            DtpDepositDate.Location = New Point(567, 82)
            LblRemarks.Location = New Point(724, 82)
            TxtRemarks.Location = New Point(844, 82)
            DtpOrderDate.Enabled = False
            LblDepositDate.Enabled = False
            DtpDepositDate.Enabled = False
            TxtRemarks.Enabled = False
            TxtRemarks.BackColor = Color.FromArgb(255, 255, 192)
            DgvHistory.Size = New Point(1326, 400)
            BtnRegist.Visible = False
            DtpDepositDate.Enabled = False
        Else
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "SalesInputMode"
            Else
                LblMode.Text = "売上入力モード"
            End If
        End If
        Dim Sql1 As String = ""
        Dim Sql2 As String = ""
        Dim Sql3 As String = ""
        Dim Sql4 As String = ""
        Try
            Sql1 += "SELECT * FROM public.t10_cymnhd"
            Sql1 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql1 += " AND 受注番号 = '" & No & "'"
            Sql1 += " AND 受注番号枝番 = '" & Suffix & "'"
            Sql1 += " AND 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString

            Sql2 = "SELECT"
            Sql2 += " t31.*, t30.取消区分"
            Sql2 += " FROM "
            Sql2 += " public.t31_urigdt t31 "

            Sql2 += " INNER JOIN "
            Sql2 += " t30_urighd t30"
            Sql2 += " ON "

            Sql2 += " t31.会社コード = t30.会社コード"
            Sql2 += " AND "
            Sql2 += " t31.売上番号 = t30.売上番号"
            Sql2 += " AND "
            Sql2 += " t31.売上番号枝番 = t30.売上番号枝番"

            Sql2 += " WHERE "
            Sql2 += " t31.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql2 += " AND "
            Sql2 += " t31.受注番号 = '" & No & "'"
            Sql2 += " AND "
            Sql2 += " t31.受注番号枝番 = '" & Suffix & "'"
            Sql2 += " AND "
            Sql2 += " t30.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString


            Sql3 = "SELECT"
            Sql3 += " t11.*, t10.取消区分"
            Sql3 += " FROM "
            Sql3 += " public.t11_cymndt t11 "

            Sql3 += " INNER JOIN "
            Sql3 += " t10_cymnhd t10"
            Sql3 += " ON "

            Sql3 += " t11.会社コード = t10.会社コード"
            Sql3 += " AND "
            Sql3 += " t11.受注番号 = t10.受注番号"
            Sql3 += " AND "
            Sql3 += " t11.受注番号枝番 = t10.受注番号枝番"

            Sql3 += " WHERE "
            Sql3 += " t11.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql3 += " AND "
            Sql3 += " t11.受注番号 = '" & No & "'"
            Sql3 += " AND "
            Sql3 += " t11.受注番号枝番 = '" & Suffix & "'"
            Sql3 += " AND "
            Sql3 += " t10.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString


            Sql4 += "SELECT * FROM public.t30_urighd"
            Sql4 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql4 += " AND 受注番号 = '" & No & "'"
            Sql4 += " AND 受注番号枝番 = '" & Suffix & "'"
            Sql4 += " AND "
            Sql4 += " 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString

            Dim reccnt As Integer = 0
            Dim ds1 As DataSet = _db.selectDB(Sql1, RS, reccnt)
            Dim ds2 As DataSet = _db.selectDB(Sql2, RS, reccnt)
            Dim ds3 As DataSet = _db.selectDB(Sql3, RS, reccnt)
            Dim ds4 As DataSet = _db.selectDB(Sql4, RS, reccnt)

            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                DgvOrder.Columns.Add("明細", "DetailData")
                DgvOrder.Columns.Add("メーカー", "Manufacturer")
                DgvOrder.Columns.Add("品名", "ItemName")
                DgvOrder.Columns.Add("型式", "Spec")
                DgvOrder.Columns.Add("受注数量", "JobOrderQuantity")
                DgvOrder.Columns.Add("単位", "Unit")
                DgvOrder.Columns.Add("売上数量", "SalesQUantity")
                DgvOrder.Columns.Add("売単価", "SellingPrice")
                DgvOrder.Columns.Add("売上金額", "SalesAmount")
                DgvOrder.Columns.Add("受注残数", "OrderRemainingAmount")
            Else
                DgvOrder.Columns.Add("明細", "明細")
                DgvOrder.Columns.Add("メーカー", "メーカー")
                DgvOrder.Columns.Add("品名", "品名")
                DgvOrder.Columns.Add("型式", "型式")
                DgvOrder.Columns.Add("受注数量", "受注数量")
                DgvOrder.Columns.Add("単位", "単位")
                DgvOrder.Columns.Add("売上数量", "売上数量")
                DgvOrder.Columns.Add("売単価", "売単価")
                DgvOrder.Columns.Add("売上金額", "売上金額")
                DgvOrder.Columns.Add("受注残数", "受注残数")
            End If


            DgvOrder.Columns("受注数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvOrder.Columns("売上数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvOrder.Columns("売単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvOrder.Columns("売上金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvOrder.Columns("受注残数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            For i As Integer = 0 To ds3.Tables(RS).Rows.Count - 1
                DgvOrder.Rows.Add()
                DgvOrder.Rows(i).Cells("メーカー").Value = ds3.Tables(RS).Rows(i)("メーカー")
                DgvOrder.Rows(i).Cells("品名").Value = ds3.Tables(RS).Rows(i)("品名")
                DgvOrder.Rows(i).Cells("型式").Value = ds3.Tables(RS).Rows(i)("型式")
                DgvOrder.Rows(i).Cells("受注数量").Value = ds3.Tables(RS).Rows(i)("受注数量")
                DgvOrder.Rows(i).Cells("単位").Value = ds3.Tables(RS).Rows(i)("単位")
                DgvOrder.Rows(i).Cells("売上数量").Value = ds3.Tables(RS).Rows(i)("売上数量")
                DgvOrder.Rows(i).Cells("売単価").Value = ds3.Tables(RS).Rows(i)("売単価")
                DgvOrder.Rows(i).Cells("売上金額").Value = ds3.Tables(RS).Rows(i)("売上金額")
                DgvOrder.Rows(i).Cells("受注残数").Value = ds3.Tables(RS).Rows(i)("受注残数")
            Next

            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                DgvHistory.Columns.Add("No", "No")
                DgvHistory.Columns.Add("売上番号", "SalesNumber")
                DgvHistory.Columns.Add("行番号", "LineNumber")
                DgvHistory.Columns.Add("仕入区分", "PurchasingClassification")
                DgvHistory.Columns.Add("メーカー", "Manufacturer")
                DgvHistory.Columns.Add("品名", "ItemName")
                DgvHistory.Columns.Add("型式", "Spec")
                DgvHistory.Columns.Add("単位", "Unit")
                DgvHistory.Columns.Add("仕入先", "SupplierName")
                DgvHistory.Columns.Add("売単価", "SellingPrice")
                DgvHistory.Columns.Add("売上数量", "SalesQuantity")
                DgvHistory.Columns.Add("備考", "Remarks")
            Else
                DgvHistory.Columns.Add("No", "No")
                DgvHistory.Columns.Add("売上番号", "売上番号")
                DgvHistory.Columns.Add("行番号", "行番号")
                DgvHistory.Columns.Add("仕入区分", "仕入区分")
                DgvHistory.Columns.Add("メーカー", "メーカー")
                DgvHistory.Columns.Add("品名", "品名")
                DgvHistory.Columns.Add("型式", "型式")
                DgvHistory.Columns.Add("単位", "単位")
                DgvHistory.Columns.Add("仕入先", "仕入先")
                DgvHistory.Columns.Add("売単価", "売単価")
                DgvHistory.Columns.Add("売上数量", "売上数量")
                DgvHistory.Columns.Add("備考", "備考")
            End If


            DgvHistory.Columns("売単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvHistory.Columns("売上数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            For i As Integer = 0 To ds2.Tables(RS).Rows.Count - 1
                DgvHistory.Rows.Add()
                DgvHistory.Rows(i).Cells("売上番号").Value = ds2.Tables(RS).Rows(i)("売上番号")
                DgvHistory.Rows(i).Cells("行番号").Value = ds2.Tables(RS).Rows(i)("行番号")
                DgvHistory.Rows(i).Cells("仕入区分").Value = ds2.Tables(RS).Rows(i)("仕入区分")
                DgvHistory.Rows(i).Cells("メーカー").Value = ds2.Tables(RS).Rows(i)("メーカー")
                DgvHistory.Rows(i).Cells("品名").Value = ds2.Tables(RS).Rows(i)("品名")
                DgvHistory.Rows(i).Cells("型式").Value = ds2.Tables(RS).Rows(i)("型式")
                DgvHistory.Rows(i).Cells("単位").Value = ds2.Tables(RS).Rows(i)("単位")
                DgvHistory.Rows(i).Cells("仕入先").Value = ds2.Tables(RS).Rows(i)("仕入先名")
                DgvHistory.Rows(i).Cells("売単価").Value = ds2.Tables(RS).Rows(i)("売単価")
                DgvHistory.Rows(i).Cells("売上数量").Value = ds2.Tables(RS).Rows(i)("売上数量")
                DgvHistory.Rows(i).Cells("備考").Value = ds2.Tables(RS).Rows(i)("備考")
            Next

            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                DgvAdd.Columns.Add("No", "No")
                DgvAdd.Columns.Add("行番号", "LineNumber")
                DgvAdd.Columns.Add("仕入区分", "PurchasingClassification")
                DgvAdd.Columns.Add("メーカー", "Manufacturer")
                DgvAdd.Columns.Add("品名", "ItemName")
                DgvAdd.Columns.Add("型式", "Spec")
                DgvAdd.Columns.Add("単位", "Unit")
                DgvAdd.Columns.Add("仕入先", "SupplierName")
                DgvAdd.Columns.Add("売単価", "Sellingprice")
                DgvAdd.Columns.Add("売上数量", "SalesQuantity")
                DgvAdd.Columns.Add("備考", "Remarks")
            Else
                DgvAdd.Columns.Add("No", "No")
                DgvAdd.Columns.Add("行番号", "行番号")
                DgvAdd.Columns.Add("仕入区分", "仕入区分")
                DgvAdd.Columns.Add("メーカー", "メーカー")
                DgvAdd.Columns.Add("品名", "品名")
                DgvAdd.Columns.Add("型式", "型式")
                DgvAdd.Columns.Add("単位", "単位")
                DgvAdd.Columns.Add("仕入先", "仕入先")
                DgvAdd.Columns.Add("売単価", "売単価")
                DgvAdd.Columns.Add("売上数量", "売上数量")
                DgvAdd.Columns.Add("備考", "備考")
            End If


            DgvAdd.Columns("売単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvAdd.Columns("売上数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            DgvAdd.Columns("No").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvAdd.Columns("No").ReadOnly = True
            DgvAdd.Columns("行番号").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvAdd.Columns("行番号").ReadOnly = True
            DgvAdd.Columns("仕入区分").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvAdd.Columns("仕入区分").ReadOnly = True
            DgvAdd.Columns("メーカー").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvAdd.Columns("メーカー").ReadOnly = True
            DgvAdd.Columns("品名").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvAdd.Columns("品名").ReadOnly = True
            DgvAdd.Columns("型式").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvAdd.Columns("型式").ReadOnly = True
            DgvAdd.Columns("単位").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvAdd.Columns("単位").ReadOnly = True
            DgvAdd.Columns("仕入先").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvAdd.Columns("仕入先").ReadOnly = True

            For index As Integer = 0 To ds3.Tables(RS).Rows.Count - 1
                If ds3.Tables(RS).Rows(index)("受注残数") = 0 Then
                Else
                    DgvAdd.Rows.Add()
                    DgvAdd.Rows(DgvAdd.Rows.Count - 1).Cells("行番号").Value = ds3.Tables(RS).Rows(index)("行番号")
                    DgvAdd.Rows(DgvAdd.Rows.Count - 1).Cells("仕入区分").Value = ds3.Tables(RS).Rows(index)("仕入区分")
                    DgvAdd.Rows(DgvAdd.Rows.Count - 1).Cells("メーカー").Value = ds3.Tables(RS).Rows(index)("メーカー")
                    DgvAdd.Rows(DgvAdd.Rows.Count - 1).Cells("品名").Value = ds3.Tables(RS).Rows(index)("品名")
                    DgvAdd.Rows(DgvAdd.Rows.Count - 1).Cells("型式").Value = ds3.Tables(RS).Rows(index)("型式")
                    DgvAdd.Rows(DgvAdd.Rows.Count - 1).Cells("仕入先").Value = ds3.Tables(RS).Rows(index)("仕入先名")
                    DgvAdd.Rows(DgvAdd.Rows.Count - 1).Cells("単位").Value = ds3.Tables(RS).Rows(index)("単位")
                    DgvAdd.Rows(DgvAdd.Rows.Count - 1).Cells("売単価").Value = ds3.Tables(RS).Rows(index)("売単価")
                    DgvAdd.Rows(DgvAdd.Rows.Count - 1).Cells("売上数量").Value = 0
                    DgvAdd.Rows(DgvAdd.Rows.Count - 1).Cells("備考").Value = ds3.Tables(RS).Rows(index)("備考")
                End If
            Next

            '行番号の振り直し
            Dim i1 As Integer = DgvOrder.Rows.Count()
            Dim No1 As Integer = 1
            For c As Integer = 0 To i1 - 1
                DgvOrder.Rows(c).Cells(0).Value = No1
                No1 += 1
            Next c
            TxtCount1.Text = DgvOrder.Rows.Count()

            Dim i2 As Integer = DgvHistory.Rows.Count()
            Dim No2 As Integer = 1
            For c As Integer = 0 To i2 - 1
                DgvHistory.Rows(c).Cells(0).Value = No2
                No2 += 1
            Next c
            TxtCount2.Text = DgvHistory.Rows.Count()

            Dim i3 As Integer = DgvAdd.Rows.Count()
            Dim No3 As Integer = 1
            For c As Integer = 0 To i3 - 1
                DgvAdd.Rows(c).Cells(0).Value = No3
                No3 += 1
            Next c
            TxtCount3.Text = DgvAdd.Rows.Count()

            TxtOrderNo.Text = ds1.Tables(RS).Rows(0)("受注番号")
            TxtSuffixNo.Text = ds1.Tables(RS).Rows(0)("受注番号枝番")
            TxtCustomerPO.Text = ds1.Tables(RS).Rows(0)("客先番号").ToString
            TxtOrderDate.Text = ds1.Tables(RS).Rows(0)("受注日")
            TxtCustomerCode.Text = ds1.Tables(RS).Rows(0)("得意先コード")
            TxtCustomerName.Text = ds1.Tables(RS).Rows(0)("得意先名")

            '売上日、入金予定日のMinDateを受注日に設定
            DtpOrderDate.MinDate = ds1.Tables(RS).Rows(0)("受注日").ToShortDateString()
            DtpDepositDate.MinDate = ds1.Tables(RS).Rows(0)("受注日").ToShortDateString()

            If _status = CommonConst.STATUS_VIEW Then
                DtpOrderDate.Value = ds4.Tables(RS).Rows(0)("売上日")
                TxtRemarks.Text = ds4.Tables(RS).Rows(0)("備考")
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        'Dim openForm As Form = Nothing
        'openForm = New frmC01F30_Menu(_msgHd, _langHd, _db)
        'openForm.Show()
        Me.Close()
    End Sub

    Private Sub BtnRegist_Click(sender As Object, e As EventArgs) Handles BtnRegist.Click
        Dim dtToday As DateTime = DateTime.Now
        Dim strToday As String = UtilClass.formatDatetime(dtToday)
        Dim errFlg As Boolean = True

        Dim Sql1 As String = ""
        Dim Sql2 As String = ""
        Dim Sql3 As String = ""
        Dim Sql4 As String = ""
        Dim Sql5 As String = ""
        Dim Sql6 As String = ""
        Dim Sql7 As String = ""
        Dim Sql8 As String = ""
        Dim Sql9 As String = ""
        Dim Saiban1 As String = ""
        Dim Saiban2 As String = ""

        Sql1 += "SELECT * FROM public.t10_cymnhd"
        Sql1 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql1 += " AND 受注番号 = '" & No & "'"
        Sql1 += " AND 受注番号枝番 = '" & Suffix & "'"
        Sql1 += " AND 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

        Dim reccnt As Integer = 0
        Dim ds1 As DataSet = _db.selectDB(Sql1, RS, reccnt)

        Sql2 += "SELECT * FROM public.t11_cymndt"
        Sql2 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql2 += " AND 受注番号 = '" & No & "'"
        Sql2 += " AND 受注番号枝番 = '" & Suffix & "'"

        Dim ds2 As DataSet = _db.selectDB(Sql2, RS, reccnt)


        For i As Integer = 0 To DgvAdd.Rows.Count() - 1
            If ds2.Tables(RS).Rows(i)("受注数量") < ds2.Tables(RS).Rows(i)("売上数量") + DgvAdd.Rows(i).Cells("売上数量").Value Then
                '売上数量が受注数量を超えているアラートを表示
                _msgHd.dspMSG("chkUriageBalanceError", frmC01F10_Login.loginValue.Language)

                Return
            End If
        Next

        Dim nullCount As Integer = 0
        For i As Integer = 0 To DgvAdd.Rows.Count() - 1
            If DgvAdd.Rows(i).Cells("売上数量").Value = 0 Then
                nullCount += 1
            End If
        Next
        If DgvAdd.Rows.Count() = nullCount Then
            '登録する内容がないことをアラートする
            _msgHd.dspMSG("chkUriageAddError", frmC01F10_Login.loginValue.Language)

            Return

        End If
        If errFlg Then
            Saiban1 += "SELECT * FROM public.m80_saiban"
            Saiban1 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Saiban1 += " AND 採番キー = '40'"

            Saiban2 += "SELECT * FROM public.m80_saiban"
            Saiban2 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Saiban2 += " AND 採番キー = '70'"

            Dim dsSaiban1 As DataSet = _db.selectDB(Saiban1, RS, reccnt)
            Dim dsSaiban2 As DataSet = _db.selectDB(Saiban2, RS, reccnt)

            Dim ER As String = dsSaiban1.Tables(RS).Rows(0)("接頭文字")
            ER += dtToday.ToString("MMdd")
            ER += dsSaiban1.Tables(RS).Rows(0)("最新値").ToString.PadLeft(dsSaiban1.Tables(RS).Rows(0)("連番桁数"), "0")

            Dim LS As String = dsSaiban2.Tables(RS).Rows(0)("接頭文字")
            LS += dtToday.ToString("MMdd")
            LS += dsSaiban2.Tables(RS).Rows(0)("最新値").ToString.PadLeft(dsSaiban2.Tables(RS).Rows(0)("連番桁数"), "0")

            Sql3 = "INSERT INTO "
            Sql3 += "Public."
            Sql3 += "t30_urighd("
            Sql3 += "会社コード, 売上番号, 売上番号枝番, 客先番号, 受注番号, 受注番号枝番, 見積番号, 見積番号枝番, 得意先コード, 得意先名, 得意先郵便番号, 得意先住所"
            Sql3 += ", 得意先電話番号, 得意先ＦＡＸ, 得意先担当者役職, 得意先担当者名, 見積日, 見積有効期限, 支払条件, 見積金額, 売上金額, 粗利額, 営業担当者, 入力担当者, 備考"
            Sql3 += ", 取消日, 取消区分, ＶＡＴ, ＰＰＨ, 受注日, 売上日, 入金予定日, 登録日, 更新日, 更新者, 仕入金額)"
            Sql3 += " VALUES('"
            Sql3 += ds1.Tables(RS).Rows(0)("会社コード").ToString '会社コード
            Sql3 += "', '"
            Sql3 += ER '売上番号
            Sql3 += "', '"
            Sql3 += "1" '売上番号枝番
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("客先番号").ToString '客先番号
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("受注番号").ToString '受注番号
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("受注番号枝番").ToString '受注番号枝番
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("見積番号").ToString '見積番号
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("見積番号枝番").ToString '見積番号枝番
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("得意先コード").ToString '得意先コード
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("得意先名").ToString '得意先名
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("得意先郵便番号").ToString '得意先郵便番号
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("得意先住所").ToString '得意先住所
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("得意先電話番号").ToString '得意先電話番号
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("得意先ＦＡＸ").ToString '得意先ＦＡＸ
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("得意先担当者役職").ToString '得意先担当者役職
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("得意先担当者名").ToString '得意先担当者名
            Sql3 += "', '"
            Sql3 += UtilClass.strFormatDate(ds1.Tables(RS).Rows(0)("見積日").ToString) '見積日
            Sql3 += "', '"
            Sql3 += UtilClass.strFormatDate(ds1.Tables(RS).Rows(0)("見積有効期限").ToString) '見積有効期限
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("支払条件").ToString '支払条件
            Sql3 += "', '"
            Sql3 += UtilClass.formatNumber(ds1.Tables(RS).Rows(0)("見積金額").ToString) '見積金額
            Sql3 += "', '"
            Sql3 += UtilClass.formatNumber(ds1.Tables(RS).Rows(0)("見積金額").ToString) '売上金額
            Sql3 += "', '"
            Sql3 += UtilClass.formatNumber(ds1.Tables(RS).Rows(0)("粗利額").ToString) '粗利額
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("営業担当者").ToString '営業担当者
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("入力担当者").ToString '入力担当者
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("備考").ToString '備考
            Sql3 += "', "
            Sql3 += "null" '取消日
            Sql3 += ", "
            Sql3 += "0" '取消区分
            Sql3 += ", '"
            Sql3 += UtilClass.formatNumber(ds1.Tables(RS).Rows(0)("ＶＡＴ").ToString) 'ＶＡＴ
            Sql3 += "', '"
            If ds1.Tables(RS).Rows(0)("ＰＰＨ") Is DBNull.Value Then
                Sql3 += "0" 'ＰＰＨ
            Else
                Sql3 += UtilClass.formatNumber(ds1.Tables(RS).Rows(0)("ＰＰＨ").ToString) 'ＰＰＨ
            End If
            Sql3 += "', '"
            Sql3 += strToday '受注日
            Sql3 += "', '"
            Sql3 += UtilClass.strFormatDate(DtpOrderDate.Text) '売上日
            Sql3 += "', '"
            Sql3 += UtilClass.strFormatDate(DtpDepositDate.Text) '入金予定日
            Sql3 += "', '"
            Sql3 += strToday '登録日
            Sql3 += "', '"
            Sql3 += strToday '更新日
            Sql3 += "', '"
            Sql3 += Input '更新者
            Sql3 += "', '"
            Sql3 += UtilClass.formatNumber(ds1.Tables(RS).Rows(0)("仕入金額").ToString) '仕入金額
            Sql3 += " ')"


            _db.executeDB(Sql3)

            For index As Integer = 0 To DgvAdd.Rows.Count() - 1
                Sql4 = ""
                Sql4 += "INSERT INTO "
                Sql4 += "Public."
                Sql4 += "t31_urigdt("
                Sql4 += "会社コード, 売上番号, 売上番号枝番, 受注番号, 受注番号枝番, 行番号, 仕入区分, メーカー, 品名, 型式, 仕入先名, 仕入値, 受注数量, 売上数量, 受注残数, 単位, 売単価, 売上金額, 粗利額, 粗利率, 間接費, リードタイム, 備考, 更新者, 更新日, 仕入原価, 関税率, 関税額, 前払法人税率, 前払法人税額, 輸送費率, 輸送費額, 仕入金額, 見積単価, 見積金額)"
                Sql4 += " VALUES('"
                Sql4 += ds2.Tables(RS).Rows(index)("会社コード").ToString
                Sql4 += "', '"
                Sql4 += ER
                Sql4 += "', '"
                Sql4 += "1"
                Sql4 += "', '"
                Sql4 += ds2.Tables(RS).Rows(index)("受注番号").ToString
                Sql4 += "', '"
                Sql4 += ds2.Tables(RS).Rows(index)("受注番号枝番").ToString
                Sql4 += "', '"
                Sql4 += DgvAdd.Rows(index).Cells("行番号").Value.ToString
                Sql4 += "', '"
                Sql4 += DgvAdd.Rows(index).Cells("仕入区分").Value.ToString
                Sql4 += "', '"
                Sql4 += DgvAdd.Rows(index).Cells("メーカー").Value.ToString
                Sql4 += "', '"
                Sql4 += DgvAdd.Rows(index).Cells("品名").Value.ToString
                Sql4 += "', '"
                Sql4 += DgvAdd.Rows(index).Cells("型式").Value.ToString
                Sql4 += "', '"
                Sql4 += DgvAdd.Rows(index).Cells("仕入先").Value.ToString
                Sql4 += "', '"
                Sql4 += UtilClass.formatNumber(ds2.Tables(RS).Rows(index)("仕入値").ToString)
                Sql4 += "', '"
                Sql4 += UtilClass.formatNumber(ds2.Tables(RS).Rows(index)("受注数量").ToString)
                Sql4 += "', '"
                Sql4 += UtilClass.formatNumber(DgvAdd.Rows(index).Cells("売上数量").Value.ToString)
                Sql4 += "', '"

                Dim OrderNo As Integer = ds2.Tables(RS).Rows(index)("受注数量")
                Dim PurchaseNo As Integer = DgvAdd.Rows(index).Cells("売上数量").Value
                Dim RemainingNo As Integer = OrderNo - PurchaseNo

                Sql4 += (RemainingNo.ToString)
                Sql4 += "', '"
                Sql4 += DgvAdd.Rows(index).Cells("単位").Value
                Sql4 += "', '"
                Sql4 += UtilClass.formatNumber(ds2.Tables(RS).Rows(index)("売単価").ToString)
                Sql4 += "', '"
                Sql4 += UtilClass.formatNumber(ds2.Tables(RS).Rows(index)("売上金額").ToString)
                Sql4 += "', '"
                Sql4 += UtilClass.formatNumber(ds2.Tables(RS).Rows(index)("粗利額").ToString)
                Sql4 += "', '"
                Sql4 += UtilClass.formatNumber(ds2.Tables(RS).Rows(index)("粗利率").ToString)
                Sql4 += "', '"
                Sql4 += UtilClass.formatNumber(ds2.Tables(RS).Rows(index)("間接費").ToString)
                Sql4 += "', '"
                Sql4 += ds2.Tables(RS).Rows(index)("リードタイム").ToString
                Sql4 += "', '"
                Sql4 += DgvAdd.Rows(index).Cells("備考").Value
                Sql4 += "', '"
                Sql4 += Input
                Sql4 += "', '"
                Sql4 += strToday
                Sql4 += "', '"
                Sql4 += UtilClass.formatNumber(ds2.Tables(RS).Rows(index)("仕入原価").ToString)
                Sql4 += "', '"
                Sql4 += UtilClass.formatNumber(ds2.Tables(RS).Rows(index)("関税率").ToString)
                Sql4 += "', '"
                Sql4 += UtilClass.formatNumber(ds2.Tables(RS).Rows(index)("関税額").ToString)
                Sql4 += "', '"
                Sql4 += UtilClass.formatNumber(ds2.Tables(RS).Rows(index)("前払法人税率").ToString)
                Sql4 += "', '"
                Sql4 += UtilClass.formatNumber(ds2.Tables(RS).Rows(index)("前払法人税額").ToString)
                Sql4 += "', '"
                Sql4 += UtilClass.formatNumber(ds2.Tables(RS).Rows(index)("輸送費率").ToString)
                Sql4 += "', '"
                Sql4 += UtilClass.formatNumber(ds2.Tables(RS).Rows(index)("輸送費額").ToString)
                Sql4 += "', '"
                Sql4 += UtilClass.formatNumber(ds2.Tables(RS).Rows(index)("仕入金額").ToString)
                Sql4 += "', '"
                Sql4 += UtilClass.formatNumber(ds2.Tables(RS).Rows(index)("見積単価").ToString)
                Sql4 += "', '"
                Sql4 += UtilClass.formatNumber(ds2.Tables(RS).Rows(index)("見積金額").ToString)
                Sql4 += " ')"
                If DgvAdd.Rows(index).Cells("売上数量").Value = 0 Then
                Else
                    _db.executeDB(Sql4)
                End If
            Next

            Dim ERNo As Integer

            If dsSaiban1.Tables(RS).Rows(0)("最新値") = dsSaiban1.Tables(RS).Rows(0)("最大値") Then
                ERNo = dsSaiban1.Tables(RS).Rows(0)("最小値")
            Else
                ERNo = dsSaiban1.Tables(RS).Rows(0)("最新値") + 1
            End If

            Sql5 = ""
            Sql5 += "UPDATE "
            Sql5 += "Public."
            Sql5 += "m80_saiban "
            Sql5 += "SET "
            Sql5 += " 最新値"
            Sql5 += " = '"
            Sql5 += ERNo.ToString
            Sql5 += "', "
            Sql5 += "更新者"
            Sql5 += " = '"
            Sql5 += Input
            Sql5 += "', "
            Sql5 += "更新日"
            Sql5 += " = '"
            Sql5 += strToday
            Sql5 += "' "
            Sql5 += "WHERE"
            Sql5 += " 会社コード"
            Sql5 += "='"
            Sql5 += ds1.Tables(RS).Rows(0)("会社コード").ToString
            Sql5 += "'"
            Sql5 += " AND"
            Sql5 += " 採番キー"
            Sql5 += "='"
            Sql5 += "40"
            Sql5 += "' "

            _db.executeDB(Sql5)

            Dim PurchaseNum As Integer
            Dim OrdingNum As Integer
            For index As Integer = 0 To DgvAdd.Rows.Count() - 1
                Sql6 = ""
                Sql6 += "UPDATE "
                Sql6 += "Public."
                Sql6 += "t11_cymndt "
                Sql6 += "SET "
                Sql6 += "売上数量"
                Sql6 += " = '"
                PurchaseNum = ds2.Tables(RS).Rows(index)("売上数量") + DgvAdd.Rows(index).Cells("売上数量").Value
                Sql6 += UtilClass.formatNumber(PurchaseNum.ToString)
                Sql6 += "', "
                Sql6 += " 受注残数"
                Sql6 += " = '"
                OrdingNum = ds2.Tables(RS).Rows(index)("受注残数") - DgvAdd.Rows(index).Cells("売上数量").Value
                Sql6 += UtilClass.formatNumber(OrdingNum.ToString)
                Sql6 += "', "
                Sql6 += "更新者"
                Sql6 += " = '"
                Sql6 += Input
                Sql6 += "' "
                Sql6 += "WHERE"
                Sql6 += " 会社コード"
                Sql6 += "='"
                Sql6 += ds1.Tables(RS).Rows(0)("会社コード").ToString
                Sql6 += "'"
                Sql6 += " AND"
                Sql6 += " 受注番号"
                Sql6 += "='"
                Sql6 += ds2.Tables(RS).Rows(index)("受注番号").ToString
                Sql6 += "'"
                Sql6 += " AND"
                Sql6 += " 受注番号枝番"
                Sql6 += "='"
                Sql6 += ds2.Tables(RS).Rows(index)("受注番号枝番").ToString
                Sql6 += "'"
                Sql6 += " AND"
                Sql6 += " 行番号"
                Sql6 += "='"
                Sql6 += ds2.Tables(RS).Rows(index)("行番号").ToString
                Sql6 += "' "
                If DgvAdd.Rows(index).Cells("売上数量").Value = 0 Then
                Else
                    _db.executeDB(Sql6)
                End If
            Next

            Dim Sql As String = ""
            Sql += "SELECT * FROM public.t45_shukodt"
            Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND 受注番号 = '" & No & "'"
            Sql += " AND 受注番号枝番 = '" & Suffix & "'"

            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)
            For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                Sql = ""
                Sql += "UPDATE Public.t45_shukodt "
                Sql += "SET "
                Sql += " 出庫区分"
                Sql += " = '"
                Sql += "2"
                Sql += "', "
                Sql += "更新者"
                Sql += " = '"
                Sql += frmC01F10_Login.loginValue.TantoNM
                Sql += "', "
                Sql += "更新日"
                Sql += " = '"
                Sql += strToday
                Sql += "' "
                Sql += "WHERE"
                Sql += " 会社コード"
                Sql += "='"
                Sql += frmC01F10_Login.loginValue.BumonCD
                Sql += "'"
                Sql += " AND "
                Sql += "出庫番号"
                Sql += "='"
                Sql += ds.Tables(RS).Rows(i)("出庫番号").ToString
                Sql += "' "
                Sql += " AND "
                Sql += "行番号"
                Sql += "='"
                Sql += ds.Tables(RS).Rows(i)("行番号").ToString
                Sql += "' "

                _db.executeDB(Sql)
            Next


            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                Dim result As DialogResult = MessageBox.Show("Create billing data?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2)
                If result = DialogResult.Yes Then
                    Biilng()
                End If
            Else
                Dim result As DialogResult = MessageBox.Show("請求データを作成しますか？", "質問", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2)
                If result = DialogResult.Yes Then
                    Biilng()
                End If
            End If

            Dim openForm As Form = Nothing
            Dim Status As String = CommonConst.STATUS_SALES
            openForm = New OrderList(_msgHd, _db, _langHd, Status)
            openForm.Show()
            Me.Close()
        End If
    End Sub

    Private Sub Biilng()
        Dim dtToday As DateTime = DateTime.Now
        Dim strToday As String = UtilClass.formatDatetime(dtToday)

        Dim reccnt As Integer = 0
        Dim BillingAmount As Integer = 0
        For index As Integer = 0 To DgvAdd.Rows.Count - 1
            BillingAmount += DgvAdd.Rows(index).Cells("売単価").Value * DgvAdd.Rows(index).Cells("売上数量").Value
        Next

        Dim Saiban1 As String = ""
        Dim Sql1 As String = ""
        Dim Sql2 As String = ""
        Dim Sql3 As String = ""
        Dim Sql4 As String = ""

        Saiban1 += "SELECT * FROM public.m80_saiban"
        Saiban1 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Saiban1 += " AND 採番キー = '80'"

        Dim dsSaiban1 As DataSet = _db.selectDB(Saiban1, RS, reccnt)

        Dim DM As String = dsSaiban1.Tables(RS).Rows(0)("接頭文字")
        DM += dtToday.ToString("MMdd")
        DM += dsSaiban1.Tables(RS).Rows(0)("最新値").ToString.PadLeft(dsSaiban1.Tables(RS).Rows(0)("連番桁数"), "0")

        Sql1 += "SELECT * FROM public.t10_cymnhd"
        Sql1 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql1 += " AND 受注番号 = '" & No & "'"
        Sql1 += " AND 受注番号枝番 = '" & Suffix & "'"
        Sql1 += " AND 取消区分 = " & CommonConst.FLAG_ENABLED.ToString

        Dim ds1 As DataSet = _db.selectDB(Sql1, RS, reccnt)

        Sql2 += "SELECT * FROM public.t23_skyuhd"
        Sql2 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql2 += " AND 受注番号 = '" & No & "'"
        Sql2 += " AND 受注番号枝番 ='" & Suffix & "'"
        Sql2 += " AND 取消区分 = " & CommonConst.FLAG_ENABLED.ToString

        Dim ds2 As DataSet = _db.selectDB(Sql2, RS, reccnt)
        Dim skyuPrice As Integer = 0
        If ds2.Tables(RS).Rows.Count > 0 Then
            For i As Integer = 0 To ds2.Tables(RS).Rows.Count - 1
                skyuPrice += ds2.Tables(RS).Rows(i)("請求金額計")
            Next
        End If

        Dim tmp As Decimal = 0

        Try

            Sql3 = ""
            Sql3 += "INSERT INTO "
            Sql3 += "Public."
            Sql3 += "t23_skyuhd("
            Sql3 += "会社コード, 請求番号, 客先番号, 請求区分, 請求日, 受注番号, 受注番号枝番, 得意先コード"
            Sql3 += ", 得意先名, 請求金額計, 入金額計, 売掛残高, 備考1, 備考2, 取消区分, 入金予定日, 登録日, 更新者, 更新日)"
            Sql3 += " VALUES('"
            Sql3 += ds1.Tables(RS).Rows(0)("会社コード").ToString
            Sql3 += "', '"
            Sql3 += DM
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("客先番号").ToString
            Sql3 += "', '"
            Sql3 += "1"
            Sql3 += "', '"
            Sql3 += UtilClass.strFormatDate(DtpOrderDate.Value)
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("受注番号").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("受注番号枝番").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("得意先コード").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("得意先名").ToString
            Sql3 += "', '"
            tmp = BillingAmount.ToString - skyuPrice
            Sql3 += UtilClass.escapeSql(tmp)
            Sql3 += "', 0"
            Sql3 += ", '"
            Sql3 += UtilClass.escapeSql(tmp)
            Sql3 += "', '"
            Sql3 += TxtRemarks.Text
            Sql3 += "', '"
            Sql3 += DgvAdd.Rows(0).Cells("備考").Value
            Sql3 += "', '"
            Sql3 += "0"
            Sql3 += "', '"
            Sql3 += UtilClass.strFormatDate(DtpDepositDate.Text)
            Sql3 += "', '"
            Sql3 += strToday
            Sql3 += "', '"
            Sql3 += frmC01F10_Login.loginValue.TantoNM
            Sql3 += "', '"
            Sql3 += strToday
            Sql3 += " ')"

            _db.executeDB(Sql3)

            Dim DMNo As Integer

            If dsSaiban1.Tables(RS).Rows(0)("最新値") = dsSaiban1.Tables(RS).Rows(0)("最大値") Then
                DMNo = dsSaiban1.Tables(RS).Rows(0)("最小値")
            Else
                DMNo = dsSaiban1.Tables(RS).Rows(0)("最新値") + 1
            End If

            Sql4 = "UPDATE "
            Sql4 += "Public."
            Sql4 += "m80_saiban "
            Sql4 += "SET "
            Sql4 += " 最新値"
            Sql4 += " = '"
            Sql4 += DMNo.ToString
            Sql4 += "', "
            Sql4 += "更新者"
            Sql4 += " = '"
            Sql4 += frmC01F10_Login.loginValue.TantoNM
            Sql4 += "', "
            Sql4 += "更新日"
            Sql4 += " = '"
            Sql4 += strToday
            Sql4 += "' "
            Sql4 += "WHERE"
            Sql4 += " 会社コード"
            Sql4 += "='"
            Sql4 += ds1.Tables(RS).Rows(0)("会社コード").ToString
            Sql4 += "'"
            Sql4 += " AND"
            Sql4 += " 採番キー"
            Sql4 += "='"
            Sql4 += "80"
            Sql4 += "' "

            _db.executeDB(Sql4)

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Sub

End Class