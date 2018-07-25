Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class SupplierSearch
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
    Private _parentForm As Form
    Private _langHd As UtilLangHandler
    'Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ
    Private RowIdx As Integer
    Private _status As String

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
                   ByRef prmRefDbLang As UtilLangHandler,
                   ByRef prmRefRowIdx As Integer,
                   ByRef prmRefForm As Form,
                   Optional ByRef prmRefStatus As String = "")
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        RowIdx = prmRefRowIdx
        _status = prmRefStatus
        _parentForm = prmRefForm
        _langHd = prmRefDbLang
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True

    End Sub

    Private Sub MstSupplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Sql As String = ""
        Try
            Sql += "SELECT "
            Sql += "会社コード, "
            Sql += "仕入先コード, "
            Sql += "仕入先名, "
            Sql += "仕入先名略称, "
            Sql += "郵便番号, "
            Sql += "住所１, "
            Sql += "住所２, "
            Sql += "住所３, "
            Sql += "電話番号, "
            Sql += "電話番号検索用, "
            Sql += "ＦＡＸ番号, "
            Sql += "担当者名, "
            Sql += "既定間接費率, "
            Sql += "メモ, "
            Sql += "銀行コード, "
            Sql += "支店コード, "
            Sql += "預金種目, "
            Sql += "口座番号, "
            Sql += "口座名義, "
            Sql += "更新者, "
            Sql += "更新日, "
            Sql += "担当者役職 "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "m11_supplier"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                Dgv_Supplier.Rows.Add()
                Dgv_Supplier.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)(0)        '会社コード
                Dgv_Supplier.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)(1)        '言語コード
                Dgv_Supplier.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)(2)        '氏名
                Dgv_Supplier.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)(3)      '略名
                Dgv_Supplier.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)(4)      '備考
                Dgv_Supplier.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)(5)      '無効フラグ
                Dgv_Supplier.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)(6)      '更新者
                Dgv_Supplier.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)(7)      '更新日
                Dgv_Supplier.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)(8)        '会社コード
                Dgv_Supplier.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)(9)        '言語コード
                Dgv_Supplier.Rows(index).Cells(10).Value = ds.Tables(RS).Rows(index)(10)        '氏名
                Dgv_Supplier.Rows(index).Cells(11).Value = ds.Tables(RS).Rows(index)(11)      '略名
                Dgv_Supplier.Rows(index).Cells(12).Value = ds.Tables(RS).Rows(index)(12)      '備考
                Dgv_Supplier.Rows(index).Cells(13).Value = ds.Tables(RS).Rows(index)(13)      '無効フラグ
                Dgv_Supplier.Rows(index).Cells(14).Value = ds.Tables(RS).Rows(index)(14)      '更新者
                Dgv_Supplier.Rows(index).Cells(15).Value = ds.Tables(RS).Rows(index)(15)      '更新日
                Dgv_Supplier.Rows(index).Cells(16).Value = ds.Tables(RS).Rows(index)(16)        '氏名
                Dgv_Supplier.Rows(index).Cells(17).Value = ds.Tables(RS).Rows(index)(17)      '略名
                Dgv_Supplier.Rows(index).Cells(18).Value = ds.Tables(RS).Rows(index)(18)      '備考
                Dgv_Supplier.Rows(index).Cells(19).Value = ds.Tables(RS).Rows(index)(19)      '無効フラグ
                Dgv_Supplier.Rows(index).Cells(20).Value = ds.Tables(RS).Rows(index)(20)      '更新者
                Dgv_Supplier.Rows(index).Cells(21).Value = ds.Tables(RS).Rows(index)(21)      '更新者
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    Private Sub btnSupplierSelect_Click(sender As Object, e As EventArgs) Handles btnSupplierSelect.Click

        If _status = "ADD" Then
            Dim frm As OrderingAdd = CType(Me.Owner, OrderingAdd)
            Dim idx As Integer = Dgv_Supplier.CurrentCell.RowIndex

            frm.TxtSupplierCode.Text = Dgv_Supplier.Rows(idx).Cells("仕入先コード").Value
            frm.TxtSupplierName.Text = Dgv_Supplier.Rows(idx).Cells("仕入先名").Value
            frm.TxtPostalCode.Text = Dgv_Supplier.Rows(idx).Cells("郵便番号").Value
            frm.TxtAddress1.Text = Dgv_Supplier.Rows(idx).Cells("住所１").Value
            frm.TxtAddress2.Text = Dgv_Supplier.Rows(idx).Cells("住所２").Value
            frm.TxtAddress3.Text = Dgv_Supplier.Rows(idx).Cells("住所３").Value
            frm.TxtTel.Text = Dgv_Supplier.Rows(idx).Cells("電話番号").Value
            frm.TxtFax.Text = Dgv_Supplier.Rows(idx).Cells("FAX番号").Value
            frm.TxtPerson.Text = Dgv_Supplier.Rows(idx).Cells("担当者名").Value
            frm.TxtPosition.Text = Dgv_Supplier.Rows(idx).Cells("担当者役職").Value
        Else
            Dim frm As Quote = CType(Me.Owner, Quote)
            Dim idx As Integer = Dgv_Supplier.CurrentCell.RowIndex

            frm.DgvItemList.Rows(RowIdx).Cells(7).Value = Dgv_Supplier.Rows(idx).Cells(2).Value
            frm.DgvItemList.Rows(RowIdx).Cells(9).Value = Dgv_Supplier.Rows(idx).Cells(12).Value
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
        Dgv_Supplier.Rows.Clear()

        Dim Sql As String = ""
        Try
            Sql += "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "m11_supplier"
            Sql += " WHERE "
            Sql += "会社コード"
            Sql += " ILIKE "
            Sql += "'%"
            Sql += Search.Text
            Sql += "%'"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                Dgv_Supplier.Rows.Add()
                Dgv_Supplier.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)(0)        '会社コード
                Dgv_Supplier.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)(1)        '言語コード
                Dgv_Supplier.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)(2)        '氏名
                Dgv_Supplier.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)(3)      '略名
                Dgv_Supplier.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)(4)      '備考
                Dgv_Supplier.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)(5)      '無効フラグ
                Dgv_Supplier.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)(6)      '更新者
                Dgv_Supplier.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)(7)      '更新日
                Dgv_Supplier.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)(8)        '会社コード
                Dgv_Supplier.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)(9)        '言語コード
                Dgv_Supplier.Rows(index).Cells(10).Value = ds.Tables(RS).Rows(index)(10)        '氏名
                Dgv_Supplier.Rows(index).Cells(11).Value = ds.Tables(RS).Rows(index)(11)      '略名
                Dgv_Supplier.Rows(index).Cells(12).Value = ds.Tables(RS).Rows(index)(12)      '備考
                Dgv_Supplier.Rows(index).Cells(13).Value = ds.Tables(RS).Rows(index)(13)      '無効フラグ
                Dgv_Supplier.Rows(index).Cells(14).Value = ds.Tables(RS).Rows(index)(14)      '更新者
                Dgv_Supplier.Rows(index).Cells(15).Value = ds.Tables(RS).Rows(index)(15)      '更新日
                Dgv_Supplier.Rows(index).Cells(16).Value = ds.Tables(RS).Rows(index)(16)        '氏名
                Dgv_Supplier.Rows(index).Cells(17).Value = ds.Tables(RS).Rows(index)(17)      '略名
                Dgv_Supplier.Rows(index).Cells(18).Value = ds.Tables(RS).Rows(index)(18)      '備考
                Dgv_Supplier.Rows(index).Cells(19).Value = ds.Tables(RS).Rows(index)(19)      '無効フラグ
                Dgv_Supplier.Rows(index).Cells(20).Value = ds.Tables(RS).Rows(index)(20)      '更新者
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