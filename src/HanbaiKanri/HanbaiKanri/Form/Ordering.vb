﻿Option Explicit On

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
Imports System.Text.RegularExpressions
Imports System.IO

Public Class Ordering
    Inherits System.Windows.Forms.Form

    '------------------------------------------------------------------------------------------------------
    'メンバー定数宣言
    '------------------------------------------------------------------------------------------------------
    'PG制御文字 
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
    Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ
    Private _parentForm As Form

    Private CompanyCode As String = ""
    Private PurchaseNo As String = ""
    Private PurchaseSuffix As String = ""
    Private PurchaseStatus As String = ""
    Private PurchaseCount As String = ""

    Private OrderCount As String = ""

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
                   Optional ByRef prmRefNo As String = Nothing,
                   Optional ByRef prmRefSuffix As String = Nothing,
                   Optional ByRef prmRefStatus As String = Nothing)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _parentForm = prmRefForm

        PurchaseNo = prmRefNo
        PurchaseSuffix = prmRefSuffix
        PurchaseStatus = prmRefStatus

        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True
        TxtInput.Text = frmC01F10_Login.loginValue.TantoNM
        CompanyCode = frmC01F10_Login.loginValue.BumonCD

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

    '新規登録時の発注番号採番処理
    '
    Private Sub GetSiireNo_New()
        Dim reccnt As Integer = 0
        Dim dtNow As DateTime = DateTime.Now
        Dim strNow As String = UtilClass.formatDatetime(dtNow)
        Dim SqlSaiban As String = ""
        SqlSaiban += "SELECT "
        SqlSaiban += "会社コード, "
        SqlSaiban += "採番キー, "
        SqlSaiban += "最新値, "
        SqlSaiban += "最小値, "
        SqlSaiban += "最大値, "
        SqlSaiban += "接頭文字, "
        SqlSaiban += "連番桁数 "
        SqlSaiban += "FROM public.m80_saiban"
        SqlSaiban += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        SqlSaiban += " AND 採番キー = '30'"

        Dim Saiban As DataSet = _db.selectDB(SqlSaiban, RS, reccnt)

        PurchaseCount = Saiban.Tables(RS).Rows(0)(2)
        PurchaseNo = Saiban.Tables(RS).Rows(0)(5)
        PurchaseNo += dtNow.ToString("MMdd")
        PurchaseNo += PurchaseCount.PadLeft(Saiban.Tables(RS).Rows(0)(6), "0")

        PurchaseCount += 1
        Dim Saiban4 As String = ""
        Saiban4 += "UPDATE Public.m80_saiban "
        Saiban4 += "SET "
        Saiban4 += " 最新値 = '" & PurchaseCount.ToString & "'"
        Saiban4 += " , 更新者 = 'Admin'"
        Saiban4 += " , 更新日 = '" & strNow & "'"
        Saiban4 += " WHERE 会社コード ='" & frmC01F10_Login.loginValue.BumonCD & "'"
        Saiban4 += " AND 採番キー ='30' "
        _db.executeDB(Saiban4)

        TxtOrderingNo.Text = PurchaseNo
        TxtOrderingSuffix.Text = 1

    End Sub

    '画面表示時
    Private Sub Ordering_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim dtNow As DateTime = DateTime.Now
        Dim dtToday As String = formatDatetime(dtNow)

        '汎用マスタからリードタイム単位を取得
        Dim dsHanyo = getDsHanyoData(CommonConst.FIXED_KEY_READTIME)

        Dim dtReadtime As New DataTable("Table2")
        dtReadtime.Columns.Add("Display", GetType(String))
        dtReadtime.Columns.Add("Value", GetType(Integer))

        For i As Integer = 0 To dsHanyo.Tables(RS).Rows.Count - 1
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                dtReadtime.Rows.Add(dsHanyo.Tables(RS).Rows(i)("文字２"), dsHanyo.Tables(RS).Rows(i)("可変キー"))
            Else
                dtReadtime.Rows.Add(dsHanyo.Tables(RS).Rows(i)("文字１"), dsHanyo.Tables(RS).Rows(i)("可変キー"))
            End If
        Next

        Dim column2 As New DataGridViewComboBoxColumn()
        column2.DataSource = dtReadtime
        column2.ValueMember = "Value"
        column2.DisplayMember = "Display"
        column2.HeaderText = "リードタイム単位"
        column2.Name = "リードタイム単位"

        DgvItemList.Columns.Insert(12, column2)

        '汎用マスタから貿易条件を取得
        dsHanyo = getDsHanyoData(CommonConst.FIXED_KEY_TRADE_TERMS)

        Dim table3 As New DataTable("Table3")
        table3.Columns.Add("Display", GetType(String))
        table3.Columns.Add("Value", GetType(Integer))

        For index As Integer = 0 To dsHanyo.Tables(RS).Rows.Count - 1
            table3.Rows.Add(dsHanyo.Tables(RS).Rows(index)("文字１"), dsHanyo.Tables(RS).Rows(index)("可変キー"))
        Next

        Dim column3 As New DataGridViewComboBoxColumn()
        column3.DataSource = table3
        column3.ValueMember = "Value"
        column3.DisplayMember = "Display"
        column3.HeaderText = "貿易条件"
        column3.Name = "貿易条件"

        DgvItemList.Columns.Insert(14, column3)
        CbShippedBy.SelectedIndex = 0

        createWarehouseCombobox() '倉庫コンボボックス

        '翻訳
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblPurchaseNo.Text = "PurchaseOrderNo"
            LblPurchaseNo.Size = New Size(145, 23)
            TxtOrderingNo.Location = New Point(162, 12)
            Label2.Location = New Point(256, 13)
            TxtOrderingSuffix.Location = New Point(273, 13)
            LblCustomerPO.Text = "CustomerNo"
            LblCustomerPO.Location = New Point(308, 13)
            LblCustomerPO.Size = New Size(142, 23)
            TxtCustomerPO.Location = New Point(456, 13)
            LblPurchaseDate.Text = "OrderDate"
            LblPurchaseDate.Location = New Point(550, 13)
            DtpPurchaseDate.Location = New Point(668, 13)
            DtpPurchaseDate.Size = New Size(130, 22)
            LblRegistrationDate.Text = "OrderRegistrationDate"
            LblRegistrationDate.Size = New Size(158, 23)
            LblRegistrationDate.Location = New Point(802, 13)
            DtpRegistrationDate.Location = New Point(968, 13)
            DtpRegistrationDate.Size = New Size(130, 22)

            LblSupplierName.Text = "SupplierName"
            LblAddress.Text = "Address"
            LblTel.Text = "PhoneNumber"
            LblFax.Text = "FAX"
            LblPerson.Text = "NameOfPIC"
            LblPosition.Text = "PositionPICSupplier"
            LblSales.Text = "SalesPersonInCharge"
            LblInput.Text = "PICForInputting"
            LblPaymentTerms.Text = "PaymentTerms"
            TxtPaymentTerms.Location = New Point(181, 158)
            LblPaymentTerms.Size = New Size(162, 23)
            LblPurchaseRemarks.Text = "PurchaseRemarks"
            LblPurchaseRemarks.Size = New Size(162, 23)
            TxtPurchaseRemark.Location = New Point(181, 187)
            LblRemarks.Text = "QuotationRemarks"
            LblRemarks.Size = New Size(162, 23)
            TxtQuoteRemarks.Location = New Point(181, 216)
            LblItemCount.Text = "ItemCount"
            LblMethod.Text = "ShippingMethod"
            LblShipDate.Text = "ShipDate"
            LblWarehouse.Text = "Warehouse"

            LblPurchaseAmount.Text = "PurchaseOrderAmount"
            LblPurchaseAmount.Size = New Size(180, 23)
            LblPurchaseAmount.Location = New Point(923, 465)

            TxtSupplierCode.Size = New Point(62, 23)
            BtnCodeSearch.Text = "Search"
            BtnCodeSearch.Location = New Point(195, 42)
            BtnCodeSearch.Size = New Size(72, 23)
            BtnInsert.Text = "InsertLine"
            BtnUp.Text = "ShiftLineUp"
            BtnDown.Text = "ShiftLineDown"
            BtnRowsAdd.Text = "AddLine"
            BtnRowsDel.Text = "DeleteLine"
            BtnClone.Text = "LineDuplication"

            BtnPurchase.Text = "IssuePurchaseOrder"
            BtnRegistration.Text = "Registrartion"
            BtnBack.Text = "Back"

            'DgvItemList.Columns("仕入区分").HeaderText = "PurchasingClassification"
            DgvItemList.Columns("メーカー").HeaderText = "Manufacturer"
            DgvItemList.Columns("品名").HeaderText = "ItemName"
            DgvItemList.Columns("型式").HeaderText = "Spec"
            DgvItemList.Columns("数量").HeaderText = "Quantity"
            DgvItemList.Columns("単位").HeaderText = "Unit"
            DgvItemList.Columns("仕入単価").HeaderText = "PurchaseUnitPrice"
            DgvItemList.Columns("仕入金額").HeaderText = "PurchaseAmount"
            DgvItemList.Columns("リードタイム").HeaderText = "LeadTime"
            DgvItemList.Columns("リードタイム単位").HeaderText = "LeadTimeUnit"
            DgvItemList.Columns("貿易条件").HeaderText = "TradeTerms"
            DgvItemList.Columns("備考").HeaderText = "Remarks"
        End If

        '検索（Date）の初期値
        DtpRegistrationDate.Value = DateTime.Today
        DtpPurchaseDate.Value = DateTime.Today
        DtpShippedDate.Value = DateTime.Today

        'セルの内容に合わせて、行の高さが自動的に調節されるようにする
        DgvItemList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        '"Column1"列のセルのテキストを折り返して表示する
        DgvItemList.Columns("型式").DefaultCellStyle.WrapMode = DataGridViewTriState.True
        DgvItemList.Columns("備考").DefaultCellStyle.WrapMode = DataGridViewTriState.True


        'ComboBoxに表示する項目のリストを作成する
        ''汎用マスタから仕入区分を取得
        'Dim dsHanyo As DataSet = getDsHanyoData(CommonConst.FIXED_KEY_PURCHASING_CLASS)

        'Dim dtPurchasingClass As New DataTable("Table")
        'dtPurchasingClass.Columns.Add("Display", GetType(String))
        'dtPurchasingClass.Columns.Add("Value", GetType(Integer))

        'For index As Integer = 0 To dsHanyo.Tables(RS).Rows.Count - 1
        '    dtPurchasingClass.Rows.Add(dsHanyo.Tables(RS).Rows(index)("文字１"), dsHanyo.Tables(RS).Rows(index)("可変キー"))
        'Next

        'DataGridViewComboBoxColumnを作成
        'Dim column As New DataGridViewComboBoxColumn()
        'DataGridViewComboBoxColumnのDataSourceを設定
        'column.DataSource = dtPurchasingClass
        '実際の値が"Value"列、表示するテキストが"Display"列とする
        'column.ValueMember = "Value" '実際の値
        'column.DisplayMember = "Display" '表示用の値
        'column.HeaderText = "仕入区分"
        'column.Name = "仕入区分"
        'column.ValueMember = 1
        'DataGridView1に追加する
        'DgvItemList.Columns.Insert(1, column)

        Dim reccnt As Integer = 0

        '新規登録モード 伝票番号取得
        If PurchaseStatus = CommonConst.STATUS_ADD Then
            GetSiireNo_New()
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "AddNewMode"
            Else
                LblMode.Text = "新規登録モード"
            End If

            TxtSupplierCode.Enabled = True
            TxtSupplierName.Enabled = True
            TxtPostalCode.Enabled = True
            TxtAddress1.Enabled = True
            TxtTel.Enabled = True
            TxtFax.Enabled = True
            TxtPosition.Enabled = True
            TxtPerson.Enabled = True
            TxtSales.Enabled = True
            TxtPaymentTerms.Enabled = True
            DtpRegistrationDate.Enabled = True

            Exit Sub

        ElseIf PurchaseStatus Is CommonConst.STATUS_VIEW Then
            '参照モード

            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "ViewMode"
            Else
                LblMode.Text = "参照モード"
            End If

            BtnRegistration.Visible = False
            TxtCustomerPO.Enabled = False
            DtpPurchaseDate.Enabled = False
            DtpRegistrationDate.Enabled = False
            TxtSales.Enabled = False
            TxtPerson.Enabled = False
            TxtPosition.Enabled = False
            TxtPaymentTerms.Enabled = False
            TxtPurchaseRemark.Enabled = False
            CbShippedBy.Enabled = False
            DtpShippedDate.Enabled = False
            CmWarehouse.Enabled = False

            BtnInsert.Visible = False
            BtnUp.Visible = False
            BtnDown.Visible = False
            BtnRowsAdd.Visible = False
            BtnRowsDel.Visible = False
            BtnClone.Visible = False
            DgvItemList.ReadOnly = True

        End If

        '発注基本情報
        Dim Sql As String = ""

        'ここで最大値の高いものを取得するSQLを実行する
        Sql = " AND 発注番号 = '" & PurchaseNo.ToString & "'"
        Sql += " AND 発注番号枝番 = '" & PurchaseSuffix.ToString & "'"
        'Sql += " AND 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

        Dim dsHattyu As DataSet = getDsData("t20_hattyu", Sql)
        CompanyCode = dsHattyu.Tables(RS).Rows(0)("会社コード")


        '発注データから各項目を取得、表示
        If dsHattyu.Tables(RS).Rows(0)("発注番号") IsNot DBNull.Value Then
            TxtOrderingNo.Text = dsHattyu.Tables(RS).Rows(0)("発注番号")
        End If
        If dsHattyu.Tables(RS).Rows(0)("発注番号枝番") IsNot DBNull.Value Then
            TxtOrderingSuffix.Text = dsHattyu.Tables(RS).Rows(0)("発注番号枝番")
        End If
        If dsHattyu.Tables(RS).Rows(0)("発注日") IsNot DBNull.Value Then
            DtpPurchaseDate.Value = dsHattyu.Tables(RS).Rows(0)("発注日")
        End If
        If dsHattyu.Tables(RS).Rows(0)("登録日") IsNot DBNull.Value Then
            DtpRegistrationDate.Value = dsHattyu.Tables(RS).Rows(0)("登録日")
        End If
        If dsHattyu.Tables(RS).Rows(0)("仕入先コード") IsNot DBNull.Value Then
            TxtSupplierCode.Text = dsHattyu.Tables(RS).Rows(0)("仕入先コード")
        End If
        If dsHattyu.Tables(RS).Rows(0)("仕入先名") IsNot DBNull.Value Then
            TxtSupplierName.Text = dsHattyu.Tables(RS).Rows(0)("仕入先名")
        End If
        If dsHattyu.Tables(RS).Rows(0)("仕入先担当者名") IsNot DBNull.Value Then
            TxtPerson.Text = dsHattyu.Tables(RS).Rows(0)("仕入先担当者名")
        End If
        If dsHattyu.Tables(RS).Rows(0)("仕入先担当者役職") IsNot DBNull.Value Then
            TxtPosition.Text = dsHattyu.Tables(RS).Rows(0)("仕入先担当者役職")
        End If
        If dsHattyu.Tables(RS).Rows(0)("仕入先郵便番号") IsNot DBNull.Value Then
            TxtPostalCode.Text = dsHattyu.Tables(RS).Rows(0)("仕入先郵便番号")
        End If
        If dsHattyu.Tables(RS).Rows(0)("仕入先住所") IsNot DBNull.Value Then
            TxtAddress1.Text = dsHattyu.Tables(RS).Rows(0)("仕入先住所")
        End If
        If dsHattyu.Tables(RS).Rows(0)("仕入先電話番号") IsNot DBNull.Value Then
            TxtTel.Text = dsHattyu.Tables(RS).Rows(0)("仕入先電話番号")
        End If
        If dsHattyu.Tables(RS).Rows(0)("仕入先ＦＡＸ") IsNot DBNull.Value Then
            TxtFax.Text = dsHattyu.Tables(RS).Rows(0)("仕入先ＦＡＸ")
        End If
        If dsHattyu.Tables(RS).Rows(0)("営業担当者コード") IsNot DBNull.Value Then
            TxtSales.Tag = dsHattyu.Tables(RS).Rows(0)("営業担当者コード")
        End If
        If dsHattyu.Tables(RS).Rows(0)("営業担当者") IsNot DBNull.Value Then
            TxtSales.Text = dsHattyu.Tables(RS).Rows(0)("営業担当者")
        End If
        If dsHattyu.Tables(RS).Rows(0)("入力担当者") IsNot DBNull.Value Then
            TxtInput.Text = dsHattyu.Tables(RS).Rows(0)("入力担当者")
        End If
        If dsHattyu.Tables(RS).Rows(0)("支払条件") IsNot DBNull.Value Then
            TxtPaymentTerms.Text = dsHattyu.Tables(RS).Rows(0)("支払条件")
        End If
        If dsHattyu.Tables(RS).Rows(0)("備考") IsNot DBNull.Value Then
            TxtPurchaseRemark.Text = dsHattyu.Tables(RS).Rows(0)("備考")
        End If
        If dsHattyu.Tables(RS).Rows(0)("仕入金額") IsNot DBNull.Value Then
            TxtPurchaseAmount.Text = dsHattyu.Tables(RS).Rows(0)("仕入金額")
        End If
        If dsHattyu.Tables(RS).Rows(0)("見積備考") IsNot DBNull.Value Then
            TxtQuoteRemarks.Text = dsHattyu.Tables(RS).Rows(0)("見積備考")
        End If
        If dsHattyu.Tables(RS).Rows(0)("客先番号") IsNot DBNull.Value Then
            TxtCustomerPO.Text = dsHattyu.Tables(RS).Rows(0)("客先番号")
        End If

        If dsHattyu.Tables(RS).Rows(0)("出荷方法") IsNot DBNull.Value Then
            CbShippedBy.SelectedIndex = dsHattyu.Tables(RS).Rows(0)("出荷方法")
        End If
        If dsHattyu.Tables(RS).Rows(0)("出荷日") IsNot DBNull.Value Then
            '出荷日の最小値を調べて出荷日が入るようにする
            If DtpShippedDate.MinDate > dsHattyu.Tables(RS).Rows(0)("出荷日") Then
                DtpShippedDate.MinDate = dsHattyu.Tables(RS).Rows(0)("出荷日")
            End If
            DtpShippedDate.Value = dsHattyu.Tables(RS).Rows(0)("出荷日")
        End If
        If dsHattyu.Tables(RS).Rows(0)("倉庫コード") IsNot DBNull.Value Then
            CmWarehouse.SelectedValue = dsHattyu.Tables(RS).Rows(0)("倉庫コード")
        End If

        Sql = "SELECT t21.*"
        Sql += " FROM  public.t21_hattyu t21 "
        Sql += " INNER JOIN  t20_hattyu t20"
        Sql += " ON t21.会社コード = t20.会社コード"
        Sql += " And  t21.発注番号 = t20.発注番号"
        Sql += " And  t21.発注番号枝番 = t20.発注番号枝番"

        Sql += " WHERE t21.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND t21.発注番号 ILIKE '" & PurchaseNo.ToString & "'"
        Sql += " AND t21.発注番号枝番 ILIKE '" & PurchaseSuffix.ToString & "'"
        'Sql += " AND t20.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
        Sql += " ORDER BY t21.行番号 "

        Dim dsHattyudt As DataSet = _db.selectDB(Sql, RS, reccnt)
        'Dim dsHattyudt As DataSet = getDsData("t21_hattyu", Sql)

        For i As Integer = 0 To dsHattyudt.Tables(RS).Rows.Count - 1
            DgvItemList.Rows.Add()
            'DgvItemList.Rows(i).Cells("仕入区分").Value = Integer.Parse(dsHattyudt.Tables(RS).Rows(i)("仕入区分"))
            DgvItemList.Rows(i).Cells("メーカー").Value = dsHattyudt.Tables(RS).Rows(i)("メーカー")
            DgvItemList.Rows(i).Cells("品名").Value = dsHattyudt.Tables(RS).Rows(i)("品名")
            DgvItemList.Rows(i).Cells("型式").Value = dsHattyudt.Tables(RS).Rows(i)("型式")
            DgvItemList.Rows(i).Cells("数量").Value = dsHattyudt.Tables(RS).Rows(i)("発注数量")
            DgvItemList.Rows(i).Cells("単位").Value = dsHattyudt.Tables(RS).Rows(i)("単位")
            DgvItemList.Rows(i).Cells("仕入単価").Value = dsHattyudt.Tables(RS).Rows(i)("仕入値")
            DgvItemList.Rows(i).Cells("間接費").Value = dsHattyudt.Tables(RS).Rows(i)("間接費")
            DgvItemList.Rows(i).Cells("仕入金額").Value = dsHattyudt.Tables(RS).Rows(i)("仕入金額")
            DgvItemList.Rows(i).Cells("リードタイム").Value = dsHattyudt.Tables(RS).Rows(i)("リードタイム")

            If dsHattyudt.Tables(RS).Rows(i)("リードタイム単位") IsNot DBNull.Value Then
                DgvItemList.Rows(i).Cells("リードタイム単位").Value = Integer.Parse(dsHattyudt.Tables(RS).Rows(i)("リードタイム単位"))
            End If

            If dsHattyudt.Tables(RS).Rows(i)("貿易条件") IsNot DBNull.Value Then
                DgvItemList.Rows(i).Cells("貿易条件").Value = Integer.Parse(dsHattyudt.Tables(RS).Rows(i)("貿易条件"))
            End If

            DgvItemList.Rows(i).Cells("備考").Value = dsHattyudt.Tables(RS).Rows(i)("備考")
            DgvItemList.Rows(i).Cells("入庫数").Value = dsHattyudt.Tables(RS).Rows(i)("入庫数")
            DgvItemList.Rows(i).Cells("未入庫数").Value = dsHattyudt.Tables(RS).Rows(i)("未入庫数")
        Next

        '行番号の振り直し
        Dim rowNo As Integer = DgvItemList.Rows.Count()
        Dim No As Integer = 1
        For c As Integer = 0 To rowNo - 1
            DgvItemList.Rows(c).Cells(0).Value = No
            No += 1
        Next c
        TxtItemCount.Text = DgvItemList.Rows.Count()


        '参照モード
        If PurchaseStatus = CommonConst.STATUS_VIEW Then

            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "ViewMode"
            Else
                LblMode.Text = "参照モード"
            End If

            DtpPurchaseDate.Enabled = False
            TxtPurchaseRemark.Enabled = False
            TxtCustomerPO.Enabled = False
            DgvItemList.ReadOnly = True
            BtnRegistration.Visible = False
            BtnPurchase.Visible = True
            BtnPurchase.Location = New Point(1004, 509)

            '複写モード
        ElseIf PurchaseStatus = CommonConst.STATUS_CLONE Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "NewCopyMode"
            Else
                LblMode.Text = "新規複写モード"
            End If

            TxtSupplierCode.Enabled = True
            TxtSupplierName.Enabled = True
            TxtPostalCode.Enabled = True
            TxtAddress1.Enabled = True
            TxtTel.Enabled = True
            TxtFax.Enabled = True
            TxtPosition.Enabled = True
            TxtPerson.Enabled = True
            TxtSales.Enabled = True
            TxtPaymentTerms.Enabled = True
            DtpRegistrationDate.Enabled = True

            '発注番号を新規発行
            Dim PO As String = getSaiban("30", dtNow)
            TxtOrderingNo.Text = PO

            '枝番は1
            TxtOrderingSuffix.Text = 1

        ElseIf PurchaseStatus = CommonConst.STATUS_EDIT Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "EditMode"
            Else
                LblMode.Text = "編集モード"
            End If


            '枝番の最大値を取得し、 +1 加算する

            Sql = " SELECT public.t20_hattyu.* "
            Sql += " FROM "
            Sql += "t20_hattyu"

            Sql += " INNER JOIN( "
            Sql += " SELECT "
            Sql += "発注番号"
            Sql += ", MAX(""発注番号枝番"") As max_val "
            Sql += " FROM t20_hattyu "
            Sql += " GROUP BY "
            Sql += " 発注番号 "
            Sql += " ) tmp "
            Sql += " ON "
            Sql += " t20_hattyu.""発注番号"" = tmp.""発注番号"""
            Sql += " AND "
            Sql += " t20_hattyu.""発注番号枝番"" = tmp.max_val"

            Sql += " where "
            Sql += " t20_hattyu.""会社コード"" = '" & frmC01F10_Login.loginValue.BumonCD & "'"

            Sql += " AND "
            Sql += " t20_hattyu.""発注番号"" "
            Sql += " ILIKE "
            Sql += "'"
            Sql += dsHattyu.Tables(RS).Rows(0)("発注番号")
            Sql += "'"
            Sql += " AND "
            Sql += " t20_hattyu.""取消区分"" = " & CommonConst.CANCEL_KBN_ENABLED


            Dim ds2 As DataSet = _db.selectDB(Sql, RS, reccnt)

            '枝番は+1
            TxtOrderingSuffix.Text = ds2.Tables(RS).Rows(0)("発注番号枝番") + 1

        End If

    End Sub
    '前の画面に戻る
    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    'セルの値が変更されたら
    Private Sub CellValueChanged(ByVal sender As Object,
    ByVal e As DataGridViewCellEventArgs) Handles DgvItemList.CellValueChanged

        Dim PurchaseTotal As Integer = 0

        '発注金額をクリア
        TxtPurchaseAmount.Clear()



        'ヘッダー以外だったら
        If e.RowIndex > -1 Then

            '各項目の属性チェック
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("数量").Value) And (DgvItemList.Rows(e.RowIndex).Cells("数量").Value IsNot Nothing) Then
                MessageBox.Show("Please enter with numeric value.", "Quantity Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                DgvItemList.Rows(e.RowIndex).Cells("数量").Value = Nothing
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value) And (DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value IsNot Nothing) Then
                MessageBox.Show("Please enter with numeric value.", "PurchaseAmount Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value = Nothing
                Exit Sub
            End If

            '数量と仕入単価が入力されていたら
            If DgvItemList.Rows(e.RowIndex).Cells("数量").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value IsNot Nothing Then
                '仕入金額 = 数量 * 仕入単価
                DgvItemList.Rows(e.RowIndex).Cells("仕入金額").Value = DgvItemList.Rows(e.RowIndex).Cells("数量").Value * DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value

            End If
        End If

        '明細をループし、仕入金額を合算する
        For i As Integer = 0 To DgvItemList.Rows.Count - 1
            PurchaseTotal += DgvItemList.Rows(i).Cells("仕入金額").Value
        Next
        TxtPurchaseAmount.Text = PurchaseTotal

    End Sub

    '行移動上
    Private Sub BtnUp_Click(sender As Object, e As EventArgs) Handles BtnUp.Click
        'グリッドに何もないときは処理しない
        If DgvItemList.CurrentCell Is Nothing Then
            Exit Sub
        End If

        If DgvItemList.CurrentCell.RowIndex > 0 Then
            DgvItemList.CurrentCell = DgvItemList(DgvItemList.CurrentCell.ColumnIndex, DgvItemList.CurrentCell.RowIndex - 1)
        End If
    End Sub

    '行移動下
    Private Sub BtnDown_Click(sender As Object, e As EventArgs) Handles BtnDown.Click
        'グリッドに何もないときは処理しない
        If DgvItemList.CurrentCell Is Nothing Then
            Exit Sub
        End If

        If DgvItemList.CurrentCell.RowIndex + 1 < DgvItemList.Rows.Count Then
            DgvItemList.CurrentCell = DgvItemList(DgvItemList.CurrentCell.ColumnIndex, DgvItemList.CurrentCell.RowIndex + 1)
        End If
    End Sub

    '任意の場所に行を挿入
    Private Sub BtnInsert_Click(sender As Object, e As EventArgs) Handles BtnInsert.Click

        If DgvItemList.Rows.Count > 0 Then
            Dim RowIdx As Integer = DgvItemList.CurrentCell.RowIndex
            '行を挿入
            DgvItemList.Rows.Insert(RowIdx + 1)
            DgvItemList.Rows(RowIdx + 1).Cells("リードタイム単位").Value = 1
        Else
            DgvItemList.Rows.Add()
            DgvItemList.Rows(0).Cells("リードタイム単位").Value = 1
            TxtItemCount.Text = DgvItemList.Rows.Count()
        End If

        '最終行のインデックスを取得
        Dim index As Integer = DgvItemList.Rows.Count()
        '行番号の振り直し
        Dim No As Integer = 1
        For c As Integer = 0 To index - 1
            DgvItemList.Rows(c).Cells("No").Value = No
            No += 1
        Next c

        'リストの行数をセット
        TxtItemCount.Text = DgvItemList.Rows.Count()
    End Sub

    '行追加（DGVの最終行に追加）
    Private Sub BtnRowsAdd_Click(sender As Object, e As EventArgs) Handles BtnRowsAdd.Click
        DgvItemList.Rows.Add()
        DgvItemList.Rows(DgvItemList.Rows.Count() - 1).Cells("リードタイム単位").Value = 1

        '行番号の振り直し
        Dim index As Integer = DgvItemList.Rows.Count()
        Dim No As Integer = 1
        For c As Integer = 0 To index - 1
            DgvItemList.Rows(c).Cells("No").Value = No
            No += 1
        Next c

        'リストの行数をセット
        TxtItemCount.Text = DgvItemList.Rows.Count()
    End Sub

    '選択行の削除（削除時に金額の再計算、Noの再採番）
    Private Sub BtnRowsDel_Click(sender As Object, e As EventArgs) Handles BtnRowsDel.Click

        For Each r As DataGridViewCell In DgvItemList.SelectedCells
            DgvItemList.Rows.RemoveAt(r.RowIndex)
        Next r

        TxtPurchaseAmount.Clear()

        Dim Total As Integer = 0
        Dim PurchaseTotal As Integer = 0
        Dim GrossProfit As Decimal = 0

        For c As Integer = 0 To DgvItemList.Rows.Count - 1
            PurchaseTotal += DgvItemList.Rows(c).Cells("仕入金額").Value
        Next
        TxtPurchaseAmount.Text = PurchaseTotal

        '行番号の振り直し
        Dim index As Integer = DgvItemList.Rows.Count()
        Dim No As Integer = 1
        For c As Integer = 0 To index - 1
            DgvItemList.Rows(c).Cells(0).Value = No
            No += 1
        Next c
        TxtItemCount.Text = DgvItemList.Rows.Count()
    End Sub

    '行の複写（選択行の直下に複写）
    Private Sub BtnClone_Click(sender As Object, e As EventArgs) Handles BtnClone.Click
        Try
            'メニュー選択処理
            Dim RowIdx As Integer
            Dim Item(15) As String

            '一覧選択行インデックスの取得
            'グリッドに何もないときは処理しない
            If DgvItemList.CurrentCell Is Nothing Then
                Exit Sub
            End If

            RowIdx = DgvItemList.CurrentCell.RowIndex

            Console.WriteLine("列数カウント：" & DgvItemList.Rows(RowIdx).Cells.Count)

            '選択行の値を格納
            For c As Integer = 0 To 15
                Item(c) = DgvItemList.Rows(RowIdx).Cells(c).Value
            Next c

            '行を挿入
            DgvItemList.Rows.Insert(RowIdx + 1)

            '追加した行に複製元の値を格納
            For c As Integer = 0 To 15
                If c = 12 Or c = 14 Then
                    If Item(c) IsNot Nothing Then
                        Dim tmp As Integer = Item(c)
                        DgvItemList(c, RowIdx + 1).Value = tmp
                    End If
                Else
                    DgvItemList.Rows(RowIdx + 1).Cells(c).Value = Item(c)
                End If

            Next c

            '最終行のインデックスを取得
            Dim index As Integer = DgvItemList.Rows.Count()
            '行番号の振り直し
            Dim No As Integer = 1
            For c As Integer = 0 To index - 1
                DgvItemList.Rows(c).Cells(0).Value = No
                No += 1
            Next c
            TxtItemCount.Text = DgvItemList.Rows.Count()
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    Private Sub TxtCustomerCode_DoubleClick(sender As Object, e As EventArgs) Handles TxtSupplierCode.DoubleClick
        Dim openForm As Form = Nothing
        Dim idx As Integer = 0
        Dim Status As String = CommonConst.STATUS_CLONE
        openForm = New SupplierSearch(_msgHd, _db, _langHd, idx, Me, Status)   '処理選択
        openForm.Show(Me)
        Me.Enabled = False
    End Sub

    Private Sub TxtSales_DoubleClick(sender As Object, e As EventArgs) Handles TxtSales.DoubleClick
        Dim openForm As Form = Nothing
        Dim Status As String = CommonConst.STATUS_CLONE
        openForm = New SalesSearch(_msgHd, _db, _langHd, Me, Status)   '処理選択
        openForm.Show(Me)
        Me.Enabled = False
    End Sub

    Private Sub DgvItemList_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvItemList.CellDoubleClick

        '行ヘッダークリック時は無効
        If e.RowIndex < 0 Then
            Exit Sub
        End If

        If PurchaseStatus Is CommonConst.STATUS_VIEW Then
            Exit Sub
        End If


        Dim Status As String = CommonConst.STATUS_CLONE

        Dim selectColumn As String = DgvItemList.Columns(e.ColumnIndex).Name

        Dim Maker As String = DgvItemList("メーカー", e.RowIndex).Value
        Dim Item As String = DgvItemList("品名", e.RowIndex).Value
        Dim Model As String = DgvItemList("型式", e.RowIndex).Value

        If selectColumn = "メーカー" Or selectColumn = "品名" Or selectColumn = "型式" Then
            '各項目チェック
            If selectColumn = "型式" And (Maker Is Nothing And Item Is Nothing) Then
                'メーカー、品名を入力してください。
                _msgHd.dspMSG("chkManufacturerItemNameError", frmC01F10_Login.loginValue.Language)
                Return

            ElseIf selectColumn = "品名" And (Maker Is Nothing) Then
                'メーカーを入力してください。
                _msgHd.dspMSG("chkManufacturerError", frmC01F10_Login.loginValue.Language)
                Return
            End If

            Dim openForm As Form = Nothing
            openForm = New MakerSearch(_msgHd, _db, Me, e.RowIndex, e.ColumnIndex, Maker, Item, Model, selectColumn, Status)   '処理選択
            openForm.Show(Me)
            Me.Enabled = False
        End If

    End Sub

    '登録ボタン押下時
    Private Sub BtnRegistration_Click(sender As Object, e As EventArgs) Handles BtnRegistration.Click
        '項目チェック
        Dim strMessage As String = ""    'メッセージ本文
        Dim strMessageTitle As String = ""      'メッセージタイトル
        ''仕入先は必須入力としましょう
        If TxtSupplierCode.Text = "" Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                strMessage = "Please enter Supplier Code. "
                strMessageTitle = "SupplierCode Error"
            Else
                strMessage = "仕入先を入力してください。"
                strMessageTitle = "仕入先入力エラー"
            End If
            Dim result As DialogResult = MessageBox.Show(strMessage, strMessageTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        '明細行がゼロ件の場合はエラーとする
        If DgvItemList.Rows.Count = 0 Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                strMessage = "Please enter the details. "
                strMessageTitle = "details Error"
            Else
                strMessage = "明細を入力してください。"
                strMessageTitle = "明細入力エラー"
            End If
            Dim result As DialogResult = MessageBox.Show(strMessage, strMessageTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        '数量と仕入単価がなかったらエラーで戻す
        For i As Integer = 0 To DgvItemList.RowCount - 1
            If DgvItemList.Rows(i).Cells("仕入単価").Value Is Nothing Or DgvItemList.Rows(i).Cells("数量").Value Is Nothing Then

                '対象データがないメッセージを表示
                _msgHd.dspMSG("chkOrderingInputError", frmC01F10_Login.loginValue.Language)

                Exit Sub
            Else
                If DgvItemList.Rows(i).Cells("数量").Value = 0 Then
                    '対象データがないメッセージを表示
                    _msgHd.dspMSG("chkQuantityError", frmC01F10_Login.loginValue.Language)

                    Exit Sub
                End If

            End If
        Next

        Dim reccnt As Integer = 0
        Dim dtNow As String = formatDatetime(DateTime.Now)
        Dim Sql As String = ""

        Try
            '複写か編集の時
            If PurchaseStatus = CommonConst.STATUS_CLONE Or PurchaseStatus = CommonConst.STATUS_EDIT Or PurchaseStatus = CommonConst.STATUS_ADD Then
                Sql = "INSERT INTO "
                Sql += "Public."
                Sql += "t20_hattyu("
                Sql += "会社コード, 発注番号, 発注番号枝番, 客先番号, 受注番号, 受注番号枝番, 見積番号, 見積番号枝番"
                Sql += ", 得意先コード, 得意先名, 得意先郵便番号, 得意先住所, 得意先電話番号, 得意先ＦＡＸ"
                Sql += ", 得意先担当者役職, 得意先担当者名, 仕入先コード, 仕入先名, 仕入先郵便番号, 仕入先住所"
                Sql += ", 仕入先電話番号, 仕入先ＦＡＸ, 仕入先担当者役職, 仕入先担当者名, 見積日, 見積有効期限"
                Sql += ", 支払条件, 見積金額,仕入金額, 粗利額, 営業担当者,入力担当者, 備考, 見積備考, ＶＡＴ, ＰＰＨ"
                Sql += ", 受注日, 発注日, 登録日, 更新日, 更新者, 取消区分, 出荷方法, 出荷日, 営業担当者コード, 入力担当者コード, 倉庫コード)"

                Sql += " VALUES('"

                Sql += CompanyCode '会社コード
                Sql += "', '"
                Sql += TxtOrderingNo.Text '発注番号
                Sql += "', '"
                Sql += TxtOrderingSuffix.Text '発注番号枝番
                Sql += "', '"
                Sql += UtilClass.escapeSql(TxtCustomerPO.Text) '客先番号
                Sql += "', '"
                Sql += "" '受注番号
                Sql += "', '"
                Sql += "" '受注番号枝番
                Sql += "', '"
                Sql += "" '見積番号
                Sql += "', '"
                Sql += "" '見積番号枝番
                Sql += "', '"
                Sql += "" '得意先コード
                Sql += "', '"
                Sql += "" '得意先名
                Sql += "', '"
                Sql += "" '得意先郵便番号
                Sql += "', '"
                Sql += "" '得意先住所
                Sql += "', '"
                Sql += "" '得意先電話番号
                Sql += "', '"
                Sql += "" '得意先ＦＡＸ
                Sql += "', '"
                Sql += "" '得意先担当者役職
                Sql += "', '"
                Sql += "" '得意先担当者名
                Sql += "', '"
                Sql += TxtSupplierCode.Text '仕入先コード
                Sql += "', '"
                Sql += TxtSupplierName.Text '仕入先名
                Sql += "', '"
                Sql += TxtPostalCode.Text '仕入先郵便番号
                Sql += "', '"
                Sql += TxtAddress1.Text '仕入先住所
                Sql += "', '"
                Sql += TxtTel.Text '仕入先電話番号
                Sql += "', '"
                Sql += TxtFax.Text '仕入先ＦＡＸ
                Sql += "', '"
                Sql += UtilClass.escapeSql(TxtPosition.Text) '仕入先担当者役職
                Sql += "', '"
                Sql += UtilClass.escapeSql(TxtPerson.Text) '仕入先担当者名

                Sql += "', "
                Sql += "null" '見積日
                Sql += ", "
                Sql += "null" '見積有効期限
                Sql += ", '"
                Sql += UtilClass.escapeSql(TxtPaymentTerms.Text) '支払条件
                Sql += "', '"
                Sql += "0" '見積金額
                Sql += "', '"
                Sql += formatNumber(TxtPurchaseAmount.Text) '仕入金額
                Sql += "', '"
                Sql += "0" '粗利額
                Sql += "', '"
                Sql += TxtSales.Text '営業担当者
                Sql += "', '"
                Sql += TxtInput.Text '入力担当者
                Sql += "', '"
                Sql += UtilClass.escapeSql(TxtPurchaseRemark.Text) '備考
                Sql += "', '"
                Sql += "" '見積備考
                Sql += "', '"
                Sql += "0" 'ＶＡＴ
                Sql += "', '"
                Sql += "0" 'ＰＰＨ
                Sql += "', "
                Sql += "null" '受注日
                Sql += ", '"
                Sql += strFormatDate(DtpPurchaseDate.Value) '発注日
                Sql += "', '"
                Sql += dtNow '登録日
                Sql += "', '"
                Sql += dtNow '更新日
                Sql += "', '"
                Sql += frmC01F10_Login.loginValue.TantoNM '更新者
                Sql += "', '"
                Sql += "0" '取消区分

                Sql += "', '"
                Sql += CbShippedBy.SelectedIndex.ToString '出荷方法
                Sql += "', '"
                Sql += strFormatDate(DtpShippedDate.Value) '出荷日
                Sql += "', '"
                Sql += TxtSales.Tag '営業担当者コード
                Sql += "', '"
                Sql += frmC01F10_Login.loginValue.TantoCD '入力担当者コード
                Sql += "', '"

                If CmWarehouse.SelectedIndex <> -1 Then
                    Sql += CmWarehouse.SelectedValue.ToString '倉庫コード
                Else
                    Sql += "" '倉庫コード
                End If

                Sql += "') "

                _db.executeDB(Sql)

                For i As Integer = 0 To DgvItemList.Rows.Count - 1

                    Sql = "INSERT INTO "
                    Sql += "Public."
                    Sql += "t21_hattyu("
                    Sql += "会社コード, 発注番号, 発注番号枝番, 行番号, 仕入区分, 仕入先名, メーカー, 品名, 型式, 単位, 仕入値, 発注数量, 仕入数量, 発注残数, 間接費, 仕入単価, 仕入金額, リードタイム, リードタイム単位, 入庫数, 未入庫数, 備考, 更新者, 登録日, 更新日"

                    Sql += IIf(
                    DgvItemList.Rows(i).Cells("貿易条件").Value IsNot Nothing,
                    ", 貿易条件",
                    "")
                    Sql += " )VALUES('"
                    Sql += CompanyCode
                    Sql += "', '"
                    Sql += TxtOrderingNo.Text
                    Sql += "', '"
                    Sql += TxtOrderingSuffix.Text
                    Sql += "', '"
                    Sql += DgvItemList.Rows(i).Cells("No").Value.ToString
                    Sql += "', '2"
                    'Sql += DgvItemList.Rows(hattyuIdx).Cells("仕入区分").Value.ToString
                    Sql += "', '"
                    Sql += TxtSupplierName.Text
                    Sql += "', '"
                    Sql += DgvItemList.Rows(i).Cells("メーカー").Value.ToString
                    Sql += "', '"
                    Sql += DgvItemList.Rows(i).Cells("品名").Value.ToString
                    Sql += "', '"
                    Sql += DgvItemList.Rows(i).Cells("型式").Value.ToString
                    Sql += "', '"
                    Sql += UtilClass.escapeSql(DgvItemList.Rows(i).Cells("単位").Value)
                    Sql += "', '"
                    Sql += formatNumber(DgvItemList.Rows(i).Cells("仕入単価").Value.ToString)
                    Sql += "', '"
                    Sql += formatNumber(DgvItemList.Rows(i).Cells("数量").Value.ToString)
                    Sql += "', '"
                    Sql += "0"
                    Sql += "', '"
                    Sql += formatNumber(DgvItemList.Rows(i).Cells("数量").Value.ToString)
                    Sql += "', 0"
                    'Sql += formatNumber(DgvItemList.Rows(hattyuIdx).Cells("間接費").Value.ToString)
                    Sql += ", '"
                    Sql += formatNumber(DgvItemList.Rows(i).Cells("仕入単価").Value.ToString)
                    Sql += "', '"
                    Sql += formatNumber(DgvItemList.Rows(i).Cells("仕入金額").Value.ToString)
                    Sql += "', '"
                    Sql += IIf(
                                DgvItemList.Rows(i).Cells("リードタイム").Value IsNot Nothing,
                                DgvItemList.Rows(i).Cells("リードタイム").Value,
                                "")
                    Sql += "', '"
                    Sql += DgvItemList.Rows(i).Cells("リードタイム単位").Value.ToString
                    Sql += "', '"
                    Sql += "0"
                    Sql += "', '"
                    Sql += DgvItemList.Rows(i).Cells("数量").Value.ToString
                    Sql += "', '"
                    Sql += UtilClass.escapeSql(DgvItemList.Rows(i).Cells("備考").Value)
                    Sql += "', '"
                    Sql += frmC01F10_Login.loginValue.TantoNM
                    Sql += "', '"
                    Sql += dtNow
                    Sql += "', '"
                    Sql += dtNow
                    Sql += "'"
                    Sql += IIf(
                    DgvItemList.Rows(i).Cells("貿易条件").Value IsNot Nothing,
                    ", '" & DgvItemList.Rows(i).Cells("貿易条件").Value & "'",
                    "")
                    Sql += " )"

                    _db.executeDB(Sql)
                Next

                '複写か編集の時以外
            Else

                Sql = "UPDATE "
                Sql += "Public."
                Sql += "t20_hattyu "
                Sql += "SET "

                Sql += "備考"
                Sql += " = '"
                Sql += UtilClass.escapeSql(TxtPurchaseRemark.Text)
                Sql += "', "
                Sql += "受注日"
                Sql += " = '"
                Sql += DtpPurchaseDate.Value
                Sql += "', "
                Sql += "更新日"
                Sql += " = '"
                Sql += dtNow
                Sql += "', "
                Sql += "更新者"
                Sql += " = '"
                Sql += frmC01F10_Login.loginValue.TantoNM
                Sql += " ' "

                Sql += "WHERE"
                Sql += " 会社コード"
                Sql += "='"
                Sql += CompanyCode
                Sql += "'"
                Sql += " AND"
                Sql += " 発注番号"
                Sql += "='"
                Sql += PurchaseNo
                Sql += "' "
                Sql += " AND"
                Sql += " 発注番号枝番"
                Sql += "='"
                Sql += PurchaseSuffix
                Sql += "' "

                _db.executeDB(Sql)
            End If

            'Me.Close()
            _parentForm.Enabled = True
            _parentForm.Show()
            Me.Dispose()

        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '発注書発行のボタン押下時
    Private Sub BtnPurchase_Click(sender As Object, e As EventArgs) Handles BtnPurchase.Click
        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        Dim reccnt As Integer = 0
        Dim Sql As String = ""

        Sql = " AND 発注番号 = '" & PurchaseNo.ToString & "'"
        Sql += " AND 発注番号枝番 = '" & PurchaseSuffix.ToString & "'"
        Sql += " AND 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

        Dim dsHattyuhd = getDsData("t20_hattyu", Sql)

        Sql = ""
        Sql += " AND 発注番号 = '" & PurchaseNo.ToString & "'"
        Sql += " AND 発注番号枝番 = '" & PurchaseSuffix.ToString & "'"

        Dim dsHattyudt = getDsData("t21_hattyu", Sql)

        '====================================
        ' Excel作成
        '====================================
        '定義
        Dim app As Excel.Application = Nothing
        Dim book As Excel.Workbook = Nothing
        Dim sheet As Excel.Worksheet = Nothing

        Try
            '雛形パス
            Dim sHinaPath As String = ""
            sHinaPath = StartUp._iniVal.BaseXlsPath

            '雛形ファイル名
            Dim sHinaFile As String = ""
            sHinaFile = sHinaPath & "\" & "PurchaseOrder.xlsx"

            '出力先パス
            Dim sOutPath As String = ""
            sOutPath = StartUp._iniVal.OutXlsPath

            '出力ファイル名
            Dim sOutFile As String = ""
            sOutFile = sOutPath & "\" & dsHattyuhd.Tables(RS).Rows(0)("発注番号") & "-" & dsHattyuhd.Tables(RS).Rows(0)("発注番号枝番") & ".xlsx"

            app = New Excel.Application()
            book = app.Workbooks.Add(sHinaFile)  'テンプレート
            sheet = CType(book.Worksheets(1), Excel.Worksheet)

            sheet.Range("C8").Value = dsHattyuhd.Tables(RS).Rows(0)("仕入先名") & vbLf & dsHattyuhd.Tables(RS).Rows(0)("仕入先郵便番号") & vbLf & dsHattyuhd.Tables(RS).Rows(0)("仕入先住所")
            sheet.Range("C14").Value = dsHattyuhd.Tables(RS).Rows(0)("仕入先担当者役職") & " " & dsHattyuhd.Tables(RS).Rows(0)("仕入先担当者名")
            sheet.Range("A15").Value = "Telp." & dsHattyuhd.Tables(RS).Rows(0)("仕入先電話番号") & "　Fax." & dsHattyuhd.Tables(RS).Rows(0)("仕入先ＦＡＸ")
            sheet.Range("T8").Value = dsHattyuhd.Tables(RS).Rows(0)("発注番号") & "-" & dsHattyuhd.Tables(RS).Rows(0)("発注番号枝番")
            sheet.Range("T9").Value = dsHattyuhd.Tables(RS).Rows(0)("発注日")
            If dsHattyuhd.Tables(RS).Rows(0)("出荷方法") Is DBNull.Value Then
                sheet.Range("T10").Value = ""
            Else
                Dim tmp = CbShippedBy.Items(dsHattyuhd.Tables(RS).Rows(0)("出荷方法"))
                sheet.Range("T10").Value = tmp
            End If

            sheet.Range("T11").Value = dsHattyuhd.Tables(RS).Rows(0)("出荷日")
            'sheet.Range("T12").Value = dsHattyuhd.Tables(RS).Rows(0)("仕入先名")
            'sheet.Range("T13").Value = dsHattyuhd.Tables(RS).Rows(0)("客先番号")
            sheet.Range("T13").Value = dsHattyuhd.Tables(RS).Rows(0)("支払条件")

            'sheet.Range("H26").Value = dsHattyuhd.Tables(RS).Rows(0)("仕入金額")
            sheet.Range("H27").Value = dsHattyuhd.Tables(RS).Rows(0)("備考")

            'sheet.Range("A34").Value = dsHattyuhd.Tables(RS).Rows(0)("営業担当者")
            'sheet.Range("A35").Value = dsHattyuhd.Tables(RS).Rows(0)("入力担当者")
            sheet.Range("R30").Value = dsHattyuhd.Tables(RS).Rows(0)("仕入先名")

            Dim rowCnt As Integer = 0
            Dim lstRow As Integer = 21
            Dim addRowCnt As Integer = 0
            Dim currentCnt As Integer = 19
            Dim num As Integer = 1

            rowCnt = dsHattyudt.Tables(RS).Rows.Count - 1
            'rowCnt = 10

            Dim cellPos As String = lstRow & ":" & lstRow

            If rowCnt > 1 Then
                For addRow As Integer = 0 To rowCnt
                    Dim R As Object
                    cellPos = lstRow - 2 & ":" & lstRow - 2
                    R = sheet.Range(cellPos)
                    R.Copy()
                    R.Insert()
                    If Marshal.IsComObject(R) Then
                        Marshal.ReleaseComObject(R)
                    End If

                    lstRow = lstRow + 1
                Next
            End If

            Dim totalPrice As Integer = 0
            'Dim Sql As String = ""
            For i As Integer = 0 To dsHattyudt.Tables(RS).Rows.Count - 1
                Dim cell As String

                cell = "A" & currentCnt
                sheet.Range(cell).Value = num
                cell = "C" & currentCnt
                sheet.Range(cell).Value = dsHattyudt.Tables(RS).Rows(i)("メーカー") & vbLf & dsHattyudt.Tables(RS).Rows(i)("品名") & vbLf & dsHattyudt.Tables(RS).Rows(i)("型式")
                cell = "L" & currentCnt
                sheet.Range(cell).Value = dsHattyudt.Tables(RS).Rows(i)("発注数量") & " " & dsHattyudt.Tables(RS).Rows(i)("単位")

                Dim dsHanyo = getDsHanyoData(CommonConst.FIXED_KEY_TRADE_TERMS, dsHattyudt.Tables(RS).Rows(i)("貿易条件").ToString)

                cell = "O" & currentCnt
                If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                    sheet.Range(cell).Value = dsHanyo.Tables(RS).Rows(0)("文字２")
                Else
                    sheet.Range(cell).Value = dsHanyo.Tables(RS).Rows(0)("文字１")
                End If

                cell = "R" & currentCnt
                sheet.Range(cell).Value = dsHattyudt.Tables(RS).Rows(i)("仕入値")
                cell = "W" & currentCnt
                sheet.Range(cell).Value = dsHattyudt.Tables(RS).Rows(i)("仕入金額")

                totalPrice = totalPrice + dsHattyudt.Tables(RS).Rows(i)("仕入金額")

                currentCnt = currentCnt + 1
                num = num + 1
            Next


            sheet.Range("W" & lstRow + 1).Value = totalPrice
            sheet.Range("W" & lstRow + 2).Value = Math.Ceiling(totalPrice * 10 * 0.01)
            sheet.Range("W" & lstRow + 3).Value = Math.Ceiling(totalPrice * 10 * 0.01) + totalPrice
            sheet.Range("H" & lstRow + 5).Value = Math.Ceiling(totalPrice * 10 * 0.01) + totalPrice

            app.DisplayAlerts = False 'Microsoft Excelのアラート一旦無効化

            Dim excelChk As Boolean = excelOutput(sOutFile)
            If excelChk = False Then
                Exit Sub
            End If
            book.SaveAs(sOutFile) '書き込み実行

            app.DisplayAlerts = True 'アラート無効化を解除

            app.Visible = True

            'カーソルを砂時計から元に戻す
            Cursor.Current = Cursors.Default

            _msgHd.dspMSG("CreateExcel", frmC01F10_Login.loginValue.Language)

        Catch ex As Exception
            Throw ex

        Finally
            'app.Quit()
            'Marshal.ReleaseComObject(sheet)
            'Marshal.ReleaseComObject(book)
            'Marshal.ReleaseComObject(app)

        End Try
    End Sub

    '仕入先検索ボタン
    Private Sub BtnCodeSearch_Click(sender As Object, e As EventArgs) Handles BtnCodeSearch.Click
        Dim Sql As String = ""

        Sql = " AND "
        Sql += "仕入先コード"
        Sql += " ILIKE "
        Sql += "'"
        Sql += TxtSupplierCode.Text
        Sql += "'"

        Dim dsSupplier As DataSet = getDsData("m11_supplier", Sql)

        If dsSupplier.Tables(RS).Rows.Count > 0 Then
            TxtSupplierName.Text = dsSupplier.Tables(RS).Rows(0)("仕入先名").ToString
            TxtPostalCode.Text = dsSupplier.Tables(RS).Rows(0)("郵便番号").ToString
            TxtAddress1.Text = dsSupplier.Tables(RS).Rows(0)("住所１").ToString & " " & dsSupplier.Tables(RS).Rows(0)("住所２").ToString & " " & dsSupplier.Tables(RS).Rows(0)("住所３").ToString
            TxtTel.Text = dsSupplier.Tables(RS).Rows(0)("電話番号").ToString
            TxtFax.Text = dsSupplier.Tables(RS).Rows(0)("ＦＡＸ番号").ToString
            TxtPerson.Text = dsSupplier.Tables(RS).Rows(0)("担当者名").ToString
            TxtPosition.Text = dsSupplier.Tables(RS).Rows(0)("担当者役職").ToString

        End If
    End Sub

    '金額フォーマット（登録の際の小数点指定子）を日本の形式に合わせる
    '桁区切り記号は外す
    Private Function formatNumber(ByVal prmVal As Decimal) As String

        Dim nfi As NumberFormatInfo = New CultureInfo(CommonConst.CI_JP, False).NumberFormat

        '日本の形式に書き換える
        Return prmVal.ToString("F3", nfi) '売掛残高を増やす
    End Function

    'sqlで実行する文字列からシングルクォーテーションを文字コードにする
    Private Function escapeSql(ByVal prmSql As String) As String
        Dim sql As String = prmSql

        sql = sql.Replace("'"c, "''") 'シングルクォーテーションを置換

        Return Regex.Escape(sql)
        Return sql
    End Function

    'param1：String テーブル名
    'param2：String 詳細条件
    'Return: DataSet
    Private Function getDsData(ByVal tableName As String, Optional ByRef txtParam As String = "") As DataSet
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        Sql += "SELECT * FROM public." & tableName
        Sql += " WHERE 会社コード =  '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += txtParam

        Return _db.selectDB(Sql, RS, reccnt)
    End Function

    '汎用マスタから固定キー、可変キーに応じた結果を返す
    'param1：String 固定キー
    'param2：String 可変キー
    'Return: DataSet
    Private Function getDsHanyoData(ByVal prmFixed As String, Optional ByRef prmVariable As String = "") As DataSet
        Dim Sql As String = ""

        Sql = " AND 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND 固定キー = '" & prmFixed & "'"

        If prmVariable IsNot "" Then
            Sql += " AND 可変キー = '" & prmVariable & "'"
        End If

        'リードタイムのリストを汎用マスタから取得
        Return getDsData("m90_hanyo", Sql)

    End Function

    'param1：String 採番キー
    'param2：DateTime 登録日
    'Return: String 伝票番号
    '伝票番号を取得
    Private Function getSaiban(ByVal key As String, ByVal today As DateTime) As String
        Dim Sql As String = ""
        Dim saibanID As String = ""
        Dim reccnt As Integer = 0 'DB用（デフォルト）

        Try
            Sql = "SELECT * FROM public.m80_saiban"
            Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND 採番キー = '" & key & "'"

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

            Sql = "UPDATE Public.m80_saiban "
            Sql += "SET  最新値  = '" & keyNo.ToString & "'"
            Sql += " , 更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
            Sql += " , 更新日 = '" & UtilClass.formatDatetime(today) & "'"
            Sql += "WHERE 会社コード ='" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND 採番キー = '" & key & "'"
            Console.WriteLine(Sql)
            _db.executeDB(Sql)

            Return saibanID
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Function

    'どんなカルチャーであっても、日本の形式に変換する
    Private Function strFormatDate(ByVal prmDate As String, Optional ByRef prmFormat As String = "yyyy/MM/dd") As String

        'PCのカルチャーを取得し、それに応じてStringからDatetimeを作成
        Dim ci As New System.Globalization.CultureInfo(CultureInfo.CurrentCulture.Name.ToString)
        Dim dateFormat As DateTime = DateTime.Parse(prmDate, ci, System.Globalization.DateTimeStyles.AssumeLocal)

        '日本の形式に書き換える
        Return dateFormat.ToString(prmFormat)
    End Function

    'どんなカルチャーであっても、日本の形式に変換する
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

    '発注日変更時、出荷日のMinDate及びValueを変更
    Private Sub DtpPurchaseDate_ValueChanged(sender As Object, e As EventArgs) Handles DtpPurchaseDate.ValueChanged
        '出荷日が発注日より小さかったら
        If DtpPurchaseDate.Value.ToString("yyyyMMdd") > DtpShippedDate.Value.ToString("yyyyMMdd") Then
            DtpShippedDate.MinDate = DtpPurchaseDate.Value
            DtpShippedDate.Value = DtpPurchaseDate.Value
        End If

    End Sub

    '倉庫のコンボボックスを作成
    '編集モードの時は値を渡してセットさせる
    Private Sub createWarehouseCombobox(Optional ByRef prmVal As String = "")
        CmWarehouse.DisplayMember = "Text"
        CmWarehouse.ValueMember = "Value"

        Dim Sql As String = " AND 無効フラグ = '" & CommonConst.FLAG_ENABLED & "'"

        Dim ds As DataSet = getDsData("m20_warehouse", Sql)

        Dim tb As New DataTable
        tb.Columns.Add("Text", GetType(String))
        tb.Columns.Add("Value", GetType(String))

        For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
            tb.Rows.Add(ds.Tables(RS).Rows(i)("名称"), ds.Tables(RS).Rows(i)("倉庫コード"))

        Next

        CmWarehouse.DataSource = tb

        If prmVal IsNot "" Then
            CmWarehouse.SelectedValue = prmVal
        Else
            CmWarehouse.SelectedIndex = 0
        End If

    End Sub

    'Excel出力する際のチェック
    Private Function excelOutput(ByVal prmFilePath As String)
        Dim fileChk As String = Dir(prmFilePath)
        '同名ファイルがあるかどうかチェック
        If fileChk <> "" Then
            Dim result = _msgHd.dspMSG("confirmFileExist", frmC01F10_Login.loginValue.Language, prmFilePath)
            If result = DialogResult.No Then
                Return False
            End If

            Try
                'ファイルが開けるかどうかチェック
                Dim sr As StreamReader = New StreamReader(prmFilePath)
                sr.Close() '処理が通ったら閉じる
            Catch ex As Exception
                '開けない場合はアラートを表示してリターンさせる
                MessageBox.Show(ex.Message, CommonConst.AP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End Try

            Return True
        End If
        Return True
    End Function

End Class