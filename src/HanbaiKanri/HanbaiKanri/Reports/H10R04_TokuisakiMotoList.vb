'===============================================================================
'
'　カネキ吉田商店様
'　　（システム名）販売管理
'　　（処理機能名）得意先元帳
'    （フォームID）H10R04
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   狩野        2018/03/12                 新規              
'-------------------------------------------------------------------------------
Imports GrapeCity.ActiveReports
Imports GrapeCity.ActiveReports.Document
Imports GrapeCity.ActiveReports.SectionReportModel
Imports HanbaiKanri
Imports UtilMDL.DB
Imports UtilMDL.MSG

Public Class H10R04_TokuisakiMotoList
    Implements RepSectionIF

    Private _msgHd As UtilMsgHandler
    Private Shared _db As UtilDBIf
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
    Private _denpyoBango As String = "000000"
    Private _zandaka As Decimal = 0
    Private _seikyusakiCd As String = ""
    Private _hidukeFrom As String = ""
    Private _hidukeTo As String = ""
    Private _itakuInfo As String = ""
    Private _itakuZanStr As String = ""
    'サブレポート
    Private rptSub As H10R04S_TokuisakiMotoList

    Private Const RS As String = "RecSet"                   'レコードセットテーブル
    Private Const RS2 As String = "RecSet2"                   'レコードセットテーブル
    Private Const ITAKU_INFO_NO As String = "出力しない"
    Private Const ITAKU_ZAN_YES As String = "委託残指定あり"
    Private Const ITAKU_ZAN_NO As String = "委託残指定なし"


    '-------------------------------------------------------------------------------
    'プロパティ宣言
    '-------------------------------------------------------------------------------
    Public Shared ReadOnly Property parentDb() As UtilDBIf
        Get
            Return _db
        End Get
    End Property


    '---------------------------------------------------------------------------------
    'レポート作表
    '---------------------------------------------------------------------------------
    Public Sub runReport() Implements RepSectionIF.runReport
        Dim v As New PreviewForm
        v.viewer.Document = Me.Document
        Me.Run()
        If frmH10F04_TokuisakiMotoList.printKbn = CommonConst.REPORT_PREVIEW Then
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
        _shoriId = frmH10F04_TokuisakiMotoList.shoriId                         '処理ID
        _userId = frmC01F10_Login.loginValue.TantoCD                        'ユーザＩＤ
        _printKbn = frmH10F04_TokuisakiMotoList.printKbn

    End Sub

    '---------------------------------------------------------------------------------
    'フェッチデータ
    '---------------------------------------------------------------------------------
    Private Sub H10R04_TokuisakiMotoList_FetchData(sender As Object, eArgs As FetchEventArgs) Handles MyBase.FetchData
        Try

            'グルーピング用
            fld請求先.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("請求先コード"))
            _seikyusakiCd = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("請求先コード"))
            _hidukeFrom = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("条件_日付From"))
            _hidukeTo = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("条件_日付To"))
            _itakuInfo = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("条件_委託残情報"))

            'ページヘッダ
            txt区分ヘッダ.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("条件_区分"))
            txt売上入金年月ヘッダ.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("条件_年月"))
            txt日付ヘッダ.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("条件_日付"))
            txt作成日ヘッダ.Text = _comLogc.getSysDdate
            txt出力順ヘッダ.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("条件_出力順"))
            jyokenBotonn = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("条件_ログ用"))
            'ページはプロパティで行っている

            '請求先ヘッダ
            txt請求先コード.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("請求先コード"))
            txt請求先名.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("請求先名"))

            '残高計算　前明細の残高＋売上額＋消費税－入金額
            If _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("取得元テーブル")).Equals("1") Then
                _zandaka = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("残高"))
            Else
                _zandaka = _zandaka + _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("売上額")) _
                                    + _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("消費税")) _
                                    - _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("入金額"))

            End If

            '明細
            If _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("取得元テーブル")).Equals("1") Then
                '債権残高
                txt売上日.Text = ""
                txt伝票番号.Text = ""
                txt区分.Text = ""
                txt出荷先名.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("出荷先"))
                txt委託残あり表示.Text = ""
                txt商品入金摘要.Text = ""
                txt数量.Text = ""
                txt単位.Text = ""
                txt単価.Text = ""

                '請求先フッタ集計用（集計はプロパティで行っている）
                fld売上額.Value = 0
                fld消費税.Value = 0
                fld入金額.Value = 0
                fld残高.Value = _zandaka

            ElseIf _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("取得元テーブル")).Equals("2") Then
                '売上基本／明細
                If Not _denpyoBango.Equals(_db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("伝票番号"))) Then
                    txt売上日.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("売上日"))
                    txt伝票番号.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("伝票番号"))
                    txt区分.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("区分"))
                    txt出荷先名.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("出荷先"))
                Else
                    '重複列非表示
                    txt売上日.Text = ""
                    txt伝票番号.Text = ""
                    txt区分.Text = ""
                    txt出荷先名.Text = ""
                End If
                txt委託残あり表示.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("委託残あり表示"))
                txt商品入金摘要.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("商品入金摘要"))
                txt数量.Text = Decimal.Parse(_ds.Tables(RS).Rows(rowIdx)("数量")).ToString("#,##0.00")
                txt単位.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("単位"))
                txt単価.Text = Decimal.Parse(_ds.Tables(RS).Rows(rowIdx)("単価")).ToString("#,##0.00")

                '請求先フッタ集計用（集計はプロパティで行っている）
                fld売上額.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("売上額"))
                fld消費税.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("消費税"))
                fld入金額.Value = 0
                fld残高.Value = _zandaka
            Else
                '入金基本／明細
                If Not _denpyoBango.Equals(_db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("伝票番号"))) Then
                    txt売上日.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("売上日"))
                    txt伝票番号.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("伝票番号"))
                    txt区分.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("区分"))
                Else
                    '重複列非表示
                    txt売上日.Text = ""
                    txt伝票番号.Text = ""
                    txt区分.Text = ""
                End If
                txt出荷先名.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("出荷先"))
                txt委託残あり表示.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("委託残あり表示"))
                txt商品入金摘要.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("商品入金摘要"))
                txt数量.Text = ""
                txt単位.Text = ""
                txt単価.Text = ""
                txt売上額.Text = ""
                txt消費税.Text = ""

                '請求先フッタ集計用（集計はプロパティで行っている）
                fld売上額.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("売上額"))
                fld消費税.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("消費税"))
                fld入金額.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("入金額"))
                fld残高.Value = _zandaka
            End If
            txt委託日.Text = ""

            '委託残情報の表示
            If _itakuInfo.Equals(ITAKU_INFO_NO) Then
                txt委託残あり表示.Visible = False
                lbl委託残あり.Visible = False
                SubReport1.Visible = False
                _itakuZanStr = ITAKU_ZAN_NO
            Else
                txt委託残あり表示.Visible = True
                lbl委託残あり.Visible = True
                SubReport1.Visible = True
                _itakuZanStr = ITAKU_ZAN_YES
            End If

            '明細の上線
            If _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("取得元テーブル")).Equals("1") Then
                Line4.Visible = False
            ElseIf Not _denpyoBango.Equals(_db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("伝票番号"))) Then
                Line4.Visible = True
            Else
                Line4.Visible = False
            End If
            _denpyoBango = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("伝票番号"))

            'ページフッタ
            txt会社名.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("会社名"))

            'レポートフッタ

            rowIdx = rowIdx + 1

            'ログ用
            iMeisaiCnt = rowIdx

            eArgs.EOF = False
        Catch ie As IndexOutOfRangeException
            eArgs.EOF = True 'データ末尾
        End Try

    End Sub


    '---------------------------------------------------------------------------------
    'Detail_Format（ページフッターの文言設定）
    '---------------------------------------------------------------------------------
    Private Sub Detail_Format(sender As Object, e As EventArgs) Handles Detail.Format

    End Sub

    '---------------------------------------------------------------------------------
    'ページヘッダー（ページフッターの文言設定）
    '---------------------------------------------------------------------------------
    Private Sub PageHeader_Format(sender As Object, e As EventArgs) Handles PageHeader.Format

    End Sub

    '---------------------------------------------------------------------------------
    'レポートエンド
    '---------------------------------------------------------------------------------
    Private Sub H10R04_TokuisakiMotoList_ReportEnd(sender As Object, e As EventArgs) Handles MyBase.ReportEnd

        '操作履歴ログ作成
        Call _comLogc.Insert_L01_ProcLog(_companyCd, DBNull.Value, _shoriId, CommonConst.PROGRAM_REPORT, CommonConst.STATUS_NORMAL,
                                                jyokenBotonn, txt売上入金年月ヘッダ.Text, txt日付ヘッダ.Text, _printKbn, _itakuZanStr,
                                                iMeisaiCnt, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _userId)


    End Sub


    '---------------------------------------------------------------------------------
    'グループヘッダ
    '---------------------------------------------------------------------------------
    Private Sub gh請求先_Format(sender As Object, e As EventArgs) Handles gh請求先.Format

        'サブレポート
        Dim sSql As String = ""
        Dim rc As Integer = 0

        sSql = sSql & "SELECT "
        sSql = sSql & "  t15.着日          委託日"
        sSql = sSql & " ,t15.委託伝番      伝票番号"
        sSql = sSql & " ,'委託'            区分"
        sSql = sSql & " ,t15.出荷先名      出荷先"
        sSql = sSql & " ,t16.商品名 || COALESCE(t16.荷姿形状,'') 商品名"       'NVL
        sSql = sSql & " ,t16.委託残数      残数量"
        sSql = sSql & " ,t16.単位          単位"
        sSql = sSql & " ,t16.仮単価"
        sSql = sSql & " ,t16.委託残金額    見込額"
        sSql = sSql & " FROM t15_itakhd t15"      '委託基本
        sSql = sSql & " LEFT JOIN t16_itakdt As t16    On t16.会社コード   = t15.会社コード"
        sSql = sSql & "                               And t16.委託伝番     = t15.委託伝番"
        sSql = sSql & " WHERE t15.会社コード   = '" & _companyCd & "'"
        sSql = sSql & "   AND t15.取消区分     = '0'"
        sSql = sSql & "   AND t15.請求先コード = '" & _seikyusakiCd & "'"
        sSql = sSql & "   AND TO_CHAR(t15.着日,'YYYY/MM/DD') >= '" & _hidukeFrom & "'"
        sSql = sSql & "   AND TO_CHAR(t15.着日,'YYYY/MM/DD') <= '" & _hidukeTo & "'"
        sSql = sSql & " ORDER BY t15.着日, t15.委託伝番, t16.行番"

        Dim ds As DataSet = _db.selectDB(sSql, RS2, rc)

        rptSub.DataSource = ds
        rptSub.DataMember = RS2
        SubReport1.Report = rptSub

    End Sub

    '---------------------------------------------------------------------------------
    'レポートスタート
    '---------------------------------------------------------------------------------
    Private Sub H10R04_TokuisakiMotoList_ReportStart(sender As Object, e As EventArgs) Handles MyBase.ReportStart
        rptSub = New H10R04S_TokuisakiMotoList()
    End Sub

End Class
