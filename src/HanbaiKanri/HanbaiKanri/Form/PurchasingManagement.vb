Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports System.Globalization
Imports System.Text.RegularExpressions

Public Class PurchasingManagement
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
    Private Status As String = ""
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
        Status = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        DtpPurchaseDate.Value = Date.Now
        DtpPaymentDate.Value = Date.Now
        _init = True

    End Sub

    '見出しのセット
    Private Sub setCulmnHd()

        '
        '発注エリア
        '
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            DgvPurchase.Columns.Add("明細", "LineNo")
            DgvPurchase.Columns.Add("メーカー", "Manufacturer")
            DgvPurchase.Columns.Add("品名", "ItemName")
            DgvPurchase.Columns.Add("型式", "Spec")
            DgvPurchase.Columns.Add("発注数量", "OrderQuantity" & vbCrLf & "a")
            DgvPurchase.Columns.Add("単位", "Unit")
            DgvPurchase.Columns.Add("仕入数量", "PurchasedQuantity" & vbCrLf & "b")
            DgvPurchase.Columns.Add("発注残数", "UnregisteredQuantity" & vbCrLf & "c=a-b")
            DgvPurchase.Columns.Add("仕入単価", "PurchaseUnitPrice")
            'DgvPurchase.Columns.Add("仕入金額", "PurchaseAmount")

            '20200811
            If frmC01F10_Login.loginValue.BumonCD = "ZENBI" Then  'ゼンビさんの場合

                DgvPurchase.Columns.Add("仕入メーカー", "PurchaseManufacturer")
                DgvPurchase.Columns.Add("仕入品名", "PurchaseItemName")
                DgvPurchase.Columns.Add("仕入型式", "PurchaseSpec")
            End If

        Else
            DgvPurchase.Columns.Add("明細", "行No")
            DgvPurchase.Columns.Add("メーカー", "メーカー")
            DgvPurchase.Columns.Add("品名", "品名")
            DgvPurchase.Columns.Add("型式", "型式")
            DgvPurchase.Columns.Add("発注数量", "発注数量" & vbCrLf & "a")
            DgvPurchase.Columns.Add("単位", "単位")
            DgvPurchase.Columns.Add("仕入数量", "仕入(検収)登録済み数量" & vbCrLf & "b")
            DgvPurchase.Columns.Add("発注残数", "未登録数量" & vbCrLf & "c=a-b")
            DgvPurchase.Columns.Add("仕入単価", "仕入単価")
            'DgvPurchase.Columns.Add("仕入金額", "仕入金額")

            '20200809
            If frmC01F10_Login.loginValue.BumonCD = "ZENBI" Then  'ゼンビさんの場合

                DgvPurchase.Columns.Add("仕入メーカー", "仕入メーカー")
                DgvPurchase.Columns.Add("仕入品名", "仕入品名")
                DgvPurchase.Columns.Add("仕入型式", "仕入型式")
            End If

        End If

        '中央寄せ
        DgvPurchase.Columns("明細").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvPurchase.Columns("メーカー").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvPurchase.Columns("品名").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvPurchase.Columns("型式").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvPurchase.Columns("発注数量").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvPurchase.Columns("単位").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvPurchase.Columns("仕入数量").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvPurchase.Columns("発注残数").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvPurchase.Columns("仕入単価").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        '右寄せ
        DgvPurchase.Columns("発注数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvPurchase.Columns("仕入数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvPurchase.Columns("仕入単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvPurchase.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvPurchase.Columns("発注残数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        '数字形式
        DgvPurchase.Columns("発注数量").DefaultCellStyle.Format = "N2"
        DgvPurchase.Columns("仕入数量").DefaultCellStyle.Format = "N2"
        DgvPurchase.Columns("仕入単価").DefaultCellStyle.Format = "N2"
        'DgvPurchase.Columns("仕入金額").DefaultCellStyle.Format = "N2"
        DgvPurchase.Columns("発注残数").DefaultCellStyle.Format = "N2"

        '
        '仕入済みエリア
        '
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            DgvHistory.Columns.Add("No", "No")
            DgvHistory.Columns.Add("仕入番号", "PurchaseNumber")
            DgvHistory.Columns.Add("行番号", "LineNo")
            DgvHistory.Columns.Add("仕入区分", "PurchasingClassification")
            DgvHistory.Columns.Add("メーカー", "Manufacturer")
            DgvHistory.Columns.Add("品名", "ItemName")
            DgvHistory.Columns.Add("型式", "Spec")
            DgvHistory.Columns.Add("単位", "Unit")
            DgvHistory.Columns.Add("仕入先", "SupplierName")
            DgvHistory.Columns.Add("仕入値", "PurchaseUnitPrice")
            DgvHistory.Columns.Add("仕入数量", "PurchasedQuantity")
            DgvHistory.Columns.Add("仕入日", "PurchaseDate")
            DgvHistory.Columns.Add("備考", "Remarks")
            DgvHistory.Columns.Add("発注行番号", "POLINENO")
            DgvHistory.Columns.Add("取消", "DEL")

            '20200811
            If frmC01F10_Login.loginValue.BumonCD = "ZENBI" Then  'ゼンビさんの場合

                DgvHistory.Columns.Add("仕入メーカー", "PurchaseManufacturer")
                DgvHistory.Columns.Add("仕入品名", "PurchaseItemName")
                DgvHistory.Columns.Add("仕入型式", "PurchaseSpec")
            End If

        Else
            DgvHistory.Columns.Add("No", "No")
            DgvHistory.Columns.Add("仕入番号", "仕入番号")
            DgvHistory.Columns.Add("行番号", "行No")
            DgvHistory.Columns.Add("仕入区分", "仕入区分")
            DgvHistory.Columns.Add("メーカー", "メーカー")
            DgvHistory.Columns.Add("品名", "品名")
            DgvHistory.Columns.Add("型式", "型式")
            DgvHistory.Columns.Add("単位", "単位")
            DgvHistory.Columns.Add("仕入先", "仕入先")
            DgvHistory.Columns.Add("仕入値", "仕入値")
            DgvHistory.Columns.Add("仕入数量", "仕入(検収)登録済み数量")
            DgvHistory.Columns.Add("仕入日", "仕入(検収)登録日")
            DgvHistory.Columns.Add("備考", "備考")
            DgvHistory.Columns.Add("発注行番号", "POLINENO")
            DgvHistory.Columns.Add("取消", "取消")

            '20200809
            If frmC01F10_Login.loginValue.BumonCD = "ZENBI" Then  'ゼンビさんの場合

                DgvHistory.Columns.Add("仕入メーカー", "仕入メーカー")
                DgvHistory.Columns.Add("仕入品名", "仕入品名")
                DgvHistory.Columns.Add("仕入型式", "仕入型式")
            End If

        End If

        'DgvHistory.Columns("仕入値").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvHistory.Columns("仕入数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


        '数字形式
        'DgvHistory.Columns("仕入値").DefaultCellStyle.Format = "N2"
        DgvHistory.Columns("仕入数量").DefaultCellStyle.Format = "N2"

        DgvHistory.Columns("仕入先").Visible = False
        DgvHistory.Columns("仕入値").Visible = False


        '
        '今回仕入エリア
        '
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            DgvAdd.Columns.Add("No", "No")
            DgvAdd.Columns.Add("行番号", "LineNo")
            DgvAdd.Columns.Add("仕入区分値", "仕入区分値")
            DgvAdd.Columns.Add("仕入区分", "PurchasingClassification")
            DgvAdd.Columns.Add("メーカー", "Manufacturer")
            DgvAdd.Columns.Add("品名", "ItemName")
            DgvAdd.Columns.Add("型式", "Spec")

            DgvAdd.Columns.Add("発注数量", "QuantityOrdered")
            DgvAdd.Columns.Add("単位", "Unit")
            DgvAdd.Columns.Add("仕入先", "SupplierName")
            DgvAdd.Columns.Add("仕入値", "PurchaseUnitPrice")

            DgvAdd.Columns.Add("登録済数量", "PurchasedQuantity")
            DgvAdd.Columns.Add("未登録数量", "UnregisteredQuantity")
            DgvAdd.Columns.Add("仕入数量", "PurchaseQuantityThisTime")
            DgvAdd.Columns.Add("備考", "Remarks")

        Else
            DgvAdd.Columns.Add("No", "No")
            DgvAdd.Columns.Add("行番号", "行No")
            DgvAdd.Columns.Add("仕入区分値", "仕入区分値")
            DgvAdd.Columns.Add("仕入区分", "仕入区分")
            DgvAdd.Columns.Add("メーカー", "メーカー")
            DgvAdd.Columns.Add("品名", "品名")
            DgvAdd.Columns.Add("型式", "型式")

            DgvAdd.Columns.Add("発注数量", "発注数量")
            DgvAdd.Columns.Add("単位", "単位")
            DgvAdd.Columns.Add("仕入先", "仕入先")
            DgvAdd.Columns.Add("仕入値", "仕入値")

            DgvAdd.Columns.Add("登録済数量", "仕入(検収)登録済み数量")
            DgvAdd.Columns.Add("未登録数量", "未登録数量")
            DgvAdd.Columns.Add("仕入数量", "今回仕入数量")

            DgvAdd.Columns.Add("備考", "備考")
        End If

        'DgvAdd.Columns("仕入値").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvAdd.Columns("発注数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvAdd.Columns("登録済数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvAdd.Columns("未登録数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvAdd.Columns("仕入数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        '数字形式
        'DgvAdd.Columns("仕入値").DefaultCellStyle.Format = "N2"

        DgvAdd.Columns("発注数量").DefaultCellStyle.Format = "N2"
        DgvAdd.Columns("登録済数量").DefaultCellStyle.Format = "N2"
        DgvAdd.Columns("未登録数量").DefaultCellStyle.Format = "N2"
        DgvAdd.Columns("仕入数量").DefaultCellStyle.Format = "N2"


        DgvAdd.Columns("No").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
        DgvAdd.Columns("行番号").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
        DgvAdd.Columns("仕入区分").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
        DgvAdd.Columns("メーカー").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
        DgvAdd.Columns("品名").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
        DgvAdd.Columns("型式").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)

        DgvAdd.Columns("発注数量").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
        DgvAdd.Columns("単位").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
        DgvAdd.Columns("登録済数量").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
        DgvAdd.Columns("未登録数量").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)

        'DgvAdd.Columns("仕入先").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
        'DgvAdd.Columns("仕入値").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)

        DgvAdd.Columns("No").ReadOnly = True
        DgvAdd.Columns("行番号").ReadOnly = True
        DgvAdd.Columns("仕入区分").ReadOnly = True
        DgvAdd.Columns("メーカー").ReadOnly = True
        DgvAdd.Columns("品名").ReadOnly = True
        DgvAdd.Columns("型式").ReadOnly = True

        DgvAdd.Columns("発注数量").ReadOnly = True
        DgvAdd.Columns("単位").ReadOnly = True
        DgvAdd.Columns("登録済数量").ReadOnly = True
        DgvAdd.Columns("未登録数量").ReadOnly = True
        'DgvAdd.Columns("仕入先").ReadOnly = True
        'DgvAdd.Columns("仕入値").ReadOnly = True

        DgvAdd.Columns("仕入区分値").Visible = False

        DgvAdd.Columns("仕入先").Visible = False
        DgvAdd.Columns("仕入値").Visible = False

    End Sub

    '画面表示時
    Private Sub PurchaseManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim curds As DataSet  'm25_currency
        Dim cur As String

        If Status = CommonConst.STATUS_VIEW Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "ViewMode"
            Else
                LblMode.Text = "参照モード"
            End If

            LblNo1.Visible = False
            LblNo2.Visible = False
            LblNo3.Visible = False
            LblAdd.Visible = False
            LblPurchasedDate.Visible = False
            DtpPurchaseDate.Visible = False
            LblPaymentDate.Visible = False
            DtpPaymentDate.Visible = False
            LblRemarks.Visible = False
            TxtRemarks.Visible = False

            'LblPurchasedDate.Location = New Point(172, 80)
            'DtpPurchaseDate.Enabled = False
            'DtpPurchaseDate.Location = New Point(292, 80)
            'LblPaymentDate.Location = New Point(447, 80)
            'DtpPaymentDate.Enabled = False
            'DtpPaymentDate.Location = New Point(567, 80)
            'LblRemarks.Location = New Point(724, 80)
            'TxtRemarks.Enabled = False
            'TxtRemarks.Location = New Point(844, 80)
            TxtCount1.Visible = False
            TxtCount2.Visible = False
            TxtCount3.Visible = False
            DgvPurchase.Visible = False
            DgvAdd.Visible = False
            DgvHistory.ReadOnly = False

            LblHistory.Location = New Point(12, 82)
            DgvHistory.Location = New Point(12, 106)
            DgvHistory.Size = New Point(1326, 400)

            BtnRegist.Visible = False
        Else
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "PurchasingInputMode"
            Else
                LblMode.Text = "仕入入力モード"
            End If
        End If

        setCulmnHd() '見出し行のセット

        Dim reccnt As Integer = 0
        Dim Sql As String = ""

        Try

            'ヘッダエリア
            Sql = " AND 発注番号 = '" & No & "'"
            Sql += " AND 発注番号枝番 = '" & Suffix & "'"

            '取消のデータを参照できるようにする
            '仕入登録と仕入取消の処理はボタンを押下した時に弾く
            'Sql += " AND "
            'Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED '取消区分=0  

            Dim dsHattyuHd As DataSet = getDsData("t20_hattyu", Sql)

            '仕入済みエリア
            Sql = "SELECT"
            Sql += " t41.*, t40.取消区分"
            Sql += ",t21.仕入値_外貨,t21.仕入金額_外貨"

            Sql += " FROM "
            'Sql += " public.t41_siredt t41 "

            'Sql += " INNER JOIN "
            'Sql += " t40_sirehd t40"
            'Sql += " ON "

            'Sql += " t41.会社コード = t40.会社コード"
            'Sql += " AND "
            'Sql += " t41.仕入番号 = t40.仕入番号"
            'Sql += " AND "
            'Sql += " t41.発注番号枝番 = t40.発注番号枝番"

            ' fuke
            'Sql += " left join t21_hattyu t21"
            'Sql += " on t41.発注番号 = t21.発注番号 and t41.発注番号枝番 = t21.発注番号枝番"
            'Sql += " and t41.メーカー = t21.メーカー and t41.品名 = t21.品名 and t41.型式 = t21.型式 and t41.発注数量 = t21.発注数量"

            Sql += "public.t41_siredt t41, public.t40_sirehd t40, public.t21_hattyu t21 "
            Sql += "where t41.会社コード = t40.会社コード And t41.仕入番号 = t40.仕入番号 and "
            Sql += "t41.発注番号 = t21.発注番号 And t41.発注番号枝番 = t21.発注番号枝番 and t41.発注行番号 = t21.行番号 and "

            'Sql += " WHERE "
            Sql += " t41.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND t41.発注番号 = '" & No & "'"
            Sql += " AND t41.発注番号枝番 = '" & Suffix & "'"
            'Sql += " AND t40.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED '取消区分=0

            Dim dsSireDt As DataSet = _db.selectDB(Sql, RS, reccnt)

            '発注済みエリア
            Sql = "SELECT"
            Sql += " t21.*, t20.取消区分"
            Sql += " FROM "
            Sql += " public.t21_hattyu t21 "

            Sql += " INNER JOIN "
            Sql += " t20_hattyu t20"
            Sql += " ON "

            Sql += " t21.会社コード = t20.会社コード"
            Sql += " AND "
            Sql += " t21.発注番号 = t20.発注番号"
            Sql += " AND "
            Sql += " t21.発注番号枝番 = t20.発注番号枝番"

            Sql += " WHERE "
            Sql += " t21.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND t21.発注番号 = '" & No & "'"
            Sql += " AND t21.発注番号枝番 = '" & Suffix & "'"
            'Sql += " AND t20.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED '取消区分=0

            Dim dsHattyuDt As DataSet = _db.selectDB(Sql, RS, reccnt)

            '通貨の表示
            If IsDBNull(dsHattyuDt.Tables(RS).Rows(0)("仕入通貨")) Then
                cur = vbNullString
            Else
                Sql = " and 採番キー = " & dsHattyuDt.Tables(RS).Rows(0)("仕入通貨")
                curds = getDsData("m25_currency", Sql)

                cur = curds.Tables(RS).Rows(0)("通貨コード")
            End If
            TxtIDRCurrency.Text = cur

            '発注エリアに明細を表示
            For i As Integer = 0 To dsHattyuDt.Tables(RS).Rows.Count - 1
                DgvPurchase.Rows.Add()
                DgvPurchase.Rows(i).Cells("メーカー").Value = dsHattyuDt.Tables(RS).Rows(i)("メーカー")
                DgvPurchase.Rows(i).Cells("品名").Value = dsHattyuDt.Tables(RS).Rows(i)("品名")
                DgvPurchase.Rows(i).Cells("型式").Value = dsHattyuDt.Tables(RS).Rows(i)("型式")
                DgvPurchase.Rows(i).Cells("発注数量").Value = dsHattyuDt.Tables(RS).Rows(i)("発注数量")
                DgvPurchase.Rows(i).Cells("単位").Value = dsHattyuDt.Tables(RS).Rows(i)("単位")
                DgvPurchase.Rows(i).Cells("仕入数量").Value = dsHattyuDt.Tables(RS).Rows(i)("仕入数量")
                DgvPurchase.Rows(i).Cells("仕入単価").Value = dsHattyuDt.Tables(RS).Rows(i)("仕入値_外貨")
                'DgvPurchase.Rows(i).Cells("仕入金額").Value = dsHattyuDt.Tables(RS).Rows(i)("仕入金額_外貨")
                DgvPurchase.Rows(i).Cells("発注残数").Value = dsHattyuDt.Tables(RS).Rows(i)("発注残数")
                DgvPurchase.Rows(i).Cells("明細").Value = dsHattyuDt.Tables(RS).Rows(i)("行番号")


                '20200809
                Dim dsItem As DataTable = mSetHatyuItem(No, Suffix, dsHattyuDt.Tables(RS).Rows(i)("行番号"))

                If frmC01F10_Login.loginValue.BumonCD = "ZENBI" Then  'ゼンビさんの場合

                    If dsItem.Rows.Count > 0 Then
                        'データが存在した場合は品名をセット
                        DgvPurchase.Rows(i).Cells("仕入メーカー").Value = dsItem.Rows(0)("メーカー")
                        DgvPurchase.Rows(i).Cells("仕入品名").Value = dsItem.Rows(0)("品名")
                        DgvPurchase.Rows(i).Cells("仕入型式").Value = dsItem.Rows(0)("型式")
                    End If
                End If

            Next

            '仕入済みエリアに明細を表示
            For i As Integer = 0 To dsSireDt.Tables(RS).Rows.Count - 1
                DgvHistory.Rows.Add()
                DgvHistory.Rows(i).Cells("仕入番号").Value = dsSireDt.Tables(RS).Rows(i)("仕入番号")
                DgvHistory.Rows(i).Cells("行番号").Value = dsSireDt.Tables(RS).Rows(i)("行番号")

                '仕入区分のリストを汎用マスタから取得
                Dim dsHanyou As DataSet = getDsHanyoData(CommonConst.FIXED_KEY_PURCHASING_CLASS, dsSireDt.Tables(RS).Rows(i)("仕入区分"))
                If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                    DgvHistory.Rows(i).Cells("仕入区分").Value = dsHanyou.Tables(RS).Rows(0)("文字２")
                Else
                    DgvHistory.Rows(i).Cells("仕入区分").Value = dsHanyou.Tables(RS).Rows(0)("文字１")
                End If

                DgvHistory.Rows(i).Cells("メーカー").Value = dsSireDt.Tables(RS).Rows(i)("メーカー")
                DgvHistory.Rows(i).Cells("品名").Value = dsSireDt.Tables(RS).Rows(i)("品名")
                DgvHistory.Rows(i).Cells("型式").Value = dsSireDt.Tables(RS).Rows(i)("型式")
                DgvHistory.Rows(i).Cells("単位").Value = dsSireDt.Tables(RS).Rows(i)("単位")
                DgvHistory.Rows(i).Cells("仕入先").Value = dsSireDt.Tables(RS).Rows(i)("仕入先名")
                DgvHistory.Rows(i).Cells("仕入値").Value = dsSireDt.Tables(RS).Rows(i)("仕入値_外貨")
                DgvHistory.Rows(i).Cells("仕入数量").Value = dsSireDt.Tables(RS).Rows(i)("仕入数量")
                DgvHistory.Rows(i).Cells("仕入日").Value = dsSireDt.Tables(RS).Rows(i)("仕入日").ToShortDateString()
                DgvHistory.Rows(i).Cells("備考").Value = dsSireDt.Tables(RS).Rows(i)("備考")
                DgvHistory.Rows(i).Cells("発注行番号").Value = dsSireDt.Tables(RS).Rows(i)("発注行番号")
                DgvHistory.Rows(i).Cells("取消").Value = dsSireDt.Tables(RS).Rows(i)("取消区分")

                '20200809
                Dim dsItem As DataTable = mSetHatyuItem(No, Suffix, dsSireDt.Tables(RS).Rows(i)("発注行番号"))

                If frmC01F10_Login.loginValue.BumonCD = "ZENBI" Then  'ゼンビさんの場合

                    If dsItem.Rows.Count > 0 Then
                        'データが存在した場合は品名をセット
                        DgvHistory.Rows(i).Cells("仕入メーカー").Value = dsItem.Rows(0)("メーカー")
                        DgvHistory.Rows(i).Cells("仕入品名").Value = dsItem.Rows(0)("品名")
                        DgvHistory.Rows(i).Cells("仕入型式").Value = dsItem.Rows(0)("型式")
                    End If
                End If

            Next

            '今回仕入エリアに入力エリアを作成
            For i As Integer = 0 To dsHattyuDt.Tables(RS).Rows.Count - 1
                If dsHattyuDt.Tables(RS).Rows(i)("発注残数") <> 0 Then
                    DgvAdd.Rows.Add()
                    DgvAdd.Rows(DgvAdd.Rows.Count - 1).Cells("行番号").Value = dsHattyuDt.Tables(RS).Rows(i)("行番号")
                    DgvAdd.Rows(DgvAdd.Rows.Count - 1).Cells("仕入区分値").Value = dsHattyuDt.Tables(RS).Rows(i)("仕入区分")

                    '汎用マスタから仕入区分を取得
                    Dim dsSireKbn As DataSet = getDsHanyoData(CommonConst.FIXED_KEY_PURCHASING_CLASS, dsHattyuDt.Tables(RS).Rows(i)("仕入区分").ToString)
                    DgvAdd.Rows(DgvAdd.Rows.Count - 1).Cells("仕入区分").Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                                                                dsSireKbn.Tables(RS).Rows(0)("文字２"),
                                                                dsSireKbn.Tables(RS).Rows(0)("文字１"))

                    DgvAdd.Rows(DgvAdd.Rows.Count - 1).Cells("メーカー").Value = dsHattyuDt.Tables(RS).Rows(i)("メーカー")
                    DgvAdd.Rows(DgvAdd.Rows.Count - 1).Cells("品名").Value = dsHattyuDt.Tables(RS).Rows(i)("品名")
                    DgvAdd.Rows(DgvAdd.Rows.Count - 1).Cells("型式").Value = dsHattyuDt.Tables(RS).Rows(i)("型式")
                    DgvAdd.Rows(DgvAdd.Rows.Count - 1).Cells("仕入先").Value = dsHattyuDt.Tables(RS).Rows(i)("仕入先名")

                    DgvAdd.Rows(DgvAdd.Rows.Count - 1).Cells("発注数量").Value = dsHattyuDt.Tables(RS).Rows(i)("発注数量")
                    DgvAdd.Rows(DgvAdd.Rows.Count - 1).Cells("単位").Value = dsHattyuDt.Tables(RS).Rows(i)("単位")
                    DgvAdd.Rows(DgvAdd.Rows.Count - 1).Cells("仕入値").Value = dsHattyuDt.Tables(RS).Rows(i)("仕入値_外貨")

                    DgvAdd.Rows(DgvAdd.Rows.Count - 1).Cells("登録済数量").Value = dsHattyuDt.Tables(RS).Rows(i)("仕入数量")
                    DgvAdd.Rows(DgvAdd.Rows.Count - 1).Cells("未登録数量").Value = dsHattyuDt.Tables(RS).Rows(i)("発注残数")

                    '自動で発注残数をセットする
                    DgvAdd.Rows(DgvAdd.Rows.Count - 1).Cells("仕入数量").Value = dsHattyuDt.Tables(RS).Rows(i)("発注残数")
                    'DgvAdd.Rows(DgvAdd.Rows.Count - 1).Cells("備考").Value = dsHattyuDt.Tables(RS).Rows(i)("備考")
                End If
            Next

            '行番号の振り直し
            Dim i1 As Integer = DgvPurchase.Rows.Count()
            Dim No1 As Integer = 1
            For c As Integer = 0 To i1 - 1
                'DgvPurchase.Rows(c).Cells(0).Value = No1
                No1 += 1
            Next c
            TxtCount1.Text = DgvPurchase.Rows.Count()

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

            TxtPurchaseNo.Text = dsHattyuHd.Tables(RS).Rows(0)("発注番号")
            TxtSuffixNo.Text = dsHattyuHd.Tables(RS).Rows(0)("発注番号枝番")
            TxtOrdingDate.Text = dsHattyuHd.Tables(RS).Rows(0)("発注日")
            TxtSupplierCode.Text = dsHattyuHd.Tables(RS).Rows(0)("仕入先コード")
            TxtSupplierName.Text = dsHattyuHd.Tables(RS).Rows(0)("仕入先名")
            TxtCustomerPO.Text = dsHattyuHd.Tables(RS).Rows(0)("客先番号").ToString

            '#633 のためコメントアウト
            ''売上日、入金予定日のMinDateを受注日に設定
            'DtpPurchaseDate.MinDate = dsHattyuHd.Tables(RS).Rows(0)("発注日").ToShortDateString()
            'DtpPaymentDate.MinDate = dsHattyuHd.Tables(RS).Rows(0)("発注日").ToShortDateString()

            '今回仕入の初期カーソル位置
            'If DgvAdd.Rows.Count > 0 Then
            '    DgvAdd.CurrentCell = DgvAdd(11, 0)
            'End If

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

        '翻訳
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblPurchaseNo.Text = "PurchaseNumber"
            LblCustomerNo.Text = "CustomerNumber"
            LblPurchaseDate.Text = "PurchaseDate"
            LblSupplier.Text = "SupplierName"
            LblPurchase.Text = "Purchase"
            LblHistory.Text = "PurchaseHistory"
            LblAdd.Text = "PurchaseThisTime"
            LblPurchasedDate.Text = "PurchaseDate"
            LblPaymentDate.Text = "PaymentDate"
            LblIDRCurrency.Text = "Currency"
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
            TxtRemarks.Size = New Size(600, 22)

            BtnRegist.Text = "Registration"
            BtnBack.Text = "Back"
        End If

        If DgvAdd.Rows.Count > 0 Then
            DgvAdd.Rows(0).Cells("仕入数量").Selected = True
        End If

    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        'Dim openForm As Form = Nothing
        'openForm = New frmC01F30_Menu(_msgHd, _langHd, _db)
        'openForm.Show()
        Me.Close()
    End Sub

    '登録ボタン押下時
    Private Sub BtnRegist_Click(sender As Object, e As EventArgs) Handles BtnRegist.Click

        Dim dtToday As DateTime = DateTime.Now
        Dim strToday As String = UtilClass.formatDatetime(dtToday)
        Dim errFlg As Boolean = True

        Dim Sql As String = ""
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

        Dim reccnt As Integer = 0

        '仕入残数がなかったらアラートで返す
        If DgvAdd.Rows.Count() = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Return

        End If


#Region "SQL"

        Sql = " AND 発注番号 = '" & No & "'"
        Sql += " AND 発注番号枝番 = '" & Suffix & "'"
        Sql += " AND "
        Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED '取消区分=0

        Dim ds1 As DataSet = getDsData("t20_hattyu", Sql)

        Sql2 += "SELECT * FROM public.t21_hattyu"
        Sql2 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql2 += " AND 発注番号 = '" & No & "'"
        Sql2 += " AND 発注番号枝番 = '" & Suffix & "'"

        'Dim ds2a As DataSet = _db.selectDB(Sql2, RS, reccnt)

#End Region


#Region "Check"

        Dim chkCount As Integer = 0 '仕入データがあるか合算する用
        '最初に今回仕入に入力がなかったらエラーで返す
        For i As Integer = 0 To DgvAdd.RowCount - 1
            chkCount += DgvAdd.Rows(i).Cells("仕入数量").Value
        Next

        If chkCount <= 0 Then
            '対象データがないメッセージを表示
            _msgHd.dspMSG("chkPurchaseAddError", frmC01F10_Login.loginValue.Language)

            Return
        End If

        For i As Integer = 0 To DgvAdd.Rows.Count() - 1

            '仕入数が発注数を超えたら
            'If DgvPurchase.Rows(i).Cells("発注数量").Value < DgvPurchase.Rows(i).Cells("仕入数量").Value + DgvAdd.Rows(i).Cells("仕入数量").Value Then
            If DgvAdd.Rows(i).Cells("仕入数量").Value > 0 Then
                If Not chkPurchasedQuantity(DgvAdd.Rows(i).Cells("仕入数量").Value, No, Suffix, DgvAdd.Rows(i).Cells("行番号").Value) Then

                    '操作できないアラートを出す
                    _msgHd.dspMSG("chkAPBalanceError", frmC01F10_Login.loginValue.Language)

                    Return
                End If
            End If

        Next

#End Region


        Try
            Dim PC As String = getSaiban("50", dtToday)
            Dim WH As String = getSaiban("60", dtToday)

            Dim PurchaseAmount As Decimal = 0
            For i As Integer = 0 To DgvAdd.Rows.Count - 1 'ds2.Tables(RS).Rows.Count - 1
                If DgvAdd.Rows(i).Cells("仕入数量").Value > 0 Then
                    PurchaseAmount += DgvAdd.Rows(i).Cells("仕入数量").Value * DgvAdd.Rows(i).Cells("仕入値").Value
                End If
            Next

#Region "t40_sirehd"

            Sql3 = ""
            Sql3 += "INSERT INTO "
            Sql3 += "Public."
            Sql3 += "t40_sirehd("
            Sql3 += "会社コード, 仕入番号, 発注番号, 発注番号枝番, 客先番号, 仕入先コード, 仕入先名, 仕入先郵便番号, 仕入先住所, 仕入先電話番号"
            Sql3 += ", 仕入先ＦＡＸ, 仕入先担当者役職, 仕入先担当者名, 支払条件, 仕入金額, 粗利額, 営業担当者, 入力担当者, 備考, 取消日, 取消区分"
            Sql3 += ", ＶＡＴ, ＰＰＨ, 仕入日, 登録日, 更新日, 更新者, 支払予定日, 営業担当者コード, 入力担当者コード)"
            Sql3 += " VALUES('"
            Sql3 += ds1.Tables(RS).Rows(0)("会社コード").ToString '会社コード
            Sql3 += "', '"
            Sql3 += PC '仕入番号
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("発注番号").ToString '発注番号
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("発注番号枝番").ToString '発注番号枝番
            Sql3 += "', '"
            Sql3 += UtilClass.escapeSql(ds1.Tables(RS).Rows(0)("客先番号").ToString) '客先番号
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("仕入先コード").ToString '仕入先コード
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("仕入先名").ToString '仕入先名
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("仕入先郵便番号").ToString '仕入先郵便番号
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("仕入先住所").ToString '仕入先住所
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("仕入先電話番号").ToString '仕入先電話番号
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("仕入先ＦＡＸ").ToString '仕入先ＦＡＸ
            Sql3 += "', '"
            Sql3 += UtilClass.escapeSql(ds1.Tables(RS).Rows(0)("仕入先担当者役職").ToString) '仕入先担当者役職
            Sql3 += "', '"
            Sql3 += UtilClass.escapeSql(ds1.Tables(RS).Rows(0)("仕入先担当者名").ToString) '仕入先担当者名
            Sql3 += "', '"
            Sql3 += UtilClass.escapeSql(ds1.Tables(RS).Rows(0)("支払条件").ToString) '支払条件
            Sql3 += "', '"
            Sql3 += UtilClass.formatNumber(PurchaseAmount) '仕入金額
            Sql3 += "', '"
            Sql3 += UtilClass.formatNumber(Decimal.Parse(ds1.Tables(RS).Rows(0)("粗利額"))) '粗利額
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("営業担当者").ToString '営業担当者
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("入力担当者").ToString '入力担当者
            Sql3 += "', '"
            Sql3 += UtilClass.escapeSql(ds1.Tables(RS).Rows(0)("備考").ToString) '備考
            Sql3 += "', "
            Sql3 += "null" '取消日
            Sql3 += ", "
            Sql3 += CommonConst.CANCEL_KBN_ENABLED.ToString() '取消区分 = 0
            Sql3 += ", '"
            Sql3 += UtilClass.formatNumber(Decimal.Parse(ds1.Tables(RS).Rows(0)("ＶＡＴ").ToString)) 'ＶＡＴ
            Sql3 += "', '"
            If ds1.Tables(RS).Rows(0)("ＰＰＨ") Is DBNull.Value Then
                Sql3 += "0" 'ＰＰＨ
            Else
                Sql3 += UtilClass.formatNumber(Decimal.Parse(ds1.Tables(RS).Rows(0)("ＰＰＨ").ToString)) 'ＰＰＨ
            End If
            Sql3 += "', '"
            Sql3 += UtilClass.strFormatDate(DtpPurchaseDate.Text) '仕入日
            Sql3 += "', '"
            Sql3 += strToday '登録日
            Sql3 += "', '"
            Sql3 += strToday '更新日
            Sql3 += "', '"
            Sql3 += Input '更新者
            'Sql3 += "', '"
            'Sql3 += UtilClass.strFormatDate(DtpPaymentDate.Text) '支払予定日
            'Sql3 += "', '"
            Sql3 += "', Null"       '支払予定日
            Sql3 += ", '"
            Sql3 += ds1.Tables(RS).Rows(0)("営業担当者コード").ToString '営業担当者コード
            Sql3 += "', '"
            Sql3 += frmC01F10_Login.loginValue.TantoCD '入力担当者コード
            Sql3 += "')"

            _db.executeDB(Sql3)

#End Region


#Region "t41_siredt"

            For index As Integer = 0 To DgvAdd.Rows.Count() - 1
                Sql4 = ""
                Sql4 += "INSERT INTO "
                Sql4 += "Public."
                Sql4 += "t41_siredt("
                Sql4 += "会社コード, 仕入番号, 発注番号, 発注番号枝番, 行番号, 仕入区分, メーカー, 品名, 型式, 仕入先名, 仕入値, 発注数量"
                Sql4 += ", 仕入数量, 発注残数, 単位, 仕入金額, 間接費, リードタイム, 備考, 仕入日, 更新者, 更新日, 発注行番号)"
                Sql4 += " VALUES('"
                Sql4 += ds1.Tables(RS).Rows(0)("会社コード").ToString '会社コード
                Sql4 += "', '"
                Sql4 += PC '仕入番号
                Sql4 += "', '"
                Sql4 += ds1.Tables(RS).Rows(0)("発注番号").ToString '発注番号
                Sql4 += "', '"
                Sql4 += ds1.Tables(RS).Rows(0)("発注番号枝番").ToString '発注番号枝番
                Sql4 += "', '"
                Sql4 += DgvAdd.Rows(index).Cells("No").Value.ToString '行番号
                Sql4 += "', '"
                Sql4 += DgvAdd.Rows(index).Cells("仕入区分値").Value.ToString '仕入区分
                Sql4 += "', '"
                Sql4 += DgvAdd.Rows(index).Cells("メーカー").Value.ToString 'メーカー
                Sql4 += "', '"
                Sql4 += DgvAdd.Rows(index).Cells("品名").Value.ToString '品名
                Sql4 += "', '"
                Sql4 += DgvAdd.Rows(index).Cells("型式").Value.ToString '型式
                Sql4 += "', '"
                'Sql4 += DgvAdd.Rows(index).Cells("仕入先").Value.ToString '仕入先名
                Sql4 += TxtSupplierName.Text    '仕入先名
                Sql4 += "', '"
                Sql4 += UtilClass.formatNumber(Decimal.Parse(DgvAdd.Rows(index).Cells("仕入値").Value.ToString)) '仕入値
                Sql4 += "', '"
                Sql4 += DgvAdd.Rows(index).Cells("発注数量").Value.ToString '発注数量
                Sql4 += "', '"
                Sql4 += UtilClass.formatNumber(DgvAdd.Rows(index).Cells("仕入数量").Value.ToString) '仕入数量
                Sql4 += "', '"

                Dim OrderQ As Integer = DgvAdd.Rows(index).Cells("発注数量").Value
                Dim PurchaseQ As Integer = DgvAdd.Rows(index).Cells("仕入数量").Value
                Dim RemainingQ As Integer = OrderQ - PurchaseQ
                'If RemainingQ < 0 Then
                '_msgHd.dspMSG("chkAPBalanceError", frmC01F10_Login.loginValue.Language)
                'End If
                Sql4 += RemainingQ.ToString '発注残数
                Sql4 += "', '"
                Sql4 += UtilClass.escapeSql(DgvAdd.Rows(index).Cells("単位").Value.ToString) '単位
                Sql4 += "', '"
                Dim dsx As DataSet = getPolByLineNo(No, Suffix, DgvAdd.Rows(index).Cells("行番号").Value)
                Sql4 += UtilClass.formatNumber(Decimal.Parse(dsx.Tables(RS).Rows(0)("仕入金額").ToString)) '仕入金額
                Sql4 += "', '"
                Sql4 += UtilClass.formatNumber(Decimal.Parse(dsx.Tables(RS).Rows(0)("間接費").ToString)) '間接費
                Sql4 += "', '"
                Sql4 += dsx.Tables(RS).Rows(0)("リードタイム").ToString 'リードタイム
                Sql4 += "', '"
                Sql4 += UtilClass.escapeSql(DgvAdd.Rows(index).Cells("備考").Value) '備考
                Sql4 += "', '"
                Sql4 += UtilClass.strFormatDate(DtpPurchaseDate.Text) '仕入日
                Sql4 += "', '"
                Sql4 += Input '更新者
                Sql4 += "', '"
                Sql4 += strToday '更新日
                Sql4 += "', '"
                Sql4 += DgvAdd.Rows(index).Cells("行番号").Value.ToString
                Sql4 += "')"
                If DgvAdd.Rows(index).Cells("仕入数量").Value.ToString = 0 Then
                Else
                    _db.executeDB(Sql4)
                End If
            Next

#End Region


            Dim PurchaseNum As Integer
            Dim OrdingNum As Integer
            For index As Integer = 0 To DgvAdd.Rows.Count() - 1
                Sql6 = ""
                Sql6 += "UPDATE "
                Sql6 += "Public."
                Sql6 += "t21_hattyu "
                Sql6 += "SET "
                Sql6 += "仕入数量"
                Sql6 += " = '"
                Dim dsy As DataSet = getPolByLineNo(No, Suffix, DgvAdd.Rows(index).Cells("行番号").Value)
                PurchaseNum = dsy.Tables(RS).Rows(0)("仕入数量") + DgvAdd.Rows(index).Cells("仕入数量").Value
                Sql6 += PurchaseNum.ToString
                Sql6 += "', "
                Sql6 += " 発注残数"
                Sql6 += " = '"
                OrdingNum = dsy.Tables(RS).Rows(0)("発注残数") - DgvAdd.Rows(index).Cells("仕入数量").Value
                'If OrdingNum < 0 Then
                '_msgHd.dspMSG("chkAPBalanceError", frmC01F10_Login.loginValue.Language)
                'End If
                Sql6 += OrdingNum.ToString
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
                Sql6 += " 発注番号"
                Sql6 += "='"
                Sql6 += No 'ds2.Tables(RS).Rows(index)("発注番号").ToString
                Sql6 += "'"
                Sql6 += " AND"
                Sql6 += " 発注番号枝番"
                Sql6 += "='"
                Sql6 += Suffix 'ds2a.Tables(RS).Rows(index)("発注番号枝番").ToString
                Sql6 += "'"
                Sql6 += " AND"
                Sql6 += " 行番号"
                Sql6 += "='"
                Sql6 += DgvAdd.Rows(index).Cells("行番号").Value.ToString
                Sql6 += "' "
                If DgvAdd.Rows(index).Cells("仕入数量").Value.ToString = 0 Then
                Else
                    _db.executeDB(Sql6)
                End If
            Next
            '登録完了メッセージ
            _msgHd.dspMSG("completeInsert", frmC01F10_Login.loginValue.Language)

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

        Me.Dispose()

        ''買掛データ作成の確認
        'Dim result As DialogResult = _msgHd.dspMSG("addAPData", frmC01F10_Login.loginValue.Language)

        'If result = DialogResult.Yes Then
        '    Accounts() 'データ更新
        'End If

        'Dim openForm As Form = Nothing
        'Dim Status As String = CommonConst.STATUS_ORDING
        'openForm = New OrderingList(_msgHd, _db, _langHd, Me, Status)
        'openForm.Show()
        'Me.Close()

    End Sub

    '買掛登録
    Private Sub Accounts()
        Dim dtToday As DateTime = DateTime.Now
        Dim strToday As String = UtilClass.formatDatetime(dtToday)

        'グリッドに何もないときは次画面へ移動しない
        If DgvAdd.RowCount = 0 Then
            '操作できるデータではないことをアラートする
            _msgHd.dspMSG("chkActionPropriety", frmC01F10_Login.loginValue.Language)
            Exit Sub
        End If

        Dim reccnt As Integer = 0
        Dim APAmount As Decimal = 0
        For i As Integer = 0 To DgvAdd.Rows.Count - 1
            APAmount += DgvAdd.Rows(i).Cells("仕入値").Value * DgvAdd.Rows(i).Cells("仕入数量").Value
        Next

        Dim Saiban1 As String = ""
        Dim Sql As String = ""
        Dim tmp As Decimal = 0

        Dim AP As String = getSaiban("100", dtToday)

        Sql = "SELECT * FROM public.t20_hattyu"
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND 発注番号 = '" & No & "'"
        Sql += " AND 発注番号枝番 = '" & Suffix & "'"
        Sql += " AND 取消区分 = '" & CommonConst.CANCEL_KBN_ENABLED & "'" '未取消

        '発注データから該当データを取得
        Dim dsHattyu As DataSet = _db.selectDB(Sql, RS, reccnt)

        Sql = "SELECT * FROM public.t46_kikehd"
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND 発注番号 = '" & No & "'"
        Sql += " AND 発注番号枝番 = '" & Suffix & "'"
        Sql += " AND 取消区分 = '" & CommonConst.CANCEL_KBN_ENABLED & "'" '未取消

        '買掛データから該当データを取得
        Dim dsKikehd As DataSet = _db.selectDB(Sql, RS, reccnt)

        '買掛済みの金額を算出
        Dim kikePrice As Decimal = 0

        '買掛金額計を集計
        kikePrice = IIf(dsKikehd.Tables(RS).Compute("SUM(買掛金額計)", Nothing) IsNot DBNull.Value,
                        dsKikehd.Tables(RS).Compute("SUM(買掛金額計)", Nothing),
                        0)

        '今回仕入分で起こせる買掛金よりも発注金額を上回る場合、仕入金額から買掛金額を引いた金額で買掛データを作成する
        If dsHattyu.Tables(RS).Rows(0)("仕入金額") >= (kikePrice + APAmount) Then
            tmp = APAmount
        Else
            tmp = dsHattyu.Tables(RS).Rows(0)("仕入金額") - kikePrice
        End If

        Sql = "INSERT INTO "
        Sql += "Public."
        Sql += "t46_kikehd("
        Sql += "会社コード, 買掛番号, 客先番号, 買掛区分, 買掛日, 発注番号, 発注番号枝番, 仕入先コード, 仕入先名"
        Sql += ", 買掛金額計, 買掛残高, 備考1, 備考2, 取消区分, 登録日, 更新者, 更新日,支払予定日)"
        Sql += " VALUES('"
        Sql += dsHattyu.Tables(RS).Rows(0)("会社コード").ToString
        Sql += "', '"
        Sql += AP
        Sql += "', '"
        Sql += dsHattyu.Tables(RS).Rows(0)("客先番号").ToString
        Sql += "', '"
        Sql += CommonConst.APC_KBN_NORMAL.ToString
        Sql += "', '"
        Sql += UtilClass.strFormatDate(DtpPurchaseDate.Value)
        Sql += "', '"
        Sql += dsHattyu.Tables(RS).Rows(0)("発注番号").ToString
        Sql += "', '"
        Sql += dsHattyu.Tables(RS).Rows(0)("発注番号枝番").ToString
        Sql += "', '"
        Sql += dsHattyu.Tables(RS).Rows(0)("仕入先コード").ToString
        Sql += "', '"
        Sql += dsHattyu.Tables(RS).Rows(0)("仕入先名").ToString
        Sql += "', '"
        Sql += UtilClass.formatNumber(tmp.ToString)
        Sql += "', '"
        Sql += UtilClass.formatNumber(tmp.ToString)
        Sql += "', '"
        Sql += UtilClass.escapeSql(TxtRemarks.Text)
        Sql += "', '"
        Sql += UtilClass.escapeSql(DgvAdd.Rows(0).Cells("備考").Value)
        Sql += "', '"
        Sql += "0"
        Sql += "', '"
        Sql += strToday
        Sql += "', '"
        Sql += frmC01F10_Login.loginValue.TantoNM
        Sql += "', '"
        Sql += strToday
        Sql += "', '"
        Sql += UtilClass.strFormatDate(DtpPaymentDate.Text)
        Sql += "')"

        _db.executeDB(Sql)

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
            Sql += UtilClass.formatDatetime(today)
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

    'セルの値が変更されたら
    Private Sub CellValueChanged(ByVal sender As Object,
    ByVal e As DataGridViewCellEventArgs) Handles DgvAdd.CellValueChanged

        Dim PurchaseTotal As Integer = 0

        'ヘッダー以外だったら
        If e.RowIndex > -1 Then

            '操作したカラム名を取得
            Dim currentColumn As String = DgvAdd.Columns(e.ColumnIndex).Name

            If currentColumn = "仕入数量" Then  'Cellが仕入数量の場合

                '各項目の属性チェック
                If Not IsNumeric(DgvAdd.Rows(e.RowIndex).Cells("仕入数量").Value) And (DgvAdd.Rows(e.RowIndex).Cells("仕入数量").Value IsNot Nothing) Then
                    _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
                    DgvAdd.Rows(e.RowIndex).Cells("仕入数量").Value = 0
                    Exit Sub
                End If

                Dim decTmp As Decimal = DgvAdd.Rows(e.RowIndex).Cells("仕入数量").Value
                DgvAdd.Rows(e.RowIndex).Cells("仕入数量").Value = decTmp.ToString("N2")
            End If
        End If

    End Sub

    'param1：String テーブル名
    'param2：String 詳細条件
    'Return: DataSet
    Private Function getDsData(ByVal tableName As String, Optional ByRef txtParam As String = "") As DataSet
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        Sql += "SELECT * FROM public." & tableName
        Sql += " WHERE "
        Sql += "会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += txtParam
        Return _db.selectDB(Sql, RS, reccnt)
    End Function

    '金額フォーマット（登録の際の小数点指定子）を日本の形式に合わせる
    '桁区切り記号は外す
    'Private Function formatNumber(ByVal prmVal As Decimal) As String

    '    Dim nfi As NumberFormatInfo = New CultureInfo(CommonConst.CI_JP, False).NumberFormat

    '日本の形式に書き換える
    'Return prmVal.ToString("F3", nfi) '売掛残高を増やす
    'End Function

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

    Private Function chkPurchasedQuantity(ByVal q_ As Integer, ByVal h_ As String, ByVal l_ As String, pol_ As String) As Boolean
        Dim Sql As String
        Sql = " and 発注番号='" + h_ + "' and 発注番号枝番='" + l_ + "' and 行番号=" + pol_
        Dim dsb As DataSet = getDsData("t21_hattyu", Sql)

        If dsb.Tables(RS).Rows.Count > 0 Then
            If dsb.Tables(RS).Rows(0)("発注数量") < dsb.Tables(RS).Rows(0)("仕入数量") + q_ Then
                Return False
            Else
                Return True
            End If
        End If

        Return False

    End Function

    Private Function getPolByLineNo(ByVal o_ As String, ByVal l_ As String, pol_ As String) As DataSet
        Dim Sql As String
        Sql = " AND 発注番号 = '" & o_ & "'"
        Sql += " AND 発注番号枝番 = '" & l_ & "' and 行番号=" + pol_
        Return getDsData("t21_hattyu", Sql)
    End Function

    '20200809 発注品名を取得する
    Private Function mSetHatyuItem(ByVal No As String, ByVal Suffix As String, ByVal Gyo As Integer) As DataTable

        '20200809
        If frmC01F10_Login.loginValue.BumonCD = "ZENBI" Then  'ゼンビさんの場合

            '発注品名を取得する
            Dim Sql As String = "SELECT t21_i.*"
            Sql += " FROM  public.t21_hattyu_item t21_i "

            Sql += " WHERE t21_i.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND t21_i.発注番号 = '" & No & "'"
            Sql += " AND t21_i.発注番号枝番 = '" & Suffix & "'"
            Sql += " AND t21_i.行番号 = '" & Gyo & "'"

            mSetHatyuItem = _db.selectDB(Sql, RS, 0).Tables(0)
        Else
            mSetHatyuItem = Nothing
        End If

    End Function


End Class