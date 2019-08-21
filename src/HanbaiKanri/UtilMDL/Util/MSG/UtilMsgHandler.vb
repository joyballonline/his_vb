Imports System.Xml
Imports System.ComponentModel
Imports System.Windows.Forms

Namespace MSG
    '===============================================================================
    '
    '  ユーティリティクラス
    '    （クラス名）    UtilMsgHandler
    '    （処理機能名）      xml定義のメッセージボックスを表示する
    '    （本MDL使用前提）   UtilMsgVOが取り込まれていること
    '    （備考）            Message.xml形式定義ファイルを想定
    '
    '===============================================================================
    '  履歴  名前          日  付      マーク      内容
    '-------------------------------------------------------------------------------
    '  (1)   Laevigata, Inc.    2006/04/17             新規
    '-------------------------------------------------------------------------------
    Public Class UtilMsgHandler

        '===============================================================================
        'メンバー変数定義
        '===============================================================================
        Private _xmlDoc As XmlDocument
        Private _defTitle As String = ""                      'MSGBOXへのデフォルトパラメタ
        Private _defButton As MessageBoxButtons               'MSGBOXへのデフォルトパラメタ
        Private _defIcon As MessageBoxIcon                    'MSGBOXへのデフォルトパラメタ
        Private _defDefaultButton As MessageBoxDefaultButton  'MSGBOXへのデフォルトパラメタ

        '===============================================================================
        'プロパティ(アクセサ)
        '===============================================================================
        Public ReadOnly Property xmlDoc() As XmlDocument
            'Geter--------
            Get
                xmlDoc = _xmlDoc
            End Get
            'Setter-------
            'なし
        End Property
        Public ReadOnly Property defTitle() As String
            'Geter--------
            Get
                defTitle = _defTitle
            End Get
            'Setter-------
            'なし
        End Property
        Public ReadOnly Property defButton() As String
            'Geter--------
            Get
                defButton = _defButton
            End Get
            'Setter-------
            'なし
        End Property
        Public ReadOnly Property defIcon() As String
            'Geter--------
            Get
                defIcon = _defIcon
            End Get
            'Setter-------
            'なし
        End Property
        Public ReadOnly Property defDefaultButton() As String
            'Geter--------
            Get
                defDefaultButton = _defDefaultButton
            End Get
            'Setter-------
            'なし
        End Property

        '===============================================================================
        ' コンストラクタ
        '   ●入力パラメタ   ：prmFileName    フルパスメッセージファイル名
        '===============================================================================
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="prmFileName">フルパスメッセージファイル名</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal prmFileName As String)
            Try
                _xmlDoc = New XmlDocument()  '_xmlDocumentオブジェクトを作成    
                _xmlDoc.Load(prmFileName)
            Catch ex As XmlException
                Dim lex As UsrDefException = New UsrDefException("メッセージ定義ファイル読込エラー" & ControlChars.NewLine &
                                                     "メッセージ定義ファイルの存在・パスを確認してください。")
                Debug.WriteLine(lex.Message)
                Throw lex
            End Try
            Try
                'デフォルト定義の読込
                Dim msgDefault As XmlElement = _xmlDoc.DocumentElement
                Dim elemList As XmlNodeList = msgDefault.GetElementsByTagName("DEFAULT_MSG")
                If elemList.Count <> 1 Then
                    Dim lex As UsrDefException = New UsrDefException("DEFAULT_MSGの定義が存在しないか、複数定義されています。")
                    Debug.WriteLine(lex.Message)
                    Throw lex
                End If
                'タイトル編集
                Try
                    _defTitle = elemList.ItemOf(0).Item("TITLE").InnerText()
                Catch ex As Exception
                    Dim lex As UsrDefException = New UsrDefException("DEFAULT_MSGのTITLE定義が存在しません。")
                    Debug.WriteLine(lex.Message)
                    Throw lex
                End Try
                'ボタン編集
                Dim button As String
                Try
                    button = elemList.ItemOf(0).Item("BUTTON_TYPE").InnerText
                Catch ex As Exception : button = "" : End Try '表記省略時
                Select Case button.ToLower
                    Case "abortretryignore"
                        _defButton = MessageBoxButtons.AbortRetryIgnore
                    Case "ok"
                        _defButton = MessageBoxButtons.OK
                    Case "okcancel"
                        _defButton = MessageBoxButtons.OKCancel
                    Case "retrycancel"
                        _defButton = MessageBoxButtons.RetryCancel
                    Case "yesno"
                        _defButton = MessageBoxButtons.YesNo
                    Case "yesnocancel"
                        _defButton = MessageBoxButtons.YesNoCancel
                    Case ""
                        Dim lex As UsrDefException = New UsrDefException("DEFAULT_MSGのBUTTON_TYPE定義が存在しません。")
                        Debug.WriteLine(lex.Message)
                        Throw lex
                    Case Else
                        Dim lex As UsrDefException = New UsrDefException("DEFAULT_MSGのBUTTON_TYPE定義が誤っています。")
                        Debug.WriteLine(lex.Message)
                        Throw lex
                End Select
                'アイコン編集
                Dim icon As String
                Try
                    icon = elemList.ItemOf(0).Item("ICONT_TYPE").InnerText
                Catch ex As Exception : icon = "" : End Try '表記省略時
                Select Case icon.ToLower
                    Case "asterisk"
                        _defIcon = MessageBoxIcon.Asterisk
                    Case "error"
                        _defIcon = MessageBoxIcon.Error
                    Case "exclamation"
                        _defIcon = MessageBoxIcon.Exclamation
                    Case "hand"
                        _defIcon = MessageBoxIcon.Hand
                    Case "information"
                        _defIcon = MessageBoxIcon.Information
                    Case "none"
                        _defIcon = MessageBoxIcon.None
                    Case "question"
                        _defIcon = MessageBoxIcon.Question
                    Case "stop"
                        _defIcon = MessageBoxIcon.Stop
                    Case "warning"
                        _defIcon = MessageBoxIcon.Warning
                    Case ""
                        Dim lex As UsrDefException = New UsrDefException("DEFAULT_MSGのICONT_TYPE定義が存在しません。")
                        Debug.WriteLine(lex.Message)
                        Throw lex
                    Case Else
                        Dim lex As UsrDefException = New UsrDefException("ICONT_TYPEの定義が誤っています。")
                        Debug.WriteLine(lex.Message)
                        Throw lex
                End Select
                'デフォルトボタン編集
                Dim defaultButton As String
                Try
                    defaultButton = elemList.ItemOf(0).Item("DEFAULT_BUTTON").InnerText
                Catch ex As Exception : defaultButton = "" : End Try '表記省略時
                Select Case defaultButton.ToLower
                    Case "button1"
                        _defDefaultButton = MessageBoxDefaultButton.Button1
                    Case "button2"
                        _defDefaultButton = MessageBoxDefaultButton.Button2
                    Case "button3"
                        _defDefaultButton = MessageBoxDefaultButton.Button3
                    Case ""
                        Dim lex As UsrDefException = New UsrDefException("DEFAULT_MSGのDEFAULT_BUTTON定義が存在しません。")
                        Debug.WriteLine(lex.Message)
                        Throw lex
                    Case Else
                        Dim lex As UsrDefException = New UsrDefException("DEFAULT_BUTTONの定義が誤っています。")
                        Debug.WriteLine(lex.Message)
                        Throw lex
                End Select

            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Debug.WriteLine(ex.StackTrace)
                Throw ex
            End Try
        End Sub

        '-------------------------------------------------------------------------------
        '   メッセージ表示
        '   （処理概要）通知されたメッセージIDに対応するMSGを編集して表示する
        '   ●入力パラメタ   ：prmMsgId         メッセージID
        '                   ：prmOptionalMsg   追加メッセージ
        '   ●メソッド戻り値 ：押下ボタン(DialogResult)
        '   ●発生例外       ：Exception,UsrDefException
        '                                               2006.05.07 Updated By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' メッセージ表示 通知されたメッセージIDに対応するMSGを編集して表示する
        ''' </summary>
        ''' <param name="prmMsgId">メッセージID</param>
        ''' <param name="prmOptionalMsg">追加メッセージ</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function dspMSG(ByVal prmMsgId As String, ByVal prmLang As String, Optional ByVal prmOptionalMsg As String = "") As DialogResult

            Dim mv As UtilMsgVO = Me.getMSG(prmMsgId, prmLang, prmOptionalMsg)
            Return MessageBox.Show(mv.dspStr, mv.title, mv.button, mv.icon, mv.defaultButton)

        End Function

        '-------------------------------------------------------------------------------
        '   メッセージ取得
        '   （処理概要）通知されたメッセージIDに対応するMSGを編集して返却する
        '   ●入力パラメタ   ：prmMsgId         メッセージID
        '                   ：prmOptionalMsg   追加メッセージ
        '   ●メソッド戻り値 ：検索されたメッセージビーン(ValueObject)
        '   ●発生例外       ：Exception,UsrDefException
        '                                               2006.05.07 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' メッセージ取得 通知されたメッセージIDに対応するMSGを編集して返却する
        ''' </summary>
        ''' <param name="prmMsgId">メッセージID</param>
        ''' <param name="prmOptionalMsg">追加メッセージ</param>
        ''' <returns>検索されたメッセージビーン(ValueObject)</returns>
        ''' <remarks>発生例外       ：Exception,UsrDefException</remarks>
        Public Function getMSG(ByVal prmMsgId As String, ByVal prmLang As String, Optional ByVal prmOptionalMsg As String = "") As UtilMsgVO
            Try
                Dim msgDef As XmlElement = _xmlDoc.DocumentElement
                Dim elemList As XmlNodeList = msgDef.GetElementsByTagName("MSG_DATA")
                Dim i As Integer
                Dim buttonWk As MessageBoxButtons
                Dim iconWk As MessageBoxIcon
                Dim defaultButtonWk As MessageBoxDefaultButton
                For i = 0 To elemList.Count - 1
                    If elemList.ItemOf(i).Item("ID").InnerText = prmMsgId Then
                        'メッセージIDが一致するなら
                        'タイトル編集
                        Dim titleWk As String
                        Try
                            titleWk = elemList.ItemOf(i).Item("TITLE").InnerText
                        Catch ex As Exception
                            titleWk = _defTitle
                        End Try
                        'メッセージ編集
                        Dim msg1 As String = ""
                        Dim msg2 As String = ""

                        If prmLang = "JPN" AndAlso elemList.ItemOf(i).Item("MSG1_EN") IsNot Nothing Then
                            Try : msg1 = elemList.ItemOf(i).Item("MSG1").InnerText : Catch ex As Exception : End Try
                        Else
                            Try : msg1 = elemList.ItemOf(i).Item("MSG1_EN").InnerText : Catch ex As Exception : End Try
                        End If

                        If prmLang = "JPN" AndAlso elemList.ItemOf(i).Item("MSG2_EN") IsNot Nothing Then
                            Try : msg2 = elemList.ItemOf(i).Item("MSG2").InnerText : Catch ex As Exception : End Try
                        Else
                            Try : msg2 = elemList.ItemOf(i).Item("MSG2_EN").InnerText : Catch ex As Exception : End Try
                        End If

                        Dim dspStrWk As String
                        If msg2 <> "" Then
                            dspStrWk = msg1 & ControlChars.NewLine & msg2
                        Else
                            dspStrWk = msg1
                        End If
                        If prmOptionalMsg <> "" Then
                            dspStrWk = dspStrWk & ControlChars.NewLine & ControlChars.NewLine & prmOptionalMsg
                        End If
                        'ボタン編集
                        Dim button As String
                        Try
                            button = elemList.ItemOf(i).Item("BUTTON_TYPE").InnerText
                        Catch ex As Exception : button = "" : End Try '表記省略時
                        Select Case button.ToLower
                            Case "abortretryignore"
                                buttonWk = MessageBoxButtons.AbortRetryIgnore
                            Case "ok"
                                buttonWk = MessageBoxButtons.OK
                            Case "okcancel"
                                buttonWk = MessageBoxButtons.OKCancel
                            Case "retrycancel"
                                buttonWk = MessageBoxButtons.RetryCancel
                            Case "yesno"
                                buttonWk = MessageBoxButtons.YesNo
                            Case "yesnocancel"
                                buttonWk = MessageBoxButtons.YesNoCancel
                            Case ""
                                buttonWk = _defButton
                            Case Else
                                Dim lex As UsrDefException = New UsrDefException("BUTTON_TYPEの定義が誤っています。")
                                Debug.WriteLine(lex.Message)
                                Throw lex
                        End Select
                        'アイコン編集
                        Dim icon As String
                        Try
                            icon = elemList.ItemOf(i).Item("ICONT_TYPE").InnerText
                        Catch ex As Exception : icon = "" : End Try '表記省略時
                        Select Case icon.ToLower
                            Case "asterisk"
                                iconWk = MessageBoxIcon.Asterisk
                            Case "error"
                                iconWk = MessageBoxIcon.Error
                            Case "exclamation"
                                iconWk = MessageBoxIcon.Exclamation
                            Case "hand"
                                iconWk = MessageBoxIcon.Hand
                            Case "information"
                                iconWk = MessageBoxIcon.Information
                            Case "none"
                                iconWk = MessageBoxIcon.None
                            Case "question"
                                iconWk = MessageBoxIcon.Question
                            Case "stop"
                                iconWk = MessageBoxIcon.Stop
                            Case "warning"
                                iconWk = MessageBoxIcon.Warning
                            Case ""
                                iconWk = _defIcon
                            Case Else
                                Dim lex As UsrDefException = New UsrDefException("ICONT_TYPEの定義が誤っています。")
                                Debug.WriteLine(lex.Message)
                                Throw lex
                        End Select
                        'デフォルトボタン編集
                        Dim defaultButton As String
                        Try
                            defaultButton = elemList.ItemOf(i).Item("DEFAULT_BUTTON").InnerText
                        Catch ex As Exception : defaultButton = "" : End Try '表記省略時
                        Select Case defaultButton.ToLower
                            Case "button1"
                                defaultButtonWk = MessageBoxDefaultButton.Button1
                            Case "button2"
                                defaultButtonWk = MessageBoxDefaultButton.Button2
                            Case "button3"
                                defaultButtonWk = MessageBoxDefaultButton.Button3
                            Case ""
                                defaultButtonWk = _defDefaultButton
                            Case Else
                                Dim lex As UsrDefException = New UsrDefException("DEFAULT_BUTTONの定義が誤っています。")
                                Debug.WriteLine(lex.Message)
                                Throw lex
                        End Select

                        'MSG表示
                        Return New UtilMsgVO(dspStrWk, titleWk, buttonWk, iconWk, defaultButtonWk)
                    End If
                Next
                Return New UtilMsgVO("メッセージIDが見つかりません。(" & prmMsgId & ")", "システムエラー", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Debug.WriteLine(ex.StackTrace)
                Throw ex
            End Try

        End Function
    End Class

    '===============================================================================
    '
    '  ユーティリティクラス
    '    （クラス名）    UtilMsgVO
    '    （処理機能名）      xml定義のメッセージボックスビーン
    '    （本MDL使用前提）   UtilMsgHandlerが取り込まれていること
    '    （備考）            
    '
    '===============================================================================
    '  履歴  名前          日  付      マーク      内容
    '-------------------------------------------------------------------------------
    '  (1)   Laevigata, Inc.    2006/05/07              新規
    '-------------------------------------------------------------------------------
    Public Class UtilMsgVO

        '===============================================================================
        'メンバー変数定義
        '===============================================================================
        Private _dspStr As String
        Private _title As String
        Private _button As MessageBoxButtons
        Private _icon As MessageBoxIcon
        Private _defaultButton As MessageBoxDefaultButton

        '===============================================================================
        'プロパティ(アクセサ)
        '===============================================================================
        Public ReadOnly Property dspStr() As String
            Get
                Return _dspStr
            End Get
        End Property
        Public ReadOnly Property title() As String
            Get
                Return _title
            End Get
        End Property
        Public ReadOnly Property button() As MessageBoxButtons
            Get
                Return _button
            End Get
        End Property
        Public ReadOnly Property icon() As MessageBoxIcon
            Get
                Return _icon
            End Get
        End Property
        Public ReadOnly Property defaultButton() As MessageBoxDefaultButton
            Get
                Return _defaultButton
            End Get
        End Property

        '===============================================================================
        ' コンストラクタ
        '   ●入力パラメタ   ：各種MessageBoxパラメタ
        '===============================================================================
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="prmDspStr">出力するテキスト</param>
        ''' <param name="prmTitle">タイトル</param>
        ''' <param name="prmButton">ボタンの種類</param>
        ''' <param name="prmIcon">アイコン</param>
        ''' <param name="prmDefaultButton">既定のボタン</param>
        ''' <remarks></remarks>
        Friend Sub New(ByVal prmDspStr As String, ByVal prmTitle As String, ByVal prmButton As MessageBoxButtons, ByVal prmIcon As MessageBoxIcon, ByVal prmDefaultButton As MessageBoxDefaultButton)
            _dspStr = prmDspStr
            _title = prmTitle
            _button = prmButton
            _icon = prmIcon
            _defaultButton = prmDefaultButton
        End Sub

    End Class
End Namespace
