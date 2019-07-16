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

Public Class ExchangeRateList
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
    Private _parentForm As Form
    'Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ
    Private CompanyCode As String = ""
    Private ReceiptNo As String()
    Private ReceiptStatus As String = ""


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
                   ByRef prmRefStatus As String)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _parentForm = prmRefForm
        ReceiptStatus = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True
        DgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.DisplayedCells
    End Sub

    '画面表示時
    Private Sub ReceiptList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '検索（Date）の初期値
        dtDateSince.Value = DateAdd("d", CommonConst.SINCE_DEFAULT_DAY, DateTime.Today)
        dtDateUntil.Value = DateTime.Today

        getList()

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblConditions.Text = "TermsOfSelection"
            LblStandardDate.Text = "StandardDate"

            BtnSearch.Text = "Search"
            BtnAdd.Text = "Add"
            BtnEdit.Text = "Edit"
            BtnBack.Text = "Back"
        End If
    End Sub

    '一覧表示処理
    Private Sub getList()
        'Dim Status As String = prmRefStatus
        Dim Sql As String = ""
        Dim reccnt As Integer = 0 'DB用（デフォルト）

        '一覧クリア
        DgvList.Rows.Clear()
        'DgvList.Columns.Clear()


        '使用言語によって表示切替
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            DgvList.Columns("基準日").HeaderText = "StandardDate"
            DgvList.Columns("採番キー").HeaderText = "NumberingKey"
            DgvList.Columns("レート").HeaderText = "Rate"
            DgvList.Columns("更新者").HeaderText = "ModifiedBy"
            DgvList.Columns("更新日").HeaderText = "UpdateDate"

        End If
        Try

            Sql = "SELECT"
            Sql += " t71.会社コード, t71.基準日, t71.採番キー,t71.レート,t71.更新者,t71.更新日 "
            Sql += ",m25.通貨コード"
            Sql += " FROM "
            Sql += " public.t71_exchangerate t71 "

            Sql += " INNER JOIN "
            Sql += " m25_currency m25"
            Sql += " ON "
            Sql += " t71.会社コード = m25.会社コード "
            Sql += " AND "
            Sql += " t71.採番キー = m25.採番キー"
            Sql += " WHERE "
            Sql += " t71.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"

            Sql += viewSearchConditions() '抽出条件取得

            Sql += " GROUP BY "
            Sql += " t71.会社コード, t71.基準日, t71.採番キー,t71.レート,t71.更新者,t71.更新日 "
            Sql += ",m25.通貨コード"
            Sql += " ORDER BY "
            Sql += "t71.基準日 DESC, t71.採番キー"

            ds = _db.selectDB(Sql, RS, reccnt)

            For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvList.Rows.Add()
                DgvList.Rows(i).Cells("基準日").Value = ds.Tables(RS).Rows(i)("基準日").ToShortDateString()
                DgvList.Rows(i).Cells("採番キー").Value = ds.Tables(RS).Rows(i)("採番キー")
                DgvList.Rows(i).Cells("レート").Value = ds.Tables(RS).Rows(i)("レート")
                DgvList.Rows(i).Cells("更新者").Value = ds.Tables(RS).Rows(i)("更新者")
                DgvList.Rows(i).Cells("更新日").Value = ds.Tables(RS).Rows(i)("更新日")
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
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    '表示形式変更時
    Private Sub RbtnDetails_CheckedChanged(sender As Object, e As EventArgs)
        getList() '一覧再表示
    End Sub

    '取消データを含めないチェックイベント
    Private Sub ChkCancelData_CheckedChanged(sender As Object, e As EventArgs)
        getList() '一覧再表示
    End Sub

    '検索ボタン押下時
    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        getList() '一覧再表示
    End Sub

    'sqlで実行する文字列からシングルクォーテーションを文字コードにする
    Private Function escapeSql(ByVal prmSql As String) As String
        Dim sql As String = prmSql

        sql = sql.Replace("'"c, "''") 'シングルクォーテーションを置換

        Return Regex.Escape(sql)
        Return sql
    End Function

    Private Function actionChk() As Boolean
        '対象データがない場合は取消操作不可能
        If DgvList.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)

            Return False

        Else

            Return True

        End If
    End Function

    '抽出条件取得
    Private Function viewSearchConditions() As String
        Dim Sql As String = ""

        '抽出条件
        Dim sinceDate As String = strFormatDate(dtDateSince.Text)
        Dim untilDate As String = strFormatDate(dtDateUntil.Text)

        If sinceDate <> Nothing Then
            Sql += " AND "
            Sql += " t71.基準日 >= '" & sinceDate & "'"
        End If
        If untilDate <> Nothing Then
            Sql += " AND "
            Sql += " t71.基準日 <= '" & untilDate & "'"
        End If

        ''取消データを含めない場合
        'If ChkCancelData.Checked = False Then
        '    Sql += " AND "
        '    Sql += "t42.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
        'End If

        Return Sql
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

    '汎用マスタから固定キー、可変キーに応じた結果を返す
    'param1：String 固定キー
    'param2：String 可変キー
    'Return: DataSet
    Private Function getDsHanyoData(ByVal prmFixed As String, ByVal prmVariable As String) As DataSet
        Dim Sql As String = ""

        Sql = " AND "
        Sql += "固定キー ILIKE '" & prmFixed & "'"
        Sql += " AND "
        Sql += "可変キー ILIKE '" & prmVariable & "'"

        'リードタイムのリストを汎用マスタから取得
        Return getDsData("m90_hanyo", Sql)

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

        Console.WriteLine(Sql)
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

    '新規追加
    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        Dim Status As String = CommonConst.STATUS_ADD
        Dim openForm As Form = Nothing
        openForm = New ExchangeRate(_msgHd, _db, _langHd, Me, Status,,)
        Me.Hide()
        openForm.ShowDialog(Me)
    End Sub

    'Private Sub BtnEdit_Click(sender As Object, e As EventArgs) Handles BtnEdit.Click
    '    Dim Status As String = CommonConst.STATUS_ADD
    '    Dim openForm As Form = Nothing

    '    Dim standardDateDate As Date = DgvList.Rows(DgvList.CurrentCell.RowIndex).Cells("基準日").Value
    '    Dim numberingKeyDate As Date = DgvList.Rows(DgvList.CurrentCell.RowIndex).Cells("採番キー").Value

    '    openForm = New ExchangeRate(_msgHd, _db, _langHd, Me, Status, standardDateDate, numberingKeyDate)
    '    Me.Hide()
    '    openForm.ShowDialog(Me)
    'End Sub

    Private Sub ExchangeRateList_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        getList()
    End Sub

    '削除ボタン
    Private Sub BtnDel_Click(sender As Object, e As EventArgs) Handles BtnDel.Click

        'グリッドに何もないときはなにもしない
        '対象データがない場合は取消操作不可能
        If DgvList.Rows.Count = 0 Then
            Exit Sub
        End If


        '対象データがないメッセージを表示
        If _msgHd.dspMSG("confirmDelete", frmC01F10_Login.loginValue.Language) = vbNo Then
            Exit Sub
        End If

        Try
            Dim Sql As String = vbNullString
            Dim RowIdx As Integer
            RowIdx = Me.DgvList.CurrentCell.RowIndex
            Dim Kijyun As String = DgvList.Rows(RowIdx).Cells("基準日").Value
            Dim Key As String = DgvList.Rows(RowIdx).Cells("採番キー").Value
            Dim UpdatedOn As Date = DgvList.Rows(RowIdx).Cells("更新日").Value

            '1レコード目削除
            Sql = "delete from Public.t71_exchangerate"
            Sql += " where 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += "   and 基準日 = '" & Kijyun & "'"
            Sql += "   and 採番キー = " & Key
            Sql += "   and 更新日 = '" & UpdatedOn & "'"

            _db.executeDB(Sql)

            If Key = 2 Then
                Key = Key + 1
            Else
                Key = Key - 1
            End If

            '2レコード目削除
            Sql = "delete from Public.t71_exchangerate"
            Sql += " where 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += "   and 基準日 = '" & Kijyun & "'"
            Sql += "   and 採番キー = " & Key
            Sql += "   and 更新日 = '" & UpdatedOn & "'"

            _db.executeDB(Sql)

        Catch ex As Exception

            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))

        End Try


    End Sub
End Class