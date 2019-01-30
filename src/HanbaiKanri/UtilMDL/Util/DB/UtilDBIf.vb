Namespace DB

    '===============================================================================
    '
    '  ユーティリティクラス
    '    （クラス名）    UtilDBIf
    '    （処理機能名）     Util.DBによるDBアクセス機能のI/Fを提供する
    '    （本MDL使用前提）  UtilDBIfインターフェースを実装を実装したDBアクセスクラスを
    '                       プロジェクトに取り込んでいること
    '    （備考）           UtilDBIfインターフェースを定義
    '
    '===============================================================================
    '  履歴  名前          日  付      マーク      内容
    '-------------------------------------------------------------------------------
    '  (1)   Laevigata, Inc.    2006/05/10              新規
    '  (2)   Laevigata, Inc.    2006/06/16              置換パラメータクエリ対応
    '  (3)   Laevigata, Inc.    2010/08/26              SystemInfoテーブルからの取得に対応
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' Util.DBによるDBアクセス機能のI/Fを提供する
    ''' </summary>
    ''' <remarks>本I/Fを実装しているUtil.DBのDBアクセスクラスのインターフェースを提供する。</remarks>
    Public Interface UtilDBIf

        'SystemInfo構造体
        Structure sysinfoRec                    '2010.08.26 add by Laevigata, Inc. #SystemInfo
            Public fixKey As String
            Public variableKey As String
            Public stringValue1 As String
            Public stringValue2 As String
            Public stringValue3 As String
            Public numericValue1 As String      '本来数値型だがNULLの取り扱いを考慮して意図的に文字型とする
            Public numericValue2 As String      '本来数値型だがNULLの取り扱いを考慮して意図的に文字型とする
            Public numericValue3 As String      '本来数値型だがNULLの取り扱いを考慮して意図的に文字型とする
            Public dateValue1 As String         '本来日付型だがNULLの取り扱いを考慮して意図的に文字型とする
            Public dateValue2 As String         '本来日付型だがNULLの取り扱いを考慮して意図的に文字型とする
            Public dateValue3 As String         '本来日付型だがNULLの取り扱いを考慮して意図的に文字型とする
            Public UpdDate As String            '本来日付型だがNULLの取り扱いを考慮して意図的に文字型とする
            Public Memo As String
        End Structure

        '===============================================================================
        'プロパティ(アクセサ)
        '===============================================================================
        ''' <summary>
        ''' トランザクション中かどうかを示す
        ''' </summary>
        ''' <value>なし</value>
        ''' <returns>True/False</returns>
        ''' <remarks>トランザクションが開始されている場合にTrueを戻す。</remarks>
        ReadOnly Property isTransactionOpen() As Boolean

        '-------------------------------------------------------------------------------
        '   DB切断
        '   （処理概要）DB接続をクローズする
        '   ●入力パラメタ  ：なし
        '   ●メソッド戻り値：なし
        '                                               2006.05.23 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' DB切断
        ''' </summary>
        ''' <remarks>DBコネクションを閉じ、コネクションオブジェクトを破棄する。</remarks>
        Sub close()

        '-------------------------------------------------------------------------------
        '   トランザクション開始
        '   （処理概要)　トランザクションを開始する
        '   ●入力パラメタ  ：なし
        '   ●メソッド戻り値：なし
        '                                               2006.05.23 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' トランザクション開始
        ''' </summary>
        ''' <remarks>トランザクションを開始する。</remarks>
        Sub beginTran()

        '-------------------------------------------------------------------------------
        '   トランザクション終了
        '   （処理概要)　トランザクションをCommitする
        '   ●入力パラメタ  ：なし
        '   ●メソッド戻り値：なし
        '                                               2006.05.23 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' トランザクション確定
        ''' </summary>
        ''' <remarks>Commitを発行し、トランザクションを閉じる。</remarks>
        Sub commitTran()

        '-------------------------------------------------------------------------------
        '   トランザクション破棄
        '   （処理概要)　トランザクションをRollbackする
        '   ●入力パラメタ  ：なし
        '   ●メソッド戻り値：なし
        '                                               2006.05.23 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' トランザクション破棄
        ''' </summary>
        ''' <remarks>RollBackを発行し、トランザクションを閉じる。</remarks>
        Sub rollbackTran()

        '-------------------------------------------------------------------------------
        '   Select文発行
        '   （処理概要）Select文を発行し、DataSetを返却する
        '   ●入力パラメタ  ：prmSQL        Select文
        '                  ：prmTblName     返却されるDataSetのTBL名称
        '                  ：<prmRefRecCnt> 取得件数
        '   ●メソッド戻り値：DataSet
        '   ●備考          ：返却するDataSetはprmTblNameのTBL名称で格納
        '                                               2006.05.23 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' SELECT文を発行する
        ''' </summary>
        ''' <param name="prmSQL">発行するSELECT文</param>
        ''' <param name="prmTblName">返却されるDataSetのTABLE名称</param>
        ''' <param name="prmRefRecCnt">省略可能：SELECT文の取得レコード件数</param>
        ''' <returns>取得したレコードセットをDataSetオブジェクトとして返却</returns>
        ''' <remarks>SELECT文を発行し、レコードセットを取得する。取得したレコードセットはDataSetオブジェクトとして返却する。</remarks>
        Function selectDB(ByVal prmSQL As String, ByVal prmTblName As String, Optional ByRef prmRefRecCnt As Integer = 0) As DataSet

        '-------------------------------------------------------------------------------
        '   Select文発行
        '   （処理概要）置換パラメータ付きSelect文を発行し、DataSetを返却する
        '   ●入力パラメタ  ：prmSQL            パラメータ付きSelect文(置換パラメタは「?」)
        '                   ：prmParameters     置換パラメータリスト
        '                   ：prmTblName        返却されるDataSetのTBL名称
        '                   ：<prmRefRecCnt>    取得件数
        '   ●メソッド戻り値：DataSet
        '   ●備考          ：返却するDataSetはprmTblNameのTBL名称で格納
        '   ●使用例
        '                     Dim rtnCnt As Integer = 0
        '                     Dim listPrm As List(Of UtilDBPrm) = New List(Of UtilDBPrm)
        '                         listPrm.Add(New UtilDBPrm(1, , UtilDBPrm.parameterType.tNumber))
        '                         listPrm.Add(New UtilDBPrm(4, , UtilDBPrm.parameterType.tNumber))
        '                     Dim ds As DataSet = _db.selectDB("select * from test where col2 in (?,?)", listPrm, "RS", rtnCnt)
        '                         If rtnCnt > 0 Then
        '                             With ds.Tables("RS")
        '                                 For i As Integer = 0 To rtnCnt - 1
        '                                     Debug.WriteLine(.Rows(i)("col1") & "|" & .Rows(i)("col2") & "|" & .Rows(i)("col3"))
        '                                 Next
        '                             End With
        '                         End If
        '                                               2006.06.16 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 置換パラメータ付きSELECT文を発行する (詳細は使用例参照)
        ''' </summary>
        ''' <param name="prmSQL">パラメータ付きSelect文(置換パラメタは「?」)</param>
        ''' <param name="prmParameters">置換パラメータリスト</param>
        ''' <param name="prmTblName">返却されるDataSetのTABLE名称</param>
        ''' <param name="prmRefRecCnt">省略可能：SELECT文の取得レコード件数</param>
        ''' <returns>取得したレコードセットをDataSetオブジェクトとして返却</returns>
        ''' <remarks>SELECT文を発行し、レコードセットを取得する。取得したレコードセットはDataSetオブジェクトとして返却する。</remarks>
        Function selectDB(ByVal prmSQL As String,
                          ByVal prmParameters As List(Of UtilDBPrm),
                          ByVal prmTblName As String,
                          Optional ByRef prmRefRecCnt As Integer = 0) As DataSet

        '-------------------------------------------------------------------------------
        '   更新SQL文発行
        '   （処理概要）Insert/Update/Delete文を発行する
        '   ●入力パラメタ  ：prmSQL        SQL文
        '   ●メソッド戻り値：なし
        '                                               2006.05.23 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 実行系SQLを発行する
        ''' </summary>
        ''' <param name="prmSQL">発行するSQL文</param>
        ''' <remarks>レコードセットを生成しないSQL(INSERT/UPDATE/DELETE…etc)を発行する。</remarks>
        Sub executeDB(ByVal prmSQL As String)

        '-------------------------------------------------------------------------------
        '   更新SQL文発行
        '   （処理概要）Insert/Update/Delete文を発行する
        '   ●入力パラメタ  ：prmSQL                SQL文
        '                   ：prmRefAffectedRows    影響を受けた行数
        '   ●メソッド戻り値：なし
        '                                               2006.06.23 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 実行系SQLを発行する（影響件数引数付き）
        ''' </summary>
        ''' <param name="prmSQL">発行するSQL文</param>
        ''' <param name="prmRefAffectedRows">影響を受けた行数</param>
        ''' <remarks>レコードセットを生成しないSQL(INSERT/UPDATE/DELETE…etc)を発行する。</remarks>
        Sub executeDB(ByVal prmSQL As String, ByRef prmRefAffectedRows As Integer)

        '-------------------------------------------------------------------------------
        '   更新SQL文発行
        '   （処理概要）置換パラメータ付き実行系SQLを発行する
        '   ●入力パラメタ  ：prmSQL        SQL文
        '                   ：prmParameters 置換パラメータリスト
        '   ●メソッド戻り値：なし
        '   ●使用例
        '                     'パラメタ設定
        '                     Dim listPrm As List(Of UtilDBPrm) = New List(Of UtilDBPrm)
        '                     listPrm.Add(New UtilDBPrm(Nothing, 255, UtilDBPrm.parameterType.tVarchar, UtilDBPrm.parameterDirection.dReturn)) '戻り値
        '                     listPrm.Add(New UtilDBPrm(10, , UtilDBPrm.parameterType.tNumber, UtilDBPrm.parameterDirection.dInput))           '①
        '                     listPrm.Add(New UtilDBPrm(Nothing, , UtilDBPrm.parameterType.tNumber, UtilDBPrm.parameterDirection.dOutput))     '②
        '                     listPrm.Add(New UtilDBPrm(30, , UtilDBPrm.parameterType.tNumber, UtilDBPrm.parameterDirection.dInputOutput))     '③
        '                     listPrm.Add(New UtilDBPrm(Nothing, , UtilDBPrm.parameterType.tDate, UtilDBPrm.parameterDirection.dOutput))       '④
        '                     listPrm.Add(New UtilDBPrm("置換パラメタクエリ実行テスト", _
        '                                                       14, UtilDBPrm.parameterType.tVarchar, UtilDBPrm.parameterDirection.dInput))    '⑤
        '                     '実行
        '                     _db.executeDB("BEGIN ? := TESTFUNC(?,?,?,?,?); END;", listPrm)
        '
        '                     '結果確認
        '                     Debug.WriteLine("戻り値=" & listPrm(0).value)
        '                     Debug.WriteLine("prm1  =" & listPrm(1).value)
        '                     Debug.WriteLine("prm2  =" & listPrm(2).value)
        '                     Debug.WriteLine("prm3  =" & listPrm(3).value)
        '                     Debug.WriteLine("prm4  =" & listPrm(4).value)
        '                     Debug.WriteLine("prm5  =" & listPrm(5).value)
        '
        '
        '                     ===実行ストアド==========================
        '                     CREATE OR REPLACE FUNCTION TESTFUNC(
        '                     	 INPRM 		IN		NUMBER
        '                     	,OUTPRM		OUT		NUMBER
        '                     	,INOUTPRM	IN	OUT	NUMBER
        '                     	,DTPRM		OUT		DATE
        '                     	,VCPRM		IN		VARCHAR
        '                     )
        '                     RETURN VARCHAR2 
        '                     IS
        '                     	WK	DATE;
        '                     BEGIN
        '                         INOUTPRM := INOUTPRM * 2;             --INOUTPRMを２倍
        '                         OUTPRM := INPRM + 1;                  --INPRMに１を加えてOUTPRMに設定
        '                         SELECT SYSDATE INTO DTPRM FROM DUAL;  --DTPRMにシステム日付を設定
        '                         RETURN VCPRM || 'を実行しました。';   --戻り値にVCPRM＋αを設定
        '                     END;
        '                     /
        '                     =========================================
        '                                               2006.06.16 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 置換パラメータ付き実行系SQLを発行する (詳細は使用例参照)
        ''' </summary>
        ''' <param name="prmSQL">パラメータ付きSQL文</param>
        ''' <param name="prmRefParameters">置換パラメータリスト</param>
        ''' <remarks>ストアド実行などを想定(それ以外も実行可能)</remarks>
        Sub executeDB(ByVal prmSQL As String, ByRef prmRefParameters As List(Of UtilDBPrm))

        '-------------------------------------------------------------------------------
        '   シングルクォート文字列化
        '   （処理概要）シングルクォートを「''」に置換して返却
        '   ●入力パラメタ  ：prmSQL     Select文
        '   ●メソッド戻り値：置換後SQL文字列
        '                                               2006.05.23 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' シングルクォーテーション文字列化
        ''' </summary>
        ''' <param name="prmSQL">置換対象SQL文字列</param>
        ''' <returns>置換後のSQL文字列</returns>
        ''' <remarks>シングルクォーテーションを文字列化し、SQLインジェクション対策およびデータ登録可能とする。</remarks>
        Function rmSQ(ByVal prmSQL As String) As String

        '-------------------------------------------------------------------------------
        '   Null⇒文字列
        '                                               2006.05.23 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' Nullの文字列置換
        ''' </summary>
        ''' <param name="prmField">フィールド値</param>
        ''' <returns>置換後文字列</returns>
        ''' <remarks>Nullのフィールド値を""(長さ0文字列)へ置換する。</remarks>
        Function rmNullStr(ByVal prmField As Object) As String

        '-------------------------------------------------------------------------------
        '   Null⇒Short
        '                                               2006.05.23 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' Nullの数値置換(Short型)
        ''' </summary>
        ''' <param name="prmField">フィールド値</param>
        ''' <returns>置換後数値</returns>
        ''' <remarks>Nullのフィールド値をShort型数値へ置換する。</remarks>
        Function rmNullShort(ByVal prmField As Object) As Short

        '-------------------------------------------------------------------------------
        '   Null⇒Integer
        '                                               2006.05.23 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' Nullの数値置換(Integer型)
        ''' </summary>
        ''' <param name="prmField">フィールド値</param>
        ''' <returns>置換後数値</returns>
        ''' <remarks>Nullのフィールド値をInteger型数値へ置換する。</remarks>
        Function rmNullInt(ByVal prmField As Object) As Integer

        '-------------------------------------------------------------------------------
        '   Null⇒Long
        '                                               2006.05.23 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' Nullの数値置換(Long型)
        ''' </summary>
        ''' <param name="prmField">フィールド値</param>
        ''' <returns>置換後数値</returns>
        ''' <remarks>Nullのフィールド値をLong型数値へ置換する。</remarks>
        Function rmNullLong(ByVal prmField As Object) As Long

        '-------------------------------------------------------------------------------
        '   Null⇒Double
        '                                               2006.05.23 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' Nullの数値置換(Double型)
        ''' </summary>
        ''' <param name="prmField">フィールド値</param>
        ''' <returns>置換後数値</returns>
        ''' <remarks>Nullのフィールド値をDouble型数値へ置換する。</remarks>
        Function rmNullDouble(ByVal prmField As Object) As Double

        '-------------------------------------------------------------------------------
        '   Null⇒日付文字列値
        '                                               2006.05.23 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' Nullの日付文字列置換
        ''' </summary>
        ''' <param name="prmField">フィールド値</param>
        ''' <param name="prmFormatStr">日付書式</param>
        ''' <returns>置換後日付文字列</returns>
        ''' <remarks>Nullのフィールド値を日付フォーマットの文字列へ置換する。</remarks>
        Function rmNullDate(ByVal prmField As Object,
                            Optional ByVal prmFormatStr As String = "yyyy/MM/dd HH:mm:ss"
                            ) As String
    End Interface




    '===============================================================================
    '
    '  ユーティリティクラス
    '    （クラス名）    UtilDBPrm
    '    （処理機能名）     Util.DBによるDBアクセスにおいて、置換パラメタクエリのデータ枠を提供
    '    （本MDL使用前提）  UtilDBIfインターフェースを実装を実装したDBアクセスクラスを
    '                       プロジェクトに取り込んでいること
    '    （備考）           
    '
    '===============================================================================
    '  履歴  名前          日  付      マーク      内容
    '-------------------------------------------------------------------------------
    '  (1)   Laevigata, Inc.    2006/06/16              新規
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' Util.DBによるDBアクセスにおいて、置換パラメタクエリのデータ枠を提供
    ''' </summary>
    ''' <remarks></remarks>
    Public Class UtilDBPrm

        '===============================================================================
        '列挙体定義
        '===============================================================================
        ''' <summary>
        ''' パラメータタイプ(型)
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum parameterType As Short
            ''' <summary>
            ''' 論理値
            ''' </summary>
            ''' <remarks></remarks>
            tBoolean = 0
            ''' <summary>
            ''' 可変長文字列値
            ''' </summary>
            ''' <remarks></remarks>
            tVarchar = 1
            ''' <summary>
            ''' 日付値
            ''' </summary>
            ''' <remarks></remarks>
            tDate = 2
            ''' <summary>
            ''' 数値
            ''' </summary>
            ''' <remarks></remarks>
            tNumber = 3
        End Enum

        ''' <summary>
        ''' パラメータディレクション(I/O)
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum parameterDirection As Short
            ''' <summary>
            ''' INパラメータ
            ''' </summary>
            ''' <remarks></remarks>
            dInput = 0
            ''' <summary>
            ''' OUTパラメータ
            ''' </summary>
            ''' <remarks></remarks>
            dOutput = 1
            ''' <summary>
            ''' IN/OUTパラメータ
            ''' </summary>
            ''' <remarks></remarks>
            dInputOutput = 2
            ''' <summary>
            ''' 戻り値
            ''' </summary>
            ''' <remarks></remarks>
            dReturn = 3
        End Enum

        '===============================================================================
        'メンバー変数定義
        '===============================================================================
        Private _value As Object                                            '値
        Private _type As parameterType                                      '型
        Private _direction As parameterDirection                            '方向
        Private _size As Short                                              'サイズ
        Private _refParameter As System.Data.Common.DbParameter = Nothing   '設定後のパラメタポインタ(実行後の結果[パラメタ値]取得に使用)

        '===============================================================================
        'プロパティ(アクセサ)
        '===============================================================================
        ''' <summary>
        ''' 値
        ''' </summary>
        ''' <returns>パラメータ値</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property value() As Object
            Get
                If _refParameter Is Nothing Then
                    'パラメータ設定前
                    Select Case _type
                        Case parameterType.tBoolean
                            Return CBool(_value)
                        Case parameterType.tDate
                            Return CDate(_value)
                        Case parameterType.tNumber
                            Return CDec(_value)
                        Case parameterType.tVarchar
                            Return CStr(_value)
                        Case Else
                            Return Nothing
                    End Select
                Else
                    'パラメータ設定後
                    Select Case _type
                        Case parameterType.tBoolean
                            Return CBool(_refParameter.Value)
                        Case parameterType.tDate
                            Return CDate(_refParameter.Value)
                        Case parameterType.tNumber
                            Return CDec(_refParameter.Value)
                        Case parameterType.tVarchar
                            Return CStr(_refParameter.Value)
                        Case Else
                            Return Nothing
                    End Select
                End If
            End Get
        End Property
        ''' <summary>
        ''' タイプ(型)
        ''' </summary>
        ''' <returns>パラメータタイプ</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property type() As parameterType
            Get
                Return _type
            End Get
        End Property
        ''' <summary>
        ''' ディレクション(I/O)[UtilMDL.DB.UtilDBPrm.parameterDirectionとして返却]
        ''' </summary>
        ''' <returns>パラメータディレクション(方向)</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property direction() As parameterDirection
            Get
                Return _direction
            End Get
        End Property
        ''' <summary>
        ''' ディレクション(I/O)[System.Data.ParameterDirectionとして返却]
        ''' </summary>
        ''' <returns>パラメータディレクション(方向)</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property systemDirection() As System.Data.ParameterDirection
            Get
                Dim ret As System.Data.ParameterDirection
                Select Case _direction
                    Case parameterDirection.dInput
                        ret = Data.ParameterDirection.Input
                    Case parameterDirection.dInputOutput
                        ret = Data.ParameterDirection.InputOutput
                    Case parameterDirection.dOutput
                        ret = Data.ParameterDirection.Output
                    Case parameterDirection.dReturn
                        ret = Data.ParameterDirection.ReturnValue
                    Case Else
                        ret = Nothing
                End Select
                Return ret
            End Get
        End Property
        ''' <summary>
        ''' サイズ
        ''' </summary>
        ''' <returns>パラメータサイズ</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property size() As Short
            Get
                Return _size
            End Get
        End Property
        ''' <summary>
        ''' 設定後のパラメタポインタ(実行後の結果[パラメタ値]取得に使用)
        ''' </summary>
        ''' <value>ポインタ</value>
        ''' <remarks></remarks>
        Public WriteOnly Property refParameter() As System.Data.Common.DbParameter
            Set(ByVal value As System.Data.Common.DbParameter)
                _refParameter = value
            End Set
        End Property

        '===============================================================================
        ' コンストラクタ
        '   ●入力パラメタ   ： prmValue        パラメータ値
        '                       prmSize         サイズ
        '                       prmType         パラメータタイプ(型)
        '                       prmDirection    パラメータディレクション(I/O)
        '                                               2006.06.19 Created By Laevigata, Inc.
        '===============================================================================
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="prmValue">パラメータ値</param>
        ''' <param name="prmSize">サイズ</param>
        ''' <param name="prmType">パラメータタイプ(型)</param>
        ''' <param name="prmDirection">パラメータディレクション(I/O)</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal prmValue As Object, _
                               Optional ByVal prmSize As Short = 0, _
                               Optional ByVal prmType As parameterType = parameterType.tVarchar, _
                               Optional ByVal prmDirection As parameterDirection = parameterDirection.dInput)

            'パラメタチェック
            If (prmType <> parameterType.tBoolean And _
                prmType <> parameterType.tVarchar And _
                prmType <> parameterType.tDate And _
                prmType <> parameterType.tNumber) Then
                Throw New UsrDefException("置換パラメータの設定が誤っています。" & ControlChars.NewLine & _
                                          "prmTypeにはparameterType定数を使用して下さい。")
            End If
            If (prmDirection <> parameterDirection.dInput And _
                prmDirection <> parameterDirection.dOutput And _
                prmDirection <> parameterDirection.dInputOutput And _
                prmDirection <> parameterDirection.dReturn) Then
                Throw New UsrDefException("置換パラメータの設定が誤っています。" & ControlChars.NewLine & _
                                          "prmDirectionにはparameterDirection定数を使用して下さい。")
            End If
            If prmType = parameterType.tVarchar And _
               prmSize = 0 Then
                Throw New UsrDefException("置換パラメータの設定が誤っています。" & ControlChars.NewLine & _
                                          "prmTypeにparameterType.tVarchar定数を使用している場合、" & ControlChars.NewLine & _
                                          "正しくprmSizeを指定して下さい。")
            End If
            _value = prmValue
            _type = prmType
            _direction = prmDirection
            _size = prmSize

        End Sub

    End Class

End Namespace
