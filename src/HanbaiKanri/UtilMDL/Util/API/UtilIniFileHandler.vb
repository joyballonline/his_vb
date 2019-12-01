Imports System.Text


Namespace API
    '===============================================================================
    '
    '  ユーティリティクラス
    '    （クラス名）    UtilIniFileHandler
    '    （処理機能名）      Iniファイルの項目値を読み込む
    '    （本MDL使用前提）   特になし
    '    （備考）            API(Kernel32.GetPrivateProfileStringA)使用のため
    '                       以下に示すiniファイル形式に則る事
    '                           [セクション名]
    '                           キー名 = 設定値
    '
    '===============================================================================
    '  履歴  名前          日  付      マーク      内容
    '-------------------------------------------------------------------------------
    '  (1)   Laevigata, Inc.    2006/04/09             新規
    '-------------------------------------------------------------------------------
    Public Class UtilIniFileHandler

        '===============================================================================
        'API定義
        '===============================================================================
        <System.Security.SuppressUnmanagedCodeSecurity()>
        Private Declare Function GetPrivateProfileString Lib "KERNEL32.DLL" Alias "GetPrivateProfileStringA" (
            ByVal lpAppName As String,
            ByVal lpKeyName As String, ByVal lpDefault As String,
            ByVal lpReturnedString As StringBuilder, ByVal nSize As Integer,
            ByVal lpFileName As String) As Integer

        '===============================================================================
        'メンバー変数定義
        '===============================================================================
        Private _iniFilePath As String

        '===============================================================================
        'プロパティ(アクセサ)
        '===============================================================================
        ''' <summary>
        ''' ハンドルしているIniファイル名
        ''' </summary>
        ''' <value>Iniファイル名</value>
        ''' <returns>Iniファイル名</returns>
        ''' <remarks></remarks>
        Public Property fileName() As String
            'Geter--------
            Get
                fileName = _iniFilePath
            End Get
            'Setter-------
            Set(ByVal Value As String)
                _iniFilePath = Value
            End Set
        End Property

        '===============================================================================
        ' コンストラクタ
        '   ●入力パラメタ   ：prmFileName    フルパスIniファイル名
        '===============================================================================
        ''' <summary>
        ''' Iniファイルハンドラを生成する
        ''' </summary>
        ''' <param name="prmFileName">対象とするIniファイル名(フルパス)</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal prmFileName As String)
            _iniFilePath = prmFileName 'メンバーへIniファイル名を設定
        End Sub

        '-------------------------------------------------------------------------------
        '   項目取得
        '   （処理概要）通知されたセクション名/項目名に対応した設定値を取得する
        '   ●入力パラメタ   ：sAppName    セクション名
        '                   ：sKeyName    項目名
        '   ●メソッド戻り値 ：取得値
        '   ●発生例外       ：Exception,UsrDefException
        '                                               2006.04.09 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 項目取得
        ''' </summary>
        ''' <param name="prmAppName">セクション名</param>
        ''' <param name="prmKeyName">項目名</param>
        ''' <returns>取得値</returns>
        ''' <remarks>（処理概要）通知されたセクション名/項目名に対応した設定値を取得する</remarks>
        Public Function getIni(ByVal prmAppName As String, ByVal prmKeyName As String, Optional ByVal prmDefault As String = "") As String

            Dim sb As StringBuilder
            sb = New StringBuilder(1024)
            Const DEFAULTVALUE As String = "Initial File Read Error"
            Dim rtnCode As Integer
            Dim rtnStr As String = ""
            Dim ini As String = _iniFilePath
            If prmDefault = "" Then
                prmDefault = DEFAULTVALUE
            End If
            'APIコール
            rtnCode = GetPrivateProfileString(prmAppName, prmKeyName, prmDefault, sb, sb.Capacity, ini)
            rtnStr = sb.ToString()                                                 '読込内容取得

            If rtnCode < 1 Or rtnStr = DEFAULTVALUE Then                          '読込内容チェック
                Dim tex As UsrDefException = New UsrDefException("INIファイル読み込みエラー" & ControlChars.NewLine &
                                                     "Iniファイル・セクション・キーの存在を確認してください。")
                Debug.WriteLine(tex.Message)
                Throw tex
            Else
                Return rtnStr                                                      '読込内容返却
            End If

        End Function
    End Class
End Namespace