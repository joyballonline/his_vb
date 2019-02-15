Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class OrderList
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
                   Optional ByRef prmRefStatus As String = "")
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        OrderStatus = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True
        DgvCymnhd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.DisplayedCells
    End Sub

    '受注一覧を表示
    Private Sub OrderListLoad(Optional ByRef prmRefStatus As String = "")
        '一覧をクリア
        DgvCymnhd.Rows.Clear()
        DgvCymnhd.Columns.Clear()

        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        Try

            '伝票単位の場合
            If RbtnSlip.Checked Then

                Sql = searchConditions() '抽出条件取得
                Sql += viewFormat() '表示形式条件

                Sql += " ORDER BY "
                Sql += "登録日 DESC"

                ds = getDsData("t10_cymnhd", Sql)

                setHdColumns() '表示カラムの設定

                For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                    DgvCymnhd.Rows.Add()
                    DgvCymnhd.Rows(index).Cells("取消").Value = getDelKbnTxt(ds.Tables(RS).Rows(index)("取消区分"))
                    DgvCymnhd.Rows(index).Cells("受注番号").Value = ds.Tables(RS).Rows(index)("受注番号")
                    DgvCymnhd.Rows(index).Cells("受注番号枝番").Value = ds.Tables(RS).Rows(index)("受注番号枝番")
                    DgvCymnhd.Rows(index).Cells("客先番号").Value = ds.Tables(RS).Rows(index)("客先番号")
                    DgvCymnhd.Rows(index).Cells("受注日").Value = ds.Tables(RS).Rows(index)("受注日")
                    DgvCymnhd.Rows(index).Cells("見積番号").Value = ds.Tables(RS).Rows(index)("見積番号")
                    DgvCymnhd.Rows(index).Cells("見積番号枝番").Value = ds.Tables(RS).Rows(index)("見積番号枝番")
                    DgvCymnhd.Rows(index).Cells("見積日").Value = ds.Tables(RS).Rows(index)("見積日")
                    DgvCymnhd.Rows(index).Cells("見積有効期限").Value = ds.Tables(RS).Rows(index)("見積有効期限")
                    DgvCymnhd.Rows(index).Cells("得意先コード").Value = ds.Tables(RS).Rows(index)("得意先コード")
                    DgvCymnhd.Rows(index).Cells("得意先名").Value = ds.Tables(RS).Rows(index)("得意先名")
                    DgvCymnhd.Rows(index).Cells("得意先郵便番号").Value = ds.Tables(RS).Rows(index)("得意先郵便番号")
                    DgvCymnhd.Rows(index).Cells("得意先住所").Value = ds.Tables(RS).Rows(index)("得意先住所")
                    DgvCymnhd.Rows(index).Cells("得意先電話番号").Value = ds.Tables(RS).Rows(index)("得意先電話番号")
                    DgvCymnhd.Rows(index).Cells("得意先ＦＡＸ").Value = ds.Tables(RS).Rows(index)("得意先ＦＡＸ")
                    DgvCymnhd.Rows(index).Cells("得意先担当者名").Value = ds.Tables(RS).Rows(index)("得意先担当者名")
                    DgvCymnhd.Rows(index).Cells("得意先担当者役職").Value = ds.Tables(RS).Rows(index)("得意先担当者役職")
                    DgvCymnhd.Rows(index).Cells("ＶＡＴ").Value = ds.Tables(RS).Rows(index)("ＶＡＴ")
                    DgvCymnhd.Rows(index).Cells("受注金額").Value = ds.Tables(RS).Rows(index)("見積金額")
                    DgvCymnhd.Rows(index).Cells("仕入金額").Value = ds.Tables(RS).Rows(index)("仕入金額")
                    DgvCymnhd.Rows(index).Cells("粗利額").Value = ds.Tables(RS).Rows(index)("粗利額")
                    DgvCymnhd.Rows(index).Cells("支払条件").Value = ds.Tables(RS).Rows(index)("支払条件")
                    DgvCymnhd.Rows(index).Cells("営業担当者").Value = ds.Tables(RS).Rows(index)("営業担当者")
                    DgvCymnhd.Rows(index).Cells("入力担当者").Value = ds.Tables(RS).Rows(index)("入力担当者")
                    DgvCymnhd.Rows(index).Cells("備考").Value = ds.Tables(RS).Rows(index)("備考")
                    DgvCymnhd.Rows(index).Cells("登録日").Value = ds.Tables(RS).Rows(index)("登録日")
                Next

            Else

                '明細単位の場合
                'joinするのでとりあえず直書き
                Sql = "SELECT"
                Sql += " *"
                Sql += " FROM "
                Sql += " public.t11_cymndt t11 "

                Sql += " INNER JOIN "
                Sql += " t10_cymnhd t10"
                Sql += " ON "

                Sql += " t11.会社コード = t10.会社コード"
                Sql += " AND "
                Sql += " t11.受注番号 = t10.受注番号"

                Sql += " WHERE "
                Sql += " t11.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonNM & "'"

                '抽出条件
                Dim customerName As String = TxtCustomerName.Text
                Dim customerAddress As String = TxtAddress.Text
                Dim customerTel As String = TxtTel.Text
                Dim customerCode As String = TxtCustomerCode.Text
                Dim sinceDate As String = dtOrderDateSince.Text
                Dim untilDate As String = dtOrderDateUntil.Text
                Dim sinceNum As String = TxtOrderSince.Text
                Dim untilNum As String = TxtOrderUntil.Text
                Dim salesName As String = TxtSales.Text
                Dim customerPO As String = TxtCustomerPO.Text

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
                    Sql += " t11.受注番号 >= '" & sinceNum & "' "
                End If
                If untilNum <> Nothing Then
                    Sql += " AND "
                    Sql += " t11.受注番号 <= '" & untilNum & "' "
                End If

                If salesName <> Nothing Then
                    Sql += " AND "
                    Sql += " t10.営業担当者 ILIKE '%" & salesName & "%' "
                End If

                If customerPO <> Nothing Then
                    Sql += " AND "
                    Sql += " t10.客先番号 ILIKE '%" & customerPO & "%' "
                End If

                '取消データを含めない場合
                If ChkCancelData.Checked = False Then
                    Sql += " AND "
                    Sql += " t10.取消区分 = 0 "
                End If

                Sql += " ORDER BY "
                Sql += " t11.登録日 DESC"

                ds = _db.selectDB(Sql, RS, reccnt)

                setDtColumns() '表示カラムの設定

                Dim tmp1 As String = ""
                For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1

                    DgvCymnhd.Rows.Add()
                    DgvCymnhd.Rows(index).Cells("取消").Value = getDelKbnTxt(ds.Tables(RS).Rows(index)("取消区分"))
                    DgvCymnhd.Rows(index).Cells("受注番号").Value = ds.Tables(RS).Rows(index)("受注番号")
                    DgvCymnhd.Rows(index).Cells("受注番号枝番").Value = ds.Tables(RS).Rows(index)("受注番号枝番")
                    DgvCymnhd.Rows(index).Cells("行番号").Value = ds.Tables(RS).Rows(index)("行番号")

                    '★汎用マスタからの取得に変更する
                    If ds.Tables(RS).Rows(index)("仕入区分") = CommonConst.Sire_KBN_Sire Then
                        DgvCymnhd.Rows(index).Cells("仕入区分").Value = CommonConst.Sire_KBN_Sire_TXT
                    ElseIf ds.Tables(RS).Rows(index)("仕入区分") = CommonConst.Sire_KBN_Zaiko Then
                        DgvCymnhd.Rows(index).Cells("仕入区分").Value = CommonConst.Sire_KBN_Zaiko_TXT
                    Else
                        DgvCymnhd.Rows(index).Cells("仕入区分").Value = CommonConst.Sire_KBN_SERVICE_TXT
                    End If

                    DgvCymnhd.Rows(index).Cells("メーカー").Value = ds.Tables(RS).Rows(index)("メーカー")
                    DgvCymnhd.Rows(index).Cells("品名").Value = ds.Tables(RS).Rows(index)("品名")
                    DgvCymnhd.Rows(index).Cells("型式").Value = ds.Tables(RS).Rows(index)("型式")
                    DgvCymnhd.Rows(index).Cells("仕入先名").Value = ds.Tables(RS).Rows(index)("仕入先名")
                    DgvCymnhd.Rows(index).Cells("仕入値").Value = ds.Tables(RS).Rows(index)("仕入値")
                    DgvCymnhd.Rows(index).Cells("受注数量").Value = ds.Tables(RS).Rows(index)("受注数量")
                    DgvCymnhd.Rows(index).Cells("売上数量").Value = ds.Tables(RS).Rows(index)("売上数量")
                    DgvCymnhd.Rows(index).Cells("受注残数").Value = ds.Tables(RS).Rows(index)("受注残数")
                    DgvCymnhd.Rows(index).Cells("単位").Value = ds.Tables(RS).Rows(index)("単位")
                    DgvCymnhd.Rows(index).Cells("間接費").Value = ds.Tables(RS).Rows(index)("間接費")
                    DgvCymnhd.Rows(index).Cells("売単価").Value = ds.Tables(RS).Rows(index)("売単価")
                    DgvCymnhd.Rows(index).Cells("売上金額").Value = ds.Tables(RS).Rows(index)("売上金額")
                    DgvCymnhd.Rows(index).Cells("粗利額").Value = ds.Tables(RS).Rows(index)("粗利額")
                    DgvCymnhd.Rows(index).Cells("粗利率").Value = ds.Tables(RS).Rows(index)("粗利率")

                    If ds.Tables(RS).Rows(index)("リードタイム単位") Is DBNull.Value Then

                        'リードタイムが空だったらそのまま
                        DgvCymnhd.Rows(index).Cells("リードタイム").Value = ds.Tables(RS).Rows(index)("リードタイム")

                    Else

                        'リードタイムが入っていたら汎用マスタから単位を取得して連結する
                        Sql = " AND "
                        Sql += "固定キー"
                        Sql += " ILIKE "
                        Sql += "'"
                        Sql += "4"
                        Sql += "'"
                        Sql += " AND "
                        Sql += "可変キー"
                        Sql += " ILIKE "
                        Sql += "'"
                        Sql += ds.Tables(RS).Rows(index)("リードタイム単位").ToString
                        Sql += "'"

                        Dim dsHanyo As DataSet = getDsData("m90_hanyo", Sql)

                        tmp1 = ""
                        tmp1 += ds.Tables(RS).Rows(index)("リードタイム")
                        tmp1 += dsHanyo.Tables(RS).Rows(0)("文字１")
                        DgvCymnhd.Rows(index).Cells("リードタイム").Value = tmp1
                    End If

                    DgvCymnhd.Rows(index).Cells("出庫数").Value = ds.Tables(RS).Rows(index)("出庫数")
                    DgvCymnhd.Rows(index).Cells("未出庫数").Value = ds.Tables(RS).Rows(index)("未出庫数")
                    DgvCymnhd.Rows(index).Cells("備考").Value = ds.Tables(RS).Rows(index)("備考")
                    DgvCymnhd.Rows(index).Cells("更新者").Value = ds.Tables(RS).Rows(index)("更新者")
                    DgvCymnhd.Rows(index).Cells("登録日").Value = ds.Tables(RS).Rows(index)("登録日")
                Next
            End If



        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    Private Sub MstHanyoue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If OrderStatus = "SALES" Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "SalesInputMode"
            Else
                LblMode.Text = "売上入力モード"
            End If

            BtnSales.Visible = True
            BtnSales.Location = New Point(997, 509)
        ElseIf OrderStatus = "GOODS_ISSUE" Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "GoodsDeliveryInputMode"
            Else
                LblMode.Text = "出庫入力モード"
            End If

            BtnGoodsIssue.Visible = True
            BtnGoodsIssue.Location = New Point(997, 509)
        ElseIf OrderStatus = "EDIT" Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "EditMode"
            Else
                LblMode.Text = "編集モード"
            End If

            BtnOrderEdit.Visible = True
            BtnOrderEdit.Location = New Point(997, 509)
        ElseIf OrderStatus = "VIEW" Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "ViewMode"
            Else
                LblMode.Text = "参照モード"
            End If

            BtnOrderView.Visible = True
            BtnOrderView.Location = New Point(997, 509)
        ElseIf OrderStatus = "CANCEL" Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "CancelMode"
            Else
                LblMode.Text = "取消モード"
            End If

            BtnOrderCancel.Visible = True
            BtnOrderCancel.Location = New Point(997, 509)
        ElseIf OrderStatus = "CLONE" Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "NewCopyMode"
            Else
                LblMode.Text = "新規複写モード"
            End If

            BtnOrderClone.Visible = True
            BtnOrderClone.Location = New Point(997, 509)
        ElseIf OrderStatus = "BILL" Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "BillingRegistrationMode"
            Else
                LblMode.Text = "請求登録モード"
            End If

            BtnBill.Visible = True
            BtnBill.Location = New Point(997, 509)
        End If

        '検索（Date）の初期値
        dtOrderDateSince.Value = DateAdd("d", CommonConst.SINCE_DEFAULT_DAY, DateTime.Today)
        dtOrderDateUntil.Value = DateTime.Today

        OrderListLoad() '一覧表示

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblConditions.Text = "TermsOfSelection"
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

            RbtnDetails.Text = "LineItemUnit"
            RbtnDetails.Location = New Point(166, 202)

            ChkCancelData.Text = "IncludeCancelData"
            ChkCancelData.Location = New Point(556, 203)

            BtnOrderSearch.Text = "Search"
            BtnBill.Text = "BillingRegistration"
            BtnOrderCancel.Text = "CancelOfOrder"
            BtnSales.Text = "SalesRagistration"
            BtnGoodsIssue.Text = "GoodsIssueRegistration"
            BtnOrderClone.Text = "OrderCopy"
            BtnOrderView.Text = "OrderView"
            BtnOrderEdit.Text = "OrderEdit"
            BtnBack.Text = "Back"
        End If
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        Dim openForm As Form = Nothing
        openForm = New frmC01F30_Menu(_msgHd, _langHd, _db)
        openForm.Show()
        Me.Close()
    End Sub

    '明細単位切替時で表示形式のイベントを取得
    Private Sub RbtnDetails_CheckedChanged(sender As Object, e As EventArgs) Handles RbtnDetails.CheckedChanged

        OrderListLoad() '一覧を再表示

    End Sub

    '検索実行
    Private Sub BtnQuoteSearch_Click(sender As Object, e As EventArgs) Handles BtnOrderSearch.Click

        OrderListLoad() '一覧を再表示

    End Sub

    '機能としては使用しない
    '受注修正
    Private Sub BtnOrderEdit_Click(sender As Object, e As EventArgs) Handles BtnOrderEdit.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvCymnhd.CurrentCell.RowIndex
        Dim No As String = DgvCymnhd.Rows(RowIdx).Cells("受注番号").Value
        Dim Suffix As String = DgvCymnhd.Rows(RowIdx).Cells("受注番号枝番").Value
        Dim Status As String = "EDIT"
        Dim openForm As Form = Nothing
        openForm = New Order(_msgHd, _db, _langHd, Me, No, Suffix, Status)   '処理選択
        openForm.ShowDialog(Me)

        OrderListLoad() '一覧表示
    End Sub

    '受注参照
    Private Sub BtnOrderView_Click(sender As Object, e As EventArgs) Handles BtnOrderView.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvCymnhd.CurrentCell.RowIndex
        Dim No As String = DgvCymnhd.Rows(RowIdx).Cells("受注番号").Value
        Dim Suffix As String = DgvCymnhd.Rows(RowIdx).Cells("受注番号枝番").Value
        Dim Status As String = "VIEW"

        Dim openForm As Form = Nothing
        openForm = New Order(_msgHd, _db, _langHd, Me, No, Suffix, Status)   '処理選択
        openForm.Show(Me)
    End Sub

    '売上入力
    Private Sub BtnOrder_Click(sender As Object, e As EventArgs) Handles BtnSales.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvCymnhd.CurrentCell.RowIndex
        Dim No As String = DgvCymnhd.Rows(RowIdx).Cells("受注番号").Value
        Dim Suffix As String = DgvCymnhd.Rows(RowIdx).Cells("受注番号枝番").Value
        Dim openForm As Form = Nothing
        openForm = New OrderManagement(_msgHd, _db, _langHd, No, Suffix)   '処理選択
        openForm.Show(Me)
    End Sub

    '出庫入力
    Private Sub BtnReceipt_Click(sender As Object, e As EventArgs) Handles BtnGoodsIssue.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvCymnhd.CurrentCell.RowIndex
        Dim No As String = DgvCymnhd.Rows(RowIdx).Cells("受注番号").Value
        Dim Suffix As String = DgvCymnhd.Rows(RowIdx).Cells("受注番号枝番").Value
        Dim openForm As Form = Nothing
        openForm = New GoodsIssue(_msgHd, _db, _langHd, No, Suffix)   '処理選択
        openForm.Show(Me)
    End Sub

    '受注取消
    Private Sub BtnOrderCancel_Click(sender As Object, e As EventArgs) Handles BtnOrderCancel.Click
        Dim dtNow As DateTime = DateTime.Now
        Dim Sql1 As String = ""

        Try
            Sql1 = "UPDATE "
            Sql1 += " Public.t10_cymnhd "
            Sql1 += " SET "

            Sql1 += " 取消区分 = 1"
            Sql1 += ", 取消日 = '" & dtNow & "'"
            Sql1 += ", 更新日 = '" & dtNow & "'"
            Sql1 += ", 更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"

            Sql1 += " WHERE "
            Sql1 += " 会社コード "
            Sql1 += "='"
            Sql1 += frmC01F10_Login.loginValue.BumonNM
            Sql1 += "'"
            Sql1 += " AND "
            Sql1 += " 受注番号"
            Sql1 += "='"
            Sql1 += DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("受注番号").Value
            Sql1 += "' "
            Sql1 += " AND"
            Sql1 += " 受注番号枝番"
            Sql1 += "='"
            Sql1 += DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("受注番号枝番").Value
            Sql1 += "' "

            '取消確認のアラート
            Dim result As DialogResult = _msgHd.dspMSG("confirmCancel", frmC01F10_Login.loginValue.Language)

            If result = DialogResult.Yes Then
                _db.executeDB(Sql1)
                OrderListLoad() 'データ更新
            End If

        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '取消データを含めるイベントの取得
    Private Sub ChkCancelData_CheckedChanged(sender As Object, e As EventArgs) Handles ChkCancelData.CheckedChanged

        OrderListLoad() '一覧を再表示

    End Sub

    '受注複写
    Private Sub BtnOrderClone_Click(sender As Object, e As EventArgs) Handles BtnOrderClone.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvCymnhd.CurrentCell.RowIndex
        Dim No As String = DgvCymnhd.Rows(RowIdx).Cells("受注番号").Value
        Dim Suffix As String = DgvCymnhd.Rows(RowIdx).Cells("受注番号枝番").Value
        Dim Status As String = "CLONE"
        Dim openForm As Form = Nothing
        openForm = New Order(_msgHd, _db, _langHd, Me, No, Suffix, Status)   '処理選択
        Me.Enabled = False
        Me.Hide()
        openForm.ShowDialog(Me)

        OrderListLoad() '一覧を再表示
    End Sub

    '請求登録
    Private Sub BtnBill_Click(sender As Object, e As EventArgs) Handles BtnBill.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvCymnhd.CurrentCell.RowIndex
        Dim No As String = DgvCymnhd.Rows(RowIdx).Cells("受注番号").Value
        Dim Suffix As String = DgvCymnhd.Rows(RowIdx).Cells("受注番号枝番").Value
        Dim openForm As Form = Nothing
        openForm = New BillingManagement(_msgHd, _db, _langHd, Me, No, Suffix)   '処理選択
        Me.Enabled = False
        Me.Hide()
        openForm.ShowDialog(Me)

        OrderListLoad() '一覧表示
    End Sub

    '使用言語に合わせて受注基本見出しを切替
    Private Sub setHdColumns()
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            DgvCymnhd.Columns.Add("取消", "Cancel")
            DgvCymnhd.Columns.Add("受注番号", "OrderNumber")
            DgvCymnhd.Columns.Add("受注番号枝番", "JobOrderSubNumber")
            DgvCymnhd.Columns.Add("客先番号", "CustomerNumber")
            DgvCymnhd.Columns.Add("受注日", "JobOrderDate")
            DgvCymnhd.Columns.Add("見積番号", "QuotationNumber")
            DgvCymnhd.Columns.Add("見積番号枝番", "BranchNumber")
            DgvCymnhd.Columns.Add("見積日", "QuotationDate")
            DgvCymnhd.Columns.Add("見積有効期限", "QuotationExpriedDate")
            DgvCymnhd.Columns.Add("得意先コード", "CustomerCode")
            DgvCymnhd.Columns.Add("得意先名", "CustomerName")
            DgvCymnhd.Columns.Add("得意先郵便番号", "PostalCode")
            DgvCymnhd.Columns.Add("得意先住所", "Address")
            DgvCymnhd.Columns.Add("得意先電話番号", "PhoneNumber")
            DgvCymnhd.Columns.Add("得意先ＦＡＸ", "FAX")
            DgvCymnhd.Columns.Add("得意先担当者名", "NameOfPIC")
            DgvCymnhd.Columns.Add("得意先担当者役職", "PositionPICCustomer")
            DgvCymnhd.Columns.Add("ＶＡＴ", "VAT")
            DgvCymnhd.Columns.Add("受注金額", "OrderAmount")
            DgvCymnhd.Columns.Add("仕入金額", "PurchaseAmount")
            DgvCymnhd.Columns.Add("粗利額", "GrossMargin")
            DgvCymnhd.Columns.Add("支払条件", "PeymentTerms")
            DgvCymnhd.Columns.Add("営業担当者", "SalesPersonInCharge")
            DgvCymnhd.Columns.Add("入力担当者", "PICForInputting")
            DgvCymnhd.Columns.Add("備考", "Remarks")
            DgvCymnhd.Columns.Add("登録日", "RegistrationDate")
        Else
            DgvCymnhd.Columns.Add("取消", "取消")
            DgvCymnhd.Columns.Add("受注番号", "受注番号")
            DgvCymnhd.Columns.Add("受注番号枝番", "受注番号枝番")
            DgvCymnhd.Columns.Add("客先番号", "客先番号")
            DgvCymnhd.Columns.Add("受注日", "受注日")
            DgvCymnhd.Columns.Add("見積番号", "見積番号")
            DgvCymnhd.Columns.Add("見積番号枝番", "見積番号枝番")
            DgvCymnhd.Columns.Add("見積日", "見積日")
            DgvCymnhd.Columns.Add("見積有効期限", "見積有効期限")
            DgvCymnhd.Columns.Add("得意先コード", "得意先コード")
            DgvCymnhd.Columns.Add("得意先名", "得意先名")
            DgvCymnhd.Columns.Add("得意先郵便番号", "得意先郵便番号")
            DgvCymnhd.Columns.Add("得意先住所", "得意先住所")
            DgvCymnhd.Columns.Add("得意先電話番号", "得意先電話番号")
            DgvCymnhd.Columns.Add("得意先ＦＡＸ", "得意先ＦＡＸ")
            DgvCymnhd.Columns.Add("得意先担当者名", "得意先担当者名")
            DgvCymnhd.Columns.Add("得意先担当者役職", "得意先担当者役職")
            DgvCymnhd.Columns.Add("ＶＡＴ", "ＶＡＴ")
            DgvCymnhd.Columns.Add("受注金額", "受注金額")
            DgvCymnhd.Columns.Add("仕入金額", "仕入金額")
            DgvCymnhd.Columns.Add("粗利額", "粗利額")
            DgvCymnhd.Columns.Add("支払条件", "支払条件")
            DgvCymnhd.Columns.Add("営業担当者", "営業担当者")
            DgvCymnhd.Columns.Add("入力担当者", "入力担当者")
            DgvCymnhd.Columns.Add("備考", "備考")
            DgvCymnhd.Columns.Add("登録日", "登録日")
        End If

        DgvCymnhd.Columns("ＶＡＴ").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("受注金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("粗利額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    End Sub

    '使用言語に合わせて受注明細見出しを切替
    Private Sub setDtColumns()
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            DgvCymnhd.Columns.Add("取消", "Cancel")
            DgvCymnhd.Columns.Add("受注番号", "OrderNumber")
            DgvCymnhd.Columns.Add("受注番号枝番", "OrderSuffixNumber")
            DgvCymnhd.Columns.Add("行番号", "LineNumber")
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

    End Sub

    '抽出条件取得
    Private Function searchConditions() As String
        Dim Sql As String = ""

        '抽出条件
        Dim customerName As String = TxtCustomerName.Text
        Dim customerAddress As String = TxtAddress.Text
        Dim customerTel As String = TxtTel.Text
        Dim customerCode As String = TxtCustomerCode.Text
        Dim sinceDate As String = dtOrderDateSince.Text
        Dim untilDate As String = dtOrderDateUntil.Text
        Dim sinceNum As String = TxtOrderSince.Text
        Dim untilNum As String = TxtOrderUntil.Text
        Dim salesName As String = TxtSales.Text
        Dim customerPO As String = TxtCustomerPO.Text

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
            Sql += " 受注日 >= '" & sinceDate & "'"
        End If
        If untilDate <> Nothing Then
            Sql += " AND "
            Sql += " 受注日 <= '" & untilDate & "'"
        End If

        If sinceNum <> Nothing Then
            Sql += " AND "
            Sql += " 受注番号 >= '" & sinceNum & "' "
        End If
        If untilNum <> Nothing Then
            Sql += " AND "
            Sql += " 受注番号 <= '" & untilNum & "' "
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

    '表示形式条件
    Private Function viewFormat() As String
        Dim Sql As String = ""

        '取消データを含めない場合
        If ChkCancelData.Checked = False Then
            Sql += " AND "
            Sql += " 取消区分 = 0 "
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

        Sql += "SELECT"
        Sql += " *"
        Sql += " FROM "

        Sql += "public." & tableName
        Sql += " WHERE "
        Sql += "会社コード"
        Sql += " ILIKE  "
        Sql += "'" & frmC01F10_Login.loginValue.BumonNM & "'"
        Sql += txtParam
        Return _db.selectDB(Sql, RS, reccnt)
    End Function

End Class