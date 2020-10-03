Imports Npgsql
Imports NpgsqlTypes

Namespace DB
    '===============================================================================
    '
    '  ユーティリティクラス
    '    （クラス名）    UtilPostgresHandler
    '    （処理機能名）     NpgsqlによるDBアクセス機能を提供する
    '    （本MDL使用前提）  UtilDBInheritBase/UtilDBIfをプロジェクトに取り込んでいること
    '                       Npgsql.dllを参照設定していること
    '    （備考）           UtilDBInheritBaseを継承
    '                       UtilDBIfインターフェースを(UtilDBInheritBaseにて)実装
    '
    '===============================================================================
    '  履歴  名前          日  付      マーク      内容
    '-------------------------------------------------------------------------------
    '  (1)   Laevigata, Inc.    2014/02/20              新規
    '-------------------------------------------------------------------------------
    Public Class UtilPostgresHandler
        Inherits UtilDBInheritBase

        '===============================================================================
        'メンバー変数定義
        '===============================================================================
        Private _con As NpgsqlConnection    'コネクション
        Private _cmd As NpgsqlCommand       'コマンド
        Private _adp As NpgsqlDataAdapter   'アダプタ
        Private _tran As NpgsqlTransaction  'トランザクション
        Private _tranFlg As Boolean = False 'トランザクション中フラグ

        '===============================================================================
        'プロパティ(アクセサ)
        '===============================================================================
        Public ReadOnly Property con() As NpgsqlConnection
            'Geter--------
            Get
                con = _con
            End Get
            'Setter-------
            'なし
        End Property
        Public Overrides ReadOnly Property isTransactionOpen() As Boolean
            'Geter--------
            Get
                isTransactionOpen = _tranFlg
            End Get
            'Setter-------
            'なし
        End Property

        '===============================================================================
        ' コンストラクタ
        '   （処理概要）パラメータよりDBコネクションを新規生成する
        '   ●入力パラメタ   ：  prmSvAdr      サーバーアドレス
        '                    ：  prmPortNo     ポート番号
        '                    ：  prmDbNm       データベース名
        '                    ：  prmUserId     ユーザーID
        '                    ：  prmPswd       パスワード
        '                    ：  <prmTimeout>  発行SQLのタイムアウト設定(省略時は既定の30秒/0設定時は永久待機)
        '                                               2014.02.20 Created By Laevigata, Inc.
        '===============================================================================
        ''' <summary>
        ''' コンストラクタ UDLファイルよりDBコネクションを新規生成する
        ''' </summary>
        ''' <param name="prmSvAdr">サーバーアドレス</param>
        ''' <param name="prmPortNo">ポート番号</param>
        ''' <param name="prmDbNm">データベース名</param>
        ''' <param name="prmUserId">ユーザーID</param>
        ''' <param name="prmPswd">パスワード</param>
        ''' <param name="prmTimeout">発行SQLのタイムアウト設定(省略時は既定の30秒/0設定時は永久待機)</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal prmSvAdr As String, ByVal prmPortNo As String, ByVal prmDbNm As String, ByVal prmUserId As String, ByVal prmPswd As String, Optional ByVal prmTimeout As Short = -1, Optional ByVal prmSsl As Boolean = False)
            Try
                Call initInstance(prmSvAdr, prmPortNo, prmDbNm, prmUserId, prmPswd, prmTimeout, prmSsl)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        '===============================================================================
        ' コンストラクタ
        '   （処理概要）サブクラス用のコンストラクタ。インスタンス生成後initInstanceを呼び出すこと
        '===============================================================================
        ''' <summary>
        ''' コンストラクタ サブクラス用のコンストラクタ。インスタンス生成後initInstanceを呼び出すこと
        ''' </summary>
        ''' <remarks></remarks>
        Protected Sub New()
            '処理なし
        End Sub

        '===============================================================================
        ' サブルーチン[サブクラス用]
        '   （処理概要）パラメータよりDBコネクションを新規生成する
        '   ●入力パラメタ   ：  prmSvAdr      サーバーアドレス
        '                    ：  prmPortNo     ポート番号
        '                    ：  prmDbNm       データベース名
        '                    ：  prmUserId     ユーザーID
        '                    ：  prmPswd       パスワード
        '                    ：  <prmTimeout>  発行SQLのタイムアウト設定(省略時は既定の30秒/0設定時は永久待機)
        '                                               2014.02.20 Created By Laevigata, Inc.
        '===============================================================================
        ''' <summary>
        ''' サブルーチン[サブクラス用] UDLファイルよりDBコネクションを新規生成する
        ''' </summary>
        ''' <param name="prmSvAdr">サーバーアドレス</param>
        ''' <param name="prmPortNo">ポート番号</param>
        ''' <param name="prmDbNm">データベース名</param>
        ''' <param name="prmUserId">ユーザーID</param>
        ''' <param name="prmPswd">パスワード</param>
        ''' <param name="prmTimeout">発行SQLのタイムアウト設定(省略時は既定の30秒/0設定時は永久待機)</param>
        ''' <remarks></remarks>
        Protected Sub initInstance(ByVal prmSvAdr As String, ByVal prmPortNo As String, ByVal prmDbNm As String, ByVal prmUserId As String, ByVal prmPswd As String, Optional ByVal prmTimeout As Short = -1, Optional ByVal prmSsl As Boolean = False)
            Try
                Dim ext As String = ";"
                If prmSsl Then
                    ext = ";SSL=True;SSLMode=Require;"
                Else

                End If
                _con = New NpgsqlConnection()
                _con.ConnectionString = "Server=" & prmSvAdr & ";Port=" & prmPortNo & ";Database=" & prmDbNm & ";Encoding=UNICODE;User Id=" & prmUserId & ";Password=" & prmPswd & ext '";" 'SSL=True;SSLMode=Require;"
                Try
                    _con.Open()
                Catch lex As Exception
                    Throw New Exception("Failed to connect to the database." & ControlChars.NewLine &
                                        ControlChars.Tab & "　 " & lex.Message, lex)
                End Try


                _adp = New NpgsqlDataAdapter()
                _cmd = New NpgsqlCommand()
                _cmd.Connection = _con
                If prmTimeout > -1 Then 'Optional引数省略の場合は規定値の30秒が適用される
                    _cmd.CommandTimeout = prmTimeout '0の場合はタイムアウト無し(永久待機)となるので注意のこと！
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Debug.WriteLine(ex.StackTrace)
                Throw ex
            End Try

        End Sub

        '===============================================================================
        ' デストラクタ
        '   （処理概要）DBコネクションの終了処理実施
        '===============================================================================
        ''' <summary>
        ''' デストラクタ DBコネクションの終了処理実施
        ''' </summary>
        ''' <remarks></remarks>
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            Try
                If _con IsNot Nothing Then
                    If _con.State = ConnectionState.Open Then
                        _con.Close()
                    End If
                End If
            Catch ex As Exception
            Finally
                _con = Nothing
                _adp = Nothing
                _cmd = Nothing
            End Try

        End Sub

        '-------------------------------------------------------------------------------
        '   DB切断
        '   （処理概要）DB接続をクローズする
        '   ●入力パラメタ  ：なし
        '   ●メソッド戻り値：なし
        '                                               2014.02.20 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' DB切断 
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub close()
            Try
                If _con.State = ConnectionState.Open Then
                    _con.Close()
                End If
            Catch ex As Exception
            Finally
                _con.Dispose()
            End Try

        End Sub

        '-------------------------------------------------------------------------------
        '   トランザクション開始
        '   （処理概要)　トランザクションを開始する
        '   ●入力パラメタ  ：なし
        '   ●メソッド戻り値：なし
        '                                               2014.02.20 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' トランザクション開始
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub beginTran()
            Try
                _tran = _con.BeginTransaction()
                _cmd.Transaction = _tran
                _tranFlg = True
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Debug.WriteLine(ex.StackTrace)
                Throw ex
            End Try
        End Sub

        '-------------------------------------------------------------------------------
        '   トランザクション終了
        '   （処理概要)　トランザクションをCommitする
        '   ●入力パラメタ  ：なし
        '   ●メソッド戻り値：なし
        '                                               2014.02.20 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' トランザクション終了 トランザクションをCommitする
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub commitTran()
            Try
                _tran.Commit()
                _tranFlg = False
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Debug.WriteLine(ex.StackTrace)
                Throw ex
            End Try
        End Sub

        '-------------------------------------------------------------------------------
        '   トランザクション破棄
        '   （処理概要)　トランザクションをRollbackする
        '   ●入力パラメタ  ：なし
        '   ●メソッド戻り値：なし
        '                                               2014.02.20 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' トランザクション破棄 トランザクションをRollbackする
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub rollbackTran()
            Try
                _tran.Rollback()
                _tranFlg = False
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Debug.WriteLine(ex.StackTrace)
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
        '                                               2014.02.20 Created By Laevigata, Inc.
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
            Try

                _cmd.CommandText = prmSQL
                _adp.SelectCommand = _cmd
                Dim ds As DataSet = New DataSet()

                'Select文発行
                Call _adp.Fill(ds, prmTblName)

                '取得件数
                prmRefRecCnt = ds.Tables(0).Rows.Count

                '戻り値設定
                selectDB = ds

            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Debug.WriteLine(ex.StackTrace)
                Throw ex
            End Try

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
        '                                               2014.02.20 Created By Laevigata, Inc.
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
            Try

                _cmd.CommandText = prmSQL
                _cmd.Parameters.Clear()
                If prmParameters Is Nothing Then

                    'パラメタなし
                    If InStr(prmSQL, "?") > 0 Then
                        '置換パラメタが存在するのにパラメタリストを受領していないのでエラーとする
                        Throw New UsrDefException("置換パラメータが設定されていません。")
                    End If

                Else

                    'パラメタあり
                    Dim p As NpgsqlParameter = Nothing
                    For i As Integer = 0 To prmParameters.Count - 1      '受領パラメタ数分Loop
                        Dim typePrm As NpgsqlDbType = Nothing               '型
                        Select Case prmParameters(i).type
                            Case UtilDBPrm.parameterType.tBoolean
                                typePrm = NpgsqlDbType.Boolean
                            Case UtilDBPrm.parameterType.tDate
                                typePrm = NpgsqlDbType.Date
                            Case UtilDBPrm.parameterType.tNumber
                                typePrm = NpgsqlDbType.Numeric
                            Case UtilDBPrm.parameterType.tVarchar
                                typePrm = NpgsqlDbType.Char
                            Case Else
                                Throw New UsrDefException("置換パラメータの設定が誤っています。")
                        End Select
                        Dim wkName As String = CStr(i)                   '名称
                        p = _cmd.Parameters.Add(wkName, typePrm)
                        p.Value = prmParameters(i).value                 '値
                        p.Direction = prmParameters(i).systemDirection   '方向
                        p.Size = prmParameters(i).size                   'サイズ
                    Next

                End If

                _adp.SelectCommand = _cmd
                Dim ds As DataSet = New DataSet()

                'Select文発行
                Call _adp.Fill(ds, prmTblName)

                '取得件数
                prmRefRecCnt = ds.Tables(0).Rows.Count

                '戻り値設定
                selectDB = ds

            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Debug.WriteLine(ex.StackTrace)
                Throw ex
            End Try

        End Function

        '-------------------------------------------------------------------------------
        '   更新SQL文発行
        '   （処理概要）Insert/Update/Delete文を発行する
        '   ●入力パラメタ  ：prmSQL        Select文
        '   ●メソッド戻り値：なし
        '                                               2014.02.20 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 更新SQL文発行 Insert/Update/Delete文を発行する
        ''' </summary>
        ''' <param name="prmSQL">SQL</param>
        ''' <remarks></remarks>
        Public Overrides Sub executeDB(ByVal prmSQL As String)
            Try
                _cmd.CommandText = prmSQL
                _cmd.ExecuteNonQuery()

            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Debug.WriteLine(ex.StackTrace)
                Throw ex
            End Try

        End Sub

        '-------------------------------------------------------------------------------
        '   更新SQL文発行
        '   （処理概要）Insert/Update/Delete文を発行する
        '   ●入力パラメタ  ：prmSQL                SQL文
        '                   ：prmRefAffectedRows    影響を受けた行数
        '   ●メソッド戻り値：なし
        '                                               2014.02.20 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 実行系SQLを発行する（影響件数引数付き）
        ''' </summary>
        ''' <param name="prmSQL">発行するSQL文</param>
        ''' <param name="prmRefAffectedRows">影響を受けた行数</param>
        ''' <remarks>レコードセットを生成しないSQL(INSERT/UPDATE/DELETE…etc)を発行する。</remarks>
        Public Overrides Sub executeDB(ByVal prmSQL As String, ByRef prmRefAffectedRows As Integer)
            Try
                _cmd.CommandText = prmSQL
                prmRefAffectedRows = _cmd.ExecuteNonQuery

            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Debug.WriteLine(ex.StackTrace)
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
        '                                               2014.02.20 Created By Laevigata, Inc.
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

                'パラメタを設定
                _cmd.Parameters.Clear()
                If prmRefParameters Is Nothing Then

                    'パラメタなし
                    If InStr(prmSQL, "?") > 0 Then
                        '置換パラメタが存在するのにパラメタリストを受領していないのでエラーとする
                        Throw New UsrDefException("置換パラメータが設定されていません。")
                    End If

                Else

                    'パラメタあり
                    Dim p As NpgsqlParameter = Nothing
                    For i As Integer = 0 To prmRefParameters.Count - 1      '受領パラメタ数分Loop
                        Dim typePrm As NpgsqlDbType = Nothing                  '型
                        Select Case prmRefParameters(i).type
                            Case UtilDBPrm.parameterType.tBoolean
                                typePrm = NpgsqlDbType.Boolean
                            Case UtilDBPrm.parameterType.tDate
                                typePrm = NpgsqlDbType.Date
                            Case UtilDBPrm.parameterType.tNumber
                                typePrm = NpgsqlDbType.Numeric
                            Case UtilDBPrm.parameterType.tVarchar
                                typePrm = NpgsqlDbType.Char
                            Case Else
                                Throw New UsrDefException("置換パラメータの設定が誤っています。")
                        End Select
                        Dim wkName As String = CStr(i)                      '名称
                        p = _cmd.Parameters.Add(wkName, typePrm)
                        p.Value = prmRefParameters(i).value                 '値
                        p.Direction = prmRefParameters(i).systemDirection   '方向
                        p.Size = prmRefParameters(i).size                   'サイズ
                        prmRefParameters(i).refParameter = p                'ポインタ(クエリ実行後の結果[パラメタ値]取得に使用)
                    Next

                End If

                'クエリ実行
                _cmd.CommandText = prmSQL
                _cmd.ExecuteNonQuery()

            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Debug.WriteLine(ex.StackTrace)
                Throw ex
            End Try

        End Sub

    End Class

End Namespace

