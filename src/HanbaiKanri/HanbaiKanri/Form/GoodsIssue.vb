﻿'2020.01.09 ロケ番号→出庫開始サインに名称変更

Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices
Imports System.Globalization
Imports System.IO

Public Class GoodsIssue
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
    Private _db As UtilDBIf
    'Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ
    'Private CompanyCode As String = ""
    Private No As String = ""
    Private Suffix As String = ""
    Private _status As String = ""
    Private _langHd As UtilLangHandler
    Private Input As String = frmC01F10_Login.loginValue.TantoNM
    Private _com As CommonLogic
    Private _vs As String = "2"
    Private _hash As Dictionary(Of String, String)

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
                   ByRef prmRefNo As String,
                   ByRef prmRefSuffix As String,
                   Optional ByRef prmRefStatus As String = "",
                   Optional ByRef prmHash As Dictionary(Of String, String) = Nothing)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        No = prmRefNo
        Suffix = prmRefSuffix
        _status = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        _com = New CommonLogic(_db, _msgHd)
        _hash = prmHash
        Me.Text += _vs
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True

    End Sub

    '画面表示時
    Private Sub GoodsIssue_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblOrderNo.Text = "JobOrderNo"
            LblCustomerNo.Text = "CustomerNumber"
            LblOrderDate.Text = "JobOrderDate"
            LblCustomer.Text = "CustomerName"
            LblOrder.Text = "JobOrder"
            LblHistory.Text = "GoodsDeliveryHistory"
            LblAdd.Text = "ShipmentThisTime"
            LblGoodsIssueDate.Text = "GoodsDeliveryDate"
            LblIDRCurrency.Text = "Currency"
            LblRemarks.Text = "Remarks"
            LblDepositDate.Text = "DepositDate"

            LblCount1.Text = "Record"
            LblCount1.Location = New Point(1272, 82)
            LblCount1.Size = New Size(66, 22)
            LblCount2.Text = "Record"
            LblCount2.Location = New Point(1272, 212)
            LblCount2.Size = New Size(66, 22)
            LblCount3.Text = "Record"
            LblCount3.Location = New Point(1272, 343)
            LblCount3.Size = New Size(66, 22)

            TxtCount1.Location = New Point(1228, 82)
            TxtCount2.Location = New Point(1228, 212)
            TxtCount3.Location = New Point(1228, 343)
            'TxtRemarks.Size = New Size(600, 22)

            'BtnDeliveryNote.Text = "Invoice/Receipt Issue"
            BtnDeliveryNote.Text = "Delivery Note" & Environment.NewLine & "Receipt Issue"
            BtnRegist.Text = "Registration"
            BtnBack.Text = "Back"
        End If

        '参照モード
        If _status = CommonConst.STATUS_VIEW Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "ViewMode"
            Else
                LblMode.Text = "参照モード"
            End If
            LblCount1.Visible = False
            LblCount2.Visible = False
            LblCount3.Visible = False
            LblOrder.Visible = False
            LblAdd.Visible = False
            LblGoodsIssueDate.Visible = False
            LblRemarks.Visible = False
            DtpGoodsIssueDate.Visible = False
            TxtCount1.Visible = False
            TxtCount2.Visible = False
            TxtCount3.Visible = False
            TxtRemarks.Visible = False
            DgvOrder.Visible = False
            DgvAdd.Visible = False
            DgvHistory.ReadOnly = True

            LblHistory.Location = New Point(12, 82)
            DgvHistory.Location = New Point(12, 106)
            DgvHistory.Size = New Point(1326, 400)

            TableLayoutPanel1.Visible = False

            BtnRegist.Visible = False
            BtnDeliveryNote.Visible = True
            BtnDeliveryNote.Location = New Point(1002, 509)

            LblDepositDate.Visible = False
            DtpDepositDate.Visible = False

        Else

            '入力モード

            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "GoodsDelivelyInputMode"
            Else
                LblMode.Text = "出庫入力モード"
            End If

            DtpDepositDate.Value = Date.Now

            'm90_hanyo  SimpleRegistrationの可変キーが1か判定
            Dim Sql As String = "  AND 固定キー = 'SR'"
            Sql += " AND 可変キー = '1'"

            Dim dsHanyo As DataTable = getDsData("m90_hanyo", Sql).Tables(0)

            If dsHanyo.Rows.Count = 0 Then  'データなしの場合は入金予定日を非表示にする
                LblDepositDate.Visible = False
                DtpDepositDate.Visible = False
            Else
                LblDepositDate.Visible = True
                DtpDepositDate.Visible = True
            End If
        End If

        setDgvHd() '見出し行セット
        getlist() '内容表示

        setCellValueChanged()
    End Sub

    '各一覧表示
    Private Sub getlist()
        Dim Sql As String = ""
        Dim reccnt As Integer = 0
        Dim sireKbn As DataSet
        Dim iCnt As Integer = 0

        Try

            If _status = CommonConst.STATUS_VIEW Then

                '出庫情報取得
                '------------------------------------
                Sql = "SELECT t45.*, t44.出庫日, t44.取消区分, t70.入出庫種別, t44.得意先コード,t44.得意先名,t44.客先番号 "
                Sql += " FROM  public.t45_shukodt t45 "
                Sql += " INNER JOIN  t44_shukohd t44"
                Sql += " ON t45.会社コード = t44.会社コード"
                Sql += " AND  t45.出庫番号 = t44.出庫番号"
                Sql += " LEFT JOIN  t70_inout t70"
                Sql += " ON t70.会社コード = t44.会社コード"
                Sql += " AND  t70.入出庫区分 = '2'"
                Sql += " AND  t70.伝票番号 = t44.出庫番号"
                Sql += " AND  t70.行番号 = t45.行番号"
                Sql += " WHERE t45.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql += " AND "
                Sql += "t45.出庫番号='" & No & "'"
                Sql += " AND "
                Sql += "t44.取消区分 = '" & CommonConst.CANCEL_KBN_ENABLED & "'"
                Sql += " AND "
                Sql += " t45.出庫区分 <> '" & CommonConst.SHUKO_KBN_TMP & "' " '仮出庫のものは省く
                Sql += " ORDER BY t45.行番号"
                Dim dsShukodt As DataSet = _db.selectDB(Sql, RS, reccnt)

                Dim j As Integer = DgvHistory.RowCount
                For i As Integer = 0 To dsShukodt.Tables(RS).Rows.Count - 1
                    '汎用マスタから仕入区分名称取得
                    sireKbn = getDsHanyoData(CommonConst.FIXED_KEY_PURCHASING_CLASS, dsShukodt.Tables(RS).Rows(i)("仕入区分").ToString)

                    DgvHistory.Rows.Add()
                    DgvHistory.Rows(j + i).Cells("No").Value = j + i + 1
                    DgvHistory.Rows(j + i).Cells("出庫番号").Value = dsShukodt.Tables(RS).Rows(i)("出庫番号")
                    DgvHistory.Rows(j + i).Cells("行番号").Value = dsShukodt.Tables(RS).Rows(i)("行番号")
                    DgvHistory.Rows(j + i).Cells("受注番号").Value = dsShukodt.Tables(RS).Rows(i)("受注番号")
                    DgvHistory.Rows(j + i).Cells("受注番号枝番").Value = dsShukodt.Tables(RS).Rows(i)("受注番号枝番")
                    DgvHistory.Rows(j + i).Cells("仕入区分").Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                                                                sireKbn.Tables(RS).Rows(0)("文字２"),
                                                                sireKbn.Tables(RS).Rows(0)("文字１"))
                    DgvHistory.Rows(j + i).Cells("メーカー").Value = dsShukodt.Tables(RS).Rows(i)("メーカー")
                    DgvHistory.Rows(j + i).Cells("品名").Value = dsShukodt.Tables(RS).Rows(i)("品名")
                    DgvHistory.Rows(j + i).Cells("型式").Value = dsShukodt.Tables(RS).Rows(i)("型式")
                    DgvHistory.Rows(j + i).Cells("単位").Value = dsShukodt.Tables(RS).Rows(i)("単位")
                    DgvHistory.Rows(j + i).Cells("仕入先").Value = dsShukodt.Tables(RS).Rows(i)("仕入先名")
                    DgvHistory.Rows(j + i).Cells("売単価").Value = dsShukodt.Tables(RS).Rows(i)("売単価")
                    DgvHistory.Rows(j + i).Cells("出庫数量").Value = dsShukodt.Tables(RS).Rows(i)("出庫数量")

                    If dsShukodt.Tables(RS).Rows(i).IsNull(("倉庫コード")) = False Then
                        DgvHistory.Rows(j + i).Cells("倉庫").Value = getWarehouseName(dsShukodt.Tables(RS).Rows(i)("倉庫コード"))
                    End If

                    If dsShukodt.Tables(RS).Rows(i).IsNull("入出庫種別") = False Then
                        DgvHistory.Rows(j + i).Cells("入出庫種別").Value = getInOutName(dsShukodt.Tables(RS).Rows(i)("入出庫種別"))
                    End If

                    DgvHistory.Rows(j + i).Cells("出庫日").Value = dsShukodt.Tables(RS).Rows(i)("出庫日").ToShortDateString
                    DgvHistory.Rows(j + i).Cells("備考").Value = dsShukodt.Tables(RS).Rows(i)("備考")
                    DgvHistory.Rows(j + i).Cells("受注行番号").Value = dsShukodt.Tables(RS).Rows(i)("受注行番号")
                Next

                TxtOrderNo.Text = dsShukodt.Tables(RS).Rows(0)("受注番号").ToString
                TxtSuffixNo.Text = dsShukodt.Tables(RS).Rows(0)("受注番号枝番").ToString
                TxtCustomerPO.Text = dsShukodt.Tables(RS).Rows(0)("客先番号").ToString
                'TxtOrderDate.Text = ""
                TxtCustomerCode.Text = dsShukodt.Tables(RS).Rows(0)("得意先コード").ToString
                TxtCustomerName.Text = dsShukodt.Tables(RS).Rows(0)("得意先名").ToString
                'DtpGoodsIssueDate.Value = dsShukodt.Tables(RS).Rows(0)("出庫日") '出庫日
                'TxtIDRCurrency.Text = ""






            Else

                '倉庫マスタを取得、コンボボックスを作成
                Sql = " AND "
                Sql += " 無効フラグ = '" & CommonConst.FLAG_ENABLED & "'"

                Dim dsWarehouse As DataSet = getDsData("m20_warehouse", Sql)

                'ComboBoxに表示する項目のリストを作成する
                Dim table As New DataTable("Table")
                table.Columns.Add("Display", GetType(String))
                table.Columns.Add("Value", GetType(String))
                For i As Integer = 0 To dsWarehouse.Tables(RS).Rows.Count - 1
                    table.Rows.Add(dsWarehouse.Tables(RS).Rows(i)("名称"), dsWarehouse.Tables(RS).Rows(i)("倉庫コード"))
                Next

                '指定列をコンボボックスに変換
                Dim cmWarehouse As New DataGridViewComboBoxColumn()
                cmWarehouse.DataSource = table
                '実際の値が"Value"列、表示するテキストが"Display"列とする
                cmWarehouse.ValueMember = "Value"
                cmWarehouse.DisplayMember = "Display"
                cmWarehouse.HeaderText = "倉庫"
                cmWarehouse.Name = "倉庫"
                If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                    cmWarehouse.HeaderText = "Warehouse"
                End If

                DgvAdd.Columns.Insert(10, cmWarehouse)
                'DgvAdd.Columns("倉庫").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
                'DgvAdd.Columns("倉庫").ReadOnly = True


                '入出庫種別コンボボックス作成
                Dim cmInOutKbn As New DataGridViewComboBoxColumn()
                cmInOutKbn.DataSource = getInOutKbn("0,1")
                '実際の値が"Value"列、表示するテキストが"Display"列とする
                cmInOutKbn.ValueMember = "Value"
                cmInOutKbn.DisplayMember = "Display"
                cmInOutKbn.HeaderText = "入出庫種別"
                cmInOutKbn.Name = "入出庫種別"
                If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                    cmInOutKbn.HeaderText = "StorageType"
                End If

                DgvAdd.Columns.Insert(15, cmInOutKbn)

                ''引当区分コンボボックス作成
                'Dim cmAllocationKbn As New DataGridViewComboBoxColumn()
                'cmAllocationKbn.DataSource = getAssignKbn()
                ''実際の値が"Value"列、表示するテキストが"Display"列とする
                'cmAllocationKbn.ValueMember = "Value"
                'cmAllocationKbn.DisplayMember = "Display"
                'cmAllocationKbn.HeaderText = "引当区分"
                'cmAllocationKbn.Name = "引当区分"
                'If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                '    cmAllocationKbn.HeaderText = "AssignClassification"
                'End If

                'DgvAdd.Columns.Insert(13, cmAllocationKbn)

                'リードタイムのリストを汎用マスタから取得
                Dim dsHanyo As DataSet = getDsHanyoData(CommonConst.INOUT_CLASS, "0,1")

                For Each yx As KeyValuePair(Of String, String) In _hash

                    No = yx.Key
                    Suffix = yx.Value

                    '受注情報取得
                    '------------------------------------
                    Sql = " AND "
                    Sql += "受注番号 ILIKE '" & No & "'"
                    Sql += " AND "
                    Sql += "受注番号枝番 ILIKE '" & Suffix & "'"
                    Sql += " AND "
                    Sql += "取消区分 = '" & CommonConst.CANCEL_KBN_ENABLED & "'"
                    Sql += " order by 会社コード, 受注番号, 受注番号枝番 "

                    Dim dsCymnhd As DataSet = getDsData("t10_cymnhd", Sql)
                    'If dsCymnhd.Tables(RS).Rows.Count = 0 Then
                    '    '受注が取り消されているものであることをアラートし、以降の処理を中止する
                    '    _msgHd.dspMSG("giOrderError", frmC01F10_Login.loginValue.Language)
                    'End If


                    '出庫情報取得
                    '------------------------------------
                    'Sql = "SELECT t45.*, t44.出庫日, t44.取消区分, t70.入出庫種別, t70.引当区分"
                    Sql = "SELECT t45.*, t44.出庫日, t44.取消区分, t70.入出庫種別"
                    Sql += " FROM  public.t45_shukodt t45 "

                    Sql += " INNER JOIN  t44_shukohd t44"
                    Sql += " ON t45.会社コード = t44.会社コード"
                    Sql += " AND  t45.出庫番号 = t44.出庫番号"

                    Sql += " LEFT JOIN  t70_inout t70"
                    Sql += " ON t70.会社コード = t44.会社コード"
                    Sql += " AND  t70.入出庫区分 = '2'"
                    'Sql += " AND  t70.倉庫コード = t45.倉庫コード"
                    Sql += " AND  t70.伝票番号 = t44.出庫番号"
                    Sql += " AND  t70.行番号 = t45.行番号"
                    'Sql += " AND  t70.出庫開始サイン = concat(t44.出庫番号, t45.行番号)"     '2020.01.09 REP

                    'If dsCymnhd.Tables(RS).Rows.Count > 0 Then
                    Sql += " WHERE t45.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                        Sql += " AND "
                        Sql += "t45.受注番号 ILIKE '" & No & "'"
                        Sql += " AND "
                        Sql += "t45.受注番号枝番 ILIKE '" & Suffix & "'"
                        Sql += " AND "
                        Sql += "t44.取消区分 = '" & CommonConst.CANCEL_KBN_ENABLED & "'"
                        Sql += " AND "
                        Sql += " t45.出庫区分 <> '" & CommonConst.SHUKO_KBN_TMP & "' " '仮出庫のものは省く
                        Sql += " ORDER BY t45.行番号"
                    'Else
                    'Sql += " WHERE t45.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                    'Sql += " AND "
                    'Sql += "t45.出庫番号 ILIKE '" & No & "'"
                    'Sql += " AND "
                    'Sql += "t44.取消区分 = '" & CommonConst.CANCEL_KBN_ENABLED & "'"
                    'Sql += " AND "
                    'Sql += " t45.出庫区分 <> '" & CommonConst.SHUKO_KBN_TMP & "' " '仮出庫のものは省く
                    'Sql += " ORDER BY t45.行番号"

                    'End If

                    Dim dsShukodt As DataSet = _db.selectDB(Sql, RS, reccnt)

                    Sql = "SELECT t11.*, t10.更新日"
                    Sql += " FROM  public.t11_cymndt t11 "
                    Sql += " INNER JOIN  t10_cymnhd t10"
                    Sql += " ON t11.会社コード = t10.会社コード"
                    Sql += " AND  t11.受注番号 = t10.受注番号"
                    Sql += " AND  t11.受注番号枝番 = t10.受注番号枝番"

                    Sql += " WHERE t11.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                    Sql += " AND "
                    Sql += "t11.受注番号 ILIKE '" & No & "'"
                    Sql += " AND "
                    Sql += "t11.受注番号枝番 ILIKE '" & Suffix & "'"
                    Sql += " AND "
                    Sql += "t10.取消区分 = '" & CommonConst.CANCEL_KBN_ENABLED & "'"

                    Sql += " ORDER BY t11.行番号"

                    Dim dsCymndt As DataSet = _db.selectDB(Sql, RS, reccnt)



                    Dim j As Integer = DgvOrder.RowCount
                    For i As Integer = 0 To dsCymndt.Tables(RS).Rows.Count - 1
                        DgvOrder.Rows.Add()
                        DgvOrder.Rows(j + i).Cells("受注番号").Value = dsCymndt.Tables(RS).Rows(i)("受注番号")
                        DgvOrder.Rows(j + i).Cells("受注番号枝番").Value = dsCymndt.Tables(RS).Rows(i)("受注番号枝番")
                        DgvOrder.Rows(j + i).Cells("受注日").Value = dsCymnhd.Tables(RS).Rows(0)("受注日")
                        DgvOrder.Rows(j + i).Cells("客先番号").Value = dsCymnhd.Tables(RS).Rows(0)("客先番号")
                        DgvOrder.Rows(j + i).Cells("明細").Value = dsCymndt.Tables(RS).Rows(i)("行番号")
                        DgvOrder.Rows(j + i).Cells("メーカー").Value = dsCymndt.Tables(RS).Rows(i)("メーカー")
                        DgvOrder.Rows(j + i).Cells("品名").Value = dsCymndt.Tables(RS).Rows(i)("品名")
                        DgvOrder.Rows(j + i).Cells("型式").Value = dsCymndt.Tables(RS).Rows(i)("型式")
                        DgvOrder.Rows(j + i).Cells("受注数量").Value = dsCymndt.Tables(RS).Rows(i)("受注数量")
                        DgvOrder.Rows(j + i).Cells("単位").Value = dsCymndt.Tables(RS).Rows(i)("単位")

                        'DgvOrder.Rows(i).Cells("売上数量").Value = dsCymndt.Tables(RS).Rows(i)("売上数量")
                        DgvOrder.Rows(j + i).Cells("売上数量").Value = dsCymndt.Tables(RS).Rows(i)("出庫数")
                        DgvOrder.Rows(j + i).Cells("受注残数").Value = dsCymndt.Tables(RS).Rows(i)("未出庫数")

                        DgvOrder.Rows(j + i).Cells("通貨").Value = _com.getCurrencyEx(dsCymnhd.Tables(RS).Rows(0)("通貨"))
                        DgvOrder.Rows(j + i).Cells("売単価").Value = dsCymndt.Tables(RS).Rows(i)("見積単価_外貨") '見積単価")

                        DgvOrder.Rows(j + i).Cells("売上金額").Value = dsCymndt.Tables(RS).Rows(i)("売上金額")
                        DgvOrder.Rows(j + i).Cells("未出庫数").Value = dsCymndt.Tables(RS).Rows(i)("未出庫数")
                        DgvOrder.Rows(j + i).Cells("更新日").Value = dsCymndt.Tables(RS).Rows(i)("更新日")
                    Next

                    j = DgvHistory.RowCount
                    For i As Integer = 0 To dsShukodt.Tables(RS).Rows.Count - 1
                        '汎用マスタから仕入区分名称取得
                        sireKbn = getDsHanyoData(CommonConst.FIXED_KEY_PURCHASING_CLASS, dsShukodt.Tables(RS).Rows(i)("仕入区分").ToString)

                        DgvHistory.Rows.Add()
                        DgvHistory.Rows(j + i).Cells("No").Value = j + i + 1
                        DgvHistory.Rows(j + i).Cells("出庫番号").Value = dsShukodt.Tables(RS).Rows(i)("出庫番号")
                        DgvHistory.Rows(j + i).Cells("行番号").Value = dsShukodt.Tables(RS).Rows(i)("行番号")
                        DgvHistory.Rows(j + i).Cells("受注番号").Value = dsShukodt.Tables(RS).Rows(i)("受注番号")
                        DgvHistory.Rows(j + i).Cells("受注番号枝番").Value = dsShukodt.Tables(RS).Rows(i)("受注番号枝番")
                        DgvHistory.Rows(j + i).Cells("仕入区分").Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                                                                    sireKbn.Tables(RS).Rows(0)("文字２"),
                                                                    sireKbn.Tables(RS).Rows(0)("文字１"))
                        DgvHistory.Rows(j + i).Cells("メーカー").Value = dsShukodt.Tables(RS).Rows(i)("メーカー")
                        DgvHistory.Rows(j + i).Cells("品名").Value = dsShukodt.Tables(RS).Rows(i)("品名")
                        DgvHistory.Rows(j + i).Cells("型式").Value = dsShukodt.Tables(RS).Rows(i)("型式")
                        DgvHistory.Rows(j + i).Cells("単位").Value = dsShukodt.Tables(RS).Rows(i)("単位")
                        DgvHistory.Rows(j + i).Cells("仕入先").Value = dsShukodt.Tables(RS).Rows(i)("仕入先名")
                        DgvHistory.Rows(j + i).Cells("売単価").Value = dsShukodt.Tables(RS).Rows(i)("売単価")
                        DgvHistory.Rows(j + i).Cells("出庫数量").Value = dsShukodt.Tables(RS).Rows(i)("出庫数量")

                        If dsShukodt.Tables(RS).Rows(i).IsNull(("倉庫コード")) = False Then
                            DgvHistory.Rows(j + i).Cells("倉庫").Value = getWarehouseName(dsShukodt.Tables(RS).Rows(i)("倉庫コード"))
                        End If

                        If dsShukodt.Tables(RS).Rows(i).IsNull("入出庫種別") = False Then
                            DgvHistory.Rows(j + i).Cells("入出庫種別").Value = getInOutName(dsShukodt.Tables(RS).Rows(i)("入出庫種別"))
                        End If

                        DgvHistory.Rows(j + i).Cells("出庫日").Value = dsShukodt.Tables(RS).Rows(i)("出庫日").ToShortDateString
                        DgvHistory.Rows(j + i).Cells("備考").Value = dsShukodt.Tables(RS).Rows(i)("備考")
                        DgvHistory.Rows(j + i).Cells("受注行番号").Value = dsShukodt.Tables(RS).Rows(i)("受注行番号")

                    Next

                    Sql = " AND 出庫番号 = '" & No & "'"
                    '出庫基本取得
                    Dim dsShukohd As DataSet = getDsData("t44_shukohd", Sql)

                    Dim rowIndex As Long = 0

                    For i As Integer = 0 To dsCymndt.Tables(RS).Rows.Count - 1
                        If dsCymndt.Tables(RS).Rows(i)("未出庫数") <> 0 Then
                            '汎用マスタから仕入区分名称取得
                            sireKbn = getDsHanyoData(CommonConst.FIXED_KEY_PURCHASING_CLASS, dsCymndt.Tables(RS).Rows(i)("仕入区分").ToString)

                            DgvAdd.Rows.Add()

                            rowIndex = DgvAdd.RowCount - 1

                            DgvAdd.Rows(rowIndex).Cells("No").Value = rowIndex + 1
                            DgvAdd.Rows(rowIndex).Cells("受注番号").Value = dsCymndt.Tables(RS).Rows(i)("受注番号")
                            DgvAdd.Rows(rowIndex).Cells("受注番号枝番").Value = dsCymndt.Tables(RS).Rows(i)("受注番号枝番")
                            DgvAdd.Rows(rowIndex).Cells("行番号").Value = dsCymndt.Tables(RS).Rows(i)("行番号")
                            DgvAdd.Rows(rowIndex).Cells("仕入区分").Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                                                                    sireKbn.Tables(RS).Rows(0)("文字２"),
                                                                    sireKbn.Tables(RS).Rows(0)("文字１"))
                            DgvAdd.Rows(rowIndex).Cells("仕入区分値").Value = dsCymndt.Tables(RS).Rows(i)("仕入区分")
                            DgvAdd.Rows(rowIndex).Cells("メーカー").Value = dsCymndt.Tables(RS).Rows(i)("メーカー")
                            DgvAdd.Rows(rowIndex).Cells("品名").Value = dsCymndt.Tables(RS).Rows(i)("品名")
                            DgvAdd.Rows(rowIndex).Cells("型式").Value = dsCymndt.Tables(RS).Rows(i)("型式")
                            DgvAdd.Rows(rowIndex).Cells("仕入先").Value = dsCymndt.Tables(RS).Rows(i)("仕入先名")

                            DgvAdd.Rows(rowIndex).Cells("単位").Value = dsCymndt.Tables(RS).Rows(i)("単位")
                            DgvAdd.Rows(rowIndex).Cells("未出庫数量").Value = dsCymndt.Tables(RS).Rows(i)("未出庫数")
                            DgvAdd.Rows(rowIndex).Cells("出庫数").Value = dsCymndt.Tables(RS).Rows(i)("出庫数")

                            '今回出庫数量に未出庫数をセットする
                            DgvAdd.Rows(rowIndex).Cells("出庫数量").Value = dsCymndt.Tables(RS).Rows(i)("未出庫数")


                            DgvAdd.Rows(rowIndex).Cells("売単価").Value = dsCymndt.Tables(RS).Rows(i)("見積単価")
                            'DgvAdd.Rows(rowIndex).Cells("出庫数量").Value = 0

                            If dsCymnhd.Tables(RS).Rows.Count > 0 Then
                                DgvAdd.Rows(rowIndex).Cells("倉庫").Value = dsWarehouse.Tables(RS).Rows(0)("倉庫コード")
                            Else
                                DgvAdd.Rows(rowIndex).Cells("倉庫").Value = dsShukohd.Tables(RS).Rows(0)("倉庫コード")
                            End If

                            DgvAdd.Rows(rowIndex).Cells("入出庫種別").Value = dsHanyo.Tables(RS).Rows(0)("可変キー")
                            DgvAdd.Rows(rowIndex).Cells("備考").Value = dsCymndt.Tables(RS).Rows(i)("備考")

                            'DgvAdd.Rows(rowIndex).Cells("在庫数量").Value = setZaikoQuantity(rowIndex) 'セットされた内容から現在庫数をセット
                            'DgvAdd.Rows(rowIndex).Cells("在庫数量").Value = getZikoQuantity(rowIndex) 'セットされた内容から現在庫数をセット
                            '仕入区分が「サービス時」
                            'If dsCymndt.Tables(RS).Rows(i)("仕入区分").ToString = CommonConst.Sire_KBN_SERVICE.ToString Then
                            'DgvAdd.Rows(rowIndex).Cells("在庫数量").Style.BackColor = Color.FromArgb(255, 255, 192)
                            'End If
                        End If
                    Next

                    '行番号の振り直し
                    'Dim i1 As Integer = DgvOrder.Rows.Count()
                    'Dim No1 As Integer = 1
                    'For i As Integer = 0 To i1 - 1
                    '    DgvOrder.Rows(i).Cells("明細").Value = No1
                    '    No1 += 1
                    'Next i

                    '各項目件数表示
                    TxtCount1.Text = DgvOrder.Rows.Count()
                    TxtCount2.Text = DgvHistory.Rows.Count()
                    TxtCount3.Text = DgvAdd.Rows.Count()

                    '受注データがある場合
                    If dsCymnhd.Tables(RS).Rows.Count > 0 Then
                        TxtOrderNo.Text = dsCymnhd.Tables(RS).Rows(0)("受注番号")
                        TxtSuffixNo.Text = dsCymnhd.Tables(RS).Rows(0)("受注番号枝番")
                        TxtCustomerPO.Text = dsCymnhd.Tables(RS).Rows(0)("客先番号").ToString
                        TxtOrderDate.Text = dsCymnhd.Tables(RS).Rows(0)("受注日")
                        TxtCustomerCode.Text = dsCymnhd.Tables(RS).Rows(0)("得意先コード")
                        TxtCustomerName.Text = dsCymnhd.Tables(RS).Rows(0)("得意先名")
                        DtpGoodsIssueDate.Value = Date.Now '出庫日
                        '#633 のためコメントアウト
                        'DtpGoodsIssueDate.MinDate = dsCymnhd.Tables(RS).Rows(0)("受注日") '出庫日のMinDateに受注日を設定する
                        '通貨の表示
                        TxtIDRCurrency.Text = _com.getCurrencyEx(dsCymnhd.Tables(RS).Rows(0)("通貨"))
                    Else
                        TxtOrderNo.Text = dsShukohd.Tables(RS).Rows(0)("受注番号").ToString
                        TxtSuffixNo.Text = dsShukohd.Tables(RS).Rows(0)("受注番号枝番").ToString
                        TxtCustomerPO.Text = dsShukohd.Tables(RS).Rows(0)("客先番号").ToString
                        TxtOrderDate.Text = ""
                        TxtCustomerCode.Text = dsShukohd.Tables(RS).Rows(0)("得意先コード").ToString
                        TxtCustomerName.Text = dsShukohd.Tables(RS).Rows(0)("得意先名").ToString
                        DtpGoodsIssueDate.Value = dsShukohd.Tables(RS).Rows(0)("出庫日") '出庫日
                        TxtIDRCurrency.Text = ""
                    End If

                    '出庫入力一覧をループしながら今回出庫の「在庫数」を確認・セット
                    For i As Integer = 0 To DgvAdd.Rows.Count() - 1
                        '仕入区分が「サービス時」
                        'If dsCymndt.Tables(RS).Rows(i)("仕入区分").ToString = CommonConst.Sire_KBN_SERVICE.ToString Then
                        If DgvAdd.Rows(i).Cells("仕入区分値").Value.ToString = CommonConst.Sire_KBN_SERVICE.ToString Then
                            DgvAdd.Rows(i).Cells("在庫数量").Style.BackColor = Color.LightGray
                        Else
                            '仕入区分が「サービス時」以外
                            DgvAdd.Rows(i).Cells("在庫数量").Value = getZikoQuantity(i)
                        End If

                    Next

                Next
            End If


        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            If ex.HResult = -2147467259 Then
                MessageBox.Show("Signal is not so good, Reconnect...", "SPIN", MessageBoxButtons.OK, MessageBoxIcon.Information)
                _db = StartUp.Connect()
            Else
                'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
                'Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
            End If
        End Try

    End Sub

    Private Sub setDgvHd()

        '受注エリア見出しセット
        '
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            DgvOrder.Columns.Add("受注番号", "JobOrderNo")
            DgvOrder.Columns.Add("受注番号枝番", "JobOrderVer")
            DgvOrder.Columns.Add("受注日", "OrderDate")
            DgvOrder.Columns.Add("客先番号", "CustomerNumber")

            DgvOrder.Columns.Add("明細", "LineNo")
            DgvOrder.Columns.Add("メーカー", "Manufacturer")
            DgvOrder.Columns.Add("品名", "ItemName")
            DgvOrder.Columns.Add("型式", "Spec")
            DgvOrder.Columns.Add("受注数量", "JobOrderQuantity" & vbCrLf & "a")
            DgvOrder.Columns.Add("単位", "Unit")

            DgvOrder.Columns.Add("売上数量", "IssuedQuantity" & vbCrLf & "b")
            DgvOrder.Columns.Add("受注残数", "UnissuedQuantity" & vbCrLf & "c=a-b")
            DgvOrder.Columns.Add("通貨", "Currency")
            DgvOrder.Columns.Add("売単価", "OrderUnitPrice")

            DgvOrder.Columns.Add("売上金額", "SalesAmount")
            DgvOrder.Columns.Add("未出庫数", "NoShippedQntity")

            DgvOrder.Columns.Add("更新日", "UpdateDate")
        Else
            DgvOrder.Columns.Add("受注番号", "受注番号")
            DgvOrder.Columns.Add("受注番号枝番", "受注番号枝番")
            DgvOrder.Columns.Add("受注日", "受注日")
            DgvOrder.Columns.Add("客先番号", "客先番号")
            DgvOrder.Columns.Add("通貨", "通貨")

            DgvOrder.Columns.Add("明細", "行No")
            DgvOrder.Columns.Add("メーカー", "メーカー")
            DgvOrder.Columns.Add("品名", "品名")
            DgvOrder.Columns.Add("型式", "型式")
            DgvOrder.Columns.Add("受注数量", "受注数量" & vbCrLf & "a")
            DgvOrder.Columns.Add("単位", "単位")

            DgvOrder.Columns.Add("売上数量", "出庫済数量" & vbCrLf & "b")
            DgvOrder.Columns.Add("受注残数", "未出庫数量" & vbCrLf & "c=a-b")
            DgvOrder.Columns.Add("売単価", "受注単価")

            DgvOrder.Columns.Add("売上金額", "売上金額")
            DgvOrder.Columns.Add("未出庫数", "未出庫数")

            DgvOrder.Columns.Add("更新日", "更新日")
        End If

        '中央寄せ
        DgvOrder.Columns("明細").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvOrder.Columns("メーカー").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvOrder.Columns("品名").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvOrder.Columns("型式").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvOrder.Columns("受注数量").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvOrder.Columns("単位").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvOrder.Columns("売上数量").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvOrder.Columns("受注残数").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvOrder.Columns("売単価").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        '右寄せ
        DgvOrder.Columns("受注数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvOrder.Columns("売上数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvOrder.Columns("売単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvOrder.Columns("売上金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvOrder.Columns("受注残数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvOrder.Columns("未出庫数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        DgvOrder.Columns("売上金額").Visible = False
        DgvOrder.Columns("未出庫数").Visible = False
        DgvOrder.Columns("更新日").Visible = False


        '数字形式
        DgvOrder.Columns("受注数量").DefaultCellStyle.Format = "N2"
        DgvOrder.Columns("売上数量").DefaultCellStyle.Format = "N2"
        DgvOrder.Columns("売単価").DefaultCellStyle.Format = "N2"
        DgvOrder.Columns("売上金額").DefaultCellStyle.Format = "N2"
        DgvOrder.Columns("受注残数").DefaultCellStyle.Format = "N2"
        DgvOrder.Columns("未出庫数").DefaultCellStyle.Format = "N2"
        DgvOrder.Columns("受注日").DefaultCellStyle.Format = "d"

        '出庫済みエリア見出しセット
        '
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            DgvHistory.Columns.Add("No", "No")
            DgvHistory.Columns.Add("出庫番号", "GoodsDeliveryNumber")
            DgvHistory.Columns.Add("行番号", "LineNo")
            DgvHistory.Columns.Add("受注番号", "JobOrderNo")
            DgvHistory.Columns.Add("受注番号枝番", "JobOrderVer")
            DgvHistory.Columns.Add("受注行番号", "LineNo")
            DgvHistory.Columns.Add("仕入区分", "PurchasingClassification")
            DgvHistory.Columns.Add("メーカー", "Manufacturer")
            DgvHistory.Columns.Add("品名", "ItemName")
            DgvHistory.Columns.Add("型式", "Spec")
            DgvHistory.Columns.Add("単位", "Unit")
            DgvHistory.Columns.Add("仕入先", "SupplierName")
            DgvHistory.Columns.Add("売単価", "SellingPrice")
            DgvHistory.Columns.Add("出庫数量", "GoodsDeliveryQuantity")
            DgvHistory.Columns.Add("倉庫", "Warehouse")
            DgvHistory.Columns.Add("入出庫種別", "StorageType")
            'DgvHistory.Columns.Add("引当区分", "AssignClassification")
            DgvHistory.Columns.Add("出庫日", "GoodsDeliveryDate")
            DgvHistory.Columns.Add("備考", "Remarks")
        Else
            DgvHistory.Columns.Add("No", "No")
            DgvHistory.Columns.Add("出庫番号", "出庫番号")
            DgvHistory.Columns.Add("行番号", "行No")
            DgvHistory.Columns.Add("受注番号", "受注番号")
            DgvHistory.Columns.Add("受注番号枝番", "受注番号枝番")
            DgvHistory.Columns.Add("受注行番号", "受注行番号")
            DgvHistory.Columns.Add("仕入区分", "仕入区分")
            DgvHistory.Columns.Add("メーカー", "メーカー")
            DgvHistory.Columns.Add("品名", "品名")
            DgvHistory.Columns.Add("型式", "型式")
            DgvHistory.Columns.Add("単位", "単位")
            DgvHistory.Columns.Add("仕入先", "仕入先")
            DgvHistory.Columns.Add("売単価", "売単価")
            DgvHistory.Columns.Add("出庫数量", "出庫済数量")
            DgvHistory.Columns.Add("倉庫", "倉庫")
            DgvHistory.Columns.Add("入出庫種別", "入出庫種別")
            'DgvHistory.Columns.Add("引当区分", "引当区分")
            DgvHistory.Columns.Add("出庫日", "出庫日")
            DgvHistory.Columns.Add("備考", "備考")
        End If

        DgvHistory.Columns("売単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvHistory.Columns("出庫数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


        '数字形式
        DgvHistory.Columns("売単価").DefaultCellStyle.Format = "N2"
        DgvHistory.Columns("出庫数量").DefaultCellStyle.Format = "N2"

        DgvHistory.Columns("仕入先").Visible = False
        DgvHistory.Columns("売単価").Visible = False
        DgvHistory.Columns("No").Visible = False


        '今回出庫エリア見出しセット
        '
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            DgvAdd.Columns.Add("No", "No")
            DgvAdd.Columns.Add("受注番号", "JobOrderNo")
            DgvAdd.Columns.Add("受注番号枝番", "JobOrderVer")
            DgvAdd.Columns.Add("行番号", "LineNo")
            DgvAdd.Columns.Add("仕入区分", "PurchasingClassification")
            DgvAdd.Columns.Add("仕入区分値", "仕入区分値")
            DgvAdd.Columns.Add("メーカー", "Manufacturer")
            DgvAdd.Columns.Add("品名", "ItemName")
            DgvAdd.Columns.Add("型式", "Spec")
            DgvAdd.Columns.Add("仕入先", "SupplierName")

            DgvAdd.Columns.Add("在庫数量", "StockQuantity")
            DgvAdd.Columns.Add("単位", "Unit")
            DgvAdd.Columns.Add("未出庫数量", "UnissuedQuantity")
            DgvAdd.Columns.Add("出庫数量", "GoodsDeliveryQuantity")
            DgvAdd.Columns.Add("出庫数", "IssuedQuantity")

            DgvAdd.Columns.Add("備考", "Remarks")

            DgvAdd.Columns.Add("売単価", "SellingPrice")

        Else
            DgvAdd.Columns.Add("No", "No")
            DgvAdd.Columns.Add("受注番号", "受注番号")
            DgvAdd.Columns.Add("受注番号枝番", "受注番号枝番")
            DgvAdd.Columns.Add("行番号", "行No")
            DgvAdd.Columns.Add("仕入区分", "仕入区分")
            DgvAdd.Columns.Add("仕入区分値", "仕入区分値")
            DgvAdd.Columns.Add("メーカー", "メーカー")
            DgvAdd.Columns.Add("品名", "品名")
            DgvAdd.Columns.Add("型式", "型式")
            DgvAdd.Columns.Add("仕入先", "仕入先")

            DgvAdd.Columns.Add("在庫数量", "在庫数量")
            DgvAdd.Columns.Add("単位", "単位")
            DgvAdd.Columns.Add("未出庫数量", "未出庫数量")
            DgvAdd.Columns.Add("出庫数量", "今回出庫数量")
            DgvAdd.Columns.Add("出庫数", "出庫数")

            DgvAdd.Columns.Add("備考", "備考")

            DgvAdd.Columns.Add("売単価", "売単価")

        End If

        DgvAdd.Columns("在庫数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvAdd.Columns("未出庫数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvAdd.Columns("出庫数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvAdd.Columns("売単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        '数字形式
        DgvAdd.Columns("在庫数量").DefaultCellStyle.Format = "N2"
        DgvAdd.Columns("未出庫数量").DefaultCellStyle.Format = "N2"
        DgvAdd.Columns("出庫数量").DefaultCellStyle.Format = "N2"
        DgvAdd.Columns("売単価").DefaultCellStyle.Format = "N2"


        DgvAdd.Columns("No").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
        DgvAdd.Columns("No").ReadOnly = True
        DgvAdd.Columns("行番号").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
        DgvAdd.Columns("行番号").ReadOnly = True
        DgvAdd.Columns("仕入区分").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
        DgvAdd.Columns("仕入区分").ReadOnly = True
        DgvAdd.Columns("仕入区分値").Visible = False
        DgvAdd.Columns("メーカー").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
        DgvAdd.Columns("メーカー").ReadOnly = True
        DgvAdd.Columns("品名").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
        DgvAdd.Columns("品名").ReadOnly = True
        DgvAdd.Columns("型式").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
        DgvAdd.Columns("型式").ReadOnly = True
        DgvAdd.Columns("仕入先").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
        DgvAdd.Columns("仕入先").ReadOnly = True

        DgvAdd.Columns("在庫数量").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
        DgvAdd.Columns("在庫数量").ReadOnly = True
        DgvAdd.Columns("単位").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
        DgvAdd.Columns("単位").ReadOnly = True
        DgvAdd.Columns("未出庫数量").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
        DgvAdd.Columns("未出庫数量").ReadOnly = True
        DgvAdd.Columns("受注番号").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
        DgvAdd.Columns("受注番号枝番").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
        DgvAdd.Columns("受注番号").ReadOnly = True
        DgvAdd.Columns("受注番号枝番").ReadOnly = True

        DgvAdd.Columns("売単価").ReadOnly = True
        DgvAdd.Columns("売単価").Visible = False
        DgvAdd.Columns("出庫数").Visible = False
    End Sub

    'セルの値が変更されたら
    Private Sub CellValueChanged(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs)

        Dim PurchaseTotal As Integer = 0

        Dim currentColumn As String = DgvAdd.Columns(e.ColumnIndex).Name

        'ヘッダー以外だったら
        If e.RowIndex > -1 Then

            '各項目の属性チェック
            If Not IsNumeric(DgvAdd.Rows(e.RowIndex).Cells("出庫数量").Value) And (DgvAdd.Rows(e.RowIndex).Cells("出庫数量").Value IsNot Nothing) Then
                _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
                DgvAdd.Rows(e.RowIndex).Cells("出庫数量").Value = 0
                Exit Sub
            End If

            DgvAdd.Rows(e.RowIndex).Cells("在庫数量").Value = getZikoQuantity(e.RowIndex)

        End If

    End Sub

    '戻るボタン押下時
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        'Dim openForm As Form = Nothing
        'openForm = New frmC01F30_Menu(_msgHd, _langHd, _db)
        'openForm.Show()
        Me.Close()
    End Sub

    '登録ボタン押下時
    Private Sub BtnRegist_Click(sender As Object, e As EventArgs) Handles BtnRegist.Click

        Dim dtToday As DateTime = DateTime.Now

        'Dim shukkoTime As TimeSpan = dtToday.TimeOfDay
        Dim shukkoDate As String = UtilClass.formatDatetime(DtpGoodsIssueDate.Value) '.Text & " " & shukkoTime.ToString)

        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""


#Region "SQL"

        '受注基本
        Sql = " And "
        'Sql += "受注番号 ILIKE '" & No & "'"
        Sql += " AND "
        'Sql += "受注番号枝番 ILIKE '" & Suffix & "'"
        Sql += " AND "
        Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED '取消区分=0

        Dim dsCymnhd As DataSet '= getDsData("t10_cymnhd", Sql)

        '受注明細
        Sql = "SELECT t11.行番号, t11.出庫数, t11.未出庫数 FROM "
        Sql += " t11_cymndt t11"

        Sql += " INNER JOIN "
        Sql += " t10_cymnhd t10"
        Sql += " ON "

        Sql += " t11.会社コード = t10.会社コード"
        Sql += " AND "
        Sql += " t11.受注番号 = t10.受注番号"
        Sql += " AND "
        Sql += " t11.受注番号枝番 = t10.受注番号枝番"

        Sql += " where t10.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND "
        'Sql += "t10.受注番号 ILIKE '" & No & "'"
        Sql += " AND "
        'Sql += "t10.受注番号枝番 ILIKE '" & Suffix & "'"
        Sql += " AND "
        Sql += "t10.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED '取消区分=0

        '受注明細の「出庫数」更新に使用
        'Dim dsCymndt As DataSet = _db.selectDB(Sql, RS, reccnt)

#End Region


#Region "Check"

        '対象データがなかったらメッセージを表示
        If DgvAdd.RowCount = 0 Then
            '操作できないメッセージを表示
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)

            Return
        End If

        Dim chkGIAmount As Decimal = 0 '出庫データがあるか合算する用
        Dim chkShukkoFlg As Boolean = True '在庫チェック用フラグ
        '最初に今回出庫に入力がなかったらエラーで返す
        For i As Integer = 0 To DgvAdd.RowCount - 1
            chkGIAmount += DgvAdd.Rows(i).Cells("出庫数量").Value
            'サービス以外だったら数量チェック
            If DgvAdd.Rows(i).Cells("仕入区分値").Value.ToString <> CommonConst.Sire_KBN_SERVICE.ToString Then
                If DgvAdd.Rows(i).Cells("出庫数量").Value > DgvAdd.Rows(i).Cells("在庫数量").Value Then
                    chkShukkoFlg = False
                End If
            End If
        Next

        If chkGIAmount <= 0 Then
            '対象データがないメッセージを表示
            _msgHd.dspMSG("chkGoodsIssueAddError", frmC01F10_Login.loginValue.Language)

            Return
        End If

        If chkShukkoFlg = False Then
            '出庫数量が在庫数量を超えているメッセージを表示
            _msgHd.dspMSG("chkShukoOverError", frmC01F10_Login.loginValue.Language)

            Exit Sub
        End If

        'For i As Integer = 0 To DgvAdd.Rows.Count() - 1
        'For x As Integer = 0 To DgvOrder.Rows.Count() - 1
        '行番号が一致したら
        'If DgvOrder.Rows(x).Cells("明細").Value = DgvAdd.Rows(i).Cells("行番号").Value Then
        '出庫数が未出庫数を超えたら
        'If DgvOrder.Rows(x).Cells("未出庫数").Value < DgvAdd.Rows(i).Cells("出庫数量").Value Then

        '操作できないアラートを出す
        '_msgHd.dspMSG("chkGIBalanceError", frmC01F10_Login.loginValue.Language)
        'Return
        'End If
        'End If
        'Next
        'Next

        'If DgvOrder.Rows(0).Cells("更新日").Value <> dsCymnhd.Tables(RS).Rows(0)("更新日") Then
        '画面を開いたときの日時とデータの日時が異なっていた場合
        'データが誰かに変更された旨を伝える
        '_msgHd.dspMSG("chkData", frmC01F10_Login.loginValue.Language)

        'Exit Sub
        'End If

        '入出庫データ(t70_input)を作成する前に在庫があるか最終チェック
        For i As Integer = 0 To DgvAdd.Rows.Count() - 1
            'サービス以外だったら数量チェック
            If DgvAdd.Rows(i).Cells("仕入区分値").Value.ToString <> CommonConst.Sire_KBN_SERVICE.ToString Then
                If DgvAdd.Rows(i).Cells("出庫数量").Value > getZikoQuantity(i) Then
                    '出庫数量が在庫数量を超えているメッセージを表示
                    _msgHd.dspMSG("chkShukoOverError", frmC01F10_Login.loginValue.Language)
                    Exit Sub
                End If
            End If
        Next

#End Region


        Try
            _db.beginTran()

            '出庫データ登録前に、「在庫引当」の商品があるかどうかチェック
            'あれば「仮出庫」データを取消更新する
            For i As Integer = 0 To DgvAdd.Rows.Count() - 1

                'No = DgvAdd.Rows(i).Cells("受注番号").Value
                'Suffix = DgvAdd.Rows(i).Cells("受注番号枝番").Value

                '仕入区分が2（在庫引当）の場合、作成済みの仮出庫データを「取消区分=0, 取消日=Datetime.Date」でUPDATEする
                If DgvAdd.Rows(i).Cells("仕入区分値").Value = CommonConst.Sire_KBN_Zaiko Then

#Region "t44_shukohd"

                    Sql = " SELECT t44.出庫番号,t45.行番号 "
                    Sql += " FROM "
                    Sql += " t44_shukohd t44 "
                    Sql += " INNER JOIN "
                    Sql += " t45_shukodt t45 "
                    Sql += " ON t44.会社コード = t45.会社コード "
                    Sql += " AND t44.出庫番号 = t45.出庫番号 "
                    Sql += " WHERE "
                    Sql += " t44.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                    Sql += " AND t44.受注番号 = '" & DgvAdd.Rows(i).Cells("受注番号").Value.ToString & "'"
                    'Sql += " AND t44.受注番号枝番 = '" & Suffix & "'"
                    Sql += " AND t45.出庫区分 = '" & CommonConst.SHUKO_KBN_TMP & "'" '仮出庫のものを取得
                    Sql += " AND t44.取消区分 = '" & CommonConst.CANCEL_KBN_ENABLED & "'" '見取消のもの

                    Dim shukkoTmpData As DataSet = _db.selectDB(Sql, RS, reccnt)

#End Region

                    '該当データがあったら
                    'If shukkoTmpData.Tables(RS).Rows.Count > 0 Then
                    For x As Integer = 0 To shukkoTmpData.Tables(RS).Rows.Count - 1


#Region "t44_shukohd"

                        Sql = "UPDATE "
                        Sql += " t44_shukohd "
                        Sql += " SET "
                        Sql += " 取消区分 = '" & CommonConst.CANCEL_KBN_DISABLED.ToString & "'"
                        Sql += " ,取消日 = '" & UtilClass.formatDatetime(dtToday) & "'"
                        Sql += " ,更新日 = '" & UtilClass.formatDatetime(dtToday) & "'"
                        Sql += " ,更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
                        Sql += " WHERE "
                        Sql += " 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                        Sql += " AND 出庫番号 = '" & shukkoTmpData.Tables(RS).Rows(x)("出庫番号") & "'"

                        _db.executeDB(Sql)

#End Region


#Region "t70_inout"

                        'inoutを取消す
                        Sql = "UPDATE "
                        Sql += "Public."
                        Sql += "t70_inout "
                        Sql += "SET "

                        Sql += "取消日"
                        Sql += " = '"
                        Sql += UtilClass.formatDatetime(dtToday)
                        Sql += "', "
                        Sql += "取消区分 = '" & CommonConst.CANCEL_KBN_DISABLED.ToString & "'"
                        Sql += ", 更新日 = '" & UtilClass.formatDatetime(dtToday) & "'"
                        Sql += " ,更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"

                        Sql += "WHERE"
                        Sql += " 会社コード"
                        Sql += "='"
                        Sql += frmC01F10_Login.loginValue.BumonCD
                        Sql += "'"
                        Sql += " AND 伝票番号 ='" & shukkoTmpData.Tables(RS).Rows(x)("出庫番号") & "' "
                        Sql += " AND 行番号 ='" & shukkoTmpData.Tables(RS).Rows(x)("行番号") & "' "
                        Sql += " AND 取消区分='" & CommonConst.CANCEL_KBN_ENABLED.ToString & "'"

                        _db.executeDB(Sql)

#End Region

                    Next

                    'End If

                End If
            Next


            '採番データを取得・更新
            '出庫登録データの伝票番号は基本的に LS で統一される（1商品で複数の在庫マスタをまたぐ場合を除く）
            Dim MAIN_LS As String = _com.getSaiban("70", dtToday)

            '出庫登録一覧をループし、出庫登録対象データがあれば登録処理を実行
            For i As Integer = 0 To DgvAdd.Rows.Count() - 1

                '出庫数量があったら
                If DgvAdd.Rows(i).Cells("出庫数量").Value > 0 Then

                    '該当する在庫データを取得・ループ
                    '対象の在庫がなくなるまでデータを作成する
                    Dim dsCurrentList As DataSet = Nothing
                    Dim totalShukkoVal As Decimal = Decimal.Parse(DgvAdd.Rows(i).Cells("出庫数量").Value)
                    Dim currentVal As Decimal = 0
                    Dim currentLS As String = ""

                    If DgvAdd.Rows(i).Cells("仕入区分値").Value.ToString = CommonConst.Sire_KBN_Sire.ToString Then
                        '仕入区分 = 受発注
                        '---------------------------------------
                        dsCurrentList = getNukoList(i)
                    ElseIf DgvAdd.Rows(i).Cells("仕入区分値").Value.ToString = CommonConst.Sire_KBN_Zaiko.ToString Then
                        '仕入区分 = 在庫
                        '---------------------------------------
                        dsCurrentList = getZaikoList(i)
                    ElseIf DgvAdd.Rows(i).Cells("仕入区分値").Value.ToString = CommonConst.Sire_KBN_DELIVERY.ToString Then
                        dsCurrentList = getNukoList(i)
                    ElseIf DgvAdd.Rows(i).Cells("仕入区分値").Value.ToString = CommonConst.Sire_KBN_OS.ToString Then
                        dsCurrentList = getNukoList(i)
                    Else
                        '仕入区分 = サービス（在庫には関わらない）
                        '---------------------------------------

#Region "t45_shukodt"

                        Sql = "INSERT INTO "
                        Sql += "Public."
                        Sql += "t45_shukodt("
                        Sql += "会社コード, 出庫番号, 受注番号, 受注番号枝番, 行番号, 仕入区分"
                        Sql += ", メーカー, 品名, 型式, 仕入先名, 売単価, 出庫数量, 単位, 備考"
                        Sql += ", 更新者, 更新日, 出庫区分, 倉庫コード,受注行番号"
                        Sql += " )VALUES('"
                        Sql += frmC01F10_Login.loginValue.BumonCD '会社コード
                        Sql += "', '"
                        Sql += MAIN_LS '出庫番号
                        Sql += "', '"
                        Sql += DgvAdd.Rows(i).Cells("受注番号").Value.ToString '受注番号
                        Sql += "', '"
                        Sql += DgvAdd.Rows(i).Cells("受注番号枝番").Value.ToString '受注番号枝番
                        Sql += "', '"
                        Sql += DgvAdd.Rows(i).Cells("No").Value.ToString '行番号
                        Sql += "', '"
                        Sql += DgvAdd.Rows(i).Cells("仕入区分値").Value.ToString '仕入区分
                        Sql += "', '"
                        Sql += DgvAdd.Rows(i).Cells("メーカー").Value.ToString 'メーカー
                        Sql += "', '"
                        Sql += DgvAdd.Rows(i).Cells("品名").Value.ToString '品名
                        Sql += "', '"
                        Sql += DgvAdd.Rows(i).Cells("型式").Value.ToString '型式
                        Sql += "', '"
                        Sql += DgvAdd.Rows(i).Cells("仕入先").Value.ToString '仕入先名
                        Sql += "', '"
                        Sql += UtilClass.formatNumber(DgvAdd.Rows(i).Cells("売単価").Value.ToString) '売単価
                        Sql += "', '"
                        Sql += DgvAdd.Rows(i).Cells("出庫数量").Value.ToString '出庫数量
                        Sql += "', '"
                        Sql += DgvAdd.Rows(i).Cells("単位").Value.ToString '単位
                        Sql += "', '"
                        Sql += DgvAdd.Rows(i).Cells("備考").Value.ToString '備考
                        Sql += "', '"
                        Sql += Input '更新者
                        Sql += "', '"
                        Sql += UtilClass.formatDatetime(dtToday) '更新日
                        Sql += "', '"
                        Sql += CommonConst.SHUKO_KBN_NORMAL '出庫区分（通常出庫時は 1 ）
                        Sql += "', '"
                        Sql += DgvAdd.Rows(i).Cells("倉庫").Value.ToString '倉庫コード
                        Sql += "', '"
                        Sql += DgvAdd.Rows(i).Cells("行番号").Value.ToString()
                        Sql += "')"

                        _db.executeDB(Sql)

#End Region


                        '受注明細更新　受注と出庫の行番号は一致する
                        '---------------------------------------
                        'If DgvAdd.Rows(i).Cells("行番号").Value = dsCymndt.Tables(RS).Rows(i)("行番号") Then

                        Dim calShukko As Decimal = DgvAdd.Rows(i).Cells("出庫数").Value + DgvAdd.Rows(i).Cells("出庫数量").Value
                        Dim calUnShukko As Decimal = DgvAdd.Rows(i).Cells("未出庫数量").Value - DgvAdd.Rows(i).Cells("出庫数量").Value

#Region "t11_cymndt"

                        Sql = "update t11_cymndt set "
                        Sql += "出庫数 = '" & calShukko & "'"
                        Sql += ",未出庫数 = '" & calUnShukko & "'"
                        Sql += ",更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
                        Sql += " where 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                        Sql += " AND "
                        Sql += "受注番号='" & DgvAdd.Rows(i).Cells("受注番号").Value.ToString & "'"
                        Sql += " AND "
                        Sql += "受注番号枝番='" & DgvAdd.Rows(i).Cells("受注番号枝番").Value.ToString & "'"
                        Sql += " AND "
                        Sql += "行番号 = '" & DgvAdd.Rows(i).Cells("行番号").Value.ToString & "'"

                        _db.executeDB(Sql)

#End Region


#Region "t10_cymnhd"

                        Sql = "update t10_cymnhd set "
                        Sql += "更新日 = '" & UtilClass.formatDatetime(dtToday) & "'"
                        Sql += ",更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
                        Sql += " where 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                        Sql += " AND "
                        Sql += "受注番号='" & DgvAdd.Rows(i).Cells("受注番号").Value.ToString & "'"
                        Sql += " AND "
                        Sql += "受注番号枝番='" & DgvAdd.Rows(i).Cells("受注番号枝番").Value.ToString & "'"
                        Sql += " AND "
                        Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED '取消区分=0

                        _db.executeDB(Sql)

#End Region

                        'End If

                    End If

                    If dsCurrentList IsNot Nothing Then
                        'データがあれば
                        If dsCurrentList.Tables(RS).Rows.Count > 0 Then
                            For x As Integer = 0 To dsCurrentList.Tables(RS).Rows.Count - 1

                                If totalShukkoVal = 0 Then
                                    Exit For
                                End If

                                currentVal = Decimal.Parse(dsCurrentList.Tables(RS).Rows(x)("現在庫数"))

                                '現在庫数より出庫数量の方が大きかった場合、現在庫数をそのまま出庫データとして作成
                                If currentVal < totalShukkoVal Then
                                    totalShukkoVal -= currentVal 'currentValをそのまま登録し、全体数からcurrentValを減算する
                                Else
                                    currentVal = totalShukkoVal '登録するのは残数分のみ
                                    totalShukkoVal -= currentVal
                                End If

                                '作成データが複数以上の場合、出庫番号を新規取得
                                currentLS = IIf(x = 0, MAIN_LS, _com.getSaiban("70", dtToday))

                                't45：出庫明細 新規作成
                                '---------------------------------------
#Region "t45_shukodt"

                                Sql = "INSERT INTO "
                                Sql += "Public."
                                Sql += "t45_shukodt("
                                Sql += "会社コード, 出庫番号, 受注番号, 受注番号枝番, 行番号, 仕入区分"
                                Sql += ", メーカー, 品名, 型式, 仕入先名, 売単価, 出庫数量, 単位, 備考"
                                Sql += ", 更新者, 更新日, 出庫区分, 倉庫コード,受注行番号"
                                Sql += " )VALUES('"
                                Sql += frmC01F10_Login.loginValue.BumonCD '会社コード
                                Sql += "', '"
                                Sql += currentLS '出庫番号
                                Sql += "', '"
                                Sql += DgvAdd.Rows(i).Cells("受注番号").Value.ToString '受注番号
                                Sql += "', '"
                                Sql += DgvAdd.Rows(i).Cells("受注番号枝番").Value.ToString '受注番号枝番
                                Sql += "', '"
                                Sql += DgvAdd.Rows(i).Cells("No").Value.ToString '行番号
                                Sql += "', '"
                                Sql += DgvAdd.Rows(i).Cells("仕入区分値").Value.ToString '仕入区分
                                Sql += "', '"
                                Sql += UtilClass.escapeSql(DgvAdd.Rows(i).Cells("メーカー").Value.ToString) 'メーカー
                                Sql += "', '"
                                Sql += UtilClass.escapeSql(DgvAdd.Rows(i).Cells("品名").Value.ToString) '品名
                                Sql += "', '"
                                Sql += UtilClass.escapeSql(DgvAdd.Rows(i).Cells("型式").Value.ToString) '型式
                                Sql += "', '"
                                Sql += DgvAdd.Rows(i).Cells("仕入先").Value.ToString '仕入先名
                                Sql += "', '"
                                Sql += UtilClass.formatNumber(DgvAdd.Rows(i).Cells("売単価").Value.ToString) '売単価
                                Sql += "', '"
                                Sql += currentVal.ToString '出庫数量
                                Sql += "', '"
                                Sql += DgvAdd.Rows(i).Cells("単位").Value.ToString '単位
                                Sql += "', '"
                                Sql += DgvAdd.Rows(i).Cells("備考").Value.ToString '備考
                                Sql += "', '"
                                Sql += Input '更新者
                                Sql += "', '"
                                Sql += UtilClass.formatDatetime(dtToday) '更新日
                                Sql += "', '"
                                Sql += CommonConst.SHUKO_KBN_NORMAL '出庫区分（通常出庫時は 1 ）
                                Sql += "', '"
                                Sql += DgvAdd.Rows(i).Cells("倉庫").Value.ToString '倉庫コード
                                Sql += "', '"
                                Sql += DgvAdd.Rows(i).Cells("行番号").Value.ToString
                                Sql += "')"

                                _db.executeDB(Sql)
#End Region


                                '受注明細の「未出庫数」「出庫数」を更新
                                '受注明細更新　受注と出庫の行番号は一致する
                                '---------------------------------------
                                'If DgvAdd.Rows(i).Cells("行番号").Value = dsCymndt.Tables(RS).Rows(i)("行番号") Then

                                Dim calShukko As Decimal = DgvAdd.Rows(i).Cells("出庫数").Value + DgvAdd.Rows(i).Cells("出庫数量").Value
                                Dim calUnShukko As Decimal = DgvAdd.Rows(i).Cells("未出庫数量").Value - DgvAdd.Rows(i).Cells("出庫数量").Value

#Region "t11_cymndt"

                                Sql = "update t11_cymndt set "
                                Sql += "出庫数 = '" & calShukko & "'"
                                Sql += ",未出庫数 = '" & calUnShukko & "'"
                                Sql += ",更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
                                Sql += " where 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                                Sql += " AND "
                                Sql += "受注番号 ILIKE '" & DgvAdd.Rows(i).Cells("受注番号").Value.ToString & "'"
                                Sql += " AND "
                                Sql += "受注番号枝番 ILIKE '" & DgvAdd.Rows(i).Cells("受注番号枝番").Value.ToString & "'"
                                Sql += " AND "
                                Sql += "行番号 = '" & DgvAdd.Rows(i).Cells("行番号").Value.ToString & "'"

                                _db.executeDB(Sql)

#End Region


#Region "t10_cymnhd"

                                Sql = "update t10_cymnhd set "
                                Sql += "更新日 = '" & UtilClass.formatDatetime(dtToday) & "'"
                                Sql += ",更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
                                Sql += " where 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                                Sql += " AND "
                                Sql += "受注番号 ILIKE '" & DgvAdd.Rows(i).Cells("受注番号").Value.ToString & "'"
                                Sql += " AND "
                                Sql += "受注番号枝番 ILIKE '" & DgvAdd.Rows(i).Cells("受注番号枝番").Value.ToString & "'"
                                Sql += " AND "
                                Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED '取消区分=0

                                _db.executeDB(Sql)
#End Region


                                'End If


#Region "t70_inout"

                                't70_inout にデータ登録
                                Sql = "INSERT INTO "
                                Sql += "Public."
                                Sql += "t70_inout("
                                Sql += "会社コード, 入出庫区分, 倉庫コード, 伝票番号, 行番号, 入出庫種別"
                                Sql += ", メーカー, 品名, 型式, 数量, 単位, 備考, 入出庫日"
                                'Sql += ", 引当区分, メーカー, 品名, 型式, 数量, 単位, 備考, 入出庫日"
                                ''Sql += ", 取消区分, 更新者, 更新日, ロケ番号, 仕入区分"                     '2020.01.09 DEL
                                Sql += ", 取消区分, 更新者, 更新日, 出庫開始サイン, 仕入区分"                 '2020.01.09 ADD
                                Sql += " )VALUES('"
                                Sql += frmC01F10_Login.loginValue.BumonCD '会社コード
                                Sql += "', '"
                                Sql += "2" '入出庫区分 1.入庫, 2.出庫
                                Sql += "', '"
                                Sql += DgvAdd.Rows(i).Cells("倉庫").Value.ToString '倉庫コード
                                Sql += "', '"
                                Sql += currentLS '伝票番号
                                'Sql += dsCurrentList.Tables(RS).Rows(x)("伝票番号").ToString '伝票番号
                                Sql += "', '"
                                Sql += DgvAdd.Rows(i).Cells("No").Value.ToString '行番号
                                'Sql += dsCurrentList.Tables(RS).Rows(x)("行番号").ToString '行番号
                                Sql += "', '"
                                Sql += DgvAdd.Rows(i).Cells("入出庫種別").Value.ToString '入出庫種別
                                'Sql += "', '"
                                'Sql += CommonConst.AC_KBN_NORMAL.ToString '引当区分(0：通常）
                                Sql += "', '"
                                Sql += DgvAdd.Rows(i).Cells("メーカー").Value.ToString 'メーカー
                                Sql += "', '"
                                Sql += DgvAdd.Rows(i).Cells("品名").Value.ToString '品名
                                Sql += "', '"
                                Sql += DgvAdd.Rows(i).Cells("型式").Value.ToString '型式
                                Sql += "', '"
                                Sql += currentVal.ToString '数量
                                Sql += "', '"
                                Sql += DgvAdd.Rows(i).Cells("単位").Value.ToString '単位
                                Sql += "', '"
                                Sql += DgvAdd.Rows(i).Cells("備考").Value.ToString '備考
                                Sql += "', '"
                                'Sql += UtilClass.formatDatetime(DtpGoodsIssueDate.Text) '入出庫日
                                Sql += shukkoDate
                                Sql += "', '"
                                Sql += CommonConst.CANCEL_KBN_ENABLED.ToString '取消区分
                                Sql += "', '"
                                Sql += Input '更新者
                                Sql += "', '"
                                Sql += UtilClass.formatDatetime(dtToday) '更新日
                                Sql += "', '"
                                'Sql += dsCurrentList.Tables(RS).Rows(x)("伝票番号") & DgvAdd.Rows(i).Cells("行番号").Value.ToString
                                Sql += dsCurrentList.Tables(RS).Rows(x)("伝票番号") & dsCurrentList.Tables(RS).Rows(x)("行番号")
                                'Sql += currentLS & "1"
                                Sql += "', '"
                                Sql += DgvAdd.Rows(i).Cells("仕入区分値").Value.ToString '仕入区分

                                Sql += "')"

                                _db.executeDB(Sql)

#End Region


                                '作成データが複数の場合にヘッダーも作成
                                If MAIN_LS <> currentLS Then

#Region "t44_shukohd"
                                    '受注基本
                                    Sql = " And "
                                    Sql += "受注番号 ILIKE '" & DgvAdd.Rows(i).Cells("受注番号").Value.ToString & "'"
                                    Sql += " AND "
                                    Sql += "受注番号枝番 ILIKE '" & DgvAdd.Rows(i).Cells("受注番号枝番").Value.ToString & "'"
                                    Sql += " AND "
                                    Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED '取消区分=0

                                    dsCymnhd = getDsData("t10_cymnhd", Sql)

                                    Sql = "INSERT INTO "
                                    Sql += "Public."
                                    Sql += "t44_shukohd("
                                    Sql += "会社コード, 出庫番号, 見積番号, 見積番号枝番, 受注番号, 受注番号枝番, 客先番号"
                                    Sql += ", 得意先コード, 得意先名, 得意先郵便番号, 得意先住所, 得意先電話番号, 得意先ＦＡＸ"
                                    Sql += ", 得意先担当者役職, 得意先担当者名, 営業担当者, 入力担当者, 備考, 取消日, 取消区分"
                                    Sql += ", 出庫日, 登録日, 更新日, 更新者, 営業担当者コード, 入力担当者コード)"
                                    Sql += " VALUES('"
                                    Sql += frmC01F10_Login.loginValue.BumonCD  '会社コード
                                    Sql += "', '"
                                    Sql += currentLS '出庫番号
                                    Sql += "', '"
                                    Sql += dsCymnhd.Tables(RS).Rows(0)("見積番号").ToString '見積番号
                                    Sql += "', '"
                                    Sql += dsCymnhd.Tables(RS).Rows(0)("見積番号枝番").ToString '見積番号枝番
                                    Sql += "', '"
                                    Sql += DgvAdd.Rows(i).Cells("受注番号").Value.ToString '受注番号
                                    Sql += "', '"
                                    Sql += DgvAdd.Rows(i).Cells("受注番号枝番").Value.ToString '受注番号枝番
                                    Sql += "', '"
                                    Sql += dsCymnhd.Tables(RS).Rows(0)("客先番号").ToString '客先番号
                                    Sql += "', '"
                                    Sql += dsCymnhd.Tables(RS).Rows(0)("得意先コード").ToString '得意先コード
                                    Sql += "', '"
                                    Sql += dsCymnhd.Tables(RS).Rows(0)("得意先名").ToString '得意先名
                                    Sql += "', '"
                                    Sql += dsCymnhd.Tables(RS).Rows(0)("得意先郵便番号").ToString '得意先郵便番号
                                    Sql += "', '"
                                    Sql += dsCymnhd.Tables(RS).Rows(0)("得意先住所").ToString '得意先住所
                                    Sql += "', '"
                                    Sql += dsCymnhd.Tables(RS).Rows(0)("得意先電話番号").ToString '得意先電話番号
                                    Sql += "', '"
                                    Sql += dsCymnhd.Tables(RS).Rows(0)("得意先ＦＡＸ").ToString '得意先ＦＡＸ
                                    Sql += "', '"
                                    Sql += dsCymnhd.Tables(RS).Rows(0)("得意先担当者役職").ToString '得意先担当者役職
                                    Sql += "', '"
                                    Sql += dsCymnhd.Tables(RS).Rows(0)("得意先担当者名").ToString '得意先担当者名
                                    Sql += "', '"
                                    Sql += dsCymnhd.Tables(RS).Rows(0)("営業担当者").ToString '営業担当者
                                    Sql += "', '"
                                    Sql += frmC01F10_Login.loginValue.TantoNM '.Tables(RS).Rows(0)("入力担当者").ToString '入力担当者
                                    Sql += "', '"
                                    Sql += UtilClass.escapeSql(TxtRemarks.Text) 'dsCymnhd.Tables(RS).Rows(0)("備考").ToString '備考
                                    Sql += "', "
                                    Sql += "null" '取消日
                                    Sql += ", '"
                                    Sql += CommonConst.CANCEL_KBN_ENABLED.ToString '取消区分
                                    Sql += "', '"
                                    Sql += UtilClass.formatDatetime(DtpGoodsIssueDate.Text) '出庫日
                                    Sql += "', '"
                                    Sql += UtilClass.formatDatetime(dtToday) '登録日
                                    Sql += "', '"
                                    Sql += UtilClass.formatDatetime(dtToday) '更新日
                                    Sql += "', '"
                                    Sql += Input '更新者
                                    Sql += "', '"
                                    Sql += dsCymnhd.Tables(RS).Rows(0)("営業担当者コード").ToString '営業担当者
                                    Sql += "', '"
                                    Sql += frmC01F10_Login.loginValue.TantoCD '入力担当者
                                    Sql += "')"

                                    _db.executeDB(Sql)

#End Region

                                End If

                            Next

                        End If

                    End If

                End If

            Next


#Region "t44_shukohd"
            '受注基本
            Sql = " And "
            Sql += "受注番号 ILIKE '" & DgvAdd.Rows(0).Cells("受注番号").Value.ToString & "'"
            Sql += " AND "
            Sql += "受注番号枝番 ILIKE '" & DgvAdd.Rows(0).Cells("受注番号枝番").Value.ToString & "'"
            Sql += " AND "
            Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED '取消区分=0

            dsCymnhd = getDsData("t10_cymnhd", Sql)

            Sql = "INSERT INTO "
            Sql += "Public."
            Sql += "t44_shukohd("
            Sql += "会社コード, 出庫番号, 見積番号, 見積番号枝番, 受注番号, 受注番号枝番, 客先番号"
            Sql += ", 得意先コード, 得意先名, 得意先郵便番号, 得意先住所, 得意先電話番号, 得意先ＦＡＸ"
            Sql += ", 得意先担当者役職, 得意先担当者名, 営業担当者, 入力担当者, 備考, 取消日, 取消区分"
            Sql += ", 出庫日, 登録日, 更新日, 更新者, 営業担当者コード, 入力担当者コード)"
            Sql += " VALUES('"
            Sql += dsCymnhd.Tables(RS).Rows(0)("会社コード").ToString '会社コード
            Sql += "', '"
            Sql += MAIN_LS '出庫番号
            Sql += "', '"
            Sql += dsCymnhd.Tables(RS).Rows(0)("見積番号").ToString '見積番号
            Sql += "', '"
            Sql += dsCymnhd.Tables(RS).Rows(0)("見積番号枝番").ToString '見積番号枝番
            Sql += "', '"
            Sql += dsCymnhd.Tables(RS).Rows(0)("受注番号").ToString '受注番号
            Sql += "', '"
            Sql += dsCymnhd.Tables(RS).Rows(0)("受注番号枝番").ToString '受注番号枝番
            Sql += "', '"
            Sql += dsCymnhd.Tables(RS).Rows(0)("客先番号").ToString '客先番号
            Sql += "', '"
            Sql += dsCymnhd.Tables(RS).Rows(0)("得意先コード").ToString '得意先コード
            Sql += "', '"
            Sql += dsCymnhd.Tables(RS).Rows(0)("得意先名").ToString '得意先名
            Sql += "', '"
            Sql += dsCymnhd.Tables(RS).Rows(0)("得意先郵便番号").ToString '得意先郵便番号
            Sql += "', '"
            Sql += dsCymnhd.Tables(RS).Rows(0)("得意先住所").ToString '得意先住所
            Sql += "', '"
            Sql += dsCymnhd.Tables(RS).Rows(0)("得意先電話番号").ToString '得意先電話番号
            Sql += "', '"
            Sql += dsCymnhd.Tables(RS).Rows(0)("得意先ＦＡＸ").ToString '得意先ＦＡＸ
            Sql += "', '"
            Sql += dsCymnhd.Tables(RS).Rows(0)("得意先担当者役職").ToString '得意先担当者役職
            Sql += "', '"
            Sql += dsCymnhd.Tables(RS).Rows(0)("得意先担当者名").ToString '得意先担当者名
            Sql += "', '"
            Sql += dsCymnhd.Tables(RS).Rows(0)("営業担当者").ToString '営業担当者
            Sql += "', '"
            Sql += frmC01F10_Login.loginValue.TantoNM '入力担当者
            Sql += "', '"
            Sql += UtilClass.escapeSql(TxtRemarks.Text) 'dsCymnhd.Tables(RS).Rows(0)("備考").ToString '備考
            Sql += "', "
            Sql += "null" '取消日
            Sql += ", '"
            Sql += CommonConst.CANCEL_KBN_ENABLED.ToString '取消区分
            Sql += "', '"
            Sql += UtilClass.formatDatetime(DtpGoodsIssueDate.Text) '出庫日
            Sql += "', '"
            Sql += UtilClass.formatDatetime(dtToday) '登録日
            Sql += "', '"
            Sql += UtilClass.formatDatetime(dtToday) '更新日
            Sql += "', '"
            Sql += Input '更新者
            Sql += "', '"
            Sql += dsCymnhd.Tables(RS).Rows(0)("営業担当者コード").ToString '営業担当者
            Sql += "', '"
            Sql += frmC01F10_Login.loginValue.TantoCD '入力担当者
            Sql += "')"

            _db.executeDB(Sql)

#End Region
            _db.commitTran()

        Catch ue As UsrDefException
            _db.rollbackTran()
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            _db.rollbackTran()
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try


        'm90_hanyo  SimpleRegistrationの可変キーが1の場合は仕入処理も行う
        Sql = "  AND 固定キー = 'SR'"
        Sql += " AND 可変キー = '1'"

        Dim dsHanyo As DataTable = getDsData("m90_hanyo", Sql).Tables(0)

        If dsHanyo.Rows.Count = 0 Then  'データなしの場合は終了
        Else
            '売上処理
            Dim blnFlg As Boolean = mUpdate_Uriage()
            If blnFlg = False Then
                Exit Sub
            End If
        End If


        '登録完了メッセージ
        _msgHd.dspMSG("completeInsert", frmC01F10_Login.loginValue.Language)


        dsHanyo = Nothing
        Me.Dispose()

    End Sub

    Private Function mUpdate_Uriage() As Boolean

        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim dtToday As DateTime = DateTime.Now
        Dim strToday As String = UtilClass.formatDatetime(dtToday)


#Region "SQL"

        Dim Sql As String = "SELECT * FROM public.t10_cymnhd"
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        'Sql += " AND 受注番号 = '" & No & "'"
        'Sql += " AND 受注番号枝番 = '" & Suffix & "'"
        Sql += " AND 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

        'Dim dsCymnHd As DataSet = _db.selectDB(Sql, RS, reccnt)


        'joinするのでとりあえず直書き
        Dim Sql2 As String = "SELECT"
        Sql2 += " t11.*"
        Sql2 += " FROM "
        Sql2 += " public.t11_cymndt t11 "

        Sql2 += " INNER JOIN "
        Sql2 += " t10_cymnhd t10"
        Sql2 += " ON "

        Sql2 += " t11.会社コード = t10.会社コード"
        Sql2 += " AND "
        Sql2 += " t11.受注番号 = t10.受注番号"
        Sql2 += " AND "
        Sql2 += " t11.受注番号 = t10.受注番号"

        Sql2 += " WHERE "
        Sql2 += " t11.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql2 += " AND "
        'Sql2 += " t11.受注番号 = '" & No & "'"
        Sql2 += " AND "
        'Sql2 += " t11.受注番号枝番 = '" & Suffix & "'"
        Sql2 += " AND "
        Sql2 += "t10.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

        '受注明細取得
        'Dim dsCymndt As DataSet = _db.selectDB(Sql2, RS, reccnt)

#End Region


        Try
            _db.beginTran()

            'Dim ER As String = _com.getSaiban("40", dtToday)
            'Dim LS As String = getSaiban("70", dtToday　  // 使用されていない

            Dim totalUriAmount As Decimal = 0
            Dim totalArariAmount As Decimal = 0
            Dim totalmitsuAmount As Decimal = 0
            Dim totalSalesAmount_cur As Decimal = 0
            Dim ao_ As String = ""
            Dim v_ As String = ""
            Dim b_ As Boolean = False
            Dim ER As String = "" '= _com.getSaiban("40", dtToday)

            't31_urigdt 売上明細更新
            For i As Integer = 0 To DgvAdd.Rows.Count() - 1


                If DgvAdd.Rows(i).Cells("出庫数量").Value = 0 Then  '今回出庫数が入力されていない場合は読み飛ばし
                    Continue For
                End If

                If ao_ & v_ <> DgvAdd.Rows(i).Cells("受注番号").Value & DgvAdd.Rows(i).Cells("受注番号枝番").Value Then
                    ao_ = DgvAdd.Rows(i).Cells("受注番号").Value
                    v_ = DgvAdd.Rows(i).Cells("受注番号枝番").Value
                    b_ = True
                    ER = _com.getSaiban("40", dtToday)
                End If

                'joinするのでとりあえず直書き
                Sql2 = "SELECT"
                Sql2 += " t11.* "
                Sql2 += " FROM "
                Sql2 += " public.t11_cymndt t11 "

                Sql2 += " INNER JOIN "
                Sql2 += " t10_cymnhd t10"
                Sql2 += " ON "

                Sql2 += " t11.会社コード = t10.会社コード"
                Sql2 += " AND "
                Sql2 += " t11.受注番号 = t10.受注番号"
                Sql2 += " AND "
                Sql2 += " t11.受注番号 = t10.受注番号"

                Sql2 += " WHERE "
                Sql2 += " t11.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql2 += " AND "
                Sql2 += " t11.受注番号 = '" & ao_ & "'"
                Sql2 += " AND "
                Sql2 += " t11.受注番号枝番 = '" & v_ & "'"
                Sql2 += " AND "
                Sql2 += "t10.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
                Sql2 += " AND "
                Sql2 += " t11.行番号=" & DgvAdd.Rows(i).Cells("行番号").Value & ""

                '受注明細取得
                Dim dsCymndt As DataSet = _db.selectDB(Sql2, RS, reccnt)

                For x As Integer = 0 To dsCymndt.Tables(RS).Rows.Count - 1  '受注ループ

#Region "t31_urigdt"

                    Dim Sql4 As String = "INSERT INTO "
                    Sql4 += "Public."
                    Sql4 += "t31_urigdt("
                    Sql4 += "会社コード, 売上番号, 売上番号枝番, 受注番号, 受注番号枝番, 行番号, 仕入区分, メーカー, 品名, 型式"
                    Sql4 += ", 仕入先名, 仕入値, 受注数量, 売上数量, 受注残数, 単位, 売単価, 売上金額, 粗利額, 粗利率, 間接費"
                    Sql4 += ", リードタイム, 備考, 更新者, 更新日, 仕入原価, 関税率, 関税額, 前払法人税率, 前払法人税額"
                    Sql4 += ", 輸送費率, 輸送費額, 仕入金額, 見積単価, 見積金額,売上金額_外貨,売上単価_外貨)"
                    Sql4 += " VALUES('"
                    Sql4 += frmC01F10_Login.loginValue.BumonCD
                    Sql4 += "', '"
                    Sql4 += ER '売上番号
                    Sql4 += "', '"
                    Sql4 += "1" '売上番号枝番
                    Sql4 += "', '"
                    Sql4 += ao_ '受注番号
                    Sql4 += "', '"
                    Sql4 += v_ '受注番号枝番
                    Sql4 += "', '"
                    Sql4 += DgvAdd.Rows(i).Cells("行番号").Value.ToString '行番号
                    Sql4 += "', '"
                    Sql4 += DgvAdd.Rows(i).Cells("仕入区分値").Value.ToString '仕入区分値
                    Sql4 += "', '"
                    Sql4 += UtilClass.escapeSql(DgvAdd.Rows(i).Cells("メーカー").Value.ToString) 'メーカー
                    Sql4 += "', '"
                    Sql4 += UtilClass.escapeSql(DgvAdd.Rows(i).Cells("品名").Value.ToString) '品名
                    Sql4 += "', '"
                    Sql4 += UtilClass.escapeSql(DgvAdd.Rows(i).Cells("型式").Value.ToString) '型式
                    Sql4 += "', '"
                    Sql4 += DgvAdd.Rows(i).Cells("仕入先").Value.ToString '仕入先名
                    Sql4 += "', '"
                    Sql4 += UtilClass.formatNumber(dsCymndt.Tables(RS).Rows(x)("仕入値").ToString) '仕入値
                    Sql4 += "', '"
                    Sql4 += UtilClass.formatNumber(dsCymndt.Tables(RS).Rows(x)("受注数量").ToString) '受注数量
                    Sql4 += "', '"
                    Sql4 += UtilClass.formatNumber(DgvAdd.Rows(i).Cells("出庫数量").Value.ToString) '売上数量
                    Sql4 += "', '"

                    'Dim OrderNo As Integer = dsCymndt.Tables(RS).Rows(x)("受注数量")
                    Dim OrderNo As Decimal = dsCymndt.Tables(RS).Rows(x)("受注残数")
                    Dim PurchaseNo As Decimal = DgvAdd.Rows(i).Cells("出庫数量").Value
                    Dim RemainingNo As Decimal = OrderNo - PurchaseNo

                    Sql4 += UtilClass.formatNumber(RemainingNo) '受注残数
                    Sql4 += "', '"
                    Sql4 += DgvAdd.Rows(i).Cells("単位").Value '単位
                    Sql4 += "', '"
                    Sql4 += UtilClass.formatNumber(dsCymndt.Tables(RS).Rows(x)("売単価").ToString) '売単価
                    Sql4 += "', '"

                    Dim uriAmount As Decimal = IIf(dsCymndt.Tables(RS).Rows(x)("売単価").ToString <> 0,
                                                      Decimal.Parse(dsCymndt.Tables(RS).Rows(x)("売単価").ToString * DgvAdd.Rows(i).Cells("出庫数量").Value.ToString),
                                                      0)

                    Sql4 += UtilClass.formatNumber(Format(uriAmount, "0.000")) '売上金額
                    Sql4 += "', '"

                    Dim arariAmount As Decimal = IIf(dsCymndt.Tables(RS).Rows(x)("売単価").ToString <> 0,
                                                      Decimal.Parse((dsCymndt.Tables(RS).Rows(x)("売単価").ToString - dsCymndt.Tables(RS).Rows(x)("仕入値").ToString) * DgvAdd.Rows(i).Cells("出庫数量").Value),
                                                      0)

                    Sql4 += UtilClass.formatNumber(Format(arariAmount, "0.000")) '粗利額
                    Sql4 += "', '"

                    If dsCymndt.Tables(RS).Rows(x)("売単価").ToString > 0 Then
                        Sql4 += UtilClass.formatNumber(Format(100 - (dsCymndt.Tables(RS).Rows(x)("仕入値").ToString / dsCymndt.Tables(RS).Rows(x)("売単価").ToString) * 100, "0.0")) '粗利率
                    Else
                        Sql4 += "0" '粗利率
                    End If
                    Sql4 += "', '"

                    Dim kansetsuhi As Decimal = (dsCymndt.Tables(RS).Rows(x)("関税額") + dsCymndt.Tables(RS).Rows(x)("前払法人税額") + dsCymndt.Tables(RS).Rows(x)("輸送費額")) * DgvAdd.Rows(i).Cells("出庫数量").Value.ToString

                    Sql4 += UtilClass.formatNumber(kansetsuhi) '間接費
                    Sql4 += "', '"
                    Sql4 += dsCymndt.Tables(RS).Rows(x)("リードタイム").ToString
                    Sql4 += "', '"
                    Sql4 += DgvAdd.Rows(i).Cells("備考").Value
                    Sql4 += "', '"
                    Sql4 += Input '更新者
                    Sql4 += "', '"
                    Sql4 += strToday '更新日
                    Sql4 += "', '"
                    Sql4 += UtilClass.formatNumber(dsCymndt.Tables(RS).Rows(x)("仕入値").ToString * DgvAdd.Rows(i).Cells("出庫数量").Value.ToString)
                    Sql4 += "', '"
                    Sql4 += UtilClass.formatNumber(dsCymndt.Tables(RS).Rows(x)("関税率").ToString)
                    Sql4 += "', '"
                    Sql4 += UtilClass.formatNumber(dsCymndt.Tables(RS).Rows(x)("関税額").ToString)
                    Sql4 += "', '"
                    Sql4 += UtilClass.formatNumber(dsCymndt.Tables(RS).Rows(x)("前払法人税率").ToString)
                    Sql4 += "', '"
                    Sql4 += UtilClass.formatNumber(dsCymndt.Tables(RS).Rows(x)("前払法人税額").ToString)
                    Sql4 += "', '"
                    Sql4 += UtilClass.formatNumber(dsCymndt.Tables(RS).Rows(x)("輸送費率").ToString)
                    Sql4 += "', '"
                    Sql4 += UtilClass.formatNumber(dsCymndt.Tables(RS).Rows(x)("輸送費額").ToString)
                    Sql4 += "', '"
                    'Sql4 += UtilClass.formatNumber((dsCymndt.Tables(RS).Rows(x)("仕入値").ToString + kansetsuhi) * DgvAdd.Rows(i).Cells("出庫数量").Value.ToString)
                    Sql4 += UtilClass.formatNumber(dsCymndt.Tables(RS).Rows(x)("仕入値").ToString * DgvAdd.Rows(i).Cells("出庫数量").Value.ToString + kansetsuhi)
                    Sql4 += "',"
                    Sql4 += UtilClass.formatNumber(dsCymndt.Tables(RS).Rows(x)("見積単価").ToString)
                    Sql4 += ","

                    Dim mitsuAmount As Decimal = dsCymndt.Tables(RS).Rows(x)("見積単価").ToString * DgvAdd.Rows(i).Cells("出庫数量").Value.ToString

                    Sql4 += UtilClass.formatNumber(mitsuAmount) '見積金額

                    Dim decUriTanka As Decimal = Decimal.Parse(dsCymndt.Tables(RS).Rows(x)("見積単価_外貨").ToString)
                    Dim decUrikingaku As Decimal = decUriTanka * Decimal.Parse(DgvAdd.Rows(i).Cells("出庫数量").Value.ToString)
                    Sql4 += "," + UtilClass.formatNumber(decUrikingaku) + ""  '売上金額_外貨  2020.02.23
                    Sql4 += "," + UtilClass.formatNumber(decUriTanka) + ""    '売上単価_外貨  2020.02.23
                    Sql4 += ")"
                    _db.executeDB(Sql4)

#End Region

                    totalUriAmount += uriAmount
                    totalArariAmount += arariAmount
                    totalmitsuAmount += mitsuAmount
                    totalSalesAmount_cur += decUrikingaku

#Region "t11_cymndt"

                    Dim PurchaseNum As Decimal = 0
                    Dim OrdingNum As Decimal = 0


                    Dim Sql6 As String = "UPDATE Public.t11_cymndt "
                    Sql6 += "SET "
                    Sql6 += "売上数量"
                    Sql6 += " = '"
                    PurchaseNum = dsCymndt.Tables(RS).Rows(x)("売上数量") + DgvAdd.Rows(i).Cells("出庫数量").Value
                    Sql6 += UtilClass.formatNumber(PurchaseNum.ToString)
                    Sql6 += "'"
                    Sql6 += " ,受注残数 = '"
                    OrdingNum = dsCymndt.Tables(RS).Rows(x)("受注残数") - DgvAdd.Rows(i).Cells("出庫数量").Value
                    Sql6 += UtilClass.formatNumber(OrdingNum.ToString)
                    Sql6 += "' "
                    Sql6 += " ,更新者 = '" & Input & "' "
                    Sql6 += "WHERE 会社コード='" & frmC01F10_Login.loginValue.BumonCD & "'"
                    Sql6 += " AND 受注番号='" & ao_ & "'"
                    Sql6 += " AND 受注番号枝番='" & v_ & "'"
                    Sql6 += " AND 行番号='" & dsCymndt.Tables(RS).Rows(x)("行番号").ToString & "' "

                    _db.executeDB(Sql6)

#End Region

                Next
                If b_ Then
#Region "t30_urighd"
                    Sql = "SELECT * FROM public.t10_cymnhd"
                    Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                    Sql += " AND 受注番号 = '" & ao_ & "'"
                    Sql += " AND 受注番号枝番 = '" & v_ & "'"
                    Sql += " AND 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

                    Dim dsCymnHd As DataSet = _db.selectDB(Sql, RS, reccnt)

                    't30_urighd 売上基本更新
                    Dim Sql3 As String = "INSERT INTO "
                    Sql3 += "Public."
                    Sql3 += "t30_urighd("
                    Sql3 += "会社コード, 売上番号, 売上番号枝番, 客先番号, 受注番号, 受注番号枝番, 見積番号, 見積番号枝番"
                    Sql3 += ", 得意先コード, 得意先名, 得意先郵便番号, 得意先住所, 得意先電話番号, 得意先ＦＡＸ, 得意先担当者役職"
                    Sql3 += ", 得意先担当者名, 見積日, 見積有効期限, 支払条件, 見積金額, 売上金額, 粗利額, 営業担当者, 入力担当者"
                    Sql3 += ", 備考, 取消日, 取消区分, ＶＡＴ, ＰＰＨ, 受注日, 売上日, 入金予定日, 登録日, 更新日, 更新者, 仕入金額"
                    Sql3 += ", 営業担当者コード, 入力担当者コード,売上金額_外貨, 通貨, レート)"
                    Sql3 += " VALUES('"
                    Sql3 += frmC01F10_Login.loginValue.BumonCD '会社コード
                    Sql3 += "', '"
                    Sql3 += ER '売上番号
                    Sql3 += "', '"
                    Sql3 += "1" '売上番号枝番
                    Sql3 += "', '"
                    Sql3 += dsCymnHd.Tables(RS).Rows(0)("客先番号").ToString '客先番号
                    Sql3 += "', '"
                    Sql3 += ao_ '受注番号
                    Sql3 += "', '"
                    Sql3 += v_ '受注番号枝番
                    Sql3 += "', '"
                    Sql3 += dsCymnHd.Tables(RS).Rows(0)("見積番号").ToString '見積番号
                    Sql3 += "', '"
                    Sql3 += dsCymnHd.Tables(RS).Rows(0)("見積番号枝番").ToString '見積番号枝番
                    Sql3 += "', '"
                    Sql3 += dsCymnHd.Tables(RS).Rows(0)("得意先コード").ToString '得意先コード
                    Sql3 += "', '"
                    Sql3 += dsCymnHd.Tables(RS).Rows(0)("得意先名").ToString '得意先名
                    Sql3 += "', '"
                    Sql3 += dsCymnHd.Tables(RS).Rows(0)("得意先郵便番号").ToString '得意先郵便番号
                    Sql3 += "', '"
                    Sql3 += dsCymnHd.Tables(RS).Rows(0)("得意先住所").ToString '得意先住所
                    Sql3 += "', '"
                    Sql3 += dsCymnHd.Tables(RS).Rows(0)("得意先電話番号").ToString '得意先電話番号
                    Sql3 += "', '"
                    Sql3 += dsCymnHd.Tables(RS).Rows(0)("得意先ＦＡＸ").ToString '得意先ＦＡＸ
                    Sql3 += "', '"
                    Sql3 += dsCymnHd.Tables(RS).Rows(0)("得意先担当者役職").ToString '得意先担当者役職
                    Sql3 += "', '"
                    Sql3 += dsCymnHd.Tables(RS).Rows(0)("得意先担当者名").ToString '得意先担当者名
                    Sql3 += "', '"
                    Sql3 += UtilClass.strFormatDate(dsCymnHd.Tables(RS).Rows(0)("見積日").ToString) '見積日
                    Sql3 += "', '"
                    Sql3 += UtilClass.strFormatDate(dsCymnHd.Tables(RS).Rows(0)("見積有効期限").ToString) '見積有効期限
                    Sql3 += "', '"
                    Sql3 += dsCymnHd.Tables(RS).Rows(0)("支払条件").ToString '支払条件
                    Sql3 += "', '"
                    Sql3 += UtilClass.formatNumber(totalmitsuAmount) '見積金額
                    Sql3 += "', '"
                    Sql3 += UtilClass.formatNumber(totalUriAmount) '売上金額（売単価 * 数量）
                    Sql3 += "', '"
                    Sql3 += UtilClass.formatNumber(totalArariAmount) '粗利額
                    Sql3 += "', '"
                    Sql3 += dsCymnHd.Tables(RS).Rows(0)("営業担当者").ToString '営業担当者
                    Sql3 += "', '"
                    Sql3 += dsCymnHd.Tables(RS).Rows(0)("入力担当者").ToString '入力担当者
                    Sql3 += "', '"
                    Sql3 += TxtRemarks.Text '備考
                    Sql3 += "', "
                    Sql3 += "null" '取消日
                    Sql3 += ", "
                    Sql3 += "0" '取消区分
                    Sql3 += ", '"
                    Sql3 += UtilClass.formatNumber(dsCymnHd.Tables(RS).Rows(0)("ＶＡＴ")) 'ＶＡＴ 率
                    Sql3 += "', '"
                    If dsCymnHd.Tables(RS).Rows(0)("ＰＰＨ") Is DBNull.Value Then
                        Sql3 += "0" 'ＰＰＨ
                    Else
                        Sql3 += UtilClass.formatNumber(dsCymnHd.Tables(RS).Rows(0)("ＰＰＨ")) 'ＰＰＨ
                    End If
                    Sql3 += "', '"
                    Sql3 += UtilClass.strFormatDate(TxtOrderDate.Text) '受注日
                    Sql3 += "', '"
                    Sql3 += UtilClass.strFormatDate(DtpGoodsIssueDate.Text) '売上日
                    Sql3 += "', '"
                    Sql3 += UtilClass.strFormatDate(DtpDepositDate.Text) '入金予定日
                    Sql3 += "', '"
                    Sql3 += strToday '登録日
                    Sql3 += "', '"
                    Sql3 += strToday '更新日
                    Sql3 += "', '"
                    Sql3 += Input '更新者
                    Sql3 += "', "
                    Sql3 += UtilClass.formatNumber(dsCymnHd.Tables(RS).Rows(0)("仕入金額")) '仕入金額
                    Sql3 += ", '"
                    Sql3 += dsCymnHd.Tables(RS).Rows(0)("営業担当者コード").ToString '営業担当者コード
                    Sql3 += "', '"
                    Sql3 += frmC01F10_Login.loginValue.TantoCD '入力担当者コード
                    Sql3 += "'," + UtilClass.formatNumber(totalSalesAmount_cur) + ""               '売上金額_外貨
                    Sql3 += ",'" + dsCymnHd.Tables(RS).Rows(0)("通貨").ToString() + "'"  '通貨
                    Sql3 += "," + UtilClass.formatNumberF10(_com.setRate(dsCymnHd.Tables(RS).Rows(0)("通貨"), DtpGoodsIssueDate.Text)) + ""              'レート
                    Sql3 += ")"

                    _db.executeDB(Sql3)
                    b_ = False
                End If
#End Region
            Next
            _db.commitTran()
        Catch ex As Exception
            _db.rollbackTran()
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))

        End Try


        mUpdate_Uriage = True

    End Function

    Private Sub BtnDeliveryNote_Click(sender As Object, e As EventArgs) Handles BtnDeliveryNote.Click

        'If frmC01F10_Login.loginValue.BumonCD = "ZENBI" Then
        DeliveryNote_Zenbi()
        'Else
        'DeliveryNote_StdCompany()
        'End If

    End Sub

    Private Sub DeliveryNote_StdCompany()

        Dim SelectedRow As Integer = DgvHistory.CurrentCell.RowIndex

        Dim createFlg = False

        '2019.12.19 受注番号基準に変更し、受注番号、出庫日、出庫区分を抽出条件とする
        Dim Sql1 As String = ""
        Sql1 += "SELECT HD.* FROM public.t44_shukohd HD"
        Sql1 += " INNER JOIN public.t45_shukodt DT ON (HD.会社コード=DT.会社コード AND HD.出庫番号=DT.出庫番号)"
        ''Sql1 += " WHERE 出庫番号 = '" & DgvHistory.Rows(SelectedRow).Cells("出庫番号").Value & "'"
        Sql1 += " WHERE HD.受注番号 = '" & TxtOrderNo.Text & "'"
        Sql1 += " AND HD.出庫日 = '" & CDate(DgvHistory.Rows(SelectedRow).Cells("出庫日").Value).ToString("yyyyMMdd") & "'"
        Sql1 += " AND DT.出庫区分 = '1'"



        '2019.12.19 受注番号基準に変更し、受注番号、出庫日、出庫区分を抽出条件とする
        Dim Sql2 As String = ""
        Sql2 += "SELECT DT.* FROM public.t45_shukodt DT"
        Sql2 += " INNER JOIN public.t44_shukohd HD ON (HD.会社コード=DT.会社コード AND HD.出庫番号=DT.出庫番号)"
        'Sql2 += " WHERE 出庫番号 = '" & DgvHistory.Rows(SelectedRow).Cells("出庫番号").Value & "'"
        Sql2 += " WHERE DT.受注番号 = '" & TxtOrderNo.Text & "' AND DT.出庫区分 = '1'"
        Sql2 += "   AND HD.出庫日 = '" & CDate(DgvHistory.Rows(SelectedRow).Cells("出庫日").Value).ToString("yyyyMMdd") & "'"

        Dim reccnt As Integer = 0
        Dim ds1 As DataSet = _db.selectDB(Sql1, RS, reccnt)
        Dim ds2 As DataSet = _db.selectDB(Sql2, RS, reccnt)

        '定義
        Dim app As Excel.Application = Nothing
        Dim book As Excel.Workbook = Nothing
        Dim sheet As Excel.Worksheet = Nothing

        'カーソルをビジー状態にする
        Cursor.Current = Cursors.WaitCursor

        Try
            '雛形パス
            Dim sHinaPath As String = ""
            sHinaPath = StartUp._iniVal.BaseXlsPath

            '雛形ファイル名
            Dim sHinaFileDeliv As String = ""
            sHinaFileDeliv = sHinaPath & "\" & "DeliveryNote.xlsx"

            Dim sHinaFileReceipt As String = ""
            sHinaFileReceipt = sHinaPath & "\" & "DeliveryNote.xlsx"

            '出力先パス
            Dim sOutPath As String = ""
            sOutPath = StartUp._iniVal.OutXlsPath

            '出力ファイル名
            '2019.12.19 受注番号+1行目出庫番号基準に変更
            Dim sOutFile As String = ""
            ''sOutFile = sOutPath & "\DeliveryNote_" & ds2.Tables(RS).Rows(0)("出庫番号") & ".xlsx"
            sOutFile = sOutPath & "\DeliveryNote_" & TxtOrderNo.Text & ds2.Tables(RS).Rows(0)("出庫番号") & ".xlsx"



            app = New Excel.Application()
            book = app.Workbooks.Add(sHinaFileDeliv)  'テンプレート
            sheet = CType(book.Worksheets(1), Excel.Worksheet)

            sheet.Range("B7").Value = ds1.Tables(RS).Rows(0)("得意先名")
            sheet.Range("B8").Value = ds1.Tables(RS).Rows(0)("得意先住所") & " " & ds1.Tables(RS).Rows(0)("得意先郵便番号")
            sheet.Range("B11").Value = "'" & ds1.Tables(RS).Rows(0)("得意先電話番号")

            '2019.12.19 出庫番号ー＞受注番号表示に改訂
            ''sheet.Range("E7").Value = ":" & ds1.Tables(RS).Rows(0)("出庫番号")
            sheet.Range("E7").Value = ":" & TxtOrderNo.Text
            sheet.Range("E8").Value = ds1.Tables(RS).Rows(0)("出庫日")
            sheet.Range("E9").Value = ds1.Tables(RS).Rows(0)("客先番号")


            Dim rowCnt As Integer = 0
            Dim lstRow As Integer = 14
            'Dim addRowCnt As Integer = 0
            'Dim currentCnt As Integer = 20
            Dim num As Integer = 1


            For j As Integer = 0 To ds2.Tables(RS).Rows.Count - 1
                'If rowCnt = 0 Then
                '    sheet.Range("A14").Value = num
                '    'sheet.Range("C14").Value = ds2.Tables(RS).Rows(j)("メーカー") & " / " & ds2.Tables(RS).Rows(j)("品名") & " / " & ds2.Tables(RS).Rows(j)("型式")
                '    sheet.Range("C14").Value = ds2.Tables(RS).Rows(j)("メーカー") & Environment.NewLine & ds2.Tables(RS).Rows(j)("品名") & Environment.NewLine & ds2.Tables(RS).Rows(j)("型式")
                '    sheet.Range("N14").Value = ds2.Tables(RS).Rows(j)("出庫数量")
                '    sheet.Range("Q14").Value = ds2.Tables(RS).Rows(j)("単位")
                '    sheet.Range("T14").Value = ds2.Tables(RS).Rows(j)("備考")
                '    'sheet.Rows(lstRow & ":" & lstRow).AutoFit
                'Else
                Dim cellPos As String = lstRow & ":" & lstRow
                Dim R As Object
                cellPos = lstRow & ":" & lstRow
                R = sheet.Range(cellPos)
                R.Copy()
                R.Insert()
                If Marshal.IsComObject(R) Then
                    Marshal.ReleaseComObject(R)
                End If

                'lstRow = lstRow + 1

                '2019.12.19 Range("B")に出庫番号を出力する改訂
                sheet.Range("A" & lstRow).Value = num
                sheet.Range("B" & lstRow).Value = ds2.Tables(RS).Rows(j)("メーカー") & Environment.NewLine & ds2.Tables(RS).Rows(j)("品名") &
                                                  Environment.NewLine & ds2.Tables(RS).Rows(j)("型式") &
                                                  Environment.NewLine & "DeliveryNoteNo." & ds2.Tables(RS).Rows(j)("出庫番号") & "-" & ds2.Tables(RS).Rows(j)("行番号")
                sheet.Range("C" & lstRow).Value = ds2.Tables(RS).Rows(j)("出庫数量")
                sheet.Range("D" & lstRow).Value = ds2.Tables(RS).Rows(j)("単位")
                sheet.Range("E" & lstRow).Value = ds2.Tables(RS).Rows(j)("備考")
                'sheet.Rows(lstRow & ":" & lstRow)
                '.EntireColumn.AutoFit


                'End If
                num += 1
                lstRow = lstRow + 1
            Next

            sheet.Cells.Rows.AutoFit()

            app.DisplayAlerts = False 'Microsoft Excelのアラート一旦無効化

            Dim excelChk As Boolean = _com.excelOutput(sOutFile)
            If excelChk = False Then
                Exit Sub
            End If
            book.SaveAs(sOutFile) '書き込み実行

            app.DisplayAlerts = True 'アラート無効化を解除

            app.Visible = True

            '_msgHd.dspMSG("CreateExcel")
            createFlg = True

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

        Try

            '雛形パス
            Dim sHinaPath As String = ""
            sHinaPath = StartUp._iniVal.BaseXlsPath

            '雛形ファイル名
            Dim sHinaFile As String = ""
            sHinaFile = sHinaPath & "\" & "PackingList.xlsx"

            '出力先パス
            Dim sOutPath As String = ""
            sOutPath = StartUp._iniVal.OutXlsPath

            '出力ファイル名
            '2019.12.19 受注番号+1行目出庫番号基準に変更
            Dim sOutFile As String = ""
            ''sOutFile = sOutPath & "\PackingList_" & ds2.Tables(RS).Rows(0)("出庫番号") & ".xlsx"
            sOutFile = sOutPath & "\PackingList_" & TxtOrderNo.Text & ds2.Tables(RS).Rows(0)("出庫番号") & ".xlsx"

            app = New Excel.Application()
            book = app.Workbooks.Add(sHinaFile)  'テンプレート
            sheet = CType(book.Worksheets(1), Excel.Worksheet)

            sheet.Range("B8").Value = ds1.Tables(RS).Rows(0)("得意先名")
            'sheet.Range("B10").Value = ds1.Tables(RS).Rows(0)("得意先郵便番号")
            sheet.Range("B9").Value = ds1.Tables(RS).Rows(0)("得意先住所") & " " & ds1.Tables(RS).Rows(0)("得意先郵便番号")
            sheet.Range("B11").Value = "'" & ds1.Tables(RS).Rows(0)("得意先電話番号")

            '2019.12.19 出庫番号ー＞受注番号表示に改訂
            ''sheet.Range("G8").Value = ds1.Tables(RS).Rows(0)("出庫番号")
            sheet.Range("G8").Value = TxtOrderNo.Text
            sheet.Range("G9").Value = ds1.Tables(RS).Rows(0)("出庫日")
            sheet.Range("G10").Value = ds1.Tables(RS).Rows(0)("客先番号")


            Dim rowCnt As Integer = 0
            Dim lstRow As Integer = 14
            'Dim addRowCnt As Integer = 0
            'Dim currentCnt As Integer = 20
            Dim num As Integer = 1


            For j As Integer = 0 To ds2.Tables(RS).Rows.Count - 1
                'If rowCnt = 0 Then
                '    sheet.Range("A" & lstRow).Value = num
                '    sheet.Range("B" & lstRow).Value = ds2.Tables(RS).Rows(j)("メーカー") & Environment.NewLine & ds2.Tables(RS).Rows(j)("品名") & Environment.NewLine & ds2.Tables(RS).Rows(j)("型式")
                '    sheet.Range("F" & lstRow).Value = ds2.Tables(RS).Rows(j)("出庫数量") & " " & ds2.Tables(RS).Rows(j)("単位")
                '    'sheet.Rows(lstRow & ":" & lstRow).AutoFit
                'Else
                Dim cellPos As String = lstRow & ":" & lstRow
                Dim R As Object
                cellPos = lstRow & ":" & lstRow
                R = sheet.Range(cellPos)
                R.Copy()
                R.Insert()
                If Marshal.IsComObject(R) Then
                    Marshal.ReleaseComObject(R)
                End If

                'lstRow = lstRow + 1

                '2019.12.19 Range("B")に出庫番号を出力する改訂
                sheet.Range("A" & lstRow).Value = num
                sheet.Range("B" & lstRow).Value = ds2.Tables(RS).Rows(j)("メーカー") & Environment.NewLine & ds2.Tables(RS).Rows(j)("品名") &
                                                  Environment.NewLine & ds2.Tables(RS).Rows(j)("型式") &
                                                  Environment.NewLine & "DeliveryNoteNo." & ds2.Tables(RS).Rows(j)("出庫番号") & "-" & ds2.Tables(RS).Rows(j)("行番号")
                sheet.Range("F" & lstRow).Value = ds2.Tables(RS).Rows(j)("出庫数量") & " " & ds2.Tables(RS).Rows(j)("単位")
                'sheet.Rows(lstRow & ":" & lstRow).AutoFit

                'End If
                num += 1
                lstRow = lstRow + 1
            Next

            app.DisplayAlerts = False 'Microsoft Excelのアラート一旦無効化

            Dim excelChk As Boolean = _com.excelOutput(sOutFile)
            If excelChk = False Then
                Exit Sub
            End If
            book.SaveAs(sOutFile) '書き込み実行

            app.DisplayAlerts = True 'アラート無効化を解除
            app.Visible = True

            '_msgHd.dspMSG("CreateExcel")
            createFlg = True

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

        Try
            '雛形パス
            Dim sHinaPath As String = ""
            sHinaPath = StartUp._iniVal.BaseXlsPath

            '雛形ファイル名
            Dim sHinaFileReceipt As String = ""
            sHinaFileReceipt = sHinaPath & "\" & "Receipt.xlsx"

            '出力先パス
            Dim sOutPath As String = ""
            sOutPath = StartUp._iniVal.OutXlsPath

            '出力ファイル名
            '2019.12.19 受注番号+1行目出庫番号基準に変更
            Dim sOutFile As String = ""
            ''sOutFile = sOutPath & "\Receipt_" & ds2.Tables(RS).Rows(0)("出庫番号") & ".xlsx"
            sOutFile = sOutPath & "\Receipt_" & TxtOrderNo.Text & ds2.Tables(RS).Rows(0)("出庫番号") & ".xlsx"



            app = New Excel.Application()
            book = app.Workbooks.Add(sHinaFileReceipt)  'テンプレート
            sheet = CType(book.Worksheets(1), Excel.Worksheet)

            sheet.Range("E8").Value = ds1.Tables(RS).Rows(0)("得意先名")
            sheet.Range("E9").Value = ds1.Tables(RS).Rows(0)("得意先住所") & " " & ds1.Tables(RS).Rows(0)("得意先郵便番号")
            sheet.Range("E11").Value = "'" & ds1.Tables(RS).Rows(0)("得意先電話番号")

            '2019.12.19 出庫番号ー＞受注番号表示に改訂
            ''sheet.Range("U8").Value = ds1.Tables(RS).Rows(0)("出庫番号")
            sheet.Range("U8").Value = TxtOrderNo.Text
            sheet.Range("U9").Value = ds1.Tables(RS).Rows(0)("出庫日")
            sheet.Range("U10").Value = ds1.Tables(RS).Rows(0)("客先番号")


            Dim rowCnt As Integer = 0
            Dim lstRow As Integer = 14
            'Dim addRowCnt As Integer = 0
            'Dim currentCnt As Integer = 20
            Dim num As Integer = 1


            For j As Integer = 0 To ds2.Tables(RS).Rows.Count - 1
                'If rowCnt = 0 Then
                '    sheet.Range("A14").Value = num
                '    sheet.Range("C14").Value = ds2.Tables(RS).Rows(j)("メーカー") & " / " & ds2.Tables(RS).Rows(j)("品名") & " / " & ds2.Tables(RS).Rows(j)("型式")
                '    sheet.Range("N14").Value = ds2.Tables(RS).Rows(j)("出庫数量")
                '    sheet.Range("Q14").Value = ds2.Tables(RS).Rows(j)("単位")
                '    sheet.Range("T14").Value = ds2.Tables(RS).Rows(j)("備考")
                '    'sheet.Rows(lstRow & ":" & lstRow).AutoFit
                'Else
                Dim cellPos As String = lstRow & ":" & lstRow
                Dim R As Object
                cellPos = lstRow & ":" & lstRow
                R = sheet.Range(cellPos)
                R.Copy()
                R.Insert()
                If Marshal.IsComObject(R) Then
                    Marshal.ReleaseComObject(R)
                End If

                'lstRow = lstRow + 1

                '2019.12.19 Range("C")に出庫番号を出力する改訂
                sheet.Range("A" & lstRow).Value = num
                sheet.Range("C" & lstRow).Value = ds2.Tables(RS).Rows(j)("メーカー") & Environment.NewLine & ds2.Tables(RS).Rows(j)("品名") &
                                                  Environment.NewLine & ds2.Tables(RS).Rows(j)("型式") &
                                                  Environment.NewLine & "DeliveryNoteNo." & ds2.Tables(RS).Rows(j)("出庫番号") & "-" & ds2.Tables(RS).Rows(j)("行番号")
                sheet.Range("N" & lstRow).Value = ds2.Tables(RS).Rows(j)("出庫数量")
                sheet.Range("Q" & lstRow).Value = ds2.Tables(RS).Rows(j)("単位")
                sheet.Range("T" & lstRow).Value = ds2.Tables(RS).Rows(j)("備考")
                'sheet.Rows(lstRow & ":" & lstRow).AutoFit

                'End If
                num += 1
                lstRow = lstRow + 1
            Next

            app.DisplayAlerts = False 'Microsoft Excelのアラート一旦無効化

            Dim excelChk As Boolean = _com.excelOutput(sOutFile)
            If excelChk = False Then
                Exit Sub
            End If
            book.SaveAs(sOutFile) '書き込み実行

            app.DisplayAlerts = True 'アラート無効化を解除
            app.Visible = True

            '_msgHd.dspMSG("CreateExcel")
            createFlg = True

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
        'Dim test As String = ds1.Tables(RS).Rows(0)("")

        'カーソルをビジー状態から元に戻す
        Cursor.Current = Cursors.Default

        If createFlg = True Then
            _msgHd.dspMSG("CreateExcel", frmC01F10_Login.loginValue.Language)
        End If
    End Sub

    '汎用マスタから固定キー、可変キーに応じた結果を返す
    'param1：String 固定キー
    'param2：String 可変キー
    'Return: DataSet
    Private Function getDsHanyoData(ByVal prmFixed As String, Optional ByVal prmVariable As String = "") As DataSet
        Return _com.getDsHanyoDataEx(prmFixed, prmVariable)
    End Function

    'param1：String テーブル名
    'param2：String 詳細条件
    'Return: DataSet
    Private Function getDsData(ByVal tableName As String, Optional ByRef txtParam As String = "") As DataSet
        Return _com.getDsData(tableName, txtParam)
    End Function

    Private Function getWarehouseName(ByVal prmString As String) As String
        Return _com.getWarehouseName(prmString)
    End Function

    'Return: DataTable
    Private Function getInOutKbn(Optional ByVal removeVal As String = "") As DataTable
        Dim Sql As String = ""
        Dim strViewText As String = ""
        Dim strArrayData As String() = removeVal.Split(","c)

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

        'リードタイムのリストを汎用マスタから取得
        Dim dsHanyo As DataSet = getDsData("m90_hanyo", Sql)

        'ComboBoxに表示する項目のリストを作成する
        Dim table As New DataTable("Table")
        table.Columns.Add("Display", GetType(String))
        table.Columns.Add("Value", GetType(String))

        strViewText = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "文字２", "文字１")

        For x As Integer = 0 To dsHanyo.Tables(RS).Rows.Count - 1
            table.Rows.Add(dsHanyo.Tables(RS).Rows(x)(strViewText), dsHanyo.Tables(RS).Rows(x)("可変キー"))
        Next

        Return table
    End Function

    Private Function getInOutName(ByVal prmString As String) As String
        Dim Sql As String = " AND 固定キー ILIKE '" & CommonConst.INOUT_CLASS & "'"

        'Dim ds As DataSet = _db.selectDB("m90_hanyo", Sql)

        Dim ds As DataSet = getDsHanyoData(CommonConst.INOUT_CLASS, prmString)

        'Dim table2 As New DataTable("Table")
        'table2.Columns.Add("Display", GetType(String))
        'table2.Columns.Add("Value", GetType(Integer))

        Dim displayTxt As String = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "文字２", "文字１")

        Return ds.Tables(RS).Rows(0)(displayTxt)
    End Function

    '引当区分　Return: DataTable
    Private Function getAssignKbn() As DataTable
        Dim Sql As String = ""
        Dim strViewText As String = ""

        'ComboBoxに表示する項目のリストを作成する
        Dim table As New DataTable("Table")
        table.Columns.Add("Display", GetType(String))
        table.Columns.Add("Value", GetType(Integer))

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            table.Rows.Add(CommonConst.AC_KBN_NORMAL_TXT_ENG, CommonConst.AC_KBN_NORMAL)
            table.Rows.Add(CommonConst.AC_KBN_ASSIGN_TXT_ENG, CommonConst.AC_KBN_ASSIGN)
        Else
            table.Rows.Add(CommonConst.AC_KBN_NORMAL_TXT, CommonConst.AC_KBN_NORMAL)
            table.Rows.Add(CommonConst.AC_KBN_ASSIGN_TXT, CommonConst.AC_KBN_ASSIGN)
        End If

        Return table
    End Function


    Private Function getAssignName(ByVal prmString As String) As String
        Dim reString As String = ""

        If prmString = CommonConst.AC_KBN_NORMAL Then
            reString = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                           CommonConst.AC_KBN_NORMAL_TXT_ENG,
                           CommonConst.AC_KBN_NORMAL_TXT)
        Else
            reString = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                           CommonConst.AC_KBN_ASSIGN_TXT_ENG,
                           CommonConst.AC_KBN_ASSIGN_TXT)
        End If

        Return reString

    End Function

    'DGV内で指定列名(プルダウン)は一度のクリックで開く
    'それ以外は一回で入力状態にする
    Private Sub DgvAdd_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DgvAdd.CellEnter

        If DgvAdd.Columns(e.ColumnIndex).Name = "倉庫" Or DgvAdd.Columns(e.ColumnIndex).Name = "入出庫種別" Or DgvAdd.Columns(e.ColumnIndex).Name = "引当区分" Then
            SendKeys.Send("{F4}")
            'Else
            '    SendKeys.Send("{F2}")
        End If

    End Sub

    Private Sub setCellValueChanged()

        AddHandler DgvAdd.CellValueChanged, AddressOf CellValueChanged
    End Sub
    Private Sub delCellValueChanged()

        RemoveHandler DgvAdd.CellValueChanged, AddressOf CellValueChanged
    End Sub

    '在庫マスタから現在庫数を取得
    '検索対象行番を引数にセット
    Private Function setZaikoQuantity1(ByVal rowIndex As Integer) As Long
        Dim Sql As String = ""
        Dim reccnt As Integer = 0 'DB用（デフォルト）

        Sql = "select"
        Sql += " sum(t45.出庫数量) as 在庫数量 "
        Sql += " from "
        Sql += " t44_shukohd t44 "
        Sql += " ,t45_shukodt t45 "
        Sql += " ,t70_inout t70 "
        Sql += " where "
        Sql += " t44.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "' "
        Sql += " and "
        Sql += " t44.受注番号 ILIKE '" & No & "' "
        'Sql += " and "
        'Sql += " t44.受注番号枝番 ILIKE '" & Suffix & "' "
        Sql += " and "
        Sql += " t44.出庫番号 ILIKE t45.出庫番号 "
        Sql += " and "
        Sql += " t45.受注行番号 = '" & DgvAdd.Rows(rowIndex).Cells("行番号").Value.ToString() & "'"
        Sql += " and "
        Sql += " t45.倉庫コード ILIKE '" & DgvAdd.Rows(rowIndex).Cells("倉庫").Value.ToString() & "'"
        Sql += " and "
        Sql += " t45.出庫区分 = '" & CommonConst.SHUKO_KBN_TMP & "' "
        Sql += " and "
        Sql += " t44.取消区分 =  " & CommonConst.CANCEL_KBN_ENABLED
        Sql += " and "
        Sql += " t45.出庫番号 ILIKE t70.伝票番号 "
        Sql += " and "
        Sql += " t45.行番号 = t70.行番号 "
        Sql += " and "
        Sql += " t70.入出庫種別 = '" & DgvAdd.Rows(rowIndex).Cells("入出庫種別").Value.ToString() & "' "

        '在庫マスタから現在庫数を取得
        Dim dsZaiko As DataSet = _db.selectDB(Sql, RS, reccnt)

        If dsZaiko.Tables(RS).Rows.Count > 0 Then
            If dsZaiko.Tables(RS).Rows(0)("在庫数量") IsNot DBNull.Value Then
                Return dsZaiko.Tables(RS).Rows(0)("在庫数量")
            Else
                Return 0
            End If
        End If

    End Function

    '在庫マスタから現在庫数を取得
    '検索対象行番を引数にセット(regist and load, ALLOC)
    Private Function setZaikoCurrency(ByVal rowIndex As Integer) As Long
        Dim Sql As String = ""
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim lngSuryo As Long = 0


        '通常の場合
        Sql = "SELECT sum(m21.現在庫数) as 在庫数量 "
        Sql += " from m21_zaiko m21 "

        Sql += " WHERE m21.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

        Sql += " AND  m21.メーカー = '" & DgvAdd.Rows(rowIndex).Cells("メーカー").Value.ToString & "'"
        Sql += " AND  m21.品名 = '" & DgvAdd.Rows(rowIndex).Cells("品名").Value.ToString & "'"
        Sql += " AND  m21.型式 = '" & DgvAdd.Rows(rowIndex).Cells("型式").Value.ToString & "'"
        Sql += " AND  m21.倉庫コード = '" & DgvAdd.Rows(rowIndex).Cells("倉庫").Value.ToString & "'"
        Sql += " AND  m21.入出庫種別 = '" & DgvAdd.Rows(rowIndex).Cells("入出庫種別").Value.ToString & "'"
        Sql += " AND  m21.無効フラグ = " & CommonConst.CANCEL_KBN_ENABLED
        Sql += " AND  m21.現在庫数 <> 0"

        '在庫マスタから現在庫数を取得
        Dim dsZaiko As DataSet = _db.selectDB(Sql, RS, reccnt)

        If dsZaiko.Tables(RS).Rows.Count > 0 Then
            If dsZaiko.Tables(RS).Rows(0)("在庫数量") IsNot DBNull.Value Then
                lngSuryo = dsZaiko.Tables(RS).Rows(0)("在庫数量")
            End If
        End If



        '引き当ての場合は仮出庫の分も加算する
        Sql = "SELECT t45.出庫数量"

        Sql += " from t45_shukodt t45 left join t44_shukohd t44"
        Sql += " on t45.出庫番号 = t44.出庫番号"

        Sql += " WHERE t45.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND  t45.受注番号 = '" & DgvAdd.Rows(rowIndex).Cells("受注番号").Value.ToString & "'"
        'Sql += " AND  t45.受注番号枝番 = '" & TxtSuffixNo.Text & "'"
        Sql += " AND  t45.受注行番号 = " & DgvAdd.Rows(rowIndex).Cells("行番号").Value.ToString
        Sql += " AND  t45.出庫区分 = '" & CommonConst.SHUKO_KBN_TMP & "'"  '仮出庫
        Sql += " AND  t44.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString
        Sql += " AND  t45.倉庫コード='" & DgvAdd.Rows(rowIndex).Cells("倉庫").Value.ToString & "'"

        '在庫マスタから現在庫数を取得
        Dim dsSyuko As DataSet = _db.selectDB(Sql, RS, reccnt)


        'データがなければ終了
        If dsSyuko.Tables(RS).Rows.Count = 0 Then
            Return lngSuryo
        End If


        For i As Integer = 0 To dsSyuko.Tables(RS).Rows.Count - 1

            '仮出庫の数量を足す
            lngSuryo += dsSyuko.Tables(RS).Rows(i)("出庫数量")
        Next


        dsSyuko.Dispose()


        Return lngSuryo

    End Function

    '仕入区分 2 の時の在庫数取得
    '在庫マスタから現在庫数一覧を取得
    Private Function getZaikoList(ByVal rowIndex As Integer) As DataSet

        Dim Sql As String = ""
        Dim reccnt As Integer = 0 'DB用（デフォルト）

        Sql = "SELECT sum(m21.現在庫数) as 現在庫数, m21.入出庫種別, m21.伝票番号, m21.行番号 "
        Sql += " from m21_zaiko m21" ', t70_inout t70 "

        Sql += " WHERE m21.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"

        Sql += " AND  m21.メーカー ILIKE '" & DgvAdd.Rows(rowIndex).Cells("メーカー").Value.ToString & "'"
        Sql += " AND  m21.品名 ILIKE '" & DgvAdd.Rows(rowIndex).Cells("品名").Value.ToString & "'"
        Sql += " AND  m21.型式 ILIKE '" & DgvAdd.Rows(rowIndex).Cells("型式").Value.ToString & "'"
        Sql += " AND  m21.倉庫コード ILIKE '" & DgvAdd.Rows(rowIndex).Cells("倉庫").Value.ToString & "'"
        Sql += " AND  m21.入出庫種別 ILIKE '" & DgvAdd.Rows(rowIndex).Cells("入出庫種別").Value.ToString & "'"
        Sql += " AND  m21.無効フラグ = " & CommonConst.CANCEL_KBN_ENABLED
        Sql += " AND  m21.現在庫数 <> 0"

        'Sql += " AND m21.会社コード ILIKE t70.会社コード "
        'Sql += " AND m21.倉庫コード ILIKE t70.倉庫コード "
        'Sql += " AND m21.伝票番号 ILIKE t70.伝票番号 "
        'Sql += " AND m21.行番号 = t70.行番号 "
        'Sql += " AND COALESCE(t70.仕入区分,'NL') = '" & CommonConst.Sire_KBN_Zaiko.ToString & "'"

        Sql += " GROUP BY m21.入出庫種別, m21.伝票番号, m21.行番号, m21.最終入庫日"
        Sql += " ORDER BY m21.最終入庫日 "

        '在庫マスタから現在庫数を取得
        Dim dsZaiko As DataSet = _db.selectDB(Sql, RS, reccnt)

        Return dsZaiko

    End Function

    '仕入区分 1 or 9 の時の在庫数取得
    '入庫リスト取得(-OR)
    Private Function setNukoList(ByVal rowIndex As Integer) As Long
        Dim Sql As String = ""
        Dim reccnt As Integer = 0 'DB用（デフォルト）

        '受付番号 → 発注番号 → 入庫番号で紐づけ、対象の入庫番号を取得する
        Sql = " SELECT t42.入庫番号 "
        Sql += " FROM t10_cymnhd t10  "

        Sql += " LEFT JOIN t20_hattyu t20 "
        Sql += " ON "
        Sql += " t10.会社コード = t20.会社コード "
        Sql += " AND "
        Sql += " t10.受注番号 = t20.受注番号 "
        'Sql += " AND "
        'Sql += " t10.受注番号枝番 = t20.受注番号枝番 "
        'Sql += " AND "
        'Sql += " t20.発注番号枝番 = (SELECT MAX(発注番号枝番) AS 発注番号枝番 FROM t20_hattyu) "

        Sql += " LEFT JOIN t42_nyukohd t42 "
        Sql += " ON "
        Sql += " t20.会社コード = t42.会社コード "
        Sql += " AND "
        Sql += " t20.発注番号 = t42.発注番号 "
        'Sql += " AND "
        'Sql += " t20.発注番号枝番 = t42.発注番号枝番 "
        Sql += " AND "
        Sql += " t20.仕入先コード = t42.仕入先コード "

        Sql += " LEFT JOIN t43_nyukodt t43 "
        Sql += " ON "
        Sql += " t42.会社コード = t43.会社コード "
        Sql += " AND "
        Sql += " t42.入庫番号 = t43.入庫番号 "

        Sql += " WHERE "
        Sql += " t10.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND "
        Sql += " t10.受注番号 = '" & DgvAdd.Rows(rowIndex).Cells("受注番号").Value.ToString & "'"
        'Sql += " AND "
        'Sql += " t10.受注番号枝番 = '" & TxtSuffixNo.Text & "'"

        Sql += " AND t43.メーカー ILIKE '" & DgvAdd.Rows(rowIndex).Cells("メーカー").Value.ToString & "'"
        Sql += " AND t43.品名 ILIKE '" & DgvAdd.Rows(rowIndex).Cells("品名").Value.ToString & "'"
        Sql += " AND t43.型式 ILIKE '" & DgvAdd.Rows(rowIndex).Cells("型式").Value.ToString & "'"
        Sql += " AND t42.倉庫コード ILIKE '" & DgvAdd.Rows(rowIndex).Cells("倉庫").Value.ToString & "'"
        'Sql += " AND t43.仕入区分 = '" & DgvAdd.Rows(rowIndex).Cells("仕入区分値").Value.ToString & "'"
        Sql += " AND t42.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString
        Sql += " AND t43.行番号 = " & DgvAdd.Rows(rowIndex).Cells("行番号").Value.ToString
        Sql += " AND t20.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString

        Sql += " order by t20.発注番号枝番 desc"


        Dim dsNyukoList As DataSet = _db.selectDB(Sql, RS, reccnt)

        '対象入庫情報があった場合
        If dsNyukoList.Tables(RS).Rows.Count > 0 Then
            Sql = "SELECT sum(現在庫数) as 在庫数量 from m21_zaiko"

            Sql += " WHERE "
            Sql += " 会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"

            Sql += " AND メーカー ILIKE '" & DgvAdd.Rows(rowIndex).Cells("メーカー").Value.ToString & "'"
            Sql += " AND 品名 ILIKE '" & DgvAdd.Rows(rowIndex).Cells("品名").Value.ToString & "'"
            Sql += " AND 型式 ILIKE '" & DgvAdd.Rows(rowIndex).Cells("型式").Value.ToString & "'"
            Sql += " AND 倉庫コード ILIKE '" & DgvAdd.Rows(rowIndex).Cells("倉庫").Value.ToString & "'"
            Sql += " AND 入出庫種別 ILIKE '" & DgvAdd.Rows(rowIndex).Cells("入出庫種別").Value.ToString & "'"
            Sql += " AND 無効フラグ = " & CommonConst.CANCEL_KBN_ENABLED
            Sql += " AND 現在庫数 <> 0"
            Sql += " AND ( "
            '取得した入庫番号一覧から現在庫数を取得
            For i As Integer = 0 To dsNyukoList.Tables(RS).Rows.Count - 1
                Sql += IIf(i > 0, " OR ", "")
                Sql += " 伝票番号 ILIKE '" & dsNyukoList.Tables(RS).Rows(i)("入庫番号") & "'"
            Next
            Sql += " ) "
            'Sql += " GROUP BY 倉庫コード, 入出庫種別, 最終入庫日 "
            Sql += " GROUP BY 倉庫コード, 入出庫種別 "
            'Sql += " ORDER BY 最終入庫日 "

            '在庫マスタから現在庫数を取得
            Dim dsZaiko As DataSet = _db.selectDB(Sql, RS, reccnt)

            If dsZaiko.Tables(RS).Rows.Count > 0 Then
                If dsZaiko.Tables(RS).Rows(0)("在庫数量").ToString IsNot "" Then
                    Return dsZaiko.Tables(RS).Rows(0)("在庫数量")
                Else
                    Return 0
                End If
            Else
                Return 0
            End If
        Else
            Return 0
        End If



    End Function

    '在庫マスタから現在庫数一覧を取得
    '仕入区分 2 の時(-OR, regist)
    Private Function getNukoList(ByVal rowIndex As Integer) As DataSet
        Dim Sql As String = ""
        Dim reccnt As Integer = 0 'DB用（デフォルト）

        Sql = " SELECT t42.入庫番号 "
        Sql += " FROM t43_nyukodt t43  "
        Sql += " LEFT JOIN t42_nyukohd t42 "
        Sql += " ON t43.入庫番号 = t42.入庫番号"
        'Sql += " and t43.発注番号 = t42.発注番号 and t43.発注番号枝番 = t42.発注番号枝番"
        Sql += " and t43.発注番号 = t42.発注番号"

        Sql += " LEFT JOIN t20_hattyu t20 "
        Sql += " on  t20.会社コード = t43.会社コード "
        'Sql += " and t20.発注番号 = t42.発注番号 and t20.発注番号枝番 = t42.発注番号枝番 "
        Sql += " and t20.発注番号 = t42.発注番号 "

        Sql += " WHERE "
        Sql += "     t42.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND t42.倉庫コード ILIKE '" & DgvAdd.Rows(rowIndex).Cells("倉庫").Value.ToString & "'"
        Sql += " AND t43.メーカー ILIKE '" & DgvAdd.Rows(rowIndex).Cells("メーカー").Value.ToString & "'"
        Sql += " AND t43.品名 ILIKE '" & DgvAdd.Rows(rowIndex).Cells("品名").Value.ToString & "'"
        Sql += " AND t43.型式 ILIKE '" & DgvAdd.Rows(rowIndex).Cells("型式").Value.ToString & "'"
        'Sql += " AND t43.仕入区分 = '" & DgvAdd.Rows(rowIndex).Cells("仕入区分値").Value.ToString & "'"
        Sql += " AND t42.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString
        Sql += " and t20.受注番号 = '" & DgvAdd.Rows(rowIndex).Cells("受注番号").Value.ToString & "'"
        'Sql += " and t20.受注番号枝番 = '" & TxtSuffixNo.Text & "'"

        'Sql = " SELECT t42.入庫番号 "
        'Sql += " FROM t10_cymnhd t10  "

        'Sql += " LEFT JOIN t20_hattyu t20 "
        'Sql += " ON "
        'Sql += " t10.会社コード = t20.会社コード "
        'Sql += " AND "
        'Sql += " t10.受注番号 = t20.受注番号 "
        'Sql += " AND "
        'Sql += " t10.受注番号枝番 = t20.受注番号枝番 "
        'Sql += " AND "
        'Sql += " t20.発注番号枝番 = (SELECT MAX(発注番号枝番) AS 発注番号枝番 FROM t20_hattyu) "

        'Sql += " LEFT JOIN t42_nyukohd t42 "
        'Sql += " ON "
        'Sql += " t20.会社コード = t42.会社コード "
        'Sql += " AND "
        'Sql += " t20.発注番号 = t42.発注番号 "
        'Sql += " AND "
        'Sql += " t20.発注番号枝番 = t42.発注番号枝番 "
        'Sql += " AND "
        'Sql += " t20.仕入先コード = t42.仕入先コード "

        'Sql += " LEFT JOIN t43_nyukodt t43 "
        'Sql += " ON "
        'Sql += " t42.会社コード = t43.会社コード "
        'Sql += " AND "
        'Sql += " t42.入庫番号 = t43.入庫番号 "

        'Sql += " WHERE "
        'Sql += " t10.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        'Sql += " AND "
        'Sql += " t10.受注番号 = '" & TxtOrderNo.Text & "'"
        'Sql += " AND "
        'Sql += " t10.受注番号枝番 = '" & TxtSuffixNo.Text & "'"

        'Sql += " AND t43.メーカー ILIKE '" & DgvAdd.Rows(rowIndex).Cells("メーカー").Value.ToString & "'"
        'Sql += " AND t43.品名 ILIKE '" & DgvAdd.Rows(rowIndex).Cells("品名").Value.ToString & "'"
        'Sql += " AND t43.型式 ILIKE '" & DgvAdd.Rows(rowIndex).Cells("型式").Value.ToString & "'"

        'Sql += " AND t42.倉庫コード ILIKE '" & DgvAdd.Rows(rowIndex).Cells("倉庫").Value.ToString & "'"

        'Sql += " AND t43.仕入区分 = '" & DgvAdd.Rows(rowIndex).Cells("仕入区分値").Value.ToString & "'"

        Dim dsNyukoList As DataSet = _db.selectDB(Sql, RS, reccnt)

        '取得した入庫番号一覧から現在庫数を取得

        Dim dsZaiko As DataSet = Nothing

        If dsNyukoList.Tables(RS).Rows.Count > 0 Then

            Sql = "SELECT sum(現在庫数) as 現在庫数, 入出庫種別, 伝票番号, 行番号 from m21_zaiko"

            Sql += " WHERE 会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"

            Sql += " AND メーカー ILIKE '" & DgvAdd.Rows(rowIndex).Cells("メーカー").Value.ToString & "'"
            Sql += " AND 品名 ILIKE '" & DgvAdd.Rows(rowIndex).Cells("品名").Value.ToString & "'"
            Sql += " AND 型式 ILIKE '" & DgvAdd.Rows(rowIndex).Cells("型式").Value.ToString & "'"
            Sql += " AND 倉庫コード ILIKE '" & DgvAdd.Rows(rowIndex).Cells("倉庫").Value.ToString & "'"
            Sql += " AND 入出庫種別 ILIKE '" & DgvAdd.Rows(rowIndex).Cells("入出庫種別").Value.ToString & "'"
            Sql += " AND 無効フラグ = " & CommonConst.CANCEL_KBN_ENABLED
            Sql += " AND 現在庫数 <> 0"

            Sql += " AND ( "
            For i As Integer = 0 To dsNyukoList.Tables(RS).Rows.Count - 1
                Sql += IIf(i > 0, " OR ", "")
                Sql += " 伝票番号 ILIKE '" & dsNyukoList.Tables(RS).Rows(i)("入庫番号") & "'"
            Next
            Sql += " ) "

            Sql += " GROUP BY 倉庫コード, 入出庫種別, 最終入庫日, 伝票番号, 行番号 "
            Sql += " ORDER BY 最終入庫日 "

            '在庫マスタから現在庫数を取得
            dsZaiko = _db.selectDB(Sql, RS, reccnt)

        End If

        Return dsZaiko

    End Function

    '在庫数取得
    Private Function getZikoQuantity(ByVal rowIndex As Integer) As Long

        Dim retZaikoQuantity As Long '在庫数セット用

        Select Case DgvAdd.Rows(rowIndex).Cells("仕入区分値").Value

            Case CommonConst.Sire_KBN_Zaiko '在庫
                '在庫マスタから在庫数を確認
                'retZaikoQuantity = setZaikoQuantity(rowIndex)
                retZaikoQuantity = setZaikoCurrency(rowIndex)
            Case CommonConst.Sire_KBN_Sire  '受発注
                retZaikoQuantity = setNukoList(rowIndex)
            Case CommonConst.Sire_KBN_DELIVERY  'Delivery
                retZaikoQuantity = setNukoList(rowIndex)
            Case CommonConst.Sire_KBN_OS  'Outsource
                retZaikoQuantity = setNukoList(rowIndex)
            Case Else 'サービス
                '入庫数から在庫数を確認
                retZaikoQuantity = setNukoList(rowIndex)
        End Select

        Return retZaikoQuantity

    End Function

    Private Sub DgvAdd_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DgvAdd.CellValueChanged

        Dim PurchaseTotal As Integer = 0



        'ヘッダー以外だったら
        If e.RowIndex > -1 Then

            '操作したカラム名を取得
            Dim currentColumn As String = DgvAdd.Columns(e.ColumnIndex).Name

            If currentColumn = "出庫数量" Then  'Cellが出庫数量の場合

                '各項目の属性チェック
                If Not IsNumeric(DgvAdd.Rows(e.RowIndex).Cells("出庫数量").Value) And (DgvAdd.Rows(e.RowIndex).Cells("出庫数量").Value IsNot Nothing) Then
                    _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
                    DgvAdd.Rows(e.RowIndex).Cells("出庫数量").Value = 0
                    Exit Sub
                End If

                Dim decTmp As Decimal = DgvAdd.Rows(e.RowIndex).Cells("出庫数量").Value
                DgvAdd.Rows(e.RowIndex).Cells("出庫数量").Value = decTmp
            End If
        End If

    End Sub

    Private Sub DeliveryNote_Zenbi()

        Dim SelectedRow As Integer = DgvHistory.CurrentCell.RowIndex

        Dim createFlg = False

        Dim lsno_ As String = DgvHistory.Rows(SelectedRow).Cells("出庫番号").Value

        '2019.12.19 受注番号基準に変更し、受注番号、出庫日、出庫区分を抽出条件とする
        Dim Sql1 As String = ""
        Sql1 += "SELECT HD.* FROM public.t44_shukohd HD "
        Sql1 += " WHERE HD.会社コード='" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql1 += " AND HD.出庫番号 = '" & lsno_ & "'"
        'Sql1 += " WHERE HD.受注番号 = '" & TxtOrderNo.Text & "'"
        'Sql1 += " AND HD.出庫日 = '" & CDate(DgvHistory.Rows(SelectedRow).Cells("出庫日").Value).ToString("yyyyMMdd") & "'"
        'Sql1 += " AND DT.出庫区分 = '1'"

        Dim reccnt As Integer = 0
        Dim ds1 As DataSet = _db.selectDB(Sql1, RS, reccnt)

        Dim Sql2 As String = ""
        Sql2 += "SELECT * FROM public.m10_customer HD "
        Sql2 += " WHERE HD.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql2 += " AND HD.得意先コード = '" & ds1.Tables(RS).Rows(0)("得意先コード") & "'"
        Dim ds2 As DataSet = _db.selectDB(Sql2, RS, reccnt)

        '定義
        'Dim app As Excel.Application = Nothing
        'Dim book As Excel.Workbook = Nothing
        'Dim sheet As Excel.Worksheet = Nothing

        'カーソルをビジー状態にする
        Cursor.Current = Cursors.WaitCursor

        Try
            '雛形パス
            Dim sHinaPath As String = ""
            sHinaPath = StartUp._iniVal.BaseXlsPath

            '雛形ファイル名
            Dim sHinaFileDeliv As String = ""
            sHinaFileDeliv = sHinaPath & "\" & "DeliveryNote.xlsx"

            'Dim sHinaFileReceipt As String = ""
            'sHinaFileReceipt = sHinaPath & "\" & "DeliveryNote.xlsx"

            '出力先パス
            Dim sOutPath As String = ""
            sOutPath = StartUp._iniVal.OutXlsPath

            '出力ファイル名
            '2019.12.19 受注番号+1行目出庫番号基準に変更
            Dim sOutFile As String = ""
            sOutFile = sOutPath & "\DeliveryNote_" & lsno_ & ".xlsx"
            'sOutFile = sOutPath & "\DeliveryNote_" & TxtOrderNo.Text & ds2.Tables(RS).Rows(0)("出庫番号") & ".xlsx"

            Dim xls As UtilExcelHandler = New UtilExcelHandler(sHinaFileDeliv)
            xls.open()
            xls.visible()

            'app = New Excel.Application()
            'book = app.Workbooks.Add(sHinaFileDeliv)  'テンプレート
            'sheet = CType(book.Worksheets(1), Excel.Worksheet)

            xls.setValue(": " & ds2.Tables(RS).Rows(0)("得意先名"), 8, 2)
            xls.setValue(": " & ds2.Tables(RS).Rows(0)("住所１"), 9, 2)
            xls.setValue("" & ds2.Tables(RS).Rows(0)("住所２") & " " & ds2.Tables(RS).Rows(0)("住所３") & " " & ds2.Tables(RS).Rows(0)("郵便番号"), 10, 2)
            xls.setValue(": " & ds2.Tables(RS).Rows(0)("電話番号"), 11, 2)
            xls.setValue(": " & lsno_, 8, 5)
            xls.setValue(": " & ds1.Tables(RS).Rows(0)("出庫日"), 9, 5)
            xls.setValue(": " & ds1.Tables(RS).Rows(0)("客先番号"), 10, 5)
            'sheet.Range("B8").Value = ":" & ds1.Tables(RS).Rows(0)("得意先名")
            'sheet.Range("B9").Value = ":" & ds1.Tables(RS).Rows(0)("得意先住所") & " " & ds1.Tables(RS).Rows(0)("得意先郵便番号")
            'sheet.Range("B11").Value = ":" & ds1.Tables(RS).Rows(0)("得意先電話番号")

            '2019.12.19 出庫番号ー＞受注番号表示に改訂
            'sheet.Range("E8").Value = ":" & lsno_
            'sheet.Range("E8").Value = ":" & TxtOrderNo.Text
            'sheet.Range("E9").Value = ":" & ds1.Tables(RS).Rows(0)("出庫日")
            'sheet.Range("E10").Value = ":" & ds1.Tables(RS).Rows(0)("客先番号")


            Dim rowCnt As Integer = 0
            Dim lstRow As Integer = 14
            Dim num As Integer = 1
            Dim lstlstRow As Integer = 30


            For j As Integer = 0 To DgvHistory.RowCount - 1
                If DgvHistory.Rows(j).Cells("出庫番号").Value = lsno_ Then
                    xls.setValue(num, lstRow, 1)
                    xls.setValue(DgvHistory.Rows(j).Cells("メーカー").Value, lstRow, 2)
                    xls.setValue(DgvHistory.Rows(j).Cells("出庫数量").Value, lstRow, 3)
                    xls.setValue(DgvHistory.Rows(j).Cells("単位").Value, lstRow, 4)
                    insert_row(xls, lstlstRow, lstRow)
                    'lstRow += 1
                    xls.setValue(DgvHistory.Rows(j).Cells("品名").Value, lstRow, 2)
                    insert_row(xls, lstlstRow, lstRow)
                    'lstRow += 1
                    xls.setValue(DgvHistory.Rows(j).Cells("型式").Value, lstRow, 2) 'Environment.NewLine & DgvHistory.Rows(j).Cells("備考").Value &
                    insert_row(xls, lstlstRow, lstRow)
                    'lstRow += 1
                    xls.setValue("JobOrderNo." & DgvHistory.Rows(j).Cells("受注番号").Value & "-" & DgvHistory.Rows(j).Cells("受注番号枝番").Value, lstRow, 2)
                    'xls.setValue("JobOrderNo." & ds1.Tables(RS).Rows(0)("受注番号") & "-" & ds1.Tables(RS).Rows(0)("受注番号枝番"), lstRow, 2)
                    'sheet.Range("C" & lstRow).Value = ds2.Tables(RS).Rows(j)("出庫数量")
                    'sheet.Range("D" & lstRow).Value = ds2.Tables(RS).Rows(j)("単位")
                    'sheet.Range("E" & lstRow).Value = ds2.Tables(RS).Rows(j)("備考")
                    'xls.setValue(DgvHistory.Rows(j).Cells("備考").Value, lstRow, 5)

                    num += 1
                    insert_row(xls, lstlstRow, lstRow)

                    'If lstlstRow - 1 <= lstRow Then
                    'xls.copyRow(lstRow + 1)
                    'xls.insertRow(lstRow + 1)
                    'xls.paste(lstRow + 1, 1)
                    'End If
                    'lstRow += 1
                End If
            Next

            'sheet.Cells.Rows.AutoFit()
            xls.autofit()

            'app.DisplayAlerts = False 'Microsoft Excelのアラート一旦無効化

            Dim excelChk As Boolean = _com.excelOutput(sOutFile)
            If excelChk = False Then
                Exit Sub
            End If
            'book.SaveAs(sOutFile) '書き込み実行
            xls.saveas(sOutFile)
            'xls.close(False)
            'xls = New UtilExcelHandler(sOutFile)
            'xls.open()
            'xls.close(False)
            'xls.display()

            'app.DisplayAlerts = True 'アラート無効化を解除

            'app.Visible = True

            '_msgHd.dspMSG("CreateExcel")
            createFlg = True
            'カーソルをビジー状態から元に戻す
            Cursor.Current = Cursors.Default


        Catch ex As Exception
            'カーソルをビジー状態から元に戻す
            Cursor.Current = Cursors.Default
            Throw ex
        Finally

        End Try

        If createFlg = True Then
            _msgHd.dspMSG("CreateExcel", frmC01F10_Login.loginValue.Language)
        End If
    End Sub

    Private Sub insert_row(ByRef xls As UtilExcelHandler, ByVal lstlstrow As Integer, ByRef lstrow As Integer)
        If lstlstrow - 1 <= lstrow Then
            xls.insertRow(lstrow + 1)
        End If
        lstrow += 1
    End Sub

    Private Sub Bulk_Regist_Click()

        'Dim dtToday1 As DateTime = DateTime.Now
        Dim dtToday2 As Date = New Date(2021, 1, 31)

        'Dim shukkoTime As TimeSpan = dtToday1.TimeOfDay
        Dim shukkoDate As String = UtilClass.formatDatetime(dtToday2)
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""
        Dim wh As String = "01" '"souko_A"
        Dim f As Boolean = False

        '
        Sql = " And "
        Sql += " x is null"
        Dim ds82 As DataSet = getDsData("t82_tmp_order", Sql)

        For y As Integer = 0 To ds82.Tables(RS).Rows.Count - 1
            f = False
            '受注基本
            Sql = " And "
            Sql += " 受注番号 = '" & ds82.Tables(RS).Rows(y)("order_no") & "'"
            'Sql += " AND "
            'Sql += "受注番号枝番 ILIKE '" & Suffix & "'"
            Sql += " AND "
            Sql += " 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED '取消区分=0
            Sql += " order by 受注番号枝番 desc"

            Dim dsCymnhd As DataSet = getDsData("t10_cymnhd", Sql)
            If dsCymnhd.Tables(RS).Rows.Count = 0 Then
                _db.executeDB("update t82_tmp_order set x='cancel AO' where order_no='" & ds82.Tables(RS).Rows(y)("order_no") & "';")
                Continue For
            End If

            '受注明細
            Sql = "SELECT t11.行番号, t11.出庫数, t11.未出庫数,t11.仕入区分,t11.メーカー,t11.品名,t11.型式,t11.仕入先名,t11.売単価,t11.単位,t11.備考 FROM "
            Sql += " t11_cymndt t11"

            Sql += " INNER JOIN "
            Sql += " t10_cymnhd t10"
            Sql += " ON "

            Sql += " t11.会社コード = t10.会社コード"
            Sql += " AND "
            Sql += " t11.受注番号 = t10.受注番号"
            Sql += " AND "
            Sql += " t11.受注番号枝番 = t10.受注番号枝番"

            Sql += " where t10.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND "
            Sql += "t10.受注番号 = '" & ds82.Tables(RS).Rows(y)("order_no") & "'"
            Sql += " AND "
            Sql += "t10.受注番号枝番 = '" & dsCymnhd.Tables(RS).Rows(0)("受注番号枝番") & "'"
            Sql += " AND "
            Sql += "t10.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED '取消区分=0

            '受注明細の「出庫数」更新に使用
            Dim dsCymndt As DataSet = _db.selectDB(Sql, RS, reccnt)

            '入出庫データ(t70_input)を作成する前に在庫があるか最終チェック
            'For i As Integer = 0 To dsCymndt.Tables(RS).Rows.Count() - 1
            'サービス以外だったら数量チェック
            'If dsCymndt.Tables(RS).Rows(i)("仕入区分") <> CommonConst.Sire_KBN_SERVICE.ToString Then
            'If dsCymndt.Tables(RS).Rows(i)("未出庫数") > _com.cymn_getNukoList4(ds82.Tables(RS).Rows(y)("order_no"), dsCymndt.Tables(RS).Rows(i)("行番号"), dsCymndt.Tables(RS).Rows(i)("仕入区分")) Then 'getZikoQuantity(i) Then
            '出庫数量が在庫数量を超えているメッセージを表示
            '_msgHd.dspMSG("chkShukoOverError", frmC01F10_Login.loginValue.Language)
            'Exit Sub
            'End If
            'End If
            'Next

            Try

                '出庫データ登録前に、「在庫引当」の商品があるかどうかチェック
                'あれば「仮出庫」データを取消更新する
                'For i As Integer = 0 To dsCymndt.Tables(RS).Rows.Count() - 1

                'Next

                '採番データを取得・更新
                '出庫登録データの伝票番号は基本的に LS で統一される（1商品で複数の在庫マスタをまたぐ場合を除く）
                Dim MAIN_LS As String = _com.getSaiban("70", dtToday2.ToShortDateString())

                '出庫登録一覧をループし、出庫登録対象データがあれば登録処理を実行
                For i As Integer = 0 To dsCymndt.Tables(RS).Rows.Count() - 1

                    '出庫数量があったら
                    If dsCymndt.Tables(RS).Rows(i)("未出庫数") > 0 Then

                        '該当する在庫データを取得・ループ
                        '対象の在庫がなくなるまでデータを作成する
                        Dim dsCurrentList As DataSet = Nothing
                        Dim totalShukkoVal As Decimal = (dsCymndt.Tables(RS).Rows(i)("未出庫数"))
                        Dim currentVal As Decimal = 0
                        Dim currentLS As String = ""

                        'If dsCymndt.Tables(RS).Rows(i)("仕入区分").Value.ToString = CommonConst.Sire_KBN_Sire.ToString Then
                        '仕入区分 = 受発注
                        '---------------------------------------
                        dsCurrentList = _com.cymn_getNukoList4(ds82.Tables(RS).Rows(y)("order_no"),
                                                               dsCymndt.Tables(RS).Rows(i)("メーカー"),
                                                               dsCymndt.Tables(RS).Rows(i)("品名").ToString,
                                                               dsCymndt.Tables(RS).Rows(i)("型式").ToString,
                                                               dsCymndt.Tables(RS).Rows(i)("仕入区分"))

                        'End If

                        If dsCurrentList IsNot Nothing Then
                            'データがあれば
                            If dsCurrentList.Tables(RS).Rows.Count > 0 Then
                                'Dim MAIN_LS As String = _com.getSaiban("70", dtToday2.ToShortDateString())
                                For x As Integer = 0 To dsCurrentList.Tables(RS).Rows.Count - 1

                                    If totalShukkoVal = 0 Then
                                        Exit For
                                    End If

                                    currentVal = Decimal.Parse(dsCurrentList.Tables(RS).Rows(x)("在庫数量"))

                                    '現在庫数より出庫数量の方が大きかった場合、現在庫数をそのまま出庫データとして作成
                                    If currentVal < totalShukkoVal Then
                                        _db.executeDB("update t82_tmp_order set x='no zaiko " & currentVal.ToString & "' where order_no='" & ds82.Tables(RS).Rows(y)("order_no") & "';")
                                        Exit For
                                        'totalShukkoVal -= currentVal 'currentValをそのまま登録し、全体数からcurrentValを減算する
                                    Else
                                        currentVal = totalShukkoVal '登録するのは残数分のみ
                                        totalShukkoVal -= currentVal
                                    End If

                                    '作成データが複数以上の場合、出庫番号を新規取得
                                    currentLS = IIf(x = 0, MAIN_LS, _com.getSaiban("70", dtToday2.ToShortDateString()))

                                    't45：出庫明細 新規作成
                                    '---------------------------------------
#Region "t45_shukodt"

                                    Sql = "INSERT INTO "
                                    Sql += "Public."
                                    Sql += "t45_shukodt("
                                    Sql += "会社コード, 出庫番号, 受注番号, 受注番号枝番, 行番号, 仕入区分"
                                    Sql += ", メーカー, 品名, 型式, 仕入先名, 売単価, 出庫数量, 単位, 備考"
                                    Sql += ", 更新者, 更新日, 出庫区分, 倉庫コード,受注行番号"
                                    Sql += " )VALUES('"
                                    Sql += dsCymnhd.Tables(RS).Rows(0)("会社コード").ToString '会社コード
                                    Sql += "', '"
                                    Sql += currentLS '出庫番号
                                    Sql += "', '"
                                    Sql += dsCymnhd.Tables(RS).Rows(0)("受注番号").ToString '受注番号
                                    Sql += "', '"
                                    Sql += dsCymnhd.Tables(RS).Rows(0)("受注番号枝番").ToString '受注番号枝番
                                    Sql += "', '"
                                    Sql += dsCymndt.Tables(RS).Rows(i)("行番号").ToString '行番号
                                    Sql += "', '"
                                    Sql += dsCymndt.Tables(RS).Rows(i)("仕入区分").ToString '仕入区分
                                    Sql += "', '"
                                    Sql += UtilClass.escapeSql(dsCymndt.Tables(RS).Rows(i)("メーカー").ToString) 'メーカー
                                    Sql += "', '"
                                    Sql += UtilClass.escapeSql(dsCymndt.Tables(RS).Rows(i)("品名").ToString) '品名
                                    Sql += "', '"
                                    Sql += UtilClass.escapeSql(dsCymndt.Tables(RS).Rows(i)("型式").ToString) '型式
                                    Sql += "', '"
                                    Sql += dsCymndt.Tables(RS).Rows(i)("仕入先名").ToString '仕入先名
                                    Sql += "', '"
                                    Sql += UtilClass.formatNumber(dsCymndt.Tables(RS).Rows(i)("売単価").ToString) '売単価
                                    Sql += "', '"
                                    Sql += currentVal.ToString '出庫数量
                                    Sql += "', '"
                                    Sql += dsCymndt.Tables(RS).Rows(i)("単位").ToString '単位
                                    Sql += "', '"
                                    Sql += dsCymndt.Tables(RS).Rows(i)("備考").ToString '備考
                                    Sql += "', '"
                                    Sql += Input '更新者
                                    Sql += "', '"
                                    Sql += UtilClass.formatDatetime(dtToday2) '更新日
                                    Sql += "', '"
                                    Sql += CommonConst.SHUKO_KBN_NORMAL '出庫区分（通常出庫時は 1 ）
                                    Sql += "', '"
                                    Sql += wh '倉庫コード
                                    Sql += "', '"
                                    Sql += dsCymndt.Tables(RS).Rows(i)("行番号").ToString
                                    Sql += "')"

                                    _db.executeDB(Sql)
                                    f = True
#End Region


                                    '受注明細の「未出庫数」「出庫数」を更新
                                    '受注明細更新　受注と出庫の行番号は一致する
                                    '---------------------------------------
                                    Dim calShukko As Decimal = dsCymndt.Tables(RS).Rows(i)("出庫数") + currentVal
                                    Dim calUnShukko As Decimal = dsCymndt.Tables(RS).Rows(i)("未出庫数") - currentVal
                                    If calUnShukko < 0 Then
                                        _db.executeDB("update t82_tmp_order set x='error unshkko minus' where order_no='" & ds82.Tables(RS).Rows(y)("order_no") & "';")
                                    End If

#Region "t11_cymndt"

                                    Sql = "update t11_cymndt set "
                                    Sql += "出庫数 = '" & calShukko & "'"
                                    Sql += ",未出庫数 = '" & calUnShukko & "'"
                                    Sql += ",更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
                                    Sql += " where 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                                    Sql += " AND "
                                    Sql += "受注番号 = '" & dsCymnhd.Tables(RS).Rows(0)("受注番号").ToString & "'"
                                    Sql += " AND "
                                    Sql += "受注番号枝番 = '" & dsCymnhd.Tables(RS).Rows(0)("受注番号枝番") & "'"
                                    Sql += " AND "
                                    Sql += "行番号 = '" & dsCymndt.Tables(RS).Rows(i)("行番号").ToString & "'"

                                    _db.executeDB(Sql)

#End Region

#Region "t70_inout"

                                    't70_inout にデータ登録
                                    Sql = "INSERT INTO "
                                    Sql += "Public."
                                    Sql += "t70_inout("
                                    Sql += "会社コード, 入出庫区分, 倉庫コード, 伝票番号, 行番号, 入出庫種別"
                                    Sql += ", メーカー, 品名, 型式, 数量, 単位, 備考, 入出庫日"
                                    'Sql += ", 引当区分, メーカー, 品名, 型式, 数量, 単位, 備考, 入出庫日"
                                    ''Sql += ", 取消区分, 更新者, 更新日, ロケ番号, 仕入区分"                     '2020.01.09 DEL
                                    Sql += ", 取消区分, 更新者, 更新日, 出庫開始サイン, 仕入区分"                 '2020.01.09 ADD
                                    Sql += " )VALUES('"
                                    Sql += frmC01F10_Login.loginValue.BumonCD '会社コード
                                    Sql += "', '"
                                    Sql += "2" '入出庫区分 1.入庫, 2.出庫
                                    Sql += "', '"
                                    Sql += wh '倉庫コード
                                    Sql += "', '"
                                    Sql += currentLS '伝票番号
                                    'Sql += dsCurrentList.Tables(RS).Rows(x)("伝票番号").ToString '伝票番号
                                    Sql += "', '"
                                    Sql += dsCymndt.Tables(RS).Rows(i)("行番号").ToString '行番号
                                    'Sql += dsCurrentList.Tables(RS).Rows(x)("行番号").ToString '行番号
                                    Sql += "', '"
                                    Sql += "0" '入出庫種別
                                    'Sql += "', '"
                                    'Sql += CommonConst.AC_KBN_NORMAL.ToString '引当区分(0：通常）
                                    Sql += "', '"
                                    Sql += UtilClass.escapeSql(dsCymndt.Tables(RS).Rows(i)("メーカー").ToString) 'メーカー
                                    Sql += "', '"
                                    Sql += UtilClass.escapeSql(dsCymndt.Tables(RS).Rows(i)("品名").ToString) '品名
                                    Sql += "', '"
                                    Sql += UtilClass.escapeSql(dsCymndt.Tables(RS).Rows(i)("型式").ToString) '型式
                                    Sql += "', '"
                                    Sql += currentVal.ToString '数量
                                    Sql += "', '"
                                    Sql += dsCymndt.Tables(RS).Rows(i)("単位").ToString '単位
                                    Sql += "', '"
                                    Sql += UtilClass.escapeSql(dsCymndt.Tables(RS).Rows(i)("備考").ToString) '備考
                                    Sql += "', '"
                                    'Sql += UtilClass.formatDatetime(DtpGoodsIssueDate.Text) '入出庫日
                                    Sql += shukkoDate
                                    Sql += "', '"
                                    Sql += CommonConst.CANCEL_KBN_ENABLED.ToString '取消区分
                                    Sql += "', '"
                                    Sql += Input '更新者
                                    Sql += "', '"
                                    Sql += UtilClass.formatDatetime(dtToday2) '更新日
                                    Sql += "', '"
                                    'Sql += dsCurrentList.Tables(RS).Rows(x)("伝票番号") & DgvAdd.Rows(i).Cells("行番号").Value.ToString
                                    Sql += dsCurrentList.Tables(RS).Rows(x)("伝票番号") & dsCurrentList.Tables(RS).Rows(x)("行番号")
                                    'Sql += currentLS & "1"
                                    Sql += "', '"
                                    Sql += dsCymndt.Tables(RS).Rows(i)("仕入区分").ToString '仕入区分

                                    Sql += "')"

                                    _db.executeDB(Sql)

#End Region


                                    '作成データが複数の場合にヘッダーも作成
                                    If MAIN_LS <> currentLS Then

#Region "t44_shukohd"

                                        Sql = "INSERT INTO "
                                        Sql += "Public."
                                        Sql += "t44_shukohd("
                                        Sql += "会社コード, 出庫番号, 見積番号, 見積番号枝番, 受注番号, 受注番号枝番, 客先番号"
                                        Sql += ", 得意先コード, 得意先名, 得意先郵便番号, 得意先住所, 得意先電話番号, 得意先ＦＡＸ"
                                        Sql += ", 得意先担当者役職, 得意先担当者名, 営業担当者, 入力担当者, 備考, 取消日, 取消区分"
                                        Sql += ", 出庫日, 登録日, 更新日, 更新者, 営業担当者コード, 入力担当者コード)"
                                        Sql += " VALUES('"
                                        Sql += dsCymnhd.Tables(RS).Rows(0)("会社コード").ToString '会社コード
                                        Sql += "', '"
                                        Sql += currentLS '出庫番号
                                        Sql += "', '"
                                        Sql += dsCymnhd.Tables(RS).Rows(0)("見積番号").ToString '見積番号
                                        Sql += "', '"
                                        Sql += dsCymnhd.Tables(RS).Rows(0)("見積番号枝番").ToString '見積番号枝番
                                        Sql += "', '"
                                        Sql += dsCymnhd.Tables(RS).Rows(0)("受注番号").ToString '受注番号
                                        Sql += "', '"
                                        Sql += dsCymnhd.Tables(RS).Rows(0)("受注番号枝番").ToString '受注番号枝番
                                        Sql += "', '"
                                        Sql += dsCymnhd.Tables(RS).Rows(0)("客先番号").ToString '客先番号
                                        Sql += "', '"
                                        Sql += dsCymnhd.Tables(RS).Rows(0)("得意先コード").ToString '得意先コード
                                        Sql += "', '"
                                        Sql += dsCymnhd.Tables(RS).Rows(0)("得意先名").ToString '得意先名
                                        Sql += "', '"
                                        Sql += dsCymnhd.Tables(RS).Rows(0)("得意先郵便番号").ToString '得意先郵便番号
                                        Sql += "', '"
                                        Sql += dsCymnhd.Tables(RS).Rows(0)("得意先住所").ToString '得意先住所
                                        Sql += "', '"
                                        Sql += dsCymnhd.Tables(RS).Rows(0)("得意先電話番号").ToString '得意先電話番号
                                        Sql += "', '"
                                        Sql += dsCymnhd.Tables(RS).Rows(0)("得意先ＦＡＸ").ToString '得意先ＦＡＸ
                                        Sql += "', '"
                                        Sql += dsCymnhd.Tables(RS).Rows(0)("得意先担当者役職").ToString '得意先担当者役職
                                        Sql += "', '"
                                        Sql += dsCymnhd.Tables(RS).Rows(0)("得意先担当者名").ToString '得意先担当者名
                                        Sql += "', '"
                                        Sql += dsCymnhd.Tables(RS).Rows(0)("営業担当者").ToString '営業担当者
                                        Sql += "', '"
                                        Sql += dsCymnhd.Tables(RS).Rows(0)("入力担当者").ToString '入力担当者
                                        Sql += "', '"
                                        Sql += UtilClass.escapeSql("AUTO") 'dsCymnhd.Tables(RS).Rows(0)("備考").ToString '備考
                                        Sql += "', "
                                        Sql += "null" '取消日
                                        Sql += ", '"
                                        Sql += CommonConst.CANCEL_KBN_ENABLED.ToString '取消区分
                                        Sql += "', '"
                                        Sql += shukkoDate '出庫日
                                        Sql += "', '"
                                        Sql += UtilClass.formatDatetime(dtToday2) '登録日
                                        Sql += "', '"
                                        Sql += UtilClass.formatDatetime(dtToday2) '更新日
                                        Sql += "', '"
                                        Sql += Input '更新者
                                        Sql += "', '"
                                        Sql += dsCymnhd.Tables(RS).Rows(0)("営業担当者コード").ToString '営業担当者
                                        Sql += "', '"
                                        Sql += frmC01F10_Login.loginValue.TantoCD '入力担当者
                                        Sql += "')"

                                        _db.executeDB(Sql)

#End Region

                                    End If

                                Next

                            Else

                            End If

                        Else
                            _db.executeDB("update t82_tmp_order set x='no nyuko' where order_no='" & ds82.Tables(RS).Rows(y)("order_no") & "';")
                        End If

                    Else
                        _db.executeDB("update t82_tmp_order set x='delivered' where order_no='" & ds82.Tables(RS).Rows(y)("order_no") & "';")
                    End If

                Next


#Region "t44_shukohd"

                If f Then
                    Sql = "INSERT INTO "
                    Sql += "Public."
                    Sql += "t44_shukohd("
                    Sql += "会社コード, 出庫番号, 見積番号, 見積番号枝番, 受注番号, 受注番号枝番, 客先番号"
                    Sql += ", 得意先コード, 得意先名, 得意先郵便番号, 得意先住所, 得意先電話番号, 得意先ＦＡＸ"
                    Sql += ", 得意先担当者役職, 得意先担当者名, 営業担当者, 入力担当者, 備考, 取消日, 取消区分"
                    Sql += ", 出庫日, 登録日, 更新日, 更新者, 営業担当者コード, 入力担当者コード)"
                    Sql += " VALUES('"
                    Sql += dsCymnhd.Tables(RS).Rows(0)("会社コード").ToString '会社コード
                    Sql += "', '"
                    Sql += MAIN_LS '出庫番号
                    Sql += "', '"
                    Sql += dsCymnhd.Tables(RS).Rows(0)("見積番号").ToString '見積番号
                    Sql += "', '"
                    Sql += dsCymnhd.Tables(RS).Rows(0)("見積番号枝番").ToString '見積番号枝番
                    Sql += "', '"
                    Sql += dsCymnhd.Tables(RS).Rows(0)("受注番号").ToString '受注番号
                    Sql += "', '"
                    Sql += dsCymnhd.Tables(RS).Rows(0)("受注番号枝番").ToString '受注番号枝番
                    Sql += "', '"
                    Sql += dsCymnhd.Tables(RS).Rows(0)("客先番号").ToString '客先番号
                    Sql += "', '"
                    Sql += dsCymnhd.Tables(RS).Rows(0)("得意先コード").ToString '得意先コード
                    Sql += "', '"
                    Sql += dsCymnhd.Tables(RS).Rows(0)("得意先名").ToString '得意先名
                    Sql += "', '"
                    Sql += dsCymnhd.Tables(RS).Rows(0)("得意先郵便番号").ToString '得意先郵便番号
                    Sql += "', '"
                    Sql += dsCymnhd.Tables(RS).Rows(0)("得意先住所").ToString '得意先住所
                    Sql += "', '"
                    Sql += dsCymnhd.Tables(RS).Rows(0)("得意先電話番号").ToString '得意先電話番号
                    Sql += "', '"
                    Sql += dsCymnhd.Tables(RS).Rows(0)("得意先ＦＡＸ").ToString '得意先ＦＡＸ
                    Sql += "', '"
                    Sql += dsCymnhd.Tables(RS).Rows(0)("得意先担当者役職").ToString '得意先担当者役職
                    Sql += "', '"
                    Sql += dsCymnhd.Tables(RS).Rows(0)("得意先担当者名").ToString '得意先担当者名
                    Sql += "', '"
                    Sql += dsCymnhd.Tables(RS).Rows(0)("営業担当者").ToString '営業担当者
                    Sql += "', '"
                    Sql += dsCymnhd.Tables(RS).Rows(0)("入力担当者").ToString '入力担当者
                    Sql += "', '"
                    Sql += UtilClass.escapeSql("AUTO") 'dsCymnhd.Tables(RS).Rows(0)("備考").ToString '備考
                    Sql += "', "
                    Sql += "null" '取消日
                    Sql += ", '"
                    Sql += CommonConst.CANCEL_KBN_ENABLED.ToString '取消区分
                    Sql += "', '"
                    Sql += shukkoDate '出庫日
                    Sql += "', '"
                    Sql += UtilClass.formatDatetime(dtToday2) '登録日
                    Sql += "', '"
                    Sql += UtilClass.formatDatetime(dtToday2) '更新日
                    Sql += "', '"
                    Sql += Input '更新者
                    Sql += "', '"
                    Sql += dsCymnhd.Tables(RS).Rows(0)("営業担当者コード").ToString '営業担当者
                    Sql += "', '"
                    Sql += frmC01F10_Login.loginValue.TantoCD '入力担当者
                    Sql += "')"

                    _db.executeDB(Sql)

                    _db.executeDB("update t82_tmp_order set x='ok' where order_no='" & ds82.Tables(RS).Rows(y)("order_no") & "';")

                End If
#End Region


            Catch ue As UsrDefException
                ue.dspMsg()
                Throw ue
            Catch ex As Exception
                'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
            End Try


            'm90_hanyo  SimpleRegistrationの可変キーが1の場合は仕入処理も行う
            'Sql = "  AND 固定キー = 'SR'"
            'Sql += " AND 可変キー = '1'"

            'Dim dsHanyo As DataTable = getDsData("m90_hanyo", Sql).Tables(0)

            'If dsHanyo.Rows.Count = 0 Then  'データなしの場合は終了
            'Exit Sub
            'End If


            '売上処理
            'Dim blnFlg As Boolean = mUpdate_Uriage()
            'If blnFlg = False Then
            'Exit Sub
            'End If



            '登録完了メッセージ
            '_msgHd.dspMSG("completeInsert", frmC01F10_Login.loginValue.Language)


            'dsHanyo = Nothing
            'Me.Dispose()

        Next

        _msgHd.dspMSG("completeInsert", frmC01F10_Login.loginValue.Language)


    End Sub

    Private Sub BtnX_Click(sender As Object, e As EventArgs) Handles BtnX.Click
        Call Bulk_Regist_Click()
    End Sub
End Class