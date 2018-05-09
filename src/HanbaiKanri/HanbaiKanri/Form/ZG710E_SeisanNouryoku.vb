'===============================================================================
'
'　北日本電線株式会社
'　　（システム名）在庫計画システム
'　　（処理機能名）生産能力設定画面
'    （フォームID）ZG710E_SeisanNouryoku
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   鈴木        2010/11/25                 新規              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView

Public Class ZG710E_SeisanNouryoku
    Inherits System.Windows.Forms.Form

#Region "リテラル値定義"

    'PG制御文字
    Private Const RS As String = "RecSet"                       'データセットテーブル名
    Private Const N As String = ControlChars.NewLine            '改行文字

    '一覧データバインド名
    Private Const COLDT_KOUTEI As String = "dtKoutei"            '工程
    Private Const COLDT_MACHINENAME As String = "dtMachineName"  '機械略記号
    Private Const COLDT_TUUJOUDAY As String = "dtTuujouDay"      '通常稼働（日）
    Private Const COLDT_TUUJOUMON As String = "dtTuujouMon"      '通常稼働（月）
    Private Const COLDT_DOYOUDAY As String = "dtDoyouDay"        '土曜休出（日）
    Private Const COLDT_DOYOUMON As String = "dtDoyouMon"        '土曜休出（月）
    Private Const COLDT_NICHIYOUDAY As String = "dtNichiyouDay"  '日曜休出（日）
    Private Const COLDT_NICHIYOUMON As String = "dtNichiyouMon"  '日曜休出（月）
    Private Const COLDT_KEISANTI As String = "dtKeisanti"        '計算MCH
    Private Const COLDT_TYOUSEI As String = "dtTyousei"          '調整MCH
    Private Const COLDT_TEKIYOU As String = "dtTekiyou"          '生産能力MCH

    Private Const COLDT_TUUJOU As String = "dtTuujou"      '通常稼働
    Private Const COLDT_DOYOU As String = "dtDoyou"        '土曜休出
    Private Const COLDT_NICHIYOU As String = "dtNichiyou"  '日曜休出
    Private Const COLDT_BUMON As String = "dtBumon"        '製造部門

    '一覧グリッド名
    Private Const COLCN_KOUTEI As String = "cnKoutei"             '工程
    Private Const COLCN_MACHINENAME As String = "cnMachineName"   '機械略記号
    Private Const COLCN_TUUJOUDAY As String = "cnTuujouDay"       '通常稼働（日）
    Private Const COLCN_TUUJOUMON As String = "cnTuujouMon"       '通常稼働（月）
    Private Const COLCN_DOYOUDAY As String = "cnDoyouDay"         '土曜休出（日）
    Private Const COLCN_DOYOUMON As String = "cnDoyouMon"         '土曜休出（月）
    Private Const COLCN_NICHIYOUDAY As String = "cnNichiyouDay"   '日曜休出（日）
    Private Const COLCN_NICHIYOUMON As String = "cnNichiyouMon"   '日曜休出（月）
    Private Const COLCN_KEISANTI As String = "cnKeisanti"         '計算MCH
    Private Const COLCN_TYOUSEI As String = "cnTyousei"           '調整MCH
    Private Const COLCN_TEKIYOU As String = "cnTekiyou"           '生産能力MCH

    '一覧グリッド名
    Private Const COLNO_KOUTEI As Integer = 0          '工程
    Private Const COLNO_MACHINENAME As Integer = 1     '機械略記号
    Private Const COLNO_TUUJOUDAY As Integer = 2       '通常稼働（日）
    Private Const COLNO_TUUJOUMON As Integer = 3       '通常稼働（月）
    Private Const COLNO_DOYOUDAY As Integer = 4        '土曜休出（日）
    Private Const COLNO_DOYOUMON As Integer = 5        '土曜休出（月）
    Private Const COLNO_NICHIYOUDAY As Integer = 6     '日曜休出（日）
    Private Const COLNO_NICHIYOUMON As Integer = 7     '日曜休出（月）
    Private Const COLNO_KEISANTI As Integer = 8        '計算MCH
    Private Const COLNO_TYOUSEI As Integer = 9         '調整MCH
    Private Const COLNO_TEKIYOU As Integer = 10        '生産能力MCH

    Private Const PGID As String = "ZG710E"
    Private Const BMNCD_DENSEN As String = "3"         '製造部門（電線）
    Private Const BMNCD_TUUSIN As String = "1"         '製造部門（通信）
    Private Const TYOUSEIMAX As Single = 999.9         '調整MCHの最大値
    Private Const TYOUSEIMIN As Single = -999.9        '調整MCHの最小値
    Private Const KOUTEI As String = "12"              '工程の固定キー
    Private Const MAXSORT As Integer = 99999999        'ソート番号の最大値

#End Region

#Region "メンバ変数定義"

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As IfRturnSeisanNouryoku
    Private _oyaKensu As Integer
    Private _oldRowIndex As Integer = -1            '選択行の背景色を変更するためのフラグを宣言
    Private _colorCtlFlg As Boolean = False         '選択行の背景色を変更するためのフラグを宣言
    Private _chkCellVO As UtilDgvChkCellVO          '一覧の入力制限用
    Private _errSet As UtilDataGridViewHandler.dgvErrSet                'エラー発生時にフォーカスするセル位置
    Private _nyuuryokuErrFlg As Boolean = False                         '入力エラー有無フラグ

    '-->2010.12/12 add by takagi 
    '-------------------------------------------------------------------------------
    '   オーバーライドプロパティで×ボタンだけを無効にする(ControlBoxはTrueのまま使用可能)
    '-------------------------------------------------------------------------------
    Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
        Get
            Const CS_NOCLOSE As Integer = &H200

            Dim tmpCreateParams As System.Windows.Forms.CreateParams = MyBase.CreateParams
            tmpCreateParams.ClassStyle = tmpCreateParams.ClassStyle Or CS_NOCLOSE

            Return tmpCreateParams
        End Get
    End Property
    '<--2010.12/12 add by takagi 
#End Region

#Region "コンストラクタ"

    '-------------------------------------------------------------------------------
    'コンストラクタ（Privateにして、外からは呼べないようにする）
    '-------------------------------------------------------------------------------
    Private Sub New()

        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

    End Sub

    '-------------------------------------------------------------------------------
    'コンストラクタ　メニューから呼ばれる
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByVal prmForm As IfRturnSeisanNouryoku, ByVal prmOyaKensu As Integer)

        Call Me.New()

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _parentForm = prmForm                                               '親フォーム
        _oyaKensu = prmOyaKensu
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示

    End Sub

#End Region

#Region "Formイベント"

    '-------------------------------------------------------------------------------
    '　処理開始イベント
    '-------------------------------------------------------------------------------
    Private Sub ZG710E_SeisanNouryoku_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            '描画関係の設定
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           'サイズが変更されたときに、コントロールがコントロール自体を再描画するかどうかを示す値を設定
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '描画はバッファで実行され、完了後に、結果が画面に出力されるよう設定
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  'コントロールは、画面に直接ではなく、まずバッファに描画されます。これにより、ちらつきを抑えることができます。
            Me.SetStyle(ControlStyles.UserPaint, True)              'コントロールは、オペレーティング システムによってではなく、独自に描画されるよう設定
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   'コントロールはウィンドウ メッセージ WM_ERASEBKGND を無視するように設定

            Dim optionStr As String = ComBiz.getFormTitleOption(_db, _msgHd)
            If Not "".Equals(optionStr) Then Me.Text = Me.Text & " - " & optionStr 'タイトルオプション表示

            '画面表示
            Call initForm()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

#End Region

#Region "ボタンイベント"

    '------------------------------------------------------------------------------------------------------
    '　戻るボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnModoru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModoru.Click

        '自画面を終了し、負荷山積結果確認（工程）画面に戻る。
        _parentForm.myShow()
        _parentForm.myActivate()

        Me.Close()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　MCH算出ボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnSansyutu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSansyutu.Click

        Try
            '稼働日の入力チェック
            Call checkWorkingDay()

            'MCH算出
            Dim dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanNouryoku)
            Dim maxRow As Integer = dgv.getMaxRow()         'データグリッドの最大行数
            Dim dTuujou As Single = CSng(txtDTuujou.Text)   '（電線）通常稼働日
            Dim dDoyou As Single = CSng(txtDDoyou.Text)     '（電線）土曜稼働日
            Dim dNitiyou As Single = CSng(txtDNitiyou.Text) '（電線）日曜稼働日
            Dim tTuujou As Single = CSng(txtTTuujou.Text)   '（通信）通常稼働日
            Dim tDoyou As Single = CSng(txtTDoyou.Text)     '（通信）土曜稼働日
            Dim tNitiyou As Single = CSng(txtTNitiyou.Text) '（通信）日曜稼働日
            Dim tyouseiMch As Single                        '調整MCH
            Dim bumon As String                             '製造部門
            Dim tuujou As Single                            '通常稼働時間（月）
            Dim doyou As Single                             '土曜稼働時間（月）
            Dim nitiyou As Single                           '日曜稼働時間（月）

            For i As Integer = 0 To maxRow - 1
                '稼働時（月間）列の値チェック
                If String.Empty.Equals(_db.rmNullStr(dgv.getCellData(COLDT_TUUJOUDAY, i))) Or _
                    String.Empty.Equals(_db.rmNullStr(dgv.getCellData(COLDT_DOYOUDAY, i))) Or _
                    String.Empty.Equals(_db.rmNullStr(dgv.getCellData(COLDT_NICHIYOUDAY, i))) Then
                    '計算MCHが空になる可能性があるなら計算は行わない
                    Continue For
                End If

                '調整MCHを画面から取得する
                If String.Empty.Equals(_db.rmNullStr(dgv.getCellData(COLDT_TYOUSEI, i))) Then
                    tyouseiMch = 0.0
                Else
                    tyouseiMch = _db.rmNullStr(dgv.getCellData(COLDT_TYOUSEI, i))
                End If

                '機械名略称より製造部門を取得
                bumon = _db.rmNullStr(dgv.getCellData(COLDT_MACHINENAME, i)).Substring(0, 1)

                If BMNCD_DENSEN.Equals(bumon) Then
                    tuujou = dTuujou * CSng(_db.rmNullStr(dgv.getCellData(COLDT_TUUJOUDAY, i)))
                    doyou = dDoyou * CSng(_db.rmNullStr(dgv.getCellData(COLDT_DOYOUDAY, i)))
                    nitiyou = dNitiyou * CSng(_db.rmNullStr(dgv.getCellData(COLDT_NICHIYOUDAY, i)))
                Else
                    tuujou = tTuujou * CSng(_db.rmNullStr(dgv.getCellData(COLDT_TUUJOUDAY, i)))
                    doyou = tDoyou * CSng(_db.rmNullStr(dgv.getCellData(COLDT_DOYOUDAY, i)))
                    nitiyou = tNitiyou * CSng(_db.rmNullStr(dgv.getCellData(COLDT_NICHIYOUDAY, i)))
                End If

                'データグリッドに格納
                dgv.setCellData(COLDT_TUUJOUMON, i, tuujou)
                dgv.setCellData(COLDT_DOYOUMON, i, doyou)
                dgv.setCellData(COLDT_NICHIYOUMON, i, nitiyou)
                dgv.setCellData(COLDT_KEISANTI, i, tuujou + doyou + nitiyou)
                dgv.setCellData(COLDT_TEKIYOU, i, tuujou + doyou + nitiyou + tyouseiMch)
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　登録ボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnTouroku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTouroku.Click
        Try
            '一覧のデータ数の取得
            Dim dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanNouryoku)
            Dim lMaxCnt As Long = dgv.getMaxRow 'データグリッドの最大行数

            '登録チェック
            Call checkTouroku(lMaxCnt)

            '登録確認メッセージ
            If _oyaKensu > 0 Then
                '親フォームの一覧件数が0より大きい場合（親画面再表示の旨のメッセージ表示）
                Dim rtn As DialogResult = _msgHd.dspMSG("confirmRegistReDisp")
                If rtn <> Windows.Forms.DialogResult.Yes Then
                    Exit Sub
                End If
            Else
                '親フォームの一覧件数が0の場合
                Dim rtn As DialogResult = _msgHd.dspMSG("confirmRegist")
                If rtn <> Windows.Forms.DialogResult.Yes Then
                    Exit Sub
                End If
            End If

            'トランザクション開始
            _db.beginTran()

            'マウスカーソル砂時計
            Me.Cursor = Cursors.WaitCursor

            'DB登録処理
            Call registDB()

            'トランザクション終了
            _db.commitTran()

            'マウスカーソル矢印
            Me.Cursor = Cursors.Arrow

            _parentForm.setRegist(True)

            '完了メッセージ
            _msgHd.dspMSG("completeInsert")

            '親フォーム表示
            _parentForm.myShow()
            _parentForm.myActivate()
            Me.Close()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        Finally
            If _db.isTransactionOpen = True Then
                _db.rollbackTran()                          'ロールバック
            End If
            'マウスカーソル矢印
            Me.Cursor = Cursors.Arrow
        End Try
    End Sub

#End Region

#Region "データグリッド関数"

    '-------------------------------------------------------------------------------
    '   一覧　編集チェック（EditingControlShowingイベント）
    '   （処理概要）入力の制限をかける
    '-------------------------------------------------------------------------------
    Private Sub dgvSeisanNouryoku_EditingControlShowing(ByVal sender As Object, _
                                              ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs _
                                              ) Handles dgvSeisanNouryoku.EditingControlShowing

        Try
            Dim dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanNouryoku) 'DGVハンドラの設定
            '■調整MCHの場合
            If dgvSeisanNouryoku.CurrentCell.ColumnIndex = COLNO_TYOUSEI Then

                '■グリッドに、数値入力モードの制限をかける
                _chkCellVO = dgv.chkCell(sender, e, UtilDataGridViewHandler.chkType.Num_M)

            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '   サイズ別一覧　選択セル検証イベント（DataErrorイベント）
    '   （処理概要）数値入力欄に数値以外が入力された場合のエラー処理
    '-------------------------------------------------------------------------------
    Private Sub dgvSeisanNouryoku_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvSeisanNouryoku.DataError

        Try
            e.Cancel = False    '編集モード終了

            Dim dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanNouryoku)
            Dim inputStr As String = _db.rmNullStr(dgvSeisanNouryoku.EditingControl.Text)       '調整MCHに入力された文字列

            '■調整MCHの場合、グリッドには数値入力モード(0～9)の制限をかけているので、制限の解除
            If dgvSeisanNouryoku.CurrentCell.ColumnIndex = COLNO_TYOUSEI Then
                dgv.AfterchkCell(_chkCellVO)
            End If

            '調整MCHが空、文字列の場合、調整MCHはNullをセット
            dgv.setCellData(COLDT_TYOUSEI, dgvSeisanNouryoku.CurrentCell.RowIndex, DBNull.Value)

            If String.Empty.Equals(inputStr) = False Then
                '調整MCHが空の場合エラーメッセージ表示
                Throw New UsrDefException("半角数字のみ入力可能です。", _msgHd.getMSG("onlyAcceptNumeric"))
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   一覧のセル値変更時
    '-------------------------------------------------------------------------------
    Private Sub dgvSeisanNouryoku_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSeisanNouryoku.CellEndEdit

        Try
            Dim dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanNouryoku) 'DGVハンドラの設定

            '数値入力モード(0～9)の制限がかかっている場合は、制限の解除
            If dgvSeisanNouryoku.CurrentCell.ColumnIndex = COLNO_TYOUSEI Then
                dgv.AfterchkCell(_chkCellVO)
            End If

            '変更した行を取得する
            Dim RowNo As Integer = dgvSeisanNouryoku.CurrentCell.RowIndex

            '変更行の調整MCH
            Dim tyousei As Single

            '調整MCHが空なら0と見なして計算する
            If String.Empty.Equals(_db.rmNullStr(dgv.getCellData(COLDT_TYOUSEI, RowNo))) Then
                tyousei = 0.0
            Else
                tyousei = _db.rmNullStr(dgv.getCellData(COLDT_TYOUSEI, RowNo))
                '調整MCH値の上限・下限をチェック
                If tyousei > TYOUSEIMAX Or tyousei < TYOUSEIMIN Then
                    Call _msgHd.dspMSG("over3Keta")     '上限下限を超えていた場合
                    dgv.setCellData(COLDT_TYOUSEI, RowNo, DBNull.Value)                             '調整MCHを空にする
                    If Not "".Equals(_db.rmNullStr(dgv.getCellData(COLDT_KEISANTI, RowNo))) Then
                        dgv.setCellData(COLDT_TEKIYOU, RowNo, _db.rmNullStr(dgv.getCellData(COLDT_KEISANTI, RowNo)))   '生産能力MCHを計算MCHと同値にする
                    End If
                    Exit Sub
                End If
            End If

            '変更行の計算MCHが空の場合、生産能力MCHは空に更新して終了
            If String.Empty.Equals(_db.rmNullStr(dgv.getCellData(COLDT_KEISANTI, RowNo))) Then
                dgv.setCellData(COLDT_TEKIYOU, RowNo, DBNull.Value)
                Exit Sub
            End If

            '変更行の計算MCHを取得
            Dim keisan As Single = _db.rmNullStr(dgv.getCellData(COLDT_KEISANTI, RowNo))
            '生産能力MCHを更新
            dgv.setCellData(COLDT_TEKIYOU, RowNo, tyousei + keisan)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '　　選択行に着色する処理
    '　　(処理概要）選択行に着色する
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvSeisanNouryoku_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvSeisanNouryoku.SelectionChanged

        Try
            Dim dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanNouryoku)

            If _colorCtlFlg Then
                dgv.setSelectionRowColor(dgvSeisanNouryoku.CurrentCellAddress.Y, _oldRowIndex, StartUp.lCOLOR_BLUE)
            End If
            _oldRowIndex = dgvSeisanNouryoku.CurrentCellAddress.Y

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

#End Region

#Region "ユーザ定義関数"

    '-------------------------------------------------------------------------------
    '　画面起動時
    '-------------------------------------------------------------------------------
    Private Sub initForm()

        Try
            '処理年月、計画年月表示
            Call dispDate()

            '稼働日数表示
            Call dispWorkingdays()

            _colorCtlFlg = False
            '一覧画面表示
            Call dispDGV()
            _colorCtlFlg = True

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　コントロールキー押下イベント
    '　(処理概要)エンターボタン押下時に次のコントロールに移る
    '-------------------------------------------------------------------------------
    Private Sub txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDTuujou.KeyPress, _
                                                                                                                txtDDoyou.KeyPress, _
                                                                                                                txtDNitiyou.KeyPress, _
                                                                                                                txtTTuujou.KeyPress, _
                                                                                                                txtTDoyou.KeyPress, _
                                                                                                                txtTNitiyou.KeyPress
        Try
            '次のコントロールへ移動する
            UtilClass.moveNextFocus(Me, e)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　コントロールキーフォーカス取得イベント
    '　(処理概要)フォーカス取得時、全選択状態にする
    '-------------------------------------------------------------------------------
    Private Sub txt_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDTuujou.GotFocus, _
                                                                                          txtDDoyou.GotFocus, _
                                                                                          txtDNitiyou.GotFocus, _
                                                                                          txtTTuujou.GotFocus, _
                                                                                          txtTDoyou.GotFocus, _
                                                                                          txtTNitiyou.GotFocus
        Try
            '全選択状態にする
            UtilClass.selAll(sender)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　稼働日入力チェック
    '-------------------------------------------------------------------------------
    Private Sub checkWorkingDay()

        Try
            '必須入力チェック
            If String.Empty.Equals(Trim(txtDTuujou.Text)) Then
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput"), txtDTuujou)
            End If
            If String.Empty.Equals(Trim(txtDDoyou.Text)) Then
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput"), txtDDoyou)
            End If
            If String.Empty.Equals(Trim(txtDNitiyou.Text)) Then
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput"), txtDNitiyou)
            End If
            If String.Empty.Equals(Trim(txtTTuujou.Text)) Then
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput"), txtTTuujou)
            End If
            If String.Empty.Equals(Trim(txtTDoyou.Text)) Then
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput"), txtTDoyou)
            End If
            If String.Empty.Equals(Trim(txtTNitiyou.Text)) Then
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput"), txtTNitiyou)
            End If

            '正数値チェック
            If checkSeisuu(txtDTuujou.Text) = False Then
                Throw New UsrDefException("0以上の正の整数を入力してください。", _msgHd.getMSG("inputOverZero"), txtDTuujou)
            End If
            If checkSeisuu(txtDDoyou.Text) = False Then
                Throw New UsrDefException("0以上の正の整数を入力してください。", _msgHd.getMSG("inputOverZero"), txtDDoyou)
            End If
            If checkSeisuu(txtDNitiyou.Text) = False Then
                Throw New UsrDefException("0以上の正の整数を入力してください。", _msgHd.getMSG("inputOverZero"), txtDNitiyou)
            End If
            If checkSeisuu(txtTTuujou.Text) = False Then
                Throw New UsrDefException("0以上の正の整数を入力してください。", _msgHd.getMSG("inputOverZero"), txtTTuujou)
            End If
            If checkSeisuu(txtTDoyou.Text) = False Then
                Throw New UsrDefException("0以上の正の整数を入力してください。", _msgHd.getMSG("inputOverZero"), txtTDoyou)
            End If
            If checkSeisuu(txtTNitiyou.Text) = False Then
                Throw New UsrDefException("0以上の正の整数を入力してください。", _msgHd.getMSG("inputOverZero"), txtTNitiyou)
            End If

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    ' 　0以上の整数チェック関数
    '   (処理概要)パラメータが0以上の正の整数であるか確認する
    '　　I　：　prmInput     　　 入力された文字列
    '    R　：　Boolean           0以上の正の整数の場合True、そうではない場合False
    '-------------------------------------------------------------------------------
    Private Function checkSeisuu(ByVal prmInput As String) As Boolean

        Dim retFlg As Boolean = True    '返却用フラグ
        Dim value As Integer            '数値変換の戻り値

        If Integer.TryParse(Trim(prmInput), value) Then '数値に変換できた場合
            If value < 0 Then   'マイナス値ならNG
                retFlg = False
            End If
        Else    '数値に変換できなかった場合
            retFlg = False
        End If

        Return retFlg

    End Function

    '------------------------------------------------------------------------------------------------------
    '  登録チェック
    '　(処理概要)画面のDB登録内容が設定されているかチェックする
    '　　I　：　prmMaxCnt     　　 一覧の件数
    '------------------------------------------------------------------------------------------------------
    Private Sub checkTouroku(ByRef prmMaxCnt As Long)

        Try
            Dim dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanNouryoku)

            Dim tuujou As String    '通常稼働（月）
            Dim doyou As String     '土曜休出（月）
            Dim nitiyou As String   '日曜休出（月）
            Dim seisan As String    '生産能力MCH
            Dim tyousei As Single   '調整MCH

            For i As Integer = 0 To prmMaxCnt - 1
                tuujou = _db.rmNullStr(dgv.getCellData(COLDT_TUUJOUMON, i))       '通常稼働（月）
                doyou = _db.rmNullStr(dgv.getCellData(COLDT_DOYOUMON, i))         '土曜休出（月）
                nitiyou = _db.rmNullStr(dgv.getCellData(COLDT_NICHIYOUMON, i))    '日曜休出（月）
                seisan = _db.rmNullStr(dgv.getCellData(COLDT_TEKIYOU, i))         '生産能力MCH

                '必須入力チェック
                If String.Empty.Equals(tuujou) Or String.Empty.Equals(doyou) Or String.Empty.Equals(nitiyou) Or String.Empty.Equals(seisan) Then
                    Throw New UsrDefException("MCHを算出してください。", _msgHd.getMSG("calculateMch"), btnSansyutu)
                End If

                '調整MCHが空ではない場合、範囲チェックを行う
                If String.Empty.Equals(_db.rmNullStr(dgv.getCellData(COLDT_TYOUSEI, i))) = False Then
                    tyousei = _db.rmNullStr(dgv.getCellData(COLDT_TYOUSEI, i))
                    '調整MCH値の上限・下限をチェック
                    If tyousei > TYOUSEIMAX Or tyousei < TYOUSEIMIN Then
                        dgv.setCurrentCell(dgv.readyForErrSet(i, COLCN_TYOUSEI))
                        If _colorCtlFlg Then
                            dgv.setSelectionRowColor(dgvSeisanNouryoku.CurrentCellAddress.Y, _oldRowIndex, StartUp.lCOLOR_BLUE)
                        End If
                        _oldRowIndex = dgvSeisanNouryoku.CurrentCellAddress.Y

                        'エラーメッセージ表示
                        '-->2010.12.17 chg by takagi #13
                        'Throw New UsrDefException("整数部は３桁以内で入力して下さい。", _msgHd.getMSG("over3Keta", "【 '調整MCH' ：" & i + 1 & "行目】"))
                        Throw New UsrDefException("整数部は３桁以内で入力して下さい。", _msgHd.getMSG("over3Keta"))
                        '<--2010.12.17 chg by takagi #13
                    End If
                End If
            Next

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　処理年月、計画年月表示
    '　(処理概要)処理年月、計画年月を表示する
    '-------------------------------------------------------------------------------
    Private Sub dispDate()
        Try
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & " SNENGETU " & "SYORI"          '処理年月
            sql = sql & N & " ,KNENGETU " & "KEIKAKU"       '計画年月
            sql = sql & N & " FROM T01KEIKANRI "

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '抽出レコードが１件もない場合
                Throw New UsrDefException("登録されていません。", _msgHd.getMSG("noData"))
            End If

            Dim syoriDate As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("SYORI"))     '処理年月
            Dim keikakuDate As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("KEIKAKU")) '計画年月

            '「YYYY/MM」形式で表示
            lblSyori.Text = syoriDate.Substring(0, 4) & "/" & syoriDate.Substring(4, 2)
            lblKeikaku.Text = keikakuDate.Substring(0, 4) & "/" & keikakuDate.Substring(4, 2)

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　稼働日数表示
    '　(処理概要)T63より電線・通信の稼働日数を取得し、画面に表示する
    '-------------------------------------------------------------------------------
    Private Sub dispWorkingdays()

        Try
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & " TUUJOUD " & COLDT_TUUJOU         '通常稼働
            sql = sql & N & " ,DOYOUD " & COLDT_DOYOU          '土曜休出
            sql = sql & N & " ,NITIYOUD " & COLDT_NICHIYOU     '日曜休出
            sql = sql & N & " ,BMNCD " & COLDT_BUMON           '製造部門
            sql = sql & N & " FROM T63KADOUBI "
            sql = sql & N & " ORDER BY BMNCD"

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            '抽出レコードが2件でない場合
            If iRecCnt <> 2 Then
                '稼働日数は空表示
                txtDTuujou.Text = String.Empty
                txtDDoyou.Text = String.Empty
                txtDNitiyou.Text = String.Empty
                txtTTuujou.Text = String.Empty
                txtTDoyou.Text = String.Empty
                txtTNitiyou.Text = String.Empty
            Else
                '1レコード目の部門が通信かつ2レコード目の部門が電線の場合
                If BMNCD_TUUSIN.Equals(_db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_BUMON))) And BMNCD_DENSEN.Equals(_db.rmNullStr(ds.Tables(RS).Rows(1)(COLDT_BUMON))) Then
                    txtTTuujou.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_TUUJOU))
                    txtTDoyou.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_DOYOU))
                    txtTNitiyou.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_NICHIYOU))
                    txtDTuujou.Text = _db.rmNullStr(ds.Tables(RS).Rows(1)(COLDT_TUUJOU))
                    txtDDoyou.Text = _db.rmNullStr(ds.Tables(RS).Rows(1)(COLDT_DOYOU))
                    txtDNitiyou.Text = _db.rmNullStr(ds.Tables(RS).Rows(1)(COLDT_NICHIYOU))
                Else
                    '部門が通信と電線ではない場合は空表示
                    txtDTuujou.Text = String.Empty
                    txtDDoyou.Text = String.Empty
                    txtDNitiyou.Text = String.Empty
                    txtTTuujou.Text = String.Empty
                    txtTDoyou.Text = String.Empty
                    txtTNitiyou.Text = String.Empty
                End If
            End If

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '検索処理
    '   （処理概要）　検索処理を行ない、一覧にデータを表示する。
    '------------------------------------------------------------------------------------------------------
    Private Sub dispDGV()

        Dim c As Cursor = Me.Cursor
        Me.Cursor = Cursors.WaitCursor

        Try
            Dim dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanNouryoku)

            '一覧の初期化
            dgv.clearRow()
            dgvSeisanNouryoku.Enabled = False
            lblKensuu.Text = "0件"

            'SQL
            'M21をメインとして、T63、T64からデータ取得
            'ソート順についてはM01を参照(M01に存在しない工程名も考慮)
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & "  TMP.KOUTEI " & COLDT_KOUTEI
            sql = sql & N & " ,TMP.KIKAIMEI " & COLDT_MACHINENAME
            sql = sql & N & " ,TMP.TUUJOUH " & COLDT_TUUJOUDAY
            sql = sql & N & " ,TO_NUMBER(TMP.TUUJOUM) " & COLDT_TUUJOUMON
            sql = sql & N & " ,TMP.DOYOUH " & COLDT_DOYOUDAY
            sql = sql & N & " ,TO_NUMBER(TMP.DOYOUM) " & COLDT_DOYOUMON
            sql = sql & N & " ,TMP.NITIYOUH " & COLDT_NICHIYOUDAY
            sql = sql & N & " ,TO_NUMBER(TMP.NITIYOUM) " & COLDT_NICHIYOUMON
            sql = sql & N & " ,DECODE(TMP.KEIFLG,1,TMP.TUUJOUM + TMP.DOYOUM + TMP.NITIYOUM,NULL) " & COLDT_KEISANTI
            sql = sql & N & " ,T64.TYOUSEIMCH " & COLDT_TYOUSEI
            sql = sql & N & " ,DECODE(TMP.KEIFLG,1,TMP.TUUJOUM + TMP.DOYOUM + TMP.NITIYOUM + NVL(T64.TYOUSEIMCH,0)) " & COLDT_TEKIYOU
            sql = sql & N & " FROM (SELECT "
            sql = sql & N & "    M21.KOUTEI "
            sql = sql & N & "   ,M21.KIKAIMEI "
            sql = sql & N & "   ,M21.TUUJOUH "
            sql = sql & N & "   ,M21.DOYOUH "
            sql = sql & N & "   ,M21.NITIYOUH "
            sql = sql & N & "   ,DECODE(T63.TUUJOUD,NULL,NULL,T63.TUUJOUD * M21.TUUJOUH) TUUJOUM "
            sql = sql & N & "   ,DECODE(T63.DOYOUD,NULL,NULL,T63.DOYOUD * M21.DOYOUH) DOYOUM "
            sql = sql & N & "   ,DECODE(T63.NITIYOUD,NULL,NULL,T63.NITIYOUD * M21.NITIYOUH) NITIYOUM "
            sql = sql & N & "   ,(CASE WHEN T63.TUUJOUD IS NULL OR T63.DOYOUD IS NULL OR T63.NITIYOUD IS NULL THEN 0 ELSE 1 END) KEIFLG "
            sql = sql & N & "   FROM M21SEISAN M21 "
            sql = sql & N & "   LEFT OUTER JOIN T63KADOUBI T63 ON T63.BMNCD = SUBSTR(M21.KIKAIMEI,1,1) " '機械名の1桁目が製造部門コード
            sql = sql & N & " ) TMP LEFT OUTER JOIN T64MCH T64 ON T64.NAME = TMP.KIKAIMEI "
            sql = sql & N & " LEFT OUTER JOIN (SELECT KAHENKEY,SORT FROM M01HANYO WHERE KOTEIKEY = '" & KOUTEI & "') M01 ON M01.KAHENKEY = TMP.KOUTEI "
            sql = sql & N & " ORDER BY NVL(M01.SORT," & maxSort & "), TMP.KIKAIMEI "

            'SQL発行
            Dim iRecCnt As Integer                  'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            '抽出データを一覧に表示する
            dgvSeisanNouryoku.DataSource = ds
            dgvSeisanNouryoku.DataMember = RS

            '件数を表示
            lblKensuu.Text = CStr(iRecCnt) & "件"

            'ボタン制御
            If dgvSeisanNouryoku.RowCount <= 0 Then
                dgvSeisanNouryoku.Enabled = False  '一覧の使用不可
                btnTouroku.Enabled = False         '登録ボタンの使用不可
            Else
                dgvSeisanNouryoku.Enabled = True   '一覧の使用不可
                btnTouroku.Enabled = True          '登録ボタンの使用可
                '一覧先頭行選択
                dgvSeisanNouryoku.Focus()
                dgv.setSelectionRowColor(0, 0, StartUp.lCOLOR_BLUE)
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        Finally
            Me.Cursor = c
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　DB登録処理
    '　(処理概要)画面内容をDB（T64,T63）に反映する
    '------------------------------------------------------------------------------------------------------
    Private Sub registDB()

        Try
            Dim dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanNouryoku)
            Dim sql As String = ""

            '処理開始時間と端末IDの取得
            Dim updStartDate As Date = Now()                            '処理開始日時
            Dim sPCName As String = _db.rmSQ(UtilClass.getComputerName) '端末ID

            '削除処理
            'SQL文発行(T63,T64の全削除)
            sql = "DELETE FROM T63KADOUBI"
            _db.executeDB(sql)
            sql = "DELETE FROM T64MCH"
            _db.executeDB(sql)

            'T63に稼働日を登録
            'SQL文発行(登録件数は2レコード固定なので、まとめて登録する)
            sql = "INSERT ALL INTO T63KADOUBI ("
            sql = sql & N & " BMNCD "
            sql = sql & N & ",TUUJOUD "
            sql = sql & N & ",DOYOUD "
            sql = sql & N & ",NITIYOUD "
            sql = sql & N & ",UPDNAME "
            sql = sql & N & ",UPDDATE "
            sql = sql & N & ") VALUES ("
            sql = sql & N & "'" & BMNCD_TUUSIN & "'"      '製造部門(通信)
            sql = sql & N & "," & txtTTuujou.Text         '通常稼働日数
            sql = sql & N & "," & txtTDoyou.Text          '土曜休出日数
            sql = sql & N & "," & txtTNitiyou.Text        '日曜休出日数
            sql = sql & N & ",'" & sPCName & "'" '端末ID
            sql = sql & N & ",TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS') "  '更新日時
            sql = sql & N & ") "
            sql = sql & N & "INTO T63KADOUBI ("
            sql = sql & N & " BMNCD "
            sql = sql & N & ",TUUJOUD "
            sql = sql & N & ",DOYOUD "
            sql = sql & N & ",NITIYOUD "
            sql = sql & N & ",UPDNAME "
            sql = sql & N & ",UPDDATE "
            sql = sql & N & ") VALUES ("
            sql = sql & N & "'" & BMNCD_DENSEN & "'"      '製造部門(電線)
            sql = sql & N & "," & txtDTuujou.Text         '通常稼働日数
            sql = sql & N & "," & txtDDoyou.Text          '土曜休出日数
            sql = sql & N & "," & txtDNitiyou.Text        '日曜休出日数
            sql = sql & N & ",'" & sPCName & "'" '端末ID
            sql = sql & N & ",TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS') "  '更新日時
            sql = sql & N & ") "
            sql = sql & N & " SELECT * FROM DUAL "
            _db.executeDB(sql)

            '登録処理した件数
            Dim rCntIns As Integer = 0
            Dim tyousei As String
            'T64に一覧の内容を登録する
            For i As Integer = 0 To dgv.getMaxRow - 1
                tyousei = _db.rmNullStr(dgv.getCellData(COLDT_TYOUSEI, i))
                If String.Empty.Equals(tyousei) Then
                    tyousei = "0"
                End If
                'SQL文発行
                sql = "INSERT INTO T64MCH ("
                sql = sql & N & " KOUTEI "         '工程
                sql = sql & N & ",NAME "           '機械名
                sql = sql & N & ",TUUJOUH_D "      '通常稼動時間(日)
                sql = sql & N & ",TUUJOUH_M "      '通常稼動時間(月間)
                sql = sql & N & ",DOYOUH_D "       '土曜稼動時間(日)
                sql = sql & N & ",DOYOUH_M "       '土曜稼動時間(月間)
                sql = sql & N & ",NITIYOUH_D "     '日曜稼動時間(日)
                sql = sql & N & ",NITIYOUH_M "     '日曜稼動時間(月間)
                sql = sql & N & ",KEISANMCH "      '計算MCH
                sql = sql & N & ",TYOUSEIMCH "     '調整MCH
                sql = sql & N & ",MSTMCH "         'マスタMCH
                sql = sql & N & ",UPDNAME "        '端末ID
                sql = sql & N & ",UPDDATE "        '更新日時
                sql = sql & N & ") VALUES ("
                sql = sql & N & " '" & _db.rmNullStr(dgv.getCellData(COLDT_KOUTEI, i)) & "'"        '工程
                sql = sql & N & ",'" & _db.rmNullStr(dgv.getCellData(COLDT_MACHINENAME, i)) & "'"   '機械名
                sql = sql & N & ", " & _db.rmNullStr(dgv.getCellData(COLDT_TUUJOUDAY, i))           '通常稼動時間(日)
                sql = sql & N & ", " & _db.rmNullStr(dgv.getCellData(COLDT_TUUJOUMON, i))           '通常稼動時間(月間)
                sql = sql & N & ", " & _db.rmNullStr(dgv.getCellData(COLDT_DOYOUDAY, i))            '通常稼動時間(日)
                sql = sql & N & ", " & _db.rmNullStr(dgv.getCellData(COLDT_DOYOUMON, i))            '通常稼動時間(月間)
                sql = sql & N & ", " & _db.rmNullStr(dgv.getCellData(COLDT_NICHIYOUDAY, i))         '通常稼動時間(日)
                sql = sql & N & ", " & _db.rmNullStr(dgv.getCellData(COLDT_NICHIYOUMON, i))         '通常稼動時間(月間)
                sql = sql & N & ", " & _db.rmNullStr(dgv.getCellData(COLDT_KEISANTI, i))            '計算MCH
                sql = sql & N & ", " & _db.rmSQ(tyousei)                                            '調整MCH
                sql = sql & N & ", " & _db.rmNullStr(dgv.getCellData(COLDT_TEKIYOU, i))             'マスタMCH
                sql = sql & N & ", '" & sPCName & "'"                                               '端末ID
                sql = sql & N & ", TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS') "       '更新日時
                sql = sql & N & " )"
                _db.executeDB(sql)

                '登録件数のカウントアップ
                rCntIns += 1
            Next

            '処理終了日時を取得
            Dim updFinDate As Date
            updFinDate = Now

            '実行履歴テーブル更新
            sql = "INSERT INTO T91RIREKI "
            sql = sql & N & "( "
            sql = sql & N & " PGID "       '機能ID
            sql = sql & N & ",SNENGETU "   '処理年月
            sql = sql & N & ",KNENGETU "   '計画年月
            sql = sql & N & ",SDATESTART " '処理開始日時
            sql = sql & N & ",SDATEEND "   '処理終了日時
            sql = sql & N & ",KENNSU1 "    '件数1
            sql = sql & N & ",KENNSU2 "    '件数2
            sql = sql & N & ",UPDNAME "    '端末ID
            sql = sql & N & ",UPDDATE "    '最終更新日
            sql = sql & N & ")VALUES( "
            sql = sql & N & " '" & _db.rmSQ(PGID) & "' "                                 '機能ID
            sql = sql & N & ",'" & _db.rmSQ(lblSyori.Text.Replace("/", "")) & "' "       '処理年月
            sql = sql & N & ",'" & _db.rmSQ(lblKeikaku.Text.Replace("/", "")) & "' "     '計画年月
            sql = sql & N & ",TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS') " '処理開始日時
            sql = sql & N & ",TO_DATE('" & updFinDate & "', 'YYYY/MM/DD HH24:MI:SS') "   '処理終了日時
            sql = sql & N & ",2"                                                         '件数1
            sql = sql & N & ", " & rCntIns                                               '件数2
            sql = sql & N & ",'" & sPCName & "' "                                        '端末ID
            sql = sql & N & ",TO_DATE('" & updFinDate & "', 'YYYY/MM/DD HH24:MI:SS') "   '最終更新日
            sql = sql & N & ") "
            _db.executeDB(sql)

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

#End Region

End Class