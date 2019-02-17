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
    Private _companyCode As String = frmC01F10_Login.loginValue.BumonNM

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

    Private Sub MstCustomere_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Sql As String = ""
        Try
            Sql += "SELECT "
            Sql += "会社コード "
            Sql += " ,得意先コード "
            Sql += " ,得意先名 "
            Sql += " ,得意先名略称 "
            Sql += " ,郵便番号 "
            Sql += " ,住所１ "
            Sql += " ,住所２ "
            Sql += " ,住所３ "
            Sql += " ,電話番号 "
            Sql += " ,電話番号検索用 "
            Sql += " ,ＦＡＸ番号 "
            Sql += " ,担当者名 "
            Sql += " ,担当者役職 "
            Sql += " ,既定支払条件 "
            Sql += " ,メモ "
            Sql += " ,更新者 "
            Sql += " ,更新日 "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "m10_customer"
            Sql += " WHERE "
            Sql += "会社コード ='" & frmC01F10_Login.loginValue.BumonCD & "'"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                Dgv_Customer.Rows.Add()
                Dgv_Customer.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)(0)        '会社コード
                Dgv_Customer.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)(1)        '言語コード
                Dgv_Customer.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)(2)        '氏名
                Dgv_Customer.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)(3)      '略名
                Dgv_Customer.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)(4)      '備考
                Dgv_Customer.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)(5)      '無効フラグ
                Dgv_Customer.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)(6)      '更新者
                Dgv_Customer.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)(7)      '更新日
                Dgv_Customer.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)(8)        '会社コード
                Dgv_Customer.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)(9)        '言語コード
                Dgv_Customer.Rows(index).Cells(10).Value = ds.Tables(RS).Rows(index)(10)        '氏名
                Dgv_Customer.Rows(index).Cells(11).Value = ds.Tables(RS).Rows(index)(11)      '略名
                Dgv_Customer.Rows(index).Cells(12).Value = ds.Tables(RS).Rows(index)(12)      '備考
                Dgv_Customer.Rows(index).Cells(13).Value = ds.Tables(RS).Rows(index)(13)      '無効フラグ
                Dgv_Customer.Rows(index).Cells(14).Value = ds.Tables(RS).Rows(index)(14)      '更新者
                Dgv_Customer.Rows(index).Cells(15).Value = ds.Tables(RS).Rows(index)(15)      '更新日
                Dgv_Customer.Rows(index).Cells(16).Value = ds.Tables(RS).Rows(index)(16)      '更新日
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try
        If frmC01F10_Login.loginValue.Language = "ENG" Then
            LblCustomerName.Text = "CustomerName"
            BtnSearch.Text = "Search"
            btnSelectCustomer.Text = "Select"
            btnBack.Text = "Back"

            Dgv_Customer.Columns("会社コード").HeaderText = "CompanyCode"
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
    End Sub

    Private Sub btnSelectCustomer_Click(sender As Object, e As EventArgs) Handles btnSelectCustomer.Click, Dgv_Customer.DoubleClick
        Try
            'メニュー選択処理
            Dim idx As Integer
            Dim frm As Quote = CType(Me.Owner, Quote)

            '一覧選択行インデックスの取得
            For Each c As DataGridViewRow In Dgv_Customer.SelectedRows
                idx = c.Index
                Exit For
            Next c

            frm.TxtCustomerCode.Text = Dgv_Customer.Rows(idx).Cells(1).Value
            frm.TxtCustomerName.Text = Dgv_Customer.Rows(idx).Cells(2).Value
            If Dgv_Customer.Rows(idx).Cells(4).Value Is DBNull.Value Then
            Else
                frm.TxtPostalCode.Text = Dgv_Customer.Rows(idx).Cells(4).Value
            End If
            If Dgv_Customer.Rows(idx).Cells(5).Value Is DBNull.Value Then
            Else
                frm.TxtAddress1.Text = Dgv_Customer.Rows(idx).Cells(5).Value
            End If
            If Dgv_Customer.Rows(idx).Cells(6).Value Is DBNull.Value Then
            Else
                frm.TxtAddress1.Text = frm.TxtAddress1.Text & " " & Dgv_Customer.Rows(idx).Cells(6).Value
            End If
            If Dgv_Customer.Rows(idx).Cells(7).Value Is DBNull.Value Then
            Else
                frm.TxtAddress1.Text = frm.TxtAddress1.Text & " " & Dgv_Customer.Rows(idx).Cells(7).Value
            End If
            If Dgv_Customer.Rows(idx).Cells(8).Value Is DBNull.Value Then
            Else
                frm.TxtTel.Text = Dgv_Customer.Rows(idx).Cells(8).Value
            End If
            If Dgv_Customer.Rows(idx).Cells(10).Value Is DBNull.Value Then
            Else
                frm.TxtFax.Text = Dgv_Customer.Rows(idx).Cells(10).Value
            End If
            If Dgv_Customer.Rows(idx).Cells(11).Value Is DBNull.Value Then
            Else
                frm.TxtPerson.Text = Dgv_Customer.Rows(idx).Cells(11).Value
            End If
            If Dgv_Customer.Rows(idx).Cells(12).Value Is DBNull.Value Then
            Else
                frm.TxtPosition.Text = Dgv_Customer.Rows(idx).Cells(12).Value
            End If
            If Dgv_Customer.Rows(idx).Cells(13).Value Is DBNull.Value Then
            Else
                frm.TxtPaymentTerms.Text = Dgv_Customer.Rows(idx).Cells(13).Value
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

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        Dgv_Customer.Rows.Clear()

        Dim Sql As String = ""
        Try
            Sql += "SELECT * FROM public.m10_customer"
            Sql += " WHERE 会社コード ='" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " and 得意先名 ILIKE '%" & Search.Text & "%'"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                Dgv_Customer.Rows.Add()
                Dgv_Customer.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)(0)        '会社コード
                Dgv_Customer.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)(1)        '言語コード
                Dgv_Customer.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)(2)        '氏名
                Dgv_Customer.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)(3)      '略名
                Dgv_Customer.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)(4)      '備考
                Dgv_Customer.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)(5)      '無効フラグ
                Dgv_Customer.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)(6)      '更新者
                Dgv_Customer.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)(7)      '更新日
                Dgv_Customer.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)(8)        '会社コード
                Dgv_Customer.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)(9)        '言語コード
                Dgv_Customer.Rows(index).Cells(10).Value = ds.Tables(RS).Rows(index)(10)        '氏名
                Dgv_Customer.Rows(index).Cells(11).Value = ds.Tables(RS).Rows(index)(11)      '略名
                Dgv_Customer.Rows(index).Cells(12).Value = ds.Tables(RS).Rows(index)(12)      '備考
                Dgv_Customer.Rows(index).Cells(13).Value = ds.Tables(RS).Rows(index)(13)      '無効フラグ
                Dgv_Customer.Rows(index).Cells(14).Value = ds.Tables(RS).Rows(index)(14)      '更新者
                Dgv_Customer.Rows(index).Cells(15).Value = ds.Tables(RS).Rows(index)(15)      '更新日
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