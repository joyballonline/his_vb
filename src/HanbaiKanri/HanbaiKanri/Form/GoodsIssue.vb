Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices
Imports System.Globalization

Public Class GoodsIssue
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
    Private _status As String = ""
    Private _langHd As UtilLangHandler
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
        _init = True

    End Sub

    '画面表示時
    Private Sub GoodsIssue_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblOrderNo.Text = "JobOrderNumber"
            LblCustomerNo.Text = "CustomerNumber"
            LblOrderDate.Text = "JobOrderDate"
            LblCustomer.Text = "CustomerName"
            LblOrder.Text = "JobOrder"
            LblHistory.Text = "GoodsDeliveryHistory"
            LblAdd.Text = "ShipmentThisTime"
            LblGoodsIssueDate.Text = "GoodsDeliveryDate"
            LblRemarks.Text = "Remarks"
            LblCount1.Text = "Record"
            LblCount1.Location = New Point(1272, 82)
            LblCount1.Size = New Size(66, 22)
            LblCount2.Text = "Record"
            LblCount2.Location = New Point(1272, 212)
            LblCount2.Size = New Size(66, 22)
            LblCount3.Text = "Record"
            LblCount3.Location = New Point(1272, 343)
            LblCount3.Size = New Size(66, 22)

            TxtCount1.Location = New Point(1228, 82)
            TxtCount2.Location = New Point(1228, 212)
            TxtCount3.Location = New Point(1228, 343)
            TxtRemarks.Size = New Size(600, 22)

            BtnDeliveryNote.Text = "Invoice/Receipt Issue"
            BtnRegist.Text = "Registration"
            BtnBack.Text = "Back"
        End If

        '参照モード
        If _status = CommonConst.STATUS_VIEW Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "ViewMode"
            Else
                LblMode.Text = "参照モード"
            End If
            LblCount1.Visible = False
            LblCount2.Visible = False
            LblCount3.Visible = False
            LblOrder.Visible = False
            LblAdd.Visible = False
            LblGoodsIssueDate.Visible = False
            LblRemarks.Visible = False
            DtpGoodsIssueDate.Visible = False
            TxtCount1.Visible = False
            TxtCount2.Visible = False
            TxtCount3.Visible = False
            TxtRemarks.Visible = False
            DgvOrder.Visible = False
            DgvAdd.Visible = False
            DgvHistory.ReadOnly = True

            LblHistory.Location = New Point(12, 82)
            DgvHistory.Location = New Point(12, 106)
            DgvHistory.Size = New Point(1326, 400)

            BtnRegist.Visible = False
            BtnDeliveryNote.Visible = True
            BtnDeliveryNote.Location = New Point(1002, 509)

        Else

            '入力モード

            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "GoodsDelivelyInputMode"
            Else
                LblMode.Text = "出庫入力モード"
            End If

        End If

        getlist() '内容表示

    End Sub

    Private Sub getlist()
        Dim Sql As String = ""
        Dim reccnt As Integer = 0
        Dim sireKbn As DataSet

        Try

            Sql = " AND "
            Sql += "受注番号 ILIKE '" & No & "'"
            Sql += " AND "
            Sql += "受注番号枝番 ILIKE '" & Suffix & "'"


            Dim dsCymnhd As DataSet = getDsData("t10_cymnhd", Sql)


            Sql = "SELECT t45.*"
            Sql += " FROM  public.t45_shukodt t45 "
            Sql += " INNER JOIN  t44_shukohd t44"
            Sql += " ON t45.会社コード = t44.会社コード"
            Sql += " AND  t45.出庫番号 = t44.出庫番号"

            Sql += " WHERE t45.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND "
            Sql += "t45.受注番号 ILIKE '" & No & "'"
            Sql += " AND "
            Sql += "t45.受注番号枝番 ILIKE '" & Suffix & "'"
            Sql += " ORDER BY t45.行番号"

            Dim dsShukodt As DataSet = _db.selectDB(Sql, RS, reccnt)

            Sql = "SELECT t11.*, t10.更新日"
            Sql += " FROM  public.t11_cymndt t11 "
            Sql += " INNER JOIN  t10_cymnhd t10"
            Sql += " ON t11.会社コード = t10.会社コード"
            Sql += " AND  t11.受注番号 = t10.受注番号"
            Sql += " AND  t11.受注番号枝番 = t10.受注番号枝番"

            Sql += " WHERE t11.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND "
            Sql += "t11.受注番号 ILIKE '" & No & "'"
            Sql += " AND "
            Sql += "t11.受注番号枝番 ILIKE '" & Suffix & "'"
            Sql += " ORDER BY t11.行番号"

            Dim dsCymndt As DataSet = _db.selectDB(Sql, RS, reccnt)

            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                DgvOrder.Columns.Add("明細", "DetailData")
                DgvOrder.Columns.Add("メーカー", "Manufacturer")
                DgvOrder.Columns.Add("品名", "ItemName")
                DgvOrder.Columns.Add("型式", "Spec")
                DgvOrder.Columns.Add("受注数量", "JobOrderQuantity")
                DgvOrder.Columns.Add("単位", "Unit")
                DgvOrder.Columns.Add("売上数量", "SalesQuantity")
                DgvOrder.Columns.Add("売単価", "SellingPrice")
                DgvOrder.Columns.Add("売上金額", "SalesAmount")
                DgvOrder.Columns.Add("受注残数", "OrderRemainingAmount")
                DgvOrder.Columns.Add("未出庫数", "NoShippedQntity")
                DgvOrder.Columns.Add("更新日", "更新日")
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
                DgvOrder.Columns.Add("未出庫数", "未出庫数")
                DgvOrder.Columns.Add("更新日", "更新日")
            End If

            DgvOrder.Columns("受注数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvOrder.Columns("売上数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvOrder.Columns("売単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvOrder.Columns("売上金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvOrder.Columns("受注残数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvOrder.Columns("未出庫数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvOrder.Columns("更新日").Visible = False

            For i As Integer = 0 To dsCymndt.Tables(RS).Rows.Count - 1
                DgvOrder.Rows.Add()
                DgvOrder.Rows(i).Cells("メーカー").Value = dsCymndt.Tables(RS).Rows(i)("メーカー")
                DgvOrder.Rows(i).Cells("品名").Value = dsCymndt.Tables(RS).Rows(i)("品名")
                DgvOrder.Rows(i).Cells("型式").Value = dsCymndt.Tables(RS).Rows(i)("型式")
                DgvOrder.Rows(i).Cells("受注数量").Value = dsCymndt.Tables(RS).Rows(i)("受注数量")
                DgvOrder.Rows(i).Cells("単位").Value = dsCymndt.Tables(RS).Rows(i)("単位")
                DgvOrder.Rows(i).Cells("売上数量").Value = dsCymndt.Tables(RS).Rows(i)("売上数量")
                DgvOrder.Rows(i).Cells("売単価").Value = dsCymndt.Tables(RS).Rows(i)("売単価")
                DgvOrder.Rows(i).Cells("売上金額").Value = dsCymndt.Tables(RS).Rows(i)("売上金額")
                DgvOrder.Rows(i).Cells("受注残数").Value = dsCymndt.Tables(RS).Rows(i)("受注残数")
                DgvOrder.Rows(i).Cells("未出庫数").Value = dsCymndt.Tables(RS).Rows(i)("未出庫数")
                DgvOrder.Rows(i).Cells("更新日").Value = dsCymndt.Tables(RS).Rows(i)("更新日")
            Next

            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                DgvHistory.Columns.Add("No", "No")
                DgvHistory.Columns.Add("出庫番号", "GoodsDeliveryNumber")
                DgvHistory.Columns.Add("行番号", "LineNumber")
                DgvHistory.Columns.Add("仕入区分", "PurchasingClassification")
                DgvHistory.Columns.Add("メーカー", "Manufacturer")
                DgvHistory.Columns.Add("品名", "ItemName")
                DgvHistory.Columns.Add("型式", "Spec")
                DgvHistory.Columns.Add("単位", "Unit")
                DgvHistory.Columns.Add("仕入先", "SupplierName")
                DgvHistory.Columns.Add("売単価", "SellingPrice")
                DgvHistory.Columns.Add("出庫数量", "GoodsDeliveryQuantity")
                DgvHistory.Columns.Add("備考", "Remarks")
            Else
                DgvHistory.Columns.Add("No", "No")
                DgvHistory.Columns.Add("出庫番号", "出庫番号")
                DgvHistory.Columns.Add("行番号", "行番号")
                DgvHistory.Columns.Add("仕入区分", "仕入区分")
                DgvHistory.Columns.Add("メーカー", "メーカー")
                DgvHistory.Columns.Add("品名", "品名")
                DgvHistory.Columns.Add("型式", "型式")
                DgvHistory.Columns.Add("単位", "単位")
                DgvHistory.Columns.Add("仕入先", "仕入先")
                DgvHistory.Columns.Add("売単価", "売単価")
                DgvHistory.Columns.Add("出庫数量", "出庫数量")
                DgvHistory.Columns.Add("備考", "備考")
            End If

            DgvHistory.Columns("売単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvHistory.Columns("出庫数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            For i As Integer = 0 To dsShukodt.Tables(RS).Rows.Count - 1
                '汎用マスタから仕入区分名称取得
                sireKbn = getDsHanyoData(CommonConst.FIXED_KEY_PURCHASING_CLASS, dsShukodt.Tables(RS).Rows(i)("仕入区分").ToString)

                DgvHistory.Rows.Add()
                DgvHistory.Rows(i).Cells("出庫番号").Value = dsShukodt.Tables(RS).Rows(i)("出庫番号")
                DgvHistory.Rows(i).Cells("行番号").Value = dsShukodt.Tables(RS).Rows(i)("行番号")
                DgvHistory.Rows(i).Cells("仕入区分").Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                                                                sireKbn.Tables(RS).Rows(0)("文字２"),
                                                                sireKbn.Tables(RS).Rows(0)("文字１"))
                DgvHistory.Rows(i).Cells("メーカー").Value = dsShukodt.Tables(RS).Rows(i)("メーカー")
                DgvHistory.Rows(i).Cells("品名").Value = dsShukodt.Tables(RS).Rows(i)("品名")
                DgvHistory.Rows(i).Cells("型式").Value = dsShukodt.Tables(RS).Rows(i)("型式")
                DgvHistory.Rows(i).Cells("単位").Value = dsShukodt.Tables(RS).Rows(i)("単位")
                DgvHistory.Rows(i).Cells("仕入先").Value = dsShukodt.Tables(RS).Rows(i)("仕入先名")
                DgvHistory.Rows(i).Cells("売単価").Value = dsShukodt.Tables(RS).Rows(i)("売単価")
                DgvHistory.Rows(i).Cells("出庫数量").Value = dsShukodt.Tables(RS).Rows(i)("出庫数量")
                DgvHistory.Rows(i).Cells("備考").Value = dsShukodt.Tables(RS).Rows(i)("備考")
            Next

            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                DgvAdd.Columns.Add("No", "No")
                DgvAdd.Columns.Add("行番号", "LineNumber")
                DgvAdd.Columns.Add("仕入区分", "PurchasingClassification")
                DgvAdd.Columns.Add("仕入区分値", "仕入区分値")
                DgvAdd.Columns.Add("メーカー", "Manufacturer")
                DgvAdd.Columns.Add("品名", "ItemName")
                DgvAdd.Columns.Add("型式", "Spec")
                DgvAdd.Columns.Add("単位", "Unit")
                DgvAdd.Columns.Add("仕入先", "SupplierName")
                DgvAdd.Columns.Add("売単価", "SellingPrice")
                DgvAdd.Columns.Add("出庫数量", "GoodsDeliveryQuantity")
                DgvAdd.Columns.Add("備考", "Remarks")
            Else
                DgvAdd.Columns.Add("No", "No")
                DgvAdd.Columns.Add("行番号", "行番号")
                DgvAdd.Columns.Add("仕入区分", "仕入区分")
                DgvAdd.Columns.Add("仕入区分値", "仕入区分値")
                DgvAdd.Columns.Add("メーカー", "メーカー")
                DgvAdd.Columns.Add("品名", "品名")
                DgvAdd.Columns.Add("型式", "型式")
                DgvAdd.Columns.Add("単位", "単位")
                DgvAdd.Columns.Add("仕入先", "仕入先")
                DgvAdd.Columns.Add("売単価", "売単価")
                DgvAdd.Columns.Add("出庫数量", "出庫数量")
                DgvAdd.Columns.Add("備考", "備考")
            End If



            DgvAdd.Columns("売単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvAdd.Columns("出庫数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            DgvAdd.Columns("No").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvAdd.Columns("No").ReadOnly = True
            DgvAdd.Columns("行番号").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvAdd.Columns("行番号").ReadOnly = True
            DgvAdd.Columns("仕入区分").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvAdd.Columns("仕入区分").ReadOnly = True
            DgvAdd.Columns("仕入区分値").Visible = False
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
            DgvAdd.Columns("売単価").ReadOnly = True

            For i As Integer = 0 To dsCymndt.Tables(RS).Rows.Count - 1
                If dsCymndt.Tables(RS).Rows(i)("受注残数") <> 0 Then
                    '汎用マスタから仕入区分名称取得
                    sireKbn = getDsHanyoData(CommonConst.FIXED_KEY_PURCHASING_CLASS, dsShukodt.Tables(RS).Rows(i)("仕入区分").ToString)

                    DgvAdd.Rows.Add()
                    DgvAdd.Rows(i).Cells("行番号").Value = dsCymndt.Tables(RS).Rows(i)("行番号")
                    DgvAdd.Rows(i).Cells("仕入区分").Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                                                                sireKbn.Tables(RS).Rows(0)("文字２"),
                                                                sireKbn.Tables(RS).Rows(0)("文字１"))
                    DgvAdd.Rows(i).Cells("仕入区分値").Value = dsShukodt.Tables(RS).Rows(i)("仕入区分")
                    DgvAdd.Rows(i).Cells("メーカー").Value = dsCymndt.Tables(RS).Rows(i)("メーカー")
                    DgvAdd.Rows(i).Cells("品名").Value = dsCymndt.Tables(RS).Rows(i)("品名")
                    DgvAdd.Rows(i).Cells("型式").Value = dsCymndt.Tables(RS).Rows(i)("型式")
                    DgvAdd.Rows(i).Cells("仕入先").Value = dsCymndt.Tables(RS).Rows(i)("仕入先名")
                    DgvAdd.Rows(i).Cells("単位").Value = dsCymndt.Tables(RS).Rows(i)("単位")
                    DgvAdd.Rows(i).Cells("売単価").Value = dsCymndt.Tables(RS).Rows(i)("売単価")
                    DgvAdd.Rows(i).Cells("出庫数量").Value = 0
                    DgvAdd.Rows(i).Cells("備考").Value = dsCymndt.Tables(RS).Rows(i)("備考")
                End If
            Next

            '行番号の振り直し
            'なぜ？
            Dim i1 As Integer = DgvOrder.Rows.Count()
            Dim No1 As Integer = 1
            For i As Integer = 0 To i1 - 1
                DgvOrder.Rows(i).Cells("明細").Value = No1
                No1 += 1
            Next i
            TxtCount1.Text = DgvOrder.Rows.Count()

            Dim i2 As Integer = DgvHistory.Rows.Count()
            Dim No2 As Integer = 1
            For i As Integer = 0 To i2 - 1
                DgvHistory.Rows(i).Cells("No").Value = No2
                No2 += 1
            Next i
            TxtCount2.Text = DgvHistory.Rows.Count()

            Dim i3 As Integer = DgvAdd.Rows.Count()
            Dim No3 As Integer = 1
            For i As Integer = 0 To i3 - 1
                DgvAdd.Rows(i).Cells("No").Value = No3
                No3 += 1
            Next i
            TxtCount3.Text = DgvAdd.Rows.Count()

            TxtOrderNo.Text = dsCymnhd.Tables(RS).Rows(0)("受注番号")
            TxtSuffixNo.Text = dsCymnhd.Tables(RS).Rows(0)("受注番号枝番")
            TxtCustomerPO.Text = dsCymnhd.Tables(RS).Rows(0)("客先番号").ToString
            TxtOrderDate.Text = dsCymnhd.Tables(RS).Rows(0)("受注日")
            TxtCustomerCode.Text = dsCymnhd.Tables(RS).Rows(0)("得意先コード")
            TxtCustomerName.Text = dsCymnhd.Tables(RS).Rows(0)("得意先名")
            DtpGoodsIssueDate.Value = Date.Now '出庫日
            DtpGoodsIssueDate.MinDate = dsCymnhd.Tables(RS).Rows(0)("受注日") '出庫日のMinDateに受注日を設定する

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    'セルの値が変更されたら
    Private Sub CellValueChanged(ByVal sender As Object,
    ByVal e As DataGridViewCellEventArgs) Handles DgvAdd.CellValueChanged

        Dim PurchaseTotal As Integer = 0

        'ヘッダー以外だったら
        If e.RowIndex > -1 Then

            '各項目の属性チェック
            If Not IsNumeric(DgvAdd.Rows(e.RowIndex).Cells("出庫数量").Value) And (DgvAdd.Rows(e.RowIndex).Cells("出庫数量").Value IsNot Nothing) Then
                _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
                DgvAdd.Rows(e.RowIndex).Cells("出庫数量").Value = 0
                Exit Sub
            End If
        End If

    End Sub

    '戻るボタン押下時
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        'Dim openForm As Form = Nothing
        'openForm = New frmC01F30_Menu(_msgHd, _langHd, _db)
        'openForm.Show()
        Me.Close()
    End Sub

    '登録ボタン押下時
    Private Sub BtnRegist_Click(sender As Object, e As EventArgs) Handles BtnRegist.Click

        Dim dtToday As DateTime = DateTime.Now
        Dim strToday As String = strFormatDate(DateTime.Now)

        Dim reccnt As Integer = 0

        Dim Sql As String = ""

        Sql = " AND "
        Sql += "受注番号 ILIKE '" & No & "'"
        Sql += " AND "
        Sql += "受注番号枝番 ILIKE '" & Suffix & "'"
        Sql += " AND "
        Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED '取消区分=0

        Dim dsCymnhd As DataSet = getDsData("t10_cymnhd", Sql)

        '対象データがなかったらメッセージを表示
        If DgvAdd.RowCount = 0 Then
            '操作できないメッセージを表示
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)

            Return
        End If

        Dim chkGIAmount As Integer = 0 '出庫データがあるか合算する用
        '最初に今回入庫に入力がなかったらエラーで返す
        For i As Integer = 0 To DgvAdd.RowCount - 1
            chkGIAmount += DgvAdd.Rows(i).Cells("出庫数量").Value
        Next

        If chkGIAmount <= 0 Then
            '対象データがないメッセージを表示
            _msgHd.dspMSG("chkGoodsIssueAddError", frmC01F10_Login.loginValue.Language)

            Return
        End If

        For i As Integer = 0 To DgvAdd.Rows.Count() - 1

            '出庫数が受注数を超えたら
            If DgvOrder.Rows(i).Cells("受注数量").Value < DgvOrder.Rows(i).Cells("売上数量").Value + DgvAdd.Rows(i).Cells("出庫数量").Value Then

                '操作できないアラートを出す
                _msgHd.dspMSG("chkGIBalanceError", frmC01F10_Login.loginValue.Language)

                Return
            End If

        Next

        If DgvOrder.Rows(0).Cells("更新日").Value <> dsCymnhd.Tables(RS).Rows(0)("更新日") Then
            '画面を開いたときの日時とデータの日時が異なっていた場合
            'データが誰かに変更された旨を伝える
            _msgHd.dspMSG("chkData", frmC01F10_Login.loginValue.Language)

            Return
        End If


        '採番データを取得・更新
        'Dim ER As String = getSaiban("40", dtToday.ToShortDateString())
        Dim LS As String = getSaiban("70", dtToday.ToShortDateString())

        Sql = "INSERT INTO "
        Sql += "Public."
        Sql += "t44_shukohd("
        Sql += "会社コード, 出庫番号, 見積番号, 見積番号枝番, 受注番号, 受注番号枝番, 客先番号, 得意先コード, 得意先名, 得意先郵便番号, 得意先住所, 得意先電話番号, 得意先ＦＡＸ, 得意先担当者役職, 得意先担当者名, 営業担当者, 入力担当者, 備考, 取消日, 取消区分, 出庫日, 登録日, 更新日, 更新者)"
        Sql += " VALUES('"
        Sql += dsCymnhd.Tables(RS).Rows(0)("会社コード").ToString
        Sql += "', '"
        Sql += LS
        Sql += "', '"
        Sql += dsCymnhd.Tables(RS).Rows(0)("見積番号").ToString
        Sql += "', '"
        Sql += dsCymnhd.Tables(RS).Rows(0)("見積番号枝番").ToString
        Sql += "', '"
        Sql += dsCymnhd.Tables(RS).Rows(0)("受注番号").ToString
        Sql += "', '"
        Sql += dsCymnhd.Tables(RS).Rows(0)("受注番号枝番").ToString
        Sql += "', '"
        Sql += dsCymnhd.Tables(RS).Rows(0)("客先番号").ToString
        Sql += "', '"
        Sql += dsCymnhd.Tables(RS).Rows(0)("得意先コード").ToString
        Sql += "', '"
        Sql += dsCymnhd.Tables(RS).Rows(0)("得意先名").ToString
        Sql += "', '"
        Sql += dsCymnhd.Tables(RS).Rows(0)("得意先郵便番号").ToString
        Sql += "', '"
        Sql += dsCymnhd.Tables(RS).Rows(0)("得意先住所").ToString
        Sql += "', '"
        Sql += dsCymnhd.Tables(RS).Rows(0)("得意先電話番号").ToString
        Sql += "', '"
        Sql += dsCymnhd.Tables(RS).Rows(0)("得意先ＦＡＸ").ToString
        Sql += "', '"
        Sql += dsCymnhd.Tables(RS).Rows(0)("得意先担当者役職").ToString
        Sql += "', '"
        Sql += dsCymnhd.Tables(RS).Rows(0)("得意先担当者名").ToString
        Sql += "', '"
        Sql += dsCymnhd.Tables(RS).Rows(0)("営業担当者").ToString
        Sql += "', '"
        Sql += dsCymnhd.Tables(RS).Rows(0)("入力担当者").ToString
        Sql += "', '"
        Sql += dsCymnhd.Tables(RS).Rows(0)("備考").ToString
        Sql += "', "
        Sql += "null"
        Sql += ", '"
        Sql += CommonConst.CANCEL_KBN_ENABLED.ToString
        Sql += "', '"
        Sql += UtilClass.formatDatetime(dtToday)
        Sql += "', '"
        Sql += UtilClass.formatDatetime(dtToday)
        Sql += "', '"
        Sql += UtilClass.formatDatetime(dtToday)
        Sql += "', '"
        Sql += Input
        Sql += " ')"

        _db.executeDB(Sql)

        For i As Integer = 0 To DgvAdd.Rows.Count() - 1

            Sql = "INSERT INTO "
            Sql += "Public."
            Sql += "t45_shukodt("
            Sql += "会社コード, 出庫番号, 受注番号, 受注番号枝番, 行番号, 仕入区分, メーカー, 品名, 型式, 仕入先名, 売単価, 出庫数量, 単位, 備考, 更新者, 更新日)"
            Sql += " VALUES('"
            Sql += dsCymnhd.Tables(RS).Rows(0)("会社コード").ToString
            Sql += "', '"
            Sql += LS
            Sql += "', '"
            Sql += dsCymnhd.Tables(RS).Rows(0)("受注番号").ToString
            Sql += "', '"
            Sql += dsCymnhd.Tables(RS).Rows(0)("受注番号枝番").ToString
            Sql += "', '"
            Sql += DgvAdd.Rows(i).Cells("No").Value.ToString
            Sql += "', '"
            Sql += DgvAdd.Rows(i).Cells("仕入区分値").Value.ToString
            Sql += "', '"
            Sql += DgvAdd.Rows(i).Cells("メーカー").Value.ToString
            Sql += "', '"
            Sql += DgvAdd.Rows(i).Cells("品名").Value.ToString
            Sql += "', '"
            Sql += DgvAdd.Rows(i).Cells("型式").Value.ToString
            Sql += "', '"
            Sql += DgvAdd.Rows(i).Cells("仕入先").Value.ToString
            Sql += "', '"
            Sql += formatNumber(DgvAdd.Rows(i).Cells("売単価").Value.ToString)
            Sql += "', '"
            Sql += formatNumber(DgvAdd.Rows(i).Cells("出庫数量").Value.ToString)
            Sql += "', '"
            Sql += DgvAdd.Rows(i).Cells("単位").Value.ToString
            Sql += "', '"
            Sql += DgvAdd.Rows(i).Cells("備考").Value.ToString
            Sql += "', '"
            Sql += Input
            Sql += "', '"
            Sql += UtilClass.formatDatetime(dtToday)
            Sql += " ')"

            If DgvAdd.Rows(i).Cells("出庫数量").Value > 0 Then
                _db.executeDB(Sql)
            End If
        Next

        'Dim openForm As Form = Nothing
        'openForm = New OrderingList(_msgHd, _db, _langHd, Me, CommonConst.STATUS_GOODS_ISSUE)
        'openForm.Show()
        'Me.Close()

        Me.Dispose()
    End Sub

    Private Sub BtnDeliveryNote_Click(sender As Object, e As EventArgs) Handles BtnDeliveryNote.Click
        Dim SelectedRow As Integer = DgvHistory.CurrentCell.RowIndex

        Dim createFlg = False

        Dim Sql1 As String = ""
        Sql1 += "SELECT "
        Sql1 += "* "
        Sql1 += "FROM "
        Sql1 += "public"
        Sql1 += "."
        Sql1 += "t44_shukohd"
        Sql1 += " WHERE "
        Sql1 += "出庫番号"
        Sql1 += " ILIKE "
        Sql1 += "'"
        Sql1 += DgvHistory.Rows(SelectedRow).Cells("出庫番号").Value
        Sql1 += "'"


        Dim Sql2 As String = ""
        Sql2 += "SELECT "
        Sql2 += "* "
        Sql2 += "FROM "
        Sql2 += "public"
        Sql2 += "."
        Sql2 += "t45_shukodt"
        Sql2 += " WHERE "
        Sql2 += "出庫番号"
        Sql2 += " ILIKE "
        Sql2 += "'"
        Sql2 += DgvHistory.Rows(SelectedRow).Cells("出庫番号").Value
        Sql2 += "'"

        Dim reccnt As Integer = 0
        Dim ds1 As DataSet = _db.selectDB(Sql1, RS, reccnt)
        Dim ds2 As DataSet = _db.selectDB(Sql2, RS, reccnt)

        '定義
        Dim app As Excel.Application = Nothing
        Dim book As Excel.Workbook = Nothing
        Dim sheet As Excel.Worksheet = Nothing



        Try
            '雛形パス
            Dim sHinaPath As String = ""
            sHinaPath = StartUp._iniVal.BaseXlsPath

            '雛形ファイル名
            Dim sHinaFileDeliv As String = ""
            sHinaFileDeliv = sHinaPath & "\" & "DeliveryNote.xlsx"

            Dim sHinaFileReceipt As String = ""
            sHinaFileReceipt = sHinaPath & "\" & "DeliveryNote.xlsx"

            '出力先パス
            Dim sOutPath As String = ""
            sOutPath = StartUp._iniVal.OutXlsPath

            '出力ファイル名
            Dim sOutFile As String = ""
            sOutFile = sOutPath & "\DeliveryNote_" & ds2.Tables(RS).Rows(0)("出庫番号") & ".xlsx"



            app = New Excel.Application()
            book = app.Workbooks.Add(sHinaFileDeliv)  'テンプレート
            sheet = CType(book.Worksheets(1), Excel.Worksheet)

            sheet.Range("E8").Value = ds1.Tables(RS).Rows(0)("得意先名")
            sheet.Range("E9").Value = ds1.Tables(RS).Rows(0)("得意先郵便番号") & " " & ds1.Tables(RS).Rows(0)("得意先住所")
            sheet.Range("E11").Value = ds1.Tables(RS).Rows(0)("得意先電話番号")

            sheet.Range("U8").Value = ds1.Tables(RS).Rows(0)("出庫番号")
            sheet.Range("U9").Value = ds1.Tables(RS).Rows(0)("出庫日")



            Dim rowCnt As Integer = 0
            Dim lstRow As Integer = 14
            'Dim addRowCnt As Integer = 0
            'Dim currentCnt As Integer = 20
            Dim num As Integer = 1


            For j As Integer = 0 To ds2.Tables(RS).Rows.Count - 1
                If rowCnt = 0 Then
                    sheet.Range("A14").Value = num
                    sheet.Range("C14").Value = ds2.Tables(RS).Rows(j)("メーカー") & " / " & ds2.Tables(RS).Rows(j)("品名") & " / " & ds2.Tables(RS).Rows(j)("型式")
                    sheet.Range("N14").Value = ds2.Tables(RS).Rows(j)("出庫数量")
                    sheet.Range("Q14").Value = ds2.Tables(RS).Rows(j)("単位")
                    sheet.Range("T14").Value = ds2.Tables(RS).Rows(j)("備考")
                    'sheet.Rows(lstRow & ":" & lstRow).AutoFit
                Else
                    Dim cellPos As String = lstRow & ":" & lstRow
                    Dim R As Object
                    cellPos = lstRow & ":" & lstRow
                    R = sheet.Range(cellPos)
                    R.Copy()
                    R.Insert()
                    If Marshal.IsComObject(R) Then
                        Marshal.ReleaseComObject(R)
                    End If

                    lstRow = lstRow + 1

                    sheet.Range("A" & lstRow).Value = num
                    sheet.Range("C" & lstRow).Value = ds2.Tables(RS).Rows(j)("メーカー") & " / " & ds2.Tables(RS).Rows(j)("品名") & " / " & ds2.Tables(RS).Rows(j)("型式")
                    sheet.Range("N" & lstRow).Value = ds2.Tables(RS).Rows(j)("出庫数量")
                    sheet.Range("Q" & lstRow).Value = ds2.Tables(RS).Rows(j)("単位")
                    sheet.Range("T" & lstRow).Value = ds2.Tables(RS).Rows(j)("備考")
                    'sheet.Rows(lstRow & ":" & lstRow).AutoFit

                End If
            Next

            book.SaveAs(sOutFile)
            app.Visible = True

            '_msgHd.dspMSG("CreateExcel")
            createFlg = True

        Catch ex As Exception
            Throw ex

        Finally
            'app.Quit()
            'Marshal.ReleaseComObject(sheet)
            'Marshal.ReleaseComObject(book)
            'Marshal.ReleaseComObject(app)

        End Try

        Try
            '雛形パス
            Dim sHinaPath As String = ""
            sHinaPath = StartUp._iniVal.BaseXlsPath

            '雛形ファイル名
            Dim sHinaFile As String = ""
            sHinaFile = sHinaPath & "\" & "PackingList.xlsx"



            '出力先パス
            Dim sOutPath As String = ""
            sOutPath = StartUp._iniVal.OutXlsPath

            '出力ファイル名
            Dim sOutFile As String = ""
            sOutFile = sOutPath & "\PackingList_" & ds2.Tables(RS).Rows(0)("出庫番号") & ".xlsx"



            app = New Excel.Application()
            book = app.Workbooks.Add(sHinaFile)  'テンプレート
            sheet = CType(book.Worksheets(1), Excel.Worksheet)

            sheet.Range("B2").Value = ds1.Tables(RS).Rows(0)("得意先名")
            sheet.Range("B9").Value = ds1.Tables(RS).Rows(0)("得意先郵便番号")
            sheet.Range("B10").Value = ds1.Tables(RS).Rows(0)("得意先住所")
            sheet.Range("B11").Value = ds1.Tables(RS).Rows(0)("得意先電話番号")

            sheet.Range("G8").Value = ds1.Tables(RS).Rows(0)("出庫番号")
            sheet.Range("G9").Value = ds1.Tables(RS).Rows(0)("出庫日")



            Dim rowCnt As Integer = 0
            Dim lstRow As Integer = 14
            'Dim addRowCnt As Integer = 0
            'Dim currentCnt As Integer = 20
            Dim num As Integer = 1


            For j As Integer = 0 To ds2.Tables(RS).Rows.Count - 1
                If rowCnt = 0 Then
                    sheet.Range("A" & lstRow).Value = num
                    sheet.Range("B" & lstRow).Value = ds2.Tables(RS).Rows(j)("メーカー") & " / " & ds2.Tables(RS).Rows(j)("品名") & " / " & ds2.Tables(RS).Rows(j)("型式")
                    sheet.Range("F" & lstRow).Value = ds2.Tables(RS).Rows(j)("出庫数量") & " " & ds2.Tables(RS).Rows(j)("単位")
                    'sheet.Rows(lstRow & ":" & lstRow).AutoFit
                Else
                    Dim cellPos As String = lstRow & ":" & lstRow
                    Dim R As Object
                    cellPos = lstRow & ":" & lstRow
                    R = sheet.Range(cellPos)
                    R.Copy()
                    R.Insert()
                    If Marshal.IsComObject(R) Then
                        Marshal.ReleaseComObject(R)
                    End If

                    lstRow = lstRow + 1

                    sheet.Range("A" & lstRow).Value = num
                    sheet.Range("B" & lstRow).Value = ds2.Tables(RS).Rows(j)("メーカー") & " / " & ds2.Tables(RS).Rows(j)("品名") & " / " & ds2.Tables(RS).Rows(j)("型式")
                    sheet.Range("F" & lstRow).Value = ds2.Tables(RS).Rows(j)("出庫数量") & " " & ds2.Tables(RS).Rows(j)("単位")
                    'sheet.Rows(lstRow & ":" & lstRow).AutoFit

                End If
            Next

            book.SaveAs(sOutFile)
            app.Visible = True

            '_msgHd.dspMSG("CreateExcel")
            createFlg = True

        Catch ex As Exception
            Throw ex

        Finally
            'app.Quit()
            'Marshal.ReleaseComObject(sheet)
            'Marshal.ReleaseComObject(book)
            'Marshal.ReleaseComObject(app)

        End Try

        Try
            '雛形パス
            Dim sHinaPath As String = ""
            sHinaPath = StartUp._iniVal.BaseXlsPath

            '雛形ファイル名
            Dim sHinaFileReceipt As String = ""
            sHinaFileReceipt = sHinaPath & "\" & "Receipt.xlsx"

            '出力先パス
            Dim sOutPath As String = ""
            sOutPath = StartUp._iniVal.OutXlsPath

            '出力ファイル名
            Dim sOutFile As String = ""
            sOutFile = sOutPath & "\Receipt_" & ds2.Tables(RS).Rows(0)("出庫番号") & ".xlsx"



            app = New Excel.Application()
            book = app.Workbooks.Add(sHinaFileReceipt)  'テンプレート
            sheet = CType(book.Worksheets(1), Excel.Worksheet)

            sheet.Range("E8").Value = ds1.Tables(RS).Rows(0)("得意先名")
            sheet.Range("E9").Value = ds1.Tables(RS).Rows(0)("得意先郵便番号") & " " & ds1.Tables(RS).Rows(0)("得意先住所")
            sheet.Range("E11").Value = ds1.Tables(RS).Rows(0)("得意先電話番号")

            sheet.Range("U8").Value = ds1.Tables(RS).Rows(0)("出庫番号")
            sheet.Range("U9").Value = ds1.Tables(RS).Rows(0)("出庫日")



            Dim rowCnt As Integer = 0
            Dim lstRow As Integer = 14
            'Dim addRowCnt As Integer = 0
            'Dim currentCnt As Integer = 20
            Dim num As Integer = 1


            For j As Integer = 0 To ds2.Tables(RS).Rows.Count - 1
                If rowCnt = 0 Then
                    sheet.Range("A14").Value = num
                    sheet.Range("C14").Value = ds2.Tables(RS).Rows(j)("メーカー") & " / " & ds2.Tables(RS).Rows(j)("品名") & " / " & ds2.Tables(RS).Rows(j)("型式")
                    sheet.Range("N14").Value = ds2.Tables(RS).Rows(j)("出庫数量")
                    sheet.Range("Q14").Value = ds2.Tables(RS).Rows(j)("単位")
                    sheet.Range("T14").Value = ds2.Tables(RS).Rows(j)("備考")
                    'sheet.Rows(lstRow & ":" & lstRow).AutoFit
                Else
                    Dim cellPos As String = lstRow & ":" & lstRow
                    Dim R As Object
                    cellPos = lstRow & ":" & lstRow
                    R = sheet.Range(cellPos)
                    R.Copy()
                    R.Insert()
                    If Marshal.IsComObject(R) Then
                        Marshal.ReleaseComObject(R)
                    End If

                    lstRow = lstRow + 1

                    sheet.Range("A" & lstRow).Value = num
                    sheet.Range("C" & lstRow).Value = ds2.Tables(RS).Rows(j)("メーカー") & " / " & ds2.Tables(RS).Rows(j)("品名") & " / " & ds2.Tables(RS).Rows(j)("型式")
                    sheet.Range("N" & lstRow).Value = ds2.Tables(RS).Rows(j)("出庫数量")
                    sheet.Range("Q" & lstRow).Value = ds2.Tables(RS).Rows(j)("単位")
                    sheet.Range("T" & lstRow).Value = ds2.Tables(RS).Rows(j)("備考")
                    'sheet.Rows(lstRow & ":" & lstRow).AutoFit

                End If
            Next

            book.SaveAs(sOutFile)
            app.Visible = True

            '_msgHd.dspMSG("CreateExcel")
            createFlg = True

        Catch ex As Exception
            Throw ex

        Finally
            'app.Quit()
            'Marshal.ReleaseComObject(sheet)
            'Marshal.ReleaseComObject(book)
            'Marshal.ReleaseComObject(app)

        End Try
        'Dim test As String = ds1.Tables(RS).Rows(0)("")
        If createFlg = True Then
            _msgHd.dspMSG("CreateExcel", frmC01F10_Login.loginValue.Language)
        End If
    End Sub

    'param1：String 採番キー
    'param2：DateTime 登録日
    'Return: String 伝票番号
    '伝票番号を取得
    Private Function getSaiban(ByVal key As String, ByVal today As DateTime) As String
        Dim Sql As String = ""
        Dim saibanID As String = ""
        Dim reccnt As Integer = 0 'DB用（デフォルト）

        Try
            Sql = "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public.m80_saiban"
            Sql += " WHERE "
            Sql += "会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND "
            Sql += "採番キー = '" & key & "'"

            Dim dsSaiban As DataSet = _db.selectDB(Sql, RS, reccnt)

            saibanID = dsSaiban.Tables(RS).Rows(0)("接頭文字")
            saibanID += today.ToString("MMdd")
            saibanID += dsSaiban.Tables(RS).Rows(0)("最新値").ToString.PadLeft(dsSaiban.Tables(RS).Rows(0)("連番桁数"), "0")

            Dim keyNo As Integer

            If dsSaiban.Tables(RS).Rows(0)("最新値") = dsSaiban.Tables(RS).Rows(0)("最大値") Then
                '最新値が最大と同じ場合、最小値にリセット
                keyNo = dsSaiban.Tables(RS).Rows(0)("最小値")
            Else
                '最新値+1
                keyNo = dsSaiban.Tables(RS).Rows(0)("最新値") + 1
            End If

            Sql = "UPDATE "
            Sql += "Public.m80_saiban "
            Sql += "SET "
            Sql += " 最新値 "
            Sql += " = '"
            Sql += keyNo.ToString
            Sql += "', "
            Sql += "更新者"
            Sql += " = '"
            Sql += frmC01F10_Login.loginValue.TantoNM
            Sql += "', "
            Sql += "更新日"
            Sql += " = '"
            Sql += formatDatetime(today)
            Sql += "' "
            Sql += "WHERE"
            Sql += " 会社コード"
            Sql += "='"
            Sql += frmC01F10_Login.loginValue.BumonCD
            Sql += "'"
            Sql += " AND"
            Sql += " 採番キー = '" & key & "'"
            Console.WriteLine(Sql)
            _db.executeDB(Sql)

            Return saibanID
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Function

    '汎用マスタから固定キー、可変キーに応じた結果を返す
    'param1：String 固定キー
    'param2：String 可変キー
    'Return: DataSet
    Private Function getDsHanyoData(ByVal prmFixed As String, ByVal prmVariable As String) As DataSet
        Dim Sql As String = ""

        Sql = " AND "
        Sql += "固定キー ILIKE '" & prmFixed & "'"
        Sql += " AND "
        Sql += "可変キー ILIKE '" & prmVariable & "'"

        'リードタイムのリストを汎用マスタから取得
        Return getDsData("m90_hanyo", Sql)

    End Function

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

        Console.WriteLine(Sql)
        Return _db.selectDB(Sql, RS, reccnt)
    End Function

    'ユーザーのカルチャーから、日本の形式に変換する
    Private Function strFormatDate(ByVal prmDate As String, Optional ByRef prmFormat As String = "yyyy/MM/dd") As String

        'PCのカルチャーを取得し、それに応じてStringからDatetimeを作成
        Dim ci As New System.Globalization.CultureInfo(CultureInfo.CurrentCulture.Name.ToString)
        Dim dateFormat As DateTime = DateTime.Parse(prmDate, ci, System.Globalization.DateTimeStyles.AssumeLocal)

        '日本の形式に書き換える
        Return dateFormat.ToString(prmFormat)
    End Function

    'ユーザーのカルチャーから、日本の形式に変換する
    Private Function formatDatetime(ByVal prmDatetime As DateTime) As String

        'PCのカルチャーを取得し、それに応じてStringからDatetimeを作成
        Dim ciCurrent As New System.Globalization.CultureInfo(CultureInfo.CurrentCulture.Name.ToString)
        Dim dateFormat As DateTime = DateTime.Parse(prmDatetime.ToString, ciCurrent, System.Globalization.DateTimeStyles.AssumeLocal)

        Dim changeFormat As String = dateFormat.ToString("yyyy/MM/dd HH:mm:ss")

        Dim ciJP As New System.Globalization.CultureInfo(CommonConst.CI_JP)
        Dim rtnDatetime As DateTime = DateTime.Parse(changeFormat, ciJP, System.Globalization.DateTimeStyles.AssumeLocal)


        '日本の形式に書き換える
        Return changeFormat
    End Function

    '金額フォーマット（登録の際の小数点指定子）を日本の形式に合わせる
    '桁区切り記号は外す
    Private Function formatNumber(ByVal prmVal As Decimal) As String

        Dim nfi As NumberFormatInfo = New CultureInfo(CommonConst.CI_JP, False).NumberFormat

        '日本の形式に書き換える
        Return prmVal.ToString("F3", nfi) '売掛残高を増やす
    End Function

End Class