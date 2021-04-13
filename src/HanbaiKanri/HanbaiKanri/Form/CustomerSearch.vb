Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class CustomerSearch
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
    Private _parentForm As Form
    'Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ
    'Private _companyCode As String = frmC01F10_Login.loginValue.BumonCD

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
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByRef prmRefLang As UtilLangHandler, ByRef prmRefForm As Form)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _parentForm = prmRefForm
        _langHd = prmRefLang
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True


    End Sub

    '一覧表示時
    Private Sub MstCustomere_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblCustomerName.Text = "CustomerName"
            BtnSearch.Text = "Search"
            btnSelectCustomer.Text = "Select"
            btnBack.Text = "Back"

            Dgv_Customer.Columns("得意先コード").HeaderText = "CustomerCode"
            Dgv_Customer.Columns("得意先名").HeaderText = "CustomerName"
            Dgv_Customer.Columns("得意先名略称").HeaderText = "CustoemrShortName"
            Dgv_Customer.Columns("郵便番号").HeaderText = "PostalCode"
            Dgv_Customer.Columns("住所１").HeaderText = "Address1"
            Dgv_Customer.Columns("住所２").HeaderText = "Address2"
            Dgv_Customer.Columns("住所３").HeaderText = "Address3"
            Dgv_Customer.Columns("電話番号").HeaderText = "TEL"
            Dgv_Customer.Columns("電話番号検索用").HeaderText = "TEL(ForTheSearch)"
            Dgv_Customer.Columns("FAX番号").HeaderText = "FAX"
            Dgv_Customer.Columns("担当者名").HeaderText = "ContactPersonName"
            Dgv_Customer.Columns("担当者役職").HeaderText = "ContactPersonPosition"
            Dgv_Customer.Columns("既定支払条件").HeaderText = "PaymentTerms"
            Dgv_Customer.Columns("メモ").HeaderText = "Memo"
            Dgv_Customer.Columns("更新者").HeaderText = "Changer"
            Dgv_Customer.Columns("更新日").HeaderText = "UpdateDate"

        End If

        setList() '一覧セット
    End Sub

    Private Sub btnSelectCustomer_Click(sender As Object, e As EventArgs) Handles btnSelectCustomer.Click, Dgv_Customer.DoubleClick
        Try
            'メニュー選択処理
            Dim idx As Integer
            Dim frm As Quote = CType(Me.Owner, Quote)
            '明細が１行もない場合は選択不能
            If Dgv_Customer.Rows.Count = 0 Then
                Exit Sub
            End If

            '一覧選択行インデックスの取得
            For Each c As DataGridViewRow In Dgv_Customer.SelectedRows
                idx = c.Index
                Exit For
            Next c

            frm.TxtCustomerCode.Text = Dgv_Customer.Rows(idx).Cells("得意先コード").Value
            frm.TxtCustomerName.Text = Dgv_Customer.Rows(idx).Cells("得意先名").Value
            If Dgv_Customer.Rows(idx).Cells("郵便番号").Value Is DBNull.Value Then
            Else
                frm.TxtPostalCode.Text = Dgv_Customer.Rows(idx).Cells("郵便番号").Value
            End If
            If Dgv_Customer.Rows(idx).Cells("住所１").Value Is DBNull.Value Then
            Else
                frm.TxtAddress1.Text = Dgv_Customer.Rows(idx).Cells("住所１").Value
            End If
            If Dgv_Customer.Rows(idx).Cells("住所２").Value Is DBNull.Value Then
            Else
                frm.TxtAddress1.Text = frm.TxtAddress1.Text & " " & Dgv_Customer.Rows(idx).Cells("住所２").Value
            End If
            If Dgv_Customer.Rows(idx).Cells("住所３").Value Is DBNull.Value Then
            Else
                frm.TxtAddress1.Text = frm.TxtAddress1.Text & " " & Dgv_Customer.Rows(idx).Cells("住所３").Value
            End If
            If Dgv_Customer.Rows(idx).Cells("電話番号").Value Is DBNull.Value Then
            Else
                frm.TxtTel.Text = Dgv_Customer.Rows(idx).Cells("電話番号").Value
            End If
            If Dgv_Customer.Rows(idx).Cells("FAX番号").Value Is DBNull.Value Then
            Else
                frm.TxtFax.Text = Dgv_Customer.Rows(idx).Cells("FAX番号").Value
            End If
            If Dgv_Customer.Rows(idx).Cells("担当者名").Value Is DBNull.Value Then
            Else
                frm.TxtPerson.Text = Dgv_Customer.Rows(idx).Cells("担当者名").Value
            End If
            If Dgv_Customer.Rows(idx).Cells("担当者役職").Value Is DBNull.Value Then
            Else
                frm.TxtPosition.Text = Dgv_Customer.Rows(idx).Cells("担当者役職").Value
            End If
            If Dgv_Customer.Rows(idx).Cells("既定支払条件").Value Is DBNull.Value Then
            Else
                frm.TxtPaymentTerms.Text = Dgv_Customer.Rows(idx).Cells("既定支払条件").Value
            End If

            _parentForm.Enabled = True
            _parentForm.Show()
            Me.Dispose()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '戻るボタン押下時
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    '検索ボタン押下時
    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        setList()
    End Sub

    '一覧セット
    Private Sub setList()
        Dgv_Customer.Rows.Clear()

        Dim Sql As String = ""
        Try
            Sql += "SELECT * FROM public.m10_customer"
            Sql += " WHERE 会社コード ='" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " and 得意先名 ILIKE '%" & UtilClass.escapeSql(Search.Text) & "%'"
            Sql += " and is_active=0"
            Sql += " order by 会社コード, 得意先コード "

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                Dgv_Customer.Rows.Add()
                Dgv_Customer.Rows(i).Cells("得意先コード").Value = ds.Tables(RS).Rows(i)("得意先コード")    '得意先コード
                Dgv_Customer.Rows(i).Cells("得意先名").Value = ds.Tables(RS).Rows(i)("得意先名")                '得意先名
                Dgv_Customer.Rows(i).Cells("得意先名略称").Value = ds.Tables(RS).Rows(i)("得意先名略称")    '得意先名略称
                Dgv_Customer.Rows(i).Cells("郵便番号").Value = ds.Tables(RS).Rows(i)("郵便番号")            '郵便番号
                Dgv_Customer.Rows(i).Cells("住所１").Value = ds.Tables(RS).Rows(i)("住所１")                '住所１
                Dgv_Customer.Rows(i).Cells("住所２").Value = ds.Tables(RS).Rows(i)("住所２")                '住所２
                Dgv_Customer.Rows(i).Cells("住所３").Value = ds.Tables(RS).Rows(i)("住所３")                '住所３
                Dgv_Customer.Rows(i).Cells("電話番号").Value = ds.Tables(RS).Rows(i)("電話番号")            '電話番号
                Dgv_Customer.Rows(i).Cells("電話番号検索用").Value = ds.Tables(RS).Rows(i)("電話番号検索用") '電話番号検索用
                Dgv_Customer.Rows(i).Cells("FAX番号").Value = ds.Tables(RS).Rows(i)("FAX番号")              'FAX番号
                Dgv_Customer.Rows(i).Cells("担当者名").Value = ds.Tables(RS).Rows(i)("担当者名")            '担当者名
                Dgv_Customer.Rows(i).Cells("担当者役職").Value = ds.Tables(RS).Rows(i)("担当者役職")        '担当者役職
                Dgv_Customer.Rows(i).Cells("既定支払条件").Value = ds.Tables(RS).Rows(i)("既定支払条件")    '既定支払条件
                Dgv_Customer.Rows(i).Cells("メモ").Value = ds.Tables(RS).Rows(i)("メモ")                    '更新者
                Dgv_Customer.Rows(i).Cells("更新者").Value = ds.Tables(RS).Rows(i)("更新者")                '更新者
                Dgv_Customer.Rows(i).Cells("更新日").Value = ds.Tables(RS).Rows(i)("更新日")                '更新日
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try
    End Sub

End Class