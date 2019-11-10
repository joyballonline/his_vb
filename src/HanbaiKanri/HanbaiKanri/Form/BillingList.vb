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
        DgvBilling.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.DisplayedCells
    End Sub

    Private Sub BillingList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If _status = CommonConst.STATUS_VIEW Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "ViewMode"
            Else
                LblMode.Text = "参照モード"
            End If

            'BtnBillingView.Visible = True
            'BtnBillingView.Location = New Point(997, 677)
        ElseIf _status = CommonConst.STATUS_CANCEL Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "CancelMode"
            Else
                LblMode.Text = "取消モード"
            End If

            BtnBillingCancel.Visible = True
            BtnBillingCancel.Location = New Point(997, 509)
        End If

        '検索（Date）の初期値
        dtBillingDateSince.Value = DateAdd("d", CommonConst.SINCE_DEFAULT_DAY, DateTime.Today)
        dtBillingDateUntil.Value = DateTime.Today

        '請求一覧を表示
        PurchaseListLoad()

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblConditions.Text = "ExtractionCondition"
            Label1.Text = "CustomerName"
            Label4.Text = "CustomerCode"
            Label8.Text = "BillingDate"
            Label7.Text = "InvoiceNumber"
            Label11.Text = "CustomerNumber"
            LblItemName.Text = "ItemName"
            LblSpec.Text = "Spec"

            Label10.Text = "DisplayFormat"
            ChkCancelData.Text = "IncludeCancelData"

            BtnBillingSearch.Text = "Search"
            BtnBillingCancel.Text = "CancelOfInvoice"
            BtnBillingView.Text = "BillingDataView"
            BtnBack.Text = "Back"
        End If
    End Sub

    '表示処理
    Private Sub PurchaseListLoad()

        '一覧クリア
        DgvBilling.Rows.Clear()
        DgvBilling.Columns.Clear()

        Dim Sql As String = ""
        Dim reccnt As Integer = 0 'DB用（デフォルト）

        Dim curds As DataSet  'm25_currency
        Dim cur As String


        Try

            Sql = "SELECT t23.* FROM  public.t23_skyuhd t23 "

            Sql += " INNER JOIN t10_cymnhd t10"
            Sql += " ON t23.会社コード = t10.会社コード "
            Sql += "  AND t23.受注番号 = t10.受注番号"
            Sql += "  AND t23.受注番号枝番 = t10.受注番号枝番"
            Sql += "  AND t10.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED '受注取消されていないデータ

            Sql += " WHERE t23.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

            Sql += viewSearchConditions() '抽出条件取得

            Sql += " ORDER BY t23.更新日 DESC"

            ds = _db.selectDB(Sql, RS, reccnt)

            '英語の表記
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                DgvBilling.Columns.Add("取消", "Cancel")
                DgvBilling.Columns.Add("請求番号", "InvoiceNumber")
                DgvBilling.Columns.Add("請求区分", "BillingClassification")
                DgvBilling.Columns.Add("請求日", "BillingDate")
                DgvBilling.Columns.Add("客先番号", "CustomerNumber")
                DgvBilling.Columns.Add("受注番号", "JobOrderNumber")
                DgvBilling.Columns.Add("受注番号枝番", "JobOrderSubNumber")
                DgvBilling.Columns.Add("得意先コード", "CustomerCode")
                DgvBilling.Columns.Add("得意先名", "CustomerName")
                DgvBilling.Columns.Add("通貨_外貨", "Currency")
                DgvBilling.Columns.Add("請求金額計_外貨", "TotalBillingAmount")
                DgvBilling.Columns.Add("売掛残高_外貨", "AccountsReceivableBalance")
                DgvBilling.Columns.Add("通貨", "Currency")
                DgvBilling.Columns.Add("請求金額計", "TotalBillingAmount")
                DgvBilling.Columns.Add("売掛残高", "AccountsReceivableBalance")
                DgvBilling.Columns.Add("備考1", "Remarks1")
                DgvBilling.Columns.Add("備考2", "Remarks2")
                DgvBilling.Columns.Add("更新日", "ModifiedDate")
                DgvBilling.Columns.Add("更新者", "ModifiedBy")
            Else
                DgvBilling.Columns.Add("取消", "取消")
                DgvBilling.Columns.Add("請求番号", "請求番号")
                DgvBilling.Columns.Add("請求区分", "請求区分")
                DgvBilling.Columns.Add("請求日", "請求日")
                DgvBilling.Columns.Add("客先番号", "客先番号")
                DgvBilling.Columns.Add("受注番号", "受注番号")
                DgvBilling.Columns.Add("受注番号枝番", "受注番号枝番")
                DgvBilling.Columns.Add("得意先コード", "得意先コード")
                DgvBilling.Columns.Add("得意先名", "得意先名")
                DgvBilling.Columns.Add("通貨_外貨", "通貨")
                DgvBilling.Columns.Add("請求金額計_外貨", "請求金額計(外貨)")
                DgvBilling.Columns.Add("売掛残高_外貨", "売掛残高(外貨)")
                DgvBilling.Columns.Add("通貨", "通貨")
                DgvBilling.Columns.Add("請求金額計", "請求金額計")
                DgvBilling.Columns.Add("売掛残高", "売掛残高")
                DgvBilling.Columns.Add("備考1", "備考1")
                DgvBilling.Columns.Add("備考2", "備考2")
                DgvBilling.Columns.Add("更新日", "更新日")
                DgvBilling.Columns.Add("更新者", "更新者")
            End If

            DgvBilling.Columns("請求金額計").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvBilling.Columns("売掛残高").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            '数字形式
            DgvBilling.Columns("請求金額計_外貨").DefaultCellStyle.Format = "N2"
            DgvBilling.Columns("売掛残高_外貨").DefaultCellStyle.Format = "N2"
            DgvBilling.Columns("請求金額計").DefaultCellStyle.Format = "N2"
            DgvBilling.Columns("売掛残高").DefaultCellStyle.Format = "N2"

            For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1

                If IsDBNull(ds.Tables(RS).Rows(i)("通貨")) Then
                    cur = vbNullString
                Else
                    Sql = " and 採番キー = " & ds.Tables(RS).Rows(i)("通貨")
                    curds = getDsData("m25_currency", Sql)

                    cur = curds.Tables(RS).Rows(0)("通貨コード")
                End If

                DgvBilling.Rows.Add()
                DgvBilling.Rows(i).Cells("請求番号").Value = ds.Tables(RS).Rows(i)("請求番号")

                If frmC01F10_Login.loginValue.Language = "ENG" Then
                    DgvBilling.Rows(i).Cells("請求区分").Value = IIf(
                        ds.Tables(RS).Rows(i)("請求区分") = CommonConst.BILLING_KBN_DEPOSIT,
                        CommonConst.BILLING_KBN_DEPOSIT_TXT_E,
                        CommonConst.BILLING_KBN_NORMAL_TXT_E
                        )
                Else
                    DgvBilling.Rows(i).Cells("請求区分").Value = IIf(
                        ds.Tables(RS).Rows(i)("請求区分") = CommonConst.BILLING_KBN_DEPOSIT,
                        CommonConst.BILLING_KBN_DEPOSIT_TXT,
                        CommonConst.BILLING_KBN_NORMAL_TXT
                        )
                End If

                DgvBilling.Rows(i).Cells("取消").Value = getDelKbnTxt(ds.Tables(RS).Rows(i)("取消区分"))
                DgvBilling.Rows(i).Cells("請求日").Value = ds.Tables(RS).Rows(i)("請求日").ToShortDateString()
                DgvBilling.Rows(i).Cells("客先番号").Value = ds.Tables(RS).Rows(i)("客先番号")
                DgvBilling.Rows(i).Cells("受注番号").Value = ds.Tables(RS).Rows(i)("受注番号")
                DgvBilling.Rows(i).Cells("受注番号枝番").Value = ds.Tables(RS).Rows(i)("受注番号枝番")
                DgvBilling.Rows(i).Cells("得意先コード").Value = ds.Tables(RS).Rows(i)("得意先コード")
                DgvBilling.Rows(i).Cells("得意先名").Value = ds.Tables(RS).Rows(i)("得意先名")
                DgvBilling.Rows(i).Cells("通貨_外貨").Value = cur
                DgvBilling.Rows(i).Cells("請求金額計_外貨").Value = ds.Tables(RS).Rows(i)("請求金額計_外貨")
                DgvBilling.Rows(i).Cells("売掛残高_外貨").Value = ds.Tables(RS).Rows(i)("売掛残高_外貨")
                DgvBilling.Rows(i).Cells("通貨").Value = setBaseCurrency()
                DgvBilling.Rows(i).Cells("請求金額計").Value = ds.Tables(RS).Rows(i)("請求金額計")
                DgvBilling.Rows(i).Cells("売掛残高").Value = ds.Tables(RS).Rows(i)("売掛残高")
                DgvBilling.Rows(i).Cells("備考1").Value = ds.Tables(RS).Rows(i)("備考1")
                DgvBilling.Rows(i).Cells("備考2").Value = ds.Tables(RS).Rows(i)("備考2")
                DgvBilling.Rows(i).Cells("更新日").Value = ds.Tables(RS).Rows(i)("更新日")
                DgvBilling.Rows(i).Cells("更新者").Value = ds.Tables(RS).Rows(i)("更新者")
            Next

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

    '検索ボタン押下時
    Private Sub BtnPurchaseSearch_Click(sender As Object, e As EventArgs) Handles BtnBillingSearch.Click
        PurchaseListLoad() '一覧再表示
    End Sub

    '参照ボタン押下時
    Private Sub BtnBillingView_Click(sender As Object, e As EventArgs) Handles BtnBillingView.Click
        '実行できるデータがあるかチェック
        If actionChk() = False Then
            Return
        End If

        Dim RowIdx As Integer
        RowIdx = Me.DgvBilling.CurrentCell.RowIndex
        Dim No As String = DgvBilling.Rows(RowIdx).Cells("受注番号").Value
        Dim Suffix As String = DgvBilling.Rows(RowIdx).Cells("受注番号枝番").Value
        Dim Status As String = CommonConst.STATUS_VIEW
        Dim openForm As Form = Nothing
        openForm = New BillingManagement(_msgHd, _db, _langHd, Me, No, Suffix, Status)   '処理選択
        openForm.Show(Me)
    End Sub

    '取消データチェックイベント
    Private Sub ChkCancelData_CheckedChanged(sender As Object, e As EventArgs) Handles ChkCancelData.CheckedChanged
        PurchaseListLoad() '一覧再表示
    End Sub

    '取消ボタン押下時
    Private Sub BtnBillingCancel_Click(sender As Object, e As EventArgs) Handles BtnBillingCancel.Click
        '実行できるデータがあるかチェック
        If actionChk() = False Then
            Return
        End If

        '取消済みデータは取消操作不可能
        If DgvBilling.Rows(DgvBilling.CurrentCell.RowIndex).Cells("取消").Value =
            IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_JPN, CommonConst.CANCEL_KBN_JPN_TXT, CommonConst.CANCEL_KBN_ENG_TXT) Then
            '取消データは選択できないアラートを出す
            _msgHd.dspMSG("cannotSelectTorikeshiData", frmC01F10_Login.loginValue.Language)
            Return
        End If

        '入金済みのデータはエラー
        Dim blnFlg As Boolean = mCheckNyukin()
        If blnFlg = False Then
            '取消データは選択できないアラートを出す
            _msgHd.dspMSG("cannotSelectTorikeshiData_nyukin", frmC01F10_Login.loginValue.Language)
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


    Private Function mCheckNyukin() As Boolean

        Dim reccnt As Integer = 0


        't80_shiwakenyuに請求番号があるか検索
        Dim strSeikyu As String = DgvBilling.Rows(DgvBilling.CurrentCell.RowIndex).Cells("請求番号").Value


        Dim Sql As String = "SELECT count(*) as 件数"

        Sql += " FROM t80_shiwakenyu"

        Sql += " WHERE "
        Sql += "     会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " and 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
        Sql += " and 請求番号 = '" & strSeikyu & "'"


        Dim dsSeikyu As DataSet = _db.selectDB(Sql, RS, reccnt)

        If dsSeikyu.Tables(RS).Rows(0)("件数") = 0 Then
            '対象の入金データがない場合は正常終了
            mCheckNyukin = True
        Else
            '対象の入金データがあった場合は取消ができない
            mCheckNyukin = False
        End If


        dsSeikyu.Dispose()


    End Function

    Private Function viewSearchConditions() As String
        Dim Sql As String = ""

        '抽出条件
        Dim customerName As String = UtilClass.escapeSql(TxtCustomerName.Text)
        Dim customerCode As String = UtilClass.escapeSql(TxtCustomerCode.Text)
        Dim sinceDate As String = UtilClass.strFormatDate(dtBillingDateSince.Text)
        Dim untilDate As String = UtilClass.strFormatDate(dtBillingDateUntil.Text)
        Dim sinceNum As String = escapeSql(TxtBillingNoSince.Text)
        Dim poNum As String = UtilClass.escapeSql(TxtCustomerPO.Text)
        Dim itemName As String = UtilClass.escapeSql(TxtItemName.Text)
        Dim spec As String = UtilClass.escapeSql(TxtSpec.Text)

        If customerName <> Nothing Then
            Sql += " AND "
            Sql += " t23.得意先名 ILIKE '%" & customerName & "%' "
        End If

        If customerCode <> Nothing Then
            Sql += " AND "
            Sql += " t23.得意先コード ILIKE '%" & customerCode & "%' "
        End If

        If sinceDate <> Nothing Then
            Sql += " AND "
            Sql += " t23.請求日 >= '" & sinceDate & "'"
        End If
        If untilDate <> Nothing Then
            Sql += " AND "
            Sql += " t23.請求日 <= '" & untilDate & "'"
        End If

        If sinceNum <> Nothing Then
            Sql += " AND "
            Sql += " t23.請求番号 ILIKE '%" & sinceNum & "%' "
        End If

        If poNum <> Nothing Then
            Sql += " AND "
            Sql += " t23.客先番号 ILIKE '%" & poNum & "%' "
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
            Sql += "t23.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
        End If

        Return Sql
    End Function

    '選択データをもとに以下テーブル更新
    't23_skyuhd
    Private Sub updateData()
        Dim dtNow As String = formatDatetime(DateTime.Now)
        Dim Sql As String = ""
        Dim ds As DataSet

        Sql = " AND 請求番号 ='" & DgvBilling.Rows(DgvBilling.CurrentCell.RowIndex).Cells("請求番号").Value & "'"

        '画面を開いた時から対象データに対して更新がされていないかどうか確認
        ds = getDsData("t23_skyuhd", Sql)

        If ds.Tables(RS).Rows(0)("更新日") = DgvBilling.Rows(DgvBilling.CurrentCell.RowIndex).Cells("更新日").Value Then

            Sql = "UPDATE Public.t23_skyuhd "
            Sql += "SET "

            Sql += "取消区分 = '" & CommonConst.CANCEL_KBN_DISABLED & "'"
            Sql += ", 取消日 = current_date"
            Sql += ", 更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
            Sql += ", 更新日 = current_timestamp"

            Sql += " WHERE 会社コード ='" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND 請求番号 = '" & DgvBilling.Rows(DgvBilling.CurrentCell.RowIndex).Cells("請求番号").Value & "'"

            '請求基本を更新
            _db.executeDB(Sql)

            '表示データを更新
            PurchaseListLoad()

        Else

            '画面を開いたときの日時とデータの日時が異なっていた場合
            'データが誰かに変更された旨を伝える
            _msgHd.dspMSG("chkData", frmC01F10_Login.loginValue.Language)

            '表示データを更新
            PurchaseListLoad()


        End If

    End Sub

    'sqlで実行する文字列からシングルクォーテーションを文字コードにする
    Private Function escapeSql(ByVal prmSql As String) As String
        Dim sql As String = prmSql

        sql = sql.Replace("'"c, "''") 'シングルクォーテーションを置換

        Return Regex.Escape(sql)
        Return sql
    End Function

    Private Function actionChk() As Boolean
        '対象データがない場合は取消操作不可能
        If DgvBilling.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)

            Return False

        Else

            Return True

        End If
    End Function

    'param1：String テーブル名
    'param2：String 詳細条件
    'Return: DataSet
    Private Function getDsData(ByVal tableName As String, Optional ByRef txtParam As String = "") As DataSet
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        Sql += "SELECT * FROM public." & tableName
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += txtParam
        Return _db.selectDB(Sql, RS, reccnt)
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