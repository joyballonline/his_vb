'===============================================================================
'
'　カネキ吉田商店様
'　　（システム名）販売管理
'　　（処理機能名）売掛金一覧表
'    （フォームID）H10R03
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   狩野        2018/03/06                 新規              
'-------------------------------------------------------------------------------
Imports GrapeCity.ActiveReports
Imports GrapeCity.ActiveReports.Document
Imports HanbaiKanri
Imports UtilMDL.DB
Imports UtilMDL.MSG

Public Class H10R03_UrikakeKinList
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
    Private dcNyukinZan_Itaku As Decimal = 0
    Private dcNyukinZan_Uriage As Decimal = 0
    Private dcNyukinZan_Gokei As Decimal = 0
    Private _shukkabu As String = ""
    Private jyokenBotonn As String = ""

    Private Const RS As String = "RecSet"                   'レコードセットテーブル


    '---------------------------------------------------------------------------------
    'レポート作表
    '---------------------------------------------------------------------------------
    Public Sub runReport() Implements RepSectionIF.runReport
        Dim v As New PreviewForm
        v.viewer.Document = Me.Document
        Me.Run()
        If frmH10F03_UrikakeKinList.printKbn = CommonConst.REPORT_PREVIEW Then
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
        _shoriId = frmH10F03_UrikakeKinList.shoriId                         '処理ID
        _userId = frmC01F10_Login.loginValue.TantoCD                        'ユーザＩＤ
        _printKbn = frmH10F03_UrikakeKinList.printKbn

    End Sub

    '---------------------------------------------------------------------------------
    'フェッチデータ
    '---------------------------------------------------------------------------------
    Private Sub H10R03_UrikakeKinList_FetchData(sender As Object, eArgs As FetchEventArgs) Handles MyBase.FetchData
        Try

            'グルーピング用
            fid出荷先分類.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("出荷先分類"))

            'ページヘッダ
            txt区分ヘッダ.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("条件_区分"))
            txt売上入金年月ヘッダ.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("条件_年月"))
            txt作成日ヘッダ.Text = _comLogc.getSysDdate
            txt出力順ヘッダ.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("条件_出力順"))
            jyokenBotonn = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("条件_ログ用"))
            'ページはプロパティで行っている

            '出荷先分類ヘッダ

            '明細
            txt番号.Text = (rowIdx + 1).ToString()
            txtコード.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("請求先コード"))
            txt請求先名.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("請求先名"))

            '集計用（集計はプロパティで行っている）
            fld前月残.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("前月残集計"))
            fld売上額.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("売上額集計"))
            fld消費税.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("消費税額集計"))
            fld税込額.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("税込額集計"))
            fld入金額.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("入金額集計"))
            fld手数料.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("手数料集計"))
            fld入金残額.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("入金残額"))

            'ページフッタ
            txt会社名.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("会社名"))

            'レポートフッタ

            rowIdx = rowIdx + 1

            'ログ用
            iMeisaiCnt = rowIdx
            If fid出荷先分類.Value.Equals(CommonConst.SKBUNRUI_ITAKU) Then
                dcNyukinZan_Itaku = dcNyukinZan_Itaku + CType(fld入金残額.Value, Decimal)
            Else
                dcNyukinZan_Uriage = dcNyukinZan_Uriage + CType(fld入金残額.Value, Decimal)
            End If
            dcNyukinZan_Gokei = dcNyukinZan_Itaku + dcNyukinZan_Uriage

            eArgs.EOF = False
        Catch ie As IndexOutOfRangeException
            eArgs.EOF = True 'データ末尾
        End Try

    End Sub

    '---------------------------------------------------------------------------------
    'ページヘッダー（ページフッターの文言設定）
    '---------------------------------------------------------------------------------
    Private Sub PageHeader_Format(sender As Object, e As EventArgs) Handles PageHeader.Format
        If fid出荷先分類.Value.Equals(CommonConst.SKBUNRUI_ITAKU) Then
            lbl見出計.Text = "★[委託計]"
        Else
            lbl見出計.Text = "★[売上計]"
        End If
    End Sub

    '---------------------------------------------------------------------------------
    'レポートエンド
    '---------------------------------------------------------------------------------
    Private Sub H10R03_UrikakeKinList_ReportEnd(sender As Object, e As EventArgs) Handles MyBase.ReportEnd

        '操作履歴ログ作成
        Call _comLogc.Insert_L01_ProcLog(_companyCd, DBNull.Value, _shoriId, CommonConst.PROGRAM_REPORT, CommonConst.STATUS_NORMAL,
                                                jyokenBotonn, txt売上入金年月ヘッダ.Text, DBNull.Value, _printKbn, DBNull.Value,
                                                iMeisaiCnt, dcNyukinZan_Itaku, dcNyukinZan_Uriage, dcNyukinZan_Gokei, DBNull.Value, _userId)


    End Sub

End Class
