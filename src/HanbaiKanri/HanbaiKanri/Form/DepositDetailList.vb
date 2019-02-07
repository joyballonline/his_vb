Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class DepositDetailList
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
                   Optional ByRef prmRefStatus As String = "")
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _status = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True
        DgvBilling.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.DisplayedCells
    End Sub

    Private Sub DepositDetailList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If _status = "VIEW" Then
            If frmC01F10_Login.loginValue.Language = "ENG" Then
                LblMode.Text = "ViewMode"
            Else
                LblMode.Text = "参照モード"
            End If

        ElseIf _status = "CANCEL" Then
            If frmC01F10_Login.loginValue.Language = "ENG" Then
                LblMode.Text = "CancelMode"
            Else
                LblMode.Text = "取消モード"
            End If

            BtnDepositCancel.Visible = True
            BtnDepositCancel.Location = New Point(997, 509)
        End If

        'データ描画
        createDgvBilling()

        '翻訳
        If frmC01F10_Login.loginValue.Language = "ENG" Then
            LblConditions.Text = "ExtractionCondition"
            Label1.Text = "CustomerName"
            Label4.Text = "CustomerCode"
            Label8.Text = "MoneyReceiptDate"
            Label7.Text = "MoneyReceiptNumber"
            Label10.Text = "DisplayFormat"
            RbtnSlip.Text = "UnitOfVoucher"

            RbtnDetails.Text = "UnitOfDetailData"
            RbtnDetails.Location = New Point(166, 196)
            ChkCancelData.Text = "IncludeCancelData"

            BtnDepositSearch.Text = "Search"
            BtnDepositCancel.Text = "CancelMfMoneyReceipt"
            BtnDepositView.Text = "MoneyReceiptView"
            BtnBack.Text = "Back"
        End If
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        Dim openForm As Form = Nothing
        openForm = New frmC01F30_Menu(_msgHd, _langHd, _db)
        openForm.Show()
        Me.Close()
    End Sub

    'DgvBilling内を再描画
    Private Sub createDgvBilling()
        clearDGV() 'テーブルクリア
        Dim Sql As String = ""

        Sql += searchConditions() '抽出条件取得
        Sql += viewFormat() '表示形式条件

        Sql += " ORDER BY "
        Sql += "更新日 DESC"

        Try

            '伝票単位
            If RbtnSlip.Checked Then

                ds = getDsData("t25_nkinhd", Sql)

                '英語の表記
                If frmC01F10_Login.loginValue.Language = "ENG" Then
                    DgvBilling.Columns.Add("入金番号", "MoneyReceiptNumber")
                    DgvBilling.Columns.Add("入金日", "MoneyReceiptDate")
                    DgvBilling.Columns.Add("請求先名", "BillingAddress")
                    DgvBilling.Columns.Add("振込先", "PaymentDestination")
                    DgvBilling.Columns.Add("入金額", "MoneyReceiptAmount")
                    DgvBilling.Columns.Add("備考", "Remarks")
                Else
                    DgvBilling.Columns.Add("入金番号", "入金番号")
                    DgvBilling.Columns.Add("入金日", "入金日")
                    DgvBilling.Columns.Add("請求先名", "請求先名")
                    DgvBilling.Columns.Add("振込先", "振込先")
                    DgvBilling.Columns.Add("入金額", "入金額")
                    DgvBilling.Columns.Add("備考", "備考")
                End If

                '伝票単位時のセル書式
                DgvBilling.Columns("入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                    DgvBilling.Rows.Add()
                    DgvBilling.Rows(index).Cells("入金番号").Value = ds.Tables(RS).Rows(index)("入金番号")
                    DgvBilling.Rows(index).Cells("入金日").Value = ds.Tables(RS).Rows(index)("登録日")
                    DgvBilling.Rows(index).Cells("請求先名").Value = ds.Tables(RS).Rows(index)("請求先名")
                    DgvBilling.Rows(index).Cells("振込先").Value = ds.Tables(RS).Rows(index)("振込先")
                    DgvBilling.Rows(index).Cells("入金額").Value = ds.Tables(RS).Rows(index)("入金額")
                    DgvBilling.Rows(index).Cells("備考").Value = ds.Tables(RS).Rows(index)("備考")
                Next

            Else '明細単位

                ds = getDsData("t27_nkinkshihd", Sql)

                If frmC01F10_Login.loginValue.Language = "ENG" Then
                    DgvBilling.Columns.Add("入金番号", "MoneyReceiptNumber")
                    DgvBilling.Columns.Add("請求番号", "BillingNumber")
                    DgvBilling.Columns.Add("入金日", "MoneyReceiptDate")
                    DgvBilling.Columns.Add("請求先名", "BillingAddress")
                    DgvBilling.Columns.Add("入金額", "MoneyReceiptAmount")
                    DgvBilling.Columns.Add("備考", "Remarks")
                Else
                    DgvBilling.Columns.Add("入金番号", "入金番号")
                    DgvBilling.Columns.Add("請求番号", "請求番号")
                    DgvBilling.Columns.Add("入金日", "入金日")
                    DgvBilling.Columns.Add("請求先名", "請求先名")
                    DgvBilling.Columns.Add("入金額", "入金額")
                    DgvBilling.Columns.Add("備考", "備考")
                End If

                '伝票単位時のセル書式
                DgvBilling.Columns("入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                    DgvBilling.Rows.Add()
                    DgvBilling.Rows(index).Cells("入金番号").Value = ds.Tables(RS).Rows(index)("入金番号")
                    DgvBilling.Rows(index).Cells("請求番号").Value = ds.Tables(RS).Rows(index)("請求番号")
                    DgvBilling.Rows(index).Cells("入金日").Value = ds.Tables(RS).Rows(index)("入金日")
                    DgvBilling.Rows(index).Cells("請求先名").Value = ds.Tables(RS).Rows(index)("請求先名")
                    DgvBilling.Rows(index).Cells("入金額").Value = ds.Tables(RS).Rows(index)("入金消込額計")
                    DgvBilling.Rows(index).Cells("備考").Value = ds.Tables(RS).Rows(index)("備考")
                Next
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '表示形式を切り替えたら
    Private Sub RbtnDetails_CheckedChanged(sender As Object, e As EventArgs) Handles RbtnDetails.CheckedChanged
        createDgvBilling()
    End Sub

    '検索ボタンをクリックしたら
    Private Sub BtnPurchaseSearch_Click(sender As Object, e As EventArgs) Handles BtnDepositSearch.Click
        createDgvBilling()
    End Sub

    'Private Sub BtnBillingView_Click(sender As Object, e As EventArgs) Handles BtnBillingView.Click
    '    Dim RowIdx As Integer
    '    RowIdx = Me.DgvBilling.CurrentCell.RowIndex
    '    Dim No As String = DgvBilling.Rows(RowIdx).Cells("受注番号").Value
    '    Dim Suffix As String = DgvBilling.Rows(RowIdx).Cells("受注番号枝番").Value
    '    Dim Status As String = "VIEW"
    '    Dim openForm As Form = Nothing
    '    openForm = New DepositManagement(_msgHd, _db, _langHd, Me, No, Suffix, Status)   '処理選択
    '    openForm.Show(Me)
    'End Sub

    '「取消データを含める」変更イベント取得時
    Private Sub ChkCancelData_CheckedChanged(sender As Object, e As EventArgs) Handles ChkCancelData.CheckedChanged
        createDgvBilling()
    End Sub

    '入金取消処理
    Private Sub BtnBillingCancel_Click(sender As Object, e As EventArgs) Handles BtnDepositCancel.Click
        Dim dtNow As DateTime = DateTime.Now
        Dim Sql1 As String = ""
        Sql1 = ""
        Sql1 += "UPDATE "
        Sql1 += "Public."
        Sql1 += "t25_nkinhd "
        Sql1 += "SET "

        Sql1 += "取消区分"
        Sql1 += " = '"
        Sql1 += "1"
        Sql1 += "', "
        Sql1 += "取消日"
        Sql1 += " = '"
        Sql1 += dtNow
        Sql1 += "', "
        Sql1 += "更新者"
        Sql1 += " = '"
        Sql1 += frmC01F10_Login.loginValue.TantoNM
        Sql1 += "', "
        Sql1 += "更新日"
        Sql1 += " = '"
        Sql1 += dtNow
        Sql1 += "' "

        Sql1 += "WHERE"
        Sql1 += " 会社コード"
        Sql1 += "='"
        Sql1 += frmC01F10_Login.loginValue.BumonNM
        Sql1 += "'"
        Sql1 += " AND"
        Sql1 += " 入金番号"
        Sql1 += "='"
        Sql1 += DgvBilling.Rows(DgvBilling.CurrentCell.RowIndex).Cells("入金番号").Value
        Sql1 += "' "
        Sql1 += "RETURNING 会社コード"
        Sql1 += ", "
        Sql1 += "入金番号"
        Sql1 += ", "
        Sql1 += "入金日"
        Sql1 += ", "
        Sql1 += "請求先コード"
        Sql1 += ", "
        Sql1 += "請求先名"
        Sql1 += ", "
        Sql1 += "請求金額"
        Sql1 += ", "
        Sql1 += "入金額計"
        Sql1 += ", "
        Sql1 += "請求残高"
        Sql1 += ", "
        Sql1 += "備考"
        Sql1 += ", "
        Sql1 += "振込先"
        Sql1 += ", "
        Sql1 += "入金額"
        Sql1 += ", "
        Sql1 += "取消日"
        Sql1 += ", "
        Sql1 += "取消区分"
        Sql1 += ", "
        Sql1 += "登録日"
        Sql1 += ", "
        Sql1 += "更新者"
        Sql1 += ", "
        Sql1 += "更新日"

        If frmC01F10_Login.loginValue.Language = "ENG" Then
            Dim result As DialogResult = MessageBox.Show("Would you like to cancel the Deposit?",
                                             "Question",
                                             MessageBoxButtons.YesNoCancel,
                                             MessageBoxIcon.Exclamation,
                                             MessageBoxDefaultButton.Button2)

            If result = DialogResult.Yes Then
                _db.executeDB(Sql1)
                createDgvBilling()
            ElseIf result = DialogResult.No Then

            ElseIf result = DialogResult.Cancel Then

            End If
        Else
            Dim result As DialogResult = MessageBox.Show("入金を取り消しますか？",
                                             "質問",
                                             MessageBoxButtons.YesNoCancel,
                                             MessageBoxIcon.Exclamation,
                                             MessageBoxDefaultButton.Button2)

            If result = DialogResult.Yes Then
                _db.executeDB(Sql1)
                createDgvBilling()
            ElseIf result = DialogResult.No Then

            ElseIf result = DialogResult.Cancel Then

            End If
        End If

    End Sub

    'テーブルをクリア
    Private Sub clearDGV()
        DgvBilling.Rows.Clear()
        DgvBilling.Columns.Clear()
    End Sub

    '抽出条件取得
    Private Function searchConditions() As String
        Dim Sql As String = ""

        '抽出条件
        Dim customerName As String = TxtCustomerName.Text
        Dim customerCode As String = TxtCustomerCode.Text
        Dim sinceDate As String = TxtBillingDate1.Text
        Dim untilDate As String = TxtBillingDate2.Text
        Dim sinceNum As String = TxtBillingNo1.Text
        Dim untilNum As String = TxtBillingNo2.Text

        If customerName <> Nothing Then
            Sql += " AND "
            Sql += " 請求先名 ILIKE '%" & customerName & "%' "
        End If

        If customerCode <> Nothing Then
            Sql += " AND "
            Sql += " 請求先コード ILIKE '%" & customerCode & "%' "
        End If

        If sinceDate <> Nothing Then
            Sql += " AND "
            Sql += " 入金日 >= '" & sinceDate & "'"
        End If
        If untilDate <> Nothing Then
            Sql += " AND "
            Sql += " 入金日 <= '" & untilDate & "'"
        End If

        If sinceNum <> Nothing Then
            Sql += " AND "
            Sql += " 入金番号 >= '" & sinceNum & "' "
        End If
        If untilNum <> Nothing Then
            Sql += " AND "
            Sql += " 入金番号 <= '" & untilNum & "' "
        End If

        Return Sql

    End Function

    '表示形式条件
    Private Function viewFormat() As String
        Dim Sql As String = ""

        '取消データを含めない場合
        If ChkCancelData.Checked = False Then
            Sql += " AND "
            Sql += " 取消区分 = 0 "
        End If

        Return Sql

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
        Sql += "'" & frmC01F10_Login.loginValue.BumonNM & "'"
        Sql += txtParam

        Return _db.selectDB(Sql, RS, reccnt)
    End Function

End Class