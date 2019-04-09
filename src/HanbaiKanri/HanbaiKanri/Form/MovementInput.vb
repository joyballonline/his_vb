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
        DgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.DisplayedCells
    End Sub

    '画面表示時
    Private Sub MstHanyoue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        LblMode.Text = "参照モード"

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblMode.Text = OrderStatus & " Mode"

            BtnRegistration.Text = "Registration"
            BtnBack.Text = "Back"

            DgvList.Columns("メーカー").HeaderText = "Manufacturer"
            DgvList.Columns("品名").HeaderText = "ItemName"
            DgvList.Columns("型式").HeaderText = "Spec"
        End If

        DtpProcessingDate.Text = DateTime.Today

        createWarehouseFromCombobox() '倉庫コンボボックス
        createWarehouseToCombobox() '倉庫コンボボックス
        createPCKbn() '処理区分コンボボックス
        createInOutKbn() '入出庫種別コンボボックス

    End Sub

    '登録ボタン押下時
    Private Sub BtnRegistration_Click(sender As Object, e As EventArgs) Handles BtnRegistration.Click
        Dim dtToday As DateTime = DateTime.Now
        Dim strToday As String = UtilClass.strFormatDate(DateTime.Now)

        Dim Sql As String = ""
        Dim reccnt As Integer = 0 'DB用（デフォルト）


        '対象データがないメッセージを表示
        If DgvList.Rows.Count = 0 Then
            _msgHd.dspMSG("NonData", frmC01F10_Login.loginValue.Language)
            Exit Sub
        End If

        '在庫があるかチェック

        Dim denpyoNo As String = "00000000000000"
        Dim denpyoEda As String = "00"

        Try

            Select Case CmProcessingClassification.SelectedValue

            '移動
                Case 1

                    If CmWarehouseFrom.SelectedValue = CmWarehouseTo.SelectedValue Then
                        _msgHd.dspMSG("chkWarehouseMoveError", frmC01F10_Login.loginValue.Language)
                        Exit Sub
                    End If

                    '出庫, 入庫, INOUT
                    '採番データを取得・更新
                    Dim LS As String = getSaiban("70", strToday)
                    Dim WH As String = getSaiban("60", strToday)

                    '出庫基本
                    Sql = "INSERT INTO "
                    Sql += "Public."
                    Sql += "t44_shukohd("
                    Sql += "会社コード, 出庫番号, 見積番号, 見積番号枝番, 受注番号, 受注番号枝番"
                    Sql += ", 入力担当者, 出庫日, 登録日, 取消区分, 更新日, 更新者, 入力担当者コード)"
                    Sql += " VALUES('"
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
                    Sql += frmC01F10_Login.loginValue.TantoCD '更新者
                    Sql += "', '"
                    Sql += frmC01F10_Login.loginValue.TantoCD '入力担当者コード
                    Sql += "')"

                    _db.executeDB(Sql)

                    '入庫基本
                    Sql = "INSERT INTO "
                    Sql += "Public."
                    Sql += "t42_nyukohd("
                    Sql += "会社コード, 入庫番号, 発注番号, 発注番号枝番, 仕入金額, 粗利額 , 入力担当者"
                    Sql += ", 取消区分, ＶＡＴ, ＰＰＨ, 入庫日, 登録日, 更新日, 更新者, 入力担当者コード"
                    Sql += ", 倉庫コード"
                    Sql += ") VALUES ("
                    Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'" '会社コード
                    Sql += ", '"
                    Sql += WH '入庫番号
                    Sql += "', '"
                    Sql += denpyoNo '発注番号
                    Sql += "', '"
                    Sql += denpyoEda '発注番号枝番
                    Sql += "', '"
                    Sql += "0" '仕入金額
                    Sql += "', '"
                    Sql += "0" '粗利額
                    Sql += "', '"
                    Sql += frmC01F10_Login.loginValue.TantoNM '入力担当者
                    Sql += "', '"
                    Sql += CommonConst.CANCEL_KBN_ENABLED.ToString '取消区分
                    Sql += "', '"
                    Sql += "0" 'ＶＡＴ
                    Sql += "', '"
                    Sql += "0" 'ＰＰＨ
                    Sql += "', '"
                    Sql += UtilClass.strFormatDate(DtpProcessingDate.Text) '入庫日
                    Sql += "', '"
                    Sql += strToday '登録日
                    Sql += "', '"
                    Sql += strToday '更新日
                    Sql += "', '"
                    Sql += frmC01F10_Login.loginValue.TantoNM '更新者
                    Sql += "', '"
                    Sql += frmC01F10_Login.loginValue.TantoCD '入力担当者コード
                    Sql += "', '"
                    Sql += CmWarehouseTo.SelectedValue.ToString '倉庫コード
                    Sql += "')"

                    _db.executeDB(Sql)

                    For i As Integer = 0 To DgvList.Rows.Count() - 1

                        If DgvList.Rows(i).Cells("数量").Value > 0 Then

                            '出庫明細
                            Sql = "INSERT INTO "
                            Sql += "Public."
                            Sql += "t45_shukodt("
                            Sql += "会社コード, 出庫番号, 受注番号, 受注番号枝番, 行番号"
                            Sql += ",仕入区分 , メーカー, 品名, 型式, 売単価, 出庫数量"
                            Sql += ", 更新者, 更新日, 倉庫コード)"
                            Sql += " VALUES('"
                            Sql += frmC01F10_Login.loginValue.BumonCD '会社コード
                            Sql += "', '"
                            Sql += LS '出庫番号
                            Sql += "', '"
                            Sql += denpyoNo '受注番号
                            Sql += "', '"
                            Sql += denpyoEda '受注番号枝番
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("No").Value.ToString '行番号
                            Sql += "', '"
                            Sql += "0" '仕入区分
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("メーカー").Value.ToString 'メーカー
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("品名").Value.ToString '品名
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("型式").Value.ToString '型式
                            Sql += "', '"
                            Sql += "0" '売単価
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("数量").Value.ToString '出庫数量
                            Sql += "', '"
                            Sql += frmC01F10_Login.loginValue.TantoNM '更新者
                            Sql += "', '"
                            Sql += UtilClass.formatDatetime(dtToday) '更新日
                            Sql += "', '"
                            Sql += CmWarehouseFrom.SelectedValue.ToString '倉庫コード
                            Sql += "')"

                            _db.executeDB(Sql)

                            't70_inout にデータ登録
                            Sql = "INSERT INTO "
                            Sql += "Public."
                            Sql += "t70_inout("
                            Sql += "会社コード, 入出庫区分, 倉庫コード, 伝票番号, 行番号"
                            Sql += ", 引当区分, メーカー, 品名, 型式, 数量, 入出庫日"
                            Sql += ", 取消区分, 更新者, 更新日)"
                            Sql += " VALUES('"
                            Sql += frmC01F10_Login.loginValue.BumonCD '会社コード
                            Sql += "', '"
                            Sql += "2" '入出庫区分 1.入庫, 2.出庫
                            Sql += "', '"
                            Sql += CmWarehouseFrom.SelectedValue.ToString '倉庫コード
                            Sql += "', '"
                            Sql += LS '伝票番号
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("No").Value.ToString '行番号
                            Sql += "', '"
                            Sql += CommonConst.AC_KBN_NORMAL.ToString '引当区分
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("メーカー").Value.ToString 'メーカー
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("品名").Value.ToString '品名
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("型式").Value.ToString '型式
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("数量").Value.ToString '数量
                            Sql += "', '"
                            Sql += UtilClass.formatDatetime(DtpProcessingDate.Text) '入出庫日
                            Sql += "', '"
                            Sql += CommonConst.CANCEL_KBN_ENABLED.ToString '取消区分
                            Sql += "', '"
                            Sql += frmC01F10_Login.loginValue.TantoNM '更新者
                            Sql += "', '"
                            Sql += UtilClass.formatDatetime(dtToday) '更新日
                            Sql += "')"

                            _db.executeDB(Sql)

                            '入庫明細
                            Sql = "INSERT INTO "
                            Sql += "Public."
                            Sql += "t43_nyukodt("
                            Sql += "会社コード, 入庫番号, 発注番号, 発注番号枝番, 行番号 ,仕入区分"
                            Sql += ", メーカー, 品名, 型式, 仕入値, 入庫数量, 更新者, 更新日"
                            Sql += " )VALUES( "
                            Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'" '会社コード
                            Sql += ", '"
                            Sql += WH '入庫番号
                            Sql += "', '"
                            Sql += denpyoNo '発注番号
                            Sql += "', '"
                            Sql += denpyoEda '発注番号枝番
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("No").Value.ToString '行番号
                            Sql += "', '"
                            Sql += "0" '仕入区分
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("メーカー").Value.ToString 'メーカー
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("品名").Value.ToString '品名
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("型式").Value.ToString '型式
                            Sql += "', '"
                            Sql += "0" '仕入値
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("数量").Value.ToString '入庫数量
                            Sql += "', '"
                            Sql += frmC01F10_Login.loginValue.TantoNM '更新者
                            Sql += "', '"
                            Sql += strToday '更新日
                            Sql += "')"

                            _db.executeDB(Sql)

                            't70_inout にデータ登録
                            Sql = "INSERT INTO "
                            Sql += "Public."
                            Sql += "t70_inout("
                            Sql += "会社コード, 入出庫区分, 倉庫コード, 伝票番号, 行番号"
                            Sql += ", 引当区分, メーカー, 品名, 型式, 数量, 入出庫日"
                            Sql += ", 取消区分, 更新者, 更新日)"
                            Sql += " VALUES('"
                            Sql += frmC01F10_Login.loginValue.BumonCD '会社コード
                            Sql += "', '"
                            Sql += "1" '入出庫区分 1.入庫, 2.出庫
                            Sql += "', '"
                            Sql += CmWarehouseTo.SelectedValue.ToString '倉庫コード
                            Sql += "', '"
                            Sql += WH '伝票番号
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("No").Value.ToString '行番号
                            Sql += "', '"
                            Sql += CommonConst.AC_KBN_NORMAL.ToString '引当区分
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("メーカー").Value.ToString 'メーカー
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("品名").Value.ToString '品名
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("型式").Value.ToString '型式
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("数量").Value.ToString '数量
                            Sql += "', '"
                            Sql += UtilClass.formatDatetime(DtpProcessingDate.Text) '入出庫日
                            Sql += "', '"
                            Sql += CommonConst.CANCEL_KBN_ENABLED.ToString '取消区分
                            Sql += "', '"
                            Sql += frmC01F10_Login.loginValue.TantoNM '更新者
                            Sql += "', '"
                            Sql += UtilClass.formatDatetime(dtToday) '更新日
                            Sql += "')"

                            _db.executeDB(Sql)

                        End If

                    Next

            '出庫
                Case 2
                    '出庫, INOUT
                    '出庫, INOUT
                    '採番データを取得・更新
                    Dim LS As String = getSaiban("70", strToday)

                    '出庫基本
                    Sql = "INSERT INTO "
                    Sql += "Public."
                    Sql += "t44_shukohd("
                    Sql += "会社コード, 出庫番号, 見積番号, 見積番号枝番, 受注番号, 受注番号枝番"
                    Sql += ", 入力担当者, 出庫日, 登録日, 取消区分, 更新日, 更新者, 入力担当者コード)"
                    Sql += " VALUES('"
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
                    Sql += frmC01F10_Login.loginValue.TantoCD '更新者
                    Sql += "', '"
                    Sql += frmC01F10_Login.loginValue.TantoCD '入力担当者コード
                    Sql += "')"

                    _db.executeDB(Sql)

                    For i As Integer = 0 To DgvList.Rows.Count() - 1

                        If DgvList.Rows(i).Cells("数量").Value > 0 Then

                            '出庫明細
                            '出庫明細
                            Sql = "INSERT INTO "
                            Sql += "Public."
                            Sql += "t45_shukodt("
                            Sql += "会社コード, 出庫番号, 受注番号, 受注番号枝番, 行番号"
                            Sql += ",仕入区分 , メーカー, 品名, 型式, 売単価, 出庫数量"
                            Sql += ", 更新者, 更新日, 倉庫コード)"
                            Sql += " VALUES('"
                            Sql += frmC01F10_Login.loginValue.BumonCD '会社コード
                            Sql += "', '"
                            Sql += LS '出庫番号
                            Sql += "', '"
                            Sql += denpyoNo '受注番号
                            Sql += "', '"
                            Sql += denpyoEda '受注番号枝番
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("No").Value.ToString '行番号
                            Sql += "', '"
                            Sql += "0" '仕入区分
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("メーカー").Value.ToString 'メーカー
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("品名").Value.ToString '品名
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("型式").Value.ToString '型式
                            Sql += "', '"
                            Sql += "0" '売単価
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("数量").Value.ToString '出庫数量
                            Sql += "', '"
                            Sql += frmC01F10_Login.loginValue.TantoNM '更新者
                            Sql += "', '"
                            Sql += UtilClass.formatDatetime(dtToday) '更新日
                            Sql += "', '"
                            Sql += CmWarehouseFrom.SelectedValue.ToString '倉庫コード
                            Sql += "')"

                            _db.executeDB(Sql)

                            't70_inout にデータ登録
                            Sql = "INSERT INTO "
                            Sql += "Public."
                            Sql += "t70_inout("
                            Sql += "会社コード, 入出庫区分, 倉庫コード, 伝票番号, 行番号, 入出庫種別"
                            Sql += ", 引当区分, メーカー, 品名, 型式, 数量, 入出庫日"
                            Sql += ", 取消区分, 更新者, 更新日)"
                            Sql += " VALUES('"
                            Sql += frmC01F10_Login.loginValue.BumonCD '会社コード
                            Sql += "', '"
                            Sql += "2" '入出庫区分 1.入庫, 2.出庫
                            Sql += "', '"
                            Sql += CmWarehouseFrom.SelectedValue.ToString '倉庫コード
                            Sql += "', '"
                            Sql += LS '伝票番号
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("No").Value.ToString '行番号
                            Sql += "', '"
                            Sql += CmInOutKbn.SelectedValue.ToString '入出庫種別
                            Sql += "', '"
                            Sql += CommonConst.AC_KBN_NORMAL.ToString '引当区分
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("メーカー").Value.ToString 'メーカー
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("品名").Value.ToString '品名
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("型式").Value.ToString '型式
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("数量").Value.ToString '数量
                            Sql += "', '"
                            Sql += UtilClass.formatDatetime(DtpProcessingDate.Text) '入出庫日
                            Sql += "', '"
                            Sql += CommonConst.CANCEL_KBN_ENABLED.ToString '取消区分
                            Sql += "', '"
                            Sql += frmC01F10_Login.loginValue.TantoNM '更新者
                            Sql += "', '"
                            Sql += UtilClass.formatDatetime(dtToday) '更新日
                            Sql += "')"

                            _db.executeDB(Sql)

                        End If

                    Next

            '入庫
                Case 3

                    '入庫, INOUT
                    '採番データを取得・更新
                    Dim WH As String = getSaiban("60", strToday)

                    '入庫, INOUT
                    '入庫基本
                    Sql = "INSERT INTO "
                    Sql += "Public."
                    Sql += "t42_nyukohd("
                    Sql += "会社コード, 入庫番号, 発注番号, 発注番号枝番, 仕入金額, 粗利額 , 入力担当者"
                    Sql += ", 取消区分, ＶＡＴ, ＰＰＨ, 入庫日, 登録日, 更新日, 更新者, 入力担当者コード"
                    Sql += ", 倉庫コード"
                    Sql += ") VALUES ("
                    Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'" '会社コード
                    Sql += ", '"
                    Sql += WH '入庫番号
                    Sql += "', '"
                    Sql += denpyoNo '発注番号
                    Sql += "', '"
                    Sql += denpyoEda '発注番号枝番
                    Sql += "', '"
                    Sql += "0" '仕入金額
                    Sql += "', '"
                    Sql += "0" '粗利額
                    Sql += "', '"
                    Sql += frmC01F10_Login.loginValue.TantoNM '入力担当者
                    Sql += "', '"
                    Sql += CommonConst.CANCEL_KBN_ENABLED.ToString '取消区分
                    Sql += "', '"
                    Sql += "0" 'ＶＡＴ
                    Sql += "', '"
                    Sql += "0" 'ＰＰＨ
                    Sql += "', '"
                    Sql += UtilClass.strFormatDate(DtpProcessingDate.Text) '入庫日
                    Sql += "', '"
                    Sql += strToday '登録日
                    Sql += "', '"
                    Sql += strToday '更新日
                    Sql += "', '"
                    Sql += frmC01F10_Login.loginValue.TantoNM '更新者
                    Sql += "', '"
                    Sql += frmC01F10_Login.loginValue.TantoCD '入力担当者コード
                    Sql += "', '"
                    Sql += CmWarehouseFrom.SelectedValue.ToString '倉庫コード
                    Sql += "')"

                    _db.executeDB(Sql)

                    For i As Integer = 0 To DgvList.Rows.Count() - 1

                        If DgvList.Rows(i).Cells("数量").Value > 0 Then


                            '入庫明細
                            Sql = "INSERT INTO "
                            Sql += "Public."
                            Sql += "t43_nyukodt("
                            Sql += "会社コード, 入庫番号, 発注番号, 発注番号枝番, 行番号 ,仕入区分"
                            Sql += ", メーカー, 品名, 型式, 仕入値, 入庫数量, 更新者, 更新日"
                            Sql += " )VALUES( "
                            Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'" '会社コード
                            Sql += ", '"
                            Sql += WH '入庫番号
                            Sql += "', '"
                            Sql += denpyoNo '発注番号
                            Sql += "', '"
                            Sql += denpyoEda '発注番号枝番
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("No").Value.ToString '行番号
                            Sql += "', '"
                            Sql += "0" '仕入区分
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("メーカー").Value.ToString 'メーカー
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("品名").Value.ToString '品名
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("型式").Value.ToString '型式
                            Sql += "', '"
                            Sql += "0" '仕入値
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("数量").Value.ToString '入庫数量
                            Sql += "', '"
                            Sql += frmC01F10_Login.loginValue.TantoNM '更新者
                            Sql += "', '"
                            Sql += strToday '更新日
                            Sql += "')"

                            _db.executeDB(Sql)

                            't70_inout にデータ登録
                            Sql = "INSERT INTO "
                            Sql += "Public."
                            Sql += "t70_inout("
                            Sql += "会社コード, 入出庫区分, 倉庫コード, 伝票番号, 行番号, 入出庫種別"
                            Sql += ", 引当区分, メーカー, 品名, 型式, 数量, 入出庫日"
                            Sql += ", 取消区分, 更新者, 更新日)"
                            Sql += " VALUES('"
                            Sql += frmC01F10_Login.loginValue.BumonCD '会社コード
                            Sql += "', '"
                            Sql += "1" '入出庫区分 1.入庫, 2.出庫
                            Sql += "', '"
                            Sql += CmWarehouseTo.SelectedValue.ToString '倉庫コード
                            Sql += "', '"
                            Sql += WH '伝票番号
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("No").Value.ToString '行番号
                            Sql += "', '"
                            Sql += CmInOutKbn.SelectedValue.ToString '入出庫種別
                            Sql += "', '"
                            Sql += CmInOutKbn.SelectedValue.ToString '引当区分
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("メーカー").Value.ToString 'メーカー
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("品名").Value.ToString '品名
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("型式").Value.ToString '型式
                            Sql += "', '"
                            Sql += DgvList.Rows(i).Cells("数量").Value.ToString '数量
                            Sql += "', '"
                            Sql += UtilClass.formatDatetime(DtpProcessingDate.Text) '入出庫日
                            Sql += "', '"
                            Sql += CommonConst.CANCEL_KBN_ENABLED.ToString '取消区分
                            Sql += "', '"
                            Sql += frmC01F10_Login.loginValue.TantoNM '更新者
                            Sql += "', '"
                            Sql += UtilClass.formatDatetime(dtToday) '更新日
                            Sql += "')"

                            _db.executeDB(Sql)

                        End If

                    Next

            End Select

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

        '一覧をリセット
        createWarehouseFromCombobox() '倉庫コンボボックス
        createWarehouseToCombobox() '倉庫コンボボックス
        createPCKbn() '処理区分コンボボックス
        createInOutKbn() '入出庫種別コンボボックス
        DgvList.Rows.Clear()


    End Sub

    '戻るボタン押下時
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    '行追加（DGVの最終行に追加）
    Private Sub BtnRowsAdd_Click(sender As Object, e As EventArgs) Handles BtnRowsAdd.Click
        DgvList.Rows.Add()

        '行番号の振り直し
        Dim index As Integer = DgvList.Rows.Count()
        Dim No As Integer = 1
        For c As Integer = 0 To index - 1
            DgvList.Rows(c).Cells("No").Value = No
            No += 1
        Next c
    End Sub

    '選択行の削除（削除時に金額の再計算、Noの再採番）
    Private Sub BtnRowsDel_Click(sender As Object, e As EventArgs) Handles BtnRowsDel.Click

        For Each r As DataGridViewCell In DgvList.SelectedCells
            DgvList.Rows.RemoveAt(r.RowIndex)
        Next r

        '行番号の振り直し
        Dim index As Integer = DgvList.Rows.Count()
        Dim No As Integer = 1
        For c As Integer = 0 To index - 1
            DgvList.Rows(c).Cells("No").Value = No
            No += 1
        Next c

    End Sub

    'キーイベント取得
    Private Sub DgvItemList_KeyDown(sender As Object, e As KeyEventArgs) Handles DgvList.KeyDown
        'F4キー押下
        Dim currentColumn As String = DgvList.Columns(DgvList.CurrentCell.ColumnIndex).Name

        Dim manufactuer As String = DgvList("メーカー", DgvList.CurrentCell.RowIndex).Value
        Dim itemName As String = DgvList("品名", DgvList.CurrentCell.RowIndex).Value
        Dim spec As String = DgvList("型式", DgvList.CurrentCell.RowIndex).Value

        '仕入区分[ 在庫引当 ] + 数量にいた場合
        If e.KeyData = Keys.F4 Then

            If currentColumn = "数量" Then
                manufactuer = IIf(manufactuer <> Nothing, manufactuer, "")
                itemName = IIf(itemName <> Nothing, itemName, "")
                spec = IIf(spec <> Nothing, spec, "")

                Dim openForm As Form = Nothing
                openForm = New StockSearch(_msgHd, _db, _langHd, Me, manufactuer, itemName, spec)
                openForm.Show()
                Me.Enabled = False

            End If

        End If
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
            Sql += FormatDateTime(today)
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

    '対象倉庫のコンボボックスを作成
    Private Sub createWarehouseFromCombobox()
        CmWarehouseFrom.DisplayMember = "Text"
        CmWarehouseFrom.ValueMember = "Value"

        Dim Sql As String = " AND 無効フラグ = '" & CommonConst.FLAG_ENABLED & "'"

        Dim ds As DataSet = getDsData("m20_warehouse", Sql)

        Dim tb As New DataTable
        tb.Columns.Add("Text", GetType(String))
        tb.Columns.Add("Value", GetType(String))

        For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
            tb.Rows.Add(ds.Tables(RS).Rows(i)("名称"), ds.Tables(RS).Rows(i)("倉庫コード"))
        Next

        CmWarehouseFrom.DataSource = tb
        CmWarehouseFrom.SelectedIndex = 0

    End Sub

    '対象倉庫のコンボボックスを作成
    Private Sub createWarehouseToCombobox()
        CmWarehouseTo.DisplayMember = "Text"
        CmWarehouseTo.ValueMember = "Value"

        Dim Sql As String = " AND 無効フラグ = '" & CommonConst.FLAG_ENABLED & "'"

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

        CmInOutKbn.DisplayMember = "Text"
        CmInOutKbn.ValueMember = "Value"

        Sql = " AND "
        Sql += "固定キー ILIKE '" & CommonConst.INOUT_CLASS & "'"
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


        CmInOutKbn.DataSource = tb
        CmInOutKbn.SelectedIndex = 0

    End Sub

    'Dgv内での検索
    Private Sub DgvList_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvList.CellDoubleClick

        Dim selectColumn As String = DgvList.Columns(e.ColumnIndex).Name

        Dim Maker As String = DgvList("メーカー", e.RowIndex).Value
        Dim Item As String = DgvList("品名", e.RowIndex).Value
        Dim Model As String = DgvList("型式", e.RowIndex).Value

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
            openForm = New MakerSearch(_msgHd, _db, Me, e.RowIndex, e.ColumnIndex, Maker, Item, Model, selectColumn)   '処理選択
            openForm.Show(Me)
            Me.Enabled = False
        End If

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

    'プルダウン変更時
    Private Sub CmProcessingClassification_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmProcessingClassification.SelectedIndexChanged

        '処理区分：移動 のとき「入出庫種別」は無効化
        If CmProcessingClassification.SelectedValue = 1 Then
            CmInOutKbn.Enabled = False
            CmWarehouseTo.Enabled = True

        Else
            '処理区分：入庫、出庫 のとき「入出庫種別」は無効化
            CmWarehouseTo.Enabled = False
            CmInOutKbn.Enabled = True
        End If

    End Sub
End Class