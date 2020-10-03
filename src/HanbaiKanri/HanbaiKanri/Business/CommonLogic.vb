'===============================================================================
'
'　SPIN
'　　（システム名）販売管理
'　　（処理機能名）共通定数
'    （機能ID）    CommonConst
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   狩野        2018/02/22                 新規              
'-------------------------------------------------------------------------------
Imports UtilMDL
Imports UtilMDL.DB
Imports UtilMDL.MSG
Imports UtilMDL.Text
Imports System.IO
Public Class CommonLogic
    ' PG制御文字
    Private Const N As String = ControlChars.NewLine        '改行文字
    Private Const RS As String = "RecSet"                   'レコードセットテーブル

    '-------------------------------------------------------------------------------
    'メンバー変数宣言
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf

    '===============================================================================
    '   構造体定義
    '===============================================================================
    '汎用マスタデータ格納用変数
    Public Structure ptHanyou_tb
        Dim KoteiKey As String
        Dim KahenKey As String
        Dim DispNo As Integer
        Dim Char1 As String
        Dim Char2 As String
        Dim Char3 As String
        Dim Char4 As String
        Dim Char5 As String
        Dim Number1 As Decimal
        Dim Number2 As Decimal
        Dim Number3 As Decimal
        Dim Number4 As Decimal
        Dim Number5 As Decimal
        Dim Memo As String
        Dim UpdNm As String
        Dim UpdDt As DateTime
    End Structure
    Public Shared uHanyou_tb() As ptHanyou_tb               '汎用マスタデータ

    '-------------------------------------------------------------------------------
    '   コンストラクタ
    '   （処理概要）　内部初期化を行う
    '   ●入力パラメタ   ：prmRefDbHd   DBハンドラ
    '                      prmMsgHd     MSGハンドラ
    '   ●メソッド戻り値 ：インスタンス
    '-------------------------------------------------------------------------------
    Public Sub New(ByVal prmDbHd As UtilDBIf, ByVal prmMsgHd As UtilMsgHandler)
        _db = prmDbHd
        _msgHd = prmMsgHd
    End Sub

    ''' <summary>
    ''' 操作履歴作成（L01_PROCLOG）
    ''' </summary>
    ''' <param name="companyCd"></param>
    ''' <param name="gyomuId"></param>
    ''' <param name="shoriId"></param>
    ''' <param name="sousa"></param>
    ''' <param name="kekka"></param>
    ''' <param name="str1"></param>
    ''' <param name="str2"></param>
    ''' <param name="str3"></param>
    ''' <param name="str4"></param>
    ''' <param name="str5"></param>
    ''' <param name="num1"></param>
    ''' <param name="num2"></param>
    ''' <param name="num3"></param>
    ''' <param name="num4"></param>
    ''' <param name="num5"></param>
    ''' <param name="userId"></param>
    Private Sub Insert_L01_ProcLog(ByVal companyCd As String, ByVal gyomuId As Object, ByVal shoriId As String, ByVal sousa As String, ByVal kekka As String,
                                  ByVal str1 As Object, ByVal str2 As Object, ByVal str3 As Object, ByVal str4 As Object, ByVal str5 As Object,
                                  ByVal num1 As Object, ByVal num2 As Object, ByVal num3 As Object, ByVal num4 As Object, ByVal num5 As Object, ByVal userId As String)

        Dim sql As String = ""
        sql = sql & "INSERT INTO l10_proclog ( "
        sql = sql & "   会社コード "
        sql = sql & " , 業務ＩＤ "
        sql = sql & " , 処理ＩＤ "
        sql = sql & " , 操作契機 "
        sql = sql & " , 処理結果 "
        sql = sql & " , 文字１ "
        sql = sql & " , 文字２ "
        sql = sql & " , 文字３ "
        sql = sql & " , 文字４ "
        sql = sql & " , 文字５ "
        sql = sql & " , 数値１ "
        sql = sql & " , 数値２ "
        sql = sql & " , 数値３ "
        sql = sql & " , 数値４ "
        sql = sql & " , 数値５ "
        sql = sql & " , 更新者 "
        sql = sql & " , 更新日 "
        sql = sql & ") VALUES ( "
        sql = sql & "   " & nullToStr(companyCd)          '会社コード
        sql = sql & " , " & nullToStr(gyomuId)            '業務ＩＤ
        sql = sql & " , " & nullToStr(shoriId)            '処理ＩＤ
        sql = sql & " , " & nullToStr(sousa)              '操作契機
        sql = sql & " , " & nullToStr(kekka)              '操作結果
        sql = sql & " , " & nullToStr(str1)
        sql = sql & " , " & nullToStr(str2)
        sql = sql & " , " & nullToStr(str3)
        sql = sql & " , " & nullToStr(str4)
        sql = sql & " , " & nullToStr(str5)
        sql = sql & " , " & nullToNum(num1)
        sql = sql & " , " & nullToNum(num2)
        sql = sql & " , " & nullToNum(num3)
        sql = sql & " , " & nullToNum(num4)
        sql = sql & " , " & nullToNum(num5)
        sql = sql & " , '" & _db.rmSQ(userId) & "' "      '更新者
        sql = sql & " , now() "
        sql = sql & ") "
        _db.executeDB(sql)

    End Sub

    ''' <summary>
    ''' システム日付取得
    ''' </summary>
    ''' <returns></returns>
    Public Function getSysDdate() As String

        Dim sql As String = ""
        sql = sql & "SELECT to_char(current_timestamp, 'YYYY/MM/DD') AS SYSDATE"
        Dim reccnt As Integer = 0
        Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)

        If reccnt <= 0 Then
            Throw New UsrDefException("", _msgHd.getMSG("NonSysDate", frmC01F10_Login.loginValue.Language))
        End If

        Return _db.rmNullStr(ds.Tables(RS).Rows(0)("SYSDATE"))

    End Function

    ''' <summary>
    ''' システム日付の曜日取得
    ''' </summary>
    ''' <returns></returns>
    Public Function getSysWeek() As String

        Dim sql As String = ""
        sql = sql & "SELECT to_char(current_timestamp, 'TMDy') AS SYSWEEK"
        Dim reccnt As Integer = 0
        Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)

        If reccnt <= 0 Then
            Throw New UsrDefException("曜日", _msgHd.getMSG("NonSysDate", frmC01F10_Login.loginValue.Language))
        End If

        Return _db.rmNullStr(ds.Tables(RS).Rows(0)("SYSWEEK"))

    End Function

    Public Function nullToStr(ByVal obj As Object) As String
        If (obj Is DBNull.Value) Then
            Return "NULL"
        Else
            Return "'" & obj & "'"
        End If
    End Function

    Public Function nullToNum(ByVal obj As Object) As String
        If (obj Is DBNull.Value) Then
            Return "NULL"
        Else
            Return obj
        End If
    End Function

    '-------------------------------------------------------------------------------
    '   伝票番号取得処理
    '   （処理概要）採番マスタから、入力パラメータに対応したレコードを取得し、最新値＋１した値を返却する
    '               最大値を超える場合は、最小値を返却する
    '   ●入力パラメタ   ：prmCompanyCd  会社コード
    '                    ：prmSaibanKbn  採番区分
    '   ●メソッド戻り値 ：伝票番号
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' 伝票番号取得処理（GetDenpyoNo）
    ''' </summary>
    ''' <param name="prmCompanyCd"></param>
    ''' <param name="prmSaibanKbn"></param>
    Private Function GetDenpyoNo(ByVal prmCompanyCd As String, ByVal prmSaibanKbn As String) As String

        '①採番マスタ(M80)から、入力P)採番区分に対応したレコードを取得する
        Dim sql As String = ""
        sql = sql & "SELECT "
        sql = sql & "    最新値 "
        sql = sql & "   ,最小値 "
        sql = sql & "   ,最大値 "
        sql = sql & "   ,接頭文字 "
        sql = sql & "   ,連番桁数 "
        sql = sql & "FROM M80_SAIBAN "
        sql = sql & "WHERE 会社コード = '" & _db.rmSQ(prmCompanyCd) & "' "
        sql = sql & "  AND 採番キー = '" & _db.rmSQ(prmSaibanKbn) & "' "
        Dim reccnt As Integer = 0
        Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)

        If reccnt <= 0 Then
            _msgHd.dspMSG("NoSaibanMst", frmC01F10_Login.loginValue.Language)
            Return ""
        End If

        '②最新値＋１をした結果、最大値を超える場合は、最新値を最小値に置換して更新
        '　最新値＋１をした結果、最大値を超えない場合は、＋１した値で更新
        Dim curnum As Integer = 0
        If _db.rmNullInt(ds.Tables(RS).Rows(0)("最新値")) + 1 > _db.rmNullInt(ds.Tables(RS).Rows(0)("最大値")) Then
            curnum = _db.rmNullInt(ds.Tables(RS).Rows(0)("最小値"))
        Else
            curnum = _db.rmNullInt(ds.Tables(RS).Rows(0)("最新値")) + 1
        End If
        sql = ""
        sql = sql & "UPDATE M80_SAIBAN SET "
        sql = sql & "    最新値 = " & curnum
        sql = sql & "   ,更新者 = 'SYSTEM' "
        sql = sql & "   ,更新日 = current_timestamp "
        sql = sql & "WHERE 会社コード = '" & _db.rmSQ(prmCompanyCd) & "' "
        sql = sql & "  AND 採番キー = '" & _db.rmSQ(prmSaibanKbn) & "' "
        _db.executeDB(sql)

        '③②取得値から、M80)連番桁数分を切り取り（右から）不足桁には前ゼロを埋め込み
        '　先頭に、「接頭文字」を付加した値を呼び出し元に返却する
        Dim denpyono As String = ""
        denpyono = _db.rmNullStr(ds.Tables(RS).Rows(0)("接頭文字")) & curnum.ToString("D" & _db.rmNullInt(ds.Tables(RS).Rows(0)("連番桁数")).ToString)

        Return denpyono

    End Function

    '-------------------------------------------------------------------------------
    '   販売単価取得処理
    '   （処理概要）販売単価マスタから、入力パラメータに対応した単価を取得する
    '   ●入力パラメタ   ：prmCompanyCd   会社コード
    '                    ：prmGoodsCd     商品コード
    '                    ：prmCustomerCd  取引先コード
    '                    ：prmChakuDt     着日
    '   ●出力パラメタ   ：prmSalesTanka  販売単価
    '                    ：prmTankaPtn    単価パターン（1:特売単価,2:出荷先別単価,3:商品別単価,9:単価なし）
    '   ●メソッド戻り値 ：なし
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' 販売単価取得処理（GetHanbaiTanka）
    ''' </summary>
    ''' <param name="prmCompanyCd"></param>
    ''' <param name="prmGoodsCd"></param>
    ''' <param name="prmCustomerCd"></param>
    ''' <param name="prmChakuDt"></param>
    ''' <param name="prmSalesTanka"></param>
    ''' <param name="prmTankaPtn"></param>
    Private Sub GetSalesTanka(ByVal prmCompanyCd As String, ByVal prmGoodsCd As String, ByVal prmCustomerCd As String, ByVal prmChakuDt As String,
                             ByRef prmSalesTanka As Decimal, ByRef prmTankaPtn As String)

        '①出荷先別期間指定特売単価ありの場合（該当レコードが存在しない場合、②へ）
        Dim sql As String = ""
        sql = sql & "SELECT "
        sql = sql & "    販売単価 "
        sql = sql & "FROM M25_SLPRICE "
        sql = sql & "WHERE 会社コード = '" & _db.rmSQ(prmCompanyCd) & "' "
        sql = sql & "  AND 商品コード = '" & _db.rmSQ(prmGoodsCd) & "' "
        sql = sql & "  AND 特売区分 = '" & _db.rmSQ(CommonConst.TOKUBAI_KBN_BARGAIN) & "' "       '1:特売
        sql = sql & "  AND 取引先コード = '" & _db.rmSQ(prmCustomerCd) & "' "
        sql = sql & "  AND 適用開始日 <= '" & prmChakuDt & "' "
        sql = sql & "  AND 適用終了日 >= '" & prmChakuDt & "' "
        Dim reccnt As Integer = 0
        Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)

        If reccnt > 0 Then
            prmTankaPtn = CommonConst.TANKA_PTN_BARGAIN     '1:特売単価
            prmSalesTanka = _db.rmNullDouble(ds.Tables(RS).Rows(0)("販売単価"))
            Exit Sub
        End If

        '②出荷先別通常単価ありの場合（該当レコードが存在しない場合、③へ）
        sql = ""
        sql = sql & "SELECT "
        sql = sql & "    販売単価 "
        sql = sql & "FROM M25_SLPRICE "
        sql = sql & "WHERE 会社コード = '" & _db.rmSQ(prmCompanyCd) & "' "
        sql = sql & "  AND 商品コード = '" & _db.rmSQ(prmGoodsCd) & "' "
        sql = sql & "  AND 特売区分 = '" & _db.rmSQ(CommonConst.TOKUBAI_KBN_NORMAL) & "' "        '0:通常
        sql = sql & "  AND 取引先コード = '" & _db.rmSQ(prmCustomerCd) & "' "
        sql = sql & "  AND 適用開始日 <= '" & prmChakuDt & "' "
        sql = sql & "  AND 適用終了日 >= '" & prmChakuDt & "' "
        reccnt = 0
        ds = _db.selectDB(sql, RS, reccnt)

        If reccnt > 0 Then
            prmTankaPtn = CommonConst.TANKA_PTN_CUSTOMER        '2:出荷先別単価
            prmSalesTanka = _db.rmNullDouble(ds.Tables(RS).Rows(0)("販売単価"))
            Exit Sub
        End If

        '③出荷先別通常単価ありの場合（該当レコードが存在しない場合、④へ）
        sql = ""
        sql = sql & "SELECT "
        sql = sql & "    販売単価 "
        sql = sql & "FROM M25_SLPRICE "
        sql = sql & "WHERE 会社コード = '" & _db.rmSQ(prmCompanyCd) & "' "
        sql = sql & "  AND 商品コード = '" & _db.rmSQ(prmGoodsCd) & "' "
        sql = sql & "  AND 特売区分 = '" & _db.rmSQ(CommonConst.TOKUBAI_KBN_NORMAL) & "' "        '0:通常
        sql = sql & "  AND 取引先コード = '" & _db.rmSQ(CommonConst.CUSTOMER_CODE_ALL) & "' "     'ALL:指定なし
        sql = sql & "  AND 適用開始日 <= '" & prmChakuDt & "' "
        sql = sql & "  AND 適用終了日 >= '" & prmChakuDt & "' "
        reccnt = 0
        ds = _db.selectDB(sql, RS, reccnt)

        If reccnt > 0 Then
            prmTankaPtn = CommonConst.TANKA_PTN_GOODS           '3:商品別単価
            prmSalesTanka = _db.rmNullDouble(ds.Tables(RS).Rows(0)("販売単価"))
            Exit Sub
        End If

        '④値を返却する（該当単価なしの場合）
        prmTankaPtn = CommonConst.TANKA_PTN_NONE                '9:単価なし
        prmSalesTanka = 0

    End Sub


    '-------------------------------------------------------------------------------
    '  汎用マスタデータ取得
    '   （処理概要）汎用マスタのデータを読み込み、内部テーブルに格納する
    '-------------------------------------------------------------------------------
    Private Sub Get_HanyouMST()

        Try

            Dim SQL As String
            Dim lRecCnt As Long
            Dim oDataSet As DataSet             ' データセット型

            SQL = "SELECT * FROM M90_HANYO"
            SQL = SQL & " ORDER BY 固定キー"
            SQL = SQL & "         ,可変キー"
            'SQL発行
            oDataSet = _db.selectDB(SQL, RS, lRecCnt)                           '抽出結果をDSへ格納

            If lRecCnt = 0 Then
                Call _msgHd.dspMSG("I003", "M90_HANYO")                         '対象データがありません。
                Exit Sub
            End If

            ReDim CommonLogic.uHanyou_tb(lRecCnt)

            For lCnt As Long = 0 To lRecCnt - 1
                With CommonLogic.uHanyou_tb(lCnt)
                    .KoteiKey = _db.rmNullStr(oDataSet.Tables(RS).Rows(lCnt)("固定キー"))   '固定キー
                    .KahenKey = _db.rmNullStr(oDataSet.Tables(RS).Rows(lCnt)("可変キー"))   '可変キー
                    .DispNo = _db.rmNullInt(oDataSet.Tables(RS).Rows(lCnt)("表示順"))       '表示順
                    .Char1 = _db.rmNullStr(oDataSet.Tables(RS).Rows(lCnt)("文字１"))        '文字１
                    .Char2 = _db.rmNullStr(oDataSet.Tables(RS).Rows(lCnt)("文字２"))        '文字２
                    .Char3 = _db.rmNullStr(oDataSet.Tables(RS).Rows(lCnt)("文字３"))        '文字３
                    .Char4 = _db.rmNullStr(oDataSet.Tables(RS).Rows(lCnt)("文字４"))        '文字４
                    .Char5 = _db.rmNullStr(oDataSet.Tables(RS).Rows(lCnt)("文字５"))        '文字５
                    .Number1 = _db.rmNullLong(oDataSet.Tables(RS).Rows(lCnt)("数値１"))     '数値１
                    .Number2 = _db.rmNullLong(oDataSet.Tables(RS).Rows(lCnt)("数値２"))     '数値２
                    .Number3 = _db.rmNullLong(oDataSet.Tables(RS).Rows(lCnt)("数値３"))     '数値３
                    .Number4 = _db.rmNullLong(oDataSet.Tables(RS).Rows(lCnt)("数値４"))     '数値４
                    .Number5 = _db.rmNullLong(oDataSet.Tables(RS).Rows(lCnt)("数値５"))     '数値５
                    .Memo = _db.rmNullStr(oDataSet.Tables(RS).Rows(lCnt)("メモ"))           'メモ
                    .UpdNm = _db.rmNullStr(oDataSet.Tables(RS).Rows(lCnt)("更新者"))        '更新者
                    .UpdDt = _db.rmNullDate(oDataSet.Tables(RS).Rows(lCnt)("更新日"))       '更新日
                End With
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    ''' <summary>
    ''' 住所データ取得
    ''' </summary>
    ''' <param name="prmPostalCd">郵便番号</param>
    ''' <returns>住所データ</returns>
    Private Function getAddress(ByVal prmPostalCd As String) As DataSet

        Dim sql As String = ""

        sql &= N & " SELECT "
        sql &= N & "        z.都道府県名 "
        sql &= N & "       ,z.市区町村名 "
        sql &= N & "       ,z.町域名 "
        sql &= N & " FROM   M70_ZIP              z "
        sql &= N & " WHERE  z.郵便番号         = '" + _db.rmSQ(prmPostalCd) + "' "

        Dim reccnt As Integer = 0

        Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)

        Return ds

    End Function

    ''' <summary>
    ''' 取引先データ取得
    ''' </summary>
    ''' <param name="prmCompanyCd">会社コード</param>
    ''' <param name="prmTargetKbn">対象区分</param>
    ''' <param name="prmTorihikisakiCd">取引先コード</param>
    ''' <param name="prmTorihikisakiName">取引先名</param>
    ''' <param name="prmDirectInputFlg">直接入力フラグ</param>
    ''' <returns>取引先データ</returns>
    Private Function getTorihikisaki(ByVal prmCompanyCd As String,
                                    ByVal prmTargetKbn As String,
                                    ByVal prmTorihikisakiCd As String,
                                    ByVal prmTorihikisakiName As String,
                                    ByVal prmDirectInputFlg As Boolean
                                   ) As DataSet

        Dim sql As String = ""

        sql &= N & "         SELECT "
        sql &= N & "                CST.取引先コード "
        sql &= N & "               ,CST.取引先名 "                                                     '取引先マスタ
        '分類
        Select Case prmTargetKbn
            Case CommonConst.TORIHIKISAKI_TARGET_KBN_SEIKYU
                sql &= N & "       ,'" & _db.rmSQ(CommonConst.TORIHIKISAKI_BUNRUINM_SEIKYU) & "'   AS 分類 "
            Case CommonConst.TORIHIKISAKI_TARGET_KBN_SHUKKA
                sql &= N & "       ,HAN.文字１                                                     AS 分類 "
            Case CommonConst.TORIHIKISAKI_TARGET_KBN_SHUKKAG
                sql &= N & "       ,'" & _db.rmSQ(CommonConst.TORIHIKISAKI_BUNRUINM_SHUKKAG) & "'  AS 分類 "
            Case CommonConst.TORIHIKISAKI_TARGET_KBN_SHIIRE
                sql &= N & "       ,'" & _db.rmSQ(CommonConst.TORIHIKISAKI_BUNRUINM_SHIIRE) & "'   AS 分類 "
            Case CommonConst.TORIHIKISAKI_TARGET_KBN_SHIHARAI
                sql &= N & "       ,'" & _db.rmSQ(CommonConst.TORIHIKISAKI_BUNRUINM_SHIHARAI) & "' AS 分類 "
        End Select
        sql &= N & "               ,COALESCE(CST.住所１,'') || COALESCE(CST.住所２,'') || COALESCE(CST.住所３,'') AS 住所 "
        sql &= N & "               ,CST.電話番号 "
        sql &= N & "               ,CST.メモ                    AS 備考 "
        sql &= N & "         FROM   M10_CUSTOMER                CST "                                                         '取引先マスタ
        If prmTargetKbn = CommonConst.TORIHIKISAKI_TARGET_KBN_SHUKKA Then
            sql &= N & "     LEFT   JOIN M90_HANYO              HAN "                                                         '汎用マスタ
            sql &= N & "        ON  HAN.会社コード            = CST.会社コード "
            sql &= N & "       AND  HAN.固定キー              = '" & _db.rmSQ(CommonConst.HANYO_KOTEI_SKBUNRUI) & "' "
            sql &= N & "       AND  HAN.可変キー              = CST.出荷先分類 "
        End If
        sql &= N & "         WHERE  CST.会社コード            = '" & _db.rmSQ(prmCompanyCd) & "' "
        '取引先コード
        If prmTorihikisakiCd <> String.Empty Then
            If prmDirectInputFlg Then
                '直接入力の場合、取引先コード完全一致
                sql &= N & "   AND  CST.取引先コード          = '" & _db.rmSQ(prmTorihikisakiCd) & "' "
            Else
                '上記以外の場合、取引先コード前方一致
                sql &= N & "   AND  CST.取引先コード       LIKE '" & _db.rmSQ(prmTorihikisakiCd) & "%' "
            End If
        End If
        '取引先名
        If prmTorihikisakiName <> String.Empty Then
            sql &= N & "       AND  CST.取引先名           LIKE '%" & _db.rmSQ(prmTorihikisakiName) & "%' "
        End If
        '対象区分
        Select Case prmTargetKbn
            Case CommonConst.TORIHIKISAKI_TARGET_KBN_SEIKYU
                sql &= N & "   AND  CST.請求先該当            = 1 "
            Case CommonConst.TORIHIKISAKI_TARGET_KBN_SHUKKA
                sql &= N & "   AND  CST.出荷先該当            = 1 "
            Case CommonConst.TORIHIKISAKI_TARGET_KBN_SHUKKAG
                sql &= N & "   AND  CST.出荷先Ｇ該当          = 1 "
            Case CommonConst.TORIHIKISAKI_TARGET_KBN_SHIIRE
                sql &= N & "   AND  CST.仕入先該当            = 1 "
            Case CommonConst.TORIHIKISAKI_TARGET_KBN_SHIHARAI
                sql &= N & "   AND  CST.支払先該当            = 1 "
        End Select

        Dim reccnt As Integer = 0

        Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)

        Return ds

    End Function

    ' fuketa common
    'param1：String テーブル名
    'param2：String 詳細条件
    'Return: DataSet
    Public Function getDsData(ByVal tableName As String, Optional ByRef txtParam As String = "") As DataSet
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        Sql += "SELECT * FROM public." & tableName
        Sql += " WHERE "
        Sql += "会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += txtParam
        Return _db.selectDB(Sql, RS, reccnt)
    End Function

    '汎用マスタから固定キー、可変キーに応じた結果を返す
    'param1：String 固定キー
    'param2：String 可変キー
    'Return: DataSet
    Public Function getDsHanyoData(ByVal prmFixed As String, Optional ByVal prmVariable As String = "") As DataSet
        Dim Sql As String = ""

        Sql = " AND "
        Sql += "固定キー ILIKE '" & prmFixed & "'"

        If prmVariable IsNot "" Then
            Sql += " AND "
            Sql += "可変キー ILIKE '" & prmVariable & "'"
        End If

        Sql += " order by 表示順 "

        'リードタイムのリストを汎用マスタから取得
        Return getDsData("m90_hanyo", Sql)
    End Function

    '汎用マスタから固定キー、可変キーに応じた結果を返す
    'param1：String 固定キー
    'param2：String 可変キー
    'Return: DataSet
    Public Function getDsHanyoDataEx(ByVal prmFixed As String, Optional ByVal prmVariable As String = "") As DataSet
        Dim Sql As String = ""
        Dim strViewText As String = ""
        Dim strArrayData As String() = prmVariable.Split(","c)

        Sql = " AND "
        Sql += "固定キー ILIKE '" & prmFixed & "'"

        If strArrayData.Length <> 0 Then
            Sql += " AND ( "
            For i As Integer = 0 To strArrayData.Length - 1
                Sql += IIf(i > 0, " OR ", "")
                Sql += "可変キー ILIKE '" & strArrayData(i) & "'"
            Next
            Sql += " ) "
        End If

        Sql += " order by 表示順 "

        'リードタイムのリストを汎用マスタから取得
        Return getDsData("m90_hanyo", Sql)
    End Function

    'param1：String 採番キー
    'param2：DateTime 登録日
    'Return: String 伝票番号
    '伝票番号を取得
    'cymn, 
    Public Function getSaiban(ByVal key As String, ByVal today As DateTime) As String
        Dim Sql As String = ""
        Dim saibanID As String = ""
        Dim reccnt As Integer = 0 'DB用（デフォルト）

        Try
            Sql = "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public.m80_saiban"
            Sql += " WHERE "
            Sql += "会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND "
            Sql += "採番キー = '" & key & "'"

            Dim dsSaiban As DataSet = _db.selectDB(Sql, RS, reccnt)

            saibanID = dsSaiban.Tables(RS).Rows(0)("接頭文字")
            saibanID += today.ToString("MMdd")
            saibanID += dsSaiban.Tables(RS).Rows(0)("最新値").ToString.PadLeft(dsSaiban.Tables(RS).Rows(0)("連番桁数"), "0")

            Dim keyNo As Integer

            If dsSaiban.Tables(RS).Rows(0)("最新値") = dsSaiban.Tables(RS).Rows(0)("最大値") Then
                '最新値が最大と同じ場合、最小値にリセット
                keyNo = dsSaiban.Tables(RS).Rows(0)("最小値")
            Else
                '最新値+1
                keyNo = dsSaiban.Tables(RS).Rows(0)("最新値") + 1
            End If

            Sql = "UPDATE "
            Sql += "Public.m80_saiban "
            Sql += "SET "
            Sql += " 最新値 "
            Sql += " = '"
            Sql += keyNo.ToString
            Sql += "', "
            Sql += "更新者"
            Sql += " = '"
            Sql += frmC01F10_Login.loginValue.TantoNM
            Sql += "', "
            Sql += "更新日"
            Sql += " = '"
            Sql += UtilClass.formatDatetime(today)
            Sql += "' "
            Sql += "WHERE"
            Sql += " 会社コード"
            Sql += "='"
            Sql += frmC01F10_Login.loginValue.BumonCD
            Sql += "'"
            Sql += " AND"
            Sql += " 採番キー = '" & key & "'"

            _db.executeDB(Sql)

            Return saibanID
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Function

    'Return: DataTable
    Public Function getLeadTime() As DataTable

        Dim Sql As String = ""
        Sql += " AND 固定キー = '" & CommonConst.FIXED_KEY_READTIME & "'"

        Dim ds As DataSet = getDsData("m90_hanyo", Sql)

        Dim table As New DataTable("Table")
        table.Columns.Add("Display", GetType(String))
        table.Columns.Add("Value", GetType(Integer))

        'リードタイム単位の多言語対応
        For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                table.Rows.Add(ds.Tables(RS).Rows(i)("文字２"), ds.Tables(RS).Rows(i)("可変キー"))
            Else
                table.Rows.Add(ds.Tables(RS).Rows(i)("文字１"), ds.Tables(RS).Rows(i)("可変キー"))
            End If

        Next
        Return table
    End Function

    ' ordering
    Public Function getCurrency(ByVal prmVal As Integer) As String
        Dim Sql As String = " AND 取消区分 = '" & CommonConst.FLAG_ENABLED & "'"
        Sql += " AND 採番キー =" & prmVal.ToString

        Dim ds As DataSet = getDsData("m25_currency", Sql)

        Return ds.Tables(RS).Rows(0)("通貨コード")

    End Function

    'Excel出力する際のチェック
    Public Function excelOutput(ByVal prmFilePath As String) As Boolean
        Dim fileChk As String = Dir(prmFilePath)
        '同名ファイルがあるかどうかチェック
        If fileChk <> "" Then
            Dim result = _msgHd.dspMSG("confirmFileExist", frmC01F10_Login.loginValue.Language, prmFilePath)
            If result = DialogResult.No Then
                Return False
            End If

            Try
                'ファイルが開けるかどうかチェック
                Dim sr As StreamReader = New StreamReader(prmFilePath)
                sr.Close() '処理が通ったら閉じる
            Catch ex As Exception
                '開けない場合はアラートを表示してリターンさせる
                MessageBox.Show(ex.Message, CommonConst.AP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End Try

            Return True
        End If
        Return True
    End Function

    '現在庫から出庫
    Public Function m_issue_from_onhand(ByVal currentLS As String,
                                       ByVal sWarehouse_ As String,
                                       ByVal sLine_no_ As String, 'ByVal sInout_ As String,
                                       ByVal sMaker As String,
                                       ByVal sItem As String,
                                       ByVal sSpec As String,
                                       ByVal currentVal As Long,
                                       ByVal sUnit_ As String,
                                       ByVal sRemark_ As String,
                                       ByVal sLotno_ As String,
                                       ByVal purclass_ As String,
                                       ByVal dtIssueDate_ As String
                                        ) As Boolean
        Return m_insert_t70_inout(currentLS, sWarehouse_, sLine_no_, CommonConst.INOUT_KBN_NORMAL.ToString, sMaker, sItem, sSpec, currentVal, sUnit_, sRemark_, sLotno_, purclass_, dtIssueDate_, 2, CommonConst.AC_KBN_NORMAL)
    End Function

    't70_inout にデータ登録
    Public Function m_insert_t70_inout(ByVal currentLS As String,
                                       ByVal sWarehouse_ As String,
                                       ByVal sLine_no_ As String,
                                       ByVal sInout_ As String,
                                       ByVal sMaker As String,
                                       ByVal sItem As String,
                                       ByVal sSpec As String,
                                       ByVal currentVal As Long,
                                       ByVal sUnit_ As String,
                                       ByVal sRemark_ As String,
                                       ByVal sLotno_ As String,
                                       ByVal purclass_ As String,
                                       ByVal dtIssueDate_ As String,
                                       ByVal intNyuSyukoCode As Integer, ByVal intHikiate As Integer) As Boolean

        Dim sql As String = vbNullString
        If intHikiate.ToString = "" Then
            intHikiate = 0
        End If

        If intNyuSyukoCode = 2 Then
            If intHikiate = 0 Then
                If m21_get_qty_(sMaker, sItem, sSpec, sWarehouse_) < currentVal Then
                    Throw New Exception(_msgHd.getMSG("chkShukoOverError", frmC01F10_Login.loginValue.Language).dspStr & " " & sItem)
                End If
            End If
        End If

        sql = "INSERT INTO "
        sql += "Public."
        sql += "t70_inout("
        sql += "会社コード, 入出庫区分, 倉庫コード, 伝票番号, 行番号, 入出庫種別"
        sql += ", 引当区分, メーカー, 品名, 型式, 数量, 単位, 備考, 入出庫日"
        sql += ", 取消区分, 更新者, 更新日, ロケ番号, 仕入区分)"
        sql += " VALUES('"
        sql += frmC01F10_Login.loginValue.BumonCD '会社コード
        sql += "', '"
        sql += intNyuSyukoCode.ToString           '入出庫区分 1.入庫, 2.出庫
        sql += "', '"
        sql += sWarehouse_ '倉庫コード
        sql += "', '"
        sql += currentLS '伝票番号（出庫番号）
        sql += "', '"
        sql += sLine_no_    '行番号
        sql += "', '"
        sql += sInout_ '入出庫種別
        sql += "', '"
        sql += intHikiate.ToString '引当区分
        sql += "', '"
        sql += UtilClass.escapeSql(sMaker) 'メーカー
        sql += "', '"
        sql += UtilClass.escapeSql(sItem) '品名
        sql += "', '"
        sql += UtilClass.escapeSql(sSpec) '型式
        sql += "', '"
        sql += currentVal.ToString '数量
        sql += "', '"
        sql += sUnit_ '単位
        sql += "', '"
        sql += UtilClass.escapeSql(sRemark_) '備考
        sql += "', '"
        sql += dtIssueDate_ '入出庫日
        sql += "', '"
        sql += CommonConst.CANCEL_KBN_ENABLED.ToString '取消区分
        sql += "', '"
        sql += frmC01F10_Login.loginValue.TantoNM      '更新者
        sql += "', '"
        sql += UtilClass.formatDatetime(DateTime.Now) '更新日
        sql += "', '"
        sql += sLotno_                        '入庫番号+行番号
        sql += "', '"
        sql += purclass_
        sql += "')"

        _db.executeDB(sql)

        m_insert_t70_inout = True

    End Function

    '取消区分の表示テキストを返す
    Public Function getDelKbnTxt(ByVal delKbn As String) As String
        '区分の値を取得し、使用言語に応じて値を返却

        Dim reDelKbn As String = IIf(delKbn = CommonConst.CANCEL_KBN_DISABLED,
                                    IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_JPN, CommonConst.CANCEL_KBN_JPN_TXT, CommonConst.CANCEL_KBN_ENG_TXT),
                                    "")
        Return reDelKbn
    End Function

    '入庫登録
    '-----------------------------
    '入庫基本
    Public Function m_insert_t42_nyukohd(ByVal currentWH As String,
                                         ByVal sPO As String, ByVal sPOB As String,
                                         ByVal sSupplierCd As String, ByVal sSupplierName As String, ByVal sSupPos As String,
                                         ByVal sSupAddr As String, ByVal sSupTel As String, ByVal sSupFax As String,
                                         ByVal sPosition As String, ByVal sSupContact As String, ByVal sPaymentterm As String,
                                         ByVal dPurAmt As Decimal, ByVal dGpAmt As Decimal, ByVal sSalesNm As String,
                                         ByVal sRemark_ As String, ByVal dVat As Decimal, ByVal dPph As Decimal,
                                         ByVal dtReceiveDate_ As String, ByVal sCustomerPO As String, ByVal sSalesCd As String,
                                         ByVal sWarehouse_ As String) As Boolean
        Dim Sql As String = vbNullString
        Sql = "INSERT INTO "
        Sql += "Public."
        Sql += "t42_nyukohd("
        Sql += "会社コード, 入庫番号, 発注番号, 発注番号枝番, 仕入先コード, 仕入先名 , 仕入先郵便番号, 仕入先住所, 仕入先電話番号, 仕入先ＦＡＸ, " '10
        Sql += "仕入先担当者役職, 仕入先担当者名, 支払条件, 仕入金額, 粗利額, 営業担当者, 入力担当者, 備考,  取消区分, ＶＡＴ, " '20
        Sql += "ＰＰＨ, 入庫日, 登録日, 更新日, 更新者, 客先番号,  営業担当者コード, 入力担当者コード, 倉庫コード"
        Sql += ""
        Sql += ") VALUES ("
        Sql += "'" & frmC01F10_Login.loginValue.BumonCD '会社コード1
        Sql += "', '"
        Sql += currentWH '入庫番号
        Sql += "', '"
        Sql += sPO '発注番号
        Sql += "', '"
        Sql += sPOB '発注番号枝番
        Sql += "', '"
        Sql += sSupplierCd '仕入先コード
        Sql += "', '"
        Sql += sSupplierName '仕入先名
        Sql += "', '"
        Sql += sSupPos '仕入先郵便番号
        Sql += "', '"
        Sql += sSupAddr '仕入先住所
        Sql += "', '"
        Sql += sSupTel '仕入先電話番号
        Sql += "', '"
        Sql += sSupFax '仕入先ＦＡＸ10
        Sql += "', '"
        Sql += sPosition '仕入先担当者役職
        Sql += "', '"
        Sql += sSupContact '仕入先担当者名
        Sql += "', '"
        Sql += sPaymentterm '支払条件
        Sql += "', "
        Sql += UtilClass.formatNumber(dPurAmt) '仕入金額
        Sql += ", "
        Sql += UtilClass.formatNumber(dGpAmt)  '粗利額
        Sql += ", '"
        Sql += sSalesNm '営業担当者
        Sql += "', '"
        Sql += frmC01F10_Login.loginValue.TantoNM '入力担当者
        Sql += "', '"
        Sql += UtilClass.escapeSql(sRemark_) '備考
        Sql += "', '"
        Sql += CommonConst.CANCEL_KBN_ENABLED.ToString '取消区分
        Sql += "', "
        Sql += UtilClass.formatNumber(dVat) 'ＶＡＴ20
        Sql += ", "
        Sql += UtilClass.formatNumber(dPph)  'ＰＰＨ
        Sql += ", '"
        Sql += dtReceiveDate_ '入庫日
        Sql += "', '"
        Sql += UtilClass.formatDatetime(DateTime.Now) '登録日
        Sql += "', '"
        Sql += UtilClass.formatDatetime(DateTime.Now) '更新日
        Sql += "', '"
        Sql += frmC01F10_Login.loginValue.TantoNM '更新者
        Sql += "', '"
        Sql += sCustomerPO '客先番号
        Sql += "', '"
        Sql += sSalesCd '営業担当者コード
        Sql += "', '"
        Sql += frmC01F10_Login.loginValue.TantoCD '入力担当者コード
        Sql += "', '"
        Sql += sWarehouse_ '倉庫コード29
        Sql += "')"

        _db.executeDB(Sql)

        m_insert_t42_nyukohd = True

    End Function

    '入庫明細
    Public Function m_insert_t43_nyukodt(ByVal currentWH As String, ByVal sLine_no_ As String, ByVal purclass_ As String,
                                         ByVal sMaker As String,
                                       ByVal sItem As String,
                                       ByVal sSpec As String, ByVal sSupplierName As String,
                                       ByVal dPurUnitPrice As Decimal, ByVal sUnit_ As String, ByVal currentVal As Long,
                                        ByVal sPO As String, ByVal sPOB As String,
                                        ByVal sRemark_ As String) As Boolean
        Dim Sql As String = vbNullString
        Sql = "INSERT INTO "
        Sql += "Public."
        Sql += "t43_nyukodt("
        Sql += "会社コード, 入庫番号, 行番号, 仕入区分, メーカー , 品名, 型式, 仕入先名, 仕入値, 単位, "
        Sql += "入庫数量, 備考, 更新者, 更新日, 発注番号, 発注番号枝番 "
        Sql += " )VALUES( "
        Sql += "'" & frmC01F10_Login.loginValue.BumonCD '会社コード1
        Sql += "', '"
        Sql += currentWH '入庫番号
        Sql += "', '"
        Sql += sLine_no_ '行番号
        Sql += "', '"
        Sql += purclass_ '仕入区分
        Sql += "', '"
        Sql += UtilClass.escapeSql(sMaker) 'メーカー
        Sql += "', '"
        Sql += UtilClass.escapeSql(sItem) '品名
        Sql += "', '"
        Sql += UtilClass.escapeSql(sSpec) '型式
        Sql += "', '"
        Sql += sSupplierName '仕入先名
        Sql += "', "
        Sql += UtilClass.formatNumber(dPurUnitPrice) '仕入値
        Sql += ", '"
        Sql += sUnit_ '単位10
        Sql += "',"
        Sql += currentVal.ToString '入庫数量
        Sql += ", '"
        Sql += UtilClass.escapeSql(sRemark_)  '備考
        Sql += "', '"
        Sql += frmC01F10_Login.loginValue.TantoNM '更新者
        Sql += "', '"
        Sql += UtilClass.formatDatetime(DateTime.Now) '更新日
        Sql += "', '"
        Sql += sPO '発注番号
        Sql += "', '"
        Sql += sPOB '発注番号枝番16
        Sql += "')"

        _db.executeDB(Sql)

        m_insert_t43_nyukodt = True

    End Function

    '出庫登録
    '-----------------------------
    '出庫基本
    Public Function m_insert_t44_shukohd(ByVal currentLS As String, ByVal sQT As String, ByVal sQTB As String,
                                         ByVal sCO As String, ByVal sCOB As String, ByVal dtIssueDate_ As String) As Boolean
        Dim Sql As String = vbNullString
        Sql = "INSERT INTO "
        Sql += "Public."
        Sql += "t44_shukohd("
        Sql += "会社コード, 出庫番号, 見積番号, 見積番号枝番, 受注番号, 受注番号枝番, 入力担当者, 出庫日, 登録日, 取消区分, "
        Sql += "更新日, 更新者, 入力担当者コード "
        Sql += " )VALUES( '"
        Sql += frmC01F10_Login.loginValue.BumonCD '会社コード1
        Sql += "', '"
        Sql += currentLS '出庫番号
        Sql += "', '"
        Sql += sQT '見積番号
        Sql += "', '"
        Sql += sQTB '見積番号枝番
        Sql += "', '"
        Sql += sCO '受注番号
        Sql += "', '"
        Sql += sCOB '受注番号枝番
        Sql += "', '"
        Sql += frmC01F10_Login.loginValue.TantoNM '入力担当者
        Sql += "', '"
        Sql += dtIssueDate_ '出庫日
        Sql += "', '"
        Sql += UtilClass.formatDatetime(DateTime.Now) '登録日
        Sql += "', '"
        Sql += CommonConst.CANCEL_KBN_ENABLED.ToString '取消区分10
        Sql += "', '"
        Sql += UtilClass.formatDatetime(DateTime.Now) '更新日
        Sql += "', '"
        Sql += frmC01F10_Login.loginValue.TantoNM '更新者
        Sql += "', '"
        Sql += frmC01F10_Login.loginValue.TantoCD '入力担当者コード13
        Sql += "')"

        _db.executeDB(Sql)
        m_insert_t44_shukohd = True

    End Function

    Public Function m_insert_t45_shukodt(ByVal currentLS As String, ByVal sLine_no_ As String, ByVal purclass_ As String,
                                         ByVal sCO As String, ByVal sCOB As String, ByVal sMaker As String,
                                       ByVal sItem As String,
                                       ByVal sSpec As String, ByVal sSupplierName As String,
                                         ByVal dSellUnitPrice As Decimal, ByVal sUnit_ As String, ByVal currentVal As Long,
                                     ByVal sWarehouse_ As String, ByVal syukokbn_ As String) As Boolean
        Dim Sql As String = vbNullString
        '出庫明細
        Sql = "INSERT INTO "
        Sql += "Public."
        Sql += "t45_shukodt("
        Sql += "会社コード, 出庫番号, 受注番号, 受注番号枝番, 行番号, "
        Sql += "仕入区分 , メーカー, 品名, 型式, 仕入先名, "
        Sql += "売単価, 単位, 出庫数量, 更新者, 更新日, 倉庫コード, 出庫区分)"
        Sql += " VALUES('"
        Sql += frmC01F10_Login.loginValue.BumonCD '会社コード1
        Sql += "', '"
        Sql += currentLS '出庫番号
        Sql += "', '"
        Sql += sCO '受注番号
        Sql += "', '"
        Sql += sCOB '受注番号枝番
        Sql += "', '"
        Sql += sLine_no_ '行番号5
        Sql += "', '"
        Sql += purclass_  '仕入区分（0:移動）6
        Sql += "', '"
        Sql += UtilClass.escapeSql(sMaker) 'メーカー
        Sql += "', '"
        Sql += UtilClass.escapeSql(sItem) '品名
        Sql += "', '"
        Sql += UtilClass.escapeSql(sSpec) '型式
        Sql += "', '"
        Sql += sSupplierName '仕入先名10
        Sql += "', "
        Sql += UtilClass.formatNumber(dSellUnitPrice) '売単価11
        Sql += ", '"
        Sql += sUnit_ '単位
        Sql += "', "
        Sql += currentVal.ToString '出庫数量
        Sql += ", '"
        Sql += frmC01F10_Login.loginValue.TantoNM '更新者
        Sql += "', '"
        Sql += UtilClass.formatDatetime(DateTime.Now) '更新日15
        Sql += "', '"
        Sql += sWarehouse_ '倉庫コード
        Sql += "', '"
        Sql += syukokbn_ 'CommonConst.SHUKO_KBN_NORMAL '出庫区分 （1:通常出庫）
        Sql += "')"

        _db.executeDB(Sql)
        m_insert_t45_shukodt = True

    End Function

    Public Function getWarehouseName(ByVal sWarehouseCode_ As String) As String
        Dim dsWarehouse As DataSet = getDsWarehouse(sWarehouseCode_)
        If dsWarehouse.Tables.Count > 0 Then
            Return dsWarehouse.Tables(RS).Rows(0)("名称").ToString
        Else
            Return vbNullString
        End If
    End Function

    Public Function getDsWarehouse(ByVal sWarehouseCode_ As String) As DataSet
        Dim Sql As String = " AND 倉庫コード ILIKE '" & sWarehouseCode_ & "'"
        Sql += " AND 会社コード='" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND 無効フラグ=0"
        Return getDsData("m20_warehouse", Sql)
    End Function

    Public Sub createCombobox(ByRef cmKbn As ComboBox, ByVal sWhat As String, Optional ByVal prmVal As String = "")
        cmKbn.DisplayMember = "Text"
        cmKbn.ValueMember = "Value"

        Dim dsHanyo As DataSet = getDsHanyoData(sWhat)

        Dim dt As New DataTable("Table")
        dt.Columns.Add("Text", GetType(String))
        dt.Columns.Add("Value", GetType(Integer))

        For i As Integer = 0 To dsHanyo.Tables(RS).Rows.Count - 1
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                dt.Rows.Add(dsHanyo.Tables(RS).Rows(i)("文字２"), dsHanyo.Tables(RS).Rows(i)("可変キー"))
            Else
                dt.Rows.Add(dsHanyo.Tables(RS).Rows(i)("文字１"), dsHanyo.Tables(RS).Rows(i)("可変キー"))
            End If
        Next
        cmKbn.DataSource = dt

        If prmVal IsNot "" Then
            cmKbn.SelectedValue = prmVal
        End If
    End Sub

    Public Function m70_new(m_ As String, i_ As String, s_ As String, q_ As Decimal, d_ As String, wh_ As String) As Boolean
        Dim Sql As String = vbNullString
        'm70.new()
        Sql = "INSERT INTO "
        Sql += "Public."
        Sql += "m70_zikmst("
        Sql += "会社コード, メーカー, 品名, 型式, 	数量, 登録日, 更新者, 更新日, 倉庫コード)"
        Sql += " VALUES('"
        Sql += frmC01F10_Login.loginValue.BumonCD '会社コード
        Sql += "', '"
        Sql += UtilClass.escapeSql(m_)
        Sql += "', '"
        Sql += UtilClass.escapeSql(i_)
        Sql += "', '"
        Sql += UtilClass.escapeSql(s_)
        Sql += "', '"
        Sql += q_.ToString
        Sql += "', '"
        Sql += d_
        Sql += "', '"
        Sql += frmC01F10_Login.loginValue.TantoNM
        Sql += "', '"
        Sql += UtilClass.formatDatetime(DateTime.Now) '更新日
        Sql += "', '"
        Sql += wh_
        Sql += "')"

        _db.executeDB(Sql)
        m70_new = True

    End Function

    Public Function m70_get_qty_(m_ As String, i_ As String, s_ As String, ym_ As String, wh_ As String) As Decimal
        Dim Sql As String = vbNullString
        Sql = " AND メーカー = '" & UtilClass.escapeSql(m_) & "'"
        Sql += " AND 会社コード='" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND 品名 = '" & UtilClass.escapeSql(i_) & "'"
        Sql += " AND 型式 = '" & UtilClass.escapeSql(s_) & "'"
        Sql += " AND to_char(登録日,'yyyymm') = '" & ym_ & "'"
        Sql += " AND 倉庫コード = '" & wh_ & "' order by 登録日 desc "
        Dim ds As DataSet = getDsData("m70_zikmst", Sql)
        If ds.Tables(RS).Rows.Count > 0 Then
            Return CType(_db.rmNullDouble(ds.Tables(RS).Rows(0)("数量")), Decimal)
        Else
            Return 0
        End If
    End Function

    Public Function getCurrencyEx(ByVal curkey_ As Object) As String
        Dim cur As String
        If IsDBNull(curkey_) Then
            cur = vbNullString
        Else
            cur = getCurrency(CInt(curkey_))
        End If
        Return cur
    End Function

    Public Function m21_get_qty_(m_ As String, i_ As String, s_ As String, wh_ As String) As Decimal
        Dim Sql As String = vbNullString
        Dim reccnt As Integer = 0
        Sql = "SELECT sum(適正在庫数) as 数量 from m21_zaiko where 会社コード='" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND メーカー = '" & UtilClass.escapeSql(m_) & "'"
        Sql += " AND 品名 = '" & UtilClass.escapeSql(i_) & "'"
        Sql += " AND 型式 = '" & UtilClass.escapeSql(s_) & "'"
        Sql += " AND 倉庫コード = '" & wh_ & "'"
        Sql += " And 入出庫種別 = '0'"
        Sql += " AND 無効フラグ = " & CommonConst.CANCEL_KBN_ENABLED
        Sql += ""
        Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

        If ds.Tables(RS).Rows.Count > 0 Then
            Return CType(_db.rmNullDouble(ds.Tables(RS).Rows(0)("数量")), Decimal)
        Else
            Return 0
        End If
    End Function

    Public Function Cancel_t44_tmp_byao(ByVal o_ As String, ByVal v_ As String) As Boolean
        Dim Sql As String = vbNullString
        Dim reccnt As Integer = 0
        Sql = "select 出庫番号,行番号 from t45_shukodt "
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND 受注番号 = '" & o_ & "'"
        Sql += " AND 受注番号枝番 = '" & v_ & "'"
        'Sql += " AND 出庫区分 ='" & CommonConst.SHUKO_KBN_NORMAL & "'"
        Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)
        For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1

            Sql = "UPDATE "
            Sql += "Public."
            Sql += "t70_inout "
            Sql += "SET "

            Sql += "取消日"
            Sql += " = '"
            Sql += UtilClass.formatDatetime(Now)
            Sql += "', "
            Sql += "取消区分 = '" & CommonConst.CANCEL_KBN_DISABLED.ToString & "'"
            Sql += ", 更新日 = '" & UtilClass.formatDatetime(Now) & "'"
            Sql += " ,更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"

            Sql += "WHERE"
            Sql += " 会社コード"
            Sql += "='"
            Sql += frmC01F10_Login.loginValue.BumonCD
            Sql += "'"
            Sql += " AND 伝票番号 ='" & ds.Tables(RS).Rows(i)("出庫番号") & "' "
            Sql += " AND 行番号 ='" & ds.Tables(RS).Rows(i)("行番号") & "' "
            _db.executeDB(Sql)

        Next

        Sql = "UPDATE "
        Sql += " t44_shukohd "
        Sql += " SET "
        Sql += " 取消区分 = '" & CommonConst.CANCEL_KBN_DISABLED.ToString & "'"
        Sql += " ,取消日 = '" & UtilClass.formatDatetime(Now) & "'"
        Sql += " ,更新日 = '" & UtilClass.formatDatetime(Now) & "'"
        Sql += " ,更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
        Sql += " WHERE "
        Sql += " 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND 受注番号 = '" & o_ & "'"
        Sql += " AND 受注番号枝番 = '" & v_ & "'"
        'Sql += " AND 出庫番号 = '" & ds.Tables(RS).Rows(i)("出庫番号") & "'"
        _db.executeDB(Sql)

        Return True
    End Function

    Public Function t27_get_invoice_no_by_pm(ByVal pm_ As String) As String
        Dim Sql As String = vbNullString
        Dim reccnt As Integer = 0
        Sql = "select 請求番号 from t27_nkinkshihd "
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND 入金番号 = '" & pm_ & "'"
        Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)
        Dim retval As String = ""
        For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
            retval += ds.Tables(RS).Rows(i)("請求番号") & ","
        Next

        Return retval

    End Function

End Class
