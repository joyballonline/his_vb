Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class StockSearch
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
    Private _langHd As UtilLangHandler
    Private _parentForm As Form
    'Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ

    Private manufactuer As String
    Private itemName As String
    Private spec As String

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
                   ByRef prmManufacturer As String,
                   ByRef prmItemName As String,
                   ByRef prmSpec As String)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _parentForm = prmRefForm
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True

        manufactuer = prmManufacturer
        itemName = prmItemName
        spec = prmSpec

    End Sub

    '画面表示時
    Private Sub MstHanyoue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            DgvList.Columns("倉庫").HeaderText = "Warehouse"
            DgvList.Columns("数量").HeaderText = "Quantity"

            BtnBack.Text = "Back"
        End If

        Try

            '入庫データの取得
            '
            Sql = " select "
            'Sql += " t43.メーカー, t43.品名, t43.型式, sum(t43.入庫数量) as 入庫数量, t43.単位, t42.倉庫コード, m20.名称 "
            Sql += " t43.メーカー, t43.品名, t43.型式, sum(t43.入庫数量) as 入庫数量, t42.倉庫コード, m20.名称 "
            Sql += " from "
            Sql += " t43_nyukodt t43 "

            Sql += " LEFT JOIN "
            Sql += " t42_nyukohd t42 "
            Sql += " ON  t43.会社コード = t42.会社コード "
            Sql += " AND  t43.入庫番号 = t42.入庫番号 "

            Sql += " LEFT JOIN "
            Sql += " m20_warehouse m20 "
            Sql += " ON  t43.会社コード = m20.会社コード "
            Sql += " AND  t42.倉庫コード = m20.倉庫コード "

            Sql += " where "
            Sql += " t43.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND "
            Sql += " t42.取消区分 = '" & CommonConst.CANCEL_KBN_ENABLED & "'"
            Sql += " AND "
            Sql += " t43.メーカー ILIKE '" & manufactuer & "'"
            Sql += " AND "
            Sql += " t43.品名 ILIKE '" & itemName & "'"
            Sql += " AND "
            Sql += " t43.型式 ILIKE '" & spec & "'"
            Sql += " GROUP BY "
            'Sql += " t43.メーカー, t43.品名, t43.型式, t43.単位, t42.倉庫コード, m20.名称 "
            Sql += " t43.メーカー, t43.品名, t43.型式, t42.倉庫コード, m20.名称 "
            Sql += " order by "
            Sql += " t42.倉庫コード, t43.メーカー, t43.品名, t43.型式 "

            Dim dsNyuko As DataSet = _db.selectDB(Sql, RS, reccnt)

            '出庫データの取得
            '
            Sql = " select "
            'Sql += " t45.メーカー, t45.品名, t45.型式, sum(t45.出庫数量) as 出庫数量, t45.単位, t45.倉庫コード, m20.名称 "
            Sql += " t45.メーカー, t45.品名, t45.型式, sum(t45.出庫数量) as 出庫数量, t45.倉庫コード, m20.名称 "
            Sql += " from "
            Sql += " t45_shukodt t45 "

            Sql += " LEFT JOIN "
            Sql += " t44_shukohd t44 "
            Sql += " ON  t45.会社コード = t44.会社コード "
            Sql += " AND  t45.出庫番号 = t44.出庫番号 "

            Sql += " LEFT JOIN "
            Sql += " m20_warehouse m20 "
            Sql += " ON  t45.会社コード = m20.会社コード "
            Sql += " AND  t45.倉庫コード = m20.倉庫コード "

            Sql += " where "
            Sql += " t45.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND "
            Sql += " t44.取消区分 = '" & CommonConst.CANCEL_KBN_ENABLED & "'"
            Sql += " AND "
            Sql += " t45.メーカー ILIKE '" & manufactuer & "'"
            Sql += " AND "
            Sql += " t45.品名 ILIKE '" & itemName & "'"
            Sql += " AND "
            Sql += " t45.型式 ILIKE '" & spec & "'"
            Sql += " GROUP BY "
            'Sql += " t45.メーカー, t45.品名, t45.型式, t45.単位, t45.倉庫コード, m20.名称 "
            Sql += " t45.メーカー, t45.品名, t45.型式, t45.倉庫コード, m20.名称 "
            Sql += " order by "
            Sql += " t45.倉庫コード, t45.メーカー, t45.品名, t45.型式 "

            Dim dsShukko As DataSet = _db.selectDB(Sql, RS, reccnt)

            Dim tmpWarehouseCd As String = ""
            Dim tmpWarehouseName As String = ""
            Dim chkFlg As Boolean = False


            For i As Integer = 0 To dsNyuko.Tables(RS).Rows.Count - 1 '入庫データ

                '得意先コードが変わったら取得
                If (tmpWarehouseCd <> dsNyuko.Tables(RS).Rows(i)("倉庫コード").ToString) Then
                    tmpWarehouseName = dsNyuko.Tables(RS).Rows(i)("名称").ToString
                    tmpWarehouseCd = dsNyuko.Tables(RS).Rows(i)("倉庫コード").ToString
                Else
                    tmpWarehouseName = ""
                End If

                For x As Integer = 0 To dsShukko.Tables(RS).Rows.Count - 1 '出庫データ

                    '一致したら 入庫数 - 出庫数
                    If dsNyuko.Tables(RS).Rows(i)("倉庫コード") = dsShukko.Tables(RS).Rows(x)("倉庫コード") And
                                dsNyuko.Tables(RS).Rows(i)("メーカー") = dsShukko.Tables(RS).Rows(x)("メーカー") And
                                 dsNyuko.Tables(RS).Rows(i)("品名") = dsShukko.Tables(RS).Rows(x)("品名") And
                                  dsNyuko.Tables(RS).Rows(i)("型式") = dsShukko.Tables(RS).Rows(x)("型式") Then

                        DgvList.Rows.Add()
                        DgvList.Rows(i).Cells("倉庫").Value = tmpWarehouseName
                        DgvList.Rows(i).Cells("数量").Value = dsNyuko.Tables(RS).Rows(i)("入庫数量") - dsShukko.Tables(RS).Rows(x)("出庫数量")

                        chkFlg = True '入庫データと出庫データがある場合は true
                    End If

                Next

                '出庫データがなかった場合
                If chkFlg = False Then

                    DgvList.Rows.Add()
                    DgvList.Rows(i).Cells("倉庫").Value = dsNyuko.Tables(RS).Rows(i)("名称")
                    DgvList.Rows(i).Cells("数量").Value = dsNyuko.Tables(RS).Rows(i)("入庫数量")

                Else
                    chkFlg = False '初期化
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

    '戻るボタン押下時
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        'Dim QuoteRequest As Quote
        'QuoteRequest = New Quote(_msgHd, _db, _langHd, Me)
        'QuoteRequest.ShowDialog()
        _parentForm.Enabled = True

        Me.Close()
        Me.Dispose()

    End Sub

End Class