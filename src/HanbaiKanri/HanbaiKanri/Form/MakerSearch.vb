Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class MakerSearch
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
    'Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ
    Private RowIdx As Integer
    Private ColIdx As Integer
    Private Maker As String
    Private Item As String
    Private Model As String
    Private SelectColumn As String
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
                   ByRef prmRefForm As Form,
                   ByRef prmRefRowIdx As Integer,
                   ByRef prmRefColIdx As Integer,
                   ByRef prmRefMaker As String,
                   ByRef prmRefItem As String,
                   ByRef prmRefModel As String,
                   Optional ByRef prmRefSelectColumn As String = "",
                   Optional ByRef prmRefStatus As String = "")
        Call Me.New()

        _init = False

        '初期処理

        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _parentForm = prmRefForm
        RowIdx = prmRefRowIdx
        ColIdx = prmRefColIdx
        SelectColumn = prmRefSelectColumn
        Maker = prmRefMaker
        Item = prmRefItem
        Model = prmRefModel
        _status = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True

    End Sub

    Private Sub MakerSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblManufacturer.Text = "Maker"
            LblItemName.Text = "Item"
            LblSpec.Text = "Model"
            BtnSelect.Text = "Select"
            BtnBack.Text = "Back"
        End If

        Dim Sql As String = ""
        Dim reccnt As Integer = 0

        Try
            '一覧クリア
            LbManufacturer.Items.Clear()
            LbItemName.Items.Clear()
            LbSpec.Items.Clear()

            '選択していたカラムによって振分
            If SelectColumn = "メーカー" Then

                setManufacturer()

            ElseIf SelectColumn = "品名" Then

                LbManufacturer.Items.Add(Maker)
                LbManufacturer.SetSelected(0, True)

                setItemName(Maker)

            ElseIf SelectColumn = "型式" Then

                LbManufacturer.Items.Add(Maker)
                LbManufacturer.SetSelected(0, True)
                LbItemName.Items.Add(Item)
                LbItemName.SetSelected(0, True)

                setSpec(Maker, Item)

            End If

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    'メーカーリストボックス選択時
    Private Sub LbMaker_IndexChanged(sender As Object, e As EventArgs) Handles LbManufacturer.MouseClick
        'リストボックス初期化
        LbItemName.Items.Clear()
        LbSpec.Items.Clear()

        Dim Sql As String = ""
        Try

            setItemName(LbManufacturer.SelectedItem)

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '品名リストボックス選択時
    Private Sub LbItem_IndexChanged(sender As Object, e As EventArgs) Handles LbItemName.MouseClick
        LbSpec.Items.Clear()

        Dim Sql As String = ""
        Try

            setSpec(LbManufacturer.SelectedItem, LbItemName.SelectedItem)

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '選択ボタン押下時
    Private Sub BtnSelectMaker_Click(sender As Object, e As EventArgs) Handles BtnSelect.Click

        If _status = CommonConst.STATUS_CLONE Then
            '複製モード
            Dim frm As Ordering = CType(Me.Owner, Ordering)

            frm.DgvItemList("メーカー", RowIdx).Value = IIf(LbManufacturer.SelectedIndex > -1, LbManufacturer.SelectedItem, "")
            frm.DgvItemList("品名", RowIdx).Value = IIf(LbItemName.SelectedIndex > -1, LbItemName.SelectedItem, "")
            frm.DgvItemList("型式", RowIdx).Value = IIf(LbSpec.SelectedIndex > -1, LbSpec.SelectedItem, "")

        ElseIf _status = CommonConst.STATUS_ADD Then
            '見積登録モード
            Dim frm As Quote = CType(Me.Owner, Quote)
            frm.DgvItemList("メーカー", RowIdx).Value = IIf(LbManufacturer.SelectedIndex > -1, LbManufacturer.SelectedItem, "")
            frm.DgvItemList("品名", RowIdx).Value = IIf(LbItemName.SelectedIndex > -1, LbItemName.SelectedItem, "")
            frm.DgvItemList("型式", RowIdx).Value = IIf(LbSpec.SelectedIndex > -1, LbSpec.SelectedItem, "")

        Else
            '移動入力画面
            Dim frm As MovementInput = CType(Me.Owner, MovementInput)
            frm.TxtManufacturer.Text = IIf(LbManufacturer.SelectedIndex > -1, LbManufacturer.SelectedItem, "")
            frm.TxtItemName.Text = IIf(LbItemName.SelectedIndex > -1, LbItemName.SelectedItem, "")
            frm.TxtSpec.Text = IIf(LbSpec.SelectedIndex > -1, LbSpec.SelectedItem, "")
        End If

        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    '戻るボタン押下時
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    'メーカーセット
    Private Sub setManufacturer(Optional ByRef prmManufacturer As String = "")
        Dim Sql As String = ""
        Dim reccnt As Integer = 0

        Sql = "SELECT "
        Sql += "メーカー "
        Sql += "FROM t02_mitdt"

        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += IIf(prmManufacturer <> "", " AND メーカー ILIKE '%" & prmManufacturer & "%'", "")
        Sql += " GROUP BY メーカー "
        Sql += " ORDER BY メーカー"

        Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

        '重複無しのメーカリスト
        For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
            LbManufacturer.Items.Add(ds.Tables(RS).Rows(i)(0))
        Next

    End Sub

    '品名セット
    Private Sub setItemName(ByVal prmManufacturer As String, Optional ByRef prmItemName As String = "")
        Dim Sql As String = ""
        Dim reccnt As Integer = 0

        Sql = "SELECT "
        Sql += " 品名 "
        Sql += "FROM t02_mitdt"

        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND "
        Sql += "メーカー = '" & prmManufacturer & "'"
        Sql += IIf(prmItemName <> "", " AND 品名 ILIKE '%" & prmItemName & "%'", "")
        Sql += " GROUP BY メーカー, 品名"
        Sql += " ORDER BY メーカー, 品名"

        Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

        For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
            LbItemName.Items.Add(ds.Tables(RS).Rows(i)(0))
        Next

    End Sub

    '型式セット
    Private Sub setSpec(ByVal prmManufacturer As String, ByVal prmItemName As String, Optional ByRef prmSpec As String = "")
        Dim Sql As String = ""
        Dim reccnt As Integer = 0

        Sql += "SELECT "
        Sql += " 型式 "
        Sql += "FROM t02_mitdt"

        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND "
        Sql += "メーカー = '" & prmManufacturer & "'"
        Sql += " AND "
        Sql += "品名 = '" & prmItemName & "'"
        Sql += IIf(prmSpec <> "", " AND 型式 ILIKE '%" & prmSpec & "%'", "")
        Sql += " GROUP BY メーカー, 品名, 型式"
        Sql += " ORDER BY メーカー, 品名, 型式"

        Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

        For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
            LbSpec.Items.Add(ds.Tables(RS).Rows(i)(0))
        Next

    End Sub

    'メーカー検索
    Private Sub TxtManufacturer_TextChanged(sender As Object, e As EventArgs) Handles TxtManufacturer.TextChanged
        LbManufacturer.Items.Clear()
        LbItemName.Items.Clear()
        LbSpec.Items.Clear()

        setManufacturer(TxtManufacturer.Text)
    End Sub

    '品名検索
    Private Sub TxtItemName_TextChanged(sender As Object, e As EventArgs) Handles TxtItemName.TextChanged
        LbItemName.Items.Clear()
        LbSpec.Items.Clear()

        setItemName(LbManufacturer.SelectedItem, TxtItemName.Text)
    End Sub

    '型式検索
    Private Sub TxtSpec_TextChanged(sender As Object, e As EventArgs) Handles TxtSpec.TextChanged
        LbSpec.Items.Clear()

        setSpec(LbManufacturer.SelectedItem, LbItemName.SelectedItem, TxtSpec.Text)
    End Sub

End Class