'===============================================================================
'
'　カネキ吉田商店様
'　　（システム名）販売管理
'　　（処理機能名）買掛金一覧表
'    （フォームID）H11R02
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   狩野        2018/03/16                 新規              
'-------------------------------------------------------------------------------
Imports GrapeCity.ActiveReports
Imports GrapeCity.ActiveReports.Document
Imports HanbaiKanri
Imports UtilMDL.DB
Imports UtilMDL.MSG

Public Class H11R02_KaikakeList
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
    Private dcSiharaiZan_Gokei As Decimal = 0
    Private _shukkabu As String = ""

    Private Const RS As String = "RecSet"                   'レコードセットテーブル


    '---------------------------------------------------------------------------------
    'レポート作表
    '---------------------------------------------------------------------------------
    Public Sub runReport() Implements RepSectionIF.runReport
        Dim v As New PreviewForm
        v.viewer.Document = Me.Document
        Me.Run()
        If frmH11F02_KaikakeList.printKbn = CommonConst.REPORT_PREVIEW Then
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
        _shoriId = frmH11F02_KaikakeList.shoriId                         '処理ID
        _userId = frmC01F10_Login.loginValue.TantoCD                        'ユーザＩＤ
        _printKbn = frmH11F02_KaikakeList.printKbn

    End Sub

    '---------------------------------------------------------------------------------
    'フェッチデータ
    '---------------------------------------------------------------------------------
    Private Sub H10R03_UrikakeKinList_FetchData(sender As Object, eArgs As FetchEventArgs) Handles MyBase.FetchData
        Try

            'ページヘッダ
            txt区分ヘッダ.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("条件_区分"))
            txt仕入支払年月ヘッダ.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("条件_年月"))
            txt作成日ヘッダ.Text = _comLogc.getSysDdate
            txt出力順ヘッダ.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("条件_出力順"))
            'ページはプロパティで行っている

            '明細
            txt番号.Text = (rowIdx + 1).ToString()
            txtコード.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("支払先コード"))
            txt支払先.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("支払先名"))

            '集計用（集計はプロパティで行っている）
            fld前月残.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("前月残"))
            fld仕入額.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("仕入額"))
            fld共販手数料.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("共販手数料"))
            fld仕入合計.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("仕入額") + _ds.Tables(RS).Rows(rowIdx)("共販手数料"))
            fld消費税.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("仕入消費税"))
            fld税込額.Value = CType(fld仕入合計.Value, Decimal) + CType(fld消費税.Value, Decimal)
            fld支払額.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("支払額"))
            fld手数料.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("手数料"))
            fld当月残.Value = CType(fld前月残.Value, Decimal) + CType(fld税込額.Value, Decimal) -
                              CType(fld支払額.Value, Decimal) - CType(fld手数料.Value, Decimal)

            'ページフッタ
            txt会社名.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("会社名"))

            'レポートフッタ

            rowIdx = rowIdx + 1

            'ログ用
            iMeisaiCnt = rowIdx
            dcSiharaiZan_Gokei = dcSiharaiZan_Gokei + CType(fld当月残.Value, Decimal)

            eArgs.EOF = False
        Catch ie As IndexOutOfRangeException
            eArgs.EOF = True 'データ末尾
        End Try

    End Sub

    '---------------------------------------------------------------------------------
    'レポートエンド
    '---------------------------------------------------------------------------------
    Private Sub H10R03_UrikakeKinList_ReportEnd(sender As Object, e As EventArgs) Handles MyBase.ReportEnd

        '操作履歴ログ作成
        Call _comLogc.Insert_L01_ProcLog(_companyCd, DBNull.Value, _shoriId, CommonConst.PROGRAM_REPORT, CommonConst.STATUS_NORMAL,
                                                txt区分ヘッダ.Text, txt仕入支払年月ヘッダ.Text, DBNull.Value, _printKbn, DBNull.Value,
                                                iMeisaiCnt, DBNull.Value, DBNull.Value, dcSiharaiZan_Gokei, DBNull.Value, _userId)


    End Sub

End Class
