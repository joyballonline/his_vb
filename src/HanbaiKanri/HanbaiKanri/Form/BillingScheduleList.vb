Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class BillingScheduleList
    Inherits System.Windows.Forms.Form

    '------------------------------------------------------------------------------------------------------
    'メンバー定数宣言
    '------------------------------------------------------------------------------------------------------
    'PG制御文字 
    Dim ds As DataTable
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
    Private _parentForm As Form
    'Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ
    Private CompanyCode As String = ""
    Private SalesNo As String()
    Private SalesStatus As String = ""

    Private cymnUpdateDate As DataSet
    Private _com As CommonLogic

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
    ByRef prmRefStatus As String)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _parentForm = prmRefForm
        SalesStatus = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        _com = New CommonLogic(_db, _msgHd)
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True
        DgvCymnhd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.DisplayedCells
    End Sub

    '一覧取得
    Private Sub getList()

        Dim Sql As String = ""
        Dim reccnt As Integer = 0 'DB用（デフォルト）

        '一覧クリア
        DgvCymnhd.Rows.Clear()
        DgvCymnhd.Columns.Clear()

        Try

            '抽出条件の判定
            If RbtnSlip.Checked Then


#Region "伝票単位"

                setListHd() '見出しセット

                Sql = "SELECT t30.*"
                Sql += " FROM t30_urighd t30 "

                Sql += " WHERE "
                Sql += " t30.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

                Sql += viewSearchConditions() '検索条件

                Sql += " ORDER BY "
                Sql += "t30.更新日 DESC"

                ds = _db.selectDB(Sql, RS, reccnt).Tables(0)

                For i As Integer = 0 To ds.Rows.Count - 1

                    Dim cur As String = _com.getCurrencyEx(ds.Rows(i)("通貨"))
                    'Dim curds As DataTable

                    '通貨の判定
                    'If IsDBNull(ds.Rows(i)("通貨")) Then
                    'cur = vbNullString
                    'Else
                    'Sql = " and 採番キー = " & ds.Rows(i)("通貨")
                    'curds = _com.getDsData("m25_currency", Sql).Tables(0)

                    'cur = curds.Rows(0)("通貨コード")
                    'End If


                    DgvCymnhd.Rows.Add()
                    DgvCymnhd.Rows(i).Cells("B").Value = False
                    DgvCymnhd.Rows(i).Cells("取消").Value = _com.getDelKbnTxt(ds.Rows(i)("取消区分"))
                    DgvCymnhd.Rows(i).Cells("売上番号").Value = ds.Rows(i)("売上番号")
                    DgvCymnhd.Rows(i).Cells("売上番号枝番").Value = ds.Rows(i)("売上番号枝番")

                    DgvCymnhd.Rows(i).Cells("受注番号").Value = ds.Rows(i)("受注番号")
                    DgvCymnhd.Rows(i).Cells("受注番号枝番").Value = ds.Rows(i)("受注番号枝番")
                    DgvCymnhd.Rows(i).Cells("客先番号").Value = ds.Rows(i)("客先番号")

                    DgvCymnhd.Rows(i).Cells("売上日").Value = ds.Rows(i)("売上日") '.ToShortDateString()
                    DgvCymnhd.Rows(i).Cells("受注日").Value = ds.Rows(i)("受注日") '.ToShortDateString()

                    DgvCymnhd.Rows(i).Cells("得意先コード").Value = ds.Rows(i)("得意先コード")
                    DgvCymnhd.Rows(i).Cells("得意先名").Value = ds.Rows(i)("得意先名")

                    DgvCymnhd.Rows(i).Cells("通貨_外貨").Value = cur
                    DgvCymnhd.Rows(i).Cells("売上金額_外貨").Value = ds.Rows(i)("売上金額_外貨")

                    DgvCymnhd.Rows(i).Cells("売上金額").Value = ds.Rows(i)("見積金額")
                    DgvCymnhd.Rows(i).Cells("ＶＡＴ").Value = UtilClass.VAT_round_AP(ds.Rows(i)("ＶＡＴ"), DgvCymnhd.Rows(i).Cells("売上金額_外貨").Value)

                    DgvCymnhd.Rows(i).Cells("仕入金額").Value = ds.Rows(i)("仕入金額")
                    DgvCymnhd.Rows(i).Cells("粗利額").Value = ds.Rows(i)("見積金額") - ds.Rows(i)("仕入金額")

                    If ds.Rows(i)("見積金額") = 0 Or DgvCymnhd.Rows(i).Cells("粗利額").Value = 0 Then
                        DgvCymnhd.Rows(i).Cells("粗利率").Value = 0
                    Else
                        DgvCymnhd.Rows(i).Cells("粗利率").Value = DgvCymnhd.Rows(i).Cells("粗利額").Value / ds.Rows(i)("見積金額") * 100
                    End If

                    DgvCymnhd.Rows(i).Cells("売上金額_外貨_VAT").Value = DgvCymnhd.Rows(i).Cells("売上金額_外貨").Value + DgvCymnhd.Rows(i).Cells("ＶＡＴ").Value

                    DgvCymnhd.Rows(i).Cells("得意先郵便番号").Value = ds.Rows(i)("得意先郵便番号")
                    DgvCymnhd.Rows(i).Cells("得意先住所").Value = ds.Rows(i)("得意先住所")
                    DgvCymnhd.Rows(i).Cells("得意先電話番号").Value = ds.Rows(i)("得意先電話番号")
                    DgvCymnhd.Rows(i).Cells("得意先ＦＡＸ").Value = ds.Rows(i)("得意先ＦＡＸ")
                    DgvCymnhd.Rows(i).Cells("得意先担当者名").Value = ds.Rows(i)("得意先担当者名")
                    DgvCymnhd.Rows(i).Cells("得意先担当者役職").Value = ds.Rows(i)("得意先担当者役職")

                    DgvCymnhd.Rows(i).Cells("支払条件").Value = ds.Rows(i)("支払条件")
                    DgvCymnhd.Rows(i).Cells("営業担当者").Value = ds.Rows(i)("営業担当者")
                    DgvCymnhd.Rows(i).Cells("入力担当者").Value = ds.Rows(i)("入力担当者")
                    DgvCymnhd.Rows(i).Cells("備考").Value = ds.Rows(i)("備考")
                    DgvCymnhd.Rows(i).Cells("登録日").Value = ds.Rows(i)("登録日")
                    DgvCymnhd.Rows(i).Cells("更新日").Value = ds.Rows(i)("更新日")
                Next

#End Region

            Else


#Region "明細表示"

                setListDt() '見出しセット


                Sql = "SELECT"
                Sql += " t31.*, t30.取消区分, t30.受注番号, t30.受注番号枝番, t30.得意先名"
                Sql += " FROM "
                Sql += " public.t31_urigdt t31 "

                Sql += " INNER JOIN "
                Sql += " t30_urighd t30"
                Sql += " ON "
                Sql += " t31.会社コード = t30.会社コード"
                Sql += " AND "
                Sql += " t31.売上番号 = t30.売上番号"
                Sql += " AND "
                Sql += " t31.売上番号枝番 = t30.売上番号枝番"

                Sql += " WHERE "
                Sql += " t31.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"

                Sql += viewSearchConditions() '検索条件

                Sql += " ORDER BY "
                Sql += " t30.更新日 DESC, t30.売上番号 desc, t30.売上番号枝番, t31.行番号"

                ds = _db.selectDB(Sql, RS, reccnt).Tables(0)


                For i As Integer = 0 To ds.Rows.Count - 1
                    DgvCymnhd.Rows.Add()
                    DgvCymnhd.Rows(i).Cells("売上番号").Value = ds.Rows(i)("売上番号")
                    DgvCymnhd.Rows(i).Cells("売上番号枝番").Value = ds.Rows(i)("売上番号枝番")
                    DgvCymnhd.Rows(i).Cells("行番号").Value = ds.Rows(i)("行番号")

                    'リストを汎用マスタから取得
                    Dim dsHanyou As DataTable = _com.getDsHanyoData(CommonConst.FIXED_KEY_PURCHASING_CLASS, ds.Rows(i)("仕入区分")).Tables(0)
                    DgvCymnhd.Rows(i).Cells("仕入区分").Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                                                            dsHanyou.Rows(0)("文字２"),
                                                            dsHanyou.Rows(0)("文字１"))

                    DgvCymnhd.Rows(i).Cells("取消").Value = _com.getDelKbnTxt(ds.Rows(i)("取消区分"))
                    DgvCymnhd.Rows(i).Cells("受注番号").Value = ds.Rows(i)("受注番号")
                    DgvCymnhd.Rows(i).Cells("受注番号枝番").Value = ds.Rows(i)("受注番号枝番")
                    DgvCymnhd.Rows(i).Cells("得意先名").Value = ds.Rows(i)("得意先名")
                    DgvCymnhd.Rows(i).Cells("メーカー").Value = ds.Rows(i)("メーカー")
                    DgvCymnhd.Rows(i).Cells("品名").Value = ds.Rows(i)("品名")
                    DgvCymnhd.Rows(i).Cells("型式").Value = ds.Rows(i)("型式")
                    DgvCymnhd.Rows(i).Cells("仕入先名").Value = ds.Rows(i)("仕入先名")
                    DgvCymnhd.Rows(i).Cells("仕入値").Value = ds.Rows(i)("仕入値")
                    DgvCymnhd.Rows(i).Cells("受注数量").Value = ds.Rows(i)("受注数量")
                    DgvCymnhd.Rows(i).Cells("売上数量").Value = ds.Rows(i)("売上数量")
                    DgvCymnhd.Rows(i).Cells("受注残数").Value = ds.Rows(i)("受注残数")
                    DgvCymnhd.Rows(i).Cells("単位").Value = ds.Rows(i)("単位")
                    DgvCymnhd.Rows(i).Cells("間接費").Value = ds.Rows(i)("間接費")
                    DgvCymnhd.Rows(i).Cells("売単価").Value = ds.Rows(i)("売単価")
                    DgvCymnhd.Rows(i).Cells("売上金額").Value = ds.Rows(i)("売上金額")
                    DgvCymnhd.Rows(i).Cells("粗利額").Value = ds.Rows(i)("粗利額")
                    DgvCymnhd.Rows(i).Cells("粗利率").Value = ds.Rows(i)("粗利率")
                    DgvCymnhd.Rows(i).Cells("リードタイム").Value = ds.Rows(i)("リードタイム")
                    DgvCymnhd.Rows(i).Cells("備考").Value = ds.Rows(i)("備考")
                    DgvCymnhd.Rows(i).Cells("更新者").Value = ds.Rows(i)("更新者")

                    Dim dt2 As DateTime = DateTime.Parse(ds.Rows(i)("更新日").ToShortDateString)
                    DgvCymnhd.Rows(i).Cells("更新日").Value = dt2

                Next
            End If

#End Region


        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try


    End Sub

    Private Function viewSearchConditions() As String
        Dim Sql As String = ""

        '抽出条件
        Dim customerName As String = UtilClass.escapeSql(TxtCustomerName.Text)
        Dim customerCode As String = UtilClass.escapeSql(TxtCustomerCode.Text)
        Dim customerAddress As String = UtilClass.escapeSql(TxtAddress.Text)
        Dim customerTel As String = UtilClass.escapeSql(TxtTel.Text)
        Dim sinceDate As String = UtilClass.strFormatDate(dtDateSince.Text)
        Dim untilDate As String = UtilClass.strFormatDate(dtDateUntil.Text)
        Dim sinceNum As String = UtilClass.escapeSql(TxtSalesSince.Text)
        Dim salesName As String = UtilClass.escapeSql(TxtSales.Text)
        Dim poNum As String = UtilClass.escapeSql(TxtCustomerPO.Text)
        Dim itemName As String = UtilClass.escapeSql(TxtItemName.Text)
        Dim spec As String = UtilClass.escapeSql(TxtSpec.Text)

        If customerName <> Nothing Then
            Sql += " AND "
            Sql += " t30.得意先名 ILIKE '%" & customerName & "%' "
        End If

        If customerAddress <> Nothing Then
            Sql += " AND "
            Sql += " t30.得意先住所 ILIKE '%" & customerAddress & "%' "
        End If

        If customerTel <> Nothing Then
            Sql += " AND "
            Sql += " t30.得意先電話番号 ILIKE '%" & customerTel & "%' "
        End If

        If customerCode <> Nothing Then
            Sql += " AND "
            Sql += " t30.得意先コード ILIKE '%" & customerCode & "%' "
        End If

        If sinceDate <> Nothing Then
            Sql += " AND "
            Sql += " t30.売上日 >= '" & sinceDate & "'"
        End If
        If untilDate <> Nothing Then
            Sql += " AND "
            Sql += " t30.売上日 <= '" & untilDate & "'"
        End If

        If sinceNum <> Nothing Then
            Sql += " AND "
            Sql += " t30.売上番号 ILIKE '%" & sinceNum & "%' "
        End If

        If salesName <> Nothing Then
            Sql += " AND "
            Sql += " t30.受注番号 ILIKE '%" & salesName & "%' "
        End If

        If poNum <> Nothing Then
            Sql += " AND "
            Sql += " t30.客先番号 ILIKE '%" & poNum & "%' "
        End If

        If itemName <> Nothing Then
            Sql += " AND "
            Sql += " t30.売上番号 IN(select t31.売上番号 from t31_urigdt t31 where t31.品名 ILIKE '%" & itemName & "%') "
        End If

        If spec <> Nothing Then
            Sql += " AND "
            Sql += " t30.売上番号 IN(select t31.売上番号 from t31_urigdt t31 where t31.型式 ILIKE '%" & spec & "%') "
        End If

        '取消データを含めない場合
        If ChkCancelData.Checked = False Then
            Sql += " AND "
            Sql += "t30.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
        End If

        If ChkDisplayBilled.Checked = False Then
            Sql += " AND "
            'Sql += " (t30.売上番号,t30.売上番号枝番) not in (select t23.売上番号,t23.売上番号枝番 from t23_skyuhd t23 where t23.売上番号 = t30.売上番号 "
            'Sql += " AND t23.売上番号枝番 = t30.売上番号枝番 and t30.会社コード = t23.会社コード and t23.取消区分 = 0)"
            Sql += " (t30.売上番号,t30.売上番号枝番) in (select t23.売上番号,t23.売上番号枝番 from t31_urigdt t23 where t30.会社コード = t23.会社コード and t23.入金番号 is null) "
        End If

        Return Sql
    End Function

    '伝票の見出しをセット
    Private Sub setListHd()

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            Dim objcol As DataGridViewColumn = New DataGridViewColumn()
            objcol.HeaderText = "Check"
            objcol.Name = "B"
            objcol.CellTemplate = New DataGridViewCheckBoxCell()
            objcol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            objcol.ReadOnly = False
            DgvCymnhd.Columns.Add(objcol)

            DgvCymnhd.Columns.Add("取消", "Cancel")

            DgvCymnhd.Columns.Add("売上番号", "SalesNumber")
            DgvCymnhd.Columns.Add("売上番号枝番", "SalesVer")

            DgvCymnhd.Columns.Add("受注番号", "OrderNumber")
            DgvCymnhd.Columns.Add("受注番号枝番", "OrderVer")
            DgvCymnhd.Columns.Add("客先番号", "CustomerNumber")

            DgvCymnhd.Columns.Add("売上日", "DeliveryDate")
            DgvCymnhd.Columns.Add("受注日", "JobOrderDate")

            DgvCymnhd.Columns.Add("得意先コード", "CustomerCode")
            DgvCymnhd.Columns.Add("得意先名", "CustomerName")

            DgvCymnhd.Columns.Add("通貨_外貨", "Currency")
            DgvCymnhd.Columns.Add("売上金額_外貨", "SalesAmount" & vbCrLf & "(OrignalCurrency)")

            DgvCymnhd.Columns.Add("売上金額", "SalesAmount" & vbCrLf & "(" & _com.setBaseCurrency() & ")")
            DgvCymnhd.Columns.Add("ＶＡＴ", "VAT-OUT" & vbCrLf & "(OrignalCurrency)")
            DgvCymnhd.Columns.Add("売上金額_外貨_VAT", "SalesAmount+VAT" & vbCrLf & "(OrignalCurrency)")

            DgvCymnhd.Columns.Add("仕入金額", "PurchaseAmount" & vbCrLf & "(" & _com.setBaseCurrency() & ")")
            DgvCymnhd.Columns.Add("粗利額", "Profitmargin" & vbCrLf & "(" & _com.setBaseCurrency() & ")")
            DgvCymnhd.Columns.Add("粗利率", "ProfitmarginRate(%)")

            DgvCymnhd.Columns.Add("得意先郵便番号", "PostalCode")
            DgvCymnhd.Columns.Add("得意先住所", "Address")
            DgvCymnhd.Columns.Add("得意先電話番号", "PhoneNumber")
            DgvCymnhd.Columns.Add("得意先ＦＡＸ", "FAX")
            DgvCymnhd.Columns.Add("得意先担当者名", "NameOfPIC")
            DgvCymnhd.Columns.Add("得意先担当者役職", "PositionPICCustomer")

            DgvCymnhd.Columns.Add("支払条件", "PeymentTerms")
            DgvCymnhd.Columns.Add("営業担当者", "SalesPersonInCharge")
            DgvCymnhd.Columns.Add("入力担当者", "PICForInputting")
            DgvCymnhd.Columns.Add("備考", "Remarks")
            DgvCymnhd.Columns.Add("登録日", "RegistrationDate")
            DgvCymnhd.Columns.Add("更新日", "UpdateDate")
        Else
            Dim objcol As DataGridViewColumn = New DataGridViewColumn()
            objcol.HeaderText = "Check"
            objcol.Name = "B"
            objcol.CellTemplate = New DataGridViewCheckBoxCell()
            objcol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            objcol.ReadOnly = False
            DgvCymnhd.Columns.Add(objcol)

            DgvCymnhd.Columns.Add("取消", "取消")

            DgvCymnhd.Columns.Add("売上番号", "売上番号")
            DgvCymnhd.Columns.Add("売上番号枝番", "売上Ver")

            DgvCymnhd.Columns.Add("受注番号", "受注番号")
            DgvCymnhd.Columns.Add("受注番号枝番", "受注Ver")
            DgvCymnhd.Columns.Add("客先番号", "客先番号")

            DgvCymnhd.Columns.Add("売上日", "納品日")
            DgvCymnhd.Columns.Add("受注日", "受注日")

            DgvCymnhd.Columns.Add("得意先コード", "得意先コード")
            DgvCymnhd.Columns.Add("得意先名", "得意先名")

            DgvCymnhd.Columns.Add("通貨_外貨", "販売通貨")
            DgvCymnhd.Columns.Add("売上金額_外貨", "売上金額" & vbCrLf & "(原通貨)")

            DgvCymnhd.Columns.Add("売上金額", "売上金額" & vbCrLf & "(" & _com.setBaseCurrency() & ")")
            DgvCymnhd.Columns.Add("ＶＡＴ", "VAT-OUT")
            DgvCymnhd.Columns.Add("売上金額_外貨_VAT", "SalesAmount+VAT" & vbCrLf & "(OrignalCurrency)")

            DgvCymnhd.Columns.Add("仕入金額", "仕入金額" & vbCrLf & "(" & _com.setBaseCurrency() & ")")
            DgvCymnhd.Columns.Add("粗利額", "利益" & vbCrLf & "(" & _com.setBaseCurrency() & ")")
            DgvCymnhd.Columns.Add("粗利率", "利益率(%)")

            DgvCymnhd.Columns.Add("得意先郵便番号", "得意先郵便番号")
            DgvCymnhd.Columns.Add("得意先住所", "得意先住所")
            DgvCymnhd.Columns.Add("得意先電話番号", "得意先電話番号")
            DgvCymnhd.Columns.Add("得意先ＦＡＸ", "得意先ＦＡＸ")
            DgvCymnhd.Columns.Add("得意先担当者名", "得意先担当者名")
            DgvCymnhd.Columns.Add("得意先担当者役職", "得意先担当者役職")

            DgvCymnhd.Columns.Add("支払条件", "支払条件")
            DgvCymnhd.Columns.Add("営業担当者", "営業担当者")
            DgvCymnhd.Columns.Add("入力担当者", "入力担当者")
            DgvCymnhd.Columns.Add("備考", "備考")
            DgvCymnhd.Columns.Add("登録日", "登録日")
            DgvCymnhd.Columns.Add("更新日", "最終更新日")

        End If

        DgvCymnhd.Columns("売上金額_外貨").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("売上金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("粗利額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("粗利率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        DgvCymnhd.Columns("ＶＡＴ").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("売上金額_外貨_VAT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


        '数字形式
        DgvCymnhd.Columns("売上金額_外貨").DefaultCellStyle.Format = "N2"
        DgvCymnhd.Columns("売上金額").DefaultCellStyle.Format = "N2"
        DgvCymnhd.Columns("仕入金額").DefaultCellStyle.Format = "N2"
        DgvCymnhd.Columns("粗利額").DefaultCellStyle.Format = "N2"
        DgvCymnhd.Columns("粗利率").DefaultCellStyle.Format = "N1"

        DgvCymnhd.Columns("ＶＡＴ").DefaultCellStyle.Format = "N2"

        DgvCymnhd.Columns("売上日").DefaultCellStyle.Format = "d"
        DgvCymnhd.Columns("受注日").DefaultCellStyle.Format = "d"

        DgvCymnhd.Columns("売上金額_外貨_VAT").DefaultCellStyle.Format = "N2"

    End Sub

    '明細の見出しをセット
    Private Sub setListDt()
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            DgvCymnhd.Columns.Add("取消", "Cancel")
            DgvCymnhd.Columns.Add("売上番号", "SalesNumber")
            DgvCymnhd.Columns.Add("売上番号枝番", "SalesSubNumber")
            DgvCymnhd.Columns.Add("行番号", "LineNumber")
            DgvCymnhd.Columns.Add("受注番号", "OrderNumber")
            DgvCymnhd.Columns.Add("受注番号枝番", "JobOrderSubNumber")
            DgvCymnhd.Columns.Add("得意先名", "CustomerName")
            DgvCymnhd.Columns.Add("仕入区分", "PurchasingClassification")
            DgvCymnhd.Columns.Add("備考", "Division")
            DgvCymnhd.Columns.Add("メーカー", "Manufacturer")
            DgvCymnhd.Columns.Add("品名", "ItemName")
            DgvCymnhd.Columns.Add("型式", "Spec")
            DgvCymnhd.Columns.Add("仕入先名", "SupplierName")
            DgvCymnhd.Columns.Add("仕入値", "PurchaseAmount")
            DgvCymnhd.Columns.Add("受注数量", "JobOrderQuantity")
            DgvCymnhd.Columns.Add("売上数量", "SalesQuantity")
            DgvCymnhd.Columns.Add("受注残数", "OrderRemainingAmount")
            DgvCymnhd.Columns.Add("単位", "Unit")
            DgvCymnhd.Columns.Add("間接費", "Overhead")
            DgvCymnhd.Columns.Add("売単価", "SellingPrice")
            DgvCymnhd.Columns.Add("売上金額", "SalesAmount")
            DgvCymnhd.Columns.Add("粗利額", "GrossMargin")
            DgvCymnhd.Columns.Add("粗利率", "GrossMarginRate")
            DgvCymnhd.Columns.Add("リードタイム", "LeadTime")

            DgvCymnhd.Columns.Add("更新者", "ModifiedBy")
            DgvCymnhd.Columns.Add("更新日", "UpdateDate")

        Else

            DgvCymnhd.Columns.Add("取消", "取消")
            DgvCymnhd.Columns.Add("売上番号", "売上番号")
            DgvCymnhd.Columns.Add("売上番号枝番", "売上Ver")
            DgvCymnhd.Columns.Add("行番号", "行番号")
            DgvCymnhd.Columns.Add("受注番号", "受注番号")
            DgvCymnhd.Columns.Add("受注番号枝番", "受注Ver")

            DgvCymnhd.Columns.Add("得意先名", "得意先名")
            DgvCymnhd.Columns.Add("仕入区分", "仕入区分")
            DgvCymnhd.Columns.Add("備考", "部門")
            DgvCymnhd.Columns.Add("メーカー", "メーカー")
            DgvCymnhd.Columns.Add("品名", "品名")
            DgvCymnhd.Columns.Add("型式", "型式")
            DgvCymnhd.Columns.Add("仕入先名", "仕入先名")
            DgvCymnhd.Columns.Add("仕入値", "仕入値")
            DgvCymnhd.Columns.Add("受注数量", "受注数量")
            DgvCymnhd.Columns.Add("売上数量", "売上数量")
            DgvCymnhd.Columns.Add("受注残数", "受注残数")
            DgvCymnhd.Columns.Add("単位", "単位")
            DgvCymnhd.Columns.Add("間接費", "間接費")
            DgvCymnhd.Columns.Add("売単価", "売単価")
            DgvCymnhd.Columns.Add("売上金額", "売上金額")
            DgvCymnhd.Columns.Add("粗利額", "粗利額")
            DgvCymnhd.Columns.Add("粗利率", "粗利率")
            DgvCymnhd.Columns.Add("リードタイム", "リードタイム")

            DgvCymnhd.Columns.Add("更新者", "更新者")
            DgvCymnhd.Columns.Add("更新日", "更新日")

        End If

        DgvCymnhd.Columns("仕入値").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("受注数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("売上数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("受注残数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("間接費").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("売単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("売上金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("粗利額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("粗利率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        '数字形式
        DgvCymnhd.Columns("仕入値").DefaultCellStyle.Format = "N2"
        DgvCymnhd.Columns("受注数量").DefaultCellStyle.Format = "N2"
        DgvCymnhd.Columns("売上数量").DefaultCellStyle.Format = "N2"
        DgvCymnhd.Columns("受注残数").DefaultCellStyle.Format = "N2"
        DgvCymnhd.Columns("間接費").DefaultCellStyle.Format = "N2"
        DgvCymnhd.Columns("売単価").DefaultCellStyle.Format = "N2"
        DgvCymnhd.Columns("売上金額").DefaultCellStyle.Format = "N2"
        DgvCymnhd.Columns("粗利額").DefaultCellStyle.Format = "N2"
        DgvCymnhd.Columns("粗利率").DefaultCellStyle.Format = "N1"

        DgvCymnhd.Columns("受注番号").Visible = False
        DgvCymnhd.Columns("受注番号枝番").Visible = False

        DgvCymnhd.Columns("受注数量").Visible = False
        DgvCymnhd.Columns("受注残数").Visible = False

        'DgvCymnhd.Columns("売上日").DefaultCellStyle.Format = "d"
        'DgvCymnhd.Columns("受注日").DefaultCellStyle.Format = "d"

    End Sub

    Private Sub MstHanyoue_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'モードの設定
        If SalesStatus = CommonConst.STATUS_CANCEL Then

            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "CancelMode"
            Else
                LblMode.Text = "取消モード"
            End If

        ElseIf SalesStatus = CommonConst.STATUS_VIEW Then

            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "ViewMode"
            Else
                LblMode.Text = "参照モード"
            End If

        End If


        '英語表記
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblConditions.Text = "ExtractionCondition"
            Label1.Text = "CustomerName"
            Label2.Text = "Address"
            Label3.Text = "PhoneNumber"
            Label4.Text = "CustomerCode"
            Label8.Text = "SalesDate"
            Label7.Text = "SalesNumber"
            Label6.Text = "JobOrderNo"
            Label11.Text = "CustomerNumber"

            LblItemName.Text = "ItemName"
            LblSpec.Text = "Spec"

            Label10.Text = "DisplayFormat"
            LblConditions.Text = "ExtractionCondition"

            ChkCancelData.Text = "IncludeCancelData"
            ChkCancelData.Location = New Point(556, 203)

            BtnBill.Text = "BillingRegistration"
            BtnSearch.Text = "Search"
            BtnBack.Text = "Back"

            RbtnSlip.Text = "UnitOfVoucher"
            RbtnDetails.Text = "UnitOfDetailData"

        End If

        '検索（Date）の初期値
        dtDateSince.Value = DateAdd("d", CommonConst.SINCE_DEFAULT_DAY, DateTime.Today)
        dtDateUntil.Value = DateTime.Today

        getList() '一覧取得

    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    '表示形式の変更イベント
    Private Sub RbtnDetails_CheckedChanged(sender As Object, e As EventArgs)
        getList() '一覧取得
    End Sub

    '検索ボタン押下時
    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        getList() '一覧取得
    End Sub

    '取消ボタン押下時
    Private Sub BtnSalesCancel_Click(sender As Object, e As EventArgs)

        '実行できるデータがあるかチェック
        If actionChk() = False Then
            Return
        End If

        '取消済みデータは取消操作不可能
        If DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("取消").Value =
            IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_JPN, CommonConst.CANCEL_KBN_JPN_TXT, CommonConst.CANCEL_KBN_ENG_TXT) Then
            '取消データは選択できないアラートを出す
            _msgHd.dspMSG("cannotSelectTorikeshiData", frmC01F10_Login.loginValue.Language)
            Return
        End If

        Try

            '取消確認のアラート
            Dim result As DialogResult = _msgHd.dspMSG("confirmCancel", frmC01F10_Login.loginValue.Language)

            If result = DialogResult.Yes Then
                updateData() 'データ更新
            End If

        Catch ex As Exception

            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))

        End Try

    End Sub

    '取消実行
    Private Sub updateData()

        Dim dtNow As DateTime = DateTime.Now
        Dim strNow As String = UtilClass.formatDatetime(dtNow)
        Dim reccnt As Integer = 0
        Dim Sql As String = ""

        '画面を開いた時から対象データに対して更新がされていないかどうか確認
        Sql = " AND "
        Sql += " 売上番号 ILIKE '" & DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("売上番号").Value & "'"
        Sql += " AND "
        Sql += " 売上番号枝番 ILIKE '" & DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("売上番号枝番").Value & "'"
        Sql += " AND "
        Sql += " 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString

        ds = _com.getDsData("t30_urighd", Sql).Tables(0)

        'データが更新されていなかった場合
        If ds.Rows(0)("更新日") = DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("更新日").Value Then

            Sql = "SELECT"
            Sql += " t11.*, t10.取消区分"
            Sql += " FROM "
            Sql += " public.t11_cymndt t11 "

            Sql += " INNER JOIN "
            Sql += " t10_cymnhd t10"
            Sql += " ON "

            Sql += " t11.会社コード = t10.会社コード"
            Sql += " AND "
            Sql += " t11.受注番号 = t10.受注番号"
            Sql += " AND "
            Sql += " t11.受注番号枝番 = t10.受注番号枝番"

            Sql += " WHERE "
            Sql += " t11.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND "
            Sql += " t11.受注番号 ILIKE '" & DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("受注番号").Value & "'"
            Sql += " AND "
            Sql += " t11.受注番号枝番 ILIKE '" & DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("受注番号枝番").Value & "'"
            Sql += " AND "
            Sql += " t10.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString

            Dim dsCyminDt As DataTable = _db.selectDB(Sql, RS, reccnt).Tables(0)


            Sql = "SELECT"
            Sql += " t31.*, t30.取消区分"
            Sql += " FROM "
            Sql += " public.t31_urigdt t31 "

            Sql += " INNER JOIN "
            Sql += " t30_urighd t30"
            Sql += " ON "

            Sql += " t31.会社コード = t30.会社コード"
            Sql += " AND "
            Sql += " t31.売上番号 = t30.売上番号"
            Sql += " AND "
            Sql += " t31.売上番号枝番 = t30.売上番号枝番"

            Sql += " WHERE "
            Sql += " t31.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND "
            Sql += " t31.売上番号 ILIKE '" & DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("売上番号").Value & "'"
            Sql += " AND "
            Sql += " t31.売上番号枝番 ILIKE '" & DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("売上番号枝番").Value & "'"
            Sql += " AND "
            Sql += " t30.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString

            Dim dsUrigDt As DataTable = _db.selectDB(Sql, RS, reccnt).Tables(0)

            '取消す売上データをループさせながら、受注データから減算していく
            For i As Integer = 0 To dsUrigDt.Rows.Count() - 1

                For x As Integer = 0 To dsCyminDt.Rows.Count() - 1

                    '行番号が一致したら
                    If dsUrigDt.Rows(i)("行番号") = dsCyminDt.Rows(x)("行番号") Then

                        Try
                            '受注明細から取消す売上データ明細の数を減算
                            Sql = "UPDATE "
                            Sql += "Public."
                            Sql += "t11_cymndt "
                            Sql += "SET "
                            Sql += "売上数量"
                            Sql += " = '"
                            Sql += UtilClass.formatNumber(dsCyminDt.Rows(x)("売上数量") - dsUrigDt.Rows(i)("売上数量"))
                            Sql += "', "
                            Sql += " 受注残数"
                            Sql += " = '"
                            Sql += UtilClass.formatNumber(dsCyminDt.Rows(x)("受注残数") + dsUrigDt.Rows(i)("売上数量"))
                            Sql += "', "
                            Sql += "更新者"
                            Sql += " = '"
                            Sql += frmC01F10_Login.loginValue.TantoNM
                            Sql += "' "
                            Sql += "WHERE"
                            Sql += " 会社コード"
                            Sql += "='"
                            Sql += frmC01F10_Login.loginValue.BumonCD
                            Sql += "'"
                            Sql += " AND"
                            Sql += " 受注番号"
                            Sql += "='"
                            Sql += dsCyminDt.Rows(x)("受注番号")
                            Sql += "'"
                            Sql += " AND"
                            Sql += " 受注番号枝番"
                            Sql += "='"
                            Sql += dsCyminDt.Rows(x)("受注番号枝番")
                            Sql += "'"
                            Sql += " AND"
                            Sql += " 行番号"
                            Sql += "='"
                            Sql += dsCyminDt.Rows(x)("行番号").ToString
                            Sql += "' "

                            _db.executeDB(Sql)

                        Catch ue As UsrDefException
                            ue.dspMsg()
                            Throw ue
                        Catch ex As Exception
                            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
                            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
                        End Try

                    End If

                Next

            Next

            Try
                '受注基本データを更新
                Sql = "UPDATE "
                Sql += "Public."
                Sql += "t10_cymnhd "
                Sql += "SET "
                Sql += "更新日"
                Sql += " = '"
                Sql += strNow
                Sql += "', "
                Sql += "更新者"
                Sql += " = '"
                Sql += frmC01F10_Login.loginValue.TantoNM
                Sql += "' "
                Sql += "WHERE"
                Sql += " 会社コード"
                Sql += "='"
                Sql += frmC01F10_Login.loginValue.BumonCD
                Sql += "'"
                Sql += " AND"
                Sql += " 受注番号"
                Sql += "='"
                Sql += DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("受注番号").Value
                Sql += "'"
                Sql += " AND"
                Sql += " 受注番号枝番"
                Sql += "='"
                Sql += DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("受注番号枝番").Value
                Sql += "'"

                _db.executeDB(Sql)

                Sql = "UPDATE "
                Sql += "Public."
                Sql += "t30_urighd "
                Sql += "SET "

                Sql += "取消区分 = " & CommonConst.CANCEL_KBN_DISABLED.ToString
                Sql += ", "
                Sql += "取消日"
                Sql += " = '"
                Sql += strNow
                Sql += "', "
                Sql += "更新日"
                Sql += " = '"
                Sql += strNow
                Sql += "', "
                Sql += "更新者"
                Sql += " = '"
                Sql += frmC01F10_Login.loginValue.TantoNM
                Sql += " ' "

                Sql += "WHERE"
                Sql += " 会社コード"
                Sql += "='"
                Sql += frmC01F10_Login.loginValue.BumonCD
                Sql += "'"
                Sql += " AND"
                Sql += " 売上番号"
                Sql += "='"
                Sql += DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("売上番号").Value
                Sql += "' "
                Sql += " AND"
                Sql += " 売上番号枝番"
                Sql += "='"
                Sql += DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("売上番号枝番").Value
                Sql += "' "

                _db.executeDB(Sql)

            Catch ue As UsrDefException
                ue.dspMsg()
                Throw ue
            Catch ex As Exception
                'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
            End Try

        Else
            '画面を開いたときの日時とデータの日時が異なっていた場合
            'データが誰かに変更された旨を伝える
            _msgHd.dspMSG("chkData", frmC01F10_Login.loginValue.Language)

        End If

        getList() '一覧再表示

    End Sub


    '取消済みデータチェックイベント
    Private Sub ChkCancelData_CheckedChanged(sender As Object, e As EventArgs)
        getList() '一覧再表示
    End Sub

    '参照ボタン押下時
    Private Sub BtnSalesView_Click(sender As Object, e As EventArgs)

        If DgvCymnhd.Rows.Count = 0 Then
            Exit Sub
        End If

        Dim RowIdx As Integer
        RowIdx = Me.DgvCymnhd.CurrentCell.RowIndex
        Dim No As String = DgvCymnhd.Rows(RowIdx).Cells("受注番号").Value
        Dim Suffix As String = DgvCymnhd.Rows(RowIdx).Cells("受注番号枝番").Value
        Dim Status As String = CommonConst.STATUS_VIEW

        Dim openForm As Form = Nothing
        openForm = New OrderManagement(_msgHd, _db, _langHd, Me, No, Suffix, Status)   '処理選択
        openForm.Show(Me)
    End Sub

    '抽出条件取得
    Private Function searchConditions2() As String
        Dim Sql As String = ""

        '抽出条件
        Dim customerName As String = UtilClass.escapeSql(TxtCustomerName.Text)
        Dim customerCode As String = UtilClass.escapeSql(TxtCustomerCode.Text)
        Dim customerAddress As String = UtilClass.escapeSql(TxtAddress.Text)
        Dim customerTel As String = UtilClass.escapeSql(TxtTel.Text)
        Dim sinceDate As String = UtilClass.strFormatDate(dtDateSince.Text)
        Dim untilDate As String = UtilClass.strFormatDate(dtDateUntil.Text)
        Dim sinceNum As String = UtilClass.escapeSql(TxtSalesSince.Text)
        Dim salesName As String = UtilClass.escapeSql(TxtSales.Text)
        Dim poNum As String = UtilClass.escapeSql(TxtCustomerPO.Text)

        If customerName <> Nothing Then
            Sql += " AND "
            Sql += " 得意先名 ILIKE '%" & customerName & "%' "
        End If

        If customerAddress <> Nothing Then
            Sql += " AND "
            Sql += " 得意先住所 ILIKE '%" & customerAddress & "%' "
        End If

        If customerTel <> Nothing Then
            Sql += " AND "
            Sql += " 得意先電話番号 ILIKE '%" & customerTel & "%' "
        End If

        If customerCode <> Nothing Then
            Sql += " AND "
            Sql += " 得意先コード ILIKE '%" & customerCode & "%' "
        End If

        If sinceDate <> Nothing Then
            Sql += " AND "
            Sql += " 売上日 >= '" & sinceDate & "'"
        End If
        If untilDate <> Nothing Then
            Sql += " AND "
            Sql += " 売上日 <= '" & untilDate & "'"
        End If

        If sinceNum <> Nothing Then
            Sql += " AND "
            Sql += " 売上番号 ILIKE '%" & sinceNum & "%' "
        End If

        If salesName <> Nothing Then
            Sql += " AND "
            Sql += " 営業担当者 ILIKE '%" & salesName & "%' "
        End If

        If poNum <> Nothing Then
            Sql += " AND "
            Sql += " 客先番号 ILIKE '%" & poNum & "%' "
        End If

        Return Sql

    End Function

    '表示形式条件
    Private Function viewFormat() As String
        Dim Sql As String = ""

        '取消データを含めない場合
        If ChkCancelData.Checked = False Then
            Sql += " AND "
            Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
        End If

        Return Sql

    End Function

    Private Function actionChk() As Boolean
        '対象データがない場合は取消操作不可能
        If DgvCymnhd.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)

            Return False

        Else

            Return True

        End If
    End Function

    Private Sub BtnBill_Click(sender As Object, e As EventArgs) Handles BtnBill.Click

        'グリッドに何もないときは次画面へ移動しない
        If Me.DgvCymnhd.RowCount = 0 Then
            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Exit Sub
        End If

        Dim dicNo As New Dictionary(Of String, String)

        For RowIdx As Integer = 0 To DgvCymnhd.RowCount - 1

            If DgvCymnhd.Rows(RowIdx).Cells("B").Value Then

                '取消済みデータは取消操作不可能
                If DgvCymnhd.Rows(RowIdx).Cells("取消").Value =
                IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_JPN, CommonConst.CANCEL_KBN_JPN_TXT, CommonConst.CANCEL_KBN_ENG_TXT) Then
                    '取消データは選択できないアラートを出す
                    _msgHd.dspMSG("cannotSelectTorikeshiData", frmC01F10_Login.loginValue.Language)
                    Exit Sub
                End If

                dicNo.Add(DgvCymnhd.Rows(RowIdx).Cells("売上番号").Value, DgvCymnhd.Rows(RowIdx).Cells("売上番号枝番").Value)

            End If

        Next

        Dim openForm As Form = Nothing
        openForm = New BillingManagement(_msgHd, _db, _langHd, Me, "", "", "", "", dicNo)   '処理選択
        Me.Enabled = False
        Me.Hide()
        openForm.ShowDialog(Me)

        getList() '一覧表示

    End Sub

    '明細単位
    Private Sub RbtnDetails_CheckedChanged_1(sender As Object, e As EventArgs) Handles RbtnDetails.CheckedChanged
        Call getList()
    End Sub

    Private Sub ChkDisplayBilled_CheckedChanged(sender As Object, e As EventArgs) Handles ChkDisplayBilled.CheckedChanged
        Call getList()
    End Sub
End Class