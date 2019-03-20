Imports System.Globalization
Imports System.Text.RegularExpressions
Imports System.Windows.Forms
'===============================================================================
'
'  ユーティリティクラス
'    （クラス名）    UtilClass
'    （処理機能名）      ユーティリティメソッド群
'    （本MDL使用前提）   特になし
'    （備考）            メソッド単位での移植を可能とするため、Imports宣言を
'                        行わず、完全修飾名前空間を使用のこと
'
'===============================================================================
'  履歴  名前          日  付      マーク      内容
'-------------------------------------------------------------------------------
'  (1)   Laevigata, Inc.    2006/05/01              新規
'  (2)   Laevigata, Inc.    2010/08/26              エラーメッセージ取得(getErrDetail)に発生時刻追加
'-------------------------------------------------------------------------------
Public Class UtilClass
    Public Shared Sub main()
    End Sub

    '-------------------------------------------------------------------------------
    'コンストラクタ
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub New() 'インスタンス化を抑制
        '処理無し
    End Sub

    '-------------------------------------------------------------------------------
    '   アプリケーション実行パスを取得
    '   （処理概要）アプリケーション実行パスを返却する
    '   ●入力パラメタ：prmAssembly  アセンブリ
    '   ●メソッド戻り値　：取得アプリケーション実行パス
    '                                               2006.05.01 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' アプリケーション実行パスを取得
    ''' </summary>
    ''' <param name="prmAssembly">アセンブリ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getAppPath(ByVal prmAssembly As System.Reflection.Assembly) As String
        Return System.IO.Path.GetDirectoryName(prmAssembly.Location)
    End Function

    '-------------------------------------------------------------------------------
    '   アプリケーション名称を取得
    '   （処理概要）アプリケーション名称を返却する
    '   ●入力パラメタ：prmAssembly  アセンブリ
    '   ●メソッド戻り値　：取得アプリケーション名称
    '                                               2006.05.01 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' アプリケーション名称を取得
    ''' </summary>
    ''' <param name="prmAssembly">アセンブリ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getAppName(ByVal prmAssembly As System.Reflection.Assembly) As String
        Return prmAssembly.GetName().Name
    End Function

    '-------------------------------------------------------------------------------
    '   アプリケーションのVersionを取得
    '   （処理概要）プロジェクトのプロパティのアプリケーションタブ-アセンブリ情報ボタンから
    '   　　　　　　起動されるアセンブリのファイルバージョンを返却する
    '   ●入力パラメタ：prmAssembly  アセンブリ
    '   ●メソッド戻り値　：取得Version
    '                                               2006.05.22 Updated By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' アプリケーションのVersionを取得
    ''' </summary>
    ''' <param name="prmAssembly">アセンブリ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getAppVersion(ByVal prmAssembly As System.Reflection.Assembly) As String

        'アセンブリのバージョン情報を取得する
        Dim v As System.Diagnostics.FileVersionInfo
        v = (System.Diagnostics.FileVersionInfo.GetVersionInfo(prmAssembly.Location))
        '-->2006.05.22 chg start by Laevigata, Inc.
        'Return v.ProductMajorPart & "." & v.ProductMinorPart & "." & v.ProductBuildPart & "." & v.ProductPrivatePart
        'Return v.ProductMajorPart & "." & v.ProductMinorPart & "." & String.Format("{0:00}", v.ProductPrivatePart)
        Return v.ProductMajorPart & "." & v.ProductMinorPart & "." & v.ProductBuildPart & "." & v.ProductPrivatePart
        '<--2006.05.22 chg end by Laevigata, Inc.

    End Function

    '-------------------------------------------------------------------------------
    '　 ファイル存在チェック
    '   （処理概要）引数のファイルが存在するかどうかを判定
    '   ●入力パラメタ：prmDir  判定ファイルフルパス文字列
    '   ●メソッド戻り値　：True/False
    '                                               2006.05.01 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' ファイル存在チェック 引数のファイルが存在するかどうかを判定
    ''' </summary>
    ''' <param name="prmFile">判定ファイルフルパス文字列</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Public Shared Function isFileExists(ByVal prmFile As String) As Boolean
        Return System.IO.File.Exists(prmFile)
    End Function

    '-------------------------------------------------------------------------------
    '　 フォルダ存在チェック
    '   （処理概要）引数のディレクトリが存在するかどうかを判定
    '   ●入力パラメタ：prmDir  判定ディレクトリ文字列
    '   ●メソッド戻り値　：True/False
    '                                               2006.05.01 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' フォルダ存在チェック 引数のディレクトリが存在するかどうかを判定
    ''' </summary>
    ''' <param name="prmDir">判定ディレクトリ文字列</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Public Shared Function isDirExists(ByVal prmDir As String) As Boolean
        Return System.IO.Directory.Exists(prmDir)
    End Function

    '-------------------------------------------------------------------------------
    '　 エラーメッセージ取得
    '   （処理概要）Exceptionの詳細メッセージを取得する
    '   ●入力パラメタ：prmException  メッセージを取得する例外
    '   ●メソッド戻り値　：編集済みエラーメッセージ
    '                                               20010.08.26 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' エラーメッセージ取得 Exceptionの詳細メッセージを取得する
    ''' </summary>
    ''' <param name="prmException">メッセージを取得する例外</param>
    ''' <returns>編集済みエラーメッセージ</returns>
    ''' <remarks></remarks>
    Public Shared Function getErrDetail(ByVal prmException As Exception) As String
        Dim wkSorce As String = prmException.TargetSite.DeclaringType.FullName
        wkSorce = wkSorce.Replace(prmException.Source & ".", "")
        '-->2010.08.26 upd by Laevigata, Inc. #発生時刻追加
        'Return "メッセージ" & ControlChars.Tab & "： " & prmException.Message & ControlChars.NewLine & _
        '       "発生元" & ControlChars.Tab & "： " & prmException.Source & ControlChars.NewLine & _
        '       "発生箇所" & ControlChars.Tab & "： " & wkSorce & " [ " & prmException.TargetSite.ToString & " ]"
        Return "メッセージ" & ControlChars.Tab & "： " & prmException.Message & ControlChars.NewLine &
               "発生元" & ControlChars.Tab & "： " & prmException.Source & ControlChars.NewLine &
               "発生箇所" & ControlChars.Tab & "： " & wkSorce & " [ " & prmException.TargetSite.ToString & " ]" & ControlChars.NewLine &
               "発生時刻" & ControlChars.Tab & "： " & Now.ToString("G")
        '<--2010.08.26 upd by Laevigata, Inc. #発生時刻追加
    End Function

    '-------------------------------------------------------------------------------
    '　 フォーカス遷移
    '   （処理概要）次のコントロールへフォーカス移動を行う
    '   ●入力パラメタ：prmForm    フォーカス制御を行うフォーム
    '                   prmEvent   KeyPressイベント
    '   ●メソッド戻り値　：なし
    '                                               2006.05.10 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' フォーカス遷移 次のコントロールへフォーカス移動を行う
    ''' </summary>
    ''' <param name="prmForm">フォーカス制御を行うフォーム</param>
    ''' <param name="prmEvent">KeyPressイベント</param>
    ''' <remarks></remarks>
    Public Shared Sub moveNextFocus(ByVal prmForm As Form, ByVal prmEvent As System.Windows.Forms.KeyPressEventArgs)
        Try
            '押下キーがEnterの場合、次のコントロールへフォーカス移動
            If prmEvent.KeyChar = Chr(Keys.Enter) Then
                prmForm.SelectNextControl(prmForm.ActiveControl, True, True, True, True)
                prmEvent.Handled = True 'キー押下に関する処理が終了したことを.NET Frameworkに通知(Beepさせない)
            End If

        Catch ex As Exception
            Debug.WriteLine("moveNextFocusでエラーが発生しました。：" & ex.Message)
            Debug.WriteLine(ex.StackTrace)
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　 文字列データ長取得(全角・半角対応)
    '   （処理概要）渡された文字列の長さを求める(全角1文字=2，半角1文字=1で計算)
    '   ●入力パラメタ：sPrmStr 対象文字列
    '   ●メソッド戻り値　：文字列データ長(バイト単位)
    '                                               2006.05.10 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' 文字列データ長取得(全角・半角対応) 渡された文字列の長さを求める(全角1文字=2，半角1文字=1で計算)
    ''' </summary>
    ''' <param name="prmStr">対象文字列</param>
    ''' <returns>文字列データ長(バイト単位)</returns>
    ''' <remarks></remarks>
    Public Shared Function getLenB(ByVal prmStr As String) As Short

        'Shift JISに変換したときに必要なバイト数を返す
        Return System.Text.Encoding.GetEncoding("shift_jis").GetByteCount(prmStr)

    End Function

    '-------------------------------------------------------------------------------
    '　 データ長取得(全角・半角対応)
    '   （処理概要）文字列を指定された長さに編集する
    '               文字列＞データ長 ： 超過分切り捨て
    '               文字列＜データ長 ： 不足分スペース詰め
    '   ●入力パラメタ：prmStr(対象文字列)
    '                 ：prmLen(指定データ長 … バイト単位)
    '   ●メソッド戻り値　：編集文字列
    '                                               2006.05.10 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' データ長取得(全角・半角対応) 文字列を指定された長さに編集する. 文字列＞データ長 ： 超過分切り捨て.  文字列＜データ長 ： 不足分スペース詰め.
    ''' </summary>
    ''' <param name="prmStr">対象文字列</param>
    ''' <param name="prmLen">指定データ長 … バイト単位</param>
    ''' <returns>編集文字列</returns>
    ''' <remarks></remarks>
    Public Shared Function setDataLen(ByVal prmStr As String, ByVal prmLen As Integer) As String
        Dim ret As String
        Const encodType As String = "shift_jis"
        If System.Text.Encoding.GetEncoding(encodType).GetByteCount(prmStr) > prmLen Then
            Dim wkStr As String = prmStr
            While System.Text.Encoding.GetEncoding(encodType).GetByteCount(wkStr) > prmLen
                wkStr = wkStr.Substring(0, wkStr.Length - 1)
            End While
            If System.Text.Encoding.GetEncoding(encodType).GetByteCount(wkStr) < prmLen Then
                wkStr = wkStr & Space(1)
            End If
            ret = wkStr
        Else
            ret = prmStr & Space(prmLen - System.Text.Encoding.GetEncoding(encodType).GetByteCount(prmStr))
        End If
        Return ret
    End Function

    '-------------------------------------------------------------------------------
    '　 全角・半角混在チェック
    '   （処理概要）文字列中に半角・全角が混在しているかどうかを判定
    '   ●入力パラメタ：prmStr(対象文字列）
    '   ●メソッド戻り値　：TRUE(全角半角混在あり）／FALSE(半角全角混在なし)
    '                                               2006.05.10 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' 全角・半角混在チェック 文字列中に半角・全角が混在しているかどうかを判定
    ''' </summary>
    ''' <param name="prmStr">対象文字列</param>
    ''' <returns>TRUE(全角半角混在あり）／FALSE(半角全角混在なし)</returns>
    ''' <remarks></remarks>
    Public Shared Function isSharedNWStr(ByVal prmStr As String) As Boolean
        Dim wCnt As Short
        Dim nCnt As Short
        For i As Integer = 0 To prmStr.Length - 1
            If System.Text.Encoding.GetEncoding("shift_jis").GetByteCount(prmStr.Substring(i, 1)) > 1 Then
                wCnt = wCnt + 1
            Else
                nCnt = nCnt + 1
            End If
        Next
        Dim ret As Boolean
        If wCnt <> 0 And nCnt <> 0 Then
            ret = True
        Else
            ret = False
        End If
        Return ret
    End Function

    '-------------------------------------------------------------------------------
    '　 半角のみチェック
    '   （処理概要）文字列中に全角が混在しているかどうかを判定
    '   ●入力パラメタ：prmStr(対象文字列）
    '   ●メソッド戻り値　：TRUE(半角のみ）／FALSE(全角あり)
    '                                               2006.05.10 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' 半角のみチェック 文字列中に全角が混在しているかどうかを判定
    ''' </summary>
    ''' <param name="prmStr">対象文字列</param>
    ''' <returns>TRUE(半角のみ）／FALSE(全角あり)</returns>
    ''' <remarks></remarks>
    Public Shared Function isOnlyNStr(ByVal prmStr As String) As Boolean
        Dim wCnt As Short
        Dim nCnt As Short
        For i As Integer = 0 To prmStr.Length - 1
            If System.Text.Encoding.GetEncoding("shift_jis").GetByteCount(prmStr.Substring(i, 1)) > 1 Then
                wCnt = wCnt + 1
            Else
                nCnt = nCnt + 1
            End If
        Next
        Dim ret As Boolean
        If wCnt <> 0 Then
            ret = False
        Else
            ret = True
        End If
        Return ret
    End Function

    '-------------------------------------------------------------------------------
    '　ディレクトリ/ファイル名分割
    '   （処理概要）フルパスのファイル名をディレクトリ&ファイル名に分割する
    '   ●入力パラメタ：i prmFullPath フルパス
    '                 ：o prmPath     ディレクトリ
    '                 ：o prmFile     ファイル名
    '                                               2006.05.10 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' ディレクトリ・ファイル名分割 フルパスのファイル名をディレクトリ・ファイル名に分割する
    ''' </summary>
    ''' <param name="prmFullPath">フルパス</param>
    ''' <param name="prmRefPath">ディレクトリ</param>
    ''' <param name="prmRefFile">ファイル名</param>
    ''' <remarks></remarks>
    Public Shared Sub dividePathAndFile(ByVal prmFullPath As String, ByRef prmRefPath As String, ByRef prmRefFile As String)
        Dim devPos As Integer
        devPos = InStrRev(prmFullPath.Replace("/", "\"), "\")

        If devPos <= 0 Then
            prmRefFile = prmFullPath
        Else
            prmRefFile = prmFullPath.Substring(devPos)
            prmRefPath = prmFullPath.Substring(0, devPos - 1)
        End If
    End Sub

    '-------------------------------------------------------------------------------
    '　切捨て
    '   （処理概要）入力パラメタの数値を切り捨てして返却
    '   ●入力パラメタ：i num パラメタ
    '   ●メソッド戻り値　：処理値
    '                                               2006.05.10 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' 切捨て　入力パラメタの数値を切り捨てして返却
    ''' </summary>
    ''' <param name="prmNum">パラメタ</param>
    ''' <returns>処理値</returns>
    ''' <remarks></remarks>
    Public Shared Function roundDown(ByVal prmNum As Double) As Integer
        Return Fix(prmNum)
    End Function

    '-------------------------------------------------------------------------------
    '　切捨て
    '   （処理概要）入力パラメタの数値を切り捨てして返却
    '   ●入力パラメタ：i prmNum    パラメタ
    '               　：i prmDigit  実行桁
    '   ●メソッド戻り値　：処理値
    '   ●備考      　：0.15を少数第２位で切捨ての場合、prmDigitは 2 で実行
    '  　　　       　：1520を百の位で切捨ての場合、prmDigitは -3 で実行
    '                                               2006.07.10 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' 切捨て　入力パラメタの数値を切捨てして返却
    ''' </summary>
    ''' <param name="prmNum">パラメタ</param>
    ''' <param name="prmDigit">実行桁(少数第１位で切捨ての場合[1]、一の位の場合[-1])</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function roundDown(ByVal prmNum As Double, ByVal prmDigit As Short) As Double
        If prmDigit = 0 Then
            Throw New UsrDefException("prmDigitパラメタが不正です。0以外の値を設定してください。")
        End If

        Dim wkDigit As Short
        If prmDigit > 0 Then
            '少数で実行
            wkDigit = prmDigit
        Else
            '整数で実行
            wkDigit = prmDigit * (-1)
            prmNum = Fix(prmNum)
        End If
        Dim multiple As Double = 1
        For i As Integer = wkDigit - 1 To 1 Step -1
            multiple = multiple * 10
        Next

        Dim ret As Double = 0
        If prmDigit > 0 Then
            '少数で実行
            prmNum = prmNum * multiple
            ret = (Fix(prmNum)) / multiple
        Else
            '整数で実行
            prmNum = prmNum / (multiple * 10)
            ret = (Fix(prmNum)) * (multiple * 10)
        End If
        Return ret
    End Function

    '-------------------------------------------------------------------------------
    '　切り上げ
    '   （処理概要）入力パラメタの数値を切り上げして返却
    '   ●入力パラメタ：i prmNum パラメタ
    '   ●メソッド戻り値　：処理値
    '                                               2006.05.10 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' 切り上げ　入力パラメタの数値を切り上げして返却
    ''' </summary>
    ''' <param name="prmNum">パラメタ</param>
    ''' <returns>処理値</returns>
    ''' <remarks></remarks>
    Public Shared Function roundUp(ByVal prmNum As Double) As Integer
        Return Int(System.Math.Abs(prmNum) * -1) * (Math.Sign(prmNum) * -1)
    End Function

    '-------------------------------------------------------------------------------
    '　切り上げ
    '   （処理概要）入力パラメタの数値を切り上げして返却
    '   ●入力パラメタ：i prmNum    パラメタ
    '               　：i prmDigit  実行桁
    '   ●メソッド戻り値　：処理値
    '   ●備考      　：0.15を少数第２位で切り上げの場合、prmDigitは 2 で実行
    '  　　　       　：1520を百の位で切り上げの場合、prmDigitは -3 で実行
    '                                               2006.07.10 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' 切り上げ　入力パラメタの数値を切り上げして返却
    ''' </summary>
    ''' <param name="prmNum">パラメタ</param>
    ''' <param name="prmDigit">実行桁(少数第１位で切り上げの場合[1]、一の位の場合[-1])</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function roundUp(ByVal prmNum As Double, ByVal prmDigit As Short) As Double
        If prmDigit = 0 Then
            Throw New UsrDefException("prmDigitパラメタが不正です。0以外の値を設定してください。")
        End If

        Dim wkDigit As Short
        If prmDigit > 0 Then
            '少数で実行
            wkDigit = prmDigit
        Else
            '整数で実行
            wkDigit = prmDigit * (-1)
            prmNum = Fix(prmNum)
        End If
        Dim multiple As Double = 1
        For i As Integer = wkDigit - 1 To 1 Step -1
            multiple = multiple * 10
        Next

        Dim ret As Double = 0
        If prmDigit > 0 Then
            '少数で実行
            prmNum = prmNum * multiple
            ret = (Int(System.Math.Abs(prmNum) * -1) * (Math.Sign(prmNum) * -1)) / multiple
        Else
            '整数で実行
            prmNum = prmNum / (multiple * 10)
            ret = (Int(System.Math.Abs(prmNum) * -1) * (Math.Sign(prmNum) * -1)) * (multiple * 10)
        End If
        Return ret
    End Function

    '-------------------------------------------------------------------------------
    '　四捨五入
    '   （処理概要）入力パラメタの数値を四捨五入して返却
    '   ●入力パラメタ：i num パラメタ
    '   ●メソッド戻り値　：処理値
    '                                               2006.05.10 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' 四捨五入　入力パラメタの数値を四捨五入して返却
    ''' </summary>
    ''' <param name="prmNum">パラメタ</param>
    ''' <returns>処理値</returns>
    ''' <remarks></remarks>
    Public Shared Function roundOff(ByVal prmNum As Double) As Integer
        Return Fix(prmNum + (0.5 * Math.Sign(prmNum)))
    End Function

    '-------------------------------------------------------------------------------
    '　四捨五入
    '   （処理概要）入力パラメタの数値を指定桁で四捨五入して返却
    '   ●入力パラメタ：i prmNum    パラメタ
    '               　：i prmDigit  実行桁
    '   ●メソッド戻り値　：処理値
    '   ●備考      　：0.15を少数第２位で四捨五入の場合、prmDigitは 2 で実行
    '  　　　       　：1520を百の位で四捨五入の場合、prmDigitは -3 で実行
    '                                               2006.07.10 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' 四捨五入　入力パラメタの数値を四捨五入して返却
    ''' </summary>
    ''' <param name="prmNum">パラメタ</param>
    ''' <param name="prmDigit">実行桁(少数第１位で四捨五入の場合[1]、一の位の場合[-1])</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function roundOff(ByVal prmNum As Double, ByVal prmDigit As Short) As Double
        If prmDigit = 0 Then
            Throw New UsrDefException("prmDigitパラメタが不正です。0以外の値を設定してください。")
        End If

        Dim wkDigit As Short
        If prmDigit > 0 Then
            '少数で実行
            wkDigit = prmDigit
        Else
            '整数で実行
            wkDigit = prmDigit * (-1)
            prmNum = Fix(prmNum)
        End If
        Dim multiple As Double = 1
        For i As Integer = wkDigit - 1 To 1 Step -1
            multiple = multiple * 10
        Next

        Dim ret As Double = 0
        If prmDigit > 0 Then
            '少数で実行
            prmNum = prmNum * multiple
            ret = Fix((prmNum + (0.5 * Math.Sign(prmNum)))) / multiple
        Else
            '整数で実行
            prmNum = prmNum / (multiple * 10)
            ret = Fix((prmNum + (0.5 * Math.Sign(prmNum)))) * (multiple * 10)
        End If
        Return ret
    End Function

    '-------------------------------------------------------------------------------
    '　コントロール全選択状態生成
    '   （処理概要）入力パラメタのコントロールを全選択状態とする
    '   ●入力パラメタ：i prmObj 対象コントロール(TextBox,MskedTextBoxを想定)
    '   ●メソッド戻り値　：なし
    '   ●使用例　　　：     Private Sub Text1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Text1.GotFocus
    '                            Call UtilClass.selAll(Text1)
    '                        End Sub
    '                                               2006.06.09 Updated By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' コントロール全選択状態生成　入力パラメタのコントロールを全選択状態とする
    ''' </summary>
    ''' <param name="prmRefObj">対象コントロール(TextBox,MskedTextBoxを想定)</param>
    ''' <remarks></remarks>
    Public Shared Sub selAll(ByRef prmRefObj As Object)
        Try

            'テキストボックスへ変換
            Dim wkText As TextBox = CType(prmRefObj, TextBox)
            'wkText.SelectionStart = 0
            'wkText.SelectionLength = wkText.Text.Length
            wkText.SelectAll()
        Catch ex As Exception
            Try
                'マスクドテキストボックスへ置換
                Dim wkMaskedText As MaskedTextBox = CType(prmRefObj, MaskedTextBox)
                'wkMaskedText.SelectionStart = 0
                'wkMaskedText.SelectionLength = wkMaskedText.Text.Length
                wkMaskedText.SelectAll()
            Catch ex2 As Exception
                'TextBoxでもMaskedTextBoxでもない場合何もしない
            End Try
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '  端末名取得
    '   （処理概要）実行端末のコンピュータ名を取得する
    '   ●入力パラメタ：なし
    '   ●メソッド戻り値　：端末名
    '                                               2006.05.19 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' 実行端末のコンピュータ名を取得する
    ''' </summary>
    ''' <returns>端末名</returns>
    ''' <remarks></remarks>
    Public Shared Function getComputerName() As String
        Return System.Net.Dns.GetHostName
    End Function

    '-------------------------------------------------------------------------------
    '  IP取得
    '   （処理概要）実行端末のIPを取得する
    '   ●入力パラメタ：なし
    '   ●メソッド戻り値　：IP
    '                                               2006.05.19 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' 実行端末のIPを取得する
    ''' </summary>
    ''' <returns>IP</returns>
    ''' <remarks></remarks>
    Public Shared Function getComputerIP() As String
        Dim cn As String = System.Net.Dns.GetHostName
        Dim ipInfo As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(cn)
        Dim ipInfoAddress As System.Net.IPAddress = ipInfo.AddressList(0)
        Return ipInfoAddress.ToString
    End Function

    '-------------------------------------------------------------------------------
    '  端末名取得(IPから取得)
    '   （処理概要）IPから端末名を取得する
    '   ●入力パラメタ：prmIP   IP
    '   ●メソッド戻り値　：端末名
    '   ●備考　　　　：DNSが逆引きをサポートしていること。 ※NSL-LANはサポート外
    '                                               2006.05.19 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' IPから端末名を取得する
    ''' </summary>
    ''' <param name="prmIP">IP</param>
    ''' <returns>端末名</returns>
    ''' <remarks>DNSが逆引きをサポートしていること。</remarks>
    Public Shared Function getComputerNameFromIP(ByVal prmIP As String) As String
        Dim hostInfo As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(prmIP)
        Return hostInfo.HostName
    End Function

    '-------------------------------------------------------------------------------
    '  IP取得(端末名から取得)
    '   （処理概要）端末名からIPを取得する
    '   ●入力パラメタ：prmComputerName   端末名
    '   ●メソッド戻り値　：IP
    '                                               2006.05.19 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' 端末名からIPを取得する
    ''' </summary>
    ''' <param name="prmComputerName">端末名</param>
    ''' <returns>IP</returns>
    ''' <remarks></remarks>
    Public Shared Function getComputerIPFromName(ByVal prmComputerName As String) As String
        Dim ipInfo As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(prmComputerName)
        Dim ipInfoAddress As System.Net.IPAddress = ipInfo.AddressList(0)
        Return ipInfoAddress.ToString
    End Function

    '-------------------------------------------------------------------------------
    '  Boolean型を取得する
    '   （処理概要）BooleanのTypeを返却する
    '   ●入力パラメタ　　：なし
    '   ●メソッド戻り値　：取得Type
    '                                               2006.05.30 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' BooleanのTypeを返却する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getBln() As Type
        Return Type.GetType("System.Boolean")
    End Function

    '-------------------------------------------------------------------------------
    '  String型を取得する
    '   （処理概要）StringのTypeを返却する
    '   ●入力パラメタ　　：なし
    '   ●メソッド戻り値　：取得Type
    '                                               2006.05.30 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' StringのTypeを返却する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getStr() As Type
        Return Type.GetType("System.String")
    End Function

    '-------------------------------------------------------------------------------
    '  Short型を取得する
    '   （処理概要）ShortのTypeを返却する
    '   ●入力パラメタ　　：なし
    '   ●メソッド戻り値　：取得Type
    '                                               2006.05.30 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' ShortのTypeを返却する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getSho() As Type
        Return Type.GetType("System.Int16")
    End Function

    '-------------------------------------------------------------------------------
    '  Integer型を取得する
    '   （処理概要）IntegerのTypeを返却する
    '   ●入力パラメタ　　：なし
    '   ●メソッド戻り値　：取得Type
    '                                               2006.05.30 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' IntegerのTypeを返却する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getInt() As Type
        Return Type.GetType("System.Int32")
    End Function

    '-------------------------------------------------------------------------------
    '  Long型を取得する
    '   （処理概要）LongのTypeを返却する
    '   ●入力パラメタ　　：なし
    '   ●メソッド戻り値　：取得Type
    '                                               2006.05.30 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' LongのTypeを返却する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getLng() As Type
        Return Type.GetType("System.Int64")
    End Function

    '-------------------------------------------------------------------------------
    '  Single型を取得する
    '   （処理概要）SingleのTypeを返却する
    '   ●入力パラメタ　　：なし
    '   ●メソッド戻り値　：取得Type
    '                                               2006.05.30 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' SingleのTypeを返却する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getSgl() As Type
        Return Type.GetType("System.Single")
    End Function

    '-------------------------------------------------------------------------------
    '  Double型を取得する
    '   （処理概要）DoubleのTypeを返却する
    '   ●入力パラメタ　　：なし
    '   ●メソッド戻り値　：取得Type
    '                                               2006.05.30 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' DoubleのTypeを返却する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getDbl() As Type
        Return Type.GetType("System.Double")
    End Function

    '-------------------------------------------------------------------------------
    '  DateTime型を取得する
    '   （処理概要）DateTimeのTypeを返却する
    '   ●入力パラメタ　　：なし
    '   ●メソッド戻り値　：取得Type
    '                                               2006.05.30 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' DateTimeのTypeを返却する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getDte() As Type
        Return Type.GetType("System.DateTime")
    End Function

    '-------------------------------------------------------------------------------
    '  Object型を取得する
    '   （処理概要）ObjectのTypeを返却する
    '   ●入力パラメタ　　：なし
    '   ●メソッド戻り値　：取得Type
    '                                               2006.05.30 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' ObjectのTypeを返却する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getObj() As Type
        Return Type.GetType("System.Object")
    End Function

    '-------------------------------------------------------------------------------
    '  Byte型を取得する
    '   （処理概要）ByteのTypeを返却する
    '   ●入力パラメタ　　：なし
    '   ●メソッド戻り値　：取得Type
    '                                               2006.05.30 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' ByteのTypeを返却する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getBte() As Type
        Return Type.GetType("System.Byte")
    End Function
    '-------------------------------------------------------------------------------
    '  日付スラッシュ変換関数
    '   （処理概要）yyyyMMdd → yyyy/MM/dd or yyyy/MM/dd → yyyyMMdd
    '               yyMMdd   → yy/MM/dd   or yy/MM/dd   → yyMMdd
    '   ●入力パラメタ　　：なし
    '   ●メソッド戻り値　：変換後文字列（日付としておかしい場合は空白("")を返します。）
    '                                               2006.11.10 Created By Laevigata inc.
    '-------------------------------------------------------------------------------
    Public Shared Function convertDateSlash(ByVal prmstrDate As String) As String
        Dim w_Date As System.DateTime
        Dim w_Str As String = prmstrDate

        If IsExistString(w_Str) = False Then
            Return ""
        End If

        If w_Str.IndexOf("/", 0) = -1 Then
            '"/"が無い場合
            Try
                If Len(prmstrDate) > 6 Then
                    w_Date = DateTime.ParseExact(prmstrDate, "yyyyMMdd", New System.Globalization.CultureInfo("ja-JP"))
                    Return w_Date.ToString("yyyy/MM/dd")
                Else
                    w_Date = DateTime.ParseExact(prmstrDate, "yyMMdd", New System.Globalization.CultureInfo("ja-JP"))
                    Return w_Date.ToString("yy/MM/dd")
                End If
            Catch
                Return ""
            End Try
        Else
            Try
                If Len(prmstrDate) > 8 Then
                    w_Date = DateTime.ParseExact(prmstrDate, "yyyy/MM/dd", New System.Globalization.CultureInfo("ja-JP"))
                    Return w_Date.ToString("yyyyMMdd")
                Else
                    w_Date = DateTime.ParseExact(prmstrDate, "yy/MM/dd", New System.Globalization.CultureInfo("ja-JP"))
                    Return w_Date.ToString("yyMMdd")
                End If
            Catch
                Return ""
            End Try
        End If
    End Function
    '-------------------------------------------------------------------------------
    '  空白判定
    '   （処理概要）文字列が空白かを判定する
    '   ●入力パラメタ　　：なし
    '   ●メソッド戻り値　：True=空白では無い, False=空白
    '                                               2006.11.10 Created By Laevigata inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' 空白判定
    ''' </summary>
    ''' <returns>True=空白では無い, False=空白</returns>
    ''' <remarks></remarks>
    Public Shared Function IsExistString(ByVal prmstrDate As String) As Boolean
        'Nothing判定
        If IsNothing(prmstrDate) = True Then
            Return False
        End If
        '空文字判定
        If prmstrDate Is String.Empty Then
            Return False
        End If
        '""文字判定
        If "".Equals(prmstrDate.Trim) Then
            Return False
        End If
        Return True
    End Function

    '-------------------------------------------------------------------------------
    '  データ更新時の日時フォーマット
    '   （処理概要）日付表示は環境に合わせるため、登録時は日本形式にする
    '   ●入力パラメタ　　：Datetime
    '   ●メソッド戻り値　：Datetime
    '                                               2019.02.07 Created By Laevigata inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' 空白判定
    ''' </summary>
    ''' <returns>True=空白では無い, False=空白</returns>
    ''' <remarks></remarks>
    Public Shared Function jaDatetimeFormat(ByVal prmDate As DateTime) As String

        '日本の日付形式にする
        Return Format(prmDate, "yyyy/MM/dd HH:mm:ss").ToString

    End Function

    'String型のDateを日本の形式に直す
    Public Shared Function strFormatDate(ByVal prmDate As String, Optional ByRef prmFormat As String = "yyyy/MM/dd") As String

        'PCのカルチャーを取得し、それに応じてStringからDatetimeを作成
        Dim ci As New System.Globalization.CultureInfo(CultureInfo.CurrentCulture.Name.ToString)
        Dim dateFormat As DateTime = DateTime.Parse(prmDate, ci, System.Globalization.DateTimeStyles.AssumeLocal)

        '日本の形式に書き換える
        Return dateFormat.ToString(prmFormat)
    End Function

    'Datetime型を日本の形式に直す
    Public Shared Function formatDatetime(ByVal prmDatetime As DateTime) As String

        'PCのカルチャーを取得し、それに応じてStringからDatetimeを作成
        Dim ciCurrent As New System.Globalization.CultureInfo(CultureInfo.CurrentCulture.Name.ToString)
        Dim dateFormat As DateTime = DateTime.Parse(prmDatetime.ToString, ciCurrent, System.Globalization.DateTimeStyles.AssumeLocal)

        Dim changeFormat As String = dateFormat.ToString("yyyy/MM/dd HH:mm:ss")

        Dim ciJP As New System.Globalization.CultureInfo("ja-JP")
        Dim rtnDatetime As DateTime = DateTime.Parse(changeFormat, ciJP, System.Globalization.DateTimeStyles.AssumeLocal)


        '日本の形式に書き換える
        Return changeFormat
    End Function

    '金額フォーマット（登録の際の小数点指定子）を日本の形式に合わせる
    '桁区切り記号は外す（小数点のみのフォーマットに変換している）
    Public Shared Function formatNumber(ByVal prmVal As Decimal) As String

        Dim nfi As NumberFormatInfo = New CultureInfo("ja-JP", False).NumberFormat

        '日本の形式に書き換える
        Return prmVal.ToString("F3", nfi)
    End Function

    'sqlで実行する文字列からシングルクォーテーションを文字コードにする
    Public Shared Function escapeSql(ByVal prmSql As String) As String
        Dim sql As String = prmSql

        If prmSql IsNot Nothing Then
            sql = sql.Replace("'"c, "''") 'シングルクォーテーションを置換

            'Return Regex.Escape(sql)
            Return sql

        End If

        Return sql
    End Function

End Class
