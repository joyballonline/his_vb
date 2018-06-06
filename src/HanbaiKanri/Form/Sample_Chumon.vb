'===============================================================================
'
'　北日本電線株式会社
'　　（システム名）在庫計画システム
'　　（処理機能名）メニュー画面
'    （フォームID）ZC110M_Menu
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   高木        2010/10/15                 新規              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls

Public Class Sample_Chumon
    Inherits System.Windows.Forms.Form

#Region "宣言"
    ''-------------------------------------------------------------------------------
    ''   構造体定義
    ''-------------------------------------------------------------------------------
    'Private Structure UpdatableType
    '    Public updFlgSyokisettei As Boolean         '初期設定
    '    Public updFlgSetteitisyuusei As Boolean     '希望出来日修正
    '    Public updFlgTDTorikomi As Boolean          '手配済ﾃﾞｰﾀ取込
    '    Public updFlgNDTorikomi As Boolean          '入庫済ﾃﾞｰﾀ取込
    '    Public updFlgSDSyuusei As Boolean           '生産量ﾃﾞｰﾀ修正
    '    Public updFlgSeisanKakutei As Boolean       '生産量確定
    '    Public updFlgHKNyuryoku As Boolean          '品種別計画入力
    '    Public updFlgKKNyuroku As Boolean           '個別計画入力
    '    Public updFlgHJTorikomi As Boolean          '販売実績取込
    '    Public updFlgSyuukeiTenkai As Boolean       '販売計画集計展開
    '    Public updFlgHKSyuusei As Boolean           '販売計画量修正
    '    Public updFlgSKakutei As Boolean            '販売計画確定
    '    Public updFlgZaikoTorikomi As Boolean       '在庫実績取込
    '    Public updFlgSHZTorikomi As Boolean         '生産販売在庫取込
    '    Public updFlgSKSyuusei As Boolean           '生産計画数量修正
    '    Public updFlgKakutei As Boolean             '生産計画確定
    '    Public updFlgTDSakusei As Boolean           '手配ﾃﾞｰﾀ作成
    '    Public updFlgTDSyuusei As Boolean           '手配ﾃﾞｰﾀ修正・出力
    '    Public updFlgTDSousin As Boolean            '手配ﾃﾞｰﾀ作成(生産管理ｼｽﾃﾑ送信用)
    '    Public updFlgFYamadumi As Boolean           '負荷山積データ取込
    '    Public updFlgKKakunin As Boolean            '負荷山積集計結果確認
    '    Public updFlgSTDB As Boolean                '製作手配DB登録
    '    Public updFlgSinki As Boolean               '新規登録
    '    Public updFlgSyuusei As Boolean             '修正・EXCEL出力
    '    Public updFlgSakujo As Boolean              '削除
    '    Public updFlgKExcel As Boolean              '計画対象品一覧表印刷
    '    Public updFlgABC As Boolean                 'ABC分析
    '    Public updFlgHMstMente As Boolean           '品種区分マスタメンテ
    '    Public updFlgHanyoMst As Boolean            '汎用マスタメンテ
    '    Public updFlgSNouryokuMst As Boolean        '生産能力マスタメンテ
    '    Public updFlgGRenkei As Boolean             '外部システム連携
    'End Structure

    '-------------------------------------------------------------------------------
    '   定数定義
    '-------------------------------------------------------------------------------
    Private Const N As String = ControlChars.NewLine            '改行文字
    Private Const RS As String = "RecSet"                       'レコードセットテーブル
    'Public Const NON_EXECUTE As String = "- - -"

    'DataGridView-------------------------------------------------------------------
    'グリッド列№
    'dgvIchiran
    Private Const COLNO_NO = 0                                      '01:No.
    Private Const COLNO_ITEMCD = 1                                  '02:商品CD
    Private Const COLNO_ITEMNM = 2                                  '03:商品名
    Private Const COLNO_NISUGATA = 3                                '04:荷姿・形状
    Private Const COLNO_ZEIKBN = 4                                  '05:税
    Private Const COLNO_IRISUU = 5                                  '06:入数
    Private Const COLNO_KOSUU = 6                                   '07:個数
    Private Const COLNO_SURYOU = 7                                  '08:数量
    Private Const COLNO_TANNI = 8                                   '09:単位
    Private Const COLNO_URITANKA = 9                                '10:売上単価
    Private Const COLNO_URIKINGAKU = 10                             '11:売上金額
    Private Const COLNO_MEISAIBIKOU = 11                            '12:明細備考
    Private Const COLNO_KONPOU = 12                                 '13:梱包

    'グリッド列名
    'dgvIchiran
    Private Const CCOL_NO As String = "cnNo"                        '01:No.
    Private Const CCOL_ITEMCD As String = "cnItemCd"                '02:商品CD
    Private Const CCOL_ITEMNM As String = "cnItemNm"                '03:商品名
    Private Const CCOL_NISUGATA As String = "cnNisugata"            '04:荷姿・形状
    Private Const CCOL_ZEIKBN As String = "cnZeiKbn"                '05:税
    Private Const CCOL_IRISUU As String = "cnIrisuu"                '06:入数
    Private Const CCOL_KOSUU As String = "cnKosuu"                  '07:個数
    Private Const CCOL_SURYOU As String = "cnSuryou"                '08:数量
    Private Const CCOL_TANNI As String = "cnTanni"                  '09:単位
    Private Const CCOL_URITANKA As String = "cnUriTanka"            '10:売上単価
    Private Const CCOL_URIKINGAKU As String = "cnUriKingaku"        '11:売上金額
    Private Const CCOL_MEISAIBIKOU As String = "cnMeisaiBikou"      '12:明細備考
    Private Const CCOL_KONPOU As String = "cnKonpou"                '13:梱包

    'グリッドデータ名
    'dgvIchiran
    Private Const DTCOL_NO As String = "dtNo"                       '01:No.
    Private Const DTCOL_ITEMCD As String = "dtItemCd"               '02:商品CD
    Private Const DTCOL_ITEMNM As String = "dtItemNm"               '03:商品名
    Private Const DTCOL_NISUGATA As String = "dtNisugata"           '04:荷姿・形状
    Private Const DTCOL_ZEIKBN As String = "dtZeiKbn"               '05:税
    Private Const DTCOL_IRISUU As String = "dtIrisuu"               '06:入数
    Private Const DTCOL_KOSUU As String = "dtKosuu"                 '07:個数
    Private Const DTCOL_SURYOU As String = "dtSuryou"               '08:数量
    Private Const DTCOL_TANNI As String = "dtTanni"                 '09:単位
    Private Const DTCOL_URITANKA As String = "dtUriTanka"           '10:売上単価
    Private Const DTCOL_URIKINGAKU As String = "dtUriKingaku"       '11:売上金額
    Private Const DTCOL_MEISAIBIKOU As String = "dtMeisaiBikou"     '12:明細備考
    Private Const DTCOL_KONPOU As String = "dtKonpou"               '13:梱包

    ''-------------------------------------------------------------------------------
    ''計画対象品一覧出力用定数
    ''-------------------------------------------------------------------------------
    'Private Const RS2 As String = "RecSetM12ForxLS"             'レコードセットテーブル

    ''汎用マスタ固定キー
    'Private Const M01KOTEI_JUYOUSAKI As String = "01"           '需要先

    ''汎用マスタ可変キー
    'Private Const M01KAHEN_KURIKAESI As String = "9"            '他繰返品

    ''M11エイリアス
    'Private Const COLDT_HINMEICD As String = "dtHinmeiCD"       '品名コード
    'Private Const COLDT_HINMEI As String = "dtHinmei"           '品名
    'Private Const COLDT_LOTTYOU As String = "dtLottyou"         '標準ロット長
    'Private Const COLDT_TANTYOU As String = "dtSeisakuTantyou"  '単長
    'Private Const COLDT_JOSU As String = "dtJosu"               '条数
    'Private Const COLDT_KIJUNTUKISU As String = "dtKijunTuki"   '基準月数
    'Private Const COLDT_ABC As String = "dtABC"                 'ABC区分

    ''M12エイリアス
    'Private Const COLDT_M12KHINMEICD As String = "KHINMEICD"    '計画品名コード
    'Private Const COLDT_M12HINMEICD As String = "HINMEICD"      '実品名コード

    ''EXCEL
    'Private Const START_PRINT_ROW As Integer = 7                'EXCEL出力開始行数
    'Private Const START_PRINT_COL As Integer = 1                'EXCEL出力開始列数
    'Private Const XLSSHEETNM_HINSYU As String = "Ver01-00"      '計画対商品一覧雛形シート名
    'Private Const XLS_TITLE As String = "計画対象品一覧表"      'EXCELタイトル

    ''計画対象品一覧表のPGID
    'Private Const ZM130P_PGID As String = "ZM130P"

    '-------------------------------------------------------------------------------
    '   変数定義
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    'Private updFlg As UpdatableType
#End Region

    ''-->2010.12/12 add by takagi 
    ''-------------------------------------------------------------------------------
    ''   オーバーライドプロパティで×ボタンだけを無効にする(ControlBoxはTrueのまま使用可能)
    ''-------------------------------------------------------------------------------
    'Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
    '    Get
    '        Const CS_NOCLOSE As Integer = &H200

    '        Dim tmpCreateParams As System.Windows.Forms.CreateParams = MyBase.CreateParams
    '        tmpCreateParams.ClassStyle = tmpCreateParams.ClassStyle Or CS_NOCLOSE

    '        Return tmpCreateParams
    '    End Get
    'End Property
    ''<--2010.12/12 add by takagi 

    '-------------------------------------------------------------------------------
    'デフォルトコンストラクタ（隠蔽）
    '-------------------------------------------------------------------------------
    Private Sub New()
        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

        'フォーム上のキーイベントを拾う
        'Me.KeyPreview = True       'データグリッド上のキーイベントをうまく拾えなかったためコメント化

        '
        '売上か委託かの判定
        Dim tokuisakiBri As Integer
        tokuisakiBri = 1    '得意先分類を格納
        If tokuisakiBri = 1 Then
            lblNyuuryokuMode.Text = "売上"
            lblNyuuryokuMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        ElseIf tokuisakiBri = 2 Then
            lblNyuuryokuMode.Text = "委託"
            lblNyuuryokuMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        End If

        '
        ' 曜日を取得します
        Dim strDenpyoDate As String = imdShukkaDt.Text
        lblShukkaDay.Text = YobiReturn(strDenpyoDate)

        Dim strChakuDate As String = imdChakuDt.Text
        lblChakuDay.Text = YobiReturn(strChakuDate)

        Dim dataViewRowCnt As Integer = dgvIchiran.Rows.Count
        lblMeisaiCnt.Text = dataViewRowCnt


        '★★★暫定ソースここから★★★
        '実際はここにSELECT文を記載することになる
        Dim dt As DataTable = New DataTable(RS)
        dt.Columns().Add(DTCOL_NO, Type.GetType("System.String"))
        dt.Columns().Add(DTCOL_ITEMCD, Type.GetType("System.String"))
        dt.Columns().Add(DTCOL_ITEMNM, Type.GetType("System.String"))
        dt.Columns().Add(DTCOL_NISUGATA, Type.GetType("System.String"))
        dt.Columns().Add(DTCOL_ZEIKBN, Type.GetType("System.String"))
        dt.Columns().Add(DTCOL_IRISUU, Type.GetType("System.Double"))
        dt.Columns().Add(DTCOL_KOSUU, Type.GetType("System.Decimal"))
        dt.Columns().Add(DTCOL_SURYOU, Type.GetType("System.Double"))
        dt.Columns().Add(DTCOL_TANNI, Type.GetType("System.String"))
        dt.Columns().Add(DTCOL_URITANKA, Type.GetType("System.Double"))
        dt.Columns().Add(DTCOL_URIKINGAKU, Type.GetType("System.Decimal"))
        dt.Columns().Add(DTCOL_MEISAIBIKOU, Type.GetType("System.String"))
        dt.Columns().Add(DTCOL_KONPOU, Type.GetType("System.String"))

        For i As Integer = 0 To 5

            Dim dr As DataRow = dt.NewRow()
            dr.Item(DTCOL_NO) = i.ToString + 1
            dr.Item(DTCOL_ITEMCD) = "123456"
            dr.Item(DTCOL_ITEMNM) = "志津川産ぶっかけめかぶ(宮城県産)"
            dr.Item(DTCOL_NISUGATA) = "1kg×10"
            dr.Item(DTCOL_ZEIKBN) = "外"
            dr.Item(DTCOL_IRISUU) = 999
            dr.Item(DTCOL_KOSUU) = 99999
            dr.Item(DTCOL_SURYOU) = 99999
            dr.Item(DTCOL_TANNI) = "個"
            dr.Item(DTCOL_URITANKA) = 999999
            dr.Item(DTCOL_URIKINGAKU) = 99999999
            dr.Item(DTCOL_MEISAIBIKOU) = "伝票番号：12345678"
            dr.Item(DTCOL_KONPOU) = "発泡"

            dt.Rows.Add(dr)
        Next

        Dim ds As DataSet = New DataSet()
        ds.Tables.Add(dt)
        '★★★暫定ソースここまで★★★

        dgvIchiran.DataSource = ds      '★SQL実行後のレコードセットをここにセットする
        dgvIchiran.DataMember = RS


        'テスト用データ
        imdShukkaDt.Text = "2017/12/26"
        imdChakuDt.Text = "2017/12/27"
        ' ユーザ操作による行追加を無効(禁止)
        'dgvIchiran.AllowUserToAddRows = False      →プロパティにて設定（鴫原コメント化）

        '↓一覧データはデータソースを扱うように変更したためコメント化
        ''' DataGridViewの行追加(1行目) サンプル用
        ''DataGridView1.Rows.Add()
        '''idx = DataGridView1.Rows.Count - 1
        ''DataGridView1.Rows(0).Cells(0).Value = "1"
        ''DataGridView1.Rows(0).Cells(1).Value = "123456"
        ''DataGridView1.Rows(0).Cells(2).Value = "志津川産ぶっかけめかぶ(宮城県産)"
        ''DataGridView1.Rows(0).Cells(3).Value = "1kg×10"
        ''DataGridView1.Rows(0).Cells(4).Value = "外"
        ''DataGridView1.Rows(0).Cells(5).Value = "999.00"
        ''DataGridView1.Rows(0).Cells(6).Value = "99,999"
        ''DataGridView1.Rows(0).Cells(7).Value = "99,999.00"
        ''DataGridView1.Rows(0).Cells(8).Value = "個"
        ''DataGridView1.Rows(0).Cells(9).Value = "999,999.00"
        ''DataGridView1.Rows(0).Cells(10).Value = "99,999,999"
        ''DataGridView1.Rows(0).Cells(11).Value = "伝票番号：12345678"
        ''DataGridView1.Rows(0).Cells(12).Value = "発泡"
        ''DataGridView1.Rows.Add()
    End Sub

    'Stringのyyyy/mm/ddを引数に渡すと曜日を返す
    Public Function YobiReturn(ByRef strDenpyoDate As String)

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

    '-------------------------------------------------------------------------------
    'コンストラクタ　メニューから呼ばれる
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf)
        Call Me.New()

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示

    End Sub

    '-------------------------------------------------------------------------------
    '　フォームロードイベント
    '-------------------------------------------------------------------------------
    Private Sub Sample_Chumon_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            '描画関係の設定
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           'サイズが変更されたときに、コントロールがコントロール自体を再描画するかどうかを示す値を設定
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '描画はバッファで実行され、完了後に、結果が画面に出力されるよう設定
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  'コントロールは、画面に直接ではなく、まずバッファに描画されます。これにより、ちらつきを抑えることができます。
            Me.SetStyle(ControlStyles.UserPaint, True)              'コントロールは、オペレーティング システムによってではなく、独自に描画されるよう設定
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   'コントロールはウィンドウ メッセージ WM_ERASEBKGND を無視するように設定

            '画面タイトル設定
            'Dim optionStr As String = ComBiz.getFormTitleOption(_db, _msgHd)
            'If Not "".Equals(optionStr) Then Me.Text = Me.Text & " - " & optionStr 'タイトルオプション表示

            'バージョン表記
            'lblVersion.Text = UtilClass.getAppVersion(StartUp.assembly)

            '画面初期化
            'Call initForm()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblShoriMode = New System.Windows.Forms.Label()
        Me.TableLayoutPanel23 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblDenpyoNo = New System.Windows.Forms.Label()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.imdShukkaDt = New CustomControl.TextBoxDate()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblShukkaDay = New System.Windows.Forms.Label()
        Me.TableLayoutPanel24 = New System.Windows.Forms.TableLayoutPanel()
        Me.imdChakuDt = New CustomControl.TextBoxDate()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.lblChakuDay = New System.Windows.Forms.Label()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbUriageKbn = New System.Windows.Forms.ComboBox()
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
        Me.TableLayoutPanel12 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblUnsoubin = New System.Windows.Forms.Label()
        Me.txtJikansitei = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtIrainusi = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel11 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtFaxNo = New System.Windows.Forms.TextBox()
        Me.txtTelNo = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtTantousha = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel13 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtShukkaGrpNm = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblSeikyuCd = New System.Windows.Forms.Label()
        Me.lblShukkaGrpCd = New System.Windows.Forms.Label()
        Me.txtSeikyuNm = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel25 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.lblMeisaiCnt = New System.Windows.Forms.Label()
        Me.TableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdTouroku = New System.Windows.Forms.Button()
        Me.cmdModoru = New System.Windows.Forms.Button()
        Me.TableLayoutPanel16 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel21 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.lblNihudaNum = New System.Windows.Forms.Label()
        Me.lblHassouNum = New System.Windows.Forms.Label()
        Me.lblResupuriNum = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel22 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.lblShouhizei = New System.Windows.Forms.Label()
        Me.lblZeikomi = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.lblNyuuryokuMode = New System.Windows.Forms.Label()
        Me.TableLayoutPanel26 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmdDelRow = New System.Windows.Forms.Button()
        Me.cmdAddRow = New System.Windows.Forms.Button()
        Me.cmdTopRow = New System.Windows.Forms.Button()
        Me.dgvIchiran = New CustomTabStopDataGridView.TabStop.UtilTabStopDataGridView()
        Me.cnNo = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnItemCd = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnItemNm = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnNisugata = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnZeiKbn = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnIrisuu = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnKosuu = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnSuryou = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnTanni = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnUriTanka = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnUriKingaku = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnMeisaiBikou = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnKonpou = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel23.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.TableLayoutPanel24.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
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
        Me.TableLayoutPanel12.SuspendLayout()
        Me.TableLayoutPanel11.SuspendLayout()
        Me.TableLayoutPanel13.SuspendLayout()
        Me.TableLayoutPanel25.SuspendLayout()
        Me.TableLayoutPanel8.SuspendLayout()
        Me.TableLayoutPanel16.SuspendLayout()
        Me.TableLayoutPanel21.SuspendLayout()
        Me.TableLayoutPanel22.SuspendLayout()
        Me.TableLayoutPanel26.SuspendLayout()
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
        Me.TableLayoutPanel1.RowCount = 6
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
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
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(1284, 76)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'lblShoriMode
        '
        Me.lblShoriMode.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblShoriMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblShoriMode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblShoriMode.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblShoriMode.Location = New System.Drawing.Point(1122, 18)
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
        Me.TableLayoutPanel23.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel23.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel23.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel23.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.0!))
        Me.TableLayoutPanel23.Controls.Add(Me.TableLayoutPanel3, 0, 1)
        Me.TableLayoutPanel23.Controls.Add(Me.TableLayoutPanel4, 1, 1)
        Me.TableLayoutPanel23.Controls.Add(Me.TableLayoutPanel24, 2, 1)
        Me.TableLayoutPanel23.Controls.Add(Me.TableLayoutPanel5, 3, 1)
        Me.TableLayoutPanel23.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel23.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel23.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel23.Name = "TableLayoutPanel23"
        Me.TableLayoutPanel23.RowCount = 2
        Me.TableLayoutPanel23.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel23.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel23.Size = New System.Drawing.Size(1091, 76)
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
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.imdShukkaDt, 0, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.Label1, 0, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.lblShukkaDay, 2, 1)
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(188, 18)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 2
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(212, 55)
        Me.TableLayoutPanel4.TabIndex = 1
        '
        'imdShukkaDt
        '
        Me.imdShukkaDt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.imdShukkaDt.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdShukkaDt.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.imdShukkaDt.Location = New System.Drawing.Point(100, 33)
        Me.imdShukkaDt.Margin = New System.Windows.Forms.Padding(0)
        Me.imdShukkaDt.Mask = "0000/00/00"
        Me.imdShukkaDt.Name = "imdShukkaDt"
        Me.imdShukkaDt.PermitChars = New Char() {Global.Microsoft.VisualBasic.ChrW(48), Global.Microsoft.VisualBasic.ChrW(49), Global.Microsoft.VisualBasic.ChrW(50), Global.Microsoft.VisualBasic.ChrW(51), Global.Microsoft.VisualBasic.ChrW(52), Global.Microsoft.VisualBasic.ChrW(53), Global.Microsoft.VisualBasic.ChrW(54), Global.Microsoft.VisualBasic.ChrW(55), Global.Microsoft.VisualBasic.ChrW(56), Global.Microsoft.VisualBasic.ChrW(57), Global.Microsoft.VisualBasic.ChrW(47), Global.Microsoft.VisualBasic.ChrW(95)}
        Me.imdShukkaDt.Size = New System.Drawing.Size(96, 22)
        Me.imdShukkaDt.TabIndex = 1
        Me.imdShukkaDt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
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
        Me.Label1.Text = "伝票日付"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblShukkaDay
        '
        Me.lblShukkaDay.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblShukkaDay.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblShukkaDay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblShukkaDay.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblShukkaDay.Location = New System.Drawing.Point(196, 33)
        Me.lblShukkaDay.Margin = New System.Windows.Forms.Padding(0)
        Me.lblShukkaDay.Name = "lblShukkaDay"
        Me.lblShukkaDay.Size = New System.Drawing.Size(16, 22)
        Me.lblShukkaDay.TabIndex = 2
        Me.lblShukkaDay.Text = "火"
        Me.lblShukkaDay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel24
        '
        Me.TableLayoutPanel24.ColumnCount = 3
        Me.TableLayoutPanel24.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel24.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel24.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel24.Controls.Add(Me.imdChakuDt, 0, 1)
        Me.TableLayoutPanel24.Controls.Add(Me.Label34, 0, 1)
        Me.TableLayoutPanel24.Controls.Add(Me.lblChakuDay, 2, 1)
        Me.TableLayoutPanel24.Location = New System.Drawing.Point(406, 18)
        Me.TableLayoutPanel24.Name = "TableLayoutPanel24"
        Me.TableLayoutPanel24.RowCount = 2
        Me.TableLayoutPanel24.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel24.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel24.Size = New System.Drawing.Size(212, 55)
        Me.TableLayoutPanel24.TabIndex = 2
        '
        'imdChakuDt
        '
        Me.imdChakuDt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.imdChakuDt.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdChakuDt.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.imdChakuDt.Location = New System.Drawing.Point(100, 33)
        Me.imdChakuDt.Margin = New System.Windows.Forms.Padding(0)
        Me.imdChakuDt.Mask = "0000/00/00"
        Me.imdChakuDt.Name = "imdChakuDt"
        Me.imdChakuDt.PermitChars = New Char() {Global.Microsoft.VisualBasic.ChrW(48), Global.Microsoft.VisualBasic.ChrW(49), Global.Microsoft.VisualBasic.ChrW(50), Global.Microsoft.VisualBasic.ChrW(51), Global.Microsoft.VisualBasic.ChrW(52), Global.Microsoft.VisualBasic.ChrW(53), Global.Microsoft.VisualBasic.ChrW(54), Global.Microsoft.VisualBasic.ChrW(55), Global.Microsoft.VisualBasic.ChrW(56), Global.Microsoft.VisualBasic.ChrW(57), Global.Microsoft.VisualBasic.ChrW(47), Global.Microsoft.VisualBasic.ChrW(95)}
        Me.imdChakuDt.Size = New System.Drawing.Size(96, 22)
        Me.imdChakuDt.TabIndex = 1
        Me.imdChakuDt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
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
        'lblChakuDay
        '
        Me.lblChakuDay.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblChakuDay.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblChakuDay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblChakuDay.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblChakuDay.Location = New System.Drawing.Point(196, 33)
        Me.lblChakuDay.Margin = New System.Windows.Forms.Padding(0)
        Me.lblChakuDay.Name = "lblChakuDay"
        Me.lblChakuDay.Size = New System.Drawing.Size(16, 22)
        Me.lblChakuDay.TabIndex = 2
        Me.lblChakuDay.Text = "水"
        Me.lblChakuDay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 2
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.Label2, 0, 1)
        Me.TableLayoutPanel5.Controls.Add(Me.cmbUriageKbn, 1, 1)
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(624, 18)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 2
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(212, 55)
        Me.TableLayoutPanel5.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(0, 33)
        Me.Label2.Margin = New System.Windows.Forms.Padding(0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 22)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "売上区分"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbUriageKbn
        '
        Me.cmbUriageKbn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbUriageKbn.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbUriageKbn.FormattingEnabled = True
        Me.cmbUriageKbn.Items.AddRange(New Object() {"売上"})
        Me.cmbUriageKbn.Location = New System.Drawing.Point(88, 32)
        Me.cmbUriageKbn.Margin = New System.Windows.Forms.Padding(0)
        Me.cmbUriageKbn.Name = "cmbUriageKbn"
        Me.cmbUriageKbn.Size = New System.Drawing.Size(96, 23)
        Me.cmbUriageKbn.TabIndex = 1
        Me.cmbUriageKbn.Text = "売上"
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 2
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel6.Controls.Add(Me.TableLayoutPanel7, 0, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.TableLayoutPanel9, 1, 0)
        Me.TableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(3, 79)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 1
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(1278, 146)
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
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(639, 146)
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
        Me.TableLayoutPanel14.Location = New System.Drawing.Point(0, 102)
        Me.TableLayoutPanel14.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel14.Name = "TableLayoutPanel14"
        Me.TableLayoutPanel14.RowCount = 2
        Me.TableLayoutPanel14.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel14.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel14.Size = New System.Drawing.Size(621, 44)
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
        Me.txtShagaiBikou.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf
        Me.txtShagaiBikou.Location = New System.Drawing.Point(100, 0)
        Me.txtShagaiBikou.Margin = New System.Windows.Forms.Padding(0)
        Me.txtShagaiBikou.MaxLength = 20
        Me.txtShagaiBikou.Name = "txtShagaiBikou"
        Me.txtShagaiBikou.Size = New System.Drawing.Size(521, 22)
        Me.txtShagaiBikou.TabIndex = 1
        '
        'txtShanaiBikou
        '
        Me.txtShanaiBikou.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShanaiBikou.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf
        Me.txtShanaiBikou.Location = New System.Drawing.Point(100, 22)
        Me.txtShanaiBikou.Margin = New System.Windows.Forms.Padding(0)
        Me.txtShanaiBikou.MaxLength = 20
        Me.txtShanaiBikou.Name = "txtShanaiBikou"
        Me.txtShanaiBikou.Size = New System.Drawing.Size(521, 22)
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
        Me.TableLayoutPanel15.Size = New System.Drawing.Size(639, 102)
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
        Me.TableLayoutPanel17.Dock = System.Windows.Forms.DockStyle.Fill
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
        Me.TableLayoutPanel19.Size = New System.Drawing.Size(437, 69)
        Me.TableLayoutPanel19.TabIndex = 1242
        '
        'txtAddress1
        '
        Me.txtAddress1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtAddress1.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtAddress1.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf
        Me.txtAddress1.Location = New System.Drawing.Point(0, 0)
        Me.txtAddress1.Margin = New System.Windows.Forms.Padding(0)
        Me.txtAddress1.MaxLength = 20
        Me.txtAddress1.Name = "txtAddress1"
        Me.txtAddress1.Size = New System.Drawing.Size(437, 22)
        Me.txtAddress1.TabIndex = 0
        Me.txtAddress1.Text = "青森県青森市大字合子沢字"
        '
        'txtAddress2
        '
        Me.txtAddress2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtAddress2.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtAddress2.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf
        Me.txtAddress2.Location = New System.Drawing.Point(0, 22)
        Me.txtAddress2.Margin = New System.Windows.Forms.Padding(0)
        Me.txtAddress2.MaxLength = 20
        Me.txtAddress2.Name = "txtAddress2"
        Me.txtAddress2.Size = New System.Drawing.Size(437, 22)
        Me.txtAddress2.TabIndex = 1
        Me.txtAddress2.Text = "松森259-6"
        '
        'txtAddress3
        '
        Me.txtAddress3.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtAddress3.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf
        Me.txtAddress3.Location = New System.Drawing.Point(0, 44)
        Me.txtAddress3.Margin = New System.Windows.Forms.Padding(0)
        Me.txtAddress3.MaxLength = 20
        Me.txtAddress3.Name = "txtAddress3"
        Me.txtAddress3.Size = New System.Drawing.Size(437, 22)
        Me.txtAddress3.TabIndex = 2
        Me.txtAddress3.Text = "(青森中核工業団地内)　仙台水産　詰所着"
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
        Me.txtPostalCd2.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf
        Me.txtPostalCd2.Location = New System.Drawing.Point(43, 0)
        Me.txtPostalCd2.Margin = New System.Windows.Forms.Padding(0)
        Me.txtPostalCd2.MaxLength = 20
        Me.txtPostalCd2.Name = "txtPostalCd2"
        Me.txtPostalCd2.Size = New System.Drawing.Size(41, 22)
        Me.txtPostalCd2.TabIndex = 2
        Me.txtPostalCd2.Text = "0134"
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
        Me.txtPostalCd1.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf
        Me.txtPostalCd1.Location = New System.Drawing.Point(0, 0)
        Me.txtPostalCd1.Margin = New System.Windows.Forms.Padding(0)
        Me.txtPostalCd1.MaxLength = 20
        Me.txtPostalCd1.Name = "txtPostalCd1"
        Me.txtPostalCd1.Size = New System.Drawing.Size(30, 22)
        Me.txtPostalCd1.TabIndex = 0
        Me.txtPostalCd1.Text = "030"
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
        Me.lblShukkaCd.Text = "059701"
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
        Me.txtShukkaNm.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf
        Me.txtShukkaNm.Location = New System.Drawing.Point(184, 0)
        Me.txtShukkaNm.Margin = New System.Windows.Forms.Padding(0)
        Me.txtShukkaNm.MaxLength = 20
        Me.txtShukkaNm.Name = "txtShukkaNm"
        Me.txtShukkaNm.Size = New System.Drawing.Size(437, 22)
        Me.txtShukkaNm.TabIndex = 2
        Me.txtShukkaNm.Text = "(株)ユニバース　青森低温物流センター"
        '
        'TableLayoutPanel9
        '
        Me.TableLayoutPanel9.ColumnCount = 2
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85.0!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
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
        Me.TableLayoutPanel9.Size = New System.Drawing.Size(639, 146)
        Me.TableLayoutPanel9.TabIndex = 1
        '
        'TableLayoutPanel10
        '
        Me.TableLayoutPanel10.ColumnCount = 2
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel10.Controls.Add(Me.TableLayoutPanel12, 1, 0)
        Me.TableLayoutPanel10.Controls.Add(Me.TableLayoutPanel11, 0, 0)
        Me.TableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel10.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel10.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel10.Name = "TableLayoutPanel10"
        Me.TableLayoutPanel10.RowCount = 1
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel10.Size = New System.Drawing.Size(543, 102)
        Me.TableLayoutPanel10.TabIndex = 0
        '
        'TableLayoutPanel12
        '
        Me.TableLayoutPanel12.ColumnCount = 2
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel12.Controls.Add(Me.lblUnsoubin, 1, 2)
        Me.TableLayoutPanel12.Controls.Add(Me.txtJikansitei, 1, 1)
        Me.TableLayoutPanel12.Controls.Add(Me.Label8, 0, 2)
        Me.TableLayoutPanel12.Controls.Add(Me.Label9, 0, 0)
        Me.TableLayoutPanel12.Controls.Add(Me.Label10, 0, 1)
        Me.TableLayoutPanel12.Controls.Add(Me.txtIrainusi, 1, 0)
        Me.TableLayoutPanel12.Location = New System.Drawing.Point(271, 0)
        Me.TableLayoutPanel12.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel12.Name = "TableLayoutPanel12"
        Me.TableLayoutPanel12.RowCount = 3
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel12.Size = New System.Drawing.Size(272, 68)
        Me.TableLayoutPanel12.TabIndex = 1
        '
        'lblUnsoubin
        '
        Me.lblUnsoubin.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblUnsoubin.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUnsoubin.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnsoubin.Location = New System.Drawing.Point(100, 44)
        Me.lblUnsoubin.Margin = New System.Windows.Forms.Padding(0)
        Me.lblUnsoubin.Name = "lblUnsoubin"
        Me.lblUnsoubin.Size = New System.Drawing.Size(182, 22)
        Me.lblUnsoubin.TabIndex = 5
        Me.lblUnsoubin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtJikansitei
        '
        Me.txtJikansitei.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtJikansitei.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf
        Me.txtJikansitei.Location = New System.Drawing.Point(100, 22)
        Me.txtJikansitei.Margin = New System.Windows.Forms.Padding(0)
        Me.txtJikansitei.MaxLength = 20
        Me.txtJikansitei.Name = "txtJikansitei"
        Me.txtJikansitei.Size = New System.Drawing.Size(182, 22)
        Me.txtJikansitei.TabIndex = 4
        Me.txtJikansitei.Text = "AM4時必着"
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
        Me.txtIrainusi.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtIrainusi.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf
        Me.txtIrainusi.Location = New System.Drawing.Point(100, 0)
        Me.txtIrainusi.Margin = New System.Windows.Forms.Padding(0)
        Me.txtIrainusi.MaxLength = 20
        Me.txtIrainusi.Name = "txtIrainusi"
        Me.txtIrainusi.Size = New System.Drawing.Size(182, 22)
        Me.txtIrainusi.TabIndex = 3
        Me.txtIrainusi.Text = "仙台水産様御依頼分"
        '
        'TableLayoutPanel11
        '
        Me.TableLayoutPanel11.ColumnCount = 2
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel11.Controls.Add(Me.txtFaxNo, 1, 2)
        Me.TableLayoutPanel11.Controls.Add(Me.txtTelNo, 1, 1)
        Me.TableLayoutPanel11.Controls.Add(Me.Label7, 0, 2)
        Me.TableLayoutPanel11.Controls.Add(Me.Label5, 0, 0)
        Me.TableLayoutPanel11.Controls.Add(Me.Label6, 0, 1)
        Me.TableLayoutPanel11.Controls.Add(Me.txtTantousha, 1, 0)
        Me.TableLayoutPanel11.Location = New System.Drawing.Point(30, 0)
        Me.TableLayoutPanel11.Margin = New System.Windows.Forms.Padding(30, 0, 0, 0)
        Me.TableLayoutPanel11.Name = "TableLayoutPanel11"
        Me.TableLayoutPanel11.RowCount = 3
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel11.Size = New System.Drawing.Size(221, 68)
        Me.TableLayoutPanel11.TabIndex = 0
        '
        'txtFaxNo
        '
        Me.txtFaxNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtFaxNo.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf
        Me.txtFaxNo.Location = New System.Drawing.Point(100, 44)
        Me.txtFaxNo.Margin = New System.Windows.Forms.Padding(0)
        Me.txtFaxNo.MaxLength = 20
        Me.txtFaxNo.Name = "txtFaxNo"
        Me.txtFaxNo.Size = New System.Drawing.Size(120, 22)
        Me.txtFaxNo.TabIndex = 5
        Me.txtFaxNo.Text = "018-111-2233"
        '
        'txtTelNo
        '
        Me.txtTelNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTelNo.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf
        Me.txtTelNo.Location = New System.Drawing.Point(100, 22)
        Me.txtTelNo.Margin = New System.Windows.Forms.Padding(0)
        Me.txtTelNo.MaxLength = 20
        Me.txtTelNo.Name = "txtTelNo"
        Me.txtTelNo.Size = New System.Drawing.Size(120, 22)
        Me.txtTelNo.TabIndex = 4
        Me.txtTelNo.Text = "018-111-2222"
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
        Me.txtTantousha.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf
        Me.txtTantousha.Location = New System.Drawing.Point(100, 0)
        Me.txtTantousha.Margin = New System.Windows.Forms.Padding(0)
        Me.txtTantousha.MaxLength = 20
        Me.txtTantousha.Name = "txtTantousha"
        Me.txtTantousha.Size = New System.Drawing.Size(120, 22)
        Me.txtTantousha.TabIndex = 3
        '
        'TableLayoutPanel13
        '
        Me.TableLayoutPanel13.ColumnCount = 3
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel13.Controls.Add(Me.txtShukkaGrpNm, 2, 1)
        Me.TableLayoutPanel13.Controls.Add(Me.Label13, 0, 1)
        Me.TableLayoutPanel13.Controls.Add(Me.Label12, 0, 0)
        Me.TableLayoutPanel13.Controls.Add(Me.lblSeikyuCd, 1, 0)
        Me.TableLayoutPanel13.Controls.Add(Me.lblShukkaGrpCd, 1, 1)
        Me.TableLayoutPanel13.Controls.Add(Me.txtSeikyuNm, 2, 0)
        Me.TableLayoutPanel13.Location = New System.Drawing.Point(30, 102)
        Me.TableLayoutPanel13.Margin = New System.Windows.Forms.Padding(30, 0, 0, 0)
        Me.TableLayoutPanel13.Name = "TableLayoutPanel13"
        Me.TableLayoutPanel13.RowCount = 2
        Me.TableLayoutPanel13.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel13.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel13.Size = New System.Drawing.Size(513, 44)
        Me.TableLayoutPanel13.TabIndex = 1
        '
        'txtShukkaGrpNm
        '
        Me.txtShukkaGrpNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShukkaGrpNm.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf
        Me.txtShukkaGrpNm.Location = New System.Drawing.Point(210, 22)
        Me.txtShukkaGrpNm.Margin = New System.Windows.Forms.Padding(0)
        Me.txtShukkaGrpNm.MaxLength = 20
        Me.txtShukkaGrpNm.Name = "txtShukkaGrpNm"
        Me.txtShukkaGrpNm.Size = New System.Drawing.Size(378, 22)
        Me.txtShukkaGrpNm.TabIndex = 5
        Me.txtShukkaGrpNm.Text = "(株)ユニバース"
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label13.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label13.Location = New System.Drawing.Point(0, 22)
        Me.Label13.Margin = New System.Windows.Forms.Padding(0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(100, 22)
        Me.Label13.TabIndex = 3
        Me.Label13.Text = " 出荷先GRP"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label12.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.Location = New System.Drawing.Point(0, 0)
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
        Me.lblSeikyuCd.Location = New System.Drawing.Point(100, 0)
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
        Me.lblShukkaGrpCd.Location = New System.Drawing.Point(100, 22)
        Me.lblShukkaGrpCd.Margin = New System.Windows.Forms.Padding(0)
        Me.lblShukkaGrpCd.Name = "lblShukkaGrpCd"
        Me.lblShukkaGrpCd.Size = New System.Drawing.Size(110, 22)
        Me.lblShukkaGrpCd.TabIndex = 4
        Me.lblShukkaGrpCd.Text = " 059700"
        Me.lblShukkaGrpCd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtSeikyuNm
        '
        Me.txtSeikyuNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSeikyuNm.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf
        Me.txtSeikyuNm.Location = New System.Drawing.Point(210, 0)
        Me.txtSeikyuNm.Margin = New System.Windows.Forms.Padding(0)
        Me.txtSeikyuNm.MaxLength = 20
        Me.txtSeikyuNm.Name = "txtSeikyuNm"
        Me.txtSeikyuNm.Size = New System.Drawing.Size(378, 22)
        Me.txtSeikyuNm.TabIndex = 2
        Me.txtSeikyuNm.Text = "(株)仙台水産　貝類　相対課"
        '
        'TableLayoutPanel25
        '
        Me.TableLayoutPanel25.ColumnCount = 1
        Me.TableLayoutPanel25.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel25.Controls.Add(Me.Label36, 0, 0)
        Me.TableLayoutPanel25.Controls.Add(Me.lblMeisaiCnt, 0, 1)
        Me.TableLayoutPanel25.Location = New System.Drawing.Point(543, 102)
        Me.TableLayoutPanel25.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel25.Name = "TableLayoutPanel25"
        Me.TableLayoutPanel25.RowCount = 2
        Me.TableLayoutPanel25.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel25.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel25.Size = New System.Drawing.Size(96, 44)
        Me.TableLayoutPanel25.TabIndex = 1
        '
        'Label36
        '
        Me.Label36.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label36.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label36.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label36.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label36.Location = New System.Drawing.Point(0, 0)
        Me.Label36.Margin = New System.Windows.Forms.Padding(0)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(96, 22)
        Me.Label36.TabIndex = 0
        Me.Label36.Text = "明細数"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblMeisaiCnt
        '
        Me.lblMeisaiCnt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMeisaiCnt.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblMeisaiCnt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMeisaiCnt.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMeisaiCnt.Location = New System.Drawing.Point(0, 22)
        Me.lblMeisaiCnt.Margin = New System.Windows.Forms.Padding(0)
        Me.lblMeisaiCnt.Name = "lblMeisaiCnt"
        Me.lblMeisaiCnt.Size = New System.Drawing.Size(96, 22)
        Me.lblMeisaiCnt.TabIndex = 1
        Me.lblMeisaiCnt.Text = "2"
        Me.lblMeisaiCnt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel8
        '
        Me.TableLayoutPanel8.ColumnCount = 4
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel8.Controls.Add(Me.cmdPrint, 0, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.cmdTouroku, 2, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.cmdModoru, 3, 0)
        Me.TableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel8.Location = New System.Drawing.Point(3, 687)
        Me.TableLayoutPanel8.Name = "TableLayoutPanel8"
        Me.TableLayoutPanel8.RowCount = 1
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.Size = New System.Drawing.Size(1278, 70)
        Me.TableLayoutPanel8.TabIndex = 4
        '
        'cmdPrint
        '
        Me.cmdPrint.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdPrint.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdPrint.Location = New System.Drawing.Point(4, 20)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.Size = New System.Drawing.Size(119, 30)
        Me.cmdPrint.TabIndex = 0
        Me.cmdPrint.Text = "再印刷(&P)"
        Me.cmdPrint.UseVisualStyleBackColor = True
        '
        'cmdTouroku
        '
        Me.cmdTouroku.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdTouroku.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdTouroku.Location = New System.Drawing.Point(1025, 20)
        Me.cmdTouroku.Name = "cmdTouroku"
        Me.cmdTouroku.Size = New System.Drawing.Size(119, 30)
        Me.cmdTouroku.TabIndex = 25
        Me.cmdTouroku.Text = "登録(&G)"
        Me.cmdTouroku.UseVisualStyleBackColor = True
        '
        'cmdModoru
        '
        Me.cmdModoru.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdModoru.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdModoru.Location = New System.Drawing.Point(1153, 20)
        Me.cmdModoru.Name = "cmdModoru"
        Me.cmdModoru.Size = New System.Drawing.Size(119, 30)
        Me.cmdModoru.TabIndex = 26
        Me.cmdModoru.Text = "戻る(&B)"
        Me.cmdModoru.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel16
        '
        Me.TableLayoutPanel16.ColumnCount = 5
        Me.TableLayoutPanel16.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.0!))
        Me.TableLayoutPanel16.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel16.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel16.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel16.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel16.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel16.Controls.Add(Me.TableLayoutPanel21, 1, 0)
        Me.TableLayoutPanel16.Controls.Add(Me.TableLayoutPanel22, 3, 0)
        Me.TableLayoutPanel16.Controls.Add(Me.lblNyuuryokuMode, 4, 0)
        Me.TableLayoutPanel16.Controls.Add(Me.TableLayoutPanel26, 0, 0)
        Me.TableLayoutPanel16.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel16.Location = New System.Drawing.Point(3, 573)
        Me.TableLayoutPanel16.Name = "TableLayoutPanel16"
        Me.TableLayoutPanel16.RowCount = 1
        Me.TableLayoutPanel16.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel16.Size = New System.Drawing.Size(1278, 108)
        Me.TableLayoutPanel16.TabIndex = 3
        '
        'TableLayoutPanel21
        '
        Me.TableLayoutPanel21.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel21.ColumnCount = 2
        Me.TableLayoutPanel21.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel21.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel21.Controls.Add(Me.Label23, 0, 2)
        Me.TableLayoutPanel21.Controls.Add(Me.Label19, 0, 1)
        Me.TableLayoutPanel21.Controls.Add(Me.lblNihudaNum, 1, 0)
        Me.TableLayoutPanel21.Controls.Add(Me.lblHassouNum, 1, 1)
        Me.TableLayoutPanel21.Controls.Add(Me.lblResupuriNum, 1, 2)
        Me.TableLayoutPanel21.Controls.Add(Me.Label18, 0, 0)
        Me.TableLayoutPanel21.Location = New System.Drawing.Point(518, 3)
        Me.TableLayoutPanel21.Name = "TableLayoutPanel21"
        Me.TableLayoutPanel21.RowCount = 3
        Me.TableLayoutPanel21.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel21.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel21.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel21.Size = New System.Drawing.Size(181, 64)
        Me.TableLayoutPanel21.TabIndex = 0
        '
        'Label23
        '
        Me.Label23.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.Label19.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.lblNihudaNum.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblNihudaNum.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblNihudaNum.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNihudaNum.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNihudaNum.Location = New System.Drawing.Point(100, 0)
        Me.lblNihudaNum.Margin = New System.Windows.Forms.Padding(0)
        Me.lblNihudaNum.Name = "lblNihudaNum"
        Me.lblNihudaNum.Size = New System.Drawing.Size(84, 22)
        Me.lblNihudaNum.TabIndex = 1
        Me.lblNihudaNum.Text = "999"
        Me.lblNihudaNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblHassouNum
        '
        Me.lblHassouNum.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblHassouNum.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblHassouNum.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblHassouNum.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHassouNum.Location = New System.Drawing.Point(100, 22)
        Me.lblHassouNum.Margin = New System.Windows.Forms.Padding(0)
        Me.lblHassouNum.Name = "lblHassouNum"
        Me.lblHassouNum.Size = New System.Drawing.Size(84, 22)
        Me.lblHassouNum.TabIndex = 3
        Me.lblHassouNum.Text = "999"
        Me.lblHassouNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblResupuriNum
        '
        Me.lblResupuriNum.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblResupuriNum.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblResupuriNum.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblResupuriNum.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblResupuriNum.Location = New System.Drawing.Point(100, 44)
        Me.lblResupuriNum.Margin = New System.Windows.Forms.Padding(0)
        Me.lblResupuriNum.Name = "lblResupuriNum"
        Me.lblResupuriNum.Size = New System.Drawing.Size(84, 22)
        Me.lblResupuriNum.TabIndex = 5
        Me.lblResupuriNum.Text = "999"
        Me.lblResupuriNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label18
        '
        Me.Label18.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        'TableLayoutPanel22
        '
        Me.TableLayoutPanel22.ColumnCount = 2
        Me.TableLayoutPanel22.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel22.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel22.Controls.Add(Me.Label27, 0, 2)
        Me.TableLayoutPanel22.Controls.Add(Me.Label28, 0, 1)
        Me.TableLayoutPanel22.Controls.Add(Me.lblTotal, 1, 0)
        Me.TableLayoutPanel22.Controls.Add(Me.lblShouhizei, 1, 1)
        Me.TableLayoutPanel22.Controls.Add(Me.lblZeikomi, 1, 2)
        Me.TableLayoutPanel22.Controls.Add(Me.Label32, 0, 0)
        Me.TableLayoutPanel22.Location = New System.Drawing.Point(832, 3)
        Me.TableLayoutPanel22.Name = "TableLayoutPanel22"
        Me.TableLayoutPanel22.RowCount = 3
        Me.TableLayoutPanel22.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel22.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel22.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel22.Size = New System.Drawing.Size(232, 64)
        Me.TableLayoutPanel22.TabIndex = 1
        '
        'Label27
        '
        Me.Label27.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label27.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label27.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label27.Location = New System.Drawing.Point(0, 42)
        Me.Label27.Margin = New System.Windows.Forms.Padding(0)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(100, 22)
        Me.Label27.TabIndex = 4
        Me.Label27.Text = "税込額"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label28
        '
        Me.Label28.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label28.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label28.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label28.Location = New System.Drawing.Point(0, 21)
        Me.Label28.Margin = New System.Windows.Forms.Padding(0)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(100, 21)
        Me.Label28.TabIndex = 2
        Me.Label28.Text = "消費税"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTotal
        '
        Me.lblTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblTotal.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotal.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTotal.Location = New System.Drawing.Point(100, 0)
        Me.lblTotal.Margin = New System.Windows.Forms.Padding(0)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(130, 21)
        Me.lblTotal.TabIndex = 1
        Me.lblTotal.Text = "99,999,999"
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblShouhizei
        '
        Me.lblShouhizei.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblShouhizei.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblShouhizei.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblShouhizei.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblShouhizei.Location = New System.Drawing.Point(100, 21)
        Me.lblShouhizei.Margin = New System.Windows.Forms.Padding(0)
        Me.lblShouhizei.Name = "lblShouhizei"
        Me.lblShouhizei.Size = New System.Drawing.Size(130, 21)
        Me.lblShouhizei.TabIndex = 3
        Me.lblShouhizei.Text = "99,999,999"
        Me.lblShouhizei.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblZeikomi
        '
        Me.lblZeikomi.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblZeikomi.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblZeikomi.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblZeikomi.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZeikomi.Location = New System.Drawing.Point(100, 42)
        Me.lblZeikomi.Margin = New System.Windows.Forms.Padding(0)
        Me.lblZeikomi.Name = "lblZeikomi"
        Me.lblZeikomi.Size = New System.Drawing.Size(130, 22)
        Me.lblZeikomi.TabIndex = 5
        Me.lblZeikomi.Text = "99,999,999"
        Me.lblZeikomi.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label32
        '
        Me.Label32.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label32.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label32.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label32.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label32.Location = New System.Drawing.Point(0, 0)
        Me.Label32.Margin = New System.Windows.Forms.Padding(0)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(100, 21)
        Me.Label32.TabIndex = 0
        Me.Label32.Text = "合計"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblNyuuryokuMode
        '
        Me.lblNyuuryokuMode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNyuuryokuMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblNyuuryokuMode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNyuuryokuMode.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNyuuryokuMode.Location = New System.Drawing.Point(1084, 3)
        Me.lblNyuuryokuMode.Margin = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.lblNyuuryokuMode.Name = "lblNyuuryokuMode"
        Me.lblNyuuryokuMode.Size = New System.Drawing.Size(194, 67)
        Me.lblNyuuryokuMode.TabIndex = 1301
        Me.lblNyuuryokuMode.Text = "売上"
        Me.lblNyuuryokuMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel26
        '
        Me.TableLayoutPanel26.ColumnCount = 3
        Me.TableLayoutPanel26.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel26.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel26.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel26.Controls.Add(Me.cmdDelRow, 0, 0)
        Me.TableLayoutPanel26.Controls.Add(Me.cmdAddRow, 0, 0)
        Me.TableLayoutPanel26.Controls.Add(Me.cmdTopRow, 0, 0)
        Me.TableLayoutPanel26.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel26.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel26.Name = "TableLayoutPanel26"
        Me.TableLayoutPanel26.RowCount = 1
        Me.TableLayoutPanel26.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel26.Size = New System.Drawing.Size(441, 102)
        Me.TableLayoutPanel26.TabIndex = 0
        '
        'cmdDelRow
        '
        Me.cmdDelRow.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cmdDelRow.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdDelRow.Location = New System.Drawing.Point(308, 3)
        Me.cmdDelRow.Name = "cmdDelRow"
        Me.cmdDelRow.Size = New System.Drawing.Size(119, 30)
        Me.cmdDelRow.TabIndex = 2
        Me.cmdDelRow.Text = "行削除(&D)"
        Me.cmdDelRow.UseVisualStyleBackColor = True
        '
        'cmdAddRow
        '
        Me.cmdAddRow.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cmdAddRow.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdAddRow.Location = New System.Drawing.Point(161, 3)
        Me.cmdAddRow.Name = "cmdAddRow"
        Me.cmdAddRow.Size = New System.Drawing.Size(119, 30)
        Me.cmdAddRow.TabIndex = 1
        Me.cmdAddRow.Text = "行追加(&I)"
        Me.cmdAddRow.UseVisualStyleBackColor = True
        '
        'cmdTopRow
        '
        Me.cmdTopRow.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cmdTopRow.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdTopRow.Location = New System.Drawing.Point(14, 3)
        Me.cmdTopRow.Name = "cmdTopRow"
        Me.cmdTopRow.Size = New System.Drawing.Size(119, 30)
        Me.cmdTopRow.TabIndex = 0
        Me.cmdTopRow.Text = "先頭へ(&T)"
        Me.cmdTopRow.UseVisualStyleBackColor = True
        '
        'dgvIchiran
        '
        Me.dgvIchiran.AllowUserToAddRows = False
        Me.dgvIchiran.AllowUserToDeleteRows = False
        Me.dgvIchiran.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvIchiran.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvIchiran.ColumnHeadersHeight = 38
        Me.dgvIchiran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvIchiran.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cnNo, Me.cnItemCd, Me.cnItemNm, Me.cnNisugata, Me.cnZeiKbn, Me.cnIrisuu, Me.cnKosuu, Me.cnSuryou, Me.cnTanni, Me.cnUriTanka, Me.cnUriKingaku, Me.cnMeisaiBikou, Me.cnKonpou})
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle15.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        DataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvIchiran.DefaultCellStyle = DataGridViewCellStyle15
        Me.dgvIchiran.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvIchiran.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvIchiran.Location = New System.Drawing.Point(3, 231)
        Me.dgvIchiran.Name = "dgvIchiran"
        Me.dgvIchiran.restrainEnterKeyMoving = False
        Me.dgvIchiran.RowHeadersVisible = False
        Me.dgvIchiran.RowTemplate.Height = 18
        Me.dgvIchiran.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvIchiran.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvIchiran.ShowCellToolTips = False
        Me.dgvIchiran.Size = New System.Drawing.Size(1278, 336)
        Me.dgvIchiran.TabIndex = 2
        '
        'cnNo
        '
        Me.cnNo.DataPropertyName = "dtNo"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        Me.cnNo.DefaultCellStyle = DataGridViewCellStyle2
        Me.cnNo.HeaderText = "No."
        Me.cnNo.Name = "cnNo"
        Me.cnNo.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnNo.TabStop = False
        Me.cnNo.Width = 30
        '
        'cnItemCd
        '
        Me.cnItemCd.DataPropertyName = "dtItemCd"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        Me.cnItemCd.DefaultCellStyle = DataGridViewCellStyle3
        Me.cnItemCd.HeaderText = "商品CD"
        Me.cnItemCd.Name = "cnItemCd"
        Me.cnItemCd.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnItemCd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnItemCd.TabStop = True
        Me.cnItemCd.Width = 70
        '
        'cnItemNm
        '
        Me.cnItemNm.DataPropertyName = "dtItemNm"
        DataGridViewCellStyle4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.cnItemNm.DefaultCellStyle = DataGridViewCellStyle4
        Me.cnItemNm.HeaderText = "商品名"
        Me.cnItemNm.Name = "cnItemNm"
        Me.cnItemNm.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnItemNm.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnItemNm.TabStop = True
        Me.cnItemNm.Width = 290
        '
        'cnNisugata
        '
        Me.cnNisugata.DataPropertyName = "dtNisugata"
        DataGridViewCellStyle5.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.cnNisugata.DefaultCellStyle = DataGridViewCellStyle5
        Me.cnNisugata.HeaderText = "荷姿・形状"
        Me.cnNisugata.Name = "cnNisugata"
        Me.cnNisugata.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnNisugata.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnNisugata.TabStop = True
        Me.cnNisugata.Width = 110
        '
        'cnZeiKbn
        '
        Me.cnZeiKbn.DataPropertyName = "dtZeiKbn"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle6.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black
        Me.cnZeiKbn.DefaultCellStyle = DataGridViewCellStyle6
        Me.cnZeiKbn.HeaderText = "税"
        Me.cnZeiKbn.Name = "cnZeiKbn"
        Me.cnZeiKbn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnZeiKbn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnZeiKbn.TabStop = True
        Me.cnZeiKbn.Width = 30
        '
        'cnIrisuu
        '
        Me.cnIrisuu.DataPropertyName = "dtIrisuu"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        DataGridViewCellStyle7.Format = "#.00"
        Me.cnIrisuu.DefaultCellStyle = DataGridViewCellStyle7
        Me.cnIrisuu.HeaderText = "入数"
        Me.cnIrisuu.Name = "cnIrisuu"
        Me.cnIrisuu.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnIrisuu.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnIrisuu.TabStop = True
        Me.cnIrisuu.Width = 70
        '
        'cnKosuu
        '
        Me.cnKosuu.DataPropertyName = "dtKosuu"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        DataGridViewCellStyle8.Format = "#,###"
        Me.cnKosuu.DefaultCellStyle = DataGridViewCellStyle8
        Me.cnKosuu.HeaderText = "個数"
        Me.cnKosuu.Name = "cnKosuu"
        Me.cnKosuu.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnKosuu.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnKosuu.TabStop = True
        Me.cnKosuu.Width = 70
        '
        'cnSuryou
        '
        Me.cnSuryou.DataPropertyName = "dtSuryou"
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        DataGridViewCellStyle9.Format = "#,###.00"
        Me.cnSuryou.DefaultCellStyle = DataGridViewCellStyle9
        Me.cnSuryou.HeaderText = "数量"
        Me.cnSuryou.Name = "cnSuryou"
        Me.cnSuryou.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnSuryou.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnSuryou.TabStop = True
        Me.cnSuryou.Width = 90
        '
        'cnTanni
        '
        Me.cnTanni.DataPropertyName = "dtTanni"
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle10.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        DataGridViewCellStyle10.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black
        Me.cnTanni.DefaultCellStyle = DataGridViewCellStyle10
        Me.cnTanni.HeaderText = "単位"
        Me.cnTanni.Name = "cnTanni"
        Me.cnTanni.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnTanni.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnTanni.TabStop = True
        Me.cnTanni.Width = 50
        '
        'cnUriTanka
        '
        Me.cnUriTanka.DataPropertyName = "dtUriTanka"
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle11.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        DataGridViewCellStyle11.Format = "#,###.00"
        Me.cnUriTanka.DefaultCellStyle = DataGridViewCellStyle11
        Me.cnUriTanka.HeaderText = "売上単価"
        Me.cnUriTanka.Name = "cnUriTanka"
        Me.cnUriTanka.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnUriTanka.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnUriTanka.TabStop = True
        '
        'cnUriKingaku
        '
        Me.cnUriKingaku.DataPropertyName = "dtUriKingaku"
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle12.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        DataGridViewCellStyle12.Format = "#,###.##"
        Me.cnUriKingaku.DefaultCellStyle = DataGridViewCellStyle12
        Me.cnUriKingaku.HeaderText = "売上金額"
        Me.cnUriKingaku.Name = "cnUriKingaku"
        Me.cnUriKingaku.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnUriKingaku.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnUriKingaku.TabStop = True
        '
        'cnMeisaiBikou
        '
        Me.cnMeisaiBikou.DataPropertyName = "dtMeisaiBikou"
        DataGridViewCellStyle13.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.cnMeisaiBikou.DefaultCellStyle = DataGridViewCellStyle13
        Me.cnMeisaiBikou.HeaderText = "明細備考"
        Me.cnMeisaiBikou.Name = "cnMeisaiBikou"
        Me.cnMeisaiBikou.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnMeisaiBikou.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnMeisaiBikou.TabStop = True
        Me.cnMeisaiBikou.Width = 190
        '
        'cnKonpou
        '
        Me.cnKonpou.DataPropertyName = "dtKonpou"
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle14.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        DataGridViewCellStyle14.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.Black
        Me.cnKonpou.DefaultCellStyle = DataGridViewCellStyle14
        Me.cnKonpou.HeaderText = "梱包"
        Me.cnKonpou.Name = "cnKonpou"
        Me.cnKonpou.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnKonpou.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnKonpou.TabStop = True
        Me.cnKonpou.Width = 50
        '
        'Sample_Chumon
        '
        Me.ClientSize = New System.Drawing.Size(1284, 782)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "Sample_Chumon"
        Me.Text = " 注文業務 (H01F60)"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel23.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        Me.TableLayoutPanel24.ResumeLayout(False)
        Me.TableLayoutPanel24.PerformLayout()
        Me.TableLayoutPanel5.ResumeLayout(False)
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
        Me.TableLayoutPanel12.ResumeLayout(False)
        Me.TableLayoutPanel12.PerformLayout()
        Me.TableLayoutPanel11.ResumeLayout(False)
        Me.TableLayoutPanel11.PerformLayout()
        Me.TableLayoutPanel13.ResumeLayout(False)
        Me.TableLayoutPanel13.PerformLayout()
        Me.TableLayoutPanel25.ResumeLayout(False)
        Me.TableLayoutPanel8.ResumeLayout(False)
        Me.TableLayoutPanel16.ResumeLayout(False)
        Me.TableLayoutPanel21.ResumeLayout(False)
        Me.TableLayoutPanel22.ResumeLayout(False)
        Me.TableLayoutPanel26.ResumeLayout(False)
        CType(Me.dgvIchiran, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Public Event CellEnter As DataGridViewCellEventHandler

    Private Const COMBOBOX_COLUMN As Integer = 1

    Public clickColumnIndex As Integer
    Public clickRowIndex As Integer
    Dim strReVal1 As String = ""
    Dim strReVal2 As String = ""
    Dim strReVal3 As String = ""
    Dim strReVal As String = ""

    'セルをダブルクリックしたとき
    Private Sub dgvIchiran_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvIchiran.CellDoubleClick

        '検索ウインドウオープン
        SelectWindowOpen()

    End Sub

    '検索ウインドウオープン処理
    Private Sub SelectWindowOpen()
        clickColumnIndex = dgvIchiran.CurrentCell.ColumnIndex
        clickRowIndex = dgvIchiran.CurrentCell.RowIndex

        Select Case clickColumnIndex
            Case 1      '商品ＣＤ
                Dim openForm As Sample_Shohin = New Sample_Shohin()      '画面遷移
                openForm.ShowDialog()                      '画面表示
                strReVal1 = openForm.selectVal1               'セルに表示する内容を取得
                strReVal2 = openForm.selectVal2               'セルに表示する内容を取得
                strReVal3 = openForm.selectVal3               'セルに表示する内容を取得
            Case 4      '税
                Dim openForm As Sample_Hanyo = New Sample_Hanyo(clickColumnIndex, clickRowIndex)      '画面遷移
                openForm.ShowDialog()                      '画面表示
                strReVal = openForm.selectVal               'セルに表示する内容を取得
            Case 8      '単位
                Dim openForm As Sample_Hanyo = New Sample_Hanyo(clickColumnIndex, clickRowIndex)      '画面遷移
                openForm.ShowDialog()                      '画面表示
                strReVal = openForm.selectVal               'セルに表示する内容を取得
            Case 12     '梱包
                Dim openForm As Sample_Hanyo = New Sample_Hanyo(clickColumnIndex, clickRowIndex)      '画面遷移
                openForm.ShowDialog()                      '画面表示
                strReVal = openForm.selectVal               'セルに表示する内容を取得
        End Select


        If clickColumnIndex = 1 And strReVal1 <> "" Then
            dgvIchiran.Rows(clickRowIndex).Cells(1).Value = strReVal1
            dgvIchiran.Rows(clickRowIndex).Cells(2).Value = strReVal2
            dgvIchiran.Rows(clickRowIndex).Cells(3).Value = strReVal3
        ElseIf (clickColumnIndex = 4 Or clickColumnIndex = 8 Or clickColumnIndex = 12) And strReVal <> "" Then
            dgvIchiran.Rows(clickRowIndex).Cells(clickColumnIndex).Value = strReVal
        End If

    End Sub

    'キーを押下したら
    Private Sub dgvIchiran_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles dgvIchiran.KeyDown
        'tabの場合は別制御
        'If e.KeyCode = Keys.Tab Then
        '    e.Handled = True
        '    Me.SelectNextControl(Me.DataGridView1, True, True, True, True)
        '    Exit Sub
        'End If

        Select Case e.KeyCode
            Case Keys.Space
                e.Handled = True
                '検索ウインドウオープン
                Select Case dgvIchiran.CurrentCell.ColumnIndex
                    Case 1, 4, 8, 12
                        SelectWindowOpen()
                End Select
                'Case Keys.Enter
                '    'Enterキーを押下したらTabキーと同じ動作をさせる
                '    e.Handled = True
                '    'SendKeys.Send("{TAB}")
                '    If DataGridView1.ColumnCount > DataGridView1.CurrentCell.ColumnIndex + 1 Then
                '        DataGridView1.CurrentCell = DataGridView1(DataGridView1.CurrentCell.ColumnIndex + 1, DataGridView1.CurrentCell.RowIndex)
                '    Else
                '        If DataGridView1.RowCount > DataGridView1.CurrentCell.RowIndex + 1 Then
                '            DataGridView1.CurrentCell = DataGridView1(1, DataGridView1.CurrentCell.RowIndex + 1)
                '        Else
                '            DataGridView1.CurrentCell = DataGridView1(1, DataGridView1.CurrentCell.RowIndex)
                '        End If
                '    End If
                'Case Keys.Enter, Keys.Tab
                '    Dim iRow As Integer = DataGridView1.CurrentCell.RowIndex
                '    Dim iCol As Integer = DataGridView1.CurrentCell.ColumnIndex
                '    Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(DataGridView1)
                '    If iRow = gh.getMaxRow - 1 AndAlso iCol = 12 Then
                '        'アクティブセルが最終行の梱包列の場合、最下行に行追加
                '        DataGridView1.Rows.Add()
                '        DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex + 1).Cells(1)
                '    End If
        End Select
        dgvIchiran.FirstDisplayedScrollingColumnIndex = dgvIchiran.CurrentCell.ColumnIndex

    End Sub


    '伝票日付からフォーカスが外れたら、曜日再取得
    Private Sub imdShukkaDt_Leave(sender As Object, e As System.EventArgs) Handles imdShukkaDt.Leave
        Dim strDenpyoDate As String = imdShukkaDt.Text
        lblShukkaDay.Text = YobiReturn(strDenpyoDate)
    End Sub

    '着日からフォーカスが外れたら、曜日再取得
    Private Sub imdChakuDt_Leave(sender As Object, e As System.EventArgs) Handles imdChakuDt.Leave
        Dim strChakuDate As String = imdChakuDt.Text
        lblChakuDay.Text = YobiReturn(strChakuDate)
    End Sub


    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents Label4 As Label
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents lblDenpyoNo As Label
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents Label1 As Label
    Friend WithEvents imdShukkaDt As CustomControl.TextBoxDate
    Friend WithEvents Label2 As Label
    Friend WithEvents cmbUriageKbn As ComboBox

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
    Friend WithEvents Label27 As Label
    Friend WithEvents Label28 As Label
    Friend WithEvents lblTotal As Label
    Friend WithEvents lblShouhizei As Label
    Friend WithEvents lblZeikomi As Label
    Friend WithEvents Label32 As Label



    Friend WithEvents Column13 As DataGridViewTextBoxColumn
    Friend WithEvents Column14 As DataGridViewTextBoxColumn


    Friend WithEvents lblShoriMode As Label
    Friend WithEvents TableLayoutPanel23 As TableLayoutPanel
    Friend WithEvents lblShukkaDay As Label
    Friend WithEvents TableLayoutPanel24 As TableLayoutPanel
    Friend WithEvents imdChakuDt As CustomControl.TextBoxDate
    Friend WithEvents Label34 As Label
    Friend WithEvents lblChakuDay As Label
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel25 As TableLayoutPanel
    Friend WithEvents Label36 As Label
    Friend WithEvents lblMeisaiCnt As Label

    '登録ボタン押下時
    Private Sub cmdTouroku_Click(sender As Object, e As EventArgs) Handles cmdTouroku.Click
        Me.Close()
    End Sub

    '戻るボタン押下時
    Private Sub cmdModoru_Click(sender As Object, e As EventArgs) Handles cmdModoru.Click
        Me.Close()
    End Sub

    Friend WithEvents TableLayoutPanel26 As TableLayoutPanel
    Friend WithEvents cmdDelRow As Button
    Friend WithEvents cmdAddRow As Button
    Friend WithEvents cmdTopRow As Button

    '再印刷ボタン押下時
    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        Me.Close()
    End Sub

    '先頭へボタン押下時
    Private Sub cmdTopRow_Click(sender As Object, e As EventArgs) Handles cmdTopRow.Click
        dgvIchiran.CurrentCell = dgvIchiran.Rows(dgvIchiran.CurrentCell.RowIndex).Cells(1)
    End Sub
    '行追加ボタン押下時
    Private Sub cmdAddRow_Click(sender As Object, e As EventArgs) Handles cmdAddRow.Click
        'dgvIchiran.Rows.Add()
        'dgvIchiran.CurrentCell = dgvIchiran.Rows(dgvIchiran.CurrentCell.RowIndex + 1).Cells(1)
        If TypeOf Me.dgvIchiran.DataSource Is DataSet Then
            Dim ds As DataSet = TryCast(Me.dgvIchiran.DataSource, DataSet)
            ds.Tables("RecSet").Rows.Add()
        ElseIf TypeOf Me.dgvichiran.DataSource Is DataTable Then
            Dim dt As DataTable = TryCast(Me.dgvIchiran.DataSource, DataTable)
            dt.Rows.Add()
        End If
    End Sub
    '行削除ボタン押下時
    Private Sub cmdDelRow_Click(sender As Object, e As EventArgs) Handles cmdDelRow.Click
        'If dgvIchiran.RowCount > 0 Then
        '    clickRowIndex = dgvIchiran.CurrentCell.RowIndex
        '    dgvIchiran.Rows.RemoveAt(clickRowIndex)
        'End If
        Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvIchiran)
        If gh.getMaxRow > 0 Then
            If TypeOf Me.dgvIchiran.DataSource Is DataSet Then
                Dim ds As DataSet = TryCast(Me.dgvIchiran.DataSource, DataSet)
                ds.Tables("RecSet").Rows.RemoveAt(Me.dgvIchiran.CurrentCell.RowIndex)
            ElseIf TypeOf Me.dgvIchiran.DataSource Is DataTable Then
                Dim dt As DataTable = TryCast(Me.dgvIchiran.DataSource, DataTable)
                dt.Rows.RemoveAt(Me.dgvIchiran.CurrentCell.RowIndex)
            End If
        End If
    End Sub

    Private Sub imdShukkaDt_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles imdShukkaDt.KeyPress,
                                                                                                            imdChakuDt.KeyPress,
                                                                                                            cmbUriageKbn.KeyPress,
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

        UtilMDL.UtilClass.moveNextFocus(Me, e)
    End Sub


    'Private Sub Sample_Chumon_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
    '    If e.KeyCode = Keys.Enter Then
    '        e.Handled = True
    '        SendKeys.Send("{TAB}")
    '        Exit Sub
    '    End If

    'End Sub

    Friend WithEvents dgvIchiran As CustomTabStopDataGridView.TabStop.UtilTabStopDataGridView
    Friend WithEvents cnNo As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnItemCd As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnItemNm As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnNisugata As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnZeiKbn As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnIrisuu As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnKosuu As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnSuryou As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnTanni As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnUriTanka As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnUriKingaku As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnMeisaiBikou As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnKonpou As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn



    '-------------------------------------------------------------------------------
    '   画面初期化
    '   （処理概要）画面項目を初期設定する
    '   ●入力パラメタ  ：なし
    '   ●出力パラメタ  ：なし
    '   ●メソッド戻り値：なし
    '-------------------------------------------------------------------------------
    'Private Sub initForm()

    '    '計画年月/処理年月表示
    '    Call getKeikakuKanriTblRec(lblKeikaku.Text, lblSyori.Text)

    '    '実行日時表示
    '    With updFlg
    '        '１シート目-----
    '        Call getExecuteDt(lblSyokisettei, btnSyokisettei, .updFlgSyokisettei)              '初期設定
    '        Call getExecuteDt(lblSetteitisyuusei, btnSetteitisyuusei, .updFlgSetteitisyuusei)  '希望出来日修正
    '        Call getExecuteDt(lblTDTorikomi, btnTDTorikomi, .updFlgTDTorikomi)                 '手配済ﾃﾞｰﾀ取込
    '        Call getExecuteDt(lblNDTorikomi, btnNDTorikomi, .updFlgNDTorikomi)                 '入庫済ﾃﾞｰﾀ取込
    '        Call getExecuteDt(lblSDSyuusei, btnSDSyuusei, .updFlgSDSyuusei)                    '生産量ﾃﾞｰﾀ修正
    '        Call getExecuteDt(lblSeisanKakutei, btnSeisanKakutei, .updFlgSeisanKakutei)        '生産量確定
    '        Call getExecuteDt(lblHKNyuryoku, btnHKNyuryoku, .updFlgHKNyuryoku)                 '品種別計画入力
    '        Call getExecuteDt(lblKKNyuroku, btnKKNyuroku, .updFlgKKNyuroku)                    '個別計画入力
    '        Call getExecuteDt(lblHJTorikomi, btnHJTorikomi, .updFlgHJTorikomi)                 '販売実績取込
    '        Call getExecuteDt(lblSyuukeiTenkai, btnSyuukeiTenkai, .updFlgSyuukeiTenkai)        '販売計画集計展開
    '        Call getExecuteDt(lblHKSyuusei, btnHKSyuusei, .updFlgHKSyuusei)                    '販売計画量修正
    '        Call getExecuteDt(lblSKakutei, btnSKakutei, .updFlgSKakutei)                       '販売計画確定
    '        Call getExecuteDt(lblZaikoTorikomi, btnZaikoTorikomi, .updFlgZaikoTorikomi)        '在庫実績取込
    '        Call getExecuteDt(lblSHZTorikomi, btnSHZTorikomi, .updFlgSHZTorikomi)              '生産販売在庫取込
    '        Call getExecuteDt(lblSKSyuusei, btnSKSyuusei, .updFlgSKSyuusei)                    '生産計画数量修正
    '        Call getExecuteDt(lblKakutei, btnKakutei, .updFlgKakutei)                          '生産計画確定
    '        Call getExecuteDt(lblTDSakusei, btnTDSakusei, .updFlgTDSakusei)                    '手配ﾃﾞｰﾀ作成
    '        Call getExecuteDt(lblTDSyuusei, btnTDSyuusei, .updFlgTDSyuusei)                    '手配ﾃﾞｰﾀ修正・出力
    '        Call getExecuteDt(lblTDSousin, btnTDSousin, .updFlgTDSousin)                       '手配ﾃﾞｰﾀ作成(生産管理ｼｽﾃﾑ送信用								)
    '        Call getExecuteDt(lblFYamadumi, btnFYamadumi, .updFlgFYamadumi)                    '負荷山積データ取込
    '        Call getExecuteDt(lblKKakunin, btnKKakunin, .updFlgKKakunin)                       '負荷山積集計結果確認
    '        Call getExecuteDt(lblSTDB, btnSTDB, .updFlgSTDB)                                   '製作手配DB登録

    '        '-->2010.12.17 add by takagi #16
    '        '同一タブ内での最新日付を取得する
    '        Dim latestDt As String = ""
    '        For Each ctl As Control In tabGeturei.Controls                               '同一タブ内のコントロールをループ
    '            Dim l As System.Windows.Forms.Label = TryCast(ctl, System.Windows.Forms.Label)
    '            If l IsNot Nothing Then                                                         '取得コントロールがLabelか判断
    '                If IsDate(l.Text) Then                                                      '日付以外のコントロールはスキップ
    '                    If "".Equals(latestDt) Then latestDt = l.Text '                          初回ループ時は無条件格納
    '                    If CDate(latestDt) < CDate(l.Text) Then                                 '保持している最新日付よりも新しいか？
    '                        latestDt = l.Text                                                   'その場合はより新しい日付を保持
    '                    End If
    '                End If
    '            End If
    '        Next
    '        For Each ctl As Control In tabGeturei.Controls                               '同一タブ内のコントロールをループ
    '            Dim l As System.Windows.Forms.Label = TryCast(ctl, System.Windows.Forms.Label)
    '            If l IsNot Nothing Then                                                         '取得コントロールがLabelか判断
    '                'ラベル
    '                If NON_EXECUTE.Equals(l.Text) OrElse IsDate(l.Text) Then
    '                    '日付ラベル
    '                    Select Case True
    '                        Case NON_EXECUTE.Equals(l.Text) : l.ForeColor = Color.Black
    '                        Case CDate(latestDt) <= CDate(l.Text) : l.ForeColor = Color.Red
    '                        Case Else : l.ForeColor = Color.Blue
    '                    End Select
    '                End If
    '            End If
    '        Next
    '        '<--2010.12.17 add by takagi #16

    '        '２シート目-----                                            
    '        '-->2010.12.02 upd by takagi
    '        'Call getExecuteDt(lblTSakujo, btnSinki, .updFlgSinki)                              '新規登録
    '        Call getExecuteDt(lblShinki, btnSinki, .updFlgSinki)                              '新規登録
    '        '<--2010.12.02 upd by takagi
    '        Call getExecuteDt(lblSyuusei, btnSyuusei, .updFlgSyuusei)                          '修正・EXCEL出力
    '        '-->2010.12.02 upd by takagi
    '        'Call getExecuteDt(lblTSakujo, btnSakujo, .updFlgSakujo)                            '削除
    '        Call getExecuteDt(lblM11Del, btnSakujo, .updFlgSakujo)                            '削除
    '        '<--2010.12.02 upd by takagi
    '        Call getExecuteDt(lblKExcel, btnKExcel, .updFlgKExcel)                             '計画対象品一覧表印刷
    '        Call getExecuteDt(lblABC, btnABC, .updFlgABC)                                      'ABC分析
    '        Call getExecuteDt(lblHMstMente, btnHMstMente, .updFlgHMstMente)                    '品種区分マスタメンテ
    '        Call getExecuteDt(lblHanyoMst, btnHanyoMst, .updFlgHanyoMst)                       '汎用マスタメンテ
    '        Call getExecuteDt(lblSNouryokuMst, btnSNouryokuMst, .updFlgSNouryokuMst)           '生産能力マスタメンテ
    '        Call getExecuteDt(lblGRenkei, btnGRenkei, .updFlgGRenkei)                          '外部システム連携

    '    End With

    'End Sub

    '-------------------------------------------------------------------------------
    '   計画/処理年月の取得
    '   （処理概要）計画管理ＴＢＬから計画年月と処理年月を取得する
    '   ●入力パラメタ  ：なし
    '   ●出力パラメタ  ：prmRefSyoriYM     取得済処理年月
    '                     prmRefKeikakuYM   取得済計画年月
    '   ●メソッド戻り値：なし
    '-------------------------------------------------------------------------------
    'Private Sub getKeikakuKanriTblRec(ByRef prmRefSyoriYM As String, ByRef prmRefKeikakuYM As String)
    '    Try
    '        '初期化
    '        prmRefSyoriYM = ""              '処理年月
    '        prmRefKeikakuYM = ""            '計画年月

    '        '計画管理TBL検索
    '        Dim sql As String = ""
    '        Dim iRecCnt As Integer = 0
    '        sql = sql & N & " SELECT "
    '        sql = sql & N & "  SNENGETU "
    '        sql = sql & N & " ,KNENGETU "
    '        sql = sql & N & " FROM T01KEIKANRI "
    '        Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)
    '        If iRecCnt <> 1 Then Throw New UsrDefException("計画管理ＴＢＬのレコード構成が不正です。(" & iRecCnt & "件)")

    '        '返却値編集
    '        prmRefSyoriYM = _db.rmNullStr(ds.Tables(RS).Rows(0)("SNENGETU"))
    '        If Not "".Equals(prmRefSyoriYM) Then prmRefSyoriYM = prmRefSyoriYM.Substring(0, 4) & "/" & prmRefSyoriYM.Substring(4)
    '        prmRefKeikakuYM = _db.rmNullStr(ds.Tables(RS).Rows(0)("KNENGETU"))
    '        If Not "".Equals(prmRefKeikakuYM) Then prmRefKeikakuYM = prmRefKeikakuYM.Substring(0, 4) & "/" & prmRefKeikakuYM.Substring(4)

    '    Catch ue As UsrDefException         'ユーザー定義例外
    '        Call ue.dspMsg()
    '        Throw ue                        'キャッチした例外をそのままスロー
    '    Catch ex As Exception               'システム例外
    '        Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
    '    End Try

    'End Sub

    '-------------------------------------------------------------------------------
    '   計画/処理年月の取得
    '   （処理概要）処理制御TBLから機能IDの処理終了日時を取得すると共に、その機能の使用可否と更新可否を判定する
    '   ●入力パラメタ  ：prmExecBtn        押下ボタン(Tagプロパティに該当する機能IDを設定していること)
    '   ●出力パラメタ  ：prmRefUpdatable   更新可否(該当ボタンより起動される機能が更新権限を保有するか否か
    '   ●メソッド戻り値：処理終了日時
    '-------------------------------------------------------------------------------
    'Private Sub getExecuteDt(ByRef prmRefLabel As Label, ByVal prmExecBtn As Button, ByRef prmRefUpdatable As Boolean)
    '    Dim ret As String = ""
    '    Try
    '        'パラメタチェック
    '        If "".Equals(prmExecBtn.Tag) Then Throw New UsrDefException("押下ボタンのTagプロパティが未設定です。" & N & "Tagプロパティに機能IDを正しく設定してください。")

    '        '初期化
    '        prmExecBtn.Enabled = False
    '        prmRefUpdatable = False

    '        '処理終了日時の取得-----
    '        Dim iRecCnt As Integer = 0
    '        Dim ds As DataSet = _db.selectDB("SELECT SDATEEND FROM T02SEIGYO WHERE PGID = '" & _db.rmSQ(prmExecBtn.Tag) & "'", RS, iRecCnt)
    '        If iRecCnt <> 1 Then Throw New UsrDefException("処理制御ＴＢＬに該当機能のレコードが見つかりません。(" & prmExecBtn.Tag & ")")

    '        '-->2010.12.17 chg by takagi #16
    '        'ret = _db.rmNullDate(ds.Tables(RS).Rows(0)("SDATEEND"), "yyyy/MM/dd HH:mm")
    '        ret = _db.rmNullDate(ds.Tables(RS).Rows(0)("SDATEEND"), "yyyy/MM/dd HH:mm:ss")
    '        '<--2010.12.17 chg by takagi #16
    '        If "".Equals(ret) Then ret = NON_EXECUTE


    '        '先行ジョブの判定-------
    '        ds = _db.selectDB("SELECT BEFOREJOB_ID FROM M81BEFOREJOB WHERE PGID = '" & _db.rmSQ(prmExecBtn.Tag) & "'", RS, iRecCnt)
    '        If iRecCnt <= 0 Then
    '            '先行ジョブ定義なし→起動可能
    '            prmExecBtn.Enabled = True
    '        Else
    '            '先行ジョブ定義あり
    '            Dim wkCnt As Integer = 0
    '            Dim wkDs As DataSet = Nothing
    '            Dim wkPgId As String = ""
    '            Dim execCnt As Integer = 0
    '            For i As Integer = 0 To iRecCnt - 1
    '                '先行ジョブごとに実行済か判定
    '                wkPgId = _db.rmNullStr(ds.Tables(RS).Rows(i)("BEFOREJOB_ID"))
    '                wkDs = _db.selectDB("SELECT SDATEEND FROM T02SEIGYO WHERE PGID = '" & _db.rmSQ(wkPgId) & "'", RS, wkCnt)
    '                If Not "".Equals(_db.rmNullDate(wkDs.Tables(RS).Rows(0)("SDATEEND"))) Then
    '                    execCnt += 1                            '処理済
    '                End If
    '            Next
    '            If execCnt = iRecCnt Then
    '                prmExecBtn.Enabled = True                   '全て処理済→起動可能
    '            End If
    '        End If

    '        '後続ジョブの判定-------
    '        If prmExecBtn.Enabled Then                          '起動可能の場合のみ更新可否を判断する
    '            ds = _db.selectDB("SELECT AFTERJOB_ID FROM M82AFTERJOB WHERE PGID = '" & _db.rmSQ(prmExecBtn.Tag) & "'", RS, iRecCnt)
    '            If iRecCnt <= 0 Then
    '                '後続ジョブ定義なし→更新可能
    '                prmRefUpdatable = True
    '            Else
    '                '後続ジョブ定義あり
    '                prmRefUpdatable = True
    '                Dim wkCnt As Integer = 0
    '                Dim wkDs As DataSet = Nothing
    '                Dim wkPgId As String = ""
    '                Dim execCnt As Integer = 0
    '                For i As Integer = 0 To iRecCnt - 1
    '                    '後続ジョブごとに実行済か判定
    '                    wkPgId = _db.rmNullStr(ds.Tables(RS).Rows(i)("AFTERJOB_ID"))
    '                    wkDs = _db.selectDB("SELECT SDATEEND FROM T02SEIGYO WHERE PGID = '" & _db.rmSQ(wkPgId) & "'", RS, wkCnt)
    '                    If Not "".Equals(_db.rmNullDate(wkDs.Tables(RS).Rows(0)("SDATEEND"))) Then
    '                        prmRefUpdatable = False             '処理済
    '                        Exit For                            '一つでも処理済があればその時点で更新不可
    '                    End If
    '                Next
    '            End If
    '        End If

    '        'デバッグ用に更新権限をボタンテキストに表示
    '        If StartUp.DebugMode Then
    '            lblDebugDsp.Visible = True
    '            prmExecBtn.Text = System.Text.RegularExpressions.Regex.Replace(prmExecBtn.Text, "\[.*\]", "") & "[" & prmRefUpdatable & "]"
    '        End If

    '    Catch ue As UsrDefException         'ユーザー定義例外
    '        Call ue.dspMsg()
    '        Throw ue                        'キャッチした例外をそのままスロー
    '    Catch ex As Exception               'システム例外
    '        Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
    '    End Try
    '    prmRefLabel.Text = ret
    '    '-->2010.12.17 add by takagi #16
    '    'If Not prmExecBtn.Enabled Then prmRefLabel.Text = NON_EXECUTE
    '    '<--2010.12.17 add by takagi #16
    '    If Not NON_EXECUTE.Equals(prmRefLabel.Text) Then prmRefLabel.ForeColor = Color.Blue

    'End Sub

    '-------------------------------------------------------------------------------
    '   処理制御TBL更新
    '   （処理概要）処理制御TBL(T02SEIGYO)の処理実行日時を設定する
    '   ●入力パラメタ  ：prmPgId       処理制御TBLのレコード特定に使用する機能ID
    '                     prmRunFlg     実行かキャンセルかを示すフラグ(キャンセル：確定解除等に使用)
    '                     [prmStartDt]  処理開始日時(キャンセル時は未使用)
    '                     [prmEndDt]    処理終了日時(キャンセル時は未使用)
    '   ●出力パラメタ  ：なし
    '   ●メソッド戻り値：なし
    '-------------------------------------------------------------------------------
    'Public Sub updateSeigyoTbl(ByVal prmPgId As String, ByVal prmRunFlg As Boolean, Optional ByVal prmStartDt As Date = Nothing, Optional ByVal prmEndDt As Date = Nothing)
    '    Try
    '        'パラメタチェック
    '        If prmRunFlg AndAlso (prmStartDt = #12:00:00 AM# OrElse prmEndDt = #12:00:00 AM#) Then
    '            Throw New UsrDefException("実行処理(prmRunFlg=True)の場合は処理開始日時(prmStartDt)・処理終了日時(prmEndDt)が必須です。")
    '        End If

    '        '制御テーブル更新
    '        Dim sql As String = ""
    '        Dim affectedRows As Integer = 0
    '        sql = sql & N & "UPDATE T02SEIGYO SET "
    '        If prmRunFlg Then
    '            sql = sql & N & "SDATESTART = TO_DATE('" & Format(prmStartDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS'), "
    '            sql = sql & N & "SDATEEND   = TO_DATE('" & Format(prmEndDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS'), "
    '        Else
    '            sql = sql & N & "SDATESTART = NULL, "
    '            sql = sql & N & "SDATEEND   = NULL, "
    '        End If
    '        sql = sql & N & "UPDNAME = '" & _db.rmSQ(UtilClass.getComputerName()) & "', "
    '        sql = sql & N & "UPDDATE = TO_DATE('" & Format(Now, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "
    '        sql = sql & N & "WHERE PGID = '" & _db.rmSQ(prmPgId) & "' "
    '        _db.executeDB(sql, affectedRows)
    '        If affectedRows <= 0 Then
    '            Throw New UsrDefException("制御TBLのレコード構成が不正です。(" & prmPgId & "非存在)")
    '        End If

    '        'メニュー画面再描画
    '        Call initForm()

    '    Catch ue As UsrDefException         'ユーザー定義例外
    '        Call ue.dspMsg()
    '        Throw ue                        'キャッチした例外をそのままスロー
    '    Catch ex As Exception               'システム例外
    '        Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
    '    End Try
    'End Sub

    ''------------------------------------------------------------------------------------------------------
    ''　終了ボタン押下
    ''------------------------------------------------------------------------------------------------------
    'Private Sub btnEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnd.Click

    '    '画面クローズ
    '    Me.Close()

    'End Sub

    '    '------------------------------------------------------------------------------------------------------
    '    '　初期設定ボタン押下
    '    '------------------------------------------------------------------------------------------------------
    '    Private Sub btnSyokisettei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSyokisettei.Click

    '        Dim openForm As ZG110B_Junbi = New ZG110B_Junbi(_msgHd, _db, Me, ZG110B_Junbi.BOOTMODE_INIT, updFlg.updFlgSyokisettei)      '画面遷移
    '        openForm.Show()                                                             '画面表示
    '        Me.Hide()

    '    End Sub

    '    '------------------------------------------------------------------------------------------------------
    '    '　希望出来日ボタン押下
    '    '------------------------------------------------------------------------------------------------------
    '    Private Sub btnSetteitisyuusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetteitisyuusei.Click

    '        Dim openForm As ZG110B_Junbi = New ZG110B_Junbi(_msgHd, _db, Me, ZG110B_Junbi.BOOTMODE_UPD, updFlg.updFlgSetteitisyuusei)      '画面遷移
    '        openForm.Show()                                                             '画面表示
    '        Me.Hide()

    '    End Sub

    '    '------------------------------------------------------------------------------------------------------
    '    '　手配済データ登録ボタン押下
    '    '------------------------------------------------------------------------------------------------------
    '    Private Sub btnTDTorikomi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTDTorikomi.Click

    '        Dim openForm As ZG210E_SeisanHanei = New ZG210E_SeisanHanei(_msgHd, _db, Me, ZG210E_SeisanHanei.TEHAI, updFlg.updFlgTDTorikomi)      '画面遷移
    '        openForm.Show()                                                             '画面表示
    '        Me.Hide()

    '    End Sub

    '    '------------------------------------------------------------------------------------------------------
    '    '　入庫済データ登録ボタン押下
    '    '------------------------------------------------------------------------------------------------------
    '    Private Sub btnNDTorikomi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNDTorikomi.Click

    '        Dim openForm As ZG210E_SeisanHanei = New ZG210E_SeisanHanei(_msgHd, _db, Me, ZG210E_SeisanHanei.NYUKO, updFlg.updFlgNDTorikomi)      '画面遷移
    '        openForm.Show()                                                             '画面表示
    '        Me.Hide()

    '    End Sub

    '    '------------------------------------------------------------------------------------------------------
    '    '　生産量データ修正ボタン押下
    '    '------------------------------------------------------------------------------------------------------
    '    Private Sub btnSDSyuusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSDSyuusei.Click

    '        Dim openForm As ZG220E_SeisanSyusei = New ZG220E_SeisanSyusei(_msgHd, _db, Me, updFlg.updFlgSDSyuusei)      '画面遷移
    '        openForm.Show()                                                             '画面表示
    '        Me.Hide()

    '    End Sub

    '    '------------------------------------------------------------------------------------------------------
    '    '　生産量確定ボタン押下
    '    '------------------------------------------------------------------------------------------------------
    '    Private Sub btnSeisanKakutei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeisanKakutei.Click

    '        Dim openForm As ZG230B_SeisanryouKakutei = New ZG230B_SeisanryouKakutei(_msgHd, _db, Me, updFlg.updFlgSeisanKakutei)      '画面遷移
    '        openForm.Show()                                                             '画面表示
    '        Me.Hide()

    '    End Sub

    '    '------------------------------------------------------------------------------------------------------
    '    '　品種別計画入力ボタン押下
    '    '------------------------------------------------------------------------------------------------------
    '    Private Sub btnHKNyuryoku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHKNyuryoku.Click

    '        Dim openForm As ZG310E_Hinsyubetu = New ZG310E_Hinsyubetu(_msgHd, _db, Me, updFlg.updFlgHKNyuryoku)      '画面遷移
    '        openForm.Show()                                                             '画面表示
    '        Me.Hide()

    '    End Sub

    '    '------------------------------------------------------------------------------------------------------
    '    '　個別計画入力ボタン押下
    '    '------------------------------------------------------------------------------------------------------
    '    Private Sub btnKKNyuroku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKKNyuroku.Click

    '        Dim openForm As ZG320E_KobetuNyuuroku = New ZG320E_KobetuNyuuroku(_msgHd, _db, Me, updFlg.updFlgKKNyuroku)      '画面遷移
    '        openForm.Show()                                                             '画面表示
    '        Me.Hide()

    '    End Sub

    '    '------------------------------------------------------------------------------------------------------
    '    '　販売実績取込ボタン押下
    '    '------------------------------------------------------------------------------------------------------
    '    Private Sub btnHJTorikomi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHJTorikomi.Click

    '        Dim openForm As ZG330B_HJissekiTorikomi = New ZG330B_HJissekiTorikomi(_msgHd, _db, Me, updFlg.updFlgHJTorikomi)      '画面遷移
    '        openForm.Show()                                                             '画面表示
    '        Me.Hide()

    '    End Sub

    '    '------------------------------------------------------------------------------------------------------
    '    '　販売計画集計展開ボタン押下
    '    '------------------------------------------------------------------------------------------------------
    '    Private Sub btnSyuukeiTenkai_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSyuukeiTenkai.Click

    '        Dim openForm As ZG340B_HJissekiTenkai = New ZG340B_HJissekiTenkai(_msgHd, _db, Me, updFlg.updFlgSyuukeiTenkai)      '画面遷移
    '        openForm.Show()                                                             '画面表示
    '        Me.Hide()

    '    End Sub

    '    '------------------------------------------------------------------------------------------------------
    '    '　販売計画量修正ボタン押下
    '    '------------------------------------------------------------------------------------------------------
    '    Private Sub btnHKSyuusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHKSyuusei.Click

    '        Dim openForm As ZG350E_KeikakuryouHosei = New ZG350E_KeikakuryouHosei(_msgHd, _db, Me, updFlg.updFlgHKSyuusei)      '画面遷移
    '        openForm.Show()                                                             '画面表示
    '        Me.Hide()

    '    End Sub

    '    '------------------------------------------------------------------------------------------------------
    '    '　販売計画量確定ボタン押下
    '    '------------------------------------------------------------------------------------------------------
    '    Private Sub btnSKakutei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSKakutei.Click

    '        Dim openForm As ZG360B_HKeikakuKakutei = New ZG360B_HKeikakuKakutei(_msgHd, _db, Me, updFlg.updFlgSKakutei)      '画面遷移
    '        openForm.Show()                                                             '画面表示
    '        Me.Hide()

    '    End Sub

    '    '------------------------------------------------------------------------------------------------------
    '    '　在庫実績取込ボタン押下
    '    '------------------------------------------------------------------------------------------------------
    '    Private Sub btnZaikoTorikomi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZaikoTorikomi.Click

    '        Dim openForm As ZG410B_ZJissekiTorikomi = New ZG410B_ZJissekiTorikomi(_msgHd, _db, Me, updFlg.updFlgZaikoTorikomi)      '画面遷移
    '        openForm.Show()                                                             '画面表示
    '        Me.Hide()

    '    End Sub

    '    '------------------------------------------------------------------------------------------------------
    '    '　生産販売在庫取込ボタン押下
    '    '------------------------------------------------------------------------------------------------------
    '    Private Sub btnSHZTorikomi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSHZTorikomi.Click

    '        Dim openForm As ZG510B_SHZTorikomiIkkatu = New ZG510B_SHZTorikomiIkkatu(_msgHd, _db, Me, updFlg.updFlgSHZTorikomi)      '画面遷移
    '        openForm.Show()                                                             '画面表示
    '        Me.Hide()

    '    End Sub

    '    '------------------------------------------------------------------------------------------------------
    '    '　生産計画数量修正ボタン押下
    '    '------------------------------------------------------------------------------------------------------
    '    Private Sub btnSKSyuusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSKSyuusei.Click

    '        Dim openForm As ZG530E_SeisanSuuryouSyuusei = New ZG530E_SeisanSuuryouSyuusei(_msgHd, _db, Me, updFlg.updFlgSKSyuusei)      '画面遷移
    '        openForm.Show()                                                             '画面表示
    '        Me.Hide()

    '    End Sub

    '    '------------------------------------------------------------------------------------------------------
    '    '　生産計画確定ボタン押下
    '    '------------------------------------------------------------------------------------------------------
    '    Private Sub btnKakutei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKakutei.Click

    '        Dim openForm As ZG540B_SKeikakuKakutei = New ZG540B_SKeikakuKakutei(_msgHd, _db, Me, updFlg.updFlgKakutei)      '画面遷移
    '        openForm.Show()                                                             '画面表示
    '        Me.Hide()

    '    End Sub

    '    '------------------------------------------------------------------------------------------------------
    '    '　手配データ作成ボタン押下
    '    '------------------------------------------------------------------------------------------------------
    '    Private Sub btnTDSakusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTDSakusei.Click

    '        Dim openForm As ZG610B_TehaiDateSakusei = New ZG610B_TehaiDateSakusei(_msgHd, _db, Me, updFlg.updFlgTDSakusei)      '画面遷移
    '        openForm.Show()                                                             '画面表示
    '        Me.Hide()

    '    End Sub

    '    '------------------------------------------------------------------------------------------------------
    '    '　手配データ修正・出力ボタン押下
    '    '------------------------------------------------------------------------------------------------------
    '    Private Sub btnTDSyuusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTDSyuusei.Click

    '        Dim openForm As ZG620E_TehaiSyuuseiItiran = New ZG620E_TehaiSyuuseiItiran(_msgHd, _db, Me, updFlg.updFlgTDSyuusei)      '画面遷移
    '        openForm.Show()                                                             '画面表示
    '        Me.Hide()

    '    End Sub

    '    '------------------------------------------------------------------------------------------------------
    '    '　手配データ作成(生産管理システム送信用)ボタン押下
    '    '------------------------------------------------------------------------------------------------------
    '    Private Sub btnTDSousin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTDSousin.Click

    '        Dim openForm As ZG630B_TehaiSakuseiSeisan = New ZG630B_TehaiSakuseiSeisan(_msgHd, _db, Me, updFlg.updFlgTDSousin)      '画面遷移
    '        openForm.Show()                                                             '画面表示
    '        Me.Hide()

    '    End Sub

    '    '------------------------------------------------------------------------------------------------------
    '    '　負荷山積データ取込ボタン押下
    '    '------------------------------------------------------------------------------------------------------
    '    Private Sub btnFYamadumi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFYamadumi.Click

    '        Dim openForm As ZG720B_FukaYamadumiTorikomi = New ZG720B_FukaYamadumiTorikomi(_msgHd, _db, Me, updFlg.updFlgFYamadumi)      '画面遷移
    '        openForm.Show()                                                             '画面表示
    '        Me.Hide()

    '    End Sub

    '    '------------------------------------------------------------------------------------------------------
    '    '　負荷山積集計結果確認ボタン押下
    '    '------------------------------------------------------------------------------------------------------
    '    Private Sub btnKKakunin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKKakunin.Click

    '        Dim openForm As ZG730Q_FukaYamadumiKoutei = New ZG730Q_FukaYamadumiKoutei(_msgHd, _db, Me, updFlg.updFlgKKakunin)      '画面遷移
    '        openForm.Show()                                                             '画面表示
    '        Me.Hide()

    '    End Sub

    '    '------------------------------------------------------------------------------------------------------
    '    '　製作手配DB登録ボタン押下
    '    '------------------------------------------------------------------------------------------------------
    '    Private Sub btnSTDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSTDB.Click

    '        Dim openForm As ZG640B_SeisakuTehaiDB = New ZG640B_SeisakuTehaiDB(_msgHd, _db, Me, updFlg.updFlgSTDB)      '画面遷移
    '        openForm.Show()                                                             '画面表示
    '        Me.Hide()

    '    End Sub

    '    '------------------------------------------------------------------------------------------------------
    '    '　新規登録ボタン押下
    '    '------------------------------------------------------------------------------------------------------
    '    Private Sub btnTSakujo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSinki.Click

    '        '-->2010.12.02 add by takagi
    '        'Dim openForm As ZM110E_Sinki = New ZM110E_Sinki(_msgHd, _db, Me, updFlg.updFlgSinki, True)      'パラメタを遷移先画面へ渡す
    '        Dim openForm As ZM110E_Sinki = New ZM110E_Sinki(_msgHd, _db, Me, updFlg.updFlgSinki, True, btnSinki.Tag)      'パラメタを遷移先画面へ渡す
    '        '<--2010.12.02 add by takagi
    '        openForm.Show()                                                             '画面表示
    '        Me.Hide()

    '    End Sub

    '    '------------------------------------------------------------------------------------------------------
    '    '　修正・EXCEL出力押下
    '    '------------------------------------------------------------------------------------------------------
    '    Private Sub btnSyuusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSyuusei.Click

    '        Dim openForm As ZM120E_Syuusei = New ZM120E_Syuusei(_msgHd, _db, Me, updFlg.updFlgSyuusei)      'パラメタを遷移先画面へ渡す
    '        openForm.Show()                                                             '画面表示
    '        Me.Hide()

    '    End Sub

    '    '------------------------------------------------------------------------------------------------------
    '    '　削除ボタン押下
    '    '------------------------------------------------------------------------------------------------------
    '    Private Sub btnSakujo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSakujo.Click

    '        '-->2010.12.02 add by takagi
    '        'Dim openForm As ZM110E_Sinki = New ZM110E_Sinki(_msgHd, _db, Me, updFlg.updFlgSinki, False)      'パラメタを遷移先画面へ渡す
    '        Dim openForm As ZM110E_Sinki = New ZM110E_Sinki(_msgHd, _db, Me, updFlg.updFlgSinki, False, btnSakujo.Tag)      'パラメタを遷移先画面へ渡す
    '        '<--2010.12.02 add by takagi
    '        openForm.Show()                                                             '画面表示
    '        Me.Hide()

    '    End Sub

    '#Region "計画対象品一覧表印刷ボタン押下　計画対商品一覧印刷"
    '    '------------------------------------------------------------------------------------------------------
    '    '　計画対象品一覧表印刷ボタン押下
    '    '------------------------------------------------------------------------------------------------------
    '    Private Sub btnKExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKExcel.Click
    '        Try
    '            'マウスカーソル砂時計
    '            Me.Cursor = Cursors.WaitCursor

    '            '印刷
    '            Dim startPrintTime As Date = Now

    '            'EXCEL出力
    '            Call printExcel()

    '            Dim endPrintTime As Date = Now

    '            '制御テーブル更新
    '            Call updateSeigyoTbl(ZM130P_PGID, True, startPrintTime, endPrintTime)

    '        Catch ue As UsrDefException
    '            ue.dspMsg()
    '        Catch ex As Exception
    '            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
    '        Finally
    '            'マウスカーソル元に戻す
    '            Me.Cursor = Cursors.Default
    '        End Try
    '    End Sub

    '    '------------------------------------------------------------------------------------------------------
    '    '　計画対象品一覧出力
    '    '------------------------------------------------------------------------------------------------------
    '    Private Sub printExcel()
    '        Try
    '            Dim pb As UtilProgressBar = New UtilProgressBar(Me)
    '            Try
    '                pb.Show()

    '                'プログレスバー設定
    '                pb.jobName = "出力を準備しています。"
    '                pb.status = "初期化中．．．"

    '                '雛形ファイル(品名別販売計画と同じ雛形)
    '                Dim openFilePath As String = StartUp.iniValue.BaseXlsPath & "\" & StartUp.iniValue.ExcelZM130R1_Base
    '                '雛形ファイルが開かれていないかチェック
    '                Dim fh As UtilFile = New UtilFile()
    '                Try
    '                    fh.move(openFilePath, openFilePath & 1)
    '                    fh.move(openFilePath & 1, openFilePath)
    '                Catch ioe As System.IO.IOException
    '                    Throw New UsrDefException("ファイルが開かれています。ファイルを閉じてから再実行してください。",
    '                                              _msgHd.getMSG("fileOpenErr", "【ファイル名】：" & openFilePath))
    '                End Try

    '                '出力用ファイル
    '                'ファイル名取得-----------------------------------------------------
    '                Dim wkEditFile As String = StartUp.iniValue.OutXlsPath & "\" & StartUp.iniValue.ExcelZM130R1_Out     'コピー先ファイル

    '                'コピー先ファイルが存在する場合、コピー先ファイルを削除----------------
    '                If UtilClass.isFileExists(wkEditFile) Then
    '                    Try
    '                        fh.delete(wkEditFile)
    '                    Catch ioe As System.IO.IOException
    '                        Throw New UsrDefException("ファイルが開かれています。ファイルを閉じてから再実行してください。",
    '                                                  _msgHd.getMSG("fileOpenErr", "【ファイル名】：" & wkEditFile))
    '                    End Try
    '                End If

    '                Try
    '                    '出力用ファイルへ雛型ファイルコピー
    '                    FileCopy(openFilePath, wkEditFile)
    '                Catch ioe As System.IO.IOException
    '                    Throw New UsrDefException(ioe, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ioe)))
    '                End Try

    '                Dim eh As xls.UtilExcelHandler = New xls.UtilExcelHandler(wkEditFile)
    '                Try
    '                    eh.open()
    '                    Try
    '                        '汎用マスタから需要先情報を取得
    '                        Dim sql As String = ""
    '                        sql = sql & N & " SELECT KAHENKEY, NAME1 FROM M01HANYO "
    '                        sql = sql & N & " WHERE KOTEIKEY = '" & M01KOTEI_JUYOUSAKI & "'"
    '                        sql = sql & N & " ORDER BY KAHENKEY "
    '                        'SQL発行
    '                        Dim iRecCnt As Integer          'データセットの行数
    '                        Dim dsHanyo As DataSet = _db.selectDB(sql, RS, iRecCnt)

    '                        If iRecCnt <= 0 Then                    'M01汎用マスタ抽出レコードが１件もない場合
    '                            Throw New UsrDefException("登録されていません。", _msgHd.getMSG("noData"))
    '                        End If

    '                        For i As Integer = 0 To iRecCnt - 1

    '                            'M11の値をデータセットに保持
    '                            Dim dsM11 As DataSet = Nothing
    '                            Dim rowCntM11 As Integer = 0
    '                            '需要先ごとにM11のデータを抽出
    '                            Call getM11DataForXls(_db.rmNullStr(dsHanyo.Tables(RS).Rows(i)("KAHENKEY")), dsM11, rowCntM11)

    '                            'M12の値をデータセットに保持
    '                            Dim dsM12 As DataSet = Nothing
    '                            Dim rowCntM12 As Integer = 0
    '                            '需要先ごとにM12のデータを抽出
    '                            Call getM12DataForXls(_db.rmNullStr(dsHanyo.Tables(RS).Rows(i)("KAHENKEY")), dsM12, rowCntM12)

    '                            If rowCntM11 > 0 Then

    '                                'シート(雛形)を複製保存
    '                                Dim baseName As String = XLSSHEETNM_HINSYU  '雛形シート名
    '                                Dim newName As String = _db.rmNullStr(dsHanyo.Tables(RS).Rows(i)("NAME1"))    '新たに作成するシート
    '                                Try
    '                                    eh.targetSheet = baseName               '雛形シート選択
    '                                    eh.copySheetOnLast(newName)             '雛形シートコピー
    '                                Catch ex As Exception
    '                                    Throw New UsrDefException("シートの複製に失敗しました。", _msgHd.getMSG("failCopySheet"))
    '                                End Try

    '                                'プログレスバー設定
    '                                pb.jobName = newName & "出力中．．．"
    '                                pb.status = ""

    '                                'コピーしたシートに出力
    '                                eh.targetSheet = newName

    '                                '作成日時編集
    '                                Dim printDate As String = Now.ToString("yyyy/MM/dd HH:mm")
    '                                eh.setValue("作成日時 ： " & printDate, 1, 8)   'H1

    '                                'タイトル・需要品名編集
    '                                eh.setValue(XLS_TITLE & "      (" & _db.rmNullStr(dsHanyo.Tables(RS).Rows(i)("NAME1")) & ")", 3, 1)      'A3

    '                                Dim startPrintRow As Integer = START_PRINT_ROW          '出力開始行数

    '                                Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder
    '                                pb.maxVal = rowCntM11

    '                                Dim k As Integer = 0        'M11ループカウンター
    '                                Dim m As Integer = 0        'M12のレコード数カウンター
    '                                Dim xlsRow As Integer = 0
    '                                For k = 0 To rowCntM11 - 1

    '                                    pb.status = (k + 1) & "/" & rowCntM11 & "件"
    '                                    pb.oneStep = 10
    '                                    pb.value = k + 1

    '                                    xlsRow = startPrintRow + k

    '                                    '行を1行追加
    '                                    eh.copyRow(xlsRow)
    '                                    eh.insertPasteRow(xlsRow)

    '                                    '一覧データ出力
    '                                    With dsM11.Tables(RS)
    '                                        Dim sHinmeiCD As String = ""        '出力する計画品名コードを保持する変数
    '                                        For n As Integer = m To rowCntM12 - 1
    '                                            '実品名コードが等しい場合
    '                                            If _db.rmNullStr(.Rows(k)(COLDT_HINMEICD)).Equals _
    '                                                            (_db.rmNullStr(dsM12.Tables(RS2).Rows(n)(COLDT_M12KHINMEICD))) Then
    '                                                If "".Equals(sHinmeiCD) Then
    '                                                    sHinmeiCD = _db.rmNullStr(dsM12.Tables(RS2).Rows(n)(COLDT_M12HINMEICD))
    '                                                Else
    '                                                    '該当する実品名コードをカンマ区切りでつなげる
    '                                                    sHinmeiCD = sHinmeiCD & "," & _db.rmNullStr(dsM12.Tables(RS2).Rows(n)(COLDT_M12HINMEICD))
    '                                                End If
    '                                                m = n + 1
    '                                            Else
    '                                                Exit For
    '                                            End If
    '                                        Next

    '                                        sb.Append(_db.rmNullStr(.Rows(k)(COLDT_HINMEICD)) & ControlChars.Tab)       '品名コード
    '                                        sb.Append(_db.rmNullStr(.Rows(k)(COLDT_HINMEI)) & ControlChars.Tab)         '品名
    '                                        sb.Append(_db.rmNullStr(.Rows(k)(COLDT_LOTTYOU)) & ControlChars.Tab)        'ロット長
    '                                        sb.Append(_db.rmNullStr(.Rows(k)(COLDT_TANTYOU)) & ControlChars.Tab)        '単長
    '                                        sb.Append(_db.rmNullStr(.Rows(k)(COLDT_JOSU)) & ControlChars.Tab)           '条数
    '                                        sb.Append(_db.rmNullStr(.Rows(k)(COLDT_KIJUNTUKISU)) & ControlChars.Tab)    '基準月数
    '                                        sb.Append(_db.rmNullStr(.Rows(k)(COLDT_ABC)) & ControlChars.Tab)            'ABC区分
    '                                        sb.Append(sHinmeiCD)                                                        '集計品名数

    '                                        sb.Append(ControlChars.CrLf)
    '                                    End With
    '                                Next

    '                                Clipboard.SetText(sb.ToString)
    '                                eh.paste(START_PRINT_ROW, START_PRINT_COL) '一括貼り付け

    '                                '罫線を再設定
    '                                Dim lineV As LineVO = New LineVO()
    '                                lineV.Bottom = LineVO.LineType.NomalL
    '                                eh.drawRuledLine(lineV, xlsRow, START_PRINT_COL, , 8)

    '                                eh.deleteRow(xlsRow + 1)    '余分な行を削除
    '                            End If
    '                        Next

    '                        eh.deleteSheet(XLSSHEETNM_HINSYU)   '余分なシートを削除

    '                        '-->2010.12.25 add by takagi #43
    '                        '先頭シートを選択
    '                        eh.targetSheetByIdx = 1
    '                        eh.selectSheet(eh.targetSheet)
    '                        eh.selectCell(1, 1)
    '                        '<--2010.12.25 add by takagi #43
    '                    Finally
    '                        eh.close()
    '                    End Try

    '                    'EXCELファイル開く
    '                    eh.display()

    '                Catch ue As UsrDefException
    '                    ue.dspMsg()
    '                    Throw ue
    '                Catch ex As Exception
    '                    'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
    '                    Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)), , StartUp.iniValue.LogFilePath)
    '                Finally
    '                    eh.endUse()
    '                    eh = Nothing
    '                End Try
    '            Finally
    '                '画面消去
    '                pb.Close()
    '            End Try
    '        Catch ue As UsrDefException         'ユーザー定義例外
    '            Call ue.dspMsg()
    '            Throw ue                        'キャッチした例外をそのままスロー
    '        Catch ex As Exception               'システム例外
    '            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
    '        End Try
    '    End Sub

    '    '-------------------------------------------------------------------------------
    '    '   エクセル出力用データ抽出
    '    '　（処理概要）エクセル出力用のデータをM11から抽出する。
    '    '   ●入力パラメタ  ：prmJJuyousaki     需要先の値
    '    '   ●出力パラメタ  ：prmDs             抽出結果のデータセット
    '    '   ●出力パラメタ  ：prmRecCnt         抽出結果件数
    '    '-------------------------------------------------------------------------------
    '    Private Sub getM11DataForXls(ByVal prmJuyousaki As String, ByRef prmDs As DataSet, ByRef prmRecCnt As Integer)
    '        Try

    '            'EXCEL用のデータ取得
    '            Dim SQL As String = ""
    '            SQL = "SELECT "
    '            SQL = SQL & N & " (TT_H_SIYOU_CD "
    '            SQL = SQL & N & "  || TT_H_HIN_CD "
    '            SQL = SQL & N & "  || TT_H_SENSIN_CD "
    '            SQL = SQL & N & "  || TT_H_SIZE_CD "
    '            SQL = SQL & N & "  || TT_H_COLOR_CD)   " & COLDT_HINMEICD       '品名コード
    '            SQL = SQL & N & " ,TT_HINMEI           " & COLDT_HINMEI         '品名
    '            SQL = SQL & N & " ,TT_LOT              " & COLDT_LOTTYOU        '標準ロット長
    '            SQL = SQL & N & " ,TT_TANCYO           " & COLDT_TANTYOU        '製作単長
    '            SQL = SQL & N & " ,TT_JYOSU            " & COLDT_JOSU           '入庫本数 全体
    '            SQL = SQL & N & " ,TT_KZAIKOTUKISU     " & COLDT_KIJUNTUKISU    '基準月数
    '            SQL = SQL & N & " ,TT_ABCKBN           " & COLDT_ABC            'ABC区分
    '            SQL = SQL & N & " FROM M11KEIKAKUHIN "
    '            SQL = SQL & N & "   WHERE "
    '            '需要先
    '            SQL = SQL & "   TT_JUYOUCD = '" & _db.rmSQ(prmJuyousaki) & "'"
    '            SQL = SQL & "   ORDER BY TT_JUYOUCD, TT_H_HIN_CD, TT_H_SENSIN_CD,  "
    '            SQL = SQL & "   TT_H_SIZE_CD, TT_H_SIYOU_CD, TT_H_COLOR_CD, TT_TEHAI_KBN "

    '            'SQL発行
    '            prmDs = _db.selectDB(SQL, RS, prmRecCnt)

    '        Catch ue As UsrDefException         'ユーザー定義例外
    '            Call ue.dspMsg()
    '            Throw ue                        'キャッチした例外をそのままスロー
    '        Catch ex As Exception               'システム例外
    '            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
    '        End Try
    '    End Sub

    '    '-------------------------------------------------------------------------------
    '    '   エクセル出力用データ抽出
    '    '　（処理概要）エクセル出力用のデータをM12から抽出する。
    '    '   ●入力パラメタ  ：prmJuyousaki      需要先の値
    '    '   ●出力パラメタ  ：prmDs             抽出結果のデータセット
    '    '   ●出力パラメタ  ：prmRecCnt         抽出結果件数
    '    '-------------------------------------------------------------------------------
    '    Private Sub getM12DataForXls(ByVal prmJuyousaki As String, ByRef prmDs As DataSet, ByRef prmRecCnt As Integer)
    '        Try

    '            'EXCEL用のデータ取得
    '            Dim SQL As String = ""
    '            SQL = "SELECT "
    '            SQL = SQL & N & "  M12.HINMEICD " & COLDT_M12HINMEICD       '実品名コード
    '            SQL = SQL & N & "  ,M12.KHINMEICD " & COLDT_M12KHINMEICD    '計画品名コード
    '            SQL = SQL & N & " FROM  M12SYUYAKU M12 "
    '            SQL = SQL & N & "   LEFT JOIN  M11KEIKAKUHIN M11 "
    '            SQL = SQL & N & "   ON M11.TT_KHINMEICD = M12.KHINMEICD "
    '            SQL = SQL & N & "   WHERE "
    '            SQL = SQL & N & "   NOT M12.KHINMEICD = M12.HINMEICD "

    '            '需要先
    '            SQL = SQL & N & "   AND "
    '            SQL = SQL & "   M11.TT_JUYOUCD = '" & _db.rmSQ(prmJuyousaki) & "'"

    '            SQL = SQL & "   ORDER BY TT_JUYOUCD, TT_H_HIN_CD, TT_H_SENSIN_CD,  "
    '            SQL = SQL & "   TT_H_SIZE_CD, TT_H_SIYOU_CD, TT_H_COLOR_CD, TT_TEHAI_KBN "

    '            'SQL発行
    '            prmDs = _db.selectDB(SQL, RS2, prmRecCnt)


    '        Catch ue As UsrDefException         'ユーザー定義例外
    '            Call ue.dspMsg()
    '            Throw ue                        'キャッチした例外をそのままスロー
    '        Catch ex As Exception               'システム例外
    '            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
    '        End Try
    '    End Sub

    '#End Region

    '    '------------------------------------------------------------------------------------------------------
    '    '　ABC分析ボタン押下
    '    '------------------------------------------------------------------------------------------------------
    '    Private Sub btnABC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnABC.Click

    '        Dim openForm As ZM410B_ABCBunseki = New ZM410B_ABCBunseki(_msgHd, _db, Me, updFlg.updFlgABC)      '画面遷移
    '        openForm.Show()                                                             '画面表示
    '        Me.Hide()

    '    End Sub

    '    '------------------------------------------------------------------------------------------------------
    '    '　品種区分マスタメンテボタン押下
    '    '------------------------------------------------------------------------------------------------------
    '    Private Sub btnHMstMente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHMstMente.Click

    '        Dim openForm As ZM210E_HinsyuKbn = New ZM210E_HinsyuKbn(_msgHd, _db, Me, updFlg.updFlgHMstMente)      'パラメタを遷移先画面へ渡す
    '        openForm.Show()                                                             '画面表示
    '        Me.Hide()

    '    End Sub

    '    '------------------------------------------------------------------------------------------------------
    '    '　汎用マスタメンテボタン押下
    '    '------------------------------------------------------------------------------------------------------
    '    Private Sub btnHanyoMst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHanyoMst.Click

    '        Me.Hide()
    '        Dim openForm As ZM310E_HanyouMstMente = New ZM310E_HanyouMstMente(_msgHd, _db, Me, updFlg.updFlgHanyoMst)      '画面遷移
    '        openForm.Show()                                                             '画面表示

    '    End Sub

    '    '------------------------------------------------------------------------------------------------------
    '    '　生産能力マスタメンテボタン押下
    '    '------------------------------------------------------------------------------------------------------
    '    Private Sub btnSNouryokuMst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSNouryokuMst.Click

    '        Dim openForm As ZM610E_SeisanMstMente = New ZM610E_SeisanMstMente(_msgHd, _db, Me, updFlg.updFlgSNouryokuMst)      '画面遷移
    '        openForm.Show()                                                             '画面表示
    '        Me.Hide()

    '    End Sub

    '    '------------------------------------------------------------------------------------------------------
    '    '　外部システム連携ボタン押下
    '    '------------------------------------------------------------------------------------------------------
    '    Private Sub btnGRenkei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGRenkei.Click

    '        Dim openForm As ZM510B_GaibuSystem = New ZM510B_GaibuSystem(_msgHd, _db, Me)      '画面遷移
    '        openForm.Show()                                                             '画面表示
    '        Me.Hide()

    '    End Sub

    '    '------------------------------------------------------------------------------------------------------
    '    '　販売実績照会ボタン押下
    '    '------------------------------------------------------------------------------------------------------
    '    Private Sub btnHSyoukai_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHSyoukai.Click

    '        Dim openForm As ZE110Q_HanbaiJisseki = New ZE110Q_HanbaiJisseki(_msgHd, _db, Me)      '画面遷移
    '        openForm.Show()                                                             '画面表示
    '        Me.Hide()

    '    End Sub

    '    '------------------------------------------------------------------------------------------------------
    '    '　在庫実績照会ボタン押下
    '    '------------------------------------------------------------------------------------------------------
    '    Private Sub btnZSyoukai_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZSyoukai.Click

    '        Dim openForm As ZE210Q_ZaikoJisseki = New ZE210Q_ZaikoJisseki(_msgHd, _db, Me)      '画面遷移
    '        openForm.Show()                                                             '画面表示
    '        Me.Hide()

    '    End Sub

    '    Private Sub tabGeturei_Click(sender As Object, e As EventArgs) Handles tabGeturei.Click

    '    End Sub
End Class