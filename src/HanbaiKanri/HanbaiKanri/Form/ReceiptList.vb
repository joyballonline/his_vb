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

Public Class ReceiptList
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
    Private ReceiptNo As String()
    Private ReceiptStatus As String = ""


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
        ReceiptStatus = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True
        DgvNyuko.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.DisplayedCells
    End Sub

    '画面表示時
    Private Sub ReceiptList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If ReceiptStatus = CommonConst.STATUS_CANCEL Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "CancelMode"
            Else
                LblMode.Text = "取消モード"
            End If

            BtnReceiptCancel.Visible = True
            BtnReceiptCancel.Location = New Point(997, 509)
        ElseIf ReceiptStatus = CommonConst.STATUS_VIEW Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "ViewMode"
            Else
                LblMode.Text = "参照モード"
            End If

            BtnReceiptView.Visible = True
            BtnReceiptView.Location = New Point(997, 509)
        End If

        '検索（Date）の初期値
        dtReceiptDateSince.Value = DateAdd("d", CommonConst.SINCE_DEFAULT_DAY, DateTime.Today)
        dtReceiptDateUntil.Value = DateTime.Today

        nyukoList()

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblConditions.Text = "ExtractionCondition"
            Label1.Text = "SupplierName"
            Label2.Text = "Address"
            Label3.Text = "PhoneNumber"
            Label4.Text = "SupplierCode"
            Label8.Text = "GoodsReceiptDate"
            Label7.Text = "GoodsReceiptNumber"
            Label6.Text = "SalesPersonInCharge"
            Label11.Text = "CustomerNumber"
            LblItemName.Text = "ItemName"
            LblSpec.Text = "Spec"
            Label10.Text = "DisplayFormat"
            RbtnSlip.Text = "UnitOfVoucher"

            RbtnDetails.Text = "UnitOfDetailData"
            RbtnDetails.Location = New Point(166, 196)

            ChkCancelData.Text = "IncludeCancelData"
            ChkCancelData.Location = New Point(556, 196)

            BtnSearch.Text = "Search"
            BtnReceiptCancel.Text = "CancelOfGoodsReceipt"
            BtnReceiptView.Text = "GoodsReceiptView"
            BtnBack.Text = "Back"
        End If
    End Sub

    '一覧表示処理
    Private Sub nyukoList(Optional ByRef prmRefStatus As String = "")
        Dim Status As String = prmRefStatus
        Dim Sql As String = ""
        Dim reccnt As Integer = 0 'DB用（デフォルト）

        '一覧クリア
        DgvNyuko.Rows.Clear()
        DgvNyuko.Columns.Clear()

        '伝票単位選択時
        If RbtnSlip.Checked Then

            '使用言語によって表示切替
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                DgvNyuko.Columns.Add("取消", "Cancel")
                DgvNyuko.Columns.Add("入庫番号", "GoodsReceiptNumber")
                DgvNyuko.Columns.Add("入庫日", "GoodsReceiptDate")
                DgvNyuko.Columns.Add("発注番号", "PurchaseOrderNumber")
                DgvNyuko.Columns.Add("発注番号枝番", "PurchaseOrderSubNumber")
                DgvNyuko.Columns.Add("客先番号", "CustomerNumber")
                DgvNyuko.Columns.Add("仕入先コード", "SupplierCode")
                DgvNyuko.Columns.Add("仕入先名", "SupplierName")
                DgvNyuko.Columns.Add("仕入先郵便番号", "PostalCode")
                DgvNyuko.Columns.Add("仕入先住所", "Address")
                DgvNyuko.Columns.Add("仕入先電話番号", "PhoneNumber")
                DgvNyuko.Columns.Add("仕入先ＦＡＸ", "FAX")
                DgvNyuko.Columns.Add("仕入先担当者名", "NameOfPIC")
                DgvNyuko.Columns.Add("仕入先担当者役職", "PositionPICSupplier")
                DgvNyuko.Columns.Add("営業担当者", "SalesPersonInCharge")
                DgvNyuko.Columns.Add("入力担当者", "PICForInputting")
                DgvNyuko.Columns.Add("備考", "Remarks")
                DgvNyuko.Columns.Add("登録日", "RegistrationDate")
            Else
                DgvNyuko.Columns.Add("取消", "取消")
                DgvNyuko.Columns.Add("入庫番号", "入庫番号")
                DgvNyuko.Columns.Add("入庫日", "入庫日")
                DgvNyuko.Columns.Add("発注番号", "発注番号")
                DgvNyuko.Columns.Add("発注番号枝番", "発注番号枝番")
                DgvNyuko.Columns.Add("客先番号", "客先番号")
                DgvNyuko.Columns.Add("仕入先コード", "仕入先コード")
                DgvNyuko.Columns.Add("仕入先名", "仕入先名")
                DgvNyuko.Columns.Add("仕入先郵便番号", "仕入先郵便番号")
                DgvNyuko.Columns.Add("仕入先住所", "仕入先住所")
                DgvNyuko.Columns.Add("仕入先電話番号", "仕入先電話番号")
                DgvNyuko.Columns.Add("仕入先ＦＡＸ", "仕入先ＦＡＸ")
                DgvNyuko.Columns.Add("仕入先担当者名", "仕入先担当者名")
                DgvNyuko.Columns.Add("仕入先担当者役職", "仕入先担当者役職")
                DgvNyuko.Columns.Add("営業担当者", "営業担当者")
                DgvNyuko.Columns.Add("入力担当者", "入力担当者")
                DgvNyuko.Columns.Add("備考", "備考")
                DgvNyuko.Columns.Add("登録日", "登録日")
            End If

            Try

                Sql = "SELECT"
                Sql += " t42.取消区分,t42.入庫番号,t42.入庫日,t42.発注番号,t42.発注番号枝番,t42.客先番号"
                Sql += ",t42.仕入先コード,t42.仕入先名,t42.仕入先郵便番号,t42.仕入先住所,t42.仕入先電話番号"
                Sql += ",t42.仕入先ＦＡＸ,t42.仕入先担当者名,t42.仕入先担当者役職"
                Sql += ",t42.営業担当者,t42.入力担当者,t42.備考,t42.登録日"

                Sql += " FROM "
                Sql += " public.t42_nyukohd t42 "

                Sql += " INNER JOIN "
                Sql += " t43_nyukodt t43"
                Sql += " ON "
                Sql += " t42.会社コード = t43.会社コード "
                Sql += " AND "
                Sql += " t42.入庫番号 = t43.入庫番号"

                Sql += " WHERE "
                Sql += " t42.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql += " AND "
                Sql += " t43.仕入区分 <> '" & CommonConst.Sire_KBN_Move.ToString & "'"

                Sql += viewSearchConditions() '抽出条件取得

                Sql += " GROUP BY "
                Sql += " t42.取消区分,t42.入庫番号,t42.入庫日,t42.発注番号,t42.発注番号枝番,t42.客先番号"
                Sql += ",t42.仕入先コード,t42.仕入先名,t42.仕入先郵便番号,t42.仕入先住所,t42.仕入先電話番号"
                Sql += ",t42.仕入先ＦＡＸ,t42.仕入先担当者名,t42.仕入先担当者役職"
                Sql += ",t42.営業担当者,t42.入力担当者,t42.備考,t42.登録日,t42.更新日"

                Sql += " ORDER BY "
                Sql += "t42.更新日 DESC, t42.入庫番号"

                ds = _db.selectDB(Sql, RS, reccnt)

                For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                    DgvNyuko.Rows.Add()
                    DgvNyuko.Rows(i).Cells("取消").Value = getDelKbnTxt(ds.Tables(RS).Rows(i)("取消区分"))
                    DgvNyuko.Rows(i).Cells("入庫番号").Value = ds.Tables(RS).Rows(i)("入庫番号")
                    DgvNyuko.Rows(i).Cells("入庫日").Value = ds.Tables(RS).Rows(i)("入庫日").ToShortDateString()
                    DgvNyuko.Rows(i).Cells("発注番号").Value = ds.Tables(RS).Rows(i)("発注番号")
                    DgvNyuko.Rows(i).Cells("発注番号枝番").Value = ds.Tables(RS).Rows(i)("発注番号枝番")
                    DgvNyuko.Rows(i).Cells("客先番号").Value = ds.Tables(RS).Rows(i)("客先番号")
                    DgvNyuko.Rows(i).Cells("仕入先コード").Value = ds.Tables(RS).Rows(i)("仕入先コード")
                    DgvNyuko.Rows(i).Cells("仕入先名").Value = ds.Tables(RS).Rows(i)("仕入先名")
                    DgvNyuko.Rows(i).Cells("仕入先郵便番号").Value = ds.Tables(RS).Rows(i)("仕入先郵便番号")
                    DgvNyuko.Rows(i).Cells("仕入先住所").Value = ds.Tables(RS).Rows(i)("仕入先住所")
                    DgvNyuko.Rows(i).Cells("仕入先電話番号").Value = ds.Tables(RS).Rows(i)("仕入先電話番号")
                    DgvNyuko.Rows(i).Cells("仕入先ＦＡＸ").Value = ds.Tables(RS).Rows(i)("仕入先ＦＡＸ")
                    DgvNyuko.Rows(i).Cells("仕入先担当者名").Value = ds.Tables(RS).Rows(i)("仕入先担当者名")
                    DgvNyuko.Rows(i).Cells("仕入先担当者役職").Value = ds.Tables(RS).Rows(i)("仕入先担当者役職")
                    DgvNyuko.Rows(i).Cells("営業担当者").Value = ds.Tables(RS).Rows(i)("営業担当者")
                    DgvNyuko.Rows(i).Cells("入力担当者").Value = ds.Tables(RS).Rows(i)("入力担当者")
                    DgvNyuko.Rows(i).Cells("備考").Value = ds.Tables(RS).Rows(i)("備考")
                    DgvNyuko.Rows(i).Cells("登録日").Value = ds.Tables(RS).Rows(i)("登録日")
                Next

            Catch ue As UsrDefException
                ue.dspMsg()
                Throw ue
            Catch ex As Exception
                'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
            End Try

        Else

            '明細単位選択時

            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                DgvNyuko.Columns.Add("取消", "Cancel")
                DgvNyuko.Columns.Add("入庫番号", "GoodsReceiptNumber")
                DgvNyuko.Columns.Add("行番号", "LineNumber")
                DgvNyuko.Columns.Add("仕入区分", "PurchasingClassification")
                DgvNyuko.Columns.Add("メーカー", "Manufacturer")
                DgvNyuko.Columns.Add("品名", "ItemName")
                DgvNyuko.Columns.Add("型式", "Spec")
                DgvNyuko.Columns.Add("仕入先名", "SupplierName")
                DgvNyuko.Columns.Add("入庫数量", "GoodsReceiptQuantity")
                DgvNyuko.Columns.Add("単位", "Unit")
                DgvNyuko.Columns.Add("仕入値", "PurchaseAmount")
                DgvNyuko.Columns.Add("備考", "Remarks")
                DgvNyuko.Columns.Add("更新者", "ModifiedBy")
            Else
                DgvNyuko.Columns.Add("取消", "取消")
                DgvNyuko.Columns.Add("入庫番号", "入庫番号")
                DgvNyuko.Columns.Add("行番号", "行番号")
                DgvNyuko.Columns.Add("仕入区分", "仕入区分")
                DgvNyuko.Columns.Add("メーカー", "メーカー")
                DgvNyuko.Columns.Add("品名", "品名")
                DgvNyuko.Columns.Add("型式", "型式")
                DgvNyuko.Columns.Add("仕入先名", "仕入先名")
                DgvNyuko.Columns.Add("入庫数量", "入庫数量")
                DgvNyuko.Columns.Add("単位", "単位")
                DgvNyuko.Columns.Add("仕入値", "仕入値")
                DgvNyuko.Columns.Add("備考", "備考")
                DgvNyuko.Columns.Add("更新者", "更新者")
            End If

            DgvNyuko.Columns("入庫数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvNyuko.Columns("仕入値").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            '数字形式
            DgvNyuko.Columns("入庫数量").DefaultCellStyle.Format = "N2"
            DgvNyuko.Columns("仕入値").DefaultCellStyle.Format = "N2"


            Try

                Sql = "SELECT"
                Sql += " t43.*, t42.取消区分"
                Sql += " FROM "
                Sql += " public.t43_nyukodt t43 "

                Sql += " INNER JOIN "
                Sql += " t42_nyukohd t42"
                Sql += " ON "
                Sql += " t43.会社コード = t42.会社コード "
                Sql += " AND "
                Sql += " t43.入庫番号 = t42.入庫番号"
                Sql += " WHERE "
                Sql += " t43.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql += " AND "
                Sql += " t43.仕入区分 <> '" & CommonConst.Sire_KBN_Move.ToString & "'"

                Sql += viewSearchConditions() '抽出条件取得

                Sql += " ORDER BY "
                Sql += "t43.更新日 DESC, t43.入庫番号, t43.行番号"

                ds = _db.selectDB(Sql, RS, reccnt)

                For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                    '汎用マスタから仕入区分名称取得
                    Dim sireKbn As DataSet = getDsHanyoData(CommonConst.FIXED_KEY_PURCHASING_CLASS, ds.Tables(RS).Rows(i)("仕入区分").ToString)

                    DgvNyuko.Rows.Add()
                    DgvNyuko.Rows(i).Cells("取消").Value = getDelKbnTxt(ds.Tables(RS).Rows(i)("取消区分"))
                    DgvNyuko.Rows(i).Cells("入庫番号").Value = ds.Tables(RS).Rows(i)("入庫番号")
                    DgvNyuko.Rows(i).Cells("行番号").Value = ds.Tables(RS).Rows(i)("行番号")
                    DgvNyuko.Rows(i).Cells("仕入区分").Value = ds.Tables(RS).Rows(i)("仕入区分")
                    DgvNyuko.Rows(i).Cells("仕入区分").Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                                                                sireKbn.Tables(RS).Rows(0)("文字２"),
                                                                sireKbn.Tables(RS).Rows(0)("文字１"))
                    DgvNyuko.Rows(i).Cells("メーカー").Value = ds.Tables(RS).Rows(i)("メーカー")
                    DgvNyuko.Rows(i).Cells("品名").Value = ds.Tables(RS).Rows(i)("品名")
                    DgvNyuko.Rows(i).Cells("型式").Value = ds.Tables(RS).Rows(i)("型式")
                    DgvNyuko.Rows(i).Cells("仕入先名").Value = ds.Tables(RS).Rows(i)("仕入先名")
                    DgvNyuko.Rows(i).Cells("入庫数量").Value = ds.Tables(RS).Rows(i)("入庫数量")
                    DgvNyuko.Rows(i).Cells("単位").Value = ds.Tables(RS).Rows(i)("単位")
                    DgvNyuko.Rows(i).Cells("仕入値").Value = ds.Tables(RS).Rows(i)("仕入値")
                    DgvNyuko.Rows(i).Cells("備考").Value = ds.Tables(RS).Rows(i)("備考")
                    DgvNyuko.Rows(i).Cells("更新者").Value = ds.Tables(RS).Rows(i)("更新者")
                Next

            Catch ue As UsrDefException
                ue.dspMsg()
                Throw ue
            Catch ex As Exception
                'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
            End Try
        End If

    End Sub

    '戻るボタン押下時
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    '表示形式変更時
    Private Sub RbtnDetails_CheckedChanged(sender As Object, e As EventArgs) Handles RbtnDetails.CheckedChanged
        nyukoList() '一覧再表示
    End Sub

    '取消データを含めないチェックイベント
    Private Sub ChkCancelData_CheckedChanged(sender As Object, e As EventArgs) Handles ChkCancelData.CheckedChanged
        nyukoList() '一覧再表示
    End Sub

    '検索ボタン押下時
    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        nyukoList() '一覧再表示
    End Sub

    '取消ボタン押下時
    Private Sub BtnSalesCancel_Click(sender As Object, e As EventArgs) Handles BtnReceiptCancel.Click
        Dim dtNow As DateTime = DateTime.Now

        '明細表示時、または対象データがない場合は取消操作不可能
        If RbtnDetails.Checked Or DgvNyuko.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("chkDetailsCancel", frmC01F10_Login.loginValue.Language)
            Return

        End If


        '取消済みデータは取消操作不可能
        If DgvNyuko.Rows(DgvNyuko.CurrentCell.RowIndex).Cells("取消").Value =
            IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_JPN, CommonConst.CANCEL_KBN_JPN_TXT, CommonConst.CANCEL_KBN_ENG_TXT) Then
            '取消データは選択できないアラートを出す
            _msgHd.dspMSG("cannotSelectTorikeshiData", frmC01F10_Login.loginValue.Language)
            Return
        End If


        '出庫済のデータはエラー
        Dim blnFlg As Boolean = mCheckSyuko()
        If blnFlg = False Then
            '取消データは選択できないアラートを出す
            _msgHd.dspMSG("cannotSelectTorikeshiData_syuko", frmC01F10_Login.loginValue.Language)
            Return
        End If


        '取消確認のアラート
        Dim result As DialogResult = _msgHd.dspMSG("confirmCancel", frmC01F10_Login.loginValue.Language)

        If result = DialogResult.Yes Then
            updateData() 'データ更新
        End If

    End Sub

    Private Function mCheckSyuko() As Boolean

        Dim reccnt As Integer = 0

        '入庫と結び付いた出庫データが存在するか検索する
        '発注番号で発注データを検索 → 受注番号で取消されていない出庫データを検索
        Dim strHatyuNo As String = DgvNyuko.Rows(DgvNyuko.CurrentCell.RowIndex).Cells("発注番号").Value
        Dim strEda As String = DgvNyuko.Rows(DgvNyuko.CurrentCell.RowIndex).Cells("発注番号枝番").Value


        Dim Sql As String = "SELECT t20.受注番号 as 受注1,t44.受注番号 as 受注2"

        Sql += " FROM t20_hattyu t20 left join t44_shukohd t44"
        Sql += " on t20.受注番号 = t44.受注番号 and t20.受注番号枝番 = t44.受注番号枝番"

        Sql += " WHERE "
        Sql += "     t20.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " and t20.発注番号 = '" & strHatyuNo & "'"
        Sql += " and t20.発注番号枝番 = '" & strEda & "'"
        Sql += " and t44.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED


        Dim dsNyukodt As DataSet = _db.selectDB(Sql, RS, reccnt)

        If dsNyukodt.Tables(RS).Rows.Count = 0 Then
            '対象の出庫データがない場合は正常終了
            mCheckSyuko = True
            Exit Function
        End If


        Dim strMoji = Convert.ToString(dsNyukodt.Tables(RS).Rows(0)("受注2"))
        If String.IsNullOrEmpty(strMoji) Then
            '対象の出庫データがない場合は正常終了
            mCheckSyuko = True
        Else
            '対象の出庫データがあった場合は入庫取消ができない
            mCheckSyuko = False
        End If

    End Function

    '選択データをもとに以下テーブル更新
    't20_hattyu, t21_hattyu
    Private Sub updateData()
        Dim dtNow As String = formatDatetime(DateTime.Now)
        Dim Sql As String = ""

        Try

            '発注データ
            Sql = " AND "
            Sql += "発注番号 ILIKE '" & DgvNyuko.Rows(DgvNyuko.CurrentCell.RowIndex).Cells("発注番号").Value & "'"
            Sql += " AND "
            Sql += "発注番号枝番 ILIKE '" & DgvNyuko.Rows(DgvNyuko.CurrentCell.RowIndex).Cells("発注番号枝番").Value & "'"

            Dim dsHattyudt As DataSet = getDsData("t21_hattyu", Sql)

            '入庫データ
            Sql = " AND"
            Sql += " 入庫番号"
            Sql += "='"
            Sql += DgvNyuko.Rows(DgvNyuko.CurrentCell.RowIndex).Cells("入庫番号").Value
            Sql += "'"

            Dim dsNyukodt As DataSet = getDsData("t43_nyukodt", Sql)

            Sql = "UPDATE "
            Sql += "Public."
            Sql += "t42_nyukohd "
            Sql += "SET "

            Sql += "取消区分 = " & CommonConst.CANCEL_KBN_DISABLED '取消区分：1
            Sql += ", "
            Sql += "取消日"
            Sql += " = '"
            Sql += UtilClass.strFormatDate(dtNow)
            Sql += "', "
            Sql += "更新日"
            Sql += " = '"
            Sql += UtilClass.strFormatDate(dtNow)
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
            Sql += " 入庫番号"
            Sql += "='"
            Sql += DgvNyuko.Rows(DgvNyuko.CurrentCell.RowIndex).Cells("入庫番号").Value
            Sql += "' "

            _db.executeDB(Sql)

            Sql = "UPDATE "
            Sql += "Public."
            Sql += "t43_nyukodt "
            Sql += "SET "

            Sql += "更新日"
            Sql += " = '"
            Sql += UtilClass.strFormatDate(dtNow)
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
            Sql += " 入庫番号"
            Sql += "='"
            Sql += DgvNyuko.Rows(DgvNyuko.CurrentCell.RowIndex).Cells("入庫番号").Value
            Sql += "' "

            _db.executeDB(Sql)

            '発注データを更新する
            For i As Integer = 0 To dsHattyudt.Tables(RS).Rows.Count - 1
                For x As Integer = 0 To dsNyukodt.Tables(RS).Rows.Count - 1

                    '行番号が一致したら
                    If dsHattyudt.Tables(RS).Rows(i)("行番号") = dsNyukodt.Tables(RS).Rows(x)("行番号") Then
                        Dim calShukko As Integer = dsHattyudt.Tables(RS).Rows(i)("入庫数") - dsNyukodt.Tables(RS).Rows(x)("入庫数量")
                        Dim calUnShukko As Integer = dsHattyudt.Tables(RS).Rows(i)("未入庫数") + dsNyukodt.Tables(RS).Rows(x)("入庫数量")

                        Sql = "update t21_hattyu set "
                        Sql += "入庫数 = '" & calShukko & "'"
                        Sql += ",未入庫数 = '" & calUnShukko & "'"
                        Sql += ",更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
                        Sql += " where 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                        Sql += " AND "
                        Sql += "発注番号 ILIKE '" & DgvNyuko.Rows(DgvNyuko.CurrentCell.RowIndex).Cells("発注番号").Value & "'"
                        Sql += " AND "
                        Sql += "発注番号枝番 ILIKE '" & DgvNyuko.Rows(DgvNyuko.CurrentCell.RowIndex).Cells("発注番号枝番").Value & "'"
                        Sql += " AND "
                        Sql += "行番号 = '" & dsHattyudt.Tables(RS).Rows(i)("行番号") & "'"

                        _db.executeDB(Sql)

                        Sql = "update t20_hattyu set "
                        Sql += "更新日 = '" & UtilClass.strFormatDate(dtNow) & "'"
                        Sql += ",更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
                        Sql += " where 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                        Sql += " AND "
                        Sql += "発注番号 ILIKE '" & DgvNyuko.Rows(DgvNyuko.CurrentCell.RowIndex).Cells("発注番号").Value & "'"
                        Sql += " AND "
                        Sql += "発注番号枝番 ILIKE '" & DgvNyuko.Rows(DgvNyuko.CurrentCell.RowIndex).Cells("発注番号枝番").Value & "'"
                        Sql += " AND "
                        Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED '取消区分=0

                        _db.executeDB(Sql)

                    End If

                Next

            Next

            Sql = "UPDATE "
            Sql += "Public."
            Sql += "t70_inout "
            Sql += "SET "

            Sql += "取消日"
            Sql += " = '"
            Sql += UtilClass.strFormatDate(dtNow)
            Sql += "', "
            Sql += "取消区分"
            Sql += " = '"
            Sql += CommonConst.CANCEL_KBN_DISABLED.ToString
            Sql += "', "
            Sql += "更新日"
            Sql += " = '"
            Sql += UtilClass.strFormatDate(dtNow)
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
            Sql += " 伝票番号"
            Sql += "='"
            Sql += DgvNyuko.Rows(DgvNyuko.CurrentCell.RowIndex).Cells("入庫番号").Value
            Sql += "' "

            _db.executeDB(Sql)

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

        '一覧再表示
        nyukoList()

    End Sub

    '参照ボタン押下時
    Private Sub BtnSalesView_Click(sender As Object, e As EventArgs) Handles BtnReceiptView.Click

        '明細表示時、または対象データがない場合は取消操作不可能
        If RbtnDetails.Checked Or DgvNyuko.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Return

        End If

        Dim RowIdx As Integer
        RowIdx = Me.DgvNyuko.CurrentCell.RowIndex
        Dim No As String = DgvNyuko.Rows(RowIdx).Cells("発注番号").Value
        Dim Suffix As String = DgvNyuko.Rows(RowIdx).Cells("発注番号枝番").Value
        Dim Status As String = CommonConst.STATUS_VIEW

        Dim denpyoNo As String = "00000000000000"
        Dim denpyoEda As String = "00"

        If No = denpyoNo And Suffix = denpyoEda Then
            No = DgvNyuko.Rows(RowIdx).Cells("入庫番号").Value
        End If

        Dim openForm As Form = Nothing
        openForm = New Receipt(_msgHd, _db, _langHd, No, Suffix, Status)   '処理選択
        openForm.Show(Me)
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
        If DgvNyuko.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)

            Return False

        Else

            Return True

        End If
    End Function

    '抽出条件取得
    Private Function viewSearchConditions() As String
        Dim Sql As String = ""

        '抽出条件
        Dim supplierName As String = escapeSql(TxtSupplierName.Text)
        Dim supplierAddress As String = escapeSql(TxtAddress.Text)
        Dim supplierTel As String = escapeSql(TxtTel.Text)
        Dim supplierCode As String = escapeSql(TxtSupplierCode.Text)
        Dim sinceDate As String = UtilClass.strFormatDate(dtReceiptDateSince.Text)
        Dim untilDate As String = UtilClass.strFormatDate(dtReceiptDateUntil.Text)
        Dim sinceNum As String = escapeSql(TxtReceiptSince.Text)
        Dim salesName As String = escapeSql(TxtSales.Text)
        Dim poNum As String = escapeSql(TxtCustomerPO.Text)
        Dim itemName As String = UtilClass.escapeSql(TxtItemName.Text)
        Dim spec As String = UtilClass.escapeSql(TxtSpec.Text)

        If supplierName <> Nothing Then
            Sql += " AND "
            Sql += " t42.仕入先名 ILIKE '%" & supplierName & "%' "
        End If

        If supplierAddress <> Nothing Then
            Sql += " AND "
            Sql += " t42.仕入先住所 ILIKE '%" & supplierAddress & "%' "
        End If

        If supplierTel <> Nothing Then
            Sql += " AND "
            Sql += " t42.仕入先電話番号 ILIKE '%" & supplierTel & "%' "
        End If

        If supplierCode <> Nothing Then
            Sql += " AND "
            Sql += " t42.仕入先コード ILIKE '%" & supplierCode & "%' "
        End If

        If sinceDate <> Nothing Then
            Sql += " AND "
            Sql += " t42.入庫日 >= '" & sinceDate & "'"
        End If
        If untilDate <> Nothing Then
            Sql += " AND "
            Sql += " t42.入庫日 <= '" & untilDate & "'"
        End If

        If sinceNum <> Nothing Then
            Sql += " AND "
            Sql += " t42.入庫番号 ILIKE '%" & sinceNum & "%' "
        End If

        If poNum <> Nothing Then
            Sql += " AND "
            Sql += " t42.営業担当者 ILIKE '%" & salesName & "%' "
        End If

        If poNum <> Nothing Then
            Sql += " AND "
            Sql += " t42.客先番号 ILIKE '%" & salesName & "%' "
        End If

        If itemName <> Nothing Then
            Sql += " AND "
            Sql += " t43.品名 ILIKE '%" & itemName & "%' "
        End If

        If spec <> Nothing Then
            Sql += " AND "
            Sql += " t43.型式 ILIKE '%" & spec & "%' "
        End If

        '取消データを含めない場合
        If ChkCancelData.Checked = False Then
            Sql += " AND "
            Sql += "t42.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
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

End Class