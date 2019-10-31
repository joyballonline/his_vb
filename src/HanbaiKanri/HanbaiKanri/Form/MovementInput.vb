Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports System.Globalization

Public Class MovementInput
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
        'DgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.DisplayedCells
    End Sub

    '画面表示時
    Private Sub MstHanyoue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        LblMode.Text = "参照モード"

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblMode.Text = OrderStatus & " Mode"

            LblProcessingDate.Text = "ProcessingDate" '処理日
            LblProcessingClassification.Text = "ProcessingClassification" '処理区分

            LblManufacturer.Text = "Manufacturer" 'メーカー
            LblItemName.Text = "ItemName" '品名
            LblSpec.Text = "Spec" '型式

            BtnSearch.Text = "InventorySearch" '在庫検索

            LblMovingSource.Text = "MovingSource" '移動元
            LblWarehouseSince.Text = "Warehouse" '倉庫
            LblStorageTypeSince.Text = "StorageType" '入出庫種別
            LblQuantityFrom.Text = "Quantity" '数量
            LblUnitPrice.Text = "UnitPrice" '単価
            LblGoodsReceiptDate.Text = "LblGoodsReceiptDate" '入庫日


            LblMovingDestination.Text = "MovingDestination" '移動先
            LblWarehouseTo.Text = "Warehouse" '倉庫
            LblStorageTypeTo.Text = "StorageType" '入出庫種別
            LblQuantityTo.Text = "Quantity" '数量

            'LblWarehouseSince.Text = "TargetWarehouse"
            'LblWarehouseTo.Text = "Destination"
            'LblInOutKbn.Text = "StorageType"

            BtnRegistration.Text = "Registration"
            BtnBack.Text = "Back"

            'DgvList.Columns("メーカー").HeaderText = "Manufacturer"
            'DgvList.Columns("品名").HeaderText = "ItemName"
            'DgvList.Columns("型式").HeaderText = "Spec"
            'DgvList.Columns("数量").HeaderText = "Quantity"

            'BtnRowsAdd.Text = "AddLine"
            'BtnRowsDel.Text = "DeleteLine"
        End If

        DtpProcessingDate.Text = DateTime.Today

        createWarehouseToCombobox() '倉庫コンボボックス
        createPCKbn() '処理区分コンボボックス
        createInOutKbn("0,1") '入出庫種別コンボボックス

    End Sub

    '登録ボタン押下時
    Private Sub BtnRegistration_Click(sender As Object, e As EventArgs) Handles BtnRegistration.Click

        Dim dtToday As DateTime = DateTime.Now
        Dim strToday As String = UtilClass.strFormatDate(dtToday)

        Dim inoutTime As TimeSpan = dtToday.TimeOfDay
        Dim inoutDate As String = UtilClass.formatDatetime(DtpProcessingDate.Text & " " & inoutTime.ToString)

        Dim Sql As String = ""
        Dim reccnt As Integer = 0 'DB用（デフォルト）

        '在庫があるかチェック

        Dim denpyoNo As String = "00000000000000"
        Dim denpyoEda As String = "00"

        If TxtQuantityTo.Text = "" Then
            _msgHd.dspMSG("noMovingQuantityError", frmC01F10_Login.loginValue.Language)
            Exit Sub
        Else
            If TxtQuantityTo.Text = "0" Then
                _msgHd.dspMSG("noMovingQuantityError", frmC01F10_Login.loginValue.Language)
                Exit Sub
            End If
        End If

        Try

            '在庫マスタデータを元に、入庫データ取得
            '-----------------------------
            Sql = " SELECT t43.* ,t42.* FROM t43_nyukodt t43 "
            Sql += " LEFT JOIN "
            Sql += " t42_nyukohd t42 "
            Sql += " ON "
            Sql += " t43.会社コード ILIKE t42.会社コード "
            Sql += " AND "
            Sql += " t43.入庫番号 ILIKE t42.入庫番号 "

            Sql += " WHERE "
            Sql += " t43.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND "

            'ロケ番号がない場合は伝票番号をそのまま使用する
            If TxtDenpyoNo.Tag.ToString <> "" Then
                Sql += " t43.入庫番号 ILIKE '" & TxtDenpyoNo.Tag & "'"
            Else
                Sql += " t43.入庫番号 ILIKE '" & TxtDenpyoNo.Text & "'"
            End If

            Sql += " AND "

            If TxtLineNumber.Tag.ToString <> "" Then
                Sql += " t43.行番号 = '" & TxtLineNumber.Tag & "'"
            Else
                Sql += " t43.行番号 = '" & TxtLineNumber.Text & "'"
            End If

            '対象の入庫データを取得
            Dim dsNyuko As DataSet = _db.selectDB(Sql, RS, reccnt)

            Dim goodsIssueFlg As Boolean = False 'GoodsIssue createフラグ
            Dim receiptFlg As Boolean = False 'Receipt createフラグ

            '出庫データ作成対象チェック
            If CmProcessingClassification.SelectedValue = CommonConst.PC_KBN_MOVE Or
                CmStorageTypeTo.SelectedValue = CommonConst.INOUT_KBN_HAIKI Or
                CmStorageTypeTo.SelectedValue = CommonConst.INOUT_KBN_DECREASE Then

                'goodsIssueFlg = True

                Dim LS As String = getSaiban("70", dtToday) '出庫番号新規取得

                '出庫登録
                '-----------------------------

                '出庫基本
                Sql = "INSERT INTO "
                Sql += "Public."
                Sql += "t44_shukohd("
                Sql += " 会社コード, 出庫番号, 見積番号, 見積番号枝番, 受注番号, 受注番号枝番 "
                Sql += ", 入力担当者, 出庫日, 登録日, 取消区分, 更新日, 更新者, 入力担当者コード "
                Sql += " )VALUES( '"
                Sql += frmC01F10_Login.loginValue.BumonCD '会社コード
                Sql += "', '"
                Sql += LS '出庫番号
                Sql += "', '"
                Sql += denpyoNo '見積番号
                Sql += "', '"
                Sql += denpyoEda '見積番号枝番
                Sql += "', '"
                Sql += denpyoNo '受注番号
                Sql += "', '"
                Sql += denpyoEda '受注番号枝番
                Sql += "', '"
                Sql += frmC01F10_Login.loginValue.TantoNM '入力担当者
                Sql += "', '"
                Sql += UtilClass.formatDatetime(DtpProcessingDate.Text) '出庫日
                Sql += "', '"
                Sql += UtilClass.formatDatetime(dtToday) '登録日
                Sql += "', '"
                Sql += CommonConst.CANCEL_KBN_ENABLED.ToString '取消区分
                Sql += "', '"
                Sql += UtilClass.formatDatetime(dtToday) '更新日
                Sql += "', '"
                Sql += frmC01F10_Login.loginValue.TantoNM '更新者
                Sql += "', '"
                Sql += frmC01F10_Login.loginValue.TantoCD '入力担当者コード
                Sql += "')"

                _db.executeDB(Sql)

                '出庫明細
                Sql = "INSERT INTO "
                Sql += "Public."
                Sql += "t45_shukodt("
                Sql += "会社コード, 出庫番号, 受注番号, 受注番号枝番, 行番号"
                Sql += ",仕入区分 , メーカー, 品名, 型式, 仕入先名, 売単価, 単位"
                Sql += ", 出庫数量, 更新者, 更新日, 倉庫コード, 出庫区分)"
                Sql += " VALUES('"
                Sql += frmC01F10_Login.loginValue.BumonCD '会社コード
                Sql += "', '"
                Sql += LS '出庫番号
                Sql += "', '"
                Sql += denpyoNo '受注番号
                Sql += "', '"
                Sql += denpyoEda '受注番号枝番
                Sql += "', '"
                Sql += "1" '行番号
                Sql += "', '"
                Sql += CommonConst.Sire_KBN_Move.ToString '仕入区分（0:移動）
                Sql += "', '"
                Sql += TxtManufacturer.Text 'メーカー
                Sql += "', '"
                Sql += TxtItemName.Text '品名
                Sql += "', '"
                Sql += TxtSpec.Text '型式
                Sql += "', "
                Sql += IIf(dsNyuko.Tables(RS).Rows(0)("仕入先名").ToString <> "",
                               "'" & dsNyuko.Tables(RS).Rows(0)("仕入先名").ToString & "'",
                               "NULL") '仕入先名
                Sql += ", '"
                Sql += "0" '売単価
                Sql += "', "
                Sql += IIf(dsNyuko.Tables(RS).Rows(0)("単位").ToString <> "",
                               "'" & dsNyuko.Tables(RS).Rows(0)("単位").ToString & "'",
                               "NULL") '単位
                Sql += ", '"
                Sql += TxtQuantityTo.Text '出庫数量
                Sql += "', '"
                Sql += frmC01F10_Login.loginValue.TantoNM '更新者
                Sql += "', '"
                Sql += UtilClass.formatDatetime(dtToday) '更新日
                Sql += "', '"
                Sql += TxtWarehouseSince.Tag '倉庫コード
                Sql += "', '"
                Sql += CommonConst.SHUKO_KBN_NORMAL '出庫区分 （1:通常出庫）
                Sql += "')"

                _db.executeDB(Sql)

                '入出庫登録（出庫）
                '-----------------------------

                't70_inout にデータ登録
                Sql = "INSERT INTO "
                Sql += "Public."
                Sql += "t70_inout("
                Sql += "会社コード, 入出庫区分, 倉庫コード, 伝票番号, 行番号"
                Sql += ", 入出庫種別, メーカー, 品名, 型式, 数量, 単位, 入出庫日"
                Sql += ", 取消区分, 更新者, 更新日, ロケ番号, 仕入区分)"
                'Sql += ", 取消区分, 更新者, 更新日, 仕入区分)"
                Sql += " VALUES('"
                Sql += frmC01F10_Login.loginValue.BumonCD '会社コード
                Sql += "', '"
                Sql += "2" '入出庫区分 1.入庫, 2.出庫
                Sql += "', '"
                Sql += TxtWarehouseSince.Tag.ToString '倉庫コード
                Sql += "', '"
                Sql += LS '伝票番号
                Sql += "', '"
                Sql += "1" '行番号
                'Sql += dsNyuko.Tables(RS).Rows(0)("入庫番号").ToString '伝票番号
                'Sql += "', '"
                'Sql += dsNyuko.Tables(RS).Rows(0)("行番号").ToString '行番号
                Sql += "', '"
                Sql += TxtStorageTypeSince.Tag '入出庫種別
                Sql += "', '"
                Sql += TxtManufacturer.Text 'メーカー
                Sql += "', '"
                Sql += TxtItemName.Text '品名
                Sql += "', '"
                Sql += TxtSpec.Text '型式
                Sql += "', '"
                Sql += TxtQuantityTo.Text '数量
                Sql += "', "
                Sql += IIf(dsNyuko.Tables(RS).Rows(0)("単位").ToString <> "",
                               "'" & dsNyuko.Tables(RS).Rows(0)("単位").ToString & "'",
                               "NULL") '単位
                Sql += ", '"
                'Sql += UtilClass.formatDatetime(DtpProcessingDate.Text) '入出庫日
                Sql += inoutDate '入出庫日
                Sql += "', '"
                Sql += CommonConst.CANCEL_KBN_ENABLED.ToString '取消区分
                Sql += "', '"
                Sql += frmC01F10_Login.loginValue.TantoNM '更新者
                Sql += "', '"
                Sql += UtilClass.formatDatetime(dtToday) '更新日
                Sql += "', '"
                'Sql += IIf(TxtDenpyoNo.Tag.ToString <> "", "'" & TxtDenpyoNo.Tag & TxtLineNumber.Text & "'", "NULL") 'ロケ番号
                Sql += dsNyuko.Tables(RS).Rows(0)("入庫番号").ToString & dsNyuko.Tables(RS).Rows(0)("行番号").ToString 'ロケ番号
                Sql += "', '"
                Sql += CommonConst.Sire_KBN_Move.ToString '仕入区分
                Sql += "')"

                _db.executeDB(Sql)

            End If

            '入庫データ作成対象チェック
            If CmProcessingClassification.SelectedValue = CommonConst.PC_KBN_MOVE Or
                CmStorageTypeTo.SelectedValue = CommonConst.INOUT_KBN_INCREASE Then

                'receiptFlg = True

                Dim WH As String = getSaiban("60", dtToday) '入庫番号新規取得

                '入庫登録
                '-----------------------------
                '入庫基本
                Sql = "INSERT INTO "
                Sql += "Public."
                Sql += "t42_nyukohd("
                Sql += "会社コード, 入庫番号, 発注番号, 発注番号枝番, 仕入先コード, 仕入先名 , 仕入先郵便番号"
                Sql += ",仕入先住所, 仕入先電話番号, 仕入先ＦＡＸ, 仕入先担当者役職, 仕入先担当者名, 支払条件"
                Sql += ",仕入金額, 粗利額, 営業担当者, 入力担当者, 備考,  取消区分, ＶＡＴ, ＰＰＨ"
                Sql += ",入庫日, 登録日, 更新日, 更新者, 客先番号,  営業担当者コード, 入力担当者コード, 倉庫コード"
                Sql += ") VALUES ("
                Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'" '会社コード
                Sql += ", '"
                Sql += WH '入庫番号
                Sql += "', "
                Sql += IIf(dsNyuko.Tables(RS).Rows(0)("発注番号").ToString <> "",
                               "'" & dsNyuko.Tables(RS).Rows(0)("発注番号").ToString & "'",
                               "NULL") '発注番号
                Sql += ", "
                Sql += IIf(dsNyuko.Tables(RS).Rows(0)("発注番号枝番").ToString <> "",
                               "'" & dsNyuko.Tables(RS).Rows(0)("発注番号枝番").ToString & "'",
                               "NULL") '発注番号枝番
                Sql += ", "
                Sql += IIf(dsNyuko.Tables(RS).Rows(0)("仕入先コード").ToString <> "",
                               "'" & dsNyuko.Tables(RS).Rows(0)("仕入先コード").ToString & "'",
                               "NULL") '仕入先コード
                Sql += ", "
                Sql += IIf(dsNyuko.Tables(RS).Rows(0)("仕入先名").ToString <> "",
                               "'" & dsNyuko.Tables(RS).Rows(0)("仕入先名").ToString & "'",
                               "NULL") '仕入先名
                Sql += ", "
                Sql += IIf(dsNyuko.Tables(RS).Rows(0)("仕入先郵便番号").ToString <> "",
                               "'" & dsNyuko.Tables(RS).Rows(0)("仕入先郵便番号").ToString & "'",
                               "NULL") '仕入先郵便番号
                Sql += ", "
                Sql += IIf(dsNyuko.Tables(RS).Rows(0)("仕入先住所").ToString <> "",
                               "'" & dsNyuko.Tables(RS).Rows(0)("仕入先住所").ToString & "'",
                               "NULL") '仕入先住所
                Sql += ", "
                Sql += IIf(dsNyuko.Tables(RS).Rows(0)("仕入先電話番号").ToString <> "",
                               "'" & dsNyuko.Tables(RS).Rows(0)("仕入先電話番号").ToString & "'",
                               "NULL") '仕入先電話番号
                Sql += ", "
                Sql += IIf(dsNyuko.Tables(RS).Rows(0)("仕入先ＦＡＸ").ToString <> "",
                               "'" & dsNyuko.Tables(RS).Rows(0)("仕入先ＦＡＸ").ToString & "'",
                               "NULL") '仕入先ＦＡＸ
                Sql += ", "
                Sql += IIf(dsNyuko.Tables(RS).Rows(0)("仕入先担当者役職").ToString <> "",
                               "'" & dsNyuko.Tables(RS).Rows(0)("仕入先担当者役職").ToString & "'",
                               "NULL") '仕入先担当者役職
                Sql += ", "
                Sql += IIf(dsNyuko.Tables(RS).Rows(0)("仕入先担当者名").ToString <> "",
                               "'" & dsNyuko.Tables(RS).Rows(0)("仕入先担当者名").ToString & "'",
                               "NULL") '仕入先担当者名
                Sql += ", "
                Sql += IIf(dsNyuko.Tables(RS).Rows(0)("支払条件").ToString <> "",
                               "'" & dsNyuko.Tables(RS).Rows(0)("支払条件").ToString & "'",
                               "NULL") '支払条件
                Sql += ", "
                Sql += IIf(dsNyuko.Tables(RS).Rows(0)("仕入金額").ToString <> "",
                               "'" & UtilClass.formatNumber(dsNyuko.Tables(RS).Rows(0)("仕入金額")).ToString & "'",
                               "NULL") '仕入金額
                Sql += ", "
                Sql += IIf(dsNyuko.Tables(RS).Rows(0)("粗利額").ToString <> "",
                               "'" & UtilClass.formatNumber(dsNyuko.Tables(RS).Rows(0)("粗利額")).ToString & "'",
                               "NULL") '粗利額
                Sql += ", "
                Sql += IIf(dsNyuko.Tables(RS).Rows(0)("営業担当者").ToString <> "",
                               "'" & dsNyuko.Tables(RS).Rows(0)("営業担当者").ToString & "'",
                               "NULL") '営業担当者
                Sql += ", '"
                Sql += frmC01F10_Login.loginValue.TantoNM '入力担当者
                Sql += "', "
                Sql += IIf(dsNyuko.Tables(RS).Rows(0)("備考").ToString <> "",
                               "'" & dsNyuko.Tables(RS).Rows(0)("備考").ToString & "'",
                               "NULL") '備考
                Sql += ", '"
                Sql += CommonConst.CANCEL_KBN_ENABLED.ToString '取消区分
                Sql += "', "
                Sql += IIf(dsNyuko.Tables(RS).Rows(0)("ＶＡＴ").ToString <> "",
                               "'" & UtilClass.formatNumber(dsNyuko.Tables(RS).Rows(0)("ＶＡＴ")).ToString & "'",
                               "NULL") 'ＶＡＴ
                Sql += ", "
                Sql += IIf(dsNyuko.Tables(RS).Rows(0)("ＰＰＨ").ToString <> "",
                               "'" & UtilClass.formatNumber(dsNyuko.Tables(RS).Rows(0)("ＰＰＨ")).ToString & "'",
                               "NULL") 'ＰＰＨ
                Sql += ", '"
                Sql += UtilClass.formatDatetime(DtpProcessingDate.Text) '入庫日
                Sql += "', '"
                Sql += UtilClass.formatDatetime(dtToday) '登録日
                Sql += "', '"
                Sql += UtilClass.formatDatetime(dtToday) '更新日
                Sql += "', '"
                Sql += frmC01F10_Login.loginValue.TantoNM '更新者
                Sql += "', "
                Sql += IIf(dsNyuko.Tables(RS).Rows(0)("客先番号").ToString <> "",
                               "'" & dsNyuko.Tables(RS).Rows(0)("客先番号").ToString & "'",
                               "NULL") '客先番号
                Sql += ", "
                Sql += IIf(dsNyuko.Tables(RS).Rows(0)("営業担当者コード").ToString <> "",
                               "'" & dsNyuko.Tables(RS).Rows(0)("営業担当者コード").ToString & "'",
                               "NULL") '営業担当者コード
                Sql += ", '"
                Sql += frmC01F10_Login.loginValue.TantoCD '入力担当者コード
                Sql += "', "
                Sql += IIf(dsNyuko.Tables(RS).Rows(0)("倉庫コード").ToString <> "",
                               "'" & dsNyuko.Tables(RS).Rows(0)("倉庫コード").ToString & "'",
                               "NULL") '倉庫コード
                Sql += ")"

                _db.executeDB(Sql)

                '入庫明細
                Sql = "INSERT INTO "
                Sql += "Public."
                Sql += "t43_nyukodt("
                Sql += "会社コード, 入庫番号, 行番号, 仕入区分, メーカー , 品名, 型式, 仕入先名 "
                Sql += ", 仕入値, 単位, 入庫数量, 備考, 更新者, 更新日, 発注番号, 発注番号枝番 "
                Sql += " )VALUES( "
                Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'" '会社コード
                Sql += ", '"
                Sql += WH '入庫番号
                Sql += "', '"
                Sql += "1" '行番号
                Sql += "', "

                Sql += CommonConst.Sire_KBN_Move.ToString '仕入区分
                Sql += ", '"
                Sql += TxtManufacturer.Text 'メーカー
                Sql += "', '"
                Sql += TxtItemName.Text '品名
                Sql += "', '"
                Sql += TxtSpec.Text '型式
                Sql += "', "
                Sql += IIf(dsNyuko.Tables(RS).Rows(0)("仕入先名").ToString <> "",
                               "'" & dsNyuko.Tables(RS).Rows(0)("仕入先名").ToString & "'",
                               "NULL") '仕入先名
                Sql += ", "
                Sql += IIf(dsNyuko.Tables(RS).Rows(0)("仕入値").ToString <> "",
                               "'" & UtilClass.formatNumber(dsNyuko.Tables(RS).Rows(0)("仕入値")).ToString & "'",
                               "NULL") '仕入値
                Sql += ", "
                Sql += IIf(dsNyuko.Tables(RS).Rows(0)("単位").ToString <> "",
                               "'" & dsNyuko.Tables(RS).Rows(0)("単位").ToString & "'",
                               "NULL") '単位
                Sql += ",'"
                Sql += UtilClass.formatNumber(TxtQuantityTo.Text) '入庫数量
                Sql += "', "

                Sql += IIf(dsNyuko.Tables(RS).Rows(0)("備考").ToString <> "",
                               "'" & dsNyuko.Tables(RS).Rows(0)("備考").ToString & "'",
                               "NULL") '備考
                Sql += ", '"
                Sql += frmC01F10_Login.loginValue.TantoNM '更新者
                Sql += "', '"
                Sql += UtilClass.formatDatetime(dtToday) '更新日
                Sql += "', "
                Sql += IIf(dsNyuko.Tables(RS).Rows(0)("発注番号").ToString <> "",
                               "'" & dsNyuko.Tables(RS).Rows(0)("発注番号").ToString & "'",
                               "NULL") '発注番号
                Sql += ", "
                Sql += IIf(dsNyuko.Tables(RS).Rows(0)("発注番号枝番").ToString <> "",
                               "'" & dsNyuko.Tables(RS).Rows(0)("発注番号枝番").ToString & "'",
                               "NULL") '発注番号枝番
                Sql += ")"

                _db.executeDB(Sql)

                't70_inout にデータ登録
                Sql = "INSERT INTO "
                Sql += "Public."
                Sql += "t70_inout("
                Sql += "会社コード, 入出庫区分, 倉庫コード, 伝票番号, 行番号"
                Sql += ", 入出庫種別, メーカー, 品名, 型式, 数量, 単位, 入出庫日"
                'Sql += ", 取消区分, 更新者, 更新日, ロケ番号, 仕入区分"
                Sql += ", 取消区分, 更新者, 更新日, 仕入区分"
                Sql += " )VALUES('"
                Sql += frmC01F10_Login.loginValue.BumonCD '会社コード
                Sql += "', '"
                Sql += "1" '入出庫区分 1.入庫, 2.出庫
                Sql += "', '"
                Sql += CmWarehouseTo.SelectedValue.ToString '倉庫コード
                Sql += "', '"
                Sql += WH '伝票番号
                Sql += "', '"
                Sql += "1" '行番号
                Sql += "', '"

                '棚卸増選択時は、入出庫種別を引き継ぐ
                If CmStorageTypeTo.SelectedValue = CommonConst.INOUT_KBN_INCREASE Then
                    Sql += TxtStorageTypeSince.Tag '入出庫種別
                Else
                    Sql += CmStorageTypeTo.SelectedValue '入出庫種別
                End If

                Sql += "', '"
                Sql += TxtManufacturer.Text 'メーカー
                Sql += "', '"
                Sql += TxtItemName.Text '品名
                Sql += "', '"
                Sql += TxtSpec.Text '型式
                Sql += "', '"
                Sql += UtilClass.formatNumber(TxtQuantityTo.Text) '数量
                Sql += "', "
                Sql += IIf(dsNyuko.Tables(RS).Rows(0)("単位").ToString <> "",
                                "'" & dsNyuko.Tables(RS).Rows(0)("単位").ToString & "'",
                                "NULL") '単位
                Sql += ", '"
                'Sql += UtilClass.formatDatetime(DtpProcessingDate.Text) '入出庫日
                Sql += inoutDate '入出庫日
                Sql += "', '"
                Sql += CommonConst.CANCEL_KBN_ENABLED.ToString '取消区分
                Sql += "', '"
                Sql += frmC01F10_Login.loginValue.TantoNM '更新者
                Sql += "', '"
                Sql += UtilClass.formatDatetime(dtToday) '更新日
                'Sql += "', '"
                'Sql += dsNyuko.Tables(RS).Rows(0)("入庫番号").ToString & dsNyuko.Tables(RS).Rows(0)("行番号").ToString 'ロケ番号
                Sql += "', '"
                Sql += CommonConst.Sire_KBN_Zaiko.ToString '仕入区分
                Sql += "')"

                _db.executeDB(Sql)

            End If

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

        ''一覧をリセット
        'createPCKbn() '処理区分コンボボックス
        'createWarehouseToCombobox() '倉庫コンボボックス
        'createInOutKbn("0,1") '入出庫種別コンボボックス

        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()

    End Sub

    '戻るボタン押下時
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
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

    '処理区分のコンボボックスを作成
    Private Sub createPCKbn(Optional ByRef prmVal As String = "")
        Dim Sql As String = ""
        Dim strViewText As String = ""

        CmProcessingClassification.DisplayMember = "Text"
        CmProcessingClassification.ValueMember = "Value"

        Sql = " AND "
        Sql += "固定キー ILIKE '" & CommonConst.PC_CODE & "'"
        Sql += " AND "
        Sql += "( 可変キー ILIKE '" & CommonConst.PC_KBN_MOVE & "'"
        Sql += " OR "
        Sql += " 可変キー ILIKE '" & CommonConst.PC_KBN_ADJUSTMENT & "' ) "
        Sql += " ORDER BY 表示順"

        'リードタイムのリストを汎用マスタから取得
        Dim dsHanyo As DataSet = getDsData("m90_hanyo", Sql)

        Dim tb As New DataTable("Table")
        tb.Columns.Add("Text", GetType(String))
        tb.Columns.Add("Value", GetType(String))

        strViewText = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "文字２", "文字１")

        For x As Integer = 0 To dsHanyo.Tables(RS).Rows.Count - 1
            tb.Rows.Add(dsHanyo.Tables(RS).Rows(x)(strViewText), dsHanyo.Tables(RS).Rows(x)("可変キー"))
        Next

        CmProcessingClassification.DataSource = tb
        CmProcessingClassification.SelectedIndex = 0

    End Sub

    ''対象倉庫のコンボボックスを作成
    'Private Sub createWarehouseFromCombobox()
    '    CmWarehouseTo.DisplayMember = "Text"
    '    CmWarehouseTo.ValueMember = "Value"

    '    Dim Sql As String = " AND 無効フラグ = '" & CommonConst.FLAG_ENABLED & "'"

    '    Dim ds As DataSet = getDsData("m20_warehouse", Sql)

    '    Dim tb As New DataTable
    '    tb.Columns.Add("Text", GetType(String))
    '    tb.Columns.Add("Value", GetType(String))

    '    For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
    '        tb.Rows.Add(ds.Tables(RS).Rows(i)("名称"), ds.Tables(RS).Rows(i)("倉庫コード"))
    '    Next

    '    CmWarehouseTo.DataSource = tb
    '    CmWarehouseTo.SelectedIndex = 0

    'End Sub

    '対象倉庫のコンボボックスを作成
    Private Sub createWarehouseToCombobox(Optional ByRef prmVal As String = "")

        CmWarehouseTo.DisplayMember = "Text"
        CmWarehouseTo.ValueMember = "Value"

        Dim Sql As String = " AND 無効フラグ = '" & CommonConst.FLAG_ENABLED & "'"

        If prmVal <> "" And CmProcessingClassification.SelectedValue = CommonConst.PC_KBN_MOVE Then
            Sql += " AND "
            Sql += "倉庫コード <> '" & prmVal & "'"
        End If

        Dim ds As DataSet = getDsData("m20_warehouse", Sql)

        Dim tb As New DataTable
        tb.Columns.Add("Text", GetType(String))
        tb.Columns.Add("Value", GetType(String))

        For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
            tb.Rows.Add(ds.Tables(RS).Rows(i)("名称"), ds.Tables(RS).Rows(i)("倉庫コード"))
        Next

        CmWarehouseTo.DataSource = tb
        CmWarehouseTo.SelectedIndex = 0

    End Sub

    '処理区分のコンボボックスを作成
    Private Sub createInOutKbn(Optional ByRef prmVal As String = "")
        Dim Sql As String = ""
        Dim strViewText As String = ""
        Dim strArrayData As String() = prmVal.Split(","c)

        CmStorageTypeTo.DisplayMember = "Text"
        CmStorageTypeTo.ValueMember = "Value"

        Sql = " AND "
        Sql += "固定キー ILIKE '" & CommonConst.INOUT_CLASS & "'"

        If strArrayData.Length <> 0 Then
            Sql += " AND ( "
            For i As Integer = 0 To strArrayData.Length - 1
                Sql += IIf(i > 0, " OR ", "")
                Sql += "可変キー ILIKE '" & strArrayData(i) & "'"
            Next
            Sql += " ) "
        End If

        Sql += " ORDER BY 表示順"

        'リードタイムのリストを汎用マスタから取得
        Dim dsHanyo As DataSet = getDsData("m90_hanyo", Sql)

        Dim tb As New DataTable("Table")
        tb.Columns.Add("Text", GetType(String))
        tb.Columns.Add("Value", GetType(String))

        strViewText = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "文字２", "文字１")

        For x As Integer = 0 To dsHanyo.Tables(RS).Rows.Count - 1
            tb.Rows.Add(dsHanyo.Tables(RS).Rows(x)(strViewText), dsHanyo.Tables(RS).Rows(x)("可変キー"))
        Next

        CmStorageTypeTo.DataSource = tb
        CmStorageTypeTo.SelectedIndex = 0

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

    '処理区分プルダウン変更時
    Private Sub CmProcessingClassification_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmProcessingClassification.SelectedIndexChanged

        '処理区分：移動 のとき「入出庫種別」は無効化
        If CmProcessingClassification.SelectedValue = CommonConst.PC_KBN_MOVE Then
            CmStorageTypeTo.Enabled = False
            CmWarehouseTo.Enabled = True
            'CmWarehouseTo.Enabled = True

            '入出庫種別のプルダウン内容を変更
            createInOutKbn("0,1")

            '入出庫種別を移動元 = 移動先にする
            If TxtStorageTypeSince.Tag <> "" Then
                CmStorageTypeTo.SelectedValue = TxtStorageTypeSince.Tag
            End If

            If TxtWarehouseSince.Tag <> Nothing Then
                createWarehouseToCombobox(TxtWarehouseSince.Tag.ToString) '対象外にする項目を引き渡す
            End If

        Else
            '処理区分：調整 のとき「入出庫種別」は無効化
            CmWarehouseTo.Enabled = False
            CmStorageTypeTo.Enabled = True
            'CmInOutKbn.Enabled = True

            '入出庫種別のプルダウン内容を変更
            createInOutKbn("2,3,4")

            '倉庫を移動元 = 移動先にする
            If TxtWarehouseSince.Tag <> "" Then
                createWarehouseToCombobox()
                CmWarehouseTo.SelectedValue = TxtWarehouseSince.Tag
            End If
        End If

    End Sub

    ''メーカー入力欄ダブルクリック時
    'Private Sub TxtCustomerPO_TextChanged(sender As Object, e As EventArgs) Handles TxtManufacturer.TextChanged
    '    setItem("メーカー")
    'End Sub

    ''品名入力欄ダブルクリック時
    'Private Sub TxtItemName_TextChanged(sender As Object, e As EventArgs) Handles TxtItemName.TextChanged
    '    setItem("品名")
    'End Sub

    ''型式入力欄ダブルクリック時
    'Private Sub TxtSpec_TextChanged(sender As Object, e As EventArgs) Handles TxtSpec.TextChanged
    '    setItem("型式")
    'End Sub

    '商品検索
    Private Sub setItem(ByVal prmColumn As String)

        Dim Maker As String = TxtManufacturer.Text
        Dim Item As String = TxtItemName.Text
        Dim Model As String = TxtSpec.Text

        '各項目チェック
        If Maker Is Nothing And Item Is Nothing Then
            'メーカー、品名を入力してください。
            _msgHd.dspMSG("chkManufacturerItemNameError", frmC01F10_Login.loginValue.Language)

            Return

        ElseIf Maker Is Nothing Then
            'メーカーを入力してください。
            _msgHd.dspMSG("chkManufacturerError", frmC01F10_Login.loginValue.Language)
            Return
        End If

        Dim openForm As Form = Nothing
        openForm = New MakerSearch(_msgHd, _db, Me, 0, 0, Maker, Item, Model, prmColumn)   '処理選択
        openForm.Show(Me)
        Me.Enabled = False

    End Sub

    Private Sub TxtManufacturer_DoubleClick_1(sender As Object, e As EventArgs) Handles TxtManufacturer.DoubleClick
        setItem("メーカー")
    End Sub

    Private Sub TxtItemName_DoubleClick(sender As Object, e As EventArgs) Handles TxtItemName.DoubleClick
        setItem("品名")
    End Sub

    Private Sub TxtSpec_DoubleClick(sender As Object, e As EventArgs) Handles TxtSpec.DoubleClick
        setItem("型式")
    End Sub

    '在庫検索
    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        Dim manufactuer As String = TxtManufacturer.Text
        Dim itemName As String = TxtItemName.Text
        Dim spec As String = TxtSpec.Text

        manufactuer = IIf(manufactuer <> Nothing, manufactuer, "")
        itemName = IIf(itemName <> Nothing, itemName, "")
        spec = IIf(spec <> Nothing, spec, "")

        Dim openForm As Form = Nothing
        openForm = New StockSearch(_msgHd, _db, _langHd, Me, manufactuer, itemName, spec, "Select")
        openForm.Show(Me)
        Me.Enabled = False

    End Sub

    '移動元の情報が変わるときに、移動先の情報を適宜書き換える
    Public Sub setMovingDestination()
        '処理区分：移動 のとき「入出庫種別」は無効化
        If CmProcessingClassification.SelectedValue = CommonConst.PC_KBN_MOVE Then
            CmStorageTypeTo.Enabled = False
            CmWarehouseTo.Enabled = True
            'CmWarehouseTo.Enabled = True

            '入出庫種別のプルダウン内容を変更
            createInOutKbn("0,1")

            '入出庫種別を移動元 = 移動先にする
            If TxtStorageTypeSince.Tag <> "" Then
                CmStorageTypeTo.SelectedValue = TxtStorageTypeSince.Tag
            End If
            '倉庫を移動元 = 移動先にする
            If TxtWarehouseSince.Tag <> "" Then
                CmWarehouseTo.SelectedValue = TxtWarehouseSince.Tag
            End If
        Else
            '処理区分：調整 のとき「入出庫種別」は無効化
            CmWarehouseTo.Enabled = False
            CmStorageTypeTo.Enabled = True
            'CmInOutKbn.Enabled = True

            '入出庫種別のプルダウン内容を変更
            createInOutKbn("2,3,4")

            '倉庫を移動元 = 移動先にする
            If TxtWarehouseSince.Tag <> "" Then
                CmWarehouseTo.SelectedValue = TxtWarehouseSince.Tag
            End If
        End If

        If TxtWarehouseSince.Tag <> Nothing Then
            createWarehouseToCombobox(TxtWarehouseSince.Tag.ToString) '対象外にする項目を引き渡す
        End If

    End Sub

    Private Sub TxtQuantityTo_Leave(sender As Object, e As EventArgs) Handles TxtQuantityTo.Leave

        If IsNumeric(TxtQuantityTo.Text) = False Then
            '数値チェックエラーメッセージを表示
            _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
            TxtQuantityTo.Focus()
            Exit Sub
        End If

        If TxtQuantityFrom.Text = "" Then
            TxtQuantityFrom.Text = "0"
        End If

        If Long.Parse(TxtQuantityFrom.Text) < Long.Parse(TxtQuantityTo.Text) Then
            '入力可能数量を超えているメッセージを表示
            _msgHd.dspMSG("chkMovingQuantityError", frmC01F10_Login.loginValue.Language)
            TxtQuantityTo.Focus()
            Exit Sub
        End If

    End Sub
End Class