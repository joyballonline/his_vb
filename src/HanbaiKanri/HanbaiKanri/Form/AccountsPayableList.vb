Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports System.Text.RegularExpressions
Imports System.Globalization

Public Class AccountsPayableList
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
                   ByRef prmRefForm As Form,
                   Optional ByRef prmRefStatus As String = "")
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _parentForm = prmRefForm
        _status = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True

    End Sub

    '画面表示時
    Private Sub MstHanyoue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If _status = CommonConst.STATUS_VIEW Then

            LblMode.Text = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                            "ViewMode",
                            "参照モード")

        ElseIf _status = CommonConst.STATUS_CANCEL Then

            LblMode.Text = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                            "CancelMode",
                            "取消モード")

            BtnAPCancel.Visible = True
            BtnAPCancel.Location = New Point(997, 509)
        End If

        '検索（Date）の初期値
        dtAPDateSince.Value = DateAdd("d", CommonConst.SINCE_DEFAULT_DAY, DateTime.Today)
        dtAPDateUntil.Value = DateTime.Today

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then

            DgvKike.Columns("取消").HeaderText = "Cancel"
            DgvKike.Columns("買掛番号").HeaderText = "AccountsPayableNumber"
            DgvKike.Columns("買掛区分").HeaderText = "AccountsPayableClassification"
            'DgvKike.Columns("買掛日").HeaderText = "AccountsPayableDate"
            DgvKike.Columns("客先番号").HeaderText = "CustomerNumber"
            DgvKike.Columns("発注番号").HeaderText = "PurchaseOrderNumber"
            DgvKike.Columns("発注番号枝番").HeaderText = "PurchaseOrderVer"
            DgvKike.Columns("仕入先コード").HeaderText = "SupplierCode"
            DgvKike.Columns("仕入先名").HeaderText = "SupplierName"

            DgvKike.Columns("通貨_外貨").HeaderText = "Currency"
            DgvKike.Columns("仕入原価").HeaderText = "PurchaseCost" & vbCrLf & "a"
            DgvKike.Columns("VAT").HeaderText = "VAT-IN" & vbCrLf & "b"
            DgvKike.Columns("買掛金額").HeaderText = "AccountsPayableAmount" & vbCrLf & "c=a+b"
            DgvKike.Columns("買掛金額計_外貨").HeaderText = "RegisteredAmount" & vbCrLf & "d" '既登録額
            DgvKike.Columns("未登録額").HeaderText = "UnregisteredAmount" & vbCrLf & "e=c-d"

            DgvKike.Columns("買掛残高_外貨").HeaderText = "AccountsPayableBalance"
            DgvKike.Columns("通貨").HeaderText = "Currency"
            DgvKike.Columns("買掛金額計").HeaderText = "TotalAccountsPayable"
            DgvKike.Columns("買掛残高").HeaderText = "AccountsPayableBalance"

            DgvKike.Columns("備考1").HeaderText = "Remarks1"
            DgvKike.Columns("備考2").HeaderText = "Remarks2"
            DgvKike.Columns("更新日").HeaderText = "UpdateDate"
            DgvKike.Columns("更新者").HeaderText = "ModifiedBy"

        Else  '日本語

            DgvKike.Columns("仕入原価").HeaderText = "仕入原価" & vbCrLf & "a"
            DgvKike.Columns("VAT").HeaderText = "VAT-IN" & vbCrLf & "b"
            DgvKike.Columns("買掛金額").HeaderText = "買掛金額" & vbCrLf & "c=a+b"
            DgvKike.Columns("買掛金額計_外貨").HeaderText = "既登録額" & vbCrLf & "d" '既登録額
            DgvKike.Columns("未登録額").HeaderText = "未登録額" & vbCrLf & "e=c-d"

        End If

        '数字形式
        DgvKike.Columns("仕入原価").DefaultCellStyle.Format = "N2"
        DgvKike.Columns("VAT").DefaultCellStyle.Format = "N2"
        DgvKike.Columns("買掛金額").DefaultCellStyle.Format = "N2"
        DgvKike.Columns("買掛金額計_外貨").DefaultCellStyle.Format = "N2"
        DgvKike.Columns("未登録額").DefaultCellStyle.Format = "N2"

        DgvKike.Columns("買掛残高_外貨").DefaultCellStyle.Format = "N2"
        DgvKike.Columns("買掛金額計").DefaultCellStyle.Format = "N2"
        DgvKike.Columns("買掛残高").DefaultCellStyle.Format = "N2"


        '右寄せ
        DgvKike.Columns("仕入原価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvKike.Columns("VAT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvKike.Columns("買掛金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvKike.Columns("買掛金額計_外貨").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvKike.Columns("未登録額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        DgvKike.Columns("買掛残高_外貨").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvKike.Columns("買掛金額計").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvKike.Columns("買掛残高").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        '中央寄せ
        DgvKike.Columns("取消").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvKike.Columns("買掛番号").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvKike.Columns("買掛区分").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvKike.Columns("買掛日").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvKike.Columns("客先番号").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvKike.Columns("発注番号枝番").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvKike.Columns("仕入先コード").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvKike.Columns("仕入先名").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvKike.Columns("通貨_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvKike.Columns("仕入原価").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvKike.Columns("VAT").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        DgvKike.Columns("買掛金額").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvKike.Columns("買掛金額計_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvKike.Columns("未登録額").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        DgvKike.Columns("買掛残高_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvKike.Columns("通貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvKike.Columns("買掛金額計").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvKike.Columns("買掛残高").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        DgvKike.Columns("備考1").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvKike.Columns("備考2").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvKike.Columns("更新日").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvKike.Columns("更新者").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter


        DgvKike.Columns("買掛日").DefaultCellStyle.Format = "d"

        PurchaseListLoad() '一覧表示

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblConditions.Text = "ExtractionCondition"
            Label1.Text = "SupplierName"
            Label4.Text = "SupplierCode"
            'Label8.Text = "AccountsPayableDate"
            Label7.Text = "AccountsPayableNumber"
            Label11.Text = "CustomerNumber"
            LblItemName.Text = "ItemName"
            LblSpec.Text = "Spec"
            Label10.Text = "DisplayFormat"

            ChkCancelData.Text = "IncludeCancelData"

            BtnAPSearch.Text = "Search"
            BtnAPCancel.Text = "CancelOfAccountsPayable"
            BtnAPView.Text = "AccountsPayableView"
            BtnBack.Text = "Back"
        End If
    End Sub

    '一覧表示
    Private Sub PurchaseListLoad()
        DgvKike.Visible = False

        ' 行や列を追加したり、セルに値を設定するときは、自動サイズ設定しない。
        DgvKike.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
        DgvKike.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None
        DgvKike.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

        '変数等
        Dim Sql As String = ""
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim curds As DataSet  'm25_currency
        Dim cur As String = ""

        '一覧クリア
        DgvKike.Rows.Clear()

        Try
            Sql = "SELECT"
            Sql += " t46.*, t20.ＶＡＴ"
            Sql += " FROM "
            Sql += " public.t46_kikehd t46 "

            Sql += " INNER JOIN "
            Sql += " t20_hattyu t20"
            Sql += " ON "
            Sql += " t46.会社コード = t20.会社コード "
            Sql += " AND "
            Sql += " t46.発注番号 = t20.発注番号"
            Sql += " AND "
            Sql += " t46.発注番号枝番 = t20.発注番号枝番"
            Sql += " AND "
            Sql += " t20.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED '発注取消されていないデータ

            Sql += " WHERE "
            Sql += " t46.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"

            Sql += viewSearchConditions() '抽出条件取得

            Sql += " ORDER BY "
            Sql += "t46.更新日 DESC, t46.買掛番号"

            ds = _db.selectDB(Sql, RS, reccnt)

            For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1

                '通貨の表示
                If IsDBNull(ds.Tables(RS).Rows(0)("通貨")) Then
                    cur = vbNullString
                Else
                    Sql = " and 採番キー = " & ds.Tables(RS).Rows(0)("通貨")
                    curds = getDsData("m25_currency", Sql)

                    cur = curds.Tables(RS).Rows(0)("通貨コード")
                End If


                DgvKike.Rows.Add()
                DgvKike.Rows(i).Cells("取消").Value = getDelKbnTxt(ds.Tables(RS).Rows(i)("取消区分"))
                DgvKike.Rows(i).Cells("買掛番号").Value = ds.Tables(RS).Rows(i)("買掛番号")
                If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                    DgvKike.Rows(i).Cells("買掛区分").Value = IIf(ds.Tables(RS).Rows(i)("買掛区分") = CommonConst.APC_KBN_DEPOSIT.ToString,
                                                                                                    CommonConst.APC_KBN_DEPOSIT_TXT_E,
                                                                                                    CommonConst.APC_KBN_NORMAL_TXT_E)
                Else
                    DgvKike.Rows(i).Cells("買掛区分").Value = IIf(ds.Tables(RS).Rows(i)("買掛区分") = CommonConst.APC_KBN_DEPOSIT.ToString,
                                                                                                    CommonConst.APC_KBN_DEPOSIT_TXT,
                                                                                                    CommonConst.APC_KBN_NORMAL_TXT)
                End If
                'DgvKike.Rows(i).Cells("買掛日").Value = ds.Tables(RS).Rows(i)("買掛日").ToShortDateString()
                DgvKike.Rows(i).Cells("買掛日").Value = ds.Tables(RS).Rows(i)("買掛日")
                DgvKike.Rows(i).Cells("仕入先請求番号").Value = ds.Tables(RS).Rows(i)("仕入先請求番号")
                DgvKike.Rows(i).Cells("客先番号").Value = ds.Tables(RS).Rows(i)("客先番号")
                DgvKike.Rows(i).Cells("発注番号").Value = ds.Tables(RS).Rows(i)("発注番号")
                DgvKike.Rows(i).Cells("発注番号枝番").Value = ds.Tables(RS).Rows(i)("発注番号枝番")
                DgvKike.Rows(i).Cells("仕入先コード").Value = ds.Tables(RS).Rows(i)("仕入先コード")
                DgvKike.Rows(i).Cells("仕入先名").Value = ds.Tables(RS).Rows(i)("仕入先名")

                Dim decShiire As Decimal = 0
                Call mGet_t21(ds.Tables(RS).Rows(i)("発注番号"), ds.Tables(RS).Rows(i)("発注番号枝番") _
                              , decShiire)
                Dim decVAT As Decimal = decShiire * ds.Tables(RS).Rows(i)("ＶＡＴ") / 100

                DgvKike.Rows(i).Cells("通貨_外貨").Value = cur
                DgvKike.Rows(i).Cells("仕入原価").Value = decShiire
                DgvKike.Rows(i).Cells("VAT").Value = decVAT
                DgvKike.Rows(i).Cells("買掛金額").Value = decShiire + decVAT
                DgvKike.Rows(i).Cells("買掛金額計_外貨").Value = ds.Tables(RS).Rows(i)("買掛金額計_外貨")  '既登録額
                DgvKike.Rows(i).Cells("未登録額").Value = （decShiire + decVAT） - ds.Tables(RS).Rows(i)("買掛金額計_外貨")

                'DgvKike.Rows(i).Cells("買掛残高_外貨").Value = ds.Tables(RS).Rows(i)("買掛残高_外貨")
                'DgvKike.Rows(i).Cells("通貨").Value =   setBaseCurrency()
                'DgvKike.Rows(i).Cells("買掛金額計").Value = ds.Tables(RS).Rows(i)("買掛金額計")
                'DgvKike.Rows(i).Cells("買掛残高").Value = ds.Tables(RS).Rows(i)("買掛残高")

                DgvKike.Rows(i).Cells("備考1").Value = ds.Tables(RS).Rows(i)("備考1")
                DgvKike.Rows(i).Cells("備考2").Value = ds.Tables(RS).Rows(i)("備考2")
                DgvKike.Rows(i).Cells("更新日").Value = ds.Tables(RS).Rows(i)("更新日")
                DgvKike.Rows(i).Cells("更新者").Value = ds.Tables(RS).Rows(i)("更新者")
            Next

            '自動でサイズを設定するのは、行や列を追加したり、セルに値を設定した後にする。
            DgvKike.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            DgvKike.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            DgvKike.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
            DgvKike.Visible = True

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

        DgvKike.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.DisplayedCells

    End Sub

    Private Sub mGet_t21(ByVal strHattyu As String, ByVal strNo As String _
                         , ByRef decShiire As Decimal)

        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""
        Dim ds_t20 As DataSet

        Sql = "SELECT"
        Sql += " *"
        Sql += " FROM "
        Sql += " t21_hattyu"

        Sql += " WHERE "
        Sql += " 会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " and 発注番号 = '" & strHattyu & "'"
        Sql += " and 発注番号枝番 = '" & strNo & "'"

        ds_t20 = _db.selectDB(Sql, RS, reccnt)


        For i As Integer = 0 To ds_t20.Tables(RS).Rows.Count - 1
            decShiire += ds_t20.Tables(RS).Rows(i)("仕入値_外貨") * ds_t20.Tables(RS).Rows(i)("仕入数量")
        Next

    End Sub


    '戻るボタン押下時
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    '検索ボタン押下時
    Private Sub BtnPurchaseSearch_Click(sender As Object, e As EventArgs) Handles BtnAPSearch.Click
        PurchaseListLoad() '一覧再表示
    End Sub

    '参照ボタン押下時
    Private Sub BtnBillingView_Click(sender As Object, e As EventArgs) Handles BtnAPView.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvKike.CurrentCell.RowIndex
        Dim No As String = DgvKike.Rows(RowIdx).Cells("発注番号").Value
        Dim Suffix As String = DgvKike.Rows(RowIdx).Cells("発注番号枝番").Value
        Dim Status As String = CommonConst.STATUS_VIEW
        Dim openForm As Form = Nothing
        openForm = New BillingManagement(_msgHd, _db, _langHd, Me, No, Suffix, Status)   '処理選択
        openForm.Show(Me)
    End Sub

    '「取消データを含める」チェックイベント
    Private Sub ChkCancelData_CheckedChanged(sender As Object, e As EventArgs) Handles ChkCancelData.CheckedChanged
        PurchaseListLoad() '一覧再表示
    End Sub

    '取消ボタン押下時
    Private Sub BtnBillingCancel_Click(sender As Object, e As EventArgs) Handles BtnAPCancel.Click

        '明細表示時、または対象データがない場合は取消操作不可能
        If DgvKike.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Return

        End If

        '取消済みデータは取消操作不可能
        If DgvKike.Rows(DgvKike.CurrentCell.RowIndex).Cells("取消").Value =
            IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_JPN, CommonConst.CANCEL_KBN_JPN_TXT, CommonConst.CANCEL_KBN_ENG_TXT) Then
            '取消データは選択できないアラートを出す
            _msgHd.dspMSG("cannotSelectTorikeshiData", frmC01F10_Login.loginValue.Language)
            Return
        End If

        '支払済みのデータはエラー
        Dim blnFlg As Boolean = mCheckShiharai()
        If blnFlg = False Then
            '取消データは選択できないアラートを出す
            _msgHd.dspMSG("cannotSelectTorikeshiData_shiharai", frmC01F10_Login.loginValue.Language)
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


    Private Function mCheckShiharai() As Boolean

        Dim reccnt As Integer = 0


        't49_shrikshihdに買掛番号があるか検索
        Dim strKaikake As String = DgvKike.Rows(DgvKike.CurrentCell.RowIndex).Cells("買掛番号").Value


        Dim Sql As String = "SELECT count(*) as 件数"

        Sql += " FROM t49_shrikshihd"

        Sql += " WHERE "
        Sql += "     会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " and 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
        Sql += " and 買掛番号 = '" & strKaikake & "'"


        Dim dsKaikake As DataSet = _db.selectDB(Sql, RS, reccnt)

        If dsKaikake.Tables(RS).Rows(0)("件数") = 0 Then
            '対象の入金データがない場合は正常終了
            mCheckShiharai = True
        Else
            '対象の入金データがあった場合は取消ができない
            mCheckShiharai = False
        End If


        dsKaikake.Dispose()


    End Function


    '選択データをもとに以下テーブル更新
    't25_nkinhd, t27_nkinkshihd, t23_skyuhd
    Private Sub updateData()

        Dim dtNow As String = UtilClass.formatDatetime(DateTime.Now)
        Dim Sql As String = ""

        'If ds.Tables(RS).Rows(0)("更新日") = DgvKike.Rows(DgvKike.CurrentCell.RowIndex).Cells("更新日").Value Then

        Sql = "UPDATE Public.t46_kikehd "
            Sql += "SET "

            Sql += "取消区分 = " & CommonConst.CANCEL_KBN_DISABLED
            Sql += " , 取消日 = '" & dtNow & "'"
            Sql += " , 更新日 = '" & dtNow & "'"
            Sql += " , 更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"

            Sql += "WHERE 会社コード ='" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND 買掛番号 ='" & DgvKike.Rows(DgvKike.CurrentCell.RowIndex).Cells("買掛番号").Value & "'"

            '買掛基本を更新
            _db.executeDB(Sql)

            PurchaseListLoad()

        'Else

        '画面を開いたときの日時とデータの日時が異なっていた場合
        'データが誰かに変更された旨を伝える
        '_msgHd.dspMSG("chkData", frmC01F10_Login.loginValue.Language)

        '表示データを更新
        'PurchaseListLoad()

        'End If

    End Sub

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

    'sqlで実行する文字列からシングルクォーテーションを文字コードにする
    Private Function escapeSql(ByVal prmSql As String) As String
        Dim sql As String = prmSql

        sql = sql.Replace("'"c, "''") 'シングルクォーテーションを置換

        Return Regex.Escape(sql)
        Return sql
    End Function

    Private Function actionChk() As Boolean
        '対象データがない場合は取消操作不可能
        If DgvKike.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)

            Return False

        Else

            Return True

        End If
    End Function

    '抽出条件取得    '抽出条件取得
    Private Function viewSearchConditions() As String
        Dim Sql As String = ""

        '抽出条件
        Dim supplierName As String = escapeSql(TxtSupplierName.Text)
        Dim supplierCode As String = escapeSql(TxtSupplierCode.Text)
        Dim sinceDate As String = UtilClass.strFormatDate(dtAPDateSince.Text)
        Dim untilDate As String = UtilClass.strFormatDate(dtAPDateUntil.Text)
        Dim sinceNum As String = escapeSql(TxtAPSince.Text)
        Dim poNum As String = escapeSql(TxtCustomerPO.Text)
        Dim itemName As String = UtilClass.escapeSql(TxtItemName.Text)
        Dim spec As String = UtilClass.escapeSql(TxtSpec.Text)

        If supplierName <> Nothing Then
            Sql += " AND "
            Sql += " t46.仕入先名 ILIKE '%" & supplierName & "%' "
        End If

        If supplierCode <> Nothing Then
            Sql += " AND "
            Sql += " t46.仕入先コード ILIKE '%" & supplierCode & "%' "
        End If

        If sinceDate <> Nothing Then
            Sql += " AND "
            Sql += " t46.買掛日 >= '" & sinceDate & "'"
        End If
        If untilDate <> Nothing Then
            Sql += " AND "
            Sql += " t46.買掛日 <= '" & untilDate & "'"
        End If

        If sinceNum <> Nothing Then
            Sql += " AND "
            Sql += " t46.買掛番号 ILIKE '%" & sinceNum & "%' "
        End If

        If poNum <> Nothing Then
            Sql += " AND "
            Sql += " t46.客先番号 ILIKE '%" & poNum & "%' "
        End If

        If itemName <> Nothing Then
            Sql += " AND "
            Sql += " t21.品名 ILIKE '%" & itemName & "%' "
        End If

        If spec <> Nothing Then
            Sql += " AND "
            Sql += " t21.型式 ILIKE '%" & spec & "%' "
        End If

        '取消データを含めない場合
        If ChkCancelData.Checked = False Then
            Sql += " AND "
            Sql += "t46.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
        End If

        Return Sql
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

    Private Sub AccountsPayableList_Activated(sender As Object, e As EventArgs) Handles Me.Activated

    End Sub
End Class