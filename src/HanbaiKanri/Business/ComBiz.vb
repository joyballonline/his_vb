'===================================================================================
'　 （システム名）      在庫計画システム
'
'   （機能名）          共通ビジネスロジック
'   （クラス名）        ComBiz
'   （処理機能名）      共通系のビジネスロジックを格納する
'   （備考）            
'
'===================================================================================
' 履歴  名前               日付         マーク      内容
'-----------------------------------------------------------------------------------
'  (1)  中澤            2010/09/13                  新規
'  (2)  菅野            2014/06/04                  変更　材料票マスタ(MPESEKKEI)テーブル変更対応
'-----------------------------------------------------------------------------------
Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.Combo
Imports UtilMDL.Text
Imports System.Reflection
Imports System.Reflection.Assembly
Imports System.Drawing.Printing
Imports UtilMDL.FileDirectory

Public Class ComBiz
    '-------------------------------------------------------------------------------
    'メンバー定数宣言
    '-------------------------------------------------------------------------------
    'コンボ用
    Public Const CBO_ALLN As String = "すべて"                  '表示
    Public Const CBO_ALLC_MEISYO As String = "ALL________"      'コード（名称コンボ用）
    'コンボ用（先頭行を空欄にする場合）
    Public Const CBO_BLANC As String = "　　　"                 '表示
    Public Const CBO_BLANC_MEISYO As String = "BLANC______"     'コード（名称コンボ用）

    'PG制御文字
    Private Const N As String = ControlChars.NewLine            '改行文字
    Private Const RS As String = "RecSet"                       'レコードセットテーブル

    '-------------------------------------------------------------------------------
    'メンバー変数宣言
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf

    '-------------------------------------------------------------------------------
    '   コンストラクタ
    '   （処理概要）　内部初期化を行う
    '   ●入力パラメタ   ：prmRefDbHd   DBハンドラ
    '                      prmMsgHd     MSGハンドラ
    '   ●メソッド戻り値 ：インスタンス
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefDbHd As UtilDBIf, ByVal prmMsgHd As UtilMsgHandler)
        _db = prmRefDbHd
        _msgHd = prmMsgHd
    End Sub

    '-------------------------------------------------------------------------------
    '   フォームタイトル オプション文字列取得
    '   （処理概要）SystemInfoより取得文字列を取得する
    '   ●入力パラメタ   ：prmRefDbHd   DBハンドラ
    '                      prmMsgHd     MSGハンドラ
    '   ●メソッド戻り値 ：取得文字列(STRINGVALUE1～3を文字列結合)
    '-------------------------------------------------------------------------------
    Public Shared Function getFormTitleOption(ByRef prmRefDbHd As UtilDBIf, ByVal prmMsgHd As UtilMsgHandler) As String
        Dim ret As String = ""
        Try
            'SystemInfo取得
            Dim sysinfoRec As UtilDBIf.sysinfoRec = prmRefDbHd.getSystemInfo("FormTitle", "OptionValue")

            '返却文字構築
            If Not "".Equals(sysinfoRec.stringValue1) Then
                ret = sysinfoRec.stringValue1
            End If
            If Not "".Equals(sysinfoRec.stringValue2) AndAlso "".Equals(ret) Then
                ret = sysinfoRec.stringValue2
            ElseIf Not "".Equals(sysinfoRec.stringValue2) Then
                ret = ret & " : " & sysinfoRec.stringValue2
            End If
            If Not "".Equals(sysinfoRec.stringValue3) AndAlso "".Equals(ret) Then
                ret = sysinfoRec.stringValue3
            ElseIf Not "".Equals(sysinfoRec.stringValue3) Then
                ret = ret & " : " & sysinfoRec.stringValue3
            End If

        Catch ex As Exception
            '処理無し
        End Try
        Return ret
    End Function

    '-------------------------------------------------------------------------------
    '   DBサーバシステム日付取得
    '   （処理概要）　DBサーバよりシステム日付の取得を行う
    '   ●入力パラメタ   ：prmFormat  日付書式
    '   ●メソッド戻り値 ：取得日付
    '-------------------------------------------------------------------------------
    Public Function getSysDate(Optional ByVal prmFormat As String = "yyyy/MM/dd HH:mm:ss") As String
        Dim reccnt As Integer
        Dim ds As DataSet = _db.selectDB("select sysdate dt from dual", RS, reccnt)
        If reccnt <= 0 Then
            Throw New UsrDefException("システム日付の取得に失敗しました。")
        Else
            Return _db.rmNullDate(ds.Tables(RS).Rows(0)("dt"), prmFormat)
        End If
    End Function

    '-------------------------------------------------------------------------------
    '   品名データ取得
    '   （処理概要）　材料表マスタより品名を取得し、編集して返す
    '   ●入力パラメタ      ：prmSiyoCD         仕様コード
    '                       ：prmHinsyuCD       品種コード
    '                       ：prmSensinsuCD     線心数コード
    '                       ：prmSizeCD         サイズコード
    '                       ：prmIroCD          色コード
    '   ●出力パラメタ      ：prmRefHinmei      編集後の品名
    '                       ：prmRefHinsyuNM    品種名
    '                       ：prmRefSizeNM      サイズ名
    '                       ：prmRefIroNM       色名
    '-------------------------------------------------------------------------------
    Public Sub getHinmeiFromZairyoMst(ByVal prmSiyoCD As String, ByVal prmHinsyuCD As String, ByVal prmSensinsuCD As String, _
                ByVal prmSizeCD As String, ByVal prmIroCD As String, ByRef prmRefHinmei As String, ByRef prmRefHinsyuNM As String, _
                                                            ByRef prmRefSizeNM As String, ByRef prmRefIroNM As String)
        Try

            'データセットから取り出すための列名
            Dim MPE_HINSYUMEI As String = "HINSYUMEI"
            Dim MPE_SAIZUMEI As String = "SAIZUMEI"
            Dim MPE_IROMEI As String = "IROMEI"

            '材料票から品名検索
            Dim sql As String = ""
            sql = sql & N & " SELECT "
            sql = sql & N & "  HINSYU_MEI " & MPE_HINSYUMEI
            sql = sql & N & " ,SAIZU_MEI " & MPE_SAIZUMEI
            sql = sql & N & " ,IRO_MEI " & MPE_IROMEI
            '2014/06/04 UPD-S Sugano 
            'sql = sql & N & " FROM MPESEKKEI "
            'sql = sql & N & "   WHERE SHIYO = '" & _db.rmSQ(prmSiyoCD) & "' "
            'sql = sql & N & "   AND LPAD(TO_CHAR(HINSYU)  ,3,'0') = '" & _db.rmSQ(prmHinsyuCD) & "' "
            'sql = sql & N & "   AND LPAD(TO_CHAR(SENSHIN)  ,3,'0') = '" & _db.rmSQ(prmSensinsuCD) & "' "
            'sql = sql & N & "   AND LPAD(TO_CHAR(SAIZU)  ,2,'0') = '" & _db.rmSQ(prmSizeCD) & "' "
            'sql = sql & N & "   AND LPAD(TO_CHAR(IRO)  ,3,'0') = '" & _db.rmSQ(prmIroCD) & "' "
            'sql = sql & N & "   AND SEQ_NO = 1 "
            'sql = sql & N & "   AND SEKKEI_HUKA = "
            'sql = sql & N & "           (SELECT MAX(SEKKEI_HUKA) FROM MPESEKKEI "
            'sql = sql & N & "               WHERE SHIYO = '" & _db.rmSQ(prmSiyoCD) & "' "
            'sql = sql & N & "               AND HINSYU = '" & _db.rmSQ(prmHinsyuCD) & "' "
            'sql = sql & N & "               AND SENSHIN = '" & _db.rmSQ(prmSensinsuCD) & "' "
            'sql = sql & N & "               AND SAIZU = '" & _db.rmSQ(prmSizeCD) & "' "
            'sql = sql & N & "               AND IRO = '" & _db.rmSQ(prmIroCD) & "') "
            sql = sql & N & " FROM MPESEKKEI1 "
            sql = sql & N & "   WHERE SHIYO = '" & _db.rmSQ(prmSiyoCD) & "' "
            sql = sql & N & "   AND HINSYU = '" & _db.rmSQ(prmHinsyuCD) & "' "
            sql = sql & N & "   AND SENSHIN = '" & _db.rmSQ(prmSensinsuCD) & "' "
            sql = sql & N & "   AND SAIZU = '" & _db.rmSQ(prmSizeCD) & "' "
            sql = sql & N & "   AND IRO = '" & _db.rmSQ(prmIroCD) & "' "
            sql = sql & N & "   AND SEKKEI_FUKA = 'A'"
            sql = sql & N & "   AND SEKKEI_KAITEI = (SELECT MAX(SEKKEI_KAITEI) FROM MPESEKKEI1 "
            sql = sql & N & "               WHERE SHIYO = '" & _db.rmSQ(prmSiyoCD) & "' "
            sql = sql & N & "               AND HINSYU = '" & _db.rmSQ(prmHinsyuCD) & "' "
            sql = sql & N & "               AND SENSHIN = '" & _db.rmSQ(prmSensinsuCD) & "' "
            sql = sql & N & "               AND SAIZU = '" & _db.rmSQ(prmSizeCD) & "' "
            sql = sql & N & "               AND IRO = '" & _db.rmSQ(prmIroCD) & "' "
            sql = sql & N & "               AND SEKKEI_FUKA = 'A') "
            '2014/06/04 UPD-E Sugano 

            'SQL発行
            Dim recCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, recCnt)

            If recCnt = 0 Then             '抽出レコードが１件もない場合
                '空文字を返す。エラー処理は呼出元で行う。
                prmRefHinmei = ""
                prmRefHinsyuNM = ""
                prmRefSizeNM = ""
                prmRefIroNM = ""
                Exit Sub
            End If

            '品名の"M="を消去、2文字以上のスペースを1文字スペースに変換する
            prmRefHinmei = ds.Tables(RS).Rows(0)(MPE_HINSYUMEI) & ds.Tables(RS).Rows(0)(MPE_SAIZUMEI) & _
                                                                        ds.Tables(RS).Rows(0)(MPE_IROMEI)

            prmRefHinmei = deleteSome(prmRefHinmei)

            '抽出データを呼出元に返す
            '品種名
            prmRefHinsyuNM = ds.Tables(RS).Rows(0)(MPE_HINSYUMEI)
            'サイズ名
            prmRefSizeNM = ds.Tables(RS).Rows(0)(MPE_SAIZUMEI)
            '色名
            prmRefIroNM = ds.Tables(RS).Rows(0)(MPE_IROMEI)

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   品名編集
    '   （処理概要）　渡された品名の余剰部分を消去して返却
    '   ●入力パラメタ   ： prmHinmei     品名  
    '   ●メソッド戻り値 ： ①と②処理後の品名
    '                         ①「M=」を消去
    '                         ②2文字以上のスペースを1文字スペースに変換   
    '-------------------------------------------------------------------------------
    Private Function deleteSome(ByVal prmHinmei As String) As String
        Try

            '"M="を消去
            deleteSome = prmHinmei.Replace("M=", "")
            '文字列の長さ取得
            Dim cnt As Integer
            'スペース2文字以上をスペース1文字に変換
            cnt = deleteSome.Length
            For i As Integer = 1 To cnt - 1
                deleteSome = deleteSome.Replace("  ", " ")
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Function

End Class
