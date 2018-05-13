'===============================================================================
'
'　北日本電線株式会社
'　　（システム名）在庫計画システム
'　　（処理機能名）手配データ修正(詳細)
'    （フォームID）ZE210Q_ZaikoJisseki
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   高木        2010/12/09                 新規              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.FileDirectory

Public Class ZE210Q_ZaikoJisseki
    Inherits System.Windows.Forms.Form


    'PG制御文字
    Private Const RS As String = "RecSet"                       'データセットテーブル名
    Private Const N As String = ControlChars.NewLine            '改行文字
    Const COND_COL As Integer = 1 : Const COND_ROW As Integer = 3               '検索条件
    Const P_DT_COL As Integer = 14 : Const P_DT_ROW As Integer = 1              '作成日時
    Const YM1__COL As Integer = 3 : Const YM___ROW As Integer = 5
    Const YM2__COL As Integer = 5
    Const YM3__COL As Integer = 7
    Const YM4__COL As Integer = 9
    Const YM5__COL As Integer = 11
    Const YM6__COL As Integer = 13
    Const DATA_START_ROW As Integer = 7 : Const DATA_START_COL As Integer = 1   'データ開始行


    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As Form

    '-->2010.12/12 add by takagi 
    '-------------------------------------------------------------------------------
    '   オーバーライドプロパティで×ボタンだけを無効にする(ControlBoxはTrueのまま使用可能)
    '-------------------------------------------------------------------------------
    Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
        Get
            Const CS_NOCLOSE As Integer = &H200

            Dim tmpCreateParams As System.Windows.Forms.CreateParams = MyBase.CreateParams
            tmpCreateParams.ClassStyle = tmpCreateParams.ClassStyle Or CS_NOCLOSE

            Return tmpCreateParams
        End Get
    End Property
    '<--2010.12/12 add by takagi 

    '-------------------------------------------------------------------------------
    'コンストラクタ（Privateにして、外からは呼べないようにする）
    '-------------------------------------------------------------------------------
    Private Sub New()
        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

    End Sub

    '-------------------------------------------------------------------------------
    'コンストラクタ　メニューから呼ばれる
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByVal prmForm As Form)
        Call Me.New()

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _parentForm = prmForm                                               '親フォーム
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
    End Sub

    '-------------------------------------------------------------------------------
    '　処理開始イベント
    '-------------------------------------------------------------------------------
    Private Sub ZE210Q_ZaikoJisseki_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            '描画関係の設定
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           'サイズが変更されたときに、コントロールがコントロール自体を再描画するかどうかを示す値を設定
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '描画はバッファで実行され、完了後に、結果が画面に出力されるよう設定
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  'コントロールは、画面に直接ではなく、まずバッファに描画されます。これにより、ちらつきを抑えることができます。
            Me.SetStyle(ControlStyles.UserPaint, True)              'コントロールは、オペレーティング システムによってではなく、独自に描画されるよう設定
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   'コントロールはウィンドウ メッセージ WM_ERASEBKGND を無視するように設定

            Dim optionStr As String = ComBiz.getFormTitleOption(_db, _msgHd)
            If Not "".Equals(optionStr) Then Me.Text = Me.Text & " - " & optionStr 'タイトルオプション表示

            Dim d As DataSet = _db.selectDB("select SNENGETU,KNENGETU from T01KEIKANRI", RS)
            Dim syoriDate As String = _db.rmNullStr(d.Tables(RS).Rows(0)("SNENGETU"))
            txtNengetsuTo.Text = Format(DateAdd(DateInterval.Month, -1, CDate(syoriDate.Substring(0, 4) & "/" & syoriDate.Substring(4) & "/01")), "yyyy/MM")
            txtNengetsuFrom.Text = Format(DateAdd(DateInterval.Month, -5, CDate(txtNengetsuTo.Text & "/01")), "yyyy/MM")


        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub


    '------------------------------------------------------------------------------------------------------
    '　戻るボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnModoru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModoru.Click

        '自画面を終了し、メニュー画面に戻る。
        _parentForm.Show()
        _parentForm.Activate()

        Me.Close()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　ラジオ変更イベント
    '------------------------------------------------------------------------------------------------------
    Private Sub rdoHinmoku_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoHinmoku1.CheckedChanged, rdoHinmoku2.CheckedChanged, rdoHinmoku3.CheckedChanged
        Try
            txtSiyoCD.Enabled = rdoHinmoku1.Checked
            txtHinsyuCD.Enabled = rdoHinmoku1.Checked
            txtSensinsuu.Enabled = rdoHinmoku1.Checked
            txtSize.Enabled = rdoHinmoku1.Checked
            txtColor.Enabled = rdoHinmoku1.Checked
            txtSiyoCD.BackColor = IIf(txtSiyoCD.Enabled, StartUp.lCOLOR_WHITE, StartUp.lCOLOR_YELLOW)
            txtHinsyuCD.BackColor = IIf(txtHinsyuCD.Enabled, StartUp.lCOLOR_WHITE, StartUp.lCOLOR_YELLOW)
            txtSensinsuu.BackColor = IIf(txtSensinsuu.Enabled, StartUp.lCOLOR_WHITE, StartUp.lCOLOR_YELLOW)
            txtSize.BackColor = IIf(txtSize.Enabled, StartUp.lCOLOR_WHITE, StartUp.lCOLOR_YELLOW)
            txtColor.BackColor = IIf(txtColor.Enabled, StartUp.lCOLOR_WHITE, StartUp.lCOLOR_YELLOW)

            txtHinsyuFrom.Enabled = rdoHinmoku2.Checked
            txtHinsyuTo.Enabled = rdoHinmoku2.Checked
            txtHinsyuFrom.BackColor = IIf(txtHinsyuFrom.Enabled, StartUp.lCOLOR_WHITE, StartUp.lCOLOR_YELLOW)
            txtHinsyuTo.BackColor = IIf(txtHinsyuTo.Enabled, StartUp.lCOLOR_WHITE, StartUp.lCOLOR_YELLOW)

            txtHinmei.Enabled = rdoHinmoku3.Checked
            txtHinmei.BackColor = IIf(txtHinmei.Enabled, StartUp.lCOLOR_WHITE, StartUp.lCOLOR_YELLOW)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '　キー押下イベント
    '------------------------------------------------------------------------------------------------------
    Private Sub rdoBumon_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles rdoBumon1.KeyPress, rdoBumon2.KeyPress, rdoBumon3.KeyPress, rdoHinmoku1.KeyPress, rdoHinmoku2.KeyPress, rdoHinmoku3.KeyPress, txtSiyoCD.KeyPress, txtHinsyuCD.KeyPress, txtSensinsuu.KeyPress, txtSize.KeyPress, txtColor.KeyPress, txtHinsyuFrom.KeyPress, txtHinsyuTo.KeyPress, txtHinmei.KeyPress, txtNengetsuFrom.KeyPress, txtNengetsuTo.KeyPress
        Try
            UtilClass.moveNextFocus(Me, e)
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '　フォーカス取得イベント
    '------------------------------------------------------------------------------------------------------
    Private Sub txt_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSiyoCD.GotFocus, txtHinsyuCD.GotFocus, txtSensinsuu.GotFocus, txtSize.GotFocus, txtColor.GotFocus, txtHinsyuFrom.GotFocus, txtHinsyuTo.GotFocus, txtHinmei.GotFocus, txtNengetsuFrom.GotFocus, txtNengetsuTo.GotFocus
        Try
            UtilClass.selAll(sender)
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '　Excelボタン押下イベント
    '------------------------------------------------------------------------------------------------------
    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Try

            '入力チェック
            If "".Equals(txtNengetsuFrom.Text.Replace("/", "").Trim) Then
                Throw New UsrDefException("必須エラー", _msgHd.getMSG("requiredImput"), txtNengetsuFrom)
            End If
            If "".Equals(txtNengetsuTo.Text.Replace("/", "").Trim) Then
                Throw New UsrDefException("必須エラー", _msgHd.getMSG("requiredImput"), txtNengetsuTo)
            End If
            If txtNengetsuFrom.Text.CompareTo(txtNengetsuTo.Text) > 0 Then
                Throw New UsrDefException("大小エラー", _msgHd.getMSG("ErrHaniChk"), txtNengetsuFrom)
            End If
            Dim diffNum As Integer = DateDiff(DateInterval.Month, CDate(txtNengetsuFrom.Text & "/01"), CDate(txtNengetsuTo.Text & "/01"))
            If diffNum >= 6 Then
                Throw New UsrDefException("無効エラー", _msgHd.getMSG("ImputedInvalidDate"), txtNengetsuFrom)
            End If

            '月１～月６までのパラメタ取得
            Dim nengetsu1 As String = ""
            Dim nengetsu2 As String = ""
            Dim nengetsu3 As String = ""
            Dim nengetsu4 As String = ""
            Dim nengetsu5 As String = ""
            Dim nengetsu6 As String = ""
            Select Case diffNum
                Case 0
                    nengetsu1 = Format(DateAdd(DateInterval.Month, 0, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                Case 1
                    nengetsu1 = Format(DateAdd(DateInterval.Month, 0, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                    nengetsu2 = Format(DateAdd(DateInterval.Month, 1, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                Case 2
                    nengetsu1 = Format(DateAdd(DateInterval.Month, 0, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                    nengetsu2 = Format(DateAdd(DateInterval.Month, 1, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                    nengetsu3 = Format(DateAdd(DateInterval.Month, 2, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                Case 3
                    nengetsu1 = Format(DateAdd(DateInterval.Month, 0, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                    nengetsu2 = Format(DateAdd(DateInterval.Month, 1, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                    nengetsu3 = Format(DateAdd(DateInterval.Month, 2, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                    nengetsu4 = Format(DateAdd(DateInterval.Month, 3, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                Case 4
                    nengetsu1 = Format(DateAdd(DateInterval.Month, 0, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                    nengetsu2 = Format(DateAdd(DateInterval.Month, 1, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                    nengetsu3 = Format(DateAdd(DateInterval.Month, 2, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                    nengetsu4 = Format(DateAdd(DateInterval.Month, 3, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                    nengetsu5 = Format(DateAdd(DateInterval.Month, 4, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                Case 5
                    nengetsu1 = Format(DateAdd(DateInterval.Month, 0, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                    nengetsu2 = Format(DateAdd(DateInterval.Month, 1, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                    nengetsu3 = Format(DateAdd(DateInterval.Month, 2, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                    nengetsu4 = Format(DateAdd(DateInterval.Month, 3, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                    nengetsu5 = Format(DateAdd(DateInterval.Month, 4, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                    nengetsu6 = Format(DateAdd(DateInterval.Month, 5, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
            End Select

            'Excel表示用＆SQL指定用検索条件構築
            Dim bumonConditilnDsp As String = ""
            Dim hinmeiCdConditionDsp As String = "条件指定なし"
            Dim hinshuConditionDsp As String = "条件指定なし"
            Dim hinmeiConditionDsp As String = "条件指定なし"
            Dim hanbaiYmDsp As String = ""

            Dim bumonCondition As String = ""
            Select Case True
                Case rdoBumon1.Checked
                    bumonCondition = "(HINKBN2 like '01%' or HINKBN2 like '02%')"
                    bumonConditilnDsp = rdoBumon1.Text
                Case rdoBumon2.Checked
                    bumonCondition = "(HINKBN2 like '01%' )"
                    bumonConditilnDsp = rdoBumon2.Text
                Case rdoBumon3.Checked
                    bumonCondition = "(HINKBN2 like '02%' )"
                    bumonConditilnDsp = rdoBumon3.Text
                Case Else : bumonCondition = "1 = 1"
            End Select

            Dim sqlWhere As String = ""
            sqlWhere = sqlWhere & N & "AND " & bumonCondition & " "
            Select Case True
                Case rdoHinmoku1.Checked
                    If Not "".Equals(txtSiyoCD.Text.Trim) Then
                        sqlWhere = sqlWhere & N & " AND SHIYO     like '" & _db.rmSQ(txtSiyoCD.Text) & "%'"
                    End If
                    If Not "".Equals(txtHinsyuCD.Text) Then
                        sqlWhere = sqlWhere & N & " AND HINSHU    like '" & _db.rmSQ(txtHinsyuCD.Text) & "%'"
                    End If
                    If Not "".Equals(txtSensinsuu.Text) Then
                        sqlWhere = sqlWhere & N & " AND SENSHIN   like '" & _db.rmSQ(txtSensinsuu.Text) & "%'"
                    End If
                    If Not "".Equals(txtSize.Text) Then
                        sqlWhere = sqlWhere & N & " AND SAIZU     like '" & _db.rmSQ(txtSize.Text) & "%'"
                    End If
                    If Not "".Equals(txtColor.Text) Then
                        sqlWhere = sqlWhere & N & " AND IRO       like  '" & _db.rmSQ(txtColor.Text) & "%'"
                    End If
                    hinmeiCdConditionDsp = txtSiyoCD.Text.PadRight(2, "*") & "-" & _
                                           txtHinsyuCD.Text.PadRight(3, "*") & "-" & _
                                           txtSensinsuu.Text.PadRight(3, "*") & "-" & _
                                           txtSize.Text.PadRight(2, "*") & "-" & _
                                           txtColor.Text.PadRight(3, "*")
                Case rdoHinmoku2.Checked
                    If Not "".Equals(txtHinsyuFrom.Text) Then
                        sqlWhere = sqlWhere & N & " AND HINSHU    >=   '" & _db.rmSQ(txtHinsyuFrom.Text) & "'"
                    End If
                    If Not "".Equals(txtHinsyuTo.Text) Then
                        sqlWhere = sqlWhere & N & " AND HINSHU    <=   '" & _db.rmSQ(txtHinsyuTo.Text) & "'"
                    End If
                    hinshuConditionDsp = IIf("".Equals(txtHinsyuFrom.Text), "始め", txtHinsyuFrom.Text) & "～" & IIf("".Equals(txtHinsyuTo.Text), "最後", txtHinsyuTo.Text)
                Case rdoHinmoku3.Checked
                    If Not "".Equals(txtHinmei.Text.Replace(" ", "")) Then
                        sqlWhere = sqlWhere & N & " AND (HINSHUMEI || SAIZUMEI || IROMEI) like '%" & _db.rmSQ(txtHinmei.Text.Replace(" ", "")) & "%'"
                    End If
                    hinmeiConditionDsp = txtHinmei.Text
            End Select
            hanbaiYmDsp = IIf("".Equals(txtNengetsuFrom.Text.Replace("/", "").Trim), "始め", txtNengetsuFrom.Text) & "～" & IIf("".Equals(txtNengetsuTo.Text.Replace("/", "").Trim), "最後", txtNengetsuTo.Text)


            '実行確認（実行します。よろしいですか？）
            Dim opMsg As String = ""
            Dim c As Cursor = Me.Cursor
            Dim rtnCnt As Integer = 0
            Dim ds As DataSet = Nothing
            Me.Cursor = Cursors.WaitCursor
            Try
                Dim SQL As String = ""
                _db.executeDB("delete from ZE210Q_W10 where UPDNAME = '" & UtilClass.getComputerName() & "'")

                '月１分インサート
                SQL = ""
                SQL = SQL & N & "		INSERT INTO ZE210Q_W10 ( "
                SQL = SQL & N & "		  HINMEICD "
                SQL = SQL & N & "		 ,NENGETSU "
                SQL = SQL & N & "		 ,HINMEI "
                SQL = SQL & N & "		 ,SU1 ,RYO1  "
                SQL = SQL & N & "		 ,SU2 ,RYO2  "
                SQL = SQL & N & "		 ,SU3 ,RYO3  "
                SQL = SQL & N & "		 ,SU4 ,RYO4  "
                SQL = SQL & N & "		 ,SU5 ,RYO5  "
                SQL = SQL & N & "		 ,SU6 ,RYO6  "
                SQL = SQL & N & "		 ,UPDNAME "
                SQL = SQL & N & "		 ,UPDDATE "
                SQL = SQL & N & "		) "
                SQL = SQL & N & "		SELECT "
                SQL = SQL & N & "		(SHIYO || HINSHU || SENSHIN || SAIZU || IRO) HINMEICD "
                SQL = SQL & N & "		,'" & nengetsu1 & "'  "
                SQL = SQL & N & "       ,max(HINSHUMEI) || max(SAIZUMEI) || max(IROMEI) "
                SQL = SQL & N & "		,SUM( ( NVL(KAGI_ZEN_SU ,0) + NVL(FUNA_ZEN_SU ,0) + NVL(SAPO_ZEN_SU ,0) + NVL(HEN_ZEN_SU ,0) + NVL(KARI_ZEN_SU ,0) + NVL(YU_ZEN_SU ,0) + NVL(KAGI_TOU_NYUKO_SU ,0) + NVL(FUNA_TOU_NYUKO_SU ,0) + NVL(SAPO_TOU_NYUKO_SU ,0) ) - ( NVL(KAGI_TOU_SHUKO_SU ,0) + NVL(FUNA_TOU_SHUKO_SU ,0) + NVL(SAPO_TOU_SHUKO_SU ,0) + NVL(HEN_TOU_SHUKO_SU ,0) + NVL(KARI_TOU_SU ,0) + NVL(YU_TOU_SU ,0) ) ) SU1 "
                SQL = SQL & N & "		,SUM( ( NVL(KAGI_ZEN_DOU ,0) + NVL(FUNA_ZEN_DOU ,0) + NVL(SAPO_ZEN_DOU ,0) + NVL(HEN_ZEN_DOU ,0) + NVL(KARI_ZEN_DOU ,0) + NVL(YU_ZEN_DOU ,0) + NVL(KAGI_TOU_NYUKO_DOU ,0) + NVL(FUNA_TOU_NYUKO_DOU ,0) + NVL(SAPO_TOU_NYUKO_DOU ,0) ) - ( NVL(KAGI_TOU_DHUKO_DOU ,0) + NVL(FUNA_TOU_SHUKO_DOU ,0) + NVL(SAPO_TOU_SHUKO_DOU ,0) + NVL(HEN_TOU_SHUKO_DOU ,0) + NVL(KARI_TOU_DOU ,0) + NVL(YU_TOU_DOU ,0) ) ) RYO1 "
                SQL = SQL & N & "		,0 SU2 ,0 RYO2 ,0 SU3 ,0 RYO3 ,0 SU4 ,0 RYO4 ,0 SU5 ,0 RYO5 ,0 SU6 ,0 RYO6 "
                SQL = SQL & N & "		,'" & UtilClass.getComputerName() & "' "
                SQL = SQL & N & "		,SYSDATE "
                SQL = SQL & N & "		FROM T72ZAIKOS "
                SQL = SQL & N & "		WHERE NENTETSU = '" & nengetsu1 & "' "
                SQL = SQL & N & "       " + sqlWhere
                SQL = SQL & N & "		GROUP BY (SHIYO || HINSHU || SENSHIN || SAIZU || IRO) "
                _db.executeDB(SQL)

                '月２分インサート
                SQL = ""
                SQL = SQL & N & "		INSERT INTO ZE210Q_W10 ( "
                SQL = SQL & N & "		  HINMEICD "
                SQL = SQL & N & "		 ,NENGETSU "
                SQL = SQL & N & "		 ,HINMEI "
                SQL = SQL & N & "		 ,SU1 ,RYO1  "
                SQL = SQL & N & "		 ,SU2 ,RYO2  "
                SQL = SQL & N & "		 ,SU3 ,RYO3  "
                SQL = SQL & N & "		 ,SU4 ,RYO4  "
                SQL = SQL & N & "		 ,SU5 ,RYO5  "
                SQL = SQL & N & "		 ,SU6 ,RYO6  "
                SQL = SQL & N & "		 ,UPDNAME "
                SQL = SQL & N & "		 ,UPDDATE "
                SQL = SQL & N & "		) "
                SQL = SQL & N & "		SELECT "
                SQL = SQL & N & "		(SHIYO || HINSHU || SENSHIN || SAIZU || IRO) HINMEICD "
                SQL = SQL & N & "		,'" & nengetsu2 & "'  "
                SQL = SQL & N & "       ,max(HINSHUMEI) || max(SAIZUMEI) || max(IROMEI) "
                SQL = SQL & N & "		,0 SU1 ,0 RYO1 "
                SQL = SQL & N & "		,SUM( ( NVL(KAGI_ZEN_SU ,0) + NVL(FUNA_ZEN_SU ,0) + NVL(SAPO_ZEN_SU ,0) + NVL(HEN_ZEN_SU ,0) + NVL(KARI_ZEN_SU ,0) + NVL(YU_ZEN_SU ,0) + NVL(KAGI_TOU_NYUKO_SU ,0) + NVL(FUNA_TOU_NYUKO_SU ,0) + NVL(SAPO_TOU_NYUKO_SU ,0) ) - ( NVL(KAGI_TOU_SHUKO_SU ,0) + NVL(FUNA_TOU_SHUKO_SU ,0) + NVL(SAPO_TOU_SHUKO_SU ,0) + NVL(HEN_TOU_SHUKO_SU ,0) + NVL(KARI_TOU_SU ,0) + NVL(YU_TOU_SU ,0) ) ) SU2 "
                SQL = SQL & N & "		,SUM( ( NVL(KAGI_ZEN_DOU ,0) + NVL(FUNA_ZEN_DOU ,0) + NVL(SAPO_ZEN_DOU ,0) + NVL(HEN_ZEN_DOU ,0) + NVL(KARI_ZEN_DOU ,0) + NVL(YU_ZEN_DOU ,0) + NVL(KAGI_TOU_NYUKO_DOU ,0) + NVL(FUNA_TOU_NYUKO_DOU ,0) + NVL(SAPO_TOU_NYUKO_DOU ,0) ) - ( NVL(KAGI_TOU_DHUKO_DOU ,0) + NVL(FUNA_TOU_SHUKO_DOU ,0) + NVL(SAPO_TOU_SHUKO_DOU ,0) + NVL(HEN_TOU_SHUKO_DOU ,0) + NVL(KARI_TOU_DOU ,0) + NVL(YU_TOU_DOU ,0) ) ) RYO2 "
                SQL = SQL & N & "		,0 SU3 ,0 RYO3 ,0 SU4 ,0 RYO4 ,0 SU5 ,0 RYO5 ,0 SU6 ,0 RYO6 "
                SQL = SQL & N & "		,'" & UtilClass.getComputerName() & "' "
                SQL = SQL & N & "		,SYSDATE "
                SQL = SQL & N & "		FROM T72ZAIKOS "
                SQL = SQL & N & "		WHERE NENTETSU = '" & nengetsu2 & "' "
                SQL = SQL & N & "       " + sqlWhere
                SQL = SQL & N & "		GROUP BY (SHIYO || HINSHU || SENSHIN || SAIZU || IRO) "
                If Not "".Equals(nengetsu2) Then _db.executeDB(SQL)

                '月３分インサート
                SQL = ""
                SQL = SQL & N & "		INSERT INTO ZE210Q_W10 ( "
                SQL = SQL & N & "		  HINMEICD "
                SQL = SQL & N & "		 ,NENGETSU "
                SQL = SQL & N & "		 ,HINMEI "
                SQL = SQL & N & "		 ,SU1 ,RYO1  "
                SQL = SQL & N & "		 ,SU2 ,RYO2  "
                SQL = SQL & N & "		 ,SU3 ,RYO3  "
                SQL = SQL & N & "		 ,SU4 ,RYO4  "
                SQL = SQL & N & "		 ,SU5 ,RYO5  "
                SQL = SQL & N & "		 ,SU6 ,RYO6  "
                SQL = SQL & N & "		 ,UPDNAME "
                SQL = SQL & N & "		 ,UPDDATE "
                SQL = SQL & N & "		) "
                SQL = SQL & N & "		SELECT "
                SQL = SQL & N & "		(SHIYO || HINSHU || SENSHIN || SAIZU || IRO) HINMEICD "
                SQL = SQL & N & "		,'" & nengetsu3 & "'  "
                SQL = SQL & N & "       ,max(HINSHUMEI) || max(SAIZUMEI) || max(IROMEI) "
                SQL = SQL & N & "		,0 SU1 ,0 RYO1 ,0 SU2 ,0 RYO2 "
                SQL = SQL & N & "		,SUM( ( NVL(KAGI_ZEN_SU ,0) + NVL(FUNA_ZEN_SU ,0) + NVL(SAPO_ZEN_SU ,0) + NVL(HEN_ZEN_SU ,0) + NVL(KARI_ZEN_SU ,0) + NVL(YU_ZEN_SU ,0) + NVL(KAGI_TOU_NYUKO_SU ,0) + NVL(FUNA_TOU_NYUKO_SU ,0) + NVL(SAPO_TOU_NYUKO_SU ,0) ) - ( NVL(KAGI_TOU_SHUKO_SU ,0) + NVL(FUNA_TOU_SHUKO_SU ,0) + NVL(SAPO_TOU_SHUKO_SU ,0) + NVL(HEN_TOU_SHUKO_SU ,0) + NVL(KARI_TOU_SU ,0) + NVL(YU_TOU_SU ,0) ) ) SU3 "
                SQL = SQL & N & "		,SUM( ( NVL(KAGI_ZEN_DOU ,0) + NVL(FUNA_ZEN_DOU ,0) + NVL(SAPO_ZEN_DOU ,0) + NVL(HEN_ZEN_DOU ,0) + NVL(KARI_ZEN_DOU ,0) + NVL(YU_ZEN_DOU ,0) + NVL(KAGI_TOU_NYUKO_DOU ,0) + NVL(FUNA_TOU_NYUKO_DOU ,0) + NVL(SAPO_TOU_NYUKO_DOU ,0) ) - ( NVL(KAGI_TOU_DHUKO_DOU ,0) + NVL(FUNA_TOU_SHUKO_DOU ,0) + NVL(SAPO_TOU_SHUKO_DOU ,0) + NVL(HEN_TOU_SHUKO_DOU ,0) + NVL(KARI_TOU_DOU ,0) + NVL(YU_TOU_DOU ,0) ) ) RYO3 "
                SQL = SQL & N & "		,0 SU4 ,0 RYO4 ,0 SU5 ,0 RYO5 ,0 SU6 ,0 RYO6 "
                SQL = SQL & N & "		,'" & UtilClass.getComputerName() & "' "
                SQL = SQL & N & "		,SYSDATE "
                SQL = SQL & N & "		FROM T72ZAIKOS "
                SQL = SQL & N & "		WHERE NENTETSU = '" & nengetsu3 & "' "
                SQL = SQL & N & "       " + sqlWhere
                SQL = SQL & N & "		GROUP BY (SHIYO || HINSHU || SENSHIN || SAIZU || IRO) "
                If Not "".Equals(nengetsu3) Then _db.executeDB(SQL)

                '月４分インサート
                SQL = ""
                SQL = SQL & N & "		INSERT INTO ZE210Q_W10 ( "
                SQL = SQL & N & "		  HINMEICD "
                SQL = SQL & N & "		 ,NENGETSU "
                SQL = SQL & N & "		 ,HINMEI "
                SQL = SQL & N & "		 ,SU1 ,RYO1  "
                SQL = SQL & N & "		 ,SU2 ,RYO2  "
                SQL = SQL & N & "		 ,SU3 ,RYO3  "
                SQL = SQL & N & "		 ,SU4 ,RYO4  "
                SQL = SQL & N & "		 ,SU5 ,RYO5  "
                SQL = SQL & N & "		 ,SU6 ,RYO6  "
                SQL = SQL & N & "		 ,UPDNAME "
                SQL = SQL & N & "		 ,UPDDATE "
                SQL = SQL & N & "		) "
                SQL = SQL & N & "		SELECT "
                SQL = SQL & N & "		(SHIYO || HINSHU || SENSHIN || SAIZU || IRO) HINMEICD "
                SQL = SQL & N & "		,'" & nengetsu4 & "'  "
                SQL = SQL & N & "       ,max(HINSHUMEI) || max(SAIZUMEI) || max(IROMEI) "
                SQL = SQL & N & "		,0 SU1 ,0 RYO1 ,0 SU2 ,0 RYO2 ,0 SU3 ,0 RYO3 "
                SQL = SQL & N & "		,SUM( ( NVL(KAGI_ZEN_SU ,0) + NVL(FUNA_ZEN_SU ,0) + NVL(SAPO_ZEN_SU ,0) + NVL(HEN_ZEN_SU ,0) + NVL(KARI_ZEN_SU ,0) + NVL(YU_ZEN_SU ,0) + NVL(KAGI_TOU_NYUKO_SU ,0) + NVL(FUNA_TOU_NYUKO_SU ,0) + NVL(SAPO_TOU_NYUKO_SU ,0) ) - ( NVL(KAGI_TOU_SHUKO_SU ,0) + NVL(FUNA_TOU_SHUKO_SU ,0) + NVL(SAPO_TOU_SHUKO_SU ,0) + NVL(HEN_TOU_SHUKO_SU ,0) + NVL(KARI_TOU_SU ,0) + NVL(YU_TOU_SU ,0) ) ) SU4 "
                SQL = SQL & N & "		,SUM( ( NVL(KAGI_ZEN_DOU ,0) + NVL(FUNA_ZEN_DOU ,0) + NVL(SAPO_ZEN_DOU ,0) + NVL(HEN_ZEN_DOU ,0) + NVL(KARI_ZEN_DOU ,0) + NVL(YU_ZEN_DOU ,0) + NVL(KAGI_TOU_NYUKO_DOU ,0) + NVL(FUNA_TOU_NYUKO_DOU ,0) + NVL(SAPO_TOU_NYUKO_DOU ,0) ) - ( NVL(KAGI_TOU_DHUKO_DOU ,0) + NVL(FUNA_TOU_SHUKO_DOU ,0) + NVL(SAPO_TOU_SHUKO_DOU ,0) + NVL(HEN_TOU_SHUKO_DOU ,0) + NVL(KARI_TOU_DOU ,0) + NVL(YU_TOU_DOU ,0) ) ) RYO4 "
                SQL = SQL & N & "		,0 SU5 ,0 RYO5 ,0 SU6 ,0 RYO6 "
                SQL = SQL & N & "		,'" & UtilClass.getComputerName() & "' "
                SQL = SQL & N & "		,SYSDATE "
                SQL = SQL & N & "		FROM T72ZAIKOS "
                SQL = SQL & N & "		WHERE NENTETSU = '" & nengetsu4 & "' "
                SQL = SQL & N & "       " + sqlWhere
                SQL = SQL & N & "		GROUP BY (SHIYO || HINSHU || SENSHIN || SAIZU || IRO) "
                If Not "".Equals(nengetsu4) Then _db.executeDB(SQL)

                '月５分インサート
                SQL = ""
                SQL = SQL & N & "		INSERT INTO ZE210Q_W10 ( "
                SQL = SQL & N & "		  HINMEICD "
                SQL = SQL & N & "		 ,NENGETSU "
                SQL = SQL & N & "		 ,HINMEI "
                SQL = SQL & N & "		 ,SU1 ,RYO1  "
                SQL = SQL & N & "		 ,SU2 ,RYO2  "
                SQL = SQL & N & "		 ,SU3 ,RYO3  "
                SQL = SQL & N & "		 ,SU4 ,RYO4  "
                SQL = SQL & N & "		 ,SU5 ,RYO5  "
                SQL = SQL & N & "		 ,SU6 ,RYO6  "
                SQL = SQL & N & "		 ,UPDNAME "
                SQL = SQL & N & "		 ,UPDDATE "
                SQL = SQL & N & "		) "
                SQL = SQL & N & "		SELECT "
                SQL = SQL & N & "		(SHIYO || HINSHU || SENSHIN || SAIZU || IRO) HINMEICD "
                SQL = SQL & N & "		,'" & nengetsu5 & "'  "
                SQL = SQL & N & "       ,max(HINSHUMEI) || max(SAIZUMEI) || max(IROMEI) "
                SQL = SQL & N & "		,0 SU1 ,0 RYO1 ,0 SU2 ,0 RYO2 ,0 SU3 ,0 RYO3 ,0 SU4 ,0 RYO4 "
                SQL = SQL & N & "		,SUM( ( NVL(KAGI_ZEN_SU ,0) + NVL(FUNA_ZEN_SU ,0) + NVL(SAPO_ZEN_SU ,0) + NVL(HEN_ZEN_SU ,0) + NVL(KARI_ZEN_SU ,0) + NVL(YU_ZEN_SU ,0) + NVL(KAGI_TOU_NYUKO_SU ,0) + NVL(FUNA_TOU_NYUKO_SU ,0) + NVL(SAPO_TOU_NYUKO_SU ,0) ) - ( NVL(KAGI_TOU_SHUKO_SU ,0) + NVL(FUNA_TOU_SHUKO_SU ,0) + NVL(SAPO_TOU_SHUKO_SU ,0) + NVL(HEN_TOU_SHUKO_SU ,0) + NVL(KARI_TOU_SU ,0) + NVL(YU_TOU_SU ,0) ) ) SU5 "
                SQL = SQL & N & "		,SUM( ( NVL(KAGI_ZEN_DOU ,0) + NVL(FUNA_ZEN_DOU ,0) + NVL(SAPO_ZEN_DOU ,0) + NVL(HEN_ZEN_DOU ,0) + NVL(KARI_ZEN_DOU ,0) + NVL(YU_ZEN_DOU ,0) + NVL(KAGI_TOU_NYUKO_DOU ,0) + NVL(FUNA_TOU_NYUKO_DOU ,0) + NVL(SAPO_TOU_NYUKO_DOU ,0) ) - ( NVL(KAGI_TOU_DHUKO_DOU ,0) + NVL(FUNA_TOU_SHUKO_DOU ,0) + NVL(SAPO_TOU_SHUKO_DOU ,0) + NVL(HEN_TOU_SHUKO_DOU ,0) + NVL(KARI_TOU_DOU ,0) + NVL(YU_TOU_DOU ,0) ) ) RYO5 "
                SQL = SQL & N & "		,0 SU6 ,0 RYO6 "
                SQL = SQL & N & "		,'" & UtilClass.getComputerName() & "' "
                SQL = SQL & N & "		,SYSDATE "
                SQL = SQL & N & "		FROM T72ZAIKOS "
                SQL = SQL & N & "		WHERE NENTETSU = '" & nengetsu5 & "' "
                SQL = SQL & N & "       " + sqlWhere
                SQL = SQL & N & "		GROUP BY (SHIYO || HINSHU || SENSHIN || SAIZU || IRO) "
                If Not "".Equals(nengetsu5) Then _db.executeDB(SQL)

                '月６分インサート
                SQL = ""
                SQL = SQL & N & "		INSERT INTO ZE210Q_W10 ( "
                SQL = SQL & N & "		  HINMEICD "
                SQL = SQL & N & "		 ,NENGETSU "
                SQL = SQL & N & "		 ,HINMEI "
                SQL = SQL & N & "		 ,SU1 ,RYO1  "
                SQL = SQL & N & "		 ,SU2 ,RYO2  "
                SQL = SQL & N & "		 ,SU3 ,RYO3  "
                SQL = SQL & N & "		 ,SU4 ,RYO4  "
                SQL = SQL & N & "		 ,SU5 ,RYO5  "
                SQL = SQL & N & "		 ,SU6 ,RYO6  "
                SQL = SQL & N & "		 ,UPDNAME "
                SQL = SQL & N & "		 ,UPDDATE "
                SQL = SQL & N & "		) "
                SQL = SQL & N & "		SELECT "
                SQL = SQL & N & "		(SHIYO || HINSHU || SENSHIN || SAIZU || IRO) HINMEICD "
                SQL = SQL & N & "		,'" & nengetsu6 & "'  "
                SQL = SQL & N & "       ,max(HINSHUMEI) || max(SAIZUMEI) || max(IROMEI) "
                SQL = SQL & N & "		,0 SU1 ,0 RYO1 ,0 SU2 ,0 RYO2 ,0 SU3 ,0 RYO3 ,0 SU4 ,0 RYO4 ,0 SU5 ,0 RYO5 "
                SQL = SQL & N & "		,SUM( ( NVL(KAGI_ZEN_SU ,0) + NVL(FUNA_ZEN_SU ,0) + NVL(SAPO_ZEN_SU ,0) + NVL(HEN_ZEN_SU ,0) + NVL(KARI_ZEN_SU ,0) + NVL(YU_ZEN_SU ,0) + NVL(KAGI_TOU_NYUKO_SU ,0) + NVL(FUNA_TOU_NYUKO_SU ,0) + NVL(SAPO_TOU_NYUKO_SU ,0) ) - ( NVL(KAGI_TOU_SHUKO_SU ,0) + NVL(FUNA_TOU_SHUKO_SU ,0) + NVL(SAPO_TOU_SHUKO_SU ,0) + NVL(HEN_TOU_SHUKO_SU ,0) + NVL(KARI_TOU_SU ,0) + NVL(YU_TOU_SU ,0) ) ) SU6 "
                SQL = SQL & N & "		,SUM( ( NVL(KAGI_ZEN_DOU ,0) + NVL(FUNA_ZEN_DOU ,0) + NVL(SAPO_ZEN_DOU ,0) + NVL(HEN_ZEN_DOU ,0) + NVL(KARI_ZEN_DOU ,0) + NVL(YU_ZEN_DOU ,0) + NVL(KAGI_TOU_NYUKO_DOU ,0) + NVL(FUNA_TOU_NYUKO_DOU ,0) + NVL(SAPO_TOU_NYUKO_DOU ,0) ) - ( NVL(KAGI_TOU_DHUKO_DOU ,0) + NVL(FUNA_TOU_SHUKO_DOU ,0) + NVL(SAPO_TOU_SHUKO_DOU ,0) + NVL(HEN_TOU_SHUKO_DOU ,0) + NVL(KARI_TOU_DOU ,0) + NVL(YU_TOU_DOU ,0) ) ) RYO6 "
                SQL = SQL & N & "		,'" & UtilClass.getComputerName() & "' "
                SQL = SQL & N & "		,SYSDATE "
                SQL = SQL & N & "		FROM T72ZAIKOS "
                SQL = SQL & N & "		WHERE NENTETSU = '" & nengetsu6 & "' "
                SQL = SQL & N & "       " + sqlWhere
                SQL = SQL & N & "		GROUP BY (SHIYO || HINSHU || SENSHIN || SAIZU || IRO) "
                If Not "".Equals(nengetsu6) Then _db.executeDB(SQL)

                'ワーク抽出
                SQL = ""
                SQL = SQL & N & " SELECT "
                SQL = SQL & N & "  HINMEICD,HINMEI,SU1,RYO1,SU2,RYO2,SU3,RYO3,SU4,RYO4,SU5,RYO5,SU6,RYO6 "
                SQL = SQL & N & " FROM ("
                SQL = SQL & N & " ( "
                '-----------------------縦横変換
                SQL = SQL & N & "		SELECT "
                SQL = SQL & N & "		  SUBSTR(HINMEICD,3,3) KEY1"
                SQL = SQL & N & "		 ,1 KEY2"
                SQL = SQL & N & "		 ,HINMEICD"
                SQL = SQL & N & "		 ,HINMEI "
                SQL = SQL & N & "		 ,SU1 ,RYO1  "
                SQL = SQL & N & "		 ,SU2 ,RYO2  "
                SQL = SQL & N & "		 ,SU3 ,RYO3  "
                SQL = SQL & N & "		 ,SU4 ,RYO4  "
                SQL = SQL & N & "		 ,SU5 ,RYO5  "
                SQL = SQL & N & "		 ,SU6 ,RYO6  "
                SQL = SQL & N & "		FROM ( "
                SQL = SQL & N & "			SELECT "
                SQL = SQL & N & "			  HINMEICD"
                SQL = SQL & N & "			 ,MAX(HINMEI) HINMEI "
                SQL = SQL & N & "			 ,SUM(SU1) SU1 ,SUM(RYO1) RYO1 "
                SQL = SQL & N & "			 ,SUM(SU2) SU2 ,SUM(RYO2) RYO2 "
                SQL = SQL & N & "			 ,SUM(SU3) SU3 ,SUM(RYO3) RYO3 "
                SQL = SQL & N & "			 ,SUM(SU4) SU4 ,SUM(RYO4) RYO4 "
                SQL = SQL & N & "			 ,SUM(SU5) SU5 ,SUM(RYO5) RYO5 "
                SQL = SQL & N & "			 ,SUM(SU6) SU6 ,SUM(RYO6) RYO6 "
                SQL = SQL & N & "			FROM ZE210Q_W10 "
                SQL = SQL & N & "			WHERE UPDNAME = '" & UtilClass.getComputerName() & "' "
                SQL = SQL & N & "			GROUP BY HINMEICD"
                SQL = SQL & N & "			)"
                SQL = SQL & N & " )UNION ALL( "
                '-----------------------ブレイクフッダ行抽出
                SQL = SQL & N & "		SELECT "
                SQL = SQL & N & "		  SUBSTR(HINMEICD,3,3) KEY1"
                SQL = SQL & N & "		 ,2 KEY2"
                SQL = SQL & N & "		 ,SUBSTR(HINMEICD,3,3) || '  品種計' HINMEICD"
                SQL = SQL & N & "		 ,'－' HINMEI "
                SQL = SQL & N & "		 ,SUM(SU1) ,SUM(RYO1)  "
                SQL = SQL & N & "		 ,SUM(SU2) ,SUM(RYO2)  "
                SQL = SQL & N & "		 ,SUM(SU3) ,SUM(RYO3)  "
                SQL = SQL & N & "		 ,SUM(SU4) ,SUM(RYO4)  "
                SQL = SQL & N & "		 ,SUM(SU5) ,SUM(RYO5)  "
                SQL = SQL & N & "		 ,SUM(SU6) ,SUM(RYO6)  "
                SQL = SQL & N & "		FROM ZE210Q_W10 "
                SQL = SQL & N & "		WHERE UPDNAME = '" & UtilClass.getComputerName() & "' "
                SQL = SQL & N & "		GROUP BY SUBSTR(HINMEICD,3,3) "
                SQL = SQL & N & " )UNION ALL( "
                '-----------------------空行抽出
                SQL = SQL & N & "		SELECT "
                SQL = SQL & N & "		  SUBSTR(HINMEICD,3,3) KEY1"
                SQL = SQL & N & "		 ,3 KEY2"
                SQL = SQL & N & "		 ,NULL"
                SQL = SQL & N & "		 ,NULL"
                SQL = SQL & N & "		 ,NULL"
                SQL = SQL & N & "		 ,NULL"
                SQL = SQL & N & "		 ,NULL"
                SQL = SQL & N & "		 ,NULL"
                SQL = SQL & N & "		 ,NULL"
                SQL = SQL & N & "		 ,NULL"
                SQL = SQL & N & "		 ,NULL"
                SQL = SQL & N & "		 ,NULL"
                SQL = SQL & N & "		 ,NULL"
                SQL = SQL & N & "		 ,NULL"
                SQL = SQL & N & "		 ,NULL"
                SQL = SQL & N & "		 ,NULL"
                SQL = SQL & N & "		FROM ZE210Q_W10 "
                SQL = SQL & N & "		WHERE UPDNAME = '" & UtilClass.getComputerName() & "' "
                SQL = SQL & N & "		GROUP BY SUBSTR(HINMEICD,3,3) "
                SQL = SQL & N & " ) )"
                SQL = SQL & N & " ORDER BY KEY1,KEY2,SUBSTR(HINMEICD,3,3),SUBSTR(HINMEICD,6,3),SUBSTR(HINMEICD,9,2),SUBSTR(HINMEICD,1,2),SUBSTR(HINMEICD,11,3) "
                ds = _db.selectDB(SQL, RS)

                rtnCnt = ds.Tables(RS).Rows.Count
            Finally
                Me.Cursor = c
            End Try
            If 65536 <= rtnCnt + DATA_START_ROW - 1 Then
                Throw New UsrDefException("検索結果（" & Format(rtnCnt, "#,##0") & "）がExcelの表示可能行数を超えています。" & N & "条件を見直してください。")
            ElseIf rtnCnt = 0 Then
                _msgHd.dspMSG("noTargetData")
                Exit Sub
            ElseIf rtnCnt > 10000 Then
                opMsg = N & N & "※データ件数が多いため、メモリの稼動状況によっては処理を完了できない場合があります。"
            End If

            Dim rtn As DialogResult = _msgHd.dspMSG("confirmRun", "出力行数：" & Format(rtnCnt, "#,##0") & opMsg)
            If rtn = Windows.Forms.DialogResult.No Or rtn = Windows.Forms.DialogResult.Cancel Then Exit Sub

            Dim pb As UtilProgresBarCancelable = New UtilProgresBarCancelable(Me)
            pb.Show()
            Try
                Application.DoEvents()
                pb.Cursor = Cursors.WaitCursor
                pb.Refresh()
                Application.DoEvents()

                'プログレスバー設定
                'pb.jobName = "出力を準備しています。"
                'pb.status = "データベース問合せ中．．．"


                pb.jobName = "出力しています。"

                Call printExcel(pb, ds.Tables(RS), bumonConditilnDsp, hinmeiCdConditionDsp, hinshuConditionDsp, hinmeiConditionDsp, hanbaiYmDsp, nengetsu1, nengetsu2, nengetsu3, nengetsu4, nengetsu5, nengetsu6)

            Catch pbe As UtilProgressBarCancelEx
                'キャンセル押下→処理なし
            Finally
                '画面消去
                pb.Close()
            End Try


        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　一覧出力
    '------------------------------------------------------------------------------------------------------
    Private Sub printExcel(ByRef prmRefPb As UtilProgresBarCancelable, ByVal prmDt As DataTable, ByVal prmBumonConditilnDsp As String, ByVal prmHinmeiCdConditionDsp As String, ByVal prmHinshuConditionDsp As String, ByVal prmHinmeiConditionDsp As String, ByVal prmZaikoYmDsp As String, ByVal prmNengetsu1 As String, ByVal prmNengetsu2 As String, ByVal prmNengetsu3 As String, ByVal prmNengetsu4 As String, ByVal prmNengetsu5 As String, ByVal prmNengetsu6 As String)
        '2011/01/21 add start Sugawara 
        Const EXCEL_COPYPASTE_MAX As Integer = 32000
        '2011/01/21 add end Sugawara 

        Try
            '雛形ファイル(品名別販売計画と同じ雛形)
            Dim openFilePath As String = StartUp.iniValue.BaseXlsPath & "\" & StartUp.iniValue.ExcelZE210R1_Base
            '雛形ファイルが開かれていないかチェック
            Dim fh As UtilFile = New UtilFile()
            Try
                fh.move(openFilePath, openFilePath & 1)
                fh.move(openFilePath & 1, openFilePath)
            Catch ioe As System.IO.IOException
                Throw New UsrDefException("ファイルが開かれています。ファイルを閉じてから再実行してください。", _
                                          _msgHd.getMSG("fileOpenErr", "【ファイル名】：" & openFilePath))
            End Try

            '出力用ファイル
            'ファイル名取得-----------------------------------------------------
            Dim wkEditFile As String = StartUp.iniValue.OutXlsPath & "\" & StartUp.iniValue.ExcelZE210R1_Out     'コピー先ファイル

            'コピー先ファイルが存在する場合、コピー先ファイルを削除----------------
            If UtilClass.isFileExists(wkEditFile) Then
                Try
                    fh.delete(wkEditFile)
                Catch ioe As System.IO.IOException
                    Throw New UsrDefException("ファイルが開かれています。ファイルを閉じてから再実行してください。", _
                                              _msgHd.getMSG("fileOpenErr", "【ファイル名】：" & wkEditFile))
                End Try
            End If

            Try
                '出力用ファイルへ雛型ファイルコピー
                FileCopy(openFilePath, wkEditFile)
            Catch ioe As System.IO.IOException
                Throw New UsrDefException(ioe, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ioe)))
            End Try

            Dim eh As xls.UtilExcelHandler = New xls.UtilExcelHandler(wkEditFile)
            Try
                eh.open()
                Try

                    prmRefPb.status = "ヘッダ情報構築中．．．"

                    '検索条件
                    Dim formatVal As String = eh.getValue(COND_ROW, COND_COL)
                    formatVal = formatVal.Replace("{@部門}", prmBumonConditilnDsp)
                    formatVal = formatVal.Replace("{@品名CD}", prmHinmeiCdConditionDsp)
                    formatVal = formatVal.Replace("{@品種CD}", prmHinshuConditionDsp)
                    formatVal = formatVal.Replace("{@品名}", prmHinmeiConditionDsp)
                    formatVal = formatVal.Replace("{@在庫年月}", prmZaikoYmDsp)
                    eh.setValue(formatVal, COND_ROW, COND_COL)

                    '作成日時
                    eh.setValue(eh.getValue(P_DT_ROW, P_DT_COL) & Format(Now, "yyyy/MM/dd HH:mm"), P_DT_ROW, P_DT_COL)

                    '年月
                    If "".Equals(prmNengetsu1) Then
                        eh.setValue("", YM___ROW, YM1__COL)
                    Else
                        eh.setValue(prmNengetsu1.Substring(0, 4) & "/" & prmNengetsu1.Substring(4, 2), YM___ROW, YM1__COL)
                    End If
                    If "".Equals(prmNengetsu2) Then
                        eh.setValue("", YM___ROW, YM2__COL)
                    Else
                        eh.setValue(prmNengetsu2.Substring(0, 4) & "/" & prmNengetsu2.Substring(4, 2), YM___ROW, YM2__COL)
                    End If
                    If "".Equals(prmNengetsu3) Then
                        eh.setValue("", YM___ROW, YM3__COL)
                    Else
                        eh.setValue(prmNengetsu3.Substring(0, 4) & "/" & prmNengetsu3.Substring(4, 2), YM___ROW, YM3__COL)
                    End If
                    If "".Equals(prmNengetsu4) Then
                        eh.setValue("", YM___ROW, YM4__COL)
                    Else
                        eh.setValue(prmNengetsu4.Substring(0, 4) & "/" & prmNengetsu4.Substring(4, 2), YM___ROW, YM4__COL)
                    End If
                    If "".Equals(prmNengetsu5) Then
                        eh.setValue("", YM___ROW, YM5__COL)
                    Else
                        eh.setValue(prmNengetsu5.Substring(0, 4) & "/" & prmNengetsu5.Substring(4, 2), YM___ROW, YM5__COL)
                    End If
                    If "".Equals(prmNengetsu6) Then
                        eh.setValue("", YM___ROW, YM6__COL)
                    Else
                        eh.setValue(prmNengetsu6.Substring(0, 4) & "/" & prmNengetsu6.Substring(4, 2), YM___ROW, YM6__COL)
                    End If
                    prmRefPb.status = "出力領域確保中．．．"

                    '出力行数拡張
                    If prmDt.Rows.Count >= 2 Then
                        '通常
                        Dim insRow As Integer = prmDt.Rows.Count
                        If insRow > EXCEL_COPYPASTE_MAX Then
                            '16bit範囲(Short型)を超えるとCOMが例外を吐くので、とりあえずEXCEL_COPYPASTE_MAXぐらいで処理分割する
                            Dim insCnt As Integer = insRow \ EXCEL_COPYPASTE_MAX
                            For ic As Integer = 0 To insCnt
                                Dim rowNum As Integer = 0
                                If ic = insCnt Then
                                    '最終挿入
                                    rowNum = insRow - ic * EXCEL_COPYPASTE_MAX
                                Else
                                    'まだ最終でない
                                    '2011/01/21 upd start Sugawara #93
                                    'rowNum = EXCEL_COPYPASTE_MAX
                                    rowNum = EXCEL_COPYPASTE_MAX + 1
                                    '2011/01/21 upd end Sugawara #93
                                End If
                                eh.copyRow(DATA_START_ROW)
                                eh.insertPasteRow(DATA_START_ROW, (DATA_START_ROW + 1) + (rowNum - 1) - 2)
                            Next
                        Else
                            '16bit限界を超えないので一発でコピー貼り付け
                            eh.copyRow(DATA_START_ROW)
                            eh.insertPasteRow(DATA_START_ROW, (DATA_START_ROW + 1) + (prmDt.Rows.Count - 1) - 2)
                        End If
                    ElseIf prmDt.Rows.Count = 0 Then
                        '出力なし
                        eh.deleteRow(DATA_START_ROW)
                    End If

                    '出力文字列構築
                    prmRefPb.status = "データ構築中．．．"
                    Dim buf As System.Text.StringBuilder = New System.Text.StringBuilder
                    With buf
                        prmRefPb.maxVal = prmDt.Rows.Count
                        For i As Integer = 0 To prmDt.Rows.Count - 1
                            .Append(_db.rmNullStr(prmDt.Rows(i)("HINMEICD")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("HINMEI")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("SU1")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("RYO1")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("SU2")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("RYO2")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("SU3")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("RYO3")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("SU4")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("RYO4")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("SU5")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("RYO5")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("SU6")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("RYO6")) & ControlChars.Tab)
                            .Append(ControlChars.CrLf)
                            If i Mod 50 = 0 Then
                                prmRefPb.value = i
                            End If
                        Next
                    End With

                    If Not "".Equals(buf.ToString) Then
                        prmRefPb.status = "データ出力中．．．"
                        Clipboard.SetText(buf.ToString)
                        Try
                            eh.paste(DATA_START_ROW, DATA_START_COL)
                        Catch ex As Exception
                            If "データを貼り付けできません。".Equals(ex.Message) Then
                                Throw New UsrDefException("データ(" & Format(prmDt.Rows.Count, "#,##0") & "件)が多すぎるため、処理を継続できません。" & N & "条件を見直してください。")
                            End If
                            Throw ex
                        End Try
                        Try
                            Clipboard.Clear()
                        Catch ex As Exception
                        End Try
                    End If

                Finally
                    eh.close()
                End Try

                'EXCELファイル開く
                eh.display()

            Catch pge As UtilProgressBarCancelEx
                Throw pge
            Catch ue As UsrDefException
                ue.dspMsg()
                Throw ue
            Catch ex As Exception
                'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)), , StartUp.iniValue.LogFilePath)
            Finally
                eh.endUse()
                eh = Nothing
            End Try

        Catch pge As UtilProgressBarCancelEx
            Throw pge
        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub
End Class