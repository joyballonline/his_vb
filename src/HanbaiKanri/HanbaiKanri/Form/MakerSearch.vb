﻿Option Explicit On

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

    Private Sub MstHanyoue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblMaker.Text = "Maker"
            LblItem.Text = "Item"
            LblModel.Text = "Model"
            BtnSelect.Text = "Select"
            BtnBack.Text = "Back"

        End If

        Dim Sql As String = ""
        Dim reccnt As Integer = 0

        Try
            '一覧クリア
            LbMaker.Items.Clear()
            LbItem.Items.Clear()
            LbModel.Items.Clear()

            If SelectColumn = "メーカー" Then
                Sql = "SELECT "
                Sql += "メーカー "
                Sql += "FROM "
                Sql += "public"
                Sql += "."
                Sql += "t02_mitdt"
                Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

                Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

                '重複無しのメーカリスト
                For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                    If LbMaker.Items.Contains(ds.Tables(RS).Rows(i)(0)) = False Then
                        LbMaker.Items.Add(ds.Tables(RS).Rows(i)(0))
                    End If
                Next

            ElseIf SelectColumn = "品名" Then

                LbMaker.Items.Add(Maker)
                LbMaker.SetSelected(0, True)
                Sql = "SELECT "
                Sql += " 品名 "
                Sql += "FROM "
                Sql += "public"
                Sql += "."
                Sql += "t02_mitdt"
                Sql += " WHERE "
                Sql += "メーカー"
                Sql += " = "
                Sql += "'"
                Sql += Maker
                Sql += "'"
                Sql += " AND 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

                Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

                For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                    If LbItem.Items.Contains(ds.Tables(RS).Rows(i)(0)) = False Then
                        LbItem.Items.Add(ds.Tables(RS).Rows(i)(0))
                    End If
                Next
            ElseIf SelectColumn = "型式" Then
                LbMaker.Items.Add(Maker)
                LbMaker.SetSelected(0, True)
                LbItem.Items.Add(Item)
                LbItem.SetSelected(0, True)

                Sql += "SELECT "
                Sql += " 型式 "
                Sql += "FROM "
                Sql += "public"
                Sql += "."
                Sql += "t02_mitdt"
                Sql += " WHERE "
                Sql += "品名"
                Sql += " = '"
                Sql += Item
                Sql += "'"
                Sql += " AND 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

                Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

                For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                    If LbModel.Items.Contains(ds.Tables(RS).Rows(i)(0)) = False Then
                        LbModel.Items.Add(ds.Tables(RS).Rows(i)(0))
                    End If
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

    Private Sub LbMaker_IndexChanged(sender As Object, e As EventArgs) Handles LbMaker.MouseClick
        LbItem.Items.Clear()
        LbModel.Items.Clear()

        Dim Sql As String = ""
        Try
            Sql += "SELECT "
            Sql += " 品名 "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t02_mitdt"
            Sql += " WHERE "
            Sql += "メーカー"
            Sql += " ILIKE "
            Sql += "'%"
            Sql += LbMaker.SelectedItem
            Sql += "%'"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                If LbItem.Items.Contains(ds.Tables(RS).Rows(index)(0)) = False Then
                    LbItem.Items.Add(ds.Tables(RS).Rows(index)(0))
                End If
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    Private Sub LbItem_IndexChanged(sender As Object, e As EventArgs) Handles LbItem.MouseClick
        LbModel.Items.Clear()

        Dim Sql As String = ""
        Try
            Sql += "SELECT "
            Sql += " 型式 "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t02_mitdt"
            Sql += " WHERE "
            Sql += "品名"
            Sql += " ILIKE "
            Sql += "'%"
            Sql += LbItem.SelectedItem
            Sql += "%'"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                If LbModel.Items.Contains(ds.Tables(RS).Rows(index)(0)) = False Then
                    LbModel.Items.Add(ds.Tables(RS).Rows(index)(0))
                End If
            Next

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

            frm.DgvItemList("メーカー", RowIdx).Value = IIf(LbMaker.SelectedIndex > -1, LbMaker.SelectedItem, "")
            frm.DgvItemList("品名", RowIdx).Value = IIf(LbItem.SelectedIndex > -1, LbItem.SelectedItem, "")
            frm.DgvItemList("型式", RowIdx).Value = IIf(LbModel.SelectedIndex > -1, LbModel.SelectedItem, "")

        Else
            '見積登録モード
            Dim frm As Quote = CType(Me.Owner, Quote)
            frm.DgvItemList("メーカー", RowIdx).Value = IIf(LbMaker.SelectedIndex > -1, LbMaker.SelectedItem, "")
            frm.DgvItemList("品名", RowIdx).Value = IIf(LbItem.SelectedIndex > -1, LbItem.SelectedItem, "")
            frm.DgvItemList("型式", RowIdx).Value = IIf(LbModel.SelectedIndex > -1, LbModel.SelectedItem, "")

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
End Class