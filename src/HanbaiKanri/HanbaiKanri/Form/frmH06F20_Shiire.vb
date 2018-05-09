'===============================================================================
'
'　カネキ吉田商店様
'　　（システム名）販売管理システム
'　　（処理機能名）仕入入力画面
'    （フォームID）H06F20
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   菅野雄      2018/3/14                  新規              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView

Public Class frmH06F20_Shiire
    Inherits System.Windows.Forms.Form

#Region "宣言"
    '-------------------------------------------------------------------------------
    '   変数定義
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _comLogc As CommonLogic                             '共通処理用
    Private _gh As UtilDataGridViewHandler                      'DataGridViewユーティリティクラス
    Private _parentForm As Form                                 '親フォーム
    Private _SelectID As String                                 'メニュー選択処理ID
    Private _userId As String                                   'ログインユーザＩＤ
    Private _init As Boolean                                    '初期処理済フラグ

    Private _ShoriMode As Integer                               '処理モード（登録、変更、取消、照会)
    Private _SyoriName As String                                '処理名

    Private _ZeiRitsu As Double                                 '税率
    Private _KinHasu As String                                  '金額端数区分
    Private _ZeiSanshutsu As String                             '税算出区分
    Private _ZeiHasu As String                                  '税端数区分
    Private _UpdateTime As DateTime = Nothing                   '更新日時　排他チェックに使用する

    '-------------------------------------------------------------------------------
    '   定数定義
    '-------------------------------------------------------------------------------
    Private Const N As String = ControlChars.NewLine            '改行文字
    Private Const RS As String = "RecSet"                       'レコードセットテーブル

    'DataGridView-------------------------------------------------------------------
    'グリッド列№
    'dgvIchiran
    Private Const COLNO_NO = 0                                      '01:No.
    Private Const COLNO_ITEMCD = 1                                  '02:商品CD
    Private Const COLNO_ITEMNM = 2                                  '03:商品名
    Private Const COLNO_ITEMDT = 3                                  '04:商品詳細
    Private Const COLNO_ZEIKBN = 4                                  '05:税
    Private Const COLNO_IRISUU = 5                                  '06:入数
    Private Const COLNO_KOSUU = 6                                   '07:個数
    Private Const COLNO_SURYOU = 7                                  '08:数量
    Private Const COLNO_TANNI = 8                                   '09:単位
    Private Const COLNO_SHIIRETANKA = 9                             '10:仕入単価
    Private Const COLNO_SHIIREKINGAKU = 10                          '11:仕入金額
    Private Const COLNO_MEISAIBIKOU = 11                            '12:仕入明細備考
    Private Const COLNO_ZEIKBNCD = 12                               '13:課税区分
    Private Const COLNO_TAX_RATE = 13                               '14:税率
    Private Const COLNO_TAX_EXCLUSION = 14                          '15:税抜額
    Private Const COLNO_TAX_TAXABLE = 15                            '16:課税対象額
    Private Const COLNO_TAX_AMOUNT = 16                             '17:消費税額

    'グリッドデータ名
    'dgvIchiran
    Private Const DTCOL_NO As String = "dtNo"                       '01:No.
    Private Const DTCOL_ITEMCD As String = "dtItemCd"               '02:商品CD
    Private Const DTCOL_ITEMNM As String = "dtItemNm"               '03:商品名
    Private Const DTCOL_ITEMDT As String = "dtItemDt"               '04:商品詳細
    Private Const DTCOL_ZEIKBN As String = "dtZeiKbn"               '05:税
    Private Const DTCOL_IRISUU As String = "dtIrisuu"               '06:入数
    Private Const DTCOL_KOSUU As String = "dtKosuu"                 '07:個数
    Private Const DTCOL_SURYOU As String = "dtSuryou"               '08:数量
    Private Const DTCOL_TANNI As String = "dtTanni"                 '09:単位
    Private Const DTCOL_SHIIRETANKA As String = "dtShiireTanka"     '10:仕入単価
    Private Const DTCOL_SHIIREKINGAKU As String = "dtShiireKingaku" '11:仕入金額
    Private Const DTCOL_MEISAIBIKOU As String = "dtMeisaiBikou"     '12:仕入明細備考
    Private Const DTCOL_ZEIKBNCD As String = "dtZeiKbnCD"           '13:課税区分
    Private Const DTCOL_TAX_RATE As String = "dtTaxRate"            '14:税率
    Private Const DTCOL_TAX_EXCLUSION As String = "dtTaxExclusion"  '15:税抜額
    Private Const DTCOL_TAX_TAXABLE As String = "dtTaxAble"         '16:課税対象額
    Private Const DTCOL_TAX_AMOUNT As String = "dtTaxAmount"        '17:消費税額

#End Region

    '-------------------------------------------------------------------------------
    'デフォルトコンストラクタ（隠蔽）
    '-------------------------------------------------------------------------------
    Private Sub New()
        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

    End Sub

    '-------------------------------------------------------------------------------
    'コンストラクタ　メニューから呼ばれる
    '引数
    '   prmSelectID     : メニュー選択処理ID
    '   prmKidoShoriID  : 起動元ID
    '   prmParentForm   : 呼び出し元フォーム
    '   prmDenpyoNo     : 仕入一覧から伝票番号が渡されるときに使用する
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByRef prmSelectID As String, ByRef prmKidoShoriID As String, ByRef prmParentForm As Form, Optional prmDenpyoNo As String = "")
        Call Me.New()

        Try
            _init = False

            '初期処理
            _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
            _db = prmRefDbHd                                                    'DBハンドラの設定
            _comLogc = New CommonLogic(_db, _msgHd)                             '共通処理用
            _gh = New UtilDataGridViewHandler(dgvIchiran)                       'DataGridViewユーティリティクラス
            _parentForm = prmParentForm                                         '親フォーム
            _SelectID = prmSelectID                                             'メニュー選択処理ID
            Me.lblDenpyoNo.Text = prmDenpyoNo                                   '仕入伝番
            _userId = frmC01F10_Login.loginValue.TantoCD                        'ユーザＩＤ

            StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
            Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                   'フォームタイトル表示

            '初期処理
            initProcess()

            _init = True

            '操作履歴ログ作成（入力画面起動）
            _comLogc.Insert_L01_ProcLog(frmC01F10_Login.loginValue.BumonCD, DBNull.Value, _SelectID, CommonConst.PROGRAM_UPDATE, CommonConst.STATUS_NORMAL,
                                        Me.lblDenpyoNo.Text, _SyoriName, DBNull.Value, DBNull.Value, DBNull.Value,
                                        DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _userId)


        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

#Region "ボタン"
    '戻るボタン押下時
    Private Sub cmdModoru_Click(sender As Object, e As EventArgs) Handles cmdModoru.Click

        _parentForm.Show()                                              ' 前画面を表示
        _parentForm.Enabled = True                                      ' 前画面の使用を可能にする
        _parentForm.Activate()                                          ' 前画面をアクティブにする

        Me.Dispose()                                                    ' 自画面を閉じる
    End Sub

    '登録ボタン押下時
    Private Sub cmdTouroku_Click(sender As Object, e As EventArgs) Handles cmdTouroku.Click
        Try
            '登録処理
            touroku()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '複写元選択ボタン押下時
    Private Sub cmdFukusya_Click(sender As Object, e As EventArgs) Handles cmdFukusya.Click
        Try
            '複写元選択ボタン押下時処理
            fukusyaClick()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '先頭へボタン押下時
    Private Sub cmdTopRow_Click(sender As Object, e As EventArgs) Handles cmdTopRow.Click

        If _gh.getMaxRow > 0 Then
            dgvIchiran.CurrentCell = dgvIchiran.Rows(0).Cells(1)
        End If

    End Sub

    '行追加ボタン押下時
    Private Sub cmdAddRow_Click(sender As Object, e As EventArgs) Handles cmdAddRow.Click
        For Each c As DataGridViewCell In dgvIchiran.SelectedCells
            If TypeOf Me.dgvIchiran.DataSource Is DataSet Then
                Dim ds As DataSet = TryCast(Me.dgvIchiran.DataSource, DataSet)
                ds.Tables(RS).Rows.InsertAt(ds.Tables(RS).NewRow, c.RowIndex + 1)
            ElseIf TypeOf Me.dgvIchiran.DataSource Is DataTable Then
                Dim dt As DataTable = TryCast(Me.dgvIchiran.DataSource, DataTable)
                dt.Rows.Add()
            End If
        Next c
        '行番号降り直し
        For i As Integer = 0 To dgvIchiran.RowCount - 1
            dgvIchiran.Rows(i).Cells(COLNO_NO).Value = i + 1
            lblMeisaiCnt.Text = dgvIchiran.RowCount
        Next i
    End Sub

    '行削除ボタン押下時
    Private Sub cmdDelRow_Click(sender As Object, e As EventArgs) Handles cmdDelRow.Click
        'グリッドに１明細しかないときは削除しない
        If dgvIchiran.RowCount <= 1 Then
            Exit Sub
        End If

        If _gh.getMaxRow > 0 Then

            Dim piRtn As Integer

            '確認メッセージを表示する
            piRtn = _msgHd.dspMSG("confirmDeleteRow")  '選択行を削除します。よろしいですか？
            If piRtn = vbNo Then
                Exit Sub
            End If

            If TypeOf Me.dgvIchiran.DataSource Is DataSet Then
                Dim ds As DataSet = TryCast(Me.dgvIchiran.DataSource, DataSet)
                ds.Tables(RS).Rows.RemoveAt(Me.dgvIchiran.CurrentCell.RowIndex)
            ElseIf TypeOf Me.dgvIchiran.DataSource Is DataTable Then
                Dim dt As DataTable = TryCast(Me.dgvIchiran.DataSource, DataTable)
                dt.Rows.RemoveAt(Me.dgvIchiran.CurrentCell.RowIndex)
            End If
        End If

        '行番号降り直し
        For i As Integer = 0 To dgvIchiran.RowCount - 1
            dgvIchiran.Rows(i).Cells(COLNO_NO).Value = i + 1
            lblMeisaiCnt.Text = dgvIchiran.RowCount
        Next i
    End Sub

    '行複写処理
    Private Sub cmdRowCopy_Click(sender As Object, e As EventArgs) Handles cmdRowCopy.Click
        For Each c As DataGridViewCell In dgvIchiran.SelectedCells
            If TypeOf Me.dgvIchiran.DataSource Is DataSet Then
                Dim ds As DataSet = TryCast(Me.dgvIchiran.DataSource, DataSet)
                ds.Tables(RS).Rows.InsertAt(ds.Tables(RS).NewRow, c.RowIndex + 1)
                ds.Tables(RS).Rows(c.RowIndex + 1).ItemArray = ds.Tables(RS).Rows(c.RowIndex).ItemArray
            ElseIf TypeOf Me.dgvIchiran.DataSource Is DataTable Then
                Dim dt As DataTable = TryCast(Me.dgvIchiran.DataSource, DataTable)
                dt.Rows.Add()
            End If
        Next c
        '行番号降り直し
        For i As Integer = 0 To dgvIchiran.RowCount - 1
            dgvIchiran.Rows(i).Cells(COLNO_NO).Value = i + 1
            lblMeisaiCnt.Text = dgvIchiran.RowCount
        Next i

    End Sub

    '行移動↑
    Private Sub cmdSwapRowUp_Click(sender As Object, e As EventArgs) Handles cmdSwapRowUp.Click

        For Each c As DataGridViewCell In dgvIchiran.SelectedCells
            '先頭行の場合処理しない
            If c.RowIndex = 0 Then
                Exit Sub
            End If
            If TypeOf Me.dgvIchiran.DataSource Is DataSet Then
                Dim ds As DataSet = TryCast(Me.dgvIchiran.DataSource, DataSet)
                Dim row1 As Object
                Dim row2 As Object
                row1 = ds.Tables(RS).Rows(c.RowIndex - 1).ItemArray
                row2 = ds.Tables(RS).Rows(c.RowIndex).ItemArray
                ds.Tables(RS).Rows(c.RowIndex - 1).ItemArray = row2
                ds.Tables(RS).Rows(c.RowIndex).ItemArray = row1
                dgvIchiran.CurrentCell = dgvIchiran(c.ColumnIndex, c.RowIndex - 1)   'カーソル移動
            ElseIf TypeOf Me.dgvIchiran.DataSource Is DataTable Then
                Dim dt As DataTable = TryCast(Me.dgvIchiran.DataSource, DataTable)
                dt.Rows.Add()
            End If
        Next c
        '行番号降り直し
        For i As Integer = 0 To dgvIchiran.RowCount - 1
            dgvIchiran.Rows(i).Cells(COLNO_NO).Value = i + 1
        Next i

    End Sub

    '行複写クリック時
    Private Sub cmdSwapRowDown_Click(sender As Object, e As EventArgs) Handles cmdSwapRowDown.Click

        For Each c As DataGridViewCell In dgvIchiran.SelectedCells
            '先頭行の場合処理しない
            If c.RowIndex = dgvIchiran.RowCount - 1 Then
                Exit Sub
            End If
            If TypeOf Me.dgvIchiran.DataSource Is DataSet Then
                Dim ds As DataSet = TryCast(Me.dgvIchiran.DataSource, DataSet)
                Dim row1 As Object
                Dim row2 As Object
                row1 = ds.Tables(RS).Rows(c.RowIndex + 1).ItemArray
                row2 = ds.Tables(RS).Rows(c.RowIndex).ItemArray
                ds.Tables(RS).Rows(c.RowIndex + 1).ItemArray = row2
                ds.Tables(RS).Rows(c.RowIndex).ItemArray = row1
                dgvIchiran.CurrentCell = dgvIchiran(c.ColumnIndex, c.RowIndex + 1)   'カーソル移動
            ElseIf TypeOf Me.dgvIchiran.DataSource Is DataTable Then
                Dim dt As DataTable = TryCast(Me.dgvIchiran.DataSource, DataTable)
                dt.Rows.Add()
            End If
        Next c
        '行番号降り直し
        For i As Integer = 0 To dgvIchiran.RowCount - 1
            dgvIchiran.Rows(i).Cells(COLNO_NO).Value = i + 1
        Next i

    End Sub

#End Region

#Region "コントロールハンドル"

    '-------------------------------------------------------------------------------
    '　セルに正しくない値を入力した場合、エラーにしない
    '-------------------------------------------------------------------------------
    Private Sub dgvIchiran_DataError(ByVal sender As Object, ByVal e As DataGridViewDataErrorEventArgs) Handles dgvIchiran.DataError
        If Not (e.Exception Is Nothing) Then
            e.Cancel = False
        End If
    End Sub

    '一覧セルをダブルクリックしたとき
    Private Sub dgvIchiran_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvIchiran.CellDoubleClick
        Try
            '検索ウインドウオープン
            SelectWindowOpen()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '一覧でキーを押下したら
    Private Sub dgvIchiran_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles dgvIchiran.KeyDown
        Try
            Select Case e.KeyCode

                Case Keys.Space
                    e.Handled = True
                    '検索ウインドウオープン
                    Select Case dgvIchiran.CurrentCell.ColumnIndex
                        Case COLNO_ITEMCD, COLNO_ZEIKBN, COLNO_TANNI
                            SelectWindowOpen()
                    End Select

                Case Keys.Enter
                    Dim iRow As Integer = dgvIchiran.CurrentCell.RowIndex
                    Dim iCol As Integer = dgvIchiran.CurrentCell.ColumnIndex

                    If iRow = _gh.getMaxRow - 1 AndAlso iCol = COLNO_MEISAIBIKOU Then
                        'アクティブセルが最終行の備考列の場合、最下行に行追加
                        If TypeOf Me.dgvIchiran.DataSource Is DataSet Then
                            Dim ds As DataSet = TryCast(Me.dgvIchiran.DataSource, DataSet)
                            ds.Tables(RS).Rows.Add()
                        ElseIf TypeOf Me.dgvIchiran.DataSource Is DataTable Then
                            Dim dt As DataTable = TryCast(Me.dgvIchiran.DataSource, DataTable)
                            dt.Rows.Add()
                        End If
                        '行番号降り直し
                        For i As Integer = 0 To dgvIchiran.RowCount - 1
                            dgvIchiran.Rows(i).Cells(COLNO_NO).Value = i + 1
                            lblMeisaiCnt.Text = dgvIchiran.RowCount
                        Next i

                    End If
            End Select
            dgvIchiran.FirstDisplayedScrollingColumnIndex = dgvIchiran.CurrentCell.ColumnIndex

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    'リスト部分のCellEnterイベント
    Private Sub dgvIchiran_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvIchiran.CellEnter

        '列ごとにIMEModeを制御する
        Select Case e.ColumnIndex
            Case COLNO_ITEMDT, COLNO_MEISAIBIKOU
                dgvIchiran.ImeMode = Windows.Forms.ImeMode.Hiragana
            Case COLNO_IRISUU, COLNO_KOSUU, COLNO_SURYOU, COLNO_SHIIRETANKA, COLNO_SHIIREKINGAKU
                dgvIchiran.ImeMode = Windows.Forms.ImeMode.Off
        End Select

    End Sub

    '仕入日の値変更時
    Private Sub dtpShiireDt_ValueChanged(sender As Object, e As System.EventArgs) Handles dtpShiireDt.ValueChanged
        Try
            '初期処理前の場合は処理終了
            If _init = False Then
                Exit Sub
            End If

            '仕入日の値変更時処理
            shiireDtChanged()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '仕入先コードの値変更時
    Private Sub txtShiiresakiCd_Validated(sender As Object, e As EventArgs) Handles txtShiiresakiCd.Validated
        Try
            '初期処理前の場合は処理終了
            If _init = False Then
                Exit Sub
            End If

            '仕入日コードの値変更時処理
            shiiresakiCdValidated()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '仕入先コードをダブルクリックしたとき
    Private Sub txtShiiresakiCd_DoubleClick(sender As Object, e As EventArgs) Handles txtShiiresakiCd.DoubleClick
        Try
            '仕入先コードダブルクリック時処理
            shiiresakiCdDoubleClick()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '一覧 入力値変更時
    Private Sub dgvIchiran_CellValidated(sender As Object, e As DataGridViewCellEventArgs) Handles dgvIchiran.CellValueChanged
        Try
            '一覧 入力値変更時処理
            ichiranValidated(e)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　キープレスイベント
    '-------------------------------------------------------------------------------
    Private Sub ctl_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles _
                             dtpShiireDt.KeyPress,
                             txtShiiresakiCd.KeyPress,
                             txtBikou.KeyPress

        '押下キーがEnterの場合、次のコントロールへフォーカス移動
        UtilMDL.UtilClass.moveNextFocus(Me, e)
    End Sub

    '-------------------------------------------------------------------------------
    '　フォーカス取得イベント
    '-------------------------------------------------------------------------------
    Private Sub ctl_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
                             dtpShiireDt.GotFocus,
                             txtShiiresakiCd.GotFocus,
                             txtBikou.GotFocus

        'フォーカス取得時、入力パラメタのコントロールを全選択状態とする
        Call UtilClass.selAll(sender)
    End Sub

#End Region

    '一覧初期化処理
    '
    Private Sub initDgvIchiran()

        Dim dt As DataTable = New DataTable(RS)
        dt.Columns().Add(DTCOL_NO, Type.GetType("System.String"))
        dt.Columns().Add(DTCOL_ITEMCD, Type.GetType("System.String"))
        dt.Columns().Add(DTCOL_ITEMNM, Type.GetType("System.String"))
        dt.Columns().Add(DTCOL_ITEMDT, Type.GetType("System.String"))
        dt.Columns().Add(DTCOL_ZEIKBN, Type.GetType("System.String"))
        dt.Columns().Add(DTCOL_IRISUU, Type.GetType("System.Double"))
        dt.Columns().Add(DTCOL_KOSUU, Type.GetType("System.Decimal"))
        dt.Columns().Add(DTCOL_SURYOU, Type.GetType("System.Double"))
        dt.Columns().Add(DTCOL_TANNI, Type.GetType("System.String"))
        dt.Columns().Add(DTCOL_SHIIRETANKA, Type.GetType("System.Double"))
        dt.Columns().Add(DTCOL_SHIIREKINGAKU, Type.GetType("System.Decimal"))
        dt.Columns().Add(DTCOL_MEISAIBIKOU, Type.GetType("System.String"))
        dt.Columns().Add(DTCOL_ZEIKBNCD, Type.GetType("System.String"))
        dt.Columns().Add(DTCOL_TAX_RATE, Type.GetType("System.Decimal"))
        dt.Columns().Add(DTCOL_TAX_EXCLUSION, Type.GetType("System.Decimal"))
        dt.Columns().Add(DTCOL_TAX_TAXABLE, Type.GetType("System.Decimal"))
        dt.Columns().Add(DTCOL_TAX_AMOUNT, Type.GetType("System.Decimal"))

        Dim dr As DataRow = dt.NewRow()
        dr.Item(DTCOL_NO) = 1
        dr.Item(DTCOL_ITEMCD) = String.Empty
        dr.Item(DTCOL_ITEMNM) = String.Empty
        dr.Item(DTCOL_ITEMNM) = String.Empty
        dr.Item(DTCOL_ZEIKBN) = String.Empty
        dr.Item(DTCOL_IRISUU) = DBNull.Value
        dr.Item(DTCOL_KOSUU) = 0
        dr.Item(DTCOL_SURYOU) = DBNull.Value
        dr.Item(DTCOL_TANNI) = String.Empty
        dr.Item(DTCOL_SHIIRETANKA) = DBNull.Value
        dr.Item(DTCOL_SHIIREKINGAKU) = 0
        dr.Item(DTCOL_MEISAIBIKOU) = String.Empty
        dr.Item(DTCOL_ZEIKBNCD) = String.Empty
        dr.Item(DTCOL_TAX_RATE) = 0
        dr.Item(DTCOL_TAX_EXCLUSION) = 0
        dr.Item(DTCOL_TAX_TAXABLE) = 0
        dr.Item(DTCOL_TAX_AMOUNT) = 0

        dt.Rows.Add(dr)

        Dim ds As DataSet = New DataSet()
        ds.Tables.Add(dt)

        dgvIchiran.DataSource = ds
        dgvIchiran.DataMember = RS
        lblMeisaiCnt.Text = dgvIchiran.RowCount

    End Sub

    '初期処理
    '
    Private Sub initProcess()

        'モード設定
        Select Case _SelectID
            Case CommonConst.MENU_H0610   '新規
                _ShoriMode = CommonConst.MODE_ADDNEW

            Case CommonConst.MENU_H0620   '変更
                _ShoriMode = CommonConst.MODE_EditStatus

            Case CommonConst.MENU_H0630   '取消
                _ShoriMode = CommonConst.MODE_CancelStatus

            Case CommonConst.MENU_H0640   '参照
                _ShoriMode = CommonConst.MODE_InquiryStatus

            Case Else                     '上記以外
                _ShoriMode = CommonConst.MODE_InquiryStatus
        End Select

        '各コントロール制御
        Select Case _ShoriMode
            Case CommonConst.MODE_ADDNEW  '登録
                lblShoriMode.Text = "登録"
                cmdFukusya.Enabled = True
                cmdTouroku.Enabled = True
                cmdModoru.Enabled = True
                _SyoriName = CommonConst.MODE_ADDNEW_NAME
                'コントロールの入力制御
                InputControl(True)

            Case CommonConst.MODE_EditStatus  '変更
                lblShoriMode.Text = "変更"
                cmdFukusya.Enabled = False
                cmdTouroku.Enabled = True
                cmdModoru.Enabled = True
                _SyoriName = CommonConst.MODE_EditStatus_NAME
                'コントロールの入力制御
                InputControl(True)

            Case CommonConst.MODE_CancelStatus  '取消
                lblShoriMode.Text = "取消"
                cmdFukusya.Enabled = False
                cmdTouroku.Enabled = True
                cmdModoru.Enabled = True
                _SyoriName = CommonConst.MODE_CancelStatus_NAME
                'コントロールの入力制御
                InputControl(False)

            Case CommonConst.MODE_InquiryStatus  '照会
                lblShoriMode.Text = "照会"
                cmdFukusya.Enabled = False
                cmdTouroku.Enabled = False
                cmdModoru.Enabled = True
                _SyoriName = CommonConst.MODE_InquiryStatus_NAME
                'コントロールの入力制御
                InputControl(False)

        End Select

        '画面表示処理
        If _ShoriMode = CommonConst.MODE_ADDNEW Then
            '新規

            '初期表示
            initDisp()

        Else
            '新規以外

            '仕入データ表示
            dispShiire(Me.lblDenpyoNo.Text)
        End If

    End Sub

    'コントロールの入力制御
    '
    '引数 prmEnabled
    '       true:入力可 false:入力不可
    Private Sub InputControl(ByVal prmEnabled As Boolean)

        '入力項目
        Me.dtpShiireDt.Enabled = prmEnabled
        Me.txtShiiresakiCd.Enabled = prmEnabled
        Me.txtBikou.Enabled = prmEnabled

        '一覧
        Me.dgvIchiran.ReadOnly = Not prmEnabled

        'ボタン
        Me.cmdTopRow.Enabled = prmEnabled
        Me.cmdAddRow.Enabled = prmEnabled
        Me.cmdDelRow.Enabled = prmEnabled
        Me.cmdRowCopy.Enabled = prmEnabled
        Me.cmdSwapRowUp.Enabled = prmEnabled
        Me.cmdSwapRowDown.Enabled = prmEnabled

    End Sub

    '初期表示
    '
    Private Sub initDisp()

        '仕入伝番取得
        Dim shiireNo As String = _comLogc.GetDenpyoNo(frmC01F10_Login.loginValue.BumonCD, CommonConst.SAIBAN_SHIIRE)

        '仕入伝番表示
        Me.lblDenpyoNo.Text = shiireNo

        '仕入日（システム日付）
        dtpShiireDt.Text = DateAdd("m", 0, Now).ToString("yyyy/MM/dd")

        '仕入日の値変更時処理
        shiireDtChanged()

        '一覧初期化処理
        initDgvIchiran()

    End Sub

    '仕入データ表示
    '
    '引数 shiireNo      仕入伝番
    Private Sub dispShiire(ByVal shiireNo As String)

        '仕入データ取得
        Dim dsShiire As DataSet = getShiireData(shiireNo)

        '仕入基本データ表示
        dispShiireHd(dsShiire)

        '仕入明細データ表示
        dispShiireDt(dsShiire)

    End Sub

    '仕入データ取得
    '
    '引数 shiireNo      仕入伝番
    Private Function getShiireData(ByVal shiireNo As String) As DataSet

        Dim sql As String = String.Empty
        sql &= N & " SELECT "
        sql &= N & "        SRH.会社コード "
        sql &= N & "       ,SRH.仕入伝番 "
        '複写新規の場合
        If _ShoriMode = CommonConst.MODE_ADDNEW Then
            sql &= N & "   ,CURRENT_DATE            AS 仕入日 "
        Else
            sql &= N & "   ,SRH.仕入日 "
        End If
        sql &= N & "       ,SRH.仕入先コード "
        sql &= N & "       ,SRH.仕入先名 "
        sql &= N & "       ,SRH.支払先コード "
        sql &= N & "       ,SRH.支払先名 "
        sql &= N & "       ,SRH.コメント "
        sql &= N & "       ,SRH.仕入金額計 "
        sql &= N & "       ,SRH.税抜額計 "
        sql &= N & "       ,SRH.課税対象額計 "
        sql &= N & "       ,SRH.消費税計 "
        sql &= N & "       ,SRH.税込額計 "
        sql &= N & "       ,SRD.商品コード "
        sql &= N & "       ,SRD.商品名 "
        sql &= N & "       ,SRD.商品詳細 "
        sql &= N & "       ,SRD.課税区分 "
        sql &= N & "       ,HAN.文字２              AS 課税区分名 "
        sql &= N & "       ,SRD.入数 "
        sql &= N & "       ,SRD.個数 "
        sql &= N & "       ,SRD.仕入数量 "
        sql &= N & "       ,SRD.単位 "
        sql &= N & "       ,SRD.仕入単価 "
        sql &= N & "       ,SRD.仕入金額 "
        sql &= N & "       ,SRD.仕入明細備考 "
        sql &= N & "       ,SRD.税抜額 "
        sql &= N & "       ,SRD.課税対象額 "
        sql &= N & "       ,SRD.消費税 "
        sql &= N & "       ,SRD.税込額 "
        sql &= N & "       ,CST.金額端数区分 "
        sql &= N & "       ,SRH.税計算区分 "
        sql &= N & "       ,CST.税端数区分 "
        sql &= N & "       ,SRH.税率 "
        sql &= N & "       ,SRH.更新日 "

        sql &= N & " FROM   T40_SIREHD              SRH "                                                  '仕入基本
        sql &= N & " INNER  JOIN T41_SIREDT         SRD "                                                  '仕入明細
        sql &= N & "    ON  SRD.会社コード        = SRH.会社コード "
        sql &= N & "   AND  SRD.仕入伝番          = SRH.仕入伝番 "
        sql &= N & " INNER  JOIN M10_CUSTOMER       CST "                                                  '取引先マスタ
        sql &= N & "    ON  CST.会社コード        = SRH.会社コード "
        sql &= N & "   AND  CST.取引先コード      = SRH.仕入先コード "
        sql &= N & " LEFT   JOIN M90_HANYO          HAN "                                                  '汎用マスタ
        sql &= N & "    ON  HAN.会社コード        = SRD.会社コード "
        sql &= N & "   AND  HAN.固定キー          = '" & _db.rmSQ(CommonConst.HANYO_KAZEI_KBN) & "' "
        sql &= N & "   AND  HAN.可変キー          = SRD.課税区分 "

        sql &= N & " WHERE  SRH.会社コード        = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "
        sql &= N & "   AND  SRH.仕入伝番          = '" & _db.rmSQ(shiireNo) & "' "

        sql &= N & " ORDER BY "
        sql &= N & "        SRD.行番 "

        Dim reccnt As Integer = 0
        Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)

        Return ds

    End Function

    '仕入基本データ表示
    '
    '引数 dsShiire    仕入データ
    Private Sub dispShiireHd(ByVal dsShiire As DataSet)

        'データがない場合
        If dsShiire.Tables(RS).Rows.Count = 0 Then
            Exit Sub
        End If

        '仕入基本データ
        Dim dataHd As DataRow = dsShiire.Tables(RS).Rows(0)

        '仕入日
        Me.dtpShiireDt.Text = _db.rmNullDate(dataHd("仕入日"), "yyyy/MM/dd")
        Me.lblShiireDay.Text = _db.rmNullDate(dataHd("仕入日"), "ddd")

        '仕入先コード、名称
        Me.txtShiiresakiCd.Text = _db.rmNullStr(dataHd("仕入先コード"))
        Me.lblShiiresakiName.Text = _db.rmNullStr(dataHd("仕入先名"))

        '支払先コード、名称
        Me.lblShiharaisakiCd.Text = _db.rmNullStr(dataHd("支払先コード"))
        Me.lblShiharaisakiName.Text = _db.rmNullStr(dataHd("支払先名"))

        '備考
        Me.txtBikou.Text = _db.rmNullStr(dataHd("コメント"))

        '税抜額
        Me.lblZeinukiSum.Text = _db.rmNullDouble(dataHd("税抜額計"))

        '課税対象額
        Me.lblKazeiSum.Text = _db.rmNullDouble(dataHd("課税対象額計"))

        '消費税額
        Me.lblTaxSum.Text = _db.rmNullDouble(dataHd("消費税計"))

        '税込額
        Me.lblMoneySum.Text = _db.rmNullDouble(dataHd("税込額計"))

        '合計金額
        Me.lblTotal.Text = _db.rmNullDouble(dataHd("仕入金額計"))

        '金額端数区分
        _KinHasu = _db.rmNullStr(dataHd("金額端数区分"))

        '税算出区分
        _ZeiSanshutsu = _db.rmNullStr(dataHd("税計算区分"))

        '税端数区分
        _ZeiHasu = _db.rmNullStr(dataHd("税端数区分"))

        '税率
        _ZeiRitsu = _db.rmNullDouble(dataHd("税率"))

        '更新日
        _UpdateTime = _db.rmNullDate(dataHd("更新日"))

    End Sub

    '仕入明細データ表示
    '
    '引数 dsShiire    仕入データ
    Private Sub dispShiireDt(ByVal dsShiire As DataSet)

        '一覧初期化処理
        initDgvIchiran()

        'データがない場合
        If dsShiire.Tables(RS).Rows.Count = 0 Then
            Exit Sub
        End If

        '描画の前にすべてクリアする
        Dim dss As DataSet = TryCast(Me.dgvIchiran.DataSource, DataSet)

        For index As Integer = 0 To dsShiire.Tables(RS).Rows.Count - 1

            If index <> 0 Then
                dss.Tables(RS).Rows.InsertAt(dss.Tables(RS).NewRow, index + 1)
            End If

            '一覧セル
            Dim cells As DataGridViewCellCollection = dgvIchiran.Rows(index).Cells

            '明細データ
            Dim data As DataRow = dsShiire.Tables(RS).Rows(index)

            cells(COLNO_NO).Value = (index + 1).ToString                             '行番号
            cells(COLNO_ITEMCD).Value = _db.rmNullStr(data("商品コード"))
            cells(COLNO_ITEMNM).Value = _db.rmNullStr(data("商品名"))
            cells(COLNO_ITEMDT).Value = _db.rmNullStr(data("商品詳細"))
            cells(COLNO_ZEIKBNCD).Value = _db.rmNullStr(data("課税区分"))
            cells(COLNO_ZEIKBN).Value = _db.rmNullStr(data("課税区分名"))
            cells(COLNO_IRISUU).Value = _db.rmNullDouble(data("入数"))
            cells(COLNO_KOSUU).Value = _db.rmNullDouble(data("個数"))
            cells(COLNO_SURYOU).Value = _db.rmNullDouble(data("仕入数量"))
            cells(COLNO_TANNI).Value = _db.rmNullStr(data("単位"))
            cells(COLNO_SHIIRETANKA).Value = _db.rmNullDouble(data("仕入単価"))
            cells(COLNO_SHIIREKINGAKU).Value = _db.rmNullDouble(data("仕入金額"))
            cells(COLNO_MEISAIBIKOU).Value = _db.rmNullStr(data("仕入明細備考"))
            cells(COLNO_TAX_RATE).Value = _db.rmNullStr(data("税率"))
            cells(COLNO_TAX_EXCLUSION).Value = _db.rmNullStr(data("税抜額"))
            cells(COLNO_TAX_TAXABLE).Value = _db.rmNullStr(data("課税対象額"))
            cells(COLNO_TAX_AMOUNT).Value = _db.rmNullStr(data("消費税"))

        Next

        '明細件数をセット
        lblMeisaiCnt.Text = dgvIchiran.RowCount

    End Sub

    '複写元選択ボタン押下時処理
    Private Sub fukusyaClick()

        '仕入一覧フォームオープン（ダイアログ）
        Dim openForm As frmH06F10_SelectShiire = New frmH06F10_SelectShiire(_msgHd, _db, _SelectID, _SelectID, Me)
        openForm.ShowDialog()

        If openForm.Selected Then
            '選択されました

            '仕入一覧で選択された仕入伝番
            Dim shiireNo As String = openForm.GetValShiireNo()

            '仕入データ表示
            dispShiire(shiireNo)

            '仕入日にシステム日付をセット
            dtpShiireDt.Text = DateAdd("m", 0, Now).ToString("yyyy/MM/dd")

            '仕入日の値変更時処理
            shiireDtChanged()

        End If
        openForm = Nothing

    End Sub

    '仕入日の値変更時処理

    Private Sub shiireDtChanged()

        Dim strShiireDate As String = CDate(dtpShiireDt.Value).ToString("yyyy/MM/dd")

        '曜日再取得
        lblShiireDay.Text = YobiReturn(strShiireDate)

        '税率取得処理
        getZeiRitsu()

        '仕入単価取得(全行)
        getShiireTankaAll()

        '金額再計算(全行)
        RecalcKingakuAll()

        '税関連再計算(全行)
        RecalcZeiAll()

        '各合計額再計算
        RecalcTotal()

    End Sub

    'Stringのyyyy/mm/ddを引数に渡すと曜日を返す
    '
    Private Function YobiReturn(ByRef strDenpyoDate As String)

        Dim dteDenpyo As DateTime
        Dim strWeek1 As String ' 短縮表記の曜日を取得します（例：日）

        If DateTime.TryParse(strDenpyoDate, dteDenpyo) Then
            strWeek1 = dteDenpyo.ToString("ddd")
        Else
            strWeek1 = String.Empty
        End If

        Return strWeek1
    End Function

    '税率取得処理
    '
    Private Sub getZeiRitsu()

        Dim sql As String = String.Empty
        sql &= N & " SELECT "
        sql &= N & "        TAX.消費税率 "
        sql &= N & " FROM   M71_CTAX                TAX "                                                  '消費税マスタ
        sql &= N & " WHERE  TAX.会社コード        = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "
        sql &= N & "   AND  TAX.適用開始日       <= '" & CDate(dtpShiireDt.Value).ToString("yyyy/MM/dd") & "' "
        sql &= N & "   AND  TAX.適用終了日       >= '" & CDate(dtpShiireDt.Value).ToString("yyyy/MM/dd") & "' "

        Dim reccnt As Integer = 0
        Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)

        '税率
        _ZeiRitsu = _db.rmNullDouble(ds.Tables(RS).Rows(0)("消費税率"))

    End Sub

    '仕入日コードの値変更時処理
    '
    Private Sub shiiresakiCdValidated()

        '仕入先コードに入力がある場合
        If (Me.txtShiiresakiCd.Text <> String.Empty) Then
            '仕入先情報取得処理
            Call getShiiresaki()

            '仕入先副情報取得処理
            Call getShiiresakiSub()

            '支払先情報取得処理
            Call getShiharaisakiSub()
        Else
            '仕入先名、支払先コード、支払先名、金額端数区分、税算出区分、税端数区分クリア
            Me.lblShiiresakiName.ResetText()
            Me.lblShiharaisakiCd.ResetText()
            Me.lblShiharaisakiName.ResetText()
            _KinHasu = String.Empty
            _ZeiSanshutsu = String.Empty
            _ZeiHasu = String.Empty
        End If

        '仕入単価取得(全行)
        getShiireTankaAll()

        '金額再計算(全行)
        RecalcKingakuAll()

        '税関連再計算(全行)
        RecalcZeiAll()

        '各合計額再計算
        RecalcTotal()

    End Sub

    '仕入先情報取得処理
    '
    Private Sub getShiiresaki()

        '入力仕入先コード
        Dim inputShiiresakiCd As String = Me.txtShiiresakiCd.Text

        '取引先マスタから仕入先データを取得
        Dim ds As DataSet = _comLogc.getTorihikisaki(frmC01F10_Login.loginValue.BumonCD,
                                                     CommonConst.TORIHIKISAKI_TARGET_KBN_SHIIRE,
                                                     inputShiiresakiCd,
                                                     String.Empty,
                                                     True)

        'データカウント
        Dim dataCount As Integer = ds.Tables(RS).Rows.Count

        If dataCount = 0 Then
            'データ0件

            '仕入先名、支払先コード、支払先名、金額端数区分、税算出区分、税端数区分クリア
            Me.lblShiiresakiName.ResetText()
            Me.lblShiharaisakiCd.ResetText()
            Me.lblShiharaisakiName.ResetText()
            _KinHasu = String.Empty
            _ZeiSanshutsu = String.Empty
            _ZeiHasu = String.Empty

            Exit Sub
        End If

        '取得データ
        Dim dataRow As DataRow = ds.Tables(RS).Rows(0)

        '仕入先名に取得データをセット
        Me.lblShiiresakiName.Text = _db.rmNullStr(dataRow("取引先名"))

    End Sub

    '仕入先副情報取得処理
    '
    Private Sub getShiiresakiSub()

        '仕入先名に値がない（＝仕入先コードにマスタに存在するコードが入力されていない）場合、処理終了
        If lblShiiresakiName.Text = String.Empty Then
            Exit Sub
        End If

        '入力仕入先コード
        Dim inputShiiresakiCd As String = Me.txtShiiresakiCd.Text

        '取引先マスタから仕入先データを取得
        Dim sql As String = ""
        sql &= N & " SELECT "
        sql &= N & "        CST.支払先コード "
        sql &= N & "       ,CST.金額端数区分 "
        sql &= N & "       ,CST.税算出区分 "
        sql &= N & "       ,CST.税端数区分 "
        sql &= N & " FROM   M10_CUSTOMER                CST "                                                         '取引先マスタ
        sql &= N & " WHERE  CST.会社コード            = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "
        sql &= N & "   AND  CST.取引先コード          = '" & _db.rmSQ(inputShiiresakiCd) & "' "

        Dim reccnt As Integer = 0

        Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)

        '取得データ
        Dim dataRow As DataRow = ds.Tables(RS).Rows(0)

        '支払先コード、税算出区分、税端数区分に取得データをセット
        Me.lblShiharaisakiCd.Text = _db.rmNullStr(dataRow("支払先コード"))
        _KinHasu = _db.rmNullStr(dataRow("金額端数区分"))
        _ZeiSanshutsu = _db.rmNullStr(dataRow("税算出区分"))
        _ZeiHasu = _db.rmNullStr(dataRow("税端数区分"))

    End Sub

    '支払先情報取得処理
    Private Sub getShiharaisakiSub()

        '仕入先名に値がない（＝仕入先コードにマスタに存在するコードが入力されていない）場合、処理終了
        If lblShiiresakiName.Text = String.Empty Then
            Exit Sub
        End If

        '支払先コード
        Dim shiharaisakiCd As String = Me.lblShiharaisakiCd.Text

        '取引先マスタから支払先データを取得
        Dim ds As DataSet = _comLogc.getTorihikisaki(frmC01F10_Login.loginValue.BumonCD,
                                                     CommonConst.TORIHIKISAKI_TARGET_KBN_SHIHARAI,
                                                     shiharaisakiCd,
                                                     String.Empty,
                                                     True)

        'データカウント
        Dim dataCount As Integer = ds.Tables(RS).Rows.Count

        If dataCount = 0 Then
            'データ0件

            '支払先名クリア
            Me.lblShiharaisakiName.ResetText()

            Exit Sub
        End If

        '取得データ
        Dim dataRow As DataRow = ds.Tables(RS).Rows(0)

        '支払先名に取得データをセット
        Me.lblShiharaisakiName.Text = _db.rmNullStr(dataRow("取引先名"))

    End Sub

    '仕入先コードダブルクリック時処理
    Private Sub shiiresakiCdDoubleClick()

        '取引先選択ウインドウオープン処理
        torihikisakiSelectWindowOpen()

    End Sub

    '取引先選択ウインドウオープン処理
    Private Sub torihikisakiSelectWindowOpen()

        Dim openForm As frmC10F20_Torihikisaki = New frmC10F20_Torihikisaki(_msgHd, _db, CommonConst.TORIHIKISAKI_TARGET_KBN_SHIIRE)
        openForm.ShowDialog()                      '画面表示

        '選択されている場合
        If openForm.Selected Then
            '画面に値をセット

            '仕入先コード
            Me.txtShiiresakiCd.Text = openForm.GetValTorihikisakiCd
            '仕入先名
            Me.lblShiiresakiName.Text = openForm.GetValTorihikisakiName

            '仕入先副情報取得処理
            Call getShiiresakiSub()

            '支払先情報取得処理
            Call getShiharaisakiSub()

            '仕入単価取得(全行)
            getShiireTankaAll()

            '金額再計算(全行)
            RecalcKingakuAll()

            '税関連再計算(全行)
            RecalcZeiAll()

            '各合計額再計算
            RecalcTotal()

        End If

        openForm = Nothing

    End Sub

    '検索ウインドウオープン処理
    Private Sub SelectWindowOpen()

        '一覧が非活性の場合は何もしない
        If Me.dgvIchiran.ReadOnly Then
            Exit Sub
        End If

        Dim clickColumnIndex As Integer
        Dim clickRowIndex As Integer

        clickColumnIndex = dgvIchiran.CurrentCell.ColumnIndex
        clickRowIndex = dgvIchiran.CurrentCell.RowIndex

        Select Case clickColumnIndex
            Case COLNO_ITEMCD      '商品ＣＤ
                Dim openForm As frmC10F10_Shohin = New frmC10F10_Shohin(_msgHd, _db, CommonConst.HAN_HANBAISIIRE_KBN_HANBAI)      '画面遷移
                openForm.ShowDialog()                      '画面表示
                If openForm.Selected Then
                    '選択されました
                    dgvIchiran.Rows(clickRowIndex).Cells(COLNO_ITEMCD).Value = openForm.GettShohinCD
                    dgvIchiran.Rows(clickRowIndex).Cells(COLNO_ITEMNM).Value = openForm.GetShohinNM
                    dgvIchiran.Rows(clickRowIndex).Cells(COLNO_ITEMDT).Value = openForm.GetNisugata
                    '商品マスタから初期値を項目編集する
                    DispShohin(clickRowIndex, openForm.GettShohinCD)
                End If
                openForm = Nothing
            Case COLNO_ZEIKBN      '税
                Dim openForm As frmC10F90_Hanyo = New frmC10F90_Hanyo(_msgHd, _db, CommonConst.HANYO_KAZEI_KBN)       '画面遷移
                openForm.ShowDialog()                      '画面表示
                If openForm.Selected Then
                    '選択されました
                    dgvIchiran.Rows(clickRowIndex).Cells(COLNO_ZEIKBN).Value = openForm.GetValNM
                    dgvIchiran.Rows(clickRowIndex).Cells(COLNO_ZEIKBNCD).Value = openForm.GetValCD
                End If
                openForm = Nothing
            Case COLNO_TANNI      '単位
                Dim openForm As frmC10F90_Hanyo = New frmC10F90_Hanyo(_msgHd, _db, CommonConst.HANYO_TANI)            '画面遷移
                openForm.ShowDialog()                      '画面表示
                If openForm.Selected Then
                    '選択されました
                    dgvIchiran.Rows(clickRowIndex).Cells(COLNO_TANNI).Value = openForm.GetValNM
                End If
                openForm = Nothing
        End Select


    End Sub

    '商品マスタから初期値を項目編集する
    '引数、グリッドのRowIndex,商品コード
    '
    Private Sub DispShohin(argIntRowIndex As Integer, argStrShohinCD As String)

        Dim strSql As String = String.Empty

        strSql = "SELECT "
        strSql = strSql & "   s.会社コード, s.商品コード, s.商品名, s.冷凍区分 , s.課税区分,h2.文字２ as 課税区分名, s.入数, s.単位 "
        strSql = strSql & " FROM m20_goods s "
        strSql = strSql & "   left join M90_HANYO h2 on h2.会社コード = s.会社コード and h2.固定キー = '" & CommonConst.HANYO_KAZEI_KBN & "' and h2.可変キー = s.課税区分 "
        strSql = strSql & " Where s.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and s.商品コード = '" & _db.rmSQ(argStrShohinCD) & "'"

        Dim reccnt As Integer = 0
        Dim ds As DataSet = _db.selectDB(strSql, RS, reccnt)

        '一覧セル
        Dim cells As DataGridViewCellCollection = dgvIchiran.Rows(argIntRowIndex).Cells

        cells(COLNO_ZEIKBNCD).Value = _db.rmNullStr(ds.Tables(RS).Rows(0)("課税区分"))
        cells(COLNO_ZEIKBN).Value = _db.rmNullStr(ds.Tables(RS).Rows(0)("課税区分名"))
        cells(COLNO_IRISUU).Value = _db.rmNullDouble(ds.Tables(RS).Rows(0)("入数"))
        cells(COLNO_TANNI).Value = _db.rmNullStr(ds.Tables(RS).Rows(0)("単位"))
        cells(COLNO_TAX_RATE).Value = _ZeiRitsu

        '仕入単価取得(1行)
        getShiireTankaOne(cells, argIntRowIndex)

    End Sub

#Region "金額計算処理"

    '一覧 入力値変更時処理
    Private Sub ichiranValidated(e As DataGridViewCellEventArgs)

        '初期処理前の場合は処理終了
        If Not _init Then
            '処理終了
            Exit Sub
        End If

        '変更行インデックス
        Dim rowIdx As Integer = e.RowIndex

        If rowIdx < 0 Then
            Exit Sub
        End If

        '変更カラムインデックス
        Dim columnIdx As Integer = e.ColumnIndex

        '一覧セル
        Dim cells As DataGridViewCellCollection = dgvIchiran.Rows(e.RowIndex).Cells

        '変更カラムで処理分岐
        Select Case columnIdx

            '商品コード、入数、個数
            Case COLNO_ITEMCD, COLNO_IRISUU, COLNO_KOSUU

                '数量再計算(1行)
                RecalcSuryoOne(cells, rowIdx)

                '金額再計算(1行)
                RecalcKingakuOne(cells, rowIdx)

                '税関連再計算(1行)
                RecalcZeiOne(cells, rowIdx)

                '各合計額再計算
                RecalcTotal()

            '数量、仕入単価
            Case COLNO_SURYOU, COLNO_SHIIRETANKA

                '金額再計算(1行)
                RecalcKingakuOne(cells, rowIdx)

                '税関連再計算(1行)
                RecalcZeiOne(cells, rowIdx)

                '各合計額再計算
                RecalcTotal()

            '税区分、仕入金額
            Case COLNO_ZEIKBNCD, COLNO_SHIIREKINGAKU

                '税関連再計算(1行)
                RecalcZeiOne(cells, rowIdx)

                '各合計額再計算
                RecalcTotal()

        End Select

    End Sub

    '数量再計算(1行)
    Private Sub RecalcSuryoOne(cells As DataGridViewCellCollection, rowIdx As Integer)

        '入数チェック
        Dim decIrisuu As Decimal = 0
        If Decimal.TryParse(_gh.getCellData(COLNO_IRISUU, rowIdx), decIrisuu) Then
        Else
            cells(COLNO_IRISUU).Value = 0
        End If

        '個数チェック
        Dim decKosuu As Integer = 0
        If Integer.TryParse(_gh.getCellData(COLNO_KOSUU, rowIdx), decKosuu) Then
        Else
            cells(COLNO_KOSUU).Value = 0
        End If

        '数量の算出
        Dim decSuryo As Decimal = decIrisuu * decKosuu

        '数量に値をセット
        cells(COLNO_SURYOU).Value = decSuryo

    End Sub

    '仕入単価取得(1行)
    Private Sub getShiireTankaOne(cells As DataGridViewCellCollection, rowIdx As Integer)

        '仕入先名に値がない（＝仕入先コードにマスタに存在するコードが入力されていない）場合、または、
        '商品コードに値無し
        If (lblShiiresakiName.Text = String.Empty) OrElse
           (_gh.getCellData(COLNO_ITEMCD, rowIdx) = String.Empty) Then
            '仕入単価にゼロをセット
            cells(COLNO_SHIIRETANKA).Value = 0
            '処理終了
            Exit Sub
        End If

        Dim decShiireTanka As Decimal                  '仕入単価
        Dim strTankaPth As String = String.Empty       '単価パターン

        '仕入単価を取得
        _comLogc.GetShiireTanka(frmC01F10_Login.loginValue.BumonCD,
                                cells(COLNO_ITEMCD).Value,
                                txtShiiresakiCd.Text,
                                CDate(dtpShiireDt.Value).ToString("yyyy/MM/dd"),
                                decShiireTanka,
                                strTankaPth)

        '仕入単価にセット
        cells(COLNO_SHIIRETANKA).Value = decShiireTanka

    End Sub

    '仕入単価取得(全行)
    Private Sub getShiireTankaAll()

        '一覧をループ
        For rowIdx As Integer = 0 To dgvIchiran.RowCount - 1

            '一覧セル
            Dim cells As DataGridViewCellCollection = dgvIchiran.Rows(rowIdx).Cells

            '金額再計算(1行)
            getShiireTankaOne(cells, rowIdx)

        Next rowIdx

    End Sub

    '金額再計算(1行)
    Private Sub RecalcKingakuOne(cells As DataGridViewCellCollection, rowIdx As Integer)

        '仕入先名に値がない（＝仕入先コードにマスタに存在するコードが入力されていない）場合
        If lblShiiresakiName.Text = String.Empty Then
            '金額にゼロをセット
            cells(COLNO_SHIIREKINGAKU).Value = 0
            '処理終了
            Exit Sub
        End If

        '数量チェック
        Dim decSuryo As Decimal = 0
        If Decimal.TryParse(_gh.getCellData(COLNO_SURYOU, rowIdx), decSuryo) Then
        Else
            cells(COLNO_SURYOU).Value = 0
        End If

        '単価チェック
        Dim decTanka As Decimal = 0
        If Decimal.TryParse(_gh.getCellData(COLNO_SHIIRETANKA, rowIdx), decTanka) Then
        Else
            cells(COLNO_SHIIRETANKA).Value = 0
        End If

        '金額の算出
        Dim decKingaku As Decimal = decSuryo * decTanka

        '金額端数処理
        Select Case _KinHasu
            Case CommonConst.HASUKBN_ROUNDDOWN      '切り捨て
                decKingaku = Math.Floor(decKingaku)
            Case CommonConst.HASUKBN_ROUNDOFF       '四捨五入
                decKingaku = Math.Round(decKingaku, MidpointRounding.AwayFromZero)
            Case CommonConst.HASUKBN_ROUNDUP        '切り上げ
                decKingaku = Math.Ceiling(decKingaku)
        End Select

        '金額に値をセット
        cells(COLNO_SHIIREKINGAKU).Value = decKingaku

    End Sub

    '金額再計算(全行)
    Private Sub RecalcKingakuAll()

        '一覧をループ
        For rowIdx As Integer = 0 To dgvIchiran.RowCount - 1

            '一覧セル
            Dim cells As DataGridViewCellCollection = dgvIchiran.Rows(rowIdx).Cells

            '金額再計算(1行)
            RecalcKingakuOne(cells, rowIdx)

        Next rowIdx

    End Sub

    '税関連再計算(1行)
    Private Sub RecalcZeiOne(cells As DataGridViewCellCollection, rowIdx As Integer)

        '仕入先名に値がない（＝仕入先コードにマスタに存在するコードが入力されていない）場合
        If lblShiiresakiName.Text = String.Empty Then
            '処理終了
            Exit Sub
        End If

        '税区分に設定なし
        Dim zeiKbnCd As String = _gh.getCellData(COLNO_ZEIKBNCD, rowIdx)
        If zeiKbnCd = String.Empty Then
            '処理終了
            Exit Sub
        End If

        '金額チェック
        Dim decKingaku As Decimal = 0
        If Decimal.TryParse(_gh.getCellData(COLNO_SHIIREKINGAKU, rowIdx), decKingaku) Then
        Else
            cells(COLNO_SHIIREKINGAKU).Value = 0
        End If

        '------------------------------------------------------------------------------------------
        '①税抜額の算出
        Dim decExclusion As Decimal = 0

        '税区分で分岐
        Select Case zeiKbnCd
            '1:外税, 3:非課税
            Case CommonConst.TAXKBN_External, CommonConst.TAXKBN_Exempt
                '金額をセット
                decExclusion = decKingaku

            '2:内税
            Case CommonConst.TAXKBN_Included
                '「金額」÷（100+（「税率」×100））×100 をセット
                decExclusion = decKingaku / (100 + (_ZeiRitsu * 100)) * 100
        End Select

        '端数処理
        Select Case _ZeiHasu
            Case CommonConst.HASUKBN_ROUNDDOWN      '切り捨て
                decExclusion = Math.Floor(decExclusion)
            Case CommonConst.HASUKBN_ROUNDOFF       '四捨五入
                decExclusion = Math.Round(decExclusion, MidpointRounding.AwayFromZero)
            Case CommonConst.HASUKBN_ROUNDUP        '切り上げ
                decExclusion = Math.Ceiling(decExclusion)
        End Select

        '税抜額に値をセット
        cells(COLNO_TAX_EXCLUSION).Value = decExclusion

        '------------------------------------------------------------------------------------------
        '②課税対象額の算出
        Dim decTaxable As Decimal = 0

        '税区分で分岐
        Select Case zeiKbnCd
            '1:外税
            Case CommonConst.TAXKBN_External
                '金額をセット
                decTaxable = decKingaku

            '2:内税
            Case CommonConst.TAXKBN_Included
                '税抜額をセット
                decTaxable = decExclusion

            '3:非課税
            Case CommonConst.TAXKBN_Exempt
                'ゼロをセット
                decTaxable = 0
        End Select

        '課税対象額に値をセット
        cells(COLNO_TAX_TAXABLE).Value = decTaxable

        '------------------------------------------------------------------------------------------
        '③消費税額の算出
        Dim decTaxAmount As Decimal = 0

        '税算出区分で分岐
        Select Case _ZeiSanshutsu

            '1:伝票単位
            Case CommonConst.TAXSANSHUTSUKBN_DENPYO
                'ゼロをセット
                decTaxAmount = 0

            '2:明細単位
            Case CommonConst.TAXSANSHUTSUKBN_MEISAI
                '税区分で分岐
                Select Case zeiKbnCd
                    '1:外税
                    Case CommonConst.TAXKBN_External
                        '課税対象額 × 税率 をセット
                        decTaxAmount = decTaxable * _ZeiRitsu

                    '2:内税
                    Case CommonConst.TAXKBN_Included
                        '金額 - 税抜額をセット
                        decTaxAmount = decKingaku - decExclusion

                    '3:非課税
                    Case CommonConst.TAXKBN_Exempt
                        'ゼロをセット
                        decTaxAmount = 0
                End Select
        End Select

        '端数処理
        Select Case _ZeiHasu
            Case CommonConst.HASUKBN_ROUNDDOWN      '切り捨て
                decTaxAmount = Math.Floor(decTaxAmount)
            Case CommonConst.HASUKBN_ROUNDOFF       '四捨五入
                decTaxAmount = Math.Round(decTaxAmount, MidpointRounding.AwayFromZero)
            Case CommonConst.HASUKBN_ROUNDUP        '切り上げ
                decTaxAmount = Math.Ceiling(decTaxAmount)
        End Select

        '消費税額に値をセット
        cells(COLNO_TAX_AMOUNT).Value = decTaxAmount

    End Sub

    '税関連再計算(全行)
    Private Sub RecalcZeiAll()

        '一覧をループ
        For rowIdx As Integer = 0 To dgvIchiran.RowCount - 1

            '一覧セル
            Dim cells As DataGridViewCellCollection = dgvIchiran.Rows(rowIdx).Cells

            '税関連再計算(1行)
            RecalcZeiOne(cells, rowIdx)

        Next rowIdx

    End Sub

    '各合計額再計算
    Private Sub RecalcTotal()

        '仕入先名に値がない（＝仕入先コードにマスタに存在するコードが入力されていない）場合
        If lblShiiresakiName.Text = String.Empty Then
            '処理終了
            Exit Sub
        End If

        Dim decExclusionSum As Decimal = 0     '税抜額計
        Dim decTaxableSum As Decimal = 0       '課税対象額計
        Dim decTaxAmountSum As Decimal = 0     '消費税額計
        Dim decMoneySum As Decimal = 0         '税込額計
        Dim decTotal As Decimal = 0            '合計金額

        '一覧をループ
        For i As Integer = 0 To dgvIchiran.RowCount - 1

            '一覧セル
            Dim cells As DataGridViewCellCollection = dgvIchiran.Rows(i).Cells

            '税抜額計
            decExclusionSum = decExclusionSum + _db.rmNullInt(cells(COLNO_TAX_EXCLUSION).Value)
            '課税対象額計
            decTaxableSum = decTaxableSum + _db.rmNullInt(cells(COLNO_TAX_TAXABLE).Value)
            '消費税額計
            decTaxAmountSum = decTaxAmountSum + _db.rmNullInt(cells(COLNO_TAX_AMOUNT).Value)
            '合計金額
            decTotal = decTotal + _db.rmNullInt(cells(COLNO_SHIIREKINGAKU).Value)
        Next i

        '消費税額計は税算出区分が伝票単位の場合、 課税対象額計 × 税率
        If _ZeiSanshutsu = CommonConst.TAXSANSHUTSUKBN_DENPYO Then

            decTaxAmountSum = decTaxableSum * _ZeiRitsu

            '端数処理
            Select Case _ZeiHasu
                Case CommonConst.HASUKBN_ROUNDDOWN      '切り捨て
                    decTaxAmountSum = Math.Floor(decTaxAmountSum)
                Case CommonConst.HASUKBN_ROUNDOFF       '四捨五入
                    decTaxAmountSum = Math.Round(decTaxAmountSum, MidpointRounding.AwayFromZero)
                Case CommonConst.HASUKBN_ROUNDUP        '切り上げ
                    decTaxAmountSum = Math.Ceiling(decTaxAmountSum)
            End Select
        End If

        '税込額計に 税抜額計 + 消費税額計 をセット
        decMoneySum = decExclusionSum + decTaxAmountSum

        '税抜額計
        lblZeinukiSum.Text = decExclusionSum.ToString("N0")
        '課税対象額計
        lblKazeiSum.Text = decTaxableSum.ToString("N0")
        '消費税額計
        lblTaxSum.Text = decTaxAmountSum.ToString("N0")
        '税込額計
        lblMoneySum.Text = decMoneySum.ToString("N0")
        '合計金額
        lblTotal.Text = decTotal.ToString("N0")

    End Sub

#End Region

#Region "登録処理"

    '登録処理
    '
    Private Sub touroku()

        Dim piRtn As Integer

        '確認メッセージを表示する
        piRtn = _msgHd.dspMSG("confirmInsert")  '登録します。よろしいですか？
        If piRtn = vbNo Then
            Exit Sub
        End If

        '排他チェック処理
        Select Case _ShoriMode     '（1:登録、2:変更、3:取消、4:照会)
            Case CommonConst.MODE_ADDNEW   '登録
                '新規登録の場合は処理不要。

            Case CommonConst.MODE_EditStatus, CommonConst.MODE_CancelStatus   '変更,取消

                Dim sql As String = String.Empty
                sql &= N & " SELECT "
                sql &= N & "        SRH.更新日 "
                sql &= N & "       ,SRH.更新者 "
                sql &= N & " FROM   T40_SIREHD              SRH "
                sql &= N & " WHERE  SRH.会社コード        = '" & frmC01F10_Login.loginValue.BumonCD & "' "
                sql &= N & "   AND  SRH.仕入伝番          = '" & _db.rmSQ(lblDenpyoNo.Text) & "' "

                Dim reccnt As Integer = 0
                Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)
                If reccnt = 0 Then
                    Exit Sub
                End If

                '他で更新がある場合
                If _UpdateTime <> DateTime.Parse(ds.Tables(RS).Rows(0)("更新日")) Then
                    '他端末更新ありメッセージ
                    Dim strMessage As String = String.Empty
                    strMessage = "更新者：" & ds.Tables(RS).Rows(0)("更新者") & " 更新日時：" & ds.Tables(RS).Rows(0)("更新日").ToString
                    _msgHd.dspMSG("Exclusion", strMessage)
                    Exit Sub
                End If
                '変更・取消の場合のみ処理を行う
        End Select

        'DB更新処理
        Select Case _ShoriMode     '（1:登録、2:変更、3:取消、4:照会)
            Case CommonConst.MODE_ADDNEW          '登録
                DataAddNew()
            Case CommonConst.MODE_EditStatus      '変更
                DataUpdate()
            Case CommonConst.MODE_CancelStatus    '取消
                DataCancel()
            Case CommonConst.MODE_InquiryStatus   '照会
                '照会はここに来ません・・・
        End Select

        'ログ出力
        '操作履歴ログ作成（DB更新）
        _comLogc.Insert_L01_ProcLog(frmC01F10_Login.loginValue.BumonCD, DBNull.Value, _SelectID, CommonConst.PROGRAM_REPORT, CommonConst.STATUS_NORMAL,
                                    Me.lblDenpyoNo.Text, _SyoriName, DBNull.Value, DBNull.Value, DBNull.Value,
                                    DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _userId)

        '登録終了メッセージ
        _msgHd.dspMSG("UpdateShiireInfo", "仕入伝番：" & lblDenpyoNo.Text)

        _parentForm.Show()                                              ' 前画面を表示
        _parentForm.Enabled = True                                      ' 前画面の使用を可能にする
        _parentForm.Activate()                                          ' 前画面をアクティブにする

        Me.Dispose()                                                    ' 自画面を閉じる

    End Sub

    'データ新規登録処理
    Private Sub DataAddNew()

        Try
            'トランザクション開始
            If Not _db.isTransactionOpen Then
                _db.beginTran()
            End If

            '仕入基本 データ追加
            Call DataInsertShiireHd()

            '仕入明細 データ追加
            Call DataInsertShiireDt()

            'トランザクション終了
            _db.commitTran()

        Catch ex As Exception
            Throw ex
        Finally
            If _db.isTransactionOpen Then
                'ロールバック
                _db.rollbackTran()
            End If
        End Try

    End Sub

    'データ更新処理
    Private Sub DataUpdate()

        Try
            'トランザクション開始
            If Not _db.isTransactionOpen Then
                _db.beginTran()
            End If

            '仕入基本 データ削除
            Call DataDeleteShiireHd()

            '仕入明細 データ削除
            Call DataDeleteShiireDt()

            '仕入基本 データ追加
            Call DataInsertShiireHd()

            '仕入明細 データ追加
            Call DataInsertShiireDt()

            'トランザクション終了
            _db.commitTran()

        Catch ex As Exception
            Throw ex
        Finally
            If _db.isTransactionOpen Then
                'ロールバック
                _db.rollbackTran()
            End If
        End Try

    End Sub

    'データ取消処理
    Private Sub DataCancel()

        Try
            'トランザクション開始
            If Not _db.isTransactionOpen Then
                _db.beginTran()
            End If

            '仕入基本 データ更新（取消処理）
            Call DataUpdateShiireHd()

            'トランザクション終了
            _db.commitTran()

        Catch ex As Exception
            Throw ex
        Finally
            If _db.isTransactionOpen Then
                'ロールバック
                _db.rollbackTran()
            End If
        End Try

    End Sub

    '仕入基本（T40_SIREHD） データ追加
    Private Sub DataInsertShiireHd()

        Dim sql As String = String.Empty
        sql &= N & "INSERT INTO T40_SIREHD ( "
        sql &= N & "    会社コード "
        sql &= N & "  , 仕入伝番 "
        sql &= N & "  , 仕入区分 "
        sql &= N & "  , 仕入入力日 "
        sql &= N & "  , 仕入日 "
        sql &= N & "  , 仕入先コード "
        sql &= N & "  , 仕入先名 "
        sql &= N & "  , 支払先コード "
        sql &= N & "  , 支払先名 "
        sql &= N & "  , コメント "
        sql &= N & "  , 仕入金額計 "
        sql &= N & "  , 税抜額計 "
        sql &= N & "  , 課税対象額計 "
        sql &= N & "  , 消費税計 "
        sql &= N & "  , 税込額計 "
        sql &= N & "  , 税率 "
        sql &= N & "  , 税計算区分 "
        sql &= N & "  , 取消区分 "
        sql &= N & "  , 更新者 "
        sql &= N & "  , 更新日 "
        sql &= N & ") VALUES ( "
        sql &= N & "    '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "                        '会社コード
        sql &= N & "  , '" & _db.rmSQ(Me.lblDenpyoNo.Text) & "' "                                       '仕入伝番
        sql &= N & "  , '" & _db.rmSQ(CommonConst.SHIIRE_KBN_NM_SHIIRE) & "' "                          '仕入区分
        sql &= N & "  , CURRENT_DATE "                                                                  '仕入入力日(システム日付)
        sql &= N & "  , '" & _db.rmSQ(Me.dtpShiireDt.Text) & "' "                                       '仕入日
        sql &= N & "  , '" & _db.rmSQ(Me.txtShiiresakiCd.Text) & "' "                                   '仕入先コード
        sql &= N & "  , '" & _db.rmSQ(Me.lblShiiresakiName.Text) & "' "                                 '仕入先名
        sql &= N & "  , '" & _db.rmSQ(Me.lblShiharaisakiCd.Text) & "' "                                 '支払先コード
        sql &= N & "  , '" & _db.rmSQ(Me.lblShiharaisakiName.Text) & "' "                               '支払先名
        sql &= N & "  , '" & _db.rmSQ(Me.txtBikou.Text) & "' "                                          'コメント
        sql &= N & "  , " & Decimal.Parse(Me.lblTotal.Text).ToString                                    '仕入金額計
        sql &= N & "  , " & Decimal.Parse(Me.lblZeinukiSum.Text).ToString                               '税抜額計
        sql &= N & "  , " & Decimal.Parse(Me.lblKazeiSum.Text).ToString                                 '課税対象額計
        sql &= N & "  , " & Decimal.Parse(Me.lblTaxSum.Text).ToString                                   '消費税計
        sql &= N & "  , " & Decimal.Parse(Me.lblMoneySum.Text).ToString                                 '税込額計
        sql &= N & "  , " & Me._ZeiRitsu.ToString                                                       '税率
        sql &= N & "  , '" & _db.rmSQ(Me._ZeiSanshutsu) & "' "                                          '税計算区分(税算出区分)
        sql &= N & "  , 0 "                                                                             '取消区分(0:有効)
        sql &= N & "  , '" & _db.rmSQ(frmC01F10_Login.loginValue.TantoCD) & "' "                        '更新者
        sql &= N & "  , CURRENT_TIMESTAMP "                                                             '更新日(システム日時)
        sql &= N & ") "

        'SQL発行
        _db.executeDB(sql)

    End Sub

    '仕入明細（T41_SIREDT） データ追加
    Private Sub DataInsertShiireDt()

        '一覧分ループ
        For index As Integer = 0 To Me.dgvIchiran.RowCount - 1

            '一覧セル
            Dim cells As DataGridViewCellCollection = dgvIchiran.Rows(index).Cells

            Dim sql As String = String.Empty
            sql &= N & "INSERT INTO T41_SIREDT ( "
            sql &= N & "    会社コード "
            sql &= N & "  , 仕入伝番 "
            sql &= N & "  , 行番 "
            sql &= N & "  , 商品コード "
            sql &= N & "  , 商品名 "
            sql &= N & "  , 商品詳細 "
            sql &= N & "  , 課税区分 "
            sql &= N & "  , 入数 "
            sql &= N & "  , 個数 "
            sql &= N & "  , 単位 "
            sql &= N & "  , 仕入数量 "
            sql &= N & "  , 仕入単価 "
            sql &= N & "  , 仕入金額 "
            sql &= N & "  , 仕入明細備考 "
            sql &= N & "  , 税抜額 "
            sql &= N & "  , 課税対象額 "
            sql &= N & "  , 消費税 "
            sql &= N & "  , 税込額 "
            sql &= N & "  , 支払有無 "
            sql &= N & "  , 支払伝番 "
            sql &= N & "  , 支払日 "
            sql &= N & "  , 更新者 "
            sql &= N & "  , 更新日 "
            sql &= N & ") VALUES ( "
            sql &= N & "    '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "                        '会社コード
            sql &= N & "  , '" & _db.rmSQ(Me.lblDenpyoNo.Text) & "' "                                       '仕入伝番
            sql &= N & "  , " & cells(COLNO_NO).Value                                                        '行番
            sql &= N & "  , '" & _db.rmSQ(cells(COLNO_ITEMCD).Value) & "' "                                  '商品コード
            sql &= N & "  , '" & _db.rmSQ(cells(COLNO_ITEMNM).Value) & "' "                                  '商品名
            sql &= N & "  , '" & _db.rmSQ(cells(COLNO_ITEMDT).Value) & "' "                                  '商品詳細
            sql &= N & "  , '" & _db.rmSQ(cells(COLNO_ZEIKBNCD).Value) & "' "                                '課税区分
            sql &= N & "  , " & Decimal.Parse(cells(COLNO_IRISUU).Value).ToString                            '入数
            sql &= N & "  , " & Decimal.Parse(cells(COLNO_KOSUU).Value).ToString                             '個数
            sql &= N & "  , '" & _db.rmSQ(cells(COLNO_TANNI).Value) & "' "                                   '単位
            sql &= N & "  , " & Decimal.Parse(cells(COLNO_SURYOU).Value).ToString                            '仕入数量
            sql &= N & "  , " & Decimal.Parse(cells(COLNO_SHIIRETANKA).Value).ToString                       '仕入単価
            sql &= N & "  , " & Decimal.Parse(cells(COLNO_SHIIREKINGAKU).Value).ToString                     '仕入金額
            sql &= N & "  , '" & _db.rmSQ(cells(COLNO_MEISAIBIKOU).Value) & "' "                             '仕入明細備考
            sql &= N & "  , " & Decimal.Parse(cells(COLNO_TAX_EXCLUSION).Value).ToString                     '税抜額
            sql &= N & "  , " & Decimal.Parse(cells(COLNO_TAX_TAXABLE).Value).ToString                       '課税対象額
            sql &= N & "  , " & Decimal.Parse(cells(COLNO_TAX_AMOUNT).Value).ToString                        '消費税
            sql &= N & "  , " & (Decimal.Parse(cells(COLNO_TAX_EXCLUSION).Value) +
                                 Decimal.Parse(cells(COLNO_TAX_AMOUNT).Value)).ToString                      '税込額
            sql &= N & "  , 0 "                                                                             '支払有無(0:未支払)
            sql &= N & "  , NULL "                                                                          '支払伝番
            sql &= N & "  , NULL "                                                                          '支払日
            sql &= N & "  , '" & _db.rmSQ(frmC01F10_Login.loginValue.TantoCD) & "' "                        '更新者
            sql &= N & "  , CURRENT_TIMESTAMP "                                                             '更新日
            sql &= N & ") "

            'SQL発行
            _db.executeDB(sql)

        Next

    End Sub

    '仕入基本（T40_SIREHD） データ更新（取消処理）
    Private Sub DataUpdateShiireHd()

        Dim sql As String = String.Empty
        sql &= N & " UPDATE T40_SIREHD "
        sql &= N & " SET    取消区分          = '1' "      '1:取消
        sql &= N & "       ,更新者            = '" & _db.rmSQ(frmC01F10_Login.loginValue.TantoCD) & "' "
        sql &= N & "       ,更新日            = CURRENT_TIMESTAMP "
        sql &= N & " WHERE  会社コード        = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "
        sql &= N & "   AND  仕入伝番          = '" & _db.rmSQ(Me.lblDenpyoNo.Text) & "' "

        'SQL発行
        _db.executeDB(sql)

    End Sub

    '仕入基本（T40_SIREHD） データ削除
    Private Sub DataDeleteShiireHd()

        Dim sql As String = String.Empty
        sql &= N & "DELETE FROM T40_SIREHD          SRH"
        sql &= N & " WHERE  SRH.会社コード        = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "
        sql &= N & "   AND  SRH.仕入伝番          = '" & _db.rmSQ(Me.lblDenpyoNo.Text) & "' "

        'SQL発行
        _db.executeDB(sql)

    End Sub

    '仕入明細（T41_SIREDT） データ削除
    Private Sub DataDeleteShiireDt()

        Dim sql As String = String.Empty
        sql &= N & "DELETE FROM T41_SIREDT          SRD"
        sql &= N & " WHERE  SRD.会社コード        = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "
        sql &= N & "   AND  SRD.仕入伝番          = '" & _db.rmSQ(Me.lblDenpyoNo.Text) & "' "

        'SQL発行
        _db.executeDB(sql)

    End Sub

#End Region

End Class