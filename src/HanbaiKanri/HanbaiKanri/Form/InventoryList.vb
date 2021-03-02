'------------------------------------------------------------------------------------------------------
'改訂履歴
'2020.02.28
'在庫管理区分、在庫表示区分によるリスト表示方式の改訂（区分を会社マスタに実装するまで変数で固定割り当てする）
'------------------------------------------------------------------------------------------------------


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
Imports Microsoft.Office.Interop
Imports System.IO

Public Class InventoryList
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
    Private _langHd As UtilLangHandler
    Private _db As UtilDBIf
    Private _parentForm As Form
    'Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ
    Private CompanyCode As String = ""
    Private OrderNo As String()
    Private OrderStatus As String = ""
    Private InventoryControl As String = "3"                 '倉庫、入出庫種別を管理初期値とする
    Private InventoryViewer As String = "7"                  '倉庫、入出庫種別、ロケーションを表示初期値とする



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
        DgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.DisplayedCells
    End Sub

    '画面表示時
    Private Sub MstHanyoue_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        LblMode.Text = "参照モード"

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblMode.Text = OrderStatus & " Mode"

            rbAll.Text = "By Product"
            rbWarehouse.Text = "By Warehouse"
            rbSyubetsu.Text = "By StorageType"


            BtnExcelOutput.Text = "ExcelOutput"
            BtnBack.Text = "Back"

        End If

        makeTableLayoutPanel1()     'ラジオボタンコントロールの表示制御
        ''makeDgvList()               'リスト列の表示制御

        getList()                   '一覧表示
        makeZaikoKBNLayout()        '在庫管理区分、表示区分により最終的な表示方法を決定する

    End Sub

    '商品別の見出しセット
    ' 2020.03.02 商品別見出しセットを表示上の初期値とする
    '            初期値でデータグリッドを生成した後に、在庫表示区分を適用して表示列、順序を決定する
    Private Sub setProductHd()

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then

            DgvList.Columns.Add("メーカー", "Manufacturer")
            DgvList.Columns.Add("品名", "ItemName")
            DgvList.Columns.Add("型式", "Spec")
            DgvList.Columns.Add("倉庫", "Warehouse")
            DgvList.Columns.Add("入出庫種別", "StorageType")
            DgvList.Columns.Add("ロケ番号", "LocationNo")
            DgvList.Columns.Add("製造番号", "PO")
            DgvList.Columns.Add("伝票番号", "CustomerNumber")
            DgvList.Columns.Add("在庫数", "StockQuantity")
            DgvList.Columns.Add("単価（入庫単価）", "UnitPrice (ReceiptUnitPrice)")
            DgvList.Columns.Add("最終入庫日", "LastReceiptDate")
            DgvList.Columns.Add("最終出庫日", "LastDeliveryDate")

            DgvList.Columns.Add("仕入先名", "SupplierName")
            DgvList.Columns.Add("仕入先請求番号", "SupplierInvoiceNo")

        Else
            DgvList.Columns.Add("メーカー", "メーカー")
            DgvList.Columns.Add("品名", "品名")
            DgvList.Columns.Add("型式", "型式")
            DgvList.Columns.Add("倉庫", "倉庫")
            DgvList.Columns.Add("入出庫種別", "入出庫種別")
            DgvList.Columns.Add("ロケ番号", "ロケ番号")
            DgvList.Columns.Add("製造番号", "製造番号")
            DgvList.Columns.Add("伝票番号", "伝票番号")
            DgvList.Columns.Add("在庫数", "在庫数")
            DgvList.Columns.Add("単価（入庫単価）", "単価（入庫単価）")
            DgvList.Columns.Add("最終入庫日", "最終入庫日")
            DgvList.Columns.Add("最終出庫日", "最終出庫日")

            DgvList.Columns.Add("仕入先名", "仕入先名")
            DgvList.Columns.Add("仕入先請求番号", "仕入先請求番号")

        End If

        DgvList.Columns("在庫数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("単価（入庫単価）").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        '数字形式
        DgvList.Columns("在庫数").DefaultCellStyle.Format = "N2"
        DgvList.Columns("単価（入庫単価）").DefaultCellStyle.Format = "N2"

    End Sub

    '倉庫別の見出しセット
    '2020.03.02 この見出しは使用しない
    Private Sub setWarehouseHd()

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then

            DgvList.Columns.Add("倉庫", "Warehouse")
            DgvList.Columns.Add("メーカー", "Manufacturer")
            DgvList.Columns.Add("品名", "ItemName")
            DgvList.Columns.Add("型式", "Spec")
            DgvList.Columns.Add("在庫数", "StockQuantity")
            DgvList.Columns.Add("入出庫種別", "StorageType")
            DgvList.Columns.Add("単価（入庫単価）", "UnitPrice (ReceiptUnitPrice)")
            DgvList.Columns.Add("最終入庫日", "LastReceiptDate")
            DgvList.Columns.Add("最終出庫日", "LastDeliveryDate")

            DgvList.Columns.Add("仕入先名", "SupplierName")
            DgvList.Columns.Add("仕入先請求番号", "SupplierInvoiceNo")

        Else
            DgvList.Columns.Add("倉庫", "倉庫")
            DgvList.Columns.Add("メーカー", "メーカー")
            DgvList.Columns.Add("品名", "品名")
            DgvList.Columns.Add("型式", "型式")
            DgvList.Columns.Add("在庫数", "在庫数")
            DgvList.Columns.Add("入出庫種別", "入出庫種別")
            DgvList.Columns.Add("単価（入庫単価）", "単価（入庫単価）")
            DgvList.Columns.Add("最終入庫日", "最終入庫日")
            DgvList.Columns.Add("最終出庫日", "最終出庫日")

            DgvList.Columns.Add("仕入先名", "仕入先名")
            DgvList.Columns.Add("仕入先請求番号", "SupplierInvoiceNo")

        End If

        DgvList.Columns("在庫数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("単価（入庫単価）").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


        '数字形式
        DgvList.Columns("在庫数").DefaultCellStyle.Format = "N2"
        DgvList.Columns("単価（入庫単価）").DefaultCellStyle.Format = "N2"

    End Sub

    Private Sub getList()
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""
        Dim SqlItem As String = ""      'SQL文抽出項目群
        Dim SqlJoin As String = ""      'SQL文連結項目群
        Dim SqlWher As String = ""      'SQL文条件項目群
        Dim SqlSumm As String = ""      'SQL文GRP項目群

        DgvList.Columns.Clear()         '見出しクリア
        DgvList.Rows.Clear()            '一覧クリア

        setProductHd()                  '商品別の見出しセット（見出し初期値のセット）

        SqlItem = "SELECT m21.メーカー, m21.品名, m21.型式,"
        SqlItem += " m20.名称 as 倉庫名,"
        SqlItem += " m21.入出庫種別,m21.出庫開始サイン as ロケ番号, m21.製造番号, m21.伝票番号, m21.現在庫数"
        SqlItem += " ,m21.入庫単価, m21.最終入庫日, m21.最終出庫日,"
        SqlItem += " m90.文字１, m90.文字２ "
        SqlItem += " ,t46.仕入先請求番号, t43.仕入先名, t43.発注番号"
        SqlItem += " ,t20_get_custpo(t43.会社コード,t43.発注番号,t43.発注番号枝番) as custpo "

        SqlJoin = " FROM m21_zaiko m21"
        SqlJoin += " LEFT JOIN "
        SqlJoin += " m90_hanyo m90 "
        SqlJoin += " On "
        SqlJoin += " m90.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"
        SqlJoin += " AND "
        SqlJoin += " m90.固定キー ILIKE '" & CommonConst.INOUT_CLASS & "'"
        SqlJoin += " AND "
        SqlJoin += " m90.可変キー ILIKE m21.入出庫種別 "
        't43_nyukodt
        SqlJoin += " LEFT JOIN t43_nyukodt t43 "
        SqlJoin += "  on m21.伝票番号 = t43.入庫番号"
        SqlJoin += " and m21.行番号 = t43.行番号"
        SqlJoin += " and m21.会社コード = t43.会社コード"
        't46_kikehd
        SqlJoin += " LEFT JOIN t46_kikehd t46 "
        SqlJoin += "  on t43.発注番号 = t46.発注番号"
        SqlJoin += " and t43.発注番号枝番 = t46.発注番号枝番"
        SqlJoin += " and t43.会社コード = t46.会社コード"
        'm20
        SqlJoin += " LEFT JOIN "
        SqlJoin += " m20_warehouse m20 "
        SqlJoin += " ON "
        SqlJoin += " m20.会社コード = m21.会社コード"
        SqlJoin += " AND "
        SqlJoin += " m20.倉庫コード = m21.倉庫コード"

        SqlWher = " WHERE "
        SqlWher += " m21.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        SqlWher += " AND "
        SqlWher += " m21.無効フラグ = " & CommonConst.CANCEL_KBN_ENABLED.ToString
        SqlWher += " AND "
        SqlWher += " m21.現在庫数 <> 0 "

        If rbAll.Checked Then
            SqlSumm = " ORDER BY "
            SqlSumm += " m21.メーカー, m21.品名, m21.型式, m21.入出庫種別, m21.伝票番号 "
        End If
        If rbWarehouse.Checked Then
            SqlSumm = " ORDER BY "
            SqlSumm += " m21.倉庫コード, m21.メーカー, m21.品名, m21.型式, m21.入出庫種別, m21.伝票番号 "
        End If
        If rbSyubetsu.Checked Then
            SqlSumm = " ORDER BY "
            SqlSumm += " m21.入出庫種別, m21.メーカー, m21.品名, m21.型式, m21.倉庫コード, m21.伝票番号 "
        End If
        If rbLocation.Checked Then
            SqlSumm = " ORDER BY "
            SqlSumm += " m21.出庫開始サイン, m21.メーカー, m21.品名, m21.型式, m21.倉庫コード,m21.入出庫種別, m21.伝票番号 "
        End If
        If rbSerialNo.Checked Then
            SqlSumm = " ORDER BY "
            SqlSumm += " m21.製造番号, m21.メーカー, m21.品名, m21.型式, m21.倉庫コード,m21.入出庫種別, m21.出庫開始サイン, m21.伝票番号 "
        End If
        If rbOrderNo.Checked Then
            SqlSumm = " ORDER BY "
            SqlSumm += " m21.伝票番号, m21.メーカー, m21.品名, m21.型式, m21.倉庫コード,m21.入出庫種別, m21.出庫開始サイン, m21.製造番号 "
        End If

        Sql = SqlItem + SqlJoin + SqlWher + SqlSumm
        Try
            Dim dsList As DataSet = _db.selectDB(Sql, RS, reccnt)

            For i As Integer = 0 To dsList.Tables(RS).Rows.Count - 1

                DgvList.Rows.Add()
                DgvList.Rows(i).Cells("メーカー").Value = dsList.Tables(RS).Rows(i)("メーカー").ToString
                DgvList.Rows(i).Cells("品名").Value = dsList.Tables(RS).Rows(i)("品名").ToString
                DgvList.Rows(i).Cells("型式").Value = dsList.Tables(RS).Rows(i)("型式").ToString
                DgvList.Rows(i).Cells("倉庫").Value = dsList.Tables(RS).Rows(i)("倉庫名").ToString
                DgvList.Rows(i).Cells("入出庫種別").Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                                                                    dsList.Tables(RS).Rows(i)("文字２"),
                                                                    dsList.Tables(RS).Rows(i)("文字１"))
                'DgvList.Rows(i).Cells("ロケ番号").Value = dsList.Tables(RS).Rows(i)("ロケ番号").ToString
                DgvList.Rows(i).Cells("ロケ番号").Value = dsList.Tables(RS).Rows(i)("伝票番号").ToString
                DgvList.Rows(i).Cells("製造番号").Value = dsList.Tables(RS).Rows(i)("発注番号").ToString
                DgvList.Rows(i).Cells("伝票番号").Value = dsList.Tables(RS).Rows(i)("custpo").ToString
                DgvList.Rows(i).Cells("在庫数").Value = dsList.Tables(RS).Rows(i)("現在庫数")
                DgvList.Rows(i).Cells("単価（入庫単価）").Value = dsList.Tables(RS).Rows(i)("入庫単価")
                If dsList.Tables(RS).Rows(i)("最終入庫日") Is DBNull.Value Then
                    DgvList.Rows(i).Cells("最終入庫日").Value = ""
                Else
                    DgvList.Rows(i).Cells("最終入庫日").Value = dsList.Tables(RS).Rows(i)("最終入庫日").ToShortDateString()
                End If
                If dsList.Tables(RS).Rows(i)("最終出庫日") Is DBNull.Value Then
                    DgvList.Rows(i).Cells("最終出庫日").Value = ""
                Else
                    DgvList.Rows(i).Cells("最終出庫日").Value = dsList.Tables(RS).Rows(i)("最終出庫日").ToShortDateString()
                End If


                DgvList.Rows(i).Cells("仕入先名").Value = dsList.Tables(RS).Rows(i)("仕入先名").ToString
                DgvList.Rows(i).Cells("仕入先請求番号").Value = dsList.Tables(RS).Rows(i)("仕入先請求番号").ToString

            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try


    End Sub

    '在庫管理区分、在庫表示区分により最終的なリスト列の表示方法を決定する
    ' 2020.03.02 
    '
    Private Sub makeZaikoKBNLayout()

        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        Sql = "SELECT "
        Sql += " m01.在庫管理区分 AS 在庫管理区分,m01.在庫表示区分 AS 在庫表示区分 "
        Sql += " FROM m01_company m01"
        Sql += " WHERE "
        Sql += " m01.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"

        Try

            Dim dsM01 As DataSet = _db.selectDB(Sql, RS, reccnt)

            InventoryControl = dsM01.Tables(RS).Rows(0)("在庫管理区分")
            InventoryViewer = dsM01.Tables(RS).Rows(0)("在庫表示区分")

        Catch ue As UsrDefException
        Catch ex As Exception
        End Try

        '在庫表示区分から表示する在庫管理対象列を選定する
        If "13579BDFHJLNPRTV".Contains(InventoryViewer) Then
            DgvList.Columns("倉庫").Visible = True
        Else
            DgvList.Columns("倉庫").Visible = False
        End If
        If "2367ABEFIJMNQRUV".Contains(InventoryViewer) Then
            DgvList.Columns("入出庫種別").Visible = True
        Else
            DgvList.Columns("入出庫種別").Visible = False
        End If
        If "4567CDEFKLMNSTUV".Contains(InventoryViewer) Then
            DgvList.Columns("ロケ番号").Visible = True
        Else
            DgvList.Columns("ロケ番号").Visible = False
        End If
        If "789ABCDEFOPQRSTUV".Contains(InventoryViewer) Then
            DgvList.Columns("製造番号").Visible = True
        Else
            DgvList.Columns("製造番号").Visible = False
        End If
        If "7GHIJKLMNOPQRSTUV".Contains(InventoryViewer) Then
            DgvList.Columns("伝票番号").Visible = True
        Else
            DgvList.Columns("伝票番号").Visible = False
        End If

        'ラジオボタン選択肢から表示対象列の表示順を選定する
        If rbAll.Checked Then
            DgvList.Columns("メーカー").DisplayIndex = 0
            DgvList.Columns("品名").DisplayIndex = 1
            DgvList.Columns("型式").DisplayIndex = 2
            DgvList.Columns("倉庫").DisplayIndex = 3
            DgvList.Columns("入出庫種別").DisplayIndex = 4
            DgvList.Columns("ロケ番号").DisplayIndex = 5
            DgvList.Columns("製造番号").DisplayIndex = 6
            DgvList.Columns("伝票番号").DisplayIndex = 7
        End If
        If rbWarehouse.Checked Then
            DgvList.Columns("倉庫").DisplayIndex = 0
            DgvList.Columns("メーカー").DisplayIndex = 1
            DgvList.Columns("品名").DisplayIndex = 2
            DgvList.Columns("型式").DisplayIndex = 3
            DgvList.Columns("入出庫種別").DisplayIndex = 4
            DgvList.Columns("ロケ番号").DisplayIndex = 5
            DgvList.Columns("製造番号").DisplayIndex = 6
            DgvList.Columns("伝票番号").DisplayIndex = 7
        End If
        If rbSyubetsu.Checked Then
            DgvList.Columns("入出庫種別").DisplayIndex = 0
            DgvList.Columns("メーカー").DisplayIndex = 1
            DgvList.Columns("品名").DisplayIndex = 2
            DgvList.Columns("型式").DisplayIndex = 3
            DgvList.Columns("倉庫").DisplayIndex = 4
            DgvList.Columns("ロケ番号").DisplayIndex = 5
            DgvList.Columns("製造番号").DisplayIndex = 6
            DgvList.Columns("伝票番号").DisplayIndex = 7
        End If
        If rbLocation.Checked Then
            DgvList.Columns("ロケ番号").DisplayIndex = 0
            DgvList.Columns("メーカー").DisplayIndex = 1
            DgvList.Columns("品名").DisplayIndex = 2
            DgvList.Columns("型式").DisplayIndex = 3
            DgvList.Columns("倉庫").DisplayIndex = 4
            DgvList.Columns("入出庫種別").DisplayIndex = 5
            DgvList.Columns("製造番号").DisplayIndex = 6
            DgvList.Columns("伝票番号").DisplayIndex = 7
        End If
        If rbSerialNo.Checked Then
            DgvList.Columns("製造番号").DisplayIndex = 0
            DgvList.Columns("メーカー").DisplayIndex = 1
            DgvList.Columns("品名").DisplayIndex = 2
            DgvList.Columns("型式").DisplayIndex = 3
            DgvList.Columns("倉庫").DisplayIndex = 4
            DgvList.Columns("入出庫種別").DisplayIndex = 5
            DgvList.Columns("ロケ番号").DisplayIndex = 6
            DgvList.Columns("伝票番号").DisplayIndex = 7
        End If
        If rbOrderNo.Checked Then
            DgvList.Columns("伝票番号").DisplayIndex = 0
            DgvList.Columns("メーカー").DisplayIndex = 1
            DgvList.Columns("品名").DisplayIndex = 2
            DgvList.Columns("型式").DisplayIndex = 3
            DgvList.Columns("倉庫").DisplayIndex = 4
            DgvList.Columns("入出庫種別").DisplayIndex = 5
            DgvList.Columns("ロケ番号").DisplayIndex = 6
            DgvList.Columns("製造番号").DisplayIndex = 7
        End If
        DgvList.Columns("在庫数").DisplayIndex = 8
        DgvList.Columns("単価（入庫単価）").DisplayIndex = 9
        DgvList.Columns("最終入庫日").DisplayIndex = 10
        DgvList.Columns("最終出庫日").DisplayIndex = 11
        DgvList.Columns("仕入先名").DisplayIndex = 12
        DgvList.Columns("仕入先請求番号").DisplayIndex = 13
    End Sub

    '戻るボタン押下時
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    'excel出力ボタン押下時
    Private Sub BtnExcelOutput_Click(sender As Object, e As EventArgs) Handles BtnExcelOutput.Click
        '対象データがない場合は取消操作不可能
        If DgvList.Rows.Count = 0 Then

            '該当データがないアラートを出す
            _msgHd.dspMSG("noTargetData", frmC01F10_Login.loginValue.Language)

            Return

        Else

            'Excel出力処理
            outputExcel()
        End If
    End Sub

    'excel出力処理
    Private Sub outputExcel()
        Dim outputDate As Date = DateTime.Now.ToShortDateString

        '定義
        Dim app As Excel.Application = Nothing
        Dim books As Excel.Workbooks = Nothing
        Dim book As Excel.Workbook = Nothing
        Dim sheet As Excel.Worksheet = Nothing

        'カーソルをビジー状態にする
        Cursor.Current = Cursors.WaitCursor

        Try
            '雛形パス
            Dim sHinaPath As String = StartUp._iniVal.BaseXlsPath
            '雛形ファイル名
            Dim sHinaFile As String = ""
            '出力先パス
            Dim sOutPath As String = StartUp._iniVal.OutXlsPath
            '出力ファイル名
            Dim sOutFile As String = ""

            '商品別処理
            '2020.03.07 スタイルシートの適用をStockProductList.xlsxに統一する
            ''If rbAll.Checked Then
            sHinaFile = sHinaPath & "\" & "StockProductList.xlsx"
            sOutFile = sOutPath & "\StockProductList_" & DateTime.Now.ToString("yyyyMMddHHmm") & ".xlsx"
            ''Else
            ''sHinaFile = sHinaPath & "\" & "StockWarehouseList.xlsx"
            ''sOutFile = sOutPath & "\StockWarehouseList_" & DateTime.Now.ToString("yyyyMMddHHmm") & ".xlsx"
            ''End If

            app = New Excel.Application()
            books = app.Workbooks
            book = books.Add(sHinaFile)  'テンプレート
            sheet = CType(book.Worksheets(1), Excel.Worksheet)


            '商品別処理
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                sheet.PageSetup.LeftHeader = "StockProductList"
                sheet.PageSetup.RightHeader = "OutputDate：" & DateTime.Now.ToShortDateString
                If rbAll.Checked Then
                    sheet.Range("A1").Value = "Manufacturer"
                    sheet.Range("B1").Value = "ItemName"
                    sheet.Range("C1").Value = "Spec"
                    sheet.Range("D1").Value = "WarehouseName"
                    sheet.Range("E1").Value = "StorageType"
                    sheet.Range("F1").Value = "Location"
                    sheet.Range("G1").Value = "PO"
                    sheet.Range("H1").Value = "CustomerNumber"
                End If
                If rbWarehouse.Checked Then
                    sheet.Range("A1").Value = "WarehouseName"
                    sheet.Range("B1").Value = "Manufacturer"
                    sheet.Range("C1").Value = "ItemName"
                    sheet.Range("D1").Value = "Spec"
                    sheet.Range("E1").Value = "StorageType"
                    sheet.Range("F1").Value = "Location"
                    sheet.Range("G1").Value = "PO"
                    sheet.Range("H1").Value = "CustomerNumber"
                End If
                If rbSyubetsu.Checked Then
                    sheet.Range("A1").Value = "StorageType"
                    sheet.Range("B1").Value = "Manufacturer"
                    sheet.Range("C1").Value = "ItemName"
                    sheet.Range("D1").Value = "Spec"
                    sheet.Range("E1").Value = "WarehouseName"
                    sheet.Range("F1").Value = "Location"
                    sheet.Range("G1").Value = "SerialNo"
                    sheet.Range("H1").Value = "OrderNo"
                End If
                If rbLocation.Checked Then
                    sheet.Range("A1").Value = "Location"
                    sheet.Range("B1").Value = "Manufacturer"
                    sheet.Range("C1").Value = "ItemName"
                    sheet.Range("D1").Value = "Spec"
                    sheet.Range("E1").Value = "WarehouseName"
                    sheet.Range("F1").Value = "StorageType"
                    sheet.Range("G1").Value = "SerialNo"
                    sheet.Range("H1").Value = "OrderNo"
                End If
                If rbSerialNo.Checked Then
                    sheet.Range("A1").Value = "SerialNo"
                    sheet.Range("B1").Value = "Manufacturer"
                    sheet.Range("C1").Value = "ItemName"
                    sheet.Range("D1").Value = "Spec"
                    sheet.Range("E1").Value = "WarehouseName"
                    sheet.Range("F1").Value = "StorageType"
                    sheet.Range("G1").Value = "Location"
                    sheet.Range("H1").Value = "OrderNo"
                End If
                If rbOrderNo.Checked Then
                    sheet.Range("A1").Value = "OrderNo"
                    sheet.Range("B1").Value = "Manufacturer"
                    sheet.Range("C1").Value = "ItemName"
                    sheet.Range("D1").Value = "Spec"
                    sheet.Range("E1").Value = "WarehouseName"
                    sheet.Range("F1").Value = "StorageType"
                    sheet.Range("G1").Value = "Location"
                    sheet.Range("H1").Value = "SerialNo"
                End If

                sheet.Range("I1").Value = "StockQuantity"
                sheet.Range("J1").Value = "UnitPrice (ReceiptUnitPrice)"
                sheet.Range("K1").Value = "LastReceiptDate"
                sheet.Range("L1").Value = "LastDeliveryDate"
            End If

            For i As Integer = 0 To DgvList.RowCount - 1
                Dim cellRowIndex As Integer = 2
                cellRowIndex += i

                If rbAll.Checked Then
                    sheet.Range("A" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("メーカー").Value
                    sheet.Range("B" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("品名").Value
                    sheet.Range("C" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("型式").Value
                    sheet.Range("D" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("倉庫").Value
                    sheet.Range("E" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("入出庫種別").Value
                    sheet.Range("F" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("ロケ番号").Value
                    sheet.Range("G" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("製造番号").Value
                    sheet.Range("H" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("伝票番号").Value
                End If
                If rbWarehouse.Checked Then
                    sheet.Range("A" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("倉庫").Value
                    sheet.Range("B" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("メーカー").Value
                    sheet.Range("C" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("品名").Value
                    sheet.Range("D" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("型式").Value
                    sheet.Range("E" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("入出庫種別").Value
                    sheet.Range("F" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("ロケ番号").Value
                    sheet.Range("G" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("製造番号").Value
                    sheet.Range("H" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("伝票番号").Value
                End If
                If rbSyubetsu.Checked Then
                    sheet.Range("A" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("入出庫種別").Value
                    sheet.Range("B" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("メーカー").Value
                    sheet.Range("C" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("品名").Value
                    sheet.Range("D" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("型式").Value
                    sheet.Range("E" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("倉庫").Value
                    sheet.Range("F" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("ロケ番号").Value
                    sheet.Range("G" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("製造番号").Value
                    sheet.Range("H" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("伝票番号").Value
                End If
                If rbLocation.Checked Then
                    sheet.Range("A" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("ロケ番号").Value
                    sheet.Range("B" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("メーカー").Value
                    sheet.Range("C" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("品名").Value
                    sheet.Range("D" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("型式").Value
                    sheet.Range("E" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("倉庫").Value
                    sheet.Range("F" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("入出庫種別").Value
                    sheet.Range("G" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("製造番号").Value
                    sheet.Range("H" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("伝票番号").Value
                End If
                If rbSerialNo.Checked Then
                    sheet.Range("A" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("製造番号").Value
                    sheet.Range("B" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("メーカー").Value
                    sheet.Range("C" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("品名").Value
                    sheet.Range("D" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("型式").Value
                    sheet.Range("E" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("倉庫").Value
                    sheet.Range("F" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("入出庫種別").Value
                    sheet.Range("G" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("ロケ番号").Value
                    sheet.Range("H" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("伝票番号").Value
                End If
                If rbOrderNo.Checked Then
                    sheet.Range("A" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("伝票番号").Value
                    sheet.Range("B" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("メーカー").Value
                    sheet.Range("C" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("品名").Value
                    sheet.Range("D" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("型式").Value
                    sheet.Range("E" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("倉庫").Value
                    sheet.Range("F" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("入出庫種別").Value
                    sheet.Range("G" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("ロケ番号").Value
                    sheet.Range("H" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("製造番号").Value
                End If
                sheet.Range("I" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("在庫数").Value
                sheet.Range("J" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("単価（入庫単価）").Value
                sheet.Range("K" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("最終入庫日").Value
                sheet.Range("L" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("最終出庫日").Value

            Next

            'Else

            '    If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            '        sheet.PageSetup.LeftHeader = "StockWarehouseList"
            '        sheet.PageSetup.RightHeader = "OutputDate：" & DateTime.Now.ToShortDateString

            '        sheet.Range("A1").Value = "WarehouseName"
            '        sheet.Range("B1").Value = "Manufacturer"
            '        sheet.Range("C1").Value = "ItemName"
            '        sheet.Range("D1").Value = "Spec"
            '        sheet.Range("E1").Value = "StockQuantity"
            '        sheet.Range("F1").Value = "StorageType"
            '        sheet.Range("G1").Value = "UnitPrice (ReceiptUnitPrice)"
            '        sheet.Range("H1").Value = "LastReceiptDate"
            '        sheet.Range("I1").Value = "LastDeliveryDate"
            '    End If

            '    For i As Integer = 0 To DgvList.RowCount - 1
            '        Dim cellRowIndex As Integer = 2
            '        cellRowIndex += i

            '        sheet.Range("A" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("倉庫").Value
            '        sheet.Range("B" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("メーカー").Value
            '        sheet.Range("C" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("品名").Value
            '        sheet.Range("D" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("型式").Value
            '        sheet.Range("E" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("在庫数").Value
            '        sheet.Range("F" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("入出庫種別").Value
            '        sheet.Range("G" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("単価（入庫単価）").Value
            '        sheet.Range("H" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("最終入庫日").Value
            '        sheet.Range("I" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("最終出庫日").Value

            '    Next
            'End If

            app.DisplayAlerts = False 'Microsoft Excelのアラート一旦無効化

            Dim excelChk As Boolean = excelOutput(sOutFile)
            If excelChk = False Then
                Exit Sub
            End If
            book.SaveAs(sOutFile) '書き込み実行

            app.DisplayAlerts = True 'アラート無効化を解除

            'カーソルをビジー状態から元に戻す
            Cursor.Current = Cursors.Default

            app.Visible = True
            _msgHd.dspMSG("CreateExcel", frmC01F10_Login.loginValue.Language)

        Catch ex As Exception
            'カーソルをビジー状態から元に戻す
            Cursor.Current = Cursors.Default

            Throw ex
        Finally
            'app.Quit()
            'Marshal.ReleaseComObject(sheet)
            'Marshal.ReleaseComObject(book)
            'Marshal.ReleaseComObject(app)

        End Try

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

    'Private Sub rbAll_CheckedChanged(sender As Object, e As EventArgs) Handles rbAll.CheckedChanged

    '    getList()
    '    makeZaikoKBNLayout()        '在庫管理区分、表示区分により最終的な表示方法を決定する

    'End Sub

    Private Sub rbWarehouse_CheckedChanged(sender As Object, e As EventArgs) Handles rbWarehouse.CheckedChanged

        getList()
        makeZaikoKBNLayout()        '在庫管理区分、表示区分により最終的な表示方法を決定する

    End Sub

    Private Sub rbSyubetsu_CheckedChanged(sender As Object, e As EventArgs) Handles rbSyubetsu.CheckedChanged

        getList()
        makeZaikoKBNLayout()        '在庫管理区分、表示区分により最終的な表示方法を決定する

    End Sub

    Private Sub rbLocation_CheckedChanged(sender As Object, e As EventArgs) Handles rbLocation.CheckedChanged

        getList()
        makeZaikoKBNLayout()        '在庫管理区分、表示区分により最終的な表示方法を決定する

    End Sub

    Private Sub rbSerialNo_CheckedChanged(sender As Object, e As EventArgs) Handles rbSerialNo.CheckedChanged

        getList()
        makeZaikoKBNLayout()        '在庫管理区分、表示区分により最終的な表示方法を決定する

    End Sub

    Private Sub rbOrderNo_CheckedChanged(sender As Object, e As EventArgs) Handles rbOrderNo.CheckedChanged

        getList()
        makeZaikoKBNLayout()        '在庫管理区分、表示区分により最終的な表示方法を決定する

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


    '-------------------------------------------------------------------------------
    'ラジオボタンの表示項目をコントロールする
    '-------------------------------------------------------------------------------
    Private Sub makeTableLayoutPanel1()

        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        Sql = "SELECT "
        Sql += " m01.在庫管理区分 AS 在庫管理区分,m01.在庫表示区分 AS 在庫表示区分 "
        Sql += " FROM m01_company m01"
        Sql += " WHERE "
        Sql += " m01.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"

        Try

            Dim dsM01 As DataSet = _db.selectDB(Sql, RS, reccnt)

            InventoryControl = dsM01.Tables(RS).Rows(0)("在庫管理区分")
            InventoryViewer = dsM01.Tables(RS).Rows(0)("在庫表示区分")

        Catch ue As UsrDefException
        Catch ex As Exception
        End Try

        'ラジオボタン表示状態を「商品別」以外リセットする
        rbWarehouse.Visible = False
        rbSyubetsu.Visible = False
        rbLocation.Visible = False
        rbSerialNo.Visible = False
        rbOrderNo.Visible = False

        Select Case InventoryControl
            Case "1", "3", "7", "F", "V"
                rbWarehouse.Visible = True
        End Select
        Select Case InventoryControl
            Case "3", "7", "F", "V"
                rbSyubetsu.Visible = True
        End Select
        Select Case InventoryControl
            Case "7", "F", "V"
                rbLocation.Visible = True
        End Select
        Select Case InventoryControl
            Case "F", "V"
                rbSerialNo.Visible = True
        End Select
        Select Case InventoryControl
            Case "V"
                rbOrderNo.Visible = True
        End Select

    End Sub



    Private Sub makeDgvList()

        'Dim reccnt As Integer = 0 'DB用（デフォルト）
        'Dim Sql As String = ""

        'DgvList.Columns.Clear() '見出しクリア
        'DgvList.Rows.Clear() '一覧クリア

        ''商品別処理
        'If rbAll.Checked Then

        '    setProductHd() '商品別の見出しセット

        '    Try

        '        Sql = "SELECT "
        '        Sql += " m21.メーカー, m21.品名, m21.型式, m21.入出庫種別, sum(m21.現在庫数) as 現在庫数 "
        '        Sql += " ,m21.入庫単価, m21.最終入庫日, m21.最終出庫日, m90.文字１, m90.文字２ "
        '        Sql += " ,t46.仕入先請求番号, t46.仕入先名"

        '        Sql += " FROM m21_zaiko m21"

        '        Sql += " LEFT JOIN "
        '        Sql += " m90_hanyo m90 "
        '        Sql += " ON "
        '        Sql += " m90.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"
        '        Sql += " AND "
        '        Sql += " m90.固定キー ILIKE '" & CommonConst.INOUT_CLASS & "'"
        '        Sql += " AND "
        '        Sql += " m90.可変キー ILIKE m21.入出庫種別 "

        '        't43_nyukodt
        '        Sql += " LEFT JOIN t43_nyukodt t43 "
        '        Sql += "  on m21.伝票番号 = t43.入庫番号"
        '        Sql += " and m21.行番号 = t43.行番号"

        '        't46_kikehd
        '        Sql += " LEFT JOIN t46_kikehd t46 "
        '        Sql += "  on t43.発注番号 = t46.発注番号"
        '        Sql += " and t43.発注番号枝番 = t46.発注番号枝番"
        '        Sql += " and t46.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED


        '        Sql += " WHERE "
        '        Sql += " m21.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"
        '        Sql += " AND "
        '        Sql += " m21.無効フラグ = " & CommonConst.CANCEL_KBN_ENABLED.ToString
        '        Sql += " AND "
        '        Sql += " m21.現在庫数 <> 0 "


        '        Sql += " GROUP BY "
        '        Sql += " m21.メーカー, m21.品名, m21.型式, m21.入出庫種別 "
        '        Sql += " ,m21.入庫単価, m21.最終入庫日, m21.最終出庫日, m90.文字１, m90.文字２, m21.伝票番号 "
        '        Sql += " ,t46.仕入先請求番号, t46.仕入先名"

        '        Sql += " ORDER BY "
        '        'Sql += " m21.メーカー, m21.品名, m21.型式, m21.最終入庫日, m21.入出庫種別 "
        '        Sql += " m21.メーカー, m21.品名, m21.型式, m21.伝票番号, m21.入出庫種別 "

        '        Dim dsList As DataSet = _db.selectDB(Sql, RS, reccnt)

        '        'Dim currentManufacturer As String = ""
        '        'Dim currentItemName As String = ""
        '        'Dim currentSpec As String = ""
        '        'Dim productFlg As Boolean = False

        '        For i As Integer = 0 To dsList.Tables(RS).Rows.Count - 1

        '            'If currentManufacturer <> dsList.Tables(RS).Rows(i)("メーカー").ToString Or
        '            '    currentItemName <> dsList.Tables(RS).Rows(i)("品名").ToString Or
        '            '    currentSpec <> dsList.Tables(RS).Rows(i)("型式").ToString Then

        '            '    currentManufacturer = dsList.Tables(RS).Rows(i)("メーカー").ToString
        '            '    currentItemName = dsList.Tables(RS).Rows(i)("品名").ToString
        '            '    currentSpec = dsList.Tables(RS).Rows(i)("型式").ToString

        '            '    productFlg = False
        '            'Else
        '            '    productFlg = True
        '            'End If

        '            DgvList.Rows.Add()
        '            'DgvList.Rows(i).Cells("メーカー").Value = IIf(productFlg, "", currentManufacturer)
        '            'DgvList.Rows(i).Cells("品名").Value = IIf(productFlg, "", currentItemName)
        '            'DgvList.Rows(i).Cells("型式").Value = IIf(productFlg, "", currentSpec)
        '            DgvList.Rows(i).Cells("メーカー").Value = dsList.Tables(RS).Rows(i)("メーカー").ToString
        '            DgvList.Rows(i).Cells("品名").Value = dsList.Tables(RS).Rows(i)("品名").ToString
        '            DgvList.Rows(i).Cells("型式").Value = dsList.Tables(RS).Rows(i)("型式").ToString
        '            DgvList.Rows(i).Cells("在庫数").Value = dsList.Tables(RS).Rows(i)("現在庫数")
        '            DgvList.Rows(i).Cells("入出庫種別").Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
        '                                                            dsList.Tables(RS).Rows(i)("文字２"),
        '                                                            dsList.Tables(RS).Rows(i)("文字１"))
        '            DgvList.Rows(i).Cells("単価（入庫単価）").Value = dsList.Tables(RS).Rows(i)("入庫単価")
        '            If dsList.Tables(RS).Rows(i)("最終入庫日") Is DBNull.Value Then
        '                DgvList.Rows(i).Cells("最終入庫日").Value = ""
        '            Else
        '                DgvList.Rows(i).Cells("最終入庫日").Value = dsList.Tables(RS).Rows(i)("最終入庫日").ToShortDateString()
        '            End If
        '            If dsList.Tables(RS).Rows(i)("最終出庫日") Is DBNull.Value Then
        '                DgvList.Rows(i).Cells("最終出庫日").Value = ""
        '            Else
        '                DgvList.Rows(i).Cells("最終出庫日").Value = dsList.Tables(RS).Rows(i)("最終出庫日").ToShortDateString()
        '            End If


        '            DgvList.Rows(i).Cells("仕入先名").Value = dsList.Tables(RS).Rows(i)("仕入先名").ToString
        '            DgvList.Rows(i).Cells("仕入先請求番号").Value = dsList.Tables(RS).Rows(i)("仕入先請求番号").ToString

        '        Next

        '    Catch ue As UsrDefException
        '        ue.dspMsg()
        '        Throw ue
        '    Catch ex As Exception
        '        'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
        '        Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        '    End Try

        'Else
        '    '倉庫別処理

        '    setWarehouseHd() '倉庫別の見出しセット

        '    Try

        '        Sql = "SELECT "
        '        Sql += " m21.メーカー, m21.品名, m21.型式, m21.入出庫種別, sum(m21.現在庫数) as 現在庫数 "
        '        Sql += " ,m21.入庫単価, m21.最終入庫日, m21.最終出庫日, m90.文字１, m90.文字２, m20.名称 "
        '        Sql += " ,t46.仕入先請求番号, t46.仕入先名"

        '        Sql += " FROM m21_zaiko m21"

        '        Sql += " LEFT JOIN "
        '        Sql += " m90_hanyo m90 "
        '        Sql += " ON "
        '        Sql += " m90.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"
        '        Sql += " AND "
        '        Sql += " m90.固定キー ILIKE '" & CommonConst.INOUT_CLASS & "'"
        '        Sql += " AND "
        '        Sql += " m90.可変キー ILIKE m21.入出庫種別 "

        '        Sql += " LEFT JOIN "
        '        Sql += " m20_warehouse m20 "
        '        Sql += " ON "
        '        Sql += " m20.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"
        '        Sql += " AND "
        '        Sql += " m20.倉庫コード ILIKE m21.倉庫コード"


        '        't43_nyukodt
        '        Sql += " LEFT JOIN t43_nyukodt t43 "
        '        Sql += "  on m21.伝票番号 = t43.入庫番号"
        '        Sql += " and m21.行番号 = t43.行番号"

        '        't46_kikehd
        '        Sql += " LEFT JOIN t46_kikehd t46 "
        '        Sql += "  on t43.発注番号 = t46.発注番号"
        '        Sql += " and t43.発注番号枝番 = t46.発注番号枝番"
        '        Sql += " and t46.取消区分 =  " & CommonConst.CANCEL_KBN_ENABLED


        '        Sql += " WHERE "
        '        Sql += " m21.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"
        '        Sql += " AND "
        '        Sql += " m21.無効フラグ = " & CommonConst.CANCEL_KBN_ENABLED.ToString
        '        Sql += " AND "
        '        Sql += " m21.現在庫数 <> 0 "
        '        Sql += " GROUP BY "
        '        Sql += " m21.倉庫コード, m21.メーカー, m21.品名, m21.型式, m21.入出庫種別, m21.現在庫数 "
        '        Sql += " ,m21.入庫単価, m21.最終入庫日, m21.最終出庫日, m90.文字１, m90.文字２, m20.名称, m21.伝票番号 "
        '        Sql += " ,t46.仕入先請求番号, t46.仕入先名"

        '        Sql += " ORDER BY "
        '        'Sql += " m21.メーカー, m21.品名, m21.型式, m21.最終入庫日, m21.入出庫種別 "
        '        Sql += " m21.倉庫コード, m21.メーカー, m21.品名, m21.型式, m21.伝票番号, m21.入出庫種別 "

        '        Dim dsList As DataSet = _db.selectDB(Sql, RS, reccnt)

        '        'Dim currentWarehouse As String = ""
        '        'Dim warehouseFlg As Boolean = False

        '        For i As Integer = 0 To dsList.Tables(RS).Rows.Count - 1

        '            'If currentWarehouse <> dsList.Tables(RS).Rows(i)("名称").ToString Then
        '            '    currentWarehouse = dsList.Tables(RS).Rows(i)("名称").ToString

        '            '    warehouseFlg = False
        '            'Else
        '            '    warehouseFlg = True
        '            'End If

        '            DgvList.Rows.Add()
        '            'DgvList.Rows(i).Cells("倉庫").Value = IIf(warehouseFlg, "", currentWarehouse)
        '            DgvList.Rows(i).Cells("倉庫").Value = dsList.Tables(RS).Rows(i)("名称").ToString
        '            DgvList.Rows(i).Cells("メーカー").Value = dsList.Tables(RS).Rows(i)("メーカー")
        '            DgvList.Rows(i).Cells("品名").Value = dsList.Tables(RS).Rows(i)("品名")
        '            DgvList.Rows(i).Cells("型式").Value = dsList.Tables(RS).Rows(i)("型式")
        '            DgvList.Rows(i).Cells("在庫数").Value = dsList.Tables(RS).Rows(i)("現在庫数")
        '            DgvList.Rows(i).Cells("入出庫種別").Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
        '                                                            dsList.Tables(RS).Rows(i)("文字２"),
        '                                                            dsList.Tables(RS).Rows(i)("文字１"))
        '            DgvList.Rows(i).Cells("単価（入庫単価）").Value = dsList.Tables(RS).Rows(i)("入庫単価")
        '            DgvList.Rows(i).Cells("最終入庫日").Value = dsList.Tables(RS).Rows(i)("最終入庫日")
        '            DgvList.Rows(i).Cells("最終出庫日").Value = dsList.Tables(RS).Rows(i)("最終出庫日")

        '            DgvList.Rows(i).Cells("仕入先名").Value = dsList.Tables(RS).Rows(i)("仕入先名").ToString
        '            DgvList.Rows(i).Cells("仕入先請求番号").Value = dsList.Tables(RS).Rows(i)("仕入先請求番号").ToString

        '        Next

        '    Catch ue As UsrDefException
        '        ue.dspMsg()
        '        Throw ue
        '    Catch ex As Exception
        '        'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
        '        Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        '    End Try

        'End If


    End Sub
End Class