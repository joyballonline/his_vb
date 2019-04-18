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
    Private _parentForm As Form
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
                   ByRef prmRefForm As Form,
                   Optional ByRef prmRefStatus As String = "")
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _parentForm = prmRefForm
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
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
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

        Dim dbName As String = frmC01F10_Login.loginValue.BumonCD

        't01_mithd
        Sql = "DELETE FROM t01_mithd WHERE ""会社コード"" = '" & dbName & "';"
        _db.executeDB(Sql)

        't02_mitdt
        Sql = "DELETE FROM t02_mitdt WHERE ""会社コード"" = '" & dbName & "';"
        _db.executeDB(Sql)

        't10_cymnhd
        Sql = "DELETE FROM t10_cymnhd WHERE ""会社コード"" = '" & dbName & "';"
        _db.executeDB(Sql)

        't11_cymndt
        Sql = "DELETE FROM t11_cymndt WHERE ""会社コード"" = '" & dbName & "';"
        _db.executeDB(Sql)

        't20_hattyu
        Sql = "DELETE FROM t20_hattyu WHERE ""会社コード"" = '" & dbName & "';"
        _db.executeDB(Sql)

        't21_hattyu
        Sql = "DELETE FROM t21_hattyu WHERE ""会社コード"" = '" & dbName & "';"
        _db.executeDB(Sql)

        't23_skyuhd
        Sql = "DELETE FROM t23_skyuhd WHERE ""会社コード"" = '" & dbName & "';"
        _db.executeDB(Sql)

        't25_nkinhd
        Sql = "DELETE FROM t25_nkinhd WHERE ""会社コード"" = '" & dbName & "';"
        _db.executeDB(Sql)

        't26_nkindt
        Sql = "DELETE FROM t26_nkindt WHERE ""会社コード"" = '" & dbName & "';"
        _db.executeDB(Sql)

        't27_nkinkshihd
        Sql = "DELETE FROM t27_nkinkshihd WHERE ""会社コード"" = '" & dbName & "';"
        _db.executeDB(Sql)

        't30_urighd
        Sql = "DELETE FROM t30_urighd WHERE ""会社コード"" = '" & dbName & "';"
        _db.executeDB(Sql)

        't31_urigdt
        Sql = "DELETE FROM t31_urigdt WHERE ""会社コード"" = '" & dbName & "';"
        _db.executeDB(Sql)

        't40_sirehd
        Sql = "DELETE FROM t40_sirehd WHERE ""会社コード"" = '" & dbName & "';"
        _db.executeDB(Sql)

        't41_siredt
        Sql = "DELETE FROM t41_siredt WHERE ""会社コード"" = '" & dbName & "';"
        _db.executeDB(Sql)

        't42_nyukohd
        Sql = "DELETE FROM t42_nyukohd WHERE ""会社コード"" = '" & dbName & "';"
        _db.executeDB(Sql)

        't43_nyukodt
        Sql = "DELETE FROM t43_nyukodt WHERE ""会社コード"" = '" & dbName & "';"
        _db.executeDB(Sql)

        't44_shukohd
        Sql = "DELETE FROM t44_shukohd WHERE ""会社コード"" = '" & dbName & "';"
        _db.executeDB(Sql)

        't45_shukodt
        Sql = "DELETE FROM t45_shukodt WHERE ""会社コード"" = '" & dbName & "';"
        _db.executeDB(Sql)

        't46_kikehd
        Sql = "DELETE FROM t46_kikehd WHERE ""会社コード"" = '" & dbName & "';"
        _db.executeDB(Sql)

        't47_shrihd
        Sql = "DELETE FROM t47_shrihd WHERE ""会社コード"" = '" & dbName & "';"
        _db.executeDB(Sql)

        't48_shridt
        Sql = "DELETE FROM t48_shridt WHERE ""会社コード"" = '" & dbName & "';"
        _db.executeDB(Sql)

        't49_shrikshihd
        Sql = "DELETE FROM t49_shrikshihd WHERE ""会社コード"" = '" & dbName & "';"
        _db.executeDB(Sql)

        't50_zikhd
        Sql = "DELETE FROM t50_zikhd WHERE ""会社コード"" = '" & dbName & "';"
        _db.executeDB(Sql)

        't70_inout
        Sql = "DELETE FROM t70_inout WHERE ""会社コード"" = '" & dbName & "';"
        _db.executeDB(Sql)

        ''t52_krurighd
        'Sql = "DELETE FROM t52_krurighd WHERE ""会社コード"" = '" & dbName & "';"
        '_db.executeDB(Sql)

        ''t53_krurigdt
        'Sql = "DELETE FROM t53_krurigdt WHERE ""会社コード"" = '" & dbName & "';"
        '_db.executeDB(Sql)

        ''t54_krsirehd
        'Sql = "DELETE FROM t54_krsirehd WHERE ""会社コード"" = '" & dbName & "';"
        '_db.executeDB(Sql)

        ''t55_krsiredt
        'Sql = "DELETE FROM t55_krsiredt WHERE ""会社コード"" = '" & dbName & "';"
        '_db.executeDB(Sql)

        ''t56_krskyuhd
        'Sql = "DELETE FROM t56_krskyuhd WHERE ""会社コード"" = '" & dbName & "';"
        '_db.executeDB(Sql)

        ''t57_krkikehd
        'Sql = "DELETE FROM t57_krkikehd WHERE ""会社コード"" = '" & dbName & "';"
        '_db.executeDB(Sql)

        ''t58_krnyukohd
        'Sql = "DELETE FROM t58_krnyukohd WHERE ""会社コード"" = '" & dbName & "';"
        '_db.executeDB(Sql)

        ''t59_krnyukodt
        'Sql = "DELETE FROM t59_krnyukodt WHERE ""会社コード"" = '" & dbName & "';"
        '_db.executeDB(Sql)

        ''t60_krshukohd
        'Sql = "DELETE FROM t60_krshukohd WHERE ""会社コード"" = '" & dbName & "';"
        '_db.executeDB(Sql)

        ''t61_krshukodt
        'Sql = "DELETE FROM t61_krshukodt WHERE ""会社コード"" = '" & dbName & "';"
        '_db.executeDB(Sql)

        ''t62_krnkinhd
        'Sql = "DELETE FROM t62_krnkinhd WHERE ""会社コード"" = '" & dbName & "';"
        '_db.executeDB(Sql)

        ''t63_krnkindt
        'Sql = "DELETE FROM t63_krnkindt WHERE ""会社コード"" = '" & dbName & "';"
        '_db.executeDB(Sql)

        ''t64_krshrihd
        'Sql = "DELETE FROM t64_krshrihd WHERE ""会社コード"" = '" & dbName & "';"
        '_db.executeDB(Sql)

        ''t65_krshridt
        'Sql = "DELETE FROM t65_krshridt WHERE ""会社コード"" = '" & dbName & "';"
        '_db.executeDB(Sql)

    End Sub

    ''新規作成ボタンを押したら
    'Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
    '    Dim companyCode As String = txtCompanyCd.Text
    '    Dim companyName As String = txtCompanyName.Text
    '    Dim userCode As String = txtUserCd.Text
    '    Dim userName As String = txtUserName.Text

    '    Dim Sql As String = ""

    '    Try
    '        '会社マスタを作成
    '        Sql = "INSERT INTO Public.m01_company( "
    '        Sql += "会社コード, 会社名, 会社略称, 郵便番号, 住所１, 住所２, 住所３, 電話番号, 
    '            ＦＡＸ番号, 代表者役職, 代表者名, 表示順, 備考, 更新者, 更新日, 銀行コード, 
    '            支店コード, 預金種目, 口座番号, 口座名義, 銀行名, 支店名, 前回締日, 今回締日, 
    '            次回締日, 在庫単価評価法, 前払法人税率, 会計用コード"
    '        Sql += ") VALUES ("
    '        Sql += "'" & companyCode & "', '" & companyName & "', 'テスト', '1310045', '東京都', '墨田区押上', '１丁目１−２', 'xxx-xxxx-xxxx', 
    '            'xxx-xxxx-xxxx', null, 'テスト太郎', 1, null, 'mikami', now(), '0123', 
    '            '03', 1, '0123456789', 'テスト株式会社', '東京銀行', 'スカイツリー支店', null, null, 
    '            null, 1, '0.025', null)"

    '        _db.executeDB(Sql)

    '        '会社マスタを作成
    '        Sql = "INSERT INTO public.m04_menu("
    '        Sql += "会社コード,処理ＩＤ,処理名,業務ＩＤ,業務名,説明,表示順,削除フラグ,更新者,更新日,英語用処理名,英語用業務名,英語用説明) VALUES "
    '        Sql += "('" & companyCode & "','H0110','見積登録','H01','見積業務','見積情報を新規登録します。',110,0,'" & userCode & "',now(),'Quotation registration','Quotation job',NULL),"
    '        Sql += "('" & companyCode & "','H0120','仕入単価入力','H01','見積業務','仕入単価の入力します。',130,0,'" & userCode & "',now(),'Purchase unit pric','Quotation job',NULL),"
    '        Sql += "('" & companyCode & "','H0130','見積修正','H01','見積業務','見積情報の修正をします。',140,0,'" & userCode & "',now(),'Quotation edit','Quotation job',NULL),"
    '        Sql += "('" & companyCode & "','H0140','見積複写','H01','見積業務','既存の見積情報を複写し、新規見積として登録します。',150,0,'" & userCode & "',now(),'Quotation copy','Quotation job',NULL),"
    '        Sql += "('" & companyCode & "','H0150','見積参照','H01','見積業務','見積情報を参照できます。見積依頼書・見積書の発行も行えます。',120,0,'" & userCode & "',now(),'Quotation data view','Quotation job',NULL),"
    '        Sql += "('" & companyCode & "','H0160','見積取消','H01','見積業務','見積情報を取消します。',160,0,'" & userCode & "',now(),'Cancel of quotation','Quotation job',NULL),"
    '        Sql += "('" & companyCode & "','H0210','受注登録','H02','受注業務','見積情報を基に受注を新規登録します。',210,0,'" & userCode & "',now(),'Job order registration','Job of receiving job order',NULL),"
    '        Sql += "('" & companyCode & "','H0220','受注編集','H02','受注業務','受注情報の修正をします。',220,1,'" & userCode & "',now(),'Edit job order','Job of receiving job order',NULL),"
    '        Sql += "('" & companyCode & "','H0240','受注取消','H02','受注業務','受注情報を取消します。',240,0,'" & userCode & "',now(),'Cancel of order','Job of receiving job order',NULL),"
    '        Sql += "('" & companyCode & "','H0250','受注参照','H02','受注業務','受注情報を参照できます。',240,0,'" & userCode & "',now(),'Order view','Job of receiving job order',NULL),"
    '        'Sql += "('" & companyCode & "','H0260','受注残一覧','H02','受注業務','受注残一覧を参照、発行できます。',250,0,'" & userCode & "',now(),'Order remaining list','Job of receiving job order',NULL),"
    '        Sql += "('" & companyCode & "','H0310','売上登録','H03','売上業務','売上情報の新規登録・編集が行えます。',310,0,'" & userCode & "',now(),'Sales registration','Sales operations',NULL),"
    '        Sql += "('" & companyCode & "','H0330','売上取消','H03','売上業務','売上情報を取消します。',330,0,'" & userCode & "',now(),'Cancel of sales','Sales operations',NULL),"
    '        Sql += "('" & companyCode & "','H0340','売上参照','H03','売上業務','売上情報を参照できます。',340,0,'" & userCode & "',now(),'Sales data view','Sales operations',NULL),"
    '        Sql += "('" & companyCode & "','H0410','出庫登録','H04','出庫業務','出庫情報の新規登録・編集が行えます。',410,0,'" & userCode & "',now(),'Goods delivery  registration','Goods delivery ',NULL),"
    '        Sql += "('" & companyCode & "','H0430','出庫取消','H04','出庫業務','出庫情報を取消します。',430,0,'" & userCode & "',now(),'Cancel of goods delivery ','Goods delivery ',NULL),"
    '        Sql += "('" & companyCode & "','H0440','出庫参照','H04','出庫業務','出庫情報を参照できます。納品書・受領書の発行も行えます。',440,0,'" & userCode & "',now(),'Goods delively data view ','Goods delivery ',NULL),"
    '        Sql += "('" & companyCode & "','H0510','発注登録','H05','発注業務','発注情報の新規登録をします。',510,0,'" & userCode & "',now(),'Order registration','Type of order',NULL),"
    '        Sql += "('" & companyCode & "','H0520','発注編集','H05','発注業務','発注情報の修正をします。',520,0,'" & userCode & "',now(),'Purchase order edit','Type of order',NULL),"
    '        Sql += "('" & companyCode & "','H0530','発注複写','H05','発注業務','既存の発注情報を複写し、新規発注として登録します。',530,0,'" & userCode & "',now(),'Purchase order copy','Type of order',NULL),"
    '        Sql += "('" & companyCode & "','H0540','発注取消','H05','発注業務','発注情報を取消します。',540,0,'" & userCode & "',now(),'Cancel of Purchase order','Type of order',NULL),"
    '        Sql += "('" & companyCode & "','H0550','発注参照','H05','発注業務','発注情報を参照できます。発注書の発行も行えます。',550,0,'" & userCode & "',now(),'Purchase order data view','Type of order',NULL),"
    '        Sql += "('" & companyCode & "','H0610','仕入登録','H06','仕入業務','仕入情報の新規登録・編集が行えます。',610,0,'" & userCode & "',now(),'Purchase registration','Purchasing job',NULL),"
    '        Sql += "('" & companyCode & "','H0630','仕入取消','H06','仕入業務','仕入情報を取消します。',630,0,'" & userCode & "',now(),'Cancel of purchase ','Purchasing job',NULL),"
    '        Sql += "('" & companyCode & "','H0640','仕入参照','H06','仕入業務','仕入情報を参照できます。',640,0,'" & userCode & "',now(),'Purchase data view','Purchasing job',NULL),"
    '        Sql += "('" & companyCode & "','H0710','入庫登録','H07','入庫業務','入庫情報の新規登録・編集が行えます。',710,0,'" & userCode & "',now(),'Goods receipt registration','Job (Goods receipt)',NULL),"
    '        Sql += "('" & companyCode & "','H0730','入庫取消','H07','入庫業務','入庫情報を取消します。',730,0,'" & userCode & "',now(),'Cancel of goods receipt','Job (Goods receipt)',NULL),"
    '        Sql += "('" & companyCode & "','H0740','入庫参照','H07','入庫業務','入庫情報を参照できます。',740,0,'" & userCode & "',now(),'Goods receipt view','Job (Goods receipt)',NULL),"
    '        Sql += "('" & companyCode & "','H0810','受発注登録','H08','受発注業務','見積情報を基に受注・発注を新規登録します。',170,0,'" & userCode & "',now(),'Job of job ordering and purchasing registration','Job of job ordering and purchasing',NULL),"
    '        Sql += "('" & companyCode & "','H0910','請求登録','H09','請求管理','請求情報の新規登録・編集が行えます。',810,0,'" & userCode & "',now(),'Billing registration','Control of Billing',NULL),"
    '        Sql += "('" & companyCode & "','H0930','請求取消','H09','請求管理','請求情報を取消します。',830,0,'" & userCode & "',now(),'Cancel of invoic','Control of Billing',NULL),"
    '        Sql += "('" & companyCode & "','H0940','請求参照','H09','請求管理','請求情報を参照できます。',840,0,'" & userCode & "',now(),'Billing data view','Control of Billing',NULL),"
    '        Sql += "('" & companyCode & "','H0950','請求書発行','H09','請求管理','請求計算をします。',850,0,'" & userCode & "',now(),'Invoicing','Control of Billing',NULL),"
    '        Sql += "('" & companyCode & "','H1010','入金登録','H10','入金管理','入金情報を登録できます。',910,0,'" & userCode & "',now(),'Money receipt registration','Money receipt management',NULL),"
    '        Sql += "('" & companyCode & "','H1020','入金取消','H10','入金管理','入金情報を取消します。',920,0,'" & userCode & "',now(),'Cancel of money receipt','Money receipt management',NULL),"
    '        Sql += "('" & companyCode & "','H1030','入金参照','H10','入金管理','入金情報を参照できます。',930,0,'" & userCode & "',now(),'Money receipt view','Money receipt management',NULL),"
    '        Sql += "('" & companyCode & "','H1110','買掛登録','H11','買掛管理','買掛情報の新規登録・編集が行えます。',1010,0,'" & userCode & "',now(),'Accounts Payable registration','Contorl of Accounts Payabl',NULL),"
    '        Sql += "('" & companyCode & "','H1130','買掛取消','H11','買掛管理','買掛情報を取消します。',1030,0,'" & userCode & "',now(),'Cancel of accounts payabl','Contorl of Accounts Payabl',NULL),"
    '        Sql += "('" & companyCode & "','H1140','買掛参照','H11','買掛管理','買掛情報を参照できます。',1040,0,'" & userCode & "',now(),'Payment data view','Contorl of Accounts Payabl',NULL),"
    '        Sql += "('" & companyCode & "','H1210','支払登録','H12','支払管理','支払情報を登録できます。',1110,0,'" & userCode & "',now(),'Payment registration','Control of payment',NULL),"
    '        Sql += "('" & companyCode & "','H1220','支払取消','H12','支払管理','支払情報を取消します。',1120,0,'" & userCode & "',now(),'Cancel of payment','Control of payment',NULL),"
    '        Sql += "('" & companyCode & "','H1230','支払参照','H12','支払管理','支払情報を参照できます。',1130,0,'" & userCode & "',now(),'Payment view','Control of payment',NULL),"
    '        Sql += "('" & companyCode & "','H1310','締処理ログ参照','H13','締処理業務','締処理ログを参照、締処理ができます。',1210,0,'" & userCode & "',now(),'Closing log view','Closing procedures job',NULL),"
    '        Sql += "('" & companyCode & "','H1320','仕訳出力','H13','締処理業務','仕訳出力ができます。',1220,1,NULL,NULL,NULL,NULL,NULL),"
    '        Sql += "('" & companyCode & "','M0110','汎用マスタ','M01','マスタ管理','汎用情報を登録できます。',2010,0,'" & userCode & "',now(),'Master','Master data registration',NULL),"
    '        Sql += "('" & companyCode & "','M0120','得意先マスタ','M01','マスタ管理','得意先情報を登録できます。',2020,0,'" & userCode & "',now(),'Customer master data','Master data registration',NULL),"
    '        Sql += "('" & companyCode & "','M0130','仕入先マスタ','M01','マスタ管理','仕入先情報を登録できます。',2030,0,'" & userCode & "',now(),'Supplier master data','Master data registration',NULL),"
    '        Sql += "('" & companyCode & "','M0140','会社マスタ','M01','マスタ管理','会社情報を登録できます。',2040,0,'" & userCode & "',now(),'Company master data','Master data registration',NULL),"
    '        Sql += "('" & companyCode & "','M0150','ユーザマスタ','M01','マスタ管理','ユーザ情報を登録できます。',2050,0,'" & userCode & "',now(),'User master data','Master data registration',NULL),"
    '        Sql += "('" & companyCode & "','M0160','言語マスタ','M01','マスタ管理','言語情報を登録できます。',2060,0,'" & userCode & "',now(),'Language master','Master data registration',NULL),"
    '        Sql += "('" & companyCode & "','M0170','在庫マスタ','M01','マスタ管理','在庫状況の参照ができます。',2070,0,'" & userCode & "',now(),'Inventory master','Master data registration',NULL),"
    '        Sql += "('" & companyCode & "','M0180','勘定科目マスタ','M01','マスタ管理','勘定科目の登録ができます。',2080,0,'" & userCode & "',now(),'Account master','Master data registration',NULL);"

    '        _db.executeDB(Sql)

    '        '汎用マスタ
    '        Sql = "INSERT INTO public.m90_hanyo"
    '        Sql += "(会社コード,固定キー,可変キー,表示順,文字１,文字２,文字３,文字４,文字５,文字６,数値１,数値２,数値３,数値４,数値５,数値６,メモ,更新者,更新日) VALUES "
    '        Sql += "('" & companyCode & "','1','1',1,'振込入金','Transfer payment',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'notes','" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','1','10',10,'諸口','Sundry',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'notes','" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','1','2',2,'振込手数料','Transfer fe',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'notes','" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','1','3',3,'現金入金','Cash payment',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'notes','" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','1','4',4,'手形受入','Bill acceptanc',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'notes','" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','1','5',5,'電子債権','Electronic Monetary Claim',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'notes','" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','1','6',6,'売上割引','Sales discount',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'notes','" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','1','7',7,'売上値引','Sales discounts',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'notes','" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','1','8',8,'リベート','Rebat',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'notes','" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','1','9',9,'相殺','Setoff',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'notes','" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','1002','1',1,'仕入','仕入',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),"
    '        Sql += "('" & companyCode & "','1002','2',2,'在庫','在庫',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),"
    '        Sql += "('" & companyCode & "','1002','9',9,'サービス','サービス',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),"
    '        Sql += "('" & companyCode & "','2','1',1,'振込入金',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'notes','" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','2','10',10,'諸口',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'notes','" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','2','2',2,'振込手数料',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'notes','" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','2','3',3,'現金入金',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'notes','" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','2','4',4,'手形受入',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'notes','" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','2','5',5,'電子債権',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'notes','" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','2','6',6,'売上割引',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'notes','" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','2','7',7,'売上値引',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'notes','" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','2','8',8,'リベート',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'notes','" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','2','9',9,'相殺',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'notes','" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','3','1',1,'先入先出法',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'notes','" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','3','2',2,'平均法',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'notes','" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','4','1',1,'日','day',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'notes','" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','4','2',2,'週','week',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'notes','" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','4','3',3,'ヶ月','month',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'notes','" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','5','1',1,'DDU','DDU',NULL,NULL,NULL,NULL,1,NULL,NULL,NULL,NULL,NULL,'notes','" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','5','2',2,'DDP','DDP',NULL,NULL,NULL,NULL,2,NULL,NULL,NULL,NULL,NULL,'notes','" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','5','3',3,'FOB','FOB',NULL,NULL,NULL,NULL,3,NULL,NULL,NULL,NULL,NULL,'notes','" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','5','4',4,'CIF','CIF',NULL,NULL,NULL,NULL,4,NULL,NULL,NULL,NULL,NULL,'notes','" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','5','5',5,'EXW','EXW',NULL,NULL,NULL,NULL,5,NULL,NULL,NULL,NULL,NULL,'notes','" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','5','6',6,'FCA','FCA',NULL,NULL,NULL,NULL,6,NULL,NULL,NULL,NULL,NULL,'notes','" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','5','7',7,'FAS','FAS',NULL,NULL,NULL,NULL,7,NULL,NULL,NULL,NULL,NULL,'notes','" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','5','8',8,'CFR','CFR',NULL,NULL,NULL,NULL,8,NULL,NULL,NULL,NULL,NULL,'notes','" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','5','9',9,'NA','NA','','','','',9,0,0,0,0,0,'','" & userCode & "',now())"

    '        _db.executeDB(Sql)


    '        'ユーザーマスタを作成
    '        Sql = "INSERT INTO public.m02_user( "
    '        Sql += "会社コード,ユーザＩＤ,氏名,略名,備考,無効フラグ,権限,言語,更新者,更新日"
    '        Sql += ") VALUES ("
    '        Sql += "'" & companyCode & "','" & userCode & "','" & userName & "','" & userName & "',null,0,0,'JPN','" & userCode & "',now())"

    '        _db.executeDB(Sql)

    '        'パスワードマスタを作成
    '        Sql = "INSERT INTO public.m03_pswd( "
    '        Sql += "会社コード,ユーザＩＤ,世代番号,適用開始日,適用終了日,パスワード,パスワード変更方法,有効期限,更新者,更新日"
    '        Sql += ") VALUES ("
    '        Sql += "'" & companyCode & "','" & userCode & "',1,CURRENT_DATE,'2099-12-31','" & userCode & "',1,'2099-12-31','" & userCode & "',now())"

    '        _db.executeDB(Sql)

    '        '採番マスタを作成
    '        Sql = "INSERT INTO public.m80_saiban( "
    '        Sql += "会社コード,採番キー,最新値,最小値,最大値,接頭文字,連番桁数,更新者,更新日"
    '        Sql += ") VALUES "
    '        Sql += "('" & companyCode & "','10',288,1,9999,'QT',4,'" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','100',8,1,9999,'AP',4,'" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','110',10,1,9999,'P',4,'" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','20',83,1,9999,'AO',4,'" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','30',17,1,9999,'PO',4,'" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','40',7,1,9999,'ER',4,'" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','50',7,1,9999,'PC',4,'" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','60',7,1,9999,'WH',4,'" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','70',8,1,9999,'LS',4,'" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','80',9,1,9999,'DM',4,'" & userCode & "',now()),"
    '        Sql += "('" & companyCode & "','90',8,1,9999,'PM',4,'" & userCode & "',now())"

    '        _db.executeDB(Sql)

    '    Catch ue As UsrDefException
    '        ue.dspMsg()
    '        Throw ue
    '    Catch ex As Exception
    '        'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
    '        Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
    '    End Try

    'End Sub
End Class