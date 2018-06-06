'===============================================================================
'
'　カネキ吉田商店様
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
    Public Sub Insert_L01_ProcLog(ByVal companyCd As String, ByVal gyomuId As Object, ByVal shoriId As String, ByVal sousa As String, ByVal kekka As String,
                                  ByVal str1 As Object, ByVal str2 As Object, ByVal str3 As Object, ByVal str4 As Object, ByVal str5 As Object,
                                  ByVal num1 As Object, ByVal num2 As Object, ByVal num3 As Object, ByVal num4 As Object, ByVal num5 As Object, ByVal userId As String)

        Dim sql As String = ""
        sql = sql & N & "INSERT INTO l10_proclog ( "
        sql = sql & N & "   会社コード "
        sql = sql & N & " , 業務ＩＤ "
        sql = sql & N & " , 処理ＩＤ "
        sql = sql & N & " , 操作契機 "
        sql = sql & N & " , 処理結果 "
        sql = sql & N & " , 文字１ "
        sql = sql & N & " , 文字２ "
        sql = sql & N & " , 文字３ "
        sql = sql & N & " , 文字４ "
        sql = sql & N & " , 文字５ "
        sql = sql & N & " , 数値１ "
        sql = sql & N & " , 数値２ "
        sql = sql & N & " , 数値３ "
        sql = sql & N & " , 数値４ "
        sql = sql & N & " , 数値５ "
        sql = sql & N & " , 更新者 "
        sql = sql & N & " , 更新日 "
        sql = sql & N & ") VALUES ( "
        sql = sql & N & "   " & nullToStr(companyCd)          '会社コード
        sql = sql & N & " , " & nullToStr(gyomuId)            '業務ＩＤ
        sql = sql & N & " , " & nullToStr(shoriId)            '処理ＩＤ
        sql = sql & N & " , " & nullToStr(sousa)              '操作契機
        sql = sql & N & " , " & nullToStr(kekka)              '操作結果
        sql = sql & N & " , " & nullToStr(str1)
        sql = sql & N & " , " & nullToStr(str2)
        sql = sql & N & " , " & nullToStr(str3)
        sql = sql & N & " , " & nullToStr(str4)
        sql = sql & N & " , " & nullToStr(str5)
        sql = sql & N & " , " & nullToNum(num1)
        sql = sql & N & " , " & nullToNum(num2)
        sql = sql & N & " , " & nullToNum(num3)
        sql = sql & N & " , " & nullToNum(num4)
        sql = sql & N & " , " & nullToNum(num5)
        sql = sql & N & " , '" & _db.rmSQ(userId) & "' "      '更新者
        sql = sql & N & " , now() "
        sql = sql & N & ") "
        _db.executeDB(sql)

    End Sub

    ''' <summary>
    ''' システム日付取得
    ''' </summary>
    ''' <returns></returns>
    Public Function getSysDdate() As String

        Dim sql As String = ""
        sql = sql & N & "SELECT to_char(current_timestamp, 'YYYY/MM/DD') AS SYSDATE"
        Dim reccnt As Integer = 0
        Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)

        If reccnt <= 0 Then
            Throw New UsrDefException("", _msgHd.getMSG("NonSysDate"))
        End If

        Return _db.rmNullStr(ds.Tables(RS).Rows(0)("SYSDATE"))

    End Function

    ''' <summary>
    ''' システム日付の曜日取得
    ''' </summary>
    ''' <returns></returns>
    Public Function getSysWeek() As String

        Dim sql As String = ""
        sql = sql & N & "SELECT to_char(current_timestamp, 'TMDy') AS SYSWEEK"
        Dim reccnt As Integer = 0
        Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)

        If reccnt <= 0 Then
            Throw New UsrDefException("曜日", _msgHd.getMSG("NonSysDate"))
        End If

        Return _db.rmNullStr(ds.Tables(RS).Rows(0)("SYSWEEK"))

    End Function

    Private Function nullToStr(ByVal obj As Object) As String
        If (obj Is DBNull.Value) Then
            Return "NULL"
        Else
            Return "'" & obj & "'"
        End If
    End Function

    Private Function nullToNum(ByVal obj As Object) As String
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
    Public Function GetDenpyoNo(ByVal prmCompanyCd As String, ByVal prmSaibanKbn As String) As String

        '①採番マスタ(M80)から、入力P)採番区分に対応したレコードを取得する
        Dim sql As String = ""
        sql = sql & N & "SELECT "
        sql = sql & N & "    最新値 "
        sql = sql & N & "   ,最小値 "
        sql = sql & N & "   ,最大値 "
        sql = sql & N & "   ,接頭文字 "
        sql = sql & N & "   ,連番桁数 "
        sql = sql & N & "FROM M80_SAIBAN "
        sql = sql & N & "WHERE 会社コード = '" & _db.rmSQ(prmCompanyCd) & "' "
        sql = sql & N & "  AND 採番キー = '" & _db.rmSQ(prmSaibanKbn) & "' "
        Dim reccnt As Integer = 0
        Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)

        If reccnt <= 0 Then
            _msgHd.dspMSG("NoSaibanMst")
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
        sql = sql & N & "UPDATE M80_SAIBAN SET "
        sql = sql & N & "    最新値 = " & curnum
        sql = sql & N & "   ,更新者 = 'SYSTEM' "
        sql = sql & N & "   ,更新日 = current_timestamp "
        sql = sql & N & "WHERE 会社コード = '" & _db.rmSQ(prmCompanyCd) & "' "
        sql = sql & N & "  AND 採番キー = '" & _db.rmSQ(prmSaibanKbn) & "' "
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
    Public Sub GetSalesTanka(ByVal prmCompanyCd As String, ByVal prmGoodsCd As String, ByVal prmCustomerCd As String, ByVal prmChakuDt As String,
                             ByRef prmSalesTanka As Decimal, ByRef prmTankaPtn As String)

        '①出荷先別期間指定特売単価ありの場合（該当レコードが存在しない場合、②へ）
        Dim sql As String = ""
        sql = sql & N & "SELECT "
        sql = sql & N & "    販売単価 "
        sql = sql & N & "FROM M25_SLPRICE "
        sql = sql & N & "WHERE 会社コード = '" & _db.rmSQ(prmCompanyCd) & "' "
        sql = sql & N & "  AND 商品コード = '" & _db.rmSQ(prmGoodsCd) & "' "
        sql = sql & N & "  AND 特売区分 = '" & _db.rmSQ(CommonConst.TOKUBAI_KBN_BARGAIN) & "' "       '1:特売
        sql = sql & N & "  AND 取引先コード = '" & _db.rmSQ(prmCustomerCd) & "' "
        sql = sql & N & "  AND 適用開始日 <= '" & prmChakuDt & "' "
        sql = sql & N & "  AND 適用終了日 >= '" & prmChakuDt & "' "
        Dim reccnt As Integer = 0
        Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)

        If reccnt > 0 Then
            prmTankaPtn = CommonConst.TANKA_PTN_BARGAIN     '1:特売単価
            prmSalesTanka = _db.rmNullDouble(ds.Tables(RS).Rows(0)("販売単価"))
            Exit Sub
        End If

        '②出荷先別通常単価ありの場合（該当レコードが存在しない場合、③へ）
        sql = ""
        sql = sql & N & "SELECT "
        sql = sql & N & "    販売単価 "
        sql = sql & N & "FROM M25_SLPRICE "
        sql = sql & N & "WHERE 会社コード = '" & _db.rmSQ(prmCompanyCd) & "' "
        sql = sql & N & "  AND 商品コード = '" & _db.rmSQ(prmGoodsCd) & "' "
        sql = sql & N & "  AND 特売区分 = '" & _db.rmSQ(CommonConst.TOKUBAI_KBN_NORMAL) & "' "        '0:通常
        sql = sql & N & "  AND 取引先コード = '" & _db.rmSQ(prmCustomerCd) & "' "
        sql = sql & N & "  AND 適用開始日 <= '" & prmChakuDt & "' "
        sql = sql & N & "  AND 適用終了日 >= '" & prmChakuDt & "' "
        reccnt = 0
        ds = _db.selectDB(sql, RS, reccnt)

        If reccnt > 0 Then
            prmTankaPtn = CommonConst.TANKA_PTN_CUSTOMER        '2:出荷先別単価
            prmSalesTanka = _db.rmNullDouble(ds.Tables(RS).Rows(0)("販売単価"))
            Exit Sub
        End If

        '③出荷先別通常単価ありの場合（該当レコードが存在しない場合、④へ）
        sql = ""
        sql = sql & N & "SELECT "
        sql = sql & N & "    販売単価 "
        sql = sql & N & "FROM M25_SLPRICE "
        sql = sql & N & "WHERE 会社コード = '" & _db.rmSQ(prmCompanyCd) & "' "
        sql = sql & N & "  AND 商品コード = '" & _db.rmSQ(prmGoodsCd) & "' "
        sql = sql & N & "  AND 特売区分 = '" & _db.rmSQ(CommonConst.TOKUBAI_KBN_NORMAL) & "' "        '0:通常
        sql = sql & N & "  AND 取引先コード = '" & _db.rmSQ(CommonConst.CUSTOMER_CODE_ALL) & "' "     'ALL:指定なし
        sql = sql & N & "  AND 適用開始日 <= '" & prmChakuDt & "' "
        sql = sql & N & "  AND 適用終了日 >= '" & prmChakuDt & "' "
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
    '   仕入単価取得処理
    '   （処理概要）仕入単価マスタから、入力パラメータに対応した単価を取得する
    '   ●入力パラメタ   ：prmCompanyCd   会社コード
    '                    ：prmGoodsCd     商品コード
    '                    ：prmCustomerCd  取引先コード
    '                    ：prmShiireDt    仕入日
    '   ●出力パラメタ   ：prmShiireTanka 仕入単価
    '                    ：prmTankaPtn    単価パターン（2:出荷先別単価,3:商品別単価,9:単価なし）
    '   ●メソッド戻り値 ：なし
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' 仕入単価取得処理（GetHanbaiTanka）
    ''' </summary>
    ''' <param name="prmCompanyCd"></param>
    ''' <param name="prmGoodsCd"></param>
    ''' <param name="prmCustomerCd"></param>
    ''' <param name="prmShiireDt"></param>
    ''' <param name="prmShiireTanka"></param>
    ''' <param name="prmTankaPtn"></param>
    Public Sub GetShiireTanka(ByVal prmCompanyCd As String, ByVal prmGoodsCd As String, ByVal prmCustomerCd As String, ByVal prmShiireDt As String,
                             ByRef prmShiireTanka As Decimal, ByRef prmTankaPtn As String)

        '①出荷先別単価ありの場合（該当レコードが存在しない場合、②へ）
        Dim sql As String = ""
        sql = sql & N & "SELECT "
        sql = sql & N & "    仕入単価 "
        sql = sql & N & "FROM M26_PCPRICE "
        sql = sql & N & "WHERE 会社コード = '" & _db.rmSQ(prmCompanyCd) & "' "
        sql = sql & N & "  AND 商品コード = '" & _db.rmSQ(prmGoodsCd) & "' "
        sql = sql & N & "  AND 取引先コード = '" & _db.rmSQ(prmCustomerCd) & "' "
        sql = sql & N & "  AND 適用開始日 <= '" & prmShiireDt & "' "
        sql = sql & N & "  AND 適用終了日 >= '" & prmShiireDt & "' "
        Dim reccnt As Integer = 0
        Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)

        If reccnt > 0 Then
            prmTankaPtn = CommonConst.SHIIRE_TANKA_PTN_CUSTOMER                                       '2:出荷先別単価
            prmShiireTanka = _db.rmNullDouble(ds.Tables(RS).Rows(0)("仕入単価"))
            Exit Sub
        End If

        '②商品別単価ありの場合（該当レコードが存在しない場合、③へ）
        sql = ""
        sql = sql & N & "SELECT "
        sql = sql & N & "    仕入単価 "
        sql = sql & N & "FROM M26_PCPRICE "
        sql = sql & N & "WHERE 会社コード = '" & _db.rmSQ(prmCompanyCd) & "' "
        sql = sql & N & "  AND 商品コード = '" & _db.rmSQ(prmGoodsCd) & "' "
        sql = sql & N & "  AND 取引先コード = '" & _db.rmSQ(CommonConst.CUSTOMER_CODE_ALL) & "' "     'ALL:指定なし
        sql = sql & N & "  AND 適用開始日 <= '" & prmShiireDt & "' "
        sql = sql & N & "  AND 適用終了日 >= '" & prmShiireDt & "' "
        reccnt = 0
        ds = _db.selectDB(sql, RS, reccnt)

        If reccnt > 0 Then
            prmTankaPtn = CommonConst.SHIIRE_TANKA_PTN_GOODS                                          '3:商品別単価
            prmShiireTanka = _db.rmNullDouble(ds.Tables(RS).Rows(0)("仕入単価"))
            Exit Sub
        End If

        '③値を返却する（該当単価なしの場合）
        prmTankaPtn = CommonConst.SHIIRE_TANKA_PTN_NONE                                               '9:単価なし
        prmShiireTanka = 0

    End Sub

    '-------------------------------------------------------------------------------
    '  汎用マスタデータ取得
    '   （処理概要）汎用マスタのデータを読み込み、内部テーブルに格納する
    '-------------------------------------------------------------------------------
    Public Sub Get_HanyouMST()

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
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    ''' <summary>
    ''' 住所データ取得
    ''' </summary>
    ''' <param name="prmPostalCd">郵便番号</param>
    ''' <returns>住所データ</returns>
    Public Function getAddress(ByVal prmPostalCd As String) As DataSet

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
    Public Function getTorihikisaki(ByVal prmCompanyCd As String,
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

End Class
