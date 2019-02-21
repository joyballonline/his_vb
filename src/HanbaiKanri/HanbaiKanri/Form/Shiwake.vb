Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports System.Xml 'XML使用に必要

Imports System.Globalization
Imports System.Threading
'Imports System.Text

Public Class Shiwake

    Inherits System.Windows.Forms.Form

    '------------------------------------------------------------------------------------------------------
    'メンバー定数宣言
    '------------------------------------------------------------------------------------------------------
    'PG制御文字 
    Dim ds As DataSet
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
    Private _langHd As UtilLangHandler
    Private _db As UtilDBIf
    Private _init As Boolean                             '初期処理済フラグ
    Private CompanyCode As String = ""
    Private OrderingNo As String()
    Private _status As String = ""

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
                   Optional ByRef prmRefStatus As String = "")
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _status = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True

    End Sub

    Private Sub ShiwakeLoad()
        Console.WriteLine("[CultureInfo]")
        '設定中のRegionを取得
        Console.WriteLine("CurrentCulture:   {0}", CultureInfo.CurrentCulture)

    End Sub
    Private Sub Shiwake_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ShiwakeLoad()
    End Sub

#Region "XML出力用"
    Public Sub ConvertDataTableToCsvSingle(ds1 As DataSet, Name As String)

        Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
        '出力先パス
        Dim sOutPath As String = ""
        sOutPath = StartUp._iniVal.OutXlsPath

        '出力ファイル名
        Dim sOutFile As String = ""
        sOutFile = sOutPath & "\" & Name & ".csv"

        '書き込むファイルを開く
        Dim sr As New System.IO.StreamWriter(sOutFile, False, enc)
        Dim Sql As String = ""
        Dim reccnt As Integer = 0

        For i As Integer = 0 To ds1.Tables(RS).Rows.Count - 1
            If i = 0 Then
                '基本列名
                For y As Integer = 0 To ds1.Tables(RS).Columns.Count - 1
                    Dim field As String = ds1.Tables(RS).Columns(y).ColumnName
                    field = EncloseDoubleQuotesIfNeed(field)
                    sr.Write(field)
                    If y < ds1.Tables(RS).Columns.Count - 1 Then
                        sr.Write(","c)
                    End If
                Next
                sr.Write(vbCrLf)
            End If

            '基本
            For y As Integer = 0 To ds1.Tables(RS).Columns.Count - 1
                Dim field As String = ds1.Tables(RS).Rows(i)(y).ToString()
                field = EncloseDoubleQuotesIfNeed(field)
                sr.Write(field)
                If y < ds1.Tables(RS).Columns.Count - 1 Then
                    sr.Write(","c)
                End If
            Next
            sr.Write(vbCrLf)
        Next
        sr.Close()
    End Sub
#End Region


    '仕訳データのXML出力
    Private Sub getShiwakeData()
        Dim dtToday As DateTime = DateTime.Now '年月の設定
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = "" 'SQL文用

        Dim dsCompany As DataSet = getDsData("m01_company") 'ログイン情報から会社データの取得


#Region "仕訳買掛金"

        '条件オプション
        'Sql = ""
        Sql = " ORDER BY "
        Sql += " 仕入日 "


        Dim dsSwkSirehd As DataSet = getDsData("t40_sirehd", Sql) '仕入データの取得
        Dim seqID As Integer 'TRANSACTIONID用変数

        For i As Integer = 0 To dsSwkSirehd.Tables(RS).Rows.Count - 1
            '条件オプション
            'Sql = ""
            Sql = " AND "
            Sql += "発注番号"
            Sql += " ILIKE "
            Sql += "'"
            Sql += dsSwkSirehd.Tables(RS).Rows(i)("発注番号")
            Sql += "'"

            't20 発注基本
            Dim dsSwkHattyu As DataSet = getDsData("t20_hattyu", Sql) '発注基本データの取得

            't42 入庫基本
            Dim dsSWKNyukohd As DataSet = getDsData("t42_nyukohd", Sql) '入庫基本データの取得


            'Sql = " AND "
            'Sql += "得意先コード"
            'Sql += " ILIKE "
            'Sql += "'"
            'Sql += dsSwkHattyu.Tables(0).Rows(0)("得意先コード")
            'Sql += "'"

            ''m10 得意先マスタ
            'Dim dsCustomer As DataSet = getDsData("m10_customer", Sql) '入庫基本データの取得
            'Dim codeAAC As String = dsCustomer.Tables(RS).Rows(0)("会計用得意先コード")

            '入庫データ回しながら以下データ作成
            '借方：棚卸資産
            '貸方：買掛金

            Dim countKeyID As Integer = 0

            upSeq() 'シーケンス更新
            seqID = getSeq("transactionid_seq")
            Console.WriteLine(seqID)

            For x As Integer = 0 To dsSWKNyukohd.Tables(RS).Rows.Count - 1

                '棚卸資産, 買掛金 = 仕入金額 * (VAT / 100)
                Dim calGlamount As Decimal = dsSWKNyukohd.Tables(RS).Rows(x)("仕入金額") * (dsSWKNyukohd.Tables(RS).Rows(x)("VAT") / 100)
                'VAT-IN, 買掛金 = calGLAMOUNT * (VAT / 100)
                Dim calGlamountVat As Decimal = calGlamount * (dsSWKNyukohd.Tables(RS).Rows(x)("VAT") / 100)



                Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(x)("入庫日"), "yyyyMM") & "'" '入庫日
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'" & getAccountName("inventory") & "'" '棚卸資産
                Sql += "," & formatDouble(calGlamount) '仕入金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & dsSwkHattyu.Tables(RS).Rows(0)("仕入先名").ToString & "'" '仕入先コード
                Sql += ",'WH-" & dsSwkSirehd.Tables(RS).Rows(x)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsSwkSirehd.Tables(RS).Rows(i)("仕入日"), "yyyy-MM-dd") & "'" '仕入日
                Sql += ",''" '空でよし
                Sql += ",'" & formatDouble(calGlamount + calGlamountVat) & "'" '仕入金額 + VAT IN
                Sql += ",''" '空でよし
                Sql += ",''" '空でよし

                countKeyID = getCount(countKeyID)

                't67_swkhd データ登録
                updateT67Swkhd(Sql)

                Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(x)("入庫日"), "yyyyMM") & "'" '入庫日
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'" & getAccountName("accounts-payable") & "'" '買掛金
                Sql += "," & formatDouble(-calGlamount) '仕入金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & dsSwkHattyu.Tables(RS).Rows(0)("仕入先名").ToString & "'" '仕入先コード
                Sql += ",'WH-" & dsSwkSirehd.Tables(RS).Rows(x)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsSwkSirehd.Tables(RS).Rows(i)("仕入日"), "yyyy-MM-dd") & "'" '仕入日
                Sql += ",''" '空でよし
                Sql += ",'" & formatDouble(calGlamount + calGlamountVat) & "'" '仕入金額 + VAT IN
                Sql += ",''" '空でよし
                Sql += ",''" '空でよし

                't67_swkhd データ登録
                updateT67Swkhd(Sql)

                countKeyID = getCount(countKeyID)

                Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(x)("入庫日"), "yyyyMM") & "'" '入庫日
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'" & getAccountName("vat-in") & "'" 'VAT-IN 
                Sql += "," & formatDouble(calGlamountVat) '仕入金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & dsSwkHattyu.Tables(RS).Rows(0)("仕入先名").ToString & "'" '仕入先コード
                Sql += ",'WH-" & dsSwkSirehd.Tables(RS).Rows(x)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsSwkSirehd.Tables(RS).Rows(i)("仕入日"), "yyyy-MM-dd") & "'" '仕入日
                Sql += ",''" '空でよし
                Sql += ",'" & formatDouble(calGlamount + calGlamountVat) & "'" '仕入金額 + VAT IN
                Sql += ",''" '空でよし
                Sql += ",''" '空でよし

                't67_swkhd データ登録
                updateT67Swkhd(Sql)

                countKeyID = getCount(countKeyID)


                Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(x)("入庫日"), "yyyyMM") & "'" '入庫日
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'" & getAccountName("accounts-payable") & "'" '買掛金
                Sql += "," & formatDouble(-calGlamountVat) '仕入金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & dsSwkHattyu.Tables(RS).Rows(0)("仕入先名").ToString & "'" '仕入先コード
                Sql += ",'WH-" & dsSwkSirehd.Tables(RS).Rows(x)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsSwkSirehd.Tables(RS).Rows(i)("仕入日"), "yyyy-MM-dd") & "'" '仕入日
                Sql += ",''" '空でよし
                Sql += ",'" & formatDouble(calGlamount + calGlamountVat) & "'" '仕入金額 + VAT IN
                Sql += ",''" '空でよし
                Sql += ",''" '空でよし

                't67_swkhd データ登録
                updateT67Swkhd(Sql)

                countKeyID = getCount(countKeyID)

            Next

        Next
#End Region

#Region "仕訳売掛金"

        Sql = " ORDER BY "
        Sql += " 売上日 "

        Dim dsSwkUrighd As DataSet = getDsData("t30_urighd", Sql) '売上データの取得

        For i As Integer = 0 To dsSwkUrighd.Tables(RS).Rows.Count - 1

            Dim countKeyID As Integer = 0

            '売掛金, 売上 = 仕入金額 * (VAT / 100)
            Dim calGlamount As Decimal = dsSwkUrighd.Tables(RS).Rows(i)("売上金額") * (dsSwkUrighd.Tables(RS).Rows(i)("VAT") / 100)
            '売掛金, VAT-OUT = calGLAMOUNT * (VAT / 100)
            Dim calGlamountVat As Decimal = calGlamount * (dsSwkUrighd.Tables(RS).Rows(i)("VAT") / 100)


            Sql = ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("売上日"), "yyyyMM") & "'" '売上日
            Sql += "," & "nextval('transactionid_seq')" 'プライマリ
            Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
            Sql += ",'" & getAccountName("accounts-receivable") & "'" '売掛金
            Sql += "," & formatDouble(calGlamount) '売上金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
            Sql += ",1" '固定
            Sql += ",'" & dsSwkUrighd.Tables(RS).Rows(i)("得意先コード").ToString & "'" '得意先コード
            Sql += ",'ER-" & dsSwkUrighd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
            Sql += ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("売上日"), "yyyy-MM-dd") & "'" '売上日
            Sql += ",''" '空でよし
            Sql += "," & formatDouble(calGlamount + calGlamountVat) '売上金額 + VAT IN
            Sql += ",''" '空でよし
            Sql += ",''" '空でよし

            countKeyID = getCount(countKeyID)

            't67_swkhd データ登録
            updateT67Swkhd(Sql)

            seqID = getSeq("transactionid_seq")

            Sql = ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("売上日"), "yyyyMM") & "'" '売上日
            Sql += "," & seqID 'プライマリ
            Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
            Sql += ",'" & getAccountName("sales") & "'" '売上
            Sql += "," & formatDouble(-(calGlamount)) '売上金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
            Sql += ",1" '固定
            Sql += ",'" & dsSwkUrighd.Tables(RS).Rows(0)("得意先コード").ToString & "'" '得意先コード
            Sql += ",'ER-" & dsSwkUrighd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
            Sql += ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("売上日"), "yyyy-MM-dd") & "'" '売上日
            Sql += ",''" '空でよし
            Sql += "," & formatDouble(calGlamount + calGlamountVat) '売上金額 + VAT IN
            Sql += ",''" '空でよし
            Sql += ",''" '空でよし

            't67_swkhd データ登録
            updateT67Swkhd(Sql)

            countKeyID = getCount(countKeyID)

            Sql = ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("売上日"), "yyyyMM") & "'" '売上日
            Sql += "," & seqID 'プライマリ
            Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
            Sql += ",'" & getAccountName("accounts-receivable") & "'" '売掛金
            Sql += "," & formatDouble(calGlamountVat) '売上金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
            Sql += ",1" '固定
            Sql += ",'" & dsSwkUrighd.Tables(RS).Rows(0)("得意先コード").ToString & "'" '得意先コード
            Sql += ",'ER-" & dsSwkUrighd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
            Sql += ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("売上日"), "yyyy-MM-dd") & "'" '売上日
            Sql += ",''" '空でよし
            Sql += "," & formatDouble(calGlamount + calGlamountVat) '売上金額 + VAT IN
            Sql += ",''" '空でよし
            Sql += ",''" '空でよし

            't67_swkhd データ登録
            updateT67Swkhd(Sql)

            countKeyID = getCount(countKeyID)

            Sql = ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("売上日"), "yyyyMM") & "'" '売上日
            Sql += "," & seqID 'プライマリ
            Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
            Sql += ",'" & getAccountName("vat-out") & "'" 'VAT-OUT
            Sql += "," & formatDouble(-(calGlamountVat)) '売上金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
            Sql += ",1" '固定
            Sql += ",'" & dsSwkUrighd.Tables(RS).Rows(0)("得意先コード").ToString & "'" '得意先コード
            Sql += ",'ER-" & dsSwkUrighd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
            Sql += ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("売上日"), "yyyy-MM-dd") & "'" '売上日
            Sql += ",''" '空でよし
            Sql += "," & formatDouble(calGlamount + calGlamountVat) '売上金額 + VAT IN
            Sql += ",''" '空でよし
            Sql += ",''" '空でよし

            't67_swkhd データ登録
            updateT67Swkhd(Sql)

        Next
#End Region


#Region "仕訳前受金"

        Sql = " ORDER BY "
        Sql += " 入金日 "

        Dim dsNkinkshihd As DataSet = getDsData("t27_nkinkshihd", Sql) '入金消込データの取得

        For i As Integer = 0 To dsNkinkshihd.Tables(RS).Rows.Count - 1

            Sql = " AND "
            Sql += "請求番号"
            Sql += " ILIKE "
            Sql += "'"
            Sql += dsNkinkshihd.Tables(RS).Rows(i)("請求番号")
            Sql += "'"

            Dim dsNkinSkyu As DataSet = getDsData("t23_skyuhd", Sql) '請求データの取得

            '------------------------------->> 共通化したい
            Sql = " AND "
            Sql += "得意先コード"
            Sql += " ILIKE "
            Sql += "'"
            Sql += dsNkinSkyu.Tables(0).Rows(0)("得意先コード")
            Sql += "'"

            'm10 得意先マスタ
            Dim dsCustomer As DataSet = getDsData("m10_customer", Sql) '得意先マスタデータの取得
            Console.WriteLine(dsCustomer.Tables(RS).Rows(0)("会計用得意先コード"))
            Dim codeAAC As String = dsCustomer.Tables(RS).Rows(0)("会計用得意先コード")
            '<<------------------------------- 共通化したい

            Dim transactionid As String = DateTime.Now.ToString("MMddHHmmss" & i) 'TRANSACTIONID
            Dim countKeyID As Integer = 0

            upSeq() 'シーケンス更新
            seqID = getSeq("transactionid_seq")


            If dsNkinSkyu.Tables(RS).Rows(0)("請求区分") = "2" Then
                '通常請求の場合

                Sql = " AND "
                Sql += "受注番号"
                Sql += " ILIKE "
                Sql += "'"
                Sql += dsNkinSkyu.Tables(RS).Rows(0)("受注番号")
                Sql += "'"

                Dim dsNkinCymn As DataSet = getDsData("t10_cymnhd", Sql) '受注データの取得


                Sql = ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyyMM") & "'" '入金日
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'" & getAccountName("cash-deposit") & "'" '現金預金
                Sql += "," & formatDouble(dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計")) '入金金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & dsNkinSkyu.Tables(RS).Rows(0)("得意先コード").ToString & "'" '得意先コード
                Sql += ",'PM-" & dsNkinSkyu.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyy-MM-dd") & "'" '入金日
                Sql += ",''" '空でよし
                Sql += "," & formatDouble(dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計")) '入金金額
                Sql += ",'" & codeAAC & "'" '会計用得意先コード
                Sql += ",''" '空でよし

                countKeyID = getCount(countKeyID)

                't67_swkhd データ登録
                updateT67Swkhd(Sql)

                Sql = ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyyMM") & "'" '入金日
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'" & getAccountName("advance") & "'" '前受金
                Sql += "," & formatDouble(-dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計")) '入金金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & dsNkinSkyu.Tables(RS).Rows(0)("得意先コード").ToString & "'" '得意先コード
                Sql += ",'PM-" & dsNkinSkyu.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyy-MM-dd") & "'" '入金日
                Sql += ",''" '空でよし
                Sql += "," & formatDouble(dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計")) '入金金額
                Sql += ",'" & codeAAC & "'" '会計用得意先コード
                Sql += ",''" '空でよし

                countKeyID = getCount(countKeyID)

                't67_swkhd データ登録
                updateT67Swkhd(Sql)

                Sql = ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyyMM") & "'" '入金日
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'" & getAccountName("advance") & "'" '前受金
                Sql += "," & formatDouble(dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計")) '入金金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & dsNkinSkyu.Tables(RS).Rows(0)("得意先コード").ToString & "'" '得意先コード
                Sql += ",'PM-" & dsNkinSkyu.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyy-MM-dd") & "'" '入金日
                Sql += ",''" '空でよし
                Sql += "," & formatDouble(dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計")) '入金金額
                Sql += ",'" & codeAAC & "'" '会計用得意先コード
                Sql += ",''" '空でよし

                countKeyID = getCount(countKeyID)

                't67_swkhd データ登録
                updateT67Swkhd(Sql)


                Sql = ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyyMM") & "'" '入金日
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'" & getAccountName("accounts-receivable") & "'" '売掛金
                Sql += "," & formatDouble(-dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計")) '入金金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & dsNkinSkyu.Tables(RS).Rows(0)("得意先コード").ToString & "'" '得意先コード
                Sql += ",'PM-" & dsNkinSkyu.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyy-MM-dd") & "'" '入金日
                Sql += ",''" '空でよし
                Sql += "," & formatDouble(dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計")) '入金金額
                Sql += ",'" & codeAAC & "'" '会計用得意先コード
                Sql += ",''" '空でよし

                't67_swkhd データ登録
                updateT67Swkhd(Sql)

            Else
                '前受請求の場合

                Sql = " AND "
                Sql += "受注番号"
                Sql += " ILIKE "
                Sql += "'"
                Sql += dsNkinSkyu.Tables(RS).Rows(0)("受注番号")
                Sql += "'"

                Dim dsNkinCymn As DataSet = getDsData("t10_cymnhd", Sql) '受注データの取得

                '売掛データ
                Sql = ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyyMM") & "'" '入金日
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'" & getAccountName("cash-deposit") & "'" '現金預金
                Sql += "," & formatDouble(dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計")) '入金金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & dsNkinSkyu.Tables(RS).Rows(0)("得意先コード").ToString & "'" '得意先コード
                Sql += ",'PM-" & dsNkinSkyu.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyy-MM-dd") & "'" '入金日
                Sql += ",''" '空でよし
                Sql += "," & formatDouble(dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計")) '入金金額
                Sql += ",'" & codeAAC & "'" '会計用得意先コード
                Sql += ",''" '空でよし

                countKeyID = getCount(countKeyID)

                't67_swkhd データ登録
                updateT67Swkhd(Sql)

                Sql = ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyyMM") & "'" '入金日
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'" & getAccountName("accounts-receivable") & "'" '売掛金
                Sql += "," & formatDouble(-dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計")) '入金金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & dsNkinSkyu.Tables(RS).Rows(0)("得意先コード").ToString & "'" '得意先コード
                Sql += ",'PM-" & dsNkinSkyu.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyy-MM-dd") & "'" '入金日
                Sql += ",''" '空でよし
                Sql += "," & formatDouble(dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計")) '入金金額
                Sql += ",'" & codeAAC & "'" '会計用得意先コード
                Sql += ",''" '空でよし

                't67_swkhd データ登録
                updateT67Swkhd(Sql)

            End If

        Next
#End Region


#Region "仕分前払金"

        Sql = " ORDER BY "
        Sql += " 支払日 "

        Dim dsShrikshihd As DataSet = getDsData("t49_shrikshihd", Sql) '支払消込データの取得

        For i As Integer = 0 To dsShrikshihd.Tables(RS).Rows.Count - 1

            Sql = " AND "
            Sql += "買掛番号"
            Sql += " ILIKE "
            Sql += "'"
            Sql += dsShrikshihd.Tables(RS).Rows(i)("買掛番号")
            Sql += "'"

            Dim dsShriKike As DataSet = getDsData("t46_kikehd", Sql) '買掛データの取得

            '------------------------------->> 共通化したい
            Sql = " AND "
            Sql += "仕入先コード"
            Sql += " ILIKE "
            Sql += "'"
            Sql += dsShriKike.Tables(0).Rows(0)("仕入先コード")
            Sql += "'"

            'm10 得意先マスタ
            Dim dsSupplier As DataSet = getDsData("m11_supplier", Sql) '得意先マスタデータの取得

            Dim codeAAC As String = dsSupplier.Tables(RS).Rows(0)("会計用仕入先コード")
            '<<------------------------------- 共通化したい

            Dim countKeyID As Integer = 0

            '通常買掛
            If dsShriKike.Tables(RS).Rows(0)("買掛区分") = "2" Then

                Sql = " AND "
                Sql += "発注番号"
                Sql += " ILIKE "
                Sql += "'"
                Sql += dsShriKike.Tables(RS).Rows(0)("発注番号")
                Sql += "'"

                '買掛データの取得（仕入先名取るためっぽい）使うか？
                Dim dsShriHattyu As DataSet = getDsData("t20_hattyu", Sql)


                Sql = ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyyMM") & "'" '入金日
                Sql += "," & "nextval('transactionid_seq')" 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'" & getAccountName("prepaid") & "'" '前払金
                Sql += "," & formatDouble(dsShrikshihd.Tables(RS).Rows(i)("支払消込額計")) '支払金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & dsShriKike.Tables(RS).Rows(0)("仕入先コード").ToString & "'" '仕入先コード
                Sql += ",'PO-" & dsShriKike.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyy-MM-dd") & "'" '入金日
                Sql += ",''" '空でよし
                Sql += "," & formatDouble(dsShrikshihd.Tables(RS).Rows(i)("支払消込額計")) '支払金額
                Sql += ",'" & codeAAC & "'" '会計用支払先コード
                Sql += ",''" '空でよし

                updateT67Swkhd(Sql) 'update実行
                seqID = getSeq("transactionid_seq")
                countKeyID = getCount(countKeyID) '0～カウントアップ

                Sql = ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyyMM") & "'" '入金日
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'" & getAccountName("cash-deposit") & "'" '現金預金
                Sql += "," & formatDouble(-dsShrikshihd.Tables(RS).Rows(i)("支払消込額計")) '支払金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & dsShriKike.Tables(RS).Rows(0)("仕入先コード").ToString & "'" '仕入先コード
                Sql += ",'PO-" & dsShriKike.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyy-MM-dd") & "'" '入金日
                Sql += ",''" '空でよし
                Sql += "," & formatDouble(dsShrikshihd.Tables(RS).Rows(i)("支払消込額計")) '支払金額
                Sql += ",'" & codeAAC & "'" '会計用支払先コード
                Sql += ",''" '空でよし

                updateT67Swkhd(Sql) 'update実行
                countKeyID = getCount(countKeyID) '0～カウントアップ

                Sql = ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyyMM") & "'" '入金日
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'" & getAccountName("accounts-payable") & "'" '買掛金
                Sql += "," & formatDouble(dsShrikshihd.Tables(RS).Rows(i)("支払消込額計")) '支払金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & dsShriKike.Tables(RS).Rows(0)("仕入先コード").ToString & "'" '仕入先コード
                Sql += ",'PO-" & dsShriKike.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyy-MM-dd") & "'" '入金日
                Sql += ",''" '空でよし
                Sql += "," & formatDouble(dsShrikshihd.Tables(RS).Rows(i)("支払消込額計")) '支払金額
                Sql += ",'" & codeAAC & "'" '会計用支払先コード
                Sql += ",''" '空でよし

                updateT67Swkhd(Sql) 'update実行
                countKeyID = getCount(countKeyID) '0～カウントアップ

                Sql = ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyyMM") & "'" '入金日
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'" & getAccountName("prepaid") & "'" '前払金
                Sql += "," & formatDouble(-dsShrikshihd.Tables(RS).Rows(i)("支払消込額計")) '支払金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & dsShriKike.Tables(RS).Rows(0)("仕入先コード").ToString & "'" '仕入先コード
                Sql += ",'PO-" & dsShriKike.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyy-MM-dd") & "'" '入金日
                Sql += ",''" '空でよし
                Sql += "," & formatDouble(dsShrikshihd.Tables(RS).Rows(i)("支払消込額計")) '支払金額
                Sql += ",'" & codeAAC & "'" '会計用支払先コード
                Sql += ",''" '空でよし

                updateT67Swkhd(Sql) 'update実行

            Else

                '前払買掛

                Sql = " AND "
                Sql += "発注番号"
                Sql += " ILIKE "
                Sql += "'"
                Sql += dsShriKike.Tables(RS).Rows(0)("発注番号")
                Sql += "'"

                '買掛データの取得（仕入先名取るためっぽい）使うか？
                Dim dsShriHattyu As DataSet = getDsData("t20_hattyu", Sql)

                Sql = ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyyMM") & "'" '入金日
                Sql += "," & "nextval('transactionid_seq')" 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'" & getAccountName("accounts-payable") & "'" '買掛金
                Sql += "," & formatDouble(dsShrikshihd.Tables(RS).Rows(i)("支払消込額計")) '支払金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & dsShriKike.Tables(RS).Rows(0)("仕入先コード").ToString & "'" '仕入先コード
                Sql += ",'PO-" & dsShriKike.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyy-MM-dd") & "'" '入金日
                Sql += ",''" '空でよし
                Sql += "," & formatDouble(dsShrikshihd.Tables(RS).Rows(i)("支払消込額計")) '支払金額
                Sql += ",'" & codeAAC & "'" '会計用支払先コード
                Sql += ",''" '空でよし

                updateT67Swkhd(Sql) 'update実行
                seqID = getSeq("transactionid_seq")
                countKeyID = getCount(countKeyID) '0～カウントアップ

                Sql = ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyyMM") & "'" '入金日
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'" & getAccountName("cash-deposit") & "'" '現金預金
                Sql += "," & formatDouble(-dsShrikshihd.Tables(RS).Rows(i)("支払消込額計")) '支払金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & dsShriKike.Tables(RS).Rows(0)("仕入先コード").ToString & "'" '仕入先コード
                Sql += ",'PO-" & dsShriKike.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyy-MM-dd") & "'" '入金日
                Sql += ",''" '空でよし
                Sql += "," & formatDouble(dsShrikshihd.Tables(RS).Rows(i)("支払消込額計")) '支払金額
                Sql += ",'" & codeAAC & "'" '会計用支払先コード
                Sql += ",''" '空でよし

                updateT67Swkhd(Sql) 'update実行

            End If

        Next
#End Region




    End Sub






    Private Sub BtnOutput_Click(sender As Object, e As EventArgs) Handles BtnOutput.Click
        '仕訳データを作成する
        getShiwakeData()

        '現在日時を取得
        Dim nowDatetime As String = DateTime.Now.ToString("yyyyMMddHHmmss")

        'xmlファイル内容の初期化
        Dim strXml As String
        Dim reccnt As Integer = 0

        Dim shiwakeSql As String = ""
        Dim shiwakeData As DataSet
        Dim branchCodeSql As String = ""
        Dim branchCode As DataSet
        Try

            '会計用コードの取得
            branchCodeSql += " WHERE "
            branchCodeSql += """会社コード"" = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            branchCode = _db.selectDB(allSelectSql("m01_company", branchCodeSql), RS, reccnt) 'reccnt:(省略可能)SELECT文の取得レコード件数

            Dim getRow As DataRow
            getRow = branchCode.Tables(0).Rows(0)


            shiwakeSql += " WHERE "
            shiwakeSql += """会社コード"" = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            shiwakeSql += " ORDER BY "
            shiwakeSql += """TRANSACTIONID"",""KeyID"""

            shiwakeData = _db.selectDB(allSelectSql("t67_swkhd", shiwakeSql), RS, reccnt) 'reccnt:(省略可能)SELECT文の取得レコード件数

            Dim cdAR As String = getAccountName("accounts-receivable") '売掛金 アキュレート用勘定科目コード
            Dim cdAP As String = getAccountName("accounts-payable") '買掛金 アキュレート用勘定科目コード

            '取得したデータをXML形式に加工する
            strXml = "<?xml version='1.0'?>"


            Dim checkTransactionid As String = ""

            For i As Integer = 0 To shiwakeData.Tables(RS).Rows.Count - 1

                Dim valId As String = shiwakeData.Tables(RS).Rows(i)(0).ToString()
                Dim valComCd As String = shiwakeData.Tables(RS).Rows(i)(1).ToString()
                Dim valDate As String = shiwakeData.Tables(RS).Rows(i)(2).ToString()
                Dim valTransactionid As String = shiwakeData.Tables(RS).Rows(i)(3).ToString()

                Dim nextTransactionid As String = ""
                If shiwakeData.Tables(RS).Rows.Count - 1 > i Then
                    nextTransactionid = shiwakeData.Tables(RS).Rows(i + 1)(3).ToString() '次のvalTransactionid（判定用）
                End If
                Dim valKeyId As String = shiwakeData.Tables(RS).Rows(i)(4).ToString()
                Dim valGlaccount As String = shiwakeData.Tables(RS).Rows(i)(5).ToString()
                Dim valGlamount As String = shiwakeData.Tables(RS).Rows(i)(6).ToString()
                Dim valRate As String = shiwakeData.Tables(RS).Rows(i)(7).ToString()
                Dim valVendorno As String = shiwakeData.Tables(RS).Rows(i)(8).ToString()
                Dim valJvnumber As String = shiwakeData.Tables(RS).Rows(i)(9).ToString() 'PO
                Dim valTransdate As String = shiwakeData.Tables(RS).Rows(i)(10).ToString()
                Dim valTransdescription As String = shiwakeData.Tables(RS).Rows(i)(11).ToString()
                Dim valJvamount As String = shiwakeData.Tables(RS).Rows(i)(12).ToString()
                Dim valCustomerno As String = shiwakeData.Tables(RS).Rows(i)(13).ToString()
                Dim valDescription As String = shiwakeData.Tables(RS).Rows(i)(14).ToString()

                '初回に必ず入れる
                If i < 1 Then
                    strXml += "<NMEXML EximID='1' BranchCode='" & getRow("会計用コード") & "' ACCOUNTANTCOPYID=''>"
                    strXml += "<TRANSACTIONS OnError='CONTINUE'>"
                End If

                Dim totalJvamount As Integer = 0

                'TRANSACTIONID が同じ場合のみ
                If valTransactionid = checkTransactionid Then

                    'strXml += "<TRANSACTIONID>" & valTransactionid & "</TRANSACTIONID>"
                    strXml += "<ACCOUNTLINE operation='Add'>"
                    strXml += "<KeyID>" & valKeyId & "</KeyID>"
                    strXml += "<GLACCOUNT>" & valGlaccount & "</GLACCOUNT>"
                    strXml += "<GLAMOUNT>" & valGlamount & "</GLAMOUNT>"
                    strXml += "<DESCRIPTION>" & valDescription & "</DESCRIPTION>"
                    strXml += "<RATE>" & valRate & "</RATE>"
                    strXml += "<PRIMEAMOUNT></PRIMEAMOUNT>"
                    strXml += "<TXDATE/>"
                    strXml += "<POSTED/>"
                    strXml += "<CURRENCYNAME></CURRENCYNAME>"

                    '売掛金だったら
                    If valGlaccount = cdAR Then
                        strXml += "<CUSTOMERNO>" & valCustomerno & "</CUSTOMERNO>"
                    ElseIf valGlaccount = cdAP Then
                        strXml += "<VENDORNO>" & valVendorno & "</VENDORNO>"
                    End If

                    strXml += "</ACCOUNTLINE>"

                    totalJvamount = 0 'リセット

                    'TRANSACTIONID が異なっていたら（初回含む）
                Else
                    checkTransactionid = valTransactionid

                    strXml += "<JV operation='Add' REQUESTID='1'>"
                    strXml += "<TRANSACTIONID>" & valTransactionid & "</TRANSACTIONID>"
                    strXml += "<ACCOUNTLINE operation='Add'>"
                    strXml += "<KeyID>" & valKeyId & "</KeyID>"
                    strXml += "<GLACCOUNT>" & valGlaccount & "</GLACCOUNT>"
                    strXml += "<GLAMOUNT>" & valGlamount & "</GLAMOUNT>"
                    strXml += "<DESCRIPTION>" & valDescription & "</DESCRIPTION>"
                    strXml += "<RATE>" & valRate & "</RATE>"
                    strXml += "<PRIMEAMOUNT></PRIMEAMOUNT>"
                    strXml += "<TXDATE/>"
                    strXml += "<POSTED/>"
                    strXml += "<CURRENCYNAME></CURRENCYNAME>"

                    '売掛金だったら
                    If valGlaccount = cdAR Then
                        strXml += "<CUSTOMERNO>" & valCustomerno & "</CUSTOMERNO>"
                    ElseIf valGlaccount = cdAP Then
                        strXml += "<VENDORNO>" & valVendorno & "</VENDORNO>"
                    End If



                    'strXml += "<VENDORNO>" & valVendorno & "</VENDORNO>"
                    'strXml += "<CUSTOMERNO>" & valCustomerno & "</CUSTOMERNO>"

                    strXml += "</ACCOUNTLINE>"
                End If

                '整数だったら加算していく
                If valJvamount > 0 Then
                    totalJvamount += valJvamount
                End If


                If nextTransactionid <> checkTransactionid Then
                    strXml += "<JVNUMBER>" & valJvnumber & "</JVNUMBER>"
                    strXml += "<TRANSDATE>" & valTransdate & "</TRANSDATE>"
                    strXml += "<SOURCE>GL</SOURCE>"
                    strXml += "<TRANSTYPE>journal voucher</TRANSTYPE>"
                    strXml += "<TRANSDESCRIPTION>" & valTransdescription & "</TRANSDESCRIPTION>"
                    strXml += "<JVAMOUNT>" & valJvamount & "</JVAMOUNT>"
                    strXml += "</JV>"
                End If


            Next
            'Console.WriteLine("xml: " & strXml)
            strXml += "</TRANSACTIONS>"
            strXml += "</NMEXML>"

            Dim xmlDoc As New System.Xml.XmlDocument

            '文字列からDOMドキュメントを生成
            xmlDoc.LoadXml(strXml)

            Try
                '作成したDOMドキュメントをファイルに保存

                'Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
                Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("UTF-8")
                '出力先パス
                Dim sOutPath As String = ""
                sOutPath = StartUp._iniVal.OutXlsPath


                xmlDoc.Save(sOutPath & "\" & nowDatetime & ".xml")

                _msgHd.dspMSG("CreateXML", frmC01F10_Login.loginValue.Language)
            Catch ex As System.Xml.XmlException
                'XMLによる例外をキャッチ
                Console.WriteLine(ex.Message)
            End Try

            'Catch ex As Exception
        Catch lex As UsrDefException
            lex.dspMsg()
            Exit Sub
        End Try







    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        Dim openForm As Form = Nothing
        openForm = New frmC01F30_Menu(_msgHd, _langHd, _db)
        openForm.Show()
        Me.Close()
    End Sub

    Private Function EncloseDoubleQuotes(field As String) As String
        If field.IndexOf(""""c) > -1 Then
            '"を""とする
            field = field.Replace("""", """""")
        End If
        Return """" & field & """"
    End Function

    Private Function EncloseDoubleQuotesIfNeed(field As String) As String
        If NeedEncloseDoubleQuotes(field) Then
            Return EncloseDoubleQuotes(field)
        End If
        Return field
    End Function

    Private Function NeedEncloseDoubleQuotes(field As String) As Boolean
        Return field.IndexOf(""""c) > -1 OrElse
        field.IndexOf(","c) > -1 OrElse
        field.IndexOf(ControlChars.Cr) > -1 OrElse
        field.IndexOf(ControlChars.Lf) > -1 OrElse
        field.StartsWith(" ") OrElse
        field.StartsWith(vbTab) OrElse
        field.EndsWith(" ") OrElse
        field.EndsWith(vbTab)
    End Function

    'select文を返すfunc(Allのみ）paramでwhere句などを入れる
    Private Function allSelectSql(ByVal tableName As String, Optional ByRef txtParam As String = "") As String
        Dim txtSql As String = ""
        txtSql += "SELECT"
        txtSql += " *"
        txtSql += " FROM "

        txtSql += "public"
        txtSql += "."
        txtSql += tableName
        txtSql += txtParam

        Return txtSql
    End Function

    'テーブル名
    'オプションがあれば（条件）第二引数
    Private Function getDsData(ByVal tableName As String, Optional ByRef txtParam As String = "") As DataSet
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        Sql += "SELECT"
        Sql += " *"
        Sql += " FROM "

        Sql += "public." & tableName
        Sql += " WHERE "
        Sql += "会社コード"
        Sql += " ILIKE  "
        Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += txtParam
        Console.WriteLine(Sql)
        Return _db.selectDB(Sql, RS, reccnt)
    End Function


    '登録する科目名
    'オプションがあれば（条件）第二引数
    Private Sub updateT67Swkhd(ByVal param As String)
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        Sql += "INSERT INTO "
        Sql += "Public.t67_swkhd"
        Sql += "("
        Sql += """会社コード"""
        Sql += ",""処理年月"""
        Sql += ",""TRANSACTIONID"""
        Sql += ",""KeyID"""
        Sql += ",""GLACCOUNT"""
        Sql += ",""GLAMOUNT"""
        Sql += ",""RATE"""
        Sql += ",""VENDORNO"""
        Sql += ",""JVNUMBER"""
        Sql += ",""TRANSDATE"""
        Sql += ",""TRANSDESCRIPTION"""
        Sql += ",""JVAMOUNT"""
        Sql += ",""CUSTOMERNO"""
        Sql += ",""DESCRIPTION"""
        Sql += ") "
        Sql += " VALUES("
        Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += param
        Sql += ") "
        'Console.WriteLine(Sql)
        _db.executeDB(Sql)
    End Sub

    '現状のシーケンス取得
    Private Function getSeq(ByVal seqName As String) As Integer
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""
        Dim dsData As DataSet

        Sql += "SELECT"
        Sql += " *"
        Sql += " FROM "
        Sql += "public." & seqName

        dsData = _db.selectDB(Sql, RS, reccnt)

        Return dsData.Tables(RS).Rows(0)("last_value")
    End Function

    'シーケンス更新
    Private Sub upSeq()
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""
        Sql = "Select nextval('transactionid_seq')"

        _db.selectDB(Sql, RS, reccnt)
    End Sub



    'カウントアップ
    Private Function getCount(ByVal nowCount As Integer) As Integer
        Dim count As Integer
        count = nowCount + 1

        Return count
    End Function

    '小数部分のフォーマット
    Private Function formatDouble(ByVal val As Decimal) As Decimal
        Dim result As Decimal

        ' 小数点第三位で四捨五入し、小数点第二位まで出力
        result = Math.Round(val, 2, MidpointRounding.AwayFromZero)

        Return result
    End Function

    '勘定科目コードからアキュレート用勘定科目コード取得（有効データのみ）
    Private Function getAccountName(ByVal codeName As String) As String
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""
        Dim dsData As DataSet

        Sql += "SELECT"
        Sql += " *"
        Sql += " FROM "
        Sql += "public.m92_kanjo"
        Sql += " WHERE "
        Sql += "会社コード"
        Sql += " ILIKE "
        Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND "
        Sql += "勘定科目コード"
        Sql += " ILIKE "
        Sql += "'" & codeName & "'"
        Sql += " AND "
        Sql += "有効区分 = 0"

        dsData = _db.selectDB(Sql, RS, reccnt)

        Return dsData.Tables(RS).Rows(0)("会計用勘定科目コード")
    End Function

    '最終的には削除する
    '会社コード=ZENBIを一括削除
    Private Sub BtnShiwakeClear_Click(sender As Object, e As EventArgs) Handles BtnShiwakeClear.Click
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = "DELETE FROM t67_swkhd WHERE ""会社コード"" = 'ZENBI';"

        _db.executeDB(Sql)

    End Sub

    '最終的には削除する
    '会社コード=ZENBIを一括削除
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String

        't01_mithd
        Sql = "DELETE FROM t01_mithd WHERE ""会社コード"" = 'ZENBI';"
        _db.executeDB(Sql)

        't02_mitdt
        Sql = "DELETE FROM t02_mitdt WHERE ""会社コード"" = 'ZENBI';"
        _db.executeDB(Sql)

        't10_cymnhd
        Sql = "DELETE FROM t10_cymnhd WHERE ""会社コード"" = 'ZENBI';"
        _db.executeDB(Sql)

        't11_cymndt
        Sql = "DELETE FROM t11_cymndt WHERE ""会社コード"" = 'ZENBI';"
        _db.executeDB(Sql)

        't20_hattyu
        Sql = "DELETE FROM t20_hattyu WHERE ""会社コード"" = 'ZENBI';"
        _db.executeDB(Sql)

        't21_hattyu
        Sql = "DELETE FROM t21_hattyu WHERE ""会社コード"" = 'ZENBI';"
        _db.executeDB(Sql)

        't23_skyuhd
        Sql = "DELETE FROM t23_skyuhd WHERE ""会社コード"" = 'ZENBI';"
        _db.executeDB(Sql)

        't25_nkinhd
        Sql = "DELETE FROM t25_nkinhd WHERE ""会社コード"" = 'ZENBI';"
        _db.executeDB(Sql)

        't26_nkindt
        Sql = "DELETE FROM t26_nkindt WHERE ""会社コード"" = 'ZENBI';"
        _db.executeDB(Sql)

        't27_nkinkshihd
        Sql = "DELETE FROM t27_nkinkshihd WHERE ""会社コード"" = 'ZENBI';"
        _db.executeDB(Sql)

        't30_urighd
        Sql = "DELETE FROM t30_urighd WHERE ""会社コード"" = 'ZENBI';"
        _db.executeDB(Sql)

        't31_urigdt
        Sql = "DELETE FROM t31_urigdt WHERE ""会社コード"" = 'ZENBI';"
        _db.executeDB(Sql)

        't40_sirehd
        Sql = "DELETE FROM t40_sirehd WHERE ""会社コード"" = 'ZENBI';"
        _db.executeDB(Sql)

        't41_siredt
        Sql = "DELETE FROM t41_siredt WHERE ""会社コード"" = 'ZENBI';"
        _db.executeDB(Sql)

        't42_nyukohd
        Sql = "DELETE FROM t42_nyukohd WHERE ""会社コード"" = 'ZENBI';"
        _db.executeDB(Sql)

        't43_nyukodt
        Sql = "DELETE FROM t43_nyukodt WHERE ""会社コード"" = 'ZENBI';"
        _db.executeDB(Sql)

        't44_shukohd
        Sql = "DELETE FROM t44_shukohd WHERE ""会社コード"" = 'ZENBI';"
        _db.executeDB(Sql)

        't45_shukodt
        Sql = "DELETE FROM t45_shukodt WHERE ""会社コード"" = 'ZENBI';"
        _db.executeDB(Sql)

        't46_kikehd
        Sql = "DELETE FROM t46_kikehd WHERE ""会社コード"" = 'ZENBI';"
        _db.executeDB(Sql)

        't47_shrihd
        Sql = "DELETE FROM t47_shrihd WHERE ""会社コード"" = 'ZENBI';"
        _db.executeDB(Sql)

        't48_shridt
        Sql = "DELETE FROM t48_shridt WHERE ""会社コード"" = 'ZENBI';"
        _db.executeDB(Sql)

        't49_shrikshihd
        Sql = "DELETE FROM t49_shrikshihd WHERE ""会社コード"" = 'ZENBI';"
        _db.executeDB(Sql)

        't50_zikhd
        Sql = "DELETE FROM t50_zikhd WHERE ""会社コード"" = 'ZENBI';"
        _db.executeDB(Sql)

        ''t52_krurighd
        'Sql = "DELETE FROM t52_krurighd WHERE ""会社コード"" = 'ZENBI';"
        '_db.executeDB(Sql)

        ''t53_krurigdt
        'Sql = "DELETE FROM t53_krurigdt WHERE ""会社コード"" = 'ZENBI';"
        '_db.executeDB(Sql)

        ''t54_krsirehd
        'Sql = "DELETE FROM t54_krsirehd WHERE ""会社コード"" = 'ZENBI';"
        '_db.executeDB(Sql)

        ''t55_krsiredt
        'Sql = "DELETE FROM t55_krsiredt WHERE ""会社コード"" = 'ZENBI';"
        '_db.executeDB(Sql)

        ''t56_krskyuhd
        'Sql = "DELETE FROM t56_krskyuhd WHERE ""会社コード"" = 'ZENBI';"
        '_db.executeDB(Sql)

        ''t57_krkikehd
        'Sql = "DELETE FROM t57_krkikehd WHERE ""会社コード"" = 'ZENBI';"
        '_db.executeDB(Sql)

        ''t58_krnyukohd
        'Sql = "DELETE FROM t58_krnyukohd WHERE ""会社コード"" = 'ZENBI';"
        '_db.executeDB(Sql)

        ''t59_krnyukodt
        'Sql = "DELETE FROM t59_krnyukodt WHERE ""会社コード"" = 'ZENBI';"
        '_db.executeDB(Sql)

        ''t60_krshukohd
        'Sql = "DELETE FROM t60_krshukohd WHERE ""会社コード"" = 'ZENBI';"
        '_db.executeDB(Sql)

        ''t61_krshukodt
        'Sql = "DELETE FROM t61_krshukodt WHERE ""会社コード"" = 'ZENBI';"
        '_db.executeDB(Sql)

        ''t62_krnkinhd
        'Sql = "DELETE FROM t62_krnkinhd WHERE ""会社コード"" = 'ZENBI';"
        '_db.executeDB(Sql)

        ''t63_krnkindt
        'Sql = "DELETE FROM t63_krnkindt WHERE ""会社コード"" = 'ZENBI';"
        '_db.executeDB(Sql)

        ''t64_krshrihd
        'Sql = "DELETE FROM t64_krshrihd WHERE ""会社コード"" = 'ZENBI';"
        '_db.executeDB(Sql)

        ''t65_krshridt
        'Sql = "DELETE FROM t65_krshridt WHERE ""会社コード"" = 'ZENBI';"
        '_db.executeDB(Sql)

    End Sub
End Class