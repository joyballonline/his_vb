Imports System.Data.OleDb

Namespace DB
    '===============================================================================
    '
    '  ユーティリティクラス
    '    （クラス名）    UtilOleDBHandler
    '    （処理機能名）     OleDBによるDBアクセス機能を提供する
    '    （本MDL使用前提）  UtilDBInheritBase/UtilDBIfをプロジェクトに取り込んでいること
    '    （備考）           UtilDBInheritBaseを継承
    '                       UtilDBIfインターフェースを(UtilDBInheritBaseにて)実装
    '
    '===============================================================================
    '  履歴  名前          日  付      マーク      内容
    '-------------------------------------------------------------------------------
    '  (1)   Jun.Takagi    2006/04/24              新規
    '  (2)   Jun.Takagi    2006/05/23              UtilDBInheritBaseを継承元とする
    '  (3)   Jun.Takagi    2010/08/26              SystemInfoテーブルからの取得に対応
    '-------------------------------------------------------------------------------
    Public Class UtilOleDBHandler
        Inherits UtilDBInheritBase

        '===============================================================================
        'メンバー変数定義
        '===============================================================================
        Private _udlFileNm As String        'UDLファイル名(フルパス)
        Private _con As OleDbConnection     'コネクション
        Private _cmd As OleDbCommand        'コマンド
        Private _adp As OleDbDataAdapter    'アダプタ
        Private _tran As OleDbTransaction   'トランザクション
        Private _tranFlg As Boolean = False 'トランザクション中フラグ

        '===============================================================================
        'プロパティ(アクセサ)
        '===============================================================================
        Public ReadOnly Property udlFileNm() As String
            'Geter--------
            Get
                udlFileNm = _udlFileNm
            End Get
            'Setter-------
            'なし
        End Property
        Public ReadOnly Property con() As OleDbConnection
            'Geter--------
            Get
                con = _con
            End Get
            'Setter-------
            'なし
        End Property
        '2006.05.23 Updated by takagi
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
        '   （処理概要）UDLファイルよりDBコネクションを新規生成する
        '   ●入力パラメタ   ：  prmUDLFileNm        UDLファイル名(フルパス)
        '===============================================================================
        ''' <summary>
        ''' コンストラクタ UDLファイルよりDBコネクションを新規生成する
        ''' </summary>
        ''' <param name="prmUDLFileNm"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal prmUDLFileNm As String)
            Try
                Call initInstance(prmUDLFileNm)
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
        '   （処理概要）UDLファイルよりDBコネクションを新規生成する
        '   ●入力パラメタ   ：  prmUDLFileNm        UDLファイル名(フルパス)
        '===============================================================================
        ''' <summary>
        ''' サブルーチン[サブクラス用] UDLファイルよりDBコネクションを新規生成する
        ''' </summary>
        ''' <param name="prmUDLFileNm">UDLファイル名(フルパス)</param>
        ''' <remarks></remarks>
        Protected Sub initInstance(ByVal prmUDLFileNm As String)
            Try
                _udlFileNm = prmUDLFileNm
                _con = New OleDbConnection()
                _con.ConnectionString = "File Name=" & _udlFileNm
                Try
                    _con.Open()
                Catch lex As Exception
                    Throw New Exception("データベースの接続に失敗しました。" & ControlChars.NewLine & _
                                        ControlChars.Tab & "　 " & lex.Message, lex)
                End Try


                _adp = New OleDbDataAdapter()
                _cmd = New OleDbCommand()
                _cmd.Connection = _con

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
        '                                               2006.05.23 Updated By Jun.Takagi
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
        '                                               2006.05.23 Updated By Jun.Takagi
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
        '                                               2006.05.23 Updated By Jun.Takagi
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
        '                                               2006.05.23 Updated By Jun.Takagi
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
                    Dim p As OleDbParameter = Nothing
                    For i As Integer = 0 To prmParameters.Count - 1      '受領パラメタ数分Loop
                        Dim typePrm As OleDbType = Nothing               '型
                        Select Case prmParameters(i).type
                            Case UtilDBPrm.parameterType.tBoolean
                                typePrm = OleDbType.Boolean
                            Case UtilDBPrm.parameterType.tDate
                                typePrm = OleDbType.Date
                            Case UtilDBPrm.parameterType.tNumber
                                typePrm = OleDbType.Decimal
                            Case UtilDBPrm.parameterType.tVarchar
                                typePrm = OleDbType.Char
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
        '                                               2006.05.23 Updated By Jun.Takagi
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
                    Dim p As OleDbParameter = Nothing
                    For i As Integer = 0 To prmRefParameters.Count - 1      '受領パラメタ数分Loop
                        Dim typePrm As OleDbType = Nothing                  '型
                        Select Case prmRefParameters(i).type
                            Case UtilDBPrm.parameterType.tBoolean
                                typePrm = OleDbType.Boolean
                            Case UtilDBPrm.parameterType.tDate
                                typePrm = OleDbType.Date
                            Case UtilDBPrm.parameterType.tNumber
                                typePrm = OleDbType.Decimal
                            Case UtilDBPrm.parameterType.tVarchar
                                typePrm = OleDbType.Char
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

        '-------------------------------------------------------------------------------
        '   SystemInfo取得
        '   （処理概要）　SystemInfoから値を取得する
        '   ●入力パラメタ   ：prmFixKey        固定キー
        '                      prmVariableKey   可変キー
        '   ●メソッド戻り値 ：SystemInfoレコード
        '                                               2006.06.16 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' SystemInfo取得　SystemInfoから値を取得する
        ''' </summary>
        ''' <param name="prmFixKey">固定キー</param>
        ''' <param name="prmVariableKey">可変キー</param>
        ''' <returns>SystemInfoレコード</returns>
        ''' <remarks></remarks>
        Public Overrides Function getSystemInfo(ByVal prmFixKey As String, ByVal prmVariableKey As String) As UtilDBIf.sysinfoRec
            Dim rec As UtilDBIf.sysinfoRec

            Dim reccnt As Integer
            Dim sql As String = SYSTEMINFOselect
            sql = sql & "WHERE fixKey      = '" & Me.rmSQ(prmFixKey) & "' "
            sql = sql & " AND  variableKey = '" & Me.rmSQ(prmVariableKey) & "' "

            Dim ds As DataSet = Me.selectDB(sql, "RS", reccnt)
            If reccnt <= 0 Then
                Throw New UsrDefException("SystemInfoに該当レコードは存在しませんでした。")
            Else
                With ds.Tables("RS").Rows(0)
                    rec.fixKey = Me.rmNullStr(.Item("fixKey"))
                    rec.variableKey = Me.rmNullStr(.Item("variableKey"))
                    rec.stringValue1 = Me.rmNullStr(.Item("stringValue1"))
                    rec.stringValue2 = Me.rmNullStr(.Item("stringValue2"))
                    rec.stringValue3 = Me.rmNullStr(.Item("stringValue3"))
                    rec.numericValue1 = Me.rmNullStr(.Item("numericValue1"))
                    rec.numericValue2 = Me.rmNullStr(.Item("numericValue2"))
                    rec.numericValue3 = Me.rmNullStr(.Item("numericValue3"))
                    rec.dateValue1 = Me.rmNullDate(.Item("dateValue1"))
                    rec.dateValue2 = Me.rmNullDate(.Item("dateValue2"))
                    rec.dateValue3 = Me.rmNullDate(.Item("dateValue3"))
                    rec.UpdDate = Me.rmNullDate(.Item("UpdDate"))
                    rec.Memo = Me.rmNullStr(.Item("Memo"))
                End With
            End If
            Return rec
        End Function

        '-------------------------------------------------------------------------------
        '   SystemInfo取得
        '   （処理概要）　SystemInfoから値を取得する
        '   ●入力パラメタ   ：prmFixKey        固定キー
        '   ●メソッド戻り値 ：SystemInfo複数レコード
        '                                               2010.08.26 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' SystemInfo取得　SystemInfoから値を取得する
        ''' </summary>
        ''' <param name="prmFixKey">固定キー</param>
        ''' <returns>SystemInfo複数レコード</returns>
        ''' <remarks></remarks>
        Public Overrides Function getSystemInfo(ByVal prmFixKey As String) As UtilDBIf.sysinfoRec()
            Dim rec() As UtilDBIf.sysinfoRec = {}

            Dim reccnt As Integer
            Dim sql As String = SYSTEMINFOselect
            sql = sql & "WHERE fixKey = '" & Me.rmSQ(prmFixKey) & "' "
            sql = sql & "ORDER BY variableKey"

            Dim ds As DataSet = Me.selectDB(sql, "RS", reccnt)
            If reccnt <= 0 Then
                Throw New UsrDefException("SystemInfoに該当レコードは存在しませんでした。")
            Else
                With ds.Tables("RS")
                    For i As Integer = 0 To reccnt - 1
                        If i = 0 Then
                            ReDim rec(i)
                        Else
                            ReDim Preserve rec(i)
                        End If
                        rec(i).fixKey = Me.rmNullStr(.Rows(i).Item("fixKey"))
                        rec(i).variableKey = Me.rmNullStr(.Rows(i).Item("variableKey"))
                        rec(i).stringValue1 = Me.rmNullStr(.Rows(i).Item("stringValue1"))
                        rec(i).stringValue2 = Me.rmNullStr(.Rows(i).Item("stringValue2"))
                        rec(i).stringValue3 = Me.rmNullStr(.Rows(i).Item("stringValue3"))
                        rec(i).numericValue1 = Me.rmNullStr(.Rows(i).Item("numericValue1"))
                        rec(i).numericValue2 = Me.rmNullStr(.Rows(i).Item("numericValue2"))
                        rec(i).numericValue3 = Me.rmNullStr(.Rows(i).Item("numericValue3"))
                        rec(i).dateValue1 = Me.rmNullDate(.Rows(i).Item("dateValue1"))
                        rec(i).dateValue2 = Me.rmNullDate(.Rows(i).Item("dateValue2"))
                        rec(i).dateValue3 = Me.rmNullDate(.Rows(i).Item("dateValue3"))
                        rec(i).UpdDate = Me.rmNullDate(.Rows(i).Item("UpdDate"))
                        rec(i).Memo = Me.rmNullStr(.Rows(i).Item("Memo"))
                    Next
                End With
            End If
            Return rec
        End Function

        '-------------------------------------------------------------------------------
        '   SystemInfo取得
        '   （処理概要）　SystemInfoから値を取得する
        '   ●入力パラメタ   ：prmFixKey        固定キー
        '   ●メソッド戻り値 ：SystemInfo複数レコード
        '                                               2010.08.26 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' SystemInfo取得　SystemInfoから値を取得する
        ''' </summary>
        ''' <returns>SystemInfo複数レコード</returns>
        ''' <remarks></remarks>
        Public Overrides Function getSystemInfo() As UtilDBIf.sysinfoRec()
            Dim rec() As UtilDBIf.sysinfoRec = {}

            Dim reccnt As Integer
            Dim sql As String = SYSTEMINFOselect
            sql = sql & "ORDER BY fixKey,variableKey"

            Dim ds As DataSet = Me.selectDB(sql, "RS", reccnt)
            If reccnt <= 0 Then
                Throw New UsrDefException("SystemInfoに該当レコードは存在しませんでした。")
            Else
                With ds.Tables("RS")
                    For i As Integer = 0 To reccnt - 1
                        If i = 0 Then
                            ReDim rec(i)
                        Else
                            ReDim Preserve rec(i)
                        End If
                        rec(i).fixKey = Me.rmNullStr(.Rows(i).Item("fixKey"))
                        rec(i).variableKey = Me.rmNullStr(.Rows(i).Item("variableKey"))
                        rec(i).stringValue1 = Me.rmNullStr(.Rows(i).Item("stringValue1"))
                        rec(i).stringValue2 = Me.rmNullStr(.Rows(i).Item("stringValue2"))
                        rec(i).stringValue3 = Me.rmNullStr(.Rows(i).Item("stringValue3"))
                        rec(i).numericValue1 = Me.rmNullStr(.Rows(i).Item("numericValue1"))
                        rec(i).numericValue2 = Me.rmNullStr(.Rows(i).Item("numericValue2"))
                        rec(i).numericValue3 = Me.rmNullStr(.Rows(i).Item("numericValue3"))
                        rec(i).dateValue1 = Me.rmNullDate(.Rows(i).Item("dateValue1"))
                        rec(i).dateValue2 = Me.rmNullDate(.Rows(i).Item("dateValue2"))
                        rec(i).dateValue3 = Me.rmNullDate(.Rows(i).Item("dateValue3"))
                        rec(i).UpdDate = Me.rmNullDate(.Rows(i).Item("UpdDate"))
                        rec(i).Memo = Me.rmNullStr(.Rows(i).Item("Memo"))
                    Next
                End With
            End If
            Return rec
        End Function

        '-------------------------------------------------------------------------------
        '   SystemInfo取得
        '   （処理概要）　SystemInfoからFixKeyリストを取得する
        '   ●入力パラメタ   ：なし
        '   ●メソッド戻り値 ：SystemInfoレコード
        '                                               2010.08.26 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' SystemInfo取得　SystemInfoからFixKeyリストを取得する
        ''' </summary>
        ''' <returns>SystemInfoレコード</returns>
        ''' <remarks></remarks>
        Public Overrides Function getSystemInfoFixKeies() As UtilDBIf.sysinfoRec()
            Dim rec() As UtilDBIf.sysinfoRec = {}

            Dim reccnt As Integer
            Dim sql As String = "SELECT DISTINCT fixKey FROM SYSTEMINFO ORDER BY fixKey"
            Dim ds As DataSet = Me.selectDB(sql, "RS", reccnt)
            If reccnt <= 0 Then
                Throw New UsrDefException("SystemInfoに該当レコードは存在しませんでした。")
            Else
                With ds.Tables("RS")
                    For i As Integer = 0 To reccnt - 1
                        If i = 0 Then
                            ReDim rec(i)
                        Else
                            ReDim Preserve rec(i)
                        End If
                        rec(i).fixKey = Me.rmNullStr(.Rows(i).Item("fixKey"))
                        rec(i).variableKey = ""
                        rec(i).stringValue1 = ""
                        rec(i).stringValue2 = ""
                        rec(i).stringValue3 = ""
                        rec(i).numericValue1 = ""
                        rec(i).numericValue2 = ""
                        rec(i).numericValue3 = ""
                        rec(i).dateValue1 = ""
                        rec(i).dateValue2 = ""
                        rec(i).dateValue3 = ""
                        rec(i).UpdDate = ""
                        rec(i).Memo = ""
                    Next
                End With
            End If
            Return rec
        End Function

    End Class

End Namespace
