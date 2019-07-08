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

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            DgvList.Columns("引当").HeaderText = "Reservation"
            DgvList.Columns("倉庫").HeaderText = "Warehouse"
            DgvList.Columns("最終入庫日").HeaderText = "LastReceiptDate"
            DgvList.Columns("入出庫種別").HeaderText = "StorageType"
            DgvList.Columns("現在庫数").HeaderText = "CurrentQuontity"
            DgvList.Columns("入庫単価").HeaderText = "GoodsReceiptPrice"
            DgvList.Columns("最終出庫日").HeaderText = "LastGoodsDeliveryDate"

            BtnSelect.Text = "Select"
            BtnBack.Text = "Back"

            'LblDescription.Text = "：Allocation not possible"
        End If

        Try
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
            Sql += " , m21.伝票番号, m21.行番号 "
            Sql += " , m21.ロケ番号, t43.入庫番号, t43.行番号 as 入庫行番号 "
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
            Sql += " ( m21.ロケ番号 = concat(t43.入庫番号, t43.行番号) ) "
            Sql += "  ) "

            Sql += " WHERE "

            Sql += " m21.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND m21.無効フラグ = " & CommonConst.CANCEL_KBN_ENABLED
            Sql += " AND m21.入出庫種別 <= '" & CommonConst.INOUT_KBN_SAMPLE & "'"

            Sql += " AND m21.メーカー ILIKE '" & manufactuer & "'"
            Sql += " AND m21.品名 ILIKE '" & itemName & "'"
            Sql += " AND m21.型式 ILIKE '" & spec & "'"

            Sql += " AND m21.現在庫数 <> 0 "

            If _mode <> "Normal" Then
                Sql += " AND ( t43.仕入区分 = '" & CommonConst.Sire_KBN_Zaiko & "'"
                Sql += " OR t43.仕入区分 = '" & CommonConst.Sire_KBN_Move & "' ) "
            End If

            'Sql += " GROUP BY m21.会社コード, m21.倉庫コード, m21.最終入庫日, m21.最終出庫日 "
            'Sql += " , m21.入出庫種別, m20.名称,m90.文字１, m90.文字２, t43.仕入区分 "
            Sql += " GROUP BY m21.会社コード, m21.倉庫コード, m21.最終入庫日, m21.入出庫種別, m21.現在庫数"
            Sql += " , m21.入庫単価, m21.最終出庫日, m20.名称, m90.文字１, m90.文字２, t43.仕入区分 "
            Sql += " , m21.伝票番号, m21.行番号, t43.入庫番号 "
            Sql += " , 入庫行番号, m21.ロケ番号 "

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
                DgvList.Rows(i).Cells("現在庫数").Value = dsZaiko.Tables(RS).Rows(i)("現在庫数") '移動入力でも使用
                DgvList.Rows(i).Cells("入庫単価").Value = dsZaiko.Tables(RS).Rows(i)("入庫単価") '移動入力でも使用
                DgvList.Rows(i).Cells("最終出庫日").Value = dsZaiko.Tables(RS).Rows(i)("最終出庫日")
                DgvList.Rows(i).Cells("伝票番号").Value = dsZaiko.Tables(RS).Rows(i)("伝票番号") '移動入力でも使用
                DgvList.Rows(i).Cells("行番号").Value = dsZaiko.Tables(RS).Rows(i)("行番号") '移動入力でも使用
                DgvList.Rows(i).Cells("ロケ番号").Value = dsZaiko.Tables(RS).Rows(i)("ロケ番号") '移動入力でも使用
                DgvList.Rows(i).Cells("入庫番号").Value = dsZaiko.Tables(RS).Rows(i)("入庫番号") '移動入力でも使用
                DgvList.Rows(i).Cells("入庫行番号").Value = dsZaiko.Tables(RS).Rows(i)("入庫行番号") '移動入力でも使用

            Next

            '移動入力時のみ「選択」ボタンを表示
            If _mode = "Normal" Then
                BtnSelect.Visible = False
            Else
                '移動入力時のみ「引当」列非表示
                DgvList.Columns("引当").Visible = False
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Sub

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
        frm.TxtDenpyoNo.Text = DgvList.Rows(rowIndex).Cells("伝票番号").Value.ToString
        frm.TxtLineNumber.Text = DgvList.Rows(rowIndex).Cells("行番号").Value.ToString
        frm.TxtDenpyoNo.Tag = DgvList.Rows(rowIndex).Cells("入庫番号").Value.ToString
        frm.TxtLineNumber.Tag = DgvList.Rows(rowIndex).Cells("入庫行番号").Value.ToString
        frm.TxtLocationNo.Text = DgvList.Rows(rowIndex).Cells("ロケ番号").Value.ToString
        'frm.TxtUnit.Text = DgvList.Rows(rowIndex).Cells("単位").Value

        frm.TxtQuantityTo.Text = 0

        frm.setMovingDestination() 'セット内容に応じて移動先のデフォルト値を書き換え

        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()

    End Sub
End Class