Imports UtilMDL.Log


Namespace DB
    '===============================================================================
    '
    '  ユーティリティクラス
    '    （クラス名）    UtilOleDBDebugger
    '    （処理機能名）     ログ出力拡張機能を持ったDBアクセス(OLE DB)提供する
    '    （本MDL使用前提）  UtilLogDebuggerがプロジェクトに取り込まれていること
    '                       UtilDBInheritBase/UtilDBIfがプロジェクトに取り込まれていること
    '    （備考）           UtilDBInheritBaseを継承
    '                       UtilDBIfインターフェースを(UtilDBInheritBaseにて)実装
    '
    '===============================================================================
    '  履歴  名前          日  付      マーク      内容
    '-------------------------------------------------------------------------------
    '  (1)   Jun.Takagi    2006/04/25              新規
    '  (2)   Jun.Takagi    2006/05/23              UtilDBInheritBaseを継承元とする
    '  (3)   Jun.Takagi    2010/08/26              SystemInfoテーブルからの取得に対応
    '                                              接続先DBを出力
    '-------------------------------------------------------------------------------
    Public Class UtilOleDBDebugger
        Inherits UtilDBInheritBase

        '===============================================================================
        'メンバー定数定義
        '===============================================================================
        Private _logger As UtilLogDebugger      'ログデバッガ
        Private _hd As UtilOleDBHandler         'DBハンドラ

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
        '2006.05.23 add by takagi
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
        '                                               2010.08.26 Updated By Jun.Takagi
        '===============================================================================
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="prmUDLFileNm">UDLファイル名(フルパス)</param>
        ''' <param name="prmFileNm">Logファイル名(フルパス)</param>
        ''' <param name="prmDebugFlg">デバッグモード</param>
        ''' <param name="prmConsoleWrite">コンソール出力するかどうか</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal prmUDLFileNm As String, _
                       ByVal prmFileNm As String, _
                       ByVal prmDebugFlg As Boolean, _
                       Optional ByVal prmConsoleWrite As Boolean = True)

            _logger = New UtilLogDebugger(prmFileNm, prmDebugFlg, prmConsoleWrite)
            Try
                _hd = New UtilOleDBHandler(prmUDLFileNm)
                '-->2010.08.26 upd by takagi #接続先DB出力
                '_logger.writeLine(UtilLogDebugger.LOG_DEBUG, "データベース接続成功")
                Dim r As IO.StreamReader = New IO.StreamReader(prmUDLFileNm, System.Text.Encoding.Default)
                Dim conStr As String = ""
                Try : conStr = r.ReadToEnd()
                Finally : r.Close()
                End Try
                _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "HashCode:" & Me.GetHashCode & " データベース接続成功：ConnectionString=[" & conStr & "]")
                '<--2010.08.26 upd by takagi #接続先DB出力
            Catch ex As Exception
                '-->2010.08.26 upd by takagi #接続先DB出力
                '_logger.writeLine(UtilLogDebugger.LOG_ERR, "データベース接続失敗", ex.Message & ControlChars.NewLine & ex.StackTrace)
                _logger.writeLine(UtilLogDebugger.LOG_ERR, "HashCode:" & Me.GetHashCode & " データベース接続失敗", ex.Message & ControlChars.NewLine & ex.StackTrace)
                '<--2010.08.26 upd by takagi #接続先DB出力
                Throw ex
            End Try

        End Sub

        '-------------------------------------------------------------------------------
        '   DB切断
        '   （処理概要）DB接続をクローズする
        '   ●入力パラメタ  ：なし
        '   ●メソッド戻り値：なし
        '                                               2006.05.23 Updated By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' DB切断
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub close()
            Try
                _hd.close()
                '-->2010.08.26 upd by takagi #接続先DB出力
                '_logger.writeLine(UtilLogDebugger.LOG_DEBUG, "データベース切断")
                _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "HashCode:" & Me.GetHashCode & " データベース切断")
                '<--2010.08.26 upd by takagi #接続先DB出力
            Catch ex As Exception
                '-->2010.08.26 upd by takagi #接続先DB出力
                '_logger.writeLine(UtilLogDebugger.LOG_ERR, "データベース切断失敗", ex.Message & ControlChars.NewLine & ex.StackTrace)
                _logger.writeLine(UtilLogDebugger.LOG_ERR, "HashCode:" & Me.GetHashCode & " データベース切断失敗", ex.Message & ControlChars.NewLine & ex.StackTrace)
                '<--2010.08.26 upd by takagi #接続先DB出力
                Throw ex
            End Try

        End Sub

        '-------------------------------------------------------------------------------
        '   トランザクション開始
        '   （処理概要) トランザクションを開始する
        '   ●入力パラメタ  ：なし
        '   ●メソッド戻り値：なし
        '                                               2006.05.23 Updated By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' トランザクション開始
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub beginTran()
            Try
                _hd.beginTran()
                '-->2010.08.26 upd by takagi #接続先DB出力
                '_logger.writeLine(UtilLogDebugger.LOG_DEBUG, "beginTran")
                _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "HashCode:" & Me.GetHashCode & " beginTran")
                '<--2010.08.26 upd by takagi #接続先DB出力
            Catch ex As Exception
                '-->2010.08.26 upd by takagi #接続先DB出力
                '_logger.writeLine(UtilLogDebugger.LOG_ERR, "beginTran失敗", ex.Message & ControlChars.NewLine & ex.StackTrace)
                _logger.writeLine(UtilLogDebugger.LOG_ERR, "HashCode:" & Me.GetHashCode & " beginTran失敗", ex.Message & ControlChars.NewLine & ex.StackTrace)
                '<--2010.08.26 upd by takagi #接続先DB出力
                Throw ex
            End Try
        End Sub

        '-------------------------------------------------------------------------------
        '   トランザクション終了
        '   （処理概要) トランザクションをCommitする
        '   ●入力パラメタ  ：なし
        '   ●メソッド戻り値：なし
        '                                               2006.05.23 Updated By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' トランザクション終了
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub commitTran()
            Try
                _hd.commitTran()
                '-->2010.08.26 upd by takagi #接続先DB出力
                '_logger.writeLine(UtilLogDebugger.LOG_DEBUG, "commitTran")
                _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "HashCode:" & Me.GetHashCode & " commitTran")
                '<--2010.08.26 upd by takagi #接続先DB出力
            Catch ex As Exception
                '-->2010.08.26 upd by takagi #接続先DB出力
                '_logger.writeLine(UtilLogDebugger.LOG_ERR, "commitTran失敗", ex.Message & ControlChars.NewLine & ex.StackTrace)
                _logger.writeLine(UtilLogDebugger.LOG_ERR, "HashCode:" & Me.GetHashCode & " commitTran失敗", ex.Message & ControlChars.NewLine & ex.StackTrace)
                '<--2010.08.26 upd by takagi #接続先DB出力
                Throw ex
            End Try
        End Sub

        '-------------------------------------------------------------------------------
        '   トランザクション破棄
        '   （処理概要) トランザクションをRollbackする
        '   ●入力パラメタ  ：なし
        '   ●メソッド戻り値：なし
        '                                               2006.05.23 Updated By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' トランザクション破棄
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub rollbackTran()
            Try
                _hd.rollbackTran()
                '-->2010.08.26 upd by takagi #接続先DB出力
                '_logger.writeLine(UtilLogDebugger.LOG_DEBUG, "rollbackTran")
                _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "HashCode:" & Me.GetHashCode & " rollbackTran")
                '<--2010.08.26 upd by takagi #接続先DB出力
            Catch ex As Exception
                '-->2010.08.26 upd by takagi #接続先DB出力
                '_logger.writeLine(UtilLogDebugger.LOG_ERR, "rollbackTran失敗", ex.Message & ControlChars.NewLine & ex.StackTrace)
                _logger.writeLine(UtilLogDebugger.LOG_ERR, "HashCode:" & Me.GetHashCode & " rollbackTran失敗", ex.Message & ControlChars.NewLine & ex.StackTrace)
                '<--2010.08.26 upd by takagi #接続先DB出力
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
        '                                               2006.05.23 Updated By Jun.Takagi
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
                '-->2010.08.26 upd by takagi #接続先DB出力
                '_logger.writeLine(UtilLogDebugger.LOG_DEBUG, "データ取得件数：" & prmRefRecCnt & "件", prmSQL)
                _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "HashCode:" & Me.GetHashCode & " データ取得件数：" & prmRefRecCnt & "件", prmSQL)
                '<--2010.08.26 upd by takagi #接続先DB出力
            Catch ex As Exception
                '-->2010.08.26 upd by takagi #接続先DB出力
                '_logger.writeLine(UtilLogDebugger.LOG_ERR, "データ取得失敗：" & ex.Message & ControlChars.NewLine & ex.StackTrace, prmSQL)
                _logger.writeLine(UtilLogDebugger.LOG_ERR, "HashCode:" & Me.GetHashCode & " データ取得失敗：" & ex.Message & ControlChars.NewLine & ex.StackTrace, prmSQL)
                '<--2010.08.26 upd by takagi #接続先DB出力
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
                    '-->2010.08.26 upd by takagi #接続先DB出力
                    '_logger.writeLine(UtilLogDebugger.LOG_DEBUG, "データ取得件数：" & prmRefRecCnt & "件", prmSQL & " {パラメータ：" & outWk & "}")
                    _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "HashCode:" & Me.GetHashCode & " データ取得件数：" & prmRefRecCnt & "件", prmSQL & " {パラメータ：" & outWk & "}")
                    '<--2010.08.26 upd by takagi #接続先DB出力
                Catch ex As Exception
                    '-->2010.08.26 upd by takagi #接続先DB出力
                    '_logger.writeLine(UtilLogDebugger.LOG_ERR, "データ取得失敗：" & ex.Message & ControlChars.NewLine & ex.StackTrace, prmSQL & " {パラメータ：" & outWk & "}")
                    _logger.writeLine(UtilLogDebugger.LOG_ERR, "HashCode:" & Me.GetHashCode & " データ取得失敗：" & ex.Message & ControlChars.NewLine & ex.StackTrace, prmSQL & " {パラメータ：" & outWk & "}")
                    '<--2010.08.26 upd by takagi #接続先DB出力
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
        '                                               2006.05.23 Updated By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 更新SQL文発行 Insert/Update/Delete文を発行する
        ''' </summary>
        ''' <param name="prmSQL">SQL</param>
        ''' <remarks></remarks>
        Public Overrides Sub executeDB(ByVal prmSQL As String)
            Try
                _hd.executeDB(prmSQL)
                '-->2010.08.26 upd by takagi #接続先DB出力
                '_logger.writeLine(UtilLogDebugger.LOG_DEBUG, "データ更新成功", prmSQL)
                _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "HashCode:" & Me.GetHashCode & " データ更新成功", prmSQL)
                '<--2010.08.26 upd by takagi #接続先DB出力
            Catch ex As Exception
                '-->2010.08.26 upd by takagi #接続先DB出力
                '_logger.writeLine(UtilLogDebugger.LOG_ERR, "データ更新失敗：" & ex.Message & ControlChars.NewLine & ex.StackTrace, prmSQL)
                _logger.writeLine(UtilLogDebugger.LOG_ERR, "HashCode:" & Me.GetHashCode & " データ更新失敗：" & ex.Message & ControlChars.NewLine & ex.StackTrace, prmSQL)
                '<--2010.08.26 upd by takagi #接続先DB出力
                Throw ex
            End Try
        End Sub

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
        Public Overrides Sub executeDB(ByVal prmSQL As String, ByRef prmRefAffectedRows As Integer)
            Try
                _hd.executeDB(prmSQL, prmRefAffectedRows)
                '-->2010.08.26 upd by takagi #接続先DB出力
                '_logger.writeLine(UtilLogDebugger.LOG_DEBUG, "データ更新成功(更新件数：" & prmRefAffectedRows.ToString & "件)", prmSQL)
                _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "HashCode:" & Me.GetHashCode & " データ更新成功(更新件数：" & prmRefAffectedRows.ToString & "件)", prmSQL)
                '<--2010.08.26 upd by takagi #接続先DB出力
            Catch ex As Exception
                '-->2010.08.26 upd by takagi #接続先DB出力
                '_logger.writeLine(UtilLogDebugger.LOG_ERR, "データ更新失敗：" & ex.Message & ControlChars.NewLine & ex.StackTrace, prmSQL)
                _logger.writeLine(UtilLogDebugger.LOG_ERR, "HashCode:" & Me.GetHashCode & " データ更新失敗：" & ex.Message & ControlChars.NewLine & ex.StackTrace, prmSQL)
                '<--2010.08.26 upd by takagi #接続先DB出力
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
        '                                               2006.06.16 Created By Jun.Takagi
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
                    '-->2010.08.26 upd by takagi #接続先DB出力
                    '_logger.writeLine(UtilLogDebugger.LOG_DEBUG, "データ更新成功", prmSQL & " {パラメータ：" & outWk & "}")
                    _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "HashCode:" & Me.GetHashCode & " データ更新成功", prmSQL & " {パラメータ：" & outWk & "}")
                    '<--2010.08.26 upd by takagi #接続先DB出力
                Catch ex As Exception
                    '-->2010.08.26 upd by takagi #接続先DB出力
                    '_logger.writeLine(UtilLogDebugger.LOG_ERR, "データ更新失敗：" & ex.Message & ControlChars.NewLine & ex.StackTrace, prmSQL & " {パラメータ：" & outWk & "}")
                    _logger.writeLine(UtilLogDebugger.LOG_ERR, "HashCode:" & Me.GetHashCode & " データ更新失敗：" & ex.Message & ControlChars.NewLine & ex.StackTrace, prmSQL & " {パラメータ：" & outWk & "}")
                    '<--2010.08.26 upd by takagi #接続先DB出力
                    Throw ex
                End Try
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        '-------------------------------------------------------------------------------
        '   SystemInfo取得
        '   （処理概要）　SystemInfoから値を取得する
        '   ●入力パラメタ   ：prmFixKey        固定キー
        '                      prmVariableKey   可変キー
        '   ●メソッド戻り値 ：SystemInfoレコード
        '                                               2010.08.26 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        Public Overrides Function getSystemInfo(ByVal prmFixKey As String, ByVal prmVariableKey As String) As UtilDBIf.sysinfoRec
            Dim rec As UtilDBIf.sysinfoRec
            Try
                rec = _hd.getSystemInfo(prmFixKey, prmVariableKey)
                _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "HashCode:" & Me.GetHashCode & " SystemInfo取得成功：固定キー=[" & prmFixKey & "] 可変キー=[" & prmVariableKey & "]")
            Catch ex As Exception
                _logger.writeLine(UtilLogDebugger.LOG_ERR, "HashCode:" & Me.GetHashCode & " SystemInfo取得失敗：" & ex.ToString)
                Throw ex
            End Try
            Return rec
        End Function

        '-------------------------------------------------------------------------------
        '   SystemInfo取得
        '   （処理概要）　SystemInfoから値を取得する
        '   ●入力パラメタ   ：prmFixKey        固定キー
        '   ●メソッド戻り値 ：SystemInfoレコード
        '                                               2010.08.26 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        Public Overrides Function getSystemInfo(ByVal prmFixKey As String) As UtilDBIf.sysinfoRec()
            Dim rec() As UtilDBIf.sysinfoRec
            Try
                rec = _hd.getSystemInfo(prmFixKey)
                _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "HashCode:" & Me.GetHashCode & " SystemInfo取得成功：固定キー=[" & prmFixKey & "] " & rec.Length & "件")
            Catch ex As Exception
                _logger.writeLine(UtilLogDebugger.LOG_ERR, "HashCode:" & Me.GetHashCode & " SystemInfo取得失敗：" & ex.ToString)
                Throw ex
            End Try
            Return rec
        End Function

        '-------------------------------------------------------------------------------
        '   SystemInfo取得
        '   （処理概要）　SystemInfoから値を取得する
        '   ●入力パラメタ   ：なし
        '   ●メソッド戻り値 ：SystemInfoレコード
        '                                               2010.08.26 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        Public Overrides Function getSystemInfo() As UtilDBIf.sysinfoRec()
            Dim rec() As UtilDBIf.sysinfoRec
            Try
                rec = _hd.getSystemInfo()
                _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "HashCode:" & Me.GetHashCode & " SystemInfo取得成功：" & rec.Length & "件")
            Catch ex As Exception
                _logger.writeLine(UtilLogDebugger.LOG_ERR, "HashCode:" & Me.GetHashCode & " SystemInfo取得失敗：" & ex.ToString)
                Throw ex
            End Try
            Return rec
        End Function

        '-------------------------------------------------------------------------------
        '   SystemInfo取得
        '   （処理概要）　SystemInfoからFixKeyリストを取得する
        '   ●入力パラメタ   ：なし
        '   ●メソッド戻り値 ：SystemInfoレコード
        '                                               2010.08.26 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        Public Overrides Function getSystemInfoFixKeies() As UtilDBIf.sysinfoRec()
            Dim rec() As UtilDBIf.sysinfoRec
            Try
                rec = _hd.getSystemInfoFixKeies()
                _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "HashCode:" & Me.GetHashCode & " SystemInfoFixKeies取得成功：" & rec.Length & "件")
            Catch ex As Exception
                _logger.writeLine(UtilLogDebugger.LOG_ERR, "HashCode:" & Me.GetHashCode & " SystemInfoFixKeies取得失敗：" & ex.ToString)
                Throw ex
            End Try
            Return rec
        End Function

    End Class
End Namespace
