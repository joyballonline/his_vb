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

Public Class AccountsPayableList
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

    End Sub

    '画面表示時
    Private Sub MstHanyoue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If _status = "VIEW" Then

            LblMode.Text = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                            "ViewMode",
                            "参照モード")

        ElseIf _status = "CANCEL" Then

            LblMode.Text = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                            "CancelMode",
                            "取消モード")

            BtnAPCancel.Visible = True
            BtnAPCancel.Location = New Point(997, 509)
        End If

        '検索（Date）の初期値
        dtAPDateSince.Value = DateAdd("d", CommonConst.SINCE_DEFAULT_DAY, DateTime.Today)
        dtAPDateUntil.Value = DateTime.Today

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then

            DgvKike.Columns("取消").HeaderText = "Cancel"
            DgvKike.Columns("買掛番号").HeaderText = "AccountsPayableNumber"
            DgvKike.Columns("買掛区分").HeaderText = "AccountsPayableClassification"
            DgvKike.Columns("買掛日").HeaderText = "AccountsPayableDate"
            DgvKike.Columns("客先番号").HeaderText = "CustomerNumber"
            DgvKike.Columns("発注番号").HeaderText = "PurchaseOrderNumber"
            DgvKike.Columns("発注番号枝番").HeaderText = "PurchaseOrderSubNumber"
            DgvKike.Columns("仕入先コード").HeaderText = "SupplierCode"
            DgvKike.Columns("仕入先名").HeaderText = "SupplierName"
            DgvKike.Columns("買掛金額計").HeaderText = "TotalAccountsPayable"
            DgvKike.Columns("買掛残高").HeaderText = "AccountsPayableBalance"
            DgvKike.Columns("備考1").HeaderText = "Remarks1"
            DgvKike.Columns("備考2").HeaderText = "Remarks2"
            DgvKike.Columns("更新日").HeaderText = "UpdateDate"
            DgvKike.Columns("更新者").HeaderText = "ModifiedBy"

        End If

        PurchaseListLoad() '一覧表示

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblConditions.Text = "ExtractionCondition"
            Label1.Text = "SupplierName"
            Label4.Text = "SupplierCode"
            Label8.Text = "AccountsPayableDate"
            Label7.Text = "AccountsPayableNumber"
            Label11.Text = "CustomerNumber"
            Label10.Text = "DisplayFormat"
            RbtnSlip.Text = "UnitOfVoucher"
            RbtnDetails.Text = "UnitOfDetailData"

            ChkCancelData.Text = "IncludeCancelData"

            BtnAPSearch.Text = "Search"
            BtnAPCancel.Text = "CancelOfAccountsPayable"
            BtnAPView.Text = "AccountsPayableView"
            BtnBack.Text = "Back"
        End If
    End Sub

    '一覧表示
    Private Sub PurchaseListLoad()
        '変数等
        Dim Sql As String = ""

        '一覧クリア
        DgvKike.Rows.Clear()

        Sql = searchConditions() '詳細条件
        Sql += viewFormat() '表示形式

        Sql += " ORDER BY "
        Sql += "更新日 DESC"

        Try
            ds = getDsData("t46_kikehd", Sql)

            For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvKike.Rows.Add()
                DgvKike.Rows(i).Cells("取消").Value = getDelKbnTxt(ds.Tables(RS).Rows(i)("取消区分"))
                DgvKike.Rows(i).Cells("買掛番号").Value = ds.Tables(RS).Rows(i)("買掛番号")
                DgvKike.Rows(i).Cells("買掛区分").Value = IIf(ds.Tables(RS).Rows(i)("買掛区分") = CommonConst.APC_KBN_DEPOSIT.ToString,
                                                                                                    CommonConst.APC_KBN_DEPOSIT_TXT,
                                                                                                    CommonConst.APC_KBN_NORMAL_TXT)
                DgvKike.Rows(i).Cells("買掛日").Value = ds.Tables(RS).Rows(i)("買掛日").ToShortDateString()
                DgvKike.Rows(i).Cells("客先番号").Value = ds.Tables(RS).Rows(i)("客先番号")
                DgvKike.Rows(i).Cells("発注番号").Value = ds.Tables(RS).Rows(i)("発注番号")
                DgvKike.Rows(i).Cells("発注番号枝番").Value = ds.Tables(RS).Rows(i)("発注番号枝番")
                DgvKike.Rows(i).Cells("仕入先コード").Value = ds.Tables(RS).Rows(i)("仕入先コード")
                DgvKike.Rows(i).Cells("仕入先名").Value = ds.Tables(RS).Rows(i)("仕入先名")
                DgvKike.Rows(i).Cells("買掛金額計").Value = ds.Tables(RS).Rows(i)("買掛金額計")
                DgvKike.Rows(i).Cells("買掛残高").Value = ds.Tables(RS).Rows(i)("買掛残高")
                DgvKike.Rows(i).Cells("備考1").Value = ds.Tables(RS).Rows(i)("備考1")
                DgvKike.Rows(i).Cells("備考2").Value = ds.Tables(RS).Rows(i)("備考2")
                DgvKike.Rows(i).Cells("更新日").Value = ds.Tables(RS).Rows(i)("更新日")
                DgvKike.Rows(i).Cells("更新者").Value = ds.Tables(RS).Rows(i)("更新者")
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

        DgvKike.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.DisplayedCells

    End Sub

    '戻るボタン押下時
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        Dim openForm As Form = Nothing
        openForm = New frmC01F30_Menu(_msgHd, _langHd, _db)
        openForm.Show()
        Me.Close()
    End Sub

    '検索ボタン押下時
    Private Sub BtnPurchaseSearch_Click(sender As Object, e As EventArgs) Handles BtnAPSearch.Click
        PurchaseListLoad() '一覧再表示
    End Sub

    '参照ボタン押下時
    Private Sub BtnBillingView_Click(sender As Object, e As EventArgs) Handles BtnAPView.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvKike.CurrentCell.RowIndex
        Dim No As String = DgvKike.Rows(RowIdx).Cells("発注番号").Value
        Dim Suffix As String = DgvKike.Rows(RowIdx).Cells("発注番号枝番").Value
        Dim Status As String = "VIEW"
        Dim openForm As Form = Nothing
        openForm = New BillingManagement(_msgHd, _db, _langHd, Me, No, Suffix, Status)   '処理選択
        openForm.Show(Me)
    End Sub

    '「取消データを含める」チェックイベント
    Private Sub ChkCancelData_CheckedChanged(sender As Object, e As EventArgs) Handles ChkCancelData.CheckedChanged
        PurchaseListLoad() '一覧再表示
    End Sub

    '取消ボタン押下時
    Private Sub BtnBillingCancel_Click(sender As Object, e As EventArgs) Handles BtnAPCancel.Click

        '明細表示時、または対象データがない場合は取消操作不可能
        If RbtnDetails.Checked Or DgvKike.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Return

        End If

        '取消済みデータは取消操作不可能
        If DgvKike.Rows(DgvKike.CurrentCell.RowIndex).Cells("取消").Value = CommonConst.CANCEL_KBN_DISABLED_TXT Then
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

        'If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
        '    Dim result As DialogResult = MessageBox.Show("Would you like to cancel the accounts payable?",
        '                                     "Question",
        '                                     MessageBoxButtons.YesNoCancel,
        '                                     MessageBoxIcon.Exclamation,
        '                                     MessageBoxDefaultButton.Button2)

        '    If result = DialogResult.Yes Then
        '        _db.executeDB(Sql)

        '        PurchaseListLoad() '一覧再表示

        '    ElseIf result = DialogResult.No Then

        '    ElseIf result = DialogResult.Cancel Then

        '    End If
        'Else
        '    Dim result As DialogResult = MessageBox.Show("買掛を取り消しますか？",
        '                                     "質問",
        '                                     MessageBoxButtons.YesNoCancel,
        '                                     MessageBoxIcon.Exclamation,
        '                                     MessageBoxDefaultButton.Button2)

        '    If result = DialogResult.Yes Then
        '        _db.executeDB(Sql)

        '        PurchaseListLoad() '一覧再表示

        '    ElseIf result = DialogResult.No Then

        '    ElseIf result = DialogResult.Cancel Then

        '    End If
        'End If

    End Sub

    '選択データをもとに以下テーブル更新
    't25_nkinhd, t27_nkinkshihd, t23_skyuhd
    Private Sub updateData()

        Dim dtNow As String = formatDatetime(DateTime.Now)
        Dim Sql As String = ""

        If ds.Tables(RS).Rows(0)("更新日") = DgvKike.Rows(DgvKike.CurrentCell.RowIndex).Cells("更新日").Value Then

            Sql = "UPDATE "
            Sql += "Public."
            Sql += "t46_kikehd "
            Sql += "SET "

            Sql += "取消区分 = " & CommonConst.CANCEL_KBN_DISABLED
            Sql += ", "
            Sql += "取消日"
            Sql += " = '"
            Sql += dtNow
            Sql += "', "
            Sql += "更新日"
            Sql += " = '"
            Sql += dtNow
            Sql += "', "
            Sql += "更新者"
            Sql += " = '"
            Sql += frmC01F10_Login.loginValue.TantoNM
            Sql += " ' "

            Sql += "WHERE"
            Sql += " 会社コード"
            Sql += "='"
            Sql += frmC01F10_Login.loginValue.BumonCD
            Sql += "'"
            Sql += " AND"
            Sql += " 買掛番号"
            Sql += "='"
            Sql += DgvKike.Rows(DgvKike.CurrentCell.RowIndex).Cells("買掛番号").Value
            Sql += "' "

            '買掛基本を更新
            _db.executeDB(Sql)

            PurchaseListLoad()

        Else

            '画面を開いたときの日時とデータの日時が異なっていた場合
            'データが誰かに変更された旨を伝える
            _msgHd.dspMSG("chkData", frmC01F10_Login.loginValue.Language)

            '表示データを更新
            PurchaseListLoad()

        End If

    End Sub

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

    'sqlで実行する文字列からシングルクォーテーションを文字コードにする
    Private Function escapeSql(ByVal prmSql As String) As String
        Dim sql As String = prmSql

        sql = sql.Replace("'"c, "''") 'シングルクォーテーションを置換

        Return Regex.Escape(sql)
        Return sql
    End Function

    Private Function actionChk() As Boolean
        '対象データがない場合は取消操作不可能
        If DgvKike.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)

            Return False

        Else

            Return True

        End If
    End Function

    '抽出条件取得
    Private Function searchConditions() As String
        Dim Sql As String = ""

        '抽出条件
        Dim supplierName As String = escapeSql(TxtSupplierName.Text)
        Dim supplierCode As String = escapeSql(TxtSupplierCode.Text)
        Dim sinceDate As String = strFormatDate(dtAPDateSince.Text)
        Dim untilDate As String = strFormatDate(dtAPDateUntil.Text)
        Dim sinceNum As String = escapeSql(TxtAPSince.Text)
        Dim untilNum As String = escapeSql(TxtAPUntil.Text)
        Dim poNum As String = escapeSql(TxtCustomerPO.Text)

        If supplierName <> Nothing Then
            Sql += " AND "
            Sql += " 仕入先名 ILIKE '%" & supplierName & "%' "
        End If

        If supplierCode <> Nothing Then
            Sql += " AND "
            Sql += " 仕入先コード ILIKE '%" & supplierCode & "%' "
        End If

        If sinceDate <> Nothing Then
            Sql += " AND "
            Sql += " 買掛日 >= '" & sinceDate & "'"
        End If
        If untilDate <> Nothing Then
            Sql += " AND "
            Sql += " 買掛日 <= '" & untilDate & "'"
        End If

        If sinceNum <> Nothing Then
            Sql += " AND "
            Sql += " 買掛番号 >= '" & sinceNum & "' "
        End If
        If untilNum <> Nothing Then
            Sql += " AND "
            Sql += " 買掛番号 <= '" & untilNum & "' "
        End If

        If poNum <> Nothing Then
            Sql += " AND "
            Sql += " 客先番号 ILIKE '%" & poNum & "%' "
        End If

        Return Sql

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

        Sql += "SELECT * FROM public." & tableName
        Sql += " WHERE "
        Sql += "会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += txtParam
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
        Return prmVal.ToString("F3", nfi) '売掛残高を増やす
    End Function

End Class