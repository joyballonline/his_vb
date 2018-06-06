'===============================================================================
'
'　カネキ吉田商店様
'　　（システム名）販売管理
'　　（処理機能名）仕入先元帳
'    （フォームID）H11R03
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   狩野        2018/03/20                 新規              
'-------------------------------------------------------------------------------
Imports GrapeCity.ActiveReports
Imports GrapeCity.ActiveReports.Document
Imports GrapeCity.ActiveReports.SectionReportModel
Imports HanbaiKanri
Imports UtilMDL.DB
Imports UtilMDL.MSG

Public Class H11R03_ShiiresakiMotoList
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
    'Private jyokenBotonn As String = ""
    Private _denpyoBango As String = "000000"
    Private _zandaka As Decimal = 0
    Private _shiharaisakiCd As String = ""
    Private _hidukeFrom As String = ""
    Private _hidukeTo As String = ""
    'サブレポート
    Private rptSub As H11R03_ShiiresakiMotoList

    Private Const RS As String = "RecSet"                   'レコードセットテーブル


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
        If frmH11F03_SiiresakiMotoList.printKbn = CommonConst.REPORT_PREVIEW Then
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
        _shoriId = frmH11F03_SiiresakiMotoList.shoriId                         '処理ID
        _userId = frmC01F10_Login.loginValue.TantoCD                        'ユーザＩＤ
        _printKbn = frmH11F03_SiiresakiMotoList.printKbn

    End Sub

    '---------------------------------------------------------------------------------
    'レポートスタート
    '---------------------------------------------------------------------------------
    Private Sub H11R03_ShiiresakiMotoList_ReportStart(sender As Object, e As EventArgs) Handles MyBase.ReportStart
        rptSub = New H11R03_ShiiresakiMotoList()
    End Sub

    '---------------------------------------------------------------------------------
    'フェッチデータ
    '---------------------------------------------------------------------------------
    Private Sub H11R03_ShiiresakiMotoList_FetchData(sender As Object, eArgs As FetchEventArgs) Handles MyBase.FetchData
        Try

            'グルーピング用
            fld支払先.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("支払先コード"))
            _shiharaisakiCd = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("支払先コード"))
            _hidukeFrom = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("条件_日付From"))
            _hidukeTo = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("条件_日付To"))

            'ページヘッダ
            txt区分ヘッダ.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("条件_区分"))
            txt仕入支払年月ヘッダ.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("条件_年月"))
            txt日付ヘッダ.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("条件_日付"))
            txt作成日ヘッダ.Text = _comLogc.getSysDdate
            txt出力順ヘッダ.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("条件_出力順"))
            'ページはプロパティで行っている

            '請求先ヘッダ
            txt支払先コード.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("支払先コード"))
            txt支払先名.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("支払先名"))

            '残高計算　前明細の残高＋仕入額＋消費税－支払額
            If _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("取得元テーブル")).Equals("1") Then
                _zandaka = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("残高"))
            Else
                _zandaka = _zandaka + _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("仕入額")) _
                                    + _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("消費税")) _
                                    - _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("支払額"))

            End If

            '明細
            If _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("取得元テーブル")).Equals("1") Then
                '債務残高
                txt仕入日.Text = ""
                txt伝票番号.Text = ""
                txt区分.Text = ""
                txt仕入先名.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("仕入先"))
                txt商品摘要.Text = ""
                txt数量.Text = ""
                txt単位.Text = ""
                txt単価.Text = ""

                '支払先フッタ集計用（集計はプロパティで行っている）
                fld仕入額.Value = 0
                fld消費税.Value = 0
                fld支払額.Value = 0
                fld残高.Value = _zandaka

            ElseIf _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("取得元テーブル")).Equals("2") Then
                '仕入基本／明細
                If Not _denpyoBango.Equals(_db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("仕入伝番"))) Then
                    txt仕入日.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("仕入日"))
                    txt伝票番号.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("仕入伝番"))
                    txt区分.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("区分"))
                    txt仕入先名.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("仕入先"))
                Else
                    '重複列非表示
                    txt仕入日.Text = ""
                    txt伝票番号.Text = ""
                    txt区分.Text = ""
                    txt仕入先名.Text = ""
                End If
                txt商品摘要.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("商品仕入摘要"))
                txt数量.Text = Decimal.Parse(_ds.Tables(RS).Rows(rowIdx)("数量")).ToString("#,##0.00")
                txt単位.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("単位"))
                txt単価.Text = Decimal.Parse(_ds.Tables(RS).Rows(rowIdx)("単価")).ToString("#,##0.00")

                '支払先フッタ集計用（集計はプロパティで行っている）
                fld仕入額.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("仕入額"))
                fld消費税.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("消費税"))
                fld支払額.Value = 0
                fld残高.Value = _zandaka
            Else
                '支払基本／明細
                If Not _denpyoBango.Equals(_db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("仕入伝番"))) Then
                    txt仕入日.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("仕入日"))
                    txt伝票番号.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("仕入伝番"))
                    txt区分.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("区分"))
                Else
                    '重複列非表示
                    txt仕入日.Text = ""
                    txt伝票番号.Text = ""
                    txt区分.Text = ""
                End If
                txt仕入先名.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("仕入先"))
                txt商品摘要.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("商品仕入摘要"))
                txt数量.Text = ""
                txt単位.Text = ""
                txt単価.Text = ""
                txt仕入額.Text = ""
                txt消費税.Text = ""

                '支払先フッタ集計用（集計はプロパティで行っている）
                fld仕入額.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("仕入額"))
                fld消費税.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("消費税"))
                fld支払額.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("支払額"))
                fld残高.Value = _zandaka
            End If


            '明細の上線
            If _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("取得元テーブル")).Equals("1") Then
                Line4.Visible = False
            ElseIf Not _denpyoBango.Equals(_db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("仕入伝番"))) Then
                Line4.Visible = True
            Else
                Line4.Visible = False
            End If
            _denpyoBango = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("仕入伝番"))

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
    Private Sub H11R03_ShiiresakiMotoList_ReportEnd(sender As Object, e As EventArgs) Handles MyBase.ReportEnd

        '操作履歴ログ作成
        Call _comLogc.Insert_L01_ProcLog(_companyCd, DBNull.Value, _shoriId, CommonConst.PROGRAM_REPORT, CommonConst.STATUS_NORMAL,
                                                CommonConst.SHIIRE_KBN_NM_SHIIRE, txt仕入支払年月ヘッダ.Text, txt日付ヘッダ.Text, _printKbn, DBNull.Value,
                                                iMeisaiCnt, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _userId)


    End Sub

End Class
