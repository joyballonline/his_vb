﻿Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports System.Globalization

Public Class DepositManagement
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
    Private CustomerCode As String = ""
    Private CustomerName As String = ""
    Private Suffix As String = ""
    Private _parentForm As Form
    Private _status As String = ""
    Private BillingAmount As Long = 0
    Private Balance As Integer = 0

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
                   ByRef prmRefCustomer As String,
                   ByRef prmRefName As String,
                   Optional ByRef prmRefStatus As String = "")
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _parentForm = prmRefForm
        CompanyCode = prmRefCompany
        CustomerCode = prmRefCustomer
        CustomerName = prmRefName
        _status = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox

        DtpDepositDate.Value = Date.Now
        _init = True

    End Sub

    Private Sub Quote_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Sql As String = ""

        Sql = " AND "
        Sql += "固定キー ILIKE '2'"

        Dim reccnt As Integer = 0

        '汎用マスタから入金種目一覧を取得、プルダウンを作成
        Dim dsHanyo As DataSet = getDsData("m90_hanyo", Sql)

        Dim table As New DataTable("Table")
        table.Columns.Add("Display", GetType(String))
        table.Columns.Add("Value", GetType(Integer))

        For i As Integer = 0 To dsHanyo.Tables(RS).Rows.Count - 1
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                table.Rows.Add(dsHanyo.Tables(RS).Rows(i)("文字２"), dsHanyo.Tables(RS).Rows(i)("可変キー"))
            Else
                table.Rows.Add(dsHanyo.Tables(RS).Rows(i)("文字１"), dsHanyo.Tables(RS).Rows(i)("可変キー"))
            End If
        Next

        '入金登録エリアに反映
        Dim column As New DataGridViewComboBoxColumn()
        column.DataSource = table
        column.ValueMember = "Value"
        column.DisplayMember = "Display"
        column.HeaderText = "入金種目"
        column.Name = "入金種目"

        DgvDeposit.Columns.Insert(1, column)

        '明細を描画
        setData()

        '翻訳
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblHistory.Text = "MoneyReceiptDataHistory"
            LblDeposit.Text = "MoneyReceiptInput"
            LblDeposit.Location = New Point(13, 203)
            LblDeposit.Size = New Size(142, 15)
            LblBillingInfo.Text = "BillingInfomation"
            LblDepositDate.Text = "MoneyReceiptDate"
            LblDepositDate.Location = New Point(137, 335)
            LblDepositDate.Size = New Size(140, 22)
            LblRemarks.Text = "Remarks"
            TxtRemarks.Size = New Size(600, 22)
            LblNo1.Text = "Record"
            LblNo1.Location = New Point(1272, 65)
            LblNo1.Size = New Size(66, 22)
            LblNo2.Text = "Record"
            LblNo2.Location = New Point(1272, 198)
            LblNo2.Size = New Size(66, 22)
            LblNo3.Text = "Record"
            LblNo3.Location = New Point(1272, 335)
            LblNo3.Size = New Size(66, 22)

            TxtHistoryCount.Location = New Point(1228, 65)
            TxtDepositCount.Location = New Point(1228, 198)
            TxtBillingCount.Location = New Point(1228, 335)

            BtnAdd.Text = "Add"
            BtnAdd.Location = New Point(151, 203)
            BtnDelete.Text = "Delete"
            BtnDelete.Location = New Point(251, 203)
            BtnCal.Text = "AutomaticAllocation"
            BtnCal.Location = New Point(351, 203)
            BtnCal.Size = New Size(120, 20)
            BtnRegist.Text = "Registration"
            BtnBack.Text = "Back"

            DgvCustomer.Columns("請求先").HeaderText = "BillingAddress"
            DgvCustomer.Columns("請求残高").HeaderText = "BillingBalance"

            DgvHistory.Columns("請求番号").HeaderText = "InvoiceNumber"
            DgvHistory.Columns("入金済請求先").HeaderText = "CustomerName"
            DgvHistory.Columns("入金番号").HeaderText = "DepositNumber"
            DgvHistory.Columns("入金日").HeaderText = "DepositDate"
            DgvHistory.Columns("入金種目").HeaderText = "DepositType"
            DgvHistory.Columns("入金済入金額計").HeaderText = "TotalMoneyReceiptAmount"
            DgvHistory.Columns("備考").HeaderText = "Remarks"

            DgvDeposit.Columns("行番号").HeaderText = "LineNumber"
            DgvDeposit.Columns("入金種目").HeaderText = "MoneyReceiptType"
            DgvDeposit.Columns("入力入金額").HeaderText = "TotalMoneyReceiptAmount"

            DgvBillingInfo.Columns("請求情報請求番号").HeaderText = "InvoiceNumber"
            DgvBillingInfo.Columns("請求日").HeaderText = "BillingDate"
            DgvBillingInfo.Columns("請求金額").HeaderText = "BillingAmount"
            DgvBillingInfo.Columns("請求情報入金額計").HeaderText = "TotalMoneyReceiptAmount"
            DgvBillingInfo.Columns("請求情報請求残高").HeaderText = "BillingBalance"
            DgvBillingInfo.Columns("入金額").HeaderText = "MoneyReceiptAmount"

        End If
    End Sub

    '各Table内の作成
    Private Sub setData()

        setDgvCustomer() '請求先情報の出力

        setDgvHistory() '入金済みデータの出力

        setDgvDeposit() '入金入力エリアの作成

        setDgvBillingInfo() '請求情報の出力

    End Sub

    '請求先情報
    Private Sub setDgvCustomer()
        Dim Sql As String = ""
        Dim AccountsReceivable As Long

        Sql = " AND "
        Sql += "得意先コード"
        Sql += " ILIKE "
        Sql += "'%"
        Sql += CustomerCode
        Sql += "%'"
        Sql += " AND "
        Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

        '得意先と一致する請求基本を取得
        Dim dsSkyuhd As DataSet = getDsData("t23_skyuhd", Sql)

        '売掛残高を集計
        AccountsReceivable = IIf(
            dsSkyuhd.Tables(RS).Compute("SUM(売掛残高)", Nothing) IsNot DBNull.Value,
            dsSkyuhd.Tables(RS).Compute("SUM(売掛残高)", Nothing),
            0
        )

        '請求先情報の出力
        DgvCustomer.Rows.Add()
        DgvCustomer.Rows(0).Cells("請求先").Value = CustomerName
        DgvCustomer.Rows(0).Cells("請求残高").Value = AccountsReceivable
    End Sub

    '入金済みデータ
    Private Sub setDgvHistory()
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        'joinするのでとりあえず直書き
        Sql = "SELECT"
        Sql += " t26.請求先名, t26.入金番号, t26.更新日, t26.入金種別名, t26.入金額, t26.備考"
        Sql += " FROM "
        Sql += " public.t26_nkindt t26 "

        Sql += " INNER JOIN "
        Sql += " t25_nkinhd t25"
        Sql += " ON "

        Sql += " t26.会社コード = t25.会社コード"
        Sql += " AND "
        Sql += " t26.入金番号 = t25.入金番号"

        Sql += " WHERE "
        Sql += " t26.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND "
        Sql += "t26.請求先コード"
        Sql += " ILIKE "
        Sql += "'%"
        Sql += CustomerCode
        Sql += "%'"
        Sql += " AND "
        Sql += "t25.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

        '得意先と一致する入金明細を取得
        Dim dsNkindt As DataSet = _db.selectDB(Sql, RS, reccnt)

        '明細行の件数をセット
        TxtHistoryCount.Text = dsNkindt.Tables(RS).Rows.Count()

        '入金済みデータの出力
        For index As Integer = 0 To dsNkindt.Tables(RS).Rows.Count - 1
            DgvHistory.Rows.Add()
            DgvHistory.Rows(index).Cells("No").Value = index + 1
            DgvHistory.Rows(index).Cells("入金済請求先").Value = dsNkindt.Tables(RS).Rows(index)("請求先名")
            DgvHistory.Rows(index).Cells("入金番号").Value = dsNkindt.Tables(RS).Rows(index)("入金番号")
            DgvHistory.Rows(index).Cells("入金日").Value = dsNkindt.Tables(RS).Rows(index)("更新日").ToShortDateString()
            DgvHistory.Rows(index).Cells("入金種目").Value = dsNkindt.Tables(RS).Rows(index)("入金種別名")
            DgvHistory.Rows(index).Cells("入金済入金額計").Value = dsNkindt.Tables(RS).Rows(index)("入金額")
            DgvHistory.Rows(index).Cells("備考").Value = dsNkindt.Tables(RS).Rows(index)("備考")
        Next

    End Sub

    '入金入力
    Private Sub setDgvDeposit()
        '明細行の件数をセット
        TxtDepositCount.Text = DgvDeposit.Rows.Count()
    End Sub


    '請求情報の出力
    Private Sub setDgvBillingInfo()
        Dim Sql As String = ""

        Sql = " AND "
        Sql += "得意先コード"
        Sql += " ILIKE "
        Sql += "'%"
        Sql += CustomerCode
        Sql += "%'"
        Sql += " AND "
        Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
        Sql += " ORDER BY 会社コード, 請求番号"

        '得意先と一致する請求基本を取得
        Dim dsSkyuhd As DataSet = getDsData("t23_skyuhd", Sql)


        '明細行の件数をセット
        TxtBillingCount.Text = dsSkyuhd.Tables(RS).Rows.Count()

        't25_nkinhd 請求金額に登録する
        BillingAmount = IIf(
            dsSkyuhd.Tables(RS).Compute("SUM(請求金額計)", Nothing) IsNot DBNull.Value,
            dsSkyuhd.Tables(RS).Compute("SUM(請求金額計)", Nothing),
            0
        )

        '請求情報の出力
        For i As Integer = 0 To dsSkyuhd.Tables(RS).Rows.Count - 1
            DgvBillingInfo.Rows.Add()
            DgvBillingInfo.Rows(i).Cells("InfoNo").Value = i + 1
            DgvBillingInfo.Rows(i).Cells("請求情報請求番号").Value = dsSkyuhd.Tables(RS).Rows(i)("請求番号")
            DgvBillingInfo.Rows(i).Cells("請求日").Value = dsSkyuhd.Tables(RS).Rows(i)("請求日").ToShortDateString()
            DgvBillingInfo.Rows(i).Cells("請求金額").Value = dsSkyuhd.Tables(RS).Rows(i)("請求金額計")
            If dsSkyuhd.Tables(RS).Rows(i)("入金額計") Is DBNull.Value Then
                DgvBillingInfo.Rows(i).Cells("請求情報入金額計").Value = 0
            Else
                DgvBillingInfo.Rows(i).Cells("請求情報入金額計").Value = dsSkyuhd.Tables(RS).Rows(i)("入金額計")
            End If

            If dsSkyuhd.Tables(RS).Rows(i)("入金額計") Is DBNull.Value Then
                DgvBillingInfo.Rows(i).Cells("請求情報請求残高").Value = dsSkyuhd.Tables(RS).Rows(i)("請求金額計")
            Else
                DgvBillingInfo.Rows(i).Cells("請求情報請求残高").Value = dsSkyuhd.Tables(RS).Rows(i)("請求金額計") - dsSkyuhd.Tables(RS).Rows(i)("入金額計")
            End If
            DgvBillingInfo.Rows(i).Cells("入金額").Value = 0
        Next


    End Sub

    '前の画面に戻る
    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    '入金入力行の追加
    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        DgvDeposit.Rows.Add()
        DgvDeposit.Rows(DgvDeposit.Rows.Count() - 1).Cells("入金種目").Value = 1

        '最終行のインデックスを取得
        Dim index As Integer = DgvDeposit.Rows.Count()

        '行番号の振り直し
        Dim No As Integer = 1
        For c As Integer = 0 To index - 1
            DgvDeposit.Rows(c).Cells(0).Value = No
            No += 1
        Next c
        TxtDepositCount.Text = DgvDeposit.Rows.Count()
    End Sub

    '入金入力行の削除
    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        For Each r As DataGridViewCell In DgvDeposit.SelectedCells
            DgvDeposit.Rows.RemoveAt(r.RowIndex)
        Next r

        '行番号の振り直し
        Dim index As Integer = DgvDeposit.Rows.Count()
        Dim No As Integer = 1
        For c As Integer = 0 To index - 1
            DgvDeposit.Rows(c).Cells(0).Value = No
            No += 1
        Next c
        TxtDepositCount.Text = DgvDeposit.Rows.Count()
    End Sub

    '自動振分の実行
    Private Sub BtnCal_Click(sender As Object, e As EventArgs) Handles BtnCal.Click
        Dim Total As Decimal = 0
        Dim count As Integer = 0

        For index As Integer = 0 To DgvDeposit.Rows.Count - 1
            Total += DgvDeposit.Rows(index).Cells("入力入金額").Value
        Next

        '買掛金額より支払金額が大きい場合はアラート
        If Total > DgvCustomer.Rows(0).Cells("請求残高").Value Then
            _msgHd.dspMSG("chkReceiptBalanceError", frmC01F10_Login.loginValue.Language)

            Return
        End If

        For i As Integer = 0 To DgvBillingInfo.Rows.Count - 1
            If DgvBillingInfo.Rows(i).Cells("請求情報請求残高").Value > 0 Then
                If Total - DgvBillingInfo.Rows(i).Cells("請求情報請求残高").Value > 0 Then
                    DgvBillingInfo.Rows(i).Cells("入金額").Value = DgvBillingInfo.Rows(i).Cells("請求情報請求残高").Value
                    DgvBillingInfo.Rows(i).Cells("請求情報入金額計").Value = DgvBillingInfo.Rows(i).Cells("請求情報入金額計").Value + DgvBillingInfo.Rows(i).Cells("入金額").Value
                    DgvBillingInfo.Rows(i).Cells("請求情報請求残高").Value = 0
                    Total -= DgvBillingInfo.Rows(i).Cells("入金額").Value
                ElseIf Total > 0 Then
                    DgvBillingInfo.Rows(i).Cells("入金額").Value = Total
                    If DgvBillingInfo.Rows(i).Cells("請求情報請求残高").Value - Total > 0 Then
                        DgvBillingInfo.Rows(i).Cells("請求情報入金額計").Value = DgvBillingInfo.Rows(i).Cells("請求情報入金額計").Value + DgvBillingInfo.Rows(i).Cells("入金額").Value
                    ElseIf DgvBillingInfo.Rows(i).Cells("請求情報請求残高").Value - Total = 0 Then
                        DgvBillingInfo.Rows(i).Cells("請求情報入金額計").Value = DgvBillingInfo.Rows(i).Cells("請求情報入金額計").Value + DgvBillingInfo.Rows(i).Cells("入金額").Value
                    Else
                        DgvBillingInfo.Rows(i).Cells("請求情報入金額計").Value = Total
                    End If
                    DgvBillingInfo.Rows(i).Cells("請求情報請求残高").Value = DgvBillingInfo.Rows(i).Cells("請求情報請求残高").Value - Total
                    Total -= Total
                End If
            End If
        Next

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
            saibanID += strFormatDate(today.ToString, "MMdd")
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
        Dim dtToday As String = formatDatetime(DateTime.Now)
        Dim reccnt As Integer = 0
        Dim DepositAmount As Decimal = 0
        Dim BillingAmount As Decimal = 0

        Dim Sql As String = ""

        '請求残高がなかったら
        If DgvCustomer.Rows(0).Cells("請求残高").Value = 0 Then
            '操作できるデータではないことをアラートする
            _msgHd.dspMSG("chkActionPropriety", frmC01F10_Login.loginValue.Language)

            Return
        End If

        '入力内容チェック

        '入金入力があっても入金額が0だったら
        '入力した入金額を合算
        For i As Integer = 0 To DgvDeposit.Rows.Count - 1
            DepositAmount += DgvDeposit.Rows(i).Cells("入力入金額").Value
        Next

        '請求情報の合算が0だったら
        For i As Integer = 0 To DgvBillingInfo.Rows.Count - 1
            BillingAmount += DgvBillingInfo.Rows(i).Cells("入金額").Value
        Next

        '入金入力がない、或いは合計が0だったら
        If DgvDeposit.Rows.Count = 0 Or DepositAmount = 0 Then
            '対象データがないメッセージを表示
            _msgHd.dspMSG("chkAPAddError", frmC01F10_Login.loginValue.Language)

            Return
        End If

        '入金入力はあるが、請求情報に反映されていない場合
        If DepositAmount > 0 And BillingAmount = 0 Then
            '入金入力が請求情報に反映されていないメッセージを表示
            _msgHd.dspMSG("chkPtoBBalanceError", frmC01F10_Login.loginValue.Language)

            Return
        End If

        '入金入力と請求情報の金額が不一致だったら
        If DepositAmount <> BillingAmount Then
            '金額があっていないアラートを表示
            _msgHd.dspMSG("chkPEqualBBalanceError", frmC01F10_Login.loginValue.Language)

            Return
        End If

        '採番テーブルから入金番号取得
        Dim PMSaiban As String = getSaiban("90", dtToday)

        Sql = " AND "
        Sql += "得意先コード"
        Sql += " ILIKE "
        Sql += "'%"
        Sql += CustomerCode
        Sql += "%'"
        Sql += " AND "
        Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

        '請求基本データ取得
        Dim dsSkyuhd As DataSet = getDsData("t23_skyuhd", Sql)

        '会社情報の取得
        Dim dsCompany As DataSet = getDsData("m01_company")

        't25_nkinhd 入金基本テーブルに新規追加
        Sql = "INSERT INTO "
        Sql += "Public."
        Sql += "t25_nkinhd("
        Sql += "会社コード, 入金番号, 入金日, 請求先コード, 請求先名, 振込先, 入金額,  備考, 取消区分, 登録日, 更新者, 更新日"
        Sql += ") VALUES ('"
        Sql += CompanyCode
        Sql += "', '"
        Sql += PMSaiban
        Sql += "', '"
        Sql += dtToday
        Sql += "', '"
        Sql += CustomerCode
        Sql += "', '"
        Sql += CustomerName
        Sql += "', '"
        Sql += dsCompany.Tables(RS).Rows(0)("銀行名")
        Sql += " "
        Sql += dsCompany.Tables(RS).Rows(0)("支店名")
        Sql += " "
        Sql += dsCompany.Tables(RS).Rows(0)("預金種目")
        Sql += " "
        Sql += dsCompany.Tables(RS).Rows(0)("口座番号")
        Sql += " "
        Sql += dsCompany.Tables(RS).Rows(0)("口座名義")
        Sql += "', '"
        Sql += formatNumber(DepositAmount)
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
        Sql += "')"

        _db.executeDB(Sql)

        't26_nkindt 入金明細テーブルに入金入力テーブルの明細を追加
        For i As Integer = 0 To DgvDeposit.Rows.Count - 1
            '入金入力額が0のものは省く
            If DgvDeposit.Rows(i).Cells("入力入金額").Value <> 0 Then

                Sql = "INSERT INTO "
                Sql += "Public."
                Sql += "t26_nkindt("
                Sql += "会社コード, 入金番号, 行番号, 入金種別, 入金種別名, 振込先, 入金額, 更新者, 更新日, 請求先コード, 請求先名, 入金日, 備考)"
                Sql += " VALUES('"
                Sql += CompanyCode
                Sql += "', '"
                Sql += PMSaiban
                Sql += "', '"
                Sql += DgvDeposit.Rows(i).Cells("行番号").Value.ToString
                Sql += "', '"
                Sql += DgvDeposit.Rows(i).Cells("入金種目").Value.ToString
                Sql += "', '"
                Sql += DgvDeposit.Rows(i).Cells("入金種目").Value.ToString
                Sql += "', '"
                Sql += dsCompany.Tables(RS).Rows(0)("銀行名").ToString
                Sql += " "
                Sql += dsCompany.Tables(RS).Rows(0)("支店名").ToString
                Sql += " "
                Sql += dsCompany.Tables(RS).Rows(0)("預金種目").ToString
                Sql += " "
                Sql += dsCompany.Tables(RS).Rows(0)("口座番号").ToString
                Sql += " "
                Sql += dsCompany.Tables(RS).Rows(0)("口座名義").ToString
                Sql += "', '"
                Sql += DgvDeposit.Rows(i).Cells("入力入金額").Value.ToString
                Sql += "', '"
                Sql += frmC01F10_Login.loginValue.TantoNM
                Sql += "', '"
                Sql += dtToday
                Sql += "', '"
                Sql += CustomerCode
                Sql += "', '"
                Sql += CustomerName
                Sql += "', '"
                Sql += dtToday
                Sql += "', '"
                Sql += TxtRemarks.Text
                Sql += "')"

                _db.executeDB(Sql)

            End If

        Next

        't27_nkinkshihd 入金消込テーブルに新規追加
        For i As Integer = 0 To DgvBillingInfo.Rows.Count - 1

            '複数の買掛情報がある場合、支払金額が0のものは登録しない
            If DgvBillingInfo.Rows(i).Cells("入金額").Value <> 0 Then

                Sql = "INSERT INTO "
                Sql += "Public."
                Sql += "t27_nkinkshihd("
                Sql += "会社コード, 入金番号, 入金日, 請求番号, 請求先コード, 請求先名, 入金消込額計, 備考, 取消区分, 更新者, 更新日)"
                Sql += " VALUES('"
                Sql += CompanyCode
                Sql += "', '"
                Sql += PMSaiban
                Sql += "', '"
                Sql += dtToday
                Sql += "', '"
                Sql += DgvBillingInfo.Rows(i).Cells("請求情報請求番号").Value.ToString
                Sql += "', '"
                Sql += CustomerCode
                Sql += "', '"
                Sql += CustomerName
                Sql += "', '"
                Sql += formatNumber(DgvBillingInfo.Rows(i).Cells("入金額").Value)
                Sql += "', '"
                Sql += TxtRemarks.Text
                Sql += "', '"
                Sql += "0"
                Sql += "', '"
                Sql += frmC01F10_Login.loginValue.TantoNM
                Sql += "', '"
                Sql += dtToday
                Sql += "')"

                _db.executeDB(Sql)

            End If
        Next

        Dim DsDeposit As Decimal = 0
        Dim SellingBalance As Decimal = 0

        't23_skyuhd 請求基本テーブルを更新
        For i As Integer = 0 To dsSkyuhd.Tables(RS).Rows.Count - 1

            If DgvBillingInfo.Rows(i).Cells("入金額").Value <> 0 Then

                If dsSkyuhd.Tables(RS).Rows(i)("入金額計") Is DBNull.Value Then
                    '請求基本の入金額計がなかったら入金額をそのまま登録
                    DsDeposit = DgvBillingInfo.Rows(i).Cells("入金額").Value
                Else
                    '入金額計があったら入金額を加算する
                    DsDeposit = DgvBillingInfo.Rows(i).Cells("入金額").Value + dsSkyuhd.Tables(RS).Rows(i)("入金額計")
                End If

                '残高を更新
                SellingBalance = dsSkyuhd.Tables(RS).Rows(i)("売掛残高") - DgvBillingInfo.Rows(i).Cells("入金額").Value

                Sql = "UPDATE "
                Sql += "Public."
                Sql += "t23_skyuhd "
                Sql += "SET "
                Sql += " 入金額計"
                Sql += " = '"
                Sql += formatNumber(DsDeposit)
                Sql += "', "
                Sql += "売掛残高"
                Sql += " = '"
                Sql += formatNumber(SellingBalance)
                Sql += "', "

                '請求額請求金額と入金額が一致したら入金完了日を設定する
                If formatNumber(dsSkyuhd.Tables(RS).Rows(i)("請求金額計")) = formatNumber(DsDeposit) Then

                    Sql += "入金完了日"
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
                Sql += " 請求番号"
                Sql += "='"
                Sql += dsSkyuhd.Tables(RS).Rows(i)("請求番号")
                Sql += "' "

                _db.executeDB(Sql)

                DsDeposit = 0
                SellingBalance = 0

            End If

        Next

        '_parentForm.Enabled = True
        '_parentForm.Show()

        'Dim openForm As Form = Nothing
        'openForm = New DepositList(_msgHd, _db, _langHd, _parentForm)
        'openForm.Show()

        'Me.Dispose()

        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()

    End Sub

    '入金入力セルの値が変更されたら
    Private Sub DgvDepositCellValueChanged(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DgvDeposit.CellValueChanged

        Dim PurchaseTotal As Integer = 0

        'ヘッダー以外だったら
        If e.RowIndex > -1 Then

            '各項目の属性チェック
            If Not IsNumeric(DgvDeposit.Rows(e.RowIndex).Cells("入力入金額").Value) And (DgvDeposit.Rows(e.RowIndex).Cells("入力入金額").Value IsNot Nothing) Then
                _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
                DgvDeposit.Rows(e.RowIndex).Cells("入力入金額").Value = 0
                Exit Sub
            End If
        End If

    End Sub

    '請求情報セルの値が変更されたら
    Private Sub DgvBillingInfoCellValueChanged(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DgvBillingInfo.CellValueChanged

        Dim PurchaseTotal As Integer = 0

        'ヘッダー以外だったら
        If e.RowIndex > -1 Then

            '各項目の属性チェック
            If Not IsNumeric(DgvBillingInfo.Rows(e.RowIndex).Cells("入金額").Value) And (DgvBillingInfo.Rows(e.RowIndex).Cells("入金額").Value IsNot Nothing) Then
                _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
                DgvBillingInfo.Rows(e.RowIndex).Cells("入金額").Value = 0
                Exit Sub
            End If
        End If

    End Sub

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

    'どんなカルチャーであっても、日本の形式に変換する
    Private Function strFormatDate(ByVal prmDate As String, Optional ByRef prmFormat As String = "yyyy/MM/dd") As String

        'PCのカルチャーを取得し、それに応じてStringからDatetimeを作成
        Dim ci As New System.Globalization.CultureInfo(CultureInfo.CurrentCulture.Name.ToString)
        Dim dateFormat As DateTime = DateTime.Parse(prmDate, ci, System.Globalization.DateTimeStyles.AssumeLocal)

        '日本の形式に書き換える
        Return dateFormat.ToString(prmFormat)
    End Function

    'どんなカルチャーであっても、日本の形式に変換する
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
        Return prmVal.ToString("F3", nfi) '売掛残高を増やす
    End Function

    'DGV内で指定列名(プルダウン)は一度のクリックで開く
    'それ以外は一回で入力状態にする
    Private Sub DgvDeposit_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DgvDeposit.CellEnter
        If DgvDeposit.Columns(e.ColumnIndex).Name = "入金種目" Then
            SendKeys.Send("{F4}")
            'Else
            '    SendKeys.Send("{F2}")
        End If
    End Sub

End Class