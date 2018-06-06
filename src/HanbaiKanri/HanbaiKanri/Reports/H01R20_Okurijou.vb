'===============================================================================
'
'　カネキ吉田商店様
'　　（システム名）販売管理
'　　（処理機能名）送り状
'    （フォームID）H01R20
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   鴫原        2018/03/02                 新規              
'-------------------------------------------------------------------------------
Imports GrapeCity.ActiveReports
Imports GrapeCity.ActiveReports.Document
Imports GrapeCity.ActiveReports.SectionReportModel
Imports HanbaiKanri
Imports UtilMDL.DB
Imports UtilMDL.MSG

Public Class H01R20_Okurijou
    Implements RepSectionIF

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _ds As DataSet
    Private _comLogc As CommonLogic                             '共通処理用
    Private _companyCd As String
    Private _shoriId As String
    Private _userId As String
    Private _printKbn As String
    Private rowIdx As Integer = 0
    Private _PageCount As Integer = 0
    Private _kosuSum As Integer = 0
    Private iDenpyoCnt As Integer = 0
    Private iMeisaiCnt As Integer = 0
    Private dcUriageKin As Decimal = 0
    Private dcShohizei As Decimal = 0
    Private dcGoukeiKin As Decimal = 0
    Private RowNumber As Integer = 0　　                        ' 件数カウンタ
    Private sDenpyoNo As String = ""
    Private sOkurijyoStr As String = ""

    Private rpt As H01R20S_Okurijou
    Private rpt2 As H01R20S_Okurijou

    Private Const RS As String = "RecSet"                       'レコードセットテーブル
    Private Const HANYO_KAHENKEY_OKURIJYO As String = "H01R20"  '送り状

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

        '汎用マスタ内容格納
        Call _comLogc.Get_HanyouMST()

        '納品書・請求書の帳票備考取得
        For lidx As Integer = 0 To UBound(CommonLogic.uHanyou_tb) - 1
            If CommonLogic.uHanyou_tb(lidx).KoteiKey.Equals(CommonConst.HANYO_CHOHYO_BIKOU) Then
                If CommonLogic.uHanyou_tb(lidx).KahenKey.Equals(HANYO_KAHENKEY_OKURIJYO) Then
                    sOkurijyoStr = CommonLogic.uHanyou_tb(lidx).Char1
                End If
            End If
        Next

    End Sub

    '---------------------------------------------------------------------------------
    'レポートスタート
    '---------------------------------------------------------------------------------
    Private Sub H01R20_Okurijou_ReportStart(sender As Object, e As EventArgs) Handles MyBase.ReportStart
        rpt = New H01R20S_Okurijou()
        rpt2 = New H01R20S_Okurijou()
    End Sub

    '---------------------------------------------------------------------------------
    'データイニシャライズ
    '---------------------------------------------------------------------------------
    Private Sub H01R20_Okurijou_DataInitialize(sender As Object, e As EventArgs) Handles MyBase.DataInitialize

    End Sub

    '---------------------------------------------------------------------------------
    'ページヘッダ初期化時
    '---------------------------------------------------------------------------------
    Private Sub PageHeader_Format(sender As Object, e As EventArgs) Handles PageHeader.Format

    End Sub

    '---------------------------------------------------------------------------------
    'フェッチデータ
    '---------------------------------------------------------------------------------
    Private Sub H01R20_Okurijou_FetchData(sender As Object, eArgs As FetchEventArgs) Handles MyBase.FetchData
        Try
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
            '個数
            _kosuSum = _kosuSum + Integer.Parse(_ds.Tables(RS).Rows(rowIdx)("個数"))
            If _PageCount > Integer.Parse(_ds.Tables(RS).Rows(rowIdx)("ページ")) Then
                txtKosuuSum1.Text = "***"
            Else
                txtKosuuSum1.Text = _kosuSum.ToString()
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
            '個数
            If _PageCount > Integer.Parse(_ds.Tables(RS).Rows(rowIdx)("ページ")) Then
                txtKosuuSum2.Text = "***"
            Else
                txtKosuuSum2.Text = _kosuSum.ToString()
            End If
            txtJikanSitei2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("時間指定"))
            txtShukkaDt2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("出荷日"))
            txtChakuDt2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("着日"))
            txtDenpyoNo2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("注文伝番"))
            txtIrainusi2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("依頼主等"))

            '帳票備考
            txt帳票備考.Text = sOkurijyoStr

            rowIdx = rowIdx + 1
            eArgs.EOF = False
        Catch ie As IndexOutOfRangeException
            eArgs.EOF = True 'データ末尾
        End Try

    End Sub

    '---------------------------------------------------------------------------------
    'レポートエンド
    '---------------------------------------------------------------------------------
    Private Sub H01R20_Okurijou_ReportEnd(sender As Object, e As EventArgs) Handles MyBase.ReportEnd
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
        sSql = sSql & "      TRUNC(((T11_CYMNDT.行番 - 1)/ 6) + 1) AS ""ページ"" "
        sSql = sSql & "     ,T11_CYMNDT.商品コード  "
        sSql = sSql & "     ,T11_CYMNDT.商品名 || T11_CYMNDT.荷姿形状 AS ""商品名"" "
        sSql = sSql & "     ,CASE WHEN T11_CYMNDT.冷凍区分 = '" & CommonConst.REITOU_KBN_REITOU & "' THEN M90_HANYO.文字１ ELSE NULL END AS ""文字１"" "
        sSql = sSql & "     ,T11_CYMNDT.入数 "
        sSql = sSql & "     ,T11_CYMNDT.単位 "
        sSql = sSql & "     ,T11_CYMNDT.個数 "
        sSql = sSql & "     ,'個' AS ""単位２"" "
        sSql = sSql & "     ,T11_CYMNDT.明細備考 "
        sSql = sSql & "FROM T11_CYMNDT "
        sSql = sSql & "LEFT JOIN M90_HANYO On M90_HANYO.会社コード = T11_CYMNDT.会社コード "
        sSql = sSql & " And M90_HANYO.固定キー = '" & CommonConst.HANYO_REITOU_KBN & "' "
        sSql = sSql & " AND M90_HANYO.可変キー = T11_CYMNDT.冷凍区分 "
        sSql = sSql & "WHERE T11_CYMNDT.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' "
        sSql = sSql & "  AND T11_CYMNDT.注文伝番 = '" & Me.txtDenpyoNo1.Text & "' "
        sSql = sSql & "  AND TRUNC(((T11_CYMNDT.行番 - 1) / 6) + 1) = '" & txtPageNo.Text & "' "
        sSql = sSql & "ORDER BY T11_CYMNDT.行番 "

        Dim ds As DataSet = _db.selectDB(sSql, RS, rc)

        rpt.DataSource = ds
        rpt.DataMember = RS
        SubReport1.Report = rpt

        Dim ds2 As DataSet = _db.selectDB(sSql, RS, rc)

        rpt2.DataSource = ds2
        rpt2.DataMember = RS
        SubReport2.Report = rpt2

    End Sub

End Class
