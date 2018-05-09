Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL
Public Class frmH03F30_SelectItakuList
    '-------------------------------------------------------------------------------
    '   変数定義
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As Form                             '親フォーム
    Private _SelectID As String
    Private _SelectMode As Integer   'メニューのどこから呼ばれたか。（0:登録、1:変更、2:取消、3:照会)
    Private _SQL As String
    Private _comLogc As CommonLogic                             '共通処理用

    '-------------------------------------------------------------------------------
    '   定数定義
    '-------------------------------------------------------------------------------
    Private Const N As String = ControlChars.NewLine            '改行文字
    Private Const RS As String = "RecSet"                       'レコードセットテーブル
    'グリッド列№
    'dgvList
    Private Const COLNO_URIYMD = 0              '01:売上日
    Private Const COLNO_CHAKUYMD = 1            '02:着日
    Private Const COLNO_DENGYONO = 2            '03:伝番-枝番-行番号
    Private Const COLNO_SYUKKANM = 3            '04:出荷先名
    Private Const COLNO_SHOHINNM = 4            '05:商品名
    Private Const COLNO_URIAGESURYO = 5         '06:売上数量
    Private Const COLNO_URIAGETANKA = 6         '07:売上単価
    Private Const COLNO_URIAGEMONEY = 7         '08:売上金額
    Private Const COLNO_DenNo = 8               '09:伝票番号（隠れ項目）
    Private Const COLNO_EDANo = 9               '10:伝票番号枝番（隠れ項目）
    'SQL
    Public ReadOnly Property SQL() As String
        Get
            Return _SQL
        End Get
    End Property

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
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByRef prmSelectID As String, ByRef prmParentForm As Form, ByRef prmSelectMode As Integer)
        Call Me.New()

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        _parentForm = prmParentForm
        _SelectID = prmSelectID
        _SelectMode = prmSelectMode                                         '処理状態
        _comLogc = New CommonLogic(_db, _msgHd)                             '共通処理用

        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        '操作履歴ログ作成（初期処理）
        _comLogc.Insert_L01_ProcLog(frmC01F10_Login.loginValue.BumonCD, DBNull.Value, _SelectID, CommonConst.PROGRAM_START, CommonConst.STATUS_NORMAL,
                                        DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value,
                                        DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, frmC01F10_Login.loginValue.TantoCD)

        '売上日表示
        dtpUriDtFrom.Text = DateAdd("m", -1, Now).ToString("yyyy/MM/dd")   'システム日付-1ヶ月（暫定処置）
        dtpUriDtTo.Text = DateAdd("m", 0, Now).ToString("yyyy/MM/dd")      'システム日付+1ヶ月（暫定処置）
        '着日表示
        dtpChakuDtFrom.Value = Nothing
        dtpChakuDtTo.Value = Nothing

        '一覧ヘッダのキャプションは中央寄せ
        dgvList.Columns(COLNO_URIYMD).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvList.Columns(COLNO_CHAKUYMD).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvList.Columns(COLNO_DENGYONO).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvList.Columns(COLNO_SYUKKANM).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvList.Columns(COLNO_SHOHINNM).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvList.Columns(COLNO_URIAGESURYO).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvList.Columns(COLNO_URIAGETANKA).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvList.Columns(COLNO_URIAGEMONEY).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

    End Sub
    '取引先選択ウインドウオープン処理
    Private Sub SeikyusakiSelectWindowOpen()

        Dim openForm As frmC10F20_Torihikisaki = New frmC10F20_Torihikisaki(_msgHd, _db, CommonConst.TORIHIKISAKI_TARGET_KBN_SHUKKA)
        openForm.ShowDialog()                      '画面表示

        '選択されている場合
        If openForm.Selected Then
            '画面に値をセット

            '出荷先コード
            Me.txtSyukkaCD.Text = openForm.GetValTorihikisakiCd
            Me.lblSyukkaNM.Text = openForm.GetValTorihikisakiName

        End If

        openForm = Nothing

    End Sub
    Private Sub getList()
        '--------------------------------
        '抽出条件からデータ取得
        'functionにする
        '--------------------------------
        Dim strSql As String = ""
        Try
            '取得したデータをDataGrdViewに反映
            strSql = "SELECT "
            strSql = strSql & "  t20.会社コード, t20.取消区分, t20.出荷先分類, t20.売上日, t20.着日, t20.売上伝番, t20.売上伝番枝番, t20.出荷先コード, t20.出荷先名  "
            strSql = strSql & "  , t21.行番, t21.商品名, t21.売上数量, t21.売上単価, t21.売上金額  "
            strSql = strSql & " FROM t20_urighd t20 "
            strSql = strSql & "     inner join T21_URIGDT t21 on t21.会社コード = t20.会社コード and t21.売上伝番 = t20.売上伝番 and t21.売上伝番枝番 = t20.売上伝番枝番 "
            strSql = strSql & " Where t20.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and  t20.取消区分 = 0 and t20.出荷先分類 ='1' "
            '出荷先コード
            If txtSyukkaCD.Text <> "" Then
                strSql = strSql & "  and t20.出荷先コード like '%" & txtSyukkaCD.Text & "' "
            End If
            '出荷先名
            If txtSyukkaNM.Text <> "" Then
                strSql = strSql & "  and t20.出荷先名 like '%" & txtSyukkaNM.Text & "%' "
            End If
            '商品名
            If txtShohinNM.Text <> "" Then
                strSql = strSql & "  and t21.商品名 like '%" & txtShohinNM.Text & "%' "
            End If
            '売上日From
            If _db.rmNullDate(Me.dtpUriDtFrom.Value) <> "" Then
                strSql = strSql & N & " and t20.売上日 >= '" & _db.rmNullDate(Me.dtpUriDtFrom.Value, "yyyy/MM/dd") & "' "
            End If
            '売上日To
            If _db.rmNullDate(Me.dtpUriDtTo.Value) <> "" Then
                strSql = strSql & N & " and t20.売上日 <= '" & _db.rmNullDate(Me.dtpUriDtTo.Value, "yyyy/MM/dd") & "' "
            End If
            '着日From
            If _db.rmNullDate(Me.dtpChakuDtFrom.Value) <> "" Then
                strSql = strSql & N & " and t20.着日 >= '" & _db.rmNullDate(Me.dtpChakuDtFrom.Value, "yyyy/MM/dd") & "' "
            End If
            '着日To
            If _db.rmNullDate(Me.dtpChakuDtTo.Value) <> "" Then
                strSql = strSql & N & " and t20.着日 <= '" & _db.rmNullDate(Me.dtpChakuDtTo.Value, "yyyy/MM/dd") & "' "
            End If
            '伝票番号From
            If Me.txtDenpyoNoFrom.Text <> "" Then
                strSql = strSql & N & " and t20.売上伝番 >= '" & Me.txtDenpyoNoFrom.Text & "' "
            End If
            '伝票番号To
            If Me.txtDenpyoNoTo.Text <> "" Then
                strSql = strSql & N & " and t20.売上伝番 <= '" & Me.txtDenpyoNoTo.Text & "' "
            End If


            strSql = strSql & " order by t20.着日, t20.売上伝番, t20.売上伝番枝番, t21.行番 "
            _SQL = strSql
            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(strSql, RS, reccnt)
            '画面入力の条件値をもとに、対象データを検索し、一覧表示する。	
            '検索にあたっては、次の手順を行う。	
            '①対象データの件数を取得する
            If reccnt > StartUp.iniValue.GamenSelUpperCnt Then
                '②件数が規定件数を超える場合、表示有無の確認を行う
                '　規定件数以内の場合は③に進む、NOの場合は、条件入力に戻る
                '確認メッセージを表示する
                Dim piRtn As Integer
                piRtn = _msgHd.dspMSG("MaxDataCnt")  '抽出したデータが上限を超えています。表示してよろしいですか？
                If piRtn = vbCancel Then
                    Exit Sub
                End If
            End If
            '③データ検索を行い、画面を表示する

            '抽出データ件数を取得、表示
            lblListCount.Text = ds.Tables(RS).Rows.Count



            '描画の前にすべてクリアする
            dgvList.Rows.Clear()
            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                dgvList.Rows.Add()
                dgvList.Rows(index).Cells(COLNO_URIYMD).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("売上日"))         '売上日
                dgvList.Rows(index).Cells(COLNO_CHAKUYMD).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("着日"))         '着日
                dgvList.Rows(index).Cells(COLNO_DENGYONO).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("売上伝番")) & "-" & _db.rmNullStr(ds.Tables(RS).Rows(index)("売上伝番枝番")).PadLeft(2, "0"c) & "-" & _db.rmNullStr(ds.Tables(RS).Rows(index)("行番")).PadLeft(2, "0"c)        '伝票番号-行番号
                dgvList.Rows(index).Cells(COLNO_SYUKKANM).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("出荷先名"))       '出荷先
                dgvList.Rows(index).Cells(COLNO_SHOHINNM).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("商品名"))       '商品名
                dgvList.Rows(index).Cells(COLNO_URIAGESURYO).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("売上数量"))  '売上数量
                dgvList.Rows(index).Cells(COLNO_URIAGETANKA).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("売上単価"))  '売上単価
                dgvList.Rows(index).Cells(COLNO_URIAGEMONEY).Value = Decimal.Parse(_db.rmNullStr(ds.Tables(RS).Rows(index)("売上金額"))).ToString("N0")  '売上金額
                dgvList.Rows(index).Cells(COLNO_DenNo).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("売上伝番"))        '売上伝番
                dgvList.Rows(index).Cells(COLNO_EDANo).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("売上伝番枝番"))    '売上伝番枝番
            Next

            '対象データの有無チェック
            If ds.Tables(RS).Rows.Count = 0 Then
                '選択ボタンを非活性
                Me.btnSelect.Enabled = False
                _msgHd.dspMSG("NonData")
                Exit Sub
            Else
                '選択ボタンを活性
                Me.btnSelect.Enabled = True
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            'Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub
    '戻るボタンクリック
    Private Sub cmdBack_Click(sender As Object, e As EventArgs) Handles cmdBack.Click
        _parentForm.Show()                                              ' 前画面を表示
        _parentForm.Enabled = True                                      ' 前画面の使用を可能にする
        _parentForm.Activate()                                          ' 前画面をアクティブにする

        Me.Dispose()                                                    ' 自画面を閉じる
    End Sub
    '選択ボタンクリック
    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click

        Dim idx As Integer
        For Each c As DataGridViewRow In dgvList.SelectedRows
            idx = c.Index
            Exit For
        Next c
        'リスト表示がゼロ件の場合は処理しない
        If dgvList.RowCount = 0 Then
            Exit Sub
        End If

        Dim strDenpyoNo As String = ""    '伝票番号
        Dim strDenpyoNoEda As String = "" '伝票番号枝番
        strDenpyoNo = dgvList.Rows(idx).Cells(COLNO_DenNo).Value
        strDenpyoNoEda = dgvList.Rows(idx).Cells(COLNO_EDANo).Value

        Dim openForm As Form = Nothing
        openForm = New frmH03F20_Itaku(_msgHd, _db, _SelectID, Me, _SelectMode, strDenpyoNo, strDenpyoNoEda)
        openForm.Show()
        Me.Hide()   ' 自分は隠れる

    End Sub
    '検索ボタンクリック時
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        getList()
    End Sub
    'コントロールのキープレスイベント

    Private Sub txtSyukkaNM_KeyPress(sender As Object, e As KeyPressEventArgs) Handles dtpUriDtFrom.KeyPress, dtpUriDtTo.KeyPress, dtpChakuDtFrom.KeyPress, dtpChakuDtTo.KeyPress

        UtilMDL.UtilClass.moveNextFocus(Me, e)

    End Sub

    '-------------------------------------------------------------------------------
    '　フォーカス取得イベント
    '-------------------------------------------------------------------------------
    Private Sub ctl_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpUriDtFrom.GotFocus, dtpUriDtTo.GotFocus, dtpChakuDtFrom.GotFocus, dtpChakuDtTo.GotFocus, txtSyukkaCD.GotFocus
        'フォーカス取得時、入力パラメタのコントロールを全選択状態とする
        Call UtilClass.selAll(sender)

    End Sub

    '伝票番号(From,To)からフォーカスが外れたとき
    Private Sub txtDenpyoNoFrom_Leave(sender As Object, e As EventArgs) Handles txtDenpyoNoFrom.Leave, txtDenpyoNoTo.Leave
        Try
            '伝票番号からフォーカスアウト時処理
            denpyoNoLeave(sender)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub
    '伝票番号フォーカスアウト時処理
    Private Sub denpyoNoLeave(textbox As TextBox)

        '入力がある場合
        If (textbox.Text <> String.Empty) Then

            '伝票番号の連番桁数で前ゼロ埋め
            textbox.Text = textbox.Text.PadLeft(CommonConst.KETA_DEN_ITAKU, "0"c)
        End If
    End Sub

    Private Sub txtDenpyoNoFrom_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDenpyoNoFrom.KeyPress, txtDenpyoNoTo.KeyPress
        If e.KeyChar < "0"c OrElse "9"c < e.KeyChar Then
            '押されたキーが 0～9でない場合は、イベントをキャンセルする
            e.Handled = True
        End If
        UtilMDL.UtilClass.moveNextFocus(Me, e)
    End Sub

    Private Sub txtSyukkaCD_DoubleClick(sender As Object, e As EventArgs) Handles txtSyukkaCD.DoubleClick
        SeikyusakiSelectWindowOpen()
    End Sub

    Private Sub txtSyukkaCD_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSyukkaCD.KeyDown

        'スペースキーを押したとき検索画面表示
        If e.KeyCode = Keys.Space Then
            SeikyusakiSelectWindowOpen()
        End If


    End Sub

    Private Sub dgvList_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellDoubleClick
        If e.RowIndex < 0 Then
            Exit Sub
        End If
        Dim idx As Integer
        For Each c As DataGridViewRow In dgvList.SelectedRows
            idx = c.Index
            Exit For
        Next c
        'リスト表示がゼロ件の場合は処理しない
        If dgvList.RowCount = 0 Then
            Exit Sub
        End If

        Dim strDenpyoNo As String = ""    '伝票番号
        Dim strDenpyoNoEda As String = "" '伝票番号枝番
        strDenpyoNo = dgvList.Rows(idx).Cells(COLNO_DenNo).Value
        strDenpyoNoEda = dgvList.Rows(idx).Cells(COLNO_EDANo).Value

        Dim openForm As Form = Nothing
        openForm = New frmH03F20_Itaku(_msgHd, _db, _SelectID, Me, _SelectMode, strDenpyoNo, strDenpyoNoEda)
        openForm.Show()
        Me.Hide()   ' 自分は隠れる

    End Sub
End Class