'===============================================================================
'
'　カネキ吉田商店様
'　　（システム名）販売管理
'　　（処理機能名）仕入総括表
'    （フォームID）H11R21
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   狩野        2018/03/23                 新規              
'-------------------------------------------------------------------------------
Imports GrapeCity.ActiveReports
Imports GrapeCity.ActiveReports.Document
Imports HanbaiKanri
Imports UtilMDL.DB
Imports UtilMDL.MSG

Public Class H11R21_ShiireSokatuList
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
    Private dcSuryoKei As Decimal = 0
    Private dcKingakuKei As Decimal = 0
    Private dcTankaKei As Decimal = 0
    Private dcTanka2 As Decimal = 0
    Private dcTanka3 As Decimal = 0
    Private dcTanka4 As Decimal = 0
    Private dcTanka5 As Decimal = 0
    Private iKensuYoko = 0
    Private dcTanka2Sum As Decimal = 0
    Private dcTanka3Sum As Decimal = 0
    Private dcTanka4Sum As Decimal = 0
    Private dcTanka5Sum As Decimal = 0
    Private iTanka2KensuTate = 0
    Private iTanka3KensuTate = 0
    Private iTanka4KensuTate = 0
    Private iTanka5KensuTate = 0

    Private Const RS As String = "RecSet"                           'レコードセットテーブル

    '---------------------------------------------------------------------------------
    'レポート作表
    '---------------------------------------------------------------------------------
    Public Sub runReport() Implements RepSectionIF.runReport
        Dim v As New PreviewForm
        v.viewer.Document = Me.Document
        Me.Run()
        If frmH11F21_ShiireSokatuList.printKbn = CommonConst.REPORT_PREVIEW Then
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
        _shoriId = frmH11F21_ShiireSokatuList.shoriId                             '処理ID
        _userId = frmC01F10_Login.loginValue.TantoCD                        'ユーザＩＤ
        _printKbn = frmH11F21_ShiireSokatuList.printKbn

    End Sub

    '---------------------------------------------------------------------------------
    'フェッチデータ
    '---------------------------------------------------------------------------------
    Private Sub H11R21_ShiireSokatuList_FetchData(sender As Object, eArgs As FetchEventArgs) Handles MyBase.FetchData
        Try

            'ページヘッダ
            txt区分ヘッダ.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("条件_区分"))
            txt仕入年度ヘッダ.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("条件_仕入年度"))
            txt作成日ヘッダ.Text = _comLogc.getSysDdate
            'ページはプロパティで行っている

            '明細
            txt仕入先コード.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("仕入先コード"))
            txt仕入先名.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("仕入先名"))
            fld２月数量.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("２月仕入数量"))
            fld３月数量.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("３月仕入数量"))
            fld４月数量.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("４月仕入数量"))
            fld５月数量.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("５月仕入数量"))
            fld数量計.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("２月仕入数量")) _
                            + _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("３月仕入数量")) _
                            + _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("４月仕入数量")) _
                            * _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("５月仕入数量"))

            fld２月金額.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("２月仕入金額"))
            fld３月金額.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("３月仕入金額"))
            fld４月金額.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("４月仕入金額"))
            fld５月金額.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("５月仕入金額"))
            fld金額計.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("２月仕入金額")) _
                            + _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("３月仕入金額")) _
                            + _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("４月仕入金額")) _
                            * _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("５月仕入金額"))

            iKensuYoko = 0
            If _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("２月件数")) = 0 Then
                dcTanka2 = 0
            Else
                dcTanka2 = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("２月仕入単価")) _
                         / _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("２月件数"))
                iKensuYoko = iKensuYoko + 1
                '縦集計
                dcTanka2Sum = dcTanka2Sum + dcTanka2
                iTanka2KensuTate = iTanka2KensuTate + 1
            End If
            fld２月平均単価.Value = dcTanka2
            If _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("３月件数")) = 0 Then
                dcTanka3 = 0
            Else
                dcTanka3 = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("３月仕入単価")) _
                         / _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("３月件数"))
                iKensuYoko = iKensuYoko + 1
                '縦集計
                dcTanka3Sum = dcTanka3Sum + dcTanka3
                iTanka3KensuTate = iTanka3KensuTate + 1
            End If
            fld３月平均単価.Value = dcTanka3
            If _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("４月件数")) = 0 Then
                dcTanka4 = 0
            Else
                dcTanka4 = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("４月仕入単価")) _
                         / _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("４月件数"))
                iKensuYoko = iKensuYoko + 1
                '縦集計
                dcTanka4Sum = dcTanka4Sum + dcTanka4
                iTanka4KensuTate = iTanka4KensuTate + 1
            End If
            fld４月平均単価.Value = dcTanka4
            If _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("５月件数")) = 0 Then
                dcTanka5 = 0
            Else
                dcTanka5 = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("５月仕入単価")) _
                         / _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("５月件数"))
                iKensuYoko = iKensuYoko + 1
                '縦集計
                dcTanka5Sum = dcTanka5Sum + dcTanka5
                iTanka5KensuTate = iTanka5KensuTate + 1
            End If
            fld４月平均単価.Value = dcTanka5
            '平均単価計
            If iKensuYoko = 0 Then
                fld平均単価計.Value = 0
            Else
                fld平均単価計.Value = (dcTanka2 + dcTanka3 + dcTanka4 + dcTanka5) / iKensuYoko
            End If

            'ページフッタ
            txt会社名.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("会社名"))

            'レポートフッタ
            dcSuryoKei = dcSuryoKei + CType(fld数量計.Value, Decimal)
            dcKingakuKei = dcSuryoKei + CType(fld金額計.Value, Decimal)
            If iTanka2KensuTate = 0 Then
                fld２月平均単価総合計.Value = 0
            Else
                fld２月平均単価総合計.Value = dcTanka2Sum / iTanka2KensuTate
            End If
            If iTanka3KensuTate = 0 Then
                fld３月平均単価総合計.Value = 0
            Else
                fld３月平均単価総合計.Value = dcTanka3Sum / iTanka3KensuTate
            End If
            If iTanka4KensuTate = 0 Then
                fld４月平均単価総合計.Value = 0
            Else
                fld４月平均単価総合計.Value = dcTanka4Sum / iTanka4KensuTate
            End If
            If iTanka5KensuTate = 0 Then
                fld５月平均単価総合計.Value = 0
            Else
                fld５月平均単価総合計.Value = dcTanka5Sum / iTanka5KensuTate
            End If
            If (iTanka2KensuTate + iTanka3KensuTate + iTanka4KensuTate + iTanka5KensuTate) = 0 Then
                fld平均単価総合計.Value = 0
            Else
                fld平均単価総合計.Value = (dcTanka2Sum + dcTanka3Sum + dcTanka4Sum + dcTanka5Sum) _
                                    / (iTanka2KensuTate + iTanka3KensuTate + iTanka4KensuTate + iTanka5KensuTate)
            End If

            rowIdx = rowIdx + 1
            eArgs.EOF = False
        Catch ie As IndexOutOfRangeException
            eArgs.EOF = True 'データ末尾
        End Try

    End Sub

    '---------------------------------------------------------------------------------
    'レポートエンド
    '---------------------------------------------------------------------------------
    Private Sub H11R21_ShiireSokatuList_ReportEnd(sender As Object, e As EventArgs) Handles MyBase.ReportEnd

        '操作履歴ログ作成
        Call _comLogc.Insert_L01_ProcLog(_companyCd, DBNull.Value, _shoriId, CommonConst.PROGRAM_REPORT, CommonConst.STATUS_NORMAL,
                                                DBNull.Value, txt仕入年度ヘッダ.Text, DBNull.Value, _printKbn, DBNull.Value,
                                                dcSuryoKei, dcKingakuKei, dcTankaKei, DBNull.Value, DBNull.Value, _userId)


    End Sub

End Class
