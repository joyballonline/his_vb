Imports UtilMDL.Log

Namespace DB
    '===============================================================================
    '
    '  ユーティリティクラス
    '    （クラス名）    UtilPostgresDebugger
    '    （処理機能名）     ログ出力拡張機能を持ったDBアクセス(Npgsql)提供する
    '    （本MDL使用前提）  UtilLogDebugger/UtilPostgresHandlerがプロジェクトに取り込まれていること
    '                       UtilDBInheritBase/UtilDBIfがプロジェクトに取り込まれていること
    '    （備考）           UtilDBInheritBaseを継承
    '                       UtilDBIfインターフェースを(UtilDBInheritBaseにて)実装
    '
    '===============================================================================
    '  履歴  名前          日  付      マーク      内容
    '-------------------------------------------------------------------------------
    '  (1)   Jun.Takagi    2014/02/20              新規
    '-------------------------------------------------------------------------------
    Public Class UtilPostgresDebugger
        Inherits UtilDBInheritBase

        '===============================================================================
        'メンバー定数定義
        '===============================================================================
        Private _logger As UtilLogDebugger      'ログデバッガ
        Private _hd As UtilPostgresHandler      'DBハンドラ

        '===============================================================================
        'メンバー変数定義
        '===============================================================================
        'なし

        '===============================================================================
        'プロパティ(アクセサ)
        '===============================================================================
        Public Property debugFlg() As Boolean 'デバッグモード
            'Geter--------
            Get
                Return _logger.debugFlg
            End Get
            'Setter-------
            Set(ByVal Value As Boolean)
                _logger.debugFlg = Value
            End Set
        End Property
        Public Property consoleWrite() As Boolean 'コンソール出力するかどうか
            'Geter--------
            Get
                Return _logger.consoleWrite
            End Get
            'Setter-------
            Set(ByVal Value As Boolean)
                _logger.consoleWrite = Value
            End Set
        End Property
        Public Overrides ReadOnly Property isTransactionOpen() As Boolean 'トランザクションが開いているかどうか
            Get
                Return _hd.isTransactionOpen
            End Get
        End Property

        '===============================================================================
        ' コンストラクタ
        '   ●入力パラメタ   ：  prmUDLFileNm        UDLファイル名(フルパス)
        '                       prmFileNm           Logファイル名(フルパス)
        '                       prmDebugFlg         デバッグモード
        '                       <prmConsoleWrite>   コンソール出力するかどうか
        '                       <prmTimeout>        発行SQLのタイムアウト設定(省略時は規定値の30秒/0設定時は永久待機)
        '                                               2014.02.20 Created By Jun.Takagi
        '===============================================================================
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="prmSvAdr">サーバーアドレス</param>
        ''' <param name="prmPortNo">ポート番号</param>
        ''' <param name="prmDbNm">データベース名</param>
        ''' <param name="prmUserId">ユーザーID</param>
        ''' <param name="prmPswd">パスワード</param>
        ''' <param name="prmFileNm">Logファイル名(フルパス)</param>
        ''' <param name="prmDebugFlg">デバッグモード</param>
        ''' <param name="prmConsoleWrite">コンソール出力するかどうか</param>
        ''' <param name="prmTimeout">発行SQLのタイムアウト設定(省略時は規定値の30秒/0設定時は永久待機)</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal prmSvAdr As String, ByVal prmPortNo As String, ByVal prmDbNm As String, ByVal prmUserId As String, ByVal prmPswd As String,
                       ByVal prmFileNm As String,
                       ByVal prmDebugFlg As Boolean,
                       Optional ByVal prmConsoleWrite As Boolean = True,
                       Optional ByVal prmTimeout As Short = -1)
            _logger = New UtilLogDebugger(prmFileNm, prmDebugFlg, prmConsoleWrite)
            Try
                _hd = New UtilPostgresHandler(prmSvAdr, prmPortNo, prmDbNm, prmUserId, prmPswd, prmTimeout)
                _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "データベース接続成功")
            Catch ex As Exception
                _logger.writeLine(UtilLogDebugger.LOG_ERR, "データベース接続失敗", ex.Message & ControlChars.NewLine & ex.StackTrace)
                Throw ex
            End Try

        End Sub

        '-------------------------------------------------------------------------------
        '   DB切断
        '   （処理概要）DB接続をクローズする
        '   ●入力パラメタ  ：なし
        '   ●メソッド戻り値：なし
        '                                               2014.02.20 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' DB切断
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub close()
            Try
                _hd.close()
                _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "データベース切断")
            Catch ex As Exception
                _logger.writeLine(UtilLogDebugger.LOG_ERR, "データベース切断失敗", ex.Message & ControlChars.NewLine & ex.StackTrace)
                Throw ex
            End Try

        End Sub

        '-------------------------------------------------------------------------------
        '   トランザクション開始
        '   （処理概要) トランザクションを開始する
        '   ●入力パラメタ  ：なし
        '   ●メソッド戻り値：なし
        '                                               2014.02.20 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' トランザクション開始
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub beginTran()
            Try
                _hd.beginTran()
                _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "beginTran")
            Catch ex As Exception
                _logger.writeLine(UtilLogDebugger.LOG_ERR, "beginTran失敗", ex.Message & ControlChars.NewLine & ex.StackTrace)
                Throw ex
            End Try
        End Sub

        '-------------------------------------------------------------------------------
        '   トランザクション終了
        '   （処理概要) トランザクションをCommitする
        '   ●入力パラメタ  ：なし
        '   ●メソッド戻り値：なし
        '                                               2014.02.20 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' トランザクション終了
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub commitTran()
            Try
                _hd.commitTran()
                _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "commitTran")
            Catch ex As Exception
                _logger.writeLine(UtilLogDebugger.LOG_ERR, "commitTran失敗", ex.Message & ControlChars.NewLine & ex.StackTrace)
                Throw ex
            End Try
        End Sub

        '-------------------------------------------------------------------------------
        '   トランザクション破棄
        '   （処理概要) トランザクションをRollbackする
        '   ●入力パラメタ  ：なし
        '   ●メソッド戻り値：なし
        '                                               2014.02.20 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' トランザクション破棄
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub rollbackTran()
            Try
                _hd.rollbackTran()
                _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "rollbackTran")
            Catch ex As Exception
                _logger.writeLine(UtilLogDebugger.LOG_ERR, "rollbackTran失敗", ex.Message & ControlChars.NewLine & ex.StackTrace)
                Throw ex
            End Try
        End Sub

        '-------------------------------------------------------------------------------
        '   Select文発行
        '   （処理概要）Select文を発行し、DataSetを返却する
        '   ●入力パラメタ  ：prmSQL        Select文
        '                  ：prmTblName     返却されるDataSetのTBL名称
        '                  ：<prmRefRecCnt> 取得件数
        '   ●メソッド戻り値：DataSet
        '   ●備考          ：返却するDataSetはprmTblNameのTBL名称で格納
        '                                               2014.02.20 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' Select文発行 Select文を発行し、DataSetを返却する 返却するDataSetはprmTblNameのTBL名称で格納
        ''' </summary>
        ''' <param name="prmSQL">Select文</param>
        ''' <param name="prmTblName">返却されるDataSetのTBL名称</param>
        ''' <param name="prmRefRecCnt">取得件数</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overrides Function selectDB(ByVal prmSQL As String, _
                                           ByVal prmTblName As String, _
                                           Optional ByRef prmRefRecCnt As Integer = 0) As DataSet
            Dim ds As DataSet
            Try
                ds = _hd.selectDB(prmSQL, prmTblName, prmRefRecCnt)
                _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "データ取得件数：" & prmRefRecCnt & "件", prmSQL)
            Catch ex As Exception
                _logger.writeLine(UtilLogDebugger.LOG_ERR, "データ取得失敗：" & ex.Message & ControlChars.NewLine & ex.StackTrace, prmSQL)
                Throw ex
            End Try
            Return ds
        End Function

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
        '                                               2014.02.20 Created By Jun.Takagi
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
        Public Overrides Function selectDB(ByVal prmSQL As String, _
                                               ByVal prmParameters As List(Of UtilDBPrm), _
                                               ByVal prmTblName As String, _
                                               Optional ByRef prmRefRecCnt As Integer = 0) As DataSet
            Dim ds As DataSet
            Try
                Dim outWk As String = ""
                For i As Integer = 0 To prmParameters.Count - 1
                    If Not "".Equals(outWk) Then
                        outWk = outWk & " , "
                    End If
                    Select Case prmParameters(i).type
                        Case UtilDBPrm.parameterType.tDate
                            outWk = outWk & "#" & CStr(prmParameters(i).value) & "#"
                        Case UtilDBPrm.parameterType.tVarchar
                            outWk = outWk & "'" & CStr(prmParameters(i).value) & "'"
                        Case Else
                            outWk = outWk & "" & CStr(prmParameters(i).value) & ""
                    End Select
                Next
                Try
                    ds = _hd.selectDB(prmSQL, prmParameters, prmTblName, prmRefRecCnt)
                    _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "データ取得件数：" & prmRefRecCnt & "件", prmSQL & " {パラメータ：" & outWk & "}")
                Catch ex As Exception
                    _logger.writeLine(UtilLogDebugger.LOG_ERR, "データ取得失敗：" & ex.Message & ControlChars.NewLine & ex.StackTrace, prmSQL & " {パラメータ：" & outWk & "}")
                    Throw ex
                End Try
            Catch ex As Exception
                Throw ex
            End Try
            Return ds
        End Function

        '-------------------------------------------------------------------------------
        '   更新SQL文発行
        '   （処理概要）Insert/Update/Delete文を発行する
        '   ●入力パラメタ  ：prmSQL        SQL文
        '   ●メソッド戻り値：なし
        '                                               2014.02.20 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 更新SQL文発行 Insert/Update/Delete文を発行する
        ''' </summary>
        ''' <param name="prmSQL">SQL</param>
        ''' <remarks></remarks>
        Public Overrides Sub executeDB(ByVal prmSQL As String)
            Try
                _hd.executeDB(prmSQL)
                _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "データ更新成功", prmSQL)
            Catch ex As Exception
                _logger.writeLine(UtilLogDebugger.LOG_ERR, "データ更新失敗：" & ex.Message & ControlChars.NewLine & ex.StackTrace, prmSQL)
                Throw ex
            End Try
        End Sub

        '-------------------------------------------------------------------------------
        '   更新SQL文発行
        '   （処理概要）Insert/Update/Delete文を発行する
        '   ●入力パラメタ  ：prmSQL                SQL文
        '                   ：prmRefAffectedRows    影響を受けた行数
        '   ●メソッド戻り値：なし
        '                                               2014.02.20 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 実行系SQLを発行する（影響件数引数付き）
        ''' </summary>
        ''' <param name="prmSQL">発行するSQL文</param>
        ''' <param name="prmRefAffectedRows">影響を受けた行数</param>
        ''' <remarks>レコードセットを生成しないSQL(INSERT/UPDATE/DELETE…etc)を発行する。</remarks>
        Public Overrides Sub executeDB(ByVal prmSQL As String, ByRef prmRefAffectedRows As Integer)
            Try
                _hd.executeDB(prmSQL, prmRefAffectedRows)
                _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "データ更新成功(更新件数：" & prmRefAffectedRows.ToString & "件)", prmSQL)
            Catch ex As Exception
                _logger.writeLine(UtilLogDebugger.LOG_ERR, "データ更新失敗：" & ex.Message & ControlChars.NewLine & ex.StackTrace, prmSQL)
                Throw ex
            End Try
        End Sub

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
        '                                               2014.02.20 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 置換パラメータ付き実行系SQLを発行する (詳細は使用例参照)
        ''' </summary>
        ''' <param name="prmSQL">パラメータ付きSQL文</param>
        ''' <param name="prmRefParameters">置換パラメータリスト</param>
        ''' <remarks>ストアド実行などを想定(それ以外も実行可能)</remarks>
        Public Overrides Sub executeDB(ByVal prmSQL As String, _
                                       ByRef prmRefParameters As List(Of UtilDBPrm))
            Try
                Dim outWk As String = ""
                For i As Integer = 0 To prmRefParameters.Count - 1
                    If Not "".Equals(outWk) Then
                        outWk = outWk & " , "
                    End If
                    Select Case prmRefParameters(i).type
                        Case UtilDBPrm.parameterType.tDate
                            outWk = outWk & "#" & CStr(prmRefParameters(i).value) & "#"
                        Case UtilDBPrm.parameterType.tVarchar
                            outWk = outWk & "'" & CStr(prmRefParameters(i).value) & "'"
                        Case Else
                            outWk = outWk & "" & CStr(prmRefParameters(i).value) & ""
                    End Select
                Next
                Try
                    _hd.executeDB(prmSQL, prmRefParameters)
                    _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "データ更新成功", prmSQL & " {パラメータ：" & outWk & "}")
                Catch ex As Exception
                    _logger.writeLine(UtilLogDebugger.LOG_ERR, "データ更新失敗：" & ex.Message & ControlChars.NewLine & ex.StackTrace, prmSQL & " {パラメータ：" & outWk & "}")
                    Throw ex
                End Try
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

    End Class
End Namespace
