Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Public Class frmH03F20_Itaku

    '-------------------------------------------------------------------------------
    '   定数定義
    '-------------------------------------------------------------------------------
    Private Const N As String = ControlChars.NewLine            '改行文字
    Private Const RS As String = "RecSet"                       'レコードセットテーブル
    'グリッド列№
    'dgvList1
    Private Const L1_COLNO_NO = 0               '01:行番号（明細）
    Private Const L1_COLNO_SHOHINCD = 1         '02:商品コード
    Private Const L1_COLNO_SHOHINNM = 2         '03:商品名
    Private Const L1_COLNO_ZEIKBN = 3           '04:税区分
    Private Const L1_COLNO_IRISU = 4            '05:入数
    Private Const L1_COLNO_KOSU = 5             '06:個数
    Private Const L1_COLNO_ITAKUSURYO = 6       '07:委託数量
    Private Const L1_COLNO_TANI = 7             '08:単位
    Private Const L1_COLNO_URISURYO = 8         '09:売上数量
    Private Const L1_COLNO_MEKIRISURYO = 9      '10:目切数量
    Private Const L1_COLNO_KONKAIURI = 10       '11:今回売上
    Private Const L1_COLNO_KONKAIMEKIRI = 11    '12:今回目切
    Private Const L1_COLNO_ITAKUZAN = 12        '13:委託残数
    Private Const L1_COLNO_KARITANKA = 13       '14:仮単価
    'グリッド列№
    'dgvList2
    Private Const L2_COLNO_NO = 0               '01:行番号
    Private Const L2_COLNO_EDANo = 1            '02:売上伝番枝番
    Private Const L2_COLNO_URIYMD = 2           '03:売上日
    Private Const L2_COLNO_GyoNO = 3            '04:注文行番号
    Private Const L2_COLNO_SHOHINCD = 4         '05:商品コード
    Private Const L2_COLNO_SHOHINNM = 5         '06:商品名
    Private Const L2_COLNO_ZEIKBN = 6           '07:税区分
    Private Const L2_COLNO_TANI = 7             '08:単位
    Private Const L2_COLNO_URISURYO = 8         '09:売上数量
    Private Const L2_COLNO_MEKIRISURYO = 9      '10:目切数量
    Private Const L2_COLNO_URIAGETANKA = 10     '11:売上単価
    Private Const L2_COLNO_URIAGEMONEY = 11     '12:売上金額
    Private Const L2_COLNO_URIBIKO = 12         '13:売上明細備考

    'dgvList3
    Private Const L3_COLNO_NO = 0               '01:行番号
    Private Const L3_COLNO_MEISAI = 1           '02:明細
    Private Const L3_COLNO_SHOHINCD = 2         '03:商品コード
    Private Const L3_COLNO_SHOHINNM = 3         '04:商品名
    Private Const L3_COLNO_ZEIKBN = 4           '05:税区分
    Private Const L3_COLNO_TANI = 5             '06:単位
    Private Const L3_COLNO_URISURYO = 6         '07:売上数量
    Private Const L3_COLNO_MEKIRISURYO = 7      '08:目切数量
    Private Const L3_COLNO_URITANKA = 8         '09:売単価
    Private Const L3_COLNO_URIMONEY = 9         '10:売上金額
    Private Const L3_COLNO_URIBIKO = 10         '11:売上明細備考
    Private Const L3_COLNO_ZEIKBNCD = 11        '12:課税区分
    Private Const L3_COLNO_TAX_RATE = 12        '13:税率
    Private Const L3_COLNO_TAX_EXCLUSION = 13   '14:税抜額
    Private Const L3_COLNO_TAX_TAXABLE = 14     '15:課税対象額
    Private Const L3_COLNO_TAX_AMOUNT = 15      '16:消費税額
    Private Const L3_COLNO_NISUGATA = 16        '17:荷姿
    Private Const L3_COLNO_IRISUU = 17          '18:入数
    Private Const L3_COLNO_KOSUU = 18           '19:個数


    'グリッド列名
    'dgvList3
    Private Const L3_CCOL_NO As String = "cnNo"                        '01:行番号.
    Private Const L3_CCOL_MEISAI As String = "cnMeisai"                '02:明細
    Private Const L3_CCOL_SHOHINCD As String = "cnShohinCD"            '03:商品コード
    Private Const L3_CCOL_SHOHINNM As String = "cnShohinNM"            '04:商品名
    Private Const L3_CCOL_ZEIKBN As String = "cnZeiKBN"                '05:税区分
    Private Const L3_CCOL_TANI As String = "cnTani"                    '06:単位
    Private Const L3_CCOL_URISURYO As String = "cnUriSuryo"            '07:売上数量
    Private Const L3_CCOL_MEKIRISURYO As String = "cnMekiriSuryo"      '08:目切数量
    Private Const L3_CCOL_URITANKA As String = "cnUriTanka"            '09:売単価
    Private Const L3_CCOL_URIMONEY As String = "cnUriMoney"            '10:売上金額
    Private Const L3_CCOL_URIBIKO As String = "cnUriBiko"              '11:売上明細備考
    Private Const L3_CCOL_ZEIKBNCD As String = "cnZeiKbnCD"            '12:課税区分
    Private Const L3_CCOL_TAX_RATE As String = "cnTaxRate"             '13:税率
    Private Const L3_CCOL_TAX_EXCLUSION As String = "cnTaxExclusion"   '14:税抜額
    Private Const L3_CCOL_TAX_TAXABLE As String = "cnTaxAble"          '15:課税対象額
    Private Const L3_CCOL_TAX_AMOUNT As String = "cnTaxAmount"         '16:消費税額
    Private Const L3_CCOL_NISUGATA As String = "cnNisugata"            '17:荷姿
    Private Const L3_CCOL_IRISUU As String = "cnIriSuu"                '18:入数
    Private Const L3_CCOL_KOSUU As String = "cnKosuu"                  '19:個数

    'グリッドデータ名
    'dgvList3
    Private Const L3_DTCO_NO As String = "dtNo"                       '01:No.
    Private Const L3_DTCO_MEISAI As String = "dtMeisai"               '02:明細
    Private Const L3_DTCO_SHOHINCD As String = "dtShohinCD"           '03:商品コード
    Private Const L3_DTCO_SHOHINNM As String = "dtShohinNM"           '04:商品名
    Private Const L3_DTCOL_ZEIKBN As String = "dtZeiKBN"              '05:税区分
    Private Const L3_DTCOL_TANI As String = "dtTani"                  '06:単位
    Private Const L3_DTCOL_URISURYO As String = "dtUriSuryo"          '07:売上数量
    Private Const L3_DTCOL_MEKIRISURYO As String = "dtMekiriSuryo"    '08:目切数量
    Private Const L3_DTCOL_URITANKA As String = "dtUriTanka"          '09:売単価
    Private Const L3_DTCOL_URIMONEY As String = "dtnUriMoney"         '10:売上金額
    Private Const L3_DTCOL_URIBIKO As String = "dtUriBiko"            '11:売上明細備考
    Private Const L3_DTCOL_ZEIKBNCD As String = "dtZeiKbnCD"          '12:課税区分
    Private Const L3_DTCOL_TAX_RATE As String = "dtTaxRate"           '13:税率
    Private Const L3_DTCOL_TAX_EXCLUSION As String = "dtTaxExclusion" '14:税抜額
    Private Const L3_DTCOL_TAX_TAXABLE As String = "dtTaxAble"        '15:課税対象額
    Private Const L3_DTCOL_TAX_AMOUNT As String = "dtTaxAmount"       '16:消費税額
    Private Const L3_DTCOL_NISUGATA As String = "dtNisugata"          '17:荷姿
    Private Const L3_DTCOL_IRISUU As String = "dtIriSuu"              '18:入数
    Private Const L3_DTCOL_KOSUU As String = "dtKosuu"                '19:個数

    '-------------------------------------------------------------------------------
    '   変数定義
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As Form                             '親フォーム
    Private _SelectID As String
    Private _SelectMode As Integer   'メニューのどこから呼ばれたか。（0:登録、1:変更、2:取消、3:照会)
    Private _DenpyoNo As String         '伝票番号
    Private _DenpyoNoEda As String      '伝票番号枝番
    Private _Tax As Decimal             '消費税率
    Private _ZeiHasu As String                  '税端数区分
    Private _UpdateTime As DateTime = Nothing   '更新日時　排他チェックに使用する
    Private _userId As String

    Private _comLogc As CommonLogic                         '共通処理用
    Private _SQL As String
    Private _frmH03F10 As frmH03F10_SelectItakuList
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
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByRef prmSelectID As String, ByRef prmParentForm As Form, ByRef prmSelectMode As Integer, ByVal prmItakuDenpyoNO As String, Optional ByVal prmItakuDenpyoNOEda As String = "", Optional ByRef prmFormH03F10 As frmH03F10_SelectItakuList = Nothing)
        Call Me.New()

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        _parentForm = prmParentForm
        _SelectID = prmSelectID
        _SelectMode = prmSelectMode                                         '処理状態
        _comLogc = New CommonLogic(_db, _msgHd)                             ' 共通処理用
        _frmH03F10 = prmFormH03F10      '継続登録確認用
        _userId = frmC01F10_Login.loginValue.TantoCD                        'ユーザＩＤ

        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        _DenpyoNo = prmItakuDenpyoNO
        _DenpyoNoEda = prmItakuDenpyoNOEda
        lblDenpyoNo.Text = _DenpyoNo

        '処理状態の選択
        Select Case _SelectMode
            Case CommonConst.MODE_ADDNEW  '登録
                lblShoriMode.Text = "登録"
                Me.cmdRowCopy.Enabled = True
                Me.btnRenzoku.Enabled = True
                Me.cmdTouroku.Enabled = True
                Me.cmdModoru.Enabled = True
                Me.dgvList3.ReadOnly = False
            Case CommonConst.MODE_EditStatus  '変更
                lblShoriMode.Text = "変更"
                Me.cmdRowCopy.Enabled = True
                Me.btnRenzoku.Enabled = False
                Me.cmdTouroku.Enabled = True
                Me.cmdModoru.Enabled = True
                Me.dgvList3.ReadOnly = False
            Case CommonConst.MODE_CancelStatus  '取消
                lblShoriMode.Text = "取消"
                Me.cmdRowCopy.Enabled = False
                Me.btnRenzoku.Enabled = False
                Me.cmdTouroku.Enabled = True
                Me.cmdModoru.Enabled = True
                Me.dgvList3.ReadOnly = True
            Case CommonConst.MODE_InquiryStatus  '照会
                lblShoriMode.Text = "照会"
                Me.cmdRowCopy.Enabled = False
                Me.btnRenzoku.Enabled = False
                Me.cmdTouroku.Enabled = False
                Me.cmdModoru.Enabled = True
                Me.dgvList3.ReadOnly = True
        End Select


        '委託基本の表示
        getItakuData()
        '委託明細の表示
        getList1()
        '売上明細の表示
        getList2()

        getKonkaiUriageDataList()

        dgvList1.CurrentCell = Nothing
        dgvList2.CurrentCell = Nothing
        dgvList3.CurrentCell = Nothing

    End Sub

    '委託データの初期表示
    Private Sub getItakuData()


        'データ初期表示

        Dim strSql As String = ""

        Try
            strSql = "SELECT  "
            strSql = strSql & "    t15.出荷日 ,t15.着日 ,t15.出荷先コード ,t15.出荷先名 ,t15.請求先コード ,t15.請求先名 , t.消費税率  ,a.税端数区分 ,t15.更新日 "
            strSql = strSql & " FROM t15_itakhd t15 "
            strSql = strSql & "   left join m71_ctax t on t.会社コード = t15.会社コード and t.適用開始日 <= t15.着日  and t.適用終了日 >= t15.着日 "
            strSql = strSql & "    inner join m10_customer a on a.会社コード = t15.会社コード and a.取引先コード = t15.出荷先コード " 'もしかしたら請求先？

            strSql = strSql & " Where t15.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and t15.委託伝番 = '" & _DenpyoNo & "'"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(strSql, RS, reccnt)

            'データがない場合
            If reccnt = 0 Then
                Exit Sub
            End If
            '消費税率
            _Tax = _db.rmNullDouble(ds.Tables(RS).Rows(0)("消費税率"))
            '出荷日
            lblShukkaDt.Text = _db.rmNullDate(ds.Tables(RS).Rows(0)("出荷日"), "yyyy/MM/dd")
            lblShukkaDay.Text = _db.rmNullDate(ds.Tables(RS).Rows(0)("出荷日"), "ddd")
            '着日
            lblChakuDt.Text = _db.rmNullDate(ds.Tables(RS).Rows(0)("着日"), "yyyy/MM/dd")
            lblChakuDay.Text = _db.rmNullDate(ds.Tables(RS).Rows(0)("着日"), "ddd")

            '出荷先
            lblSyukkaCD.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("出荷先コード"))
            lblSyukkaNM.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("出荷先名"))

            '請求先
            lblSeikyuCD.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("請求先コード"))
            lblSeikyuNM.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("請求先名"))

            _ZeiHasu = _db.rmNullStr(ds.Tables(RS).Rows(0)("税端数区分"))      '税端数区分
            _UpdateTime = _db.rmNullDate(ds.Tables(RS).Rows(0)("更新日"))      '更新日

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub
    '--------------------------------
    '抽出条件から委託明細データ取得
    '--------------------------------
    Private Sub getList1()
        Dim strSql As String = ""
        Try
            '取得したデータをDataGrdViewに反映
            strSql = "SELECT "
            strSql = strSql & "  t16.行番 ,t16.商品コード ,t16.商品名,t16.課税区分,t16.入数,t16.個数,t16.委託数量,t16.単位,t16.売上数量計,t16.委託残数,h2.文字２ as 課税区分名 ,t16.仮単価 "
            strSql = strSql & "  ,t16.売上数量計 as 売上数量,t16.目切数量計 as 目切数量,0.00 as 今回売上,0.00 as 今回目切  "
            strSql = strSql & "  ,t16.委託数量 -t16.売上数量計 - t16.目切数量計 as 委託残数  "
            strSql = strSql & " FROM T16_ITAKDT t16 "
            strSql = strSql & "   left join M90_HANYO h2 on h2.会社コード = t16.会社コード and h2.固定キー = '" & CommonConst.HANYO_KAZEI_KBN & "' and h2.可変キー = t16.課税区分 "

            strSql = strSql & " Where t16.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and t16.委託伝番 = '" & _DenpyoNo & "'"

            strSql = strSql & " order by t16.行番 "

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(strSql, RS, reccnt)

            '抽出データ件数を取得、表示
            lblListCount1.Text = reccnt


            '描画の前にすべてクリアする
            dgvList1.Rows.Clear()
            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                dgvList1.Rows.Add()
                dgvList1.Rows(index).Cells(L1_COLNO_NO).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("行番"))
                dgvList1.Rows(index).Cells(L1_COLNO_SHOHINCD).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("商品コード"))
                dgvList1.Rows(index).Cells(L1_COLNO_SHOHINNM).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("商品名"))
                dgvList1.Rows(index).Cells(L1_COLNO_ZEIKBN).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("課税区分名"))
                dgvList1.Rows(index).Cells(L1_COLNO_IRISU).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("入数"))
                dgvList1.Rows(index).Cells(L1_COLNO_KOSU).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("個数"))
                dgvList1.Rows(index).Cells(L1_COLNO_ITAKUSURYO).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("委託数量"))
                dgvList1.Rows(index).Cells(L1_COLNO_TANI).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("単位"))
                dgvList1.Rows(index).Cells(L1_COLNO_URISURYO).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("売上数量"))
                dgvList1.Rows(index).Cells(L1_COLNO_MEKIRISURYO).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("目切数量"))
                dgvList1.Rows(index).Cells(L1_COLNO_KONKAIURI).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("今回売上"))
                dgvList1.Rows(index).Cells(L1_COLNO_KONKAIMEKIRI).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("今回目切"))
                dgvList1.Rows(index).Cells(L1_COLNO_ITAKUZAN).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("委託残数"))
                dgvList1.Rows(index).Cells(L1_COLNO_KARITANKA).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("仮単価"))
            Next


        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            'Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub
    '--------------------------------
    '抽出条件から売上明細データ取得
    '--------------------------------
    Private Sub getList2()
        Dim strSql As String = ""
        Try
            '取得したデータをDataGrdViewに反映
            strSql = "SELECT "
            strSql = strSql & "  t20.税抜額計, t20.課税対象額計, t20.消費税計, t20.税込額計 "
            strSql = strSql & "  , t21.売上伝番枝番, t20.売上日, t21.注文行番, t21.商品コード, t21.商品名, h2.文字２ as 課税区分名, t21.行番 "
            strSql = strSql & "  , t21.単位, t21.売上数量, t21.目切数量, t21.売上単価, t21.売上金額, t21.売上明細備考  "
            strSql = strSql & " FROM T20_URIGHD t20 "
            strSql = strSql & "     inner join T21_URIGDT t21 on t21.会社コード = t20.会社コード and t21.売上伝番 = t20.売上伝番 and t21.売上伝番枝番 = t20.売上伝番枝番 "
            strSql = strSql & "     left join M90_HANYO h2 on h2.会社コード = t20.会社コード and h2.固定キー = '" & CommonConst.HANYO_KAZEI_KBN & "' and h2.可変キー = t21.課税区分 "

            strSql = strSql & " Where t20.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and t20.売上伝番 = '" & _DenpyoNo & "' and t20.取消区分 <>1 "
            If _DenpyoNoEda <> "" Then
                strSql = strSql & " and t21.売上伝番枝番 <> '" & _DenpyoNoEda & "' "
            End If
            strSql = strSql & " order by t21.売上伝番枝番, t21.行番 "


            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(strSql, RS, reccnt)

            '抽出データ件数を取得、表示
            lblListCount2.Text = reccnt

            Me.lblZeiNuki.Text = Decimal.Parse(_db.rmNullStr(ds.Tables(RS).Rows(0)("税抜額計"))).ToString("N0")
            Me.lblKazei.Text = Decimal.Parse(_db.rmNullStr(ds.Tables(RS).Rows(0)("課税対象額計"))).ToString("N0")
            Me.lblTax.Text = Decimal.Parse(_db.rmNullStr(ds.Tables(RS).Rows(0)("消費税計"))).ToString("N0")
            Me.lblZeikomi.Text = Decimal.Parse(_db.rmNullStr(ds.Tables(RS).Rows(0)("税込額計"))).ToString("N0")

            '描画の前にすべてクリアする
            dgvList2.Rows.Clear()
            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                dgvList2.Rows.Add()
                dgvList2.Rows(index).Cells(L2_COLNO_NO).Value = (index + 1).ToString
                dgvList2.Rows(index).Cells(L2_COLNO_EDANo).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("売上伝番枝番"))
                dgvList2.Rows(index).Cells(L2_COLNO_URIYMD).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("売上日"))
                dgvList2.Rows(index).Cells(L2_COLNO_GyoNO).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("注文行番"))
                dgvList2.Rows(index).Cells(L2_COLNO_SHOHINCD).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("商品コード"))
                dgvList2.Rows(index).Cells(L2_COLNO_SHOHINNM).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("商品名"))
                dgvList2.Rows(index).Cells(L2_COLNO_ZEIKBN).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("課税区分名"))
                dgvList2.Rows(index).Cells(L2_COLNO_TANI).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("単位"))
                dgvList2.Rows(index).Cells(L2_COLNO_URISURYO).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("売上数量"))
                dgvList2.Rows(index).Cells(L2_COLNO_MEKIRISURYO).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("目切数量"))
                dgvList2.Rows(index).Cells(L2_COLNO_URIAGETANKA).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("売上単価"))
                dgvList2.Rows(index).Cells(L2_COLNO_URIAGEMONEY).Value = Decimal.Parse(_db.rmNullStr(ds.Tables(RS).Rows(index)("売上金額"))).ToString("N0")
                dgvList2.Rows(index).Cells(L2_COLNO_URIBIKO).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("売上明細備考"))
            Next


        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            'Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '新規の場合、委託明細を今回売上明細に表示する
    '
    Private Sub getKonkaiUriageDataList()

        Dim strSql As String = ""

        Try
            '売上日
            dtpUriageDt.Value = Now()
            lblUriageDay.Text = Now.ToString("ddd")

            If _DenpyoNoEda = "" Then
                strSql = "SELECT "
                strSql = strSql & "  t16.行番 ,t16.商品コード ,t16.商品名 ,t16.課税区分 ,t16.入数,t16.個数 ,t16.単位 ,h2.文字２ as 課税区分名 ,t16.荷姿形状 "
                strSql = strSql & "  ,0.00 as 売上数量 , 0.00 as 目切数量 ,0.00 as 売上金額 ,t16.仮単価 as 売上単価 ,'' as 売上明細備考 "
                strSql = strSql & " FROM T16_ITAKDT t16 "
                strSql = strSql & "   left join M90_HANYO h2 on h2.会社コード = t16.会社コード and h2.固定キー = '" & CommonConst.HANYO_KAZEI_KBN & "' and h2.可変キー = t16.課税区分 "
                strSql = strSql & " Where t16.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and t16.委託伝番 = '" & _DenpyoNo & "'"
                strSql = strSql & " order by t16.行番 "
            Else
                strSql = "SELECT "
                strSql = strSql & "  t21.行番 ,t21.商品コード ,t21.商品名 ,t21.課税区分 ,t21.入数,t21.個数 ,t21.単位 ,h2.文字２ as 課税区分名 ,t21.荷姿形状 "
                strSql = strSql & "  ,t21.売上数量 , t21.目切数量 ,t21.売上金額 ,t21.売上単価 ,t21.売上明細備考 "
                strSql = strSql & " FROM T20_URIGHD t20 "
                strSql = strSql & "   inner join T21_URIGDT t21 on t21.会社コード = t20.会社コード and t21.売上伝番 = t20.売上伝番 and t21.売上伝番枝番 = t20.売上伝番枝番 "
                strSql = strSql & "   left join M90_HANYO h2 on h2.会社コード = t20.会社コード and h2.固定キー = '" & CommonConst.HANYO_KAZEI_KBN & "' and h2.可変キー = t21.課税区分 "
                strSql = strSql & " Where t20.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and t20.売上伝番 = '" & _DenpyoNo & "' and t20.売上伝番枝番 =" & _DenpyoNoEda
                strSql = strSql & " order by t21.行番 "
            End If

            '今回売上明細の定義
            Dim dt As DataTable = New DataTable(RS)
            dt.Columns().Add(L3_DTCO_NO, Type.GetType("System.String"))
            dt.Columns().Add(L3_DTCO_MEISAI, Type.GetType("System.String"))
            dt.Columns().Add(L3_DTCO_SHOHINCD, Type.GetType("System.String"))
            dt.Columns().Add(L3_DTCO_SHOHINNM, Type.GetType("System.String"))
            dt.Columns().Add(L3_DTCOL_ZEIKBN, Type.GetType("System.String"))
            dt.Columns().Add(L3_DTCOL_TANI, Type.GetType("System.String"))
            dt.Columns().Add(L3_DTCOL_URISURYO, Type.GetType("System.String"))
            dt.Columns().Add(L3_DTCOL_MEKIRISURYO, Type.GetType("System.String"))
            dt.Columns().Add(L3_DTCOL_URITANKA, Type.GetType("System.String"))
            dt.Columns().Add(L3_DTCOL_URIMONEY, Type.GetType("System.String"))
            dt.Columns().Add(L3_DTCOL_URIBIKO, Type.GetType("System.String"))
            dt.Columns().Add(L3_DTCOL_ZEIKBNCD, Type.GetType("System.String"))
            dt.Columns().Add(L3_DTCOL_TAX_RATE, Type.GetType("System.String"))
            dt.Columns().Add(L3_DTCOL_TAX_EXCLUSION, Type.GetType("System.String"))
            dt.Columns().Add(L3_DTCOL_TAX_TAXABLE, Type.GetType("System.String"))
            dt.Columns().Add(L3_DTCOL_TAX_AMOUNT, Type.GetType("System.String"))
            dt.Columns().Add(L3_DTCOL_NISUGATA, Type.GetType("System.String"))
            dt.Columns().Add(L3_DTCOL_IRISUU, Type.GetType("System.String"))
            dt.Columns().Add(L3_DTCOL_KOSUU, Type.GetType("System.String"))


            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(strSql, RS, reccnt)
            '描画の前にすべてクリアする

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                Dim dr As DataRow = dt.NewRow()
                dr.Item(L3_DTCO_NO) = (index + 1).ToString
                dr.Item(L3_DTCO_MEISAI) = _db.rmNullStr(ds.Tables(RS).Rows(index)("行番"))
                dr.Item(L3_DTCO_SHOHINCD) = _db.rmNullStr(ds.Tables(RS).Rows(index)("商品コード"))
                dr.Item(L3_DTCO_SHOHINNM) = _db.rmNullStr(ds.Tables(RS).Rows(index)("商品名"))
                dr.Item(L3_DTCOL_ZEIKBN) = _db.rmNullStr(ds.Tables(RS).Rows(index)("課税区分名"))
                dr.Item(L3_DTCOL_TANI) = _db.rmNullStr(ds.Tables(RS).Rows(index)("単位"))
                dr.Item(L3_DTCOL_URISURYO) = _db.rmNullStr(ds.Tables(RS).Rows(index)("売上数量"))
                dr.Item(L3_DTCOL_MEKIRISURYO) = _db.rmNullStr(ds.Tables(RS).Rows(index)("目切数量"))
                dr.Item(L3_DTCOL_URITANKA) = _db.rmNullStr(ds.Tables(RS).Rows(index)("売上単価"))
                dr.Item(L3_DTCOL_URIMONEY) = Decimal.Parse(_db.rmNullStr(ds.Tables(RS).Rows(index)("売上金額"))).ToString("N0")
                dr.Item(L3_DTCOL_URIBIKO) = _db.rmNullStr(ds.Tables(RS).Rows(index)("売上明細備考"))
                dr.Item(L3_DTCOL_ZEIKBNCD) = _db.rmNullStr(ds.Tables(RS).Rows(index)("課税区分"))
                dr.Item(L3_DTCOL_NISUGATA) = _db.rmNullStr(ds.Tables(RS).Rows(index)("荷姿形状"))
                dr.Item(L3_DTCOL_IRISUU) = _db.rmNullStr(ds.Tables(RS).Rows(index)("入数"))
                dr.Item(L3_DTCOL_KOSUU) = _db.rmNullStr(ds.Tables(RS).Rows(index)("個数"))
                dr.Item(L3_DTCOL_TAX_RATE) = _Tax
                dr.Item(L3_DTCOL_TAX_EXCLUSION) = 0
                dr.Item(L3_DTCOL_TAX_TAXABLE) = 0
                dr.Item(L3_DTCOL_TAX_AMOUNT) = 0
                dt.Rows.Add(dr)
            Next

            Dim dss As DataSet = New DataSet()
            dss.Tables.Add(dt)
            dgvList3.DataSource = dss      '★SQL実行後のレコードセットをここにセットする
            dgvList3.DataMember = RS

            Me.lblListCount3.Text = dgvList3.RowCount
        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try



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
            strWeek1 = ""
        End If

        Return strWeek1
    End Function
    '戻るボタンクリック
    Private Sub cmdModoru_Click(sender As Object, e As EventArgs) Handles cmdModoru.Click
        _parentForm.Show()                                              ' 前画面を表示
        _parentForm.Enabled = True                                      ' 前画面の使用を可能にする
        _parentForm.Activate()                                          ' 前画面をアクティブにする

        Me.Dispose()                                                    ' 自画面を閉じる
    End Sub
    '行複写ボタンクリック
    Private Sub cmdRowCopy_Click(sender As Object, e As EventArgs) Handles cmdRowCopy.Click
        Dim intRowIndex As Integer = 0

        For Each c As DataGridViewCell In dgvList3.SelectedCells
            If TypeOf Me.dgvList3.DataSource Is DataSet Then
                Dim ds As DataSet = TryCast(Me.dgvList3.DataSource, DataSet)
                ds.Tables(RS).Rows.InsertAt(ds.Tables(RS).NewRow, c.RowIndex + 1)
                intRowIndex = c.RowIndex + 1
                ds.Tables(RS).Rows(c.RowIndex + 1).ItemArray = ds.Tables(RS).Rows(c.RowIndex).ItemArray
            ElseIf TypeOf Me.dgvList3.DataSource Is DataTable Then
                Dim dt As DataTable = TryCast(Me.dgvList3.DataSource, DataTable)
                dt.Rows.Add()
            End If
        Next c
        dgvList3.Rows(intRowIndex).Cells(L3_COLNO_URISURYO).Value = (0).ToString("N2")
        dgvList3.Rows(intRowIndex).Cells(L3_COLNO_MEKIRISURYO).Value = (0).ToString("N2")
        dgvList3.Rows(intRowIndex).Cells(L3_COLNO_URITANKA).Value = (0).ToString("N2")
        dgvList3.Rows(intRowIndex).Cells(L3_COLNO_URIMONEY).Value = (0).ToString("N0")
        dgvList3.Rows(intRowIndex).Cells(L3_COLNO_URIBIKO).Value = ""

        '行番号降り直し
        For i As Integer = 0 To dgvList3.RowCount - 1
            dgvList3.Rows(i).Cells(L3_COLNO_NO).Value = i + 1
        Next
        Me.lblListCount3.Text = dgvList3.RowCount

    End Sub

    '登録ボタンクリック時
    Private Sub cmdTouroku_Click(sender As Object, e As EventArgs) Handles cmdTouroku.Click

        '確認メッセージを表示する
        Dim piRtn As Integer
        Dim strMessage As String
        '新規登録の場合
        Select Case _SelectMode     '（1:登録、2:変更、3:取消、4:照会)
            Case CommonConst.MODE_ADDNEW   '登録
                'strMessage = "更新後に委託注文一覧を表示する場合は「いいえ」" & N & "更新をせずに委託売上画面に戻る場合は「キャンセル」"
                piRtn = _msgHd.dspMSG("InsertKeizoku")
                If piRtn = vbCancel Then
                    Exit Sub
                End If
            Case Else
                piRtn = _msgHd.dspMSG("confirmInsert")  '登録します。よろしいですか？
                If piRtn = vbNo Then
                    Exit Sub
                End If
        End Select


        Dim strSql As String = ""
        Dim reccnt As Integer = 0
        Dim ds As DataSet
        '排他チェック処理
        Try
            strSql = "SELECT  "
            strSql = strSql & "    c.更新日,c.更新者 "
            strSql = strSql & " FROM T15_ITAKHD c "
            strSql = strSql & " Where c.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and c.委託伝番 = '" & _DenpyoNo & "'"
            ds = _db.selectDB(strSql, RS, reccnt)
            If reccnt = 0 Then
                Exit Sub
            End If
            If _UpdateTime <> DateTime.Parse(ds.Tables(RS).Rows(0)("更新日")) Then
                '登録終了メッセージ
                strMessage = "更新者：" & ds.Tables(RS).Rows(0)("更新者") & " 更新日時：" & ds.Tables(RS).Rows(0)("更新日").ToString
                _msgHd.dspMSG("Exclusion", strMessage)
                Exit Sub
            End If

            'データ更新
            'トランザクション開始
            If Not _db.isTransactionOpen Then
                _db.beginTran()
            End If

            Dim strMessageID As String = ""
            Select Case _SelectMode     '（1:登録、2:変更、3:取消、4:照会)
                Case CommonConst.MODE_ADDNEW   '登録
                    DataAddNew()
                    strMessageID = "UpdateItakuInfo"
                Case CommonConst.MODE_EditStatus   '変更
                    DataEdit()
                    strMessageID = "EditItakuInfo"
                Case CommonConst.MODE_CancelStatus   '取消
                    DataCancel()
                    strMessageID = "CancelItakuInfo"
                Case CommonConst.MODE_InquiryStatus   '照会

            End Select
            'トランザクション終了
            _db.commitTran()

            '登録終了メッセージ
            _msgHd.dspMSG(strMessageID, "伝票番号：" & lblDenpyoNo.Text)

            '更新後に委託注文一覧を表示する場合は「いいえ」
            If (_SelectMode = CommonConst.MODE_ADDNEW) And (piRtn = vbNo) Then
                _parentForm.Show()                                              ' 前画面を表示
                _parentForm.Enabled = True                                      ' 前画面の使用を可能にする
                _parentForm.Activate()                                          ' 前画面をアクティブにする
                Me.Dispose()                                                    ' 自画面を閉じる
                Exit Sub
            End If
            '更新後に次の伝票を表示する場合は「はい」
            If (_SelectMode = CommonConst.MODE_ADDNEW) And (piRtn = vbYes) Then
                Dim idx As Integer
                For Each c As DataGridViewRow In _frmH03F10.dgvList.SelectedRows
                    idx = c.Index
                    Exit For
                Next c
                For i As Integer = idx To _frmH03F10.dgvList.RowCount - 1
                    '売上数量
                    If _DenpyoNo <> _frmH03F10.dgvList.Rows(i).Cells(8).Value Then
                        _DenpyoNo = _frmH03F10.dgvList.Rows(i).Cells(8).Value
                        lblDenpyoNo.Text = _DenpyoNo

                        '委託基本の表示
                        getItakuData()
                        '委託明細の表示
                        getList1()
                        '売上明細の表示
                        getList2()

                        getKonkaiUriageDataList()

                        dgvList1.CurrentCell = Nothing
                        dgvList2.CurrentCell = Nothing
                        dgvList3.CurrentCell = Nothing
                        Exit Sub
                    End If
                Next
                _parentForm.Show()                                              ' 前画面を表示
                _parentForm.Enabled = True                                      ' 前画面の使用を可能にする
                _parentForm.Activate()                                          ' 前画面をアクティブにする
                Me.Dispose()                                                    ' 自画面を閉じる
                Exit Sub

            End If


        Catch
            'エラー発生したのでロールバック
            _db.rollbackTran()
        End Try

    End Sub

    '今回売上一覧の値が変更になった時
    Private Sub dgvList3_CellValidated(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList3.CellValidated
        'Try
        '処理状態が登録変更の時のみ処理をする
        Select Case _SelectMode
            Case CommonConst.MODE_ADDNEW  '登録
            Case CommonConst.MODE_EditStatus  '変更
            Case CommonConst.MODE_CancelStatus  '取消
                Exit Sub
            Case CommonConst.MODE_InquiryStatus  '照会
                Exit Sub
        End Select
        Select Case e.ColumnIndex
            Case L3_COLNO_URISURYO, L3_COLNO_MEKIRISURYO  '07:売上数量 08:目切数量
                '①委託明細欄の「今回売上」を再計算
                '「明細」が一致する行の数量を再計算する
                Dim decUriSuryo As Decimal = 0  '売上数量累積用
                Dim decMeSuryo As Decimal = 0   '目切数量累積用
                For i As Integer = 0 To dgvList3.RowCount - 1
                    If dgvList3.Rows(i).Cells(L3_COLNO_MEISAI).Value = dgvList3.Rows(e.RowIndex).Cells(L3_COLNO_MEISAI).Value Then
                        decUriSuryo += Decimal.Parse(_db.rmNullDouble(dgvList3.Rows(i).Cells(L3_COLNO_URISURYO).Value))
                        decMeSuryo += Decimal.Parse(_db.rmNullDouble(dgvList3.Rows(i).Cells(L3_COLNO_MEKIRISURYO).Value))
                    End If
                Next
                For i As Integer = 0 To dgvList1.RowCount - 1
                    '売上数量
                    If dgvList1.Rows(i).Cells(L1_COLNO_NO).Value = dgvList3.Rows(e.RowIndex).Cells(L3_COLNO_MEISAI).Value Then
                        dgvList1.Rows(i).Cells(L1_COLNO_KONKAIURI).Value = decUriSuryo.ToString("N2")
                        dgvList1.Rows(i).Cells(L1_COLNO_KONKAIMEKIRI).Value = decMeSuryo.ToString("N2")
                        dgvList1.Rows(i).Cells(L1_COLNO_ITAKUZAN).Value = (_db.rmNullDouble(dgvList1.Rows(i).Cells(L1_COLNO_ITAKUSURYO).Value) - decMeSuryo - decUriSuryo - _db.rmNullDouble(dgvList1.Rows(i).Cells(L1_COLNO_URISURYO).Value) - _db.rmNullDouble(dgvList1.Rows(i).Cells(L1_COLNO_MEKIRISURYO).Value)).ToString("N2")
                    End If
                Next
                '自分自身の表示フォーマット編集
                dgvList3.Rows(e.RowIndex).Cells(L3_COLNO_URISURYO).Value = _db.rmNullDouble(dgvList3.Rows(e.RowIndex).Cells(L3_COLNO_URISURYO).Value).ToString("N2")
                dgvList3.Rows(e.RowIndex).Cells(L3_COLNO_MEKIRISURYO).Value = _db.rmNullDouble(dgvList3.Rows(e.RowIndex).Cells(L3_COLNO_MEKIRISURYO).Value).ToString("N2")
        End Select

        '売上金額計算処理
        '数量チェック
        Dim decSuryo As Decimal
        If Decimal.TryParse(_db.rmNullDouble(dgvList3.Rows(e.RowIndex).Cells(L3_COLNO_URISURYO).Value), decSuryo) Then
        Else
            Exit Sub
        End If
        '単価チェック
        Dim decTanka As Decimal
        If Decimal.TryParse(_db.rmNullDouble(dgvList3.Rows(e.RowIndex).Cells(L3_COLNO_URITANKA).Value), decTanka) Then
        Else
            Exit Sub
        End If
        Select Case e.ColumnIndex
            Case L3_COLNO_URISURYO, L3_COLNO_URITANKA  '07:売上数量 09:売単価
                Dim decGoukei As Decimal = 0  '売上金額計算用
                decGoukei = decSuryo * decTanka
                Select Case _ZeiHasu
                    Case "1"    '切り捨て
                        decGoukei = Math.Floor(decGoukei)
                    Case "2"    '四捨五入
                        decGoukei = Math.Round(decGoukei)
                    Case "3"    '切り上げ
                        decGoukei = Math.Ceiling(decGoukei)
                End Select
                dgvList3.Rows(e.RowIndex).Cells(L3_COLNO_URIMONEY).Value = decGoukei.ToString("N0")
                '自分自身の表示フォーマット編集
                dgvList3.Rows(e.RowIndex).Cells(L3_COLNO_URISURYO).Value = _db.rmNullDouble(dgvList3.Rows(e.RowIndex).Cells(L3_COLNO_URISURYO).Value).ToString("N2")
                dgvList3.Rows(e.RowIndex).Cells(L3_COLNO_URITANKA).Value = _db.rmNullDouble(dgvList3.Rows(e.RowIndex).Cells(L3_COLNO_URITANKA).Value).ToString("N2")

        End Select
        '税抜額、課税対象額、消費税額を計算
        Dim decMoney As Decimal
        '税区分チェック
        If dgvList3.Rows(e.RowIndex).Cells(L3_COLNO_ZEIKBNCD).Value = "" Then
            Exit Sub
        End If
        dgvList3.Rows(e.RowIndex).Cells(L3_COLNO_URIMONEY).Value = _db.rmNullLong(dgvList3.Rows(e.RowIndex).Cells(L3_COLNO_URIMONEY).Value).ToString("N0")
        Select Case dgvList3.Rows(e.RowIndex).Cells(L3_COLNO_ZEIKBNCD).Value
            Case CommonConst.TAXKBN_External    '1:外税
                dgvList3.Rows(e.RowIndex).Cells(L3_COLNO_TAX_EXCLUSION).Value = dgvList3.Rows(e.RowIndex).Cells(L3_COLNO_URIMONEY).Value
                dgvList3.Rows(e.RowIndex).Cells(L3_COLNO_TAX_TAXABLE).Value = dgvList3.Rows(e.RowIndex).Cells(L3_COLNO_URIMONEY).Value
                decMoney = dgvList3.Rows(e.RowIndex).Cells(L3_COLNO_URIMONEY).Value * dgvList3.Rows(e.RowIndex).Cells(L3_COLNO_TAX_RATE).Value
                '端数処理
                Select Case _ZeiHasu
                    Case "1"    '切り捨て
                        decMoney = Math.Floor(decMoney)
                    Case "2"    '四捨五入
                        decMoney = Math.Round(decMoney)
                    Case "3"    '切り上げ
                        decMoney = Math.Ceiling(decMoney)
                End Select
                dgvList3.Rows(e.RowIndex).Cells(L3_COLNO_TAX_AMOUNT).Value = decMoney
            Case CommonConst.TAXKBN_Included    '2:内税
                '税区分=2:内税→「売上金額」÷（100+「税率」×100）×100
                decMoney = dgvList3.Rows(e.RowIndex).Cells(L3_COLNO_URIMONEY).Value / (100 + dgvList3.Rows(e.RowIndex).Cells(L3_COLNO_TAX_RATE).Value * 100) * 100
                '端数処理
                Select Case _ZeiHasu
                    Case "1"    '切り捨て
                        decMoney = Math.Floor(decMoney)
                    Case "2"    '四捨五入
                        decMoney = Math.Round(decMoney)
                    Case "3"    '切り上げ
                        decMoney = Math.Ceiling(decMoney)
                End Select
                dgvList3.Rows(e.RowIndex).Cells(L3_COLNO_TAX_EXCLUSION).Value = decMoney
                dgvList3.Rows(e.RowIndex).Cells(L3_COLNO_TAX_TAXABLE).Value = decMoney
                dgvList3.Rows(e.RowIndex).Cells(L3_COLNO_TAX_AMOUNT).Value = dgvList3.Rows(e.RowIndex).Cells(L3_COLNO_URIMONEY).Value - decMoney
            Case CommonConst.TAXKBN_Exempt    '3:非課税
                dgvList3.Rows(e.RowIndex).Cells(L3_COLNO_TAX_EXCLUSION).Value = dgvList3.Rows(e.RowIndex).Cells(L3_COLNO_URIMONEY).Value
                dgvList3.Rows(e.RowIndex).Cells(L3_COLNO_TAX_TAXABLE).Value = 0
                dgvList3.Rows(e.RowIndex).Cells(L3_COLNO_TAX_AMOUNT).Value = 0
        End Select

        '合計計算
        Dim decUriKingaku As Decimal = 0     '売上金額
        Dim decTAX_EXCLUSION As Decimal = 0     '税抜額
        Dim decTAX_TAXABLE As Decimal = 0     '課税対象額
        Dim decTAX_AMOUNT As Decimal = 0     '消費税額
        For i As Integer = 0 To dgvList3.RowCount - 1
            decUriKingaku = decUriKingaku + _db.rmNullInt(dgvList3.Rows(i).Cells(L3_COLNO_URIMONEY).Value)
            decTAX_EXCLUSION = decTAX_EXCLUSION + _db.rmNullInt(dgvList3.Rows(i).Cells(L3_COLNO_TAX_EXCLUSION).Value)
            decTAX_TAXABLE = decTAX_TAXABLE + _db.rmNullInt(dgvList3.Rows(i).Cells(L3_COLNO_TAX_TAXABLE).Value)
            decTAX_AMOUNT = decTAX_AMOUNT + _db.rmNullInt(dgvList3.Rows(i).Cells(L3_COLNO_TAX_AMOUNT).Value)
        Next i
        'lblTotal.Text = decUriKingaku.ToString("N0")
        lblZeinukiSum.Text = decTAX_EXCLUSION.ToString("N0")
        lblKazeiSum.Text = decTAX_TAXABLE.ToString("N0")
        lblTaxSum.Text = decTAX_AMOUNT.ToString("N0")
        lblMoneySum.Text = (decTAX_EXCLUSION + decTAX_AMOUNT).ToString("N0")

        'Catch ex As Exception

        'End Try

    End Sub
    'データ新規登録処理
    Private Sub DataAddNew()
        Try
            Dim Sql As String = ""
            '委託基本（T15_ITAKHD）
            Sql = ""
            Sql = Sql & N & "UPDATE T15_ITAKHD  "
            Sql = Sql & N & " SET 売上登録回数 = 売上登録回数 + 1  "
            Sql = Sql & N & "  , 更新者 = '" & frmC01F10_Login.loginValue.TantoCD & "' "         '更新者
            Sql = Sql & N & "  , 更新日 = current_timestamp "                                    '更新日
            Sql = Sql & N & "  Where 会社コード =  '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "       '会社コード
            Sql = Sql & N & "   and 委託伝番 = '" & Me.lblDenpyoNo.Text & "' "                 '注文伝番
            _db.executeDB(Sql)

            'レコード追加
            '同一売上伝番の枝番の最大値＋１を取得
            Dim reccnt As Integer = 0
            Dim ds As DataSet
            Sql = "SELECT  "
            Sql = Sql & "   MAX(売上伝番枝番) AS 枝番 "
            Sql = Sql & " FROM T20_URIGHD  "
            Sql = Sql & " Where 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and 売上伝番 = '" & _DenpyoNo & "'"
            ds = _db.selectDB(Sql, RS, reccnt)
            If reccnt = 0 Then
                Exit Sub
            End If
            Dim intEdaNo As Integer = 0 '売上伝番枝番
            intEdaNo = _db.rmNullInt(ds.Tables(RS).Rows(0)("枝番")) + 1

            Sql = ""
            Sql = Sql & N & "INSERT INTO T20_URIGHD ( "
            Sql = Sql & N & "    会社コード "
            Sql = Sql & N & "  , 売上伝番 "
            Sql = Sql & N & "  , 売上伝番枝番 "
            Sql = Sql & N & "  , 出荷先分類 "
            Sql = Sql & N & "  , 売上区分 "
            Sql = Sql & N & "  , 売上入力日 "
            Sql = Sql & N & "  , 出荷日 "
            Sql = Sql & N & "  , 着日 "
            Sql = Sql & N & "  , 売上日 "
            Sql = Sql & N & "  , 出荷先コード "
            Sql = Sql & N & "  , 出荷先名 "
            Sql = Sql & N & "  , 電話番号 "
            Sql = Sql & N & "  , 電話番号検索用 "
            Sql = Sql & N & "  , 請求先コード "
            Sql = Sql & N & "  , 請求先名 "
            Sql = Sql & N & "  , コメント "
            Sql = Sql & N & "  , 売上金額計 "
            Sql = Sql & N & "  , 税抜額計 "
            Sql = Sql & N & "  , 課税対象額計 "
            Sql = Sql & N & "  , 消費税計 "
            Sql = Sql & N & "  , 税込額計 "
            Sql = Sql & N & "  , 税率 "
            Sql = Sql & N & "  , 税計算区分 "
            Sql = Sql & N & "  , 取消区分 "
            Sql = Sql & N & "  , 更新者 "
            Sql = Sql & N & "  , 更新日 "
            Sql = Sql & N & ") VALUES ( "
            Sql = Sql & N & "    '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "       '会社コード
            Sql = Sql & N & "  , '" & Me.lblDenpyoNo.Text & "' "                 '売上伝番
            Sql = Sql & N & "  , " & intEdaNo                                    '売上伝番枝番
            Sql = Sql & N & "  , '1' "                                           '出荷先分類
            Sql = Sql & N & "  , '" & CommonConst.URI_KBN_ITAKU & "' "           '売上区分
            Sql = Sql & N & "  , current_date "                                  '売上入力日
            Sql = Sql & N & "  , '" & Me.lblShukkaDt.Text & "' "                 '出荷日
            Sql = Sql & N & "  , '" & Me.lblChakuDt.Text & "' "                  '着日
            Sql = Sql & N & "  , '" & _db.rmNullDate(Me.dtpUriageDt.Value, "yyyy/MM/dd") & "' "         '売上日
            Sql = Sql & N & "  , '" & Me.lblSyukkaCD.Text & "' "                 '出荷先コード
            Sql = Sql & N & "  , '" & Me.lblSyukkaNM.Text & "' "                 '出荷先名
            Sql = Sql & N & "  , Null "                                          '電話番号
            Sql = Sql & N & "  , Null "                                          '電話番号検索用
            Sql = Sql & N & "  , '" & Me.lblSeikyuCD.Text & "' "                 '請求先コード
            Sql = Sql & N & "  , '" & Me.lblSeikyuNM.Text & "' "                 '請求先名
            Sql = Sql & N & "  , '" & Me.txtComment.Text & "' "                  'コメント
            Sql = Sql & N & "  , " & Decimal.Parse(Me.lblMoneySum.Text).ToString            '売上金額計
            Sql = Sql & N & "  , " & Decimal.Parse(Me.lblZeinukiSum.Text).ToString          '税抜額計
            Sql = Sql & N & "  , " & Decimal.Parse(Me.lblKazeiSum.Text).ToString            '課税対象額計
            Sql = Sql & N & "  , " & Decimal.Parse(Me.lblTaxSum.Text).ToString          '消費税計
            Sql = Sql & N & "  , " & Decimal.Parse(Me.lblMoneySum.Text).ToString        '税込額計
            Sql = Sql & N & "  , " & Decimal.Parse(_Tax).ToString        '税率
            Sql = Sql & N & "  , '" & _ZeiHasu & "' "         '税計算区分
            Sql = Sql & N & "  , '0' "              '取消区分
            Sql = Sql & N & "  , '" & frmC01F10_Login.loginValue.TantoCD & "' "         '更新者
            Sql = Sql & N & "  , current_timestamp "                                    '更新日
            Sql = Sql & N & ") "
            _db.executeDB(Sql)

            '売上明細（T21_URIGDT）
            For jj As Integer = 0 To Me.dgvList3.RowCount - 1

                Sql = ""
                Sql = Sql & N & "INSERT INTO T21_URIGDT ( "
                Sql = Sql & N & "    会社コード "
                Sql = Sql & N & "  , 売上伝番 "
                Sql = Sql & N & "  , 売上伝番枝番 "
                Sql = Sql & N & "  , 行番 "
                Sql = Sql & N & "  , 注文行番 "
                Sql = Sql & N & "  , 商品コード "
                Sql = Sql & N & "  , 商品名 "
                Sql = Sql & N & "  , 荷姿形状 "
                Sql = Sql & N & "  , 課税区分 "
                Sql = Sql & N & "  , 入数 "
                Sql = Sql & N & "  , 個数 "
                Sql = Sql & N & "  , 単位 "
                Sql = Sql & N & "  , 売上数量 "
                Sql = Sql & N & "  , 目切数量 "
                Sql = Sql & N & "  , 売上単価 "
                Sql = Sql & N & "  , 売上金額 "
                Sql = Sql & N & "  , 売上明細備考 "
                Sql = Sql & N & "  , 税抜額 "
                Sql = Sql & N & "  , 課税対象額 "
                Sql = Sql & N & "  , 消費税 "
                Sql = Sql & N & "  , 税込額 "
                Sql = Sql & N & "  , 入金有無 "
                Sql = Sql & N & "  , 入金伝番 "

                Sql = Sql & N & "  , 更新者 "
                Sql = Sql & N & "  , 更新日 "
                Sql = Sql & N & ") VALUES ( "
                Sql = Sql & N & "    '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "       '会社コード
                Sql = Sql & N & "  , '" & Me.lblDenpyoNo.Text & "' "                 '売上伝番
                Sql = Sql & N & "  , " & intEdaNo                                    '売上伝番枝番
                Sql = Sql & N & "  , " & Me.dgvList3.Rows(jj).Cells(L3_COLNO_NO).Value         '行番
                Sql = Sql & N & "  , " & Me.dgvList3.Rows(jj).Cells(L3_COLNO_MEISAI).Value         '注文行番
                Sql = Sql & N & "  , '" & Me.dgvList3.Rows(jj).Cells(L3_COLNO_SHOHINCD).Value & "' "     '商品コード
                Sql = Sql & N & "  , '" & Me.dgvList3.Rows(jj).Cells(L3_COLNO_SHOHINNM).Value & "' "     '商品名
                Sql = Sql & N & "  , '" & Me.dgvList3.Rows(jj).Cells(L3_COLNO_NISUGATA).Value & "' "     '荷姿形状
                Sql = Sql & N & "  , '" & Me.dgvList3.Rows(jj).Cells(L3_COLNO_ZEIKBNCD).Value & "' "   '課税区分
                Sql = Sql & N & "  , " & Decimal.Parse(Me.dgvList3.Rows(jj).Cells(L3_COLNO_IRISUU).Value).ToString             '入数
                Sql = Sql & N & "  , " & Decimal.Parse(Me.dgvList3.Rows(jj).Cells(L3_COLNO_KOSUU).Value).ToString              '個数
                Sql = Sql & N & "  , '" & Me.dgvList3.Rows(jj).Cells(L3_COLNO_TANI).Value & "' "      '単位
                Sql = Sql & N & "  , " & Decimal.Parse(Me.dgvList3.Rows(jj).Cells(L3_COLNO_URISURYO).Value).ToString           '売上数量
                Sql = Sql & N & "  , " & Decimal.Parse(Me.dgvList3.Rows(jj).Cells(L3_COLNO_MEKIRISURYO).Value).ToString        '目切数量
                Sql = Sql & N & "  , " & Decimal.Parse(Me.dgvList3.Rows(jj).Cells(L3_COLNO_URITANKA).Value).ToString           '売上単価
                Sql = Sql & N & "  , " & Decimal.Parse(Me.dgvList3.Rows(jj).Cells(L3_COLNO_URIMONEY).Value).ToString         '売上金額
                Sql = Sql & N & "  , '" & Me.dgvList3.Rows(jj).Cells(L3_COLNO_URIBIKO).Value & "' "      '売上明細備考
                Sql = Sql & N & "  , " & Decimal.Parse(Me.dgvList3.Rows(jj).Cells(L3_COLNO_TAX_EXCLUSION).Value).ToString         '税抜額
                Sql = Sql & N & "  , " & Decimal.Parse(Me.dgvList3.Rows(jj).Cells(L3_COLNO_TAX_TAXABLE).Value).ToString         '課税対象額
                Sql = Sql & N & "  , " & Decimal.Parse(Me.dgvList3.Rows(jj).Cells(L3_COLNO_TAX_AMOUNT).Value).ToString         '消費税
                Sql = Sql & N & "  , " & (Decimal.Parse(Me.dgvList3.Rows(jj).Cells(L3_COLNO_TAX_EXCLUSION).Value) + Decimal.Parse(Me.dgvList3.Rows(jj).Cells(L3_COLNO_TAX_AMOUNT).Value)).ToString           '税込額
                Sql = Sql & N & "  , '0' "                                             '入金有無
                Sql = Sql & N & "  , Null "                                             '入金伝番

                Sql = Sql & N & "  , '" & frmC01F10_Login.loginValue.TantoCD & "' "         '更新者
                Sql = Sql & N & "  , current_timestamp "                                    '更新日
                Sql = Sql & N & ") "
                _db.executeDB(Sql)
            Next

            '委託明細（T16_ITAKDT）
            For ii As Integer = 0 To Me.dgvList1.RowCount - 1
                Sql = ""
                Sql = Sql & N & "UPDATE T16_ITAKDT  "
                Sql = Sql & N & " SET 売上数量計 = " & (Decimal.Parse(Me.dgvList1.Rows(ii).Cells(L1_COLNO_URISURYO).Value) + Decimal.Parse(Me.dgvList1.Rows(ii).Cells(L1_COLNO_KONKAIURI).Value)).ToString
                Sql = Sql & N & "   , 目切数量計 = " & (Decimal.Parse(Me.dgvList1.Rows(ii).Cells(L1_COLNO_MEKIRISURYO).Value) + Decimal.Parse(Me.dgvList1.Rows(ii).Cells(L1_COLNO_KONKAIMEKIRI).Value)).ToString
                Dim decZanSu As Decimal = 0
                decZanSu = Decimal.Parse(Me.dgvList1.Rows(ii).Cells(L1_COLNO_ITAKUSURYO).Value)
                decZanSu = decZanSu - (Decimal.Parse(Me.dgvList1.Rows(ii).Cells(L1_COLNO_URISURYO).Value) + Decimal.Parse(Me.dgvList1.Rows(ii).Cells(L1_COLNO_KONKAIURI).Value))
                decZanSu = decZanSu - (Decimal.Parse(Me.dgvList1.Rows(ii).Cells(L1_COLNO_MEKIRISURYO).Value) + Decimal.Parse(Me.dgvList1.Rows(ii).Cells(L1_COLNO_KONKAIMEKIRI).Value))
                Sql = Sql & N & "   , 委託残数 = " & decZanSu.ToString
                Dim decMoney As Decimal = 0
                decMoney = decZanSu * Decimal.Parse(Me.dgvList1.Rows(ii).Cells(L1_COLNO_KARITANKA).Value)
                '端数処理
                Select Case _ZeiHasu
                    Case "1"    '切り捨て
                        decMoney = Math.Floor(decMoney)
                    Case "2"    '四捨五入
                        decMoney = Math.Round(decMoney)
                    Case "3"    '切り上げ
                        decMoney = Math.Ceiling(decMoney)
                End Select
                Sql = Sql & N & "   , 委託残金額 = " & decMoney.ToString

                Sql = Sql & N & "  , 更新者 = '" & frmC01F10_Login.loginValue.TantoCD & "' "         '更新者
                Sql = Sql & N & "  , 更新日 = current_timestamp "                                    '更新日
                Sql = Sql & N & "  Where 会社コード =  '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "       '会社コード
                Sql = Sql & N & "   and 委託伝番 = '" & Me.lblDenpyoNo.Text & "' "                 '委託伝番
                Sql = Sql & N & "   and 行番 = '" & Me.dgvList1.Rows(ii).Cells(L1_COLNO_NO).Value & "' "                 '行番
                _db.executeDB(Sql)

            Next
            'ログ出力
            '操作履歴ログ作成
            _comLogc.Insert_L01_ProcLog(frmC01F10_Login.loginValue.BumonCD, DBNull.Value, _SelectID, CommonConst.PROGRAM_UPDATE, "9",
                                                        lblDenpyoNo.Text & "-" & intEdaNo, CommonConst.MODE_EditStatus_NAME, DBNull.Value, DBNull.Value, DBNull.Value,
                                                        DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _userId)

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub
    'データ変更登録処理
    Private Sub DataEdit()
        Try
            Dim Sql As String = ""
            '委託基本（T15_ITAKHD）
            Sql = ""
            Sql = Sql & N & "UPDATE T15_ITAKHD  "
            Sql = Sql & N & " SET 売上変更回数 = 売上変更回数 + 1  "
            Sql = Sql & N & "  , 更新者 = '" & frmC01F10_Login.loginValue.TantoCD & "' "         '更新者
            Sql = Sql & N & "  , 更新日 = current_timestamp "                                    '更新日
            Sql = Sql & N & "  Where 会社コード =  '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "       '会社コード
            Sql = Sql & N & "   and 委託伝番 = '" & Me.lblDenpyoNo.Text & "' "                 '注文伝番
            _db.executeDB(Sql)

            '売上基本（T20_URIGHD）
            '②	売上基本(T20)	DELETE & INSERT（1件）
            Sql = ""
            Sql = ""
            Sql = Sql & N & "Delete From T20_URIGHD  "
            Sql = Sql & N & "  Where 会社コード =  '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "       '会社コード
            Sql = Sql & N & "   and 売上伝番 = '" & Me.lblDenpyoNo.Text & "' "                 '注文伝番
            Sql = Sql & N & "   and 売上伝番枝番 = " & _DenpyoNoEda                            '売上伝番枝番
            _db.executeDB(Sql)
            '売上明細（T21_URIGDT）	DELETE & INSERT（n件）
            Sql = ""
            Sql = Sql & N & "Delete From T21_URIGDT  "
            Sql = Sql & N & "  Where 会社コード =  '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "       '会社コード
            Sql = Sql & N & "   and 売上伝番 = '" & Me.lblDenpyoNo.Text & "' "                 '注文伝番
            Sql = Sql & N & "   and 売上伝番枝番 = " & _DenpyoNoEda                            '売上伝番枝番
            _db.executeDB(Sql)

            'レコード追加
            '同一売上伝番の枝番の最大値＋１を取得（？）変更の場合は元の枝番使う気がする
            'Dim reccnt As Integer = 0
            'Dim ds As DataSet
            'Sql = "SELECT  "
            'Sql = Sql & "   MAX(売上伝番枝番) AS 枝番 "
            'Sql = Sql & " FROM T20_URIGHD  "
            'Sql = Sql & " Where 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and 売上伝番 = '" & _DenpyoNo & "'"
            'ds = _db.selectDB(Sql, RS, reccnt)
            'If reccnt = 0 Then
            '    Exit Sub
            'End If
            Dim intEdaNo As Integer = 0 '売上伝番枝番
            'intEdaNo = _db.rmNullInt(ds.Tables(RS).Rows(0)("枝番")) + 1
            intEdaNo = _DenpyoNoEda

            Sql = ""
            Sql = Sql & N & "INSERT INTO T20_URIGHD ( "
            Sql = Sql & N & "    会社コード "
            Sql = Sql & N & "  , 売上伝番 "
            Sql = Sql & N & "  , 売上伝番枝番 "
            Sql = Sql & N & "  , 出荷先分類 "
            Sql = Sql & N & "  , 売上区分 "
            Sql = Sql & N & "  , 売上入力日 "
            Sql = Sql & N & "  , 出荷日 "
            Sql = Sql & N & "  , 着日 "
            Sql = Sql & N & "  , 売上日 "
            Sql = Sql & N & "  , 出荷先コード "
            Sql = Sql & N & "  , 出荷先名 "
            Sql = Sql & N & "  , 電話番号 "
            Sql = Sql & N & "  , 電話番号検索用 "
            Sql = Sql & N & "  , 請求先コード "
            Sql = Sql & N & "  , 請求先名 "
            Sql = Sql & N & "  , コメント "
            Sql = Sql & N & "  , 売上金額計 "
            Sql = Sql & N & "  , 税抜額計 "
            Sql = Sql & N & "  , 課税対象額計 "
            Sql = Sql & N & "  , 消費税計 "
            Sql = Sql & N & "  , 税込額計 "
            Sql = Sql & N & "  , 税率 "
            Sql = Sql & N & "  , 税計算区分 "
            Sql = Sql & N & "  , 取消区分 "
            Sql = Sql & N & "  , 更新者 "
            Sql = Sql & N & "  , 更新日 "
            Sql = Sql & N & ") VALUES ( "
            Sql = Sql & N & "    '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "       '会社コード
            Sql = Sql & N & "  , '" & Me.lblDenpyoNo.Text & "' "                 '売上伝番
            Sql = Sql & N & "  , " & intEdaNo                                    '売上伝番枝番
            Sql = Sql & N & "  , '1' "                                           '出荷先分類
            Sql = Sql & N & "  , '" & CommonConst.URI_KBN_ITAKU & "' "           '売上区分
            Sql = Sql & N & "  , current_date "                                  '売上入力日
            Sql = Sql & N & "  , '" & Me.lblShukkaDt.Text & "' "                 '出荷日
            Sql = Sql & N & "  , '" & Me.lblChakuDt.Text & "' "                  '着日
            Sql = Sql & N & "  , '" & _db.rmNullDate(Me.dtpUriageDt.Value, "yyyy/MM/dd") & "' "         '売上日
            Sql = Sql & N & "  , '" & Me.lblSyukkaCD.Text & "' "                 '出荷先コード
            Sql = Sql & N & "  , '" & Me.lblSyukkaNM.Text & "' "                 '出荷先名
            Sql = Sql & N & "  , Null "                                          '電話番号
            Sql = Sql & N & "  , Null "                                          '電話番号検索用
            Sql = Sql & N & "  , '" & Me.lblSeikyuCD.Text & "' "                 '請求先コード
            Sql = Sql & N & "  , '" & Me.lblSeikyuNM.Text & "' "                 '請求先名
            Sql = Sql & N & "  , '" & Me.txtComment.Text & "' "                  'コメント
            Sql = Sql & N & "  , " & Decimal.Parse(Me.lblMoneySum.Text).ToString            '売上金額計
            Sql = Sql & N & "  , " & Decimal.Parse(Me.lblZeinukiSum.Text).ToString          '税抜額計
            Sql = Sql & N & "  , " & Decimal.Parse(Me.lblKazeiSum.Text).ToString            '課税対象額計
            Sql = Sql & N & "  , " & Decimal.Parse(Me.lblTaxSum.Text).ToString          '消費税計
            Sql = Sql & N & "  , " & Decimal.Parse(Me.lblMoneySum.Text).ToString        '税込額計
            Sql = Sql & N & "  , " & Decimal.Parse(_Tax).ToString        '税率
            Sql = Sql & N & "  , '" & _ZeiHasu & "' "         '税計算区分
            Sql = Sql & N & "  , '0' "              '取消区分
            Sql = Sql & N & "  , '" & frmC01F10_Login.loginValue.TantoCD & "' "         '更新者
            Sql = Sql & N & "  , current_timestamp "                                    '更新日
            Sql = Sql & N & ") "
            _db.executeDB(Sql)

            '売上明細（T21_URIGDT）
            For jj As Integer = 0 To Me.dgvList3.RowCount - 1

                Sql = ""
                Sql = Sql & N & "INSERT INTO T21_URIGDT ( "
                Sql = Sql & N & "    会社コード "
                Sql = Sql & N & "  , 売上伝番 "
                Sql = Sql & N & "  , 売上伝番枝番 "
                Sql = Sql & N & "  , 行番 "
                Sql = Sql & N & "  , 注文行番 "
                Sql = Sql & N & "  , 商品コード "
                Sql = Sql & N & "  , 商品名 "
                Sql = Sql & N & "  , 荷姿形状 "
                Sql = Sql & N & "  , 課税区分 "
                Sql = Sql & N & "  , 入数 "
                Sql = Sql & N & "  , 個数 "
                Sql = Sql & N & "  , 単位 "
                Sql = Sql & N & "  , 売上数量 "
                Sql = Sql & N & "  , 目切数量 "
                Sql = Sql & N & "  , 売上単価 "
                Sql = Sql & N & "  , 売上金額 "
                Sql = Sql & N & "  , 売上明細備考 "
                Sql = Sql & N & "  , 税抜額 "
                Sql = Sql & N & "  , 課税対象額 "
                Sql = Sql & N & "  , 消費税 "
                Sql = Sql & N & "  , 税込額 "
                Sql = Sql & N & "  , 入金有無 "
                Sql = Sql & N & "  , 入金伝番 "

                Sql = Sql & N & "  , 更新者 "
                Sql = Sql & N & "  , 更新日 "
                Sql = Sql & N & ") VALUES ( "
                Sql = Sql & N & "    '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "       '会社コード
                Sql = Sql & N & "  , '" & Me.lblDenpyoNo.Text & "' "                 '売上伝番
                Sql = Sql & N & "  , " & intEdaNo                                    '売上伝番枝番
                Sql = Sql & N & "  , " & Me.dgvList3.Rows(jj).Cells(L3_COLNO_NO).Value         '行番
                Sql = Sql & N & "  , " & Me.dgvList3.Rows(jj).Cells(L3_COLNO_MEISAI).Value         '注文行番
                Sql = Sql & N & "  , '" & Me.dgvList3.Rows(jj).Cells(L3_COLNO_SHOHINCD).Value & "' "     '商品コード
                Sql = Sql & N & "  , '" & Me.dgvList3.Rows(jj).Cells(L3_COLNO_SHOHINNM).Value & "' "     '商品名
                Sql = Sql & N & "  , '" & Me.dgvList3.Rows(jj).Cells(L3_COLNO_NISUGATA).Value & "' "     '荷姿形状
                Sql = Sql & N & "  , '" & Me.dgvList3.Rows(jj).Cells(L3_COLNO_ZEIKBNCD).Value & "' "   '課税区分
                Sql = Sql & N & "  , " & Decimal.Parse(Me.dgvList3.Rows(jj).Cells(L3_COLNO_IRISUU).Value).ToString             '入数
                Sql = Sql & N & "  , " & Decimal.Parse(Me.dgvList3.Rows(jj).Cells(L3_COLNO_KOSUU).Value).ToString              '個数
                Sql = Sql & N & "  , '" & Me.dgvList3.Rows(jj).Cells(L3_COLNO_TANI).Value & "' "      '単位
                Sql = Sql & N & "  , " & Decimal.Parse(Me.dgvList3.Rows(jj).Cells(L3_COLNO_URISURYO).Value).ToString           '売上数量
                Sql = Sql & N & "  , " & Decimal.Parse(Me.dgvList3.Rows(jj).Cells(L3_COLNO_MEKIRISURYO).Value).ToString        '目切数量
                Sql = Sql & N & "  , " & Decimal.Parse(Me.dgvList3.Rows(jj).Cells(L3_COLNO_URITANKA).Value).ToString           '売上単価
                Sql = Sql & N & "  , " & Decimal.Parse(Me.dgvList3.Rows(jj).Cells(L3_COLNO_URIMONEY).Value).ToString         '売上金額
                Sql = Sql & N & "  , '" & Me.dgvList3.Rows(jj).Cells(L3_COLNO_URIBIKO).Value & "' "      '売上明細備考
                Sql = Sql & N & "  , " & Decimal.Parse(Me.dgvList3.Rows(jj).Cells(L3_COLNO_TAX_EXCLUSION).Value).ToString         '税抜額
                Sql = Sql & N & "  , " & Decimal.Parse(Me.dgvList3.Rows(jj).Cells(L3_COLNO_TAX_TAXABLE).Value).ToString         '課税対象額
                Sql = Sql & N & "  , " & Decimal.Parse(Me.dgvList3.Rows(jj).Cells(L3_COLNO_TAX_AMOUNT).Value).ToString         '消費税
                Sql = Sql & N & "  , " & (Decimal.Parse(Me.dgvList3.Rows(jj).Cells(L3_COLNO_TAX_EXCLUSION).Value) + Decimal.Parse(Me.dgvList3.Rows(jj).Cells(L3_COLNO_TAX_AMOUNT).Value)).ToString           '税込額
                Sql = Sql & N & "  , '0' "                                             '入金有無
                Sql = Sql & N & "  , Null "                                             '入金伝番

                Sql = Sql & N & "  , '" & frmC01F10_Login.loginValue.TantoCD & "' "         '更新者
                Sql = Sql & N & "  , current_timestamp "                                    '更新日
                Sql = Sql & N & ") "
                _db.executeDB(Sql)
            Next

            '委託明細（T16_ITAKDT）
            For ii As Integer = 0 To Me.dgvList1.RowCount - 1
                Sql = ""
                Sql = Sql & N & "UPDATE T16_ITAKDT  "
                Sql = Sql & N & " SET 売上数量計 = " & (Decimal.Parse(Me.dgvList1.Rows(ii).Cells(L1_COLNO_URISURYO).Value) + Decimal.Parse(Me.dgvList1.Rows(ii).Cells(L1_COLNO_KONKAIURI).Value)).ToString
                Sql = Sql & N & "   , 目切数量計 = " & (Decimal.Parse(Me.dgvList1.Rows(ii).Cells(L1_COLNO_MEKIRISURYO).Value) + Decimal.Parse(Me.dgvList1.Rows(ii).Cells(L1_COLNO_KONKAIMEKIRI).Value)).ToString
                Dim decZanSu As Decimal = 0
                decZanSu = Decimal.Parse(Me.dgvList1.Rows(ii).Cells(L1_COLNO_ITAKUSURYO).Value)
                decZanSu = decZanSu - (Decimal.Parse(Me.dgvList1.Rows(ii).Cells(L1_COLNO_URISURYO).Value) + Decimal.Parse(Me.dgvList1.Rows(ii).Cells(L1_COLNO_KONKAIURI).Value))
                decZanSu = decZanSu - (Decimal.Parse(Me.dgvList1.Rows(ii).Cells(L1_COLNO_MEKIRISURYO).Value) + Decimal.Parse(Me.dgvList1.Rows(ii).Cells(L1_COLNO_KONKAIMEKIRI).Value))
                Sql = Sql & N & "   , 委託残数 = " & decZanSu.ToString
                Dim decMoney As Decimal = 0
                decMoney = decZanSu * Decimal.Parse(Me.dgvList1.Rows(ii).Cells(L1_COLNO_KARITANKA).Value)
                '端数処理
                Select Case _ZeiHasu
                    Case "1"    '切り捨て
                        decMoney = Math.Floor(decMoney)
                    Case "2"    '四捨五入
                        decMoney = Math.Round(decMoney)
                    Case "3"    '切り上げ
                        decMoney = Math.Ceiling(decMoney)
                End Select
                Sql = Sql & N & "   , 委託残金額 = " & decMoney.ToString

                Sql = Sql & N & "  , 更新者 = '" & frmC01F10_Login.loginValue.TantoCD & "' "         '更新者
                Sql = Sql & N & "  , 更新日 = current_timestamp "                                    '更新日
                Sql = Sql & N & "  Where 会社コード =  '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "       '会社コード
                Sql = Sql & N & "   and 委託伝番 = '" & Me.lblDenpyoNo.Text & "' "                 '委託伝番
                Sql = Sql & N & "   and 行番 = '" & Me.dgvList1.Rows(ii).Cells(L1_COLNO_NO).Value & "' "                 '行番
                _db.executeDB(Sql)

            Next
            'ログ出力
            '操作履歴ログ作成
            _comLogc.Insert_L01_ProcLog(frmC01F10_Login.loginValue.BumonCD, DBNull.Value, _SelectID, CommonConst.PROGRAM_UPDATE, "9",
                                                        lblDenpyoNo.Text & "-" & intEdaNo, CommonConst.MODE_ADDNEW_NAME, DBNull.Value, DBNull.Value, DBNull.Value,
                                                        DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _userId)

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub
    'データ取消処理
    Private Sub DataCancel()
        Try
            Dim Sql As String = ""
            '委託基本（T15_ITAKHD）
            Sql = ""
            Sql = Sql & N & "UPDATE T15_ITAKHD  "
            Sql = Sql & N & " SET 売上取消回数 = 売上取消回数 + 1  "
            Sql = Sql & N & "  , 更新者 = '" & frmC01F10_Login.loginValue.TantoCD & "' "         '更新者
            Sql = Sql & N & "  , 更新日 = current_timestamp "                                    '更新日
            Sql = Sql & N & "  Where 会社コード =  '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "       '会社コード
            Sql = Sql & N & "   and 委託伝番 = '" & Me.lblDenpyoNo.Text & "' "                 '注文伝番
            _db.executeDB(Sql)

            '売上基本（T20_URIGHD）
            Sql = ""
            Sql = Sql & N & "UPDATE T20_URIGHD  "
            Sql = Sql & N & " SET 取消区分 = '1'  "
            Sql = Sql & N & "  , 更新者 = '" & frmC01F10_Login.loginValue.TantoCD & "' "         '更新者
            Sql = Sql & N & "  , 更新日 = current_timestamp "                                    '更新日
            Sql = Sql & N & "  Where 会社コード =  '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "       '会社コード
            Sql = Sql & N & "   and 売上伝番 = '" & Me.lblDenpyoNo.Text & "' "                 '注文伝番
            Sql = Sql & N & "   and 売上伝番枝番 = " & _DenpyoNoEda                            '売上伝番枝番
            _db.executeDB(Sql)

            '委託明細（T16_ITAKDT）
            For ii As Integer = 0 To Me.dgvList1.RowCount - 1
                Dim decUri As Decimal = 0   '今回取り消される売上数量
                Dim decMe As Decimal = 0   '今回取り消される目切数量
                For jj As Integer = 0 To Me.dgvList3.RowCount - 1
                    If Me.dgvList1.Rows(ii).Cells(L1_COLNO_NO).Value = Me.dgvList3.Rows(jj).Cells(L3_COLNO_MEISAI).Value Then
                        decUri += _db.rmNullDouble(Me.dgvList3.Rows(jj).Cells(L3_COLNO_URISURYO).Value)
                        decMe += _db.rmNullDouble(Me.dgvList3.Rows(jj).Cells(L3_COLNO_MEKIRISURYO).Value)
                    End If
                Next
                Sql = ""
                    Sql = Sql & N & "UPDATE T16_ITAKDT  "
                Sql = Sql & N & " SET 売上数量計 = " & (Decimal.Parse(Me.dgvList1.Rows(ii).Cells(L1_COLNO_URISURYO).Value)) - decUri
                Sql = Sql & N & "   , 目切数量計 = " & (Decimal.Parse(Me.dgvList1.Rows(ii).Cells(L1_COLNO_MEKIRISURYO).Value)) - decMe
                Dim decZanSu As Decimal = 0
                decZanSu = Decimal.Parse(Me.dgvList1.Rows(ii).Cells(L1_COLNO_ITAKUSURYO).Value)
                decZanSu = decZanSu - (Decimal.Parse(Me.dgvList1.Rows(ii).Cells(L1_COLNO_URISURYO).Value) + Decimal.Parse(Me.dgvList1.Rows(ii).Cells(L1_COLNO_KONKAIURI).Value)) - decUri
                decZanSu = decZanSu - (Decimal.Parse(Me.dgvList1.Rows(ii).Cells(L1_COLNO_MEKIRISURYO).Value) + Decimal.Parse(Me.dgvList1.Rows(ii).Cells(L1_COLNO_KONKAIMEKIRI).Value)) - decMe
                Sql = Sql & N & "   , 委託残数 = " & decZanSu.ToString
                Dim decMoney As Decimal = 0
                decMoney = decZanSu * Decimal.Parse(Me.dgvList1.Rows(ii).Cells(L1_COLNO_KARITANKA).Value)
                '端数処理
                Select Case _ZeiHasu
                    Case "1"    '切り捨て
                        decMoney = Math.Floor(decMoney)
                    Case "2"    '四捨五入
                        decMoney = Math.Round(decMoney)
                    Case "3"    '切り上げ
                        decMoney = Math.Ceiling(decMoney)
                End Select
                Sql = Sql & N & "   , 委託残金額 = " & decMoney.ToString

                Sql = Sql & N & "  , 更新者 = '" & frmC01F10_Login.loginValue.TantoCD & "' "         '更新者
                Sql = Sql & N & "  , 更新日 = current_timestamp "                                    '更新日
                Sql = Sql & N & "  Where 会社コード =  '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "       '会社コード
                Sql = Sql & N & "   and 委託伝番 = '" & Me.lblDenpyoNo.Text & "' "                 '委託伝番
                Sql = Sql & N & "   and 行番 = '" & Me.dgvList1.Rows(ii).Cells(L1_COLNO_NO).Value & "' "                 '行番
                _db.executeDB(Sql)

            Next
            'ログ出力
            '操作履歴ログ作成
            _comLogc.Insert_L01_ProcLog(frmC01F10_Login.loginValue.BumonCD, DBNull.Value, _SelectID, CommonConst.PROGRAM_UPDATE, "9",
                                                        lblDenpyoNo.Text & "-" & _DenpyoNoEda, CommonConst.MODE_CancelStatus_NAME, DBNull.Value, DBNull.Value, DBNull.Value,
                                                        DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _userId)


        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub
    '(8) 同一伝番連続登録ボタン押下時
    Private Sub btnRenzoku_Click(sender As Object, e As EventArgs) Handles btnRenzoku.Click
        '確認メッセージを表示する
        Dim piRtn As Integer
        piRtn = _msgHd.dspMSG("confirmInsert")  '登録します。よろしいですか？
        If piRtn = vbNo Then
            Exit Sub
        End If
        Dim strSql As String = ""
        Dim reccnt As Integer = 0
        Dim ds As DataSet
        '排他チェック処理
        Try
            strSql = "SELECT  "
            strSql = strSql & "    c.更新日,c.更新者 "
            strSql = strSql & " FROM T15_ITAKHD c "
            strSql = strSql & " Where c.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and c.委託伝番 = '" & _DenpyoNo & "'"
            ds = _db.selectDB(strSql, RS, reccnt)
            If reccnt = 0 Then
                Exit Sub
            End If
            If _UpdateTime <> DateTime.Parse(ds.Tables(RS).Rows(0)("更新日")) Then
                '登録終了メッセージ
                Dim strMessage As String = ""
                strMessage = "更新者：" & ds.Tables(RS).Rows(0)("更新者") & " 更新日時：" & ds.Tables(RS).Rows(0)("更新日").ToString
                _msgHd.dspMSG("Exclusion", strMessage)
                Exit Sub
            End If

            'データ更新
            'トランザクション開始
            If Not _db.isTransactionOpen Then
                _db.beginTran()
            End If

            'データ登録
            DataAddNew()
            'トランザクション終了
            _db.commitTran()

            '画面の初期表示
            '委託基本の表示
            getItakuData()
            '委託明細の表示
            getList1()
            '売上明細の表示
            getList2()

            getKonkaiUriageDataList()

            dgvList1.CurrentCell = Nothing
            dgvList2.CurrentCell = Nothing
            dgvList3.CurrentCell = Nothing
            '登録終了メッセージ
            _msgHd.dspMSG("UpdateInfo", "伝票番号：" & lblDenpyoNo.Text)


        Catch
            'エラー発生したのでロールバック
            _db.rollbackTran()
        End Try

    End Sub
    'コントロールのキープレスイベント
    Private Sub dtpUriageDt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles dtpUriageDt.KeyPress, txtComment.KeyPress
        UtilMDL.UtilClass.moveNextFocus(Me, e)
    End Sub
    '-------------------------------------------------------------------------------
    '　フォーカス取得イベント
    '-------------------------------------------------------------------------------
    Private Sub ctl_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpUriageDt.GotFocus, txtComment.GotFocus
        'フォーカス取得時、入力パラメタのコントロールを全選択状態とする
        Call UtilClass.selAll(sender)

    End Sub

    Private Sub dgvList2_Sorted(sender As Object, e As EventArgs) Handles dgvList2.Sorted
        '行番号降り直し
        For i As Integer = 0 To dgvList2.RowCount - 1
            dgvList2.Rows(i).Cells(L2_COLNO_NO).Value = i + 1
        Next
    End Sub

    Private Sub dgvList3_Sorted(sender As Object, e As EventArgs) Handles dgvList3.Sorted
        '行番号降り直し
        For i As Integer = 0 To dgvList3.RowCount - 1
            dgvList3.Rows(i).Cells(L3_COLNO_NO).Value = i + 1
        Next
    End Sub
End Class