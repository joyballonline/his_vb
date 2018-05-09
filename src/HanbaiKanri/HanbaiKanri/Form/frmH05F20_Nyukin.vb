Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports System.Text.RegularExpressions
Public Class frmH05F20_Nyukin
    '-------------------------------------------------------------------------------
    '   定数定義
    '-------------------------------------------------------------------------------
    Private Const N As String = ControlChars.NewLine            '改行文字
    Private Const RS As String = "RecSet"                       'レコードセットテーブル
    'グリッド列№
    'dgvIchiran
    Private Const COLNO_NO = 0                                      '01:No.
    Private Const COLNO_Select = 1                                  '02:選
    Private Const COLNO_UriYMD = 2                                  '03:売上日
    Private Const COLNO_DenEdaGyo = 3                               '04:伝票番号
    Private Const COLNO_SyukkaNM = 4                                '05:出荷先名
    Private Const COLNO_ShohinNM = 5                                '06:商品名
    Private Const COLNO_ZEIKBN = 6                                  '07:税区分
    Private Const COLNO_SURYOU = 7                                  '08:数量
    Private Const COLNO_TANNI = 8                                   '09:単位
    Private Const COLNO_TANKA = 9                                   '10:単価
    Private Const COLNO_MONEY = 10                                  '11:金額
    Private Const COLNO_TAX = 11                                    '12:消費税
    Private Const COLNO_TIMESTAMP = 12                              '13:更新日時（隠し項目）
    Private Const COLNO_DenpyoNo = 13                               '14:売上伝票（隠し項目）
    Private Const COLNO_DenpyoNoEda = 14                            '15:売上伝番枝番（隠し項目）
    Private Const COLNO_DenpyoGyo = 15                              '16:行番（隠し項目）
    Private Const COLNO_SyukkaCD = 16                               '17:出荷先コード

    '-------------------------------------------------------------------------------
    '   変数定義
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _SelectMode As Integer   'メニューのどこから呼ばれたか。（1:登録、2:変更、3:取消、4:照会)
    Private _parentForm As Form                             '親フォーム
    Private _DenpyoNo As String         '伝票番号
    Private _comLogc As CommonLogic                         '共通処理用
    Private _SelectID As String
    Private _userId As String


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
    '   prmSelectMode     '各フォームに引き渡す編集モードの値 CommonConstのこれを使う
    '           Public Const MODE_ADDNEW = 1                                 '登録
    '           Public Const MODE_EditStatus = 2                             '変更
    '           Public Const MODE_CancelStatus = 3                           '取消
    '           Public Const MODE_InquiryStatus = 4                          '照会
    '   prmParentForm   : 呼び出し元フォーム
    '   prmDenpyoNO : 一覧から伝票番号が渡されるときに使用する
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByRef prmSelectID As String, ByRef prmParentForm As Form, ByRef prmSelectMode As Integer, Optional prmDenpyoNO As String = "")
        Call Me.New()


        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _SelectMode = prmSelectMode                                         '処理状態
        _DenpyoNo = prmDenpyoNO                                               '伝票番号
        _SelectID = prmSelectID
        _userId = frmC01F10_Login.loginValue.TantoCD                        'ユーザＩＤ

        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                   'フォームタイトル表示

        _parentForm = prmParentForm

        ' 共通処理使用の準備
        _comLogc = New CommonLogic(_db, _msgHd)                             ' 共通処理用

        '売上日表示
        Me.dtpUriDtFrom.Value = Nothing
        Me.dtpUriDtTo.Value = Nothing
        '入金日表示
        Me.dtpNyukinDt.Value = Now

        '入金種別コンボボックス初期設定
        clearCmbNyukinSyubetu()

        '処理状態の選択
        'コントロールのEnable処理
        ControlEnabled()

        '伝票が引数として渡された時の処理
        If prmDenpyoNO = "" Then
            '伝票番号が渡されないときは新規登録
            If _SelectMode = CommonConst.MODE_ADDNEW Then
                _DenpyoNo = _comLogc.GetDenpyoNo(frmC01F10_Login.loginValue.BumonCD, CommonConst.SAIBAN_NYUKIN)
                Me.lblDenpyoNo.Text = _DenpyoNo
            End If
        Else
            '伝票番号をもとに画面項目を表示
            '渡された伝票番号をもとに情報を表示。
            lblDenpyoNo.Text = _DenpyoNo
            getList()
            getNyukinData()
            '合計金額計算
            RecalcSum()
            SumNyukinMoney()
        End If

        '操作履歴ログ作成
        _comLogc.Insert_L01_ProcLog(frmC01F10_Login.loginValue.BumonCD, DBNull.Value, _SelectID, CommonConst.PROGRAM_START, CommonConst.STATUS_NORMAL,
                                                        _DenpyoNo, lblShoriMode.Text, DBNull.Value, DBNull.Value, DBNull.Value,
                                                        DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _userId)


    End Sub
    'コントロールのEnable処理
    Private Sub ControlEnabled()
        '処理状態の選択
        Select Case _SelectMode
            Case CommonConst.MODE_ADDNEW  '登録
                lblShoriMode.Text = "登録"
                Me.txtSeikyuCD.Enabled = True
                Me.btnAllSelect.Enabled = False
                Me.btnAllCancel.Enabled = False
                Me.cmdTouroku.Enabled = False
                Me.cmdModoru.Enabled = True
                Me.btnSearch.Enabled = True
                Me.dtpUriDtFrom.Enabled = True
                Me.dtpUriDtTo.Enabled = True
                Me.txtDenpyoNoFrom.Enabled = True
                Me.txtDenpyoNoTo.Enabled = True
                Me.txtSyukkaNM.Enabled = True
                Me.txtTEL.Enabled = True
                Me.dgvList.ReadOnly = True
                Me.dtpNyukinDt.Enabled = False
                Me.cmbNyukinSyubetu1.Enabled = False
                Me.txtNyukin1.Enabled = False
                Me.cmbNyukinSyubetu2.Enabled = False
                Me.txtNyukin2.Enabled = False
                Me.cmbNyukinSyubetu3.Enabled = False
                Me.txtNyukin3.Enabled = False
                Me.cmbNyukinSyubetu4.Enabled = False
                Me.txtNyukin4.Enabled = False
                Me.cmbNyukinSyubetu5.Enabled = False
                Me.txtNyukin5.Enabled = False
                Me.txtBiko1.Enabled = False
                Me.txtBiko2.Enabled = False
                Me.cmdChumonList.Enabled = False
            Case CommonConst.MODE_EditStatus  '変更
                lblShoriMode.Text = "変更"
                Me.txtSeikyuCD.Enabled = False
                Me.btnAllSelect.Enabled = False
                Me.btnAllCancel.Enabled = False
                Me.cmdTouroku.Enabled = True
                Me.cmdModoru.Enabled = True
                Me.btnSearch.Enabled = False
                Me.dtpUriDtFrom.Enabled = False
                Me.dtpUriDtTo.Enabled = False
                Me.txtDenpyoNoFrom.Enabled = False
                Me.txtDenpyoNoTo.Enabled = False
                Me.txtSyukkaNM.Enabled = False
                Me.txtTEL.Enabled = False
                Me.dgvList.ReadOnly = True
                Me.dtpNyukinDt.Enabled = True
                Me.cmbNyukinSyubetu1.Enabled = True
                Me.txtNyukin1.Enabled = True
                Me.cmbNyukinSyubetu2.Enabled = True
                Me.txtNyukin2.Enabled = True
                Me.cmbNyukinSyubetu3.Enabled = True
                Me.txtNyukin3.Enabled = True
                Me.cmbNyukinSyubetu4.Enabled = True
                Me.txtNyukin4.Enabled = True
                Me.cmbNyukinSyubetu5.Enabled = True
                Me.txtNyukin5.Enabled = True
                Me.txtBiko1.Enabled = True
                Me.txtBiko2.Enabled = True
            Case CommonConst.MODE_CancelStatus  '取消
                lblShoriMode.Text = "取消"
                Me.txtSeikyuCD.Enabled = False
                Me.btnAllSelect.Enabled = False
                Me.btnAllCancel.Enabled = False
                Me.cmdTouroku.Enabled = True
                Me.cmdModoru.Enabled = True
                Me.btnSearch.Enabled = False
                Me.dtpUriDtFrom.Enabled = False
                Me.dtpUriDtTo.Enabled = False
                Me.txtDenpyoNoFrom.Enabled = False
                Me.txtDenpyoNoTo.Enabled = False
                Me.txtSyukkaNM.Enabled = False
                Me.txtTEL.Enabled = False
                Me.dgvList.ReadOnly = True
                Me.dtpNyukinDt.Enabled = False
                Me.cmbNyukinSyubetu1.Enabled = False
                Me.txtNyukin1.Enabled = False
                Me.cmbNyukinSyubetu2.Enabled = False
                Me.txtNyukin2.Enabled = False
                Me.cmbNyukinSyubetu3.Enabled = False
                Me.txtNyukin3.Enabled = False
                Me.cmbNyukinSyubetu4.Enabled = False
                Me.txtNyukin4.Enabled = False
                Me.cmbNyukinSyubetu5.Enabled = False
                Me.txtNyukin5.Enabled = False
                Me.txtBiko1.Enabled = False
                Me.txtBiko2.Enabled = False
            Case CommonConst.MODE_InquiryStatus  '照会
                lblShoriMode.Text = "照会"
                Me.txtSeikyuCD.Enabled = False
                Me.btnAllSelect.Enabled = False
                Me.btnAllCancel.Enabled = False
                Me.cmdTouroku.Enabled = False
                Me.cmdModoru.Enabled = True
                Me.btnSearch.Enabled = False
                Me.dtpUriDtFrom.Enabled = False
                Me.dtpUriDtTo.Enabled = False
                Me.txtDenpyoNoFrom.Enabled = False
                Me.txtDenpyoNoTo.Enabled = False
                Me.txtSyukkaNM.Enabled = False
                Me.txtTEL.Enabled = False
                Me.dgvList.ReadOnly = True
                Me.dtpNyukinDt.Enabled = False
                Me.cmbNyukinSyubetu1.Enabled = False
                Me.txtNyukin1.Enabled = False
                Me.cmbNyukinSyubetu2.Enabled = False
                Me.txtNyukin2.Enabled = False
                Me.cmbNyukinSyubetu3.Enabled = False
                Me.txtNyukin3.Enabled = False
                Me.cmbNyukinSyubetu4.Enabled = False
                Me.txtNyukin4.Enabled = False
                Me.cmbNyukinSyubetu5.Enabled = False
                Me.txtNyukin5.Enabled = False
                Me.txtBiko1.Enabled = False
                Me.txtBiko2.Enabled = False
        End Select

    End Sub


    '入金データの初期表示
    Private Sub getNyukinData()


        'データ初期表示

        Dim strSql As String = ""

        Try
            '入金基本（T25）読み込み
            strSql = "SELECT  "
            strSql = strSql & "   会社コード, 入金伝番, 入金日, 請求先コード, 請求先名, 備考１, 備考２, 更新日 "
            strSql = strSql & " FROM t25_nkinhd "

            strSql = strSql & " Where 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and 入金伝番 = '" & _DenpyoNo & "'"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(strSql, RS, reccnt)

            'データがない場合
            If reccnt = 0 Then
                Exit Sub
            End If
            '請求先コード名称
            Me.txtSeikyuCD.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("請求先コード"))
            Me.lblSeikyusakiNM.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("請求先名"))
            Me.txtSeikyuCD.Tag = _db.rmNullDate(ds.Tables(RS).Rows(0)("更新日"), "yyyyMMddHHmmss")
            '入金日
            Me.dtpNyukinDt.Value = _db.rmNullDate(ds.Tables(RS).Rows(0)("入金日"))

            '備考
            Me.txtBiko1.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("備考１"))
            Me.txtBiko2.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("備考２"))

            strSql = "SELECT  "
            strSql = strSql & "    c.出荷先分類 "
            strSql = strSql & " FROM M10_CUSTOMER c "
            strSql = strSql & " Where c.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' "
            strSql = strSql & " And c.取引先コード = '" & Me.txtSeikyuCD.Text & "' "
            ds = _db.selectDB(strSql, RS, reccnt)
            If reccnt <> 0 Then
                Me.txtSeikyuCD.Tag = _db.rmNullStr(ds.Tables(RS).Rows(0)("出荷先分類"))
                Select Case Me.txtSeikyuCD.Tag
                    Case CommonConst.SKBUNRUI_ITAKU
                        Me.lblDenpyoNoTopFrom.Text = "T"
                        Me.lblDenpyoNoTopTo.Text = "T"
                    Case CommonConst.SKBUNRUI_URIAGE
                        Me.lblDenpyoNoTopFrom.Text = "U"
                        Me.lblDenpyoNoTopTo.Text = "U"
                End Select
            End If

            strSql = "SELECT  "
            strSql = strSql & "   会社コード, 入金伝番, 行番, 入金種別, 入金種別名, 入金分類, 金額 "
            strSql = strSql & " FROM T26_NKINDT "
            strSql = strSql & " Where 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and 入金伝番 = '" & _DenpyoNo & "'"

            reccnt = 0
            ds = _db.selectDB(strSql, RS, reccnt)
            'データがない場合
            If reccnt = 0 Then
                Exit Sub
            End If
            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                Select Case _db.rmNullStr(ds.Tables(RS).Rows(index)("行番"))
                    Case "1"
                        Me.txtNyukin1.Text = Decimal.Parse(_db.rmNullInt(_db.rmNullStr(ds.Tables(RS).Rows(index)("金額")))).ToString("#,###")
                        Me.cmbNyukinSyubetu1.SelectedValue = _db.rmNullInt(_db.rmNullStr(ds.Tables(RS).Rows(index)("入金種別")))
                    Case "2"
                        Me.txtNyukin2.Text = Decimal.Parse(_db.rmNullInt(_db.rmNullStr(ds.Tables(RS).Rows(index)("金額")))).ToString("#,###")
                        Me.cmbNyukinSyubetu2.SelectedValue = _db.rmNullInt(_db.rmNullStr(ds.Tables(RS).Rows(index)("入金種別")))
                    Case "3"
                        Me.txtNyukin3.Text = Decimal.Parse(_db.rmNullInt(_db.rmNullStr(ds.Tables(RS).Rows(index)("金額")))).ToString("#,###")
                        Me.cmbNyukinSyubetu3.SelectedValue = _db.rmNullInt(_db.rmNullStr(ds.Tables(RS).Rows(index)("入金種別")))
                    Case "4"
                        Me.txtNyukin4.Text = Decimal.Parse(_db.rmNullInt(_db.rmNullStr(ds.Tables(RS).Rows(index)("金額")))).ToString("#,###")
                        Me.cmbNyukinSyubetu4.SelectedValue = _db.rmNullInt(_db.rmNullStr(ds.Tables(RS).Rows(index)("入金種別")))
                    Case "5"
                        Me.txtNyukin5.Text = Decimal.Parse(_db.rmNullInt(_db.rmNullStr(ds.Tables(RS).Rows(index)("金額")))).ToString("#,###")
                        Me.cmbNyukinSyubetu5.SelectedValue = _db.rmNullInt(_db.rmNullStr(ds.Tables(RS).Rows(index)("入金種別")))
                End Select
            Next

            '合計金額計算
            RecalcSum()

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   入金種別コンボボックスを初期化
    '   （処理概要）　汎用マスタ（入金種別）よりデータを取得し、コンボボックスにセットする
    '-------------------------------------------------------------------------------
    Private Sub clearCmbNyukinSyubetu()

        Dim strSql As String = ""
        '会社マスタをコンボボックスにセット
        Try
            strSql = "SELECT '0' AS 可変キー,'　' AS 文字１, 0 AS 表示順, '0' AS 文字２ "
            strSql = strSql & " UNION "
            strSql = strSql & " SELECT "
            strSql = strSql & "    可変キー, 文字１, 表示順, 文字２ "
            strSql = strSql & " FROM M90_HANYO "
            strSql = strSql & " Where 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and 固定キー = '" & CommonConst.HANYO_NYUKINSYUBETU & "' "
            strSql = strSql & " order by 表示順 "
            Dim reccnt As Integer = 0
            Dim ds1 As DataSet = _db.selectDB(strSql, RS, reccnt)
            Dim ds2 As DataSet = _db.selectDB(strSql, RS, reccnt)
            Dim ds3 As DataSet = _db.selectDB(strSql, RS, reccnt)
            Dim ds4 As DataSet = _db.selectDB(strSql, RS, reccnt)
            Dim ds5 As DataSet = _db.selectDB(strSql, RS, reccnt)

            cmbNyukinSyubetu1.DataSource = ds1.Tables(RS)
            cmbNyukinSyubetu1.DisplayMember = "文字１"
            cmbNyukinSyubetu1.ValueMember = "可変キー"
            cmbNyukinSyubetu2.DataSource = ds2.Tables(RS)
            cmbNyukinSyubetu2.DisplayMember = "文字１"
            cmbNyukinSyubetu2.ValueMember = "可変キー"
            cmbNyukinSyubetu3.DataSource = ds3.Tables(RS)
            cmbNyukinSyubetu3.DisplayMember = "文字１"
            cmbNyukinSyubetu3.ValueMember = "可変キー"
            cmbNyukinSyubetu4.DataSource = ds4.Tables(RS)
            cmbNyukinSyubetu4.DisplayMember = "文字１"
            cmbNyukinSyubetu4.ValueMember = "可変キー"
            cmbNyukinSyubetu5.DataSource = ds5.Tables(RS)
            cmbNyukinSyubetu5.DisplayMember = "文字１"
            cmbNyukinSyubetu5.ValueMember = "可変キー"
        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '--------------------------------
    '抽出条件からデータ取得
    '--------------------------------
    Private Sub getList()
        Dim strSql As String = ""
        Try
            '取得したデータをDataGrdViewに反映
            strSql = "SELECT "
            strSql = strSql & "  t20.売上日, t21.売上伝番, t21.売上伝番枝番, t21.行番, t20.出荷先名, t21.商品名, t21.荷姿形状, t21.課税区分, h2.文字２ as 課税区分名 "
            strSql = strSql & "  , t21.売上数量, t21.単位, t21.売上単価, t21.売上金額, case t21.行番 when 1 then t20.消費税計 else 0 end 消費税, t21.更新日, t20.出荷先コード "
            strSql = strSql & " FROM T20_URIGHD  t20 "
            strSql = strSql & "    inner join T21_URIGDT t21 on t21.会社コード = t20.会社コード and t21.売上伝番 = t20.売上伝番  and t21.売上伝番枝番 = t20.売上伝番枝番 "
            strSql = strSql & "    left join M90_HANYO h2 on h2.会社コード = t21.会社コード and h2.固定キー = '" & CommonConst.HANYO_KAZEI_KBN & "' and h2.可変キー = t21.課税区分 "
            strSql = strSql & " Where t20.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and t20.取消区分 =0 "
            If _SelectMode = CommonConst.MODE_ADDNEW Then  '登録
                strSql = strSql & " And t21.入金有無 =0 "
            Else
                strSql = strSql & "  and t21.入金伝番 = '" & _DenpyoNo & "'"
            End If
            '請求先コード
            If Me.txtSeikyuCD.Text <> "" Then
                strSql = strSql & "  And t20.請求先コード = '" & txtSeikyuCD.Text & "' "
            End If
            '売上日From
            If _db.rmNullDate(Me.dtpUriDtFrom.Value) <> "" Then
                strSql = strSql & N & " and t20.売上日 >= '" & _db.rmNullDate(Me.dtpUriDtFrom.Value, "yyyy/MM/dd") & "' "
            End If
            '売上日To
            If _db.rmNullDate(Me.dtpUriDtTo.Value) <> "" Then
                strSql = strSql & N & " and t20.売上日 <= '" & _db.rmNullDate(Me.dtpUriDtTo.Value, "yyyy/MM/dd") & "' "
            End If
            '伝票番号From
            If Me.txtDenpyoNoFrom.Text <> "" Then
                strSql = strSql & N & " and t20.入金伝番 >= '" & Me.lblDenpyoNoTopFrom.Text & Me.txtDenpyoNoFrom.Text & "' "
            End If
            '伝票番号To
            If Me.txtDenpyoNoTo.Text <> "" Then
                strSql = strSql & N & " and t20.入金伝番 <= '" & Me.lblDenpyoNoTopTo.Text & Me.txtDenpyoNoTo.Text & "' "
            End If
            '出荷先名
            If Me.txtSyukkaNM.Text <> "" Then
                strSql = strSql & "  and t20.出荷先名 like '%" & txtSyukkaNM.Text & "%' "
            End If
            '電話番号
            If Me.txtTEL.Text <> "" Then
                Dim reg As New Regex("[^\d]")
                Dim strDes As String = reg.Replace(txtTEL.Text, "")
                strSql = strSql & "  and t20.電話番号検索用 like '%" & strDes & "%' "
            End If
            strSql = strSql & " order by  t20.売上日, t21.売上伝番, t21.売上伝番枝番, t21.行番  "

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
                dgvList.Rows(index).Cells(COLNO_NO).Value = index + 1
                dgvList.Rows(index).Cells(COLNO_Select).Value = True
                dgvList.Rows(index).Cells(COLNO_UriYMD).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("売上日"))
                dgvList.Rows(index).Cells(COLNO_DenEdaGyo).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("売上伝番")) & "-" & _db.rmNullStr(ds.Tables(RS).Rows(index)("売上伝番枝番")) & "-" & _db.rmNullStr(ds.Tables(RS).Rows(index)("行番"))
                dgvList.Rows(index).Cells(COLNO_SyukkaNM).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("出荷先名"))
                dgvList.Rows(index).Cells(COLNO_ShohinNM).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("商品名")) & "　" & _db.rmNullStr(ds.Tables(RS).Rows(index)("荷姿形状"))
                dgvList.Rows(index).Cells(COLNO_ZEIKBN).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("課税区分名"))
                dgvList.Rows(index).Cells(COLNO_SURYOU).Value = Decimal.Parse(_db.rmNullStr(ds.Tables(RS).Rows(index)("売上数量"))).ToString("N2")
                dgvList.Rows(index).Cells(COLNO_TANNI).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("単位"))
                dgvList.Rows(index).Cells(COLNO_TANKA).Value = Decimal.Parse(_db.rmNullStr(ds.Tables(RS).Rows(index)("売上単価"))).ToString("N2")
                dgvList.Rows(index).Cells(COLNO_MONEY).Value = Decimal.Parse(_db.rmNullStr(ds.Tables(RS).Rows(index)("売上金額"))).ToString("N0")
                dgvList.Rows(index).Cells(COLNO_TAX).Value = Decimal.Parse(_db.rmNullStr(ds.Tables(RS).Rows(index)("消費税"))).ToString("N0")
                dgvList.Rows(index).Cells(COLNO_TIMESTAMP).Value = DateTime.Parse(_db.rmNullStr(ds.Tables(RS).Rows(index)("更新日"))).ToString("yyyyMMddhhmmss")
                dgvList.Rows(index).Cells(COLNO_DenpyoNo).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("売上伝番"))
                dgvList.Rows(index).Cells(COLNO_DenpyoNoEda).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("売上伝番枝番"))
                dgvList.Rows(index).Cells(COLNO_DenpyoGyo).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("行番"))
                dgvList.Rows(index).Cells(COLNO_SyukkaCD).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("出荷先コード"))
            Next

            '合計金額計算
            RecalcSum()
            '対象データの有無チェック
            If ds.Tables(RS).Rows.Count = 0 Then
                '選択ボタンを非活性
                Select Case _SelectMode
                    Case CommonConst.MODE_ADDNEW            '登録
                        Me.cmdTouroku.Enabled = False
                        Me.btnAllSelect.Enabled = False
                        Me.btnAllCancel.Enabled = False
                        Me.dgvList.ReadOnly = True
                        Me.dtpNyukinDt.Enabled = False
                        Me.cmbNyukinSyubetu1.Enabled = False
                        Me.txtNyukin1.Enabled = False
                        Me.cmbNyukinSyubetu2.Enabled = False
                        Me.txtNyukin2.Enabled = False
                        Me.cmbNyukinSyubetu3.Enabled = False
                        Me.txtNyukin3.Enabled = False
                        Me.cmbNyukinSyubetu4.Enabled = False
                        Me.txtNyukin4.Enabled = False
                        Me.cmbNyukinSyubetu5.Enabled = False
                        Me.txtNyukin5.Enabled = False
                        Me.txtBiko1.Enabled = False
                        Me.txtBiko2.Enabled = False
                        Me.cmdChumonList.Enabled = False
                    Case CommonConst.MODE_EditStatus        '変更
                        Me.cmdTouroku.Enabled = False
                    Case CommonConst.MODE_CancelStatus      '取消
                        Me.cmdTouroku.Enabled = False
                    Case CommonConst.MODE_InquiryStatus     '照会
                        Me.cmdTouroku.Enabled = False
                End Select
                _msgHd.dspMSG("NonData")
                Exit Sub
            Else
                '選択ボタンを活性
                Select Case _SelectMode
                    Case CommonConst.MODE_ADDNEW            '登録
                        Me.cmdTouroku.Enabled = True
                        Me.btnAllSelect.Enabled = True
                        Me.btnAllCancel.Enabled = True
                        Me.dgvList.ReadOnly = False
                        Me.dtpNyukinDt.Enabled = True
                        Me.cmbNyukinSyubetu1.Enabled = True
                        Me.txtNyukin1.Enabled = True
                        Me.cmbNyukinSyubetu2.Enabled = True
                        Me.txtNyukin2.Enabled = True
                        Me.cmbNyukinSyubetu3.Enabled = True
                        Me.txtNyukin3.Enabled = True
                        Me.cmbNyukinSyubetu4.Enabled = True
                        Me.txtNyukin4.Enabled = True
                        Me.cmbNyukinSyubetu5.Enabled = True
                        Me.txtNyukin5.Enabled = True
                        Me.txtBiko1.Enabled = True
                        Me.txtBiko2.Enabled = True
                        Me.cmdChumonList.Enabled = True
                        Me.dgvList.Columns(COLNO_UriYMD).ReadOnly = True
                        Me.dgvList.Columns(COLNO_DenEdaGyo).ReadOnly = True
                        Me.dgvList.Columns(COLNO_SyukkaNM).ReadOnly = True
                        Me.dgvList.Columns(COLNO_ShohinNM).ReadOnly = True
                        Me.dgvList.Columns(COLNO_ZEIKBN).ReadOnly = True
                        Me.dgvList.Columns(COLNO_SURYOU).ReadOnly = True
                        Me.dgvList.Columns(COLNO_TANNI).ReadOnly = True
                        Me.dgvList.Columns(COLNO_TANKA).ReadOnly = True
                        Me.dgvList.Columns(COLNO_MONEY).ReadOnly = True
                        Me.dgvList.Columns(COLNO_TAX).ReadOnly = True
                    Case CommonConst.MODE_EditStatus        '変更
                        Me.cmdTouroku.Enabled = True
                    Case CommonConst.MODE_CancelStatus      '取消
                        Me.cmdTouroku.Enabled = True
                    Case CommonConst.MODE_InquiryStatus     '照会
                        Me.cmdTouroku.Enabled = False
                End Select


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
    Private Sub cmdModoru_Click(sender As Object, e As EventArgs) Handles cmdModoru.Click

        _parentForm.Show()                                              ' 前画面を表示
        _parentForm.Enabled = True                                      ' 前画面の使用を可能にする
        _parentForm.Activate()                                          ' 前画面をアクティブにする

        Me.Dispose()
    End Sub
    '取引先選択ウインドウオープン処理
    Private Sub SeikyusakiSelectWindowOpen()

        Dim openForm As frmC10F20_Torihikisaki = New frmC10F20_Torihikisaki(_msgHd, _db, CommonConst.TORIHIKISAKI_TARGET_KBN_SEIKYU)
        openForm.ShowDialog()                      '画面表示

        '選択されている場合
        If openForm.Selected Then
            '画面に値をセット

            '請求先コード
            Me.txtSeikyuCD.Text = openForm.GetValTorihikisakiCd
            '仕入先名
            Me.lblSeikyusakiNM.Text = openForm.GetValTorihikisakiName
            Dim strSql As String = ""
            Dim reccnt As Integer = 0
            Dim ds As DataSet
            strSql = "SELECT  "
            strSql = strSql & "    c.出荷先分類 "
            strSql = strSql & " FROM M10_CUSTOMER c "
            strSql = strSql & " Where c.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' "
            strSql = strSql & " And c.取引先コード = '" & openForm.GetValTorihikisakiCd & "' "
            ds = _db.selectDB(strSql, RS, reccnt)
            If reccnt = 0 Then
                Exit Sub
            End If
            Me.txtSeikyuCD.Tag = _db.rmNullStr(ds.Tables(RS).Rows(0)("出荷先分類"))
            Select Case Me.txtSeikyuCD.Tag
                Case CommonConst.SKBUNRUI_ITAKU
                    Me.lblDenpyoNoTopFrom.Text = "T"
                    Me.lblDenpyoNoTopTo.Text = "T"
                Case CommonConst.SKBUNRUI_URIAGE
                    Me.lblDenpyoNoTopFrom.Text = "U"
                    Me.lblDenpyoNoTopTo.Text = "U"
            End Select
        End If

        openForm = Nothing

    End Sub

    Private Sub txtSeikyuCD_DoubleClick(sender As Object, e As EventArgs) Handles txtSeikyuCD.DoubleClick
        '取引先選択ウインドウオープン処理
        SeikyusakiSelectWindowOpen()

    End Sub
    '検索ボタンクリック時
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        getList()
    End Sub
    '全選択ボタンクリック
    Private Sub btnAllSelect_Click(sender As Object, e As EventArgs) Handles btnAllSelect.Click
        For index As Integer = 0 To Me.dgvList.RowCount - 1
            Me.dgvList.Rows(index).Cells(COLNO_Select).Value = True
        Next
        '合計金額計算
        RecalcSum()
    End Sub
    '全解除ボタンクリック
    Private Sub btnAllCancel_Click(sender As Object, e As EventArgs) Handles btnAllCancel.Click
        For index As Integer = 0 To Me.dgvList.RowCount - 1
            Me.dgvList.Rows(index).Cells(COLNO_Select).Value = False
        Next
        '合計金額計算
        RecalcSum()
    End Sub
    '合計金額計算
    Private Sub RecalcSum()
        Try
            Dim decMoney As Decimal = 0
            Dim decTax As Decimal = 0
            Dim intCount As Integer = 0

            For index As Integer = 0 To Me.dgvList.RowCount - 1
                If Me.dgvList.Rows(index).Cells(COLNO_Select).Value = True Then
                    decMoney += Decimal.Parse(Me.dgvList.Rows(index).Cells(COLNO_MONEY).Value)
                    decTax += Decimal.Parse(Me.dgvList.Rows(index).Cells(COLNO_TAX).Value)
                    intCount += 1
                End If
            Next
            Me.lblSelectCount.Text = intCount.ToString("N0")
            Me.lblSumMoney.Text = decMoney.ToString("N0")
            Me.lblSumTax.Text = decTax.ToString("N0")
            Me.lblTotal.Text = (decMoney + decTax).ToString("N0")
        Catch
        End Try

    End Sub

    Private Sub dgvList_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellValueChanged
        If e.ColumnIndex = COLNO_Select Then
            '合計金額計算
            RecalcSum()
        End If

    End Sub

    Private Sub dgvList_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvList.CurrentCellDirtyStateChanged

        'ここでコミットすることで、ユーザがDataGridViewCheckBoxColumn列のチェックを変更したらすぐにCellValueChangedイベントが走るようにする

        dgvList.CommitEdit(DataGridViewDataErrorContexts.Commit)

    End Sub

    'コントロールのキープレスイベント
    Private Sub dtpUriDtFrom_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDenpyoNoFrom.KeyPress, txtDenpyoNoTo.KeyPress, dtpUriDtFrom.KeyPress, dtpUriDtTo.KeyPress,
                                                                                        txtSyukkaNM.KeyPress, txtTEL.KeyPress, dtpNyukinDt.KeyPress,
                                                                                        cmbNyukinSyubetu1.KeyPress, txtNyukin1.KeyPress, cmbNyukinSyubetu2.KeyPress, txtNyukin2.KeyPress,
                                                                                        cmbNyukinSyubetu3.KeyPress, txtNyukin3.KeyPress, cmbNyukinSyubetu4.KeyPress, txtNyukin4.KeyPress,
                                                                                        cmbNyukinSyubetu5.KeyPress, txtNyukin5.KeyPress, txtBiko1.KeyPress, txtBiko2.KeyPress, txtSeikyuCD.KeyPress

        UtilMDL.UtilClass.moveNextFocus(Me, e)
    End Sub
    '-------------------------------------------------------------------------------
    '　フォーカス取得イベント
    '-------------------------------------------------------------------------------
    Private Sub ctl_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDenpyoNoFrom.GotFocus, txtDenpyoNoTo.GotFocus, dtpUriDtFrom.GotFocus, dtpUriDtTo.GotFocus,
                                                                                        txtSyukkaNM.GotFocus, txtTEL.GotFocus, dtpNyukinDt.GotFocus,
                                                                                        cmbNyukinSyubetu1.GotFocus, txtNyukin1.GotFocus, cmbNyukinSyubetu2.GotFocus, txtNyukin2.GotFocus,
                                                                                        cmbNyukinSyubetu3.GotFocus, txtNyukin3.GotFocus, cmbNyukinSyubetu4.GotFocus, txtNyukin4.GotFocus,
                                                                                        cmbNyukinSyubetu5.GotFocus, txtNyukin5.GotFocus, txtBiko1.GotFocus, txtBiko2.GotFocus, txtSeikyuCD.GotFocus

        'フォーカス取得時、入力パラメタのコントロールを全選択状態とする
        Call UtilClass.selAll(sender)

    End Sub
    '登録ボタンクリック時
    Private Sub cmdTouroku_Click(sender As Object, e As EventArgs) Handles cmdTouroku.Click

        Dim piRtn As Integer

        '確認メッセージを表示する
        piRtn = _msgHd.dspMSG("confirmInsert")  '登録します。よろしいですか？
        If piRtn = vbNo Then
            Exit Sub
        End If


        '排他チェック処理
        For index As Integer = 0 To Me.dgvList.RowCount - 1
            If Me.dgvList.Rows(index).Cells(COLNO_Select).Value = True Then
                Try
                    Dim strSql As String = ""
                    Dim reccnt As Integer = 0
                    Dim ds As DataSet
                    strSql = "SELECT  "
                    strSql = strSql & "    c.更新日,c.更新者 "
                    strSql = strSql & " FROM T21_URIGDT c "
                    strSql = strSql & " Where c.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' "
                    strSql = strSql & " And c.売上伝番 = '" & Me.dgvList.Rows(index).Cells(COLNO_DenpyoNo).Value & "' "
                    strSql = strSql & " And c.売上伝番枝番 = " & Me.dgvList.Rows(index).Cells(COLNO_DenpyoNoEda).Value
                    strSql = strSql & " And c.行番 = " & Me.dgvList.Rows(index).Cells(COLNO_DenpyoGyo).Value
                    ds = _db.selectDB(strSql, RS, reccnt)
                    If reccnt = 0 Then
                        Exit Sub
                    End If
                    If Me.dgvList.Rows(index).Cells(COLNO_TIMESTAMP).Value <> DateTime.Parse(ds.Tables(RS).Rows(0)("更新日")).ToString("yyyyMMddhhmmss") Then
                        '登録終了メッセージ
                        Dim strMessage As String = ""
                        strMessage = "更新者：" & ds.Tables(RS).Rows(0)("更新者") & " 更新日時：" & ds.Tables(RS).Rows(0)("更新日").ToString
                        _msgHd.dspMSG("Exclusion", strMessage)
                        Me.Close()
                        Exit Sub
                    End If
                Catch
                End Try
            End If
        Next
        '排他チェックおわり
        'DB更新
        Dim strMessageID As String = ""
        Select Case _SelectMode     '（1:登録、2:変更、3:取消、4:照会)
            Case CommonConst.MODE_ADDNEW   '登録
                DataAddNew()
                strMessageID = "UpdateNyukinInfo"
            Case CommonConst.MODE_EditStatus   '変更
                DataEdit()
                strMessageID = "EditNyukinInfo"
            Case CommonConst.MODE_CancelStatus   '取消
                DataCancel()
                strMessageID = "CancelNyukinInfo"
            Case CommonConst.MODE_InquiryStatus   '照会
                '照会はここに来ません・・・
        End Select


        '登録終了メッセージ
        _msgHd.dspMSG(strMessageID, "伝票番号：" & lblDenpyoNo.Text)

        '画面遷移
        Select Case _SelectMode     '（1:登録、2:変更、3:取消、4:照会)
            Case CommonConst.MODE_ADDNEW   '登録
                '画面初期状態
                Me.txtSeikyuCD.Text = ""
                Me.lblSeikyusakiNM.Text = ""
                Me.dtpUriDtFrom.Value = Nothing
                Me.dtpUriDtTo.Value = Nothing
                Me.txtDenpyoNoFrom.Text = ""
                Me.txtDenpyoNoTo.Text = ""
                Me.txtSyukkaNM.Text = ""
                Me.txtTEL.Text = ""
                getList()   'リスト再表示
                Me.dtpNyukinDt.Value = Now
                Me.cmbNyukinSyubetu1.SelectedIndex = 0
                Me.txtNyukin1.Text = ""
                Me.cmbNyukinSyubetu2.SelectedIndex = 0
                Me.txtNyukin2.Text = ""
                Me.cmbNyukinSyubetu3.SelectedIndex = 0
                Me.txtNyukin3.Text = ""
                Me.cmbNyukinSyubetu4.SelectedIndex = 0
                Me.txtNyukin4.Text = ""
                Me.cmbNyukinSyubetu5.SelectedIndex = 0
                Me.txtNyukin5.Text = ""
                Me.txtBiko1.Text = ""
                Me.txtBiko2.Text = ""
                _DenpyoNo = _comLogc.GetDenpyoNo(frmC01F10_Login.loginValue.BumonCD, CommonConst.SAIBAN_NYUKIN)
                Me.lblDenpyoNo.Text = _DenpyoNo
            Case CommonConst.MODE_EditStatus   '変更
                _parentForm.Show()                                              ' 前画面を表示
                _parentForm.Enabled = True                                      ' 前画面の使用を可能にする
                _parentForm.Activate()                                          ' 前画面をアクティブにする

                Me.Dispose()
            Case CommonConst.MODE_CancelStatus   '取消
                _parentForm.Show()                                              ' 前画面を表示
                _parentForm.Enabled = True                                      ' 前画面の使用を可能にする
                _parentForm.Activate()                                          ' 前画面をアクティブにする

                Me.Dispose()
            Case CommonConst.MODE_InquiryStatus   '照会
                '照会はここに来ません・・・
        End Select

    End Sub

    'データ取消処理
    Private Sub DataCancel()
        Try
            'トランザクション開始
            If Not _db.isTransactionOpen Then
                _db.beginTran()
            End If

            '売上明細(T21)
            Dim Sql As String = ""
            Sql = ""
            Sql = Sql & N & "UPDATE T21_URIGDT  "
            Sql = Sql & N & " SET 入金有無 = 0  "
            Sql = Sql & N & "  , 入金伝番 = Null  "
            Sql = Sql & N & "  , 入金日 = Null  "
            Sql = Sql & N & "  , 更新者 = '" & frmC01F10_Login.loginValue.TantoCD & "' "         '更新者
            Sql = Sql & N & "  , 更新日 = current_timestamp "                                    '更新日
            Sql = Sql & N & " Where 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' "
            Sql = Sql & N & " And 入金伝番 = '" & _DenpyoNo & "' "
            _db.executeDB(Sql)

            '入金基本（T25_NKINHD）
            Sql = ""
            Sql = Sql & N & "UPDATE T25_NKINHD  "
            Sql = Sql & N & " SET 取消区分 = '1'  "
            Sql = Sql & N & "  , 更新者 = '" & frmC01F10_Login.loginValue.TantoCD & "' "         '更新者
            Sql = Sql & N & "  , 更新日 = current_timestamp "                                    '更新日
            Sql = Sql & N & "  Where 会社コード =  '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "       '会社コード
            Sql = Sql & N & "   and 入金伝番 = '" & Me.lblDenpyoNo.Text & "' "                 '注文伝番
            _db.executeDB(Sql)

            'ログ出力
            '操作履歴ログ作成
            _comLogc.Insert_L01_ProcLog(frmC01F10_Login.loginValue.BumonCD, DBNull.Value, _SelectID, CommonConst.PROGRAM_UPDATE, "9",
                                                        lblDenpyoNo.Text, CommonConst.MODE_CancelStatus_NAME, DBNull.Value, DBNull.Value, DBNull.Value,
                                                        DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _userId)

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

    'データ新規登録処理
    Private Sub DataAddNew()
        Try
            Dim strSQL As String = ""
            For index As Integer = 0 To Me.dgvList.RowCount - 1
                If Me.dgvList.Rows(index).Cells(COLNO_Select).Value = True Then
                    '売上明細（T21_URIGDT）
                    strSQL = ""
                    strSQL = strSQL & N & "UPDATE T21_URIGDT  "
                    strSQL = strSQL & N & " SET 入金有無 = 1  "
                    strSQL = strSQL & N & " , 入金伝番 = '" & Me.lblDenpyoNo.Text & "'"
                    strSQL = strSQL & N & " , 入金日 = '" & Me.dtpNyukinDt.Text & "'"
                    strSQL = strSQL & N & "  , 更新者 = '" & frmC01F10_Login.loginValue.TantoCD & "' "         '更新者
                    strSQL = strSQL & N & "  , 更新日 = current_timestamp "                                    '更新日
                    strSQL = strSQL & N & " Where 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' "
                    strSQL = strSQL & N & " And 売上伝番 = '" & Me.dgvList.Rows(index).Cells(COLNO_DenpyoNo).Value & "' "
                    strSQL = strSQL & N & " And 売上伝番枝番 = " & Me.dgvList.Rows(index).Cells(COLNO_DenpyoNoEda).Value
                    strSQL = strSQL & N & " And 行番 = " & Me.dgvList.Rows(index).Cells(COLNO_DenpyoGyo).Value
                    _db.executeDB(strSQL)
                End If
            Next
            'レコード追加 入金基本（T25_NKINHD）
            Dim decNyukin1 As Decimal = 0       '入金種別の入金分類１の合計
            If cmbNyukinSyubetu1.ValueMember = "1" Then
                decNyukin1 += Decimal.Parse(_db.rmNullInt(Me.txtNyukin1.Text))
            End If
            If cmbNyukinSyubetu2.ValueMember = "1" Then
                decNyukin1 += Decimal.Parse(_db.rmNullInt(Me.txtNyukin2.Text))
            End If
            If cmbNyukinSyubetu3.ValueMember = "1" Then
                decNyukin1 += Decimal.Parse(_db.rmNullInt(Me.txtNyukin3.Text))
            End If
            If cmbNyukinSyubetu4.ValueMember = "1" Then
                decNyukin1 += Decimal.Parse(_db.rmNullInt(Me.txtNyukin4.Text))
            End If
            If cmbNyukinSyubetu5.ValueMember = "1" Then
                decNyukin1 += Decimal.Parse(_db.rmNullInt(Me.txtNyukin5.Text))
            End If
            Dim decNyukin2 As Decimal = 0       '入金種別の入金分類２の合計
            If cmbNyukinSyubetu1.ValueMember = "2" Then
                decNyukin2 += Decimal.Parse(_db.rmNullInt(Me.txtNyukin1.Text))
            End If
            If cmbNyukinSyubetu2.ValueMember = "2" Then
                decNyukin2 += Decimal.Parse(_db.rmNullInt(Me.txtNyukin2.Text))
            End If
            If cmbNyukinSyubetu3.ValueMember = "2" Then
                decNyukin2 += Decimal.Parse(_db.rmNullInt(Me.txtNyukin3.Text))
            End If
            If cmbNyukinSyubetu4.ValueMember = "2" Then
                decNyukin2 += Decimal.Parse(_db.rmNullInt(Me.txtNyukin4.Text))
            End If
            If cmbNyukinSyubetu5.ValueMember = "2" Then
                decNyukin2 += Decimal.Parse(_db.rmNullInt(Me.txtNyukin5.Text))
            End If

            strSQL = ""
            strSQL = strSQL & N & "INSERT INTO T25_NKINHD ( "
            strSQL = strSQL & N & "    会社コード "
            strSQL = strSQL & N & "  , 入金伝番 "
            strSQL = strSQL & N & "  , 入金日 "
            strSQL = strSQL & N & "  , 請求先コード "
            strSQL = strSQL & N & "  , 請求先名 "
            strSQL = strSQL & N & "  , 売上明細数 "
            strSQL = strSQL & N & "  , 売上金額計 "
            strSQL = strSQL & N & "  , 売上消費税計 "
            strSQL = strSQL & N & "  , 現金振込計 "
            strSQL = strSQL & N & "  , 手数料計 "
            strSQL = strSQL & N & "  , 入金額計 "
            strSQL = strSQL & N & "  , 備考１ "
            strSQL = strSQL & N & "  , 備考２ "
            strSQL = strSQL & N & "  , 取消区分 "
            strSQL = strSQL & N & "  , 更新者 "
            strSQL = strSQL & N & "  , 更新日 "
            strSQL = strSQL & N & ") VALUES ( "
            strSQL = strSQL & N & "    '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "     '会社コード
            strSQL = strSQL & N & "  , '" & Me.lblDenpyoNo.Text & "' "                              '入金伝番
            strSQL = strSQL & N & "  , '" & Me.dtpNyukinDt.Text & "' "                              '入金日
            strSQL = strSQL & N & "  , '" & Me.txtSeikyuCD.Text & "' "                              '請求先コード
            strSQL = strSQL & N & "  , '" & Me.lblSeikyusakiNM.Text & "' "                          '請求先名
            strSQL = strSQL & N & "  , " & Decimal.Parse(Me.lblSelectCount.Text).ToString           '売上明細数
            strSQL = strSQL & N & "  , " & Decimal.Parse(Me.lblSumMoney.Text).ToString              '売上金額計
            strSQL = strSQL & N & "  , " & Decimal.Parse(Me.lblSumTax.Text).ToString                '売上消費税計
            strSQL = strSQL & N & "  , " & decNyukin1                                               '現金振込計
            strSQL = strSQL & N & "  , " & decNyukin2                                               '手数料計
            strSQL = strSQL & N & "  , " & Decimal.Parse(_db.rmNullInt(Me.lblNyukinSum.Text)).ToString             '入金額計
            strSQL = strSQL & N & "  , '" & Me.txtBiko1.Text & "' "                                 '備考１
            strSQL = strSQL & N & "  , '" & Me.txtBiko2.Text & "' "                                 '備考２
            strSQL = strSQL & N & "  , '0' "              '取消区分
            strSQL = strSQL & N & "  , '" & frmC01F10_Login.loginValue.TantoCD & "' "         '更新者
            strSQL = strSQL & N & "  , current_timestamp "                                    '更新日
            strSQL = strSQL & N & ") "
            _db.executeDB(strSQL)

            Dim strVALUES As String
            '入金明細（T26_NKINDT）
            strSQL = ""
            strSQL = strSQL & N & "INSERT INTO T26_NKINDT ( "
            strSQL = strSQL & N & "    会社コード "
            strSQL = strSQL & N & "  , 入金伝番 "
            strSQL = strSQL & N & "  , 行番 "
            strSQL = strSQL & N & "  , 入金種別 "
            strSQL = strSQL & N & "  , 入金種別名 "
            strSQL = strSQL & N & "  , 入金分類 "
            strSQL = strSQL & N & "  , 金額 "

            strSQL = strSQL & N & "  , 更新者 "
            strSQL = strSQL & N & "  , 更新日 "
            strSQL = strSQL & N & ") VALUES ( "
            strSQL = strSQL & N & "    '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "       '会社コード
            strSQL = strSQL & N & "  , '" & Me.lblDenpyoNo.Text & "' "                                '入金伝番
            Dim intCount As Integer = 1
            Dim selectedDataRow As DataRowView
            If Me.cmbNyukinSyubetu1.SelectedValue <> "0" Then
                selectedDataRow = CType(Me.cmbNyukinSyubetu1.SelectedItem, DataRowView)
                strVALUES = ""
                strVALUES = strVALUES & N & "  , " & intCount                                                   '行番
                strVALUES = strVALUES & N & "  , " & Me.cmbNyukinSyubetu1.SelectedValue                           '入金種別
                strVALUES = strVALUES & N & "  , '" & Me.cmbNyukinSyubetu1.DisplayMember & "' "                 '入金種別名
                strVALUES = strVALUES & N & "  , '" & selectedDataRow("文字２").ToString & "' "                 '入金分類
                strVALUES = strVALUES & N & "  , " & Decimal.Parse(_db.rmNullInt(Me.txtNyukin1.Text)).ToString        '売上金額
                strVALUES = strVALUES & N & "  , '" & frmC01F10_Login.loginValue.TantoCD & "' "                       '更新者
                strVALUES = strVALUES & N & "  , current_timestamp "                                                  '更新日
                strVALUES = strVALUES & N & ") "
                _db.executeDB(strSQL & strVALUES)
                intCount += 1
            End If
            If Me.cmbNyukinSyubetu2.SelectedValue <> "0" Then
                selectedDataRow = CType(Me.cmbNyukinSyubetu2.SelectedItem, DataRowView)
                strVALUES = ""
                strVALUES = strVALUES & N & "  , " & intCount                                                   '行番
                strVALUES = strVALUES & N & "  , '" & Me.cmbNyukinSyubetu2.SelectedValue & "' "                   '入金種別
                strVALUES = strVALUES & N & "  , '" & Me.cmbNyukinSyubetu2.DisplayMember & "' "                 '入金種別名
                strVALUES = strVALUES & N & "  , '" & selectedDataRow("文字２").ToString & "' "                 '入金分類
                strVALUES = strVALUES & N & "  , " & Decimal.Parse(_db.rmNullInt(Me.txtNyukin2.Text)).ToString  '売上金額
                strVALUES = strVALUES & N & "  , '" & frmC01F10_Login.loginValue.TantoCD & "' "                 '更新者
                strVALUES = strVALUES & N & "  , current_timestamp "                                            '更新日
                strVALUES = strVALUES & N & ") "
                _db.executeDB(strSQL & strVALUES)
                intCount += 1
            End If
            If Me.cmbNyukinSyubetu3.SelectedValue <> "0" Then
                selectedDataRow = CType(Me.cmbNyukinSyubetu2.SelectedItem, DataRowView)
                strVALUES = ""
                strVALUES = strVALUES & N & "  , " & intCount                                                   '行番
                strVALUES = strVALUES & N & "  , '" & Me.cmbNyukinSyubetu3.SelectedValue & "' "                   '入金種別
                strVALUES = strVALUES & N & "  , '" & Me.cmbNyukinSyubetu3.DisplayMember & "' "                 '入金種別名
                strVALUES = strVALUES & N & "  , '" & selectedDataRow("文字２").ToString & "' "                 '入金分類
                strVALUES = strVALUES & N & "  , " & Decimal.Parse(_db.rmNullInt(Me.txtNyukin3.Text)).ToString  '売上金額
                strVALUES = strVALUES & N & "  , '" & frmC01F10_Login.loginValue.TantoCD & "' "                 '更新者
                strVALUES = strVALUES & N & "  , current_timestamp "                                            '更新日
                strVALUES = strVALUES & N & ") "
                _db.executeDB(strSQL & strVALUES)
                intCount += 1
            End If
            If Me.cmbNyukinSyubetu4.SelectedValue <> "0" Then
                selectedDataRow = CType(Me.cmbNyukinSyubetu4.SelectedItem, DataRowView)
                strVALUES = ""
                strVALUES = strVALUES & N & "  , " & intCount                                                   '行番
                strVALUES = strVALUES & N & "  , '" & Me.cmbNyukinSyubetu4.SelectedValue & "' "                   '入金種別
                strVALUES = strVALUES & N & "  , '" & Me.cmbNyukinSyubetu4.DisplayMember & "' "                 '入金種別名
                strVALUES = strVALUES & N & "  , '" & selectedDataRow("文字２").ToString & "' "                 '入金分類
                strVALUES = strVALUES & N & "  , " & Decimal.Parse(_db.rmNullInt(Me.txtNyukin4.Text)).ToString  '売上金額
                strVALUES = strVALUES & N & "  , '" & frmC01F10_Login.loginValue.TantoCD & "' "                 '更新者
                strVALUES = strVALUES & N & "  , current_timestamp "                                            '更新日
                strVALUES = strVALUES & N & ") "
                _db.executeDB(strSQL & strVALUES)
                intCount += 1
            End If
            If Me.cmbNyukinSyubetu5.SelectedValue <> "0" Then
                selectedDataRow = CType(Me.cmbNyukinSyubetu5.SelectedItem, DataRowView)
                strVALUES = ""
                strVALUES = strVALUES & N & "  , " & intCount                                                   '行番
                strVALUES = strVALUES & N & "  , '" & Me.cmbNyukinSyubetu5.SelectedValue & "' "                   '入金種別
                strVALUES = strVALUES & N & "  , '" & Me.cmbNyukinSyubetu5.DisplayMember & "' "                 '入金種別名
                strVALUES = strVALUES & N & "  , '" & selectedDataRow("文字２").ToString & "' "                 '入金分類
                strVALUES = strVALUES & N & "  , " & Decimal.Parse(_db.rmNullInt(Me.txtNyukin5.Text)).ToString  '売上金額
                strVALUES = strVALUES & N & "  , '" & frmC01F10_Login.loginValue.TantoCD & "' "                 '更新者
                strVALUES = strVALUES & N & "  , current_timestamp "                                            '更新日
                strVALUES = strVALUES & N & ") "
                _db.executeDB(strSQL & strVALUES)
                intCount += 1
            End If

            'ログ出力
            '操作履歴ログ作成
            _comLogc.Insert_L01_ProcLog(frmC01F10_Login.loginValue.BumonCD, DBNull.Value, _SelectID, CommonConst.PROGRAM_UPDATE, "9",
                                                        lblDenpyoNo.Text, CommonConst.MODE_ADDNEW_NAME, DBNull.Value, DBNull.Value, DBNull.Value,
                                                        DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _userId)

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub
    'データ変更
    Private Sub DataEdit()

        Try

            Dim strSQL As String = ""
            strSQL = ""
            strSQL = strSQL & N & "Delete From T25_NKINHD  "
            strSQL = strSQL & N & "  Where 会社コード =  '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "
            strSQL = strSQL & N & "   and 入金伝番 = '" & _DenpyoNo & "' "
            _db.executeDB(strSQL)


            '入金明細(T26)
            strSQL = ""
            strSQL = strSQL & N & "Delete From T26_NKINDT  "
            strSQL = strSQL & N & "  Where 会社コード =  '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "       '会社コード
            strSQL = strSQL & N & "   and 入金伝番 = '" & Me.lblDenpyoNo.Text & "' "                 '注文伝番
            _db.executeDB(strSQL)
            'レコード追加 入金基本（T25_NKINHD）
            Dim decNyukin1 As Decimal = 0       '入金種別の入金分類１の合計
            If cmbNyukinSyubetu1.ValueMember = "1" Then
                decNyukin1 += Decimal.Parse(_db.rmNullInt(Me.txtNyukin1.Text))
            End If
            If cmbNyukinSyubetu2.ValueMember = "1" Then
                decNyukin1 += Decimal.Parse(_db.rmNullInt(Me.txtNyukin2.Text))
            End If
            If cmbNyukinSyubetu3.ValueMember = "1" Then
                decNyukin1 += Decimal.Parse(_db.rmNullInt(Me.txtNyukin3.Text))
            End If
            If cmbNyukinSyubetu4.ValueMember = "1" Then
                decNyukin1 += Decimal.Parse(_db.rmNullInt(Me.txtNyukin4.Text))
            End If
            If cmbNyukinSyubetu5.ValueMember = "1" Then
                decNyukin1 += Decimal.Parse(_db.rmNullInt(Me.txtNyukin5.Text))
            End If
            Dim decNyukin2 As Decimal = 0       '入金種別の入金分類２の合計
            If cmbNyukinSyubetu1.ValueMember = "2" Then
                decNyukin2 += Decimal.Parse(_db.rmNullInt(Me.txtNyukin1.Text))
            End If
            If cmbNyukinSyubetu2.ValueMember = "2" Then
                decNyukin2 += Decimal.Parse(_db.rmNullInt(Me.txtNyukin2.Text))
            End If
            If cmbNyukinSyubetu3.ValueMember = "2" Then
                decNyukin2 += Decimal.Parse(_db.rmNullInt(Me.txtNyukin3.Text))
            End If
            If cmbNyukinSyubetu4.ValueMember = "2" Then
                decNyukin2 += Decimal.Parse(_db.rmNullInt(Me.txtNyukin4.Text))
            End If
            If cmbNyukinSyubetu5.ValueMember = "2" Then
                decNyukin2 += Decimal.Parse(_db.rmNullInt(Me.txtNyukin5.Text))
            End If

            strSQL = ""
            strSQL = strSQL & N & "INSERT INTO T25_NKINHD ( "
            strSQL = strSQL & N & "    会社コード "
            strSQL = strSQL & N & "  , 入金伝番 "
            strSQL = strSQL & N & "  , 入金日 "
            strSQL = strSQL & N & "  , 請求先コード "
            strSQL = strSQL & N & "  , 請求先名 "
            strSQL = strSQL & N & "  , 売上明細数 "
            strSQL = strSQL & N & "  , 売上金額計 "
            strSQL = strSQL & N & "  , 売上消費税計 "
            strSQL = strSQL & N & "  , 現金振込計 "
            strSQL = strSQL & N & "  , 手数料計 "
            strSQL = strSQL & N & "  , 入金額計 "
            strSQL = strSQL & N & "  , 備考１ "
            strSQL = strSQL & N & "  , 備考２ "
            strSQL = strSQL & N & "  , 取消区分 "
            strSQL = strSQL & N & "  , 更新者 "
            strSQL = strSQL & N & "  , 更新日 "
            strSQL = strSQL & N & ") VALUES ( "
            strSQL = strSQL & N & "    '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "     '会社コード
            strSQL = strSQL & N & "  , '" & Me.lblDenpyoNo.Text & "' "                              '入金伝番
            strSQL = strSQL & N & "  , '" & Me.dtpNyukinDt.Text & "' "                              '入金日
            strSQL = strSQL & N & "  , '" & Me.txtSeikyuCD.Text & "' "                              '請求先コード
            strSQL = strSQL & N & "  , '" & Me.lblSeikyusakiNM.Text & "' "                          '請求先名
            strSQL = strSQL & N & "  , " & Decimal.Parse(Me.lblSelectCount.Text).ToString           '売上明細数
            strSQL = strSQL & N & "  , " & Decimal.Parse(Me.lblSumMoney.Text).ToString              '売上金額計
            strSQL = strSQL & N & "  , " & Decimal.Parse(Me.lblSumTax.Text).ToString                '売上消費税計
            strSQL = strSQL & N & "  , " & decNyukin1                                               '現金振込計
            strSQL = strSQL & N & "  , " & decNyukin2                                               '手数料計
            strSQL = strSQL & N & "  , " & Decimal.Parse(Me.lblNyukinSum.Text).ToString             '入金額計
            strSQL = strSQL & N & "  , '" & Me.txtBiko1.Text & "' "                                 '備考１
            strSQL = strSQL & N & "  , '" & Me.txtBiko2.Text & "' "                                 '備考２
            strSQL = strSQL & N & "  , '0' "              '取消区分
            strSQL = strSQL & N & "  , '" & frmC01F10_Login.loginValue.TantoCD & "' "         '更新者
            strSQL = strSQL & N & "  , current_timestamp "                                    '更新日
            strSQL = strSQL & N & ") "
            _db.executeDB(strSQL)

            Dim strVALUES As String
            '入金明細（T26_NKINDT）
            strSQL = ""
            strSQL = strSQL & N & "INSERT INTO T26_NKINDT ( "
            strSQL = strSQL & N & "    会社コード "
            strSQL = strSQL & N & "  , 入金伝番 "
            strSQL = strSQL & N & "  , 行番 "
            strSQL = strSQL & N & "  , 入金種別 "
            strSQL = strSQL & N & "  , 入金種別名 "
            strSQL = strSQL & N & "  , 入金分類 "
            strSQL = strSQL & N & "  , 金額 "

            strSQL = strSQL & N & "  , 更新者 "
            strSQL = strSQL & N & "  , 更新日 "
            strSQL = strSQL & N & ") VALUES ( "
            strSQL = strSQL & N & "    '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "       '会社コード
            strSQL = strSQL & N & "  , '" & Me.lblDenpyoNo.Text & "' "                                '入金伝番
            Dim intCount As Integer = 1
            Dim selectedDataRow As DataRowView
            If Me.cmbNyukinSyubetu1.SelectedValue <> "0" Then
                selectedDataRow = CType(Me.cmbNyukinSyubetu1.SelectedItem, DataRowView)
                strVALUES = ""
                strVALUES = strVALUES & N & "  , " & intCount                                                   '行番
                strVALUES = strVALUES & N & "  , " & Me.cmbNyukinSyubetu1.SelectedValue                           '入金種別
                strVALUES = strVALUES & N & "  , '" & Me.cmbNyukinSyubetu1.DisplayMember & "' "                 '入金種別名
                strVALUES = strVALUES & N & "  , '" & selectedDataRow("文字２").ToString & "' "                 '入金分類
                strVALUES = strVALUES & N & "  , " & Decimal.Parse(_db.rmNullInt(Me.txtNyukin1.Text)).ToString        '売上金額
                strVALUES = strVALUES & N & "  , '" & frmC01F10_Login.loginValue.TantoCD & "' "                       '更新者
                strVALUES = strVALUES & N & "  , current_timestamp "                                                  '更新日
                strVALUES = strVALUES & N & ") "
                _db.executeDB(strSQL & strVALUES)
                intCount += 1
            End If
            If Me.cmbNyukinSyubetu2.SelectedValue <> "0" Then
                selectedDataRow = CType(Me.cmbNyukinSyubetu2.SelectedItem, DataRowView)
                strVALUES = ""
                strVALUES = strVALUES & N & "  , " & intCount                                                   '行番
                strVALUES = strVALUES & N & "  , '" & Me.cmbNyukinSyubetu2.SelectedValue & "' "                   '入金種別
                strVALUES = strVALUES & N & "  , '" & Me.cmbNyukinSyubetu2.DisplayMember & "' "                 '入金種別名
                strVALUES = strVALUES & N & "  , '" & selectedDataRow("文字２").ToString & "' "                 '入金分類
                strVALUES = strVALUES & N & "  , " & Decimal.Parse(_db.rmNullInt(Me.txtNyukin2.Text)).ToString  '売上金額
                strVALUES = strVALUES & N & "  , '" & frmC01F10_Login.loginValue.TantoCD & "' "                 '更新者
                strVALUES = strVALUES & N & "  , current_timestamp "                                            '更新日
                strVALUES = strVALUES & N & ") "
                _db.executeDB(strSQL & strVALUES)
                intCount += 1
            End If
            If Me.cmbNyukinSyubetu3.SelectedValue <> "0" Then
                selectedDataRow = CType(Me.cmbNyukinSyubetu2.SelectedItem, DataRowView)
                strVALUES = ""
                strVALUES = strVALUES & N & "  , " & intCount                                                   '行番
                strVALUES = strVALUES & N & "  , '" & Me.cmbNyukinSyubetu3.SelectedValue & "' "                   '入金種別
                strVALUES = strVALUES & N & "  , '" & Me.cmbNyukinSyubetu3.DisplayMember & "' "                 '入金種別名
                strVALUES = strVALUES & N & "  , '" & selectedDataRow("文字２").ToString & "' "                 '入金分類
                strVALUES = strVALUES & N & "  , " & Decimal.Parse(_db.rmNullInt(Me.txtNyukin3.Text)).ToString  '売上金額
                strVALUES = strVALUES & N & "  , '" & frmC01F10_Login.loginValue.TantoCD & "' "                 '更新者
                strVALUES = strVALUES & N & "  , current_timestamp "                                            '更新日
                strVALUES = strVALUES & N & ") "
                _db.executeDB(strSQL & strVALUES)
                intCount += 1
            End If
            If Me.cmbNyukinSyubetu4.SelectedValue <> "0" Then
                selectedDataRow = CType(Me.cmbNyukinSyubetu4.SelectedItem, DataRowView)
                strVALUES = ""
                strVALUES = strVALUES & N & "  , " & intCount                                                   '行番
                strVALUES = strVALUES & N & "  , '" & Me.cmbNyukinSyubetu4.SelectedValue & "' "                   '入金種別
                strVALUES = strVALUES & N & "  , '" & Me.cmbNyukinSyubetu4.DisplayMember & "' "                 '入金種別名
                strVALUES = strVALUES & N & "  , '" & selectedDataRow("文字２").ToString & "' "                 '入金分類
                strVALUES = strVALUES & N & "  , " & Decimal.Parse(_db.rmNullInt(Me.txtNyukin4.Text)).ToString  '売上金額
                strVALUES = strVALUES & N & "  , '" & frmC01F10_Login.loginValue.TantoCD & "' "                 '更新者
                strVALUES = strVALUES & N & "  , current_timestamp "                                            '更新日
                strVALUES = strVALUES & N & ") "
                _db.executeDB(strSQL & strVALUES)
                intCount += 1
            End If
            If Me.cmbNyukinSyubetu5.SelectedValue <> "0" Then
                selectedDataRow = CType(Me.cmbNyukinSyubetu5.SelectedItem, DataRowView)
                strVALUES = ""
                strVALUES = strVALUES & N & "  , " & intCount                                                   '行番
                strVALUES = strVALUES & N & "  , '" & Me.cmbNyukinSyubetu5.SelectedValue & "' "                   '入金種別
                strVALUES = strVALUES & N & "  , '" & Me.cmbNyukinSyubetu5.DisplayMember & "' "                 '入金種別名
                strVALUES = strVALUES & N & "  , '" & selectedDataRow("文字２").ToString & "' "                 '入金分類
                strVALUES = strVALUES & N & "  , " & Decimal.Parse(_db.rmNullInt(Me.txtNyukin5.Text)).ToString  '売上金額
                strVALUES = strVALUES & N & "  , '" & frmC01F10_Login.loginValue.TantoCD & "' "                 '更新者
                strVALUES = strVALUES & N & "  , current_timestamp "                                            '更新日
                strVALUES = strVALUES & N & ") "
                _db.executeDB(strSQL & strVALUES)
                intCount += 1
            End If

            'ログ出力
            '操作履歴ログ作成
            _comLogc.Insert_L01_ProcLog(frmC01F10_Login.loginValue.BumonCD, DBNull.Value, _SelectID, CommonConst.PROGRAM_UPDATE, "9",
                                                        lblDenpyoNo.Text, CommonConst.MODE_EditStatus_NAME, DBNull.Value, DBNull.Value, DBNull.Value,
                                                        DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _userId)

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '入金額の計算
    Private Sub txtNyukin1_Validated(sender As Object, e As EventArgs) Handles txtNyukin1.Validated, txtNyukin2.Validated, txtNyukin3.Validated, txtNyukin4.Validated, txtNyukin5.Validated
        SumNyukinMoney()
    End Sub

    '入金額を合計する
    Private Sub SumNyukinMoney()
        Dim decNyukin As Decimal = 0
        decNyukin += Decimal.Parse(_db.rmNullInt(Me.txtNyukin1.Text))
        decNyukin += Decimal.Parse(_db.rmNullInt(Me.txtNyukin2.Text))
        decNyukin += Decimal.Parse(_db.rmNullInt(Me.txtNyukin3.Text))
        decNyukin += Decimal.Parse(_db.rmNullInt(Me.txtNyukin4.Text))
        decNyukin += Decimal.Parse(_db.rmNullInt(Me.txtNyukin5.Text))

        Me.lblNyukinSum.Text = decNyukin.ToString("N0")

        Me.txtNyukin1.Text = Decimal.Parse(_db.rmNullInt(Me.txtNyukin1.Text)).ToString("#,###")
        Me.txtNyukin2.Text = Decimal.Parse(_db.rmNullInt(Me.txtNyukin2.Text)).ToString("#,###")
        Me.txtNyukin3.Text = Decimal.Parse(_db.rmNullInt(Me.txtNyukin3.Text)).ToString("#,###")
        Me.txtNyukin4.Text = Decimal.Parse(_db.rmNullInt(Me.txtNyukin4.Text)).ToString("#,###")
        Me.txtNyukin5.Text = Decimal.Parse(_db.rmNullInt(Me.txtNyukin5.Text)).ToString("#,###")
    End Sub

    Private Sub txtSeikyuCD_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSeikyuCD.KeyDown
        'スペースキーを押したとき検索画面表示
        If e.KeyCode = Keys.Space Then
            SeikyusakiSelectWindowOpen()
        End If
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

    Private Sub dgvList_Sorted(sender As Object, e As EventArgs) Handles dgvList.Sorted
        '行番号降り直し
        For i As Integer = 0 To dgvList.RowCount - 1
            dgvList.Rows(i).Cells(COLNO_NO).Value = i + 1
        Next
    End Sub

    Private Sub cmdChumonList_Click(sender As Object, e As EventArgs) Handles cmdChumonList.Click
        Dim idx As Integer
        For Each c As DataGridViewRow In dgvList.SelectedRows
            idx = c.Index
            Exit For
        Next c

        Dim strDenpyoNo As String = ""    '伝票番号
        strDenpyoNo = dgvList.Rows(idx).Cells(COLNO_DenpyoNo).Value

        Dim openForm As Form = Nothing
        'openForm = New frmH05F20_Nyukin(_msgHd, _db, _SelectID, Me, _SelectMode, strDenpyoNo)
        openForm = New frmH01F60_Chumon(_msgHd, _db, _SelectID, CommonConst.MODE_InquiryStatus, dgvList.Rows(idx).Cells(COLNO_SyukkaCD).Value, Me, strDenpyoNo)
        openForm.Show()
        Me.Hide()   ' 自分は隠れる

    End Sub
End Class