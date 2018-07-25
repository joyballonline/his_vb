Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class SalesSearch
    Inherits System.Windows.Forms.Form

    '------------------------------------------------------------------------------------------------------
    'メンバー定数宣言
    '------------------------------------------------------------------------------------------------------
    'PG制御文字 
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
    Private _status As String
    Private _companyCode As String = frmC01F10_Login.loginValue.BumonNM
    Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ

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
                   Optional ByRef prmRefStatus As String = "")
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _parentForm = prmRefForm
        _status = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True

    End Sub

    Private Sub UserMaintenance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Sql As String = ""
        Try
            Sql += "SELECT "
            Sql += "会社コード, "
            Sql += "ユーザＩＤ, "
            Sql += "氏名, "
            Sql += "略名, "
            Sql += "備考, "
            Sql += "無効フラグ, "
            Sql += "権限, "
            Sql += "言語, "
            Sql += "更新者, "
            Sql += "更新日 "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "m02_user"
            Sql += " WHERE "
            Sql += "会社コード"
            Sql += " ILIKE "
            Sql += "'"
            Sql += _companyCode
            Sql += "'"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            'DataGridView1.DataSource = ds

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvUser.Rows.Add()
                DgvUser.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)(0)        '会社コード
                DgvUser.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)(1)        'ユーザＩＤ
                DgvUser.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)(2)              '氏名
                DgvUser.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)(3)              '略名
                DgvUser.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)(4)              '備考
                DgvUser.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)(5)        '無効フラグ
                DgvUser.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)(6)              '権限
                DgvUser.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)(7)              '言語
                DgvUser.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)(8)              '更新者
                DgvUser.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)(9)              '更新日
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    Private Sub BtnSelect_Click(sender As Object, e As EventArgs) Handles BtnSelect.Click
        If _status = "PURCHASE" Then
            Dim frm As OrderingAdd = CType(Me.Owner, OrderingAdd)
            Dim RowIndex As Integer = DgvUser.CurrentCell.RowIndex

            frm.TxtSales.Text = DgvUser.Rows(RowIndex).Cells("氏名").Value
        Else
            Dim frm As Quote = CType(Me.Owner, Quote)
            Dim RowIndex As Integer = DgvUser.CurrentCell.RowIndex

            frm.TxtSales.Text = DgvUser.Rows(RowIndex).Cells("氏名").Value
        End If


        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()


    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        DgvUser.Rows.Clear()

        Dim Sql As String = ""
        Try
            Sql += "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "m02_user"
            Sql += " WHERE "
            Sql += "会社コード"
            Sql += " ILIKE "
            Sql += "'%"
            Sql += Search.Text
            Sql += "%'"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvUser.Rows.Add()
                DgvUser.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)(0)        '会社コード
                DgvUser.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)(1)        '言語コード
                DgvUser.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)(2)        '氏名
                DgvUser.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)(3)      '略名
                DgvUser.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)(4)      '備考
                DgvUser.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)(5)      '無効フラグ
                DgvUser.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)(6)      '更新者
                DgvUser.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)(7)      '更新日
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub
End Class