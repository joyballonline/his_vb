Imports System.Xml

Namespace LANG
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
    Public Class UtilLangHandler

        '===============================================================================
        'メンバー変数定義
        '===============================================================================
        Private _xmlDoc As XmlDocument

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
                Dim lex As UsrDefException = New UsrDefException("言語定義ファイル読込エラー" & ControlChars.NewLine &
                                                     "言語定義ファイルの存在・パスを確認してください。")
                Debug.WriteLine(lex.Message)
                Throw lex
            End Try
        End Sub

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
        ''' <param name="prmLangId">メッセージID</param>
        ''' <returns>検索されたメッセージビーン(ValueObject)</returns>
        ''' <remarks>発生例外       ：Exception,UsrDefException</remarks>
        Public Function getLANG(ByVal prmLangText As String, ByVal prmLangId As String) As String
            Try
                Dim langDef As XmlElement = _xmlDoc.DocumentElement
                Dim elemList As XmlNodeList = langDef.GetElementsByTagName("LANG_DATA")
                Dim i As Integer
                For i = 0 To elemList.Count - 1
                    If elemList.ItemOf(i).Item("ID").InnerText = prmLangText Then
                        'メッセージIDが一致するなら
                        Dim textWk As String = "err"
                        textWk = elemList.ItemOf(i).Item(prmLangId).InnerText

                        'MSG表示
                        Return textWk
                    End If
                Next
                Return prmLangText
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Debug.WriteLine(ex.StackTrace)
                Throw ex
            End Try

        End Function
    End Class
End Namespace
