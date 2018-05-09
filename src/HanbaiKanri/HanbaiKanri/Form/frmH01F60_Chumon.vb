'===============================================================================
'
'　カネキ吉田商店様
'　　（システム名）販売管理システム
'　　（処理機能名）注文業務画面
'    （フォームID）H01F60
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   田頭        2018/1/27                 新規              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView

Public Class frmH01F60_Chumon
    Inherits System.Windows.Forms.Form

#Region "宣言"
    '-------------------------------------------------------------------------------
    '   変数定義
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _comLogc As CommonLogic                             '共通処理用
    Private _gh As UtilDataGridViewHandler                      'DataGridViewユーティリティクラス
    Private _SelectMode As Integer                              'メニューのどこから呼ばれたか。（1:登録、2:変更、3:取消、4:照会)
    Private _parentForm As Form                                 '親フォーム
    Private _DenpyoNo As String                                 '伝票番号
    Private _SelectID As String                                 'メニュー選択処理ID
    Private _userId As String                                   'ログインユーザＩＤ
    Private _init As Boolean                                    '初期処理済フラグ

    Private _SyoriName As String                                '処理名

    Private _HaisoNissu As Integer                              '配送日数
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
    Private Const COLNO_NISUGATA = 3                                '04:荷姿・形状
    Private Const COLNO_REI = 4                                     '05:冷
    Private Const COLNO_ZEIKBN = 5                                  '06:税
    Private Const COLNO_IRISUU = 6                                  '07:入数
    Private Const COLNO_KOSUU = 7                                   '08:個数
    Private Const COLNO_SURYOU = 8                                  '09:数量
    Private Const COLNO_TANNI = 9                                   '10:単位
    Private Const COLNO_URITANKA = 10                               '11:売上単価
    Private Const COLNO_URIKINGAKU = 11                             '12:売上金額
    Private Const COLNO_MEISAIBIKOU = 12                            '13:明細備考
    Private Const COLNO_KONPOU = 13                                 '14:梱包
    Private Const COLNO_REICD = 14                                  '15:冷凍区分
    Private Const COLNO_ZEIKBNCD = 15                               '16:課税区分
    Private Const COLNO_KONPOUCD = 16                               '17:梱包区分
    Private Const COLNO_TAX_RATE = 17                               '18:税率
    Private Const COLNO_TAX_EXCLUSION = 18                          '19:税抜額
    Private Const COLNO_TAX_TAXABLE = 19                            '20:課税対象額
    Private Const COLNO_TAX_AMOUNT = 20                             '21:消費税額

    'グリッド列名
    'dgvIchiran
    Private Const CCOL_NO As String = "cnNo"                        '01:No.
    Private Const CCOL_ITEMCD As String = "cnItemCd"                '02:商品CD
    Private Const CCOL_ITEMNM As String = "cnItemNm"                '03:商品名
    Private Const CCOL_NISUGATA As String = "cnNisugata"            '04:荷姿・形状
    Private Const CCOL_REI As String = "cnZeiKbn"                   '05:冷
    Private Const CCOL_ZEIKBN As String = "cnZeiKbn"                '06:税
    Private Const CCOL_IRISUU As String = "cnIrisuu"                '07:入数
    Private Const CCOL_KOSUU As String = "cnKosuu"                  '08:個数
    Private Const CCOL_SURYOU As String = "cnSuryou"                '09:数量
    Private Const CCOL_TANNI As String = "cnTanni"                  '10:単位
    Private Const CCOL_URITANKA As String = "cnUriTanka"            '11:売上単価
    Private Const CCOL_URIKINGAKU As String = "cnUriKingaku"        '12:売上金額
    Private Const CCOL_MEISAIBIKOU As String = "cnMeisaiBikou"      '13:明細備考
    Private Const CCOL_KONPOU As String = "cnKonpou"                '14:梱包
    Private Const CCOL_REICD As String = "cnReiCD"                  '15:冷凍区分
    Private Const CCOL_ZEIKBNCD As String = "cnZeiKbnCD"            '16:課税区分
    Private Const CCOL_KONPOUCD As String = "cnKonpouCD"            '17:梱包区分
    Private Const CCOL_TAX_RATE As String = "cnTaxRate"             '18:税率
    Private Const CCOL_TAX_EXCLUSION As String = "cnTaxExclusion"   '19:税抜額
    Private Const CCOL_TAX_TAXABLE As String = "cnTaxAble"          '20:課税対象額
    Private Const CCOL_TAX_AMOUNT As String = "cnTaxAmount"         '21:消費税額

    'グリッドデータ名
    'dgvIchiran
    Private Const DTCOL_NO As String = "dtNo"                       '01:No.
    Private Const DTCOL_ITEMCD As String = "dtItemCd"               '02:商品CD
    Private Const DTCOL_ITEMNM As String = "dtItemNm"               '03:商品名
    Private Const DTCOL_NISUGATA As String = "dtNisugata"           '04:荷姿・形状
    Private Const DTCOL_REI As String = "dtReiKbn"                  '05:冷
    Private Const DTCOL_ZEIKBN As String = "dtZeiKbn"               '06:税
    Private Const DTCOL_IRISUU As String = "dtIrisuu"               '07:入数
    Private Const DTCOL_KOSUU As String = "dtKosuu"                 '08:個数
    Private Const DTCOL_SURYOU As String = "dtSuryou"               '09:数量
    Private Const DTCOL_TANNI As String = "dtTanni"                 '10:単位
    Private Const DTCOL_URITANKA As String = "dtUriTanka"           '11:売上単価
    Private Const DTCOL_URIKINGAKU As String = "dtUriKingaku"       '12:売上金額
    Private Const DTCOL_MEISAIBIKOU As String = "dtMeisaiBikou"     '13:明細備考
    Private Const DTCOL_KONPOU As String = "dtKonpou"               '14:梱包
    Private Const DTCOL_REICD As String = "dtReiCD"                 '15:冷凍区分
    Private Const DTCOL_ZEIKBNCD As String = "dtZeiKbnCD"           '16:課税区分
    Private Const DTCOL_KONPOUCD As String = "dtKonpouCD"           '17:梱包区分
    Private Const DTCOL_TAX_RATE As String = "dtTaxRate"            '18:税率
    Private Const DTCOL_TAX_EXCLUSION As String = "dtTaxExclusion"  '19:税抜額
    Private Const DTCOL_TAX_TAXABLE As String = "dtTaxAble"         '20:課税対象額
    Private Const DTCOL_TAX_AMOUNT As String = "dtTaxAmount"        '21:消費税額

#End Region

    '税算出区分
    Public ReadOnly Property ZeiSanshutsu() As String
        Get
            Return _ZeiSanshutsu
        End Get
    End Property
    '処理名
    Public ReadOnly Property SyoriName() As String
        Get
            Return _SyoriName
        End Get
    End Property
    '更新日時
    Public ReadOnly Property UpdateTime() As DateTime
        Get
            Return _UpdateTime
        End Get
    End Property

    '-------------------------------------------------------------------------------
    'デフォルトコンストラクタ（隠蔽）
    '-------------------------------------------------------------------------------
    Private Sub New()
        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

        '一覧初期化処理
        initDgvIchiran()

    End Sub

    '-------------------------------------------------------------------------------
    'コンストラクタ　メニューから呼ばれる
    '引数
    '   prmSelectMode     '各フォームに引き渡す編集モードの値 CommonConstのこれを使う
    '           Public Const MODE_ADDNEW = 1                                 '登録
    '           Public Const MODE_EditStatus = 2                             '変更
    '           Public Const MODE_CancelStatus = 3                           '取消
    '           Public Const MODE_InquiryStatus = 4                          '照会
    '   prmSyukkasakiCd : 出荷先コード
    '   prmParentForm   : 呼び出し元フォーム
    '   prmDenpyoNO : 注文一覧から伝票番号が渡されるときに使用する
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByRef prmSelectID As String, ByRef prmSelectMode As Integer, ByRef prmSyukkasakiCd As String, ByRef prmParentForm As Form, Optional prmDenpyoNO As String = "")
        Call Me.New()

        Try
            _init = False

            '初期処理
            _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
            _db = prmRefDbHd                                                    'DBハンドラの設定
            _comLogc = New CommonLogic(_db, _msgHd)                             ' 共通処理用
            _gh = New UtilDataGridViewHandler(dgvIchiran)                       'DataGridViewユーティリティクラス
            _parentForm = prmParentForm                                         '親フォーム
            _SelectID = prmSelectID                                             'メニュー選択処理ID
            _SelectMode = prmSelectMode                                         '処理状態
            _DenpyoNo = prmDenpyoNO                                             '伝票番号
            _userId = frmC01F10_Login.loginValue.TantoCD                        'ユーザＩＤ

            StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
            Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                   'フォームタイトル表示

            '出荷先コードをセット
            lblShukkaCd.Text = prmSyukkasakiCd

            '処理状態の選択
            Select Case _SelectMode
                Case CommonConst.MODE_ADDNEW  '登録
                    lblShoriMode.Text = "登録"
                    cmdPrint.Enabled = False
                    cmdTouroku.Enabled = True
                    cmdModoru.Enabled = True
                    If prmDenpyoNO = String.Empty Then
                        '新規登録
                        _SyoriName = CommonConst.MODE_ADDNEW_NAME
                    Else
                        '複写新規
                        _SyoriName = CommonConst.MODE_ADDNEWCOPY_NAME
                    End If
                    'コントロールの入力制御
                    InputControl(True)
                Case CommonConst.MODE_EditStatus  '変更
                    lblShoriMode.Text = "変更"
                    cmdPrint.Enabled = False
                    cmdTouroku.Enabled = True
                    cmdModoru.Enabled = True
                    _SyoriName = CommonConst.MODE_EditStatus_NAME
                    'コントロールの入力制御
                    InputControl(True)
                Case CommonConst.MODE_CancelStatus  '取消
                    lblShoriMode.Text = "取消"
                    cmdPrint.Enabled = False
                    cmdTouroku.Enabled = True
                    cmdModoru.Enabled = True
                    _SyoriName = CommonConst.MODE_CancelStatus_NAME
                    'コントロールの入力制御
                    InputControl(False)
                Case CommonConst.MODE_InquiryStatus  '照会
                    lblShoriMode.Text = "照会"
                    cmdPrint.Enabled = True
                    cmdTouroku.Enabled = False
                    cmdModoru.Enabled = True
                    _SyoriName = CommonConst.MODE_InquiryStatus_NAME
                    'コントロールの入力制御
                    InputControl(False)
            End Select

            If prmDenpyoNO = String.Empty Then
                '伝票番号が渡されないときは新規登録

                '取引先情報表示 ここで伝票番号取得する
                getTorihikisaki(True)

                '出荷日（システム日付）
                dtpShukkaDt.Text = DateAdd("m", 0, Now).ToString("yyyy/MM/dd")

                '出荷日の値変更時処理
                shukkaDtChanged()

            Else
                '複写新規、変更、照会、取消の場合

                '伝票番号をもとに画面項目を表示

                If _SelectMode = CommonConst.MODE_ADDNEW Then
                    '複写新規の場合

                    '取引先情報表示 ここで伝票番号取得する
                    getTorihikisaki(True)

                    '注文データの初期表示
                    getChumonData()

                    '注文データ明細の表示
                    getChumonDataList()

                    '出荷日（システム日付）
                    dtpShukkaDt.Text = DateAdd("m", 0, Now).ToString("yyyy/MM/dd")

                    '出荷日の値変更時処理
                    shukkaDtChanged()

                Else
                    '変更、照会、取消の場合

                    '渡された伝票番号をセット
                    lblDenpyoNo.Text = _DenpyoNo

                    '取引先情報取得
                    getTorihikisaki(False)

                    '注文データの初期表示
                    getChumonData()

                    '注文データ明細の表示
                    getChumonDataList()
                End If

            End If

            _init = True

            '操作履歴ログ作成
            _comLogc.Insert_L01_ProcLog(frmC01F10_Login.loginValue.BumonCD, DBNull.Value, _SelectID, CommonConst.PROGRAM_UPDATE, CommonConst.STATUS_NORMAL,
                                        lblDenpyoNo.Text, _SyoriName, DBNull.Value, DBNull.Value, DBNull.Value,
                                        DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _userId)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    'コントロールの入力制御
    '
    '引数 prmEnabled
    '       true:入力可 false:入力不可
    Private Sub InputControl(ByVal prmEnabled As Boolean)
        Me.dtpShukkaDt.Enabled = prmEnabled
        Me.dtpChakuDt.Enabled = prmEnabled

        Me.txtPostalCd1.Enabled = prmEnabled
        Me.txtPostalCd2.Enabled = prmEnabled
        Me.txtShukkaNm.Enabled = prmEnabled
        Me.txtAddress1.Enabled = prmEnabled
        Me.txtAddress2.Enabled = prmEnabled
        Me.txtAddress3.Enabled = prmEnabled
        Me.txtTantousha.Enabled = prmEnabled
        Me.txtTelNo.Enabled = prmEnabled
        Me.txtFaxNo.Enabled = prmEnabled
        Me.txtIrainusi.Enabled = prmEnabled
        Me.txtJikansitei.Enabled = prmEnabled
        Me.txtSeikyuNm.Enabled = prmEnabled
        Me.txtShukkaGrpNm.Enabled = prmEnabled

        Me.txtShagaiBikou.Enabled = prmEnabled
        Me.txtShanaiBikou.Enabled = prmEnabled

        Me.dgvIchiran.ReadOnly = Not prmEnabled

        Me.cmdTopRow.Enabled = prmEnabled
        Me.cmdAddRow.Enabled = prmEnabled
        Me.cmdDelRow.Enabled = prmEnabled
        Me.cmdRowCopy.Enabled = prmEnabled
        Me.cmdSwapRowUp.Enabled = prmEnabled
        Me.cmdSwapRowDown.Enabled = prmEnabled


    End Sub

#Region "ボタン"
    '登録ボタン押下時
    Private Sub cmdTouroku_Click(sender As Object, e As EventArgs) Handles cmdTouroku.Click

        Try

            '入力チェック---------------------------------------------------------------
            '1)	パスワード入力項目チェック
            'Try
            '    Call checkInput()
            'Catch lex As UsrDefException
            '    lex.dspMsg()
            '    Exit Sub
            'End Try


            '印刷指示画面を開く
            Dim openForm As frmH01F70_Chohyo = New frmH01F70_Chohyo(_msgHd, _db, _SelectID, Me, _SelectMode)      '画面遷移
            '印刷（登録）したか、戻るボタンで戻ったか判定したいのでダイアログでオープン
            openForm.ShowDialog()                      '画面表示
            If openForm.Printed Then
                '印刷が行われたときは自分自身も閉じる
                _parentForm.Show()                                              ' 前画面を表示
                _parentForm.Enabled = True                                      ' 前画面の使用を可能にする
                _parentForm.Activate()                                          ' 前画面をアクティブにする

                Me.Dispose()                                                    ' 自画面を閉じる
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

        'Me.Close()
    End Sub

    '戻るボタン押下時
    Private Sub cmdModoru_Click(sender As Object, e As EventArgs) Handles cmdModoru.Click

        _parentForm.Show()                                              ' 前画面を表示
        _parentForm.Enabled = True                                      ' 前画面の使用を可能にする
        _parentForm.Activate()                                          ' 前画面をアクティブにする

        Me.Dispose()                                                    ' 自画面を閉じる
    End Sub

    '再印刷ボタン押下時
    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        '印刷指示画面を開く
        Dim openForm As frmH01F70_Chohyo = New frmH01F70_Chohyo(_msgHd, _db, _SelectID, Me, _SelectMode)      '画面遷移
        '印刷（登録）したか、戻るボタンで戻ったか判定したいのでダイアログでオープン
        openForm.ShowDialog()                      '画面表示
        If openForm.Printed Then
            '印刷が行われたときは自分自身も閉じる
            _parentForm.Show()                                              ' 前画面を表示
            _parentForm.Enabled = True                                      ' 前画面の使用を可能にする
            _parentForm.Activate()                                          ' 前画面をアクティブにする

            Me.Dispose()                                                    ' 自画面を閉じる
        End If

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
                        Case COLNO_ITEMCD, COLNO_REI, COLNO_ZEIKBN, COLNO_TANNI, COLNO_KONPOU
                            SelectWindowOpen()
                    End Select

                Case Keys.Enter
                    Dim iRow As Integer = dgvIchiran.CurrentCell.RowIndex
                    Dim iCol As Integer = dgvIchiran.CurrentCell.ColumnIndex

                    If iRow = _gh.getMaxRow - 1 AndAlso iCol = COLNO_KONPOU Then
                        'アクティブセルが最終行の梱包列の場合、最下行に行追加
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
            Case COLNO_ITEMNM, COLNO_NISUGATA, COLNO_MEISAIBIKOU
                dgvIchiran.ImeMode = Windows.Forms.ImeMode.Hiragana
            Case COLNO_IRISUU, COLNO_KOSUU, COLNO_SURYOU, COLNO_URITANKA, COLNO_URIKINGAKU
                dgvIchiran.ImeMode = Windows.Forms.ImeMode.Off
        End Select

    End Sub

    '出荷日の値変更時
    Private Sub dtpShukkaDt_ValueChanged(sender As Object, e As System.EventArgs) Handles dtpShukkaDt.ValueChanged
        Try
            '初期処理前の場合は処理終了
            If _init = False Then
                Exit Sub
            End If

            '出荷日の値変更時処理
            shukkaDtChanged()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '着日の値変更時
    Private Sub dtpChakuDt_ValueChanged(sender As Object, e As System.EventArgs) Handles dtpChakuDt.ValueChanged
        Try
            '初期処理前の場合は処理終了
            If _init = False Then
                Exit Sub
            End If

            '着日の値変更時処理
            chakuDtChanged()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '郵便番号１、２からフォーカスが外れたとき
    Private Sub txtPostalCd_Leave(sender As Object, e As EventArgs) Handles txtPostalCd1.Leave, txtPostalCd2.Leave
        Try
            '郵便番号フォーカスアウト時処理
            postalCdLeave()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '郵便番号１、２をダブルクリックしたとき
    Private Sub txtPostalCd_DoubleClick(sender As Object, e As EventArgs) Handles txtPostalCd1.DoubleClick, txtPostalCd2.DoubleClick
        Try
            '郵便番号検索ウインドウオープン
            postalCdSelectWindowOpen()

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
                             dtpShukkaDt.KeyPress,
                             dtpChakuDt.KeyPress,
                             txtShukkaNm.KeyPress,
                             txtPostalCd1.KeyPress,
                             txtPostalCd2.KeyPress,
                             txtAddress1.KeyPress,
                             txtAddress2.KeyPress,
                             txtAddress3.KeyPress,
                             txtShagaiBikou.KeyPress,
                             txtShanaiBikou.KeyPress,
                             txtTantousha.KeyPress,
                             txtTelNo.KeyPress,
                             txtFaxNo.KeyPress,
                             txtIrainusi.KeyPress,
                             txtJikansitei.KeyPress,
                             txtSeikyuNm.KeyPress,
                             txtShukkaGrpNm.KeyPress

        '押下キーがEnterの場合、次のコントロールへフォーカス移動
        UtilMDL.UtilClass.moveNextFocus(Me, e)
    End Sub

    '-------------------------------------------------------------------------------
    '　フォーカス取得イベント
    '-------------------------------------------------------------------------------
    Private Sub ctl_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
                             dtpShukkaDt.GotFocus,
                             dtpChakuDt.GotFocus,
                             txtShukkaNm.GotFocus,
                             txtPostalCd1.GotFocus,
                             txtPostalCd2.GotFocus,
                             txtAddress1.GotFocus,
                             txtAddress2.GotFocus,
                             txtAddress3.GotFocus,
                             txtShagaiBikou.GotFocus,
                             txtShanaiBikou.GotFocus,
                             txtTantousha.GotFocus,
                             txtTelNo.GotFocus,
                             txtFaxNo.GotFocus,
                             txtIrainusi.GotFocus,
                             txtJikansitei.GotFocus,
                             txtSeikyuNm.GotFocus,
                             txtShukkaGrpNm.GotFocus

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
        dt.Columns().Add(DTCOL_NISUGATA, Type.GetType("System.String"))
        dt.Columns().Add(DTCOL_REI, Type.GetType("System.String"))
        dt.Columns().Add(DTCOL_ZEIKBN, Type.GetType("System.String"))
        dt.Columns().Add(DTCOL_IRISUU, Type.GetType("System.Double"))
        dt.Columns().Add(DTCOL_KOSUU, Type.GetType("System.Decimal"))
        dt.Columns().Add(DTCOL_SURYOU, Type.GetType("System.Double"))
        dt.Columns().Add(DTCOL_TANNI, Type.GetType("System.String"))
        dt.Columns().Add(DTCOL_URITANKA, Type.GetType("System.Double"))
        dt.Columns().Add(DTCOL_URIKINGAKU, Type.GetType("System.Decimal"))
        dt.Columns().Add(DTCOL_MEISAIBIKOU, Type.GetType("System.String"))
        dt.Columns().Add(DTCOL_KONPOU, Type.GetType("System.String"))
        dt.Columns().Add(DTCOL_REICD, Type.GetType("System.String"))
        dt.Columns().Add(DTCOL_ZEIKBNCD, Type.GetType("System.String"))
        dt.Columns().Add(DTCOL_KONPOUCD, Type.GetType("System.String"))
        dt.Columns().Add(DTCOL_TAX_RATE, Type.GetType("System.Decimal"))
        dt.Columns().Add(DTCOL_TAX_EXCLUSION, Type.GetType("System.Decimal"))
        dt.Columns().Add(DTCOL_TAX_TAXABLE, Type.GetType("System.Decimal"))
        dt.Columns().Add(DTCOL_TAX_AMOUNT, Type.GetType("System.Decimal"))

        Dim dr As DataRow = dt.NewRow()
        dr.Item(DTCOL_NO) = 1
        dr.Item(DTCOL_ITEMCD) = String.Empty
        dr.Item(DTCOL_ITEMNM) = String.Empty
        dr.Item(DTCOL_NISUGATA) = String.Empty
        dr.Item(DTCOL_REI) = String.Empty
        dr.Item(DTCOL_ZEIKBN) = String.Empty
        dr.Item(DTCOL_IRISUU) = DBNull.Value
        dr.Item(DTCOL_KOSUU) = 0
        dr.Item(DTCOL_SURYOU) = DBNull.Value
        dr.Item(DTCOL_TANNI) = String.Empty
        dr.Item(DTCOL_URITANKA) = DBNull.Value
        dr.Item(DTCOL_URIKINGAKU) = 0
        dr.Item(DTCOL_MEISAIBIKOU) = String.Empty
        dr.Item(DTCOL_KONPOU) = String.Empty
        dr.Item(DTCOL_REICD) = String.Empty
        dr.Item(DTCOL_ZEIKBNCD) = String.Empty
        dr.Item(DTCOL_KONPOUCD) = String.Empty
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
                    dgvIchiran.Rows(clickRowIndex).Cells(COLNO_NISUGATA).Value = openForm.GetNisugata
                    '商品マスタから初期値を項目編集する
                    DispShohin(clickRowIndex, openForm.GettShohinCD)
                End If
                openForm = Nothing
            Case COLNO_REI      '冷
                Dim openForm As frmC10F90_Hanyo = New frmC10F90_Hanyo(_msgHd, _db, CommonConst.HANYO_REITOU_KBN)      '画面遷移
                openForm.ShowDialog()                      '画面表示
                If openForm.Selected Then
                    '選択されました
                    dgvIchiran.Rows(clickRowIndex).Cells(COLNO_REI).Value = openForm.GetValNM
                    dgvIchiran.Rows(clickRowIndex).Cells(COLNO_REICD).Value = openForm.GetValCD
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
            Case COLNO_KONPOU     '梱包
                Dim openForm As frmC10F90_Hanyo = New frmC10F90_Hanyo(_msgHd, _db, CommonConst.HANYO_KONPOU_KBN)      '画面遷移
                openForm.ShowDialog()                      '画面表示
                If openForm.Selected Then
                    '選択されました
                    dgvIchiran.Rows(clickRowIndex).Cells(COLNO_KONPOU).Value = openForm.GetValNM
                    dgvIchiran.Rows(clickRowIndex).Cells(COLNO_KONPOUCD).Value = openForm.GetValCD
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
        strSql = strSql & "   s.会社コード, s.商品コード, s.商品名, s.冷凍区分 ,h.文字２ as 冷凍区分名, s.課税区分,h2.文字２ as 課税区分名, s.入数, s.単位 "
        strSql = strSql & " FROM m20_goods s "
        strSql = strSql & "   left join M90_HANYO h on h.会社コード = s.会社コード and h.固定キー = '" & CommonConst.HANYO_REITOU_KBN & "' and h.可変キー = s.冷凍区分 "
        strSql = strSql & "   left join M90_HANYO h2 on h2.会社コード = s.会社コード and h2.固定キー = '" & CommonConst.HANYO_KAZEI_KBN & "' and h2.可変キー = s.課税区分 "
        strSql = strSql & " Where s.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and s.商品コード = '" & _db.rmSQ(argStrShohinCD) & "'"

        Dim reccnt As Integer = 0
        Dim ds As DataSet = _db.selectDB(strSql, RS, reccnt)

        '一覧セル
        Dim cells As DataGridViewCellCollection = dgvIchiran.Rows(argIntRowIndex).Cells

        cells(COLNO_REICD).Value = _db.rmNullStr(ds.Tables(RS).Rows(0)("冷凍区分"))
        cells(COLNO_REI).Value = _db.rmNullStr(ds.Tables(RS).Rows(0)("冷凍区分名"))
        cells(COLNO_ZEIKBNCD).Value = _db.rmNullStr(ds.Tables(RS).Rows(0)("課税区分"))
        cells(COLNO_ZEIKBN).Value = _db.rmNullStr(ds.Tables(RS).Rows(0)("課税区分名"))
        cells(COLNO_IRISUU).Value = _db.rmNullDouble(ds.Tables(RS).Rows(0)("入数"))
        cells(COLNO_TANNI).Value = _db.rmNullStr(ds.Tables(RS).Rows(0)("単位"))
        cells(COLNO_TAX_RATE).Value = _ZeiRitsu

        '売上単価取得(1行)
        getUriTankaOne(cells, argIntRowIndex)

    End Sub

    '出荷日の値変更時処理

    Private Sub shukkaDtChanged()

        Dim strShukkaDate As String = CDate(dtpShukkaDt.Value).ToString("yyyy/MM/dd")

        '曜日再取得
        lblShukkaDay.Text = YobiReturn(strShukkaDate)

        '着日更新
        updateChakuDt()

    End Sub

    '着日更新
    Private Sub updateChakuDt()

        '着日に「出荷日＋配送日数」をセット
        dtpChakuDt.Text = DateAdd("d", _HaisoNissu, CDate(dtpShukkaDt.Value)).ToString("yyyy/MM/dd")

        '着日の値変更時処理
        chakuDtChanged()

    End Sub

    '着日の値変更時処理

    Private Sub chakuDtChanged()

        Dim strChakuDate As String = CDate(dtpChakuDt.Value).ToString("yyyy/MM/dd")

        '曜日再取得
        lblChakuDay.Text = YobiReturn(strChakuDate)

        '税率取得処理
        getZeiRitsu()

        '売上単価取得(全行)
        getUriTankaAll()

        '税関連再計算(全行)
        RecalcZeiAll()

        '各合計額再計算
        RecalcTotal()

    End Sub

    'Stringのyyyy/mm/ddを引数に渡すと曜日を返す
    Private Function YobiReturn(ByRef strDenpyoDate As String)

        Dim dteDenpyo As DateTime
        Dim strWeek1 As String ' 短縮表記の曜日を取得します（例：日）

        If DateTime.TryParse(strDenpyoDate, dteDenpyo) Then
            Dim week As DayOfWeek = dteDenpyo.DayOfWeek           ' 現在の曜日をDayOfWeek型で取得します
            Dim weekNumber As Integer = CInt(dteDenpyo.DayOfWeek) ' Int32型にキャストして曜日を数値に変換します
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
        sql &= N & "   AND  TAX.適用開始日       <= '" & CDate(dtpChakuDt.Value).ToString("yyyy/MM/dd") & "' "
        sql &= N & "   AND  TAX.適用終了日       >= '" & CDate(dtpChakuDt.Value).ToString("yyyy/MM/dd") & "' "

        Dim reccnt As Integer = 0
        Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)

        '税率
        _ZeiRitsu = _db.rmNullDouble(ds.Tables(RS).Rows(0)("消費税率"))

    End Sub

    '郵便番号フォーカスアウト時処理
    Private Sub postalCdLeave()

        '郵便番号１、２とも入力がある場合
        If (Not (String.Empty.Equals(Me.txtPostalCd1.Text))) AndAlso (Not (String.Empty.Equals(Me.txtPostalCd2.Text))) Then
            '住所取得処理
            Call getAddress()
        Else
            '住所１クリア
            Me.txtAddress1.Clear()
        End If

    End Sub

    '住所取得処理
    Private Sub getAddress()

        '入力郵便番号
        Dim strInputPostalCd1 As String = Me.txtPostalCd1.Text  '郵便番号１
        Dim strInputPostalCd2 As String = Me.txtPostalCd2.Text  '郵便番号２

        '住所データを取得
        Dim ds As DataSet = _comLogc.getAddress(strInputPostalCd1 & strInputPostalCd2)

        'データカウント
        Dim dataCount As Integer = ds.Tables(RS).Rows.Count

        If dataCount = 0 Then
            'データ0件

            '住所１クリア
            Me.txtAddress1.Clear()

        ElseIf dataCount = 1 Then
            'データ1件

            '取得データ
            Dim dataRow As DataRow = ds.Tables(RS).Rows(0)

            '住所１に取得データをセット
            Me.txtAddress1.Text = _db.rmNullStr(dataRow("都道府県名")) &
                                  _db.rmNullStr(dataRow("市区町村名")) &
                                  _db.rmNullStr(dataRow("町域名"))

        Else
            'データ2件以上

            '郵便番号検索ウインドウオープン
            postalCdSelectWindowOpen()
        End If

    End Sub

    '郵便番号検索ウインドウオープン処理
    Private Sub postalCdSelectWindowOpen()

        '入力郵便番号
        Dim strInputPostalCd1 As String = Me.txtPostalCd1.Text  '郵便番号１
        Dim strInputPostalCd2 As String = Me.txtPostalCd2.Text  '郵便番号２

        Dim openForm As frmC10F30_Postal = New frmC10F30_Postal(_msgHd, _db, strInputPostalCd1, strInputPostalCd2)      '画面遷移
        openForm.ShowDialog()                      '画面表示

        '選択されている場合
        If openForm.Selected Then
            '画面に値をセット

            '郵便番号１
            Me.txtPostalCd1.Text = openForm.GetValPostalCd1
            '郵便番号２
            Me.txtPostalCd2.Text = openForm.GetValPostalCd2
            '住所１
            Me.txtAddress1.Text = openForm.GetValAddress
        End If

        openForm = Nothing

    End Sub

    '取引先の初期表示
    '引数
    '   newFlg     '新規または複写新規の場合、true
    Private Sub getTorihikisaki(ByVal newFlg)

        '出荷先コードを取得
        Dim syukkasakiCd = lblShukkaCd.Text

        Dim strSql As String = String.Empty

        Try
            strSql = "SELECT "
            strSql = strSql & "    c.配送日数,c.取引先名, c.郵便番号, c.住所１, c.住所２, c.住所３, c.担当者名, c.電話番号, c.ＦＡＸ番号 ,c.出荷先分類 ,c.金額端数区分 ,c.税算出区分 ,c.税端数区分 "
            strSql = strSql & ", c.依頼主等, c.時間指定, c.運送便コード, h.文字１ as 運送便名, c.請求先コード, d.取引先名 as 請求先名, c.出荷先Ｇコード, e.取引先名 as 出荷先GRP, c.出荷先分類  "
            strSql = strSql & " FROM m10_customer c "
            strSql = strSql & "    left join M90_HANYO h on h.会社コード = c.会社コード and h.固定キー = '" & CommonConst.HANYO_KOTEI_UNSOUBIN & "' and h.可変キー = c.運送便コード "
            strSql = strSql & "    left join m10_customer d on d.会社コード = c.会社コード and d.取引先コード = c.請求先コード "
            strSql = strSql & "    left join m10_customer e on e.会社コード = c.会社コード and e.取引先コード = c.出荷先Ｇコード "
            strSql = strSql & " Where c.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and c.取引先コード = '" & syukkasakiCd & "'"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(strSql, RS, reccnt)


            'データがない場合
            If reccnt = 0 Then
                Exit Sub
            End If

            '取得した出荷先コードから表示内容を取得
            '売上か委託かの判定
            Select Case _db.rmNullStr(ds.Tables(RS).Rows(0)("出荷先分類"))
                Case CommonConst.SKBUNRUI_ITAKU
                    lblNyuuryokuMode.Text = "委託"
                    lblNyuuryokuMode.Tag = _db.rmNullStr(ds.Tables(RS).Rows(0)("出荷先分類"))
                    lblNyuuryokuMode.BackColor = System.Drawing.Color.SkyBlue
                    '新規または複写新規の場合
                    If newFlg Then
                        '伝票番号表示
                        Me.lblDenpyoNo.Text = _comLogc.GetDenpyoNo(frmC01F10_Login.loginValue.BumonCD, CommonConst.SAIBAN_ITAKU)
                    End If

                Case Else
                    lblNyuuryokuMode.Text = "売上"
                    lblNyuuryokuMode.Tag = _db.rmNullStr(ds.Tables(RS).Rows(0)("出荷先分類"))
                    lblNyuuryokuMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
                    '新規または複写新規の場合
                    If newFlg Then
                        '伝票番号表示
                        Me.lblDenpyoNo.Text = _comLogc.GetDenpyoNo(frmC01F10_Login.loginValue.BumonCD, CommonConst.SAIBAN_URIAGE)
                    End If
            End Select

            'ラベルの色を変更
            LabelBackColorChange()

            _HaisoNissu = _db.rmNullInt(ds.Tables(RS).Rows(0)("配送日数"))
            _KinHasu = _db.rmNullStr(ds.Tables(RS).Rows(0)("金額端数区分"))
            _ZeiHasu = _db.rmNullStr(ds.Tables(RS).Rows(0)("税端数区分"))

            '新規または複写新規の場合
            If newFlg Then

                Dim postalCd1 As String
                Dim postalCd2 As String
                If String.IsNullOrEmpty(_db.rmNullStr(ds.Tables(RS).Rows(0)("郵便番号"))) Then
                    postalCd1 = String.Empty
                    postalCd2 = String.Empty
                Else
                    postalCd1 = (_db.rmNullStr(ds.Tables(RS).Rows(0)("郵便番号"))).Substring(0, 3)
                    postalCd2 = (_db.rmNullStr(ds.Tables(RS).Rows(0)("郵便番号"))).Substring(3, 4)
                End If
                Me.txtPostalCd1.Text = _db.rmNullStr(postalCd1)
                Me.txtPostalCd2.Text = _db.rmNullStr(postalCd2)
                Me.txtShukkaNm.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("取引先名"))
                Me.txtAddress1.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("住所１"))
                Me.txtAddress2.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("住所２"))
                Me.txtAddress3.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("住所３"))
                Me.txtTantousha.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("担当者名"))
                Me.txtTelNo.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("電話番号"))
                Me.txtFaxNo.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("ＦＡＸ番号"))
                Me.txtIrainusi.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("依頼主等"))
                Me.txtJikansitei.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("時間指定"))
                Me.lblUnsoubin.Tag = _db.rmNullStr(ds.Tables(RS).Rows(0)("運送便コード"))
                Me.lblUnsoubin.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("運送便名"))
                Me.lblSeikyuCd.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("請求先コード"))
                Me.txtSeikyuNm.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("請求先名"))
                Me.lblShukkaGrpCd.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("出荷先Ｇコード"))
                Me.txtShukkaGrpNm.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("出荷先GRP"))

                Me.txtShagaiBikou.Text = String.Empty   '社外備考
                Me.txtShanaiBikou.Text = String.Empty   '社内備考

                _ZeiSanshutsu = _db.rmNullStr(ds.Tables(RS).Rows(0)("税算出区分"))
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '注文データの初期表示
    Private Sub getChumonData()

        'データ初期表示

        Dim strSql As String = String.Empty

        Try
            strSql = "SELECT  "
            strSql = strSql & "    c.出荷日 ,c.着日 ,c.出荷先コード ,c.出荷先名 ,c.出荷先分類 ,c.郵便番号 ,c.住所１ ,c.住所２ ,c.住所３ "
            strSql = strSql & "    ,c.担当者名 ,c.電話番号 ,c.ＦＡＸ番号 ,c.依頼主等, c.時間指定, c.運送便コード, h.文字１ as 運送便名"
            strSql = strSql & "    ,c.社外備考 ,c.社内備考 ,c.請求先コード, c.請求先名 ,c.出荷先Ｇコード, c.出荷先Ｇ名"
            strSql = strSql & "    ,c.総個数, c.売上金額計, c.税抜額計, c.課税対象額計, c.消費税計, c.税込額計, c.税率, c.税計算区分 ,c.更新日 "

            strSql = strSql & " FROM T10_CYMNHD c "
            strSql = strSql & "    left  join M90_HANYO    h on h.会社コード = c.会社コード and h.固定キー = '" & CommonConst.HANYO_KOTEI_UNSOUBIN & "' and h.可変キー = c.運送便コード "

            strSql = strSql & " Where c.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and c.注文伝番 = '" & _DenpyoNo & "'"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(strSql, RS, reccnt)

            'データがない場合
            If reccnt = 0 Then
                Exit Sub
            End If

            '注文基本データ
            Dim data As DataRow = ds.Tables(RS).Rows(0)

            '出荷日
            dtpShukkaDt.Text = _db.rmNullDate(data("出荷日"), "yyyy/MM/dd")
            lblShukkaDay.Text = _db.rmNullDate(data("出荷日"), "ddd")

            '着日
            dtpChakuDt.Text = _db.rmNullDate(data("着日"), "yyyy/MM/dd")
            lblChakuDay.Text = _db.rmNullDate(data("着日"), "ddd")

            '出荷先コード
            lblShukkaCd.Text = _db.rmNullStr(data("出荷先コード"))

            '取得した出荷先コードから表示内容を取得
            '売上か委託かの判定
            Select Case _db.rmNullStr(data("出荷先分類"))
                Case CommonConst.SKBUNRUI_ITAKU
                    lblNyuuryokuMode.Text = "委託"
                    lblNyuuryokuMode.Tag = _db.rmNullStr(data("出荷先分類"))
                    lblNyuuryokuMode.BackColor = System.Drawing.Color.SkyBlue
                Case Else
                    lblNyuuryokuMode.Text = "売上"
                    lblNyuuryokuMode.Tag = _db.rmNullStr(data("出荷先分類"))
                    lblNyuuryokuMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
            End Select

            'ラベルの色を変更
            LabelBackColorChange()

            '郵便番号
            Dim postalCd1 As String
            Dim postalCd2 As String
            If String.IsNullOrEmpty(_db.rmNullStr(data("郵便番号"))) Then
                postalCd1 = String.Empty
                postalCd2 = String.Empty
            Else
                postalCd1 = (_db.rmNullStr(data("郵便番号"))).Substring(0, 3)
                postalCd2 = (_db.rmNullStr(data("郵便番号"))).Substring(3, 4)
            End If
            Me.txtPostalCd1.Text = _db.rmNullStr(postalCd1)
            Me.txtPostalCd2.Text = _db.rmNullStr(postalCd2)
            Me.txtShukkaNm.Text = _db.rmNullStr(data("出荷先名"))
            Me.txtAddress1.Text = _db.rmNullStr(data("住所１"))
            Me.txtAddress2.Text = _db.rmNullStr(data("住所２"))
            Me.txtAddress3.Text = _db.rmNullStr(data("住所３"))
            Me.txtTantousha.Text = _db.rmNullStr(data("担当者名"))
            Me.txtTelNo.Text = _db.rmNullStr(data("電話番号"))
            Me.txtFaxNo.Text = _db.rmNullStr(data("ＦＡＸ番号"))
            Me.txtIrainusi.Text = _db.rmNullStr(data("依頼主等"))
            Me.txtJikansitei.Text = _db.rmNullStr(data("時間指定"))
            Me.lblUnsoubin.Tag = _db.rmNullStr(data("運送便コード"))
            Me.lblUnsoubin.Text = _db.rmNullStr(data("運送便名"))
            Me.lblSeikyuCd.Text = _db.rmNullStr(data("請求先コード"))
            Me.txtSeikyuNm.Text = _db.rmNullStr(data("請求先名"))
            Me.lblShukkaGrpCd.Text = _db.rmNullStr(data("出荷先Ｇコード"))
            Me.txtShukkaGrpNm.Text = _db.rmNullStr(data("出荷先Ｇ名"))

            Me.txtShagaiBikou.Text = _db.rmNullStr(data("社外備考"))   '社外備考
            Me.txtShanaiBikou.Text = _db.rmNullStr(data("社内備考"))   '社内備考

            '荷札枚数
            Me.lblNihudaNum.Text = _db.rmNullDouble(data("総個数")).ToString("N0")
            '発送個数
            Me.lblHassouNum.Text = _db.rmNullDouble(data("総個数")).ToString("N0")
            'レスプリ枚数
            Me.lblResupuriNum.Text = _db.rmNullDouble(data("総個数")).ToString("N0")

            '税抜額
            Me.lblZeinukiSum.Text = _db.rmNullDouble(data("税抜額計"))

            '課税対象額
            Me.lblKazeiSum.Text = _db.rmNullDouble(data("課税対象額計"))

            '消費税額
            Me.lblTaxSum.Text = _db.rmNullDouble(data("消費税計"))

            '税込額
            Me.lblMoneySum.Text = _db.rmNullDouble(data("税込額計"))

            '合計金額
            Me.lblTotal.Text = _db.rmNullDouble(data("売上金額計"))

            '税算出区分
            _ZeiSanshutsu = _db.rmNullStr(data("税計算区分"))

            '税率
            _ZeiRitsu = _db.rmNullDouble(data("税率"))

            '更新日
            _UpdateTime = _db.rmNullDate(data("更新日"))

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '注文データ明細の表示
    Private Sub getChumonDataList()

        Dim strSql As String = String.Empty

        Try
            strSql = "SELECT "
            strSql = strSql & "   s.会社コード ,s.注文伝番, s.行番 , s.商品コード, s.商品名 ,s.荷姿形状 , s.冷凍区分 ,h.文字２ as 冷凍区分名 , s.課税区分,h2.文字２ as 課税区分名"
            strSql = strSql & ", s.入数 ,s.個数 ,s.数量, s.単位, s.注文単価, s.注文金額, s.明細備考, s.税抜額, s.課税対象額, s.消費税 ,s.梱包区分 ,h3.文字２ as 梱包区分名 "
            strSql = strSql & " FROM T11_CYMNDT s "
            strSql = strSql & "   left join M90_HANYO h on h.会社コード = s.会社コード And h.固定キー = '" & CommonConst.HANYO_REITOU_KBN & "' and h.可変キー = s.冷凍区分 "
            strSql = strSql & "   left join M90_HANYO h2 on h2.会社コード = s.会社コード and h2.固定キー = '" & CommonConst.HANYO_KAZEI_KBN & "' and h2.可変キー = s.課税区分 "
            strSql = strSql & "   left join M90_HANYO h3 on h3.会社コード = s.会社コード and h3.固定キー = '" & CommonConst.HANYO_KONPOU_KBN & "' and h3.可変キー = s.梱包区分 "
            strSql = strSql & " Where s.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and s.注文伝番 = '" & _DenpyoNo & "'"
            strSql = strSql & " order by s.行番 "

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(strSql, RS, reccnt)
            '描画の前にすべてクリアする
            Dim dss As DataSet = TryCast(Me.dgvIchiran.DataSource, DataSet)

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1

                If index <> 0 Then
                    dss.Tables(RS).Rows.InsertAt(dss.Tables(RS).NewRow, index + 1)
                End If

                '一覧セル
                Dim cells As DataGridViewCellCollection = dgvIchiran.Rows(index).Cells

                '明細データ
                Dim data As DataRow = ds.Tables(RS).Rows(index)

                cells(COLNO_NO).Value = (index + 1).ToString             '行番号
                cells(COLNO_ITEMCD).Value = _db.rmNullStr(data("商品コード"))
                cells(COLNO_ITEMNM).Value = _db.rmNullStr(data("商品名"))
                cells(COLNO_NISUGATA).Value = _db.rmNullStr(data("荷姿形状"))
                cells(COLNO_REICD).Value = _db.rmNullStr(data("冷凍区分"))
                cells(COLNO_REI).Value = _db.rmNullStr(data("冷凍区分名"))
                cells(COLNO_ZEIKBNCD).Value = _db.rmNullStr(data("課税区分"))
                cells(COLNO_ZEIKBN).Value = _db.rmNullStr(data("課税区分名"))
                cells(COLNO_IRISUU).Value = _db.rmNullDouble(data("入数"))
                cells(COLNO_KOSUU).Value = _db.rmNullDouble(data("個数"))
                cells(COLNO_SURYOU).Value = _db.rmNullDouble(data("数量"))
                cells(COLNO_TANNI).Value = _db.rmNullStr(data("単位"))
                cells(COLNO_URITANKA).Value = _db.rmNullDouble(data("注文単価"))
                cells(COLNO_URIKINGAKU).Value = _db.rmNullDouble(data("注文金額"))
                cells(COLNO_MEISAIBIKOU).Value = _db.rmNullStr(data("明細備考"))
                cells(COLNO_TAX_RATE).Value = _ZeiRitsu
                cells(CCOL_TAX_EXCLUSION).Value = _db.rmNullDouble(data("税抜額"))
                cells(CCOL_TAX_TAXABLE).Value = _db.rmNullDouble(data("課税対象額"))
                cells(CCOL_TAX_AMOUNT).Value = _db.rmNullDouble(data("消費税"))
                cells(COLNO_KONPOUCD).Value = _db.rmNullStr(data("梱包区分"))
                cells(COLNO_KONPOU).Value = _db.rmNullStr(data("梱包区分名"))

            Next

            '明細件数をセット
            lblMeisaiCnt.Text = dgvIchiran.RowCount

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    'ラベルの色を制御
    Private Sub LabelBackColorChange()
        Label4.BackColor = lblNyuuryokuMode.BackColor  '伝票番号
        Label1.BackColor = lblNyuuryokuMode.BackColor  '出荷日
        Label34.BackColor = lblNyuuryokuMode.BackColor  '着日

        Label21.BackColor = lblNyuuryokuMode.BackColor  '出荷先
        Label20.BackColor = lblNyuuryokuMode.BackColor  '住所
        Label16.BackColor = lblNyuuryokuMode.BackColor  '社外備考
        Label17.BackColor = lblNyuuryokuMode.BackColor  '社内備考

        Label5.BackColor = lblNyuuryokuMode.BackColor  '担当者
        Label6.BackColor = lblNyuuryokuMode.BackColor  '電話番号
        Label7.BackColor = lblNyuuryokuMode.BackColor  ' FAX番号

        Label9.BackColor = lblNyuuryokuMode.BackColor  '依頼主等
        Label10.BackColor = lblNyuuryokuMode.BackColor  '時間指定
        Label8.BackColor = lblNyuuryokuMode.BackColor  '運送便

        Label12.BackColor = lblNyuuryokuMode.BackColor  '請求先
        Label13.BackColor = lblNyuuryokuMode.BackColor  '出荷先GRP

        Label18.BackColor = lblNyuuryokuMode.BackColor  '荷札枚数
        Label19.BackColor = lblNyuuryokuMode.BackColor  '発送個数
        Label23.BackColor = lblNyuuryokuMode.BackColor  'レスプリ枚数
        Label32.BackColor = lblNyuuryokuMode.BackColor  '合計

        lblZeinuki.BackColor = lblNyuuryokuMode.BackColor  '税抜額
        lblKazei.BackColor = lblNyuuryokuMode.BackColor  '税抜額
        lblTax.BackColor = lblNyuuryokuMode.BackColor  '消費税
        lblMoney.BackColor = lblNyuuryokuMode.BackColor  '税込額

        dgvIchiran.ColumnHeadersDefaultCellStyle.BackColor = lblNyuuryokuMode.BackColor    '一覧

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

            '商品コード、入数
            Case COLNO_ITEMCD, COLNO_IRISUU

                '数量再計算(1行)
                RecalcSuryoOne(cells, rowIdx)

                '金額再計算(1行)
                RecalcKingakuOne(cells, rowIdx)

                '税関連再計算(1行)
                RecalcZeiOne(cells, rowIdx)

                '各合計額再計算
                RecalcTotal()

            '個数
            Case COLNO_KOSUU

                '数量再計算(1行)
                RecalcSuryoOne(cells, rowIdx)

                '金額再計算(1行)
                RecalcKingakuOne(cells, rowIdx)

                '税関連再計算(1行)
                RecalcZeiOne(cells, rowIdx)

                '各合計額再計算
                RecalcTotal()

                '各合計個数再計算
                RecalcTotalKosuu()

            '数量、売上単価
            Case COLNO_SURYOU, COLNO_URITANKA

                '金額再計算(1行)
                RecalcKingakuOne(cells, rowIdx)

                '税関連再計算(1行)
                RecalcZeiOne(cells, rowIdx)

                '各合計額再計算
                RecalcTotal()

            '税区分、売上金額
            Case COLNO_ZEIKBNCD, COLNO_URIKINGAKU

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

    '売上単価取得(1行)
    Private Sub getUriTankaOne(cells As DataGridViewCellCollection, rowIdx As Integer)

        '出荷先コードに値がない場合、または、
        '商品コードに値無し
        If (lblShukkaCd.Text = String.Empty) OrElse
           (_gh.getCellData(COLNO_ITEMCD, rowIdx) = String.Empty) Then
            '売上単価にゼロをセット
            cells(COLNO_URITANKA).Value = 0
            '処理終了
            Exit Sub
        End If

        Dim decSalesTanka As Decimal                   '売上単価
        Dim strTankaPth As String = String.Empty       '単価パターン

        '販売単価を取得
        _comLogc.GetSalesTanka(frmC01F10_Login.loginValue.BumonCD,
                               cells(COLNO_ITEMCD).Value,
                               lblShukkaCd.Text,
                               CDate(dtpChakuDt.Value).ToString("yyyy/MM/dd"),
                               decSalesTanka,
                               strTankaPth)

        '売上単価にセット
        cells(COLNO_URITANKA).Value = decSalesTanka

    End Sub

    '売上単価取得(全行)
    Private Sub getUriTankaAll()

        '一覧をループ
        For rowIdx As Integer = 0 To dgvIchiran.RowCount - 1

            '一覧セル
            Dim cells As DataGridViewCellCollection = dgvIchiran.Rows(rowIdx).Cells

            '金額再計算(1行)
            getUriTankaOne(cells, rowIdx)

        Next rowIdx

    End Sub

    '金額再計算(1行)
    Private Sub RecalcKingakuOne(cells As DataGridViewCellCollection, rowIdx As Integer)

        '出荷先コードに値がない場合
        If lblShukkaCd.Text = String.Empty Then
            '金額にゼロをセット
            cells(COLNO_URIKINGAKU).Value = 0
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
        If Decimal.TryParse(_gh.getCellData(COLNO_URITANKA, rowIdx), decTanka) Then
        Else
            cells(COLNO_URITANKA).Value = 0
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
        cells(COLNO_URIKINGAKU).Value = decKingaku

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

        '出荷先コードに値がない場合
        If lblShukkaCd.Text = String.Empty Then
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
        If Decimal.TryParse(_gh.getCellData(COLNO_URIKINGAKU, rowIdx), decKingaku) Then
        Else
            cells(COLNO_URIKINGAKU).Value = 0
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

        '出荷先コードに値がない場合
        If lblShukkaCd.Text = String.Empty Then
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
            decTotal = decTotal + _db.rmNullInt(cells(COLNO_URIKINGAKU).Value)
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

    '各合計個数再計算
    Private Sub RecalcTotalKosuu()

        Dim intKosuuSum As Integer = 0     '個数

        '一覧をループ
        For i As Integer = 0 To dgvIchiran.RowCount - 1

            '一覧セル
            Dim cells As DataGridViewCellCollection = dgvIchiran.Rows(i).Cells

            '個数計
            intKosuuSum = intKosuuSum + _db.rmNullInt(cells(COLNO_KOSUU).Value)
        Next i

        '荷札枚数
        lblNihudaNum.Text = intKosuuSum.ToString("N0")
        '発送個数
        lblHassouNum.Text = intKosuuSum.ToString("N0")
        'レスプリ枚数
        lblResupuriNum.Text = intKosuuSum.ToString("N0")

    End Sub

#End Region

#Region "コンポーネント"

    Public Event CellEnter As DataGridViewCellEventHandler

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents Label4 As Label
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents lblDenpyoNo As Label
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents Label1 As Label

    Friend WithEvents TableLayoutPanel6 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel7 As TableLayoutPanel
    Friend WithEvents lblNyuuryokuMode As Label
    Friend WithEvents TableLayoutPanel8 As TableLayoutPanel
    Friend WithEvents cmdPrint As Button
    Friend WithEvents cmdTouroku As Button
    Friend WithEvents TableLayoutPanel9 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel10 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel11 As TableLayoutPanel
    Friend WithEvents Label7 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents TableLayoutPanel12 As TableLayoutPanel
    Friend WithEvents lblUnsoubin As Label
    Friend WithEvents txtJikansitei As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents txtIrainusi As TextBox
    Friend WithEvents txtFaxNo As TextBox
    Friend WithEvents txtTelNo As TextBox
    Friend WithEvents txtTantousha As TextBox
    Friend WithEvents TableLayoutPanel13 As TableLayoutPanel
    Friend WithEvents Label13 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents lblSeikyuCd As Label

    Friend WithEvents lblShukkaGrpCd As Label
    Friend WithEvents txtSeikyuNm As TextBox
    Friend WithEvents TableLayoutPanel14 As TableLayoutPanel
    Friend WithEvents Label17 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents txtShukkaGrpNm As TextBox
    Friend WithEvents txtShagaiBikou As TextBox
    Friend WithEvents txtShanaiBikou As TextBox
    Friend WithEvents TableLayoutPanel15 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel17 As TableLayoutPanel
    Friend WithEvents Label20 As Label
    Friend WithEvents TableLayoutPanel18 As TableLayoutPanel
    Friend WithEvents txtPostalCd2 As TextBox
    Friend WithEvents TextBox11 As TextBox
    Friend WithEvents txtPostalCd1 As TextBox
    Friend WithEvents TableLayoutPanel19 As TableLayoutPanel
    Friend WithEvents txtAddress3 As TextBox
    Friend WithEvents txtAddress1 As TextBox
    Friend WithEvents txtAddress2 As TextBox

    Friend WithEvents TableLayoutPanel20 As TableLayoutPanel
    Friend WithEvents lblShukkaCd As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents txtShukkaNm As TextBox

    Friend WithEvents cmdModoru As Button
    Friend WithEvents TableLayoutPanel16 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel21 As TableLayoutPanel
    Friend WithEvents Label23 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents lblNihudaNum As Label
    Friend WithEvents lblHassouNum As Label
    Friend WithEvents lblResupuriNum As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents TableLayoutPanel22 As TableLayoutPanel
    Friend WithEvents Label32 As Label



    Friend WithEvents Column13 As DataGridViewTextBoxColumn
    Friend WithEvents Column14 As DataGridViewTextBoxColumn


    Friend WithEvents lblShoriMode As Label
    Friend WithEvents TableLayoutPanel23 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel24 As TableLayoutPanel
    Friend WithEvents Label34 As Label
    Friend WithEvents TableLayoutPanel25 As TableLayoutPanel
    Friend WithEvents lblMeisaiCnt As Label

    Friend WithEvents TableLayoutPanel26 As TableLayoutPanel
    Friend WithEvents cmdDelRow As Button
    Friend WithEvents cmdAddRow As Button
    Friend WithEvents cmdTopRow As Button

    Friend WithEvents dgvIchiran As CustomTabStopDataGridView.TabStop.UtilTabStopDataGridView
    Friend WithEvents cmdRowCopy As Button
    Friend WithEvents cmdSwapRowUp As Button
    Friend WithEvents cmdSwapRowDown As Button

    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents lblMoney As Label
    Friend WithEvents lblTax As Label
    Friend WithEvents lblKazei As Label
    Friend WithEvents lblZeinukiSum As Label
    Friend WithEvents lblKazeiSum As Label
    Friend WithEvents lblTaxSum As Label
    Friend WithEvents lblZeinuki As Label
    Friend WithEvents lblMoneySum As Label
    Friend WithEvents Label46 As Label

    Friend WithEvents lblShukkaDay As Label
    Friend WithEvents lblChakuDay As Label
    Friend WithEvents lblTotal As Label
    Friend WithEvents dtpChakuDt As CustomControl.NullableDateTimePicker
    Friend WithEvents dtpShukkaDt As CustomControl.NullableDateTimePicker
    Friend WithEvents cnNo As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnItemCd As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnItemNm As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnNisugata As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnReiKbn As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnZeiKbn As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnIrisuu As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnKosuu As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnSuryou As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnTanni As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnUriTanka As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnUriKingaku As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnMeisaiBikou As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnKonpou As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnReiCD As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnZeiKbnCD As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnKonpouCD As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnTaxRate As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnTaxExclusion As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnTaxAble As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnTaxAmount As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn

    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle47 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle69 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle48 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle49 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle50 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle51 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle52 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle53 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle54 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle55 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle56 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle57 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle58 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle59 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle60 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle61 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle62 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle63 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle64 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle65 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle66 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle67 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle68 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblShoriMode = New System.Windows.Forms.Label()
        Me.TableLayoutPanel23 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblDenpyoNo = New System.Windows.Forms.Label()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblShukkaDay = New System.Windows.Forms.Label()
        Me.dtpShukkaDt = New CustomControl.NullableDateTimePicker()
        Me.TableLayoutPanel24 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.dtpChakuDt = New CustomControl.NullableDateTimePicker()
        Me.lblChakuDay = New System.Windows.Forms.Label()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel14 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtShagaiBikou = New System.Windows.Forms.TextBox()
        Me.txtShanaiBikou = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel15 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel17 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel19 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtAddress1 = New System.Windows.Forms.TextBox()
        Me.txtAddress2 = New System.Windows.Forms.TextBox()
        Me.txtAddress3 = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel18 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtPostalCd2 = New System.Windows.Forms.TextBox()
        Me.TextBox11 = New System.Windows.Forms.TextBox()
        Me.txtPostalCd1 = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel20 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblShukkaCd = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtShukkaNm = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel9 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel10 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel11 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtFaxNo = New System.Windows.Forms.TextBox()
        Me.txtTelNo = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtTantousha = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel12 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblUnsoubin = New System.Windows.Forms.Label()
        Me.txtJikansitei = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtIrainusi = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel13 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtShukkaGrpNm = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblSeikyuCd = New System.Windows.Forms.Label()
        Me.lblShukkaGrpCd = New System.Windows.Forms.Label()
        Me.txtSeikyuNm = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel25 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.lblMeisaiCnt = New System.Windows.Forms.Label()
        Me.TableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdTouroku = New System.Windows.Forms.Button()
        Me.cmdModoru = New System.Windows.Forms.Button()
        Me.TableLayoutPanel16 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel22 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.lblNyuuryokuMode = New System.Windows.Forms.Label()
        Me.TableLayoutPanel26 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmdRowCopy = New System.Windows.Forms.Button()
        Me.cmdSwapRowUp = New System.Windows.Forms.Button()
        Me.cmdSwapRowDown = New System.Windows.Forms.Button()
        Me.cmdDelRow = New System.Windows.Forms.Button()
        Me.cmdAddRow = New System.Windows.Forms.Button()
        Me.cmdTopRow = New System.Windows.Forms.Button()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMoney = New System.Windows.Forms.Label()
        Me.lblTax = New System.Windows.Forms.Label()
        Me.lblKazei = New System.Windows.Forms.Label()
        Me.lblZeinukiSum = New System.Windows.Forms.Label()
        Me.lblKazeiSum = New System.Windows.Forms.Label()
        Me.lblTaxSum = New System.Windows.Forms.Label()
        Me.lblZeinuki = New System.Windows.Forms.Label()
        Me.lblMoneySum = New System.Windows.Forms.Label()
        Me.TableLayoutPanel21 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.lblNihudaNum = New System.Windows.Forms.Label()
        Me.lblHassouNum = New System.Windows.Forms.Label()
        Me.lblResupuriNum = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.dgvIchiran = New CustomTabStopDataGridView.TabStop.UtilTabStopDataGridView()
        Me.cnNo = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnItemCd = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnItemNm = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnNisugata = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnReiKbn = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnZeiKbn = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnIrisuu = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnKosuu = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnSuryou = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnTanni = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnUriTanka = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnUriKingaku = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnMeisaiBikou = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnKonpou = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnReiCD = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnZeiKbnCD = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnKonpouCD = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnTaxRate = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnTaxExclusion = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnTaxAble = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnTaxAmount = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel23.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.TableLayoutPanel24.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        Me.TableLayoutPanel7.SuspendLayout()
        Me.TableLayoutPanel14.SuspendLayout()
        Me.TableLayoutPanel15.SuspendLayout()
        Me.TableLayoutPanel17.SuspendLayout()
        Me.TableLayoutPanel19.SuspendLayout()
        Me.TableLayoutPanel18.SuspendLayout()
        Me.TableLayoutPanel20.SuspendLayout()
        Me.TableLayoutPanel9.SuspendLayout()
        Me.TableLayoutPanel10.SuspendLayout()
        Me.TableLayoutPanel11.SuspendLayout()
        Me.TableLayoutPanel12.SuspendLayout()
        Me.TableLayoutPanel13.SuspendLayout()
        Me.TableLayoutPanel25.SuspendLayout()
        Me.TableLayoutPanel8.SuspendLayout()
        Me.TableLayoutPanel16.SuspendLayout()
        Me.TableLayoutPanel22.SuspendLayout()
        Me.TableLayoutPanel26.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.TableLayoutPanel21.SuspendLayout()
        CType(Me.dgvIchiran, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel6, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel8, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel16, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.dgvIchiran, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 5
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1284, 782)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.lblShoriMode, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel23, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(1284, 78)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'lblShoriMode
        '
        Me.lblShoriMode.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblShoriMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblShoriMode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblShoriMode.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblShoriMode.Location = New System.Drawing.Point(1122, 19)
        Me.lblShoriMode.Name = "lblShoriMode"
        Me.lblShoriMode.Size = New System.Drawing.Size(131, 40)
        Me.lblShoriMode.TabIndex = 1
        Me.lblShoriMode.Text = "登録"
        Me.lblShoriMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel23
        '
        Me.TableLayoutPanel23.ColumnCount = 5
        Me.TableLayoutPanel23.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.0!))
        Me.TableLayoutPanel23.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.26489!))
        Me.TableLayoutPanel23.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.65628!))
        Me.TableLayoutPanel23.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.02383!))
        Me.TableLayoutPanel23.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.0!))
        Me.TableLayoutPanel23.Controls.Add(Me.TableLayoutPanel3, 0, 1)
        Me.TableLayoutPanel23.Controls.Add(Me.TableLayoutPanel4, 1, 1)
        Me.TableLayoutPanel23.Controls.Add(Me.TableLayoutPanel24, 2, 1)
        Me.TableLayoutPanel23.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel23.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel23.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel23.Name = "TableLayoutPanel23"
        Me.TableLayoutPanel23.RowCount = 2
        Me.TableLayoutPanel23.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel23.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel23.Size = New System.Drawing.Size(1091, 78)
        Me.TableLayoutPanel23.TabIndex = 0
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.Label4, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.lblDenpyoNo, 1, 1)
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 18)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(179, 55)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(0, 33)
        Me.Label4.Margin = New System.Windows.Forms.Padding(0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(100, 22)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "伝票番号"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDenpyoNo
        '
        Me.lblDenpyoNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblDenpyoNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblDenpyoNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDenpyoNo.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDenpyoNo.Location = New System.Drawing.Point(100, 33)
        Me.lblDenpyoNo.Margin = New System.Windows.Forms.Padding(0)
        Me.lblDenpyoNo.Name = "lblDenpyoNo"
        Me.lblDenpyoNo.Size = New System.Drawing.Size(79, 22)
        Me.lblDenpyoNo.TabIndex = 1
        Me.lblDenpyoNo.Text = "123456"
        Me.lblDenpyoNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 3
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.25!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.75!))
        Me.TableLayoutPanel4.Controls.Add(Me.Label1, 0, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.lblShukkaDay, 2, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.dtpShukkaDt, 1, 1)
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(188, 18)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 2
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(226, 55)
        Me.TableLayoutPanel4.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 33)
        Me.Label1.Margin = New System.Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 22)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "出荷日"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblShukkaDay
        '
        Me.lblShukkaDay.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblShukkaDay.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblShukkaDay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblShukkaDay.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblShukkaDay.Location = New System.Drawing.Point(206, 33)
        Me.lblShukkaDay.Margin = New System.Windows.Forms.Padding(0)
        Me.lblShukkaDay.Name = "lblShukkaDay"
        Me.lblShukkaDay.Size = New System.Drawing.Size(16, 22)
        Me.lblShukkaDay.TabIndex = 5
        Me.lblShukkaDay.Text = "火"
        Me.lblShukkaDay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpShukkaDt
        '
        Me.dtpShukkaDt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dtpShukkaDt.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtpShukkaDt.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpShukkaDt.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.dtpShukkaDt.Location = New System.Drawing.Point(100, 33)
        Me.dtpShukkaDt.Margin = New System.Windows.Forms.Padding(0)
        Me.dtpShukkaDt.Name = "dtpShukkaDt"
        Me.dtpShukkaDt.NullValue = String.Empty
        Me.dtpShukkaDt.Size = New System.Drawing.Size(106, 22)
        Me.dtpShukkaDt.TabIndex = 3
        Me.dtpShukkaDt.Value = New Date(2018, 3, 19, 18, 4, 53, 839)
        '
        'TableLayoutPanel24
        '
        Me.TableLayoutPanel24.ColumnCount = 3
        Me.TableLayoutPanel24.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.0!))
        Me.TableLayoutPanel24.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.5!))
        Me.TableLayoutPanel24.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.5!))
        Me.TableLayoutPanel24.Controls.Add(Me.Label34, 0, 1)
        Me.TableLayoutPanel24.Controls.Add(Me.dtpChakuDt, 1, 1)
        Me.TableLayoutPanel24.Controls.Add(Me.lblChakuDay, 2, 1)
        Me.TableLayoutPanel24.Location = New System.Drawing.Point(420, 18)
        Me.TableLayoutPanel24.Name = "TableLayoutPanel24"
        Me.TableLayoutPanel24.RowCount = 2
        Me.TableLayoutPanel24.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel24.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel24.Size = New System.Drawing.Size(234, 55)
        Me.TableLayoutPanel24.TabIndex = 2
        '
        'Label34
        '
        Me.Label34.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label34.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label34.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label34.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label34.Location = New System.Drawing.Point(0, 33)
        Me.Label34.Margin = New System.Windows.Forms.Padding(0)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(100, 22)
        Me.Label34.TabIndex = 0
        Me.Label34.Text = "着日"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpChakuDt
        '
        Me.dtpChakuDt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dtpChakuDt.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtpChakuDt.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpChakuDt.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.dtpChakuDt.Location = New System.Drawing.Point(100, 33)
        Me.dtpChakuDt.Margin = New System.Windows.Forms.Padding(0)
        Me.dtpChakuDt.Name = "dtpChakuDt"
        Me.dtpChakuDt.NullValue = String.Empty
        Me.dtpChakuDt.Size = New System.Drawing.Size(106, 22)
        Me.dtpChakuDt.TabIndex = 4
        Me.dtpChakuDt.Value = New Date(2018, 3, 19, 18, 4, 43, 283)
        '
        'lblChakuDay
        '
        Me.lblChakuDay.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblChakuDay.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblChakuDay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblChakuDay.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblChakuDay.Location = New System.Drawing.Point(206, 33)
        Me.lblChakuDay.Margin = New System.Windows.Forms.Padding(0)
        Me.lblChakuDay.Name = "lblChakuDay"
        Me.lblChakuDay.Size = New System.Drawing.Size(16, 22)
        Me.lblChakuDay.TabIndex = 5
        Me.lblChakuDay.Text = "火"
        Me.lblChakuDay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 2
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel6.Controls.Add(Me.TableLayoutPanel7, 0, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.TableLayoutPanel9, 1, 0)
        Me.TableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(3, 81)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 1
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(1278, 150)
        Me.TableLayoutPanel6.TabIndex = 1
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.ColumnCount = 1
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel7.Controls.Add(Me.TableLayoutPanel14, 0, 1)
        Me.TableLayoutPanel7.Controls.Add(Me.TableLayoutPanel15, 0, 0)
        Me.TableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel7.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 2
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(639, 150)
        Me.TableLayoutPanel7.TabIndex = 1
        '
        'TableLayoutPanel14
        '
        Me.TableLayoutPanel14.ColumnCount = 2
        Me.TableLayoutPanel14.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel14.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel14.Controls.Add(Me.Label17, 0, 1)
        Me.TableLayoutPanel14.Controls.Add(Me.Label16, 0, 0)
        Me.TableLayoutPanel14.Controls.Add(Me.txtShagaiBikou, 1, 0)
        Me.TableLayoutPanel14.Controls.Add(Me.txtShanaiBikou, 1, 1)
        Me.TableLayoutPanel14.Location = New System.Drawing.Point(0, 105)
        Me.TableLayoutPanel14.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel14.Name = "TableLayoutPanel14"
        Me.TableLayoutPanel14.RowCount = 2
        Me.TableLayoutPanel14.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel14.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel14.Size = New System.Drawing.Size(639, 44)
        Me.TableLayoutPanel14.TabIndex = 1
        '
        'Label17
        '
        Me.Label17.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label17.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label17.Location = New System.Drawing.Point(0, 22)
        Me.Label17.Margin = New System.Windows.Forms.Padding(0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(100, 22)
        Me.Label17.TabIndex = 2
        Me.Label17.Text = "社内備考"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label16
        '
        Me.Label16.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label16.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label16.Location = New System.Drawing.Point(0, 0)
        Me.Label16.Margin = New System.Windows.Forms.Padding(0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(100, 22)
        Me.Label16.TabIndex = 3
        Me.Label16.Text = "社外備考"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtShagaiBikou
        '
        Me.txtShagaiBikou.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShagaiBikou.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtShagaiBikou.Location = New System.Drawing.Point(100, 0)
        Me.txtShagaiBikou.Margin = New System.Windows.Forms.Padding(0)
        Me.txtShagaiBikou.MaxLength = 100
        Me.txtShagaiBikou.Name = "txtShagaiBikou"
        Me.txtShagaiBikou.Size = New System.Drawing.Size(537, 22)
        Me.txtShagaiBikou.TabIndex = 1
        '
        'txtShanaiBikou
        '
        Me.txtShanaiBikou.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShanaiBikou.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtShanaiBikou.Location = New System.Drawing.Point(100, 22)
        Me.txtShanaiBikou.Margin = New System.Windows.Forms.Padding(0)
        Me.txtShanaiBikou.MaxLength = 100
        Me.txtShanaiBikou.Name = "txtShanaiBikou"
        Me.txtShanaiBikou.Size = New System.Drawing.Size(537, 22)
        Me.txtShanaiBikou.TabIndex = 3
        '
        'TableLayoutPanel15
        '
        Me.TableLayoutPanel15.ColumnCount = 1
        Me.TableLayoutPanel15.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel15.Controls.Add(Me.TableLayoutPanel17, 0, 1)
        Me.TableLayoutPanel15.Controls.Add(Me.TableLayoutPanel20, 0, 0)
        Me.TableLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel15.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel15.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel15.Name = "TableLayoutPanel15"
        Me.TableLayoutPanel15.RowCount = 2
        Me.TableLayoutPanel15.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel15.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel15.Size = New System.Drawing.Size(639, 105)
        Me.TableLayoutPanel15.TabIndex = 0
        '
        'TableLayoutPanel17
        '
        Me.TableLayoutPanel17.ColumnCount = 3
        Me.TableLayoutPanel17.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel17.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel17.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel17.Controls.Add(Me.Label20, 0, 0)
        Me.TableLayoutPanel17.Controls.Add(Me.TableLayoutPanel19, 2, 0)
        Me.TableLayoutPanel17.Controls.Add(Me.TableLayoutPanel18, 1, 0)
        Me.TableLayoutPanel17.Location = New System.Drawing.Point(0, 22)
        Me.TableLayoutPanel17.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel17.Name = "TableLayoutPanel17"
        Me.TableLayoutPanel17.RowCount = 1
        Me.TableLayoutPanel17.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel17.Size = New System.Drawing.Size(639, 97)
        Me.TableLayoutPanel17.TabIndex = 1
        '
        'Label20
        '
        Me.Label20.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label20.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label20.Location = New System.Drawing.Point(0, 0)
        Me.Label20.Margin = New System.Windows.Forms.Padding(0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(100, 66)
        Me.Label20.TabIndex = 0
        Me.Label20.Text = "住所"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel19
        '
        Me.TableLayoutPanel19.ColumnCount = 1
        Me.TableLayoutPanel19.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel19.Controls.Add(Me.txtAddress1, 0, 0)
        Me.TableLayoutPanel19.Controls.Add(Me.txtAddress2, 0, 1)
        Me.TableLayoutPanel19.Controls.Add(Me.txtAddress3, 0, 2)
        Me.TableLayoutPanel19.Location = New System.Drawing.Point(184, 0)
        Me.TableLayoutPanel19.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel19.Name = "TableLayoutPanel19"
        Me.TableLayoutPanel19.RowCount = 3
        Me.TableLayoutPanel19.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel19.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel19.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel19.Size = New System.Drawing.Size(453, 69)
        Me.TableLayoutPanel19.TabIndex = 1242
        '
        'txtAddress1
        '
        Me.txtAddress1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtAddress1.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtAddress1.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtAddress1.Location = New System.Drawing.Point(0, 0)
        Me.txtAddress1.Margin = New System.Windows.Forms.Padding(0)
        Me.txtAddress1.MaxLength = 100
        Me.txtAddress1.Name = "txtAddress1"
        Me.txtAddress1.Size = New System.Drawing.Size(453, 22)
        Me.txtAddress1.TabIndex = 0
        '
        'txtAddress2
        '
        Me.txtAddress2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtAddress2.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtAddress2.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtAddress2.Location = New System.Drawing.Point(0, 22)
        Me.txtAddress2.Margin = New System.Windows.Forms.Padding(0)
        Me.txtAddress2.MaxLength = 100
        Me.txtAddress2.Name = "txtAddress2"
        Me.txtAddress2.Size = New System.Drawing.Size(453, 22)
        Me.txtAddress2.TabIndex = 1
        '
        'txtAddress3
        '
        Me.txtAddress3.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtAddress3.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtAddress3.Location = New System.Drawing.Point(0, 44)
        Me.txtAddress3.Margin = New System.Windows.Forms.Padding(0)
        Me.txtAddress3.MaxLength = 100
        Me.txtAddress3.Name = "txtAddress3"
        Me.txtAddress3.Size = New System.Drawing.Size(453, 22)
        Me.txtAddress3.TabIndex = 2
        '
        'TableLayoutPanel18
        '
        Me.TableLayoutPanel18.ColumnCount = 3
        Me.TableLayoutPanel18.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.80247!))
        Me.TableLayoutPanel18.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.04938!))
        Me.TableLayoutPanel18.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.91358!))
        Me.TableLayoutPanel18.Controls.Add(Me.txtPostalCd2, 2, 0)
        Me.TableLayoutPanel18.Controls.Add(Me.TextBox11, 1, 0)
        Me.TableLayoutPanel18.Controls.Add(Me.txtPostalCd1, 0, 0)
        Me.TableLayoutPanel18.Location = New System.Drawing.Point(100, 0)
        Me.TableLayoutPanel18.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel18.Name = "TableLayoutPanel18"
        Me.TableLayoutPanel18.RowCount = 1
        Me.TableLayoutPanel18.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel18.Size = New System.Drawing.Size(84, 69)
        Me.TableLayoutPanel18.TabIndex = 1
        '
        'txtPostalCd2
        '
        Me.txtPostalCd2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtPostalCd2.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtPostalCd2.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtPostalCd2.Location = New System.Drawing.Point(43, 0)
        Me.txtPostalCd2.Margin = New System.Windows.Forms.Padding(0)
        Me.txtPostalCd2.MaxLength = 4
        Me.txtPostalCd2.Name = "txtPostalCd2"
        Me.txtPostalCd2.Size = New System.Drawing.Size(41, 22)
        Me.txtPostalCd2.TabIndex = 2
        '
        'TextBox11
        '
        Me.TextBox11.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox11.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf
        Me.TextBox11.Location = New System.Drawing.Point(30, 0)
        Me.TextBox11.Margin = New System.Windows.Forms.Padding(0)
        Me.TextBox11.MaxLength = 20
        Me.TextBox11.Name = "TextBox11"
        Me.TextBox11.ReadOnly = True
        Me.TextBox11.Size = New System.Drawing.Size(13, 22)
        Me.TextBox11.TabIndex = 1
        Me.TextBox11.TabStop = False
        Me.TextBox11.Text = "-"
        Me.TextBox11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtPostalCd1
        '
        Me.txtPostalCd1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtPostalCd1.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtPostalCd1.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtPostalCd1.Location = New System.Drawing.Point(0, 0)
        Me.txtPostalCd1.Margin = New System.Windows.Forms.Padding(0)
        Me.txtPostalCd1.MaxLength = 3
        Me.txtPostalCd1.Name = "txtPostalCd1"
        Me.txtPostalCd1.Size = New System.Drawing.Size(30, 22)
        Me.txtPostalCd1.TabIndex = 0
        '
        'TableLayoutPanel20
        '
        Me.TableLayoutPanel20.ColumnCount = 3
        Me.TableLayoutPanel20.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel20.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel20.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel20.Controls.Add(Me.lblShukkaCd, 0, 0)
        Me.TableLayoutPanel20.Controls.Add(Me.Label21, 0, 0)
        Me.TableLayoutPanel20.Controls.Add(Me.txtShukkaNm, 2, 0)
        Me.TableLayoutPanel20.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel20.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel20.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel20.Name = "TableLayoutPanel20"
        Me.TableLayoutPanel20.RowCount = 1
        Me.TableLayoutPanel20.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel20.Size = New System.Drawing.Size(639, 22)
        Me.TableLayoutPanel20.TabIndex = 0
        '
        'lblShukkaCd
        '
        Me.lblShukkaCd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblShukkaCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblShukkaCd.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblShukkaCd.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblShukkaCd.Location = New System.Drawing.Point(100, 0)
        Me.lblShukkaCd.Margin = New System.Windows.Forms.Padding(0)
        Me.lblShukkaCd.Name = "lblShukkaCd"
        Me.lblShukkaCd.Size = New System.Drawing.Size(84, 22)
        Me.lblShukkaCd.TabIndex = 1
        Me.lblShukkaCd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label21
        '
        Me.Label21.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label21.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label21.Location = New System.Drawing.Point(0, 0)
        Me.Label21.Margin = New System.Windows.Forms.Padding(0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(100, 22)
        Me.Label21.TabIndex = 0
        Me.Label21.Text = "出荷先"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtShukkaNm
        '
        Me.txtShukkaNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtShukkaNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShukkaNm.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtShukkaNm.Location = New System.Drawing.Point(184, 0)
        Me.txtShukkaNm.Margin = New System.Windows.Forms.Padding(0)
        Me.txtShukkaNm.MaxLength = 100
        Me.txtShukkaNm.Name = "txtShukkaNm"
        Me.txtShukkaNm.Size = New System.Drawing.Size(453, 22)
        Me.txtShukkaNm.TabIndex = 2
        '
        'TableLayoutPanel9
        '
        Me.TableLayoutPanel9.ColumnCount = 2
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 82.15962!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.84038!))
        Me.TableLayoutPanel9.Controls.Add(Me.TableLayoutPanel10, 0, 0)
        Me.TableLayoutPanel9.Controls.Add(Me.TableLayoutPanel13, 0, 1)
        Me.TableLayoutPanel9.Controls.Add(Me.TableLayoutPanel25, 1, 1)
        Me.TableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel9.Location = New System.Drawing.Point(639, 0)
        Me.TableLayoutPanel9.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel9.Name = "TableLayoutPanel9"
        Me.TableLayoutPanel9.RowCount = 2
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel9.Size = New System.Drawing.Size(639, 150)
        Me.TableLayoutPanel9.TabIndex = 1
        '
        'TableLayoutPanel10
        '
        Me.TableLayoutPanel10.ColumnCount = 2
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.60305!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.39695!))
        Me.TableLayoutPanel10.Controls.Add(Me.TableLayoutPanel11, 0, 0)
        Me.TableLayoutPanel10.Controls.Add(Me.TableLayoutPanel12, 1, 0)
        Me.TableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel10.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel10.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel10.Name = "TableLayoutPanel10"
        Me.TableLayoutPanel10.RowCount = 1
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel10.Size = New System.Drawing.Size(524, 105)
        Me.TableLayoutPanel10.TabIndex = 0
        '
        'TableLayoutPanel11
        '
        Me.TableLayoutPanel11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel11.ColumnCount = 2
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel11.Controls.Add(Me.txtFaxNo, 1, 2)
        Me.TableLayoutPanel11.Controls.Add(Me.txtTelNo, 1, 1)
        Me.TableLayoutPanel11.Controls.Add(Me.Label7, 0, 2)
        Me.TableLayoutPanel11.Controls.Add(Me.Label5, 0, 0)
        Me.TableLayoutPanel11.Controls.Add(Me.Label6, 0, 1)
        Me.TableLayoutPanel11.Controls.Add(Me.txtTantousha, 1, 0)
        Me.TableLayoutPanel11.Location = New System.Drawing.Point(6, 0)
        Me.TableLayoutPanel11.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel11.Name = "TableLayoutPanel11"
        Me.TableLayoutPanel11.RowCount = 3
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel11.Size = New System.Drawing.Size(211, 68)
        Me.TableLayoutPanel11.TabIndex = 0
        '
        'txtFaxNo
        '
        Me.txtFaxNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtFaxNo.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtFaxNo.Location = New System.Drawing.Point(100, 44)
        Me.txtFaxNo.Margin = New System.Windows.Forms.Padding(0)
        Me.txtFaxNo.MaxLength = 15
        Me.txtFaxNo.Name = "txtFaxNo"
        Me.txtFaxNo.Size = New System.Drawing.Size(110, 22)
        Me.txtFaxNo.TabIndex = 5
        '
        'txtTelNo
        '
        Me.txtTelNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTelNo.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtTelNo.Location = New System.Drawing.Point(100, 22)
        Me.txtTelNo.Margin = New System.Windows.Forms.Padding(0)
        Me.txtTelNo.MaxLength = 15
        Me.txtTelNo.Name = "txtTelNo"
        Me.txtTelNo.Size = New System.Drawing.Size(110, 22)
        Me.txtTelNo.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(0, 44)
        Me.Label7.Margin = New System.Windows.Forms.Padding(0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(100, 22)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = " FAX番号"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(0, 0)
        Me.Label5.Margin = New System.Windows.Forms.Padding(0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(100, 22)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "担当者"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(0, 22)
        Me.Label6.Margin = New System.Windows.Forms.Padding(0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(100, 22)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "電話番号"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTantousha
        '
        Me.txtTantousha.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTantousha.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtTantousha.Location = New System.Drawing.Point(100, 0)
        Me.txtTantousha.Margin = New System.Windows.Forms.Padding(0)
        Me.txtTantousha.MaxLength = 30
        Me.txtTantousha.Name = "txtTantousha"
        Me.txtTantousha.Size = New System.Drawing.Size(110, 22)
        Me.txtTantousha.TabIndex = 3
        '
        'TableLayoutPanel12
        '
        Me.TableLayoutPanel12.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel12.ColumnCount = 2
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel12.Controls.Add(Me.lblUnsoubin, 1, 2)
        Me.TableLayoutPanel12.Controls.Add(Me.txtJikansitei, 1, 1)
        Me.TableLayoutPanel12.Controls.Add(Me.Label8, 0, 2)
        Me.TableLayoutPanel12.Controls.Add(Me.Label9, 0, 0)
        Me.TableLayoutPanel12.Controls.Add(Me.Label10, 0, 1)
        Me.TableLayoutPanel12.Controls.Add(Me.txtIrainusi, 1, 0)
        Me.TableLayoutPanel12.Location = New System.Drawing.Point(224, 0)
        Me.TableLayoutPanel12.Margin = New System.Windows.Forms.Padding(0, 0, 1, 0)
        Me.TableLayoutPanel12.Name = "TableLayoutPanel12"
        Me.TableLayoutPanel12.RowCount = 3
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel12.Size = New System.Drawing.Size(299, 68)
        Me.TableLayoutPanel12.TabIndex = 1
        '
        'lblUnsoubin
        '
        Me.lblUnsoubin.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblUnsoubin.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblUnsoubin.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUnsoubin.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnsoubin.Location = New System.Drawing.Point(100, 44)
        Me.lblUnsoubin.Margin = New System.Windows.Forms.Padding(0)
        Me.lblUnsoubin.Name = "lblUnsoubin"
        Me.lblUnsoubin.Size = New System.Drawing.Size(200, 22)
        Me.lblUnsoubin.TabIndex = 5
        Me.lblUnsoubin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtJikansitei
        '
        Me.txtJikansitei.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtJikansitei.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtJikansitei.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtJikansitei.Location = New System.Drawing.Point(100, 22)
        Me.txtJikansitei.Margin = New System.Windows.Forms.Padding(0)
        Me.txtJikansitei.MaxLength = 30
        Me.txtJikansitei.Name = "txtJikansitei"
        Me.txtJikansitei.Size = New System.Drawing.Size(200, 22)
        Me.txtJikansitei.TabIndex = 4
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(0, 44)
        Me.Label8.Margin = New System.Windows.Forms.Padding(0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(100, 22)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "運送便"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label9.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(0, 0)
        Me.Label9.Margin = New System.Windows.Forms.Padding(0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(100, 22)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "依頼主等"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label10.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.Location = New System.Drawing.Point(0, 22)
        Me.Label10.Margin = New System.Windows.Forms.Padding(0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(100, 22)
        Me.Label10.TabIndex = 1
        Me.Label10.Text = "時間指定"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtIrainusi
        '
        Me.txtIrainusi.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtIrainusi.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtIrainusi.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtIrainusi.Location = New System.Drawing.Point(100, 0)
        Me.txtIrainusi.Margin = New System.Windows.Forms.Padding(0)
        Me.txtIrainusi.MaxLength = 30
        Me.txtIrainusi.Name = "txtIrainusi"
        Me.txtIrainusi.Size = New System.Drawing.Size(200, 22)
        Me.txtIrainusi.TabIndex = 3
        '
        'TableLayoutPanel13
        '
        Me.TableLayoutPanel13.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel13.ColumnCount = 3
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.4!))
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.2!))
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.4!))
        Me.TableLayoutPanel13.Controls.Add(Me.txtShukkaGrpNm, 2, 1)
        Me.TableLayoutPanel13.Controls.Add(Me.Label13, 0, 1)
        Me.TableLayoutPanel13.Controls.Add(Me.Label12, 0, 0)
        Me.TableLayoutPanel13.Controls.Add(Me.lblSeikyuCd, 1, 0)
        Me.TableLayoutPanel13.Controls.Add(Me.lblShukkaGrpCd, 1, 1)
        Me.TableLayoutPanel13.Controls.Add(Me.txtSeikyuNm, 2, 0)
        Me.TableLayoutPanel13.Location = New System.Drawing.Point(0, 105)
        Me.TableLayoutPanel13.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel13.Name = "TableLayoutPanel13"
        Me.TableLayoutPanel13.RowCount = 2
        Me.TableLayoutPanel13.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel13.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel13.Size = New System.Drawing.Size(524, 44)
        Me.TableLayoutPanel13.TabIndex = 1
        '
        'txtShukkaGrpNm
        '
        Me.txtShukkaGrpNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtShukkaGrpNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShukkaGrpNm.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtShukkaGrpNm.Location = New System.Drawing.Point(218, 22)
        Me.txtShukkaGrpNm.Margin = New System.Windows.Forms.Padding(0)
        Me.txtShukkaGrpNm.MaxLength = 100
        Me.txtShukkaGrpNm.Name = "txtShukkaGrpNm"
        Me.txtShukkaGrpNm.Size = New System.Drawing.Size(306, 22)
        Me.txtShukkaGrpNm.TabIndex = 5
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label13.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label13.Location = New System.Drawing.Point(6, 22)
        Me.Label13.Margin = New System.Windows.Forms.Padding(0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(100, 22)
        Me.Label13.TabIndex = 3
        Me.Label13.Text = "出荷先GRP"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label12.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.Location = New System.Drawing.Point(6, 0)
        Me.Label12.Margin = New System.Windows.Forms.Padding(0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(100, 22)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "請求先"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSeikyuCd
        '
        Me.lblSeikyuCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblSeikyuCd.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSeikyuCd.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSeikyuCd.Location = New System.Drawing.Point(106, 0)
        Me.lblSeikyuCd.Margin = New System.Windows.Forms.Padding(0)
        Me.lblSeikyuCd.Name = "lblSeikyuCd"
        Me.lblSeikyuCd.Size = New System.Drawing.Size(110, 22)
        Me.lblSeikyuCd.TabIndex = 1
        Me.lblSeikyuCd.Text = " 030900"
        Me.lblSeikyuCd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblShukkaGrpCd
        '
        Me.lblShukkaGrpCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblShukkaGrpCd.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblShukkaGrpCd.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblShukkaGrpCd.Location = New System.Drawing.Point(106, 22)
        Me.lblShukkaGrpCd.Margin = New System.Windows.Forms.Padding(0)
        Me.lblShukkaGrpCd.Name = "lblShukkaGrpCd"
        Me.lblShukkaGrpCd.Size = New System.Drawing.Size(110, 22)
        Me.lblShukkaGrpCd.TabIndex = 4
        Me.lblShukkaGrpCd.Text = " 059700"
        Me.lblShukkaGrpCd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtSeikyuNm
        '
        Me.txtSeikyuNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSeikyuNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSeikyuNm.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtSeikyuNm.Location = New System.Drawing.Point(218, 0)
        Me.txtSeikyuNm.Margin = New System.Windows.Forms.Padding(0)
        Me.txtSeikyuNm.MaxLength = 100
        Me.txtSeikyuNm.Name = "txtSeikyuNm"
        Me.txtSeikyuNm.Size = New System.Drawing.Size(306, 22)
        Me.txtSeikyuNm.TabIndex = 2
        '
        'TableLayoutPanel25
        '
        Me.TableLayoutPanel25.ColumnCount = 2
        Me.TableLayoutPanel25.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel25.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21.0!))
        Me.TableLayoutPanel25.Controls.Add(Me.Label46, 1, 0)
        Me.TableLayoutPanel25.Controls.Add(Me.lblMeisaiCnt, 0, 0)
        Me.TableLayoutPanel25.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel25.Location = New System.Drawing.Point(524, 105)
        Me.TableLayoutPanel25.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel25.Name = "TableLayoutPanel25"
        Me.TableLayoutPanel25.RowCount = 1
        Me.TableLayoutPanel25.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel25.Size = New System.Drawing.Size(115, 45)
        Me.TableLayoutPanel25.TabIndex = 1
        '
        'Label46
        '
        Me.Label46.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label46.AutoSize = True
        Me.Label46.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label46.Location = New System.Drawing.Point(97, 27)
        Me.Label46.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(15, 15)
        Me.Label46.TabIndex = 4
        Me.Label46.Text = "件"
        '
        'lblMeisaiCnt
        '
        Me.lblMeisaiCnt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMeisaiCnt.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblMeisaiCnt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMeisaiCnt.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMeisaiCnt.Location = New System.Drawing.Point(5, 22)
        Me.lblMeisaiCnt.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.lblMeisaiCnt.Name = "lblMeisaiCnt"
        Me.lblMeisaiCnt.Size = New System.Drawing.Size(89, 22)
        Me.lblMeisaiCnt.TabIndex = 1
        Me.lblMeisaiCnt.Text = "2"
        Me.lblMeisaiCnt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel8
        '
        Me.TableLayoutPanel8.ColumnCount = 6
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3.0!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.0!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3.0!))
        Me.TableLayoutPanel8.Controls.Add(Me.cmdPrint, 1, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.cmdTouroku, 3, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.cmdModoru, 4, 0)
        Me.TableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel8.Location = New System.Drawing.Point(3, 705)
        Me.TableLayoutPanel8.Name = "TableLayoutPanel8"
        Me.TableLayoutPanel8.RowCount = 1
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.Size = New System.Drawing.Size(1278, 74)
        Me.TableLayoutPanel8.TabIndex = 4
        '
        'cmdPrint
        '
        Me.cmdPrint.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdPrint.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!)
        Me.cmdPrint.Location = New System.Drawing.Point(42, 15)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.Size = New System.Drawing.Size(119, 43)
        Me.cmdPrint.TabIndex = 0
        Me.cmdPrint.Text = "再印刷(&P)"
        Me.cmdPrint.UseVisualStyleBackColor = True
        '
        'cmdTouroku
        '
        Me.cmdTouroku.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdTouroku.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!)
        Me.cmdTouroku.Location = New System.Drawing.Point(986, 18)
        Me.cmdTouroku.Name = "cmdTouroku"
        Me.cmdTouroku.Size = New System.Drawing.Size(119, 37)
        Me.cmdTouroku.TabIndex = 25
        Me.cmdTouroku.Text = "登録(&G)"
        Me.cmdTouroku.UseVisualStyleBackColor = True
        '
        'cmdModoru
        '
        Me.cmdModoru.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdModoru.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!)
        Me.cmdModoru.Location = New System.Drawing.Point(1113, 19)
        Me.cmdModoru.Name = "cmdModoru"
        Me.cmdModoru.Size = New System.Drawing.Size(119, 36)
        Me.cmdModoru.TabIndex = 26
        Me.cmdModoru.Text = "戻る(&B)"
        Me.cmdModoru.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel16
        '
        Me.TableLayoutPanel16.ColumnCount = 5
        Me.TableLayoutPanel16.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.11581!))
        Me.TableLayoutPanel16.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.02347!))
        Me.TableLayoutPanel16.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.5446!))
        Me.TableLayoutPanel16.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.5446!))
        Me.TableLayoutPanel16.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.69327!))
        Me.TableLayoutPanel16.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel16.Controls.Add(Me.TableLayoutPanel22, 3, 0)
        Me.TableLayoutPanel16.Controls.Add(Me.lblNyuuryokuMode, 4, 0)
        Me.TableLayoutPanel16.Controls.Add(Me.TableLayoutPanel26, 0, 0)
        Me.TableLayoutPanel16.Controls.Add(Me.TableLayoutPanel5, 2, 0)
        Me.TableLayoutPanel16.Controls.Add(Me.TableLayoutPanel21, 1, 0)
        Me.TableLayoutPanel16.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel16.Location = New System.Drawing.Point(3, 596)
        Me.TableLayoutPanel16.Name = "TableLayoutPanel16"
        Me.TableLayoutPanel16.RowCount = 1
        Me.TableLayoutPanel16.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel16.Size = New System.Drawing.Size(1278, 103)
        Me.TableLayoutPanel16.TabIndex = 3
        '
        'TableLayoutPanel22
        '
        Me.TableLayoutPanel22.ColumnCount = 2
        Me.TableLayoutPanel22.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel22.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel22.Controls.Add(Me.lblTotal, 1, 0)
        Me.TableLayoutPanel22.Controls.Add(Me.Label32, 0, 0)
        Me.TableLayoutPanel22.Location = New System.Drawing.Point(868, 3)
        Me.TableLayoutPanel22.Name = "TableLayoutPanel22"
        Me.TableLayoutPanel22.RowCount = 1
        Me.TableLayoutPanel22.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel22.Size = New System.Drawing.Size(231, 22)
        Me.TableLayoutPanel22.TabIndex = 1
        '
        'lblTotal
        '
        Me.lblTotal.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotal.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTotal.Location = New System.Drawing.Point(100, 0)
        Me.lblTotal.Margin = New System.Windows.Forms.Padding(0)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(130, 22)
        Me.lblTotal.TabIndex = 1
        Me.lblTotal.Text = "0"
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label32.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label32.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label32.Location = New System.Drawing.Point(0, 0)
        Me.Label32.Margin = New System.Windows.Forms.Padding(0)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(100, 22)
        Me.Label32.TabIndex = 0
        Me.Label32.Text = "合計金額"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblNyuuryokuMode
        '
        Me.lblNyuuryokuMode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNyuuryokuMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblNyuuryokuMode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNyuuryokuMode.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNyuuryokuMode.Location = New System.Drawing.Point(1102, 3)
        Me.lblNyuuryokuMode.Margin = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.lblNyuuryokuMode.Name = "lblNyuuryokuMode"
        Me.lblNyuuryokuMode.Size = New System.Drawing.Size(176, 67)
        Me.lblNyuuryokuMode.TabIndex = 1301
        Me.lblNyuuryokuMode.Text = "売上"
        Me.lblNyuuryokuMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel26
        '
        Me.TableLayoutPanel26.ColumnCount = 3
        Me.TableLayoutPanel26.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel26.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TableLayoutPanel26.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TableLayoutPanel26.Controls.Add(Me.cmdRowCopy, 0, 1)
        Me.TableLayoutPanel26.Controls.Add(Me.cmdSwapRowUp, 1, 1)
        Me.TableLayoutPanel26.Controls.Add(Me.cmdSwapRowDown, 2, 1)
        Me.TableLayoutPanel26.Controls.Add(Me.cmdDelRow, 2, 0)
        Me.TableLayoutPanel26.Controls.Add(Me.cmdAddRow, 1, 0)
        Me.TableLayoutPanel26.Controls.Add(Me.cmdTopRow, 0, 0)
        Me.TableLayoutPanel26.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel26.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel26.Name = "TableLayoutPanel26"
        Me.TableLayoutPanel26.RowCount = 2
        Me.TableLayoutPanel26.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel26.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.TableLayoutPanel26.Size = New System.Drawing.Size(430, 97)
        Me.TableLayoutPanel26.TabIndex = 0
        '
        'cmdRowCopy
        '
        Me.cmdRowCopy.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cmdRowCopy.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!)
        Me.cmdRowCopy.Location = New System.Drawing.Point(12, 41)
        Me.cmdRowCopy.Name = "cmdRowCopy"
        Me.cmdRowCopy.Size = New System.Drawing.Size(119, 29)
        Me.cmdRowCopy.TabIndex = 3
        Me.cmdRowCopy.Text = "行複写(&R)"
        Me.cmdRowCopy.UseVisualStyleBackColor = True
        '
        'cmdSwapRowUp
        '
        Me.cmdSwapRowUp.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cmdSwapRowUp.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!)
        Me.cmdSwapRowUp.Location = New System.Drawing.Point(155, 41)
        Me.cmdSwapRowUp.Name = "cmdSwapRowUp"
        Me.cmdSwapRowUp.Size = New System.Drawing.Size(119, 29)
        Me.cmdSwapRowUp.TabIndex = 4
        Me.cmdSwapRowUp.Text = "行移動↑(&M)"
        Me.cmdSwapRowUp.UseVisualStyleBackColor = True
        '
        'cmdSwapRowDown
        '
        Me.cmdSwapRowDown.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cmdSwapRowDown.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!)
        Me.cmdSwapRowDown.Location = New System.Drawing.Point(298, 41)
        Me.cmdSwapRowDown.Name = "cmdSwapRowDown"
        Me.cmdSwapRowDown.Size = New System.Drawing.Size(119, 29)
        Me.cmdSwapRowDown.TabIndex = 5
        Me.cmdSwapRowDown.Text = "行移動↓(&N)"
        Me.cmdSwapRowDown.UseVisualStyleBackColor = True
        '
        'cmdDelRow
        '
        Me.cmdDelRow.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cmdDelRow.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdDelRow.Location = New System.Drawing.Point(298, 3)
        Me.cmdDelRow.Name = "cmdDelRow"
        Me.cmdDelRow.Size = New System.Drawing.Size(119, 29)
        Me.cmdDelRow.TabIndex = 2
        Me.cmdDelRow.Text = "行削除(&D)"
        Me.cmdDelRow.UseVisualStyleBackColor = True
        '
        'cmdAddRow
        '
        Me.cmdAddRow.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cmdAddRow.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdAddRow.Location = New System.Drawing.Point(155, 3)
        Me.cmdAddRow.Name = "cmdAddRow"
        Me.cmdAddRow.Size = New System.Drawing.Size(119, 29)
        Me.cmdAddRow.TabIndex = 1
        Me.cmdAddRow.Text = "行追加(&I)"
        Me.cmdAddRow.UseVisualStyleBackColor = True
        '
        'cmdTopRow
        '
        Me.cmdTopRow.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cmdTopRow.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdTopRow.Location = New System.Drawing.Point(12, 3)
        Me.cmdTopRow.Name = "cmdTopRow"
        Me.cmdTopRow.Size = New System.Drawing.Size(119, 29)
        Me.cmdTopRow.TabIndex = 0
        Me.cmdTopRow.Text = "先頭へ(&T)"
        Me.cmdTopRow.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 3
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.lblMoney, 0, 3)
        Me.TableLayoutPanel5.Controls.Add(Me.lblTax, 0, 2)
        Me.TableLayoutPanel5.Controls.Add(Me.lblKazei, 0, 1)
        Me.TableLayoutPanel5.Controls.Add(Me.lblZeinukiSum, 1, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.lblKazeiSum, 1, 1)
        Me.TableLayoutPanel5.Controls.Add(Me.lblTaxSum, 1, 2)
        Me.TableLayoutPanel5.Controls.Add(Me.lblZeinuki, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.lblMoneySum, 1, 3)
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(631, 3)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 4
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(231, 88)
        Me.TableLayoutPanel5.TabIndex = 1302
        '
        'lblMoney
        '
        Me.lblMoney.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblMoney.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMoney.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMoney.Location = New System.Drawing.Point(0, 66)
        Me.lblMoney.Margin = New System.Windows.Forms.Padding(0)
        Me.lblMoney.Name = "lblMoney"
        Me.lblMoney.Size = New System.Drawing.Size(100, 22)
        Me.lblMoney.TabIndex = 6
        Me.lblMoney.Text = "税込額"
        Me.lblMoney.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTax
        '
        Me.lblTax.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblTax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTax.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTax.Location = New System.Drawing.Point(0, 44)
        Me.lblTax.Margin = New System.Windows.Forms.Padding(0)
        Me.lblTax.Name = "lblTax"
        Me.lblTax.Size = New System.Drawing.Size(100, 22)
        Me.lblTax.TabIndex = 4
        Me.lblTax.Text = "消費税"
        Me.lblTax.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblKazei
        '
        Me.lblKazei.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblKazei.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblKazei.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKazei.Location = New System.Drawing.Point(0, 22)
        Me.lblKazei.Margin = New System.Windows.Forms.Padding(0)
        Me.lblKazei.Name = "lblKazei"
        Me.lblKazei.Size = New System.Drawing.Size(100, 22)
        Me.lblKazei.TabIndex = 2
        Me.lblKazei.Text = "課税対象額"
        Me.lblKazei.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblZeinukiSum
        '
        Me.lblZeinukiSum.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblZeinukiSum.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblZeinukiSum.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZeinukiSum.Location = New System.Drawing.Point(100, 0)
        Me.lblZeinukiSum.Margin = New System.Windows.Forms.Padding(0)
        Me.lblZeinukiSum.Name = "lblZeinukiSum"
        Me.lblZeinukiSum.Size = New System.Drawing.Size(130, 22)
        Me.lblZeinukiSum.TabIndex = 1
        Me.lblZeinukiSum.Text = "0"
        Me.lblZeinukiSum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblKazeiSum
        '
        Me.lblKazeiSum.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblKazeiSum.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblKazeiSum.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKazeiSum.Location = New System.Drawing.Point(100, 22)
        Me.lblKazeiSum.Margin = New System.Windows.Forms.Padding(0)
        Me.lblKazeiSum.Name = "lblKazeiSum"
        Me.lblKazeiSum.Size = New System.Drawing.Size(130, 22)
        Me.lblKazeiSum.TabIndex = 3
        Me.lblKazeiSum.Text = "0"
        Me.lblKazeiSum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTaxSum
        '
        Me.lblTaxSum.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblTaxSum.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTaxSum.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTaxSum.Location = New System.Drawing.Point(100, 44)
        Me.lblTaxSum.Margin = New System.Windows.Forms.Padding(0)
        Me.lblTaxSum.Name = "lblTaxSum"
        Me.lblTaxSum.Size = New System.Drawing.Size(130, 22)
        Me.lblTaxSum.TabIndex = 5
        Me.lblTaxSum.Text = "0"
        Me.lblTaxSum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblZeinuki
        '
        Me.lblZeinuki.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblZeinuki.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblZeinuki.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZeinuki.Location = New System.Drawing.Point(0, 0)
        Me.lblZeinuki.Margin = New System.Windows.Forms.Padding(0)
        Me.lblZeinuki.Name = "lblZeinuki"
        Me.lblZeinuki.Size = New System.Drawing.Size(100, 22)
        Me.lblZeinuki.TabIndex = 0
        Me.lblZeinuki.Text = "税抜額"
        Me.lblZeinuki.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblMoneySum
        '
        Me.lblMoneySum.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblMoneySum.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMoneySum.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMoneySum.Location = New System.Drawing.Point(100, 66)
        Me.lblMoneySum.Margin = New System.Windows.Forms.Padding(0)
        Me.lblMoneySum.Name = "lblMoneySum"
        Me.lblMoneySum.Size = New System.Drawing.Size(130, 22)
        Me.lblMoneySum.TabIndex = 7
        Me.lblMoneySum.Text = "0"
        Me.lblMoneySum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TableLayoutPanel21
        '
        Me.TableLayoutPanel21.ColumnCount = 2
        Me.TableLayoutPanel21.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel21.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel21.Controls.Add(Me.Label23, 0, 2)
        Me.TableLayoutPanel21.Controls.Add(Me.Label19, 0, 1)
        Me.TableLayoutPanel21.Controls.Add(Me.lblNihudaNum, 1, 0)
        Me.TableLayoutPanel21.Controls.Add(Me.lblHassouNum, 1, 1)
        Me.TableLayoutPanel21.Controls.Add(Me.lblResupuriNum, 1, 2)
        Me.TableLayoutPanel21.Controls.Add(Me.Label18, 0, 0)
        Me.TableLayoutPanel21.Location = New System.Drawing.Point(439, 3)
        Me.TableLayoutPanel21.Name = "TableLayoutPanel21"
        Me.TableLayoutPanel21.RowCount = 3
        Me.TableLayoutPanel21.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel21.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel21.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel21.Size = New System.Drawing.Size(186, 67)
        Me.TableLayoutPanel21.TabIndex = 0
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label23.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label23.Location = New System.Drawing.Point(0, 44)
        Me.Label23.Margin = New System.Windows.Forms.Padding(0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(100, 22)
        Me.Label23.TabIndex = 4
        Me.Label23.Text = "レスプリ枚数"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label19.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label19.Location = New System.Drawing.Point(0, 22)
        Me.Label19.Margin = New System.Windows.Forms.Padding(0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(100, 22)
        Me.Label19.TabIndex = 2
        Me.Label19.Text = "発送個数"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblNihudaNum
        '
        Me.lblNihudaNum.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblNihudaNum.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNihudaNum.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNihudaNum.Location = New System.Drawing.Point(100, 0)
        Me.lblNihudaNum.Margin = New System.Windows.Forms.Padding(0)
        Me.lblNihudaNum.Name = "lblNihudaNum"
        Me.lblNihudaNum.Size = New System.Drawing.Size(84, 22)
        Me.lblNihudaNum.TabIndex = 1
        Me.lblNihudaNum.Text = "0"
        Me.lblNihudaNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblHassouNum
        '
        Me.lblHassouNum.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblHassouNum.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblHassouNum.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHassouNum.Location = New System.Drawing.Point(100, 22)
        Me.lblHassouNum.Margin = New System.Windows.Forms.Padding(0)
        Me.lblHassouNum.Name = "lblHassouNum"
        Me.lblHassouNum.Size = New System.Drawing.Size(84, 22)
        Me.lblHassouNum.TabIndex = 3
        Me.lblHassouNum.Text = "0"
        Me.lblHassouNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblResupuriNum
        '
        Me.lblResupuriNum.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblResupuriNum.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblResupuriNum.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblResupuriNum.Location = New System.Drawing.Point(100, 44)
        Me.lblResupuriNum.Margin = New System.Windows.Forms.Padding(0)
        Me.lblResupuriNum.Name = "lblResupuriNum"
        Me.lblResupuriNum.Size = New System.Drawing.Size(84, 22)
        Me.lblResupuriNum.TabIndex = 5
        Me.lblResupuriNum.Text = "0"
        Me.lblResupuriNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label18.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label18.Location = New System.Drawing.Point(0, 0)
        Me.Label18.Margin = New System.Windows.Forms.Padding(0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(100, 22)
        Me.Label18.TabIndex = 0
        Me.Label18.Text = "荷札枚数"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dgvIchiran
        '
        Me.dgvIchiran.AllowUserToAddRows = False
        Me.dgvIchiran.AllowUserToDeleteRows = False
        Me.dgvIchiran.AllowUserToResizeRows = False
        DataGridViewCellStyle47.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle47.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle47.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle47.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle47.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle47.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle47.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvIchiran.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle47
        Me.dgvIchiran.ColumnHeadersHeight = 25
        Me.dgvIchiran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvIchiran.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cnNo, Me.cnItemCd, Me.cnItemNm, Me.cnNisugata, Me.cnReiKbn, Me.cnZeiKbn, Me.cnIrisuu, Me.cnKosuu, Me.cnSuryou, Me.cnTanni, Me.cnUriTanka, Me.cnUriKingaku, Me.cnMeisaiBikou, Me.cnKonpou, Me.cnReiCD, Me.cnZeiKbnCD, Me.cnKonpouCD, Me.cnTaxRate, Me.cnTaxExclusion, Me.cnTaxAble, Me.cnTaxAmount})
        DataGridViewCellStyle69.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle69.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle69.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        DataGridViewCellStyle69.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle69.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle69.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle69.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvIchiran.DefaultCellStyle = DataGridViewCellStyle69
        Me.dgvIchiran.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvIchiran.EnableHeadersVisualStyles = False
        Me.dgvIchiran.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvIchiran.Location = New System.Drawing.Point(3, 237)
        Me.dgvIchiran.Name = "dgvIchiran"
        Me.dgvIchiran.RowHeadersVisible = False
        Me.dgvIchiran.RowHeadersWidth = 25
        Me.dgvIchiran.RowTemplate.Height = 18
        Me.dgvIchiran.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvIchiran.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvIchiran.ShowCellToolTips = False
        Me.dgvIchiran.Size = New System.Drawing.Size(1278, 353)
        Me.dgvIchiran.TabIndex = 2
        '
        'cnNo
        '
        Me.cnNo.DataPropertyName = "dtNo"
        DataGridViewCellStyle48.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle48.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle48.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        DataGridViewCellStyle48.ForeColor = System.Drawing.Color.Black
        Me.cnNo.DefaultCellStyle = DataGridViewCellStyle48
        Me.cnNo.HeaderText = "No."
        Me.cnNo.Name = "cnNo"
        Me.cnNo.ReadOnly = True
        Me.cnNo.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnNo.TabStop = False
        Me.cnNo.Width = 30
        '
        'cnItemCd
        '
        Me.cnItemCd.DataPropertyName = "dtItemCd"
        DataGridViewCellStyle49.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle49.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle49.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        DataGridViewCellStyle49.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle49.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle49.SelectionForeColor = System.Drawing.Color.Black
        Me.cnItemCd.DefaultCellStyle = DataGridViewCellStyle49
        Me.cnItemCd.HeaderText = "商品CD"
        Me.cnItemCd.MaxInputLength = 5
        Me.cnItemCd.Name = "cnItemCd"
        Me.cnItemCd.ReadOnly = True
        Me.cnItemCd.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnItemCd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnItemCd.TabStop = True
        Me.cnItemCd.Width = 70
        '
        'cnItemNm
        '
        Me.cnItemNm.DataPropertyName = "dtItemNm"
        DataGridViewCellStyle50.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.cnItemNm.DefaultCellStyle = DataGridViewCellStyle50
        Me.cnItemNm.HeaderText = "商品名"
        Me.cnItemNm.Name = "cnItemNm"
        Me.cnItemNm.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnItemNm.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnItemNm.TabStop = True
        Me.cnItemNm.Width = 280
        '
        'cnNisugata
        '
        Me.cnNisugata.DataPropertyName = "dtNisugata"
        DataGridViewCellStyle51.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.cnNisugata.DefaultCellStyle = DataGridViewCellStyle51
        Me.cnNisugata.HeaderText = "荷姿・形状"
        Me.cnNisugata.MaxInputLength = 50
        Me.cnNisugata.Name = "cnNisugata"
        Me.cnNisugata.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnNisugata.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnNisugata.TabStop = True
        Me.cnNisugata.Width = 110
        '
        'cnReiKbn
        '
        Me.cnReiKbn.DataPropertyName = "dtReiKbn"
        DataGridViewCellStyle52.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle52.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle52.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        DataGridViewCellStyle52.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle52.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle52.SelectionForeColor = System.Drawing.Color.Black
        Me.cnReiKbn.DefaultCellStyle = DataGridViewCellStyle52
        Me.cnReiKbn.HeaderText = "冷"
        Me.cnReiKbn.Name = "cnReiKbn"
        Me.cnReiKbn.ReadOnly = True
        Me.cnReiKbn.TabStop = True
        Me.cnReiKbn.Width = 30
        '
        'cnZeiKbn
        '
        Me.cnZeiKbn.DataPropertyName = "dtZeiKbn"
        DataGridViewCellStyle53.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle53.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle53.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        DataGridViewCellStyle53.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle53.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle53.SelectionForeColor = System.Drawing.Color.Black
        Me.cnZeiKbn.DefaultCellStyle = DataGridViewCellStyle53
        Me.cnZeiKbn.HeaderText = "税"
        Me.cnZeiKbn.Name = "cnZeiKbn"
        Me.cnZeiKbn.ReadOnly = True
        Me.cnZeiKbn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnZeiKbn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnZeiKbn.TabStop = True
        Me.cnZeiKbn.Width = 30
        '
        'cnIrisuu
        '
        Me.cnIrisuu.DataPropertyName = "dtIrisuu"
        DataGridViewCellStyle54.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle54.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle54.Format = "N2"
        Me.cnIrisuu.DefaultCellStyle = DataGridViewCellStyle54
        Me.cnIrisuu.HeaderText = "入数"
        Me.cnIrisuu.MaxInputLength = 11
        Me.cnIrisuu.Name = "cnIrisuu"
        Me.cnIrisuu.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnIrisuu.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnIrisuu.TabStop = True
        Me.cnIrisuu.Width = 70
        '
        'cnKosuu
        '
        Me.cnKosuu.DataPropertyName = "dtKosuu"
        DataGridViewCellStyle55.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle55.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle55.Format = "N0"
        Me.cnKosuu.DefaultCellStyle = DataGridViewCellStyle55
        Me.cnKosuu.HeaderText = "個数"
        Me.cnKosuu.MaxInputLength = 6
        Me.cnKosuu.Name = "cnKosuu"
        Me.cnKosuu.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnKosuu.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnKosuu.TabStop = True
        Me.cnKosuu.Width = 70
        '
        'cnSuryou
        '
        Me.cnSuryou.DataPropertyName = "dtSuryou"
        DataGridViewCellStyle56.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle56.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle56.Format = "N2"
        Me.cnSuryou.DefaultCellStyle = DataGridViewCellStyle56
        Me.cnSuryou.HeaderText = "数量"
        Me.cnSuryou.MaxInputLength = 11
        Me.cnSuryou.Name = "cnSuryou"
        Me.cnSuryou.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnSuryou.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnSuryou.TabStop = True
        Me.cnSuryou.Width = 90
        '
        'cnTanni
        '
        Me.cnTanni.DataPropertyName = "dtTanni"
        DataGridViewCellStyle57.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle57.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle57.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        DataGridViewCellStyle57.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle57.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle57.SelectionForeColor = System.Drawing.Color.Black
        Me.cnTanni.DefaultCellStyle = DataGridViewCellStyle57
        Me.cnTanni.HeaderText = "単位"
        Me.cnTanni.Name = "cnTanni"
        Me.cnTanni.ReadOnly = True
        Me.cnTanni.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnTanni.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnTanni.TabStop = True
        Me.cnTanni.Width = 50
        '
        'cnUriTanka
        '
        Me.cnUriTanka.DataPropertyName = "dtUriTanka"
        DataGridViewCellStyle58.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle58.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle58.Format = "N2"
        Me.cnUriTanka.DefaultCellStyle = DataGridViewCellStyle58
        Me.cnUriTanka.HeaderText = "売上単価"
        Me.cnUriTanka.MaxInputLength = 15
        Me.cnUriTanka.Name = "cnUriTanka"
        Me.cnUriTanka.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnUriTanka.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnUriTanka.TabStop = True
        '
        'cnUriKingaku
        '
        Me.cnUriKingaku.DataPropertyName = "dtUriKingaku"
        DataGridViewCellStyle59.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle59.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle59.Format = "N0"
        Me.cnUriKingaku.DefaultCellStyle = DataGridViewCellStyle59
        Me.cnUriKingaku.HeaderText = "売上金額"
        Me.cnUriKingaku.MaxInputLength = 12
        Me.cnUriKingaku.Name = "cnUriKingaku"
        Me.cnUriKingaku.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnUriKingaku.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnUriKingaku.TabStop = True
        '
        'cnMeisaiBikou
        '
        Me.cnMeisaiBikou.DataPropertyName = "dtMeisaiBikou"
        DataGridViewCellStyle60.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.cnMeisaiBikou.DefaultCellStyle = DataGridViewCellStyle60
        Me.cnMeisaiBikou.HeaderText = "明細備考"
        Me.cnMeisaiBikou.MaxInputLength = 100
        Me.cnMeisaiBikou.Name = "cnMeisaiBikou"
        Me.cnMeisaiBikou.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnMeisaiBikou.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnMeisaiBikou.TabStop = True
        Me.cnMeisaiBikou.Width = 175
        '
        'cnKonpou
        '
        Me.cnKonpou.DataPropertyName = "dtKonpou"
        DataGridViewCellStyle61.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle61.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle61.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        DataGridViewCellStyle61.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle61.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle61.SelectionForeColor = System.Drawing.Color.Black
        Me.cnKonpou.DefaultCellStyle = DataGridViewCellStyle61
        Me.cnKonpou.HeaderText = "梱包"
        Me.cnKonpou.Name = "cnKonpou"
        Me.cnKonpou.ReadOnly = True
        Me.cnKonpou.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnKonpou.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnKonpou.TabStop = True
        Me.cnKonpou.Width = 50
        '
        'cnReiCD
        '
        Me.cnReiCD.DataPropertyName = "dtReiCD"
        DataGridViewCellStyle62.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.cnReiCD.DefaultCellStyle = DataGridViewCellStyle62
        Me.cnReiCD.HeaderText = "冷凍区分"
        Me.cnReiCD.Name = "cnReiCD"
        Me.cnReiCD.TabStop = True
        Me.cnReiCD.Visible = False
        '
        'cnZeiKbnCD
        '
        Me.cnZeiKbnCD.DataPropertyName = "dtZeiKbnCD"
        DataGridViewCellStyle63.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.cnZeiKbnCD.DefaultCellStyle = DataGridViewCellStyle63
        Me.cnZeiKbnCD.HeaderText = "課税区分"
        Me.cnZeiKbnCD.Name = "cnZeiKbnCD"
        Me.cnZeiKbnCD.TabStop = True
        Me.cnZeiKbnCD.Visible = False
        '
        'cnKonpouCD
        '
        Me.cnKonpouCD.DataPropertyName = "dtKonpouCD"
        DataGridViewCellStyle64.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.cnKonpouCD.DefaultCellStyle = DataGridViewCellStyle64
        Me.cnKonpouCD.HeaderText = "梱包区分"
        Me.cnKonpouCD.Name = "cnKonpouCD"
        Me.cnKonpouCD.TabStop = True
        Me.cnKonpouCD.Visible = False
        '
        'cnTaxRate
        '
        Me.cnTaxRate.DataPropertyName = "dtTaxRate"
        DataGridViewCellStyle65.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.cnTaxRate.DefaultCellStyle = DataGridViewCellStyle65
        Me.cnTaxRate.HeaderText = "税率"
        Me.cnTaxRate.Name = "cnTaxRate"
        Me.cnTaxRate.TabStop = True
        Me.cnTaxRate.Visible = False
        '
        'cnTaxExclusion
        '
        Me.cnTaxExclusion.DataPropertyName = "dtTaxExclusion"
        DataGridViewCellStyle66.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnTaxExclusion.DefaultCellStyle = DataGridViewCellStyle66
        Me.cnTaxExclusion.HeaderText = "税抜額"
        Me.cnTaxExclusion.Name = "cnTaxExclusion"
        Me.cnTaxExclusion.TabStop = True
        Me.cnTaxExclusion.Visible = False
        '
        'cnTaxAble
        '
        Me.cnTaxAble.DataPropertyName = "dtTaxAble"
        DataGridViewCellStyle67.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnTaxAble.DefaultCellStyle = DataGridViewCellStyle67
        Me.cnTaxAble.HeaderText = "課税対象額"
        Me.cnTaxAble.Name = "cnTaxAble"
        Me.cnTaxAble.TabStop = True
        Me.cnTaxAble.Visible = False
        '
        'cnTaxAmount
        '
        Me.cnTaxAmount.DataPropertyName = "dtTaxAmount"
        DataGridViewCellStyle68.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnTaxAmount.DefaultCellStyle = DataGridViewCellStyle68
        Me.cnTaxAmount.HeaderText = "消費税額"
        Me.cnTaxAmount.Name = "cnTaxAmount"
        Me.cnTaxAmount.TabStop = True
        Me.cnTaxAmount.Visible = False
        '
        'frmH01F60_Chumon
        '
        Me.ClientSize = New System.Drawing.Size(1284, 782)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "frmH01F60_Chumon"
        Me.Text = "注文業務（H01F60）"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel23.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel24.ResumeLayout(False)
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.TableLayoutPanel7.ResumeLayout(False)
        Me.TableLayoutPanel14.ResumeLayout(False)
        Me.TableLayoutPanel14.PerformLayout()
        Me.TableLayoutPanel15.ResumeLayout(False)
        Me.TableLayoutPanel17.ResumeLayout(False)
        Me.TableLayoutPanel19.ResumeLayout(False)
        Me.TableLayoutPanel19.PerformLayout()
        Me.TableLayoutPanel18.ResumeLayout(False)
        Me.TableLayoutPanel18.PerformLayout()
        Me.TableLayoutPanel20.ResumeLayout(False)
        Me.TableLayoutPanel20.PerformLayout()
        Me.TableLayoutPanel9.ResumeLayout(False)
        Me.TableLayoutPanel10.ResumeLayout(False)
        Me.TableLayoutPanel11.ResumeLayout(False)
        Me.TableLayoutPanel11.PerformLayout()
        Me.TableLayoutPanel12.ResumeLayout(False)
        Me.TableLayoutPanel12.PerformLayout()
        Me.TableLayoutPanel13.ResumeLayout(False)
        Me.TableLayoutPanel13.PerformLayout()
        Me.TableLayoutPanel25.ResumeLayout(False)
        Me.TableLayoutPanel25.PerformLayout()
        Me.TableLayoutPanel8.ResumeLayout(False)
        Me.TableLayoutPanel16.ResumeLayout(False)
        Me.TableLayoutPanel22.ResumeLayout(False)
        Me.TableLayoutPanel26.ResumeLayout(False)
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel21.ResumeLayout(False)
        CType(Me.dgvIchiran, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

End Class