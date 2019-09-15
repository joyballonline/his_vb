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

Public Class OrderProgress
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
    Private _parentForm As Form
    'Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ
    Private CompanyCode As String = ""
    Private OrderNo As String()
    Private OrderStatus As String = ""


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
                   Optional ByRef prmRefStatus As String = "")
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _parentForm = prmRefForm
        OrderStatus = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True
        DgvCymnhd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.DisplayedCells

        '受注参照以外では隠す
        LblItemName.Visible = False
        TxtItemName.Visible = False
        LblSpec.Visible = False
        TxtSpec.Visible = False

    End Sub

    Private Sub MstHanyoue_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'モード
        If mSet_Mode = False Then
            Exit Sub
        End If


        '検索（Date）の初期値
        dtOrderDateSince.Value = DateAdd("d", CommonConst.SINCE_DEFAULT_DAY, DateTime.Today)
        dtOrderDateUntil.Value = DateTime.Today

        OrderListLoad() '一覧表示


        '英語対応
        If mSet_Language() = False Then
            Exit Sub
        End If


    End Sub

    'モード
    Private Function mSet_Mode() As Boolean

        If OrderStatus = CommonConst.STATUS_SALES Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "SalesInputMode"
            Else
                LblMode.Text = "売上入力モード"
            End If


        ElseIf OrderStatus = CommonConst.STATUS_GOODS_ISSUE Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "GoodsDeliveryInputMode"
            Else
                LblMode.Text = "出庫入力モード"
            End If

        ElseIf OrderStatus = CommonConst.STATUS_EDIT Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "EditMode"
            Else
                LblMode.Text = "編集モード"
            End If

        ElseIf OrderStatus = CommonConst.STATUS_VIEW Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "ViewMode"
            Else
                LblMode.Text = "参照モード"
            End If

            'BtnOrderView.Visible = True
            'BtnOrderView.Location = New Point(997, 509)

            LblItemName.Visible = True
            TxtItemName.Visible = True
            LblSpec.Visible = True
            TxtSpec.Visible = True

        ElseIf OrderStatus = CommonConst.STATUS_CANCEL Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "CancelMode"
            Else
                LblMode.Text = "取消モード"
            End If

        ElseIf OrderStatus = CommonConst.STATUS_CLONE Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "NewCopyMode"
            Else
                LblMode.Text = "新規複写モード"
            End If

        ElseIf OrderStatus = CommonConst.STATUS_BILL Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "BillingRegistrationMode"
            Else
                LblMode.Text = "請求登録モード"
            End If

        End If

        mSet_Mode = True

    End Function


    '英語対応
    Private Function mSet_Language() As Boolean

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblConditions.Text = "ExtractionCondition"
            Label1.Text = "CustomerName"
            Label2.Text = "Address"
            Label3.Text = "PhoneNumber"
            Label4.Text = "CustomerCode"
            Label8.Text = "OrderDate"
            Label7.Text = "OrdernNumber"
            Label6.Text = "SalesPersonInCharge"
            Label11.Text = "CustomerNumber"
            Label10.Text = "DisplayFormat"
            RbtnSlip.Text = "UnitOfVoucher"
            lblMaker.Text = "Maker"

            RbtnDetails.Text = "LineItemUnit"
            RbtnDetails.Location = New Point(166, 202)

            ChkCancelData.Text = "IncludeCancelData"
            ChkCancelData.Location = New Point(556, 203)

            BtnOrderSearch.Text = "Search"
            BtnOrderView.Text = "OrderView"
            BtnBack.Text = "Back"


            '受注参照時のみ表示される
            LblItemName.Text = "ItemName"
            LblSpec.Text = "Spec"
        End If

        mSet_Language = True

    End Function


    '受注一覧を表示
    Private Sub OrderListLoad(Optional ByRef prmRefStatus As String = "")
        '一覧をクリア
        DgvCymnhd.Rows.Clear()
        'DgvCymnhd.Columns.Clear()

        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        Try

            Sql = " SELECT t10.*"
            Sql += " ,t11.行番号 ,t11.メーカー ,t11.品名 ,t11.型式"
            Sql += " ,t11.売上数量 ,t11.受注残数"

            Sql += " from t10_cymnhd as t10"

            Sql += " left join t11_cymndt as t11"
            Sql += " on t10.受注番号 = t11.受注番号 and t10.受注番号枝番 = t11.受注番号枝番"

            'Sql += " left join t31_urigdt as t31"
            'Sql += "   on t10.受注番号 = t31.受注番号 and t10.受注番号枝番 = t31.受注番号枝番"
            'Sql += "  and t11.メーカー = t31.メーカー and t11.品名 = t31.品名 and t11.型式 = t31.型式"

            'Sql += " left join t45_shukodt as t45"
            'Sql += " on t10.受注番号 = t45.受注番号 and t10.受注番号枝番 = t45.受注番号枝番"




            Sql += " WHERE t10.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

            '履歴最新
            Sql += "   and t10.受注番号枝番 = (SELECT MAX(t10M.受注番号枝番) FROM t10_cymnhd as t10M where t10.受注番号 = t10M.受注番号) "

            'Sql += viewSearchConditions() '検索条件 todo

            Sql += " ORDER BY "
            Sql += " t10.受注日, t10.受注番号, t10.受注番号枝番, t11.行番号"

            ds = _db.selectDB(Sql, RS, reccnt)


            For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1

                DgvCymnhd.Rows.Add()
                DgvCymnhd.Rows(i).Cells("受注日").Value = ds.Tables(RS).Rows(i)("受注日")
                DgvCymnhd.Rows(i).Cells("受注番号").Value = ds.Tables(RS).Rows(i)("受注番号")
                DgvCymnhd.Rows(i).Cells("受注番号枝番").Value = ds.Tables(RS).Rows(i)("受注番号枝番")

                DgvCymnhd.Rows(i).Cells("行番号").Value = ds.Tables(RS).Rows(i)("行番号")
                DgvCymnhd.Rows(i).Cells("メーカー").Value = ds.Tables(RS).Rows(i)("メーカー")
                DgvCymnhd.Rows(i).Cells("品名").Value = ds.Tables(RS).Rows(i)("品名")
                DgvCymnhd.Rows(i).Cells("型式").Value = ds.Tables(RS).Rows(i)("型式")

                DgvCymnhd.Rows(i).Cells("見積番号").Value = ds.Tables(RS).Rows(i)("見積番号")
                DgvCymnhd.Rows(i).Cells("見積日").Value = ds.Tables(RS).Rows(i)("見積日")

                If IsDBNull(ds.Tables(RS).Rows(i)("受注残数")) Then
                    '売上がない
                    DgvCymnhd.Rows(i).Cells("売上登録").Value = ""
                ElseIf ds.Tables(RS).Rows(i)("受注残数") = 0 Then
                    '売上済
                    DgvCymnhd.Rows(i).Cells("売上登録").Value = "〇"
                ElseIf ds.Tables(RS).Rows(i)("売上数量") > 0 AndAlso ds.Tables(RS).Rows(i)("受注残数") > 0 Then
                    '一部売上
                    DgvCymnhd.Rows(i).Cells("売上登録").Value = "△"
                End If


            Next

            DgvCymnhd.Columns("受注日").DefaultCellStyle.Format = "d"
            DgvCymnhd.Columns("見積日").DefaultCellStyle.Format = "d"

            DgvCymnhd.Columns("売上登録").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


            ds = Nothing


        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '検索内容をListにセット（行）
    Private Sub setRows(ByVal prmDataset As DataSet)
        Dim ds As DataSet = prmDataset


        '伝票単位の場合
        '------------------------------
        If RbtnSlip.Checked Then

            Dim curds As DataSet  'm25_currency
            Dim cur As String
            Dim Sql As String

            setHdColumns() '表示カラムの設定

            For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1

                If IsDBNull(ds.Tables(RS).Rows(i)("通貨")) Then
                    cur = vbNullString
                Else
                    Sql = " and 採番キー = " & ds.Tables(RS).Rows(i)("通貨")
                    curds = getDsData("m25_currency", Sql)

                    cur = curds.Tables(RS).Rows(0)("通貨コード")
                End If

                DgvCymnhd.Rows.Add()
                DgvCymnhd.Rows(i).Cells("取消").Value = getDelKbnTxt(ds.Tables(RS).Rows(i)("取消区分"))
                DgvCymnhd.Rows(i).Cells("受注番号").Value = ds.Tables(RS).Rows(i)("受注番号")
                DgvCymnhd.Rows(i).Cells("受注番号枝番").Value = ds.Tables(RS).Rows(i)("受注番号枝番")

                ''出庫登録時のみ出力
                'If OrderStatus = CommonConst.STATUS_GOODS_ISSUE Then
                '    DgvCymnhd.Rows(i).Cells("発注番号").Value = ds.Tables(RS).Rows(i)("発注番号")
                '    DgvCymnhd.Rows(i).Cells("発注番号枝番").Value = ds.Tables(RS).Rows(i)("発注番号枝番")
                'End If

                DgvCymnhd.Rows(i).Cells("客先番号").Value = ds.Tables(RS).Rows(i)("客先番号")
                DgvCymnhd.Rows(i).Cells("受注日").Value = ds.Tables(RS).Rows(i)("受注日").ToShortDateString()
                DgvCymnhd.Rows(i).Cells("見積番号").Value = ds.Tables(RS).Rows(i)("見積番号")
                DgvCymnhd.Rows(i).Cells("見積番号枝番").Value = ds.Tables(RS).Rows(i)("見積番号枝番")
                DgvCymnhd.Rows(i).Cells("見積日").Value = ds.Tables(RS).Rows(i)("見積日").ToShortDateString()
                DgvCymnhd.Rows(i).Cells("見積有効期限").Value = ds.Tables(RS).Rows(i)("見積有効期限").ToShortDateString()
                DgvCymnhd.Rows(i).Cells("得意先コード").Value = ds.Tables(RS).Rows(i)("得意先コード")
                DgvCymnhd.Rows(i).Cells("得意先名").Value = ds.Tables(RS).Rows(i)("得意先名")


                DgvCymnhd.Rows(i).Cells("通貨_外貨").Value = cur
                DgvCymnhd.Rows(i).Cells("受注金額_外貨").Value = ds.Tables(RS).Rows(i)("見積金額_外貨")

                DgvCymnhd.Rows(i).Cells("受注金額").Value = ds.Tables(RS).Rows(i)("見積金額")
                DgvCymnhd.Rows(i).Cells("ＶＡＴ").Value = ds.Tables(RS).Rows(i)("見積金額") * ds.Tables(RS).Rows(i)("ＶＡＴ") / 100

                'Dim decPurchase1 As Decimal = 0
                'Dim decPurchase2 As Decimal = 0
                'Dim decPurchaseAmount1 As Decimal = 0
                'Dim decPurchaseAmount2 As Decimal = 0

                'Call mPurchaseCost(ds.Tables(RS).Rows(i)("発注番号"), ds.Tables(RS).Rows(i)("発注番号枝番") _
                '                       , decPurchase1, decPurchase2 _
                '                       , decPurchaseAmount1, decPurchaseAmount2)

                DgvCymnhd.Rows(i).Cells("仕入金額").Value = ds.Tables(RS).Rows(i)("仕入金額")
                DgvCymnhd.Rows(i).Cells("粗利額").Value = ds.Tables(RS).Rows(i)("見積金額") - ds.Tables(RS).Rows(i)("仕入金額")

                If ds.Tables(RS).Rows(i)("見積金額") = 0 Or DgvCymnhd.Rows(i).Cells("粗利額").Value = 0 Then
                    DgvCymnhd.Rows(i).Cells("粗利率").Value = 0
                Else
                    DgvCymnhd.Rows(i).Cells("粗利率").Value = DgvCymnhd.Rows(i).Cells("粗利額").Value / ds.Tables(RS).Rows(i)("見積金額") * 100
                End If


                DgvCymnhd.Rows(i).Cells("得意先郵便番号").Value = ds.Tables(RS).Rows(i)("得意先郵便番号")
                DgvCymnhd.Rows(i).Cells("得意先住所").Value = ds.Tables(RS).Rows(i)("得意先住所")
                DgvCymnhd.Rows(i).Cells("得意先電話番号").Value = ds.Tables(RS).Rows(i)("得意先電話番号")
                DgvCymnhd.Rows(i).Cells("得意先ＦＡＸ").Value = ds.Tables(RS).Rows(i)("得意先ＦＡＸ")
                DgvCymnhd.Rows(i).Cells("得意先担当者名").Value = ds.Tables(RS).Rows(i)("得意先担当者名")
                DgvCymnhd.Rows(i).Cells("得意先担当者役職").Value = ds.Tables(RS).Rows(i)("得意先担当者役職")

                DgvCymnhd.Rows(i).Cells("支払条件").Value = ds.Tables(RS).Rows(i)("支払条件")
                DgvCymnhd.Rows(i).Cells("営業担当者").Value = ds.Tables(RS).Rows(i)("営業担当者")
                DgvCymnhd.Rows(i).Cells("入力担当者").Value = ds.Tables(RS).Rows(i)("入力担当者")
                DgvCymnhd.Rows(i).Cells("備考").Value = ds.Tables(RS).Rows(i)("備考")
                DgvCymnhd.Rows(i).Cells("登録日").Value = ds.Tables(RS).Rows(i)("登録日")
                DgvCymnhd.Rows(i).Cells("更新者").Value = ds.Tables(RS).Rows(i)("更新者")
                DgvCymnhd.Rows(i).Cells("更新日").Value = ds.Tables(RS).Rows(i)("更新日")

            Next

        Else

            '明細単位の場合
            '------------------------------
            Dim Sql As String = ""
            Dim tmp1 As String = ""

            setDtColumns() '表示カラムの設定

            For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1

                DgvCymnhd.Rows.Add()
                DgvCymnhd.Rows(i).Cells("取消").Value = getDelKbnTxt(ds.Tables(RS).Rows(i)("取消区分"))
                DgvCymnhd.Rows(i).Cells("受注番号").Value = ds.Tables(RS).Rows(i)("受注番号")
                DgvCymnhd.Rows(i).Cells("受注番号枝番").Value = ds.Tables(RS).Rows(i)("受注番号枝番")
                DgvCymnhd.Rows(i).Cells("行番号").Value = ds.Tables(RS).Rows(i)("行番号")

                ''出庫登録時のみ出力
                'If OrderStatus = CommonConst.STATUS_GOODS_ISSUE Then
                '    DgvCymnhd.Rows(i).Cells("発注番号").Value = ds.Tables(RS).Rows(i)("発注番号")
                '    DgvCymnhd.Rows(i).Cells("発注番号枝番").Value = ds.Tables(RS).Rows(i)("発注番号枝番")
                'End If

                '汎用マスタから仕入区分を取得
                Dim dsSireKbn As DataSet = getDsHanyoData(CommonConst.FIXED_KEY_PURCHASING_CLASS, ds.Tables(RS).Rows(i)("仕入区分").ToString)
                DgvCymnhd.Rows(i).Cells("仕入区分").Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                                                        dsSireKbn.Tables(RS).Rows(0)("文字２"),
                                                        dsSireKbn.Tables(RS).Rows(0)("文字１"))

                DgvCymnhd.Rows(i).Cells("メーカー").Value = ds.Tables(RS).Rows(i)("メーカー")
                DgvCymnhd.Rows(i).Cells("品名").Value = ds.Tables(RS).Rows(i)("品名")
                DgvCymnhd.Rows(i).Cells("型式").Value = ds.Tables(RS).Rows(i)("型式")
                DgvCymnhd.Rows(i).Cells("仕入先名").Value = ds.Tables(RS).Rows(i)("仕入先名")
                DgvCymnhd.Rows(i).Cells("仕入値").Value = ds.Tables(RS).Rows(i)("仕入値")
                DgvCymnhd.Rows(i).Cells("受注数量").Value = ds.Tables(RS).Rows(i)("受注数量")
                DgvCymnhd.Rows(i).Cells("売上数量").Value = ds.Tables(RS).Rows(i)("売上数量")
                DgvCymnhd.Rows(i).Cells("受注残数").Value = ds.Tables(RS).Rows(i)("受注残数")
                DgvCymnhd.Rows(i).Cells("単位").Value = ds.Tables(RS).Rows(i)("単位")
                DgvCymnhd.Rows(i).Cells("間接費").Value = ds.Tables(RS).Rows(i)("間接費")
                DgvCymnhd.Rows(i).Cells("売単価").Value = ds.Tables(RS).Rows(i)("売単価")
                DgvCymnhd.Rows(i).Cells("売上金額").Value = ds.Tables(RS).Rows(i)("売上金額")
                DgvCymnhd.Rows(i).Cells("粗利額").Value = ds.Tables(RS).Rows(i)("粗利額")
                DgvCymnhd.Rows(i).Cells("粗利率").Value = ds.Tables(RS).Rows(i)("粗利率")

                If ds.Tables(RS).Rows(i)("リードタイム単位") Is DBNull.Value Then

                    'リードタイムが空だったらそのまま
                    DgvCymnhd.Rows(i).Cells("リードタイム").Value = ds.Tables(RS).Rows(i)("リードタイム")

                Else

                    'リードタイムが入っていたら汎用マスタから単位を取得して連結する
                    Sql = " AND 固定キー = '" & CommonConst.FIXED_KEY_READTIME & "'"
                    Sql += " AND 可変キー = '" & ds.Tables(RS).Rows(i)("リードタイム単位").ToString & "'"

                    Dim dsHanyo As DataSet = getDsData("m90_hanyo", Sql)

                    tmp1 = ""
                    tmp1 += ds.Tables(RS).Rows(i)("リードタイム")
                    tmp1 += dsHanyo.Tables(RS).Rows(0)("文字１")
                    DgvCymnhd.Rows(i).Cells("リードタイム").Value = tmp1
                End If

                DgvCymnhd.Rows(i).Cells("出庫数").Value = ds.Tables(RS).Rows(i)("出庫数")
                DgvCymnhd.Rows(i).Cells("未出庫数").Value = ds.Tables(RS).Rows(i)("未出庫数")
                DgvCymnhd.Rows(i).Cells("備考").Value = ds.Tables(RS).Rows(i)("備考")
                DgvCymnhd.Rows(i).Cells("更新者").Value = ds.Tables(RS).Rows(i)("更新者")
                DgvCymnhd.Rows(i).Cells("登録日").Value = ds.Tables(RS).Rows(i)("登録日")
            Next

        End If

    End Sub

    Private Sub mPurchaseCost(ByVal OrderNo As String, ByVal BranchNo As String _
                                   , ByRef decPurchase1 As Decimal, ByRef decPurchase2 As Decimal _
                                   , ByRef decPurchaseAmount1 As Decimal, ByRef decPurchaseAmount2 As Decimal)


        Dim reccnt As Integer = 0 'DB用（デフォルト
        Dim Sql As String
        Dim ds_t21 As DataSet

        Sql = "SELECT"
        Sql += " 仕入値,仕入値_外貨,発注数量,仕入金額,間接費,仕入金額_外貨"

        Sql += " FROM "
        Sql += " public.t21_hattyu"

        Sql += " WHERE "
        Sql += " 会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"

        Sql += " and 発注番号 = '" & OrderNo & "'"
        Sql += " and 発注番号枝番 = '" & BranchNo & "'"

        ds_t21 = _db.selectDB(Sql, RS, reccnt)

        decPurchase1 = 0  '外貨
        decPurchase2 = 0
        decPurchaseAmount1 = 0
        decPurchaseAmount2 = 0

        'For i As Integer = 0 To ds_t21.Tables(RS).Rows.Count - 1

        '    decPurchase1 += rmNullDecimal(ds_t21.Tables(RS).Rows(i)("仕入値_外貨") * ds_t21.Tables(RS).Rows(i)("発注数量"))
        '    decPurchase2 += rmNullDecimal(ds_t21.Tables(RS).Rows(i)("仕入値") * ds_t21.Tables(RS).Rows(i)("発注数量"))

        '    decPurchaseAmount1 += rmNullDecimal(ds_t21.Tables(RS).Rows(i)("仕入金額_外貨"))
        '    decPurchaseAmount2 += rmNullDecimal(ds_t21.Tables(RS).Rows(i)("仕入金額") + ds_t21.Tables(RS).Rows(i)("間接費"))

        'Next

    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    '明細単位切替時で表示形式のイベントを取得
    Private Sub RbtnDetails_CheckedChanged(sender As Object, e As EventArgs) Handles RbtnDetails.CheckedChanged

        OrderListLoad() '一覧を再表示

    End Sub

    '検索実行
    Private Sub BtnQuoteSearch_Click(sender As Object, e As EventArgs) Handles BtnOrderSearch.Click

        OrderListLoad() '一覧を再表示

    End Sub


    '受注参照
    Private Sub BtnOrderView_Click(sender As Object, e As EventArgs) Handles BtnOrderView.Click
        'グリッドに何もないときは次画面へ移動しない
        If Me.DgvCymnhd.RowCount = 0 Then
            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Exit Sub
        End If

        Dim RowIdx As Integer
        RowIdx = Me.DgvCymnhd.CurrentCell.RowIndex
        Dim No As String = DgvCymnhd.Rows(RowIdx).Cells("受注番号").Value
        Dim Suffix As String = DgvCymnhd.Rows(RowIdx).Cells("受注番号枝番").Value
        Dim Status As String = CommonConst.STATUS_VIEW

        Dim openForm As Form = Nothing
        openForm = New Order(_msgHd, _db, _langHd, Me, No, Suffix, Status)   '処理選択
        openForm.Show(Me)
    End Sub


    '取消データを含めるイベントの取得
    Private Sub ChkCancelData_CheckedChanged(sender As Object, e As EventArgs) Handles ChkCancelData.CheckedChanged

        OrderListLoad() '一覧を再表示

    End Sub


    '使用言語に合わせて受注基本見出しを切替
    Private Sub setHdColumns()
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then

            DgvCymnhd.Columns.Add("取消", "Cancel")
            DgvCymnhd.Columns.Add("受注番号", "OrderNumber")
            DgvCymnhd.Columns.Add("受注番号枝番", "JobOrderSubNumber")

            '出庫登録時
            If OrderStatus = CommonConst.STATUS_GOODS_ISSUE Then
                DgvCymnhd.Columns.Add("発注番号", "PurchaseNumber")
                DgvCymnhd.Columns.Add("発注番号枝番", "PurchaseOrderVer")
            End If

            DgvCymnhd.Columns.Add("客先番号", "CustomerNumber")
            DgvCymnhd.Columns.Add("受注日", "JobOrderDate")
            DgvCymnhd.Columns.Add("見積番号", "QuotationNumber")
            DgvCymnhd.Columns.Add("見積番号枝番", "BranchNumber")
            DgvCymnhd.Columns.Add("見積日", "QuotationDate")
            DgvCymnhd.Columns.Add("見積有効期限", "QuotationExpriedDate")
            DgvCymnhd.Columns.Add("得意先コード", "CustomerCode")
            DgvCymnhd.Columns.Add("得意先名", "CustomerName")

            DgvCymnhd.Columns.Add("通貨_外貨", "Currency")
            DgvCymnhd.Columns.Add("受注金額_外貨", "OrderAmount" & vbCrLf & "(OrignalCurrency)")

            DgvCymnhd.Columns.Add("受注金額", "OrderAmount" & vbCrLf & "(" & setBaseCurrency() & ")")
            DgvCymnhd.Columns.Add("ＶＡＴ", "VAT-OUT")

            DgvCymnhd.Columns.Add("仕入金額", "PurchaseAmount" & vbCrLf & "(" & setBaseCurrency() & ")")
            DgvCymnhd.Columns.Add("粗利額", "Profitmargin" & vbCrLf & "(" & setBaseCurrency() & ")")
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
            DgvCymnhd.Columns.Add("更新者", "ModifiedBy")

        Else

            DgvCymnhd.Columns.Add("取消", "取消")
            DgvCymnhd.Columns.Add("受注番号", "受注番号")
            DgvCymnhd.Columns.Add("受注番号枝番", "受注Ver")

            '出庫登録時
            If OrderStatus = CommonConst.STATUS_GOODS_ISSUE Then
                DgvCymnhd.Columns.Add("発注番号", "発注番号")
                DgvCymnhd.Columns.Add("発注番号枝番", "発注番号枝番")
            End If

            DgvCymnhd.Columns.Add("客先番号", "客先番号")
            DgvCymnhd.Columns.Add("受注日", "受注日")
            DgvCymnhd.Columns.Add("見積番号", "見積番号")
            DgvCymnhd.Columns.Add("見積番号枝番", "見積番号枝番")
            DgvCymnhd.Columns.Add("見積日", "見積日")
            DgvCymnhd.Columns.Add("見積有効期限", "見積有効期限")
            DgvCymnhd.Columns.Add("得意先コード", "得意先コード")
            DgvCymnhd.Columns.Add("得意先名", "得意先名")

            DgvCymnhd.Columns.Add("通貨_外貨", "販売通貨")
            DgvCymnhd.Columns.Add("受注金額_外貨", "受注金額" & vbCrLf & "(原通貨)")

            DgvCymnhd.Columns.Add("受注金額", "受注金額" & vbCrLf & "(" & setBaseCurrency() & ")")
            DgvCymnhd.Columns.Add("ＶＡＴ", "VAT-OUT")

            DgvCymnhd.Columns.Add("仕入金額", "仕入金額" & vbCrLf & "(" & setBaseCurrency() & ")")
            DgvCymnhd.Columns.Add("粗利額", "利益" & vbCrLf & "(" & setBaseCurrency() & ")")
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
            DgvCymnhd.Columns.Add("更新者", "更新者")
        End If

        DgvCymnhd.Columns("受注金額_外貨").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("受注金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("粗利額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("粗利率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        DgvCymnhd.Columns("ＶＡＴ").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


        '数字形式
        DgvCymnhd.Columns("受注金額_外貨").DefaultCellStyle.Format = "N2"
        DgvCymnhd.Columns("受注金額").DefaultCellStyle.Format = "N2"
        DgvCymnhd.Columns("仕入金額").DefaultCellStyle.Format = "N2"
        DgvCymnhd.Columns("粗利額").DefaultCellStyle.Format = "N2"
        DgvCymnhd.Columns("粗利率").DefaultCellStyle.Format = "N1"

        DgvCymnhd.Columns("ＶＡＴ").DefaultCellStyle.Format = "N2"


        DgvCymnhd.Columns("見積有効期限").Visible = False
        DgvCymnhd.Columns("更新者").Visible = False

    End Sub

    '使用言語に合わせて受注明細見出しを切替
    Private Sub setDtColumns()

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            DgvCymnhd.Columns.Add("取消", "Cancel")
            DgvCymnhd.Columns.Add("受注番号", "OrderNumber")
            DgvCymnhd.Columns.Add("受注番号枝番", "OrderSuffixNumber")
            DgvCymnhd.Columns.Add("行番号", "LineNumber")

            '出庫登録時
            If OrderStatus = CommonConst.STATUS_GOODS_ISSUE Then
                DgvCymnhd.Columns.Add("発注番号", "PurchaseNumber")
                DgvCymnhd.Columns.Add("発注番号枝番", "PurchaseOrderSubNumber")
            End If

            DgvCymnhd.Columns.Add("仕入区分", "PurchasingClassification")
            DgvCymnhd.Columns.Add("メーカー", "Manufacturer")
            DgvCymnhd.Columns.Add("品名", "ItemName")
            DgvCymnhd.Columns.Add("型式", "Spec")
            DgvCymnhd.Columns.Add("仕入先名", "SupplierName")
            DgvCymnhd.Columns.Add("仕入値", "PurchaseAmount")
            DgvCymnhd.Columns.Add("受注数量", "OrderQuantity")
            DgvCymnhd.Columns.Add("売上数量", "SalesQuantity")
            DgvCymnhd.Columns.Add("受注残数", "OrderRemaining")
            DgvCymnhd.Columns.Add("単位", "Unit")
            DgvCymnhd.Columns.Add("間接費", "OverHead")
            DgvCymnhd.Columns.Add("売単価", "SellingPrice")
            DgvCymnhd.Columns.Add("売上金額", "SalesAmount")
            DgvCymnhd.Columns.Add("粗利額", "GrossProfit")
            DgvCymnhd.Columns.Add("粗利率", "GrossMarginRate")
            DgvCymnhd.Columns.Add("リードタイム", "LeadTime")
            DgvCymnhd.Columns.Add("出庫数", "GoodsDeliveryQuantity")
            DgvCymnhd.Columns.Add("未出庫数", "NoGoodsDeliveryQuantity")
            DgvCymnhd.Columns.Add("備考", "Remarks")
            DgvCymnhd.Columns.Add("更新者", "ModifiedBy")
            DgvCymnhd.Columns.Add("登録日", "Registration")
        Else
            DgvCymnhd.Columns.Add("取消", "取消")
            DgvCymnhd.Columns.Add("受注番号", "受注番号")
            DgvCymnhd.Columns.Add("受注番号枝番", "受注番号枝番")
            DgvCymnhd.Columns.Add("行番号", "行番号")

            '出庫登録時
            If OrderStatus = CommonConst.STATUS_GOODS_ISSUE Then
                DgvCymnhd.Columns.Add("発注番号", "発注番号")
                DgvCymnhd.Columns.Add("発注番号枝番", "発注番号枝番")
            End If

            DgvCymnhd.Columns.Add("仕入区分", "仕入区分")
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
            DgvCymnhd.Columns.Add("出庫数", "出庫数")
            DgvCymnhd.Columns.Add("未出庫数", "未出庫数")
            DgvCymnhd.Columns.Add("備考", "備考")
            DgvCymnhd.Columns.Add("更新者", "更新者")
            DgvCymnhd.Columns.Add("登録日", "登録日")
        End If


        DgvCymnhd.Columns("行番号").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("仕入値").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("受注数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("売上数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("受注残数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("間接費").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("売単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("売上金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("粗利額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("粗利率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        DgvCymnhd.Columns("出庫数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("未出庫数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

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

        DgvCymnhd.Columns("出庫数").DefaultCellStyle.Format = "N2"
        DgvCymnhd.Columns("未出庫数").DefaultCellStyle.Format = "N2"

    End Sub

    '抽出条件取得
    Private Function searchConditions() As String

        Dim Sql As String = ""

        '抽出条件
        Dim customerName As String = escapeSql(TxtCustomerName.Text)
        Dim customerAddress As String = escapeSql(TxtAddress.Text)
        Dim customerTel As String = escapeSql(TxtTel.Text)
        Dim customerCode As String = escapeSql(TxtCustomerCode.Text)
        Dim sinceDate As String = UtilClass.strFormatDate(dtOrderDateSince.Text)
        Dim untilDate As String = UtilClass.strFormatDate(dtOrderDateUntil.Text)
        Dim sinceNum As String = escapeSql(TxtOrderSince.Text)
        Dim salesName As String = escapeSql(TxtSales.Text)
        Dim customerPO As String = escapeSql(TxtCustomerPO.Text)

        If customerName <> Nothing Then
            Sql += " And "
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
            Sql += " 受注日 >= '" & sinceDate & "'"
        End If
        If untilDate <> Nothing Then
            Sql += " AND "
            Sql += " 受注日 <= '" & untilDate & "'"
        End If

        If sinceNum <> Nothing Then
            Sql += " AND "
            Sql += " 受注番号 ILIKE '%" & sinceNum & "%' "
        End If

        If salesName <> Nothing Then
            Sql += " AND "
            Sql += " 営業担当者 ILIKE '%" & salesName & "%' "
        End If

        If customerPO <> Nothing Then
            Sql += " AND "
            Sql += " 客先番号 ILIKE '%" & customerPO & "%' "
        End If

        Return Sql

    End Function

    Private Function viewSearchConditions() As String
        Dim Sql As String = ""

        '抽出条件
        Dim customerName As String = UtilClass.escapeSql(TxtCustomerName.Text)
        Dim customerAddress As String = UtilClass.escapeSql(TxtAddress.Text)
        Dim customerTel As String = UtilClass.escapeSql(TxtTel.Text)
        Dim customerCode As String = UtilClass.escapeSql(TxtCustomerCode.Text)
        Dim sinceDate As String = UtilClass.strFormatDate(dtOrderDateSince.Text)
        Dim untilDate As String = UtilClass.strFormatDate(dtOrderDateUntil.Text)
        Dim sinceNum As String = UtilClass.escapeSql(TxtOrderSince.Text)
        Dim salesName As String = UtilClass.escapeSql(TxtSales.Text)
        Dim customerPO As String = UtilClass.escapeSql(TxtCustomerPO.Text)
        Dim itemName As String = UtilClass.escapeSql(TxtItemName.Text)
        Dim spec As String = UtilClass.escapeSql(TxtSpec.Text)
        Dim Maker As String = UtilClass.escapeSql(txtMaker.Text)


        If customerName <> Nothing Then
            Sql += " AND "
            Sql += " t10.得意先名 ILIKE '%" & customerName & "%' "
        End If

        If customerAddress <> Nothing Then
            Sql += " AND "
            Sql += " t10.得意先住所 ILIKE '%" & customerAddress & "%' "
        End If

        If customerTel <> Nothing Then
            Sql += " AND "
            Sql += " t10.得意先電話番号 ILIKE '%" & customerTel & "%' "
        End If

        If customerCode <> Nothing Then
            Sql += " AND "
            Sql += " t10.得意先コード ILIKE '%" & customerCode & "%' "
        End If

        If sinceDate <> Nothing Then
            Sql += " AND "
            Sql += " t10.受注日 >= '" & sinceDate & "'"
        End If
        If untilDate <> Nothing Then
            Sql += " AND "
            Sql += " t10.受注日 <= '" & untilDate & "'"
        End If

        If sinceNum <> Nothing Then
            Sql += " AND "
            Sql += " t11.受注番号 ILIKE '%" & sinceNum & "%' "
        End If

        If salesName <> Nothing Then
            Sql += " AND "
            Sql += " t10.営業担当者 ILIKE '%" & salesName & "%' "
        End If

        If customerPO <> Nothing Then
            Sql += " AND "
            Sql += " t10.客先番号 ILIKE '%" & customerPO & "%' "
        End If

        If Maker <> Nothing Then
            Sql += " AND "
            Sql += " t11.メーカー ILIKE '%" & Maker & "%' "
        End If

        If itemName <> Nothing Then
            Sql += " AND "
            Sql += " t11.品名 ILIKE '%" & itemName & "%' "
        End If

        If spec <> Nothing Then
            Sql += " AND "
            Sql += " t11.型式 ILIKE '%" & spec & "%' "
        End If

        '取消データを含めない場合
        If ChkCancelData.Checked = False Then
            Sql += " AND "
            Sql += " t10.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
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

    '取消区分の表示テキストを返す
    'param1：String テーブル名
    'param2：String 詳細条件
    'Return: DataSet
    Public Function getDelKbnTxt(ByVal delKbn As String) As String
        '区分の値を取得し、使用言語に応じて値を返却

        Dim reDelKbn As String = IIf(delKbn = CommonConst.CANCEL_KBN_DISABLED,
                                    IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_JPN, CommonConst.CANCEL_KBN_JPN_TXT, CommonConst.CANCEL_KBN_ENG_TXT),
                                    "")
        Return reDelKbn
    End Function

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

    'sqlで実行する文字列からシングルクォーテーションを文字コードにする
    Private Function escapeSql(ByVal prmSql As String) As String
        Dim sql As String = prmSql

        sql = sql.Replace("'"c, "''") 'シングルクォーテーションを置換

        Return Regex.Escape(sql)
        Return sql
    End Function

    '汎用マスタから固定キー、可変キーに応じた結果を返す
    'param1：String 固定キー
    'param2：String 可変キー
    'Return: DataSet
    Private Function getDsHanyoData(ByVal prmFixed As String, Optional ByVal prmVariable As String = "") As DataSet
        Dim Sql As String = ""

        Sql = " AND "
        Sql += "固定キー ILIKE '" & prmFixed & "'"

        If prmVariable IsNot "" Then
            Sql += " AND "
            Sql += "可変キー ILIKE '" & prmVariable & "'"
        End If

        'リードタイムのリストを汎用マスタから取得
        Return getDsData("m90_hanyo", Sql)

    End Function

    Private Sub OrderList_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        'OrderListLoad() '一覧を再表示
    End Sub

    '基準通貨の通貨コードを取得する
    Private Function setBaseCurrency() As String
        Dim Sql As String
        '通貨表示：ベースの設定
        Sql = " AND 採番キー = " & CommonConst.CURRENCY_CD_IDR.ToString
        Sql += " AND 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString

        Dim ds As DataSet = getDsData("m25_currency", Sql)
        'TxtIDRCurrency.Text = ds.Tables(RS).Rows(0)("通貨コード")
        setBaseCurrency = ds.Tables(RS).Rows(0)("通貨コード")

    End Function
End Class