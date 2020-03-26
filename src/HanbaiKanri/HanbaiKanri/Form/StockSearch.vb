'2020.01.09 ロケ番号→出庫開始サインに名称変更
'2020.03.26 在庫管理区分、在庫表示区分を反映

Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class StockSearch
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
    Private _langHd As UtilLangHandler
    Private _parentForm As Form
    'Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ

    Private manufactuer As String
    Private itemName As String
    Private spec As String

    Private _mode As String
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
                   ByRef prmManufacturer As String,
                   ByRef prmItemName As String,
                   ByRef prmSpec As String,
                   ByRef prmMode As String)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _parentForm = prmRefForm
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True

        manufactuer = prmManufacturer
        itemName = prmItemName
        spec = prmSpec

        _mode = prmMode

    End Sub

    '画面表示時
    Private Sub MstHanyoue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            DgvList.Columns("引当").HeaderText = "Reservation"
            DgvList.Columns("メーカー").HeaderText = "Manufacturer"
            DgvList.Columns("品名").HeaderText = "ItemName"
            DgvList.Columns("型式").HeaderText = "Spec"
            DgvList.Columns("倉庫").HeaderText = "Warehouse"
            DgvList.Columns("最終入庫日").HeaderText = "LastReceiptDate"
            DgvList.Columns("入出庫種別").HeaderText = "StorageType"
            DgvList.Columns("現在庫数").HeaderText = "CurrentQuontity"
            DgvList.Columns("入庫単価").HeaderText = "GoodsReceiptPrice"
            DgvList.Columns("最終出庫日").HeaderText = "LastGoodsDeliveryDate"
            DgvList.Columns("ロケ番号").HeaderText = "Location"
            DgvList.Columns("製造番号").HeaderText = "SerialNo"
            DgvList.Columns("伝票番号").HeaderText = "OrderNo"

            BtnSelect.Text = "Select"
            BtnBack.Text = "Back"

            'LblDescription.Text = "：Allocation not possible"
        End If

        Try


#Region "m21_zaiko"

            '在庫マスタから対象データを取得
            '
            '会社コード = ログイン情報
            '無効フラグ = 0
            '入出庫種別 <= 1
            Sql = " SELECT "
            'Sql += " m21.会社コード, m21.倉庫コード, m21.最終入庫日, m21.入出庫種別, SUM(m21.現在庫数) as 現在庫数"
            'Sql += " , SUM(m21.入庫単価) as 入庫単価, m21.最終出庫日, m20.名称, m90.文字１, m90.文字２, t43.仕入区分 "
            Sql += " m21.会社コード, m21.倉庫コード, m21.最終入庫日, m21.入出庫種別, m21.現在庫数"
            Sql += " , m21.入庫単価, m21.最終出庫日, m20.名称, m90.文字１, m90.文字２, t43.仕入区分 "
            Sql += " , m21.メーカー, m21.品名, m21.型式 "
            Sql += " , m21.ロケ番号 "
            Sql += " , m21.製造番号 "
            Sql += " , m21.伝票番号, m21.行番号 "
            ''Sql += " , m21.ロケ番号, t43.入庫番号, t43.行番号 as 入庫行番号 "         '2020.01.09 DEL
            Sql += " , m21.出庫開始サイン, t43.入庫番号, t43.行番号 as 入庫行番号 "     '2020.01.09 ADD
            Sql += " , null as 出庫番号, '0' as 出庫区分"

            Sql += " FROM m21_zaiko m21 "

            Sql += " LEFT JOIN "
            Sql += " m20_warehouse m20 "
            Sql += " ON m21.会社コード = m20.会社コード "
            Sql += " AND m21.倉庫コード = m20.倉庫コード "

            Sql += " LEFT JOIN "
            Sql += " m90_hanyo m90 "
            Sql += " On m21.会社コード = m90.会社コード "
            Sql += " AND m90.固定キー = '" & CommonConst.INOUT_CLASS & "' "
            Sql += " AND m21.入出庫種別 = m90.可変キー "

            Sql += " LEFT JOIN "
            Sql += " t43_nyukodt t43 "
            Sql += " On m21.会社コード = t43.会社コード "
            'Sql += " AND m21.伝票番号 = t43.入庫番号 "
            'Sql += " AND m21.行番号 = t43.行番号 "

            Sql += " AND ( "
            Sql += " ( m21.伝票番号 = t43.入庫番号 "
            Sql += " AND m21.行番号 = t43.行番号 ) "
            Sql += " OR "
            ''Sql += " ( m21.ロケ番号 = concat(t43.入庫番号, t43.行番号) ) "           '2020.01.09 DEL
            Sql += " ( m21.出庫開始サイン = concat(t43.入庫番号, t43.行番号) ) "       '2020.01.09 ADD
            Sql += "  ) "

            Sql += " WHERE "

            Sql += " m21.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND m21.無効フラグ = " & CommonConst.CANCEL_KBN_ENABLED
            Sql += " AND m21.入出庫種別 <= '" & CommonConst.INOUT_KBN_SAMPLE & "'"

            ''2020.03.26 抽出条件を＝からILIKEとする（イコールでは検索条件が厳しすぎる）
            ''Sql += " AND m21.メーカー = '" & manufactuer & "'"
            ''Sql += " AND m21.品名 = '" & itemName & "'"
            ''Sql += " AND m21.型式 = '" & spec & "'"
            Sql += " AND m21.メーカー ILIKE '%" & manufactuer & "%'"
            Sql += " AND m21.品名 ILIKE '%" & itemName & "%'"
            Sql += " AND m21.型式 ILIKE '%" & spec & "%'"

            Sql += " AND m21.現在庫数 <> 0 "

            If _mode <> "Normal" Then
                Sql += " AND ( t43.仕入区分 = '" & CommonConst.Sire_KBN_Zaiko & "'"
                Sql += " OR t43.仕入区分 = '" & CommonConst.Sire_KBN_Move & "' ) "
            End If


            'Sql += " GROUP BY m21.会社コード, m21.倉庫コード, m21.最終入庫日, m21.最終出庫日 "
            'Sql += " , m21.入出庫種別, m20.名称,m90.文字１, m90.文字２, t43.仕入区分 "
            '20191117 M.Kuji このSelectだと下記Group By は不要と思われます。
            'Sql += " GROUP BY m21.会社コード, m21.倉庫コード, m21.最終入庫日, m21.入出庫種別, m21.現在庫数"
            'Sql += " , m21.入庫単価, m21.最終出庫日, m20.名称, m90.文字１, m90.文字２, t43.仕入区分 "
            'Sql += " , m21.伝票番号, m21.行番号, t43.入庫番号 "
            'Sql += " , 入庫行番号, m21.出庫開始サイン "            '2020.01.09 REP
#End Region


#Region "union t21_hattyu"

            Sql += " union all"


            Sql += " SELECT "
            Sql += " t21.会社コード, t20.倉庫コード"
            Sql += " , null as 最終入庫日"
            Sql += " , null as 入出庫種別"
            Sql += " , t21.発注残数 as 現在庫数"
            Sql += " , t21.仕入値 as 入庫単価"
            Sql += " , null as 最終出庫日"
            Sql += " , m20.名称"
            Sql += " , '入庫予定' as 文字１"
            Sql += " , 'Scheduled goods receipt' as 文字２"
            Sql += " , t21.仕入区分 "

            Sql += " , t21.メーカー, t21.品名, t21.型式 "
            Sql += " , null as ロケ番号 "
            Sql += " , null as 製造番号 "
            Sql += " , null as 伝票番号"
            Sql += " , null as 行番号 "
            ''Sql += " , null as ロケ番号"              '2020.01.09 ADD
            Sql += " , null as 出庫開始サイン"          '2020.01.09 ADD
            Sql += " , null as 入庫番号"
            Sql += " , null as 入庫行番号 "
            Sql += " , null as 出庫番号,'0' as 出庫区分"


            Sql += " FROM t21_hattyu t21 left join t20_hattyu t20"
            Sql += " on t21.発注番号 = t20.発注番号 and t21.発注番号枝番 = t20.発注番号枝番"
            Sql += " left join m20_warehouse m20 "
            Sql += " ON t20.会社コード = m20.会社コード AND t20.倉庫コード = m20.倉庫コード "


            Sql += " WHERE "
            Sql += " t21.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND t20.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

            ''2020.03.26 抽出条件を＝からILIKEとする（イコールでは検索条件が厳しすぎる）
            ''Sql += " AND t21.メーカー = '" & manufactuer & "'"
            ''Sql += " AND t21.品名 = '" & itemName & "'"
            ''Sql += " AND t21.型式 = '" & spec & "'"
            Sql += " AND t21.メーカー ILIKE '%" & manufactuer & "%'"
            Sql += " AND t21.品名 ILIKE '%" & itemName & "%'"
            Sql += " AND t21.型式 ILIKE '%" & spec & "%'"

            Sql += " AND t21.発注残数 <> 0 "
            Sql += " AND (t21.仕入区分 = '" & CommonConst.Sire_KBN_Sire & "' or t21.仕入区分 = '" & CommonConst.Sire_KBN_Zaiko & "')"
#End Region


#Region "union t45_shukodt"

            Sql += " union all"

            Sql += " SELECT "
            Sql += " t45.会社コード, t45.倉庫コード"
            Sql += " , null as 最終入庫日"
            Sql += " , null as 入出庫種別"
            Sql += " , t45.出庫数量 as 現在庫数"
            Sql += " , t11.仕入値 as 入庫単価"
            Sql += " , null as 最終出庫日"
            Sql += " , m20.名称"
            Sql += " , '引当' as 文字１"
            Sql += " , 'Number of provisions' as 文字２"
            Sql += " , t11.仕入区分 "

            Sql += " , t11.メーカー, t11.品名, t11.型式 "
            Sql += " , null as ロケ番号 "
            Sql += " , null as 製造番号 "
            Sql += " , null as 伝票番号"
            Sql += " , t45.行番号 "
            ''Sql += " , null as ロケ番号"          '2020.01.09 DEL
            Sql += " , null as 出庫開始サイン"      '2020.01.09 ADD
            Sql += " , null as 入庫番号"
            Sql += " , null as 入庫行番号 "
            Sql += " , t45.出庫番号,t45.出庫区分"

            Sql += " FROM t45_shukodt t45 left join t44_shukohd t44"
            Sql += " on t45.出庫番号 = t44.出庫番号"

            Sql += " left join t11_cymndt t11"
            Sql += " on t45.受注番号 = t11.受注番号 and t45.受注番号枝番  = t11.受注番号枝番"
            Sql += " left join m20_warehouse m20 "
            Sql += " ON t45.会社コード = m20.会社コード AND t45.倉庫コード = m20.倉庫コード "

            Sql += " WHERE "
            Sql += " t45.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND t44.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

            ''2020.03.26 抽出条件を＝からILIKEとする（イコールでは検索条件が厳しすぎる）
            ''Sql += " AND t45.メーカー = '" & manufactuer & "'"
            ''Sql += " AND t45.品名 = '" & itemName & "'"
            ''Sql += " AND t45.型式 = '" & spec & "'"
            Sql += " AND t45.メーカー ILIKE '%" & manufactuer & "%'"
            Sql += " AND t45.品名 ILIKE '%" & itemName & "%'"
            Sql += " AND t45.型式 ILIKE '%" & spec & "%'"

            Sql += " AND t45.出庫数量 <> 0 "
            Sql += " AND t45.出庫区分 = '" & CommonConst.SHUKO_KBN_TMP & "'"

#End Region


            Dim dsZaiko As DataSet = _db.selectDB(Sql, RS, reccnt)

            For i As Integer = 0 To dsZaiko.Tables(RS).Rows.Count - 1 '在庫データ
                DgvList.Rows.Add()

                '入庫データの仕入区分が2以外、または入庫データに紐づいていない在庫は引当不可として背景をLightGrayに変更
                If dsZaiko.Tables(RS).Rows(i)("仕入区分") IsNot DBNull.Value Then
                    If dsZaiko.Tables(RS).Rows(i)("仕入区分") <> CommonConst.Sire_KBN_Zaiko And
                        dsZaiko.Tables(RS).Rows(i)("仕入区分") <> CommonConst.Sire_KBN_Move Then
                        'DgvList.Rows(i).DefaultCellStyle.BackColor = Color.LightGray
                        DgvList.Rows(i).Cells("引当").Value = "×"
                    End If
                Else
                    DgvList.Rows(i).Cells("引当").Value = "×"
                End If

                DgvList.Rows(i).Cells("倉庫コード").Value = dsZaiko.Tables(RS).Rows(i)("倉庫コード") '移動入力でも使用
                DgvList.Rows(i).Cells("倉庫").Value = dsZaiko.Tables(RS).Rows(i)("名称") '移動入力でも使用
                DgvList.Rows(i).Cells("最終入庫日").Value = dsZaiko.Tables(RS).Rows(i)("最終入庫日")
                DgvList.Rows(i).Cells("入出庫種別区分").Value = dsZaiko.Tables(RS).Rows(i)("入出庫種別") '移動入力でも使用
                DgvList.Rows(i).Cells("入出庫種別").Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                                                           dsZaiko.Tables(RS).Rows(i)("文字２"),
                                                           dsZaiko.Tables(RS).Rows(i)("文字１")) '移動入力でも使用

                Dim intSuryo As Integer = dsZaiko.Tables(RS).Rows(i)("現在庫数")
                If dsZaiko.Tables(RS).Rows(i)("出庫区分") = CommonConst.SHUKO_KBN_TMP Then  '在庫引当の場合
                    Dim strSyuko As String = Convert.ToString(dsZaiko.Tables(RS).Rows(i)("出庫番号"))
                    Dim intGyo As String = dsZaiko.Tables(RS).Rows(i)("行番号")

                    '引当数を対象の入庫番号を特定し足しこむ
                    Dim blnHikiate As Boolean = mCheckHikiate(strSyuko, intGyo, intSuryo)
                End If

                DgvList.Rows(i).Cells("現在庫数").Value = dsZaiko.Tables(RS).Rows(i)("現在庫数") '移動入力でも使用

                DgvList.Rows(i).Cells("入庫単価").Value = dsZaiko.Tables(RS).Rows(i)("入庫単価") '移動入力でも使用
                DgvList.Rows(i).Cells("最終出庫日").Value = dsZaiko.Tables(RS).Rows(i)("最終出庫日")
                DgvList.Rows(i).Cells("伝票番号").Value = dsZaiko.Tables(RS).Rows(i)("伝票番号") '移動入力でも使用
                DgvList.Rows(i).Cells("行番号").Value = dsZaiko.Tables(RS).Rows(i)("行番号") '移動入力でも使用
                ''DgvList.Rows(i).Cells("ロケ番号").Value = dsZaiko.Tables(RS).Rows(i)("ロケ番号") '移動入力でも使用                '2020.01.09 DEL
                DgvList.Rows(i).Cells("出庫開始サイン").Value = dsZaiko.Tables(RS).Rows(i)("出庫開始サイン") '移動入力でも使用      '2020.01.09 ADD
                DgvList.Rows(i).Cells("入庫番号").Value = dsZaiko.Tables(RS).Rows(i)("入庫番号") '移動入力でも使用
                DgvList.Rows(i).Cells("入庫行番号").Value = dsZaiko.Tables(RS).Rows(i)("入庫行番号") '移動入力でも使用

                DgvList.Rows(i).Cells("メーカー").Value = dsZaiko.Tables(RS).Rows(i)("メーカー") '移動入力でも使用
                DgvList.Rows(i).Cells("品名").Value = dsZaiko.Tables(RS).Rows(i)("品名") '移動入力でも使用
                DgvList.Rows(i).Cells("型式").Value = dsZaiko.Tables(RS).Rows(i)("型式") '移動入力でも使用

                DgvList.Rows(i).Cells("ロケ番号").Value = dsZaiko.Tables(RS).Rows(i)("ロケ番号") '移動入力でも使用
                DgvList.Rows(i).Cells("製造番号").Value = dsZaiko.Tables(RS).Rows(i)("製造番号") '移動入力でも使用
            Next

            '移動入力時のみ「選択」ボタンを表示
            If _mode = "Normal" Then
                BtnSelect.Visible = False
            Else
                '移動入力時のみ「引当」列非表示
                DgvList.Columns("引当").Visible = False
            End If

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
            If "89ABCDEFOPQRSTUV".Contains(InventoryViewer) Then
                DgvList.Columns("製造番号").Visible = True
            Else
                DgvList.Columns("製造番号").Visible = False
            End If
            ''If "GHIJKLMNOPQRSTUV".Contains(InventoryViewer) Then
            ''    DgvList.Columns("伝票番号").Visible = True
            ''Else
            ''    DgvList.Columns("伝票番号").Visible = False
            ''End If


            DgvList.Columns("引当").DisplayIndex = 0
            DgvList.Columns("メーカー").DisplayIndex = 1
            DgvList.Columns("品名").DisplayIndex = 2
            DgvList.Columns("型式").DisplayIndex = 3
            DgvList.Columns("倉庫").DisplayIndex = 4
            DgvList.Columns("入出庫種別").DisplayIndex = 5
            DgvList.Columns("ロケ番号").DisplayIndex = 6
            DgvList.Columns("製造番号").DisplayIndex = 7
            DgvList.Columns("伝票番号").DisplayIndex = 8


            Select Case InventoryControl
                Case "1", "3", "7", "F", "V"
                    DgvList.Columns("倉庫").DisplayIndex = 1
                    DgvList.Columns("メーカー").DisplayIndex = 2
                    DgvList.Columns("品名").DisplayIndex = 3
                    DgvList.Columns("型式").DisplayIndex = 4
                    DgvList.Columns("入出庫種別").DisplayIndex = 5
                    DgvList.Columns("ロケ番号").DisplayIndex = 6
                    DgvList.Columns("製造番号").DisplayIndex = 7
                    DgvList.Columns("伝票番号").DisplayIndex = 8
            End Select
            Select Case InventoryControl
                Case "3", "7", "F", "V"
                    DgvList.Columns("入出庫種別").DisplayIndex = 1
                    DgvList.Columns("メーカー").DisplayIndex = 2
                    DgvList.Columns("品名").DisplayIndex = 3
                    DgvList.Columns("型式").DisplayIndex = 4
                    DgvList.Columns("倉庫").DisplayIndex = 5
                    DgvList.Columns("ロケ番号").DisplayIndex = 6
                    DgvList.Columns("製造番号").DisplayIndex = 7
                    DgvList.Columns("伝票番号").DisplayIndex = 8
            End Select
            Select Case InventoryControl
                Case "7", "F", "V"
                    DgvList.Columns("ロケ番号").DisplayIndex = 1
                    DgvList.Columns("メーカー").DisplayIndex = 2
                    DgvList.Columns("品名").DisplayIndex = 3
                    DgvList.Columns("型式").DisplayIndex = 4
                    DgvList.Columns("倉庫").DisplayIndex = 5
                    DgvList.Columns("入出庫種別").DisplayIndex = 6
                    DgvList.Columns("製造番号").DisplayIndex = 7
                    DgvList.Columns("伝票番号").DisplayIndex = 8
            End Select
            Select Case InventoryControl
                Case "F", "V"
                    DgvList.Columns("製造番号").DisplayIndex = 1
                    DgvList.Columns("メーカー").DisplayIndex = 2
                    DgvList.Columns("品名").DisplayIndex = 3
                    DgvList.Columns("型式").DisplayIndex = 4
                    DgvList.Columns("倉庫").DisplayIndex = 5
                    DgvList.Columns("入出庫種別").DisplayIndex = 6
                    DgvList.Columns("ロケ番号").DisplayIndex = 7
                    DgvList.Columns("伝票番号").DisplayIndex = 8
            End Select
            Select Case InventoryControl
                Case "V"
                    DgvList.Columns("伝票番号").DisplayIndex = 1
                    DgvList.Columns("メーカー").DisplayIndex = 2
                    DgvList.Columns("品名").DisplayIndex = 3
                    DgvList.Columns("型式").DisplayIndex = 4
                    DgvList.Columns("倉庫").DisplayIndex = 5
                    DgvList.Columns("入出庫種別").DisplayIndex = 6
                    DgvList.Columns("ロケ番号").DisplayIndex = 7
                    DgvList.Columns("製造番号").DisplayIndex = 8
            End Select

            DgvList.Columns("最終入庫日").DisplayIndex = 9

            DgvList.Columns("現在庫数").DisplayIndex = 10
            DgvList.Columns("入庫単価").DisplayIndex = 11
            DgvList.Columns("最終出庫日").DisplayIndex = 12
            DgvList.Columns("伝票番号").DisplayIndex = 13
            DgvList.Columns("行番号").DisplayIndex = 14
            DgvList.Columns("出庫開始サイン").DisplayIndex = 15
            DgvList.Columns("入庫番号").DisplayIndex = 16
            DgvList.Columns("入庫行番号").DisplayIndex = 17


        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Sub


    Private Function mCheckHikiate(ByVal strSyuko As String, ByVal intGyo As Integer, ByVal intSuryo As Integer) As Boolean

        Dim reccnt As Integer = 0 'DB用（デフォルト）

        'inoutを出庫番号で検索
        ''Dim SQL As String = "select ロケ番号"             '2020.01.09 DEL
        Dim SQL As String = "select 出庫開始サイン"         '2020.01.09 ADD

        SQL += " from t70_inout t70"

        SQL += " where "
        SQL += "     t70.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        SQL += " AND t70.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

        SQL += " AND t70.伝票番号 = '" & strSyuko & "'"
        SQL += " AND t70.行番号 = " & intGyo

        Dim dsZaiko As DataSet = _db.selectDB(SQL, RS, reccnt)

        If dsZaiko.Tables(0).Rows.Count = 0 Then
            Exit Function
        End If


        '表示中の一覧から対象の在庫を特定する
        ''Dim strLoca As String = Mid(Convert.ToString(dsZaiko.Tables(0).Rows(0)("ロケ番号")), 1, 10)           '2020.01.09 DEL
        ''Dim strLocaGyo As String = Mid(Convert.ToString(dsZaiko.Tables(0).Rows(0)("ロケ番号")), 11)           '2020.01.09 DEL
        Dim strLoca As String = Mid(Convert.ToString(dsZaiko.Tables(0).Rows(0)("出庫開始サイン")), 1, 10)       '2020.01.09 ADD
        Dim strLocaGyo As String = Mid(Convert.ToString(dsZaiko.Tables(0).Rows(0)("出庫開始サイン")), 11)       '2020.01.09 ADD   

        For i As Integer = 0 To DgvList.Rows.Count - 1  '一覧

            Dim strDen As String = Convert.ToString(DgvList.Rows(i).Cells("伝票番号").Value)
            Dim strDenGyo As String = Convert.ToString(DgvList.Rows(i).Cells("行番号").Value)

            If strDen = vbNullString Then
            Else
                'inoutのロケ番と画面の伝票番号が一致した場合は現在個数に引当数を足す
                If strDen = strLoca And strDenGyo = Convert.ToInt32(strLocaGyo) Then
                    DgvList.Rows(i).Cells("現在庫数").Value += intSuryo
                End If
            End If

        Next


        mCheckHikiate = True

    End Function


    '戻るボタン押下時
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        'Dim QuoteRequest As Quote
        'QuoteRequest = New Quote(_msgHd, _db, _langHd, Me)
        'QuoteRequest.ShowDialog()
        _parentForm.Enabled = True

        Me.Close()
        Me.Dispose()

    End Sub

    '選択ボタン押下時
    Private Sub BtnSelect_Click(sender As Object, e As EventArgs) Handles BtnSelect.Click

        If DgvList.RowCount = 0 Then
            '対象データがないアラートを表示
            _msgHd.dspMSG("NonData", frmC01F10_Login.loginValue.Language)
            Exit Sub
        End If

        '移動入力画面
        Dim frm As MovementInput = CType(Me.Owner, MovementInput)

        Dim rowIndex As Integer = DgvList.CurrentCell.RowIndex

        frm.TxtWarehouseSince.Tag = DgvList.Rows(rowIndex).Cells("倉庫コード").Value.ToString
        frm.TxtWarehouseSince.Text = DgvList.Rows(rowIndex).Cells("倉庫").Value.ToString
        frm.TxtStorageTypeSince.Tag = DgvList.Rows(rowIndex).Cells("入出庫種別区分").Value.ToString
        frm.TxtStorageTypeSince.Text = DgvList.Rows(rowIndex).Cells("入出庫種別").Value.ToString
        frm.TxtQuantityFrom.Text = DgvList.Rows(rowIndex).Cells("現在庫数").Value.ToString
        frm.TxtUnitPrice.Text = DgvList.Rows(rowIndex).Cells("入庫単価").Value.ToString
        frm.TxtGoodsReceiptDate.Text = DgvList.Rows(rowIndex).Cells("最終入庫日").Value.ToString

        frm.TxtManufacturer.Text = DgvList.Rows(rowIndex).Cells("メーカー").Value.ToString
        frm.TxtItemName.Text = DgvList.Rows(rowIndex).Cells("品名").Value.ToString
        frm.TxtSpec.Text = DgvList.Rows(rowIndex).Cells("型式").Value.ToString


        '2020.03.23 一旦コメントアウトする
        ''frm.TxtDenpyoNo.Text = DgvList.Rows(rowIndex).Cells("伝票番号").Value.ToString
        ''frm.TxtLineNumber.Text = DgvList.Rows(rowIndex).Cells("行番号").Value.ToString
        ''frm.TxtDenpyoNo.Tag = DgvList.Rows(rowIndex).Cells("入庫番号").Value.ToString
        ''frm.TxtLineNumber.Tag = DgvList.Rows(rowIndex).Cells("入庫行番号").Value.ToString
        ''''frm.TxtLocationNo.Text = DgvList.Rows(rowIndex).Cells("ロケ番号").Value.ToString          '2020.01.09 DEL
        ''frm.TxtLocationNo.Text = DgvList.Rows(rowIndex).Cells("出庫開始サイン").Value.ToString      '2020.01.09 ADD
        'frm.TxtUnit.Text = DgvList.Rows(rowIndex).Cells("単位").Value

        frm.TxtQuantityTo.Text = 0

        frm.setMovingDestination() 'セット内容に応じて移動先のデフォルト値を書き換え

        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()

    End Sub
End Class