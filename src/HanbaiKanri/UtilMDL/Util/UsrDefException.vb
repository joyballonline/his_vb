Imports System.Windows.Forms
'===============================================================================
'
'  ユーティリティクラス
'    （クラス名）    UsrDefException
'    （処理機能名）      ユーザー定義例外
'    （本MDL使用前提）   UtilMsgVOが取り込まれていること
'    （備考）            プログラマが意図的にエラーを発生させるときにThrowする例外
'
'===============================================================================
'  履歴  名前          日  付      マーク      内容
'-------------------------------------------------------------------------------
'  (1)   Laevigata, Inc.    2006/04/18             新規
'-------------------------------------------------------------------------------
Public Class UsrDefException
    Inherits Exception

    '===============================================================================
    'メンバー変数定義
    '===============================================================================
    Private _msgVO As UtilMDL.MSG.UtilMsgVO
    Private _DisplaiedMsgFlg As Boolean
    Private _targetCtl As Control
    Private _defaultIcon As MessageBoxIcon = MessageBoxIcon.Warning
    Private _colNm As String = ""
    Private _row As Integer = -1

    '===============================================================================
    'プロパティ(アクセサ)
    '===============================================================================
    Public ReadOnly Property msgVO() As UtilMDL.MSG.UtilMsgVO
        Get
            Return _msgVO
        End Get
    End Property
    Public ReadOnly Property hasMsg() As Boolean
        Get
            Return (_msgVO IsNot Nothing)
        End Get
    End Property

    '===============================================================================
    ' コンストラクタ
    '   ●入力パラメタ   ：  prmExceptionMsg    Exceptionのメッセージ
    '                       <prmDspMessageVO>   表示用メッセージビーン
    '                       <prmException>      システム例外からユーザー定義例外に写し取る際のシステム例外
    '                       <prmErrCtl>         入力エラーコントロール
    '===============================================================================
    '①(ユーザー定義例外生成用コンストラクタ)
    ''' <summary>
    ''' ①(ユーザー定義例外生成用コンストラクタ)
    ''' </summary>
    ''' <param name="prmExceptionMsg">Exceptionに格納するエラーメッセージ</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal prmExceptionMsg As String)
        MyBase.New(prmExceptionMsg)
        _DisplaiedMsgFlg = False
        Debug.WriteLine("==========例外メッセージ==========")
        Debug.WriteLine(prmExceptionMsg)
        Debug.WriteLine("==================================")
    End Sub
    '②(ユーザー定義例外生成用コンストラクタ)
    ''' <summary>
    ''' ②(ユーザー定義例外生成用コンストラクタ)
    ''' </summary>
    ''' <param name="prmExceptionMsg">Exceptionに格納するエラーメッセージ</param>
    ''' <param name="prmDspMessageVO">ユーザー通知用メッセージのMsgVO</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal prmExceptionMsg As String, ByVal prmDspMessageVO As UtilMDL.MSG.UtilMsgVO)
        Call Me.New(prmExceptionMsg)                        '①に処理委託
        _msgVO = prmDspMessageVO
    End Sub
    '③(システム例外からのクローン生成用コンストラクタ)
    ''' <summary>
    ''' ③(システム例外からのクローン生成用コンストラクタ)　Catchされたシステム例外からUsrDefExceptionへの詰め替えを想定
    ''' </summary>
    ''' <param name="prmException">発生したシステム例外</param>
    ''' <param name="prmDspMessageVO">ユーザー通知用メッセージのMsgVO</param>
    ''' <param name="prmSilentMode">メッセージを出すか出さないかを指定</param>
    ''' <param name="prmOutLogFile">MSGを出さない場合に代わりに出力されるログファイル名を指定</param>
    ''' <remarks>システム例外がCathされることを想定している為、インスタンス化した時点でエラーMsgを出力する</remarks>
    Public Sub New(ByVal prmException As Exception,
                   ByVal prmDspMessageVO As UtilMDL.MSG.UtilMsgVO,
                   Optional ByVal prmSilentMode As Boolean = False,
                   Optional ByVal prmOutLogFile As String = "")
        Call Me.New(prmException.Message, prmDspMessageVO)  '②に処理委託
        Debug.WriteLine(prmException.StackTrace)
        _defaultIcon = MessageBoxIcon.Error
        Call Me.dspMsg(prmSilentMode, prmOutLogFile)                                    'システム例外は直ちにエラーMSGの表示を行う
    End Sub
    '④(ユーザー定義例外生成用コンストラクタ)
    ''' <summary>
    ''' ④(ユーザー定義例外生成用コンストラクタ)　入力チェック時などのThrowを想定
    ''' </summary>
    ''' <param name="prmExceptionMsg">Exceptionに格納するエラーメッセージ</param>
    ''' <param name="prmDspMessageVO">ユーザー通知用メッセージのMsgVO</param>
    ''' <param name="prmErrCtl">フォーカスを設定するコントロール</param>
    ''' <remarks>2006.11.06 Updated By Laevigata, Inc.</remarks>
    Public Sub New(ByVal prmExceptionMsg As String, ByVal prmDspMessageVO As UtilMDL.MSG.UtilMsgVO, ByVal prmErrCtl As Control)
        Call Me.New(prmExceptionMsg, prmDspMessageVO)       '②に処理委託
        _targetCtl = prmErrCtl
        _colNm = ""
        _row = -1
    End Sub
    '⑤(ユーザー定義例外生成用コンストラクタ)
    ''' <summary>
    ''' ⑤(ユーザー定義例外生成用コンストラクタ)　DataGridView入力チェック時などのThrowを想定
    ''' </summary>
    ''' <param name="prmExceptionMsg">Exceptionに格納するエラーメッセージ</param>
    ''' <param name="prmDspMessageVO">ユーザー通知用メッセージのMsgVO</param>
    ''' <param name="prmErrDgv">フォーカスを設定するDataGridView</param>
    ''' <param name="prmColName">選択させたいセルのグリッド上の列名</param>
    ''' <param name="prmRow">選択させたいセルの行番号</param>
    ''' <remarks>2006.11.06 Created By Laevigata, Inc.</remarks>
    Public Sub New(ByVal prmExceptionMsg As String,
                       ByVal prmDspMessageVO As UtilMDL.MSG.UtilMsgVO,
                       ByVal prmErrDgv As Windows.Forms.DataGridView,
                       ByVal prmColName As String,
                       ByVal prmRow As Integer)
        Call Me.New(prmExceptionMsg, prmDspMessageVO)       '②に処理委託
        _targetCtl = CType(prmErrDgv, Control)
        _colNm = prmColName
        _row = prmRow
    End Sub
    '-------------------------------------------------------------------------------
    '   メッセージ表示
    '   （処理概要）格納済みのMSGを表示(同一のExceptionに対しては一度しかMSGを表示しない)し、
    '               エラー対象コントロールが存在した場合はフォーカスの位置づけも行う
    '   ●入力パラメタ   ：prmSilentMode    メッセージを出すか出さないかを指定
    '                    ：prmOutLogFile    MSGを出さない場合に代わりに出力されるログファイル名を指定
    '   ●メソッド戻り値 ：押下ボタン(DialogResult)
    '   ●発生例外       ：なし
    '                                               2006.11.06 Updated By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' メッセージ表示 格納済みのMSGを表示(同一のExceptionに対しては一度しかMSGを表示しない)し、エラー対象コントロールが存在した場合はフォーカスの位置づけも行う
    ''' </summary>
    ''' <param name="prmSilentMode">メッセージを出すか出さないかを指定</param>
    ''' <param name="prmOutLogFile">MSGを出さない場合に代わりに出力されるログファイル名を指定</param>
    ''' <returns>押下ボタン(DialogResult)</returns>
    ''' <remarks></remarks>
    Public Function dspMsg(Optional ByVal prmSilentMode As Boolean = False, Optional ByVal prmOutLogFile As String = "") As DialogResult

        'メッセージ表示を行う
        Dim ret As DialogResult
        If _DisplaiedMsgFlg Then
            'このExceptionのエラーは表示済みなので表示しない
            ret = DialogResult.OK
        Else
            If Not Me.hasMsg Then
                'Messageが存在しない場合はExceptionMessageを表示する
                If Not prmSilentMode Then
                    ret = MessageBox.Show(MyBase.Message, "エラー", MessageBoxButtons.OK, _defaultIcon, MessageBoxDefaultButton.Button1)
                Else
                    'サイレントモードの際はMSGを出力せずにLOGに出力する
                    'LogFileName生成
                    Dim tmpLogFile As String
                    If Not "".Equals(prmOutLogFile) Then
                        tmpLogFile = prmOutLogFile
                    Else
                        '出力ログファイル名が指定されていないので自力で生成
                        tmpLogFile = System.IO.Path.GetDirectoryName( _
                                    System.Reflection.Assembly.GetExecutingAssembly().Location _
                                ) & "\" & _
                                System.Reflection.Assembly.GetExecutingAssembly.GetName().Name & "_" & _
                                Now.ToString("yyyyMMdd") & ".log"
                    End If

                    'ロガー生成
                    Dim logger As UtilMDL.Log.UtilLogWriter = New UtilMDL.Log.UtilLogWriter(tmpLogFile)

                    'ログ出力
                    logger.writeLine(MyBase.Message)
                End If
            Else
                '既に生成されている_msgVO.dspStrを表示
                If Not prmSilentMode Then
                    ret = MessageBox.Show(_msgVO.dspStr, _msgVO.title, _msgVO.button, _msgVO.icon, _msgVO.defaultButton)
                Else
                    'サイレントモードの際はMSGを出力せずにLOGに出力する
                    'LogFileName生成
                    Dim tmpLogFile As String
                    If Not "".Equals(prmOutLogFile) Then
                        tmpLogFile = prmOutLogFile
                    Else
                        '出力ログファイル名が指定されていないので自力で生成
                        tmpLogFile = System.IO.Path.GetDirectoryName( _
                                    System.Reflection.Assembly.GetExecutingAssembly().Location _
                                ) & "\" & _
                                System.Reflection.Assembly.GetExecutingAssembly.GetName().Name & "_" & _
                                Now.ToString("yyyyMMdd") & ".log"
                    End If

                    'ロガー生成
                    Dim logger As UtilMDL.Log.UtilLogWriter = New UtilMDL.Log.UtilLogWriter(tmpLogFile)

                    'ログ出力
                    logger.writeLine(_msgVO.dspStr)
                End If

            End If
        End If
        _DisplaiedMsgFlg = True 'MSGを表示したのでフラグを倒す
        _msgVO = Nothing

        'エラーコントロールが存在する場合はフォーカスを位置付ける
        If _targetCtl IsNot Nothing Then
            Dim flg As Boolean = _targetCtl.Enabled
            _targetCtl.Enabled = True   '使用不可に備えて一旦使用可にする
            _targetCtl.Focus()          'フォーカス設定
            If (Not "".Equals(_colNm) And _row <> -1) Then
                'データグリッドビューなので行列も設定する
                CType(_targetCtl, Windows.Forms.DataGridView).CurrentCell _
                  = CType(_targetCtl, Windows.Forms.DataGridView).Rows(_row).Cells(_colNm)
            End If
            _targetCtl.Enabled = flg    '使用不可だった場合は次のコントロールへフォーカスは移動する
        End If
        Return ret

    End Function

End Class
