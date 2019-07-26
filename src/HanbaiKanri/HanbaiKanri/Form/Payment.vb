Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports System.Globalization

Public Class Payment
    Inherits System.Windows.Forms.Form

    '------------------------------------------------------------------------------------------------------
    'メンバー定数宣言
    '------------------------------------------------------------------------------------------------------
    'PG制御文字 
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
    Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ

    Private CompanyCode As String = ""
    Private SupplierCode As String = ""
    Private SupplierName As String = ""
    Private CurCode As Integer = 0
    Private Suffix As String = ""
    Private _parentForm As Form
    Private _status As String = ""
    Private KikeAmount As Decimal = 0
    Private Balance As Decimal = 0

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
                   ByRef prmRefCompany As String,
                   ByRef prmRefSupplier As String,
                   ByRef prmRefName As String,
                   ByRef prmRefCurCode As Integer,
                   Optional ByRef prmRefStatus As String = "")
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _parentForm = prmRefForm
        CompanyCode = prmRefCompany
        SupplierCode = prmRefSupplier
        SupplierName = prmRefName
        CurCode = prmRefCurCode
        _status = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        DtpDepositDate.Value = Date.Now
        _init = True

    End Sub

    Private Sub Payment_Load5(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Sql As String = ""

        Sql = " AND "
        Sql += "固定キー ILIKE '" & CommonConst.FIXED_KEY_PAYMENT_TYPE & "'"
        Sql += " ORDER BY 表示順 ASC "

        '汎用マスタから入金種目一覧を取得、プルダウンを作成
        Dim dsHanyo As DataSet = getDsData("m90_hanyo", Sql)

        Dim table As New DataTable("Table")
        table.Columns.Add("Display", GetType(String))
        table.Columns.Add("Value", GetType(Integer))

        For i As Integer = 0 To dsHanyo.Tables(RS).Rows.Count - 1
            'table.Rows.Add(dsHanyo.Tables(RS).Rows(i)("文字１"), dsHanyo.Tables(RS).Rows(i)("可変キー"))
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                table.Rows.Add(dsHanyo.Tables(RS).Rows(i)("文字２"), dsHanyo.Tables(RS).Rows(i)("可変キー"))
            Else
                table.Rows.Add(dsHanyo.Tables(RS).Rows(i)("文字１"), dsHanyo.Tables(RS).Rows(i)("可変キー"))
            End If
        Next

        '支払入力エリアに反映
        Dim column As New DataGridViewComboBoxColumn()
        column.DataSource = table
        column.ValueMember = "Value"
        column.DisplayMember = "Display"
        column.HeaderText = "支払種目"
        column.Name = "支払種目"

        DgvPayment.Columns.Insert(1, column)

        '明細を描画
        setData()

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblHistory.Text = "PaymentHistory"
            LblPayment.Text = "PaymentInput"
            LblAPInfo.Text = "AccountsPayableInfo"
            LblDepositDate.Text = "AccountsPayableDate"
            LblDepositDate.Size = New Size(157, 22)
            DtpDepositDate.Location = New Point(351, 335)
            LblRemarks.Text = "Remarks"
            LblRemarks.Location = New Point(505, 335)
            LblRemarks.Size = New Size(111, 22)
            TxtRemarks.Location = New Point(622, 335)
            LblNo1.Text = "Record"
            LblNo1.Location = New Point(1272, 65)
            LblNo1.Size = New Size(66, 22)
            LblNo2.Text = "Record"
            LblNo2.Location = New Point(1272, 198)
            LblNo2.Size = New Size(66, 22)
            LblNo3.Text = "Record"
            LblNo3.Location = New Point(1272, 335)
            LblNo3.Size = New Size(66, 22)
            LblMode.Text = "PaymentInputMode"
            TxtHistoryCount.Location = New Point(1240, 65)
            TxtPaymentCount.Location = New Point(1240, 198)
            TxtKikeCount.Location = New Point(1240, 335)
            TxtRemarks.Size = New Size(520, 22)
            BtnAdd.Text = "Add"
            BtnDelete.Text = "Delete"
            BtnCal.Text = "Distribute"
            BtnRegist.Text = "Registration"
            BtnBack.Text = "Back"

            DgvSupplier.Columns("支払先").HeaderText = "SupplierName"
            DgvSupplier.Columns("買掛残高").HeaderText = "AccountsPayableBalance"


            DgvHistory.Columns("支払済支払先").HeaderText = "SupplierName"
            DgvHistory.Columns("支払番号").HeaderText = "PaymentNumber"
            DgvHistory.Columns("支払日").HeaderText = "PaymentDate"
            DgvHistory.Columns("支払種目").HeaderText = "PaymentType"
            DgvHistory.Columns("支払済支払金額計").HeaderText = "TotalPaymentAmount"
            DgvHistory.Columns("備考").HeaderText = "Remarks"

            DgvPayment.Columns("行番号").HeaderText = "LineNumber"
            DgvPayment.Columns("支払種目").HeaderText = "PaymentType"
            DgvPayment.Columns("入力支払金額").HeaderText = "PaymentAmount"

            DgvKikeInfo.Columns("買掛情報買掛番号").HeaderText = "AccountsPayableNumber"
            DgvKikeInfo.Columns("買掛日").HeaderText = "AccountsPayableDate"
            DgvKikeInfo.Columns("買掛金額").HeaderText = "AccountsPayableAmount"
            DgvKikeInfo.Columns("買掛情報支払金額計").HeaderText = "TotalPaymentAmount"
            DgvKikeInfo.Columns("買掛情報買掛残高").HeaderText = "AccountsPayableBalance"
            DgvKikeInfo.Columns("支払金額").HeaderText = "PaymentAmount"

        End If


        '数字形式
        DgvSupplier.Columns("買掛残高").DefaultCellStyle.Format = "N0"

        DgvHistory.Columns("支払済支払金額計").DefaultCellStyle.Format = "N0"

        DgvPayment.Columns("入力支払金額").DefaultCellStyle.Format = "N0"

        DgvKikeInfo.Columns("買掛金額").DefaultCellStyle.Format = "N0"
        DgvKikeInfo.Columns("買掛情報支払金額計").DefaultCellStyle.Format = "N0"
        DgvKikeInfo.Columns("買掛情報買掛残高").DefaultCellStyle.Format = "N0"
        DgvKikeInfo.Columns("支払金額").DefaultCellStyle.Format = "N0"

    End Sub

    '各Table内の作成
    Private Sub setData()

        setDgvSupplier() '支払先情報の出力

        setDgvHistory() '支払済みデータの出力

        setDgvPayment() '支払入力エリアの作成

        setDgvKikeInfo() '買掛情報の出力

    End Sub

    '支払先情報
    Private Sub setDgvSupplier()
        Dim Sql As String = ""
        Dim AccountsReceivable As Integer
        Dim curds As DataSet  'm25_currency
        Dim cur As String

        Sql = " AND "
        Sql += "仕入先コード"
        Sql += " ILIKE "
        Sql += "'%"
        Sql += SupplierCode
        Sql += "%'"
        Sql += " AND "
        Sql += "取消区分 = 0"

        If CurCode <> 0 Then
            Sql += " AND 通貨 = " & CurCode
        End If

        '仕入先と一致する発注基本を取得
        Dim dsSkyuhd As DataSet = getDsData("t46_kikehd", Sql)

        '買掛残高を集計
        AccountsReceivable = IIf(
            dsSkyuhd.Tables(RS).Compute("SUM(買掛残高_外貨)", Nothing) IsNot DBNull.Value,
            dsSkyuhd.Tables(RS).Compute("SUM(買掛残高_外貨)", Nothing),
            0
        )

        '支払先情報の出力
        DgvSupplier.Rows.Add()
        DgvSupplier.Rows(0).Cells("支払先").Value = SupplierName
        DgvSupplier.Rows(0).Cells("買掛残高").Value = AccountsReceivable


        '通貨の表示
        If IsDBNull(dsSkyuhd.Tables(RS).Rows(0)("通貨")) Then
            cur = vbNullString
        Else
            Sql = " and 採番キー = " & dsSkyuhd.Tables(RS).Rows(0)("通貨")
            curds = getDsData("m25_currency", Sql)

            cur = curds.Tables(RS).Rows(0)("通貨コード")
        End If
        TxtIDRCurrency.Text = cur

    End Sub


    '支払済みデータ
    Private Sub setDgvHistory()
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        'joinするのでとりあえず直書き
        Sql = "SELECT"
        Sql += " t48.支払先名, t48.支払番号, t48.支払日, t48.支払種別名, t48.支払金額_外貨, t47.備考"
        Sql += " FROM "
        Sql += " public.t48_shridt t48 "

        Sql += " INNER JOIN "
        Sql += " t47_shrihd t47"
        Sql += " ON "

        Sql += " t48.会社コード = t47.会社コード"
        Sql += " AND "
        Sql += " t48.支払番号 = t47.支払番号"

        Sql += " WHERE "
        Sql += " t48.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND "
        Sql += "t48.支払先コード"
        Sql += " ILIKE "
        Sql += "'%"
        Sql += SupplierCode
        Sql += "%'"
        Sql += " AND "
        Sql += "t47.取消区分 = 0"

        If CurCode <> 0 Then
            Sql += " AND t48.通貨 = " & CurCode
        End If


        '支払先と一致する支払明細を取得
        Dim dsShridt As DataSet = _db.selectDB(Sql, RS, reccnt)

        '明細行の件数をセット
        TxtHistoryCount.Text = dsShridt.Tables(RS).Rows.Count()

        '入金済みデータの出力
        For index As Integer = 0 To dsShridt.Tables(RS).Rows.Count - 1

            Dim getHanyo As DataSet = getDsHanyoData(CommonConst.FIXED_KEY_PAYMENT_TYPE, dsShridt.Tables(RS).Rows(index)("支払種別名"))

            DgvHistory.Rows.Add()

            DgvHistory.Rows(index).Cells("No").Value = index + 1
            DgvHistory.Rows(index).Cells("支払済支払先").Value = dsShridt.Tables(RS).Rows(index)("支払先名")
            DgvHistory.Rows(index).Cells("支払番号").Value = dsShridt.Tables(RS).Rows(index)("支払番号")
            DgvHistory.Rows(index).Cells("支払日").Value = dsShridt.Tables(RS).Rows(index)("支払日").ToShortDateString()
            DgvHistory.Rows(index).Cells("支払種目").Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                                                               getHanyo.Tables(RS).Rows(0)("文字２"),
                                                               getHanyo.Tables(RS).Rows(0)("文字１"))
            DgvHistory.Rows(index).Cells("支払済支払金額計").Value = dsShridt.Tables(RS).Rows(index)("支払金額_外貨")
            DgvHistory.Rows(index).Cells("備考").Value = dsShridt.Tables(RS).Rows(index)("備考")
        Next

    End Sub

    '支払入力
    Private Sub setDgvPayment()
        '明細行の件数をセット
        TxtPaymentCount.Text = DgvPayment.Rows.Count()
    End Sub

    '請求情報の出力
    Private Sub setDgvKikeInfo()
        Dim Sql As String = ""

        Sql = " AND "
        Sql += "仕入先コード"
        Sql += " ILIKE "
        Sql += "'%"
        Sql += SupplierCode
        Sql += "%'"
        Sql += " AND "
        Sql += "取消区分 = 0"
        If CurCode <> 0 Then
            Sql += " AND 通貨 = " & CurCode
        End If

        '仕入先と一致する請求基本を取得
        Dim dsKikehd As DataSet = getDsData("t46_kikehd", Sql)


        '明細行の件数をセット
        TxtKikeCount.Text = dsKikehd.Tables(RS).Rows.Count()

        't46_kikehd 支払金額に登録する
        KikeAmount = IIf(
            dsKikehd.Tables(RS).Compute("SUM(買掛金額計_外貨)", Nothing) IsNot DBNull.Value,
            dsKikehd.Tables(RS).Compute("SUM(買掛金額計_外貨)", Nothing),
            0
        )

        '請求情報の出力
        For i As Integer = 0 To dsKikehd.Tables(RS).Rows.Count - 1
            DgvKikeInfo.Rows.Add()
            DgvKikeInfo.Rows(i).Cells("InfoNo").Value = i + 1
            DgvKikeInfo.Rows(i).Cells("買掛情報買掛番号").Value = dsKikehd.Tables(RS).Rows(i)("買掛番号")
            DgvKikeInfo.Rows(i).Cells("買掛日").Value = dsKikehd.Tables(RS).Rows(i)("買掛日").ToShortDateString()
            DgvKikeInfo.Rows(i).Cells("買掛金額").Value = dsKikehd.Tables(RS).Rows(i)("買掛金額計_外貨")
            If dsKikehd.Tables(RS).Rows(i)("支払金額計_外貨") Is DBNull.Value Then
                DgvKikeInfo.Rows(i).Cells("買掛情報支払金額計").Value = 0
                DgvKikeInfo.Rows(i).Cells("支払金額計固定").Value = 0
            Else
                DgvKikeInfo.Rows(i).Cells("買掛情報支払金額計").Value = dsKikehd.Tables(RS).Rows(i)("支払金額計_外貨")
                DgvKikeInfo.Rows(i).Cells("支払金額計固定").Value = dsKikehd.Tables(RS).Rows(i)("支払金額計_外貨")
            End If

            If dsKikehd.Tables(RS).Rows(i)("支払金額計_外貨") Is DBNull.Value Then
                DgvKikeInfo.Rows(i).Cells("買掛情報買掛残高").Value = dsKikehd.Tables(RS).Rows(i)("買掛金額計_外貨")
                DgvKikeInfo.Rows(i).Cells("買掛情報買掛残高固定").Value = dsKikehd.Tables(RS).Rows(i)("買掛金額計_外貨")
            Else
                DgvKikeInfo.Rows(i).Cells("買掛情報買掛残高").Value = dsKikehd.Tables(RS).Rows(i)("買掛金額計_外貨") - dsKikehd.Tables(RS).Rows(i)("支払金額計_外貨")
                DgvKikeInfo.Rows(i).Cells("買掛情報買掛残高固定").Value = dsKikehd.Tables(RS).Rows(i)("買掛金額計_外貨") - dsKikehd.Tables(RS).Rows(i)("支払金額計_外貨")
            End If
            DgvKikeInfo.Rows(i).Cells("支払金額").Value = 0
        Next

    End Sub

    '前の画面に戻る
    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    '支払入力行の追加
    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        DgvPayment.Rows.Add()
        DgvPayment.Rows(DgvPayment.Rows.Count() - 1).Cells("支払種目").Value = 1
        '最終行のインデックスを取得
        Dim index As Integer = DgvPayment.Rows.Count()
        '行番号の振り直し
        Dim No As Integer = 1
        For c As Integer = 0 To index - 1
            DgvPayment.Rows(c).Cells(0).Value = No
            No += 1
        Next c
        TxtPaymentCount.Text = DgvPayment.Rows.Count()

        'フォーカス
        DgvPayment.Focus()
        DgvPayment.CurrentCell = DgvPayment(2, index - 1)
    End Sub

    '支払入力行の削除
    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        For Each r As DataGridViewCell In DgvPayment.SelectedCells
            DgvPayment.Rows.RemoveAt(r.RowIndex)
        Next r

        '行番号の振り直し
        Dim index As Integer = DgvPayment.Rows.Count()
        Dim No As Integer = 1
        For c As Integer = 0 To index - 1
            DgvPayment.Rows(c).Cells(0).Value = No
            No += 1
        Next c
        TxtKikeCount.Text = DgvPayment.Rows.Count()
    End Sub

    '自動振分の実行
    Private Sub BtnCal_Click(sender As Object, e As EventArgs) Handles BtnCal.Click
        Dim Total As Decimal = 0
        Dim count As Integer = 0

        For index As Integer = 0 To DgvPayment.Rows.Count - 1
            Total += DgvPayment.Rows(index).Cells("入力支払金額").Value
        Next

        '一旦自動振分をリセット
        For i As Integer = 0 To DgvKikeInfo.Rows.Count - 1
            DgvKikeInfo.Rows(i).Cells("買掛情報支払金額計").Value = DgvKikeInfo.Rows(i).Cells("支払金額計固定").Value
            DgvKikeInfo.Rows(i).Cells("買掛情報買掛残高").Value = DgvKikeInfo.Rows(i).Cells("買掛情報買掛残高固定").Value
        Next


        '買掛金額より支払金額が大きい場合はアラート
        If Total > DgvSupplier.Rows(0).Cells("買掛残高").Value Then
            _msgHd.dspMSG("chkPaymentBalanceError", frmC01F10_Login.loginValue.Language)

            Return
        End If

        For i As Integer = 0 To DgvKikeInfo.Rows.Count - 1
            If DgvKikeInfo.Rows(i).Cells("買掛情報買掛残高").Value > 0 Then
                If Total - DgvKikeInfo.Rows(i).Cells("買掛情報買掛残高").Value > 0 Then
                    DgvKikeInfo.Rows(i).Cells("支払金額").Value = DgvKikeInfo.Rows(i).Cells("買掛情報買掛残高").Value
                    DgvKikeInfo.Rows(i).Cells("買掛情報支払金額計").Value = DgvKikeInfo.Rows(i).Cells("買掛情報支払金額計").Value + DgvKikeInfo.Rows(i).Cells("支払金額").Value
                    DgvKikeInfo.Rows(i).Cells("買掛情報買掛残高").Value = 0
                    Total -= DgvKikeInfo.Rows(i).Cells("支払金額").Value
                ElseIf Total > 0 Then
                    DgvKikeInfo.Rows(i).Cells("支払金額").Value = Total
                    If DgvKikeInfo.Rows(i).Cells("買掛情報買掛残高").Value - Total > 0 Then
                        DgvKikeInfo.Rows(i).Cells("買掛情報支払金額計").Value = DgvKikeInfo.Rows(i).Cells("買掛情報支払金額計").Value + DgvKikeInfo.Rows(i).Cells("支払金額").Value
                    ElseIf DgvKikeInfo.Rows(i).Cells("買掛情報買掛残高").Value - Total = 0 Then
                        DgvKikeInfo.Rows(i).Cells("買掛情報支払金額計").Value = DgvKikeInfo.Rows(i).Cells("買掛情報支払金額計").Value + DgvKikeInfo.Rows(i).Cells("支払金額").Value
                    Else
                        DgvKikeInfo.Rows(i).Cells("買掛情報支払金額計").Value = Total
                    End If

                    DgvKikeInfo.Rows(i).Cells("買掛情報買掛残高").Value = DgvKikeInfo.Rows(i).Cells("買掛情報買掛残高").Value - Total
                    Total -= Total
                End If
            End If
        Next

    End Sub

    '支払入力セルの値が変更されたら
    Private Sub DgvPaymentCellValueChanged(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DgvPayment.CellValueChanged

        Dim PurchaseTotal As Integer = 0

        'ヘッダー以外だったら
        If e.RowIndex > -1 Then

            '各項目の属性チェック
            If Not IsNumeric(DgvPayment.Rows(e.RowIndex).Cells("入力支払金額").Value) And (DgvPayment.Rows(e.RowIndex).Cells("入力支払金額").Value IsNot Nothing) Then
                _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
                DgvPayment.Rows(e.RowIndex).Cells("入力支払金額").Value = 0
                Exit Sub
            End If

            Dim decTmp As Decimal = DgvPayment.Rows(e.RowIndex).Cells("入力支払金額").Value
            DgvPayment.Rows(e.RowIndex).Cells("入力支払金額").Value = decTmp

        End If

    End Sub

    '買掛情報セルの値が変更されたら
    Private Sub DgvKikeInfoCellValueChanged(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DgvKikeInfo.CellValueChanged

        Dim PurchaseTotal As Integer = 0

        'ヘッダー以外だったら
        If e.RowIndex > -1 Then

            '各項目の属性チェック
            If Not IsNumeric(DgvKikeInfo.Rows(e.RowIndex).Cells("支払金額").Value) And (DgvKikeInfo.Rows(e.RowIndex).Cells("支払金額").Value IsNot Nothing) Then
                _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
                DgvKikeInfo.Rows(e.RowIndex).Cells("支払金額").Value = 0
                Exit Sub
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
            Sql += formatDatetime(today)
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

    '登録処理
    Private Sub BtnRegist_Click(sender As Object, e As EventArgs) Handles BtnRegist.Click
        Dim errflg As Boolean = True
        Dim dtToday As String = formatDatetime(DateTime.Now)
        Dim reccnt As Integer = 0

        Dim PaymentAmount As Decimal = 0
        Dim PaymentAmountFC As Decimal = 0
        Dim KikeAmountFC As Decimal = 0
        Dim BalanceFC As Decimal = 0

        Dim Sql As String = ""

        '買掛残高がなかったら
        If DgvSupplier.Rows(0).Cells("買掛残高").Value = 0 Then
            '操作できるデータではないことをアラートする
            _msgHd.dspMSG("chkActionPropriety", frmC01F10_Login.loginValue.Language)

            Return
        End If

        '入力内容チェック

        '入力した支払金額を合算
        For i As Integer = 0 To DgvPayment.Rows.Count - 1
            PaymentAmount += DgvPayment.Rows(i).Cells("入力支払金額").Value
        Next

        '買掛残高
        For i As Integer = 0 To DgvPayment.Rows.Count - 1
            Balance += DgvKikeInfo.Rows(i).Cells("買掛情報買掛残高").Value
        Next


        '支払入力がなかったら、或いは合計が0だったら
        If DgvPayment.Rows.Count = 0 Or PaymentAmount = 0 Then
            '対象データがないメッセージを表示
            _msgHd.dspMSG("chkPayAddError", frmC01F10_Login.loginValue.Language)

            Return
        End If

        '採番テーブルから支払番号取得
        Dim APSaiban As String = getSaiban("110", dtToday)

        Sql = " AND "
        Sql += "仕入先コード"
        Sql += " ILIKE "
        Sql += "'%"
        Sql += SupplierCode
        Sql += "%'"
        Sql += " AND "
        Sql += "取消区分 = 0"
        If CurCode <> 0 Then
            Sql += " AND 通貨 = " & CurCode
        End If

        '買掛基本データ取得
        Dim dsKikehd As DataSet = getDsData("t46_kikehd", Sql)


        Sql = " AND "
        Sql += "仕入先コード"
        Sql += " ILIKE "
        Sql += "'%"
        Sql += SupplierCode
        Sql += "%'"

        '仕入先情報の取得
        Dim dsSupplier As DataSet = getDsData("m11_supplier", Sql)


        'レートの取得
        Dim strRate As Decimal = setRate(dsKikehd.Tables(RS).Rows(0)("通貨").ToString())

        '買掛金額
        KikeAmountFC = KikeAmount
        KikeAmount = Math.Ceiling(KikeAmountFC / strRate) '画面の金額をIDRに変換　切り上げ

        '支払金額計
        PaymentAmountFC = PaymentAmount
        PaymentAmount = Math.Ceiling(PaymentAmountFC / strRate) '画面の金額をIDRに変換　切り上げ

        '買掛残高
        BalanceFC = Balance
        Balance = Math.Ceiling(BalanceFC / strRate) '画面の金額をIDRに変換　切り上げ


        't47_shrihd 仕入基本テーブルに新規追加
        Sql = "INSERT INTO "
        Sql += "Public."
        Sql += "t47_shrihd("
        Sql += "会社コード, 支払番号, 支払日, 支払先コード, 支払先名, 支払先, 買掛金額, 支払金額計, 買掛残高, 備考, 取消区分, 登録日, 更新者, 更新日"
        Sql += ",買掛金額_外貨,支払金額計_外貨,買掛残高_外貨,通貨,レート"

        Sql += ") VALUES('"
        Sql += CompanyCode
        Sql += "', '"
        Sql += APSaiban
        Sql += "', '"
        Sql += dtToday
        Sql += "', '"
        Sql += SupplierCode
        Sql += "', '"
        Sql += SupplierName
        Sql += "', '"
        Sql += dsSupplier.Tables(RS).Rows(0)("銀行名")
        Sql += " "
        Sql += dsSupplier.Tables(RS).Rows(0)("支店名")
        Sql += " "
        Sql += dsSupplier.Tables(RS).Rows(0)("預金種目")
        Sql += " "
        Sql += dsSupplier.Tables(RS).Rows(0)("口座番号")
        Sql += " "
        Sql += dsSupplier.Tables(RS).Rows(0)("口座名義")
        Sql += "', '"
        Sql += formatNumber(KikeAmount)     '買掛金額
        Sql += "', '"
        Sql += formatNumber(PaymentAmount)  '支払金額計
        Sql += "', '"
        Sql += formatNumber(Balance)        '買掛残高
        Sql += "', '"
        Sql += TxtRemarks.Text
        Sql += "', '"
        Sql += "0"
        Sql += "', '"
        Sql += dtToday
        Sql += "', '"
        Sql += frmC01F10_Login.loginValue.TantoNM
        Sql += "', '"
        Sql += dtToday
        Sql += "', '"
        Sql += formatNumber(KikeAmountFC)     '買掛金額_外貨
        Sql += "', '"
        Sql += formatNumber(PaymentAmountFC)  '支払金額計_外貨
        Sql += "', '"
        Sql += formatNumber(BalanceFC)        '買掛残高_外貨
        Sql += "', '"
        Sql += dsKikehd.Tables(RS).Rows(0)("通貨").ToString()
        Sql += "', '"
        Sql += UtilClass.formatNumberF10(strRate)

        Sql += "')"

        _db.executeDB(Sql)


        Dim AmountInputPayment As Decimal = 0
        Dim AmountInputPaymentFC As Decimal = 0

        't48_shridt 支払明細テーブルに入金入力テーブルの明細を追加
        For i As Integer = 0 To DgvPayment.Rows.Count - 1


            AmountInputPaymentFC = DgvPayment.Rows(i).Cells("入力支払金額").Value
            AmountInputPayment = Math.Ceiling(AmountInputPaymentFC / strRate) '画面の金額をIDRに変換　切り上げ


            Sql = "INSERT INTO "
            Sql += "Public."
            Sql += "t48_shridt("
            Sql += "会社コード, 支払番号, 行番号, 支払種別, 支払種別名, 支払先, 支払金額, 更新者, 更新日, 支払先コード, 支払先名, 支払日, 備考"
            Sql += ",支払金額_外貨,通貨,レート"

            Sql += ") VALUES('"
            Sql += CompanyCode
            Sql += "', '"
            Sql += APSaiban
            Sql += "', '"
            Sql += DgvPayment.Rows(i).Cells("行番号").Value.ToString
            Sql += "', '"
            Sql += DgvPayment.Rows(i).Cells("支払種目").Value.ToString
            Sql += "', '"
            Sql += DgvPayment.Rows(i).Cells("支払種目").Value.ToString
            Sql += "', '"
            Sql += dsSupplier.Tables(RS).Rows(0)("銀行名").ToString
            Sql += " "
            Sql += dsSupplier.Tables(RS).Rows(0)("支店名").ToString
            Sql += " "
            Sql += dsSupplier.Tables(RS).Rows(0)("預金種目").ToString
            Sql += " "
            Sql += dsSupplier.Tables(RS).Rows(0)("口座番号").ToString
            Sql += " "
            Sql += dsSupplier.Tables(RS).Rows(0)("口座名義").ToString
            Sql += "', '"
            Sql += formatNumber(AmountInputPayment)  '支払金額
            Sql += "', '"
            Sql += frmC01F10_Login.loginValue.TantoNM
            Sql += "', '"
            Sql += dtToday
            Sql += "', '"
            Sql += SupplierCode
            Sql += "', '"
            Sql += SupplierName
            Sql += "', '"
            Sql += dtToday
            Sql += "', '"
            Sql += TxtRemarks.Text

            Sql += "', '"
            Sql += formatNumber(AmountInputPaymentFC)        '支払金額_外貨
            Sql += "', '"
            Sql += dsKikehd.Tables(RS).Rows(0)("通貨").ToString()
            Sql += "', '"
            Sql += UtilClass.formatNumberF10(strRate)

            Sql += "')"

            _db.executeDB(Sql)

            Sql = ""
        Next


        Dim TotalPaymentClearingAmount As Decimal = 0
        Dim TotalPaymentClearingAmountFC As Decimal = 0

        't49_shrikshihd 支払消込テーブルに新規追加
        For i As Integer = 0 To DgvKikeInfo.Rows.Count - 1

            '複数の買掛情報がある場合、支払金額が0のものは登録しない
            If DgvKikeInfo.Rows(i).Cells("支払金額").Value <> 0 Then

                TotalPaymentClearingAmountFC = DgvKikeInfo.Rows(i).Cells("支払金額").Value
                TotalPaymentClearingAmount = Math.Ceiling(TotalPaymentClearingAmountFC / strRate) '画面の金額をIDRに変換　切り上げ

                Sql = "INSERT INTO "
                Sql += "Public."
                Sql += "t49_shrikshihd("
                Sql += "会社コード, 支払番号, 支払日, 買掛番号, 支払先コード, 支払先名, 支払消込額計, 備考, 取消区分, 更新者, 更新日"
                Sql += ",支払消込額計_外貨,通貨,レート"

                Sql += ") VALUES('"
                Sql += CompanyCode
                Sql += "', '"
                Sql += APSaiban
                Sql += "', '"
                Sql += dtToday
                Sql += "', '"
                Sql += DgvKikeInfo.Rows(i).Cells("買掛情報買掛番号").Value.ToString
                Sql += "', '"
                Sql += SupplierCode
                Sql += "', '"
                Sql += SupplierName
                Sql += "', '"
                Sql += formatNumber(TotalPaymentClearingAmount)  '支払消込額計
                Sql += "', '"
                Sql += TxtRemarks.Text
                Sql += "', '"
                Sql += "0"
                Sql += "', '"
                Sql += frmC01F10_Login.loginValue.TantoNM
                Sql += "', '"
                Sql += dtToday

                Sql += "', '"
                Sql += formatNumber(TotalPaymentClearingAmountFC)        '支払消込額計_外貨
                Sql += "', '"
                Sql += dsKikehd.Tables(RS).Rows(0)("通貨").ToString()
                Sql += "', '"
                Sql += UtilClass.formatNumberF10(strRate)

                Sql += "')"

                _db.executeDB(Sql)

            End If
        Next

        Dim DsPayment As Decimal = 0    '支払金額
        Dim DsPaymentFC As Decimal = 0
        Dim APBalance As Decimal = 0    '支払金額計
        Dim APBalanceFC As Decimal = 0

        't46_kikehd 買掛基本テーブルを更新
        For i As Integer = 0 To dsKikehd.Tables(RS).Rows.Count - 1

            If DgvKikeInfo.Rows(i).Cells("支払金額").Value <> 0 Then

                If dsKikehd.Tables(RS).Rows(i)("支払金額計") Is DBNull.Value Then
                    '買掛基本の支払金額がなかったら支払金額をそのまま登録
                    DsPaymentFC = DgvKikeInfo.Rows(i).Cells("支払金額").Value
                Else
                    '支払金額計があったら支払金額を加算する
                    DsPaymentFC = DgvKikeInfo.Rows(i).Cells("支払金額").Value + dsKikehd.Tables(RS).Rows(i)("支払金額計_外貨")
                End If

                DsPayment = Math.Ceiling(DsPaymentFC / strRate)  '画面の金額をIDRに変換　切り上げ


                '残高を更新
                APBalanceFC = dsKikehd.Tables(RS).Rows(i)("買掛残高_外貨") - DgvKikeInfo.Rows(i).Cells("支払金額").Value
                APBalance = Math.Ceiling(APBalanceFC / strRate)  '画面の金額をIDRに変換　切り上げ


                Sql = "UPDATE "
                Sql += "Public."
                Sql += "t46_kikehd "
                Sql += "SET "
                Sql += " 支払金額計"
                Sql += " = '"
                Sql += formatNumber(DsPayment)
                Sql += "', "
                Sql += "買掛残高"
                Sql += " = '"
                Sql += formatNumber(APBalance)
                Sql += "', "

                Sql += " 支払金額計_外貨"
                Sql += " = '"
                Sql += formatNumber(DsPaymentFC)
                Sql += "', "
                Sql += "買掛残高_外貨"
                Sql += " = '"
                Sql += formatNumber(APBalanceFC)
                Sql += "', "

                '買掛金額計と支払金額計が一致したら支払完了日を設定する
                If formatNumber(dsKikehd.Tables(RS).Rows(i)("買掛金額計")) = formatNumber(DsPayment) Then

                    Sql += "支払完了日"
                    Sql += " = '"
                    Sql += dtToday
                    Sql += "', "

                End If

                Sql += "更新日"
                Sql += " = '"
                Sql += dtToday
                Sql += "' "
                Sql += "WHERE"
                Sql += " 会社コード"
                Sql += "='"
                Sql += CompanyCode
                Sql += "'"
                Sql += " AND"
                Sql += " 買掛番号"
                Sql += "='"
                Sql += dsKikehd.Tables(RS).Rows(i)("買掛番号")
                Sql += "' "

                If CurCode <> 0 Then
                    Sql += " AND 通貨 = " & CurCode
                End If

                _db.executeDB(Sql)

            End If

        Next

        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()

    End Sub

    '通貨の採番キーからレートを取得・設定
    '基準日が支払日「以前」の最新のもの
    Private Function setRate(ByVal strKey As Integer) As Decimal
        Dim Sql As String

        Sql = " AND 採番キー = " & strKey & ""
        Sql += " AND 基準日 < '" & UtilClass.strFormatDate(DtpDepositDate.Text) & "'"  '支払日
        Sql += " ORDER BY 基準日 DESC "

        Dim ds As DataSet = getDsData("t71_exchangerate", Sql)

        If ds.Tables(RS).Rows.Count > 0 Then
            setRate = ds.Tables(RS).Rows(0)("レート")
        Else
            If CultureInfo.CurrentCulture.Name.ToString = CommonConst.CI_ID Then
                setRate = CommonConst.BASE_RATE_IDR
            Else
                setRate = CommonConst.BASE_RATE_JPY
            End If
        End If

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

    'ユーザーのカルチャーから、日本の形式に変換する
    Private Function strFormatDate(ByVal prmDate As String, Optional ByRef prmFormat As String = "yyyy/MM/dd") As String

        'PCのカルチャーを取得し、それに応じてStringからDatetimeを作成
        Dim ci As New System.Globalization.CultureInfo(CultureInfo.CurrentCulture.Name.ToString)
        Dim dateFormat As DateTime = DateTime.Parse(prmDate, ci, System.Globalization.DateTimeStyles.AssumeLocal)

        '日本の形式に書き換える
        Return dateFormat.ToString(prmFormat)
    End Function

    'ユーザーのカルチャーから、日本の形式に変換する
    Private Function formatDatetime(ByVal prmDatetime As DateTime) As String

        'PCのカルチャーを取得し、それに応じてStringからDatetimeを作成
        Dim ciCurrent As New System.Globalization.CultureInfo(CultureInfo.CurrentCulture.Name.ToString)
        Dim dateFormat As DateTime = DateTime.Parse(prmDatetime.ToString, ciCurrent, System.Globalization.DateTimeStyles.AssumeLocal)

        Dim changeFormat As String = dateFormat.ToString("yyyy/MM/dd HH:mm:ss")

        Dim ciJP As New System.Globalization.CultureInfo(CommonConst.CI_JP)
        Dim rtnDatetime As DateTime = DateTime.Parse(changeFormat, ciJP, System.Globalization.DateTimeStyles.AssumeLocal)


        '日本の形式に書き換える
        Return changeFormat
    End Function

    '金額フォーマット（登録の際の小数点指定子）を日本の形式に合わせる
    '桁区切り記号は外す
    Private Function formatNumber(ByVal prmVal As Decimal) As String

        Dim nfi As NumberFormatInfo = New CultureInfo(CommonConst.CI_JP, False).NumberFormat

        '日本の形式に書き換える
        Return prmVal.ToString("F3", nfi)
    End Function

    'DGV内で指定列名(プルダウン)は一度のクリックで開く
    'それ以外は一回で入力状態にする
    Private Sub DgvPayment_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DgvPayment.CellEnter
        If DgvPayment.Columns(e.ColumnIndex).Name = "支払種目" Then
            SendKeys.Send("{F4}")
            'Else
            '    SendKeys.Send("{F2}")
        End If
    End Sub

    '汎用マスタから固定キー、可変キーに応じた結果を返す
    'param1：String 固定キー
    'param2：String 可変キー
    'Return: DataSet
    Private Function getDsHanyoData(ByVal prmFixed As String, Optional ByVal prmVariable As String = "") As DataSet
        Dim Sql As String = ""

        Sql = " AND "
        Sql += "固定キー ILIKE '" & prmFixed & "'"

        If prmVariable IsNot "" Then
            Sql += " AND "
            Sql += "可変キー ILIKE '" & prmVariable & "'"
        End If

        'リードタイムのリストを汎用マスタから取得
        Return getDsData("m90_hanyo", Sql)
    End Function

End Class