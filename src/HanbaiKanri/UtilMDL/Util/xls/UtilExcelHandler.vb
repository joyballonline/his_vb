'Imports System.Runtime.InteropServices
Imports Microsoft.Office.Interop

Namespace xls
    '===============================================================================
    '
    '  ユーティリティクラス
    '    （クラス名）    UtilExcelHandler
    '    （処理機能名）     Excel操作機能を提供
    '    （本MDL使用前提）  Microsoft Excel **.* Object Library を参照設定のこと
    '    （備考）           ※複数のBookを扱うことには非対応
    '                       ※必ずExcelの使用が完了したら必ずendUseメソッドを呼び出すこと
    '                           Finallyブロックを用いるなどして、endUseの呼び出しを保障してください。
    '                           →「プロセスが正しく終了しない」「メモリリーク」などの不具合を発生させます。
    '                           また、COMコンポーネントの扱いに対する理解が深い場合を除いて、
    '                           ローカルモジュール内でExcelオブジェクトの操作(Excel.○○の記述)も推奨しません。
    '
    '===============================================================================
    '  履歴  名前          日  付      マーク      内容
    '-------------------------------------------------------------------------------
    '  (1)   Laevigata, Inc.    2006/05/17              新規
    '  (2)   Laevigata, Inc.    2006/07/03              改ページメソッドを追加
    '  (3)   Laevigata, Inc.    2007/08/10              未実装だった以下メソッドを追加
    '                                                   setColor
    '                                                   setFont
    '                                                   setNumberFormat
    '                                                   setUpPageDefine
    '  (4)   Laevigata, Inc.    2010/03/05              endUseを呼び出してもプロセスが残る場合がある不具合を修正
    '  (5)   Karino        2010/03/08              setShapeTextBox,paintShape,printOutを追加
    '  (6)   Sugano        2010/03/16              printPreviewを追加
    '-------------------------------------------------------------------------------
    Public Class UtilExcelHandler
        Inherits ExcelFunc
        '===============================================================================
        '構造体定義
        '===============================================================================
        '横位置--------------
        ''' <summary>
        ''' 横位置を表す列挙体
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum HorizontalAlignment As Short
            ''' <summary>
            ''' 左寄せ
            ''' </summary>
            ''' <remarks></remarks>
            Left = 0
            ''' <summary>
            ''' 中央寄せ
            ''' </summary>
            ''' <remarks></remarks>
            Center = 1
            ''' <summary>
            ''' 右寄せ
            ''' </summary>
            ''' <remarks></remarks>
            Right = 2
            ''' <summary>
            ''' 選択範囲で中央寄せ
            ''' </summary>
            ''' <remarks></remarks>
            CenterAcrossSelection = 3
        End Enum

        '縦位置--------------
        ''' <summary>
        ''' 縦位置を表す列挙体
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum VerticalAlignment As Short
            ''' <summary>
            ''' 上寄せ
            ''' </summary>
            ''' <remarks></remarks>
            Top = 0
            ''' <summary>
            ''' 中央寄せ
            ''' </summary>
            ''' <remarks></remarks>
            Center = 1
            ''' <summary>
            ''' 下寄せ
            ''' </summary>
            ''' <remarks></remarks>
            Bottom = 2
        End Enum

        '===============================================================================
        'メンバー変数定義
        '===============================================================================
        Private _fileName As String         'ファイル名
        Private _fileCreate As Boolean      'ファイル生成モード
        Private _app As Excel.Application   'Excelアプリ
        Private _book As Excel.Workbook     '対象ブック
        Private _books As Excel.Workbooks   'ワークブック
        Private _sheet As Excel.Worksheet   '対象シート
        Private _sheets As Excel.Sheets     'ワークシート

        '===============================================================================
        'メンバー定数定義
        '===============================================================================
        Public Const xlPasteFormats As Short = -4122
        Public Const xlNone As Short = -4142
        Public Const xlPasteValues As Short = -4163
        Public Const xlUp As Short = -4162
        Public Const xlDown As Short = -4121
        Public Const xlToRight As Short = -4161
        Public Const xlToLeft As Short = -4159
        Public Const xlLeft As Short = -4131
        Public Const xlCenter As Short = -4108
        Public Const xlCenterAcrossSelection As Short = 7
        Public Const xlRight As Short = -4152
        Public Const xlTop As Short = -4160
        Public Const xlBottom As Short = -4107
        Public Const xlEdgeLeft As Short = 7
        Public Const xlEdgeTop As Short = 8
        Public Const xlEdgeBottom As Short = 9
        Public Const xlEdgeRight As Short = 10
        Public Const xlInsideVertical As Short = 11
        Public Const xlInsideHorizontal As Short = 12
        Public Const xlContinuous As Short = 1
        Public Const xlHairline As Short = 1
        Public Const xlThin As Short = 2
        Public Const xlMedium As Short = -4138
        Public Const xlDouble As Short = -4119
        Public Const xlThick As Short = 4

        '===============================================================================
        'プロパティ(アクセサ)
        '===============================================================================
        ''' <summary>
        ''' 対象シート
        ''' </summary>
        ''' <value>シート名</value>
        ''' <returns>シート名</returns>
        ''' <remarks></remarks>
        Public Property targetSheet() As String
            Get
                Return _sheet.Name
            End Get
            Set(ByVal value As String)
                _sheet = _book.Sheets(value)
            End Set
        End Property
        ''' <summary>
        ''' 対象シート(インデックス指定用)
        ''' </summary>
        ''' <value>インデックス(1～)</value>
        ''' <returns>インデックス(1～)</returns>
        ''' <remarks></remarks>
        Public Property targetSheetByIdx() As Short
            Get
                Return _sheet.Index
            End Get
            Set(ByVal value As Short)
                _sheet = _book.Sheets(value)
            End Set
        End Property

        '===============================================================================
        'コンストラクタ
        '===============================================================================
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="prmFile">操作対象ファイル名</param>
        ''' <param name="prmCreate">ファイルを作成するかどうかのフラグ</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal prmFile As String, Optional ByVal prmCreate As Boolean = False)
            '拡張子チェック
            If Not (System.IO.Path.GetExtension(prmFile).ToLower.Equals(".xls") Or
                    System.IO.Path.GetExtension(prmFile).ToLower.Equals(".xlsx")) Then
                Throw New UsrDefException("拡張子が誤っています。")
            End If
            'ファイルを生成するのかどうか判定
            If prmCreate = False Then
                '既存のファイルを開く
                If System.IO.File.Exists(prmFile) = False Then
                    'ファイルが存在しない場合はエラー
                    Throw New UsrDefException("対象xlsファイルが存在しません。")
                End If
                _fileCreate = False
            Else
                '新しくファイルを生成する

                'パスとファイル名に分割して
                Dim devPos As Integer
                Dim prmFullPath As String = prmFile 'フルパス
                Dim prmRefPath As String = ""       'パスのみ
                Dim prmRefFile As String = ""       'ファイル名のみ
                devPos = InStrRev(prmFullPath.Replace("/", "\"), "\")
                If devPos <= 0 Then
                    prmRefFile = prmFullPath
                Else
                    prmRefFile = prmFullPath.Substring(devPos)
                    prmRefPath = prmFullPath.Substring(0, devPos - 1)
                End If

                '生成dirのみで存在チェックを実施する
                If System.IO.Directory.Exists(prmRefPath) = False Then
                    Throw New UsrDefException("生成場所の指定に誤りがあります。")
                End If

                _fileCreate = True
            End If
            _fileName = prmFile
        End Sub

        '===============================================================================
        'デストラクタ
        '===============================================================================
        ''' <summary>
        ''' デストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Protected Overrides Sub Finalize()
            Me.endUse()
            MyBase.Finalize()
        End Sub

        '===============================================================================
        'Excel開放処理
        '===============================================================================
        ''' <summary>
        ''' ExcelObject(COM)の開放処理　※メモリリーク対策
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub endUse()
            Try

                'COM開放
                Me.releaseCom(_sheet)
                Me.releaseCom(_sheets)
                Me.releaseCom(_book)
                Me.releaseCom(_books) '2010.03.04 add by Laevigata, Inc.
                Me.releaseCom(_app)

                'ガーベジコレクト強制実行
                GC.Collect()

            Catch ex As Exception
            End Try
        End Sub

        '内部メソッド：COMObjectの開放
        Private Sub releaseCom(ByRef prmCom As Object)
            Try
                If Not prmCom Is Nothing AndAlso System.Runtime.InteropServices.
                                                          Marshal.IsComObject(prmCom) Then
                    Dim i As Integer = 1
                    Do While i > 0
                        i = System.Runtime.InteropServices.Marshal.ReleaseComObject(prmCom)
                    Loop
                End If
            Catch
            Finally
                prmCom = Nothing
            End Try
        End Sub

        '===============================================================================
        'ファイル作成
        '※※※一旦呼出した後は再度呼び出すことを禁止
        '※※※createメソッドの呼出し後、編集が終わったらcloseメソッドを呼び出すこと
        '===============================================================================
        ''' <summary>
        ''' ファイル作成　createメソッドの呼出し後、編集が終わったらcloseメソッドを呼び出すこと
        ''' </summary>
        ''' <remarks>一旦呼出した後は再度呼び出すことを禁止</remarks>
        Public Sub create()
            If _fileCreate = False Then
                Me.endUse()
                Throw New UsrDefException("ファイル生成モードでないためcreateメソッドの呼出しは不正です。")
            End If
            _app = New Excel.Application    'Excelアプリの生成
            _app.Visible = False            '非表示
            _app.DisplayAlerts = False      'アラート非表示

            _books = _app.Workbooks         'ブックオープン
            _book = _books.Add              '対象ブックを追加して取得
            _sheets = _book.Worksheets
            _sheet = _sheets.Item(1)        '対象ブックの1枚目のシートを対象シートとして取得
            _sheet.SaveAs(_fileName)        '初めに保存

            _fileCreate = False             '生成が終わったのでフラグを倒す

        End Sub

        '===============================================================================
        'ファイルオープン
        '※※※openメソッドの呼出し後、編集が終わったらcloseメソッドを呼び出すこと
        '===============================================================================
        ''' <summary>
        ''' ファイルオープン　openメソッドの呼出し後、編集が終わったらcloseメソッドを呼び出すこと
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub open()
            If _fileCreate Then
                Me.endUse()
                Throw New UsrDefException("ファイル生成モードの場合は、createメソッドの呼出しを先に行って下さい。")
            End If
            If _app IsNot Nothing Then
                Me.endUse()
                Throw New UsrDefException("ファイルを開いているまま、openメソッドの呼出しは行えません。")
            End If
            _app = New Excel.Application    'Excelアプリの生成
            _app.Visible = False             '非表示
            _app.DisplayAlerts = False      'アラート非表示

            _books = _app.Workbooks         'ブックオープン
            _book = _books.Open(_fileName)  '対象ブックを開いて取得
            _sheets = _book.Worksheets
            _sheet = _sheets.Item(1)        'とりあえず対象ブックの1枚目のシートを対象シートとして取得

        End Sub

        '===============================================================================
        'ファイルクローズ
        '===============================================================================
        ''' <summary>
        ''' ファイルクローズ
        ''' </summary>
        ''' <param name="prmSaveFlg">保存するかどうかのフラグ</param>
        ''' <remarks></remarks>
        Public Sub close(Optional ByVal prmSaveFlg As Boolean = True)
            Try
                _book.Close(prmSaveFlg)
                _app.Quit()
            Catch ex As Exception
            Finally
                Me.endUse()
            End Try
        End Sub

        '===============================================================================
        'Excel表示
        '===============================================================================
        ''' <summary>
        ''' xlsファイルをExcelアプリケーションで表示する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub display()
            If _app IsNot Nothing Then
                Me.close()
                Debug.WriteLine("□■□■□■□■□■□■□■□■□■□■□■□■□■□■□■□■□■□■□")
                Debug.WriteLine("closeメソッドの呼出し後でなければ、displayメソッドの呼出しは行えません。")
                Debug.WriteLine("    強制的にcloseメソッドを呼び出しました。")
                Debug.WriteLine("    ロジックの見直しを図ってください。")
                Debug.WriteLine("□■□■□■□■□■□■□■□■□■□■□■□■□■□■□■□■□■□■□")
            End If
            _app = New Excel.Application    'Excelアプリの生成
            _app.Visible = True             '非表示
            _app.DisplayAlerts = False      'アラート非表示
            _app.WindowState = Excel.XlWindowState.xlMaximized '最大化
            _books = _app.Workbooks         'ワークブック取得
            Call _books.Open(_fileName)     '対象ブックを開く
            Me.endUse()                     '終了処理
        End Sub

        '===============================================================================
        'Excel印刷
        '===============================================================================
        ''' <summary>
        ''' 指定したワークシートを印刷する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub printOut()
            If _app IsNot Nothing Then
                Me.close()
                Debug.WriteLine("□■□■□■□■□■□■□■□■□■□■□■□■□■□■□■□■□■□■□")
                Debug.WriteLine("closeメソッドの呼出し後でなければ、displayメソッドの呼出しは行えません。")
                Debug.WriteLine("    強制的にcloseメソッドを呼び出しました。")
                Debug.WriteLine("    ロジックの見直しを図ってください。")
                Debug.WriteLine("□■□■□■□■□■□■□■□■□■□■□■□■□■□■□■□■□■□■□")
            End If
            _app = New Excel.Application    'Excelアプリの生成
            _app.Visible = False            '非表示
            _app.DisplayAlerts = False      'アラート非表示
            _books = _app.Workbooks         'ワークブック取得
            _book = _books.Open(_fileName)  '対象ブックを開いて取得
            _sheets = _book.Worksheets      'シート取得
            _sheet = _sheets.Item(1)        'とりあえず対象ブックの1枚目のシートを対象シートとして取得
            _sheet.PrintOut()               '対象シートの印刷
            Call _books.Close()
            Me.close()
            Me.endUse()                     '終了処理

        End Sub
        '===============================================================================
        'Excel印刷プレビュー表示
        '===============================================================================
        ''' <summary>
        ''' 指定したワークシートを印刷プレビューモードで表示する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub printPreview()
            If _app IsNot Nothing Then
                Me.close()
                Debug.WriteLine("□■□■□■□■□■□■□■□■□■□■□■□■□■□■□■□■□■□■□")
                Debug.WriteLine("closeメソッドの呼出し後でなければ、displayメソッドの呼出しは行えません。")
                Debug.WriteLine("    強制的にcloseメソッドを呼び出しました。")
                Debug.WriteLine("    ロジックの見直しを図ってください。")
                Debug.WriteLine("□■□■□■□■□■□■□■□■□■□■□■□■□■□■□■□■□■□■□")
            End If
            _app = New Excel.Application    'Excelアプリの生成
            _app.Visible = True            '非表示
            _app.DisplayAlerts = False      'アラート非表示
            _books = _app.Workbooks         'ワークブック取得
            _book = _books.Open(_fileName)  '対象ブックを開いて取得
            _sheets = _book.Worksheets      'シート取得
            _sheet = _sheets.Item(1)        'とりあえず対象ブックの1枚目のシートを対象シートとして取得
            _sheet.PrintPreview()           '対象シートの印刷
            Call _books.Close()
            Me.close()
            Me.endUse()                     '終了処理

        End Sub
        '===============================================================================
        '指定したワークシートを選択する
        '===============================================================================
        ''' <summary>
        ''' 指定したワークシートを選択する
        ''' </summary>
        ''' <param name="prmName"></param>
        ''' <remarks></remarks>
        Public Sub selectSheet(ByVal prmName As String)
            _sheet = _book.Sheets(prmName)
            _sheet.Select()
        End Sub

        '===============================================================================
        '指定したレンジを選択する
        'prmRow2/prmCol2を省略した場合は、それぞれprmRow/prmColと同値を採用する
        '===============================================================================
        ''' <summary>
        ''' 指定したレンジを選択する　prmRow2/prmCol2を省略した場合は、それぞれprmRow/prmColと同値を採用する
        ''' </summary>
        ''' <param name="prmRow">開始行</param>
        ''' <param name="prmCol">開始列</param>
        ''' <param name="prmRow2">終了行</param>
        ''' <param name="prmCol2">終了列</param>
        ''' <remarks></remarks>
        Public Sub selectRange(ByVal prmRow As Short, ByVal prmCol As Short,
                                       Optional ByVal prmRow2 As Short = -9, Optional ByVal prmCol2 As Short = -9)
            Dim wkRow2 As Short = prmRow2
            Dim wkCol2 As Short = prmCol2
            If wkRow2 = -9 Then wkRow2 = prmRow
            If wkCol2 = -9 Then wkCol2 = prmCol

            Dim cell1 As String = Me.convFromR1C1(prmRow, prmCol)
            Dim cell2 As String = Me.convFromR1C1(wkRow2, wkCol2)
            _app.Range(cbnCellStr(cell1, cell2)).Select()
        End Sub

        '===============================================================================
        '指定したセルを選択する
        '===============================================================================
        ''' <summary>
        ''' 指定したセルを選択する
        ''' </summary>
        ''' <param name="prmRow">行</param>
        ''' <param name="prmCol">列</param>
        ''' <remarks></remarks>
        Public Sub selectCell(ByVal prmRow As Short, ByVal prmCol As Short)
            Dim cell1 As String = convFromR1C1(prmRow, prmCol)

            _app.Range(cell1).Select()
        End Sub

        '===============================================================================
        '指定した行を選択する
        '===============================================================================
        ''' <summary>
        ''' 指定した行を選択する
        ''' </summary>
        ''' <param name="prmRow">行</param>
        ''' <param name="prmRow2">列</param>
        ''' <remarks></remarks>
        Public Sub selectRow(ByVal prmRow As Short, Optional ByVal prmRow2 As Short = -9)
            Dim wkRow2 As Short = prmRow2
            If wkRow2 = -9 Then wkRow2 = prmRow

            _sheet.Rows(cbnCellStr(prmRow, wkRow2)).Select()
        End Sub

        '===============================================================================
        '行コピー
        'prmRow2を省略した場合は、prmRowと同値を採用する
        '===============================================================================
        ''' <summary>
        ''' 行コピー　prmRow2を省略した場合は、prmRowと同値を採用する
        ''' </summary>
        ''' <param name="prmRow">開始行</param>
        ''' <param name="prmRow2">終了行</param>
        ''' <remarks></remarks>
        Public Sub copyRow(ByVal prmRow As Short, Optional ByVal prmRow2 As Short = -9)
            Dim wkRow2 As Short = prmRow2
            If wkRow2 = -9 Then wkRow2 = prmRow

            _sheet.Rows(cbnCellStr(prmRow, wkRow2)).Copy()
        End Sub

        '===============================================================================
        '列コピー
        'prmCol2を省略した場合は、prmColと同値を採用する
        '===============================================================================
        ''' <summary>
        ''' 列コピー　prmCol2を省略した場合は、prmColと同値を採用する
        ''' </summary>
        ''' <param name="prmCol">開始列</param>
        ''' <param name="prmCol2">終了列</param>
        ''' <remarks></remarks>
        Public Sub copyCol(ByVal prmCol As Short, Optional ByVal prmCol2 As Short = -9)
            Dim wkCol2 As Short = prmCol2
            If wkCol2 = -9 Then wkCol2 = prmCol

            _sheet.Columns(convColFromR1C1(prmCol)).Copy()
        End Sub

        '===============================================================================
        'レンジコピー
        'prmRow2/prmCol2を省略した場合は、それぞれprmRow/prmColと同値を採用する
        '===============================================================================
        ''' <summary>
        ''' レンジコピー　prmRow2/prmCol2を省略した場合は、それぞれprmRow/prmColと同値を採用する
        ''' </summary>
        ''' <param name="prmRow">開始行</param>
        ''' <param name="prmCol">開始列</param>
        ''' <param name="prmRow2">終了行</param>
        ''' <param name="prmCol2">終了列</param>
        ''' <remarks></remarks>
        Public Sub copyRange(ByVal prmRow As Short, ByVal prmCol As Short,
                                       Optional ByVal prmRow2 As Short = -9, Optional ByVal prmCol2 As Short = -9)
            Dim wkRow2 As Short = prmRow2
            Dim wkCol2 As Short = prmCol2
            If wkRow2 = -9 Then wkRow2 = prmRow
            If wkCol2 = -9 Then wkCol2 = prmCol

            Dim cell1 As String = Me.convFromR1C1(prmRow, prmCol)
            Dim cell2 As String = Me.convFromR1C1(wkRow2, wkCol2)
            _app.Range(cbnCellStr(cell1, cell2)).Copy()
        End Sub

        '===============================================================================
        'セルコピー
        '===============================================================================
        ''' <summary>
        ''' セルコピー
        ''' </summary>
        ''' <param name="prmRow">行</param>
        ''' <param name="prmCol">列</param>
        ''' <remarks></remarks>
        Public Sub copyCell(ByVal prmRow As Short, ByVal prmCol As Short)
            Dim cell1 As String = convFromR1C1(prmRow, prmCol)

            _app.Range(cell1).Copy()
        End Sub

        '===============================================================================
        '貼り付け
        '===============================================================================
        ''' <summary>
        ''' 貼り付け
        ''' </summary>
        ''' <param name="prmRow">行</param>
        ''' <param name="prmCol">列</param>
        ''' <remarks></remarks>
        Public Sub paste(ByVal prmRow As Short, ByVal prmCol As Short)
            Dim svRow As Short = _app.ActiveCell.Row
            Dim svCol As Short = _app.ActiveCell.Column
            selectCell(prmRow, prmCol)
            _sheet.Paste()
            selectCell(svRow, svCol)
        End Sub

        '===============================================================================
        '書式貼り付け
        '===============================================================================
        ''' <summary>
        ''' 書式貼り付け
        ''' </summary>
        ''' <param name="prmRow">行</param>
        ''' <param name="prmCol">列</param>
        ''' <remarks></remarks>
        Public Sub pasteFormat(ByVal prmRow As Short, ByVal prmCol As Short)
            Dim cell1 As String = convFromR1C1(prmRow, prmCol)

            _app.Range(cell1).PasteSpecial(Paste:=xlPasteFormats,
                                       Operation:=xlNone,
                                       SkipBlanks:=False,
                                       Transpose:=False)

        End Sub

        '===============================================================================
        '値貼り付け
        '===============================================================================
        ''' <summary>
        ''' 値貼り付け
        ''' </summary>
        ''' <param name="prmRow">行</param>
        ''' <param name="prmCol">列</param>
        ''' <remarks></remarks>
        Public Sub pasteValue(ByVal prmRow As Short, ByVal prmCol As Short)
            Dim cell1 As String = convFromR1C1(prmRow, prmCol)

            _app.Range(cell1).PasteSpecial(Paste:=xlPasteValues,
                                       Operation:=xlNone,
                                       SkipBlanks:=False,
                                       Transpose:=False)

        End Sub

        '===============================================================================
        '行挿入貼り付け
        'prmRow2を省略した場合は、prmRowと同値を採用する
        '===============================================================================
        ''' <summary>
        ''' 行挿入貼り付け　prmRow2を省略した場合は、prmRowと同値を採用する
        ''' </summary>
        ''' <param name="prmRow">開始行</param>
        ''' <param name="prmRow2">終了行</param>
        ''' <remarks></remarks>
        Public Sub insertPasteRow(ByVal prmRow As Short, Optional ByVal prmRow2 As Short = -9)
            Dim wkRow2 As Short = prmRow2
            If wkRow2 = -9 Then wkRow2 = prmRow

            _sheet.Rows(cbnCellStr(prmRow, wkRow2)).Insert(Shift:=xlDown)
        End Sub

        '===============================================================================
        '列挿入貼り付け
        'prmCol2を省略した場合は、prmColと同値を採用する
        '===============================================================================
        ''' <summary>
        ''' 列挿入貼り付け　prmCol2を省略した場合は、prmColと同値を採用する
        ''' </summary>
        ''' <param name="prmCol">開始列</param>
        ''' <param name="prmCol2">終了列</param>
        ''' <remarks></remarks>
        Public Sub insertPasteCol(ByVal prmCol As Short, Optional ByVal prmCol2 As Short = -9)
            Dim wkCol2 As Short = prmCol2
            If wkCol2 = -9 Then wkCol2 = prmCol

            _sheet.Columns(cbnCellStr(convColFromR1C1(prmCol),
                                      convColFromR1C1(wkCol2))).Insert(Shift:=xlToRight)

        End Sub

        '===============================================================================
        '行削除
        'prmRow2を省略した場合は、prmRowと同値を採用する
        '===============================================================================
        ''' <summary>
        ''' 行削除　prmRow2を省略した場合は、prmRowと同値を採用する
        ''' </summary>
        ''' <param name="prmRow">開始行</param>
        ''' <param name="prmRow2">終了行</param>
        ''' <remarks></remarks>
        Public Sub deleteRow(ByVal prmRow As Short, Optional ByVal prmRow2 As Short = -9)
            Dim wkRow2 As Short = prmRow2
            If wkRow2 = -9 Then wkRow2 = prmRow

            _sheet.Rows(cbnCellStr(prmRow, wkRow2)).Delete(Shift:=xlUp)

        End Sub

        '===============================================================================
        '行追加
        'prmRow2を省略した場合は、prmRowと同値を採用する
        '===============================================================================
        ''' <summary>
        ''' 行追加　prmRow2を省略した場合は、prmRowと同値を採用する
        ''' </summary>
        ''' <param name="prmRow">開始行</param>
        ''' <param name="prmRow2">終了行</param>
        ''' <remarks></remarks>
        Public Sub insertRow(ByVal prmRow As Short, Optional ByVal prmRow2 As Short = -9)
            Dim wkRow2 As Short = prmRow2
            If wkRow2 = -9 Then wkRow2 = prmRow

            _sheet.Rows(cbnCellStr(prmRow, wkRow2)).Insert(Shift:=xlDown)

        End Sub

        '===============================================================================
        '列削除
        'prmCol2を省略した場合は、prmColと同値を採用する
        '===============================================================================
        ''' <summary>
        ''' 列削除　prmCol2を省略した場合は、prmColと同値を採用する
        ''' </summary>
        ''' <param name="prmCol">開始列</param>
        ''' <param name="prmCol2">終了列</param>
        ''' <remarks></remarks>
        Public Sub deleteCol(ByVal prmCol As Short, Optional ByVal prmCol2 As Short = -9)
            Dim wkCol2 As Short = prmCol2
            If wkCol2 = -9 Then wkCol2 = prmCol

            _sheet.Columns(cbnCellStr(convColFromR1C1(prmCol),
                                      convColFromR1C1(wkCol2))).Delete(Shift:=xlToLeft)

        End Sub

        '===============================================================================
        '列追加
        'prmCol2を省略した場合は、prmColと同値を採用する
        '===============================================================================
        ''' <summary>
        ''' 列追加　prmCol2を省略した場合は、prmColと同値を採用する
        ''' </summary>
        ''' <param name="prmCol">開始列</param>
        ''' <param name="prmCol2">終了列</param>
        ''' <remarks></remarks>
        Public Sub insertCol(ByVal prmCol As Short, Optional ByVal prmCol2 As Short = -9)
            Dim wkCol2 As Short = prmCol2
            If wkCol2 = -9 Then wkCol2 = prmCol

            _sheet.Columns(cbnCellStr(convColFromR1C1(prmCol),
                                      convColFromR1C1(wkCol2))).Insert(Shift:=xlToRight)

        End Sub

        '===============================================================================
        'セル値取得(セル上に表示されているもの：カンマ編集済み文字列や、￥編集済み文字列など)
        '===============================================================================
        ''' <summary>
        ''' セル値取得(セル上に表示されているもの：カンマ編集済み文字列や、￥編集済み文字列など)
        ''' </summary>
        ''' <param name="prmRow">行</param>
        ''' <param name="prmCol">列</param>
        ''' <returns>取得文字列</returns>
        ''' <remarks></remarks>
        Public Function getText(ByVal prmRow As Short, ByVal prmCol As Short) As String
            Dim cell1 As String = convFromR1C1(prmRow, prmCol)
            Return _app.Range(cell1).Text
        End Function

        '===============================================================================
        'セル値取得(セルに実際に格納されている値→式の場合は式の結果)
        '===============================================================================
        ''' <summary>
        ''' セル値取得(セルに実際に格納されている値→式の場合は式の結果)
        ''' </summary>
        ''' <param name="prmRow">行</param>
        ''' <param name="prmCol">列</param>
        ''' <returns>取得文字列</returns>
        ''' <remarks></remarks>
        Public Function getValue(ByVal prmRow As Short, ByVal prmCol As Short) As String
            Dim cell1 As String = convFromR1C1(prmRow, prmCol)
            Return _app.Range(cell1).Value
        End Function

        '===============================================================================
        'セル値取得(セルに格納されている式自体)
        '===============================================================================
        ''' <summary>
        ''' セル値取得(セルに格納されている式自体)
        ''' </summary>
        ''' <param name="prmRow">行</param>
        ''' <param name="prmCol">列</param>
        ''' <returns>取得文字列</returns>
        ''' <remarks></remarks>
        Public Function getFormula(ByVal prmRow As Short, ByVal prmCol As Short) As String
            Dim cell1 As String = convFromR1C1(prmRow, prmCol)
            Return _app.Range(cell1).Formula
        End Function

        '===============================================================================
        'セル値設定(セルに格納する値→式を格納する場合はsetFormulaを利用のこと)
        '===============================================================================
        ''' <summary>
        ''' セル値設定(セルに格納する値→式を格納する場合はsetFormulaを利用のこと)
        ''' </summary>
        ''' <param name="prmText">設定文字列</param>
        ''' <param name="prmRow">行</param>
        ''' <param name="prmCol">列</param>
        ''' <remarks></remarks>
        Public Sub setValue(ByVal prmText As String, ByVal prmRow As Short, ByVal prmCol As Short)
            Dim cell1 As String = convFromR1C1(prmRow, prmCol)
            _app.Range(cell1).Value = prmText
        End Sub
        ''' <summary>
        ''' セル値設定(Ａ１形式)
        ''' </summary>
        ''' <param name="prmText">設定文字列</param>
        ''' <param name="prmCell">出力セル（Ａ１形式）</param>
        Public Sub setValueA1(ByVal prmText As String, ByVal prmCell As String)
            _app.Range(prmCell).Value = prmText
        End Sub

        '===============================================================================
        'セル値設定(セルに格納する式→値を格納する場合はsetValueを利用のこと)
        '===============================================================================
        ''' <summary>
        ''' セル値設定(セルに格納する式→値を格納する場合はsetValueを利用のこと)
        ''' </summary>
        ''' <param name="prmFormula">設定式</param>
        ''' <param name="prmRow">行</param>
        ''' <param name="prmCol">列</param>
        ''' <remarks></remarks>
        Public Sub setFormula(ByVal prmFormula As String, ByVal prmRow As Short, ByVal prmCol As Short)
            Dim cell1 As String = convFromR1C1(prmRow, prmCol)
            _app.Range(cell1).Formula = prmFormula
        End Sub

        '===============================================================================
        '罫線設定
        'prmRow2/prmCol2を省略した場合は、それぞれprmRow/prmColと同値を採用する
        '===============================================================================
        ''' <summary>
        ''' 罫線設定　prmRow2/prmCol2を省略した場合は、それぞれprmRow/prmColと同値を採用する
        ''' </summary>
        ''' <param name="prmLineVO">罫線書式を格納したVO</param>
        ''' <param name="prmRow">開始行</param>
        ''' <param name="prmCol">開始列</param>
        ''' <param name="prmRow2">終了行</param>
        ''' <param name="prmCol2">終了列</param>
        ''' <remarks></remarks>
        Public Sub drawRuledLine(ByVal prmLineVO As LineVO, ByVal prmRow As Short, ByVal prmCol As Short,
                                       Optional ByVal prmRow2 As Short = -9, Optional ByVal prmCol2 As Short = -9)
            Try

                Dim wkRow2 As Short = prmRow2
                Dim wkCol2 As Short = prmCol2
                If wkRow2 = -9 Then wkRow2 = prmRow
                If wkCol2 = -9 Then wkCol2 = prmCol

                Dim cell1 As String = Me.convFromR1C1(prmRow, prmCol)
                Dim cell2 As String = Me.convFromR1C1(wkRow2, wkCol2)
                Dim r As Excel.Range = _app.Range(cbnCellStr(cell1, cell2))
                Try
                    If prmLineVO.Left <> LineVO.LineType.Null Then setLine(r, xlEdgeLeft, prmLineVO.Left)
                    If prmLineVO.Top <> LineVO.LineType.Null Then setLine(r, xlEdgeTop, prmLineVO.Top)
                    If prmLineVO.Right <> LineVO.LineType.Null Then setLine(r, xlEdgeRight, prmLineVO.Right)
                    If prmLineVO.Bottom <> LineVO.LineType.Null Then setLine(r, xlEdgeBottom, prmLineVO.Bottom)
                    If prmLineVO.VerticalMiddle <> LineVO.LineType.Null Then setLine(r, xlInsideVertical, prmLineVO.VerticalMiddle)
                    If prmLineVO.HorizontalMiddle <> LineVO.LineType.Null Then setLine(r, xlInsideHorizontal, prmLineVO.HorizontalMiddle)
                Catch lex As Exception
                Finally
                    Me.releaseCom(r) 'COM開放
                End Try
            Catch ex As Exception
            End Try

        End Sub
        '罫線設定サブルーチン
        Private Sub setLine(ByRef prmRefRange As Excel.Range, ByVal prmLineEdge As Short, ByVal prmLinePos As LineVO.LineType)
            With prmRefRange.Borders(prmLineEdge)
                Select Case prmLinePos
                    Case LineVO.LineType.BrokenL
                        .LineStyle = xlContinuous
                        .Weight = xlHairline
                    Case LineVO.LineType.NomalL
                        .LineStyle = xlContinuous
                        .Weight = xlThin
                    Case LineVO.LineType.BoldL
                        .LineStyle = xlContinuous
                        .Weight = xlMedium
                    Case LineVO.LineType.DoubleL
                        .LineStyle = xlDouble
                        .Weight = xlThick
                    Case LineVO.LineType.None
                        .LineStyle = xlNone
                End Select
            End With
        End Sub

        '===============================================================================
        '背景色設定
        'prmRow2/prmCol2を省略した場合は、それぞれprmRow/prmColと同値を採用する
        '                                                     2007.08.10 add by Laevigata, Inc.
        '===============================================================================
        ''' <summary>
        ''' 背景色設定　prmRow2/prmCol2を省略した場合は、それぞれprmRow/prmColと同値を採用する
        ''' </summary>
        ''' <param name="prmColor">設定する背景色</param>
        ''' <param name="prmRow">開始行</param>
        ''' <param name="prmCol">開始列</param>
        ''' <param name="prmRow2">終了行</param>
        ''' <param name="prmCol2">終了列</param>
        ''' <remarks>2007.08.10 add by Laevigata, Inc.</remarks>
        Public Sub setColor(ByVal prmColor As System.Drawing.Color, ByVal prmRow As Short, ByVal prmCol As Short,
                                       Optional ByVal prmRow2 As Short = -9, Optional ByVal prmCol2 As Short = -9)
            Dim wkRow2 As Short = prmRow2
            Dim wkCol2 As Short = prmCol2
            If wkRow2 = -9 Then wkRow2 = prmRow
            If wkCol2 = -9 Then wkCol2 = prmCol

            Dim cell1 As String = Me.convFromR1C1(prmRow, prmCol)
            Dim cell2 As String = Me.convFromR1C1(wkRow2, wkCol2)
            _app.Range(cbnCellStr(cell1, cell2)).Interior.Color = RGB(prmColor.R, prmColor.G, prmColor.B)

        End Sub

        '===============================================================================
        'フォント設定
        'prmRow2/prmCol2を省略した場合は、それぞれprmRow/prmColと同値を採用する
        '                                                     2007.08.10 add by Laevigata, Inc.
        '===============================================================================
        ''' <summary>
        ''' フォント設定　prmRow2/prmCol2を省略した場合は、それぞれprmRow/prmColと同値を採用する
        ''' </summary>
        ''' <param name="prmFont">設定するフォント</param>
        ''' <param name="prmColor">設定するフォント色</param>
        ''' <param name="prmRow">開始行</param>
        ''' <param name="prmCol">開始列</param>
        ''' <param name="prmRow2">終了行</param>
        ''' <param name="prmCol2">終了列</param>
        ''' <remarks>2007.08.10 add by Laevigata, Inc.</remarks>
        Public Sub setFont(ByVal prmFont As System.Drawing.Font, ByVal prmColor As System.Drawing.Color,
                           ByVal prmRow As Short, ByVal prmCol As Short,
                           Optional ByVal prmRow2 As Short = -9, Optional ByVal prmCol2 As Short = -9)
            Dim wkRow2 As Short = prmRow2
            Dim wkCol2 As Short = prmCol2
            If wkRow2 = -9 Then wkRow2 = prmRow
            If wkCol2 = -9 Then wkCol2 = prmCol

            Dim cell1 As String = Me.convFromR1C1(prmRow, prmCol)
            Dim cell2 As String = Me.convFromR1C1(wkRow2, wkCol2)
            With _app.Range(cbnCellStr(cell1, cell2)).Font
                .Name = prmFont.Name                                'フォント名
                .Size = prmFont.SizeInPoints                        'サイズ
                .Strikethrough = prmFont.Strikeout                  '打消し線
                .Underline = prmFont.Underline                      '下線
                .Color = RGB(prmColor.R, prmColor.G, prmColor.B)    '前景色
            End With

        End Sub

        '===============================================================================
        '表示形式設定
        'prmRow2/prmCol2を省略した場合は、それぞれprmRow/prmColと同値を採用する
        'prmNumberFormatLocal   Excelマクロ同一記述のこと
        '---例---
        '標準                    "G/標準"                                          
        '数値                    "0_ "                                             
        '通貨                    "\#,##0;\-#,##0"                                  
        '会計                    "_ \* #,##0_ ;_ \* -#,##0_ ;_ \* ""-""_ ;_ @_ "   
        '会計「\」なし           "_ * #,##0_ ;_ * -#,##0_ ;_ * ""-""_ ;_ @_ "      
        '日付１                  "yyyy/m/d"                                        
        '日付２                  "yyyy/mm/dd"                                      
        '日付３                  "yy/mm/dd"                                        
        '時刻(13:30)             "h:mm;@"                                          
        '時刻(1:30 PM)           "[$-409]h:mm AM/PM;@"                             
        '時刻(13:30:50)          "h:mm:ss;@"                                       
        '時刻(1:30:55 PM)        "[$-409]h:mm:ss AM/PM;@"                          
        'パーセンテージ          "0%"                                              
        'パーセンテージ(少数2桁) "0.00%"                                           
        '文字列                  "@"                                               
        '                                                     2007.08.10 add by Laevigata, Inc.
        '===============================================================================
        ''' <summary>
        ''' 表示形式設定　prmRow2/prmCol2を省略した場合は、それぞれprmRow/prmColと同値を採用する
        ''' </summary>
        ''' <param name="prmNumberFormatLocal">表示書式　例："G/標準" (Excelマクロ同一記述のこと)</param>
        ''' <param name="prmRow">開始行</param>
        ''' <param name="prmCol">開始列</param>
        ''' <param name="prmRow2">終了行</param>
        ''' <param name="prmCol2">終了列</param>
        ''' <remarks>2007.08.10 add by Laevigata, Inc.</remarks>
        Public Sub setNumberFormat(ByVal prmNumberFormatLocal As String, ByVal prmRow As Short, ByVal prmCol As Short,
                                       Optional ByVal prmRow2 As Short = -9, Optional ByVal prmCol2 As Short = -9)
            Dim wkRow2 As Short = prmRow2
            Dim wkCol2 As Short = prmCol2
            If wkRow2 = -9 Then wkRow2 = prmRow
            If wkCol2 = -9 Then wkCol2 = prmCol

            Dim cell1 As String = Me.convFromR1C1(prmRow, prmCol)
            Dim cell2 As String = Me.convFromR1C1(wkRow2, wkCol2)
            _app.Range(cbnCellStr(cell1, cell2)).NumberFormatLocal = prmNumberFormatLocal

        End Sub

        '===============================================================================
        '横位置設定(左寄せ/中央寄せ/右寄せ/選択範囲で中央寄せ)
        'prmRow2/prmCol2を省略した場合は、それぞれprmRow/prmColと同値を採用する
        '===============================================================================
        ''' <summary>
        ''' 横位置設定(左寄せ/中央寄せ/右寄せ/選択範囲で中央寄せ)　prmRow2/prmCol2を省略した場合は、それぞれprmRow/prmColと同値を採用する
        ''' </summary>
        ''' <param name="prmPos">横位置を表す列挙体</param>
        ''' <param name="prmRow">開始行</param>
        ''' <param name="prmCol">開始列</param>
        ''' <param name="prmRow2">終了行</param>
        ''' <param name="prmCol2">終了列</param>
        ''' <remarks></remarks>
        Public Sub setHorizontalPos(ByVal prmPos As UtilExcelHandler.HorizontalAlignment, ByVal prmRow As Short, ByVal prmCol As Short,
                          Optional ByVal prmRow2 As Short = -9, Optional ByVal prmCol2 As Short = -9)
            Dim wkRow2 As Short = prmRow2
            Dim wkCol2 As Short = prmCol2
            If wkRow2 = -9 Then wkRow2 = prmRow
            If wkCol2 = -9 Then wkCol2 = prmCol

            Dim setVal As Short = xlLeft
            Select Case prmPos
                Case HorizontalAlignment.Left
                    setVal = xlLeft
                Case HorizontalAlignment.Center
                    setVal = xlCenter
                Case HorizontalAlignment.CenterAcrossSelection
                    setVal = xlCenterAcrossSelection
                Case HorizontalAlignment.Right
                    setVal = xlRight
            End Select

            Dim cell1 As String = Me.convFromR1C1(prmRow, prmCol)
            Dim cell2 As String = Me.convFromR1C1(wkRow2, wkCol2)
            _app.Range(cbnCellStr(cell1, cell2)).HorizontalAlignment = setVal

        End Sub

        '===============================================================================
        '縦位置設定(上寄せ/中央寄せ/下寄せ)
        'prmRow2/prmCol2を省略した場合は、それぞれprmRow/prmColと同値を採用する
        '===============================================================================
        ''' <summary>
        ''' 縦位置設定(上寄せ/中央寄せ/下寄せ)　prmRow2/prmCol2を省略した場合は、それぞれprmRow/prmColと同値を採用する
        ''' </summary>
        ''' <param name="prmPos">縦位置を表す列挙体</param>
        ''' <param name="prmRow">開始行</param>
        ''' <param name="prmCol">開始列</param>
        ''' <param name="prmRow2">終了行</param>
        ''' <param name="prmCol2">終了列</param>
        ''' <remarks></remarks>
        Public Sub setVerticalPos(ByVal prmPos As UtilExcelHandler.VerticalAlignment, ByVal prmRow As Short, ByVal prmCol As Short,
                                  Optional ByVal prmRow2 As Short = -9, Optional ByVal prmCol2 As Short = -9)
            Dim wkRow2 As Short = prmRow2
            Dim wkCol2 As Short = prmCol2
            If wkRow2 = -9 Then wkRow2 = prmRow
            If wkCol2 = -9 Then wkCol2 = prmCol

            Dim setVal As Short = xlTop
            Select Case prmPos
                Case VerticalAlignment.Top
                    setVal = xlTop
                Case VerticalAlignment.Center
                    setVal = xlCenter
                Case VerticalAlignment.Bottom
                    setVal = xlBottom
            End Select

            Dim cell1 As String = Me.convFromR1C1(prmRow, prmCol)
            Dim cell2 As String = Me.convFromR1C1(wkRow2, wkCol2)
            _app.Range(cbnCellStr(cell1, cell2)).VerticalAlignment = setVal


        End Sub

        '===============================================================================
        'セル結合
        'prmRow2/prmCol2を省略した場合は、それぞれprmRow/prmColと同値を採用する
        '===============================================================================
        ''' <summary>
        ''' セル結合　prmRow2/prmCol2を省略した場合は、それぞれprmRow/prmColと同値を採用する
        ''' </summary>
        ''' <param name="prmRow">開始行</param>
        ''' <param name="prmCol">開始列</param>
        ''' <param name="prmRow2">終了行</param>
        ''' <param name="prmCol2">終了列</param>
        ''' <remarks></remarks>
        Public Sub combineCell(ByVal prmRow As Short, ByVal prmCol As Short,
                                       Optional ByVal prmRow2 As Short = -9, Optional ByVal prmCol2 As Short = -9)
            Dim wkRow2 As Short = prmRow2
            Dim wkCol2 As Short = prmCol2
            If wkRow2 = -9 Then wkRow2 = prmRow
            If wkCol2 = -9 Then wkCol2 = prmCol

            Dim cell1 As String = Me.convFromR1C1(prmRow, prmCol)
            Dim cell2 As String = Me.convFromR1C1(wkRow2, wkCol2)
            _app.Range(cbnCellStr(cell1, cell2)).MergeCells = True

        End Sub

        '===============================================================================
        'ウィンドウ固定
        '===============================================================================
        ''' <summary>
        ''' ウィンドウ固定
        ''' </summary>
        ''' <param name="prmRow">行</param>
        ''' <param name="prmCol">列</param>
        ''' <remarks></remarks>
        Public Sub freezeWindow(ByVal prmRow As Short, ByVal prmCol As Short)
            _app.ActiveWindow.FreezePanes = True
        End Sub

        '===============================================================================
        'ヘッダー設定
        'prmLeftStr/prmMidStr/prmRightStr   Excelマクロ同一記述のこと
        '---例---
        'ファイル名(シート名) "&F(&A)"
        'ページ数/総ページ数  "&P/&N"
        '日付 時刻 印刷       "&D　&T　印刷"
        '===============================================================================
        ''' <summary>
        ''' ヘッダー設定　Excelマクロ同一記述のこと
        ''' </summary>
        ''' <param name="prmLeftStr">左ヘッダ</param>
        ''' <param name="prmMidStr">中央ヘッダ</param>
        ''' <param name="prmRightStr">右ヘッダ</param>
        ''' <remarks></remarks>
        Public Sub setHeader(ByVal prmLeftStr As String, ByVal prmMidStr As String, ByVal prmRightStr As String)
            With _sheet.PageSetup
                .LeftHeader = prmLeftStr
                .CenterHeader = prmMidStr
                .RightHeader = prmRightStr
            End With
        End Sub

        '===============================================================================
        'フッター設定
        'prmLeftStr/prmMidStr/prmRightStr   Excelマクロ同一記述のこと
        '---例---
        'ファイル名(シート名) "&F(&A)"
        'ページ数/総ページ数  "&P/&N"
        '日付 時刻 印刷       "&D　&T　印刷"
        '===============================================================================
        ''' <summary>
        ''' フッター設定　Excelマクロ同一記述のこと
        ''' </summary>
        ''' <param name="prmLeftStr">左フッタ</param>
        ''' <param name="prmMidStr">中央フッタ</param>
        ''' <param name="prmRightStr">右フッタ</param>
        ''' <remarks></remarks>
        Public Sub setFooter(ByVal prmLeftStr As String, ByVal prmMidStr As String, ByVal prmRightStr As String)
            With _sheet.PageSetup
                .LeftFooter = prmLeftStr
                .CenterFooter = prmMidStr
                .RightFooter = prmRightStr
            End With
        End Sub

        '===============================================================================
        'ページ設定
        ' prmPageVO ページセットアップVO
        '                                                     2007.08.10 add by Laevigata, Inc.
        '===============================================================================
        ''' <summary>
        ''' ページ設定
        ''' </summary>
        ''' <param name="prmPageVO">ページセットアップVO</param>
        ''' <remarks>2007.08.10 add by Laevigata, Inc.</remarks>
        Public Sub setUpPageDefine(ByVal prmPageVO As PageSetUpVO)
            With _sheet.PageSetup
                .PrintTitleRows = prmPageVO.PrintTitleRows                          '印刷行タイトル
                .PrintTitleColumns = prmPageVO.PrintTitleColumns                    '印刷列タイトル
                .PrintArea = prmPageVO.PrintArea                                    '印刷範囲
                .LeftMargin = _app.CentimetersToPoints(prmPageVO.LeftMargin)        '左余白(センチ：Excel同様指定)
                .RightMargin = _app.CentimetersToPoints(prmPageVO.RightMargin)      '右余白(センチ：Excel同様指定)
                .TopMargin = _app.CentimetersToPoints(prmPageVO.TopMargin)          '上余白(センチ：Excel同様指定)
                .BottomMargin = _app.CentimetersToPoints(prmPageVO.BottomMargin)    '下余白(センチ：Excel同様指定)
                .HeaderMargin = _app.CentimetersToPoints(prmPageVO.HeaderMargin)    'ヘッダ余白(センチ：Excel同様指定)
                .FooterMargin = _app.CentimetersToPoints(prmPageVO.FooterMargin)    'フッダ余白(センチ：Excel同様指定)
                .Orientation = prmPageVO.Orientation                                'ページ向き(縦横)
                .PaperSize = prmPageVO.PaperSize                                    '用紙サイズ
                .Zoom = prmPageVO.Zoom                                              '拡大縮小率
            End With

        End Sub

        '===============================================================================
        'オートフィルタ
        '===============================================================================
        ''' <summary>
        ''' オートフィルタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub AutoFilter()
            Dim svRow As Short = _app.ActiveCell.Row
            Dim svCol As Short = _app.ActiveCell.Column

            selectCell(1, 1)
            _app.Selection.AutoFilter()
            selectCell(svRow, svCol)

        End Sub

        '===============================================================================
        'シートコピー
        '===============================================================================
        ''' <summary>
        ''' シートコピー(後ろに)
        ''' </summary>
        ''' <param name="prmNewSheetName">新しいシート名</param>
        ''' <remarks></remarks>
        Public Sub copySheet(Optional ByVal prmNewSheetName As String = "org")
            _sheet.Copy(After:=_sheet)
            _sheets(_sheet.Index + 1).Name = prmNewSheetName
        End Sub

        ''' <summary>
        ''' シートコピー(前に)
        ''' </summary>
        ''' <param name="prmNewSheetName">新しいシート名</param>
        ''' <remarks></remarks>
        Public Sub copySheetBefore(Optional ByVal prmNewSheetName As String = "org")
            _sheet.Copy(Before:=_sheet)
            _sheets(_sheet.Index - 1).Name = prmNewSheetName
        End Sub

        ''' <summary>
        ''' シートコピー(末尾へ)
        ''' </summary>
        ''' <param name="prmNewSheetName">新しいシート名</param>
        ''' <remarks>2010.02.17 Created by Laevigata, Inc.</remarks>
        Public Sub copySheetOnLast(Optional ByVal prmNewSheetName As String = "newName")
            _sheet.Copy(After:=_sheets(_sheets.Count))
            _sheets(_sheets.Count).Name = prmNewSheetName
        End Sub

        '===============================================================================
        'シート削除
        '===============================================================================
        ''' <summary>
        ''' シート削除
        ''' </summary>
        ''' <param name="prmSheetName">削除するシート名</param>
        ''' <remarks></remarks>
        Public Sub deleteSheet(ByVal prmSheetName As String)
            Dim curSht As String = _sheet.Name
            _sheets(prmSheetName).Select()
            '-->2010.03.04 upd by Laevigata, Inc.
            '_app.Windows(1).SelectedSheets.Delete()
            Dim ew As Object = _app.Windows(1)
            Try
                Dim ss As Excel.Sheets = ew.SelectedSheets
                Try
                    ss.Delete()
                Finally
                    Me.releaseCom(ss)
                End Try
            Finally
                Me.releaseCom(ew)
            End Try
            '<--2010.03.04
            _sheets(curSht).Select()
        End Sub

        '===============================================================================
        '改ページ
        '===============================================================================
        ''' <summary>
        ''' 指定行列の右下に改ページを挿入する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub breakPage(ByVal prmRow As Short, ByVal prmCol As Short)
            Dim cell As String = Me.convFromR1C1(prmRow + 1, prmCol + 1)
            _sheet.HPageBreaks.Add(Before:=_app.Range(cell))
            _sheet.VPageBreaks.Add(Before:=_app.Range(cell))
        End Sub

        '===============================================================================
        'テキストボックに文字出力
        '===============================================================================
        ''' <summary>
        ''' テキストボックに文字出力
        ''' </summary>
        ''' <param name="prmShapeTextBoxNm">テキストボック名</param>
        ''' <param name="prmTextVal">出力文字列</param>
        ''' <remarks></remarks>
        Public Sub setShapeTextBox(ByVal prmShapeTextBoxNm As String, ByVal prmTextVal As String)
            Dim shps As Excel.Shapes = _sheet.Shapes
            Try
                Dim shpItem As Object = shps.Item(prmShapeTextBoxNm)
                Try
                    Dim textFrm As Object = shpItem.TextFrame
                    Try
                        Dim xlsChars As Object = textFrm.Characters
                        Try
                            xlsChars.Text = prmTextVal
                        Finally
                            Me.releaseCom(xlsChars)
                        End Try
                    Finally
                        Me.releaseCom(textFrm)
                    End Try
                Finally
                    Me.releaseCom(shpItem)
                End Try
            Finally
                Me.releaseCom(shps)
            End Try

        End Sub

        '===============================================================================
        'ボックス背景色の塗りつぶし
        '===============================================================================
        ''' <summary>
        ''' 背景色の塗りつぶし
        ''' </summary>
        ''' <param name="prmShapeTextBoxNm">ボックス名</param>
        ''' <param name="prmColorRed">RGBの赤</param>
        ''' <param name="prmColorGreen">RGBの緑</param>
        ''' <param name="prmColorBlue">RGBの青</param>
        ''' <remarks></remarks>
        Public Sub paintShape(ByVal prmShapeTextBoxNm As String,
                                    Optional ByVal prmColorRed As Short = 0,
                                    Optional ByVal prmColorGreen As Short = 0,
                                    Optional ByVal prmColorBlue As Short = 0)
            Dim shps As Excel.Shapes = _sheet.Shapes
            Try
                Dim shpsItem As Object = shps.Item(prmShapeTextBoxNm)
                Try
                    Dim shpsFill As Object = shpsItem.Fill
                    Try
                        Dim frColor As Object = shpsFill.ForeColor
                        Try
                            frColor.RGB = RGB(prmColorRed, prmColorGreen, prmColorBlue)
                            shpsFill.Visible = -1 'msoTrue
                            shpsFill.Solid()
                        Finally
                            Me.releaseCom(frColor)
                        End Try
                    Finally
                        Me.releaseCom(shpsFill)
                    End Try
                Finally
                    Me.releaseCom(shpsItem)
                End Try
            Finally
                Me.releaseCom(shps)
            End Try

        End Sub

    End Class

    '===============================================================================
    '
    '  内部使用クラス
    '    （クラス名）    ExcelFunc
    '    （処理機能名）      UtilExcelHandlerに付随する部品クラス群において使用する
    '                        内部メソッドを定義する。
    '    （本MDL使用前提）   UtilExcelHandlerに付随する部品クラス群と対で使用する
    '                        継承元とするためのクラスであるため、インスタンス化を禁止する
    '    （備考）            上記使用前提よりUtilExcelHandlerと同一SRC上に定義
    '
    '===============================================================================
    '  履歴  名前          日  付      マーク      内容
    '-------------------------------------------------------------------------------
    '  (1)   Laevigata, Inc.    2007/08/10              新規
    '-------------------------------------------------------------------------------
    Public MustInherit Class ExcelFunc

        '===============================================================================
        '領域文字列取得(内部メソッド)
        '===============================================================================
        Protected Function cbnCellStr(ByVal prmCell1 As String, ByVal prmCell2 As String) As String
            Return prmCell1 & ":" & prmCell2
        End Function

        '===============================================================================
        '領域文字列分割(内部メソッド)
        '===============================================================================
        Protected Sub devCellStr(ByVal prmCellStr As String, ByRef prmRefCell1 As String, ByRef prmRefCell2 As String)
            prmRefCell1 = prmCellStr.Substring(0, prmCellStr.IndexOf(":"))
            prmRefCell2 = prmCellStr.Substring(prmCellStr.IndexOf(":") + 1)
        End Sub

        '===============================================================================
        '行番号列番号を[A1]形式に変換(内部メソッド)
        '===============================================================================
        '行列変換
        Protected Function convFromR1C1(ByVal prmRow As Short, ByVal prmCol As Short, Optional ByVal dollar As Boolean = False) As String

            Dim rowStr As String = ""
            Dim colStr As String = ""
            If prmRow > 65536 Then
                rowStr = "1"
            Else
                rowStr = CStr(prmRow)
            End If
            colStr = convColFromR1C1(prmCol)

            If dollar Then
                Return "$" & colStr & "$" & rowStr
            Else
                Return colStr & rowStr
            End If
        End Function
        '列のみ変換
        Protected Function convColFromR1C1(ByVal prmCol As Short) As String

            Dim colStr As String = ""
            Select Case prmCol
                Case 1 : colStr = "A"
                Case 2 : colStr = "B"
                Case 3 : colStr = "C"
                Case 4 : colStr = "D"
                Case 5 : colStr = "E"
                Case 6 : colStr = "F"
                Case 7 : colStr = "G"
                Case 8 : colStr = "H"
                Case 9 : colStr = "I"
                Case 10 : colStr = "J"
                Case 11 : colStr = "K"
                Case 12 : colStr = "L"
                Case 13 : colStr = "M"
                Case 14 : colStr = "N"
                Case 15 : colStr = "O"
                Case 16 : colStr = "P"
                Case 17 : colStr = "Q"
                Case 18 : colStr = "R"
                Case 19 : colStr = "S"
                Case 20 : colStr = "T"
                Case 21 : colStr = "U"
                Case 22 : colStr = "V"
                Case 23 : colStr = "W"
                Case 24 : colStr = "X"
                Case 25 : colStr = "Y"
                Case 26 : colStr = "Z"
                Case 27 : colStr = "AA"
                Case 28 : colStr = "AB"
                Case 29 : colStr = "AC"
                Case 30 : colStr = "AD"
                Case 31 : colStr = "AE"
                Case 32 : colStr = "AF"
                Case 33 : colStr = "AG"
                Case 34 : colStr = "AH"
                Case 35 : colStr = "AI"
                Case 36 : colStr = "AJ"
                Case 37 : colStr = "AK"
                Case 38 : colStr = "AL"
                Case 39 : colStr = "AM"
                Case 40 : colStr = "AN"
                Case 41 : colStr = "AO"
                Case 42 : colStr = "AP"
                Case 43 : colStr = "AQ"
                Case 44 : colStr = "AR"
                Case 45 : colStr = "AS"
                Case 46 : colStr = "AT"
                Case 47 : colStr = "AU"
                Case 48 : colStr = "AV"
                Case 49 : colStr = "AW"
                Case 50 : colStr = "AX"
                Case 51 : colStr = "AY"
                Case 52 : colStr = "AZ"
                Case 53 : colStr = "BA"
                Case 54 : colStr = "BB"
                Case 55 : colStr = "BC"
                Case 56 : colStr = "BD"
                Case 57 : colStr = "BE"
                Case 58 : colStr = "BF"
                Case 59 : colStr = "BG"
                Case 60 : colStr = "BH"
                Case 61 : colStr = "BI"
                Case 62 : colStr = "BJ"
                Case 63 : colStr = "BK"
                Case 64 : colStr = "BL"
                Case 65 : colStr = "BM"
                Case 66 : colStr = "BN"
                Case 67 : colStr = "BO"
                Case 68 : colStr = "BP"
                Case 69 : colStr = "BQ"
                Case 70 : colStr = "BR"
                Case 71 : colStr = "BS"
                Case 72 : colStr = "BT"
                Case 73 : colStr = "BU"
                Case 74 : colStr = "BV"
                Case 75 : colStr = "BW"
                Case 76 : colStr = "BX"
                Case 77 : colStr = "BY"
                Case 78 : colStr = "BZ"
                Case 79 : colStr = "CA"
                Case 80 : colStr = "CB"
                Case 81 : colStr = "CC"
                Case 82 : colStr = "CD"
                Case 83 : colStr = "CE"
                Case 84 : colStr = "CF"
                Case 85 : colStr = "CG"
                Case 86 : colStr = "CH"
                Case 87 : colStr = "CI"
                Case 88 : colStr = "CJ"
                Case 89 : colStr = "CK"
                Case 90 : colStr = "CL"
                Case 91 : colStr = "CM"
                Case 92 : colStr = "CN"
                Case 93 : colStr = "CO"
                Case 94 : colStr = "CP"
                Case 95 : colStr = "CQ"
                Case 96 : colStr = "CR"
                Case 97 : colStr = "CS"
                Case 98 : colStr = "CT"
                Case 99 : colStr = "CU"
                Case 100 : colStr = "CV"
                Case 101 : colStr = "CW"
                Case 102 : colStr = "CX"
                Case 103 : colStr = "CY"
                Case 104 : colStr = "CZ"
                Case 105 : colStr = "DA"
                Case 106 : colStr = "DB"
                Case 107 : colStr = "DC"
                Case 108 : colStr = "DD"
                Case 109 : colStr = "DE"
                Case 110 : colStr = "DF"
                Case 111 : colStr = "DG"
                Case 112 : colStr = "DH"
                Case 113 : colStr = "DI"
                Case 114 : colStr = "DJ"
                Case 115 : colStr = "DK"
                Case 116 : colStr = "DL"
                Case 117 : colStr = "DM"
                Case 118 : colStr = "DN"
                Case 119 : colStr = "DO"
                Case 120 : colStr = "DP"
                Case 121 : colStr = "DQ"
                Case 122 : colStr = "DR"
                Case 123 : colStr = "DS"
                Case 124 : colStr = "DT"
                Case 125 : colStr = "DU"
                Case 126 : colStr = "DV"
                Case 127 : colStr = "DW"
                Case 128 : colStr = "DX"
                Case 129 : colStr = "DY"
                Case 130 : colStr = "DZ"
                Case 131 : colStr = "EA"
                Case 132 : colStr = "EB"
                Case 133 : colStr = "EC"
                Case 134 : colStr = "ED"
                Case 135 : colStr = "EE"
                Case 136 : colStr = "EF"
                Case 137 : colStr = "EG"
                Case 138 : colStr = "EH"
                Case 139 : colStr = "EI"
                Case 140 : colStr = "EJ"
                Case 141 : colStr = "EK"
                Case 142 : colStr = "EL"
                Case 143 : colStr = "EM"
                Case 144 : colStr = "EN"
                Case 145 : colStr = "EO"
                Case 146 : colStr = "EP"
                Case 147 : colStr = "EQ"
                Case 148 : colStr = "ER"
                Case 149 : colStr = "ES"
                Case 150 : colStr = "ET"
                Case 151 : colStr = "EU"
                Case 152 : colStr = "EV"
                Case 153 : colStr = "EW"
                Case 154 : colStr = "EX"
                Case 155 : colStr = "EY"
                Case 156 : colStr = "EZ"
                Case 157 : colStr = "FA"
                Case 158 : colStr = "FB"
                Case 159 : colStr = "FC"
                Case 160 : colStr = "FD"
                Case 161 : colStr = "FE"
                Case 162 : colStr = "FF"
                Case 163 : colStr = "FG"
                Case 164 : colStr = "FH"
                Case 165 : colStr = "FI"
                Case 166 : colStr = "FJ"
                Case 167 : colStr = "FK"
                Case 168 : colStr = "FL"
                Case 169 : colStr = "FM"
                Case 170 : colStr = "FN"
                Case 171 : colStr = "FO"
                Case 172 : colStr = "FP"
                Case 173 : colStr = "FQ"
                Case 174 : colStr = "FR"
                Case 175 : colStr = "FS"
                Case 176 : colStr = "FT"
                Case 177 : colStr = "FU"
                Case 178 : colStr = "FV"
                Case 179 : colStr = "FW"
                Case 180 : colStr = "FX"
                Case 181 : colStr = "FY"
                Case 182 : colStr = "FZ"
                Case 183 : colStr = "GA"
                Case 184 : colStr = "GB"
                Case 185 : colStr = "GC"
                Case 186 : colStr = "GD"
                Case 187 : colStr = "GE"
                Case 188 : colStr = "GF"
                Case 189 : colStr = "GG"
                Case 190 : colStr = "GH"
                Case 191 : colStr = "GI"
                Case 192 : colStr = "GJ"
                Case 193 : colStr = "GK"
                Case 194 : colStr = "GL"
                Case 195 : colStr = "GM"
                Case 196 : colStr = "GN"
                Case 197 : colStr = "GO"
                Case 198 : colStr = "GP"
                Case 199 : colStr = "GQ"
                Case 200 : colStr = "GR"
                Case 201 : colStr = "GS"
                Case 202 : colStr = "GT"
                Case 203 : colStr = "GU"
                Case 204 : colStr = "GV"
                Case 205 : colStr = "GW"
                Case 206 : colStr = "GX"
                Case 207 : colStr = "GY"
                Case 208 : colStr = "GZ"
                Case 209 : colStr = "HA"
                Case 210 : colStr = "HB"
                Case 211 : colStr = "HC"
                Case 212 : colStr = "HD"
                Case 213 : colStr = "HE"
                Case 214 : colStr = "HF"
                Case 215 : colStr = "HG"
                Case 216 : colStr = "HH"
                Case 217 : colStr = "HI"
                Case 218 : colStr = "HJ"
                Case 219 : colStr = "HK"
                Case 220 : colStr = "HL"
                Case 221 : colStr = "HM"
                Case 222 : colStr = "HN"
                Case 223 : colStr = "HO"
                Case 224 : colStr = "HP"
                Case 225 : colStr = "HQ"
                Case 226 : colStr = "HR"
                Case 227 : colStr = "HS"
                Case 228 : colStr = "HT"
                Case 229 : colStr = "HU"
                Case 230 : colStr = "HV"
                Case 231 : colStr = "HW"
                Case 232 : colStr = "HX"
                Case 233 : colStr = "HY"
                Case 234 : colStr = "HZ"
                Case 235 : colStr = "IA"
                Case 236 : colStr = "IB"
                Case 237 : colStr = "IC"
                Case 238 : colStr = "ID"
                Case 239 : colStr = "IE"
                Case 240 : colStr = "IF"
                Case 241 : colStr = "IG"
                Case 242 : colStr = "IH"
                Case 243 : colStr = "II"
                Case 244 : colStr = "IJ"
                Case 245 : colStr = "IK"
                Case 246 : colStr = "IL"
                Case 247 : colStr = "IM"
                Case 248 : colStr = "IN"
                Case 249 : colStr = "IO"
                Case 250 : colStr = "IP"
                Case 251 : colStr = "IQ"
                Case 252 : colStr = "IR"
                Case 253 : colStr = "IS"
                Case 254 : colStr = "IT"
                Case 255 : colStr = "IU"
                Case 256 : colStr = "IV"
                Case Else : colStr = "A"
            End Select
            Return colStr
        End Function

        '===============================================================================
        '[A1]形式を行番号列番号に変換(内部メソッド)
        '===============================================================================
        '行列変換
        Protected Sub convToR1C1(ByVal prmCellStr As String, ByRef prmRefRow As Short, ByRef prmRefCol As Short)
            Dim wkC As Char = ""
            Dim colStr As String = ""

            prmCellStr = prmCellStr.Replace("$", "")
            Dim strLen As Integer = prmCellStr.Length
            For i As Integer = 0 To strLen
                wkC = prmCellStr.Substring(i, 1)
                If CShort(Val(wkC)) <> 0 Then
                    colStr = prmCellStr.Substring(0, i)
                    prmRefRow = prmCellStr.Substring(i)
                    Exit For
                End If
            Next
            prmRefCol = convColToR1C1(colStr)

        End Sub
        '列のみ変換
        Protected Function convColToR1C1(ByVal prmCol As String) As Short
            Dim retVal As Short = 1
            If "A".Equals(prmCol) Then : retVal = 1
            ElseIf "B".Equals(prmCol) Then : retVal = 2
            ElseIf "C".Equals(prmCol) Then : retVal = 3
            ElseIf "D".Equals(prmCol) Then : retVal = 4
            ElseIf "E".Equals(prmCol) Then : retVal = 5
            ElseIf "F".Equals(prmCol) Then : retVal = 6
            ElseIf "G".Equals(prmCol) Then : retVal = 7
            ElseIf "H".Equals(prmCol) Then : retVal = 8
            ElseIf "I".Equals(prmCol) Then : retVal = 9
            ElseIf "J".Equals(prmCol) Then : retVal = 10
            ElseIf "K".Equals(prmCol) Then : retVal = 11
            ElseIf "L".Equals(prmCol) Then : retVal = 12
            ElseIf "M".Equals(prmCol) Then : retVal = 13
            ElseIf "N".Equals(prmCol) Then : retVal = 14
            ElseIf "O".Equals(prmCol) Then : retVal = 15
            ElseIf "P".Equals(prmCol) Then : retVal = 16
            ElseIf "Q".Equals(prmCol) Then : retVal = 17
            ElseIf "R".Equals(prmCol) Then : retVal = 18
            ElseIf "S".Equals(prmCol) Then : retVal = 19
            ElseIf "T".Equals(prmCol) Then : retVal = 20
            ElseIf "U".Equals(prmCol) Then : retVal = 21
            ElseIf "V".Equals(prmCol) Then : retVal = 22
            ElseIf "W".Equals(prmCol) Then : retVal = 23
            ElseIf "X".Equals(prmCol) Then : retVal = 24
            ElseIf "Y".Equals(prmCol) Then : retVal = 25
            ElseIf "Z".Equals(prmCol) Then : retVal = 26
            ElseIf "AA".Equals(prmCol) Then : retVal = 27
            ElseIf "AB".Equals(prmCol) Then : retVal = 28
            ElseIf "AC".Equals(prmCol) Then : retVal = 29
            ElseIf "AD".Equals(prmCol) Then : retVal = 30
            ElseIf "AE".Equals(prmCol) Then : retVal = 31
            ElseIf "AF".Equals(prmCol) Then : retVal = 32
            ElseIf "AG".Equals(prmCol) Then : retVal = 33
            ElseIf "AH".Equals(prmCol) Then : retVal = 34
            ElseIf "AI".Equals(prmCol) Then : retVal = 35
            ElseIf "AJ".Equals(prmCol) Then : retVal = 36
            ElseIf "AK".Equals(prmCol) Then : retVal = 37
            ElseIf "AL".Equals(prmCol) Then : retVal = 38
            ElseIf "AM".Equals(prmCol) Then : retVal = 39
            ElseIf "AN".Equals(prmCol) Then : retVal = 40
            ElseIf "AO".Equals(prmCol) Then : retVal = 41
            ElseIf "AP".Equals(prmCol) Then : retVal = 42
            ElseIf "AQ".Equals(prmCol) Then : retVal = 43
            ElseIf "AR".Equals(prmCol) Then : retVal = 44
            ElseIf "AS".Equals(prmCol) Then : retVal = 45
            ElseIf "AT".Equals(prmCol) Then : retVal = 46
            ElseIf "AU".Equals(prmCol) Then : retVal = 47
            ElseIf "AV".Equals(prmCol) Then : retVal = 48
            ElseIf "AW".Equals(prmCol) Then : retVal = 49
            ElseIf "AX".Equals(prmCol) Then : retVal = 50
            ElseIf "AY".Equals(prmCol) Then : retVal = 51
            ElseIf "AZ".Equals(prmCol) Then : retVal = 52
            ElseIf "BA".Equals(prmCol) Then : retVal = 53
            ElseIf "BB".Equals(prmCol) Then : retVal = 54
            ElseIf "BC".Equals(prmCol) Then : retVal = 55
            ElseIf "BD".Equals(prmCol) Then : retVal = 56
            ElseIf "BE".Equals(prmCol) Then : retVal = 57
            ElseIf "BF".Equals(prmCol) Then : retVal = 58
            ElseIf "BG".Equals(prmCol) Then : retVal = 59
            ElseIf "BH".Equals(prmCol) Then : retVal = 60
            ElseIf "BI".Equals(prmCol) Then : retVal = 61
            ElseIf "BJ".Equals(prmCol) Then : retVal = 62
            ElseIf "BK".Equals(prmCol) Then : retVal = 63
            ElseIf "BL".Equals(prmCol) Then : retVal = 64
            ElseIf "BM".Equals(prmCol) Then : retVal = 65
            ElseIf "BN".Equals(prmCol) Then : retVal = 66
            ElseIf "BO".Equals(prmCol) Then : retVal = 67
            ElseIf "BP".Equals(prmCol) Then : retVal = 68
            ElseIf "BQ".Equals(prmCol) Then : retVal = 69
            ElseIf "BR".Equals(prmCol) Then : retVal = 70
            ElseIf "BS".Equals(prmCol) Then : retVal = 71
            ElseIf "BT".Equals(prmCol) Then : retVal = 72
            ElseIf "BU".Equals(prmCol) Then : retVal = 73
            ElseIf "BV".Equals(prmCol) Then : retVal = 74
            ElseIf "BW".Equals(prmCol) Then : retVal = 75
            ElseIf "BX".Equals(prmCol) Then : retVal = 76
            ElseIf "BY".Equals(prmCol) Then : retVal = 77
            ElseIf "BZ".Equals(prmCol) Then : retVal = 78
            ElseIf "CA".Equals(prmCol) Then : retVal = 79
            ElseIf "CB".Equals(prmCol) Then : retVal = 80
            ElseIf "CC".Equals(prmCol) Then : retVal = 81
            ElseIf "CD".Equals(prmCol) Then : retVal = 82
            ElseIf "CE".Equals(prmCol) Then : retVal = 83
            ElseIf "CF".Equals(prmCol) Then : retVal = 84
            ElseIf "CG".Equals(prmCol) Then : retVal = 85
            ElseIf "CH".Equals(prmCol) Then : retVal = 86
            ElseIf "CI".Equals(prmCol) Then : retVal = 87
            ElseIf "CJ".Equals(prmCol) Then : retVal = 88
            ElseIf "CK".Equals(prmCol) Then : retVal = 89
            ElseIf "CL".Equals(prmCol) Then : retVal = 90
            ElseIf "CM".Equals(prmCol) Then : retVal = 91
            ElseIf "CN".Equals(prmCol) Then : retVal = 92
            ElseIf "CO".Equals(prmCol) Then : retVal = 93
            ElseIf "CP".Equals(prmCol) Then : retVal = 94
            ElseIf "CQ".Equals(prmCol) Then : retVal = 95
            ElseIf "CR".Equals(prmCol) Then : retVal = 96
            ElseIf "CS".Equals(prmCol) Then : retVal = 97
            ElseIf "CT".Equals(prmCol) Then : retVal = 98
            ElseIf "CU".Equals(prmCol) Then : retVal = 99
            ElseIf "CV".Equals(prmCol) Then : retVal = 100
            ElseIf "CW".Equals(prmCol) Then : retVal = 101
            ElseIf "CX".Equals(prmCol) Then : retVal = 102
            ElseIf "CY".Equals(prmCol) Then : retVal = 103
            ElseIf "CZ".Equals(prmCol) Then : retVal = 104
            ElseIf "DA".Equals(prmCol) Then : retVal = 105
            ElseIf "DB".Equals(prmCol) Then : retVal = 106
            ElseIf "DC".Equals(prmCol) Then : retVal = 107
            ElseIf "DD".Equals(prmCol) Then : retVal = 108
            ElseIf "DE".Equals(prmCol) Then : retVal = 109
            ElseIf "DF".Equals(prmCol) Then : retVal = 110
            ElseIf "DG".Equals(prmCol) Then : retVal = 111
            ElseIf "DH".Equals(prmCol) Then : retVal = 112
            ElseIf "DI".Equals(prmCol) Then : retVal = 113
            ElseIf "DJ".Equals(prmCol) Then : retVal = 114
            ElseIf "DK".Equals(prmCol) Then : retVal = 115
            ElseIf "DL".Equals(prmCol) Then : retVal = 116
            ElseIf "DM".Equals(prmCol) Then : retVal = 117
            ElseIf "DN".Equals(prmCol) Then : retVal = 118
            ElseIf "DO".Equals(prmCol) Then : retVal = 119
            ElseIf "DP".Equals(prmCol) Then : retVal = 120
            ElseIf "DQ".Equals(prmCol) Then : retVal = 121
            ElseIf "DR".Equals(prmCol) Then : retVal = 122
            ElseIf "DS".Equals(prmCol) Then : retVal = 123
            ElseIf "DT".Equals(prmCol) Then : retVal = 124
            ElseIf "DU".Equals(prmCol) Then : retVal = 125
            ElseIf "DV".Equals(prmCol) Then : retVal = 126
            ElseIf "DW".Equals(prmCol) Then : retVal = 127
            ElseIf "DX".Equals(prmCol) Then : retVal = 128
            ElseIf "DY".Equals(prmCol) Then : retVal = 129
            ElseIf "DZ".Equals(prmCol) Then : retVal = 130
            ElseIf "EA".Equals(prmCol) Then : retVal = 131
            ElseIf "EB".Equals(prmCol) Then : retVal = 132
            ElseIf "EC".Equals(prmCol) Then : retVal = 133
            ElseIf "ED".Equals(prmCol) Then : retVal = 134
            ElseIf "EE".Equals(prmCol) Then : retVal = 135
            ElseIf "EF".Equals(prmCol) Then : retVal = 136
            ElseIf "EG".Equals(prmCol) Then : retVal = 137
            ElseIf "EH".Equals(prmCol) Then : retVal = 138
            ElseIf "EI".Equals(prmCol) Then : retVal = 139
            ElseIf "EJ".Equals(prmCol) Then : retVal = 140
            ElseIf "EK".Equals(prmCol) Then : retVal = 141
            ElseIf "EL".Equals(prmCol) Then : retVal = 142
            ElseIf "EM".Equals(prmCol) Then : retVal = 143
            ElseIf "EN".Equals(prmCol) Then : retVal = 144
            ElseIf "EO".Equals(prmCol) Then : retVal = 145
            ElseIf "EP".Equals(prmCol) Then : retVal = 146
            ElseIf "EQ".Equals(prmCol) Then : retVal = 147
            ElseIf "ER".Equals(prmCol) Then : retVal = 148
            ElseIf "ES".Equals(prmCol) Then : retVal = 149
            ElseIf "ET".Equals(prmCol) Then : retVal = 150
            ElseIf "EU".Equals(prmCol) Then : retVal = 151
            ElseIf "EV".Equals(prmCol) Then : retVal = 152
            ElseIf "EW".Equals(prmCol) Then : retVal = 153
            ElseIf "EX".Equals(prmCol) Then : retVal = 154
            ElseIf "EY".Equals(prmCol) Then : retVal = 155
            ElseIf "EZ".Equals(prmCol) Then : retVal = 156
            ElseIf "FA".Equals(prmCol) Then : retVal = 157
            ElseIf "FB".Equals(prmCol) Then : retVal = 158
            ElseIf "FC".Equals(prmCol) Then : retVal = 159
            ElseIf "FD".Equals(prmCol) Then : retVal = 160
            ElseIf "FE".Equals(prmCol) Then : retVal = 161
            ElseIf "FF".Equals(prmCol) Then : retVal = 162
            ElseIf "FG".Equals(prmCol) Then : retVal = 163
            ElseIf "FH".Equals(prmCol) Then : retVal = 164
            ElseIf "FI".Equals(prmCol) Then : retVal = 165
            ElseIf "FJ".Equals(prmCol) Then : retVal = 166
            ElseIf "FK".Equals(prmCol) Then : retVal = 167
            ElseIf "FL".Equals(prmCol) Then : retVal = 168
            ElseIf "FM".Equals(prmCol) Then : retVal = 169
            ElseIf "FN".Equals(prmCol) Then : retVal = 170
            ElseIf "FO".Equals(prmCol) Then : retVal = 171
            ElseIf "FP".Equals(prmCol) Then : retVal = 172
            ElseIf "FQ".Equals(prmCol) Then : retVal = 173
            ElseIf "FR".Equals(prmCol) Then : retVal = 174
            ElseIf "FS".Equals(prmCol) Then : retVal = 175
            ElseIf "FT".Equals(prmCol) Then : retVal = 176
            ElseIf "FU".Equals(prmCol) Then : retVal = 177
            ElseIf "FV".Equals(prmCol) Then : retVal = 178
            ElseIf "FW".Equals(prmCol) Then : retVal = 179
            ElseIf "FX".Equals(prmCol) Then : retVal = 180
            ElseIf "FY".Equals(prmCol) Then : retVal = 181
            ElseIf "FZ".Equals(prmCol) Then : retVal = 182
            ElseIf "GA".Equals(prmCol) Then : retVal = 183
            ElseIf "GB".Equals(prmCol) Then : retVal = 184
            ElseIf "GC".Equals(prmCol) Then : retVal = 185
            ElseIf "GD".Equals(prmCol) Then : retVal = 186
            ElseIf "GE".Equals(prmCol) Then : retVal = 187
            ElseIf "GF".Equals(prmCol) Then : retVal = 188
            ElseIf "GG".Equals(prmCol) Then : retVal = 189
            ElseIf "GH".Equals(prmCol) Then : retVal = 190
            ElseIf "GI".Equals(prmCol) Then : retVal = 191
            ElseIf "GJ".Equals(prmCol) Then : retVal = 192
            ElseIf "GK".Equals(prmCol) Then : retVal = 193
            ElseIf "GL".Equals(prmCol) Then : retVal = 194
            ElseIf "GM".Equals(prmCol) Then : retVal = 195
            ElseIf "GN".Equals(prmCol) Then : retVal = 196
            ElseIf "GO".Equals(prmCol) Then : retVal = 197
            ElseIf "GP".Equals(prmCol) Then : retVal = 198
            ElseIf "GQ".Equals(prmCol) Then : retVal = 199
            ElseIf "GR".Equals(prmCol) Then : retVal = 200
            ElseIf "GS".Equals(prmCol) Then : retVal = 201
            ElseIf "GT".Equals(prmCol) Then : retVal = 202
            ElseIf "GU".Equals(prmCol) Then : retVal = 203
            ElseIf "GV".Equals(prmCol) Then : retVal = 204
            ElseIf "GW".Equals(prmCol) Then : retVal = 205
            ElseIf "GX".Equals(prmCol) Then : retVal = 206
            ElseIf "GY".Equals(prmCol) Then : retVal = 207
            ElseIf "GZ".Equals(prmCol) Then : retVal = 208
            ElseIf "HA".Equals(prmCol) Then : retVal = 209
            ElseIf "HB".Equals(prmCol) Then : retVal = 210
            ElseIf "HC".Equals(prmCol) Then : retVal = 211
            ElseIf "HD".Equals(prmCol) Then : retVal = 212
            ElseIf "HE".Equals(prmCol) Then : retVal = 213
            ElseIf "HF".Equals(prmCol) Then : retVal = 214
            ElseIf "HG".Equals(prmCol) Then : retVal = 215
            ElseIf "HH".Equals(prmCol) Then : retVal = 216
            ElseIf "HI".Equals(prmCol) Then : retVal = 217
            ElseIf "HJ".Equals(prmCol) Then : retVal = 218
            ElseIf "HK".Equals(prmCol) Then : retVal = 219
            ElseIf "HL".Equals(prmCol) Then : retVal = 220
            ElseIf "HM".Equals(prmCol) Then : retVal = 221
            ElseIf "HN".Equals(prmCol) Then : retVal = 222
            ElseIf "HO".Equals(prmCol) Then : retVal = 223
            ElseIf "HP".Equals(prmCol) Then : retVal = 224
            ElseIf "HQ".Equals(prmCol) Then : retVal = 225
            ElseIf "HR".Equals(prmCol) Then : retVal = 226
            ElseIf "HS".Equals(prmCol) Then : retVal = 227
            ElseIf "HT".Equals(prmCol) Then : retVal = 228
            ElseIf "HU".Equals(prmCol) Then : retVal = 229
            ElseIf "HV".Equals(prmCol) Then : retVal = 230
            ElseIf "HW".Equals(prmCol) Then : retVal = 231
            ElseIf "HX".Equals(prmCol) Then : retVal = 232
            ElseIf "HY".Equals(prmCol) Then : retVal = 233
            ElseIf "HZ".Equals(prmCol) Then : retVal = 234
            ElseIf "IA".Equals(prmCol) Then : retVal = 235
            ElseIf "IB".Equals(prmCol) Then : retVal = 236
            ElseIf "IC".Equals(prmCol) Then : retVal = 237
            ElseIf "ID".Equals(prmCol) Then : retVal = 238
            ElseIf "IE".Equals(prmCol) Then : retVal = 239
            ElseIf "IF".Equals(prmCol) Then : retVal = 240
            ElseIf "IG".Equals(prmCol) Then : retVal = 241
            ElseIf "IH".Equals(prmCol) Then : retVal = 242
            ElseIf "II".Equals(prmCol) Then : retVal = 243
            ElseIf "IJ".Equals(prmCol) Then : retVal = 244
            ElseIf "IK".Equals(prmCol) Then : retVal = 245
            ElseIf "IL".Equals(prmCol) Then : retVal = 246
            ElseIf "IM".Equals(prmCol) Then : retVal = 247
            ElseIf "IN".Equals(prmCol) Then : retVal = 248
            ElseIf "IO".Equals(prmCol) Then : retVal = 249
            ElseIf "IP".Equals(prmCol) Then : retVal = 250
            ElseIf "IQ".Equals(prmCol) Then : retVal = 251
            ElseIf "IR".Equals(prmCol) Then : retVal = 252
            ElseIf "IS".Equals(prmCol) Then : retVal = 253
            ElseIf "IT".Equals(prmCol) Then : retVal = 254
            ElseIf "IU".Equals(prmCol) Then : retVal = 255
            ElseIf "IV".Equals(prmCol) Then : retVal = 256
            End If
            Return retVal
        End Function
    End Class


    '===============================================================================
    '
    '  ユーティリティクラス
    '    （クラス名）    LineVO
    '    （処理機能名）      UtilExcelHandlerに渡す罫線種データの枠を提供(Beans)
    '    （本MDL使用前提）   UtilExcelHandlerと対で使用する
    '    （備考）            上記使用前提よりUtilExcelHandlerと同一SRC上に定義
    '
    '===============================================================================
    '  履歴  名前          日  付      マーク      内容
    '-------------------------------------------------------------------------------
    '  (1)   Laevigata, Inc.    2006/05/17              新規
    '-------------------------------------------------------------------------------
    Public Class LineVO

        '===============================================================================
        '構造体定義
        '===============================================================================
        '罫線種
        ''' <summary>
        ''' 罫線種列挙体
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum LineType As Short
            ''' <summary>
            ''' 通常
            ''' </summary>
            ''' <remarks></remarks>
            NomalL = 0
            ''' <summary>
            ''' 破線
            ''' </summary>
            ''' <remarks></remarks>
            BrokenL = 1
            ''' <summary>
            ''' 太線
            ''' </summary>
            ''' <remarks></remarks>
            BoldL = 2
            ''' <summary>
            ''' 二重線
            ''' </summary>
            ''' <remarks></remarks>
            DoubleL = 3
            ''' <summary>
            ''' 罫線なし
            ''' </summary>
            ''' <remarks></remarks>
            None = 4
            ''' <summary>
            ''' 未定義(初期値)
            ''' </summary>
            ''' <remarks></remarks>
            Null = -9
        End Enum

        '===============================================================================
        'メンバー変数定義
        '===============================================================================
        Private _Left As LineType = LineType.Null
        Private _Top As LineType = LineType.Null
        Private _Right As LineType = LineType.Null
        Private _Bottom As LineType = LineType.Null
        Private _VerticalMiddle As LineType = LineType.Null
        Private _HorizontalMiddle As LineType = LineType.Null

        '===============================================================================
        'プロパティ(アクセサ)
        '===============================================================================
        ''' <summary>
        ''' 左
        ''' </summary>
        ''' <value>罫線種列挙体</value>
        ''' <returns>罫線種列挙体</returns>
        ''' <remarks></remarks>
        Public Property Left() As LineType
            Get
                Return _Left
            End Get
            Set(ByVal value As LineType)
                _Left = value
            End Set
        End Property

        ''' <summary>
        ''' 上
        ''' </summary>
        ''' <value>罫線種列挙体</value>
        ''' <returns>罫線種列挙体</returns>
        ''' <remarks></remarks>
        Public Property Top() As LineType
            Get
                Return _Top
            End Get
            Set(ByVal value As LineType)
                _Top = value
            End Set
        End Property

        ''' <summary>
        ''' 右
        ''' </summary>
        ''' <value>罫線種列挙体</value>
        ''' <returns>罫線種列挙体</returns>
        ''' <remarks></remarks>
        Public Property Right() As LineType
            Get
                Return _Right
            End Get
            Set(ByVal value As LineType)
                _Right = value
            End Set
        End Property

        ''' <summary>
        ''' 下
        ''' </summary>
        ''' <value>罫線種列挙体</value>
        ''' <returns>罫線種列挙体</returns>
        ''' <remarks></remarks>
        Public Property Bottom() As LineType
            Get
                Return _Bottom
            End Get
            Set(ByVal value As LineType)
                _Bottom = value
            End Set
        End Property

        ''' <summary>
        ''' 中間縦
        ''' </summary>
        ''' <value>罫線種列挙体</value>
        ''' <returns>罫線種列挙体</returns>
        ''' <remarks></remarks>
        Public Property VerticalMiddle() As LineType
            Get
                Return _VerticalMiddle
            End Get
            Set(ByVal value As LineType)
                _VerticalMiddle = value
            End Set
        End Property

        ''' <summary>
        ''' 中間横
        ''' </summary>
        ''' <value>罫線種列挙体</value>
        ''' <returns>罫線種列挙体</returns>
        ''' <remarks></remarks>
        Public Property HorizontalMiddle() As LineType
            Get
                Return _HorizontalMiddle
            End Get
            Set(ByVal value As LineType)
                _HorizontalMiddle = value
            End Set
        End Property

    End Class

    '===============================================================================
    '
    '  ユーティリティクラス
    '    （クラス名）    PageSetUpVO
    '    （処理機能名）      UtilExcelHandlerに渡すページ設定値データの枠を提供(Beans)
    '    （本MDL使用前提）   UtilExcelHandlerと対で使用する
    '    （備考）            上記使用前提よりUtilExcelHandlerと同一SRC上に定義
    '
    '===============================================================================
    '  履歴  名前          日  付      マーク      内容
    '-------------------------------------------------------------------------------
    '  (1)   Laevigata, Inc.    2007/08/10              新規
    '-------------------------------------------------------------------------------
    Public Class PageSetUpVO
        Inherits ExcelFunc

        '===============================================================================
        '構造体定義
        '===============================================================================
        'ページ縦横(印刷の向き)列挙体
        ''' <summary>
        ''' ページ縦横(印刷の向き)
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum OrientationType As Short

            ''' <summary>
            ''' 縦
            ''' </summary>
            ''' <remarks></remarks>
            Portrait = 1

            ''' <summary>
            ''' 横
            ''' </summary>
            ''' <remarks></remarks>
            Landscape = 2

        End Enum

        '用紙サイズ列挙体
        ''' <summary>
        ''' 用紙サイズ
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum PaperSizeType As Short

            ''' <summary>
            ''' A4 (210 mm x 297 mm)  
            ''' </summary>
            ''' <remarks></remarks>
            A4 = 9

            ''' <summary>
            ''' A5 (148 mm x 210 mm) 
            ''' </summary>
            ''' <remarks></remarks>
            A5 = 11

            ''' <summary>
            ''' B5 (182 mm x 257 mm) 
            ''' </summary>
            ''' <remarks></remarks>
            B5 = 13

            ''' <summary>
            ''' A3 (297 mm x 420 mm) 
            ''' </summary>
            ''' <remarks></remarks>
            A3 = 8

            ''' <summary>
            ''' B4 (250 mm x 354 mm) 
            ''' </summary>
            ''' <remarks></remarks>
            B4 = 12

        End Enum


        '===============================================================================
        'メンバー変数定義
        '===============================================================================
        Private _orientation As OrientationType = OrientationType.Landscape
        Private _zoom As Short = 100
        Private _paperSize As PaperSizeType = PaperSizeType.A4
        Private _leftMargin As Single = 2
        Private _rightMargin As Single = 2
        Private _topMargin As Single = 2.5
        Private _bottomMargin As Single = 2.5
        Private _headerMargin As Single = 1.3
        Private _footerMargin As Single = 1.3
        Private _printTitleRows As String = ""
        Private _printTitleColumns As String = ""
        Private _printArea As String = ""

        '===============================================================================
        'プロパティ(アクセサ)
        '===============================================================================
        ''' <summary>
        ''' ページ縦横(印刷の向き)
        ''' </summary>
        ''' <value>ページ縦横(印刷の向き)列挙体</value>
        ''' <returns>ページ縦横(印刷の向き)列挙体</returns>
        ''' <remarks></remarks>
        Public Property Orientation() As OrientationType
            Get
                Return _orientation
            End Get
            Set(ByVal value As OrientationType)
                _orientation = value
            End Set
        End Property

        ''' <summary>
        ''' 拡大/縮小率
        ''' </summary>
        ''' <value>パーセンテージ</value>
        ''' <returns>パーセンテージ</returns>
        ''' <remarks></remarks>
        Public Property Zoom() As Short
            Get
                Return _zoom
            End Get
            Set(ByVal value As Short)
                _zoom = value
            End Set
        End Property

        ''' <summary>
        ''' 用紙サイズ
        ''' </summary>
        ''' <value>用紙サイズ列挙体</value>
        ''' <returns>用紙サイズ列挙体</returns>
        ''' <remarks></remarks>
        Public Property PaperSize() As PaperSizeType
            Get
                Return _paperSize
            End Get
            Set(ByVal value As PaperSizeType)
                _paperSize = value
            End Set
        End Property

        ''' <summary>
        ''' 左マージン
        ''' </summary>
        ''' <value>マージン(インチ)</value>
        ''' <returns>マージン(インチ)</returns>
        ''' <remarks></remarks>
        Public Property LeftMargin() As Single
            Get
                Return _leftMargin
            End Get
            Set(ByVal value As Single)
                _leftMargin = value
            End Set
        End Property

        ''' <summary>
        ''' 右マージン
        ''' </summary>
        ''' <value>マージン(インチ)</value>
        ''' <returns>マージン(インチ)</returns>
        ''' <remarks></remarks>
        Public Property RightMargin() As Single
            Get
                Return _rightMargin
            End Get
            Set(ByVal value As Single)
                _rightMargin = value
            End Set
        End Property

        ''' <summary>
        ''' 上マージン
        ''' </summary>
        ''' <value>マージン(インチ)</value>
        ''' <returns>マージン(インチ)</returns>
        ''' <remarks></remarks>
        Public Property TopMargin() As Single
            Get
                Return _topMargin
            End Get
            Set(ByVal value As Single)
                _topMargin = value
            End Set
        End Property

        ''' <summary>
        ''' 下マージン
        ''' </summary>
        ''' <value>マージン(インチ)</value>
        ''' <returns>マージン(インチ)</returns>
        ''' <remarks></remarks>
        Public Property BottomMargin() As Single
            Get
                Return _bottomMargin
            End Get
            Set(ByVal value As Single)
                _bottomMargin = value
            End Set
        End Property

        ''' <summary>
        ''' ヘッダマージン
        ''' </summary>
        ''' <value>マージン(インチ)</value>
        ''' <returns>マージン(インチ)</returns>
        ''' <remarks></remarks>
        Public Property HeaderMargin() As Single
            Get
                Return _headerMargin
            End Get
            Set(ByVal value As Single)
                _headerMargin = value
            End Set
        End Property

        ''' <summary>
        ''' フッタマージン
        ''' </summary>
        ''' <value>マージン(インチ)</value>
        ''' <returns>マージン(インチ)</returns>
        ''' <remarks></remarks>
        Public Property FooterMargin() As Single
            Get
                Return _footerMargin
            End Get
            Set(ByVal value As Single)
                _footerMargin = value
            End Set
        End Property

        ''' <summary>
        ''' 印刷行タイトル
        ''' </summary>
        ''' <returns>行タイトル文字列</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property PrintTitleRows() As String
            Get
                Return _printTitleRows
            End Get
        End Property

        ''' <summary>
        ''' 印刷列タイトル
        ''' </summary>
        ''' <returns>列タイトル文字列</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property PrintTitleColumns() As String
            Get
                Return _printTitleColumns
            End Get
        End Property

        ''' <summary>
        ''' 印刷範囲
        ''' </summary>
        ''' <returns>印刷範囲文字列</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property PrintArea() As String
            Get
                Return _printArea
            End Get
        End Property




        '===============================================================================
        '印刷範囲設定
        '===============================================================================
        ''' <summary>
        ''' 印刷範囲設定
        ''' </summary>
        ''' <param name="prmRow">開始行</param>
        ''' <param name="prmCol">開始列</param>
        ''' <param name="prmRow2">終了行</param>
        ''' <param name="prmCol2">終了列</param>
        ''' <remarks></remarks>
        Public Sub setPrintArea(ByVal prmRow As Short, ByVal prmCol As Short, _
                                     ByVal prmRow2 As Short, ByVal prmCol2 As Short)
            Dim cell1 As String = Me.convFromR1C1(prmRow, prmCol, True)
            Dim cell2 As String = Me.convFromR1C1(prmRow2, prmCol2, True)
            _printArea = Me.cbnCellStr(cell1, cell2)

        End Sub

        '===============================================================================
        '印刷範囲取得
        '===============================================================================
        ''' <summary>
        ''' 印刷範囲取得
        ''' </summary>
        ''' <param name="prmRefRow">開始行</param>
        ''' <param name="prmRefCol">開始列</param>
        ''' <param name="prmRefRow2">終了行</param>
        ''' <param name="prmRefCol2">終了列</param>
        ''' <remarks></remarks>
        Public Sub getPrintArea(ByRef prmRefRow As Short, ByRef prmRefCol As Short, _
                                     ByRef prmRefRow2 As Short, ByRef prmRefCol2 As Short)
            Dim cell1 As String = ""
            Dim cell2 As String = ""
            Call Me.devCellStr(_printArea, cell1, cell2)
            Call Me.convToR1C1(cell1, prmRefRow, prmRefCol)
            Call Me.convToR1C1(cell2, prmRefRow2, prmRefCol2)

        End Sub

        '===============================================================================
        '印刷行タイトル設定
        '===============================================================================
        ''' <summary>
        ''' 印刷行タイトル設定
        ''' </summary>
        ''' <param name="prmRow">開始行</param>
        ''' <param name="prmRow2">終了行</param>
        ''' <remarks></remarks>
        Public Sub setPrintTitleRows(ByVal prmRow As Short, ByVal prmRow2 As Short)
            _printTitleRows = Me.cbnCellStr("$" & prmRow, "$" & prmRow2)
        End Sub

        '===============================================================================
        '印刷行タイトル取得
        '===============================================================================
        ''' <summary>
        ''' 印刷行タイトル取得
        ''' </summary>
        ''' <param name="prmRefRow">開始行</param>
        ''' <param name="prmRefRow2">終了行</param>
        ''' <remarks></remarks>
        Public Sub getPrintTitleRows(ByRef prmRefRow As Short, ByRef prmRefRow2 As Short)
            Dim cell1 As String = ""
            Dim cell2 As String = ""
            Call Me.devCellStr(_printTitleRows, cell1, cell2)
            prmRefRow = CShort(cell1.Replace("$", ""))
            prmRefRow2 = CShort(cell2.Replace("$", ""))
        End Sub

        '===============================================================================
        '印刷列タイトル設定
        '===============================================================================
        ''' <summary>
        ''' 印刷列タイトル設定
        ''' </summary>
        ''' <param name="prmCol">開始列</param>
        ''' <param name="prmCol2">終了列</param>
        ''' <remarks></remarks>
        Public Sub setPrintTitleColumns(ByVal prmCol As Short, ByVal prmCol2 As Short)
            _printTitleColumns = Me.cbnCellStr("$" & Me.convColFromR1C1(prmCol), "$" & Me.convColFromR1C1(prmCol2))
        End Sub

        '===============================================================================
        '印刷列タイトル取得
        '===============================================================================
        ''' <summary>
        ''' 印刷列タイトル取得
        ''' </summary>
        ''' <param name="prmRefCol">開始列</param>
        ''' <param name="prmRefCol2">終了列</param>
        ''' <remarks></remarks>
        Public Sub getPrintTitleColumns(ByRef prmRefCol As Short, ByRef prmRefCol2 As Short)
            Dim cell1 As String = ""
            Dim cell2 As String = ""
            Call Me.devCellStr(_printTitleColumns, cell1, cell2)
            prmRefCol = Me.convColToR1C1(cell1.Replace("$", ""))
            prmRefCol2 = Me.convColToR1C1(cell2.Replace("$", ""))

        End Sub


    End Class

End Namespace