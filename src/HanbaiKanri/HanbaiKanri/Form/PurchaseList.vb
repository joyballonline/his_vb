Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class PurchaseList
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
                Sql += "t40_sirehd"
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
                DgvHtyhd.Columns.Add("仕入番号", "仕入番号")
                DgvHtyhd.Columns.Add("客先番号", "客先番号")
                DgvHtyhd.Columns.Add("仕入日", "仕入日")

                DgvHtyhd.Columns.Add("発注番号", "発注番号")
                DgvHtyhd.Columns.Add("発注番号枝番", "発注番号枝番")
                DgvHtyhd.Columns.Add("仕入先コード", "仕入先コード")
                DgvHtyhd.Columns.Add("仕入先名", "仕入先名")
                DgvHtyhd.Columns.Add("仕入先郵便番号", "仕入先郵便番号")
                DgvHtyhd.Columns.Add("仕入先住所", "仕入先住所")
                DgvHtyhd.Columns.Add("仕入先電話番号", "仕入先電話番号")
                DgvHtyhd.Columns.Add("仕入先ＦＡＸ", "仕入先ＦＡＸ")
                DgvHtyhd.Columns.Add("仕入先担当者名", "仕入先担当者名")
                DgvHtyhd.Columns.Add("仕入先担当者役職", "仕入先担当者役職")
                DgvHtyhd.Columns.Add("仕入金額", "仕入金額")
                DgvHtyhd.Columns.Add("支払条件", "支払条件")
                DgvHtyhd.Columns.Add("営業担当者", "営業担当者")
                DgvHtyhd.Columns.Add("入力担当者", "入力担当者")
                DgvHtyhd.Columns.Add("備考", "備考")
                DgvHtyhd.Columns.Add("登録日", "登録日")

                DgvHtyhd.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                    DgvHtyhd.Rows.Add()
                    DgvHtyhd.Rows(index).Cells("仕入番号").Value = ds.Tables(RS).Rows(index)("仕入番号")
                    DgvHtyhd.Rows(index).Cells("客先番号").Value = ds.Tables(RS).Rows(index)("客先番号")
                    DgvHtyhd.Rows(index).Cells("発注番号").Value = ds.Tables(RS).Rows(index)("発注番号")
                    DgvHtyhd.Rows(index).Cells("発注番号枝番").Value = ds.Tables(RS).Rows(index)("発注番号枝番")
                    DgvHtyhd.Rows(index).Cells("仕入日").Value = ds.Tables(RS).Rows(index)("仕入日")
                    DgvHtyhd.Rows(index).Cells("仕入先コード").Value = ds.Tables(RS).Rows(index)("仕入先コード")
                    DgvHtyhd.Rows(index).Cells("仕入先名").Value = ds.Tables(RS).Rows(index)("仕入先名")
                    DgvHtyhd.Rows(index).Cells("仕入先郵便番号").Value = ds.Tables(RS).Rows(index)("仕入先郵便番号")
                    DgvHtyhd.Rows(index).Cells("仕入先住所").Value = ds.Tables(RS).Rows(index)("仕入先住所")
                    DgvHtyhd.Rows(index).Cells("仕入先電話番号").Value = ds.Tables(RS).Rows(index)("仕入先電話番号")
                    DgvHtyhd.Rows(index).Cells("仕入先ＦＡＸ").Value = ds.Tables(RS).Rows(index)("仕入先ＦＡＸ")
                    DgvHtyhd.Rows(index).Cells("仕入先担当者名").Value = ds.Tables(RS).Rows(index)("仕入先担当者名")
                    DgvHtyhd.Rows(index).Cells("仕入先担当者役職").Value = ds.Tables(RS).Rows(index)("仕入先担当者役職")
                    DgvHtyhd.Rows(index).Cells("仕入金額").Value = ds.Tables(RS).Rows(index)("仕入金額")
                    DgvHtyhd.Rows(index).Cells("支払条件").Value = ds.Tables(RS).Rows(index)("支払条件")
                    DgvHtyhd.Rows(index).Cells("営業担当者").Value = ds.Tables(RS).Rows(index)("営業担当者")
                    DgvHtyhd.Rows(index).Cells("入力担当者").Value = ds.Tables(RS).Rows(index)("入力担当者")
                    DgvHtyhd.Rows(index).Cells("備考").Value = ds.Tables(RS).Rows(index)("備考")
                    DgvHtyhd.Rows(index).Cells("登録日").Value = ds.Tables(RS).Rows(index)("登録日")
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
                Sql += "t40_sirehd"
                Sql += " ORDER BY "
                Sql += "登録日 DESC"

                Dim reccnt As Integer = 0
                ds = _db.selectDB(Sql, RS, reccnt)
                DgvHtyhd.Columns.Add("仕入番号", "仕入番号")
                DgvHtyhd.Columns.Add("客先番号", "客先番号")
                DgvHtyhd.Columns.Add("仕入日", "仕入日")
                DgvHtyhd.Columns.Add("発注番号", "発注番号")
                DgvHtyhd.Columns.Add("発注番号枝番", "発注番号枝番")
                DgvHtyhd.Columns.Add("仕入先コード", "仕入先コード")
                DgvHtyhd.Columns.Add("仕入先名", "仕入先名")
                DgvHtyhd.Columns.Add("仕入先郵便番号", "仕入先郵便番号")
                DgvHtyhd.Columns.Add("仕入先住所", "仕入先住所")
                DgvHtyhd.Columns.Add("仕入先電話番号", "仕入先電話番号")
                DgvHtyhd.Columns.Add("仕入先ＦＡＸ", "仕入先ＦＡＸ")
                DgvHtyhd.Columns.Add("仕入先担当者名", "仕入先担当者名")
                DgvHtyhd.Columns.Add("仕入先担当者役職", "仕入先担当者役職")
                DgvHtyhd.Columns.Add("仕入金額", "仕入金額")
                DgvHtyhd.Columns.Add("支払条件", "支払条件")
                DgvHtyhd.Columns.Add("営業担当者", "営業担当者")
                DgvHtyhd.Columns.Add("入力担当者", "入力担当者")
                DgvHtyhd.Columns.Add("備考", "備考")
                DgvHtyhd.Columns.Add("登録日", "登録日")

                DgvHtyhd.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                    DgvHtyhd.Rows.Add()
                    DgvHtyhd.Rows(index).Cells("仕入番号").Value = ds.Tables(RS).Rows(index)("仕入番号")
                    DgvHtyhd.Rows(index).Cells("客先番号").Value = ds.Tables(RS).Rows(index)("客先番号")
                    DgvHtyhd.Rows(index).Cells("仕入日").Value = ds.Tables(RS).Rows(index)("仕入日")
                    DgvHtyhd.Rows(index).Cells("発注番号").Value = ds.Tables(RS).Rows(index)("発注番号")
                    DgvHtyhd.Rows(index).Cells("発注番号枝番").Value = ds.Tables(RS).Rows(index)("発注番号枝番")
                    DgvHtyhd.Rows(index).Cells("仕入日").Value = ds.Tables(RS).Rows(index)("仕入日")
                    DgvHtyhd.Rows(index).Cells("仕入先コード").Value = ds.Tables(RS).Rows(index)("仕入先コード")
                    DgvHtyhd.Rows(index).Cells("仕入先名").Value = ds.Tables(RS).Rows(index)("仕入先名")
                    DgvHtyhd.Rows(index).Cells("仕入先郵便番号").Value = ds.Tables(RS).Rows(index)("仕入先郵便番号")
                    DgvHtyhd.Rows(index).Cells("仕入先住所").Value = ds.Tables(RS).Rows(index)("仕入先住所")
                    DgvHtyhd.Rows(index).Cells("仕入先電話番号").Value = ds.Tables(RS).Rows(index)("仕入先電話番号")
                    DgvHtyhd.Rows(index).Cells("仕入先ＦＡＸ").Value = ds.Tables(RS).Rows(index)("仕入先ＦＡＸ")
                    DgvHtyhd.Rows(index).Cells("仕入先担当者名").Value = ds.Tables(RS).Rows(index)("仕入先担当者名")
                    DgvHtyhd.Rows(index).Cells("仕入先担当者役職").Value = ds.Tables(RS).Rows(index)("仕入先担当者役職")
                    DgvHtyhd.Rows(index).Cells("仕入金額").Value = ds.Tables(RS).Rows(index)("仕入金額")
                    DgvHtyhd.Rows(index).Cells("支払条件").Value = ds.Tables(RS).Rows(index)("支払条件")
                    DgvHtyhd.Rows(index).Cells("営業担当者").Value = ds.Tables(RS).Rows(index)("営業担当者")
                    DgvHtyhd.Rows(index).Cells("入力担当者").Value = ds.Tables(RS).Rows(index)("入力担当者")
                    DgvHtyhd.Rows(index).Cells("備考").Value = ds.Tables(RS).Rows(index)("備考")
                    DgvHtyhd.Rows(index).Cells("登録日").Value = ds.Tables(RS).Rows(index)("登録日")
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
            BtnPurchaseView.Visible = True
            BtnPurchaseView.Location = New Point(997, 509)
        ElseIf _status = "CANCEL" Then
            LblMode.Text = "取消モード"
            BtnPurchaseCancel.Visible = True
            BtnPurchaseCancel.Location = New Point(997, 509)
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
        DgvHtyhd.Rows.Clear()
        DgvHtyhd.Columns.Clear()

        If RbtnSlip.Checked Then
            Dim Sql As String = ""
            Sql += "SELECT "
            Sql += " * "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t40_sirehd"
            If OrderingNo IsNot Nothing Then
                For i As Integer = 0 To OrderingNo.Length - 1
                    If i = 0 Then
                        Sql += " WHERE "
                        Sql += "仕入番号"
                        Sql += " ILIKE "
                        Sql += "'%"
                        Sql += OrderingNo(i)
                        Sql += "%'"
                    Else
                        Sql += " OR "
                        Sql += "仕入番号"
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

            DgvHtyhd.Columns.Add("仕入番号", "仕入番号")
            DgvHtyhd.Columns.Add("客先番号", "客先番号")
            DgvHtyhd.Columns.Add("仕入日", "仕入日")
            DgvHtyhd.Columns.Add("発注番号", "発注番号")
            DgvHtyhd.Columns.Add("発注番号枝番", "発注番号枝番")
            DgvHtyhd.Columns.Add("仕入先コード", "仕入先コード")
            DgvHtyhd.Columns.Add("仕入先名", "仕入先名")
            DgvHtyhd.Columns.Add("仕入先郵便番号", "仕入先郵便番号")
            DgvHtyhd.Columns.Add("仕入先住所", "仕入先住所")
            DgvHtyhd.Columns.Add("仕入先電話番号", "仕入先電話番号")
            DgvHtyhd.Columns.Add("仕入先ＦＡＸ", "仕入先ＦＡＸ")
            DgvHtyhd.Columns.Add("仕入先担当者名", "仕入先担当者名")
            DgvHtyhd.Columns.Add("仕入先担当者役職", "仕入先担当者役職")
            DgvHtyhd.Columns.Add("仕入金額", "仕入金額")
            DgvHtyhd.Columns.Add("支払条件", "支払条件")
            DgvHtyhd.Columns.Add("営業担当者", "営業担当者")
            DgvHtyhd.Columns.Add("入力担当者", "入力担当者")
            DgvHtyhd.Columns.Add("備考", "備考")
            DgvHtyhd.Columns.Add("登録日", "登録日")

            DgvHtyhd.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvHtyhd.Rows.Add()
                DgvHtyhd.Rows(index).Cells("仕入番号").Value = ds.Tables(RS).Rows(index)("仕入番号")
                DgvHtyhd.Rows(index).Cells("客先番号").Value = ds.Tables(RS).Rows(index)("客先番号")
                DgvHtyhd.Rows(index).Cells("仕入日").Value = ds.Tables(RS).Rows(index)("仕入日")
                DgvHtyhd.Rows(index).Cells("発注番号").Value = ds.Tables(RS).Rows(index)("発注番号")
                DgvHtyhd.Rows(index).Cells("発注番号枝番").Value = ds.Tables(RS).Rows(index)("発注番号枝番")
                DgvHtyhd.Rows(index).Cells("仕入日").Value = ds.Tables(RS).Rows(index)("仕入日")
                DgvHtyhd.Rows(index).Cells("仕入先コード").Value = ds.Tables(RS).Rows(index)("仕入先コード")
                DgvHtyhd.Rows(index).Cells("仕入先名").Value = ds.Tables(RS).Rows(index)("仕入先名")
                DgvHtyhd.Rows(index).Cells("仕入先郵便番号").Value = ds.Tables(RS).Rows(index)("仕入先郵便番号")
                DgvHtyhd.Rows(index).Cells("仕入先住所").Value = ds.Tables(RS).Rows(index)("仕入先住所")
                DgvHtyhd.Rows(index).Cells("仕入先電話番号").Value = ds.Tables(RS).Rows(index)("仕入先電話番号")
                DgvHtyhd.Rows(index).Cells("仕入先ＦＡＸ").Value = ds.Tables(RS).Rows(index)("仕入先ＦＡＸ")
                DgvHtyhd.Rows(index).Cells("仕入先担当者名").Value = ds.Tables(RS).Rows(index)("仕入先担当者名")
                DgvHtyhd.Rows(index).Cells("仕入先担当者役職").Value = ds.Tables(RS).Rows(index)("仕入先担当者役職")
                DgvHtyhd.Rows(index).Cells("仕入金額").Value = ds.Tables(RS).Rows(index)("仕入金額")
                DgvHtyhd.Rows(index).Cells("支払条件").Value = ds.Tables(RS).Rows(index)("支払条件")
                DgvHtyhd.Rows(index).Cells("営業担当者").Value = ds.Tables(RS).Rows(index)("営業担当者")
                DgvHtyhd.Rows(index).Cells("入力担当者").Value = ds.Tables(RS).Rows(index)("入力担当者")
                DgvHtyhd.Rows(index).Cells("備考").Value = ds.Tables(RS).Rows(index)("備考")
                DgvHtyhd.Rows(index).Cells("登録日").Value = ds.Tables(RS).Rows(index)("登録日")
            Next
        Else
            Dim Sql As String = ""

            Sql += "SELECT "
            Sql += " * "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t41_siredt"

            If OrderingNo IsNot Nothing Then
                For i As Integer = 0 To OrderingNo.Length - 1
                    If i = 0 Then
                        Sql += " WHERE "
                        Sql += "仕入番号"
                        Sql += " ILIKE "
                        Sql += "'%"
                        Sql += OrderingNo(i)
                        Sql += "%'"
                    Else
                        Sql += " OR "
                        Sql += "仕入番号"
                        Sql += " ILIKE "
                        Sql += "'%"
                        Sql += OrderingNo(i)
                        Sql += "%'"
                    End If
                Next
            End If
            Sql += " ORDER BY "
            Sql += "更新日 DESC"

            Dim reccnt As Integer = 0
            ds = _db.selectDB(Sql, RS, reccnt)

            DgvHtyhd.Columns.Add("仕入番号", "仕入番号")
            DgvHtyhd.Columns.Add("行番号", "行番号")
            DgvHtyhd.Columns.Add("仕入区分", "仕入区分")
            DgvHtyhd.Columns.Add("メーカー", "メーカー")
            DgvHtyhd.Columns.Add("品名", "品名")
            DgvHtyhd.Columns.Add("型式", "型式")
            DgvHtyhd.Columns.Add("仕入先名", "仕入先名")
            DgvHtyhd.Columns.Add("仕入値", "仕入値")
            DgvHtyhd.Columns.Add("発注数量", "発注数量")
            DgvHtyhd.Columns.Add("仕入数量", "仕入数量")
            DgvHtyhd.Columns.Add("発注残数", "発注残数")
            DgvHtyhd.Columns.Add("単位", "単位")
            DgvHtyhd.Columns.Add("間接費", "間接費")
            DgvHtyhd.Columns.Add("仕入金額", "仕入金額")
            DgvHtyhd.Columns.Add("リードタイム", "リードタイム")
            DgvHtyhd.Columns.Add("備考", "備考")
            DgvHtyhd.Columns.Add("更新者", "更新者")



            DgvHtyhd.Columns("仕入値").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvHtyhd.Columns("発注数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvHtyhd.Columns("仕入数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvHtyhd.Columns("発注残数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvHtyhd.Columns("間接費").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvHtyhd.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvHtyhd.Columns("リードタイム").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvHtyhd.Rows.Add()
                DgvHtyhd.Rows(index).Cells("仕入番号").Value = ds.Tables(RS).Rows(index)("仕入番号")
                DgvHtyhd.Rows(index).Cells("行番号").Value = ds.Tables(RS).Rows(index)("行番号")
                DgvHtyhd.Rows(index).Cells("仕入区分").Value = ds.Tables(RS).Rows(index)("仕入区分")
                DgvHtyhd.Rows(index).Cells("メーカー").Value = ds.Tables(RS).Rows(index)("メーカー")
                DgvHtyhd.Rows(index).Cells("品名").Value = ds.Tables(RS).Rows(index)("品名")
                DgvHtyhd.Rows(index).Cells("型式").Value = ds.Tables(RS).Rows(index)("型式")
                DgvHtyhd.Rows(index).Cells("仕入先名").Value = ds.Tables(RS).Rows(index)("仕入先名")
                DgvHtyhd.Rows(index).Cells("仕入値").Value = ds.Tables(RS).Rows(index)("仕入値")
                DgvHtyhd.Rows(index).Cells("発注数量").Value = ds.Tables(RS).Rows(index)("発注数量")
                DgvHtyhd.Rows(index).Cells("仕入数量").Value = ds.Tables(RS).Rows(index)("仕入数量")
                DgvHtyhd.Rows(index).Cells("発注残数").Value = ds.Tables(RS).Rows(index)("発注残数")
                DgvHtyhd.Rows(index).Cells("単位").Value = ds.Tables(RS).Rows(index)("単位")
                DgvHtyhd.Rows(index).Cells("間接費").Value = ds.Tables(RS).Rows(index)("間接費")
                DgvHtyhd.Rows(index).Cells("仕入金額").Value = ds.Tables(RS).Rows(index)("仕入金額")
                DgvHtyhd.Rows(index).Cells("リードタイム").Value = ds.Tables(RS).Rows(index)("リードタイム")
                DgvHtyhd.Rows(index).Cells("備考").Value = ds.Tables(RS).Rows(index)("備考")
                DgvHtyhd.Rows(index).Cells("更新者").Value = ds.Tables(RS).Rows(index)("更新者")
            Next
        End If
    End Sub

    Private Sub BtnPurchaseSearch_Click(sender As Object, e As EventArgs) Handles BtnPurchaseSearch.Click
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
            Sql += "t40_sirehd"
            If TxtSupplierName.Text = "" Then
            Else
                Sql += " WHERE "
                Sql += "仕入先名"
                Sql += " ILIKE "
                Sql += "'%"
                Sql += TxtSupplierName.Text
                Sql += "%'"
                count += 1
            End If
            If TxtAddress.Text = "" Then
            Else
                If count > 0 Then
                    Sql += " AND "
                    Sql += "仕入先住所"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtAddress.Text
                    Sql += "%'"
                Else
                    Sql += " WHERE "
                    Sql += "仕入先住所"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtAddress.Text
                    Sql += "%'"
                    count += 1
                End If
            End If
            If TxtTel.Text = "" Then
            Else
                If count > 0 Then
                    Sql += " AND "
                    Sql += "仕入先電話番号"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtTel.Text
                    Sql += "%'"
                Else
                    Sql += " WHERE "
                    Sql += "仕入先電話番号"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtTel.Text
                    Sql += "%'"
                    count += 1
                End If
            End If
            If TxtSupplierCode.Text = "" Then
            Else
                If count > 0 Then
                    Sql += " AND "
                    Sql += "仕入先コード"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtSupplierCode.Text
                    Sql += "%'"
                Else
                    Sql += " WHERE "
                    Sql += "仕入先コード"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtSupplierCode.Text
                    Sql += "%'"
                    count += 1
                End If
            End If
            If TxtPurchaseDate1.Text = "" Then
                If TxtPurchaseDate2.Text = "" Then
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "仕入日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtPurchaseDate2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "仕入日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtPurchaseDate2.Text
                        Sql += "'"
                        count += 1
                    End If
                End If
            Else
                If TxtPurchaseDate2.Text = "" Then
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "仕入日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtPurchaseDate1.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "仕入日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtPurchaseDate1.Text
                        Sql += "'"
                        count += 1
                    End If
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "仕入日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtPurchaseDate1.Text
                        Sql += "' "
                        Sql += "AND  "
                        Sql += "仕入日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtPurchaseDate2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "仕入日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtPurchaseDate1.Text
                        Sql += "' "
                        Sql += "AND  "
                        Sql += "仕入日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtPurchaseDate2.Text
                        Sql += "'"
                        count += 1
                    End If
                End If
            End If
            If TxtPurchaseNo1.Text = "" Then
                If TxtPurchaseNo2.Text = "" Then
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "仕入番号"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtPurchaseNo2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "仕入番号"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtPurchaseNo2.Text
                        Sql += "'"
                        count += 1
                    End If
                End If
            Else
                If TxtPurchaseNo2.Text = "" Then
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "仕入番号"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtPurchaseNo1.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "仕入番号"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtPurchaseNo1.Text
                        Sql += "'"
                        count += 1
                    End If
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "仕入番号"
                        Sql += " >= "
                        Sql += "'"
                        Sql += TxtPurchaseNo1.Text
                        Sql += "' "
                        Sql += "AND "
                        Sql += "仕入番号"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtPurchaseNo2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "仕入番号"
                        Sql += " >= "
                        Sql += "'"
                        Sql += TxtPurchaseNo1.Text
                        Sql += "' "
                        Sql += "AND  "
                        Sql += "仕入番号"
                        Sql += " <= "
                        Sql += "'"
                        Sql += TxtPurchaseNo2.Text
                        Sql += "'"
                        count += 1
                    End If
                End If
            End If
            If TxtSales.Text = "" Then
            Else
                If count > 0 Then
                    Sql += " AND "
                    Sql += "営業担当者"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtSales.Text
                    Sql += "%'"
                Else
                    Sql += " WHERE "
                    Sql += "営業担当者"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtSales.Text
                    Sql += "%'"
                    count += 1
                End If
            End If
            If TxtCustomerPO.Text = "" Then
            Else
                If count > 0 Then
                    Sql += " AND "
                    Sql += "客先番号"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtCustomerPO.Text
                    Sql += "%'"
                Else
                    Sql += " WHERE "
                    Sql += "客先番号"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtCustomerPO.Text
                    Sql += "%'"
                    count += 1
                End If
            End If
            Sql += " ORDER BY "
            Sql += "登録日 DESC"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            DgvHtyhd.Columns.Add("仕入番号", "仕入番号")
            DgvHtyhd.Columns.Add("客先番号", "客先番号")
            DgvHtyhd.Columns.Add("仕入日", "仕入日")
            DgvHtyhd.Columns.Add("発注番号", "発注番号")
            DgvHtyhd.Columns.Add("発注番号枝番", "発注番号枝番")
            DgvHtyhd.Columns.Add("仕入先コード", "仕入先コード")
            DgvHtyhd.Columns.Add("仕入先名", "仕入先名")
            DgvHtyhd.Columns.Add("仕入先郵便番号", "仕入先郵便番号")
            DgvHtyhd.Columns.Add("仕入先住所", "仕入先住所")
            DgvHtyhd.Columns.Add("仕入先電話番号", "仕入先電話番号")
            DgvHtyhd.Columns.Add("仕入先ＦＡＸ", "仕入先ＦＡＸ")
            DgvHtyhd.Columns.Add("仕入先担当者名", "仕入先担当者名")
            DgvHtyhd.Columns.Add("仕入先担当者役職", "仕入先担当者役職")
            DgvHtyhd.Columns.Add("仕入金額", "仕入金額")
            DgvHtyhd.Columns.Add("支払条件", "支払条件")
            DgvHtyhd.Columns.Add("営業担当者", "営業担当者")
            DgvHtyhd.Columns.Add("入力担当者", "入力担当者")
            DgvHtyhd.Columns.Add("備考", "備考")
            DgvHtyhd.Columns.Add("登録日", "登録日")

            DgvHtyhd.Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            Dim tmp As Integer = ds.Tables(RS).Rows.Count - 1
            ReDim OrderingNo(tmp)

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvHtyhd.Rows.Add()
                DgvHtyhd.Rows(index).Cells("仕入番号").Value = ds.Tables(RS).Rows(index)("仕入番号")
                DgvHtyhd.Rows(index).Cells("客先番号").Value = ds.Tables(RS).Rows(index)("客先番号")
                DgvHtyhd.Rows(index).Cells("仕入日").Value = ds.Tables(RS).Rows(index)("仕入日")
                DgvHtyhd.Rows(index).Cells("発注番号").Value = ds.Tables(RS).Rows(index)("発注番号")
                DgvHtyhd.Rows(index).Cells("発注番号枝番").Value = ds.Tables(RS).Rows(index)("発注番号枝番")
                DgvHtyhd.Rows(index).Cells("仕入日").Value = ds.Tables(RS).Rows(index)("仕入日")
                DgvHtyhd.Rows(index).Cells("仕入先コード").Value = ds.Tables(RS).Rows(index)("仕入先コード")
                DgvHtyhd.Rows(index).Cells("仕入先名").Value = ds.Tables(RS).Rows(index)("仕入先名")
                DgvHtyhd.Rows(index).Cells("仕入先郵便番号").Value = ds.Tables(RS).Rows(index)("仕入先郵便番号")
                DgvHtyhd.Rows(index).Cells("仕入先住所").Value = ds.Tables(RS).Rows(index)("仕入先住所")
                DgvHtyhd.Rows(index).Cells("仕入先電話番号").Value = ds.Tables(RS).Rows(index)("仕入先電話番号")
                DgvHtyhd.Rows(index).Cells("仕入先ＦＡＸ").Value = ds.Tables(RS).Rows(index)("仕入先ＦＡＸ")
                DgvHtyhd.Rows(index).Cells("仕入先担当者名").Value = ds.Tables(RS).Rows(index)("仕入先担当者名")
                DgvHtyhd.Rows(index).Cells("仕入先担当者役職").Value = ds.Tables(RS).Rows(index)("仕入先担当者役職")
                DgvHtyhd.Rows(index).Cells("仕入金額").Value = ds.Tables(RS).Rows(index)("仕入金額")
                DgvHtyhd.Rows(index).Cells("支払条件").Value = ds.Tables(RS).Rows(index)("支払条件")
                DgvHtyhd.Rows(index).Cells("営業担当者").Value = ds.Tables(RS).Rows(index)("営業担当者")
                DgvHtyhd.Rows(index).Cells("入力担当者").Value = ds.Tables(RS).Rows(index)("入力担当者")
                DgvHtyhd.Rows(index).Cells("備考").Value = ds.Tables(RS).Rows(index)("備考")
                DgvHtyhd.Rows(index).Cells("登録日").Value = ds.Tables(RS).Rows(index)("登録日")
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    Private Sub BtnPurchaseView_Click(sender As Object, e As EventArgs) Handles BtnPurchaseView.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvHtyhd.CurrentCell.RowIndex
        Dim No As String = DgvHtyhd.Rows(RowIdx).Cells("発注番号").Value
        Dim Suffix As String = DgvHtyhd.Rows(RowIdx).Cells("発注番号枝番").Value
        Dim Status As String = "VIEW"
        Dim openForm As Form = Nothing
        openForm = New PurchasingManagement(_msgHd, _db, _langHd, No, Suffix, Status)   '処理選択
        openForm.Show(Me)
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

    Private Sub BtnPurchaseCancel_Click(sender As Object, e As EventArgs) Handles BtnPurchaseCancel.Click
        Dim dtNow As DateTime = DateTime.Now
        Dim reccnt As Integer = 0

        Dim Sql1 As String = ""
        Sql1 += "SELECT "
        Sql1 += " * "
        Sql1 += "FROM "
        Sql1 += "public"
        Sql1 += "."
        Sql1 += "t21_hattyu "
        Sql1 += "WHERE"
        Sql1 += " 会社コード"
        Sql1 += "='"
        Sql1 += frmC01F10_Login.loginValue.BumonNM
        Sql1 += "'"
        Sql1 += " AND"
        Sql1 += " 発注番号"
        Sql1 += "='"
        Sql1 += DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("発注番号").Value
        Sql1 += "' "
        Sql1 += " AND"
        Sql1 += " 発注番号枝番"
        Sql1 += "='"
        Sql1 += DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("発注番号枝番").Value
        Sql1 += "' "

        Dim ds1 As DataSet = _db.selectDB(Sql1, RS, reccnt)

        Dim Sql2 As String = ""
        Sql2 += "SELECT "
        Sql2 += " * "
        Sql2 += "FROM "
        Sql2 += "public"
        Sql2 += "."
        Sql2 += "t41_siredt "
        Sql2 += "WHERE"
        Sql2 += " 会社コード"
        Sql2 += "='"
        Sql2 += frmC01F10_Login.loginValue.BumonNM
        Sql2 += "'"
        Sql2 += " AND"
        Sql2 += " 仕入番号"
        Sql2 += "='"
        Sql2 += DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("仕入番号").Value
        Sql2 += "' "

        Dim ds2 As DataSet = _db.selectDB(Sql2, RS, reccnt)

        Dim Sql3 As String = ""
        Sql3 = ""
        Sql3 += "UPDATE "
        Sql3 += "Public."
        Sql3 += "t40_sirehd "
        Sql3 += "SET "

        Sql3 += "取消区分"
        Sql3 += " = '"
        Sql3 += "1"
        Sql3 += "', "
        Sql3 += "取消日"
        Sql3 += " = '"
        Sql3 += dtNow
        Sql3 += "', "
        Sql3 += "更新日"
        Sql3 += " = '"
        Sql3 += dtNow
        Sql3 += "', "
        Sql3 += "更新者"
        Sql3 += " = '"
        Sql3 += frmC01F10_Login.loginValue.TantoNM
        Sql3 += " ' "

        Sql3 += "WHERE"
        Sql3 += " 会社コード"
        Sql3 += "='"
        Sql3 += frmC01F10_Login.loginValue.BumonNM
        Sql3 += "'"
        Sql3 += " AND"
        Sql3 += " 仕入番号"
        Sql3 += "='"
        Sql3 += DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("仕入番号").Value
        Sql3 += "' "
        Sql3 += "RETURNING 会社コード"
        Sql3 += ", "
        Sql3 += "仕入番号"
        Sql3 += ", "
        Sql3 += "発注番号"
        Sql3 += ", "
        Sql3 += "発注番号枝番"
        Sql3 += ", "
        Sql3 += "仕入先コード"
        Sql3 += ", "
        Sql3 += "仕入先名"
        Sql3 += ", "
        Sql3 += "仕入先郵便番号"
        Sql3 += ", "
        Sql3 += "仕入先住所"
        Sql3 += ", "
        Sql3 += "仕入先電話番号"
        Sql3 += ", "
        Sql3 += "仕入先ＦＡＸ"
        Sql3 += ", "
        Sql3 += "仕入先担当者役職"
        Sql3 += ", "
        Sql3 += "仕入先担当者名"
        Sql3 += ", "
        Sql3 += "支払条件"
        Sql3 += ", "
        Sql3 += "仕入金額"
        Sql3 += ", "
        Sql3 += "粗利額"
        Sql3 += ", "
        Sql3 += "営業担当者"
        Sql3 += ", "
        Sql3 += "入力担当者"
        Sql3 += ", "
        Sql3 += "備考"
        Sql3 += ", "
        Sql3 += "取消日"
        Sql3 += ", "
        Sql3 += "取消区分"
        Sql3 += ", "
        Sql3 += "ＶＡＴ"
        Sql3 += ", "
        Sql3 += "ＰＰＨ"
        Sql3 += ", "
        Sql3 += "仕入日"
        Sql3 += ", "
        Sql3 += "登録日"
        Sql3 += ", "
        Sql3 += "更新日"
        Sql3 += ", "
        Sql3 += "更新者"
        Dim result As DialogResult = MessageBox.Show("仕入を取り消しますか？",
                                             "質問",
                                             MessageBoxButtons.YesNoCancel,
                                             MessageBoxIcon.Exclamation,
                                             MessageBoxDefaultButton.Button2)

        If result = DialogResult.Yes Then
            _db.executeDB(Sql3)


            Dim Sql4 As String = ""
            Dim PurchaseNum As Integer = 0
            Dim OrderingNum As Integer = 0

            For index1 As Integer = 0 To ds1.Tables(RS).Rows.Count() - 1
                For index2 As Integer = 0 To ds2.Tables(RS).Rows.Count() - 1
                    If ds1.Tables(RS).Rows(index1)("行番号") = ds2.Tables(RS).Rows(index2)("行番号") Then
                        Sql4 = ""
                        Sql4 += "UPDATE "
                        Sql4 += "Public."
                        Sql4 += "t21_hattyu "
                        Sql4 += "SET "
                        Sql4 += "仕入数量"
                        Sql4 += " = '"
                        PurchaseNum = ds1.Tables(RS).Rows(index1)("仕入数量") - ds2.Tables(RS).Rows(index1)("仕入数量")
                        Sql4 += PurchaseNum.ToString
                        Sql4 += "', "
                        Sql4 += " 発注残数"
                        Sql4 += " = '"
                        OrderingNum = ds1.Tables(RS).Rows(index1)("発注残数") + ds2.Tables(RS).Rows(index2)("仕入数量")
                        Sql4 += OrderingNum.ToString
                        Sql4 += "', "
                        Sql4 += "更新者"
                        Sql4 += " = '"
                        Sql4 += frmC01F10_Login.loginValue.TantoNM
                        Sql4 += "' "
                        Sql4 += "WHERE"
                        Sql4 += " 会社コード"
                        Sql4 += "='"
                        Sql4 += ds1.Tables(RS).Rows(index1)("会社コード")
                        Sql4 += "'"
                        Sql4 += " AND"
                        Sql4 += " 発注番号"
                        Sql4 += "='"
                        Sql4 += ds1.Tables(RS).Rows(index1)("発注番号")
                        Sql4 += "'"
                        Sql4 += " AND"
                        Sql4 += " 発注番号枝番"
                        Sql4 += "='"
                        Sql4 += ds1.Tables(RS).Rows(index1)("発注番号枝番")
                        Sql4 += "'"
                        Sql4 += " AND"
                        Sql4 += " 行番号"
                        Sql4 += "='"
                        Sql4 += ds1.Tables(RS).Rows(index1)("行番号").ToString
                        Sql4 += "' "
                        Sql4 += "RETURNING 会社コード"
                        Sql4 += ", "
                        Sql4 += "発注番号"
                        Sql4 += ", "
                        Sql4 += "発注番号枝番"
                        Sql4 += ", "
                        Sql4 += "行番号"
                        Sql4 += ", "
                        Sql4 += "仕入区分"
                        Sql4 += ", "
                        Sql4 += "メーカー"
                        Sql4 += ", "
                        Sql4 += "品名"
                        Sql4 += ", "
                        Sql4 += "型式"
                        Sql4 += ", "
                        Sql4 += "仕入先名"
                        Sql4 += ", "
                        Sql4 += "仕入値"
                        Sql4 += ", "
                        Sql4 += "発注数量"
                        Sql4 += ", "
                        Sql4 += "仕入数量"
                        Sql4 += ", "
                        Sql4 += "発注残数"
                        Sql4 += ", "
                        Sql4 += "単位"
                        Sql4 += ", "
                        Sql4 += "間接費"
                        Sql4 += ", "
                        Sql4 += "仕入単価"
                        Sql4 += ", "
                        Sql4 += "仕入金額"
                        Sql4 += ", "
                        Sql4 += "リードタイム"
                        Sql4 += ", "
                        Sql4 += "入庫数"
                        Sql4 += ", "
                        Sql4 += "未入庫数"
                        Sql4 += ", "
                        Sql4 += "備考"
                        Sql4 += ", "
                        Sql4 += "更新者"

                        _db.executeDB(Sql4)

                        Sql4 = ""
                        PurchaseNum = 0
                        OrderingNum = 0
                    End If
                Next
            Next
            DgvHtyhd.Rows.Clear()
            DgvHtyhd.Columns.Clear()
            Dim Status As String = "EXCLUSION"
            PurchaseListLoad(Status)
        End If
    End Sub
End Class