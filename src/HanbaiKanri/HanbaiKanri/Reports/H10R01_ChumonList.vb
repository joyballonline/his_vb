'===============================================================================
'
'　カネキ吉田商店様
'　　（システム名）販売管理
'　　（処理機能名）注文明細表
'    （フォームID）H10R01
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

Public Class H10R01_ChumonList
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
    Private iDenpyoCnt As Integer = 0
    Private iMeisaiCnt As Integer = 0
    Private dcUriageKin As Decimal = 0
    Private dcShohizei As Decimal = 0
    Private dcGoukeiKin As Decimal = 0
    Private sDenpyoNo As String = ""

    Private Const RS As String = "RecSet"                   'レコードセットテーブル

    '---------------------------------------------------------------------------------
    'レポート作表
    '---------------------------------------------------------------------------------
    Public Sub runReport() Implements RepSectionIF.runReport
        Dim v As New PreviewForm
        v.viewer.Document = Me.Document
        Me.Run()
        If frmH10F01_ChumonList.printKbn = CommonConst.REPORT_PREVIEW Then
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
        _shoriId = frmH10F01_ChumonList.shoriId                             '処理ID
        _userId = frmC01F10_Login.loginValue.TantoCD                        'ユーザＩＤ
        _printKbn = frmH10F01_ChumonList.printKbn

    End Sub

    '---------------------------------------------------------------------------------
    '伝票グループヘッダ初期化時
    '---------------------------------------------------------------------------------
    Private Sub gh伝票_Format(sender As Object, e As EventArgs) Handles gh伝票.Format

    End Sub

    '---------------------------------------------------------------------------------
    'フェッチデータ
    '---------------------------------------------------------------------------------
    Private Sub H10R01_ChumonList_FetchData(sender As Object, eArgs As FetchEventArgs) Handles MyBase.FetchData
        Try

            'グルーピング用
            fid伝票番号.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("注文伝番"))

            'ページヘッダ
            txt区分ヘッダ.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("条件_区分"))
            txt出荷日ヘッダ.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("条件_出荷日"))
            txt伝票番号ヘッダ.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("条件_伝票番号"))
            txt作成日ヘッダ.Text = _comLogc.getSysDdate
            'ページはプロパティで行っている

            '伝票番号ヘッダ
            txt伝票番号.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("注文伝番"))
            txt出荷日.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("出荷日"))
            txt出荷先名.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("出荷先名"))
            txt請求先ID.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("請求先ID"))
            txt請求先名.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("請求先名"))
            txt着日.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("着日"))

            '明細
            txt商品名.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("商品名"))
            txt冷凍.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("冷凍区分"))
            txt税区分.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("課税区分"))
            fld入数.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("入数"))
            fld個数.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("個数"))
            fld数量.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("数量"))
            txt単位.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("単位"))
            fld売上単価.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("注文単価"))
            fld売上金額.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("注文金額"))
            txt明細備考.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("明細備考"))

            '伝票番号フッタ
            fld売上金額グループ.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("売上金額計"))
            fld消費税グループ.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("消費税計"))
            fld合計金額グループ.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("税込額計"))
            txt社外備考.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("社外備考"))

            'ページフッタ
            txt会社名.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("会社名"))

            '伝票番号ブレイク
            If Not _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("注文伝番")).Equals(sDenpyoNo) Then
                dcUriageKin = dcUriageKin + _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("売上金額計"))
                dcShohizei = dcShohizei + _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("消費税計"))
                dcGoukeiKin = dcGoukeiKin + _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("税込額計"))
                iDenpyoCnt = iDenpyoCnt + 1
            End If
            sDenpyoNo = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("注文伝番"))

            'レポートフッタ
            iMeisaiCnt = iMeisaiCnt + 1
            txt伝票件数.Text = Decimal.Parse(iDenpyoCnt).ToString("#,##0") & " 件"
            txt明細件数.Text = Decimal.Parse(iMeisaiCnt).ToString("#,##0") & " 件"

            fld売上金額ページ.Value = Decimal.Parse(dcUriageKin)
            fld消費税ページ.Value = Decimal.Parse(dcShohizei)
            fld合計金額ページ.Value = Decimal.Parse(dcGoukeiKin)

            rowIdx = rowIdx + 1
            eArgs.EOF = False
        Catch ie As IndexOutOfRangeException
            eArgs.EOF = True 'データ末尾
        End Try

    End Sub

    '---------------------------------------------------------------------------------
    'レポートエンド
    '---------------------------------------------------------------------------------
    Private Sub H10R01_ChumonList_ReportEnd(sender As Object, e As EventArgs) Handles MyBase.ReportEnd

        '操作履歴ログ作成
        Call _comLogc.Insert_L01_ProcLog(_companyCd, DBNull.Value, _shoriId, CommonConst.PROGRAM_REPORT, CommonConst.STATUS_NORMAL,
                                                txt区分ヘッダ.Text, txt出荷日ヘッダ.Text, txt伝票番号ヘッダ.Text, _printKbn, DBNull.Value,
                                                iDenpyoCnt, iMeisaiCnt, dcUriageKin, dcShohizei, dcGoukeiKin, _userId)


    End Sub

End Class
