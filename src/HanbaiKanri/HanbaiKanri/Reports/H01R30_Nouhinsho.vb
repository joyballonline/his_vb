'===============================================================================
'
'　カネキ吉田商店様
'　　（システム名）販売管理
'　　（処理機能名）納品書
'    （フォームID）H01R30
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   鴫原        2018/03/12                 新規              
'-------------------------------------------------------------------------------
Imports GrapeCity.ActiveReports
Imports GrapeCity.ActiveReports.Document
Imports GrapeCity.ActiveReports.SectionReportModel
Imports HanbaiKanri
Imports UtilMDL.DB
Imports UtilMDL.MSG

Public Class H01R30_Nouhinsho
    Implements RepSectionIF

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _ds As DataSet
    Private _comLogc As CommonLogic                             '共通処理用
    Private _companyCd As String
    Private _shoriId As String
    Private _userId As String
    Private _printKbn As String
    Private _seikyuKbn As String
    Private rowIdx As Integer = 0
    Private _PageCount As Integer = 0
    Private _kingakuSum As Decimal = 0
    Private iDenpyoCnt As Integer = 0
    Private iMeisaiCnt As Integer = 0
    Private dcUriageKin As Decimal = 0
    Private dcShohizei As Decimal = 0
    Private dcGoukeiKin As Decimal = 0
    Private RowNumber As Integer = 0　　                        '件数カウンタ
    Private sDenpyoNo As String = ""
    Private sNohinStr As String = ""
    Private sSeikyuStr As String = ""

    Private rpt As H01R30S_Nouhinsho
    Private rpt2 As H01R30S_Nouhinsho

    Private Const RS As String = "RecSet"                       'レコードセットテーブル
    Private Const MAX_MEISAI As Integer = 6                     '明細数
    Private Const HANYO_KAHENKEY_NOHINSHO As String = "H01R30"  '納品書
    Private Const HANYO_KAHENKEY_SEIKYUSHO As String = "H01R31" '請求書

    '---------------------------------------------------------------------------------
    'レポート作表
    '---------------------------------------------------------------------------------
    Public Sub runReport() Implements RepSectionIF.runReport
        Dim v As New PreviewForm
        v.viewer.Document = Me.Document
        Me.Run()
        If frmH01F70_Chohyo.printKbn = CommonConst.REPORT_PREVIEW Then
            v.Show()            'プレビュー画面
        Else
            Me.Document.Print   'プリンタ選択ダイアログ
        End If
    End Sub

    '---------------------------------------------------------------------------------
    'データ受領
    '---------------------------------------------------------------------------------
    Public Sub setData(prmDs As DataSet, ByRef prmRefDbHd As UtilDBIf) Implements RepSectionIF.setData
        Dim dt As DataTableCollection

        _ds = prmDs
        dt = _ds.Tables
        _PageCount = dt.Item(0).Rows.Count                                  'ページカウント
        _db = prmRefDbHd
        rowIdx = 0
        ' 共通処理使用の準備
        _comLogc = New CommonLogic(_db, _msgHd)                             '共通処理用
        _companyCd = frmC01F10_Login.loginValue.BumonCD                     '会社コード
        _shoriId = frmH01F70_Chohyo._shoriId                                '処理ID
        _userId = frmC01F10_Login.loginValue.TantoCD                        'ユーザＩＤ
        _printKbn = frmH01F70_Chohyo._printKbn
        _seikyuKbn = frmH01F70_Chohyo._seikyuKbn

        '汎用マスタ内容格納
        Call _comLogc.Get_HanyouMST()

        '納品書・請求書の帳票備考取得
        For lidx As Integer = 0 To UBound(CommonLogic.uHanyou_tb) - 1
            If CommonLogic.uHanyou_tb(lidx).KoteiKey.Equals(CommonConst.HANYO_CHOHYO_BIKOU) Then
                If CommonLogic.uHanyou_tb(lidx).KahenKey.Equals(HANYO_KAHENKEY_NOHINSHO) Then
                    sNohinStr = CommonLogic.uHanyou_tb(lidx).Char1
                ElseIf CommonLogic.uHanyou_tb(lidx).KahenKey.Equals(HANYO_KAHENKEY_SEIKYUSHO) Then
                    sSeikyuStr = CommonLogic.uHanyou_tb(lidx).Char1
                End If
            End If
        Next

    End Sub

    '---------------------------------------------------------------------------------
    'レポートスタート
    '---------------------------------------------------------------------------------
    Private Sub H01R30_Nouhinsho_ReportStart(sender As Object, e As EventArgs) Handles MyBase.ReportStart
        rpt = New H01R30S_Nouhinsho()
        rpt2 = New H01R30S_Nouhinsho()
    End Sub

    '---------------------------------------------------------------------------------
    'データイニシャライズ
    '---------------------------------------------------------------------------------
    Private Sub H01R30_Nouhinsho_DataInitialize(sender As Object, e As EventArgs) Handles MyBase.DataInitialize

    End Sub

    '---------------------------------------------------------------------------------
    'ページヘッダ初期化時
    '---------------------------------------------------------------------------------
    Private Sub PageHeader_Format(sender As Object, e As EventArgs) Handles PageHeader.Format

    End Sub

    '---------------------------------------------------------------------------------
    'フェッチデータ
    '---------------------------------------------------------------------------------
    Private Sub H01R30_Nouhinsho_FetchData(sender As Object, eArgs As FetchEventArgs) Handles MyBase.FetchData
        Try
            '帳票見出
            txt帳票名.Text = _seikyuKbn
            txt帳票名控.Text = _seikyuKbn & "(控)"

            txtUnsoubin1.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("運送便"))
            lblYuubinNo1.Text = "〒 " & _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("郵便番号")).Substring(0, 3) & "-" & _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("郵便番号")).Substring(3, 4)
            lblAddress1_1.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("住所１")) & _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("住所２"))
            lblAddress1_2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("住所３"))
            lblShukkasakiNm1.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("出荷先名")) & "　" & _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("担当者名")) & "　様"
            lblShukkasakiCd1.Text = "(" & _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("出荷先コード")) & ")"
            lblCompanyNm1.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("会社名"))
            lblCoPosition1.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("会社代表者役職"))
            lblCoPresidentNm1.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("会社代表者名"))
            lblCoYuubinNo1.Text = "〒" & _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("会社郵便番号")).Substring(0, 3) & "-" & _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("会社郵便番号")).Substring(3, 4)
            lblCoAddress1.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("会社住所１")) & _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("会社住所２")) & _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("会社住所３"))
            lblCoTelNo1.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("会社電話番号"))
            lblCoFaxNo1.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("会社ＦＡＸ番号"))

            txtPageNo.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("ページ"))
            txtShagaiBikou1.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("社外備考"))
            '金額
            _kingakuSum = _kingakuSum + Integer.Parse(_ds.Tables(RS).Rows(rowIdx)("注文金額"))
            If _PageCount > Integer.Parse(_ds.Tables(RS).Rows(rowIdx)("ページ")) Then
                txtKingakuSum1.Text = "******"
            Else
                txtKingakuSum1.Value = _kingakuSum.ToString()
            End If
            txtJikanSitei1.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("時間指定"))
            txtShukkaDt1.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("出荷日"))
            txtChakuDt1.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("着日"))
            txtDenpyoNo1.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("注文伝番"))
            txtIrainusi1.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("依頼主等"))

            txtUnsoubin2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("運送便"))
            lblYuubinNo2.Text = "〒 " & _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("郵便番号")).Substring(0, 3) & "-" & _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("郵便番号")).Substring(3, 4)
            lblAddress2_1.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("住所１")) & _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("住所２"))
            lblAddress2_2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("住所３"))
            lblShukkasakiNm2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("出荷先名")) & "　" & _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("担当者名")) & "　様"
            lblShukkasakiCd2.Text = "(" & _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("出荷先コード")) & ")"
            lblCompanyNm2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("会社名"))
            lblCoPosition2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("会社代表者役職"))
            lblCoPresidentNm2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("会社代表者名"))
            lblCoYuubinNo2.Text = "〒" & _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("会社郵便番号")).Substring(0, 3) & "-" & _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("会社郵便番号")).Substring(3, 4)
            lblCoAddress2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("会社住所１")) & _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("会社住所２")) & _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("会社住所３"))
            lblCoTelNo2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("会社電話番号"))
            lblCoFaxNo2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("会社ＦＡＸ番号"))

            txtShagaiBikou2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("社外備考"))
            '金額
            If _PageCount > Integer.Parse(_ds.Tables(RS).Rows(rowIdx)("ページ")) Then
                txtKingakuSum2.Text = "******"
            Else
                txtKingakuSum2.Value = _kingakuSum.ToString()
            End If
            txtJikanSitei2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("時間指定"))
            txtShukkaDt2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("出荷日"))
            txtChakuDt2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("着日"))
            txtDenpyoNo2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("注文伝番"))
            txtIrainusi2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("依頼主等"))

            '帳票備考
            If _seikyuKbn.Equals(CommonConst.REPORT_NOHINSHO) Then
                txt帳票備考.Text = sNohinStr
            Else
                txt帳票備考.Text = sSeikyuStr
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
    Private Sub H01R30_Nouhinsho_ReportEnd(sender As Object, e As EventArgs) Handles MyBase.ReportEnd
        rpt.Dispose()
        rpt2.Dispose()
    End Sub

    Private Sub GroupFooter1_Format(sender As Object, e As EventArgs) Handles GroupFooter1.Format

    End Sub

    Private Sub PageFooter_Format(sender As Object, e As EventArgs) Handles PageFooter.Format

    End Sub

    Private Sub Detail_Format(sender As Object, e As EventArgs) Handles Detail.Format

        'データ存在チェック
        Dim sSql As String = ""
        Dim rc As Integer = 0

        sSql = sSql & "SELECT "
        sSql = sSql & "     SUB.行番 "
        sSql = sSql & "    ,SUB.商品コード "
        sSql = sSql & "    ,SUB.商品名 "
        sSql = sSql & "    ,SUB.文字１ "
        sSql = sSql & "    ,SUB.入数 "
        sSql = sSql & "    ,SUB.個数 "
        sSql = sSql & "    ,SUB.単位 "
        sSql = sSql & "    ,SUB.数量 "
        sSql = sSql & "    ,SUB.注文単価 "
        sSql = sSql & "    ,SUB.注文金額 "
        sSql = sSql & "    ,SUB.明細備考 "
        sSql = sSql & "FROM "
        sSql = sSql & "( "
        sSql = sSql & "    SELECT  "
        sSql = sSql & "          T11_CYMNDT.行番 "
        sSql = sSql & "         ,T11_CYMNDT.商品コード "
        sSql = sSql & "         ,T11_CYMNDT.商品名 || T11_CYMNDT.荷姿形状 AS ""商品名"" "
        sSql = sSql & "         ,CASE WHEN T11_CYMNDT.冷凍区分 = '" & CommonConst.HANYO_REITOU_KBN & "' THEN M90_HANYO.文字１ ELSE NULL END AS ""文字１"" "
        sSql = sSql & "         ,T11_CYMNDT.入数 "
        sSql = sSql & "         ,T11_CYMNDT.個数 "
        sSql = sSql & "         ,T11_CYMNDT.単位 "
        sSql = sSql & "         ,T11_CYMNDT.数量 || ' 個' AS ""数量"" "
        sSql = sSql & "         ,T11_CYMNDT.注文単価 "
        sSql = sSql & "         ,T11_CYMNDT.注文金額 "
        sSql = sSql & "         ,T11_CYMNDT.明細備考 "
        sSql = sSql & "    FROM T11_CYMNDT "
        sSql = sSql & "    LEFT JOIN M90_HANYO ON M90_HANYO.会社コード = T11_CYMNDT.会社コード "
        sSql = sSql & "     AND M90_HANYO.固定キー = '" & CommonConst.HANYO_REITOU_KBN & "' "
        sSql = sSql & "     AND M90_HANYO.可変キー = T11_CYMNDT.冷凍区分 "
        sSql = sSql & "    WHERE T11_CYMNDT.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' "
        sSql = sSql & "      AND T11_CYMNDT.注文伝番 = '" & Me.txtDenpyoNo1.Text & "' "
        sSql = sSql & "    UNION "
        sSql = sSql & "    SELECT "
        sSql = sSql & "          MAX(T11_CYMNDT.行番) + 1 AS ""行番"" "
        sSql = sSql & "         ,NULL AS ""商品コード"" "
        sSql = sSql & "         ,NULL AS ""商品名"" "
        sSql = sSql & "         ,NULL AS ""文字１"" "
        sSql = sSql & "         ,NULL AS ""入数"" "
        sSql = sSql & "         ,NULL AS ""個数"" "
        sSql = sSql & "         ,NULL AS ""単位"" "
        sSql = sSql & "         ,'消費税額' AS ""数量"" "
        sSql = sSql & "         ,NULL AS ""注文単価"" "
        sSql = sSql & "         ,TRUNC( SUM(COALESCE(T11_CYMNDT.消費税,0))) AS ""注文金額"" "
        sSql = sSql & "         ,NULL AS ""明細備考"" "
        sSql = sSql & "    FROM T11_CYMNDT "
        sSql = sSql & "    WHERE T11_CYMNDT.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' "
        sSql = sSql & "      AND T11_CYMNDT.注文伝番 = '" & Me.txtDenpyoNo1.Text & "' "
        sSql = sSql & "    GROUP BY T11_CYMNDT.注文伝番 "
        sSql = sSql & ") SUB "
        sSql = sSql & "WHERE TRUNC(((SUB.行番 - 1) / 6) + 1) = '" & txtPageNo.Text & "' "
        sSql = sSql & "ORDER BY SUB.行番 "

        Dim dsBase As DataSet = _db.selectDB(sSql, RS, rc)
        Dim ds As DataSet
        If _PageCount = Integer.Parse(txtPageNo.Text) Then
            '空行追加
            ds = add_BlankRec(dsBase)
        Else
            ds = dsBase
        End If

        rpt.DataSource = ds
        rpt.DataMember = RS
        SubReport1.Report = rpt

        rpt2.DataSource = ds
        rpt2.DataMember = RS
        SubReport2.Report = rpt2

    End Sub


    '-------------------------------------------------------------------------------
    '　明細の最終レコードを消費税にするため空レコードを差し込む
    '-------------------------------------------------------------------------------
    Private Function add_BlankRec(ByVal paraDs As DataSet) As DataSet

        Dim ds As New DataSet
        Dim dsIdx As Integer = 0
        Dim dt As DataTable = New DataTable(RS)

        '列追加
        dt.Columns.Add("行番", GetType(Integer))
        dt.Columns.Add("商品コード", GetType(String))
        dt.Columns.Add("商品名", GetType(String))
        dt.Columns.Add("文字１", GetType(String))
        dt.Columns.Add("入数", GetType(String))
        dt.Columns.Add("個数", GetType(String))
        dt.Columns.Add("単位", GetType(String))
        dt.Columns.Add("数量", GetType(String))
        dt.Columns.Add("注文単価", GetType(String))
        dt.Columns.Add("注文金額", GetType(String))
        dt.Columns.Add("明細備考", GetType(String))

        Dim lineNo As Integer = 0
        For index As Integer = 0 To paraDs.Tables(RS).Rows.Count - 1

            If index = 0 Then
                lineNo = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("行番"))
            Else
                lineNo = lineNo + 1
            End If
            Dim newRow As DataRow = dt.NewRow

            '消費税レコードの場合
            If _db.rmNullStr(paraDs.Tables(RS).Rows(index)("数量")).Equals("消費税額") Then

                For addidx As Integer = lineNo To _PageCount * MAX_MEISAI - 1
                    newRow("行番") = lineNo
                    newRow("商品コード") = ""
                    newRow("商品名") = ""
                    newRow("文字１") = ""
                    newRow("入数") = ""
                    newRow("個数") = ""
                    newRow("単位") = ""
                    newRow("数量") = ""
                    newRow("注文単価") = ""
                    newRow("注文金額") = ""
                    newRow("明細備考") = ""
                    '追加
                    dt.Rows.Add(newRow)
                    lineNo = lineNo + 1
                    newRow = dt.NewRow
                Next
            End If

            newRow("行番") = lineNo
            newRow("商品コード") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("商品コード"))
            newRow("商品名") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("商品名"))
            newRow("文字１") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("文字１"))
            newRow("入数") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("入数"))
            newRow("個数") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("個数"))
            newRow("単位") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("単位"))
            newRow("数量") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("数量"))
            newRow("注文単価") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("注文単価"))
            newRow("注文金額") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("注文金額"))
            newRow("明細備考") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("明細備考"))

            dt.Rows.Add(newRow)
        Next

        'DataSetにdtを追加
        ds.Tables.Add(dt)

        Return ds

    End Function

End Class
