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

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblSupplierCd.Text = "SupplierCode"
            BtnSearch.Text = "Search"
            btnSupplierSelect.Text = "Select"
            BtnBack.Text = "Back"

            '英語用見出し
            Dgv_Supplier.Columns("仕入先コード").HeaderText = "SupplierCode"
            Dgv_Supplier.Columns("仕入先名").HeaderText = "SupplierName"
            Dgv_Supplier.Columns("仕入先名略名").HeaderText = "SupplierShortName"
            Dgv_Supplier.Columns("郵便番号").HeaderText = "PostalCode"
            Dgv_Supplier.Columns("住所１").HeaderText = "Address1"
            Dgv_Supplier.Columns("住所２").HeaderText = "Address2"
            Dgv_Supplier.Columns("住所３").HeaderText = "Address3"
            Dgv_Supplier.Columns("電話番号").HeaderText = "PhoneNumber"
            Dgv_Supplier.Columns("電話番号検索用").HeaderText = "SearchPhoneNumber"
            Dgv_Supplier.Columns("FAX番号").HeaderText = "FAX"
            Dgv_Supplier.Columns("担当者名").HeaderText = "PIC"
            Dgv_Supplier.Columns("既定間接費率").HeaderText = "DefaultOverheadRate"
            Dgv_Supplier.Columns("メモ").HeaderText = "Memo"
            Dgv_Supplier.Columns("銀行コード").HeaderText = "BankCode"
            Dgv_Supplier.Columns("支店コード").HeaderText = "BranchCode"
            Dgv_Supplier.Columns("預金種目").HeaderText = "DepositCategory"
            Dgv_Supplier.Columns("口座番号").HeaderText = "AccountNumber"
            Dgv_Supplier.Columns("口座名義").HeaderText = "AccountHolder"
            Dgv_Supplier.Columns("更新者").HeaderText = "Modified By"
            Dgv_Supplier.Columns("更新日").HeaderText = "UpdateDate"
            Dgv_Supplier.Columns("担当者役職").HeaderText = "PositionPICCustomer"
            Dgv_Supplier.Columns("関税率").HeaderText = "CustomsDutyRate"
            Dgv_Supplier.Columns("前払法人税率").HeaderText = "PrepaidCorporateTaxRate"
            Dgv_Supplier.Columns("輸送費率").HeaderText = "TransportationCostRate"

        End If


        Try
            Sql += "SELECT * FROM public.m11_supplier"
            Sql += " WHERE "
            Sql += "会社コード ='" & frmC01F10_Login.loginValue.BumonCD & "'"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                Dgv_Supplier.Rows.Add()
                Dgv_Supplier.Rows(i).Cells("仕入先コード").Value = ds.Tables(RS).Rows(i)("仕入先コード")
                Dgv_Supplier.Rows(i).Cells("仕入先名").Value = ds.Tables(RS).Rows(i)("仕入先名")
                Dgv_Supplier.Rows(i).Cells("仕入先名略名").Value = ds.Tables(RS).Rows(i)("仕入先名略称")
                Dgv_Supplier.Rows(i).Cells("郵便番号").Value = ds.Tables(RS).Rows(i)("郵便番号")
                Dgv_Supplier.Rows(i).Cells("住所１").Value = ds.Tables(RS).Rows(i)("住所１")
                Dgv_Supplier.Rows(i).Cells("住所２").Value = ds.Tables(RS).Rows(i)("住所２")
                Dgv_Supplier.Rows(i).Cells("住所３").Value = ds.Tables(RS).Rows(i)("住所３")
                Dgv_Supplier.Rows(i).Cells("電話番号").Value = ds.Tables(RS).Rows(i)("電話番号")
                Dgv_Supplier.Rows(i).Cells("電話番号検索用").Value = ds.Tables(RS).Rows(i)("電話番号検索用")
                Dgv_Supplier.Rows(i).Cells("FAX番号").Value = ds.Tables(RS).Rows(i)("ＦＡＸ番号")
                Dgv_Supplier.Rows(i).Cells("担当者名").Value = ds.Tables(RS).Rows(i)("担当者名")
                Dgv_Supplier.Rows(i).Cells("既定間接費率").Value = ds.Tables(RS).Rows(i)("既定間接費率")
                Dgv_Supplier.Rows(i).Cells("メモ").Value = ds.Tables(RS).Rows(i)("メモ")
                Dgv_Supplier.Rows(i).Cells("銀行コード").Value = ds.Tables(RS).Rows(i)("銀行コード")
                Dgv_Supplier.Rows(i).Cells("支店コード").Value = ds.Tables(RS).Rows(i)("支店コード")
                Dgv_Supplier.Rows(i).Cells("預金種目").Value = ds.Tables(RS).Rows(i)("預金種目")
                Dgv_Supplier.Rows(i).Cells("口座番号").Value = ds.Tables(RS).Rows(i)("口座番号")
                Dgv_Supplier.Rows(i).Cells("口座名義").Value = ds.Tables(RS).Rows(i)("口座名義")
                Dgv_Supplier.Rows(i).Cells("更新者").Value = ds.Tables(RS).Rows(i)("更新者")
                Dgv_Supplier.Rows(i).Cells("更新日").Value = ds.Tables(RS).Rows(i)("更新日")
                Dgv_Supplier.Rows(i).Cells("担当者役職").Value = ds.Tables(RS).Rows(i)("担当者役職")
                Dgv_Supplier.Rows(i).Cells("関税率").Value = ds.Tables(RS).Rows(i)("関税率")
                Dgv_Supplier.Rows(i).Cells("前払法人税率").Value = ds.Tables(RS).Rows(i)("前払法人税率")
                Dgv_Supplier.Rows(i).Cells("輸送費率").Value = ds.Tables(RS).Rows(i)("輸送費率")

            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '選択ボタンクリック時　＆　グリッド内ダブルクリック時
    '
    Private Sub btnSupplierSelect_Click(sender As Object, e As EventArgs) Handles btnSupplierSelect.Click, Dgv_Supplier.DoubleClick
        '明細が１行もない場合は選択不能
        If Dgv_Supplier.Rows.Count = 0 Then
            Exit Sub
        End If

        If _status = CommonConst.STATUS_ADD Then
            Dim frm As OrderingAdd = CType(Me.Owner, OrderingAdd)
            Dim idx As Integer = Dgv_Supplier.CurrentCell.RowIndex

            frm.TxtSupplierCode.Text = Dgv_Supplier.Rows(idx).Cells("仕入先コード").Value
            frm.TxtSupplierName.Text = Dgv_Supplier.Rows(idx).Cells("仕入先名").Value
            frm.TxtPostalCode.Text = Dgv_Supplier.Rows(idx).Cells("郵便番号").Value
            frm.TxtAddress1.Text = Dgv_Supplier.Rows(idx).Cells("住所１").Value & " " & Dgv_Supplier.Rows(idx).Cells("住所２").Value & " " & Dgv_Supplier.Rows(idx).Cells("住所３").Value
            frm.TxtTel.Text = Dgv_Supplier.Rows(idx).Cells("電話番号").Value
            frm.TxtFax.Text = Dgv_Supplier.Rows(idx).Cells("FAX番号").Value
            frm.TxtPerson.Text = Dgv_Supplier.Rows(idx).Cells("担当者名").Value
            frm.TxtPosition.Text = Dgv_Supplier.Rows(idx).Cells("担当者役職").Value
        ElseIf _status = CommonConst.STATUS_CLONE Then
            Dim frm As Ordering = CType(Me.Owner, Ordering)
            Dim idx As Integer = Dgv_Supplier.CurrentCell.RowIndex

            frm.TxtSupplierCode.Text = Dgv_Supplier.Rows(idx).Cells("仕入先コード").Value
            frm.TxtSupplierName.Text = Dgv_Supplier.Rows(idx).Cells("仕入先名").Value
            frm.TxtPostalCode.Text = Dgv_Supplier.Rows(idx).Cells("郵便番号").Value
            frm.TxtAddress1.Text = Dgv_Supplier.Rows(idx).Cells("住所１").Value & " " & Dgv_Supplier.Rows(idx).Cells("住所２").Value & " " & Dgv_Supplier.Rows(idx).Cells("住所３").Value
            frm.TxtTel.Text = Dgv_Supplier.Rows(idx).Cells("電話番号").Value
            frm.TxtFax.Text = Dgv_Supplier.Rows(idx).Cells("FAX番号").Value
            frm.TxtPerson.Text = Dgv_Supplier.Rows(idx).Cells("担当者名").Value
            frm.TxtPosition.Text = Dgv_Supplier.Rows(idx).Cells("担当者役職").Value
        Else
            Dim frm As Quote = CType(Me.Owner, Quote)
            Dim idx As Integer = Dgv_Supplier.CurrentCell.RowIndex

            frm.DgvItemList.Rows(RowIdx).Cells("仕入先コード").Value = Dgv_Supplier.Rows(idx).Cells("仕入先コード").Value
            frm.DgvItemList.Rows(RowIdx).Cells("仕入先").Value = Dgv_Supplier.Rows(idx).Cells("仕入先名").Value
            frm.DgvItemList.Rows(RowIdx).Cells("間接費率").Value = Dgv_Supplier.Rows(idx).Cells("既定間接費率").Value
            frm.DgvItemList.Rows(RowIdx).Cells("関税率").Value = Dgv_Supplier.Rows(idx).Cells("関税率").Value
            frm.DgvItemList.Rows(RowIdx).Cells("前払法人税率").Value = Dgv_Supplier.Rows(idx).Cells("前払法人税率").Value
            frm.DgvItemList.Rows(RowIdx).Cells("輸送費率").Value = Dgv_Supplier.Rows(idx).Cells("輸送費率").Value
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
            Sql += "SELECT * FROM public.m11_supplier"
            Sql += " WHERE "
            Sql += "会社コード ='" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " and 仕入先コード  ILIKE '%" & Search.Text & "%'"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                Dgv_Supplier.Rows.Add()
                Dgv_Supplier.Rows(i).Cells("仕入先コード").Value = ds.Tables(RS).Rows(i)("仕入先コード")
                Dgv_Supplier.Rows(i).Cells("仕入先名").Value = ds.Tables(RS).Rows(i)("仕入先名")
                Dgv_Supplier.Rows(i).Cells("仕入先名略名").Value = ds.Tables(RS).Rows(i)("仕入先名略称")
                Dgv_Supplier.Rows(i).Cells("郵便番号").Value = ds.Tables(RS).Rows(i)("郵便番号")
                Dgv_Supplier.Rows(i).Cells("住所１").Value = ds.Tables(RS).Rows(i)("住所１")
                Dgv_Supplier.Rows(i).Cells("住所２").Value = ds.Tables(RS).Rows(i)("住所２")
                Dgv_Supplier.Rows(i).Cells("住所３").Value = ds.Tables(RS).Rows(i)("住所３")
                Dgv_Supplier.Rows(i).Cells("電話番号").Value = ds.Tables(RS).Rows(i)("電話番号")
                Dgv_Supplier.Rows(i).Cells("電話番号検索用").Value = ds.Tables(RS).Rows(i)("電話番号検索用")
                Dgv_Supplier.Rows(i).Cells("FAX番号").Value = ds.Tables(RS).Rows(i)("ＦＡＸ番号")
                Dgv_Supplier.Rows(i).Cells("担当者名").Value = ds.Tables(RS).Rows(i)("担当者名")
                Dgv_Supplier.Rows(i).Cells("既定間接費率").Value = ds.Tables(RS).Rows(i)("既定間接費率")
                Dgv_Supplier.Rows(i).Cells("メモ").Value = ds.Tables(RS).Rows(i)("メモ")
                Dgv_Supplier.Rows(i).Cells("銀行コード").Value = ds.Tables(RS).Rows(i)("銀行コード")
                Dgv_Supplier.Rows(i).Cells("支店コード").Value = ds.Tables(RS).Rows(i)("支店コード")
                Dgv_Supplier.Rows(i).Cells("預金種目").Value = ds.Tables(RS).Rows(i)("預金種目")
                Dgv_Supplier.Rows(i).Cells("口座番号").Value = ds.Tables(RS).Rows(i)("口座番号")
                Dgv_Supplier.Rows(i).Cells("口座名義").Value = ds.Tables(RS).Rows(i)("口座名義")
                Dgv_Supplier.Rows(i).Cells("更新者").Value = ds.Tables(RS).Rows(i)("更新者")
                Dgv_Supplier.Rows(i).Cells("更新日").Value = ds.Tables(RS).Rows(i)("更新日")
                Dgv_Supplier.Rows(i).Cells("担当者役職").Value = ds.Tables(RS).Rows(i)("担当者役職")
                Dgv_Supplier.Rows(i).Cells("関税率").Value = ds.Tables(RS).Rows(i)("関税率")
                Dgv_Supplier.Rows(i).Cells("前払法人税率").Value = ds.Tables(RS).Rows(i)("前払法人税率")
                Dgv_Supplier.Rows(i).Cells("輸送費率").Value = ds.Tables(RS).Rows(i)("輸送費率")
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