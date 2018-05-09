Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL
Public Class frmH01F50_SelectRireki
    '-------------------------------------------------------------------------------
    '   変数定義
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _comLogc As CommonLogic                             '共通処理用
    Private _gh As UtilDataGridViewHandler                      'DataGridViewユーティリティクラス
    Private _SelectMode As Integer   'メニューのどこから呼ばれたか。（0:登録、1:変更、2:取消、3:照会)
    Private _parentForm As Form                                 '親フォーム
    Private _SelectID As String
    Private _userId As String                                   'ログインユーザＩＤ
    Private _init As Boolean                                    '初期処理済フラグ


    '-------------------------------------------------------------------------------
    '   定数定義
    '-------------------------------------------------------------------------------
    Private Const N As String = ControlChars.NewLine            '改行文字
    Private Const RS As String = "RecSet"                       'レコードセットテーブル
    'グリッド列№
    'dgvIchiran
    Private Const COLNO_SYUKKAYMD = 0                           '01:出荷日
    Private Const COLNO_DENPYONO = 1                            '02:伝票番号
    Private Const COLNO_SYUKKACD = 2                            '03:出荷先コード
    Private Const COLNO_SYUKKANM = 3                            '04:出荷先名
    Private Const COLNO_ADDRESS = 4                             '05:住所
    Private Const COLNO_TEL = 5                                 '06:電話番号
    Private Const COLNO_UNSOBIN = 6                             '07:運送便
    Private Const COLNO_SHOHINNM = 7                            '08:商品名
    '編集モードの名称
    Private Const MODEP_ADDNEW = "複写元選択モード"                          '複写新規
    Private Const MODEP_EditStatus = "変更選択モード"                        '変更
    Private Const MODEP_CancelStatus = "取消選択モード"                      '取消
    Private Const MODEP_InquiryStatus = "参照選択モード"                     '参照

    Private Const TORIKESHI = "取消"                            '取消データの表示

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

        Try
            _init = False

            '初期処理
            _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
            _db = prmRefDbHd                                                    'DBハンドラの設定
            _comLogc = New CommonLogic(_db, _msgHd)                             '共通処理用
            _gh = New UtilDataGridViewHandler(dgvList)                          'DataGridViewユーティリティクラス
            _parentForm = prmParentForm
            _SelectMode = prmSelectMode                                         '処理状態
            _SelectID = prmSelectID
            _userId = frmC01F10_Login.loginValue.TantoCD                        'ユーザＩＤ

            StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
            Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                'フォームタイトル表示

            '操作履歴ログ作成（初期処理）
            _comLogc.Insert_L01_ProcLog(frmC01F10_Login.loginValue.BumonCD, DBNull.Value, _SelectID, CommonConst.PROGRAM_START, CommonConst.STATUS_NORMAL,
                                            DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value,
                                            DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _userId)

            '出荷日表示
            dtpSyukkaDateFrom.Text = DateAdd("m", -1, Now).ToString("yyyy/MM/dd")   '出荷日自 システム日付-1ヶ月
            dtpSyukkaDateTo.Text = DateAdd("m", 0, Now).ToString("yyyy/MM/dd")      '出荷日至 システム日付

            'モード表示
            Select Case prmSelectMode
                Case CommonConst.MODE_ADDNEW   '複写新規
                    Me.lblMode.Text = MODEP_ADDNEW
                    Me.chkTorikesi.Checked = True
                Case CommonConst.MODE_EditStatus   '変更
                    Me.lblMode.Text = MODEP_EditStatus
                    Me.chkTorikesi.Checked = False
                Case CommonConst.MODE_CancelStatus   '取消
                    Me.lblMode.Text = MODEP_CancelStatus
                    Me.chkTorikesi.Checked = False
                Case CommonConst.MODE_InquiryStatus   '参照
                    Me.lblMode.Text = MODEP_InquiryStatus
                    Me.chkTorikesi.Checked = False
                Case Else
                    Me.lblMode.Text = ""
            End Select

            _init = True

            '一覧初期表示
            DispList()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '注文データをもとにグリッドへ一覧表示
    Private Sub DispList()

        '初期処理前の場合は処理終了
        If Not _init Then
            '処理終了
            Exit Sub
        End If

        Dim strSQL As String = ""

        'Gridの表示（暫定処理）
        dgvList.Columns(COLNO_SYUKKAYMD).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvList.Columns(COLNO_DENPYONO).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvList.Columns(COLNO_SYUKKACD).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        'メニューを読み込み
        strSQL = "SELECT DISTINCT "
        strSQL = strSQL & N & "   T11.注文伝番 "
        strSQL = strSQL & N & " FROM T11_CYMNDT T11 "
        strSQL = strSQL & N & " 	inner join T10_CYMNHD T10 on T10.会社コード = T11.会社コード and T10.注文伝番 = T11.注文伝番 "
        strSQL = strSQL & N & "     left join M90_HANYO h on h.会社コード = T10.会社コード and h.固定キー = '" & CommonConst.HANYO_KOTEI_UNSOUBIN & "' and h.可変キー = T10.運送便コード "
        strSQL = strSQL & N & " Where T11.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        '出荷先名
        If Me.txtSyukkasakiName.Text <> "" Then
            strSQL = strSQL & N & " and T10.出荷先名 like '%" & Me.txtSyukkasakiName.Text & "%' "
        End If
        '住所
        If Me.txtAddress.Text <> "" Then
            strSQL = strSQL & N & " and (COALESCE(T10.住所１,'') || COALESCE(T10.住所２,'') || COALESCE(T10.住所３,'')) like '%" & Me.txtAddress.Text & "%' "
        End If
        '電話番号
        If Me.txtTel.Text <> "" Then
            strSQL = strSQL & N & " and T10.電話番号検索用 like '%" & Me.txtTel.Text & "%' "
        End If
        '出荷先コード
        If Me.txtSyukkaCd.Text <> "" Then
            strSQL = strSQL & N & " and T10.出荷先コード like '" & Me.txtSyukkaCd.Text & "%' "
        End If
        '出荷日From
        If Me.dtpSyukkaDateFrom.Value.ToString <> "" Then
            strSQL = strSQL & N & " and T10.出荷日 >= '" & Me.dtpSyukkaDateFrom.Value.ToString("yyyy/MM/dd") & "' "
        End If
        '出荷日To
        If Me.dtpSyukkaDateTo.Value.ToString <> "" Then
            strSQL = strSQL & N & " and T10.出荷日 <= '" & Me.dtpSyukkaDateTo.Value.ToString("yyyy/MM/dd") & "' "
        End If
        '伝票番号From
        If Me.txtDenpyoNoFrom.Text <> "" Then
            strSQL = strSQL & N & " and T10.注文伝番 >= '" & Me.txtDenpyoNoFrom.Text & "' "
        End If
        '伝票番号To
        If Me.txtDenpyoNoTo.Text <> "" Then
            strSQL = strSQL & N & " and T10.注文伝番 <= '" & Me.txtDenpyoNoTo.Text & "' "
        End If
        '運送便
        If Me.txtUnsoubin.Text <> "" Then
            strSQL = strSQL & N & " and h.文字１ = '" & Me.txtUnsoubin.Text & "' "
        End If
        '商品名
        If Me.txtSyohinName.Text <> "" Then
            strSQL = strSQL & N & " and T11.商品名 like '%" & Me.txtSyohinName.Text & "%' "
        End If
        '取消区分
        If Not Me.chkTorikesi.Checked Then
            strSQL = strSQL & N & " and T10.取消区分 = '0' "
        End If

        Dim strSQLALL As String = ""
        strSQLALL = "SELECT "
        strSQLALL = strSQLALL & N & "   T1.会社コード,T1.注文伝番,T1.行番,T0.出荷日,T0.出荷先コード,T0.出荷先名 ,h2.文字１ as 運送便名  ,T0.電話番号 ,T1.商品名 ,T0.取消区分"
        strSQLALL = strSQLALL & N & " ,COALESCE(T0.住所１,'') || COALESCE(T0.住所２,'') || COALESCE(T0.住所３,'') AS 住所 "  '住所
        strSQLALL = strSQLALL & N & " FROM T11_CYMNDT T1 "
        strSQLALL = strSQLALL & N & " 	inner join T10_CYMNHD T0 on T0.会社コード = T1.会社コード and T0.注文伝番 = T1.注文伝番 "
        strSQLALL = strSQLALL & N & "     left join M90_HANYO h2 on h2.会社コード = T0.会社コード and h2.固定キー = '" & CommonConst.HANYO_KOTEI_UNSOUBIN & "' and h2.可変キー = T0.運送便コード "
        strSQLALL = strSQLALL & N & " Where T1.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        strSQLALL = strSQLALL & N & " 	and T1.注文伝番 IN (" & strSQL & ") "
        If rdbDenpyo.Checked Then
            strSQLALL = strSQLALL & N & " 	and T1.行番 =1 "
        End If
        strSQLALL = strSQLALL & N & " order by T0.出荷日 desc,T1.注文伝番,T1.行番 "

        Dim reccnt As Integer = 0
        Dim ds As DataSet = _db.selectDB(strSQLALL, RS, reccnt)

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

        '描画の前にすべてクリアする
        dgvList.Rows.Clear()
        For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
            dgvList.Rows.Add()
            dgvList.Rows(index).Cells(COLNO_SYUKKAYMD).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("出荷日"))       '出荷日
            If _db.rmNullStr(ds.Tables(RS).Rows(index)("取消区分")) = "1" Then
                dgvList.Rows(index).Cells(COLNO_DENPYONO).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("注文伝番")) & TORIKESHI     '伝票番号
            Else
                dgvList.Rows(index).Cells(COLNO_DENPYONO).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("注文伝番"))      '伝票番号
            End If
            dgvList.Rows(index).Cells(COLNO_SYUKKACD).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("出荷先コード"))  '出荷先コード
            dgvList.Rows(index).Cells(COLNO_SYUKKANM).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("出荷先名"))      '出荷先名
            dgvList.Rows(index).Cells(COLNO_ADDRESS).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("住所"))           '住所
            dgvList.Rows(index).Cells(COLNO_TEL).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("電話番号"))           '電話番号
            dgvList.Rows(index).Cells(COLNO_UNSOBIN).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("運送便名"))       '運送便
            dgvList.Rows(index).Cells(COLNO_SHOHINNM).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("商品名"))        '商品名
        Next

        '件数表示
        Me.lblListCount.Text = dgvList.RowCount

        '対象データの有無チェック
        If ds.Tables(RS).Rows.Count = 0 Then
            '選択ボタンを非活性
            Me.btnSelect.Enabled = False
            Throw New UsrDefException("対象データがありません。", _msgHd.getMSG("NonData"))
        Else
            '選択ボタンを活性
            Me.btnSelect.Enabled = True
        End If

    End Sub


    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel31 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel29 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel30 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtSyukkaCd = New System.Windows.Forms.TextBox()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.txtTel = New System.Windows.Forms.TextBox()
        Me.txtSyukkasakiName = New System.Windows.Forms.TextBox()
        Me.cmdBack = New System.Windows.Forms.Button()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.TableLayoutPanel33 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel27 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel32 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel34 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel11 = New System.Windows.Forms.TableLayoutPanel()
        Me.rdbMeisai = New System.Windows.Forms.RadioButton()
        Me.rdbDenpyo = New System.Windows.Forms.RadioButton()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.lblListCount = New System.Windows.Forms.Label()
        Me.dgvList = New System.Windows.Forms.DataGridView()
        Me.出荷日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.伝票番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.出荷先CD = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.出荷先名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.住所 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.電話番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.運送便 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.商品名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMode = New System.Windows.Forms.Label()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel12 = New System.Windows.Forms.TableLayoutPanel()
        Me.chkTorikesi = New System.Windows.Forms.CheckBox()
        Me.TableLayoutPanel9 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtSyohinName = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtUnsoubin = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtDenpyoNoTo = New System.Windows.Forms.TextBox()
        Me.txtDenpyoNoFrom = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.dtpSyukkaDateFrom = New System.Windows.Forms.DateTimePicker()
        Me.dtpSyukkaDateTo = New System.Windows.Forms.DateTimePicker()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.TableLayoutPanel31.SuspendLayout()
        Me.TableLayoutPanel29.SuspendLayout()
        Me.TableLayoutPanel30.SuspendLayout()
        Me.TableLayoutPanel33.SuspendLayout()
        Me.TableLayoutPanel27.SuspendLayout()
        Me.TableLayoutPanel32.SuspendLayout()
        Me.TableLayoutPanel34.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TableLayoutPanel11.SuspendLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel12.SuspendLayout()
        Me.TableLayoutPanel9.SuspendLayout()
        Me.TableLayoutPanel8.SuspendLayout()
        Me.TableLayoutPanel7.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label48
        '
        Me.Label48.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label48.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label48.Location = New System.Drawing.Point(3, 66)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(124, 22)
        Me.Label48.TabIndex = 7
        Me.Label48.Text = "（前方一致検索）"
        Me.Label48.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label47
        '
        Me.Label47.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label47.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label47.Location = New System.Drawing.Point(3, 0)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(124, 22)
        Me.Label47.TabIndex = 4
        Me.Label47.Text = "（一部一致検索）"
        Me.Label47.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label44
        '
        Me.Label44.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label44.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label44.Location = New System.Drawing.Point(3, 44)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(124, 22)
        Me.Label44.TabIndex = 6
        Me.Label44.Text = "（一部一致検索）"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TableLayoutPanel31
        '
        Me.TableLayoutPanel31.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel31.ColumnCount = 1
        Me.TableLayoutPanel31.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.11765!))
        Me.TableLayoutPanel31.Controls.Add(Me.Label48, 0, 3)
        Me.TableLayoutPanel31.Controls.Add(Me.Label47, 0, 0)
        Me.TableLayoutPanel31.Controls.Add(Me.Label43, 0, 1)
        Me.TableLayoutPanel31.Controls.Add(Me.Label44, 0, 2)
        Me.TableLayoutPanel31.Location = New System.Drawing.Point(442, 0)
        Me.TableLayoutPanel31.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel31.Name = "TableLayoutPanel31"
        Me.TableLayoutPanel31.RowCount = 4
        Me.TableLayoutPanel31.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel31.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel31.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel31.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel31.Size = New System.Drawing.Size(130, 89)
        Me.TableLayoutPanel31.TabIndex = 1
        '
        'Label43
        '
        Me.Label43.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label43.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label43.Location = New System.Drawing.Point(3, 22)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(124, 22)
        Me.Label43.TabIndex = 5
        Me.Label43.Text = "（一部一致検索）"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label38
        '
        Me.Label38.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label38.AutoSize = True
        Me.Label38.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label38.Location = New System.Drawing.Point(3, 4)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(82, 15)
        Me.Label38.TabIndex = 0
        Me.Label38.Text = "■抽出条件"
        '
        'TableLayoutPanel29
        '
        Me.TableLayoutPanel29.ColumnCount = 2
        Me.TableLayoutPanel29.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.44756!))
        Me.TableLayoutPanel29.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.55245!))
        Me.TableLayoutPanel29.Controls.Add(Me.TableLayoutPanel31, 1, 0)
        Me.TableLayoutPanel29.Controls.Add(Me.TableLayoutPanel30, 0, 0)
        Me.TableLayoutPanel29.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel29.Location = New System.Drawing.Point(0, 19)
        Me.TableLayoutPanel29.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel29.Name = "TableLayoutPanel29"
        Me.TableLayoutPanel29.RowCount = 1
        Me.TableLayoutPanel29.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel29.Size = New System.Drawing.Size(572, 114)
        Me.TableLayoutPanel29.TabIndex = 1
        '
        'TableLayoutPanel30
        '
        Me.TableLayoutPanel30.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel30.ColumnCount = 2
        Me.TableLayoutPanel30.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.39468!))
        Me.TableLayoutPanel30.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.60532!))
        Me.TableLayoutPanel30.Controls.Add(Me.txtSyukkaCd, 1, 3)
        Me.TableLayoutPanel30.Controls.Add(Me.Label39, 0, 0)
        Me.TableLayoutPanel30.Controls.Add(Me.Label40, 0, 1)
        Me.TableLayoutPanel30.Controls.Add(Me.Label41, 0, 2)
        Me.TableLayoutPanel30.Controls.Add(Me.Label42, 0, 3)
        Me.TableLayoutPanel30.Controls.Add(Me.txtAddress, 1, 1)
        Me.TableLayoutPanel30.Controls.Add(Me.txtTel, 1, 2)
        Me.TableLayoutPanel30.Controls.Add(Me.txtSyukkasakiName, 1, 0)
        Me.TableLayoutPanel30.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel30.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel30.Name = "TableLayoutPanel30"
        Me.TableLayoutPanel30.RowCount = 4
        Me.TableLayoutPanel30.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel30.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel30.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel30.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel30.Size = New System.Drawing.Size(442, 89)
        Me.TableLayoutPanel30.TabIndex = 0
        '
        'txtSyukkaCd
        '
        Me.txtSyukkaCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSyukkaCd.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtSyukkaCd.Location = New System.Drawing.Point(98, 66)
        Me.txtSyukkaCd.Margin = New System.Windows.Forms.Padding(0)
        Me.txtSyukkaCd.Name = "txtSyukkaCd"
        Me.txtSyukkaCd.Size = New System.Drawing.Size(120, 22)
        Me.txtSyukkaCd.TabIndex = 4
        '
        'Label39
        '
        Me.Label39.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label39.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label39.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label39.Location = New System.Drawing.Point(0, 0)
        Me.Label39.Margin = New System.Windows.Forms.Padding(0)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(98, 22)
        Me.Label39.TabIndex = 0
        Me.Label39.Text = "出荷先名"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label40
        '
        Me.Label40.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label40.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label40.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label40.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label40.Location = New System.Drawing.Point(0, 22)
        Me.Label40.Margin = New System.Windows.Forms.Padding(0)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(98, 22)
        Me.Label40.TabIndex = 1
        Me.Label40.Text = "住所"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label41
        '
        Me.Label41.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label41.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label41.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label41.Location = New System.Drawing.Point(0, 44)
        Me.Label41.Margin = New System.Windows.Forms.Padding(0)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(98, 22)
        Me.Label41.TabIndex = 2
        Me.Label41.Text = "電話番号"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label42
        '
        Me.Label42.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label42.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label42.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label42.Location = New System.Drawing.Point(0, 66)
        Me.Label42.Margin = New System.Windows.Forms.Padding(0)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(98, 22)
        Me.Label42.TabIndex = 3
        Me.Label42.Text = "出荷先CD"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtAddress
        '
        Me.txtAddress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAddress.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtAddress.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.txtAddress.Location = New System.Drawing.Point(98, 22)
        Me.txtAddress.Margin = New System.Windows.Forms.Padding(0)
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(344, 22)
        Me.txtAddress.TabIndex = 2
        '
        'txtTel
        '
        Me.txtTel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtTel.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTel.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtTel.Location = New System.Drawing.Point(98, 44)
        Me.txtTel.Margin = New System.Windows.Forms.Padding(0)
        Me.txtTel.Name = "txtTel"
        Me.txtTel.Size = New System.Drawing.Size(120, 22)
        Me.txtTel.TabIndex = 3
        '
        'txtSyukkasakiName
        '
        Me.txtSyukkasakiName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSyukkasakiName.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSyukkasakiName.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.txtSyukkasakiName.Location = New System.Drawing.Point(98, 0)
        Me.txtSyukkasakiName.Margin = New System.Windows.Forms.Padding(0)
        Me.txtSyukkasakiName.Name = "txtSyukkasakiName"
        Me.txtSyukkasakiName.Size = New System.Drawing.Size(344, 22)
        Me.txtSyukkasakiName.TabIndex = 1
        '
        'cmdBack
        '
        Me.cmdBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdBack.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdBack.Location = New System.Drawing.Point(217, 20)
        Me.cmdBack.Name = "cmdBack"
        Me.cmdBack.Size = New System.Drawing.Size(102, 36)
        Me.cmdBack.TabIndex = 5
        Me.cmdBack.Text = "戻る(&B)"
        Me.cmdBack.UseVisualStyleBackColor = True
        '
        'btnSelect
        '
        Me.btnSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSelect.Enabled = False
        Me.btnSelect.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSelect.Location = New System.Drawing.Point(82, 20)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(102, 36)
        Me.btnSelect.TabIndex = 4
        Me.btnSelect.Text = "選択(&G)"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel33
        '
        Me.TableLayoutPanel33.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel33.ColumnCount = 2
        Me.TableLayoutPanel33.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.07454!))
        Me.TableLayoutPanel33.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.92546!))
        Me.TableLayoutPanel33.Controls.Add(Me.cmdBack, 0, 1)
        Me.TableLayoutPanel33.Controls.Add(Me.btnSelect, 0, 1)
        Me.TableLayoutPanel33.Location = New System.Drawing.Point(860, 516)
        Me.TableLayoutPanel33.Name = "TableLayoutPanel33"
        Me.TableLayoutPanel33.RowCount = 2
        Me.TableLayoutPanel33.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.25424!))
        Me.TableLayoutPanel33.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 84.74577!))
        Me.TableLayoutPanel33.Size = New System.Drawing.Size(322, 59)
        Me.TableLayoutPanel33.TabIndex = 5
        '
        'TableLayoutPanel27
        '
        Me.TableLayoutPanel27.ColumnCount = 1
        Me.TableLayoutPanel27.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel27.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel27.Controls.Add(Me.TableLayoutPanel33, 0, 2)
        Me.TableLayoutPanel27.Controls.Add(Me.TableLayoutPanel32, 0, 1)
        Me.TableLayoutPanel27.Controls.Add(Me.TableLayoutPanel1, 0, 0)
        Me.TableLayoutPanel27.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel27.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel27.Name = "TableLayoutPanel27"
        Me.TableLayoutPanel27.RowCount = 3
        Me.TableLayoutPanel27.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.04844!))
        Me.TableLayoutPanel27.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 64.70588!))
        Me.TableLayoutPanel27.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.07266!))
        Me.TableLayoutPanel27.Size = New System.Drawing.Size(1185, 578)
        Me.TableLayoutPanel27.TabIndex = 2
        '
        'TableLayoutPanel32
        '
        Me.TableLayoutPanel32.ColumnCount = 1
        Me.TableLayoutPanel32.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel32.Controls.Add(Me.TableLayoutPanel34, 0, 0)
        Me.TableLayoutPanel32.Controls.Add(Me.dgvList, 0, 1)
        Me.TableLayoutPanel32.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel32.Location = New System.Drawing.Point(3, 142)
        Me.TableLayoutPanel32.Name = "TableLayoutPanel32"
        Me.TableLayoutPanel32.RowCount = 2
        Me.TableLayoutPanel32.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.81081!))
        Me.TableLayoutPanel32.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.18919!))
        Me.TableLayoutPanel32.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel32.Size = New System.Drawing.Size(1179, 368)
        Me.TableLayoutPanel32.TabIndex = 2
        '
        'TableLayoutPanel34
        '
        Me.TableLayoutPanel34.ColumnCount = 3
        Me.TableLayoutPanel34.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 79.72858!))
        Me.TableLayoutPanel34.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.32061!))
        Me.TableLayoutPanel34.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.035623!))
        Me.TableLayoutPanel34.Controls.Add(Me.TableLayoutPanel6, 0, 0)
        Me.TableLayoutPanel34.Controls.Add(Me.Label46, 2, 0)
        Me.TableLayoutPanel34.Controls.Add(Me.lblListCount, 1, 0)
        Me.TableLayoutPanel34.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel34.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel34.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel34.Name = "TableLayoutPanel34"
        Me.TableLayoutPanel34.RowCount = 1
        Me.TableLayoutPanel34.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel34.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel34.Size = New System.Drawing.Size(1179, 39)
        Me.TableLayoutPanel34.TabIndex = 5
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 3
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.29885!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.70115!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 498.0!))
        Me.TableLayoutPanel6.Controls.Add(Me.Label3, 2, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.Label2, 0, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.Panel1, 1, 0)
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 2
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75.75758!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.24242!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(822, 33)
        Me.TableLayoutPanel6.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(343, 5)
        Me.Label3.Margin = New System.Windows.Forms.Padding(20, 0, 3, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(329, 15)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "伝票単位の場合、明細項目は先頭行の内容を表示"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Margin = New System.Windows.Forms.Padding(0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 25)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "■表示形式"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.TableLayoutPanel11)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(88, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(235, 25)
        Me.Panel1.TabIndex = 9
        '
        'TableLayoutPanel11
        '
        Me.TableLayoutPanel11.ColumnCount = 2
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel11.Controls.Add(Me.rdbMeisai, 1, 0)
        Me.TableLayoutPanel11.Controls.Add(Me.rdbDenpyo, 0, 0)
        Me.TableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel11.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel11.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel11.Name = "TableLayoutPanel11"
        Me.TableLayoutPanel11.RowCount = 1
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel11.Size = New System.Drawing.Size(233, 23)
        Me.TableLayoutPanel11.TabIndex = 0
        '
        'rdbMeisai
        '
        Me.rdbMeisai.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.rdbMeisai.AutoSize = True
        Me.rdbMeisai.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rdbMeisai.Location = New System.Drawing.Point(132, 2)
        Me.rdbMeisai.Margin = New System.Windows.Forms.Padding(0)
        Me.rdbMeisai.Name = "rdbMeisai"
        Me.rdbMeisai.Size = New System.Drawing.Size(85, 19)
        Me.rdbMeisai.TabIndex = 1
        Me.rdbMeisai.Text = "明細単位"
        Me.rdbMeisai.UseVisualStyleBackColor = True
        '
        'rdbDenpyo
        '
        Me.rdbDenpyo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.rdbDenpyo.AutoSize = True
        Me.rdbDenpyo.Checked = True
        Me.rdbDenpyo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rdbDenpyo.Location = New System.Drawing.Point(15, 2)
        Me.rdbDenpyo.Margin = New System.Windows.Forms.Padding(0)
        Me.rdbDenpyo.Name = "rdbDenpyo"
        Me.rdbDenpyo.Size = New System.Drawing.Size(85, 19)
        Me.rdbDenpyo.TabIndex = 0
        Me.rdbDenpyo.TabStop = True
        Me.rdbDenpyo.Text = "伝票単位"
        Me.rdbDenpyo.UseVisualStyleBackColor = True
        '
        'Label46
        '
        Me.Label46.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label46.AutoSize = True
        Me.Label46.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label46.Location = New System.Drawing.Point(1157, 21)
        Me.Label46.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(19, 15)
        Me.Label46.TabIndex = 3
        Me.Label46.Text = "件"
        '
        'lblListCount
        '
        Me.lblListCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblListCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblListCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblListCount.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblListCount.Location = New System.Drawing.Point(1067, 17)
        Me.lblListCount.Name = "lblListCount"
        Me.lblListCount.Size = New System.Drawing.Size(84, 22)
        Me.lblListCount.TabIndex = 5
        Me.lblListCount.Text = "0"
        Me.lblListCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dgvList
        '
        Me.dgvList.AllowUserToAddRows = False
        Me.dgvList.AllowUserToDeleteRows = False
        Me.dgvList.AllowUserToResizeColumns = False
        Me.dgvList.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvList.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvList.ColumnHeadersHeight = 25
        Me.dgvList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.出荷日, Me.伝票番号, Me.出荷先CD, Me.出荷先名, Me.住所, Me.電話番号, Me.運送便, Me.商品名})
        Me.dgvList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvList.Location = New System.Drawing.Point(3, 42)
        Me.dgvList.MultiSelect = False
        Me.dgvList.Name = "dgvList"
        Me.dgvList.RowHeadersVisible = False
        Me.dgvList.RowHeadersWidth = 25
        Me.dgvList.RowTemplate.Height = 21
        Me.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvList.Size = New System.Drawing.Size(1173, 323)
        Me.dgvList.TabIndex = 0
        '
        '出荷日
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.出荷日.DefaultCellStyle = DataGridViewCellStyle2
        Me.出荷日.HeaderText = "出荷日"
        Me.出荷日.Name = "出荷日"
        '
        '伝票番号
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.伝票番号.DefaultCellStyle = DataGridViewCellStyle3
        Me.伝票番号.HeaderText = "伝票番号"
        Me.伝票番号.Name = "伝票番号"
        '
        '出荷先CD
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.出荷先CD.DefaultCellStyle = DataGridViewCellStyle4
        Me.出荷先CD.HeaderText = "出荷先CD"
        Me.出荷先CD.Name = "出荷先CD"
        '
        '出荷先名
        '
        DataGridViewCellStyle5.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.出荷先名.DefaultCellStyle = DataGridViewCellStyle5
        Me.出荷先名.HeaderText = "出荷先名"
        Me.出荷先名.Name = "出荷先名"
        Me.出荷先名.Width = 200
        '
        '住所
        '
        DataGridViewCellStyle6.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.住所.DefaultCellStyle = DataGridViewCellStyle6
        Me.住所.HeaderText = "住所"
        Me.住所.Name = "住所"
        Me.住所.Width = 200
        '
        '電話番号
        '
        DataGridViewCellStyle7.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.電話番号.DefaultCellStyle = DataGridViewCellStyle7
        Me.電話番号.HeaderText = "電話番号"
        Me.電話番号.Name = "電話番号"
        Me.電話番号.Width = 150
        '
        '運送便
        '
        DataGridViewCellStyle8.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.運送便.DefaultCellStyle = DataGridViewCellStyle8
        Me.運送便.HeaderText = "運送便"
        Me.運送便.Name = "運送便"
        '
        '商品名
        '
        DataGridViewCellStyle9.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.商品名.DefaultCellStyle = DataGridViewCellStyle9
        Me.商品名.HeaderText = "商品名"
        Me.商品名.Name = "商品名"
        Me.商品名.ReadOnly = True
        Me.商品名.Width = 200
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.55766!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.04987!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.39248!))
        Me.TableLayoutPanel1.Controls.Add(Me.lblMode, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label38, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel29, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 2, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1179, 133)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'lblMode
        '
        Me.lblMode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMode.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMode.Location = New System.Drawing.Point(1035, 0)
        Me.lblMode.Name = "lblMode"
        Me.lblMode.Size = New System.Drawing.Size(141, 19)
        Me.lblMode.TabIndex = 13
        Me.lblMode.Text = "編集モード"
        Me.lblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel12, 0, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel9, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel8, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel7, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel4, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(572, 19)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 5
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(460, 114)
        Me.TableLayoutPanel2.TabIndex = 6
        '
        'TableLayoutPanel12
        '
        Me.TableLayoutPanel12.ColumnCount = 3
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.1588!))
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 88.8412!))
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel12.Controls.Add(Me.chkTorikesi, 2, 0)
        Me.TableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel12.Location = New System.Drawing.Point(0, 88)
        Me.TableLayoutPanel12.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel12.Name = "TableLayoutPanel12"
        Me.TableLayoutPanel12.RowCount = 1
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel12.Size = New System.Drawing.Size(460, 26)
        Me.TableLayoutPanel12.TabIndex = 11
        '
        'chkTorikesi
        '
        Me.chkTorikesi.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTorikesi.AutoSize = True
        Me.chkTorikesi.Checked = True
        Me.chkTorikesi.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTorikesi.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkTorikesi.Location = New System.Drawing.Point(54, 3)
        Me.chkTorikesi.Name = "chkTorikesi"
        Me.chkTorikesi.Size = New System.Drawing.Size(139, 19)
        Me.chkTorikesi.TabIndex = 4
        Me.chkTorikesi.Text = "取消データを含める"
        Me.chkTorikesi.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel9
        '
        Me.TableLayoutPanel9.ColumnCount = 3
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.82114!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 86.17886!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel9.Controls.Add(Me.txtSyohinName, 2, 0)
        Me.TableLayoutPanel9.Controls.Add(Me.Label13, 1, 0)
        Me.TableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel9.Location = New System.Drawing.Point(0, 66)
        Me.TableLayoutPanel9.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel9.Name = "TableLayoutPanel9"
        Me.TableLayoutPanel9.RowCount = 1
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel9.Size = New System.Drawing.Size(460, 22)
        Me.TableLayoutPanel9.TabIndex = 10
        '
        'txtSyohinName
        '
        Me.txtSyohinName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSyohinName.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSyohinName.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.txtSyohinName.Location = New System.Drawing.Point(147, 0)
        Me.txtSyohinName.Margin = New System.Windows.Forms.Padding(0)
        Me.txtSyohinName.Name = "txtSyohinName"
        Me.txtSyohinName.Size = New System.Drawing.Size(313, 22)
        Me.txtSyohinName.TabIndex = 4
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label13.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label13.Location = New System.Drawing.Point(50, 0)
        Me.Label13.Margin = New System.Windows.Forms.Padding(0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(97, 22)
        Me.Label13.TabIndex = 3
        Me.Label13.Text = "商品名"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel8
        '
        Me.TableLayoutPanel8.ColumnCount = 5
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.78928!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.54386!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.52632!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.79904!))
        Me.TableLayoutPanel8.Controls.Add(Me.txtUnsoubin, 2, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.Label12, 1, 0)
        Me.TableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel8.Location = New System.Drawing.Point(0, 44)
        Me.TableLayoutPanel8.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel8.Name = "TableLayoutPanel8"
        Me.TableLayoutPanel8.RowCount = 1
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.Size = New System.Drawing.Size(460, 22)
        Me.TableLayoutPanel8.TabIndex = 9
        '
        'txtUnsoubin
        '
        Me.txtUnsoubin.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtUnsoubin.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUnsoubin.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.txtUnsoubin.Location = New System.Drawing.Point(147, 0)
        Me.txtUnsoubin.Margin = New System.Windows.Forms.Padding(0)
        Me.txtUnsoubin.Name = "txtUnsoubin"
        Me.txtUnsoubin.Size = New System.Drawing.Size(136, 22)
        Me.txtUnsoubin.TabIndex = 4
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label12.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.Location = New System.Drawing.Point(50, 0)
        Me.Label12.Margin = New System.Windows.Forms.Padding(0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(97, 22)
        Me.Label12.TabIndex = 3
        Me.Label12.Text = "運送便"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.ColumnCount = 5
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.78928!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.54386!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.52632!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.79904!))
        Me.TableLayoutPanel7.Controls.Add(Me.txtDenpyoNoTo, 4, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.txtDenpyoNoFrom, 2, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.Label10, 1, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.Label11, 3, 0)
        Me.TableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(0, 22)
        Me.TableLayoutPanel7.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 1
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(460, 22)
        Me.TableLayoutPanel7.TabIndex = 8
        '
        'txtDenpyoNoTo
        '
        Me.txtDenpyoNoTo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDenpyoNoTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDenpyoNoTo.Location = New System.Drawing.Point(321, 0)
        Me.txtDenpyoNoTo.Margin = New System.Windows.Forms.Padding(0)
        Me.txtDenpyoNoTo.Name = "txtDenpyoNoTo"
        Me.txtDenpyoNoTo.Size = New System.Drawing.Size(139, 22)
        Me.txtDenpyoNoTo.TabIndex = 5
        '
        'txtDenpyoNoFrom
        '
        Me.txtDenpyoNoFrom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDenpyoNoFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDenpyoNoFrom.Location = New System.Drawing.Point(147, 0)
        Me.txtDenpyoNoFrom.Margin = New System.Windows.Forms.Padding(0)
        Me.txtDenpyoNoFrom.Name = "txtDenpyoNoFrom"
        Me.txtDenpyoNoFrom.Size = New System.Drawing.Size(136, 22)
        Me.txtDenpyoNoFrom.TabIndex = 4
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label10.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.Location = New System.Drawing.Point(50, 0)
        Me.Label10.Margin = New System.Windows.Forms.Padding(0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(97, 22)
        Me.Label10.TabIndex = 3
        Me.Label10.Text = "伝票番号"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(291, 3)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(22, 15)
        Me.Label11.TabIndex = 7
        Me.Label11.Text = "～"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 5
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.78928!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.54386!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.52632!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.79904!))
        Me.TableLayoutPanel4.Controls.Add(Me.Label6, 1, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.Label9, 3, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.dtpSyukkaDateFrom, 2, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.dtpSyukkaDateTo, 4, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(460, 22)
        Me.TableLayoutPanel4.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(50, 0)
        Me.Label6.Margin = New System.Windows.Forms.Padding(0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(97, 22)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "出荷日"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(291, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(22, 15)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "～"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpSyukkaDateFrom
        '
        Me.dtpSyukkaDateFrom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpSyukkaDateFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtpSyukkaDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpSyukkaDateFrom.Location = New System.Drawing.Point(147, 0)
        Me.dtpSyukkaDateFrom.Margin = New System.Windows.Forms.Padding(0)
        Me.dtpSyukkaDateFrom.Name = "dtpSyukkaDateFrom"
        Me.dtpSyukkaDateFrom.Size = New System.Drawing.Size(136, 22)
        Me.dtpSyukkaDateFrom.TabIndex = 8
        '
        'dtpSyukkaDateTo
        '
        Me.dtpSyukkaDateTo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpSyukkaDateTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtpSyukkaDateTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpSyukkaDateTo.Location = New System.Drawing.Point(321, 0)
        Me.dtpSyukkaDateTo.Margin = New System.Windows.Forms.Padding(0)
        Me.dtpSyukkaDateTo.Name = "dtpSyukkaDateTo"
        Me.dtpSyukkaDateTo.Size = New System.Drawing.Size(139, 22)
        Me.dtpSyukkaDateTo.TabIndex = 9
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel3.Controls.Add(Me.TableLayoutPanel5, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.btnSearch, 0, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(1032, 19)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.27273!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.72727!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(147, 114)
        Me.TableLayoutPanel3.TabIndex = 7
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 1
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(0, 65)
        Me.TableLayoutPanel5.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 1
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(141, 22)
        Me.TableLayoutPanel5.TabIndex = 11
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(113, 15)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "（一部一致検索）"
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(45, 0)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(0)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(102, 36)
        Me.btnSearch.TabIndex = 12
        Me.btnSearch.Text = "検索(&S)"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'frmH01F50_SelectRireki
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1185, 578)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel27)
        Me.Name = "frmH01F50_SelectRireki"
        Me.Text = "注文選択(H01F50)"
        Me.TableLayoutPanel31.ResumeLayout(False)
        Me.TableLayoutPanel29.ResumeLayout(False)
        Me.TableLayoutPanel30.ResumeLayout(False)
        Me.TableLayoutPanel30.PerformLayout()
        Me.TableLayoutPanel33.ResumeLayout(False)
        Me.TableLayoutPanel27.ResumeLayout(False)
        Me.TableLayoutPanel32.ResumeLayout(False)
        Me.TableLayoutPanel34.ResumeLayout(False)
        Me.TableLayoutPanel34.PerformLayout()
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.TableLayoutPanel6.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.TableLayoutPanel11.ResumeLayout(False)
        Me.TableLayoutPanel11.PerformLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel12.ResumeLayout(False)
        Me.TableLayoutPanel12.PerformLayout()
        Me.TableLayoutPanel9.ResumeLayout(False)
        Me.TableLayoutPanel9.PerformLayout()
        Me.TableLayoutPanel8.ResumeLayout(False)
        Me.TableLayoutPanel8.PerformLayout()
        Me.TableLayoutPanel7.ResumeLayout(False)
        Me.TableLayoutPanel7.PerformLayout()
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    '戻るボタンクリック
    Private Sub cmdBack_Click(sender As Object, e As EventArgs) Handles cmdBack.Click

        _parentForm.Show()                                              ' 前画面を表示
        _parentForm.Enabled = True                                      ' 前画面の使用を可能にする
        _parentForm.Activate()                                          ' 前画面をアクティブにする

        Me.Dispose()                                                    ' 自画面を閉じる
    End Sub

    '選択ボタンクリック時
    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        Try
            '注文データ選択処理
            selectChumon()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '注文データ選択処理
    Private Sub selectChumon()

        Dim idx As Integer
        For Each c As DataGridViewRow In dgvList.SelectedRows
            idx = c.Index
            Exit For
        Next c

        Dim strDenpyoNo As String = ""    '伝票番号
        strDenpyoNo = dgvList.Rows(idx).Cells(COLNO_DENPYONO).Value

        '項目のチェック処理
        Try
            Call checkInput(strDenpyoNo)
        Catch lex As UsrDefException
            lex.dspMsg()
            Exit Sub
        End Try

        strDenpyoNo = strDenpyoNo.Replace(TORIKESHI, "")   '取消を除去

        Dim openForm As Form = Nothing
        openForm = New frmH01F60_Chumon(_msgHd, _db, _SelectID, _SelectMode, dgvList.Rows(idx).Cells(COLNO_SYUKKACD).Value, Me, strDenpyoNo)
        openForm.Show()
        Me.Hide()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '   入力チェック
    '------------------------------------------------------------------------------------------------------
    Private Sub checkInput(prmDenpyoNo As String)
        '変更/取消/参照モード
        '①　選択データの妥当性チェック処理
        '・選択データが取消データの場合はエラーとする。
        Select Case _SelectMode
            Case CommonConst.MODE_ADDNEW   '複写新規
            Case CommonConst.MODE_EditStatus, CommonConst.MODE_CancelStatus, CommonConst.MODE_InquiryStatus   '変更 取消 参照
                If prmDenpyoNo.Contains(TORIKESHI) Then
                    Throw New UsrDefException("取消データは選択できません。", _msgHd.getMSG("cannotSelectTorikeshiData"))
                End If
            Case Else
        End Select


    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles rdbDenpyo.CheckedChanged
        Try
            'リストを表示しなおし
            DispList()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub
    '検索ボタンクリック
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            'リスト表示
            DispList()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    'テキストボックスのキープレスイベント
    Private Sub txtSyukkasakiName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSyukkasakiName.KeyPress, txtAddress.KeyPress, txtTel.KeyPress, txtSyukkaCd.KeyPress,
                                                                                             dtpSyukkaDateFrom.KeyPress, dtpSyukkaDateTo.KeyPress, txtDenpyoNoFrom.KeyPress, txtDenpyoNoTo.KeyPress,
                                                                                             txtUnsoubin.KeyPress, txtSyohinName.KeyPress
        UtilMDL.UtilClass.moveNextFocus(Me, e)
    End Sub

    '-------------------------------------------------------------------------------
    '　フォーカス取得イベント
    '-------------------------------------------------------------------------------
    Private Sub ctl_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSyukkasakiName.GotFocus, txtAddress.GotFocus, txtTel.GotFocus, txtSyukkaCd.GotFocus,
                                                                                             dtpSyukkaDateFrom.GotFocus, dtpSyukkaDateTo.GotFocus, txtDenpyoNoFrom.GotFocus, txtDenpyoNoTo.GotFocus,
                                                                                             txtUnsoubin.GotFocus, txtSyohinName.GotFocus

        'フォーカス取得時、入力パラメタのコントロールを全選択状態とする
        Call UtilClass.selAll(sender)

    End Sub

    '一覧セルダブルクリック
    Private Sub dgvLIST_CellDoubleClick(sender As Object, e As EventArgs) Handles dgvList.CellDoubleClick

        Try
            'ヘッダー行ダブルクリックの場合、処理終了
            If TryCast(e, DataGridViewCellEventArgs).RowIndex < 0 Then
                Exit Sub
            End If

            '注文データ選択処理
            selectChumon()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '一覧キーダウン
    Private Sub dgvLIST_KeyDown(sender As Object, e As EventArgs) Handles dgvList.KeyDown

        Try
            Dim keyEventArgs As KeyEventArgs = TryCast(e, KeyEventArgs)

            If keyEventArgs.KeyData = Keys.Enter Then
                '押下キーがEnterの場合

                '注文データ選択処理
                selectChumon()

                'Enterキー処理無効化
                keyEventArgs.Handled = True

            Else
                'タブキー押下時制御 タブキー押下時、行移動する
                _gh.gridTabKeyDown(Me, e)
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

End Class