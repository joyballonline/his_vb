Namespace DB
    '===============================================================================
    '
    '  ユーティリティクラス
    '    （クラス名）    UtilDBInheritBase
    '    （処理機能名）     UtilDBIfによるDBアクセス機能を提供する
    '    （本MDL使用前提）  本クラスは継承元とすることを前提とするため、
    '                       継承してサブクラスを定義すること。
    '    （備考）           ・UtilDBIfインターフェースを実装
    '                       ・本クラスのインスタンス化は行えない
    '                           ⇒サブクラスをインスタンス化すること
    '                       ・MustOverrideなメンバーをOverridesすること
    '
    '===============================================================================
    '  履歴  名前          日  付      マーク      内容
    '-------------------------------------------------------------------------------
    '  (1)   Jun.Takagi    2006/05/23              新規
    '  (2)   Jun.Takagi    2010/08/26              SystemInfoテーブルからの取得に対応
    '-------------------------------------------------------------------------------
    Public MustInherit Class UtilDBInheritBase
        Implements UtilDBIf
        '===============================================================================
        'メンバー変数定義
        '===============================================================================
        'なし


        '===============================================================================
        'プロパティ(アクセサ)
        '===============================================================================

        'トランザクションが開いているかどうかのステータスを戻す
        ''' <summary>
        ''' トランザクションが開いているかどうかのステータスを戻す
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public MustOverride ReadOnly Property isTransactionOpen() As Boolean Implements UtilDBIf.isTransactionOpen

        '===============================================================================
        ' コンストラクタ
        '===============================================================================
        '抽象クラスである為存在しない

        '-------------------------------------------------------------------------------
        '   DB切断
        '   （処理概要）DB接続をクローズする
        '   ●入力パラメタ  ：なし
        '   ●メソッド戻り値：なし
        '                                               2006.05.23 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' DB接続をクローズする
        ''' </summary>
        ''' <remarks></remarks>
        Public MustOverride Sub close() Implements UtilDBIf.close

        '-------------------------------------------------------------------------------
        '   トランザクション開始
        '   （処理概要)　トランザクションを開始する
        '   ●入力パラメタ  ：なし
        '   ●メソッド戻り値：なし
        '                                               2006.05.23 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' トランザクションを開始する
        ''' </summary>
        ''' <remarks></remarks>
        Public MustOverride Sub beginTran() Implements UtilDBIf.beginTran

        '-------------------------------------------------------------------------------
        '   トランザクション終了
        '   （処理概要)　トランザクションをCommitする
        '   ●入力パラメタ  ：なし
        '   ●メソッド戻り値：なし
        '                                               2006.05.23 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' トランザクションをCommitする
        ''' </summary>
        ''' <remarks></remarks>
        Public MustOverride Sub commitTran() Implements UtilDBIf.commitTran

        '-------------------------------------------------------------------------------
        '   トランザクション破棄
        '   （処理概要)　トランザクションをRollbackする
        '   ●入力パラメタ  ：なし
        '   ●メソッド戻り値：なし
        '                                               2006.05.23 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' トランザクションをRollbackする
        ''' </summary>
        ''' <remarks></remarks>
        Public MustOverride Sub rollbackTran() Implements UtilDBIf.rollbackTran

        '-------------------------------------------------------------------------------
        '   Select文発行
        '   （処理概要）Select文を発行し、DataSetを返却する
        '   ●入力パラメタ  ：prmSQL        Select文
        '                  ：prmTblName     返却されるDataSetのTBL名称
        '                  ：<prmRefRecCnt> 取得件数
        '   ●メソッド戻り値：DataSet
        '   ●備考          ：返却するDataSetはprmTblNameのTBL名称で格納
        '                                               2006.05.23 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' Select文を発行し、DataSetを返却する
        ''' </summary>
        ''' <param name="prmSQL">Select文</param>
        ''' <param name="prmTblName">返却されるDataSetのTBL名称</param>
        ''' <param name="prmRefRecCnt">取得件数</param>
        ''' <returns>DataSet</returns>
        ''' <remarks></remarks>
        Public MustOverride Function selectDB(ByVal prmSQL As String, _
                                              ByVal prmTblName As String, _
                                              Optional ByRef prmRefRecCnt As Integer = 0) _
                                                                           As System.Data.DataSet _
                                                                           Implements UtilDBIf.selectDB

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
        '                                               2006.06.16 Created By Jun.Takagi
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
        Public MustOverride Function selectDB(ByVal prmSQL As String, _
                                              ByVal prmParameters As List(Of UtilDBPrm), _
                                              ByVal prmTblName As String, _
                                              Optional ByRef prmRefRecCnt As Integer = 0) _
                                                                             As System.Data.DataSet _
                                                                             Implements UtilDBIf.selectDB

        '-------------------------------------------------------------------------------
        '   更新SQL文発行
        '   （処理概要）Insert/Update/Delete文を発行する
        '   ●入力パラメタ  ：prmSQL        SQL文
        '   ●メソッド戻り値：なし
        '                                               2006.05.23 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' Insert/Update/Delete文を発行する
        ''' </summary>
        ''' <param name="prmSQL">SQL文</param>
        ''' <remarks></remarks>
        Public MustOverride Sub executeDB(ByVal prmSQL As String) Implements UtilDBIf.executeDB

        '-------------------------------------------------------------------------------
        '   更新SQL文発行
        '   （処理概要）Insert/Update/Delete文を発行する
        '   ●入力パラメタ  ：prmSQL                SQL文
        '                   ：prmRefAffectedRows    影響を受けた行数
        '   ●メソッド戻り値：なし
        '                                               2006.06.23 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 実行系SQLを発行する（影響件数引数付き）
        ''' </summary>
        ''' <param name="prmSQL">発行するSQL文</param>
        ''' <param name="prmRefAffectedRows">影響を受けた行数</param>
        ''' <remarks>レコードセットを生成しないSQL(INSERT/UPDATE/DELETE…etc)を発行する。</remarks>
        Public MustOverride Sub executeDB(ByVal prmSQL As String, ByRef prmRefAffectedRows As Integer) _
                                                                           Implements UtilDBIf.executeDB

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
        '                                               2006.06.16 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 置換パラメータ付き実行系SQLを発行する (詳細は使用例参照)
        ''' </summary>
        ''' <param name="prmSQL">パラメータ付きSQL文</param>
        ''' <param name="prmRefParameters">置換パラメータリスト</param>
        ''' <remarks>ストアド実行などを想定(それ以外も実行可能)</remarks>
        Public MustOverride Sub executeDB(ByVal prmSQL As String, _
                                          ByRef prmRefParameters As List(Of UtilDBPrm)) _
                                                                   Implements UtilDBIf.executeDB

        '-------------------------------------------------------------------------------
        '   シングルクォート文字列化
        '   （処理概要）シングルクォートを「''」に置換して返却
        '   ●入力パラメタ  ：prmSQL     Select文
        '   ●メソッド戻り値：置換後SQL文字列
        '                                               2006.05.23 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' シングルクォート文字列化 シングルクォートを「''」に置換して返却
        ''' </summary>
        ''' <param name="prmSQL">Select文</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shadows Function rmSQ(ByVal prmSQL As String) As String Implements UtilDBIf.rmSQ
            Return prmSQL.Replace("'", "''")
        End Function

        '-------------------------------------------------------------------------------
        '   Null⇒文字列
        '                                               2006.05.23 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' Null⇒文字列
        ''' </summary>
        ''' <param name="prmField"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function rmNullStr(ByVal prmField As Object) As String Implements UtilDBIf.rmNullStr
            Dim ret As String = ""
            Try
                If prmField Is Nothing Then
                ElseIf IsDBNull(prmField) Then
                Else
                    ret = CStr(prmField)
                End If
            Catch ex As Exception
            End Try
            Return ret
        End Function

        '-------------------------------------------------------------------------------
        '   Null⇒Short
        '                                               2006.05.23 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' Null⇒Short
        ''' </summary>
        ''' <param name="prmField"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function rmNullShort(ByVal prmField As Object) As Short Implements UtilDBIf.rmNullShort
            Dim ret As Short = 0
            Try
                If prmField Is Nothing Then
                ElseIf IsDBNull(prmField) Then
                ElseIf Not IsNumeric(prmField) Then
                Else
                    ret = CShort(prmField)
                End If
            Catch ex As Exception
            End Try
            Return ret
        End Function

        '-------------------------------------------------------------------------------
        '   Null⇒Integer
        '                                               2006.05.23 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' Null⇒Integer
        ''' </summary>
        ''' <param name="prmField"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function rmNullInt(ByVal prmField As Object) As Integer Implements UtilDBIf.rmNullInt
            Dim ret As Integer = 0
            Try
                If prmField Is Nothing Then
                ElseIf IsDBNull(prmField) Then
                ElseIf Not IsNumeric(prmField) Then
                Else
                    ret = CInt(prmField)
                End If
            Catch ex As Exception
            End Try
            Return ret
        End Function

        '-------------------------------------------------------------------------------
        '   Null⇒Long
        '                                               2006.05.23 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' Null⇒Long
        ''' </summary>
        ''' <param name="prmField"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function rmNullLong(ByVal prmField As Object) As Long Implements UtilDBIf.rmNullLong
            Dim ret As Long = 0
            Try
                If prmField Is Nothing Then
                ElseIf IsDBNull(prmField) Then
                ElseIf Not IsNumeric(prmField) Then
                Else
                    ret = CLng(prmField)
                End If
            Catch ex As Exception
            End Try
            Return ret
        End Function

        '-------------------------------------------------------------------------------
        '   Null⇒Double
        '                                               2006.05.23 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' Null⇒Double
        ''' </summary>
        ''' <param name="prmField"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function rmNullDouble(ByVal prmField As Object) As Double Implements UtilDBIf.rmNullDouble
            Dim ret As Double = 0
            Try
                If prmField Is Nothing Then
                ElseIf IsDBNull(prmField) Then
                ElseIf Not IsNumeric(prmField) Then
                Else
                    ret = CDbl(prmField)
                End If
            Catch ex As Exception
            End Try
            Return ret
        End Function

        '-------------------------------------------------------------------------------
        '   Null⇒日付文字列値
        '                                               2006.05.23 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' Null⇒日付文字列値
        ''' </summary>
        ''' <param name="prmField"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function rmNullDate(ByVal prmField As Object, _
                                   Optional ByVal prmFormatStr As String = "yyyy/MM/dd HH:mm:ss" _
                                   ) As String Implements UtilDBIf.rmNullDate
            Dim ret As String = ""
            Try
                If prmField Is Nothing Then
                ElseIf IsDBNull(prmField) Then
                ElseIf Not IsDate(prmField) Then
                Else
                    ret = (CDate(prmField)).ToString(prmFormatStr)
                End If
            Catch ex As Exception
            End Try
            Return ret
        End Function
    End Class

End Namespace
