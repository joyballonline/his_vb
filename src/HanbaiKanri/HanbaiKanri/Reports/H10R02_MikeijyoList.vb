'===============================================================================
'
'　カネキ吉田商店様
'　　（システム名）販売管理
'　　（処理機能名）売上未計上一覧表
'    （フォームID）H10R02
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   狩野        2018/02/26                 新規              
'-------------------------------------------------------------------------------
Imports GrapeCity.ActiveReports
Imports GrapeCity.ActiveReports.Document
Imports HanbaiKanri
Imports UtilMDL.DB
Imports UtilMDL.MSG

Public Class H10R02_MikeijyoList
    Implements RepSectionIF

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _ds As DataSet
    Private _comLogc As CommonLogic                         '共通処理用
    Private _companyCd As String
    Private _shoriId As String
    Private _userId As String
    Private _printKbn As String
    Private rowIdx As Integer = 0
    Private _PageCount = 0
    Private iMeisaiCnt As Integer = 0
    Private dcGoukeiKin As Decimal = 0
    Private _shukkabu As String = ""

    Private Const RS As String = "RecSet"                   'レコードセットテーブル


    '---------------------------------------------------------------------------------
    'レポート作表
    '---------------------------------------------------------------------------------
    Public Sub runReport() Implements RepSectionIF.runReport
        Dim v As New PreviewForm
        v.viewer.Document = Me.Document
        Me.Run()
        If frmH10F02_MikeijyoList.printKbn = CommonConst.REPORT_PREVIEW Then
            v.Show()            'プレビュー画面
        Else
            Me.Document.Print   'プリンタ選択ダイアログ
        End If
    End Sub

    '---------------------------------------------------------------------------------
    'データ受領
    '---------------------------------------------------------------------------------
    Public Sub setData(prmDs As DataSet, ByRef prmRefDbHd As UtilDBIf) Implements RepSectionIF.setData

        _ds = prmDs
        _db = prmRefDbHd
        rowIdx = 0
        ' 共通処理使用の準備
        _comLogc = New CommonLogic(_db, _msgHd)                             ' 共通処理用
        _companyCd = frmC01F10_Login.loginValue.BumonCD                     '会社コード
        _shoriId = frmH10F02_MikeijyoList.shoriId                             '処理ID
        _userId = frmC01F10_Login.loginValue.TantoCD                        'ユーザＩＤ
        _printKbn = frmH10F02_MikeijyoList.printKbn

    End Sub

    '---------------------------------------------------------------------------------
    '伝票グループヘッダ
    '---------------------------------------------------------------------------------
    Private Sub gh伝票_BeforePrint(sender As Object, e As EventArgs) Handles gh伝票.BeforePrint

    End Sub
    '---------------------------------------------------------------------------------
    'フェッチデータ
    '---------------------------------------------------------------------------------
    Private Sub H10R02_MikeijyoList_FetchData(sender As Object, eArgs As FetchEventArgs) Handles MyBase.FetchData
        Try

            'グルーピング用
            fid伝票番号.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("委託伝番"))

            'ページヘッダ
            txt区分ヘッダ.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("条件_区分"))
            txt出荷日ヘッダ.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("条件_出荷日"))
            txt作成日ヘッダ.Text = _comLogc.getSysDdate
            'ページはプロパティで行っている

            '出荷日ヘッダ
            txt出荷日.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("出荷日"))
            txt伝票番号.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("委託伝番"))
            txt出荷先名.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("出荷先名"))
            txt請求先ID.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("請求先ID"))
            txt請求先名.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("請求先名"))
            txt着日.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("着日"))

            '明細
            txt商品名.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("商品名称"))
            fld入数.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("入数"))
            fld個数.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("個数"))
            fld委託数量.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("委託数量"))
            txt単位委託数量.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("単位"))
            fld売上数量.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("売上数量計"))
            txt単位売上数量.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("単位"))
            fld目切数量.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("目切数量計"))
            txt単位目切数量.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("単位"))
            fld残数量.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("委託残数"))
            txt単位残数量.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("単位"))
            fld仮単価.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("仮単価"))
            fld残金額.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("委託残金額"))

            'ページフッタ
            txt会社名.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("会社名"))

            'レポートフッタ
            iMeisaiCnt = iMeisaiCnt + 1
            txt明細件数.Text = Decimal.Parse(iMeisaiCnt).ToString("#,##0") & " 件"

            'ログ用集計
            dcGoukeiKin = dcGoukeiKin + _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("委託残金額"))

            rowIdx = rowIdx + 1
            eArgs.EOF = False
        Catch ie As IndexOutOfRangeException
            eArgs.EOF = True 'データ末尾
        End Try

    End Sub

    '---------------------------------------------------------------------------------
    'レポートエンド
    '---------------------------------------------------------------------------------
    Private Sub H10R02_MikeijyoList_ReportEnd(sender As Object, e As EventArgs) Handles MyBase.ReportEnd

        '操作履歴ログ作成
        Call _comLogc.Insert_L01_ProcLog(_companyCd, DBNull.Value, _shoriId, CommonConst.PROGRAM_REPORT, CommonConst.STATUS_NORMAL,
                                                txt区分ヘッダ.Text, txt出荷日ヘッダ.Text, DBNull.Value, _printKbn, DBNull.Value,
                                                iMeisaiCnt, dcGoukeiKin, DBNull.Value, DBNull.Value, DBNull.Value, _userId)


    End Sub

End Class
