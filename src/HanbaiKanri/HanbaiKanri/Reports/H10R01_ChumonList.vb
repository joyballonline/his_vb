'===============================================================================
'
'@JlLgc¤Xl
'@@iVXe¼jÌÇ
'@@i@\¼j¶¾×\
'    itH[IDjH10R01
'
'===============================================================================
'@ð@¼O@@@@@ú@t       }[N      àe
'-------------------------------------------------------------------------------
'@(1)   ëì        2018/02/26                 VK              
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
    Private _comLogc As CommonLogic                         '¤Êp
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

    Private Const RS As String = "RecSet"                   'R[hZbge[u

    '---------------------------------------------------------------------------------
    '|[gì\
    '---------------------------------------------------------------------------------
    Public Sub runReport() Implements RepSectionIF.runReport
        Dim v As New PreviewForm
        v.viewer.Document = Me.Document
        Me.Run()
        If frmH10F01_ChumonList.printKbn = CommonConst.REPORT_PREVIEW Then
            v.Show()            'vr[æÊ
        Else
            Me.Document.Print   'v^Ið_CAO
        End If
    End Sub

    '---------------------------------------------------------------------------------
    'f[^óÌ
    '---------------------------------------------------------------------------------
    Public Sub setData(prmDs As DataSet, ByRef prmRefDbHd As UtilDBIf) Implements RepSectionIF.setData

        _ds = prmDs
        _db = prmRefDbHd
        rowIdx = 0
        ' ¤ÊgpÌõ
        _comLogc = New CommonLogic(_db, _msgHd)                             ' ¤Êp
        _companyCd = frmC01F10_Login.loginValue.BumonCD                     'ïÐR[h
        _shoriId = frmH10F01_ChumonList.shoriId                             'ID
        _userId = frmC01F10_Login.loginValue.TantoCD                        '[Uhc
        _printKbn = frmH10F01_ChumonList.printKbn

    End Sub

    '---------------------------------------------------------------------------------
    '`[O[vwb_ú»
    '---------------------------------------------------------------------------------
    Private Sub gh`[_Format(sender As Object, e As EventArgs) Handles gh`[.Format

    End Sub

    '---------------------------------------------------------------------------------
    'tFb`f[^
    '---------------------------------------------------------------------------------
    Private Sub H10R01_ChumonList_FetchData(sender As Object, eArgs As FetchEventArgs) Handles MyBase.FetchData
        Try

            'O[sOp
            fid`[Ô.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("¶`Ô"))

            'y[Wwb_
            txtæªwb_.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("ð_æª"))
            txto×úwb_.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("ð_o×ú"))
            txt`[Ôwb_.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("ð_`[Ô"))
            txtì¬úwb_.Text = _comLogc.getSysDdate
            'y[WÍvpeBÅsÁÄ¢é

            '`[Ôwb_
            txt`[Ô.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("¶`Ô"))
            txto×ú.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("o×ú"))
            txto×æ¼.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("o×æ¼"))
            txt¿æID.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("¿æID"))
            txt¿æ¼.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("¿æ¼"))
            txtú.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("ú"))

            '¾×
            txt¤i¼.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("¤i¼"))
            txtâ.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("âæª"))
            txtÅæª.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("ÛÅæª"))
            fldü.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("ü"))
            fldÂ.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("Â"))
            fldÊ.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("Ê"))
            txtPÊ.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("PÊ"))
            fldãP¿.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("¶P¿"))
            fldãàz.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("¶àz"))
            txt¾×õl.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("¾×õl"))

            '`[Ôtb^
            fldãàzO[v.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("ãàzv"))
            fldÁïÅO[v.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("ÁïÅv"))
            fldvàzO[v.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("Åzv"))
            txtÐOõl.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("ÐOõl"))

            'y[Wtb^
            txtïÐ¼.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("ïÐ¼"))

            '`[ÔuCN
            If Not _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("¶`Ô")).Equals(sDenpyoNo) Then
                dcUriageKin = dcUriageKin + _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("ãàzv"))
                dcShohizei = dcShohizei + _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("ÁïÅv"))
                dcGoukeiKin = dcGoukeiKin + _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("Åzv"))
                iDenpyoCnt = iDenpyoCnt + 1
            End If
            sDenpyoNo = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("¶`Ô"))

            '|[gtb^
            iMeisaiCnt = iMeisaiCnt + 1
            txt`[.Text = Decimal.Parse(iDenpyoCnt).ToString("#,##0") & " "
            txt¾×.Text = Decimal.Parse(iMeisaiCnt).ToString("#,##0") & " "

            fldãàzy[W.Value = Decimal.Parse(dcUriageKin)
            fldÁïÅy[W.Value = Decimal.Parse(dcShohizei)
            fldvàzy[W.Value = Decimal.Parse(dcGoukeiKin)

            rowIdx = rowIdx + 1
            eArgs.EOF = False
        Catch ie As IndexOutOfRangeException
            eArgs.EOF = True 'f[^ö
        End Try

    End Sub

    '---------------------------------------------------------------------------------
    '|[gGh
    '---------------------------------------------------------------------------------
    Private Sub H10R01_ChumonList_ReportEnd(sender As Object, e As EventArgs) Handles MyBase.ReportEnd

        'ìðOì¬
        Call _comLogc.Insert_L01_ProcLog(_companyCd, DBNull.Value, _shoriId, CommonConst.PROGRAM_REPORT, CommonConst.STATUS_NORMAL,
                                                txtæªwb_.Text, txto×úwb_.Text, txt`[Ôwb_.Text, _printKbn, DBNull.Value,
                                                iDenpyoCnt, iMeisaiCnt, dcUriageKin, dcShohizei, dcGoukeiKin, _userId)


    End Sub

End Class
