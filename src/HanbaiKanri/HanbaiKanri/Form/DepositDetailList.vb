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
    Private Sub PurchaseListLoad(Optional ByRef Status As String = "")
        If Status = "EXCLUSION" Then
            Dim Sql As String = ""
            Try
                Sql += "SELECT "
                Sql += "* "
                Sql += "FROM "
                Sql += "public"
                Sql += "."
                Sql += "t25_nkinhd"
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
                DgvBilling.Columns.Add("入金番号", "入金番号")
                DgvBilling.Columns.Add("入金日", "入金日")
                DgvBilling.Columns.Add("請求先名", "請求先名")
                DgvBilling.Columns.Add("振込先", "振込先")
                DgvBilling.Columns.Add("請求金額", "請求金額")
                DgvBilling.Columns.Add("入金額", "入金額")
                DgvBilling.Columns.Add("入金額計", "入金額計")
                DgvBilling.Columns.Add("請求残高", "請求残高")
                DgvBilling.Columns.Add("備考", "備考")


                DgvBilling.Columns("請求金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvBilling.Columns("請求残高").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvBilling.Columns("入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvBilling.Columns("入金額計").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                    DgvBilling.Rows.Add()
                    DgvBilling.Rows(index).Cells("入金番号").Value = ds.Tables(RS).Rows(index)("入金番号")
                    DgvBilling.Rows(index).Cells("入金日").Value = ds.Tables(RS).Rows(index)("入金日")
                    DgvBilling.Rows(index).Cells("請求先名").Value = ds.Tables(RS).Rows(index)("請求先名")
                    DgvBilling.Rows(index).Cells("振込先").Value = ds.Tables(RS).Rows(index)("振込先")
                    DgvBilling.Rows(index).Cells("請求金額").Value = ds.Tables(RS).Rows(index)("請求金額")
                    DgvBilling.Rows(index).Cells("入金額").Value = ds.Tables(RS).Rows(index)("入金額")
                    DgvBilling.Rows(index).Cells("入金額計").Value = ds.Tables(RS).Rows(index)("入金額計")
                    DgvBilling.Rows(index).Cells("請求残高").Value = ds.Tables(RS).Rows(index)("請求残高")
                    DgvBilling.Rows(index).Cells("備考").Value = ds.Tables(RS).Rows(index)("備考")
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
                Sql += "t25_nkinhd"
                Sql += " ORDER BY "
                Sql += "登録日 DESC"

                Dim reccnt As Integer = 0
                ds = _db.selectDB(Sql, RS, reccnt)

                DgvBilling.Columns.Add("入金番号", "入金番号")
                DgvBilling.Columns.Add("入金日", "入金日")
                DgvBilling.Columns.Add("請求先名", "請求先名")
                DgvBilling.Columns.Add("振込先", "振込先")
                DgvBilling.Columns.Add("請求金額", "請求金額")
                DgvBilling.Columns.Add("入金額", "入金額")
                DgvBilling.Columns.Add("入金額計", "入金額計")
                DgvBilling.Columns.Add("請求残高", "請求残高")
                DgvBilling.Columns.Add("備考", "備考")

                DgvBilling.Columns("請求金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvBilling.Columns("請求残高").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvBilling.Columns("入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvBilling.Columns("入金額計").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                    DgvBilling.Rows.Add()
                    DgvBilling.Rows(index).Cells("入金番号").Value = ds.Tables(RS).Rows(index)("入金番号")
                    DgvBilling.Rows(index).Cells("入金日").Value = ds.Tables(RS).Rows(index)("入金日")
                    DgvBilling.Rows(index).Cells("請求先名").Value = ds.Tables(RS).Rows(index)("請求先名")
                    DgvBilling.Rows(index).Cells("振込先").Value = ds.Tables(RS).Rows(index)("振込先")
                    DgvBilling.Rows(index).Cells("請求金額").Value = ds.Tables(RS).Rows(index)("請求金額")
                    DgvBilling.Rows(index).Cells("入金額").Value = ds.Tables(RS).Rows(index)("入金額")
                    DgvBilling.Rows(index).Cells("入金額計").Value = ds.Tables(RS).Rows(index)("入金額計")
                    DgvBilling.Rows(index).Cells("請求残高").Value = ds.Tables(RS).Rows(index)("請求残高")
                    DgvBilling.Rows(index).Cells("備考").Value = ds.Tables(RS).Rows(index)("備考")
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
        If _status = "VIEW" Then
            LblMode.Text = "参照モード"
        ElseIf _status = "CANCEL" Then
            LblMode.Text = "取消モード"
            BtnBillingCancel.Visible = True
            BtnBillingCancel.Location = New Point(997, 509)
        End If

        Dim Status As String = "EXCLUSION"
        PurchaseListLoad(Status)
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        Dim openForm As Form = Nothing
        openForm = New frmC01F30_Menu(_msgHd, _langHd, _db)
        openForm.Show()
        Me.Close()
    End Sub

    Private Sub RbtnDetails_CheckedChanged(sender As Object, e As EventArgs) Handles RbtnDetails.CheckedChanged
        DgvBilling.Rows.Clear()
        DgvBilling.Columns.Clear()

        If RbtnSlip.Checked Then
            Dim Sql As String = ""
            Sql += "SELECT "
            Sql += " * "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t25_nkinhd"
            If OrderingNo IsNot Nothing Then
                For i As Integer = 0 To OrderingNo.Length - 1
                    If i = 0 Then
                        Sql += " WHERE "
                        Sql += "入金番号"
                        Sql += " ILIKE "
                        Sql += "'"
                        Sql += OrderingNo(i)
                        Sql += "'"
                    Else
                        Sql += " OR "
                        Sql += "入金番号"
                        Sql += " ILIKE "
                        Sql += "'"
                        Sql += OrderingNo(i)
                        Sql += "'"
                    End If
                Next
            End If
            Sql += " ORDER BY "
            Sql += "登録日 DESC"

            Dim reccnt As Integer = 0
            ds = _db.selectDB(Sql, RS, reccnt)

            DgvBilling.Columns.Add("入金番号", "入金番号")
            DgvBilling.Columns.Add("入金日", "入金日")
            DgvBilling.Columns.Add("請求先名", "請求先名")
            DgvBilling.Columns.Add("振込先", "振込先")
            DgvBilling.Columns.Add("請求金額", "請求金額")
            DgvBilling.Columns.Add("入金額", "入金額")
            DgvBilling.Columns.Add("入金額計", "入金額計")
            DgvBilling.Columns.Add("請求残高", "請求残高")
            DgvBilling.Columns.Add("備考", "備考")

            DgvBilling.Columns("請求金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvBilling.Columns("請求残高").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvBilling.Columns("入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvBilling.Columns("入金額計").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvBilling.Rows.Add()
                DgvBilling.Rows(index).Cells("入金番号").Value = ds.Tables(RS).Rows(index)("入金番号")
                DgvBilling.Rows(index).Cells("入金日").Value = ds.Tables(RS).Rows(index)("入金日")
                DgvBilling.Rows(index).Cells("請求先名").Value = ds.Tables(RS).Rows(index)("請求先名")
                DgvBilling.Rows(index).Cells("振込先").Value = ds.Tables(RS).Rows(index)("振込先")
                DgvBilling.Rows(index).Cells("請求金額").Value = ds.Tables(RS).Rows(index)("請求金額")
                DgvBilling.Rows(index).Cells("入金額").Value = ds.Tables(RS).Rows(index)("入金額")
                DgvBilling.Rows(index).Cells("入金額計").Value = ds.Tables(RS).Rows(index)("入金額計")
                DgvBilling.Rows(index).Cells("請求残高").Value = ds.Tables(RS).Rows(index)("請求残高")
                DgvBilling.Rows(index).Cells("備考").Value = ds.Tables(RS).Rows(index)("備考")
            Next
        Else
            Dim Sql As String = ""

            Sql += "SELECT "
            Sql += " * "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t26_nkindt"

            If OrderingNo IsNot Nothing Then
                For i As Integer = 0 To OrderingNo.Length - 1
                    If i = 0 Then
                        Sql += " WHERE "
                        Sql += "入金番号"
                        Sql += " ILIKE "
                        Sql += "'%"
                        Sql += OrderingNo(i)
                        Sql += "%'"
                    Else
                        Sql += " OR "
                        Sql += "入金番号"
                        Sql += " ILIKE "
                        Sql += "'%"
                        Sql += OrderingNo(i)
                        Sql += "%'"
                    End If
                Next
            End If
            Sql += " ORDER BY "
            Sql += "登録日 DESC"

            Dim reccnt As Integer = 0
            ds = _db.selectDB(Sql, RS, reccnt)

            DgvBilling.Columns.Add("入金番号", "入金番号")
            DgvBilling.Columns.Add("行番号", "行番号")
            DgvBilling.Columns.Add("入金種別", "入金種別")
            DgvBilling.Columns.Add("請求先名", "請求先名")
            DgvBilling.Columns.Add("振込先", "振込先")
            DgvBilling.Columns.Add("入金額", "入金額")
            DgvBilling.Columns.Add("入金日", "入金日")
            DgvBilling.Columns.Add("備考", "備考")

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvBilling.Rows.Add()
                DgvBilling.Rows(index).Cells("入金番号").Value = ds.Tables(RS).Rows(index)("入金番号")
                DgvBilling.Rows(index).Cells("行番号").Value = ds.Tables(RS).Rows(index)("行番号")
                DgvBilling.Rows(index).Cells("入金種別").Value = ds.Tables(RS).Rows(index)("入金種別")
                DgvBilling.Rows(index).Cells("請求先名").Value = ds.Tables(RS).Rows(index)("請求先名")
                DgvBilling.Rows(index).Cells("振込先").Value = ds.Tables(RS).Rows(index)("振込先")
                DgvBilling.Rows(index).Cells("入金額").Value = ds.Tables(RS).Rows(index)("入金額")
                DgvBilling.Rows(index).Cells("入金日").Value = ds.Tables(RS).Rows(index)("入金日")
                DgvBilling.Rows(index).Cells("備考").Value = ds.Tables(RS).Rows(index)("備考")
            Next
        End If
    End Sub

    Private Sub BtnPurchaseSearch_Click(sender As Object, e As EventArgs) Handles BtnPurchaseSearch.Click
        DgvBilling.Rows.Clear()
        DgvBilling.Columns.Clear()

        Dim count As Integer = 0
        Dim Sql As String = ""
        Try
            Sql += "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t25_nkinhd"
            If TxtCustomerName.Text = "" Then
            Else
                Sql += " WHERE "
                Sql += "得意先名"
                Sql += " ILIKE "
                Sql += "'%"
                Sql += TxtCustomerName.Text
                Sql += "%'"
                count += 1
            End If
            If TxtCustomerCode.Text = "" Then
            Else
                If count > 0 Then
                    Sql += " AND "
                    Sql += "得意先コード"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtCustomerCode.Text
                    Sql += "%'"
                Else
                    Sql += " WHERE "
                    Sql += "得意先コード"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtCustomerCode.Text
                    Sql += "%'"
                    count += 1
                End If
            End If
            If TxtBillingDate1.Text = "" Then
                If TxtBillingDate2.Text = "" Then
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "入金日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtBillingDate2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "入金日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtBillingDate2.Text
                        Sql += "'"
                        count += 1
                    End If
                End If
            Else
                If TxtBillingDate2.Text = "" Then
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "入金日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtBillingDate1.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "入金日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtBillingDate1.Text
                        Sql += "'"
                        count += 1
                    End If
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "入金日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtBillingDate1.Text
                        Sql += "' "
                        Sql += "AND  "
                        Sql += "入金日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtBillingDate2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "入金日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtBillingDate1.Text
                        Sql += "' "
                        Sql += "AND  "
                        Sql += "入金日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtBillingDate2.Text
                        Sql += "'"
                        count += 1
                    End If
                End If
            End If
            If TxtBillingNo1.Text = "" Then
                If TxtBillingNo2.Text = "" Then
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "入金番号"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtBillingNo2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "入金番号"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtBillingNo2.Text
                        Sql += "'"
                        count += 1
                    End If
                End If
            Else
                If TxtBillingNo2.Text = "" Then
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "入金番号"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtBillingNo1.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "入金番号"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtBillingNo1.Text
                        Sql += "'"
                        count += 1
                    End If
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "入金番号"
                        Sql += " >= "
                        Sql += "'"
                        Sql += TxtBillingNo1.Text
                        Sql += "' "
                        Sql += "AND "
                        Sql += "入金番号"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtBillingNo2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "入金番号"
                        Sql += " >= "
                        Sql += "'"
                        Sql += TxtBillingNo1.Text
                        Sql += "' "
                        Sql += "AND  "
                        Sql += "入金番号"
                        Sql += " <= "
                        Sql += "'"
                        Sql += TxtBillingNo2.Text
                        Sql += "'"
                        count += 1
                    End If
                End If
            End If
            Sql += " ORDER BY "
            Sql += "登録日 DESC"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            'Dim tmp As Integer = ds.Tables(RS).Rows.Count - 1
            'ReDim OrderingNo(tmp)

            DgvBilling.Columns.Add("入金番号", "入金番号")
            DgvBilling.Columns.Add("入金日", "入金日")
            DgvBilling.Columns.Add("請求先名", "請求先名")
            DgvBilling.Columns.Add("振込先", "振込先")
            DgvBilling.Columns.Add("請求金額", "請求金額")
            DgvBilling.Columns.Add("入金額", "入金額")
            DgvBilling.Columns.Add("入金額計", "入金額計")
            DgvBilling.Columns.Add("請求残高", "請求残高")
            DgvBilling.Columns.Add("備考", "備考")

            DgvBilling.Columns("請求金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvBilling.Columns("請求残高").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvBilling.Columns("入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvBilling.Columns("入金額計").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvBilling.Rows.Add()
                DgvBilling.Rows(index).Cells("入金番号").Value = ds.Tables(RS).Rows(index)("入金番号")
                DgvBilling.Rows(index).Cells("入金日").Value = ds.Tables(RS).Rows(index)("入金日")
                DgvBilling.Rows(index).Cells("請求先名").Value = ds.Tables(RS).Rows(index)("請求先名")
                DgvBilling.Rows(index).Cells("振込先").Value = ds.Tables(RS).Rows(index)("振込先")
                DgvBilling.Rows(index).Cells("請求金額").Value = ds.Tables(RS).Rows(index)("請求金額")
                DgvBilling.Rows(index).Cells("入金額").Value = ds.Tables(RS).Rows(index)("入金額")
                DgvBilling.Rows(index).Cells("入金額計").Value = ds.Tables(RS).Rows(index)("入金額計")
                DgvBilling.Rows(index).Cells("請求残高").Value = ds.Tables(RS).Rows(index)("請求残高")
                DgvBilling.Rows(index).Cells("備考").Value = ds.Tables(RS).Rows(index)("備考")
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
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

    Private Sub ChkCancelData_CheckedChanged(sender As Object, e As EventArgs) Handles ChkCancelData.CheckedChanged
        DgvBilling.Rows.Clear()
        DgvBilling.Columns.Clear()

        If ChkCancelData.Checked = False Then
            Dim Status As String = "EXCLUSION"
            PurchaseListLoad(Status)
        Else
            PurchaseListLoad()
        End If
    End Sub

    Private Sub BtnBillingCancel_Click(sender As Object, e As EventArgs) Handles BtnBillingCancel.Click
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
        Dim result As DialogResult = MessageBox.Show("入金を取り消しますか？",
                                             "質問",
                                             MessageBoxButtons.YesNoCancel,
                                             MessageBoxIcon.Exclamation,
                                             MessageBoxDefaultButton.Button2)

        If result = DialogResult.Yes Then
            _db.executeDB(Sql1)
            DgvBilling.Rows.Clear()
            DgvBilling.Columns.Clear()
            Dim Status As String = "EXCLUSION"
            PurchaseListLoad(Status)
        ElseIf result = DialogResult.No Then

        ElseIf result = DialogResult.Cancel Then

        End If
    End Sub
End Class