Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL

Public Class frmH05F10_NyukinList
    '-------------------------------------------------------------------------------
    '   変数定義
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As Form                             '親フォーム
    Private _SelectID As String
    Private _SelectMode As Integer   'メニューのどこから呼ばれたか。（0:登録、1:変更、2:取消、3:照会)

    '-------------------------------------------------------------------------------
    '   定数定義
    '-------------------------------------------------------------------------------
    Private Const N As String = ControlChars.NewLine            '改行文字
    Private Const RS As String = "RecSet"                       'レコードセットテーブル

    'グリッド列№
    'dgvList
    Private Const COLNO_NYUKINYMD = 0           '01:入金日
    Private Const COLNO_DENPYONO = 1            '02:伝票番号
    Private Const COLNO_SEIKYUCD = 2            '03:請求先ＣＤ
    Private Const COLNO_SEIKYUNM = 3            '04:請求先名
    Private Const COLNO_URIAGE = 4              '05:売上金額
    Private Const COLNO_URIAGETAX = 5           '06:売上消費税
    Private Const COLNO_NYUUKIN = 6             '07:入金額
    Private Const COLNO_BIKO = 7                '08:備考

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

        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示

        '入金日表示
        dtpNyukinDtFrom.Text = DateAdd("m", -1, Now).ToString("yyyy/MM/dd")   '入金日自 システム日付-1ヶ月
        dtpNyukinDtTo.Text = DateAdd("m", 0, Now).ToString("yyyy/MM/dd")      '入金日至 システム日付

        '一覧表示
        getList()

    End Sub
    '--------------------------------
    '抽出条件からデータ取得
    '--------------------------------
    Private Sub getList()
        Dim strSql As String = ""
        Try
            '取得したデータをDataGrdViewに反映
            strSql = "SELECT "
            strSql = strSql & "  t25.入金日, t25.入金伝番, t25.請求先コード, t25.請求先名, t25.売上金額計, t25.売上消費税計, t25.入金額計, t25.備考１, t25.備考２ "
            strSql = strSql & " FROM T25_NKINHD  t25 "
            strSql = strSql & " Where t25.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and t25.取消区分 =0 "
            '入金日From
            If _db.rmNullDate(Me.dtpNyukinDtFrom.Value) <> "" Then
                strSql = strSql & N & " and t25.入金日 >= '" & _db.rmNullDate(Me.dtpNyukinDtFrom.Value, "yyyy/MM/dd") & "' "
            End If
            '入金日To
            If _db.rmNullDate(Me.dtpNyukinDtTo.Value) <> "" Then
                strSql = strSql & N & " and t25.入金日 <= '" & _db.rmNullDate(Me.dtpNyukinDtTo.Value, "yyyy/MM/dd") & "' "
            End If
            '入金先名
            If Me.txtNyukinNM.Text <> "" Then
                strSql = strSql & "  and t25.請求先名 like '%" & txtNyukinNM.Text & "%' "
            End If
            '入金先コード
            If Me.txtNyukinCD.Text <> "" Then
                strSql = strSql & "  and t25.請求先コード like '%" & txtNyukinCD.Text & "' "
            End If
            '伝票番号From
            If Me.txtDenpyoNoFrom.Text <> "" Then
                strSql = strSql & N & " and t25.入金伝番 >= '" & Me.lblDenpyoNoTopFrom.Text & Me.txtDenpyoNoFrom.Text & "' "
            End If
            '伝票番号To
            If Me.txtDenpyoNoTo.Text <> "" Then
                strSql = strSql & N & " and t25.入金伝番 <= '" & Me.lblDenpyoNoTopTo.Text & Me.txtDenpyoNoTo.Text & "' "
            End If


            strSql = strSql & " order by  t25.入金日, t25.入金伝番 "

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

            '一覧ヘッダのキャプションは中央寄せ
            dgvList.Columns(COLNO_NYUKINYMD).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            dgvList.Columns(COLNO_DENPYONO).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            dgvList.Columns(COLNO_SEIKYUCD).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            dgvList.Columns(COLNO_SEIKYUNM).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            dgvList.Columns(COLNO_URIAGE).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            dgvList.Columns(COLNO_URIAGETAX).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            dgvList.Columns(COLNO_NYUUKIN).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            dgvList.Columns(COLNO_BIKO).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

            '抽出データ件数を取得、表示
            lblListCount.Text = ds.Tables(RS).Rows.Count


            '描画の前にすべてクリアする
            dgvList.Rows.Clear()
            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                dgvList.Rows.Add()
                dgvList.Rows(index).Cells(COLNO_NYUKINYMD).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("入金日"))         '入金日
                dgvList.Rows(index).Cells(COLNO_DENPYONO).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("入金伝番"))        '伝票番号
                dgvList.Rows(index).Cells(COLNO_SEIKYUCD).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("請求先コード"))    '請求先コード
                dgvList.Rows(index).Cells(COLNO_SEIKYUNM).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("請求先名"))        '請求先名
                dgvList.Rows(index).Cells(COLNO_URIAGE).Value = Decimal.Parse(_db.rmNullStr(ds.Tables(RS).Rows(index)("売上金額計"))).ToString("N0")        '売上金額
                dgvList.Rows(index).Cells(COLNO_URIAGETAX).Value = Decimal.Parse(_db.rmNullStr(ds.Tables(RS).Rows(index)("売上消費税計"))).ToString("N0")   '売上消費税
                dgvList.Rows(index).Cells(COLNO_NYUUKIN).Value = Decimal.Parse(_db.rmNullStr(ds.Tables(RS).Rows(index)("入金額計"))).ToString("N0")         '入金額計
                dgvList.Rows(index).Cells(COLNO_BIKO).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("備考１")) & "　" & _db.rmNullStr(ds.Tables(RS).Rows(index)("備考２"))   '備考
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
    '戻るボタンクリック時
    Private Sub cmdBack_Click(sender As Object, e As EventArgs) Handles cmdBack.Click
        _parentForm.Show()                                              ' 前画面を表示
        _parentForm.Enabled = True                                      ' 前画面の使用を可能にする
        _parentForm.Activate()                                          ' 前画面をアクティブにする

        Me.Dispose()                                                    ' 自画面を閉じる
    End Sub
    '検索ボタンクリック時
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        getList()
    End Sub
    '-------------------------------------------------------------------------------
    '　フォーカス取得イベント
    '-------------------------------------------------------------------------------
    Private Sub ctl_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNyukinNM.GotFocus, txtNyukinCD.GotFocus, dtpNyukinDtFrom.GotFocus, dtpNyukinDtTo.GotFocus, txtDenpyoNoFrom.GotFocus, txtDenpyoNoTo.GotFocus
        'フォーカス取得時、入力パラメタのコントロールを全選択状態とする
        Call UtilClass.selAll(sender)

    End Sub

    '選択ボタンクリック
    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click

        Dim idx As Integer
        For Each c As DataGridViewRow In dgvList.SelectedRows
            idx = c.Index
            Exit For
        Next c

        Dim strDenpyoNo As String = ""    '伝票番号
        strDenpyoNo = dgvList.Rows(idx).Cells(COLNO_DENPYONO).Value

        Dim openForm As Form = Nothing
        openForm = New frmH05F20_Nyukin(_msgHd, _db, _SelectID, Me, _SelectMode, strDenpyoNo)
        openForm.Show()
        Me.Hide()   ' 自分は隠れる

    End Sub
    '取引先選択ウインドウオープン処理
    Private Sub SeikyusakiSelectWindowOpen()

        Dim openForm As frmC10F20_Torihikisaki = New frmC10F20_Torihikisaki(_msgHd, _db, CommonConst.TORIHIKISAKI_TARGET_KBN_SEIKYU)
        openForm.ShowDialog()                      '画面表示

        '選択されている場合
        If openForm.Selected Then
            '画面に値をセット

            '請求先コード
            Me.txtNyukinCD.Text = openForm.GetValTorihikisakiCd

        End If

        openForm = Nothing

    End Sub
    Private Sub txtNyukinCD_DoubleClick(sender As Object, e As EventArgs) Handles txtNyukinCD.DoubleClick
        SeikyusakiSelectWindowOpen()
    End Sub


    Private Sub txtDenpyoNoFrom_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDenpyoNoFrom.KeyPress, txtDenpyoNoTo.KeyPress, txtNyukinCD.KeyPress
        If e.KeyChar < "0"c OrElse "9"c < e.KeyChar Then
            '押されたキーが 0～9でない場合は、イベントをキャンセルする
            e.Handled = True
        End If
        UtilMDL.UtilClass.moveNextFocus(Me, e)
    End Sub

    Private Sub txtNyukinNM_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNyukinNM.KeyPress, dtpNyukinDtFrom.KeyPress, dtpNyukinDtTo.KeyPress
        UtilMDL.UtilClass.moveNextFocus(Me, e)

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
            textbox.Text = textbox.Text.PadLeft(CommonConst.KETA_DEN_NYUKIN, "0"c)
        End If
    End Sub

    Private Sub txtNyukinCD_KeyDown(sender As Object, e As KeyEventArgs) Handles txtNyukinCD.KeyDown
        'スペースキーを押したとき検索画面表示
        If e.KeyCode = Keys.Space Then
            SeikyusakiSelectWindowOpen()
        End If
    End Sub

    'リストダブルクリック
    Private Sub dgvList_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellDoubleClick

        If e.RowIndex < 0 Then
            Exit Sub
        End If
        Dim idx As Integer
        For Each c As DataGridViewRow In dgvList.SelectedRows
            idx = c.Index
            Exit For
        Next c

        Dim strDenpyoNo As String = ""    '伝票番号
        strDenpyoNo = dgvList.Rows(idx).Cells(COLNO_DENPYONO).Value

        Dim openForm As Form = Nothing
        openForm = New frmH05F20_Nyukin(_msgHd, _db, _SelectID, Me, _SelectMode, strDenpyoNo)
        openForm.Show()
        Me.Hide()   ' 自分は隠れる

    End Sub
End Class