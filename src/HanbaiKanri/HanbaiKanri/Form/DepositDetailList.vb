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
    Private openDatetime As DateTime

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
        openDatetime = DateTime.Now
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True
        DgvBilling.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.DisplayedCells
    End Sub

    Private Sub DepositDetailList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If _status = "VIEW" Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "ViewMode"
            Else
                LblMode.Text = "参照モード"
            End If

        ElseIf _status = "CANCEL" Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
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
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
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
                If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                    DgvBilling.Columns.Add("取消", "Cancel")
                    DgvBilling.Columns.Add("入金番号", "MoneyReceiptNumber")
                    DgvBilling.Columns.Add("入金日", "MoneyReceiptDate")
                    DgvBilling.Columns.Add("請求先名", "BillingAddress")
                    DgvBilling.Columns.Add("振込先", "PaymentDestination")
                    DgvBilling.Columns.Add("入金額", "MoneyReceiptAmount")
                    DgvBilling.Columns.Add("更新日", "UpdateDate")
                    DgvBilling.Columns.Add("備考", "Remarks")
                Else
                    DgvBilling.Columns.Add("取消", "取消")
                    DgvBilling.Columns.Add("入金番号", "入金番号")
                    DgvBilling.Columns.Add("入金日", "入金日")
                    DgvBilling.Columns.Add("請求先名", "請求先名")
                    DgvBilling.Columns.Add("振込先", "振込先")
                    DgvBilling.Columns.Add("入金額", "入金額")
                    DgvBilling.Columns.Add("更新日", "更新日")
                    DgvBilling.Columns.Add("備考", "備考")
                End If

                '伝票単位時のセル書式
                DgvBilling.Columns("入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1

                    DgvBilling.Rows.Add()
                    DgvBilling.Rows(index).Cells("取消").Value = getDelKbnTxt(ds.Tables(RS).Rows(index)("取消区分"))
                    DgvBilling.Rows(index).Cells("入金番号").Value = ds.Tables(RS).Rows(index)("入金番号")
                    DgvBilling.Rows(index).Cells("入金日").Value = ds.Tables(RS).Rows(index)("入金日")
                    DgvBilling.Rows(index).Cells("請求先名").Value = ds.Tables(RS).Rows(index)("請求先名")
                    DgvBilling.Rows(index).Cells("振込先").Value = ds.Tables(RS).Rows(index)("振込先")
                    DgvBilling.Rows(index).Cells("入金額").Value = ds.Tables(RS).Rows(index)("入金額")
                    DgvBilling.Rows(index).Cells("更新日").Value = ds.Tables(RS).Rows(index)("更新日")
                    DgvBilling.Rows(index).Cells("備考").Value = ds.Tables(RS).Rows(index)("備考")
                Next

            Else '明細単位

                ds = getDsData("t27_nkinkshihd", Sql)

                If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                    DgvBilling.Columns.Add("取消", "Cancel")
                    DgvBilling.Columns.Add("入金番号", "MoneyReceiptNumber")
                    DgvBilling.Columns.Add("請求番号", "BillingNumber")
                    DgvBilling.Columns.Add("入金日", "MoneyReceiptDate")
                    DgvBilling.Columns.Add("請求先名", "BillingAddress")
                    DgvBilling.Columns.Add("入金額", "MoneyReceiptAmount")
                    DgvBilling.Columns.Add("備考", "Remarks")
                Else
                    DgvBilling.Columns.Add("取消", "取消")
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
                    DgvBilling.Rows(index).Cells("取消").Value = getDelKbnTxt(ds.Tables(RS).Rows(index)("取消区分"))
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
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
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

        '明細表示時は取消操作不可能
        If RbtnDetails.Checked Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Return

        End If

        Try

            '取消確認のアラート
            Dim result As DialogResult = _msgHd.dspMSG("confirmCancel", frmC01F10_Login.loginValue.Language)

            If result = DialogResult.Yes Then
                updateData() 'データ更新
            End If

        Catch ex As Exception

            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))

        End Try

    End Sub

    '選択データをもとに以下テーブル更新
    't25_nkinhd, t27_nkinkshihd, t23_skyuhd
    Private Sub updateData()
        Dim dtNow As DateTime = DateTime.Now
        Dim Sql As String = ""
        Dim ds As DataSet

        Sql = " AND"
        Sql += " 入金番号"
        Sql += "='"
        Sql += DgvBilling.Rows(DgvBilling.CurrentCell.RowIndex).Cells("入金番号").Value
        Sql += "' "

        '画面を開いた時から対象データに対して更新がされていないかどうか確認
        ds = getDsData("t25_nkinhd", Sql)

        If ds.Tables(RS).Rows(0)("更新日") = DgvBilling.Rows(DgvBilling.CurrentCell.RowIndex).Cells("更新日").Value Then

            Sql = "UPDATE "
            Sql += "Public."
            Sql += "t25_nkinhd "
            Sql += "SET "

            Sql += "取消区分 = '" & CommonConst.CANCEL_KBN_DISABLED & "'"
            Sql += ", "
            Sql += "取消日"
            Sql += " = '"
            Sql += dtNow
            Sql += "', "
            Sql += "更新者"
            Sql += " = '"
            Sql += frmC01F10_Login.loginValue.TantoNM
            Sql += "', "
            Sql += "更新日"
            Sql += " = '"
            Sql += dtNow
            Sql += "' "

            Sql += "WHERE"
            Sql += " 会社コード"
            Sql += "='"
            Sql += frmC01F10_Login.loginValue.BumonNM
            Sql += "'"
            Sql += " AND"
            Sql += " 入金番号"
            Sql += "='"
            Sql += DgvBilling.Rows(DgvBilling.CurrentCell.RowIndex).Cells("入金番号").Value
            Sql += "' "

            '入金基本を更新
            _db.executeDB(Sql)


            Sql = " AND"
            Sql += " 入金番号"
            Sql += "='"
            Sql += DgvBilling.Rows(DgvBilling.CurrentCell.RowIndex).Cells("入金番号").Value
            Sql += "' "

            '入金基本から入金額を取得
            Dim dsNkinhd As DataSet = getDsData("t25_nkinhd", Sql)
            Dim strNyukinGaku As Decimal = dsNkinhd.Tables(RS).Rows(0)("入金額")


            Sql = " AND"
            Sql += " 入金番号"
            Sql += "='"
            Sql += DgvBilling.Rows(DgvBilling.CurrentCell.RowIndex).Cells("入金番号").Value
            Sql += "' "

            '入金消込から請求番号を取得
            Dim dsNkinkshihd As DataSet = getDsData("t27_nkinkshihd", Sql)


            Sql = " AND"
            Sql += " 請求番号"
            Sql += "='"
            Sql += dsNkinkshihd.Tables(RS).Rows(0)("請求番号")
            Sql += "' "

            '請求基本から受注番号を取得
            Dim dsSkyuhd As DataSet = getDsData("t23_skyuhd", Sql)

            Dim decUrikakeZan As Decimal = dsSkyuhd.Tables(RS).Rows(0)("売掛残高") + strNyukinGaku
            Dim decNyukinKei As Decimal = dsSkyuhd.Tables(RS).Rows(0)("入金額計") - strNyukinGaku


            Sql = "UPDATE "
            Sql += "Public.t27_nkinkshihd "
            Sql += "SET "

            Sql += "取消区分 = '" & CommonConst.CANCEL_KBN_DISABLED & "'"
            Sql += ", "
            Sql += "取消日"
            Sql += " = '"
            Sql += dtNow
            Sql += "', "
            Sql += "更新者"
            Sql += " = '"
            Sql += frmC01F10_Login.loginValue.TantoNM
            Sql += "', "
            Sql += "更新日"
            Sql += " = '"
            Sql += dtNow
            Sql += "' "

            Sql += "WHERE"
            Sql += " 会社コード"
            Sql += "='"
            Sql += frmC01F10_Login.loginValue.BumonNM
            Sql += "'"
            Sql += " AND"
            Sql += " 入金番号"
            Sql += "='"
            Sql += DgvBilling.Rows(DgvBilling.CurrentCell.RowIndex).Cells("入金番号").Value
            Sql += "' "

            '請求基本を更新
            _db.executeDB(Sql)

            Sql = "UPDATE "
            Sql += "Public.t23_skyuhd "
            Sql += "SET "

            Sql += "売掛残高"
            Sql += " = '"
            Sql += decUrikakeZan.ToString '売掛残高を増やす
            Sql += "', "
            Sql += "入金額計"
            Sql += " = '"
            Sql += decNyukinKei.ToString '入金額計を減らす
            Sql += "', "
            Sql += "更新者"
            Sql += " = '"
            Sql += frmC01F10_Login.loginValue.TantoNM
            Sql += "' "

            Sql += "WHERE"
            Sql += " 会社コード"
            Sql += "='"
            Sql += frmC01F10_Login.loginValue.BumonNM
            Sql += "'"
            Sql += " AND"
            Sql += " 請求番号"
            Sql += "='"
            Sql += dsNkinkshihd.Tables(RS).Rows(0)("請求番号")
            Sql += "' "

            '請求基本を更新
            _db.executeDB(Sql)

            createDgvBilling()

        Else

            '画面を開いたときの日時とデータの日時が異なっていた場合
            'データが誰かに変更された旨を伝える
            _msgHd.dspMSG("chkData", frmC01F10_Login.loginValue.Language)

            '表示データを更新
            createDgvBilling()


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

End Class