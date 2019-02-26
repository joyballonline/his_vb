Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports System.Text.RegularExpressions
Imports System.Globalization

Public Class BillingList
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

    Private Sub BillingList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If _status = CommonConst.STATUS_VIEW Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "ViewMode"
            Else
                LblMode.Text = "参照モード"
            End If

            'BtnBillingView.Visible = True
            'BtnBillingView.Location = New Point(997, 677)
        ElseIf _status = CommonConst.STATUS_CANCEL Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "CancelMode"
            Else
                LblMode.Text = "取消モード"
            End If

            BtnBillingCancel.Visible = True
            BtnBillingCancel.Location = New Point(997, 509)
        End If

        '検索（Date）の初期値
        dtBillingDateSince.Value = DateAdd("d", CommonConst.SINCE_DEFAULT_DAY, DateTime.Today)
        dtBillingDateUntil.Value = DateTime.Today

        '請求一覧を表示
        PurchaseListLoad()

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblConditions.Text = "ExtractionCondition"
            Label1.Text = "CustomerName"
            Label4.Text = "CustomerCode"
            Label8.Text = "BillingDate"
            Label7.Text = "InvoiceNumber"
            Label11.Text = "CustomerNumber"
            Label10.Text = "DisplayFormat"

            ChkCancelData.Text = "IncludeCancelData"

            BtnBillingSearch.Text = "Search"
            BtnBillingCancel.Text = "CancelOfInvoice"
            BtnBillingView.Text = "BillingDataView"
            BtnBack.Text = "Back"
        End If
    End Sub

    '表示処理
    Private Sub PurchaseListLoad()

        '一覧クリア
        DgvBilling.Rows.Clear()
        DgvBilling.Columns.Clear()

        Dim Sql As String = ""

        Try
            Sql += searchConditions() '抽出条件取得
            Sql += viewFormat() '表示形式条件

            Sql += " ORDER BY "
            Sql += "更新日 DESC"

            ds = getDsData("t23_skyuhd", Sql)

            '英語の表記
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                DgvBilling.Columns.Add("取消", "Cancel")
                DgvBilling.Columns.Add("請求番号", "InvoiceNumber")
                DgvBilling.Columns.Add("請求区分", "BillingClassification")
                DgvBilling.Columns.Add("請求日", "BillingDate")
                DgvBilling.Columns.Add("客先番号", "CustomerNumber")
                DgvBilling.Columns.Add("受注番号", "JobOrderNumber")
                DgvBilling.Columns.Add("受注番号枝番", "JobOrderSubNumber")
                DgvBilling.Columns.Add("得意先コード", "CustomerCode")
                DgvBilling.Columns.Add("得意先名", "CustomerName")
                DgvBilling.Columns.Add("請求金額計", "TotalBillingAmount")
                DgvBilling.Columns.Add("売掛残高", "AccountsReceivableBalance")
                DgvBilling.Columns.Add("備考1", "Remarks1")
                DgvBilling.Columns.Add("備考2", "Remarks2")
                DgvBilling.Columns.Add("更新日", "ModifiedDate")
                DgvBilling.Columns.Add("更新者", "ModifiedBy")
            Else
                DgvBilling.Columns.Add("取消", "取消")
                DgvBilling.Columns.Add("請求番号", "請求番号")
                DgvBilling.Columns.Add("請求区分", "請求区分")
                DgvBilling.Columns.Add("請求日", "請求日")
                DgvBilling.Columns.Add("客先番号", "客先番号")
                DgvBilling.Columns.Add("受注番号", "受注番号")
                DgvBilling.Columns.Add("受注番号枝番", "受注番号枝番")
                DgvBilling.Columns.Add("得意先コード", "得意先コード")
                DgvBilling.Columns.Add("得意先名", "得意先名")
                DgvBilling.Columns.Add("請求金額計", "請求金額計")
                DgvBilling.Columns.Add("売掛残高", "売掛残高")
                DgvBilling.Columns.Add("備考1", "備考1")
                DgvBilling.Columns.Add("備考2", "備考2")
                DgvBilling.Columns.Add("更新日", "更新日")
                DgvBilling.Columns.Add("更新者", "更新者")
            End If

            DgvBilling.Columns("請求金額計").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvBilling.Columns("売掛残高").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvBilling.Rows.Add()
                DgvBilling.Rows(i).Cells("請求番号").Value = ds.Tables(RS).Rows(i)("請求番号")

                DgvBilling.Rows(i).Cells("請求区分").Value = IIf(
                        ds.Tables(RS).Rows(i)("請求区分") = CommonConst.BILLING_KBN_DEPOSIT,
                        CommonConst.BILLING_KBN_DEPOSIT_TXT,
                        CommonConst.BILLING_KBN_NORMAL_TXT
                    )

                DgvBilling.Rows(i).Cells("取消").Value = getDelKbnTxt(ds.Tables(RS).Rows(i)("取消区分"))
                DgvBilling.Rows(i).Cells("請求日").Value = ds.Tables(RS).Rows(i)("請求日").ToShortDateString()
                DgvBilling.Rows(i).Cells("客先番号").Value = ds.Tables(RS).Rows(i)("客先番号")
                DgvBilling.Rows(i).Cells("受注番号").Value = ds.Tables(RS).Rows(i)("受注番号")
                DgvBilling.Rows(i).Cells("受注番号枝番").Value = ds.Tables(RS).Rows(i)("受注番号枝番")
                DgvBilling.Rows(i).Cells("得意先コード").Value = ds.Tables(RS).Rows(i)("得意先コード")
                DgvBilling.Rows(i).Cells("得意先名").Value = ds.Tables(RS).Rows(i)("得意先名")
                DgvBilling.Rows(i).Cells("請求金額計").Value = ds.Tables(RS).Rows(i)("請求金額計")
                DgvBilling.Rows(i).Cells("売掛残高").Value = ds.Tables(RS).Rows(i)("売掛残高")
                DgvBilling.Rows(i).Cells("備考1").Value = ds.Tables(RS).Rows(i)("備考1")
                DgvBilling.Rows(i).Cells("備考2").Value = ds.Tables(RS).Rows(i)("備考2")
                DgvBilling.Rows(i).Cells("更新日").Value = ds.Tables(RS).Rows(i)("更新日")
                DgvBilling.Rows(i).Cells("更新者").Value = ds.Tables(RS).Rows(i)("更新者")
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        Dim openForm As Form = Nothing
        openForm = New frmC01F30_Menu(_msgHd, _langHd, _db)
        openForm.Show()
        Me.Close()
    End Sub

    '検索ボタン押下時
    Private Sub BtnPurchaseSearch_Click(sender As Object, e As EventArgs) Handles BtnBillingSearch.Click
        PurchaseListLoad() '一覧再表示
    End Sub

    '参照ボタン押下時
    Private Sub BtnBillingView_Click(sender As Object, e As EventArgs) Handles BtnBillingView.Click
        '実行できるデータがあるかチェック
        If actionChk() = False Then
            Return
        End If

        Dim RowIdx As Integer
        RowIdx = Me.DgvBilling.CurrentCell.RowIndex
        Dim No As String = DgvBilling.Rows(RowIdx).Cells("受注番号").Value
        Dim Suffix As String = DgvBilling.Rows(RowIdx).Cells("受注番号枝番").Value
        Dim Status As String = CommonConst.STATUS_VIEW
        Dim openForm As Form = Nothing
        openForm = New BillingManagement(_msgHd, _db, _langHd, Me, No, Suffix, Status)   '処理選択
        openForm.Show(Me)
    End Sub

    '取消データチェックイベント
    Private Sub ChkCancelData_CheckedChanged(sender As Object, e As EventArgs) Handles ChkCancelData.CheckedChanged
        PurchaseListLoad() '一覧再表示
    End Sub

    '取消ボタン押下時
    Private Sub BtnBillingCancel_Click(sender As Object, e As EventArgs) Handles BtnBillingCancel.Click
        '実行できるデータがあるかチェック
        If actionChk() = False Then
            Return
        End If

        '取消済みデータは取消操作不可能
        If DgvBilling.Rows(DgvBilling.CurrentCell.RowIndex).Cells("取消").Value = CommonConst.CANCEL_KBN_DISABLED_TXT Then
            '取消データは選択できないアラートを出す
            _msgHd.dspMSG("cannotSelectTorikeshiData", frmC01F10_Login.loginValue.Language)
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
    't23_skyuhd
    Private Sub updateData()
        Dim dtNow As String = formatDatetime(DateTime.Now)
        Dim Sql As String = ""
        Dim ds As DataSet

        Sql = " AND"
        Sql += " 請求番号"
        Sql += "='"
        Sql += DgvBilling.Rows(DgvBilling.CurrentCell.RowIndex).Cells("請求番号").Value
        Sql += "' "

        '画面を開いた時から対象データに対して更新がされていないかどうか確認
        ds = getDsData("t23_skyuhd", Sql)

        If ds.Tables(RS).Rows(0)("更新日") = DgvBilling.Rows(DgvBilling.CurrentCell.RowIndex).Cells("更新日").Value Then

            Sql = "UPDATE "
            Sql += "Public."
            Sql += "t23_skyuhd "
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
            Sql += frmC01F10_Login.loginValue.BumonCD
            Sql += "'"
            Sql += " AND"
            Sql += " 請求番号"
            Sql += " ILIKE '"
            Sql += DgvBilling.Rows(DgvBilling.CurrentCell.RowIndex).Cells("請求番号").Value
            Sql += "' "

            '請求基本を更新
            _db.executeDB(Sql)

            '表示データを更新
            PurchaseListLoad()

        Else

            '画面を開いたときの日時とデータの日時が異なっていた場合
            'データが誰かに変更された旨を伝える
            _msgHd.dspMSG("chkData", frmC01F10_Login.loginValue.Language)

            '表示データを更新
            PurchaseListLoad()


        End If

    End Sub

    '抽出条件取得
    Private Function searchConditions() As String
        Dim Sql As String = ""

        '抽出条件
        Dim customerName As String = escapeSql(TxtCustomerName.Text)
        Dim customerCode As String = escapeSql(TxtCustomerCode.Text)
        Dim sinceDate As String = strFormatDate(dtBillingDateSince.Text)
        Dim untilDate As String = strFormatDate(dtBillingDateUntil.Text)
        Dim sinceNum As String = escapeSql(TxtBillingNoSince.Text)
        Dim untilNum As String = escapeSql(TxtBillingNoUntil.Text)
        Dim poNum As String = escapeSql(TxtCustomerPO.Text)

        If customerName <> Nothing Then
            Sql += " AND "
            Sql += " 得意先名 ILIKE '%" & customerName & "%' "
        End If

        If customerCode <> Nothing Then
            Sql += " AND "
            Sql += " 得意先コード ILIKE '%" & customerCode & "%' "
        End If

        If sinceDate <> Nothing Then
            Sql += " AND "
            Sql += " 請求日 >= '" & sinceDate & "'"
        End If
        If untilDate <> Nothing Then
            Sql += " AND "
            Sql += " 請求日 <= '" & untilDate & "'"
        End If

        Console.WriteLine(untilDate)

        If sinceNum <> Nothing Then
            Sql += " AND "
            Sql += " 請求番号 >= '" & sinceNum & "' "
        End If
        If untilNum <> Nothing Then
            Sql += " AND "
            Sql += " 請求番号 <= '" & untilNum & "' "
        End If

        If poNum <> Nothing Then
            Sql += " AND "
            Sql += " 客先番号 ILIKE '%" & poNum & "%' "
        End If

        Return Sql

    End Function

    'sqlで実行する文字列からシングルクォーテーションを文字コードにする
    Private Function escapeSql(ByVal prmSql As String) As String
        Dim sql As String = prmSql

        sql = sql.Replace("'"c, "''") 'シングルクォーテーションを置換

        Return Regex.Escape(sql)
        Return sql
    End Function

    Private Function actionChk() As Boolean
        '対象データがない場合は取消操作不可能
        If DgvBilling.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)

            Return False

        Else

            Return True

        End If
    End Function

    '表示形式条件
    Private Function viewFormat() As String
        Dim Sql As String = ""

        '取消データを含めない場合
        If ChkCancelData.Checked = False Then
            Sql += " AND "
            Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
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
        Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"
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
        Return prmVal.ToString("F3", nfi) '売掛残高を増やす
    End Function

End Class