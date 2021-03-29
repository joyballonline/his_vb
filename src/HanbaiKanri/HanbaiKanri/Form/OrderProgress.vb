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
        If mSet_Mode() = False Then
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


            DgvCymnhd.Columns("受注日").HeaderText = "OrderDate"
            DgvCymnhd.Columns("受注番号").HeaderText = "OrderNumber"
            DgvCymnhd.Columns("受注番号枝番").HeaderText = "OrderVer"
            DgvCymnhd.Columns("行番号").HeaderText = "LineNo"
            DgvCymnhd.Columns("メーカー").HeaderText = "Maker"
            DgvCymnhd.Columns("品名").HeaderText = "ProductName"
            DgvCymnhd.Columns("型式").HeaderText = "Model"
            DgvCymnhd.Columns("見積番号").HeaderText = "QuoteNumber"
            DgvCymnhd.Columns("見積日").HeaderText = "QuoteDate"
            DgvCymnhd.Columns("売上登録").HeaderText = "SalesRegistration"
            DgvCymnhd.Columns("出庫登録").HeaderText = "GoodsIssueTegistration"
            DgvCymnhd.Columns("売掛請求登録").HeaderText = "AccountsReceivableRegistration"
            DgvCymnhd.Columns("入金登録").HeaderText = "DepositRegistration"
            DgvCymnhd.Columns("客先番号").HeaderText = "CustomerNumber"
            DgvCymnhd.Columns("請求番号").HeaderText = "InvoiceNo"

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
            Sql += " ,t11.売上数量 ,t11.受注残数 ,t11.未出庫数 ,t11.出庫数"

            Sql += " from t10_cymnhd as t10"

            Sql += " left join t11_cymndt as t11"
            Sql += " on t10.受注番号 = t11.受注番号 and t10.受注番号枝番 = t11.受注番号枝番"


            Sql += " WHERE t10.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

            '履歴最新
            Sql += "   and t10.受注番号枝番 = (SELECT MAX(t10M.受注番号枝番) FROM t10_cymnhd as t10M where t10.受注番号 = t10M.受注番号) "

            Sql += viewSearchConditions() '検索条件

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
                DgvCymnhd.Rows(i).Cells("客先番号").Value = ds.Tables(RS).Rows(i)("客先番号")

                '売上登録
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


                '出庫登録
                If IsDBNull(ds.Tables(RS).Rows(i)("未出庫数")) Then
                    '出庫なし
                    DgvCymnhd.Rows(i).Cells("出庫登録").Value = ""
                ElseIf ds.Tables(RS).Rows(i)("未出庫数") = 0 Then
                    '出庫済
                    DgvCymnhd.Rows(i).Cells("出庫登録").Value = "〇"
                ElseIf ds.Tables(RS).Rows(i)("出庫数") > 0 AndAlso ds.Tables(RS).Rows(i)("未出庫数") > 0 Then
                    '一部出庫
                    DgvCymnhd.Rows(i).Cells("出庫登録").Value = "△"
                End If


                '売掛請求登録
                Sql = " SELECT distinct t23.請求番号"
                Sql += " from v_t23_skyuhd_1 as t23"
                Sql += " WHERE t23.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql += " and 受注番号 = '" & ds.Tables(RS).Rows(i)("受注番号") & "'"
                Sql += " and 受注番号枝番 = '" & ds.Tables(RS).Rows(i)("受注番号枝番") & "'"
                'Sql += " and 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
                Dim ds2 As DataSet = _db.selectDB(Sql, RS, reccnt)
                Dim s2 As String = ""
                For x As Integer = 0 To ds2.Tables(RS).Rows.Count - 1
                    s2 += "'" & ds2.Tables(RS).Rows(x)("請求番号") & "'"
                    If x < ds2.Tables(RS).Rows.Count - 1 Then
                        s2 += ","
                    End If
                Next
                DgvCymnhd.Rows(i).Cells("請求番号").Value = s2.Replace("'", "")

                Sql = " SELECT sum(請求金額計) as 請求金額計, sum(売掛残高) as 売掛残高, sum(入金額計) as 入金額計"
                Sql += " from t23_skyuhd as t23"

                Sql += " WHERE t23.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                'Sql += " and 受注番号 = '" & ds.Tables(RS).Rows(i)("受注番号") & "'"
                'Sql += " and 受注番号枝番 = '" & ds.Tables(RS).Rows(i)("受注番号枝番") & "'"
                'Sql += " and 取消日 is null"
                If s2.Equals("") Then
                    s2 = "''"
                End If
                Sql += " and t23.請求番号 in (" & s2 & ")"

                Dim ds_seikyu As DataSet = _db.selectDB(Sql, RS, reccnt)

                If IsDBNull(ds_seikyu.Tables(RS).Rows(0)("売掛残高")) Then
                    '請求なし
                    DgvCymnhd.Rows(i).Cells("売掛請求登録").Value = ""
                    DgvCymnhd.Rows(i).Cells("入金登録").Value = ""
                Else
                    If ds.Tables(RS).Rows(i)("見積金額") > ds_seikyu.Tables(RS).Rows(0)("請求金額計") Then
                        DgvCymnhd.Rows(i).Cells("売掛請求登録").Value = "△"
                    Else
                        DgvCymnhd.Rows(i).Cells("売掛請求登録").Value = "〇"
                    End If


                    If ds_seikyu.Tables(RS).Rows(0)("売掛残高") = 0 Then
                        '入金済み
                        DgvCymnhd.Rows(i).Cells("入金登録").Value = "〇"
                    ElseIf ds_seikyu.Tables(RS).Rows(0)("入金額計") > 0 AndAlso ds_seikyu.Tables(RS).Rows(0)("売掛残高") > 0 Then
                        '一部
                        DgvCymnhd.Rows(i).Cells("入金登録").Value = "△"
                    Else
                        '請求だけ
                        DgvCymnhd.Rows(i).Cells("入金登録").Value = ""
                    End If

                End If

                ds_seikyu = Nothing

            Next

            DgvCymnhd.Columns("受注日").DefaultCellStyle.Format = "d"
            DgvCymnhd.Columns("見積日").DefaultCellStyle.Format = "d"

            '中央寄せ
            DgvCymnhd.Columns("売上登録").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DgvCymnhd.Columns("出庫登録").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DgvCymnhd.Columns("売掛請求登録").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DgvCymnhd.Columns("入金登録").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


            ds = Nothing


        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

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


    '抽出条件取得
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
        OrderListLoad() '一覧を再表示
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