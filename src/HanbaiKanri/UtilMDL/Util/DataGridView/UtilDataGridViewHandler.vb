Imports System.Windows.Forms.DataGridView

Namespace DataGridView

    '===============================================================================
    '
    '  ユーティリティクラス
    '    （クラス名）    UtilDataGridViewHandler
    '    （処理機能名）      DataGridViewコントロールの制御機能を提供する
    '    （本MDL使用前提）   特になし
    '    （備考）            
    '
    '===============================================================================
    '  履歴  名前          日  付      マーク      内容
    '-------------------------------------------------------------------------------
    '  (1)   Jun.Takagi    2006/05/01              新規
    '-------------------------------------------------------------------------------
    Public Class UtilDataGridViewHandler

        '===============================================================================
        'メンバー変数定義
        '===============================================================================
        Private _grid As Windows.Forms.DataGridView
        '>--2006/11/10 ADD -STR- A.Yamazaki
        '===============================================================================
        '列挙型定義
        '===============================================================================
        Public Enum chkType 'セル入力チェックタイプ
            ''' <summary>
            ''' 日付型１→yy/MM/dd
            ''' </summary>
            ''' <remarks></remarks>
            Date1 = 1
            ''' <summary>
            ''' 日付型２→yyyy/MM/dd
            ''' </summary>
            ''' <remarks></remarks>
            Date2 = 2
            ''' <summary>
            ''' 数値→123456789
            ''' </summary>
            ''' <remarks></remarks>
            Num = 3
            ''' <summary>
            ''' 数値（マイナス許可）→-123 123
            ''' </summary>
            ''' <remarks></remarks>
            Num_M = 4
            ''' <summary>
            ''' 金額（カンマ編集）→123,456,789
            ''' </summary>
            ''' <remarks></remarks>
            Cur = 5
            ''' <summary>
            ''' 半角英数→ABCDabcd1234
            ''' </summary>
            ''' <remarks></remarks>
            Hankaku = 6

        End Enum

        '===============================================================================
        '入力チェック用定数
        '===============================================================================
        Private Shared NUM_CHARS As Char() = New Char(10) {"0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c, "."c}
        Private Shared NUM_MINS_CHARS As Char() = New Char(11) {"0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c, "-"c, "."c}
        Private Shared HANKAKU_CHARS As Char() = New Char(61) {"0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c, _
                                                      "a"c, "b"c, "c"c, "d"c, "e"c, "f"c, "g"c, "h"c, "i"c, "j"c, "k"c, "l"c, "m"c, "n"c, "o"c, "p"c, "q"c, "r"c, "s"c, "t"c, "u"c, "v"c, "w"c, "x"c, "y"c, "z"c, _
                                                      "A"c, "B"c, "C"c, "D"c, "E"c, "F"c, "G"c, "H"c, "I"c, "J"c, "K"c, "L"c, "M"c, "N"c, "O"c, "P"c, "Q"c, "R"c, "S"c, "T"c, "U"c, "V"c, "W"c, "X"c, "Y"c, "Z"c}
        '<--2006/11/10 ADD -END- A.Yamazaki
        '===============================================================================
        'プロパティ(アクセサ)
        '===============================================================================

        '===============================================================================
        ' コンストラクタ
        '   ●入力パラメタ   ：prmTargetGrid    制御対象DataGridView
        '===============================================================================
        ''' <summary>
        ''' DataGridViewハンドラのインスタンスを生成する
        ''' </summary>
        ''' <param name="prmTargetGrid">操作対象となるDataGridView</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal prmTargetGrid As Windows.Forms.DataGridView)

            _grid = prmTargetGrid

        End Sub

        '-------------------------------------------------------------------------------
        '   データクリア
        '   （処理概要）データ行を初期化する(一覧を0行とする)
        '   ●入力パラメタ   ：prmDataColName   データセット上の列名
        '   　　　　　　　　 ：prmRow           対象行のインデックス(0～)
        '   ●メソッド戻り値 ：なし
        '   ●備考　　       ：DataGridViewとバインドされている、DataSet上のデータにONを設定する
        '                                               2006.05.01 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' データ行を初期化する(一覧を0行とする)
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub clearRow()

            If Not _grid.DataSource Is Nothing Then
                Call CType(_grid.DataSource, System.Data.DataSet).Tables(0).Rows.Clear()
            End If

        End Sub

        '-------------------------------------------------------------------------------
        '   チェックボックスON
        '   （処理概要）チェックボックス型の列データをチェックボックスONとする
        '   ●入力パラメタ   ：prmDataColName   データセット上の列名
        '   　　　　　　　　 ：prmRow           対象行のインデックス(0～)
        '   ●メソッド戻り値 ：なし
        '   ●備考　　       ：DataGridViewとバインドされている、DataSet上のデータにONを設定する
        '                                               2006.05.01 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' チェックボックス型の列データをチェックボックスONとする
        ''' </summary>
        ''' <param name="prmDataColName">データセット上の列名</param>
        ''' <param name="prmRow">対象行のインデックス(0～)</param>
        ''' <remarks></remarks>
        Public Sub checkBoxOn(ByVal prmDataColName As String, ByVal prmRow As Integer)
            Dim cell As Object = CType(_grid.DataSource, System.Data.DataSet).Tables(0).Rows(prmRow)(prmDataColName)
            If cell.GetType().ToString.Equals("System.Int16") Or _
               cell.GetType().ToString.Equals("System.Int32") Or _
               cell.GetType().ToString.Equals("System.Int64") Then
                CType(_grid.DataSource, System.Data.DataSet).Tables(0).Rows(prmRow)(prmDataColName) = 1
            ElseIf cell.GetType().ToString.Equals("System.String") Then
                CType(_grid.DataSource, System.Data.DataSet).Tables(0).Rows(prmRow)(prmDataColName) = "1"
            ElseIf cell.GetType().ToString.Equals("System.Boolean") Then
                CType(_grid.DataSource, System.Data.DataSet).Tables(0).Rows(prmRow)(prmDataColName) = True
            End If
        End Sub

        '-------------------------------------------------------------------------------
        '   チェックボックスOFF
        '   （処理概要）チェックボックス型の列データをチェックボックスOFFとする
        '   ●入力パラメタ   ：prmDataColName   データセット上の列名
        '   　　　　　　　　 ：prmRow           対象行のインデックス(0～)
        '   ●メソッド戻り値 ：なし
        '   ●備考　　       ：DataGridViewとバインドされている、DataSet上のデータにONを設定する
        '                                               2006.05.01 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' チェックボックス型の列データをチェックボックスOFFとする
        ''' </summary>
        ''' <param name="prmDataColName">データセット上の列名</param>
        ''' <param name="prmRow">対象行のインデックス(0～)</param>
        ''' <remarks></remarks>
        Public Sub checkBoxOff(ByVal prmDataColName As String, ByVal prmRow As Integer)
            Dim cell As Object = CType(_grid.DataSource, System.Data.DataSet).Tables(0).Rows(prmRow)(prmDataColName)
            If cell.GetType().ToString.Equals("System.Int16") Or _
               cell.GetType().ToString.Equals("System.Int32") Or _
               cell.GetType().ToString.Equals("System.Int64") Then
                CType(_grid.DataSource, System.Data.DataSet).Tables(0).Rows(prmRow)(prmDataColName) = 0
            ElseIf cell.GetType().ToString.Equals("System.String") Then
                CType(_grid.DataSource, System.Data.DataSet).Tables(0).Rows(prmRow)(prmDataColName) = "0"
            ElseIf cell.GetType().ToString.Equals("System.Boolean") Then
                CType(_grid.DataSource, System.Data.DataSet).Tables(0).Rows(prmRow)(prmDataColName) = False
            End If
        End Sub

        '-------------------------------------------------------------------------------
        '   セル取得
        '   （処理概要）セルオブジェクトを取得する
        '   ●入力パラメタ   ：prmColName   グリッド上の列名
        '   　　　　　　　　 ：prmRow       対象行のインデックス(0～)
        '   ●メソッド戻り値 ：なし
        '   ●備考　　       ：DataGridView上のCellそのものを取得
        '                                               2006.05.01 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' セルオブジェクトを取得する(DataGridView上のCellそのものを取得)
        ''' </summary>
        ''' <param name="prmColName">グリッド上の列名</param>
        ''' <param name="prmRow">対象行のインデックス(0～)</param>
        ''' <returns>セルオブジェクト</returns>
        ''' <remarks></remarks>
        Public Function getCell(ByVal prmColName As String, ByVal prmRow As Integer) As Windows.Forms.DataGridViewCell
            Return _grid.Rows(prmRow).Cells(prmColName)
        End Function

        '-------------------------------------------------------------------------------
        '   セルデータ取得
        '   （処理概要）セルに格納されているデータを取得する
        '   ●入力パラメタ   ：prmDataColName   データセット上の列名
        '   　　　　　　　　 ：prmRow           対象行のインデックス(0～)
        '   ●メソッド戻り値 ：格納データ(String型で返却)
        '   ●備考　　       ：DataGridViewとバインドされている、DataSet上のデータから取得する
        '                                               2006.06.05 Updated By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' セルに格納されているデータを取得する
        ''' </summary>
        ''' <param name="prmDataColName">データセット上の列名</param>
        ''' <param name="prmRow">対象行のインデックス(0～)</param>
        ''' <returns>DataSet上の列データ</returns>
        ''' <remarks></remarks>
        Public Function getCellData(ByVal prmDataColName As String, ByVal prmRow As Integer) As String
            Dim cell As Object = CType(_grid.DataSource, System.Data.DataSet).Tables(0).Rows(prmRow)(prmDataColName)
            If IsDBNull(cell) Then
                Return ""
            Else
                Return cell
            End If
        End Function

        '-------------------------------------------------------------------------------
        '   セルデータ取得
        '   （処理概要）セルに格納されているデータを取得する
        '   ●入力パラメタ   ：prmDataColIdx    データセット上の列インデックス(0～)
        '   　　　　　　　　 ：prmRow           対象行のインデックス(0～)
        '   ●メソッド戻り値 ：格納データ(String型で返却)
        '   ●備考　　       ：DataGridViewとバインドされている、DataSet上のデータから取得する
        '                                               2006.06.08 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' セルに格納されているデータを取得する
        ''' </summary>
        ''' <param name="prmDataColIdx">データセット上の列インデックス(0～)</param>
        ''' <param name="prmRow">対象行のインデックス(0～)</param>
        ''' <returns>DataSet上の列データ</returns>
        ''' <remarks></remarks>
        Public Function getCellData(ByVal prmDataColIdx As Integer, ByVal prmRow As Integer) As String
            Dim cell As Object = CType(_grid.DataSource, System.Data.DataSet).Tables(0).Rows(prmRow)(prmDataColIdx)
            If IsDBNull(cell) Then
                Return ""
            Else
                Return cell
            End If
        End Function

        '-------------------------------------------------------------------------------
        '   セルデータ設定
        '   （処理概要）セルに格納されているデータを設定する
        '   ●入力パラメタ   ：prmDataColName   データセット上の列名
        '   　　　　　　　　 ：prmRow           対象行のインデックス(0～)
        '   　　　　　　　　 ：prmVal           格納するデータ
        '   ●メソッド戻り値 ：なし
        '   ●備考　　       ：DataGridViewとバインドされている、DataSet上のデータへ設定する
        '                                               2006.05.01 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' セルに格納されているデータを設定する
        ''' </summary>
        ''' <param name="prmDataColName">データセット上の列名</param>
        ''' <param name="prmRow">対象行のインデックス(0～)</param>
        ''' <param name="prmVal">格納するデータ</param>
        ''' <remarks></remarks>
        Public Sub setCellData(ByVal prmDataColName As String, ByVal prmRow As Integer, ByVal prmVal As Object)
            CType(_grid.DataSource, System.Data.DataSet).Tables(0).Rows(prmRow)(prmDataColName) = prmVal
        End Sub

        '-------------------------------------------------------------------------------
        '   カレントセル設定
        '   （処理概要）カレントセルを設定する
        '   ●入力パラメタ   ：prmColName       グリッド上の列名
        '   　　　　　　　　 ：prmRow           対象行のインデックス(0～)
        '   ●メソッド戻り値 ：なし
        '                                               2006.05.11 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' カレントセルを設定する
        ''' </summary>
        ''' <param name="prmColName">グリッド上の列名</param>
        ''' <param name="prmRow">対象行のインデックス(0～)</param>
        ''' <remarks></remarks>
        Public Sub setCurrentCell(ByVal prmColName As String, ByVal prmRow As Integer)
            _grid.CurrentCell = Me.getCell(prmColName, prmRow)
        End Sub
        '2010.08.18 add by takagi
        Public Structure dgvErrSet
            Public onErr As Boolean
            Public row As Integer
            Public colName As String
        End Structure
        Public Function readyForErrSet(ByVal prmErrRow As Integer, ByVal prmColName As String) As dgvErrSet
            Dim ret As dgvErrSet
            ret.onErr = True
            ret.row = prmErrRow
            ret.colName = prmColName
            Return ret
        End Function
        Public Sub setCurrentCell(ByRef prmRefCell As dgvErrSet)
            If prmRefCell.onErr Then
                _grid.CurrentCell = Me.getCell(prmRefCell.colName, prmRefCell.row)
                prmRefCell.onErr = False
            End If
        End Sub
        '2010.08.18 add by takagi

        '-------------------------------------------------------------------------------
        '   最大行数取得
        '   （処理概要）グリッドに表示されているデータの最大行数取得
        '   ●入力パラメタ   ：なし
        '   ●メソッド戻り値 ：行数
        '                                               2006.05.29 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' グリッドに表示されているデータの最大行数取得
        ''' </summary>
        ''' <returns>最大行</returns>
        ''' <remarks></remarks>
        Public Function getMaxRow() As Integer
            '-->2010.11.15 chg by takagi
            'Return CType(_grid.DataSource, DataSet).Tables(0).Rows.Count
            If _grid.DataSource Is Nothing Then Return 0
            Return CType(_grid.DataSource, DataSet).Tables(0).Rows.Count
            '<--2010.11.15 chg by takagi
        End Function

        '-------------------------------------------------------------------------------
        '   列ロック
        '   （処理概要）列を読み取り専用にする
        '   ●入力パラメタ   ：prmColName       グリッド上の列名
        '   ●メソッド戻り値 ：なし
        '                                               2006.05.01 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 列をロックする(読取専用)
        ''' </summary>
        ''' <param name="prmColName">グリッド上の列名</param>
        ''' <remarks></remarks>
        Public Sub colRock(ByVal prmColName As String)
            _grid.Columns(prmColName).ReadOnly = True
        End Sub

        '-------------------------------------------------------------------------------
        '   列アンロック
        '   （処理概要）列の読み取り専用を解除し、編集可能にする
        '   ●入力パラメタ   ：prmColName       グリッド上の列名
        '   ●メソッド戻り値 ：なし
        '                                               2006.05.01 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 列のロックを解除する(編集可能)
        ''' </summary>
        ''' <param name="prmColName">グリッド上の列名</param>
        ''' <remarks></remarks>
        Public Sub colUnRock(ByVal prmColName As String)
            _grid.Columns(prmColName).ReadOnly = False
        End Sub

        '-------------------------------------------------------------------------------
        '   列背景色変更
        '   （処理概要）列の背景色を変更する
        '   ●入力パラメタ   ：prmColName       グリッド上の列名
        '                    ：prmBackColor     背景色
        '                    ：prmForeColor     前景色
        '                    ：prmSelBackColor  選択時の背景色
        '                    ：prmSelForeColor  選択時の前景色
        '   ●メソッド戻り値 ：なし
        '                                               2006.05.01 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 列の背景色を変更する
        ''' </summary>
        ''' <param name="prmColName">グリッド上の列名</param>
        ''' <param name="prmBackColor">背景色</param>
        ''' <remarks></remarks>
        Public Sub colChengeColor(ByVal prmColName As String, _
                                  ByVal prmBackColor As Drawing.Color)
            Try
                _grid.Columns(prmColName).DefaultCellStyle.BackColor = prmBackColor
            Catch ex As ArgumentOutOfRangeException
            End Try
        End Sub
        ''' <summary>
        ''' 列の背景色を変更する
        ''' </summary>
        ''' <param name="prmColName">グリッド上の列名</param>
        ''' <param name="prmBackColor">背景色</param>
        ''' <param name="prmForeColor">前景色</param>
        ''' <remarks></remarks>
        Public Sub colChengeColor(ByVal prmColName As String, _
                                  ByVal prmBackColor As Drawing.Color, _
                                  ByVal prmForeColor As Drawing.Color)
            Try
                _grid.Columns(prmColName).DefaultCellStyle.BackColor = prmBackColor
                _grid.Columns(prmColName).DefaultCellStyle.ForeColor = prmForeColor
            Catch ex As ArgumentOutOfRangeException
            End Try
        End Sub
        ''' <summary>
        ''' 列の背景色を変更する
        ''' </summary>
        ''' <param name="prmColName">グリッド上の列名</param>
        ''' <param name="prmBackColor">背景色</param>
        ''' <param name="prmForeColor">前景色</param>
        ''' <param name="prmSelBackColor">選択時の背景色</param>
        ''' <param name="prmSelForeColor">選択時の前景色</param>
        ''' <remarks></remarks>
        Public Sub colChengeColor(ByVal prmColName As String, _
                                  ByVal prmBackColor As Drawing.Color, _
                                  ByVal prmForeColor As Drawing.Color, _
                                  ByVal prmSelBackColor As Drawing.Color, _
                                  ByVal prmSelForeColor As Drawing.Color)
            Try
                _grid.Columns(prmColName).DefaultCellStyle.BackColor = prmBackColor
                _grid.Columns(prmColName).DefaultCellStyle.SelectionBackColor = prmSelBackColor
                _grid.Columns(prmColName).DefaultCellStyle.ForeColor = prmForeColor
                _grid.Columns(prmColName).DefaultCellStyle.SelectionForeColor = prmSelForeColor
            Catch ex As ArgumentOutOfRangeException
            End Try
        End Sub

        '-------------------------------------------------------------------------------
        '   列背景色取得
        '   （処理概要）列の背景色を取得する
        '   ●入力パラメタ   ：prmColName          グリッド上の列名
        '                    ：prmRefBackColor     背景色
        '                    ：prmRefForeColor     前景色
        '                    ：prmRefSelBackColor  選択時の背景色
        '                    ：prmRefSelForeColor  選択時の前景色
        '   ●メソッド戻り値 ：なし
        '                                               2006.05.19 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 列の背景色を取得する
        ''' </summary>
        ''' <param name="prmColName">グリッド上の列名</param>
        ''' <param name="prmRefBackColor">背景色</param>
        ''' <param name="prmRefForeColor">前景色</param>
        ''' <param name="prmRefSelBackColor">選択時の背景色</param>
        ''' <param name="prmRefSelForeColor">選択時の前景色</param>
        ''' <remarks></remarks>
        Public Sub colGetColor(ByVal prmColName As String, _
                               ByRef prmRefBackColor As Drawing.Color, _
                               ByRef prmRefForeColor As Drawing.Color, _
                               ByRef prmRefSelBackColor As Drawing.Color, _
                               ByRef prmRefSelForeColor As Drawing.Color)
            Try
                prmRefBackColor = _grid.Columns(prmColName).DefaultCellStyle.BackColor
                prmRefSelBackColor = _grid.Columns(prmColName).DefaultCellStyle.SelectionBackColor
                prmRefForeColor = _grid.Columns(prmColName).DefaultCellStyle.ForeColor
                prmRefSelForeColor = _grid.Columns(prmColName).DefaultCellStyle.SelectionForeColor
            Catch ex As ArgumentOutOfRangeException
            End Try
        End Sub

        '-------------------------------------------------------------------------------
        '   行背景色変更
        '   （処理概要）行の背景色を変更する
        '   ●入力パラメタ   ：prmRowIdx        対象行のインデックス(0～)
        '                    ：prmBackColor     背景色
        '                    ：prmForeColor     前景色
        '                    ：prmSelBackColor  選択時の背景色
        '                    ：prmSelForeColor  選択時の前景色
        '   ●メソッド戻り値 ：なし
        '                                               2006.05.11 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 行の背景色を変更する
        ''' </summary>
        ''' <param name="prmRowIdx">対象行のインデックス</param>
        ''' <param name="prmBackColor">背景色</param>
        ''' <remarks></remarks>
        Public Sub rowChengeColor(ByVal prmRowIdx As Integer, _
                                  ByVal prmBackColor As Drawing.Color)
            Try
                _grid.Rows(prmRowIdx).DefaultCellStyle.BackColor = prmBackColor
            Catch ex As ArgumentOutOfRangeException
            End Try
        End Sub
        ''' <summary>
        ''' 行の背景色を変更する
        ''' </summary>
        ''' <param name="prmRowIdx">対象行のインデックス</param>
        ''' <param name="prmBackColor">背景色</param>
        ''' <param name="prmForeColor">前景色</param>
        ''' <remarks></remarks>
        Public Sub rowChengeColor(ByVal prmRowIdx As Integer, _
                                  ByVal prmBackColor As Drawing.Color, _
                                  ByVal prmForeColor As Drawing.Color)
            Try
                _grid.Rows(prmRowIdx).DefaultCellStyle.BackColor = prmBackColor
                _grid.Rows(prmRowIdx).DefaultCellStyle.ForeColor = prmForeColor
            Catch ex As ArgumentOutOfRangeException
            End Try
        End Sub
        ''' <summary>
        ''' 行の背景色を変更する
        ''' </summary>
        ''' <param name="prmRowIdx">対象行のインデックス</param>
        ''' <param name="prmBackColor">背景色</param>
        ''' <param name="prmForeColor">前景色</param>
        ''' <param name="prmSelBackColor">選択時の背景色</param>
        ''' <param name="prmSelForeColor">選択時の前景色</param>
        ''' <remarks></remarks>
        Public Sub rowChengeColor(ByVal prmRowIdx As Integer, _
                                  ByVal prmBackColor As Drawing.Color, _
                                  ByVal prmForeColor As Drawing.Color, _
                                  ByVal prmSelBackColor As Drawing.Color, _
                                  ByVal prmSelForeColor As Drawing.Color)
            Try
                _grid.Rows(prmRowIdx).DefaultCellStyle.BackColor = prmBackColor
                _grid.Rows(prmRowIdx).DefaultCellStyle.SelectionBackColor = prmSelBackColor
                _grid.Rows(prmRowIdx).DefaultCellStyle.ForeColor = prmForeColor
                _grid.Rows(prmRowIdx).DefaultCellStyle.SelectionForeColor = prmSelForeColor
            Catch ex As ArgumentOutOfRangeException
            End Try
        End Sub

        '-------------------------------------------------------------------------------
        '   行背景色取得
        '   （処理概要）行の背景色を取得する
        '   ●入力パラメタ   ：prmRowIdx        対象行のインデックス(0～)
        '                    ：prmRefBackColor     背景色
        '                    ：prmRefForeColor     前景色
        '                    ：prmRefSelBackColor  選択時の背景色
        '                    ：prmRefSelForeColor  選択時の前景色
        '   ●メソッド戻り値 ：なし
        '                                               2006.05.19 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 行の背景色を取得する
        ''' </summary>
        ''' <param name="prmRowIdx">対象行のインデックス</param>
        ''' <param name="prmRefBackColor">背景色</param>
        ''' <param name="prmRefForeColor">前景色</param>
        ''' <param name="prmRefSelBackColor">選択時の背景色</param>
        ''' <param name="prmRefSelForeColor">選択時の前景色</param>
        ''' <remarks></remarks>
        Public Sub rowGetColor(ByVal prmRowIdx As Integer, _
                                  ByRef prmRefBackColor As Drawing.Color, _
                                  ByRef prmRefForeColor As Drawing.Color, _
                                  ByRef prmRefSelBackColor As Drawing.Color, _
                                  ByRef prmRefSelForeColor As Drawing.Color)
            Try
                prmRefBackColor = _grid.Rows(prmRowIdx).DefaultCellStyle.BackColor
                prmRefSelBackColor = _grid.Rows(prmRowIdx).DefaultCellStyle.SelectionBackColor
                prmRefForeColor = _grid.Rows(prmRowIdx).DefaultCellStyle.ForeColor
                prmRefSelForeColor = _grid.Rows(prmRowIdx).DefaultCellStyle.SelectionForeColor
            Catch ex As ArgumentOutOfRangeException
            End Try
        End Sub

        '-------------------------------------------------------------------------------
        '   選択行背景色設定
        '   （処理概要）選択行を指定の色へ変更し、選択解除となる行の色をデフォルトへ戻す
        '   ●入力パラメタ   ：prmNewRowIdx        選択行のインデックス(0～)
        '                    ：prmOldRowIdx        選択解除行のインデックス(0～)
        '                    ：prmRefBackColor     背景色
        '                    ：prmRefForeColor     前景色
        '                    ：prmRefSelBackColor  選択時の背景色
        '                    ：prmRefSelForeColor  選択時の前景色
        '   ●メソッド戻り値 ：なし
        '                                               2006.05.24 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        Public Sub setSelectionRowColor(ByVal prmNewRowIdx As Integer, _
                                        ByVal prmOldRowIdx As Integer, _
                                        ByVal prmBackColor As Drawing.Color)
            Try
                Me.setDefaultCellColor(DEFCLR_B, prmNewRowIdx, prmOldRowIdx)
                _grid.Rows(prmNewRowIdx).DefaultCellStyle.BackColor = prmBackColor
            Catch ex As ArgumentOutOfRangeException
            End Try

        End Sub
        Public Sub setSelectionRowColor(ByVal prmNewRowIdx As Integer, _
                                        ByVal prmOldRowIdx As Integer, _
                                        ByVal prmBackColor As Drawing.Color, _
                                        ByVal prmForeColor As Drawing.Color)
            Try
                Me.setDefaultCellColor(DEFCLR_BF, prmNewRowIdx, prmOldRowIdx)
                _grid.Rows(prmNewRowIdx).DefaultCellStyle.BackColor = prmBackColor
                _grid.Rows(prmNewRowIdx).DefaultCellStyle.ForeColor = prmForeColor
            Catch ex As ArgumentOutOfRangeException
            End Try
        End Sub
        Public Sub setSelectionRowColor(ByVal prmNewRowIdx As Integer, _
                                        ByVal prmOldRowIdx As Integer, _
                                        ByVal prmBackColor As Drawing.Color, _
                                        ByVal prmForeColor As Drawing.Color, _
                                        ByVal prmSelBackColor As Drawing.Color, _
                                        ByVal prmSelForeColor As Drawing.Color)
            Try
                Me.setDefaultCellColor(DEFCLR_BFS, prmNewRowIdx, prmOldRowIdx)
                _grid.Rows(prmNewRowIdx).DefaultCellStyle.BackColor = prmBackColor
                _grid.Rows(prmNewRowIdx).DefaultCellStyle.ForeColor = prmForeColor
                _grid.Rows(prmNewRowIdx).DefaultCellStyle.SelectionBackColor = prmSelBackColor
                _grid.Rows(prmNewRowIdx).DefaultCellStyle.SelectionForeColor = prmSelForeColor
            Catch ex As ArgumentOutOfRangeException
            End Try
        End Sub
        '内部メソッド
        Private Const DEFCLR_B As Short = 0     '背景色のみ
        Private Const DEFCLR_BF As Short = 1    '背景色＆前景色のみ
        Private Const DEFCLR_BFS As Short = 2   '背景色と前景色と選択時背景色と選択時前景色
        Private Sub setDefaultCellColor(ByVal prmKbn As Short, _
                                        ByVal prmNewRowIdx As Integer, _
                                        ByVal prmOldRowIdx As Integer)
            If prmKbn = DEFCLR_B Or prmKbn = DEFCLR_BF Or prmKbn = DEFCLR_BFS Then
                _grid.Rows(prmOldRowIdx).DefaultCellStyle.BackColor = _grid.Rows(prmNewRowIdx).DefaultCellStyle.BackColor
            End If
            If prmKbn = DEFCLR_BF Or prmKbn = DEFCLR_BFS Then
                _grid.Rows(prmOldRowIdx).DefaultCellStyle.ForeColor = _grid.Rows(prmNewRowIdx).DefaultCellStyle.ForeColor
            End If
            If prmKbn = DEFCLR_BFS Then
                _grid.Rows(prmOldRowIdx).DefaultCellStyle.SelectionBackColor = _grid.Rows(prmNewRowIdx).DefaultCellStyle.SelectionBackColor
                _grid.Rows(prmOldRowIdx).DefaultCellStyle.SelectionForeColor = _grid.Rows(prmNewRowIdx).DefaultCellStyle.SelectionForeColor
            End If
        End Sub

        '-------------------------------------------------------------------------------
        '   ボタンクリック行取得
        '   （処理概要）ボタン型の列において、クリックされた行のインデックスを返却する
        '   ●入力パラメタ   ：e(DataGridViewCellEventArgs) CellContentClickイベントのイベントオブジェクト
        '   　　　　　　　　 ：prmRefRowIdx                 押下ボタン行Idx(0～)
        '   ●メソッド戻り値 ：True/False       ボタンクリックされているか否か
        '   ●備考　　       ：CellContentClickイベントで呼び出すこと。
        '                    ：ボタンクリックで無い場合も実行される為、
        '                    ：ボタンクリックの場合のみメソッド戻り値にTrueを返却する。
        '                                               2006.05.11 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' ボタン型の列において、クリックされた行のインデックスを取得
        ''' </summary>
        ''' <param name="e">CellContentClickイベントのイベントオブジェクト</param>
        ''' <param name="prmRefRowIdx">押下ボタン行Idx(0～)</param>
        ''' <returns>True/False：ボタンクリックされているか否か</returns>
        ''' <remarks></remarks>
        Public Function getClickBtn(ByVal e As System.Windows.Forms.DataGridViewCellEventArgs, _
                               ByRef prmRefRowIdx As Integer) As Boolean
            If Not (TypeOf _grid.Columns(e.ColumnIndex) Is Windows.Forms.DataGridViewButtonColumn _
               AndAlso e.RowIndex <> -1) Then
                'ボタンクリックイベントではない
                Return False
            End If

            prmRefRowIdx = e.RowIndex
            Return True

        End Function

        '-------------------------------------------------------------------------------
        '   コンボボックス行追加
        '   （処理概要）コンボボックス型の列へデータを1件追加する
        '   ●入力パラメタ   ：prmColName   グリッド上の列名
        '   　　　　　　　　 ：prmData      コンボボックスデータのVO(UtilDgvCboVO)
        '   ●メソッド戻り値 ：なし
        '                                               2006.05.12 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' コンボボックス列へデータを1件追加する
        ''' </summary>
        ''' <param name="prmColName">グリッド上の列名</param>
        ''' <param name="prmData">コンボボックスデータのVO(UtilDgvCboVO)</param>
        ''' <remarks></remarks>
        Public Sub addItem(ByVal prmColName As String, ByVal prmData As UtilDgvCboVO)
            Try
                If prmData Is Nothing Then
                    Throw (New UsrDefException("UtilDgvCboVOのインスタンスが設定されていません"))
                End If
                '現在のコンボからDataTableを取得
                Dim dt As DataTable = CType(CType(_grid.Columns(prmColName), Windows.Forms.DataGridViewComboBoxColumn).DataSource, DataTable)
                Dim dsp As String = ""
                Dim val As String = ""
                If dt Is Nothing Then
                    '現在のコンボはデータ無しなので
                    dt = New DataTable()    'データテーブルを生成
                    dsp = prmData.name      'DisplayMenberを設定
                    val = prmData.code      'ValueMenberを設定
                Else
                    '現在のDisplayMember/ValueMemberを取得
                    dsp = CType(_grid.Columns(prmColName), Windows.Forms.DataGridViewComboBoxColumn).DisplayMember
                    val = CType(_grid.Columns(prmColName), Windows.Forms.DataGridViewComboBoxColumn).ValueMember
                End If

                'DataTableにVOを追加
                Call addRow(dt, dsp, val, prmData)

                'DataTableをコンボに戻す
                CType(_grid.Columns(prmColName), Windows.Forms.DataGridViewComboBoxColumn).DataSource = dt
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        '内部メソッド：DataTableの最終行にVOの行を挿入する
        Private Sub addRow(ByRef dt As DataTable, _
                                       ByVal DisplayMember As String, _
                                       ByVal ValueMember As String _
                                       , ByVal prmData As UtilDgvCboVO)
            Try
                Dim newRow As DataRow = dt.NewRow
                newRow(DisplayMember) = prmData.name
                newRow(ValueMember) = prmData.code

                '既存DataTableの最終行にVOを挿入
                dt.Rows.InsertAt(newRow, dt.Rows.Count)

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        '-------------------------------------------------------------------------------
        '   コンボボックス行データバインド
        '   （処理概要）コンボボックス型の列へデータを複数件追加する
        '   ●入力パラメタ   ：prmColName       グリッド上の列名
        '   　　　　　　　　 ：prmDataSet       設定するDataSet(prmTblName省略時はIndex=0のデータテーブルを使用)
        '   　　　　　　　　 ：prmNonSelRowFlg  先頭に空行(未選択行)を設けるかどうかのフラグ(設ける場合：当該行を選択時はコードを""とする)
        '   　　　　　　　　 ：prmDisplayMember DataSet上の表示名称を格納している列の列名称
        '   　　　　　　　　 ：prmValueMember   DataSet上のコードを格納している列の列名称
        '   　　　　　　　　 ：prmTblName       DataSet上の使用するTBL名を指定
        '   ●メソッド戻り値 ：なし
        '   ●備考　　       ：Form_Loadイベントなどコンボ設定時に呼び出すこと。
        '                                               2006.05.12 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' コンボボックス型の列へデータを複数件追加する
        ''' </summary>
        ''' <param name="prmColName">グリッド上の列名</param>
        ''' <param name="prmDataSet">設定するDataSet(prmTblName省略時はIndex=0のデータテーブルを使用)</param>
        ''' <param name="prmNonSelRowFlg">先頭に空行(未選択行)を設けるかどうかのフラグ(設ける場合：当該行を選択時はコードを""とする)</param>
        ''' <param name="prmDisplayMember">DataSet上の表示名称を格納している列の列名称</param>
        ''' <param name="prmValueMember">DataSet上のコードを格納している列の列名称</param>
        ''' <param name="prmTblName">DataSet上の使用するTBL名を指定</param>
        ''' <remarks></remarks>
        Public Sub setCboData(ByVal prmColName As String, ByVal prmDataSet As DataSet, _
                              Optional ByVal prmNonSelRowFlg As Boolean = False, _
                              Optional ByVal prmDisplayMember As String = "名称", _
                              Optional ByVal prmValueMember As String = "コード", _
                              Optional ByVal prmTblName As String = "")
            Try

                Dim dt As DataTable = New DataTable()

                If prmTblName.Equals("") Then
                    '0番目のTBLを使用
                    dt = prmDataSet.Tables(0)
                    If prmNonSelRowFlg Then             '空行を設定するか
                        Call addNonSelectRow(dt, prmDisplayMember, prmValueMember)
                    End If
                Else
                    '指定のTBL名称のTBLを使用
                    dt = prmDataSet.Tables(prmTblName)
                    If prmNonSelRowFlg Then             '空行を設定するか
                        Call addNonSelectRow(dt, prmDisplayMember, prmValueMember)
                    End If
                End If

                CType(_grid.Columns(prmColName), Windows.Forms.DataGridViewComboBoxColumn).DataSource = dt
                CType(_grid.Columns(prmColName), Windows.Forms.DataGridViewComboBoxColumn).DisplayMember = prmDisplayMember
                CType(_grid.Columns(prmColName), Windows.Forms.DataGridViewComboBoxColumn).ValueMember = prmValueMember

            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        '内部メソッド：DataTableの先頭行に空行を設ける
        Private Sub addNonSelectRow(ByRef dt As DataTable, _
                                       ByVal DisplayMember As String, _
                                       ByVal ValueMember As String)
            Try
                Dim newRow As DataRow = dt.NewRow
                newRow(DisplayMember) = ""
                newRow(ValueMember) = ""

                '既存DataTableの先頭に空行を挿入
                dt.Rows.InsertAt(newRow, 0)

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        '-------------------------------------------------------------------------------
        '   コンボボックス行選択
        '   （処理概要）コンボボックス型の列上のコンボを選択させる
        '   ●入力パラメタ   ：prmRowIdx        対象行のインデックス(0～)
        '   　　　　　　　　 ：prmColName       グリッド上の列名
        '   　　　　　　　　 ：prmCode          コード
        '   ●メソッド戻り値 ：なし
        '   ●備考　　       ：コードが見つからない場合は未選択とする。
        '                                               2006.05.12 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' コンボボックス型の列上のコンボを選択させる　コードが見つからない場合は未選択とする
        ''' </summary>
        ''' <param name="prmRowIdx">対象行のインデックス(0～)</param>
        ''' <param name="prmColName">グリッド上の列名</param>
        ''' <param name="prmCode">コード</param>
        ''' <remarks></remarks>
        Public Sub selectItem(ByVal prmRowIdx As Integer, ByVal prmColName As String, ByVal prmCode As String)
            Try
                '現在のコンボからDataTableを取得
                Dim dt As DataTable = CType(CType(_grid.Columns(prmColName), Windows.Forms.DataGridViewComboBoxColumn).DataSource, DataTable)
                Dim dsp As String = ""
                Dim val As String = ""
                If dt Is Nothing Then
                    '現在のコンボはデータ無しなので処理無し
                    Return
                Else
                    '現在のDisplayMember/ValueMemberを取得
                    dsp = CType(_grid.Columns(prmColName), Windows.Forms.DataGridViewComboBoxColumn).DisplayMember
                    val = CType(_grid.Columns(prmColName), Windows.Forms.DataGridViewComboBoxColumn).ValueMember
                End If

                Dim hitFlg As Boolean = False
                For i As Integer = 0 To dt.Rows.Count - 1 'iはコンボの中のindexを示す
                    If dt.Rows(i)(val).ToString.Equals(prmCode) Then
                        '一致
                        CType(Me.getCell(prmColName, prmRowIdx), Windows.Forms.DataGridViewComboBoxCell).Value = prmCode
                        hitFlg = True
                        Continue For
                    End If
                Next
                If Not hitFlg Then
                    '見つからないので未選択
                    CType(Me.getCell(prmColName, prmRowIdx), Windows.Forms.DataGridViewComboBoxCell).Value = Nothing
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        '-------------------------------------------------------------------------------
        '   コンボボックス列の選択値コード取得
        '   （処理概要）現在選択されている項目のコードを取得する
        '   ●入力パラメタ   ：prmRowIdx    行番号
        '                    ：prmColName   列名
        '   ●メソッド戻り値 ：選択値(コード)
        '   ●備考           ：未選択のばあい、""を返却
        '                                               2006.05.12 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' コンボボックス列の選択値コード取得
        ''' </summary>
        ''' <param name="prmRowIdx">行番号</param>
        ''' <param name="prmColName">列名</param>
        ''' <returns>選択値(コード)　未選択のばあい、""を返却</returns>
        ''' <remarks></remarks>
        Public Function getCode(ByVal prmRowIdx As Integer, ByVal prmColName As String) As String

            Dim ret As String = CType(Me.getCell(prmColName, prmRowIdx), Windows.Forms.DataGridViewComboBoxCell).Value
            If ret Is Nothing Then
                ret = ""
            End If
            Return ret

        End Function

        '-------------------------------------------------------------------------------
        '   表示名取得
        '   （処理概要）現在選択されている項目の表示名称を取得する
        '   ●入力パラメタ   ：なし
        '   ●メソッド戻り値 ：選択値(表示名称)
        '   ●発生例外       ：なし
        '   ●備考           ：未選択のばあい、""を返却
        '                                               2006.05.12 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 表示名取得　現在選択されている項目の表示名称を取得する　未選択のばあい、""を返却
        ''' </summary>
        ''' <param name="prmRowIdx">行番号</param>
        ''' <param name="prmColName">列の名称</param>
        ''' <returns>選択値(表示名称)</returns>
        ''' <remarks></remarks>
        Public Function getName(ByVal prmRowIdx As Integer, ByVal prmColName As String) As String
            Try
                '現在のコンボからDataTableを取得
                Dim dt As DataTable = CType(CType(_grid.Columns(prmColName), Windows.Forms.DataGridViewComboBoxColumn).DataSource, DataTable)
                Dim dsp As String = ""
                Dim val As String = ""
                If dt Is Nothing Then
                    '現在のコンボはデータ無しなので処理無し
                    Return ""
                Else
                    '現在のDisplayMember/ValueMemberを取得
                    dsp = CType(_grid.Columns(prmColName), Windows.Forms.DataGridViewComboBoxColumn).DisplayMember
                    val = CType(_grid.Columns(prmColName), Windows.Forms.DataGridViewComboBoxColumn).ValueMember
                End If

                For i As Integer = 0 To dt.Rows.Count - 1 'iはコンボの中のindexを示す
                    If dt.Rows(i)(val).ToString.Equals(CType(Me.getCell(prmColName, prmRowIdx), Windows.Forms.DataGridViewComboBoxCell).Value) Then
                        '一致
                        Return dt.Rows(i)(dsp)
                    End If
                Next
                Return "" '見つからないことは無いはず
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        '>--2006/11/10 ADD -STR- A.Yamazaki
        '-------------------------------------------------------------------------------
        '   データグリッドビューのセル入力を制限する
        '   （処理概要）データグリッドビューセルの入力制限を行う
        '   ●入力パラメタ   ：prmsender    呼び出し元（EditingControlShowing）イベントの「sender」パラメータ
        '                    ：prme         呼び出し元（EditingControlShowing）イベントの「e」パラメータ
        '                    ：prmchkType　 チェック方法
        '                    ：prmchkchr　  入力可能文字列（汎用チェックの場合に使用）
        '   ●メソッド戻り値 ：チェック必要情報（VO）
        '   ●発生例外       ：なし
        '   ●備考           ：
        '                                               2006.11.09 Created By Akiyoshi.Yamazaki
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' データグリッドビューのセル入力を制限する
        ''' </summary>
        ''' <param name="prmsender">呼び出し元（EditingControlShowing）イベントの「sender」パラメータ</param>
        ''' <param name="prme">呼び出し元（EditingControlShowing）イベントの「e」パラメータ</param>
        ''' <param name="prmchkType">チェック方法</param>
        ''' <returns>チェック必要情報（VO）</returns>
        ''' <remarks></remarks>
        Public Function chkCell(ByVal prmsender As Object _
                                , ByVal prme As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs _
                                , ByVal prmchkType As chkType) As UtilDgvChkCellVO


            'DataGridViewTextBoxEditingControlのイベントを処理する為の変数
            Dim editingControl As DataGridViewTextBoxEditingControl
            Dim befData As String   'セル編集前データ

            '編集前のデータを取得
            If _grid.CurrentCell.Value IsNot Nothing Then
                befData = _grid.CurrentCell.Value.ToString
            Else
                befData = ""
            End If

            '余分な文字を取り除く
            prme.Control.Text = Replace(prme.Control.Text, "/", "")
            prme.Control.Text = Replace(prme.Control.Text, ",", "")

            'DGVのテキストボックスコントロール取得
            editingControl = TryCast(prme.Control, DataGridViewTextBoxEditingControl)

            If editingControl IsNot Nothing Then
                'DGVセルのキープレスハンドラに入力チェックのイベントを追加
                If prmchkType = chkType.Date1 Then
                    '日付チェック１
                    AddHandler editingControl.KeyPress, AddressOf ChkDgv_Date_KeyPress

                ElseIf prmchkType = chkType.Date2 Then
                    '日付チェック２
                    AddHandler editingControl.KeyPress, AddressOf ChkDgv_Date2_KeyPress

                ElseIf prmchkType = chkType.Num Then
                    '数値チェック
                    AddHandler editingControl.KeyPress, AddressOf ChkDgv_Num_KeyPress

                ElseIf prmchkType = chkType.Num_M Then
                    '数値（マイナス）チェック
                    AddHandler editingControl.KeyPress, AddressOf ChkDgv_NumM_KeyPress

                ElseIf prmchkType = chkType.Cur Then
                    '金額チェック
                    AddHandler editingControl.KeyPress, AddressOf ChkDgv_Num_KeyPress

                ElseIf prmchkType = chkType.Hankaku Then
                    '半角チェック
                    AddHandler editingControl.KeyPress, AddressOf ChkDgv_Hankaku_KeyPress

                End If
            End If

            'チェックに必要な情報をVOに格納する
            Dim chkVO As New UtilDgvChkCellVO(befData, editingControl, prmchkType)

            'チェックに必要な情報を格納したVOを返却
            Return chkVO

        End Function
        '-------------------------------------------------------------------------------
        '   データグリッドビューセルの入力制限後の後処理
        '   （処理概要）データグリッドビューセルの入力制限で関連づけたイベントを解放し、値の最終チェックを行う
        '   ●入力パラメタ   ：prmchkVO    チェック情報格納VO(「EditingControlShowing」イベントで取得)
        '   ●メソッド戻り値 ：なし
        '   ●発生例外       ：なし
        '   ●備考           ：本メソッドはDGVイベントの「CellEndEdit」から呼び出す
        '                                               2006.11.09 Created By Akiyoshi.Yamazaki
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' データグリッドビューセルの入力制限後の後処
        ''' </summary>
        ''' <param name="prmchkVO">チェック情報格納VO</param>
        ''' <remarks></remarks>
        Public Sub AfterchkCell(ByVal prmchkVO As UtilDgvChkCellVO)

            If prmchkVO.EditingControl IsNot Nothing Then
                '入力チェックを解除
                If prmchkVO.chkType = chkType.Date1 Then
                    '日付チェック１
                    RemoveHandler prmchkVO.EditingControl.KeyPress, AddressOf ChkDgv_Date_KeyPress

                ElseIf prmchkVO.chkType = chkType.Date2 Then
                    '日付チェック２
                    RemoveHandler prmchkVO.EditingControl.KeyPress, AddressOf ChkDgv_Date2_KeyPress

                ElseIf prmchkVO.chkType = chkType.Num Then
                    '数値チェック
                    RemoveHandler prmchkVO.EditingControl.KeyPress, AddressOf ChkDgv_Num_KeyPress

                ElseIf prmchkVO.chkType = chkType.Num_M Then
                    '数値（マイナス）チェック
                    RemoveHandler prmchkVO.EditingControl.KeyPress, AddressOf ChkDgv_NumM_KeyPress

                ElseIf prmchkVO.chkType = chkType.Cur Then
                    '金額チェック
                    RemoveHandler prmchkVO.EditingControl.KeyPress, AddressOf ChkDgv_Num_KeyPress

                ElseIf prmchkVO.chkType = chkType.Hankaku Then
                    '半角チェック
                    RemoveHandler prmchkVO.EditingControl.KeyPress, AddressOf ChkDgv_Hankaku_KeyPress

                End If

                prmchkVO.EditingControl = Nothing
            End If

            'セルに入力がある場合は最終チェック(チェック×の場合は元の値に戻す)
            If IsExistString(_grid.CurrentCell.Value.ToString) = True Then

                If prmchkVO.chkType = chkType.Date1 Then
                    '日付チェック１
                    Dim datewk As String
                    '日付スラッシュ変換＆日付妥当性チェック
                    datewk = convertDateSlash(Replace(_grid.CurrentCell.Value.ToString, "/", ""))
                    If IsExistString(datewk) = True Then
                        _grid.CurrentCell.Value = datewk
                    Else
                        _grid.CurrentCell.Value = prmchkVO.befData
                    End If

                ElseIf prmchkVO.chkType = chkType.Date2 Then
                    '日付チェック２
                    Dim datewk As String
                    '日付スラッシュ変換＆日付妥当性チェック
                    If Len(Replace(_grid.CurrentCell.Value.ToString, "/", "")) = 8 Then
                        datewk = convertDateSlash(Replace(_grid.CurrentCell.Value.ToString, "/", ""))
                        If IsExistString(datewk) = True Then
                            _grid.CurrentCell.Value = datewk
                        Else
                            _grid.CurrentCell.Value = prmchkVO.befData
                        End If
                    Else
                        _grid.CurrentCell.Value = prmchkVO.befData
                    End If

                ElseIf prmchkVO.chkType = chkType.Num Then
                    '数値チェック
                    Dim permitChars As Char() = NUM_CHARS
                    Dim lidx As Integer = 0
                    For lidx = 1 To Len(_grid.CurrentCell.Value.ToString)
                        If Not hasPermitChars(Mid(_grid.CurrentCell.Value.ToString, lidx, 1), permitChars) Then
                            '2010/11.08 在庫計画用変更 start nakazawa
                            '_grid.CurrentCell.Value = prmchkVO.befData
                            '2010/11.08 在庫計画用変更 end nakazawa
                        End If
                    Next lidx

                ElseIf prmchkVO.chkType = chkType.Num_M Then
                    '数値（マイナス）チェック
                    Dim permitChars As Char() = NUM_MINS_CHARS
                    Dim minsPos As Integer
                    'マイナスの位置取得
                    minsPos = InStr(_grid.CurrentCell.Value.ToString, "-")
                    'マイナスの場合１文字目にない場合はエラー
                    If minsPos > 0 Then
                        If minsPos <> 1 Then
                            _grid.CurrentCell.Value = prmchkVO.befData
                        End If
                    End If

                    '数値チェック
                    Dim lidx As Integer = 0
                    For lidx = 1 To Len(_grid.CurrentCell.Value.ToString)
                        If Not hasPermitChars(Mid(_grid.CurrentCell.Value.ToString, lidx, 1), permitChars) Then
                            _grid.CurrentCell.Value = prmchkVO.befData
                        End If
                    Next lidx

                ElseIf prmchkVO.chkType = chkType.Cur Then
                    '金額チェック
                    Dim permitChars As Char() = NUM_CHARS

                    Dim lidx As Integer = 0
                    For lidx = 1 To Len(_grid.CurrentCell.Value.ToString)
                        If Not hasPermitChars(Mid(_grid.CurrentCell.Value.ToString, lidx, 1), permitChars) Then
                            _grid.CurrentCell.Value = prmchkVO.befData
                            Exit Sub
                        End If
                    Next lidx
                    'カンマ編集
                    _grid.CurrentCell.Value = Format(CDec(_grid.CurrentCell.Value.ToString), "###,#")

                ElseIf prmchkVO.chkType = chkType.Hankaku Then
                    '半角チェック
                    Dim permitChars As Char() = HANKAKU_CHARS

                    Dim lidx As Integer = 0
                    For lidx = 1 To Len(_grid.CurrentCell.Value.ToString)
                        If Not hasPermitChars(Mid(_grid.CurrentCell.Value.ToString, lidx, 1), permitChars) Then
                            _grid.CurrentCell.Value = prmchkVO.befData
                            Exit Sub
                        End If
                    Next lidx

                End If

            End If
        End Sub
        '-------------------------------------------------------------------------------
        '  データグリッドセル日付型（YYMMDD）の入力チェック用　キープレスイベント
        '   （処理概要）日付以外の文字列を入力不可にする
        '   ●入力パラメタ：なし
        '   ●メソッド戻り値　：なし
        '                                               2006.11.09 Created By Akiyoshi.Yamazaki
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 日付型の入力チェック
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub ChkDgv_Date_KeyPress(ByVal sender As Object, _
                                                       ByVal e As System.Windows.Forms.KeyPressEventArgs)

            Const MaxLen As Short = 6   '入力可能文字数

            Dim myBox As TextBox = CType(sender, TextBox)
            Dim permitChars As Char() = NUM_CHARS

            'CTRL+C等は有効
            If Char.IsControl(e.KeyChar) Then
                Return
            End If

            If Not hasPermitChars(e.KeyChar, permitChars) Then
                e.Handled = True    '許可していない文字は入力禁止
            Else
                '許可している文字でも、入力できるか判定する
                'YY/MM/DD形式用のチェック
                Dim KeyAscii As Integer = Asc(e.KeyChar) 'このブロックで使う可・不可フラグ（０にすれば入力無効）

                '選択反転無しで最大文字なら、それ以上の入力は不許可とする。
                If CType(sender, TextBox).SelectionLength = 0 Then
                    If CType(sender, TextBox).Text.Length >= MaxLen Then
                        KeyAscii = 0
                    End If
                End If

                '12 3 456  カーソル位置：１個目のＭの前は２。
                'YY[M]MDD　に入力する場合のチェック 
                If myBox.SelectionStart = 2 Then
                    If KeyAscii <> Asc("0") And KeyAscii <> Asc("1") Then
                        '０と１以外は無効
                        KeyAscii = 0
                    End If

                    '123 4 56 　カーソル位置：２個目のＭの前は３
                    'YYM[M]DD　に入力する場合のチェック 
                ElseIf myBox.SelectionStart = 3 Then

                    '前の文字が１なら、０，１，２のみ許可
                    If myBox.Text.ToString.Substring(2, 1).Equals("1") Then
                        If KeyAscii <> Asc("0") And KeyAscii <> Asc("1") And KeyAscii <> Asc("2") Then
                            '０，１，２以外は無効
                            KeyAscii = 0
                        End If
                    End If

                    '前の文字が０なら、１～９のみ許可
                    If myBox.Text.ToString.Substring(2, 1).Equals("0") Then
                        If KeyAscii <> Asc("1") And KeyAscii <> Asc("2") And KeyAscii <> Asc("3") And _
                           KeyAscii <> Asc("4") And KeyAscii <> Asc("5") And KeyAscii <> Asc("6") And _
                           KeyAscii <> Asc("7") And KeyAscii <> Asc("8") And KeyAscii <> Asc("9") Then
                            KeyAscii = 0
                        End If
                    End If

                    '1234 5 6　　カーソル位置：１個目のＤの前は４
                    'YYMM[D]D　に入力する場合のチェック 
                ElseIf myBox.SelectionStart = 4 Then
                    If KeyAscii <> Asc("0") And KeyAscii <> Asc("1") And KeyAscii <> Asc("2") And KeyAscii <> Asc("3") Then
                        KeyAscii = 0
                    End If

                    '12345 6　 カーソル位置：２個目のＤの前は５
                    'YYMMD[D]　に入力する場合のチェック 
                ElseIf myBox.SelectionStart = 5 Then
                    '前の文字が３なら、０，１のみ許可
                    If myBox.Text.ToString.Substring(4, 1).Equals("3") Then
                        If KeyAscii <> Asc("0") And KeyAscii <> Asc("1") Then
                            KeyAscii = 0
                        End If
                    End If

                    '前の文字が０なら、１～９と０のみ許可
                    If myBox.Text.ToString.Substring(4, 1).Equals("0") Then
                        If KeyAscii <> Asc("1") And KeyAscii <> Asc("2") And KeyAscii <> Asc("3") And _
                           KeyAscii <> Asc("4") And KeyAscii <> Asc("5") And KeyAscii <> Asc("6") And _
                           KeyAscii <> Asc("7") And KeyAscii <> Asc("8") And KeyAscii <> Asc("9") Then
                            KeyAscii = 0
                        End If
                    End If
                End If

                '処理を決定
                If KeyAscii = 0 Then
                    e.Handled = True    '処理が済んだことにする（文字は無視されるので入力が無効となる）
                Else
                    e.Handled = False   '文字は処理されるので有効となり入力される。
                End If
            End If
        End Sub
        '-------------------------------------------------------------------------------
        '  データグリッドセル日付型（YYYYMMDD）の入力チェック用　キープレスイベント
        '   （処理概要）日付以外の文字列を入力不可にする
        '   ●入力パラメタ：なし
        '   ●メソッド戻り値　：なし
        '                                               2006.11.09 Created By Akiyoshi.Yamazaki
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 日付型の入力チェック
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub ChkDgv_Date2_KeyPress(ByVal sender As Object, _
                                                       ByVal e As System.Windows.Forms.KeyPressEventArgs)

            Const MaxLen As Short = 8   '入力可能文字数

            Dim myBox As TextBox = CType(sender, TextBox)
            Dim permitChars As Char() = NUM_CHARS

            'CTRL+C等は有効
            If Char.IsControl(e.KeyChar) Then
                Return
            End If

            If Not hasPermitChars(e.KeyChar, permitChars) Then
                e.Handled = True    '許可していない文字は入力禁止
            Else
                '許可している文字でも、入力できるか判定する
                'YYYYMMDD形式用のチェック
                Dim KeyAscii As Integer = Asc(e.KeyChar) 'このブロックで使う可・不可フラグ（０にすれば入力無効）

                '選択反転無しで最大文字なら、それ以上の入力は不許可とする。
                If CType(sender, TextBox).SelectionLength = 0 Then
                    If CType(sender, TextBox).Text.Length >= MaxLen Then
                        KeyAscii = 0
                    End If
                End If

                ' 1 2345678 カーソル位置：１個目のＹの前は０。
                '[Y]YYYMMDD　に入力する場合のチェック 
                If myBox.SelectionStart = 0 Then
                    If KeyAscii <> Asc("1") And KeyAscii <> Asc("2") Then
                        '１と２以外は無効
                        KeyAscii = 0
                    End If

                    '1234 5 678 カーソル位置：１個目のＭの前は４。
                    'YYYY[M]MDD　に入力する場合のチェック 
                ElseIf myBox.SelectionStart = 4 Then
                    If KeyAscii <> Asc("0") And KeyAscii <> Asc("1") Then
                        '０と１以外は無効
                        KeyAscii = 0
                    End If

                    '12345 6 78　カーソル位置：２個目のＭの前は５
                    'YYYYM[M]DD　に入力する場合のチェック  
                ElseIf myBox.SelectionStart = 5 Then

                    '前の文字が１なら、０，１，２のみ許可
                    If myBox.Text.ToString.Substring(4, 1).Equals("1") Then
                        If KeyAscii <> Asc("0") And KeyAscii <> Asc("1") And KeyAscii <> Asc("2") Then
                            '０，１，２以外は無効
                            KeyAscii = 0
                        End If
                    End If

                    '前の文字が０なら、１～９のみ許可
                    If myBox.Text.ToString.Substring(4, 1).Equals("0") Then
                        If KeyAscii <> Asc("1") And KeyAscii <> Asc("2") And KeyAscii <> Asc("3") And _
                           KeyAscii <> Asc("4") And KeyAscii <> Asc("5") And KeyAscii <> Asc("6") And _
                           KeyAscii <> Asc("7") And KeyAscii <> Asc("8") And KeyAscii <> Asc("9") Then
                            KeyAscii = 0
                        End If
                    End If

                    '123456 7 8　　カーソル位置：１個目のＤの前は６
                    'YYYYMM[D]D　に入力する場合のチェック 
                ElseIf myBox.SelectionStart = 6 Then
                    If KeyAscii <> Asc("0") And KeyAscii <> Asc("1") And KeyAscii <> Asc("2") And KeyAscii <> Asc("3") Then
                        KeyAscii = 0
                    End If

                    '1234567 8 カーソル位置：２個目のＤの前は７
                    'YYYYMMD[D]　に入力する場合のチェック 
                ElseIf myBox.SelectionStart = 7 Then
                    '前の文字が３なら、０，１のみ許可
                    If myBox.Text.ToString.Substring(6, 1).Equals("3") Then
                        If KeyAscii <> Asc("0") And KeyAscii <> Asc("1") Then
                            KeyAscii = 0
                        End If
                    End If

                    '前の文字が０なら、１～９と０のみ許可
                    If myBox.Text.ToString.Substring(6, 1).Equals("0") Then
                        If KeyAscii <> Asc("1") And KeyAscii <> Asc("2") And KeyAscii <> Asc("3") And _
                           KeyAscii <> Asc("4") And KeyAscii <> Asc("5") And KeyAscii <> Asc("6") And _
                           KeyAscii <> Asc("7") And KeyAscii <> Asc("8") And KeyAscii <> Asc("9") Then
                            KeyAscii = 0
                        End If
                    End If
                End If

                '処理を決定
                If KeyAscii = 0 Then
                    e.Handled = True    '処理が済んだことにする（文字は無視されるので入力が無効となる）
                Else
                    e.Handled = False   '文字は処理されるので有効となり入力される。
                End If
            End If
        End Sub
        '-------------------------------------------------------------------------------
        '  データグリッドセル数値型　キープレスイベント
        '   （処理概要）数値以外の文字列を入力不可にする
        '   ●入力パラメタ：なし
        '   ●メソッド戻り値　：なし
        '                                               2006.11.09 Created By Akiyoshi.Yamazaki
        '-------------------------------------------------------------------------------
        Public Shared Sub ChkDgv_Num_KeyPress(ByVal sender As Object _
                                           , ByVal e As System.Windows.Forms.KeyPressEventArgs)

            Dim permitChars As Char() = NUM_CHARS

            If Char.IsControl(e.KeyChar) Then
                Return                      'Ctrl+Cなどは処理しない
            End If

            If Not hasPermitChars(e.KeyChar, permitChars) Then
                e.Handled = True                            '許可していない文字は入力禁止 
            Else
                Dim KeyAscii As Integer = Asc(e.KeyChar)    '入力を許可している文字でもチェックを出来る

                '処理を決定
                If KeyAscii = 0 Then
                    e.Handled = True    '処理が済んだことにする（文字は無視されるので入力が無効となる）
                Else
                    e.Handled = False   '文字は処理されるので有効となり入力される。
                End If
            End If
        End Sub
        '-------------------------------------------------------------------------------
        '  データグリッドセル数値型(マイナスを許容)　キープレスイベント
        '   （処理概要）数値以外の文字列を入力不可にする
        '   ●入力パラメタ：なし
        '   ●メソッド戻り値　：なし
        '                                               2006.11.09 Created By Akiyoshi.Yamazaki
        '-------------------------------------------------------------------------------
        Public Shared Sub ChkDgv_NumM_KeyPress(ByVal sender As Object _
                                           , ByVal e As System.Windows.Forms.KeyPressEventArgs)

            Dim permitChars As Char() = NUM_MINS_CHARS
            Dim myBox As TextBox = CType(sender, TextBox)

            If Char.IsControl(e.KeyChar) Then
                Return                      'Ctrl+Cなどは処理しない
            End If

            If Not hasPermitChars(e.KeyChar, permitChars) Then
                e.Handled = True                            '許可していない文字は入力禁止 
            Else
                Dim KeyAscii As Integer = Asc(e.KeyChar)    '入力を許可している文字でもチェックを出来る

                If myBox.SelectionStart <> 0 Then
                    If KeyAscii = Asc("-") Then
                        '先頭文字以外がマイナスは不可
                        KeyAscii = 0
                    End If
                End If

                '処理を決定
                If KeyAscii = 0 Then
                    e.Handled = True    '処理が済んだことにする（文字は無視されるので入力が無効となる）
                Else
                    e.Handled = False   '文字は処理されるので有効となり入力される。
                End If
            End If
        End Sub
        '-------------------------------------------------------------------------------
        '  データグリッドセル半角英数型　キープレスイベント
        '   （処理概要）半角英数字以外の文字列を入力不可にする
        '   ●入力パラメタ：なし
        '   ●メソッド戻り値　：なし
        '                                               2006.11.09 Created By Akiyoshi.Yamazaki
        '-------------------------------------------------------------------------------
        Public Shared Sub ChkDgv_Hankaku_KeyPress(ByVal sender As Object _
                                           , ByVal e As System.Windows.Forms.KeyPressEventArgs)

            Dim permitChars As Char() = HANKAKU_CHARS

            If Char.IsControl(e.KeyChar) Then
                Return                      'Ctrl+Cなどは処理しない
            End If

            If Not hasPermitChars(e.KeyChar, permitChars) Then
                e.Handled = True                            '許可していない文字は入力禁止 
            Else
                Dim KeyAscii As Integer = Asc(e.KeyChar)    '入力を許可している文字でもチェックを出来る

                '処理を決定
                If KeyAscii = 0 Then
                    e.Handled = True    '処理が済んだことにする（文字は無視されるので入力が無効となる）
                Else
                    e.Handled = False   '文字は処理されるので有効となり入力される。
                End If
            End If
        End Sub
        '-------------------------------------------------------------------------------
        'hasPermitChars　入力可能文字のチェック用
        '-------------------------------------------------------------------------------
        Private Shared Function hasPermitChars(ByVal chTarget As Char, ByVal chPermits As Char()) As Boolean
            For Each ch As Char In chPermits
                If chTarget = ch Then
                    Return True
                End If
            Next ch
        End Function
        '-------------------------------------------------------------------------------
        '  日付スラッシュ変換関数
        '   （処理概要）yyyyMMdd → yyyy/MM/dd or yyyy/MM/dd → yyyyMMdd
        '               yyMMdd   → yy/MM/dd   or yy/MM/dd   → yyMMdd
        '   ●入力パラメタ　　：なし
        '   ●メソッド戻り値　：変換後文字列（日付としておかしい場合は空白("")を返します。）
        '                                               2006.11.10 Created By Akiyoshi.Yamazaki
        '-------------------------------------------------------------------------------
        Private Shared Function convertDateSlash(ByVal prmstrDate As String) As String
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
        '                                               2006.11.10 Created By Akiyoshi.Yamazaki
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 空白判定
        ''' </summary>
        ''' <returns>True=空白では無い, False=空白</returns>
        ''' <remarks></remarks>
        Private Shared Function IsExistString(ByVal prmstrDate As String) As Boolean
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
        '<--2006/11/10 ADD -END- A.Yamazaki

        '-------------------------------------------------------------------------------
        '  タブキー押下時制御
        '   （処理概要）タブキー押下時、行移動（UPキー、DOWNキー動作）する
        '               先頭及び最終行では前後のコントロールに移動する
        '   ●入力パラメタ　　：prmForm    フォーカス制御を行うフォーム
        '                     ：prmEvent   KeyPressイベント
        '   ●メソッド戻り値　：なし
        '                                               2018.03.07 Created By Yuichi.Kanno
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' タブキー押下時制御 タブキー押下時、行移動（UPキー、DOWNキー動作）する
        ''' </summary>
        ''' <param name="prmForm">フォーカス制御を行うフォーム</param>
        ''' <param name="prmEvent">KeyPressイベント</param>
        ''' <remarks></remarks>
        Public Sub gridTabKeyDown(ByVal prmForm As Form, ByVal prmEvent As System.Windows.Forms.KeyEventArgs)

            If prmEvent.KeyData = Keys.Tab Then
                '押下キーがTabの場合

                Dim idx As Integer
                '一覧選択行インデックスの取得
                For Each c As DataGridViewRow In _grid.SelectedRows
                    idx = c.Index
                    Exit For
                Next c

                '一覧の最終行の場合
                If idx = _grid.RowCount - 1 Then
                    '次のコントロールへフォーカス移動
                    prmForm.SelectNextControl(prmForm.ActiveControl, True, True, True, True)
                Else
                    'DOWNキー押下時処理
                    SendKeys.Send("{DOWN}")
                End If

                'Tabキー処理無効化
                prmEvent.Handled = True

            ElseIf (prmEvent.Modifiers And Keys.Shift) = Keys.Shift Then
                If prmEvent.KeyCode = Keys.Tab Then
                    '押下キーがShift + Tabの場合

                    Dim idx As Integer
                    '一覧選択行インデックスの取得
                    For Each c As DataGridViewRow In _grid.SelectedRows
                        idx = c.Index
                        Exit For
                    Next c

                    '一覧の先頭行の場合
                    If idx = 0 Then
                        '前のコントロールへフォーカス移動
                        prmForm.SelectNextControl(prmForm.ActiveControl, False, True, True, True)
                    Else
                        'UPキー押下時処理
                        SendKeys.Send("{UP}")
                    End If

                    'Shift + Tabキー処理無効化
                    prmEvent.Handled = True
                End If
            End If

        End Sub

    End Class

    '===============================================================================
    '
    '  ユーティリティクラス
    '    （クラス名）    UtilDgvCboVO
    '    （処理機能名）      UtilDataGridViewHandlerに渡すコンボボックスデータの枠を提供(Beans)
    '    （本MDL使用前提）   UtilDataGridViewHandlerと対で使用する
    '    （備考）            上記使用前提よりUtilDataGridViewHandlerと同一SRC上に定義
    '
    '===============================================================================
    '  履歴  名前          日  付      マーク      内容
    '-------------------------------------------------------------------------------
    '  (1)   Jun.Takagi    2006/05/12              新規
    '-------------------------------------------------------------------------------
    Public Class UtilDgvCboVO
        '===============================================================================
        'メンバー変数定義
        '===============================================================================
        Private _code As String
        Private _name As String

        '===============================================================================
        'プロパティ(アクセサ)
        '===============================================================================
        Public ReadOnly Property code()
            'Geter--------
            Get
                code = _code
            End Get
        End Property
        Public ReadOnly Property name()
            'Geter--------
            Get
                name = _name
            End Get
        End Property

        '===============================================================================
        ' コンストラクタ
        '   ●入力パラメタ   ：prmCode    このインスタンスが表す項目のコード
        '                   ：prmName    このインスタンスが表す項目の表示名称
        '===============================================================================
        ''' <summary>
        ''' グリッドハンドラへの受け渡しデータをインスタンス化する
        ''' </summary>
        ''' <param name="prmCode">コード</param>
        ''' <param name="prmName">名称</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal prmCode As String, ByVal prmName As String)
            _code = prmCode
            _name = prmName
        End Sub

        '===============================================================================
        ' オーバーライドメソッド
        '   （処理概要）コンボボックスに表示するカラムを指定
        '===============================================================================
        ''' <summary>
        ''' コンボボックスに表示する文字列をあらわす
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overrides Function ToString() As String
            ToString = _name '表示名称を返却
        End Function

    End Class
    '>--2006/11/10 ADD -STR- A.Yamazaki
    '===============================================================================
    '
    '  ユーティリティクラス
    '    （クラス名）    UtilDgvchkCellVO
    '    （処理機能名）      UtilDataGridViewHandlerに渡すセルのチェック情報の枠を提供(Beans)
    '    （本MDL使用前提）   UtilDataGridViewHandlerと対で使用する
    '    （備考）            上記使用前提よりUtilDataGridViewHandlerと同一SRC上に定義
    '
    '===============================================================================
    '  履歴  名前          日  付      マーク      内容
    '-------------------------------------------------------------------------------
    '  (1)   A.Yamazaki    2006/11/10              新規
    '-------------------------------------------------------------------------------
    Public Class UtilDgvChkCellVO
        '===============================================================================
        'メンバー変数定義
        '===============================================================================
        Private _befData As String  '編集前データ
        Private _EditingControl As DataGridViewTextBoxEditingControl    'DataGridViewTextBoxEditingControlのイベントを処理する為の変数
        Private _chkType As Integer 'セルチェック方法
        '===============================================================================
        'プロパティ(アクセサ)
        '===============================================================================
        Public Property befData() As String
            'Geter--------
            Get
                befData = _befData
            End Get
            'Setter-------
            Set(ByVal Value As String)
                _befData = Value
            End Set
        End Property
        Public Property EditingControl() As DataGridViewTextBoxEditingControl
            'Geter--------
            Get
                EditingControl = _EditingControl
            End Get
            'Setter-------
            Set(ByVal Value As DataGridViewTextBoxEditingControl)
                _EditingControl = Value
            End Set
        End Property
        Public Property chkType() As Integer
            'Geter--------
            Get
                chkType = _chkType
            End Get
            'Setter-------
            Set(ByVal Value As Integer)
                _chkType = Value
            End Set
        End Property
        '===============================================================================
        ' コンストラクタ
        '   ●入力パラメタ   ：prmbefData    編集前データ
        '                    ：prmEditingControl    編集セルのコントロール
        '                  　：prmchkType    チェック方法
        '===============================================================================
        ''' <summary>
        ''' グリッドハンドラへの受け渡しデータをインスタンス化する
        ''' </summary>
        ''' <param name="prmbefData">編集前データ</param>
        ''' <param name="prmEditingControl">編集セルのコントロール</param>
        ''' <param name="prmchkType">チェック方法</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal prmbefData As String, ByVal prmEditingControl As DataGridViewTextBoxEditingControl, ByVal prmchkType As Integer)
            _befData = prmbefData
            _EditingControl = prmEditingControl
            _chkType = prmchkType
        End Sub

        '===============================================================================
        ' オーバーライドメソッド
        '   （処理概要）VOの格納データを表示する
        '===============================================================================
        ''' <summary>
        ''' VO全体を表示する文字列をあらわす
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overrides Function ToString() As String
            ToString = _EditingControl.Text '表示名称を返却
        End Function

    End Class
    '<--2006/11/10 ADD -END- A.Yamazaki
End Namespace
