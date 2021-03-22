Imports System.Xml

Namespace Text
    '===============================================================================
    '
    '  ユーティリティクラス
    '    （クラス名）    UtilCsvHandler
    '    （処理機能名）      xml定義のメッセージボックスを表示する
    '    （本MDL使用前提）   UtilMsgVOが取り込まれていること
    '    （備考）            Message.xml形式定義ファイルを想定
    '
    '===============================================================================
    '  履歴  名前          日  付      マーク      内容
    '-------------------------------------------------------------------------------
    '  (1)   Laevigata, Inc.    2006/04/17             新規
    '-------------------------------------------------------------------------------
    Public Class UtilCsvHandler

        '===============================================================================
        'メンバー変数定義
        '===============================================================================
        Private _xmlDoc As XmlDocument
        Private _dlm As Char = ","c
        Dim _elts As XmlNodeList = Nothing

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
                _dlm = getDelimiter()
            Catch ex As XmlException
                Dim lex As UsrDefException = New UsrDefException("言語定義ファイル読込エラー" & ControlChars.NewLine &
                                                     "言語定義ファイルの存在・パスを確認してください。")
                Debug.WriteLine(lex.Message)
                Throw lex
            End Try
        End Sub

        Public Function getDelimiter() As Char
            Try
                Dim langDef As XmlElement = _xmlDoc.DocumentElement
                Dim elemList As XmlNodeList = langDef.GetElementsByTagName("SETTING")
                getDelimiter = elemList.ItemOf(0).Item("DELIMITER").InnerText

            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Debug.WriteLine(ex.StackTrace)
                Throw ex
            End Try
        End Function
        Public Function load(ByVal a As String) As Integer
            Try
                Dim langDef As XmlElement = _xmlDoc.DocumentElement
                _elts = langDef.SelectNodes("//root/DATAOUTPUT/" & a & "/COLUMN_DATA")
                Return _elts.Count()

            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Debug.WriteLine(ex.StackTrace)
                Throw ex
            End Try

        End Function
        Public Function get_column_str(ByVal i As Integer) As String
            If _elts Is Nothing Then
                Return vbNullString
            End If
            Try
                Dim x As String = _elts.ItemOf(i).Item("DBCOL").GetAttribute("as")
                If Not x.Equals("") Then
                    Return get_(i, "TAB") & "." & _elts.ItemOf(i).Item("DBCOL").InnerText & " AS " & _elts.ItemOf(i).Item("DBCOL").InnerText & x
                End If
                Return get_(i, "TAB") & "." & _elts.ItemOf(i).Item("DBCOL").InnerText
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Debug.WriteLine(ex.StackTrace)
                Throw ex
            End Try
        End Function
        Public Function count() As Integer
            If _elts Is Nothing Then
                Return 0
            End If
            Try
                Return _elts.Count
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Debug.WriteLine(ex.StackTrace)
                Throw ex
            End Try
        End Function
        Public Function get_hd_text_str(ByVal i As Integer, ByVal l As String) As String
            If _elts Is Nothing Then
                Return 0
            End If
            Try
                If l = "ENG" Then
                    Return _elts.ItemOf(i).Item("ENG").InnerText
                Else
                    Return _elts.ItemOf(i).Item("JPN").InnerText
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Debug.WriteLine(ex.StackTrace)
                Throw ex
            End Try
        End Function
        Public Function get_(ByVal i As Integer, ByVal l As String) As String
            If _elts Is Nothing Then
                Return 0
            End If
            Try
                If l.Equals("DBCOL") Then
                    Dim x As String = _elts.ItemOf(i).Item(l).GetAttribute("as")
                    If Not x.Equals("") Then
                        Return _elts.ItemOf(i).Item(l).InnerText & x
                    End If
                End If
                Dim r As String = _elts.ItemOf(i).Item(l).InnerText
                Return r
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Debug.WriteLine(ex.StackTrace)
                'Throw ex
                Return Nothing
            End Try
        End Function

    End Class
End Namespace
