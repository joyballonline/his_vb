'===============================================================================
'
'@JlLgc¤Xl
'@@iVXe¼jÌÇ
'@@i@\¼jdü\
'    itH[IDjH11R21
'
'===============================================================================
'@ð@¼O@@@@@ú@t       }[N      àe
'-------------------------------------------------------------------------------
'@(1)   ëì        2018/03/23                 VK              
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
    Private _comLogc As CommonLogic                         '¤Êp
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

    Private Const RS As String = "RecSet"                           'R[hZbge[u

    '---------------------------------------------------------------------------------
    '|[gì\
    '---------------------------------------------------------------------------------
    Public Sub runReport() Implements RepSectionIF.runReport
        Dim v As New PreviewForm
        v.viewer.Document = Me.Document
        Me.Run()
        If frmH11F21_ShiireSokatuList.printKbn = CommonConst.REPORT_PREVIEW Then
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
        _shoriId = frmH11F21_ShiireSokatuList.shoriId                             'ID
        _userId = frmC01F10_Login.loginValue.TantoCD                        '[Uhc
        _printKbn = frmH11F21_ShiireSokatuList.printKbn

    End Sub

    '---------------------------------------------------------------------------------
    'tFb`f[^
    '---------------------------------------------------------------------------------
    Private Sub H11R21_ShiireSokatuList_FetchData(sender As Object, eArgs As FetchEventArgs) Handles MyBase.FetchData
        Try

            'y[Wwb_
            txtæªwb_.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("ð_æª"))
            txtdüNxwb_.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("ð_düNx"))
            txtì¬úwb_.Text = _comLogc.getSysDdate
            'y[WÍvpeBÅsÁÄ¢é

            '¾×
            txtdüæR[h.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("düæR[h"))
            txtdüæ¼.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("düæ¼"))
            fldQÊ.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("QdüÊ"))
            fldRÊ.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("RdüÊ"))
            fldSÊ.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("SdüÊ"))
            fldTÊ.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("TdüÊ"))
            fldÊv.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("QdüÊ")) _
                            + _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("RdüÊ")) _
                            + _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("SdüÊ")) _
                            * _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("TdüÊ"))

            fldQàz.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("Qdüàz"))
            fldRàz.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("Rdüàz"))
            fldSàz.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("Sdüàz"))
            fldTàz.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("Tdüàz"))
            fldàzv.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("Qdüàz")) _
                            + _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("Rdüàz")) _
                            + _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("Sdüàz")) _
                            * _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("Tdüàz"))

            iKensuYoko = 0
            If _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("Q")) = 0 Then
                dcTanka2 = 0
            Else
                dcTanka2 = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("QdüP¿")) _
                         / _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("Q"))
                iKensuYoko = iKensuYoko + 1
                'cWv
                dcTanka2Sum = dcTanka2Sum + dcTanka2
                iTanka2KensuTate = iTanka2KensuTate + 1
            End If
            fldQ½ÏP¿.Value = dcTanka2
            If _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("R")) = 0 Then
                dcTanka3 = 0
            Else
                dcTanka3 = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("RdüP¿")) _
                         / _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("R"))
                iKensuYoko = iKensuYoko + 1
                'cWv
                dcTanka3Sum = dcTanka3Sum + dcTanka3
                iTanka3KensuTate = iTanka3KensuTate + 1
            End If
            fldR½ÏP¿.Value = dcTanka3
            If _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("S")) = 0 Then
                dcTanka4 = 0
            Else
                dcTanka4 = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("SdüP¿")) _
                         / _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("S"))
                iKensuYoko = iKensuYoko + 1
                'cWv
                dcTanka4Sum = dcTanka4Sum + dcTanka4
                iTanka4KensuTate = iTanka4KensuTate + 1
            End If
            fldS½ÏP¿.Value = dcTanka4
            If _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("T")) = 0 Then
                dcTanka5 = 0
            Else
                dcTanka5 = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("TdüP¿")) _
                         / _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("T"))
                iKensuYoko = iKensuYoko + 1
                'cWv
                dcTanka5Sum = dcTanka5Sum + dcTanka5
                iTanka5KensuTate = iTanka5KensuTate + 1
            End If
            fldS½ÏP¿.Value = dcTanka5
            '½ÏP¿v
            If iKensuYoko = 0 Then
                fld½ÏP¿v.Value = 0
            Else
                fld½ÏP¿v.Value = (dcTanka2 + dcTanka3 + dcTanka4 + dcTanka5) / iKensuYoko
            End If

            'y[Wtb^
            txtïÐ¼.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("ïÐ¼"))

            '|[gtb^
            dcSuryoKei = dcSuryoKei + CType(fldÊv.Value, Decimal)
            dcKingakuKei = dcSuryoKei + CType(fldàzv.Value, Decimal)
            If iTanka2KensuTate = 0 Then
                fldQ½ÏP¿v.Value = 0
            Else
                fldQ½ÏP¿v.Value = dcTanka2Sum / iTanka2KensuTate
            End If
            If iTanka3KensuTate = 0 Then
                fldR½ÏP¿v.Value = 0
            Else
                fldR½ÏP¿v.Value = dcTanka3Sum / iTanka3KensuTate
            End If
            If iTanka4KensuTate = 0 Then
                fldS½ÏP¿v.Value = 0
            Else
                fldS½ÏP¿v.Value = dcTanka4Sum / iTanka4KensuTate
            End If
            If iTanka5KensuTate = 0 Then
                fldT½ÏP¿v.Value = 0
            Else
                fldT½ÏP¿v.Value = dcTanka5Sum / iTanka5KensuTate
            End If
            If (iTanka2KensuTate + iTanka3KensuTate + iTanka4KensuTate + iTanka5KensuTate) = 0 Then
                fld½ÏP¿v.Value = 0
            Else
                fld½ÏP¿v.Value = (dcTanka2Sum + dcTanka3Sum + dcTanka4Sum + dcTanka5Sum) _
                                    / (iTanka2KensuTate + iTanka3KensuTate + iTanka4KensuTate + iTanka5KensuTate)
            End If

            rowIdx = rowIdx + 1
            eArgs.EOF = False
        Catch ie As IndexOutOfRangeException
            eArgs.EOF = True 'f[^ö
        End Try

    End Sub

    '---------------------------------------------------------------------------------
    '|[gGh
    '---------------------------------------------------------------------------------
    Private Sub H11R21_ShiireSokatuList_ReportEnd(sender As Object, e As EventArgs) Handles MyBase.ReportEnd

        'ìðOì¬
        Call _comLogc.Insert_L01_ProcLog(_companyCd, DBNull.Value, _shoriId, CommonConst.PROGRAM_REPORT, CommonConst.STATUS_NORMAL,
                                                DBNull.Value, txtdüNxwb_.Text, DBNull.Value, _printKbn, DBNull.Value,
                                                dcSuryoKei, dcKingakuKei, dcTankaKei, DBNull.Value, DBNull.Value, _userId)


    End Sub

End Class
