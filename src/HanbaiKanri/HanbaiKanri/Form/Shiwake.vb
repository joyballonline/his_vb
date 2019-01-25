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


#Region "仕分買掛金"

        '条件オプション
        'Sql = ""
        Sql = " ORDER BY "
        Sql += " 仕入日 "


        Dim dsSwkSirehd As DataSet = getDsData("t40_sirehd", Sql) '仕入データの取得

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

            Dim VATIN As Double = 0
            VATIN = dsSwkSirehd.Tables(RS).Rows(i)("仕入金額") * dsSwkSirehd.Tables(RS).Rows(i)("ＶＡＴ") / 100

            '入庫データ回しながら以下データ作成
            '借方：棚卸資産
            '貸方：買掛金

            Dim transactionid As String = DateTime.Now.ToString("MMddHHmmss" & i) 'TRANSACTIONID
            Dim countKeyID As Integer = 0

            For x As Integer = 0 To dsSWKNyukohd.Tables(RS).Rows.Count - 1
                'Dim test As String = dsSWKNyukohd.Tables(RS).Rows(x)("入庫日").ToString()



                'Sql = ""
                Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(x)("入庫日"), "yyyyMM") & "'" '入庫日
                Sql += "," & transactionid 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",100000000000" '棚卸資産
                Sql += "," & formatDouble(dsSWKNyukohd.Tables(RS).Rows(x)("仕入金額") * dsSWKNyukohd.Tables(RS).Rows(x)("VAT")) '仕入金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & dsSwkHattyu.Tables(RS).Rows(0)("仕入先名").ToString & "'" '仕入先コード
                Sql += ",'" & dsSwkHattyu.Tables(RS).Rows(0)("客先番号").ToString & "'" 'PO
                'Sql += ",'" & dsSwkSirehd.Tables(RS).Rows(i)("仕入日") & "'" '仕入日
                Sql += ",'" & Format(dsSwkSirehd.Tables(RS).Rows(i)("仕入日"), "yyyy-MM-dd") & "'" '仕入日
                Sql += ",''" '空でよし
                'Sql += "," & formatDouble(VATIN) '仕入金額 + VAT IN
                Sql += "," & formatDouble(dsSWKNyukohd.Tables(RS).Rows(x)("仕入金額") + dsSWKNyukohd.Tables(RS).Rows(x)("VAT")) '仕入金額 + VAT IN
                Sql += ",''" '空でよし
                Sql += ",''" '空でよし

                countKeyID = getCount(countKeyID)

                't67_swkhd データ登録
                updateT67Swkhd(Sql)

                'Sql = ""
                Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(x)("入庫日"), "yyyyMM") & "'" '入庫日
                Sql += "," & transactionid 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",100000000001" '買掛金
                'Sql += "," & formatDouble(-dsSWKNyukohd.Tables(RS).Rows(x)("仕入金額")) '仕入金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += "," & formatDouble(dsSWKNyukohd.Tables(RS).Rows(x)("仕入金額") * dsSWKNyukohd.Tables(RS).Rows(x)("VAT")) '仕入金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & dsSwkHattyu.Tables(RS).Rows(0)("仕入先名").ToString & "'" '仕入先コード
                Sql += ",'" & dsSwkHattyu.Tables(RS).Rows(0)("客先番号").ToString & "'" 'PO
                Sql += ",'" & Format(dsSwkSirehd.Tables(RS).Rows(i)("仕入日"), "yyyy-MM-dd") & "'" '仕入日
                Sql += ",''" '空でよし
                'Sql += "," & formatDouble(VATIN) '仕入金額 + VAT IN
                Sql += "," & formatDouble(dsSWKNyukohd.Tables(RS).Rows(x)("仕入金額") + dsSWKNyukohd.Tables(RS).Rows(x)("VAT")) '仕入金額 + VAT IN
                Sql += ",''" '空でよし
                Sql += ",''" '空でよし

                't67_swkhd データ登録
                updateT67Swkhd(Sql)

                countKeyID = getCount(countKeyID)

                'Sql = ""
                Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(x)("入庫日"), "yyyyMM") & "'" '入庫日
                Sql += "," & transactionid 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",100000000002" 'VAT-IN
                'Sql += "," & formatDouble(dsSWKNyukohd.Tables(RS).Rows(x)("仕入金額")) '仕入金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += "," & formatDouble(dsSWKNyukohd.Tables(RS).Rows(x)("仕入金額") * dsSWKNyukohd.Tables(RS).Rows(x)("VAT")) '仕入金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & dsSwkHattyu.Tables(RS).Rows(0)("仕入先名").ToString & "'" '仕入先コード
                Sql += ",'" & dsSwkHattyu.Tables(RS).Rows(0)("客先番号").ToString & "'" 'PO
                Sql += ",'" & Format(dsSwkSirehd.Tables(RS).Rows(i)("仕入日"), "yyyy-MM-dd") & "'" '仕入日
                Sql += ",''" '空でよし
                'Sql += "," & formatDouble(VATIN) '仕入金額 + VAT IN
                Sql += "," & formatDouble(dsSWKNyukohd.Tables(RS).Rows(x)("仕入金額") + dsSWKNyukohd.Tables(RS).Rows(x)("VAT")) '仕入金額 + VAT IN
                Sql += ",''" '空でよし
                Sql += ",''" '空でよし

                't67_swkhd データ登録
                updateT67Swkhd(Sql)

                countKeyID = getCount(countKeyID)


                'Sql = ""
                Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(x)("入庫日"), "yyyyMM") & "'" '入庫日
                Sql += "," & transactionid 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",100000000001" '買掛金
                'Sql += "," & formatDouble(-dsSWKNyukohd.Tables(RS).Rows(x)("仕入金額")) '仕入金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += "," & formatDouble(-dsSWKNyukohd.Tables(RS).Rows(x)("仕入金額")) '仕入金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & dsSwkHattyu.Tables(RS).Rows(0)("仕入先名").ToString & "'" '仕入先コード
                Sql += ",'" & dsSwkHattyu.Tables(RS).Rows(0)("客先番号").ToString & "'" 'PO
                Sql += ",'" & Format(dsSwkSirehd.Tables(RS).Rows(i)("仕入日"), "yyyy-MM-dd") & "'" '仕入日
                Sql += ",''" '空でよし
                'Sql += "," & formatDouble(VATIN) '仕入金額 + VAT IN
                Sql += "," & formatDouble(dsSWKNyukohd.Tables(RS).Rows(x)("仕入金額") + dsSWKNyukohd.Tables(RS).Rows(x)("VAT")) '仕入金額 + VAT IN
                Sql += ",''" '空でよし
                Sql += ",''" '空でよし

                't67_swkhd データ登録
                updateT67Swkhd(Sql)

                countKeyID = getCount(countKeyID)

            Next



        Next
#End Region


    End Sub



    'Public Sub ConvertDataTableToCsv(ds1 As DataSet, tableName As String, ColName As String, Name As String)

    '    Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
    '    '出力先パス
    '    Dim sOutPath As String = ""
    '    sOutPath = StartUp._iniVal.OutXlsPath

    '    '出力ファイル名
    '    Dim sOutFile As String = ""
    '    sOutFile = sOutPath & "\" & Name & ".csv"

    '    '書き込むファイルを開く
    '    Dim sr As New System.IO.StreamWriter(sOutFile, False, enc)
    '    Dim Sql As String = ""
    '    Dim reccnt As Integer = 0

    '    For i As Integer = 0 To ds1.Tables(RS).Rows.Count - 1
    '        Sql = ""
    '        Sql += "SELECT "
    '        Sql += "* "
    '        Sql += "FROM "
    '        Sql += "public"
    '        Sql += "."
    '        Sql += tableName
    '        Sql += " WHERE "
    '        Sql += "会社コード"
    '        Sql += " ILIKE  "
    '        Sql += "'"
    '        Sql += frmC01F10_Login.loginValue.BumonNM
    '        Sql += "'"
    '        Sql += " AND "
    '        Sql += ColName
    '        Sql += " = "
    '        Sql += "'"
    '        Sql += ds1.Tables(RS).Rows(i)(ColName)
    '        Sql += "'"

    '        Dim ds3 As DataSet = _db.selectDB(Sql, RS, reccnt)
    '        If i = 0 Then
    '            '基本列名
    '            For y As Integer = 0 To ds1.Tables(RS).Columns.Count - 1
    '                Dim field As String = ds1.Tables(RS).Columns(y).ColumnName
    '                field = EncloseDoubleQuotesIfNeed(field)
    '                sr.Write(field)
    '                If y < ds1.Tables(RS).Columns.Count - 1 Then
    '                    sr.Write(","c)
    '                End If
    '            Next

    '            '明細列名
    '            For z As Integer = 0 To ds3.Tables(RS).Columns.Count - 1
    '                Dim field As String = ds3.Tables(RS).Columns(z).ColumnName
    '                field = EncloseDoubleQuotesIfNeed(field)
    '                sr.Write(field)
    '                If z < ds3.Tables(RS).Columns.Count - 1 Then
    '                    sr.Write(","c)
    '                End If
    '            Next
    '            sr.Write(vbCrLf)
    '        End If

    '        For x As Integer = 0 To ds3.Tables(RS).Rows.Count - 1

    '            '基本
    '            For y As Integer = 0 To ds1.Tables(RS).Columns.Count - 1
    '                Dim field As String = ds1.Tables(RS).Rows(i)(y).ToString()
    '                field = EncloseDoubleQuotesIfNeed(field)
    '                sr.Write(field)
    '                If y < ds1.Tables(RS).Columns.Count - 1 Then
    '                    sr.Write(","c)
    '                End If
    '            Next

    '            '明細
    '            For z As Integer = 0 To ds3.Tables(RS).Columns.Count - 1
    '                Dim field As String = ds3.Tables(RS).Rows(x)(z).ToString()
    '                field = EncloseDoubleQuotesIfNeed(field)
    '                sr.Write(field)
    '                If z < ds3.Tables(RS).Columns.Count - 1 Then
    '                    sr.Write(","c)
    '                End If
    '            Next
    '            sr.Write(vbCrLf)
    '        Next
    '    Next
    '    sr.Close()
    'End Sub


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
                Dim valJvnumber As String = shiwakeData.Tables(RS).Rows(i)(9).ToString()
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
                    strXml += "<CURRENCYNAME>" & valVendorno & "</CURRENCYNAME>"
                    strXml += "<VENDORNO>" & valVendorno & "</VENDORNO>"
                    strXml += "<CUSTOMERNO>" & valCustomerno & "</CUSTOMERNO>"
                    strXml += "</ACCOUNTLINE>"

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
                    strXml += "<CURRENCYNAME>" & valVendorno & "</CURRENCYNAME>"
                    strXml += "<VENDORNO>" & valVendorno & "</VENDORNO>"
                    strXml += "<CUSTOMERNO>" & valCustomerno & "</CUSTOMERNO>"
                    strXml += "</ACCOUNTLINE>"
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

                _msgHd.dspMSG("CreateXML")
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
        Sql += "'" & frmC01F10_Login.loginValue.BumonNM & "'"
        Sql += txtParam

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
        Sql += "'" & frmC01F10_Login.loginValue.BumonNM & "'"
        Sql += param
        Sql += ") "
        'Console.WriteLine(Sql)
        _db.executeDB(Sql)
    End Sub

    'カウントアップ
    Private Function getCount(ByVal nowCount As Integer) As Integer
        Dim count As Integer
        count = nowCount + 1

        Return count
    End Function

    'カウントアップ
    Private Function formatDouble(ByVal val As Decimal) As Decimal
        Dim result As Decimal

        ' 小数点第三位で四捨五入し、小数点第二位まで出力
        result = Math.Round(val, 2, MidpointRounding.AwayFromZero)

        Return result
    End Function


    '最終的には削除する
    '会社コード=ZENBIを一括削除
    Private Sub BtnShiwakeClear_Click(sender As Object, e As EventArgs) Handles BtnShiwakeClear.Click
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = "DELETE FROM t67_swkhd WHERE ""会社コード"" = 'ZENBI';"

        _db.executeDB(Sql)

    End Sub
End Class