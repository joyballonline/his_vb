Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class OrderManagement
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
    Private CompanyCode As String = ""
    Private No As String = ""
    Private Suffix As String = ""
    Private _langHd As UtilLangHandler
    Private Input As String = frmC01F10_Login.loginValue.TantoNM

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
                   ByRef prmRefSuffix As String)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        No = prmRefNo
        Suffix = prmRefSuffix
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示

        _init = True

    End Sub

    Private Sub PurchaseManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Sql1 As String = ""
        Dim Sql2 As String = ""
        Dim Sql3 As String = ""
        Try
            Sql1 += "SELECT "
            Sql1 += "* "
            Sql1 += "FROM "
            Sql1 += "public"
            Sql1 += "."
            Sql1 += "t10_cymnhd"
            Sql1 += " WHERE "
            Sql1 += "受注番号"
            Sql1 += " ILIKE "
            Sql1 += "'"
            Sql1 += No
            Sql1 += "'"
            Sql1 += " AND "
            Sql1 += "受注番号枝番"
            Sql1 += " ILIKE "
            Sql1 += "'"
            Sql1 += Suffix
            Sql1 += "'"

            Sql2 += "SELECT "
            Sql2 += "* "
            Sql2 += "FROM "
            Sql2 += "public"
            Sql2 += "."
            Sql2 += "t45_shukodt"
            Sql2 += " WHERE "
            Sql2 += "受注番号"
            Sql2 += " ILIKE "
            Sql2 += "'"
            Sql2 += No
            Sql2 += "'"
            Sql2 += " AND "
            Sql2 += "受注番号枝番"
            Sql2 += " ILIKE "
            Sql2 += "'"
            Sql2 += Suffix
            Sql2 += "'"

            Sql3 += "SELECT "
            Sql3 += "* "
            Sql3 += "FROM "
            Sql3 += "public"
            Sql3 += "."
            Sql3 += "t11_cymndt"
            Sql3 += " WHERE "
            Sql3 += "受注番号"
            Sql3 += " ILIKE "
            Sql3 += "'"
            Sql3 += No
            Sql3 += "'"
            Sql3 += " AND "
            Sql3 += "受注番号枝番"
            Sql3 += " ILIKE "
            Sql3 += "'"
            Sql3 += Suffix
            Sql3 += "'"

            Dim reccnt As Integer = 0
            Dim ds1 As DataSet = _db.selectDB(Sql1, RS, reccnt)
            Dim ds2 As DataSet = _db.selectDB(Sql2, RS, reccnt)
            Dim ds3 As DataSet = _db.selectDB(Sql3, RS, reccnt)

            DgvOrder.Columns.Add("明細", "明細")
            DgvOrder.Columns.Add("メーカー", "メーカー")
            DgvOrder.Columns.Add("品名", "品名")
            DgvOrder.Columns.Add("型式", "型式")
            DgvOrder.Columns.Add("受注数量", "受注数量")
            DgvOrder.Columns.Add("単位", "単位")
            DgvOrder.Columns.Add("売上数量", "売上数量")
            DgvOrder.Columns.Add("売単価", "売単価")
            DgvOrder.Columns.Add("売上金額", "売上金額")
            DgvOrder.Columns.Add("受注残数", "受注残数")

            DgvOrder.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvOrder.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvOrder.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvOrder.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvOrder.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            For index As Integer = 0 To ds3.Tables(RS).Rows.Count - 1
                DgvOrder.Rows.Add()
                DgvOrder.Rows(index).Cells(1).Value = ds3.Tables(RS).Rows(index)("メーカー")
                DgvOrder.Rows(index).Cells(2).Value = ds3.Tables(RS).Rows(index)("品名")
                DgvOrder.Rows(index).Cells(3).Value = ds3.Tables(RS).Rows(index)("型式")
                DgvOrder.Rows(index).Cells(4).Value = ds3.Tables(RS).Rows(index)("受注数量")
                DgvOrder.Rows(index).Cells(5).Value = ds3.Tables(RS).Rows(index)("単位")
                DgvOrder.Rows(index).Cells(6).Value = ds3.Tables(RS).Rows(index)("売上数量")
                DgvOrder.Rows(index).Cells(7).Value = ds3.Tables(RS).Rows(index)("売単価")
                DgvOrder.Rows(index).Cells(8).Value = ds3.Tables(RS).Rows(index)("売上金額")
                DgvOrder.Rows(index).Cells(9).Value = ds3.Tables(RS).Rows(index)("受注残数")
            Next

            DgvHistory.Columns.Add("No", "No")
            DgvHistory.Columns.Add("出庫番号", "出庫番号")
            DgvHistory.Columns.Add("行番号", "行番号")
            DgvHistory.Columns.Add("仕入区分", "仕入区分")
            DgvHistory.Columns.Add("メーカー", "メーカー")
            DgvHistory.Columns.Add("品名", "品名")
            DgvHistory.Columns.Add("型式", "型式")
            DgvHistory.Columns.Add("単位", "単位")
            DgvHistory.Columns.Add("仕入先", "仕入先")
            DgvHistory.Columns.Add("売単価", "売単価")
            DgvHistory.Columns.Add("出庫数量", "出庫数量")
            DgvHistory.Columns.Add("備考", "備考")

            DgvHistory.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvHistory.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            For index As Integer = 0 To ds2.Tables(RS).Rows.Count - 1
                DgvHistory.Rows.Add()
                DgvHistory.Rows(index).Cells("出庫番号").Value = ds2.Tables(RS).Rows(index)("出庫番号")
                DgvHistory.Rows(index).Cells("行番号").Value = ds2.Tables(RS).Rows(index)("行番号")
                DgvHistory.Rows(index).Cells("仕入区分").Value = ds2.Tables(RS).Rows(index)("仕入区分")
                DgvHistory.Rows(index).Cells("メーカー").Value = ds2.Tables(RS).Rows(index)("メーカー")
                DgvHistory.Rows(index).Cells("品名").Value = ds2.Tables(RS).Rows(index)("品名")
                DgvHistory.Rows(index).Cells("型式").Value = ds2.Tables(RS).Rows(index)("型式")
                DgvHistory.Rows(index).Cells("単位").Value = ds2.Tables(RS).Rows(index)("単位")
                DgvHistory.Rows(index).Cells("仕入先").Value = ds2.Tables(RS).Rows(index)("仕入先名")
                DgvHistory.Rows(index).Cells("売単価").Value = ds2.Tables(RS).Rows(index)("売単価")
                DgvHistory.Rows(index).Cells("出庫数量").Value = ds2.Tables(RS).Rows(index)("出庫数量")
                DgvHistory.Rows(index).Cells("備考").Value = ds2.Tables(RS).Rows(index)("備考")
            Next

            DgvAdd.Columns.Add("No", "No")
            DgvAdd.Columns.Add("行番号", "行番号")
            DgvAdd.Columns.Add("仕入区分", "仕入区分")
            DgvAdd.Columns.Add("メーカー", "メーカー")
            DgvAdd.Columns.Add("品名", "品名")
            DgvAdd.Columns.Add("型式", "型式")
            DgvAdd.Columns.Add("単位", "単位")
            DgvAdd.Columns.Add("仕入先", "仕入先")
            DgvAdd.Columns.Add("売単価", "売単価")
            DgvAdd.Columns.Add("売上数量", "売上数量")
            DgvAdd.Columns.Add("備考", "備考")

            DgvAdd.Columns("売単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvAdd.Columns("売上数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            DgvAdd.Columns("No").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvAdd.Columns("No").ReadOnly = True
            DgvAdd.Columns("行番号").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvAdd.Columns("行番号").ReadOnly = True
            DgvAdd.Columns("仕入区分").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvAdd.Columns("仕入区分").ReadOnly = True
            DgvAdd.Columns("メーカー").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvAdd.Columns("メーカー").ReadOnly = True
            DgvAdd.Columns("品名").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvAdd.Columns("品名").ReadOnly = True
            DgvAdd.Columns("型式").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvAdd.Columns("型式").ReadOnly = True
            DgvAdd.Columns("単位").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvAdd.Columns("単位").ReadOnly = True
            DgvAdd.Columns("仕入先").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvAdd.Columns("仕入先").ReadOnly = True

            For index As Integer = 0 To ds3.Tables(RS).Rows.Count - 1
                If ds3.Tables(RS).Rows(index)("受注残数") = 0 Then
                Else
                    DgvAdd.Rows.Add()
                    DgvAdd.Rows(index).Cells("行番号").Value = ds3.Tables(RS).Rows(index)("行番号")
                    DgvAdd.Rows(index).Cells("仕入区分").Value = ds3.Tables(RS).Rows(index)("仕入区分")
                    DgvAdd.Rows(index).Cells("メーカー").Value = ds3.Tables(RS).Rows(index)("メーカー")
                    DgvAdd.Rows(index).Cells("品名").Value = ds3.Tables(RS).Rows(index)("品名")
                    DgvAdd.Rows(index).Cells("型式").Value = ds3.Tables(RS).Rows(index)("型式")
                    DgvAdd.Rows(index).Cells("仕入先").Value = ds3.Tables(RS).Rows(index)("仕入先名")
                    DgvAdd.Rows(index).Cells("単位").Value = ds3.Tables(RS).Rows(index)("単位")
                    DgvAdd.Rows(index).Cells("売単価").Value = ds3.Tables(RS).Rows(index)("売単価")
                    DgvAdd.Rows(index).Cells("売上数量").Value = 0
                    DgvAdd.Rows(index).Cells("備考").Value = ds3.Tables(RS).Rows(index)("備考")
                End If
            Next

            '行番号の振り直し
            Dim i1 As Integer = DgvOrder.Rows.Count()
            Dim No1 As Integer = 1
            For c As Integer = 0 To i1 - 1
                DgvOrder.Rows(c).Cells(0).Value = No1
                No1 += 1
            Next c
            TxtCount1.Text = DgvOrder.Rows.Count()

            Dim i2 As Integer = DgvHistory.Rows.Count()
            Dim No2 As Integer = 1
            For c As Integer = 0 To i2 - 1
                DgvHistory.Rows(c).Cells(0).Value = No2
                No2 += 1
            Next c
            TxtCount2.Text = DgvHistory.Rows.Count()

            Dim i3 As Integer = DgvAdd.Rows.Count()
            Dim No3 As Integer = 1
            For c As Integer = 0 To i3 - 1
                DgvAdd.Rows(c).Cells(0).Value = No3
                No3 += 1
            Next c
            TxtCount3.Text = DgvAdd.Rows.Count()

            TxtOrderNo.Text = ds1.Tables(RS).Rows(0)("受注番号")
            TxtOrderDate.Text = ds1.Tables(RS).Rows(0)("受注日")
            TxtCustomerCode.Text = ds1.Tables(RS).Rows(0)("得意先コード")
            TxtCustomerName.Text = ds1.Tables(RS).Rows(0)("得意先名")

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        'Dim openForm As Form = Nothing
        'openForm = New frmC01F30_Menu(_msgHd, _langHd, _db)
        'openForm.Show()
        Me.Close()
    End Sub

    Private Sub BtnRegist_Click(sender As Object, e As EventArgs) Handles BtnRegist.Click
        Dim dtToday As DateTime = DateTime.Now
        Dim errFlg As Boolean = True

        Dim Sql1 As String = ""
        Dim Sql2 As String = ""
        Dim Sql3 As String = ""
        Dim Sql4 As String = ""
        Dim Sql5 As String = ""
        Dim Sql6 As String = ""
        Dim Sql7 As String = ""
        Dim Sql8 As String = ""
        Dim Sql9 As String = ""
        Dim Saiban1 As String = ""
        Dim Saiban2 As String = ""

        Sql1 += "SELECT "
        Sql1 += "* "
        Sql1 += "FROM "
        Sql1 += "public"
        Sql1 += "."
        Sql1 += "t10_cymnhd"
        Sql1 += " WHERE "
        Sql1 += "受注番号"
        Sql1 += " ILIKE "
        Sql1 += "'"
        Sql1 += No
        Sql1 += "'"
        Sql1 += " AND "
        Sql1 += "受注番号枝番"
        Sql1 += " ILIKE "
        Sql1 += "'"
        Sql1 += Suffix
        Sql1 += "'"

        Dim reccnt As Integer = 0
        Dim ds1 As DataSet = _db.selectDB(Sql1, RS, reccnt)

        Sql2 += "SELECT "
        Sql2 += "* "
        Sql2 += "FROM "
        Sql2 += "public"
        Sql2 += "."
        Sql2 += "t11_cymndt"
        Sql2 += " WHERE "
        Sql2 += "受注番号"
        Sql2 += " ILIKE "
        Sql2 += "'"
        Sql2 += No
        Sql2 += "'"
        Sql2 += " AND "
        Sql2 += "受注番号枝番"
        Sql2 += " ILIKE "
        Sql2 += "'"
        Sql2 += Suffix
        Sql2 += "'"

        Dim ds2 As DataSet = _db.selectDB(Sql2, RS, reccnt)


        For i As Integer = 0 To DgvAdd.Rows.Count() - 1
            If ds2.Tables(RS).Rows(i)("受注数量") < ds2.Tables(RS).Rows(i)("売上数量") + DgvAdd.Rows(i).Cells("売上数量").Value Then
                MessageBox.Show("正しい値を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
                errFlg = False
            End If
        Next


        If errFlg Then
            Saiban1 += "SELECT "
            Saiban1 += "* "
            Saiban1 += "FROM "
            Saiban1 += "public"
            Saiban1 += "."
            Saiban1 += "m80_saiban"
            Saiban1 += " WHERE "
            Saiban1 += "採番キー"
            Saiban1 += " ILIKE "
            Saiban1 += "'"
            Saiban1 += "40"
            Saiban1 += "'"

            Saiban2 += "SELECT "
            Saiban2 += "* "
            Saiban2 += "FROM "
            Saiban2 += "public"
            Saiban2 += "."
            Saiban2 += "m80_saiban"
            Saiban2 += " WHERE "
            Saiban2 += "採番キー"
            Saiban2 += " ILIKE "
            Saiban2 += "'"
            Saiban2 += "70"
            Saiban2 += "'"

            Dim dsSaiban1 As DataSet = _db.selectDB(Saiban1, RS, reccnt)
            Dim dsSaiban2 As DataSet = _db.selectDB(Saiban2, RS, reccnt)

            Dim ER As String = dsSaiban1.Tables(RS).Rows(0)("接頭文字")
            ER += dtToday.ToString("MMdd")
            ER += dsSaiban1.Tables(RS).Rows(0)("最新値").ToString.PadLeft(dsSaiban1.Tables(RS).Rows(0)("連番桁数"), "0")

            Dim LS As String = dsSaiban2.Tables(RS).Rows(0)("接頭文字")
            LS += dtToday.ToString("MMdd")
            LS += dsSaiban2.Tables(RS).Rows(0)("最新値").ToString.PadLeft(dsSaiban2.Tables(RS).Rows(0)("連番桁数"), "0")

            Sql3 = ""
            Sql3 += "INSERT INTO "
            Sql3 += "Public."
            Sql3 += "t30_urighd("
            Sql3 += "会社コード, 売上番号, 売上番号枝番, 受注番号, 受注番号枝番, 見積番号, 見積番号枝番, 得意先コード, 得意先名, 得意先郵便番号, 得意先住所, 得意先電話番号, 得意先ＦＡＸ, 得意先担当者役職, 得意先担当者名, 見積日, 見積有効期限, 支払条件, 見積金額, 売上金額, 粗利額, 営業担当者, 入力担当者, 備考, 取消日, 取消区分, ＶＡＴ, ＰＰＨ, 受注日, 売上日, 入金予定日, 登録日, 更新日, 更新者)"
            Sql3 += " VALUES('"
            Sql3 += ds1.Tables(RS).Rows(0)("会社コード").ToString
            Sql3 += "', '"
            Sql3 += ER
            Sql3 += "', '"
            Sql3 += "1"
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("受注番号").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("受注番号枝番").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("見積番号").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("見積番号枝番").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("得意先コード").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("得意先名").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("得意先郵便番号").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("得意先住所").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("得意先電話番号").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("得意先ＦＡＸ").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("得意先担当者役職").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("得意先担当者名").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("見積日").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("見積有効期限").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("支払条件").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("見積金額").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("見積金額").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("粗利額").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("営業担当者").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("入力担当者").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("備考").ToString
            Sql3 += "', "
            Sql3 += "null"
            Sql3 += ", "
            Sql3 += "null"
            Sql3 += ", '"
            Sql3 += ds1.Tables(RS).Rows(0)("ＶＡＴ").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("ＰＰＨ").ToString
            Sql3 += "', '"
            Sql3 += dtToday
            Sql3 += "', '"
            Sql3 += dtToday
            Sql3 += "', '"
            Sql3 += dtToday
            Sql3 += "', '"
            Sql3 += dtToday
            Sql3 += "', '"
            Sql3 += dtToday
            Sql3 += "', '"
            Sql3 += Input
            Sql3 += " ')"
            Sql3 += "RETURNING 会社コード"
            Sql3 += ", "
            Sql3 += "売上番号"
            Sql3 += ", "
            Sql3 += "売上番号枝番"
            Sql3 += ", "
            Sql3 += "受注番号"
            Sql3 += ", "
            Sql3 += "受注番号枝番"
            Sql3 += ", "
            Sql3 += "見積番号"
            Sql3 += ", "
            Sql3 += "見積番号枝番"
            Sql3 += ", "
            Sql3 += "得意先コード"
            Sql3 += ", "
            Sql3 += "得意先名"
            Sql3 += ", "
            Sql3 += "得意先郵便番号"
            Sql3 += ", "
            Sql3 += "得意先住所"
            Sql3 += ", "
            Sql3 += "得意先電話番号"
            Sql3 += ", "
            Sql3 += "得意先ＦＡＸ"
            Sql3 += ", "
            Sql3 += "得意先担当者役職"
            Sql3 += ", "
            Sql3 += "得意先担当者名"
            Sql3 += ", "
            Sql3 += "見積日"
            Sql3 += ", "
            Sql3 += "見積有効期限"
            Sql3 += ", "
            Sql3 += "支払条件"
            Sql3 += ", "
            Sql3 += "見積金額"
            Sql3 += ", "
            Sql3 += "売上金額"
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
            Sql3 += "受注日"
            Sql3 += ", "
            Sql3 += "売上日"
            Sql3 += ", "
            Sql3 += "入金予定日"
            Sql3 += ", "
            Sql3 += "登録日"
            Sql3 += ", "
            Sql3 += "更新日"
            Sql3 += ", "
            Sql3 += "更新者"

            _db.executeDB(Sql3)

            For index As Integer = 0 To DgvAdd.Rows.Count() - 1
                Sql4 = ""
                Sql4 += "INSERT INTO "
                Sql4 += "Public."
                Sql4 += "t31_urigdt("
                Sql4 += "会社コード, 売上番号, 売上番号枝番, 受注番号, 受注番号枝番, 行番号, 仕入区分, メーカー, 品名, 型式, 仕入先名, 仕入値, 受注数量, 売上数量, 受注残数, 単位, 間接費, 売単価, 売上金額, 粗利額, 粗利率, リードタイム, 備考, 更新者, 更新日)"
                Sql4 += " VALUES('"
                Sql4 += ds2.Tables(RS).Rows(index)("会社コード").ToString
                Sql4 += "', '"
                Sql4 += ER
                Sql4 += "', '"
                Sql4 += "1"
                Sql4 += "', '"
                Sql4 += ds2.Tables(RS).Rows(index)("受注番号").ToString
                Sql4 += "', '"
                Sql4 += ds2.Tables(RS).Rows(index)("受注番号枝番").ToString
                Sql4 += "', '"
                Sql4 += DgvAdd.Rows(index).Cells("行番号").Value.ToString
                Sql4 += "', '"
                Sql4 += DgvAdd.Rows(index).Cells("仕入区分").Value.ToString
                Sql4 += "', '"
                Sql4 += DgvAdd.Rows(index).Cells("メーカー").Value.ToString
                Sql4 += "', '"
                Sql4 += DgvAdd.Rows(index).Cells("品名").Value.ToString
                Sql4 += "', '"
                Sql4 += DgvAdd.Rows(index).Cells("型式").Value.ToString
                Sql4 += "', '"
                Sql4 += DgvAdd.Rows(index).Cells("仕入先").Value.ToString
                Sql4 += "', '"
                Sql4 += ds2.Tables(RS).Rows(index)("仕入値").ToString
                Sql4 += "', '"
                Sql4 += ds2.Tables(RS).Rows(index)("受注数量").ToString
                Sql4 += "', '"
                Sql4 += DgvAdd.Rows(index).Cells("売上数量").Value.ToString
                Sql4 += "', '"

                Dim OrderNo As Integer = ds2.Tables(RS).Rows(index)("受注数量")
                Dim PurchaseNo As Integer = DgvAdd.Rows(index).Cells("売上数量").Value
                Dim RemainingNo As Integer = OrderNo - PurchaseNo

                Sql4 += RemainingNo.ToString
                Sql4 += "', '"
                Sql4 += DgvAdd.Rows(index).Cells("単位").Value.ToString
                Sql4 += "', '"
                Sql4 += ds2.Tables(RS).Rows(index)("間接費").ToString
                Sql4 += "', '"
                Sql4 += ds2.Tables(RS).Rows(index)("売単価").ToString
                Sql4 += "', '"
                Sql4 += ds2.Tables(RS).Rows(index)("売上金額").ToString
                Sql4 += "', '"
                Sql4 += ds2.Tables(RS).Rows(index)("粗利額").ToString
                Sql4 += "', '"
                Sql4 += ds2.Tables(RS).Rows(index)("粗利率").ToString
                Sql4 += "', '"
                Sql4 += ds2.Tables(RS).Rows(index)("リードタイム").ToString
                Sql4 += "', '"
                Sql4 += DgvAdd.Rows(index).Cells("備考").Value.ToString
                Sql4 += "', '"
                Sql4 += Input
                Sql4 += "', '"
                Sql4 += dtToday
                Sql4 += " ')"
                Sql4 += "RETURNING 会社コード"
                Sql4 += ", "
                Sql4 += "売上番号"
                Sql4 += ", "
                Sql4 += "売上番号枝番"
                Sql4 += ", "
                Sql4 += "受注番号"
                Sql4 += ", "
                Sql4 += "受注番号枝番"
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
                Sql4 += "受注数量"
                Sql4 += ", "
                Sql4 += "売上数量"
                Sql4 += ", "
                Sql4 += "受注残数"
                Sql4 += ", "
                Sql4 += "単位"
                Sql4 += ", "
                Sql4 += "間接費"
                Sql4 += ", "
                Sql4 += "売単価"
                Sql4 += ", "
                Sql4 += "売上金額"
                Sql4 += ", "
                Sql4 += "リードタイム"
                Sql4 += ", "
                Sql4 += "入金有無"
                Sql4 += ", "
                Sql4 += "入金番号"
                Sql4 += ", "
                Sql4 += "入金日"
                Sql4 += ", "
                Sql4 += "備考"
                Sql4 += ", "
                Sql4 += "更新者"
                Sql4 += ", "
                Sql4 += "更新日"

                _db.executeDB(Sql4)
            Next

            Dim ERNo As Integer

            If dsSaiban1.Tables(RS).Rows(0)("最新値") = dsSaiban1.Tables(RS).Rows(0)("最大値") Then
                ERNo = dsSaiban1.Tables(RS).Rows(0)("最小値")
            Else
                ERNo = dsSaiban1.Tables(RS).Rows(0)("最新値") + 1
            End If

            Sql5 = ""
            Sql5 += "UPDATE "
            Sql5 += "Public."
            Sql5 += "m80_saiban "
            Sql5 += "SET "
            Sql5 += " 最新値"
            Sql5 += " = '"
            Sql5 += ERNo.ToString
            Sql5 += "', "
            Sql5 += "更新者"
            Sql5 += " = '"
            Sql5 += Input
            Sql5 += "', "
            Sql5 += "更新日"
            Sql5 += " = '"
            Sql5 += dtToday
            Sql5 += "' "
            Sql5 += "WHERE"
            Sql5 += " 会社コード"
            Sql5 += "='"
            Sql5 += ds1.Tables(RS).Rows(0)("会社コード").ToString
            Sql5 += "'"
            Sql5 += " AND"
            Sql5 += " 採番キー"
            Sql5 += "='"
            Sql5 += "40"
            Sql5 += "' "
            Sql5 += "RETURNING 会社コード"
            Sql5 += ", "
            Sql5 += "採番キー"
            Sql5 += ", "
            Sql5 += "最新値"
            Sql5 += ", "
            Sql5 += "最小値"
            Sql5 += ", "
            Sql5 += "最大値"
            Sql5 += ", "
            Sql5 += "接頭文字"
            Sql5 += ", "
            Sql5 += "連番桁数"
            Sql5 += ", "
            Sql5 += "更新者"
            Sql5 += ", "
            Sql5 += "更新日"

            _db.executeDB(Sql5)

            Dim PurchaseNum As Integer
            Dim OrdingNum As Integer
            For index As Integer = 0 To ds2.Tables(RS).Rows.Count - 1
                Sql6 = ""
                Sql6 += "UPDATE "
                Sql6 += "Public."
                Sql6 += "t11_cymndt "
                Sql6 += "SET "
                Sql6 += "売上数量"
                Sql6 += " = '"
                PurchaseNum = ds2.Tables(RS).Rows(index)("売上数量") + DgvAdd.Rows(index).Cells("売上数量").Value
                Sql6 += PurchaseNum.ToString
                Sql6 += "', "
                Sql6 += " 受注残数"
                Sql6 += " = '"
                OrdingNum = ds2.Tables(RS).Rows(index)("受注残数") - DgvAdd.Rows(index).Cells("売上数量").Value
                Sql6 += OrdingNum.ToString
                Sql6 += "', "
                Sql6 += "更新者"
                Sql6 += " = '"
                Sql6 += Input
                Sql6 += "' "
                Sql6 += "WHERE"
                Sql6 += " 会社コード"
                Sql6 += "='"
                Sql6 += ds1.Tables(RS).Rows(0)("会社コード").ToString
                Sql6 += "'"
                Sql6 += " AND"
                Sql6 += " 受注番号"
                Sql6 += "='"
                Sql6 += ds2.Tables(RS).Rows(index)("受注番号").ToString
                Sql6 += "'"
                Sql6 += " AND"
                Sql6 += " 受注番号枝番"
                Sql6 += "='"
                Sql6 += ds2.Tables(RS).Rows(index)("受注番号枝番").ToString
                Sql6 += "'"
                Sql6 += " AND"
                Sql6 += " 行番号"
                Sql6 += "='"
                Sql6 += ds2.Tables(RS).Rows(index)("行番号").ToString
                Sql6 += "' "
                Sql6 += "RETURNING 会社コード"
                Sql6 += ", "
                Sql6 += "受注番号"
                Sql6 += ", "
                Sql6 += "受注番号枝番"
                Sql6 += ", "
                Sql6 += "行番号"
                Sql6 += ", "
                Sql6 += "仕入区分"
                Sql6 += ", "
                Sql6 += "メーカー"
                Sql6 += ", "
                Sql6 += "品名"
                Sql6 += ", "
                Sql6 += "型式"
                Sql6 += ", "
                Sql6 += "仕入先名"
                Sql6 += ", "
                Sql6 += "仕入値"
                Sql6 += ", "
                Sql6 += "受注数量"
                Sql6 += ", "
                Sql6 += "売上数量"
                Sql6 += ", "
                Sql6 += "受注残数"
                Sql6 += ", "
                Sql6 += "単位"
                Sql6 += ", "
                Sql6 += "間接費"
                Sql6 += ", "
                Sql6 += "売単価"
                Sql6 += ", "
                Sql6 += "売上金額"
                Sql6 += ", "
                Sql6 += "リードタイム"
                Sql6 += ", "
                Sql6 += "出庫数"
                Sql6 += ", "
                Sql6 += "未出庫数"
                Sql6 += ", "
                Sql6 += "備考"
                Sql6 += ", "
                Sql6 += "更新者"

                _db.executeDB(Sql6)
            Next

            Sql7 = ""
            Sql7 += "INSERT INTO "
            Sql7 += "Public."
            Sql7 += "t44_shukohd("
            Sql7 += "会社コード, 出庫番号, 見積番号, 見積番号枝番, 受注番号, 受注番号枝番, 得意先コード, 得意先名, 得意先郵便番号, 得意先住所, 得意先電話番号, 得意先ＦＡＸ, 得意先担当者役職, 得意先担当者名, 営業担当者, 入力担当者, 備考, 取消日, 取消区分, 出庫日, 登録日, 更新日, 更新者)"
            Sql7 += " VALUES('"
            Sql7 += ds1.Tables(RS).Rows(0)("会社コード").ToString
            Sql7 += "', '"
            Sql7 += LS
            Sql7 += "', '"
            Sql7 += ds1.Tables(RS).Rows(0)("見積番号").ToString
            Sql7 += "', '"
            Sql7 += ds1.Tables(RS).Rows(0)("見積番号枝番").ToString
            Sql7 += "', '"
            Sql7 += ds1.Tables(RS).Rows(0)("受注番号").ToString
            Sql7 += "', '"
            Sql7 += ds1.Tables(RS).Rows(0)("受注番号枝番").ToString
            Sql7 += "', '"
            Sql7 += ds1.Tables(RS).Rows(0)("得意先コード").ToString
            Sql7 += "', '"
            Sql7 += ds1.Tables(RS).Rows(0)("得意先名").ToString
            Sql7 += "', '"
            Sql7 += ds1.Tables(RS).Rows(0)("得意先郵便番号").ToString
            Sql7 += "', '"
            Sql7 += ds1.Tables(RS).Rows(0)("得意先住所").ToString
            Sql7 += "', '"
            Sql7 += ds1.Tables(RS).Rows(0)("得意先電話番号").ToString
            Sql7 += "', '"
            Sql7 += ds1.Tables(RS).Rows(0)("得意先ＦＡＸ").ToString
            Sql7 += "', '"
            Sql7 += ds1.Tables(RS).Rows(0)("得意先担当者役職").ToString
            Sql7 += "', '"
            Sql7 += ds1.Tables(RS).Rows(0)("得意先担当者名").ToString
            Sql7 += "', '"
            Sql7 += ds1.Tables(RS).Rows(0)("営業担当者").ToString
            Sql7 += "', '"
            Sql7 += ds1.Tables(RS).Rows(0)("入力担当者").ToString
            Sql7 += "', '"
            Sql7 += ds1.Tables(RS).Rows(0)("備考").ToString
            Sql7 += "', "
            Sql7 += "null"
            Sql7 += ", "
            Sql7 += "null"
            Sql7 += ", '"
            Sql7 += dtToday
            Sql7 += "', '"
            Sql7 += dtToday
            Sql7 += "', '"
            Sql7 += dtToday
            Sql7 += "', '"
            Sql7 += Input
            Sql7 += " ')"
            Sql7 += "RETURNING 会社コード"
            Sql7 += ", "
            Sql7 += "出庫番号"
            Sql7 += ", "
            Sql7 += "見積番号"
            Sql7 += ", "
            Sql7 += "見積番号枝番"
            Sql7 += ", "
            Sql7 += "受注番号"
            Sql7 += ", "
            Sql7 += "受注番号枝番"
            Sql7 += ", "
            Sql7 += "得意先コード"
            Sql7 += ", "
            Sql7 += "得意先名"
            Sql7 += ", "
            Sql7 += "得意先郵便番号"
            Sql7 += ", "
            Sql7 += "得意先住所"
            Sql7 += ", "
            Sql7 += "得意先電話番号"
            Sql7 += ", "
            Sql7 += "得意先ＦＡＸ"
            Sql7 += ", "
            Sql7 += "得意先担当者役職"
            Sql7 += ", "
            Sql7 += "得意先担当者名"
            Sql7 += ", "
            Sql7 += "営業担当者"
            Sql7 += ", "
            Sql7 += "入力担当者"
            Sql7 += ", "
            Sql7 += "備考"
            Sql7 += ", "
            Sql7 += "取消日"
            Sql7 += ", "
            Sql7 += "取消区分"
            Sql7 += ", "
            Sql7 += "出庫日"
            Sql7 += ", "
            Sql7 += "登録日"
            Sql7 += ", "
            Sql7 += "更新日"
            Sql7 += ", "
            Sql7 += "更新者"

            _db.executeDB(Sql7)

            For index As Integer = 0 To DgvAdd.Rows.Count() - 1
                Sql8 = ""
                Sql8 += "INSERT INTO "
                Sql8 += "Public."
                Sql8 += "t45_shukodt("
                Sql8 += "会社コード, 出庫番号, 受注番号, 受注番号枝番, 行番号, 仕入区分, メーカー, 品名, 型式, 仕入先名, 売単価, 出庫数量, 単位, 備考, 更新者, 更新日)"
                Sql8 += " VALUES('"
                Sql8 += ds1.Tables(RS).Rows(0)("会社コード").ToString
                Sql8 += "', '"
                Sql8 += LS
                Sql8 += "', '"
                Sql8 += ds1.Tables(RS).Rows(0)("受注番号").ToString
                Sql8 += "', '"
                Sql8 += ds1.Tables(RS).Rows(0)("受注番号枝番").ToString
                Sql8 += "', '"
                Sql8 += DgvAdd.Rows(index).Cells("No").Value.ToString
                Sql8 += "', '"
                Sql8 += DgvAdd.Rows(index).Cells("仕入区分").Value.ToString
                Sql8 += "', '"
                Sql8 += DgvAdd.Rows(index).Cells("メーカー").Value.ToString
                Sql8 += "', '"
                Sql8 += DgvAdd.Rows(index).Cells("品名").Value.ToString
                Sql8 += "', '"
                Sql8 += DgvAdd.Rows(index).Cells("型式").Value.ToString
                Sql8 += "', '"
                Sql8 += DgvAdd.Rows(index).Cells("仕入先").Value.ToString
                Sql8 += "', '"
                Sql8 += DgvAdd.Rows(index).Cells("売単価").Value.ToString
                Sql8 += "', '"
                Sql8 += DgvAdd.Rows(index).Cells("売上数量").Value.ToString
                Sql8 += "', '"
                Sql8 += DgvAdd.Rows(index).Cells("単位").Value.ToString
                Sql8 += "', '"
                Sql8 += DgvAdd.Rows(index).Cells("備考").Value.ToString
                Sql8 += "', '"
                Sql8 += Input
                Sql8 += "', '"
                Sql8 += dtToday
                Sql8 += " ')"
                Sql8 += "RETURNING 会社コード"
                Sql8 += ", "
                Sql8 += "出庫番号"
                Sql8 += ", "
                Sql8 += "受注番号"
                Sql8 += ", "
                Sql8 += "受注番号枝番"
                Sql8 += ", "
                Sql8 += "行番号"
                Sql8 += ", "
                Sql8 += "仕入区分"
                Sql8 += ", "
                Sql8 += "メーカー"
                Sql8 += ", "
                Sql8 += "品名"
                Sql8 += ", "
                Sql8 += "型式"
                Sql8 += ", "
                Sql8 += "仕入先名"
                Sql8 += ", "
                Sql8 += "売単価"
                Sql8 += ", "
                Sql8 += "出庫数量"
                Sql8 += ", "
                Sql8 += "単位"
                Sql8 += ", "
                Sql8 += "備考"
                Sql8 += ", "
                Sql8 += "更新者"
                Sql8 += ", "
                Sql8 += "更新日"

                _db.executeDB(Sql8)
            Next

            Dim LSNo As Integer

            If dsSaiban2.Tables(RS).Rows(0)("最新値") = dsSaiban2.Tables(RS).Rows(0)("最大値") Then
                LSNo = dsSaiban2.Tables(RS).Rows(0)("最小値")
            Else
                LSNo = dsSaiban2.Tables(RS).Rows(0)("最新値") + 1
            End If

            Sql9 = ""
            Sql9 += "UPDATE "
            Sql9 += "Public."
            Sql9 += "m80_saiban "
            Sql9 += "SET "
            Sql9 += " 最新値"
            Sql9 += " = '"
            Sql9 += LSNo.ToString
            Sql9 += "', "
            Sql9 += "更新者"
            Sql9 += " = '"
            Sql9 += Input
            Sql9 += "', "
            Sql9 += "更新日"
            Sql9 += " = '"
            Sql9 += dtToday
            Sql9 += "' "
            Sql9 += "WHERE"
            Sql9 += " 会社コード"
            Sql9 += "='"
            Sql9 += ds1.Tables(RS).Rows(0)("会社コード").ToString
            Sql9 += "'"
            Sql9 += " AND"
            Sql9 += " 採番キー"
            Sql9 += "='"
            Sql9 += "70"
            Sql9 += "' "
            Sql9 += "RETURNING 会社コード"
            Sql9 += ", "
            Sql9 += "採番キー"
            Sql9 += ", "
            Sql9 += "最新値"
            Sql9 += ", "
            Sql9 += "最小値"
            Sql9 += ", "
            Sql9 += "最大値"
            Sql9 += ", "
            Sql9 += "接頭文字"
            Sql9 += ", "
            Sql9 += "連番桁数"
            Sql9 += ", "
            Sql9 += "更新者"
            Sql9 += ", "
            Sql9 += "更新日"

            _db.executeDB(Sql9)
        End If

    End Sub
End Class