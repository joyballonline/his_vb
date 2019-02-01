Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class PaidList
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
    Private OrderNo As String()
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
        DgvHtyhd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.DisplayedCells
    End Sub
    Private Sub PurchaseListLoad(Optional ByRef Status As String = "")
        If Status = "EXCLUSION" Then
            Dim Sql As String = ""
            Try
                Sql += "SELECT "
                Sql += "* "
                Sql += "FROM "
                Sql += "public"
                Sql += "."
                Sql += "t47_shrihd"
                Sql += " WHERE "
                Sql += "取消区分"
                Sql += " = "
                Sql += "'"
                Sql += "0"
                Sql += "'"
                Sql += " ORDER BY "
                Sql += "登録日 DESC"

                Dim reccnt As Integer = 0
                ds = _db.selectDB(Sql, RS, reccnt)

                If frmC01F10_Login.loginValue.Language = "ENG" Then
                    DgvHtyhd.Columns.Add("支払番号", "PaymentNumber")
                    DgvHtyhd.Columns.Add("支払日", "PaymentDate")
                    DgvHtyhd.Columns.Add("支払先名", "SupplierName")
                    DgvHtyhd.Columns.Add("支払先", "PaymentDestination")
                    DgvHtyhd.Columns.Add("買掛金額", "AccountsPayableAmount")
                    DgvHtyhd.Columns.Add("支払金額計", "TotalPaymentAmount")
                    DgvHtyhd.Columns.Add("買掛残高", "AccountsPayableBalance")
                    DgvHtyhd.Columns.Add("備考", "Remarks")
                Else
                    DgvHtyhd.Columns.Add("支払番号", "支払番号")
                    DgvHtyhd.Columns.Add("支払日", "支払日")
                    DgvHtyhd.Columns.Add("支払先名", "支払先名")
                    DgvHtyhd.Columns.Add("支払先", "支払先")
                    DgvHtyhd.Columns.Add("買掛金額", "買掛金額")
                    DgvHtyhd.Columns.Add("支払金額計", "支払金額計")
                    DgvHtyhd.Columns.Add("買掛残高", "買掛残高")
                    DgvHtyhd.Columns.Add("備考", "備考")
                End If

                DgvHtyhd.Columns("買掛金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvHtyhd.Columns("支払金額計").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvHtyhd.Columns("買掛残高").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                    DgvHtyhd.Rows.Add()
                    DgvHtyhd.Rows(index).Cells("支払番号").Value = ds.Tables(RS).Rows(index)("支払番号")
                    DgvHtyhd.Rows(index).Cells("支払日").Value = ds.Tables(RS).Rows(index)("支払日")
                    DgvHtyhd.Rows(index).Cells("支払先名").Value = ds.Tables(RS).Rows(index)("支払先名")
                    DgvHtyhd.Rows(index).Cells("支払先").Value = ds.Tables(RS).Rows(index)("支払先")
                    DgvHtyhd.Rows(index).Cells("買掛金額").Value = ds.Tables(RS).Rows(index)("買掛金額")
                    DgvHtyhd.Rows(index).Cells("支払金額計").Value = ds.Tables(RS).Rows(index)("支払金額計")
                    DgvHtyhd.Rows(index).Cells("買掛残高").Value = ds.Tables(RS).Rows(index)("買掛残高")
                    DgvHtyhd.Rows(index).Cells("備考").Value = ds.Tables(RS).Rows(index)("備考")
                Next

            Catch ue As UsrDefException
                ue.dspMsg()
                Throw ue
            Catch ex As Exception
                'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
            End Try
        Else
            Dim Sql As String = ""
            Try
                Sql += "SELECT "
                Sql += "* "
                Sql += "FROM "
                Sql += "public"
                Sql += "."
                Sql += "t47_shrihd"
                Sql += " ORDER BY "
                Sql += "登録日 DESC"

                Dim reccnt As Integer = 0
                ds = _db.selectDB(Sql, RS, reccnt)

                If frmC01F10_Login.loginValue.Language = "ENG" Then
                    DgvHtyhd.Columns.Add("支払番号", "PaymentNumber")
                    DgvHtyhd.Columns.Add("支払日", "PaymentDate")
                    DgvHtyhd.Columns.Add("支払先名", "SupplierName")
                    DgvHtyhd.Columns.Add("支払先", "PaymentDestination")
                    DgvHtyhd.Columns.Add("買掛金額", "AccountsPayableAmount")
                    DgvHtyhd.Columns.Add("支払金額計", "TotalPaymentAmount")
                    DgvHtyhd.Columns.Add("買掛残高", "AccountsPayableBalance")
                    DgvHtyhd.Columns.Add("備考", "Remarks")
                Else
                    DgvHtyhd.Columns.Add("支払番号", "支払番号")
                    DgvHtyhd.Columns.Add("支払日", "支払日")
                    DgvHtyhd.Columns.Add("支払先名", "支払先名")
                    DgvHtyhd.Columns.Add("支払先", "支払先")
                    DgvHtyhd.Columns.Add("買掛金額", "買掛金額")
                    DgvHtyhd.Columns.Add("支払金額計", "支払金額計")
                    DgvHtyhd.Columns.Add("買掛残高", "買掛残高")
                    DgvHtyhd.Columns.Add("備考", "備考")
                End If

                DgvHtyhd.Columns("買掛金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvHtyhd.Columns("支払金額計").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvHtyhd.Columns("買掛残高").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                    DgvHtyhd.Rows.Add()
                    DgvHtyhd.Rows(index).Cells("支払番号").Value = ds.Tables(RS).Rows(index)("支払番号")
                    DgvHtyhd.Rows(index).Cells("支払日").Value = ds.Tables(RS).Rows(index)("支払日")
                    DgvHtyhd.Rows(index).Cells("支払先名").Value = ds.Tables(RS).Rows(index)("支払先名")
                    DgvHtyhd.Rows(index).Cells("支払先").Value = ds.Tables(RS).Rows(index)("支払先")
                    DgvHtyhd.Rows(index).Cells("買掛金額").Value = ds.Tables(RS).Rows(index)("買掛金額")
                    DgvHtyhd.Rows(index).Cells("支払金額計").Value = ds.Tables(RS).Rows(index)("支払金額計")
                    DgvHtyhd.Rows(index).Cells("買掛残高").Value = ds.Tables(RS).Rows(index)("買掛残高")
                    DgvHtyhd.Rows(index).Cells("備考").Value = ds.Tables(RS).Rows(index)("備考")
                Next

            Catch ue As UsrDefException
                ue.dspMsg()
                Throw ue
            Catch ex As Exception
                'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
            End Try
        End If
    End Sub
    Private Sub MstHanyoue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If _status = "CANCEL" Then
            If frmC01F10_Login.loginValue.Language = "ENG" Then
                LblMode.Text = "CancelMode"
            Else
                LblMode.Text = "取消モード"
            End If

            BtnCancel.Visible = True
        Else
            If frmC01F10_Login.loginValue.Language = "ENG" Then
                LblMode.Text = "ViewMode"
            Else
                LblMode.Text = "参照モード"
            End If
        End If

        Dim Status As String = "EXCLUSION"
        PurchaseListLoad(Status)

        If frmC01F10_Login.loginValue.Language = "ENG" Then
            LblConditions.Text = "ExtractionCondition"
            Label1.Text = "SupplierName"
            Label4.Text = "SupplierCode"
            Label8.Text = "PaymentDate"
            Label7.Text = "PaymentNumber"
            Label10.Text = "DisplayFormat"
            RbtnSlip.Text = "UnitOfVoucher"

            RbtnDetails.Text = "LineItemUnit"
            RbtnDetails.Location = New Point(166, 196)
            ChkCancelData.Text = "IncludeCancelData"

            BtnPaymentSearch.Text = "Search"
            BtnCancel.Text = "CancelOfPayment"
            BtnBack.Text = "Back"
        End If
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        Dim openForm As Form = Nothing
        openForm = New frmC01F30_Menu(_msgHd, _langHd, _db)
        openForm.Show()
        Me.Close()
    End Sub

    Private Sub RbtnDetails_CheckedChanged(sender As Object, e As EventArgs) Handles RbtnDetails.CheckedChanged
        DgvHtyhd.Rows.Clear()
        DgvHtyhd.Columns.Clear()

        If RbtnSlip.Checked Then
            Dim Sql As String = ""
            Sql += "SELECT "
            Sql += " * "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t47_shrihd"
            Sql += " WHERE "
            Sql += "取消区分"
            Sql += " = "
            Sql += "'"
            Sql += "0"
            Sql += "'"

            If OrderNo IsNot Nothing Then
                For i As Integer = 0 To OrderNo.Length - 1
                    If i = 0 Then
                        Sql += " WHERE "
                        Sql += "支払番号"
                        Sql += " ILIKE "
                        Sql += "'%"
                        Sql += OrderNo(i)
                        Sql += "%'"
                    Else
                        Sql += " OR "
                        Sql += "支払番号"
                        Sql += " ILIKE "
                        Sql += "'%"
                        Sql += OrderNo(i)
                        Sql += "%'"
                    End If
                Next
            End If
            Sql += " ORDER BY "
            Sql += "登録日 DESC"


            Dim reccnt As Integer = 0
            ds = _db.selectDB(Sql, RS, reccnt)

            If frmC01F10_Login.loginValue.Language = "ENG" Then
                DgvHtyhd.Columns.Add("支払番号", "PaymentNumber")
                DgvHtyhd.Columns.Add("支払日", "PaymentDate")
                DgvHtyhd.Columns.Add("支払先名", "SupplierName")
                DgvHtyhd.Columns.Add("支払先", "PaymentDestination")
                DgvHtyhd.Columns.Add("買掛金額", "AccountsPayableAmount")
                DgvHtyhd.Columns.Add("支払金額計", "TotalPaymentAmount")
                DgvHtyhd.Columns.Add("買掛残高", "AccountsPayableBalance")
                DgvHtyhd.Columns.Add("備考", "Remarks")
            Else
                DgvHtyhd.Columns.Add("支払番号", "支払番号")
                DgvHtyhd.Columns.Add("支払日", "支払日")
                DgvHtyhd.Columns.Add("支払先名", "支払先名")
                DgvHtyhd.Columns.Add("支払先", "支払先")
                DgvHtyhd.Columns.Add("買掛金額", "買掛金額")
                DgvHtyhd.Columns.Add("支払金額計", "支払金額計")
                DgvHtyhd.Columns.Add("買掛残高", "買掛残高")
                DgvHtyhd.Columns.Add("備考", "備考")
            End If

            DgvHtyhd.Columns("買掛金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvHtyhd.Columns("支払金額計").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvHtyhd.Columns("買掛残高").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvHtyhd.Rows.Add()
                DgvHtyhd.Rows(index).Cells("支払番号").Value = ds.Tables(RS).Rows(index)("支払番号")
                DgvHtyhd.Rows(index).Cells("支払日").Value = ds.Tables(RS).Rows(index)("支払日")
                DgvHtyhd.Rows(index).Cells("支払先名").Value = ds.Tables(RS).Rows(index)("支払先名")
                DgvHtyhd.Rows(index).Cells("支払先").Value = ds.Tables(RS).Rows(index)("支払先")
                DgvHtyhd.Rows(index).Cells("買掛金額").Value = ds.Tables(RS).Rows(index)("買掛金額")
                DgvHtyhd.Rows(index).Cells("支払金額計").Value = ds.Tables(RS).Rows(index)("支払金額計")
                DgvHtyhd.Rows(index).Cells("買掛残高").Value = ds.Tables(RS).Rows(index)("買掛残高")
                DgvHtyhd.Rows(index).Cells("備考").Value = ds.Tables(RS).Rows(index)("備考")
            Next
        Else
            Dim Sql As String = ""

            Sql += "SELECT "
            Sql += " * "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t49_shrikshihd"

            If OrderNo IsNot Nothing Then
                For i As Integer = 0 To OrderNo.Length - 1
                    If i = 0 Then
                        Sql += " WHERE "
                        Sql += "支払番号"
                        Sql += " ILIKE "
                        Sql += "'%"
                        Sql += OrderNo(i)
                        Sql += "%'"
                    Else
                        Sql += " OR "
                        Sql += "支払番号"
                        Sql += " ILIKE "
                        Sql += "'%"
                        Sql += OrderNo(i)
                        Sql += "%'"
                    End If
                Next
            End If
            Sql += " ORDER BY "
            Sql += "更新日 DESC"

            Dim reccnt As Integer = 0
            ds = _db.selectDB(Sql, RS, reccnt)

            If frmC01F10_Login.loginValue.Language = "ENG" Then
                DgvHtyhd.Columns.Add("支払番号", "PaymentNumber")
                DgvHtyhd.Columns.Add("買掛番号", "AccountsPayableNumber")
                DgvHtyhd.Columns.Add("支払先名", "SupplierName")
                DgvHtyhd.Columns.Add("支払金額", "PaymentAmount")
                DgvHtyhd.Columns.Add("支払日", "PaymentDate")
                DgvHtyhd.Columns.Add("備考", "Remarks")
            Else
                DgvHtyhd.Columns.Add("支払番号", "支払番号")
                DgvHtyhd.Columns.Add("買掛番号", "買掛番号")
                DgvHtyhd.Columns.Add("支払先名", "支払先名")
                DgvHtyhd.Columns.Add("支払金額", "支払金額")
                DgvHtyhd.Columns.Add("支払日", "支払日")
                DgvHtyhd.Columns.Add("備考", "備考")
            End If



            DgvHtyhd.Columns("支払金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvHtyhd.Rows.Add()
                DgvHtyhd.Rows(index).Cells("支払番号").Value = ds.Tables(RS).Rows(index)("支払番号")
                DgvHtyhd.Rows(index).Cells("買掛番号").Value = ds.Tables(RS).Rows(index)("買掛番号")
                DgvHtyhd.Rows(index).Cells("支払先名").Value = ds.Tables(RS).Rows(index)("支払先名")
                DgvHtyhd.Rows(index).Cells("支払金額").Value = ds.Tables(RS).Rows(index)("支払消込額計")
                DgvHtyhd.Rows(index).Cells("支払日").Value = ds.Tables(RS).Rows(index)("支払日")
                DgvHtyhd.Rows(index).Cells("備考").Value = ds.Tables(RS).Rows(index)("備考")
            Next
        End If
    End Sub

    Private Sub ChkCancelData_CheckedChanged(sender As Object, e As EventArgs) Handles ChkCancelData.CheckedChanged
        DgvHtyhd.Rows.Clear()
        DgvHtyhd.Columns.Clear()

        If ChkCancelData.Checked = False Then
            Dim Status As String = "EXCLUSION"
            PurchaseListLoad(Status)
        Else
            PurchaseListLoad()
        End If
    End Sub

    Private Sub BtnPurchaseSearch_Click(sender As Object, e As EventArgs) Handles BtnPaymentSearch.Click
        DgvHtyhd.Rows.Clear()
        DgvHtyhd.Columns.Clear()

        Dim count As Integer = 0
        Dim Sql As String = ""
        Try
            Sql += "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t47_shrihd"
            If TxtSupplierName.Text = "" Then
            Else
                Sql += " WHERE "
                Sql += "支払先名"
                Sql += " ILIKE "
                Sql += "'%"
                Sql += TxtSupplierName.Text
                Sql += "%'"
                count += 1
            End If
            If TxtSupplierCode.Text = "" Then
            Else
                If count > 0 Then
                    Sql += " AND "
                    Sql += "支払先コード"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtSupplierCode.Text
                    Sql += "%'"
                Else
                    Sql += " WHERE "
                    Sql += "支払先コード"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtSupplierCode.Text
                    Sql += "%'"
                    count += 1
                End If
            End If
            If TxtPaidDate1.Text = "" Then
                If TxtPaidDate2.Text = "" Then
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "支払日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtPaidDate2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "支払日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtPaidDate2.Text
                        Sql += "'"
                        count += 1
                    End If
                End If
            Else
                If TxtPaidDate2.Text = "" Then
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "支払日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtPaidDate1.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "支払日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtPaidDate1.Text
                        Sql += "'"
                        count += 1
                    End If
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "支払日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtPaidDate1.Text
                        Sql += "' "
                        Sql += "AND  "
                        Sql += "支払日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtPaidDate2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "支払日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtPaidDate1.Text
                        Sql += "' "
                        Sql += "AND  "
                        Sql += "支払日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtPaidDate2.Text
                        Sql += "'"
                        count += 1
                    End If
                End If
            End If
            If TxtPaidNo1.Text = "" Then
                If TxtPaidNo2.Text = "" Then
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "支払番号"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtPaidNo2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "支払番号"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtPaidNo2.Text
                        Sql += "'"
                        count += 1
                    End If
                End If
            Else
                If TxtPaidNo2.Text = "" Then
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "支払番号"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtPaidNo1.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "支払番号"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtPaidNo1.Text
                        Sql += "'"
                        count += 1
                    End If
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "支払番号"
                        Sql += " >= "
                        Sql += "'"
                        Sql += TxtPaidNo1.Text
                        Sql += "' "
                        Sql += "AND "
                        Sql += "支払番号"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtPaidNo2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "支払番号"
                        Sql += " >= "
                        Sql += "'"
                        Sql += TxtPaidNo1.Text
                        Sql += "' "
                        Sql += "AND  "
                        Sql += "支払番号"
                        Sql += " <= "
                        Sql += "'"
                        Sql += TxtPaidNo2.Text
                        Sql += "'"
                        count += 1
                    End If
                End If
            End If
            Sql += " ORDER BY "
            Sql += "登録日 DESC"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            If frmC01F10_Login.loginValue.Language = "ENG" Then
                DgvHtyhd.Columns.Add("支払番号", "PaymentNumber")
                DgvHtyhd.Columns.Add("支払日", "PaymentDate")
                DgvHtyhd.Columns.Add("支払先名", "SupplierName")
                DgvHtyhd.Columns.Add("支払先", "PaymentDestination")
                DgvHtyhd.Columns.Add("買掛金額", "AccountsPayableAmount")
                DgvHtyhd.Columns.Add("支払金額計", "TotalPaymentAmount")
                DgvHtyhd.Columns.Add("買掛残高", "AccountsPayableBalance")
                DgvHtyhd.Columns.Add("備考", "Remarks")
            Else
                DgvHtyhd.Columns.Add("支払番号", "支払番号")
                DgvHtyhd.Columns.Add("支払日", "支払日")
                DgvHtyhd.Columns.Add("支払先名", "支払先名")
                DgvHtyhd.Columns.Add("支払先", "支払先")
                DgvHtyhd.Columns.Add("買掛金額", "買掛金額")
                DgvHtyhd.Columns.Add("支払金額計", "支払金額計")
                DgvHtyhd.Columns.Add("買掛残高", "買掛残高")
                DgvHtyhd.Columns.Add("備考", "備考")
            End If

            DgvHtyhd.Columns("買掛金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvHtyhd.Columns("支払金額計").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvHtyhd.Columns("買掛残高").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvHtyhd.Rows.Add()
                DgvHtyhd.Rows(index).Cells("支払番号").Value = ds.Tables(RS).Rows(index)("支払番号")
                DgvHtyhd.Rows(index).Cells("支払日").Value = ds.Tables(RS).Rows(index)("支払日")
                DgvHtyhd.Rows(index).Cells("支払先名").Value = ds.Tables(RS).Rows(index)("支払先名")
                DgvHtyhd.Rows(index).Cells("支払先").Value = ds.Tables(RS).Rows(index)("支払先")
                DgvHtyhd.Rows(index).Cells("買掛金額").Value = ds.Tables(RS).Rows(index)("買掛金額")
                DgvHtyhd.Rows(index).Cells("支払金額計").Value = ds.Tables(RS).Rows(index)("支払金額計")
                DgvHtyhd.Rows(index).Cells("買掛残高").Value = ds.Tables(RS).Rows(index)("買掛残高")
                DgvHtyhd.Rows(index).Cells("備考").Value = ds.Tables(RS).Rows(index)("備考")
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub


    Private Sub BtnPurchaseCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Dim dtNow As DateTime = DateTime.Now
        Dim Sql1 As String = ""
        Sql1 = ""
        Sql1 += "UPDATE "
        Sql1 += "Public."
        Sql1 += "t47_shrihd "
        Sql1 += "SET "

        Sql1 += "取消区分"
        Sql1 += " = '"
        Sql1 += "1"
        Sql1 += "', "
        Sql1 += "取消日"
        Sql1 += " = '"
        Sql1 += dtNow
        Sql1 += "', "
        Sql1 += "更新日"
        Sql1 += " = '"
        Sql1 += dtNow
        Sql1 += "', "
        Sql1 += "更新者"
        Sql1 += " = '"
        Sql1 += frmC01F10_Login.loginValue.TantoNM
        Sql1 += " ' "

        Sql1 += "WHERE"
        Sql1 += " 会社コード"
        Sql1 += "='"
        Sql1 += frmC01F10_Login.loginValue.BumonNM
        Sql1 += "'"
        Sql1 += " AND"
        Sql1 += " 支払番号"
        Sql1 += "='"
        Sql1 += DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("支払番号").Value
        Sql1 += "' "

        Sql1 += "RETURNING 会社コード"
        Sql1 += ", "
        Sql1 += "支払番号"
        Sql1 += ", "
        Sql1 += "支払日"
        Sql1 += ", "
        Sql1 += "支払先コード"
        Sql1 += ", "
        Sql1 += "支払先名"
        Sql1 += ", "
        Sql1 += "買掛金額"
        Sql1 += ", "
        Sql1 += "支払金額計"
        Sql1 += ", "
        Sql1 += "買掛残高"
        Sql1 += ", "
        Sql1 += "備考"
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
        Sql1 += ", "
        Sql1 += "支払先"

        If frmC01F10_Login.loginValue.Language = "ENG" Then
            Dim result As DialogResult = MessageBox.Show("Would you like to cancel the Paymnt?",
                                             "Question",
                                             MessageBoxButtons.YesNoCancel,
                                             MessageBoxIcon.Exclamation,
                                             MessageBoxDefaultButton.Button2)

            If result = DialogResult.Yes Then
                _db.executeDB(Sql1)
                DgvHtyhd.Rows.Clear()
                DgvHtyhd.Columns.Clear()
                Dim Status As String = "EXCLUSION"
                PurchaseListLoad(Status)
            ElseIf result = DialogResult.No Then

            ElseIf result = DialogResult.Cancel Then

            End If
        Else
            Dim result As DialogResult = MessageBox.Show("支払情報を取り消しますか？",
                                             "質問",
                                             MessageBoxButtons.YesNoCancel,
                                             MessageBoxIcon.Exclamation,
                                             MessageBoxDefaultButton.Button2)

            If result = DialogResult.Yes Then
                _db.executeDB(Sql1)
                DgvHtyhd.Rows.Clear()
                DgvHtyhd.Columns.Clear()
                Dim Status As String = "EXCLUSION"
                PurchaseListLoad(Status)
            ElseIf result = DialogResult.No Then

            ElseIf result = DialogResult.Cancel Then

            End If
        End If


    End Sub
End Class