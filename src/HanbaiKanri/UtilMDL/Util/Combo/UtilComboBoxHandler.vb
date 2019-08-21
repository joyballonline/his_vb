Imports System.Windows.Forms
Namespace Combo
    '===============================================================================
    '
    '  ユーティリティクラス
    '    （クラス名）    UtilComboBoxHandler
    '    （処理機能名）      コンボボックスの制御機能を提供
    '    （本MDL使用前提）   特になし
    '    （備考）            
    '
    '===============================================================================
    '  履歴  名前          日  付      マーク      内容
    '-------------------------------------------------------------------------------
    '  (1)   Laevigata, Inc.    2006/04/22             新規
    '  (2)   Laevigata, Inc.    2006/06/15             getRelationObjを追加
    '-------------------------------------------------------------------------------
    Public Class UtilComboBoxHandler

        '===============================================================================
        'メンバー変数定義
        '===============================================================================
        Private _target As ComboBox     '対象コンボボックス

        '===============================================================================
        ' コンストラクタ
        '   ●入力パラメタ   ：prmTarget    対象コンボボックス
        '===============================================================================
        ''' <summary>
        ''' コンボボックスハンドラのインスタンスを生成する
        ''' </summary>
        ''' <param name="prmTarget">操作対象となるコンボボックス</param>
        ''' <remarks></remarks>
        Public Sub New(ByRef prmTarget As ComboBox)
            If prmTarget Is Nothing Then
                Throw (New UsrDefException("コンボボックスのインスタンスが設定されていません"))
            End If
            _target = prmTarget 'メンバーへコンボボックスを設定
        End Sub

        '-------------------------------------------------------------------------------
        '   描画停止
        '   （処理概要）コンボボックス項目追加時の処理高速化を目的とし、項目追加前に呼び出す
        '   ●入力パラメタ   ：なし
        '   ●メソッド戻り値 ：なし
        '   ●発生例外       ：なし
        '                                               2006.04.22 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 描画停止 コンボボックス項目追加時の処理高速化を目的とし、項目追加前に呼び出す
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub beginUpdate()
            _target.BeginUpdate()
        End Sub

        '-------------------------------------------------------------------------------
        '   描画開始
        '   （処理概要）コンボボックス項目追加時の処理高速化を目的とし、項目追加後に呼び出す
        '   ●入力パラメタ   ：なし
        '   ●メソッド戻り値 ：なし
        '   ●発生例外       ：なし
        '                                               2006.04.22 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 描画開始 コンボボックス項目追加時の処理高速化を目的とし、項目追加後に呼び出す
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub endUpdate()
            _target.EndUpdate()
        End Sub

        '-------------------------------------------------------------------------------
        '   項目追加
        '   （処理概要）コンボボックス項目追加
        '   ●入力パラメタ   ：prmData   UtilCboVOのインスタンス
        '   ●メソッド戻り値 ：なし
        '   ●発生例外       ：UsrDefException
        '                                               2006.04.22 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 項目追加 コンボボックス項目追加
        ''' </summary>
        ''' <param name="prmData">UtilCboVOのインスタンス</param>
        ''' <remarks></remarks>
        Public Sub addItem(ByRef prmData As UtilCboVO)
            If prmData Is Nothing Then
                Throw (New UsrDefException("UtilCboVOのインスタンスが設定されていません"))
            End If
            Call _target.Items.Add(prmData)
        End Sub

        '-------------------------------------------------------------------------------
        '   項目選択
        '   （処理概要）コンボボックスの項目を選択させる
        '   ●入力パラメタ   ：prmCode   コンボボックスに格納されているデータの内、選択させたい項目のコード
        '   ●メソッド戻り値 ：なし
        '   ●発生例外       ：なし
        '   ●備考           ：渡されたコードが見つからない場合、未選択とする
        '                                               2006.04.22 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 項目選択 コンボボックスの項目を選択させる
        ''' </summary>
        ''' <param name="prmCode">コンボボックスに格納されているデータの内、選択させたい項目のコード</param>
        ''' <remarks>渡されたコードが見つからない場合、未選択とする</remarks>
        Public Sub selectItem(ByVal prmCode As String)

            Dim i As Short
            Dim hitFlg As Boolean
            For i = 0 To _target.Items.Count - 1
                If _target.Items.Item(i).code.Equals(prmCode) Then
                    hitFlg = True
                    _target.SelectedIndex = i
                    Exit For
                End If
            Next
            If Not hitFlg Then
                _target.SelectedIndex = -1
            End If

        End Sub

        '-------------------------------------------------------------------------------
        '   表示名取得
        '   （処理概要）現在選択されている項目の表示名称を取得する
        '   ●入力パラメタ   ：なし
        '   ●メソッド戻り値 ：選択値(表示名称)
        '   ●発生例外       ：なし
        '   ●備考           ：未選択のばあい、""を返却
        '                                               2006.04.22 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 表示名取得 現在選択されている項目の表示名称を取得する
        ''' </summary>
        ''' <returns>選択値(表示名称)</returns>
        ''' <remarks>未選択のばあい、""を返却</remarks>
        Public Function getName() As String
            getName = ""
            Try
                getName = _target.Items.Item(_target.SelectedIndex).name()
            Catch ex As Exception
            End Try
            Return getName
        End Function

        '-------------------------------------------------------------------------------
        '   コード取得
        '   （処理概要）現在選択されている項目の項目(コード)を取得する
        '   ●入力パラメタ   ：なし
        '   ●メソッド戻り値 ：選択値(コード)
        '   ●発生例外       ：なし
        '   ●備考           ：未選択のばあい、""を返却
        '                                               2006.04.22 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' コード取得 現在選択されている項目の項目(コード)を取得する
        ''' </summary>
        ''' <returns>選択値(コード)</returns>
        ''' <remarks>未選択のばあい、""を返却</remarks>
        Public Function getCode() As String
            getCode = ""
            Try
                getCode = _target.Items.Item(_target.SelectedIndex).code()
            Catch ex As Exception
            End Try
            Return getCode
        End Function

        '-------------------------------------------------------------------------------
        '   関連付けオブジェクト取得
        '   （処理概要）現在選択されている項目の関連付けオブジェクトを取得する
        '   ●入力パラメタ   ：なし
        '   ●メソッド戻り値 ：選択値(関連付けオブジェクト)
        '   ●備考           ：未選択のばあい、Nothingを返却
        '                                               2006.06.15 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' コード取得 現在選択されている項目の項目(コード)を取得する
        ''' </summary>
        ''' <returns>選択値(コード)</returns>
        ''' <remarks>未選択のばあい、""を返却</remarks>
        Public Function getRelationObj() As Object
            getRelationObj = Nothing
            Try
                getRelationObj = _target.Items.Item(_target.SelectedIndex).obj
            Catch ex As Exception
            End Try
            Return getRelationObj
        End Function

    End Class

    '===============================================================================
    '
    '  ユーティリティクラス
    '    （クラス名）    UtilCboVO
    '    （処理機能名）      UtilComboBoxHandlerに渡すコンボボックスデータの枠を提供(Beans)
    '    （本MDL使用前提）   UtilComboBoxHandlerと対で使用する
    '    （備考）            上記使用前提よりUtilComboBoxHandlerと同一SRC上に定義
    '
    '===============================================================================
    '  履歴  名前          日  付      マーク      内容
    '-------------------------------------------------------------------------------
    '  (1)   Laevigata, Inc.    2006/04/22             新規
    '  (2)   Laevigata, Inc.    2006/06/15             getRelationObj用にメンバーを追加
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' UtilComboBoxHandlerに渡すコンボボックスデータの枠を提供(Beans)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class UtilCboVO
        '===============================================================================
        'メンバー変数定義
        '===============================================================================
        Private _code As String
        Private _name As String
        Private _obj As Object  '2006.06.15 add by Laevigata, Inc.

        '===============================================================================
        'プロパティ(アクセサ)
        '===============================================================================
        ''' <summary>
        ''' コンボボックスの各行に設定されるコード
        ''' </summary>
        ''' <value>コード</value>
        ''' <returns>コード</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property code() As String
            'Geter--------
            Get
                code = _code
            End Get
        End Property
        ''' <summary>
        ''' コンボボックスの各行に設定される表示名称
        ''' </summary>
        ''' <value>表示名称</value>
        ''' <returns>表示名称</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property name() As String
            'Geter--------
            Get
                name = _name
            End Get
        End Property

        '-->2006.06.15 add start by Laevigata, Inc.
        ''' <summary>
        ''' コンボボックスの各行に設定される関連付けオブジェクト
        ''' </summary>
        ''' <value>関連付けオブジェクト</value>
        ''' <returns>関連付けオブジェクト</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property obj() As Object
            'Geter--------
            Get
                obj = _obj
            End Get
        End Property

        '===============================================================================
        ' コンストラクタ
        '   ●入力パラメタ  ：prmCode           このインスタンスが表す項目のコード
        '                   ：prmName           このインスタンスが表す項目の表示名称
        '                   ：prmRelationObj    このインスタンスが表す項目の関連付けオブジェクト
        '===============================================================================
        ''' <summary>
        ''' コンボボックスハンドラへの受け渡しデータをインスタンス化する
        ''' </summary>
        ''' <param name="prmCode">コード</param>
        ''' <param name="prmName">名称</param>
        ''' <param name="prmRelationObj">関連付けオブジェクト</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal prmCode As String, ByVal prmName As String, ByVal prmRelationObj As Object)
            Me.New(prmCode, prmName)
            _obj = prmRelationObj
        End Sub
        '<--2006.06.15 add end by Laevigata, Inc.

        '===============================================================================
        ' コンストラクタ
        '   ●入力パラメタ   ：prmCode    このインスタンスが表す項目のコード
        '                   ：prmName    このインスタンスが表す項目の表示名称
        '===============================================================================
        ''' <summary>
        ''' コンボボックスハンドラへの受け渡しデータをインスタンス化する
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
End Namespace