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
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True
        DgvHtyhd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.DisplayedCells
    End Sub


    '画面表示時
    Private Sub PaidList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If _status = CommonConst.STATUS_CANCEL Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "CancelMode"
            Else
                LblMode.Text = "取消モード"
            End If

            BtnCancel.Visible = True
        Else
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "ViewMode"
            Else
                LblMode.Text = "参照モード"
            End If
        End If

        '検索（Date）の初期値
        dtPaidDateSince.Value = DateAdd("d", CommonConst.SINCE_DEFAULT_DAY, DateTime.Today)
        dtPaidDateUntil.Value = DateTime.Today

        'データ描画
        setDgvHtyhd()

        '翻訳
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
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

    '戻るボタン押下時
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        Dim openForm As Form = Nothing
        openForm = New frmC01F30_Menu(_msgHd, _langHd, _db)
        openForm.Show()
        Me.Close()
    End Sub


    'DgvHtyhd内を再描画
    Private Sub setDgvHtyhd()
        clearDGV() 'テーブルクリア
        Dim Sql As String = ""

        Sql += searchConditions() '抽出条件取得
        Sql += viewFormat() '表示形式条件

        Sql += " ORDER BY "
        Sql += "更新日 DESC"

        Try
            '伝票単位
            If RbtnSlip.Checked Then

                ds = getDsData("t47_shrihd", Sql)

                '英語の表記
                If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                    DgvHtyhd.Columns.Add("取消", "Cancel")
                    DgvHtyhd.Columns.Add("支払番号", "PaymentNumber")
                    DgvHtyhd.Columns.Add("支払日", "PaymentDate")
                    DgvHtyhd.Columns.Add("支払先名", "SupplierName")
                    DgvHtyhd.Columns.Add("支払先", "PaymentDestination")
                    DgvHtyhd.Columns.Add("支払金額計", "TotalPaymentAmount")
                    DgvHtyhd.Columns.Add("更新日", "UpdateDate")
                    DgvHtyhd.Columns.Add("備考", "Remarks")
                Else
                    DgvHtyhd.Columns.Add("取消", "取消")
                    DgvHtyhd.Columns.Add("支払番号", "支払番号")
                    DgvHtyhd.Columns.Add("支払日", "支払日")
                    DgvHtyhd.Columns.Add("支払先名", "支払先名")
                    DgvHtyhd.Columns.Add("支払先", "支払先")
                    DgvHtyhd.Columns.Add("支払金額計", "支払金額計")
                    DgvHtyhd.Columns.Add("更新日", "更新日")
                    DgvHtyhd.Columns.Add("備考", "備考")
                End If

                '伝票単位時のセル書式
                DgvHtyhd.Columns("支払金額計").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                    DgvHtyhd.Rows.Add()
                    DgvHtyhd.Rows(index).Cells("取消").Value = getDelKbnTxt(ds.Tables(RS).Rows(index)("取消区分"))
                    DgvHtyhd.Rows(index).Cells("支払番号").Value = ds.Tables(RS).Rows(index)("支払番号")
                    DgvHtyhd.Rows(index).Cells("支払日").Value = ds.Tables(RS).Rows(index)("支払日")
                    DgvHtyhd.Rows(index).Cells("支払先名").Value = ds.Tables(RS).Rows(index)("支払先名")
                    DgvHtyhd.Rows(index).Cells("支払先").Value = ds.Tables(RS).Rows(index)("支払先")
                    DgvHtyhd.Rows(index).Cells("支払金額計").Value = ds.Tables(RS).Rows(index)("支払金額計")
                    DgvHtyhd.Rows(index).Cells("更新日").Value = ds.Tables(RS).Rows(index)("更新日")
                    DgvHtyhd.Rows(index).Cells("備考").Value = ds.Tables(RS).Rows(index)("備考")
                Next

            Else '明細単位

                ds = getDsData("t49_shrikshihd", Sql)


                '英語の表記
                If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                    DgvHtyhd.Columns.Add("取消", "Cancel")
                    DgvHtyhd.Columns.Add("支払番号", "PaymentNumber")
                    DgvHtyhd.Columns.Add("買掛番号", "AccountsPayableNumber")
                    DgvHtyhd.Columns.Add("支払日", "PaymentDate")
                    DgvHtyhd.Columns.Add("支払先名", "SupplierName")
                    DgvHtyhd.Columns.Add("支払金額計", "TotalPaymentAmount")
                    DgvHtyhd.Columns.Add("備考", "Remarks")
                Else
                    DgvHtyhd.Columns.Add("取消", "取消")
                    DgvHtyhd.Columns.Add("支払番号", "支払番号")
                    DgvHtyhd.Columns.Add("支払日", "支払日")
                    DgvHtyhd.Columns.Add("支払先名", "支払先名")
                    DgvHtyhd.Columns.Add("支払金額計", "支払金額計")
                    DgvHtyhd.Columns.Add("備考", "備考")
                End If

                '伝票単位時のセル書式
                DgvHtyhd.Columns("支払金額計").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                    DgvHtyhd.Rows.Add()
                    DgvHtyhd.Rows(index).Cells("取消").Value = getDelKbnTxt(ds.Tables(RS).Rows(index)("取消区分"))
                    DgvHtyhd.Rows(index).Cells("支払番号").Value = ds.Tables(RS).Rows(index)("支払番号")
                    DgvHtyhd.Rows(index).Cells("支払日").Value = ds.Tables(RS).Rows(index)("支払日")
                    DgvHtyhd.Rows(index).Cells("支払先名").Value = ds.Tables(RS).Rows(index)("支払先名")
                    DgvHtyhd.Rows(index).Cells("支払金額計").Value = ds.Tables(RS).Rows(index)("支払消込額計")
                    DgvHtyhd.Rows(index).Cells("備考").Value = ds.Tables(RS).Rows(index)("備考")
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
        setDgvHtyhd() '一覧再取得
    End Sub

    '検索ボタンをクリックしたら
    Private Sub ChkCancelData_CheckedChanged(sender As Object, e As EventArgs) Handles ChkCancelData.CheckedChanged
        setDgvHtyhd() '一覧再取得
    End Sub

    '「取消データを含める」変更イベント取得時
    Private Sub BtnPurchaseSearch_Click(sender As Object, e As EventArgs) Handles BtnPaymentSearch.Click
        setDgvHtyhd() '一覧再取得
    End Sub


    '支払取消処理
    Private Sub BtnPurchaseCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click

        '明細表示時、または対象データがない場合は取消操作不可能
        If RbtnDetails.Checked Or DgvHtyhd.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Return

        End If

        '取消済みデータは取消操作不可能
        If DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("取消").Value = CommonConst.CANCEL_KBN_DISABLED_TXT Then
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
    't47_shrihd, t49_shrikshihd, t46_kikehd
    Private Sub updateData()
        Dim dtNow As String = formatDatetime(DateTime.Now)
        Dim Sql As String = ""
        Dim ds As DataSet

        Sql = " AND"
        Sql += " 支払番号"
        Sql += "='"
        Sql += DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("支払番号").Value
        Sql += "' "

        '画面を開いた時から対象データに対して更新がされていないかどうか確認
        ds = getDsData("t47_shrihd", Sql)

        If ds.Tables(RS).Rows(0)("更新日") = DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("更新日").Value Then

            Sql = "UPDATE "
            Sql += "Public."
            Sql += "t47_shrihd "
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
            Sql += " 支払番号"
            Sql += "='"
            Sql += DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("支払番号").Value
            Sql += "' "

            '支払基本を更新
            _db.executeDB(Sql)


            Sql = " AND"
            Sql += " 支払番号"
            Sql += "='"
            Sql += DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("支払番号").Value
            Sql += "' "

            '支払基本から支払金額計を取得
            Dim dstShrihd As DataSet = getDsData("t47_shrihd", Sql)
            Dim strSiharaiGaku As Decimal = dstShrihd.Tables(RS).Rows(0)("支払金額計")


            Sql = " AND"
            Sql += " 支払番号"
            Sql += "='"
            Sql += DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("支払番号").Value
            Sql += "' "

            '支払消込から買掛番号を取得
            Dim dsShrikshihd As DataSet = getDsData("t49_shrikshihd", Sql)


            Sql = " AND"
            Sql += " 買掛番号"
            Sql += "='"
            Sql += dsShrikshihd.Tables(RS).Rows(0)("買掛番号")
            Sql += "' "

            '買掛基本から発注番号を取得
            Dim dsKikehd As DataSet = getDsData("t46_kikehd", Sql)

            Dim decKaikakeZan As Decimal = dsKikehd.Tables(RS).Rows(0)("買掛残高") + strSiharaiGaku
            Dim decSiharaiKei As Decimal = dsKikehd.Tables(RS).Rows(0)("支払金額計") - strSiharaiGaku


            Sql = "UPDATE "
            Sql += "Public.t49_shrikshihd "
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
            Sql += " 支払番号"
            Sql += "='"
            Sql += DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("支払番号").Value
            Sql += "' "

            '支払消込基本を更新
            _db.executeDB(Sql)

            Sql = "UPDATE "
            Sql += "Public.t46_kikehd "
            Sql += "SET "

            Sql += "買掛残高"
            Sql += " = '"
            Sql += formatNumber(decKaikakeZan) '買掛残高を増やす
            Sql += "', "
            Sql += "支払金額計"
            Sql += " = '"
            Sql += formatNumber(decSiharaiKei) '支払金額計を減らす
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
            Sql += " 買掛番号"
            Sql += "='"
            Sql += dsShrikshihd.Tables(RS).Rows(0)("買掛番号")
            Sql += "' "

            '買掛基本を更新
            _db.executeDB(Sql)

            setDgvHtyhd()

        Else

            '画面を開いたときの日時とデータの日時が異なっていた場合
            'データが誰かに変更された旨を伝える
            _msgHd.dspMSG("chkData", frmC01F10_Login.loginValue.Language)

            '表示データを更新
            setDgvHtyhd()

        End If

    End Sub


    'テーブルをクリア
    Private Sub clearDGV()
        DgvHtyhd.Rows.Clear()
        DgvHtyhd.Columns.Clear()
    End Sub

    '抽出条件取得
    Private Function searchConditions() As String
        Dim Sql As String = ""

        '抽出条件
        Dim customerName As String = escapeSql(TxtSupplierName.Text)
        Dim customerCode As String = escapeSql(TxtSupplierCode.Text)
        Dim sinceDate As String = strFormatDate(dtPaidDateSince.Text)
        Dim untilDate As String = strFormatDate(dtPaidDateUntil.Text)
        Dim sinceNum As String = escapeSql(TxtPaidNoSince.Text)
        Dim untilNum As String = escapeSql(TxtPaidNoUntil.Text)

        If customerName <> Nothing Then
            Sql += " AND "
            Sql += " 支払先名 ILIKE '%" & customerName & "%' "
        End If

        If customerCode <> Nothing Then
            Sql += " AND "
            Sql += " 支払先コード ILIKE '%" & customerCode & "%' "
        End If

        If sinceDate <> Nothing Then
            Sql += " AND "
            Sql += " 支払日 >= '" & sinceDate & "'"
        End If
        If untilDate <> Nothing Then
            Sql += " AND "
            Sql += " 支払日 <= '" & untilDate & "'"
        End If

        If sinceNum <> Nothing Then
            Sql += " AND "
            Sql += " 支払番号 >= '" & sinceNum & "' "
        End If
        If untilNum <> Nothing Then
            Sql += " AND "
            Sql += " 支払番号 <= '" & untilNum & "' "
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
        Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += txtParam

        Return _db.selectDB(Sql, RS, reccnt)
    End Function

    'sqlで実行する文字列からシングルクォーテーションを文字コードにする
    Private Function escapeSql(ByVal prmSql As String) As String
        Dim sql As String = prmSql

        sql = sql.Replace("'"c, "''") 'シングルクォーテーションを置換

        Return Regex.Escape(sql)
        Return sql
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