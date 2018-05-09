Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports System.Text.RegularExpressions


Public Class frmH01F70_Chohyo
    '-------------------------------------------------------------------------------
    '   変数定義
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _frmH01F60 As frmH01F60_Chumon

    Private _SelectMode As Integer   'メニューのどこから呼ばれたか。（1:登録、2:変更、3:取消、4:照会)
    Private _comLogc As CommonLogic                         '共通処理用
    Private _SelectID As String
    Private _Printed As Boolean = False     'フォームからの戻り値用　True:印刷された False:戻るボタンで戻った
    Private _userId As String
    Public Shared _shoriId As String
    Public Shared _printKbn As String
    Public Shared _seikyuKbn As String

    '-------------------------------------------------------------------------------
    '   定数定義
    '-------------------------------------------------------------------------------
    Private Const N As String = ControlChars.NewLine            '改行文字
    Private Const RS As String = "RecSet"                       'レコードセットテーブル

    'グリッド列№
    'dgvIchiran
    Private Const COLNO_NO = 0                                      '01:No.
    Private Const COLNO_Nifuda = 1                                  '02:荷札
    Private Const COLNO_ShohinNM1 = 2                               '03:荷札商品名１
    Private Const COLNO_ShohinNM2 = 3                               '04:荷札商品名２
    Private Const COLNO_ShohinNM3 = 4                               '05:荷札商品名３
    Private Const COLNO_IRISUU = 5                                  '06:入数
    Private Const COLNO_KOSUU = 6                                   '07:個数
    Private Const COLNO_SURYOU = 7                                  '08:数量
    Private Const COLNO_TANNI = 8                                   '09:単位
    Private Const COLNO_KONPOU = 9                                  '10:梱包

    Private Const COLNO160_NO = 0                                      '01:No.
    Private Const COLNO160_ITEMCD = 1                                  '02:商品CD
    Private Const COLNO160_ITEMNM = 2                                  '03:商品名
    Private Const COLNO160_NISUGATA = 3                                '04:荷姿・形状
    Private Const COLNO160_REI = 4                                     '05:冷
    Private Const COLNO160_ZEIKBN = 5                                  '06:税
    Private Const COLNO160_IRISUU = 6                                  '07:入数
    Private Const COLNO160_KOSUU = 7                                   '08:個数
    Private Const COLNO160_SURYOU = 8                                  '09:数量
    Private Const COLNO160_TANNI = 9                                   '10:単位
    Private Const COLNO160_URITANKA = 10                               '11:売上単価
    Private Const COLNO160_URIKINGAKU = 11                             '12:売上金額
    Private Const COLNO160_MEISAIBIKOU = 12                            '13:明細備考
    Private Const COLNO160_KONPOU = 13                                 '14:梱包
    Private Const COLNO160_REICD = 14                                  '15:冷凍区分
    Private Const COLNO160_ZEIKBNCD = 15                               '16:課税区分
    Private Const COLNO160_KONPOUCD = 16                               '17:梱包区分
    Private Const COLNO160_TAX_RATE = 17                               '18:税率
    Private Const COLNO160_TAX_EXCLUSION = 18                          '19:税抜額
    Private Const COLNO160_TAX_TAXABLE = 19                            '20:課税対象額
    Private Const COLNO160_TAX_AMOUNT = 20                             '21:消費税額


    'フォームからの戻り値用　True:印刷された False:戻るボタンで戻った
    Public ReadOnly Property Printed() As String
        Get
            Return _Printed
        End Get
    End Property
    Public Shared ReadOnly Property printKbn() As String
        Get
            Return _printKbn
        End Get
    End Property

    '-------------------------------------------------------------------------------
    'デフォルトコンストラクタ（隠蔽）
    '-------------------------------------------------------------------------------
    Private Sub New()
        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

    End Sub

    '-------------------------------------------------------------------------------
    'コンストラクタ　メニューから呼ばれる
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByRef prmSelectID As String, prmForm As frmH01F60_Chumon, ByRef prmSelectMode As Integer)
        Call Me.New()


        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _comLogc = New CommonLogic(_db, _msgHd)                             ' 共通処理用
        _SelectID = prmSelectID
        _shoriId = prmSelectID
        _userId = frmC01F10_Login.loginValue.TantoCD                        'ユーザＩＤ

        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                   'フォームタイトル表示
        _SelectMode = prmSelectMode                                         '処理状態

        _frmH01F60 = prmForm

        '画面の初期表示
        Me.lblDenpyoNo.Text = _frmH01F60.lblDenpyoNo.Text       '伝票番号
        Me.lblShukkaDt.Text = _frmH01F60.dtpShukkaDt.Text       '出荷日
        Me.lblShukkaDay.Text = _frmH01F60.lblShukkaDay.Text     '出荷日（曜日）
        Me.lblChakuDt.Text = _frmH01F60.dtpChakuDt.Text         '着日
        Me.lblChakuDay.Text = _frmH01F60.lblChakuDay.Text       '着日（曜日）
        Me.lblShukkaCd.Text = _frmH01F60.lblShukkaCd.Text       '出荷先
        Me.lblShukkaNm.Text = _frmH01F60.txtShukkaNm.Text       '出荷先名
        Me.lblUnsoubin.Text = _frmH01F60.lblUnsoubin.Text       '運送便

        Me.lblNihudaNum.Text = _frmH01F60.lblNihudaNum.Text     '荷札印刷枚数
        Dim intListCount As Integer = _frmH01F60.dgvIchiran.RowCount
        Me.lblListCount.Text = intListCount
        Dim intSheetsCount As Integer = Math.Ceiling((intListCount + 1) / 6)
        Me.lblOkuriNum.Text = intSheetsCount                    '送り状印刷枚数
        Me.lblNohinNum.Text = intSheetsCount                    '納品書印刷枚数
        Me.lblSeikyuNum.Text = intSheetsCount                   '請求書印刷枚数
        Me.lblLesNum.Text = _frmH01F60.lblResupuriNum.Text      'レスプリ印刷枚数
        Me.txtHassouNum.Text = _frmH01F60.lblHassouNum.Text     '総個数


        '印字有無
        getTorihikisaki()

        '新規モードの場合「荷札」列にチェックを入れる
        Select Case prmSelectMode
            Case CommonConst.MODE_ADDNEW
                For index As Integer = 0 To Me.dgvIchiran.RowCount - 1
                    Me.dgvIchiran.Rows(index).Cells(COLNO_Nifuda).Value = True
                Next

        End Select

    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        Try
            Dim piRtn As Integer

            '確認メッセージを表示する
            piRtn = _msgHd.dspMSG("confirmInsert")  '登録します。よろしいですか？
            If piRtn = vbNo Then
                Exit Sub
            End If

            Dim strSql As String = ""
            Dim reccnt As Integer = 0
            Dim ds As DataSet
            '排他チェック処理
            Select Case _SelectMode     '（1:登録、2:変更、3:取消、4:照会)
                Case CommonConst.MODE_ADDNEW   '登録
                    '新規登録の場合は処理不要。

                Case CommonConst.MODE_EditStatus, CommonConst.MODE_CancelStatus   '変更,取消
                    Try
                        strSql = "SELECT  "
                        strSql = strSql & "    c.更新日,c.更新者 "
                        strSql = strSql & " FROM T10_CYMNHD c "
                        strSql = strSql & " Where c.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and c.注文伝番 = '" & _frmH01F60.lblDenpyoNo.Text & "'"
                        ds = _db.selectDB(strSql, RS, reccnt)
                        If reccnt = 0 Then
                            Exit Sub
                        End If
                        If _frmH01F60.UpdateTime <> DateTime.Parse(ds.Tables(RS).Rows(0)("更新日")) Then
                            '登録終了メッセージ
                            Dim strMessage As String = ""
                            strMessage = "更新者：" & ds.Tables(RS).Rows(0)("更新者") & " 更新日時：" & ds.Tables(RS).Rows(0)("更新日").ToString
                            _msgHd.dspMSG("Exclusion", strMessage)
                            _Printed = True
                            Me.Close()
                            Exit Sub
                        End If
                    Catch
                    End Try
                    '変更・取消の場合のみ処理を行う
            End Select

            Select Case _SelectMode     '（1:登録、2:変更、3:取消、4:照会)
                Case CommonConst.MODE_ADDNEW   '登録
                    DataAddNew()
                Case CommonConst.MODE_EditStatus   '変更
                    DataUpdate()
                Case CommonConst.MODE_CancelStatus   '取消
                    DataCancel()
                Case CommonConst.MODE_InquiryStatus   '照会
                    '照会はここに来ません・・・
            End Select
            'ログ出力
            '操作履歴ログ作成
            _comLogc.Insert_L01_ProcLog(frmC01F10_Login.loginValue.BumonCD, DBNull.Value, _SelectID, CommonConst.PROGRAM_REPORT, CommonConst.STATUS_NORMAL,
                                        lblDenpyoNo.Text, _frmH01F60.SyoriName, DBNull.Value, DBNull.Value, DBNull.Value,
                                        DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _userId)

            '帳票印刷処理
            '送り状
            If rdoOkuriOn.Checked Then
                'SQL編集
                strSql = ""
                reccnt = 0
                ds = Nothing
                strSql = makeOkurijouSql()
                ds = _db.selectDB(strSql, RS, reccnt)

                _printKbn = CommonConst.REPORT_DIRECT
                '_printKbn = CommonConst.REPORT_PREVIEW          '##デバッグ用##

                '送り状
                'Dim rSect As RepSectionIF = New H01R20_Okurijou
                'rSect.setData(ds, _db)
                'rSect.runReport()
            End If

            '納品書
            If rdoNohinOn.Checked Then
                'SQL編集
                strSql = ""
                reccnt = 0
                ds = Nothing
                strSql = makeNouhinshoSql()
                ds = _db.selectDB(strSql, RS, reccnt)

                _printKbn = CommonConst.REPORT_DIRECT
                '_printKbn = CommonConst.REPORT_PREVIEW          '##デバッグ用##
                _seikyuKbn = CommonConst.REPORT_NOHINSHO        '納品書
                '納品書
                Dim rSect As RepSectionIF = New H01R30_Nouhinsho
                rSect.setData(ds, _db)
                rSect.runReport()
            End If

            '請求書
            If rdoSeikyuOn.Checked Then
                'SQL編集
                strSql = ""
                reccnt = 0
                ds = Nothing
                strSql = makeNouhinshoSql()
                ds = _db.selectDB(strSql, RS, reccnt)

                _printKbn = CommonConst.REPORT_DIRECT
                '_printKbn = CommonConst.REPORT_PREVIEW          '##デバッグ用##
                _seikyuKbn = CommonConst.REPORT_SEIKYUSHO       '請求書

                '納品書と同一
                Dim rSect As RepSectionIF = New H01R30_Nouhinsho
                rSect.setData(ds, _db)
                rSect.runReport()
            End If

            '登録終了メッセージ
            _msgHd.dspMSG("UpdateInfo", "伝票番号：" & lblDenpyoNo.Text)
            '印刷（登録）されましたフラグを立てる
            _Printed = True
            Me.Close()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    'データ新規登録処理
    Private Sub DataAddNew()
        Try
            'トランザクション開始
            If Not _db.isTransactionOpen Then
                _db.beginTran()
            End If

            'データ追加
            Call DataInsert()

            'トランザクション終了
            _db.commitTran()

        Catch ex As Exception
            Throw ex
        Finally
            If _db.isTransactionOpen Then
                'ロールバック
                _db.rollbackTran()
            End If
        End Try

    End Sub

    'データ更新処理
    Private Sub DataUpdate()

        Try
            'トランザクション開始
            If Not _db.isTransactionOpen Then
                _db.beginTran()
            End If

            'データ削除
            DataDelete()

            'データ追加
            DataInsert()

            'トランザクション終了
            _db.commitTran()

        Catch ex As Exception
            Throw ex
        Finally
            If _db.isTransactionOpen Then
                'ロールバック
                _db.rollbackTran()
            End If
        End Try

    End Sub

    'データ追加
    Private Sub DataInsert()

        '注文基本（T10_CYMNHD）の登録
        Dim Sql As String = ""
        'レコード追加
        Sql = ""
        Sql = Sql & N & "INSERT INTO T10_CYMNHD ( "
        Sql = Sql & N & "    会社コード "
        Sql = Sql & N & "  , 注文伝番 "
        Sql = Sql & N & "  , 出荷先分類 "
        Sql = Sql & N & "  , 注文入力日 "
        Sql = Sql & N & "  , 出荷日 "
        Sql = Sql & N & "  , 着日 "
        Sql = Sql & N & "  , 売上区分 "
        Sql = Sql & N & "  , 出荷先コード "
        Sql = Sql & N & "  , 出荷先名 "
        Sql = Sql & N & "  , 郵便番号 "
        Sql = Sql & N & "  , 住所１ "
        Sql = Sql & N & "  , 住所２ "
        Sql = Sql & N & "  , 住所３ "
        Sql = Sql & N & "  , 電話番号 "
        Sql = Sql & N & "  , 電話番号検索用 "
        Sql = Sql & N & "  , ＦＡＸ番号 "
        Sql = Sql & N & "  , 担当者名 "
        Sql = Sql & N & "  , 依頼主等 "
        Sql = Sql & N & "  , 時間指定 "
        Sql = Sql & N & "  , 運送便コード "
        Sql = Sql & N & "  , 社外備考 "
        Sql = Sql & N & "  , 社内備考 "
        Sql = Sql & N & "  , 請求先コード "
        Sql = Sql & N & "  , 請求先名 "
        Sql = Sql & N & "  , 出荷先Ｇコード "
        Sql = Sql & N & "  , 出荷先Ｇ名 "
        Sql = Sql & N & "  , 送り状印刷有無 "
        Sql = Sql & N & "  , 荷札印刷有無 "
        Sql = Sql & N & "  , 納品伝票印刷有無 "
        Sql = Sql & N & "  , 請求伝票印刷有無 "
        Sql = Sql & N & "  , レスプリ印刷有無 "
        Sql = Sql & N & "  , 総個数 "
        Sql = Sql & N & "  , 売上金額計 "
        Sql = Sql & N & "  , 税抜額計 "
        Sql = Sql & N & "  , 課税対象額計 "
        Sql = Sql & N & "  , 消費税計 "
        Sql = Sql & N & "  , 税込額計 "
        Sql = Sql & N & "  , 税率 "
        Sql = Sql & N & "  , 税計算区分 "
        Sql = Sql & N & "  , 取消区分 "
        Sql = Sql & N & "  , 更新者 "
        Sql = Sql & N & "  , 更新日 "
        Sql = Sql & N & ") VALUES ( "
        Sql = Sql & N & "    '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "       '会社コード
        Sql = Sql & N & "  , '" & Me.lblDenpyoNo.Text & "' "                 '注文伝番
        Sql = Sql & N & "  , '" & _frmH01F60.lblNyuuryokuMode.Tag & "' "     '出荷先分類
        Sql = Sql & N & "  , current_date "     '注文入力日
        Sql = Sql & N & "  , '" & _frmH01F60.dtpShukkaDt.Text & "' "         '出荷日
        Sql = Sql & N & "  , '" & _frmH01F60.dtpChakuDt.Text & "' "          '着日
        Sql = Sql & N & "  , '" & _frmH01F60.lblNyuuryokuMode.Text & "' "    '売上区分
        Sql = Sql & N & "  , '" & _frmH01F60.lblShukkaCd.Text & "' "         '出荷先コード
        Sql = Sql & N & "  , '" & _frmH01F60.txtShukkaNm.Text & "' "         '出荷先名
        Sql = Sql & N & "  , '" & _frmH01F60.txtPostalCd1.Text & _frmH01F60.txtPostalCd2.Text & "' "         '郵便番号
        Sql = Sql & N & "  , '" & _frmH01F60.txtAddress1.Text & "' "         '住所１
        Sql = Sql & N & "  , '" & _frmH01F60.txtAddress2.Text & "' "         '住所２
        Sql = Sql & N & "  , '" & _frmH01F60.txtAddress3.Text & "' "         '住所３
        Sql = Sql & N & "  , '" & _frmH01F60.txtTelNo.Text & "' "            '電話番号
        Dim reg As New Regex("[^\d]")
        Dim strDes As String = reg.Replace(_frmH01F60.txtTelNo.Text, "")
        Sql = Sql & N & "  , '" & strDes & "' "                   '電話番号検索用
        Sql = Sql & N & "  , '" & _frmH01F60.txtFaxNo.Text & "' "            'ＦＡＸ番号
        Sql = Sql & N & "  , '" & _frmH01F60.txtTantousha.Text & "' "        '担当者名
        Sql = Sql & N & "  , '" & _frmH01F60.txtIrainusi.Text & "' "         '依頼主等
        Sql = Sql & N & "  , '" & _frmH01F60.txtJikansitei.Text & "' "       '時間指定
        Sql = Sql & N & "  , '" & _frmH01F60.lblUnsoubin.Tag & "' "          '運送便コード
        Sql = Sql & N & "  , '" & _frmH01F60.txtShagaiBikou.Text & "' "      '社外備考
        Sql = Sql & N & "  , '" & _frmH01F60.txtShanaiBikou.Text & "' "      '社内備考
        Sql = Sql & N & "  , '" & _frmH01F60.lblSeikyuCd.Text & "' "         '請求先コード
        Sql = Sql & N & "  , '" & _frmH01F60.txtSeikyuNm.Text & "' "         '請求先名
        Sql = Sql & N & "  , '" & _frmH01F60.lblShukkaGrpCd.Text & "' "      '出荷先Ｇコード
        Sql = Sql & N & "  , '" & _frmH01F60.txtShukkaGrpNm.Text & "' "      '出荷先Ｇ名
        If rdoOkuriOn.Checked Then
            Sql = Sql & N & "  , 1 "      '送り状印刷有無
        Else
            Sql = Sql & N & "  , 0 "      '送り状印刷有無
        End If
        If rdoNifudaOn.Checked Then
            Sql = Sql & N & "  , 1 "      '荷札印刷有無
        Else
            Sql = Sql & N & "  , 0 "      '荷札印刷有無
        End If
        If rdoNohinOn.Checked Then
            Sql = Sql & N & "  , 1 "      '納品伝票印刷有無
        Else
            Sql = Sql & N & "  , 0 "      '納品伝票印刷有無
        End If
        If rdoSeikyuOn.Checked Then
            Sql = Sql & N & "  , 1 "      '請求伝票印刷有無
        Else
            Sql = Sql & N & "  , 0 "      '請求伝票印刷有無
        End If
        If rdoLesOn.Checked Then
            Sql = Sql & N & "  , 1 "      'レスプリ印刷有無
        Else
            Sql = Sql & N & "  , 0 "      'レスプリ印刷有無
        End If
        Sql = Sql & N & "  , " & Decimal.Parse(_frmH01F60.lblHassouNum.Text).ToString       '総個数
        Sql = Sql & N & "  , " & Decimal.Parse(_frmH01F60.lblTotal.Text).ToString           '売上金額計
        Sql = Sql & N & "  , " & Decimal.Parse(_frmH01F60.lblZeinukiSum.Text).ToString      '税抜額計
        Sql = Sql & N & "  , " & Decimal.Parse(_frmH01F60.lblKazeiSum.Text).ToString        '課税対象額計
        Sql = Sql & N & "  , " & Decimal.Parse(_frmH01F60.lblTaxSum.Text).ToString          '消費税計
        Sql = Sql & N & "  , " & Decimal.Parse(_frmH01F60.lblMoneySum.Text).ToString        '税込額計
        Sql = Sql & N & "  , " & Decimal.Parse(_frmH01F60.dgvIchiran.Rows(0).Cells(COLNO160_TAX_RATE).Value).ToString        '税率
        Sql = Sql & N & "  , '" & _frmH01F60.ZeiSanshutsu & "' "                            '税計算区分
        Sql = Sql & N & "  , '0' "                                                          '取消区分
        Sql = Sql & N & "  , '" & frmC01F10_Login.loginValue.TantoCD & "' "                 '更新者
        Sql = Sql & N & "  , current_timestamp "                                            '更新日
        Sql = Sql & N & ") "
        _db.executeDB(Sql)


        '注文明細（T11_CYMNDT）
        For index As Integer = 0 To _frmH01F60.dgvIchiran.RowCount - 1

            Sql = ""
            Sql = Sql & N & "INSERT INTO T11_CYMNDT ( "
            Sql = Sql & N & "    会社コード "
            Sql = Sql & N & "  , 注文伝番 "
            Sql = Sql & N & "  , 行番 "
            Sql = Sql & N & "  , 商品コード "
            Sql = Sql & N & "  , 商品名 "
            Sql = Sql & N & "  , 荷姿形状 "
            Sql = Sql & N & "  , 課税区分 "
            Sql = Sql & N & "  , 入数 "
            Sql = Sql & N & "  , 個数 "
            Sql = Sql & N & "  , 数量 "
            Sql = Sql & N & "  , 単位 "
            Sql = Sql & N & "  , 注文単価 "
            Sql = Sql & N & "  , 注文金額 "
            Sql = Sql & N & "  , 梱包区分 "
            Sql = Sql & N & "  , 冷凍区分 "
            Sql = Sql & N & "  , 明細備考 "
            Sql = Sql & N & "  , 税抜額 "
            Sql = Sql & N & "  , 課税対象額 "
            Sql = Sql & N & "  , 消費税 "
            Sql = Sql & N & "  , 税込額 "

            Sql = Sql & N & "  , 更新者 "
            Sql = Sql & N & "  , 更新日 "
            Sql = Sql & N & ") VALUES ( "
            Sql = Sql & N & "    '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "       '会社コード
            Sql = Sql & N & "  , '" & Me.lblDenpyoNo.Text & "' "                 '注文伝番
            Sql = Sql & N & "  , " & _frmH01F60.dgvIchiran.Rows(index).Cells(COLNO160_NO).Value         '行番
            Sql = Sql & N & "  , '" & _frmH01F60.dgvIchiran.Rows(index).Cells(COLNO160_ITEMCD).Value & "' "     '商品コード
            Sql = Sql & N & "  , '" & _frmH01F60.dgvIchiran.Rows(index).Cells(COLNO160_ITEMNM).Value & "' "     '商品名
            Sql = Sql & N & "  , '" & _frmH01F60.dgvIchiran.Rows(index).Cells(COLNO160_NISUGATA).Value & "' "   '荷姿形状
            Sql = Sql & N & "  , '" & _frmH01F60.dgvIchiran.Rows(index).Cells(COLNO160_ZEIKBNCD).Value & "' "   '課税区分
            Sql = Sql & N & "  , " & Decimal.Parse(_frmH01F60.dgvIchiran.Rows(index).Cells(COLNO160_IRISUU).Value).ToString             '入数
            Sql = Sql & N & "  , " & Decimal.Parse(_frmH01F60.dgvIchiran.Rows(index).Cells(COLNO160_KOSUU).Value).ToString              '個数
            Sql = Sql & N & "  , " & Decimal.Parse(_frmH01F60.dgvIchiran.Rows(index).Cells(COLNO160_SURYOU).Value).ToString             '数量
            Sql = Sql & N & "  , '" & _frmH01F60.dgvIchiran.Rows(index).Cells(COLNO160_TANNI).Value & "' "      '単位
            Sql = Sql & N & "  , " & Decimal.Parse(_frmH01F60.dgvIchiran.Rows(index).Cells(COLNO160_URITANKA).Value).ToString           '注文単価
            Sql = Sql & N & "  , " & Decimal.Parse(_frmH01F60.dgvIchiran.Rows(index).Cells(COLNO160_URIKINGAKU).Value).ToString         '注文金額
            Sql = Sql & N & "  , '" & _frmH01F60.dgvIchiran.Rows(index).Cells(COLNO160_KONPOUCD).Value & "' "       '梱包区分
            Sql = Sql & N & "  , '" & _frmH01F60.dgvIchiran.Rows(index).Cells(COLNO160_REICD).Value & "' "          '冷凍区分
            Sql = Sql & N & "  , '" & _frmH01F60.dgvIchiran.Rows(index).Cells(COLNO160_MEISAIBIKOU).Value & "' "    '明細備考
            Sql = Sql & N & "  , " & Decimal.Parse(_frmH01F60.dgvIchiran.Rows(index).Cells(COLNO160_TAX_EXCLUSION).Value).ToString         '税抜額
            Sql = Sql & N & "  , " & Decimal.Parse(_frmH01F60.dgvIchiran.Rows(index).Cells(COLNO160_TAX_TAXABLE).Value).ToString         '課税対象額
            Sql = Sql & N & "  , " & Decimal.Parse(_frmH01F60.dgvIchiran.Rows(index).Cells(COLNO160_TAX_AMOUNT).Value).ToString         '消費税
            Sql = Sql & N & "  , " & (Decimal.Parse(_frmH01F60.dgvIchiran.Rows(index).Cells(COLNO160_TAX_EXCLUSION).Value) + Decimal.Parse(_frmH01F60.dgvIchiran.Rows(index).Cells(COLNO160_TAX_AMOUNT).Value)).ToString         '税込額

            Sql = Sql & N & "  , '" & frmC01F10_Login.loginValue.TantoCD & "' "         '更新者
            Sql = Sql & N & "  , current_timestamp "                                    '更新日
            Sql = Sql & N & ") "
            _db.executeDB(Sql)

        Next

        Select Case _frmH01F60.lblNyuuryokuMode.Tag
            Case CommonConst.SKBUNRUI_ITAKU
                '委託基本（T15_ITAKHD）
                '出荷先分類が1:委託の場合、委託基本レコードを作成する
                Sql = ""
                'レコード追加
                Sql = ""
                Sql = Sql & N & "INSERT INTO T15_ITAKHD ( "
                Sql = Sql & N & "    会社コード "
                Sql = Sql & N & "  , 委託伝番 "
                Sql = Sql & N & "  , 出荷日 "
                Sql = Sql & N & "  , 着日 "
                Sql = Sql & N & "  , 出荷先コード "
                Sql = Sql & N & "  , 出荷先名 "
                Sql = Sql & N & "  , 請求先コード "
                Sql = Sql & N & "  , 請求先名 "
                Sql = Sql & N & "  , 売上登録回数 "
                Sql = Sql & N & "  , 売上変更回数 "
                Sql = Sql & N & "  , 売上取消回数 "
                Sql = Sql & N & "  , 取消区分 "
                Sql = Sql & N & "  , 更新者 "
                Sql = Sql & N & "  , 更新日 "
                Sql = Sql & N & ") VALUES ( "
                Sql = Sql & N & "    '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "       '会社コード
                Sql = Sql & N & "  , '" & Me.lblDenpyoNo.Text & "' "                 '注文伝番
                Sql = Sql & N & "  , '" & _frmH01F60.dtpShukkaDt.Text & "' "         '出荷日
                Sql = Sql & N & "  , '" & _frmH01F60.dtpChakuDt.Text & "' "          '着日
                Sql = Sql & N & "  , '" & _frmH01F60.lblShukkaCd.Text & "' "         '出荷先コード
                Sql = Sql & N & "  , '" & _frmH01F60.txtShukkaNm.Text & "' "         '出荷先名
                Sql = Sql & N & "  , '" & _frmH01F60.lblSeikyuCd.Text & "' "         '請求先コード
                Sql = Sql & N & "  , '" & _frmH01F60.txtSeikyuNm.Text & "' "         '請求先名
                Sql = Sql & N & "  , 0 "      '売上登録回数
                Sql = Sql & N & "  , 0 "      '売上変更回数
                Sql = Sql & N & "  , 0 "      '売上取消回数
                Sql = Sql & N & "  , '0' "              '取消区分
                Sql = Sql & N & "  , '" & frmC01F10_Login.loginValue.TantoCD & "' "         '更新者
                Sql = Sql & N & "  , current_timestamp "                                    '更新日
                Sql = Sql & N & ") "
                _db.executeDB(Sql)

                '委託明細（T16_ITAKDT）
                '出荷先分類が1:委託の場合、委託明細（T16_ITAKDT）レコードを作成する
                For ii As Integer = 0 To _frmH01F60.dgvIchiran.RowCount - 1

                    Sql = ""
                    Sql = Sql & N & "INSERT INTO T16_ITAKDT ( "
                    Sql = Sql & N & "    会社コード "
                    Sql = Sql & N & "  , 委託伝番 "
                    Sql = Sql & N & "  , 行番 "
                    Sql = Sql & N & "  , 商品コード "
                    Sql = Sql & N & "  , 商品名 "
                    Sql = Sql & N & "  , 荷姿形状 "
                    Sql = Sql & N & "  , 課税区分 "
                    Sql = Sql & N & "  , 入数 "
                    Sql = Sql & N & "  , 個数 "
                    Sql = Sql & N & "  , 委託数量 "
                    Sql = Sql & N & "  , 単位 "
                    Sql = Sql & N & "  , 仮単価 "
                    Sql = Sql & N & "  , 売上数量計 "
                    Sql = Sql & N & "  , 目切数量計 "
                    Sql = Sql & N & "  , 委託残数 "
                    Sql = Sql & N & "  , 委託残金額 "
                    Sql = Sql & N & "  , 更新者 "
                    Sql = Sql & N & "  , 更新日 "
                    Sql = Sql & N & ") VALUES ( "
                    Sql = Sql & N & "    '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "       '会社コード
                    Sql = Sql & N & "  , '" & Me.lblDenpyoNo.Text & "' "                 '注文伝番
                    Sql = Sql & N & "  , " & _frmH01F60.dgvIchiran.Rows(ii).Cells(COLNO160_NO).Value         '行番
                    Sql = Sql & N & "  , '" & _frmH01F60.dgvIchiran.Rows(ii).Cells(COLNO160_ITEMCD).Value & "' "     '商品コード
                    Sql = Sql & N & "  , '" & _frmH01F60.dgvIchiran.Rows(ii).Cells(COLNO160_ITEMNM).Value & "' "     '商品名
                    Sql = Sql & N & "  , '" & _frmH01F60.dgvIchiran.Rows(ii).Cells(COLNO160_NISUGATA).Value & "' "   '荷姿形状
                    Sql = Sql & N & "  , '" & _frmH01F60.dgvIchiran.Rows(ii).Cells(COLNO160_ZEIKBNCD).Value & "' "   '課税区分
                    Sql = Sql & N & "  , " & Decimal.Parse(_frmH01F60.dgvIchiran.Rows(ii).Cells(COLNO160_IRISUU).Value).ToString             '入数
                    Sql = Sql & N & "  , " & Decimal.Parse(_frmH01F60.dgvIchiran.Rows(ii).Cells(COLNO160_KOSUU).Value).ToString              '個数
                    Sql = Sql & N & "  , " & Decimal.Parse(_frmH01F60.dgvIchiran.Rows(ii).Cells(COLNO160_SURYOU).Value).ToString             '委託数量
                    Sql = Sql & N & "  , '" & _frmH01F60.dgvIchiran.Rows(ii).Cells(COLNO160_TANNI).Value & "' "      '単位
                    Sql = Sql & N & "  , " & Decimal.Parse(_frmH01F60.dgvIchiran.Rows(ii).Cells(COLNO160_URITANKA).Value).ToString           '仮単価
                    Sql = Sql & N & "  , 0 "      '売上数量計
                    Sql = Sql & N & "  , 0 "      '目切数量計
                    Sql = Sql & N & "  , " & Decimal.Parse(_frmH01F60.dgvIchiran.Rows(ii).Cells(COLNO160_SURYOU).Value).ToString             '委託残数
                    Sql = Sql & N & "  , " & Decimal.Parse(_frmH01F60.dgvIchiran.Rows(ii).Cells(COLNO160_URIKINGAKU).Value).ToString         '委託残金額
                    Sql = Sql & N & "  , '" & frmC01F10_Login.loginValue.TantoCD & "' "         '更新者
                    Sql = Sql & N & "  , current_timestamp "                                    '更新日
                    Sql = Sql & N & ") "
                    _db.executeDB(Sql)
                Next
            Case CommonConst.SKBUNRUI_URIAGE    '売上
                'レコード追加
                Sql = ""
                Sql = Sql & N & "INSERT INTO T20_URIGHD ( "
                Sql = Sql & N & "    会社コード "
                Sql = Sql & N & "  , 売上伝番 "
                Sql = Sql & N & "  , 売上伝番枝番 "
                Sql = Sql & N & "  , 出荷先分類 "
                Sql = Sql & N & "  , 売上区分 "
                Sql = Sql & N & "  , 売上入力日 "
                Sql = Sql & N & "  , 出荷日 "
                Sql = Sql & N & "  , 着日 "
                Sql = Sql & N & "  , 売上日 "
                Sql = Sql & N & "  , 出荷先コード "
                Sql = Sql & N & "  , 出荷先名 "
                Sql = Sql & N & "  , 電話番号 "
                Sql = Sql & N & "  , 電話番号検索用 "
                Sql = Sql & N & "  , 請求先コード "
                Sql = Sql & N & "  , 請求先名 "
                Sql = Sql & N & "  , コメント "
                Sql = Sql & N & "  , 売上金額計 "
                Sql = Sql & N & "  , 税抜額計 "
                Sql = Sql & N & "  , 課税対象額計 "
                Sql = Sql & N & "  , 消費税計 "
                Sql = Sql & N & "  , 税込額計 "
                Sql = Sql & N & "  , 税率 "
                Sql = Sql & N & "  , 税計算区分 "
                Sql = Sql & N & "  , 取消区分 "
                Sql = Sql & N & "  , 更新者 "
                Sql = Sql & N & "  , 更新日 "
                Sql = Sql & N & ") VALUES ( "
                Sql = Sql & N & "    '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "       '会社コード
                Sql = Sql & N & "  , '" & Me.lblDenpyoNo.Text & "' "                 '売上伝番
                Sql = Sql & N & "  , 1 "                                             '売上伝番枝番
                Sql = Sql & N & "  , '" & _frmH01F60.lblNyuuryokuMode.Tag & "' "     '出荷先分類
                Sql = Sql & N & "  , '" & _frmH01F60.lblNyuuryokuMode.Text & "' "    '売上区分
                Sql = Sql & N & "  , current_date "     '売上入力日
                Sql = Sql & N & "  , '" & _frmH01F60.dtpShukkaDt.Text & "' "         '出荷日
                Sql = Sql & N & "  , '" & _frmH01F60.dtpChakuDt.Text & "' "          '着日
                Sql = Sql & N & "  , '" & _frmH01F60.dtpShukkaDt.Text & "' "         '売上日
                Sql = Sql & N & "  , '" & _frmH01F60.lblShukkaCd.Text & "' "         '出荷先コード
                Sql = Sql & N & "  , '" & _frmH01F60.txtShukkaNm.Text & "' "         '出荷先名
                Sql = Sql & N & "  , '" & _frmH01F60.txtTelNo.Text & "' "            '電話番号
                Sql = Sql & N & "  , '" & strDes & "' "                              '電話番号検索用
                Sql = Sql & N & "  , '" & _frmH01F60.lblSeikyuCd.Text & "' "         '請求先コード
                Sql = Sql & N & "  , '" & _frmH01F60.txtSeikyuNm.Text & "' "         '請求先名
                Sql = Sql & N & "  , Null "                                          'コメント
                Sql = Sql & N & "  , " & Decimal.Parse(_frmH01F60.lblTotal.Text).ToString           '売上金額計
                Sql = Sql & N & "  , " & Decimal.Parse(_frmH01F60.lblZeinukiSum.Text).ToString      '税抜額計
                Sql = Sql & N & "  , " & Decimal.Parse(_frmH01F60.lblKazeiSum.Text).ToString        '課税対象額計
                Sql = Sql & N & "  , " & Decimal.Parse(_frmH01F60.lblTaxSum.Text).ToString          '消費税計
                Sql = Sql & N & "  , " & Decimal.Parse(_frmH01F60.lblMoneySum.Text).ToString        '税込額計
                Sql = Sql & N & "  , " & Decimal.Parse(_frmH01F60.dgvIchiran.Rows(0).Cells(COLNO160_TAX_RATE).Value).ToString        '税率
                Sql = Sql & N & "  , '" & _frmH01F60.ZeiSanshutsu & "' "                            '税計算区分
                Sql = Sql & N & "  , '0' "                                                          '取消区分
                Sql = Sql & N & "  , '" & frmC01F10_Login.loginValue.TantoCD & "' "                 '更新者
                Sql = Sql & N & "  , current_timestamp "                                            '更新日
                Sql = Sql & N & ") "
                _db.executeDB(Sql)

                '売上明細（T21_URIGDT）
                For jj As Integer = 0 To _frmH01F60.dgvIchiran.RowCount - 1

                    Sql = ""
                    Sql = Sql & N & "INSERT INTO T21_URIGDT ( "
                    Sql = Sql & N & "    会社コード "
                    Sql = Sql & N & "  , 売上伝番 "
                    Sql = Sql & N & "  , 売上伝番枝番 "
                    Sql = Sql & N & "  , 行番 "
                    Sql = Sql & N & "  , 注文行番 "
                    Sql = Sql & N & "  , 商品コード "
                    Sql = Sql & N & "  , 商品名 "
                    Sql = Sql & N & "  , 荷姿形状 "
                    Sql = Sql & N & "  , 課税区分 "
                    Sql = Sql & N & "  , 入数 "
                    Sql = Sql & N & "  , 個数 "
                    Sql = Sql & N & "  , 単位 "
                    Sql = Sql & N & "  , 売上数量 "
                    Sql = Sql & N & "  , 目切数量 "
                    Sql = Sql & N & "  , 売上単価 "
                    Sql = Sql & N & "  , 売上金額 "
                    Sql = Sql & N & "  , 売上明細備考 "
                    Sql = Sql & N & "  , 税抜額 "
                    Sql = Sql & N & "  , 課税対象額 "
                    Sql = Sql & N & "  , 消費税 "
                    Sql = Sql & N & "  , 税込額 "
                    Sql = Sql & N & "  , 入金有無 "
                    Sql = Sql & N & "  , 入金伝番 "

                    Sql = Sql & N & "  , 更新者 "
                    Sql = Sql & N & "  , 更新日 "
                    Sql = Sql & N & ") VALUES ( "
                    Sql = Sql & N & "    '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "       '会社コード
                    Sql = Sql & N & "  , '" & Me.lblDenpyoNo.Text & "' "                 '売上伝番
                    Sql = Sql & N & "  , 1 "                                             '売上伝番枝番
                    Sql = Sql & N & "  , " & _frmH01F60.dgvIchiran.Rows(jj).Cells(COLNO160_NO).Value         '行番
                    Sql = Sql & N & "  , " & _frmH01F60.dgvIchiran.Rows(jj).Cells(COLNO160_NO).Value         '注文行番
                    Sql = Sql & N & "  , '" & _frmH01F60.dgvIchiran.Rows(jj).Cells(COLNO160_ITEMCD).Value & "' "     '商品コード
                    Sql = Sql & N & "  , '" & _frmH01F60.dgvIchiran.Rows(jj).Cells(COLNO160_ITEMNM).Value & "' "     '商品名
                    Sql = Sql & N & "  , '" & _frmH01F60.dgvIchiran.Rows(jj).Cells(COLNO160_NISUGATA).Value & "' "   '荷姿形状
                    Sql = Sql & N & "  , '" & _frmH01F60.dgvIchiran.Rows(jj).Cells(COLNO160_ZEIKBNCD).Value & "' "   '課税区分
                    Sql = Sql & N & "  , " & Decimal.Parse(_frmH01F60.dgvIchiran.Rows(jj).Cells(COLNO160_IRISUU).Value).ToString             '入数
                    Sql = Sql & N & "  , " & Decimal.Parse(_frmH01F60.dgvIchiran.Rows(jj).Cells(COLNO160_KOSUU).Value).ToString              '個数
                    Sql = Sql & N & "  , '" & _frmH01F60.dgvIchiran.Rows(jj).Cells(COLNO160_TANNI).Value & "' "      '単位
                    Sql = Sql & N & "  , 0 "                                             '売上数量
                    Sql = Sql & N & "  , 0 "                                             '目切数量
                    Sql = Sql & N & "  , " & Decimal.Parse(_frmH01F60.dgvIchiran.Rows(jj).Cells(COLNO160_URITANKA).Value).ToString           '売上単価
                    Sql = Sql & N & "  , " & Decimal.Parse(_frmH01F60.dgvIchiran.Rows(jj).Cells(COLNO160_URIKINGAKU).Value).ToString         '売上金額
                    Sql = Sql & N & "  , Null "                                             '売上明細備考
                    Sql = Sql & N & "  , " & Decimal.Parse(_frmH01F60.dgvIchiran.Rows(jj).Cells(COLNO160_TAX_EXCLUSION).Value).ToString         '税抜額
                    Sql = Sql & N & "  , " & Decimal.Parse(_frmH01F60.dgvIchiran.Rows(jj).Cells(COLNO160_TAX_TAXABLE).Value).ToString         '課税対象額
                    Sql = Sql & N & "  , " & Decimal.Parse(_frmH01F60.dgvIchiran.Rows(jj).Cells(COLNO160_TAX_AMOUNT).Value).ToString         '消費税
                    Sql = Sql & N & "  , " & (Decimal.Parse(_frmH01F60.dgvIchiran.Rows(jj).Cells(COLNO160_TAX_EXCLUSION).Value) + Decimal.Parse(_frmH01F60.dgvIchiran.Rows(jj).Cells(COLNO160_TAX_AMOUNT).Value)).ToString         '税込額
                    Sql = Sql & N & "  , '0' "                                             '入金有無
                    Sql = Sql & N & "  , Null "                                             '入金伝番

                    Sql = Sql & N & "  , '" & frmC01F10_Login.loginValue.TantoCD & "' "         '更新者
                    Sql = Sql & N & "  , current_timestamp "                                    '更新日
                    Sql = Sql & N & ") "
                    _db.executeDB(Sql)

                Next

        End Select

    End Sub

    'データ削除
    Private Sub DataDelete()

        '注文基本（T10_CYMNHD）
        Dim Sql As String = ""
        Sql = ""
        Sql = Sql & N & "Delete From T10_CYMNHD  "
        Sql = Sql & N & "  Where 会社コード =  '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "       '会社コード
        Sql = Sql & N & "   and 注文伝番 = '" & Me.lblDenpyoNo.Text & "' "                 '注文伝番
        _db.executeDB(Sql)


        '注文明細（T11_CYMNDT）
        Sql = ""
        Sql = Sql & N & "Delete From T11_CYMNDT  "
        Sql = Sql & N & "  Where 会社コード =  '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "       '会社コード
        Sql = Sql & N & "   and 注文伝番 = '" & Me.lblDenpyoNo.Text & "' "                 '注文伝番
        _db.executeDB(Sql)

        '委託基本（T15_ITAKHD）
        Sql = ""
        Sql = Sql & N & "Delete From T15_ITAKHD  "
        Sql = Sql & N & "  Where 会社コード =  '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "       '会社コード
        Sql = Sql & N & "   and 委託伝番 = '" & Me.lblDenpyoNo.Text & "' "                 '注文伝番
        _db.executeDB(Sql)

        '委託明細（T16_ITAKDT）
        Sql = ""
        Sql = Sql & N & "Delete From T16_ITAKDT  "
        Sql = Sql & N & "  Where 会社コード =  '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "       '会社コード
        Sql = Sql & N & "   and 委託伝番 = '" & Me.lblDenpyoNo.Text & "' "                 '注文伝番
        _db.executeDB(Sql)

        '売上基本（T20_URIGHD）
        Sql = ""
        Sql = Sql & N & "Delete From T20_URIGHD  "
        Sql = Sql & N & "  Where 会社コード =  '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "       '会社コード
        Sql = Sql & N & "   and 売上伝番 = '" & Me.lblDenpyoNo.Text & "' "                 '注文伝番
        _db.executeDB(Sql)

        '売上明細（T21_URIGDT）
        Sql = ""
        Sql = Sql & N & "Delete From T21_URIGDT  "
        Sql = Sql & N & "  Where 会社コード =  '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "       '会社コード
        Sql = Sql & N & "   and 売上伝番 = '" & Me.lblDenpyoNo.Text & "' "                 '注文伝番
        _db.executeDB(Sql)

    End Sub

    'データ取消処理
    Private Sub DataCancel()
        Try
            'トランザクション開始
            If Not _db.isTransactionOpen Then
                _db.beginTran()
            End If

            '注文基本（T10_CYMNHD）
            Dim Sql As String = ""
            Sql = ""
            Sql = Sql & N & "UPDATE T10_CYMNHD  "
            Sql = Sql & N & " SET 取消区分 = '1'  "
            Sql = Sql & N & "  , 更新者 = '" & frmC01F10_Login.loginValue.TantoCD & "' "         '更新者
            Sql = Sql & N & "  , 更新日 = current_timestamp "                                    '更新日
            Sql = Sql & N & "  Where 会社コード =  '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "       '会社コード
            Sql = Sql & N & "   and 注文伝番 = '" & Me.lblDenpyoNo.Text & "' "                 '注文伝番
            _db.executeDB(Sql)

            '委託基本（T15_ITAKHD）
            Sql = ""
            Sql = Sql & N & "UPDATE T15_ITAKHD  "
            Sql = Sql & N & " SET 取消区分 = '1'  "
            Sql = Sql & N & "  , 更新者 = '" & frmC01F10_Login.loginValue.TantoCD & "' "         '更新者
            Sql = Sql & N & "  , 更新日 = current_timestamp "                                    '更新日
            Sql = Sql & N & "  Where 会社コード =  '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "       '会社コード
            Sql = Sql & N & "   and 委託伝番 = '" & Me.lblDenpyoNo.Text & "' "                 '注文伝番
            _db.executeDB(Sql)

            '売上基本（T20_URIGHD）
            Sql = ""
            Sql = Sql & N & "UPDATE T20_URIGHD  "
            Sql = Sql & N & " SET 取消区分 = '1'  "
            Sql = Sql & N & "  , 更新者 = '" & frmC01F10_Login.loginValue.TantoCD & "' "         '更新者
            Sql = Sql & N & "  , 更新日 = current_timestamp "                                    '更新日
            Sql = Sql & N & "  Where 会社コード =  '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "       '会社コード
            Sql = Sql & N & "   and 売上伝番 = '" & Me.lblDenpyoNo.Text & "' "                 '注文伝番
            _db.executeDB(Sql)

            'トランザクション終了
            _db.commitTran()

        Catch ex As Exception
            Throw ex
        Finally
            If _db.isTransactionOpen Then
                'ロールバック
                _db.rollbackTran()
            End If
        End Try

    End Sub

    '取引先の初期表示
    Private Sub getTorihikisaki()


        Dim strSql As String = ""

        Try
            strSql = "SELECT "
            strSql = strSql & "  c.送り状印刷有無, c.荷札印刷有無,c.納品伝票印刷有無, c.請求伝票印刷有無, c.レスプリ印刷有無 "
            strSql = strSql & " FROM m10_customer c "
            strSql = strSql & " Where c.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and c.取引先コード = '" & Me.lblShukkaCd.Text & "'"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(strSql, RS, reccnt)

            '印字有無
            If _db.rmNullInt(ds.Tables(RS).Rows(0)("荷札印刷有無")) = 1 Then
                Me.rdoNifudaOn.Checked = True
                Me.rdoNifudaOff.Checked = False
            Else
                Me.rdoNifudaOn.Checked = False
                Me.rdoNifudaOff.Checked = True
            End If
            If _db.rmNullInt(ds.Tables(RS).Rows(0)("送り状印刷有無")) = 1 Then
                Me.rdoOkuriOn.Checked = True
                Me.rdoOkuriOff.Checked = False
            Else
                Me.rdoOkuriOn.Checked = False
                Me.rdoOkuriOff.Checked = True
            End If
            If _db.rmNullInt(ds.Tables(RS).Rows(0)("納品伝票印刷有無")) = 1 Then
                Me.rdoNohinOn.Checked = True
                Me.rdoNohinOff.Checked = False
            Else
                Me.rdoNohinOn.Checked = False
                Me.rdoNohinOff.Checked = True
            End If
            If _db.rmNullInt(ds.Tables(RS).Rows(0)("請求伝票印刷有無")) = 1 Then
                Me.rdoSeikyuOn.Checked = True
                Me.rdoSeikyuOff.Checked = False
            Else
                Me.rdoSeikyuOn.Checked = False
                Me.rdoSeikyuOff.Checked = True
            End If
            If _db.rmNullInt(ds.Tables(RS).Rows(0)("レスプリ印刷有無")) = 1 Then
                Me.rdoLesOn.Checked = True
                Me.rdoLesOff.Checked = False
            Else
                Me.rdoLesOn.Checked = False
                Me.rdoLesOff.Checked = True
            End If

            '明細行
            Me.dgvIchiran.Rows.Clear()
            For index As Integer = 0 To _frmH01F60.dgvIchiran.RowCount - 1
                Me.dgvIchiran.Rows.Add()
                Me.dgvIchiran.Rows(index).Cells(COLNO_NO).Value = _frmH01F60.dgvIchiran.Rows(index).Cells(0).Value      'No.
                Me.dgvIchiran.Rows(index).Cells(COLNO_ShohinNM1).Value = _frmH01F60.dgvIchiran.Rows(index).Cells(2).Value      '荷札商品名１
                Me.dgvIchiran.Rows(index).Cells(COLNO_ShohinNM2).Value = _frmH01F60.dgvIchiran.Rows(index).Cells(3).Value      '荷札商品名２
                Me.dgvIchiran.Rows(index).Cells(COLNO_ShohinNM3).Value = _frmH01F60.dgvIchiran.Rows(index).Cells(1).Value      '荷札商品名３
                Me.dgvIchiran.Rows(index).Cells(COLNO_IRISUU).Value = _frmH01F60.dgvIchiran.Rows(index).Cells(6).Value      '入数
                Me.dgvIchiran.Rows(index).Cells(COLNO_KOSUU).Value = _frmH01F60.dgvIchiran.Rows(index).Cells(7).Value      '個数
                Me.dgvIchiran.Rows(index).Cells(COLNO_SURYOU).Value = _frmH01F60.dgvIchiran.Rows(index).Cells(8).Value      '数量
                Me.dgvIchiran.Rows(index).Cells(COLNO_TANNI).Value = _frmH01F60.dgvIchiran.Rows(index).Cells(9).Value      '単位
                Me.dgvIchiran.Rows(index).Cells(COLNO_KONPOU).Value = _frmH01F60.dgvIchiran.Rows(index).Cells(13).Value      '梱包
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　送り状出力データ取得ＳＱＬ編集
    '-------------------------------------------------------------------------------
    Private Function makeOkurijouSql() As String
        makeOkurijouSql = ""

        Dim sSql As String = ""
        sSql = sSql & "SELECT "
        sSql = sSql & "      TRUNC(((T11_CYMNDT.行番 - 1) / 6) + 1) AS ""ページ"" "
        sSql = sSql & "     ,MAX(T10_CYMNHD.注文伝番) AS ""注文伝番"" "
        sSql = sSql & "     ,TO_CHAR(MAX(T10_CYMNHD.出荷日), 'yyyy年mm月dd日') AS ""出荷日"" "
        sSql = sSql & "     ,TO_CHAR(MAX(T10_CYMNHD.着日), 'yyyy年mm月dd日') AS ""着日"" "
        sSql = sSql & "     ,MAX(T10_CYMNHD.郵便番号) AS ""郵便番号"" "
        sSql = sSql & "     ,MAX(T10_CYMNHD.住所１) AS ""住所１"" "
        sSql = sSql & "     ,MAX(T10_CYMNHD.住所２) AS ""住所２"" "
        sSql = sSql & "     ,MAX(T10_CYMNHD.住所３) AS ""住所３"" "
        sSql = sSql & "     ,MAX(T10_CYMNHD.出荷先コード) AS ""出荷先コード"" "
        sSql = sSql & "     ,MAX(T10_CYMNHD.出荷先名) AS ""出荷先名"" "
        sSql = sSql & "     ,MAX(T10_CYMNHD.担当者名) AS ""担当者名"" "
        sSql = sSql & "     ,MAX(T10_CYMNHD.依頼主等) AS ""依頼主等"" "
        sSql = sSql & "     ,MAX(T10_CYMNHD.時間指定) AS ""時間指定"" "
        sSql = sSql & "     ,MAX(M90_HANYO.文字２) AS ""運送便"" "
        sSql = sSql & "     ,MAX(T10_CYMNHD.社外備考) AS ""社外備考"" "
        sSql = sSql & "     ,MAX(T10_CYMNHD.社内備考) AS ""社内備考"" "
        sSql = sSql & "     ,SUM(T11_CYMNDT.個数) AS ""個数"" "
        sSql = sSql & "     ,MAX(M01_COMPANY.会社名) AS ""会社名"" "
        sSql = sSql & "     ,MAX(M01_COMPANY.郵便番号) AS ""会社郵便番号"" "
        sSql = sSql & "     ,MAX(M01_COMPANY.住所１) AS ""会社住所１"" "
        sSql = sSql & "     ,MAX(M01_COMPANY.住所２) AS ""会社住所２"" "
        sSql = sSql & "     ,MAX(M01_COMPANY.住所３) AS ""会社住所３"" "
        sSql = sSql & "     ,MAX(M01_COMPANY.電話番号) AS ""会社電話番号"" "
        sSql = sSql & "     ,MAX(M01_COMPANY.ＦＡＸ番号) AS ""会社ＦＡＸ番号"" "
        sSql = sSql & "     ,MAX(M01_COMPANY.代表者役職) AS ""会社代表者役職"" "
        sSql = sSql & "     ,MAX(M01_COMPANY.代表者名) AS ""会社代表者名"" "
        sSql = sSql & "FROM T10_CYMNHD "
        sSql = sSql & "INNER JOIN T11_CYMNDT ON T11_CYMNDT.会社コード = T10_CYMNHD.会社コード "
        sSql = sSql & "  AND T11_CYMNDT.注文伝番 = T10_CYMNHD.注文伝番 "
        sSql = sSql & "INNER JOIN M01_COMPANY ON M01_COMPANY.会社コード = T10_CYMNHD.会社コード "
        sSql = sSql & " LEFT JOIN M90_HANYO ON M90_HANYO.会社コード = T10_CYMNHD.会社コード "
        sSql = sSql & "  AND M90_HANYO.固定キー = '" & CommonConst.HANYO_KOTEI_UNSOUBIN & "' "
        sSql = sSql & "  AND M90_HANYO.可変キー = T10_CYMNHD.運送便コード "
        sSql = sSql & "WHERE T10_CYMNHD.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' "
        sSql = sSql & "  AND T10_CYMNHD.注文伝番 = '" & lblDenpyoNo.Text & "' "
        sSql = sSql & "GROUP BY TRUNC(((T11_CYMNDT.行番 - 1) / 6) + 1) "
        sSql = sSql & "ORDER BY TRUNC(((T11_CYMNDT.行番 - 1) / 6) + 1) "

        Return sSql

    End Function

    '-------------------------------------------------------------------------------
    '　納品書出力データ取得ＳＱＬ編集
    '-------------------------------------------------------------------------------
    Private Function makeNouhinshoSql() As String
        makeNouhinshoSql = ""

        Dim sSql As String = ""
        sSql = sSql & "SELECT "
        sSql = sSql & "      TRUNC(((SUB.行番 - 1) / 6) + 1) AS ""ページ""  "
        sSql = sSql & "     ,MAX(SUB.注文伝番) AS ""注文伝番"" "
        sSql = sSql & "     ,MAX(SUB.出荷日) AS ""出荷日"" "
        sSql = sSql & "     ,MAX(SUB.着日) AS ""着日"" "
        sSql = sSql & "     ,MAX(SUB.郵便番号) AS ""郵便番号"" "
        sSql = sSql & "     ,MAX(SUB.住所１) AS ""住所１"" "
        sSql = sSql & "     ,MAX(SUB.住所２) AS ""住所２"" "
        sSql = sSql & "     ,MAX(SUB.住所３) AS ""住所３"" "
        sSql = sSql & "     ,MAX(SUB.出荷先コード) AS ""出荷先コード"" "
        sSql = sSql & "     ,MAX(SUB.出荷先名) AS ""出荷先名"" "
        sSql = sSql & "     ,MAX(SUB.担当者名) AS ""担当者名"" "
        sSql = sSql & "     ,MAX(SUB.依頼主等) AS ""依頼主等"" "
        sSql = sSql & "     ,MAX(SUB.時間指定) AS ""時間指定"" "
        sSql = sSql & "     ,MAX(SUB.運送便) AS ""運送便"" "
        sSql = sSql & "     ,MAX(SUB.社外備考) AS ""社外備考"" "
        sSql = sSql & "     ,MAX(SUB.社内備考) AS ""社内備考"" "
        sSql = sSql & "     ,SUM(SUB.入数) AS ""入数"" "
        sSql = sSql & "     ,SUM(SUB.個数) AS ""個数"" "
        sSql = sSql & "     ,MAX(SUB.数量) AS ""数量"" "
        sSql = sSql & "     ,SUM(SUB.注文単価) AS ""注文単価"" "
        sSql = sSql & "     ,SUM(SUB.注文金額) AS ""注文金額"" "
        sSql = sSql & "     ,MAX(SUB.会社名) AS ""会社名"" "
        sSql = sSql & "     ,MAX(SUB.会社郵便番号) AS ""会社郵便番号"" "
        sSql = sSql & "     ,MAX(SUB.会社住所１) AS ""会社住所１"" "
        sSql = sSql & "     ,MAX(SUB.会社住所２) AS ""会社住所２"" "
        sSql = sSql & "     ,MAX(SUB.会社住所３) AS ""会社住所３"" "
        sSql = sSql & "     ,MAX(SUB.会社電話番号) AS ""会社電話番号"" "
        sSql = sSql & "     ,MAX(SUB.会社ＦＡＸ番号) AS ""会社ＦＡＸ番号"" "
        sSql = sSql & "     ,MAX(SUB.会社代表者役職) AS ""会社代表者役職"" "
        sSql = sSql & "     ,MAX(SUB.会社代表者名) AS ""会社代表者名"" "
        sSql = sSql & "FROM "
        sSql = sSql & "( "
        sSql = sSql & "    SELECT  "
        sSql = sSql & "         T11_CYMNDT.行番 AS ""行番"" "
        sSql = sSql & "        ,T10_CYMNHD.注文伝番 AS ""注文伝番""  "
        sSql = sSql & "        ,TO_CHAR(T10_CYMNHD.出荷日, 'yyyy年mm月dd日') AS ""出荷日""  "
        sSql = sSql & "        ,TO_CHAR(T10_CYMNHD.着日, 'yyyy年mm月dd日') AS ""着日""  "
        sSql = sSql & "        ,T10_CYMNHD.郵便番号 AS ""郵便番号""  "
        sSql = sSql & "        ,T10_CYMNHD.住所１ AS ""住所１""  "
        sSql = sSql & "        ,T10_CYMNHD.住所２ AS ""住所２""  "
        sSql = sSql & "        ,T10_CYMNHD.住所３ AS ""住所３""  "
        sSql = sSql & "        ,T10_CYMNHD.出荷先コード AS ""出荷先コード""  "
        sSql = sSql & "        ,T10_CYMNHD.出荷先名 AS ""出荷先名""  "
        sSql = sSql & "        ,T10_CYMNHD.担当者名 AS ""担当者名""  "
        sSql = sSql & "        ,T10_CYMNHD.依頼主等 AS ""依頼主等""  "
        sSql = sSql & "        ,T10_CYMNHD.時間指定 AS ""時間指定""  "
        sSql = sSql & "        ,M90_HANYO.文字２ AS ""運送便""  "
        sSql = sSql & "        ,T10_CYMNHD.社外備考 AS ""社外備考""  "
        sSql = sSql & "        ,T10_CYMNHD.社内備考 AS ""社内備考""  "
        sSql = sSql & "        ,T11_CYMNDT.入数 AS ""入数"" "
        sSql = sSql & "        ,T11_CYMNDT.個数 AS ""個数""  "
        sSql = sSql & "        ,T11_CYMNDT.数量 || ' 個' AS ""数量"" "
        sSql = sSql & "        ,T11_CYMNDT.注文単価 AS ""注文単価"" "
        sSql = sSql & "        ,T11_CYMNDT.注文金額 AS ""注文金額"" "
        sSql = sSql & "        ,M01_COMPANY.会社名 AS ""会社名""  "
        sSql = sSql & "        ,M01_COMPANY.郵便番号 AS ""会社郵便番号""  "
        sSql = sSql & "        ,M01_COMPANY.住所１ AS ""会社住所１""  "
        sSql = sSql & "        ,M01_COMPANY.住所２ AS ""会社住所２""  "
        sSql = sSql & "        ,M01_COMPANY.住所３ AS ""会社住所３""  "
        sSql = sSql & "        ,M01_COMPANY.電話番号 AS ""会社電話番号""  "
        sSql = sSql & "        ,M01_COMPANY.ＦＡＸ番号 AS ""会社ＦＡＸ番号""  "
        sSql = sSql & "        ,M01_COMPANY.代表者役職 AS ""会社代表者役職""  "
        sSql = sSql & "        ,M01_COMPANY.代表者名 AS ""会社代表者名""  "
        sSql = sSql & "    FROM T10_CYMNHD  "
        sSql = sSql & "    INNER JOIN T11_CYMNDT ON T11_CYMNDT.会社コード = T10_CYMNHD.会社コード  "
        sSql = sSql & "      AND T11_CYMNDT.注文伝番 = T10_CYMNHD.注文伝番  "
        sSql = sSql & "    INNER JOIN M01_COMPANY ON M01_COMPANY.会社コード = T10_CYMNHD.会社コード  "
        sSql = sSql & "     LEFT JOIN M90_HANYO ON M90_HANYO.会社コード = T10_CYMNHD.会社コード  "
        sSql = sSql & "      AND M90_HANYO.固定キー = '" & CommonConst.HANYO_KOTEI_UNSOUBIN & "'  "
        sSql = sSql & "      AND M90_HANYO.可変キー = T10_CYMNHD.運送便コード  "
        sSql = sSql & "    WHERE T10_CYMNHD.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'  "
        sSql = sSql & "      AND T10_CYMNHD.注文伝番 = '" & lblDenpyoNo.Text & "'  "
        sSql = sSql & "    UNION  "
        sSql = sSql & "    SELECT  "
        sSql = sSql & "         MAX(T11_CYMNDT.行番) + 1 AS ""行番"" "
        sSql = sSql & "        ,T10_CYMNHD.注文伝番 AS ""注文伝番""  "
        sSql = sSql & "        ,TO_CHAR(MAX(T10_CYMNHD.出荷日), 'yyyy年mm月dd日') AS ""出荷日""  "
        sSql = sSql & "        ,TO_CHAR(MAX(T10_CYMNHD.着日), 'yyyy年mm月dd日') AS ""着日""  "
        sSql = sSql & "        ,MAX(T10_CYMNHD.郵便番号) AS ""郵便番号""  "
        sSql = sSql & "        ,MAX(T10_CYMNHD.住所１) AS ""住所１""  "
        sSql = sSql & "        ,MAX(T10_CYMNHD.住所２) AS ""住所２""  "
        sSql = sSql & "        ,MAX(T10_CYMNHD.住所３) AS ""住所３""  "
        sSql = sSql & "        ,MAX(T10_CYMNHD.出荷先コード) AS ""出荷先コード""  "
        sSql = sSql & "        ,MAX(T10_CYMNHD.出荷先名) AS ""出荷先名""  "
        sSql = sSql & "        ,MAX(T10_CYMNHD.担当者名) AS ""担当者名""  "
        sSql = sSql & "        ,MAX(T10_CYMNHD.依頼主等) AS ""依頼主等""  "
        sSql = sSql & "        ,MAX(T10_CYMNHD.時間指定) AS ""時間指定""  "
        sSql = sSql & "        ,MAX(M90_HANYO.文字２) AS ""運送便""  "
        sSql = sSql & "        ,MAX(T10_CYMNHD.社外備考) AS ""社外備考""  "
        sSql = sSql & "        ,MAX(T10_CYMNHD.社内備考) AS ""社内備考""  "
        sSql = sSql & "        ,NULL AS ""入数"" "
        sSql = sSql & "        ,NULL AS ""個数""  "
        sSql = sSql & "        ,'消費税額' AS ""数量"" "
        sSql = sSql & "        ,NULL AS ""注文単価"" "
        sSql = sSql & "        ,TRUNC(SUM(T11_CYMNDT.注文金額 * 0.08)) AS ""注文金額"" "
        sSql = sSql & "        ,MAX(M01_COMPANY.会社名) AS ""会社名""  "
        sSql = sSql & "        ,MAX(M01_COMPANY.郵便番号) AS ""会社郵便番号""  "
        sSql = sSql & "        ,MAX(M01_COMPANY.住所１) AS ""会社住所１""  "
        sSql = sSql & "        ,MAX(M01_COMPANY.住所２) AS ""会社住所２""  "
        sSql = sSql & "        ,MAX(M01_COMPANY.住所３) AS ""会社住所３""  "
        sSql = sSql & "        ,MAX(M01_COMPANY.電話番号) AS ""会社電話番号""  "
        sSql = sSql & "        ,MAX(M01_COMPANY.ＦＡＸ番号) AS ""会社ＦＡＸ番号""  "
        sSql = sSql & "        ,MAX(M01_COMPANY.代表者役職) AS ""会社代表者役職""  "
        sSql = sSql & "        ,MAX(M01_COMPANY.代表者名) AS ""会社代表者名""  "
        sSql = sSql & "    FROM T10_CYMNHD  "
        sSql = sSql & "    INNER JOIN T11_CYMNDT ON T11_CYMNDT.会社コード = T10_CYMNHD.会社コード  "
        sSql = sSql & "      AND T11_CYMNDT.注文伝番 = T10_CYMNHD.注文伝番  "
        sSql = sSql & "    INNER JOIN M01_COMPANY ON M01_COMPANY.会社コード = T10_CYMNHD.会社コード  "
        sSql = sSql & "     LEFT JOIN M90_HANYO ON M90_HANYO.会社コード = T10_CYMNHD.会社コード  "
        sSql = sSql & "      AND M90_HANYO.固定キー = '" & CommonConst.HANYO_KOTEI_UNSOUBIN & "'  "
        sSql = sSql & "      AND M90_HANYO.可変キー = T10_CYMNHD.運送便コード  "
        sSql = sSql & "    WHERE T10_CYMNHD.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'  "
        sSql = sSql & "      AND T10_CYMNHD.注文伝番 = '" & lblDenpyoNo.Text & "'  "
        sSql = sSql & "    GROUP BY T10_CYMNHD.注文伝番 "
        sSql = sSql & ") SUB "
        sSql = sSql & "GROUP BY TRUNC(((SUB.行番 - 1) / 6) + 1)  "
        sSql = sSql & "ORDER BY TRUNC(((SUB.行番 - 1) / 6) + 1)  "

        Return sSql

    End Function

    '全選択クリック
    Private Sub btnAllSelect_Click(sender As Object, e As EventArgs) Handles btnAllSelect.Click

        For index As Integer = 0 To Me.dgvIchiran.RowCount - 1
            Me.dgvIchiran.Rows(index).Cells(COLNO_Nifuda).Value = True
        Next

    End Sub
    '全解除クリック
    Private Sub btnAllCancel_Click(sender As Object, e As EventArgs) Handles btnAllCancel.Click

        For index As Integer = 0 To Me.dgvIchiran.RowCount - 1
            Me.dgvIchiran.Rows(index).Cells(COLNO_Nifuda).Value = False
        Next

    End Sub
    '戻るボタンクリック
    Private Sub cmdBack_Click(sender As Object, e As EventArgs) Handles cmdBack.Click

        Me.Close()

    End Sub

    'テキストボックスのキープレスイベント
    Private Sub txtHassouNum_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtHassouNum.KeyPress
        UtilMDL.UtilClass.moveNextFocus(Me, e)

    End Sub
    '-------------------------------------------------------------------------------
    '　フォーカス取得イベント
    '-------------------------------------------------------------------------------
    Private Sub ctl_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtHassouNum.GotFocus

        'フォーカス取得時、入力パラメタのコントロールを全選択状態とする
        Call UtilClass.selAll(sender)

    End Sub

End Class