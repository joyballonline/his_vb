Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports System.Globalization
Imports System.Text.RegularExpressions
Imports Microsoft.Office.Interop

Public Class OrderRemainingList
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
    Private _langHd As UtilLangHandler
    Private _db As UtilDBIf
    'Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ
    Private CompanyCode As String = ""
    Private OrderNo As String()
    Private OrderStatus As String = ""


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
        OrderStatus = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True
        DgvCymndt.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.DisplayedCells
    End Sub

    '画面表示時
    Private Sub MstHanyoue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblMode.Text = OrderStatus & " Mode"
            DgvCymndt.Columns("受注番号").HeaderText = "JobOrderNo"
            DgvCymndt.Columns("受注日").HeaderText = "JobOrderDate"
            DgvCymndt.Columns("得意先名").HeaderText = "CustomerName"
            DgvCymndt.Columns("メーカー").HeaderText = "Manufacturer"
            DgvCymndt.Columns("品名").HeaderText = "ItemName"
            DgvCymndt.Columns("型式").HeaderText = "Spec"
            DgvCymndt.Columns("数量").HeaderText = "OrderQuantity"
            DgvCymndt.Columns("単位").HeaderText = "Unit"
            DgvCymndt.Columns("単価").HeaderText = "UnitPrice"
            DgvCymndt.Columns("ＶＡＴ").HeaderText = "VAT"
            DgvCymndt.Columns("計").HeaderText = "Amount"
            DgvCymndt.Columns("受注残数").HeaderText = "OrderRemainingAmount"
            DgvCymndt.Columns("備考").HeaderText = "Remarks"

        End If

        LblMode.Text = "参照モード"

        Sql = "SELECT t10.受注番号, t10.受注番号枝番, t11.行番号, t10.受注日, t10.得意先名, t11.メーカー, t11.品名, t11.型式, t11.受注数量"
        Sql += ",t11.単位, t11.見積単価, t10.ＶＡＴ, t11.売上金額, t11.受注残数, t11.備考"
        Sql += " FROM  public.t11_cymndt t11 "
        Sql += " INNER JOIN  t10_cymnhd t10"
        Sql += " ON t11.会社コード = t10.会社コード"
        Sql += " AND  t11.受注番号 = t10.受注番号"

        Sql += " WHERE t11.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND "
        Sql += " t11.受注残数 <> 0 "
        Sql += " AND "
        Sql += " t10.取消区分 = 0 "
        Sql += " ORDER BY t10.受注日 DESC"

        Try

            Dim dsCymndt As DataSet = _db.selectDB(Sql, RS, reccnt)

            For i As Integer = 0 To dsCymndt.Tables(RS).Rows.Count - 1
                DgvCymndt.Rows.Add()
                DgvCymndt.Rows(i).Cells("受注番号").Value = dsCymndt.Tables(RS).Rows(i)("受注番号")
                DgvCymndt.Rows(i).Cells("受注番号枝番").Value = dsCymndt.Tables(RS).Rows(i)("受注番号枝番")
                DgvCymndt.Rows(i).Cells("行番号").Value = dsCymndt.Tables(RS).Rows(i)("行番号")
                DgvCymndt.Rows(i).Cells("受注日").Value = dsCymndt.Tables(RS).Rows(i)("受注日").ToShortDateString()
                DgvCymndt.Rows(i).Cells("得意先名").Value = dsCymndt.Tables(RS).Rows(i)("得意先名")
                DgvCymndt.Rows(i).Cells("メーカー").Value = dsCymndt.Tables(RS).Rows(i)("メーカー")
                DgvCymndt.Rows(i).Cells("品名").Value = dsCymndt.Tables(RS).Rows(i)("品名")
                DgvCymndt.Rows(i).Cells("型式").Value = dsCymndt.Tables(RS).Rows(i)("型式")
                DgvCymndt.Rows(i).Cells("数量").Value = dsCymndt.Tables(RS).Rows(i)("受注数量")
                DgvCymndt.Rows(i).Cells("単位").Value = dsCymndt.Tables(RS).Rows(i)("単位")
                DgvCymndt.Rows(i).Cells("単価").Value = dsCymndt.Tables(RS).Rows(i)("見積単価")
                DgvCymndt.Rows(i).Cells("ＶＡＴ").Value = dsCymndt.Tables(RS).Rows(i)("ＶＡＴ")
                DgvCymndt.Rows(i).Cells("計").Value = dsCymndt.Tables(RS).Rows(i)("受注数量") * dsCymndt.Tables(RS).Rows(i)("見積単価")
                DgvCymndt.Rows(i).Cells("受注残数").Value = dsCymndt.Tables(RS).Rows(i)("受注残数")
                DgvCymndt.Rows(i).Cells("備考").Value = dsCymndt.Tables(RS).Rows(i)("備考")
            Next

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
        Dim openForm As Form = Nothing
        openForm = New frmC01F30_Menu(_msgHd, _langHd, _db)
        openForm.Show()
        Me.Close()
    End Sub

    'excel出力ボタン押下時
    Private Sub BtnExcelOutput_Click(sender As Object, e As EventArgs) Handles BtnExcelOutput.Click
        '対象データがない場合は取消操作不可能
        If DgvCymndt.Rows.Count = 0 Then

            '該当データがないアラートを出す
            _msgHd.dspMSG("noTargetData", frmC01F10_Login.loginValue.Language)

            Return

        Else

            'Excel出力処理
            outputExcel()
        End If
    End Sub

    'excel出力処理
    Private Sub outputExcel()

        '定義
        Dim app As Excel.Application = Nothing
        Dim book As Excel.Workbook = Nothing
        Dim sheet As Excel.Worksheet = Nothing



        Try
            '雛形パス
            Dim sHinaPath As String = StartUp._iniVal.BaseXlsPath
            '雛形ファイル名
            Dim sHinaFile As String = sHinaPath & "\" & "OrderRemainingList.xlsx"
            '出力先パス
            Dim sOutPath As String = StartUp._iniVal.OutXlsPath
            '出力ファイル名
            Dim sOutFile As String = sOutPath & "\OrderRemainingList_" & DateTime.Now.ToString("yyyyMMddHHmm") & ".xlsx"

            app = New Excel.Application()
            book = app.Workbooks.Add(sHinaFile)  'テンプレート
            sheet = CType(book.Worksheets(1), Excel.Worksheet)

            sheet.PageSetup.RightHeader = "出力日：" & DateTime.Now.ToShortDateString

            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                sheet.PageSetup.RightHeader = "OutputDate：" & DateTime.Now.ToShortDateString

                sheet.Range("A1").Value = "JobOrderNo"
                sheet.Range("B1").Value = "JobOrderDate"
                sheet.Range("C1").Value = "CustomerName"
                sheet.Range("D1").Value = "Manufacturer"
                sheet.Range("E1").Value = "ItemName"
                sheet.Range("F1").Value = "Spec"
                sheet.Range("G1").Value = "Quantity"
                sheet.Range("H1").Value = "Unit"
                sheet.Range("I1").Value = "UnitPrice"
                sheet.Range("J1").Value = "ＶＡＴ"
                sheet.Range("K1").Value = "Amount"
                sheet.Range("L1").Value = "OrderRemainingAmount"
                sheet.Range("M1").Value = "Remarks"
            End If

            For i As Integer = 0 To DgvCymndt.RowCount - 1
                Dim cellRowIndex As Integer = 2
                cellRowIndex += i

                sheet.Range("A" & cellRowIndex.ToString).Value = DgvCymndt.Rows(i).Cells("受注番号").Value
                sheet.Range("B" & cellRowIndex.ToString).Value = DgvCymndt.Rows(i).Cells("受注日").Value
                sheet.Range("C" & cellRowIndex.ToString).Value = DgvCymndt.Rows(i).Cells("得意先名").Value
                sheet.Range("D" & cellRowIndex.ToString).Value = DgvCymndt.Rows(i).Cells("メーカー").Value
                sheet.Range("E" & cellRowIndex.ToString).Value = DgvCymndt.Rows(i).Cells("品名").Value
                sheet.Range("F" & cellRowIndex.ToString).Value = DgvCymndt.Rows(i).Cells("型式").Value
                sheet.Range("G" & cellRowIndex.ToString).Value = DgvCymndt.Rows(i).Cells("数量").Value
                sheet.Range("H" & cellRowIndex.ToString).Value = DgvCymndt.Rows(i).Cells("単位").Value
                sheet.Range("I" & cellRowIndex.ToString).Value = DgvCymndt.Rows(i).Cells("単価").Value
                sheet.Range("J" & cellRowIndex.ToString).Value = DgvCymndt.Rows(i).Cells("ＶＡＴ").Value
                sheet.Range("K" & cellRowIndex.ToString).Value = DgvCymndt.Rows(i).Cells("計").Value
                sheet.Range("L" & cellRowIndex.ToString).Value = DgvCymndt.Rows(i).Cells("受注残数").Value
                sheet.Range("M" & cellRowIndex.ToString).Value = DgvCymndt.Rows(i).Cells("備考").Value

            Next

            book.SaveAs(sOutFile)
            app.Visible = True

            _msgHd.dspMSG("CreateExcel", frmC01F10_Login.loginValue.Language)

        Catch ex As Exception
            Throw ex


        Finally
            'app.Quit()
            'Marshal.ReleaseComObject(sheet)
            'Marshal.ReleaseComObject(book)
            'Marshal.ReleaseComObject(app)

        End Try


    End Sub

    'param1：String テーブル名
    'param2：String 詳細条件
    'Return: DataSet
    Private Function getDsData(ByVal tableName As String, Optional ByRef txtParam As String = "") As DataSet
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        Sql += "SELECT * FROM public." & tableName
        Sql += " WHERE "
        Sql += "会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += txtParam
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

End Class