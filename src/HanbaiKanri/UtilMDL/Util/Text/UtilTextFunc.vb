Namespace Text
    '===============================================================================
    '
    '  ユーティリティクラス
    '    （クラス名）    UtilTextFunc
    '    （処理機能名）      外部I/Fファイルにおいてユーティリティとなるメソッドを提供
    '    （本MDL使用前提）   特に無し
    '    （備考）            
    '
    '===============================================================================
    '  履歴  名前          日  付      マーク      内容
    '-------------------------------------------------------------------------------
    '  (1)   Jun.Takagi    2006/05/14              新規
    '-------------------------------------------------------------------------------
    Public Class UtilTextFunc

        '-------------------------------------------------------------------------------
        '　CSV形式文字列の読み込み
        '   （処理概要）CSV形式の文字列から各フィールドをStringオブジェクトの配列として生成後返却する
        '   ●入力パラメタ：prmSource       分割対象文字列
        '                   <prmSeparator>  区切り文字(省略時：,)
        '                   <prmEncloser>   括り文字(省略時：")
        '   ●関数戻り値　：分割後のString配列
        '                                               2006.05.14 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' CSV形式文字列の読み込み CSV形式の文字列から各フィールドをStringオブジェクトの配列として生成後返却する
        ''' </summary>
        ''' <param name="prmSource">分割対象文字列</param>
        ''' <param name="prmSeparator">区切り文字(省略時：,)</param>
        ''' <param name="prmEncloser">括り文字(省略時：")</param>
        ''' <returns>分割後のString配列</returns>
        ''' <remarks></remarks>
        Public Shared Function splitCSV(ByVal prmSource As String, Optional ByVal prmSeparator As Char = ",", Optional ByVal prmEncloser As Char = """") As String()
            Dim ret() As String : Erase ret
            Dim onData As Boolean
            For index As Integer = 0 To prmSource.Length - 1
                '1文字抽出
                Dim tgt As String = prmSource.Substring(index, 1)

                '括り文字かどうか判断
                If prmEncloser.ToString.Equals(tgt) Then
                    If onData Then
                        onData = False
                    Else
                        onData = True
                    End If
                    Continue For '括り文字の場合は次の文字へ
                End If

                '区切り文字かどうか判断
                If prmSeparator.ToString.Equals(tgt) Then
                    '括り文字の中かどうか判断
                    If onData Then
                        '括り文字の中なのでそのまま格納
                        ret(UBound(ret)) = ret(UBound(ret)) & tgt
                    Else
                        '括り文字の外なので配列拡張
                        If ret Is Nothing Then
                            ReDim ret(0) : ret(0) = ""
                        Else
                            ReDim Preserve ret(UBound(ret) + 1)
                            ret(UBound(ret)) = "" '初期化
                        End If
                    End If
                    Continue For '区切り文字の場合は次の文字へ
                End If

                '通常文字は配列に格納
                If ret Is Nothing Then ReDim ret(0) : ret(0) = ""
                ret(UBound(ret)) = ret(UBound(ret)) & tgt
            Next

            Return ret
        End Function

        '-------------------------------------------------------------------------------
        '　固定長文字列形式の読み込み
        '   （処理概要）固定長文字列形式のデータを各フィールドに分割しStringオブジェクトの配列として返却する
        '   ●入力パラメタ：prmSource         分割対象文字列
        '                   prmSeparateCount  各フィールドのByte数
        '   ●関数戻り値　：分割後のString配列
        '   ●使用例　　　：     Dim rtn() as String
        '                        Dim sep() As Short = New Short() {3, 3, 4}
        '                        rtn = TextFunc.splitFixedString("12345６890", sep)
        '   ●備考  　　　：区切り位置に2Byte文字が存在した場合、該当文字をスペース置換して返却
        '                                               2006.05.14 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 固定長文字列形式の読み込み 固定長文字列形式のデータを各フィールドに分割しStringオブジェクトの配列として返却する
        ''' </summary>
        ''' <param name="prmSource">分割対象文字列</param>
        ''' <param name="prmSeparateCount">各フィールドのByte数</param>
        ''' <returns>分割後のString配列</returns>
        ''' <remarks></remarks>
        Public Shared Function splitFixedString(ByVal prmSource As String, ByVal prmSeparateCount() As Short) As String()
            Dim retAry() As String
            Dim cnt As Short = UBound(prmSeparateCount)     'フィールドの数を取得
            Erase retAry
            For i As Short = 0 To cnt                       'フィールドの数分だけindexLoop
                Dim CharCnt As Short = prmSeparateCount(i)  '対象フィールドのByte数を取得
                Dim ret As String = ""                      '返却用フィールドのバッファを初期化
                Dim wkLen As Short = prmSource.Length       '受領文字列の末尾までの文字数を取得(とりあえず最後までを設定)
                For j As Short = 0 To wkLen
                    '1文字ずつ増やしていき、切り出しByte位置になったかを判定
                    If System.Text.Encoding.GetEncoding("shift_jis").GetByteCount(prmSource.Substring(0, j)) = CharCnt Then
                        '切り出しByte数取得
                        ret = prmSource.Substring(0, j)
                        prmSource = prmSource.Substring(j)  '切り出し部分以降を再格納
                        Exit For
                    ElseIf System.Text.Encoding.GetEncoding("shift_jis").GetByteCount(prmSource.Substring(0, j)) > CharCnt Then
                        '切り出しByte数よりも大きいので、1Byte削除してスペースで調整(区切りByte位置に2Byte文字が存在するケース)
                        ret = prmSource.Substring(0, j - 1) & " "
                        prmSource = " " & prmSource.Substring(j) '切り出し部分以降を再格納
                        Exit For
                    Else
                        ret = prmSource.Substring(0, j)
                    End If
                Next j
                If retAry Is Nothing Then
                    ReDim retAry(0)
                Else
                    ReDim Preserve retAry(UBound(retAry) + 1)
                End If
                retAry(UBound(retAry)) = ret
            Next i
            Return retAry
        End Function

    End Class
End Namespace
