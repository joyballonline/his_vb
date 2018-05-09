'===============================================================================
'
'�@�J�l�L�g�c���X�l
'�@�@�i�V�X�e�����j�̔��Ǘ�
'�@�@�i�����@�\���j�d�������\
'    �i�t�H�[��ID�jH11R21
'
'===============================================================================
'�@�����@���O�@�@�@�@�@���@�t       �}�[�N      ���e
'-------------------------------------------------------------------------------
'�@(1)   ���        2018/03/23                 �V�K              
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
    Private _comLogc As CommonLogic                         '���ʏ����p
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

    Private Const RS As String = "RecSet"                           '���R�[�h�Z�b�g�e�[�u��

    '---------------------------------------------------------------------------------
    '���|�[�g��\
    '---------------------------------------------------------------------------------
    Public Sub runReport() Implements RepSectionIF.runReport
        Dim v As New PreviewForm
        v.viewer.Document = Me.Document
        Me.Run()
        If frmH11F21_ShiireSokatuList.printKbn = CommonConst.REPORT_PREVIEW Then
            v.Show()            '�v���r���[���
        Else
            Me.Document.Print   '�v�����^�I���_�C�A���O
        End If
    End Sub

    '---------------------------------------------------------------------------------
    '�f�[�^���
    '---------------------------------------------------------------------------------
    Public Sub setData(prmDs As DataSet, ByRef prmRefDbHd As UtilDBIf) Implements RepSectionIF.setData

        _ds = prmDs
        _db = prmRefDbHd
        rowIdx = 0
        ' ���ʏ����g�p�̏���
        _comLogc = New CommonLogic(_db, _msgHd)                             ' ���ʏ����p
        _companyCd = frmC01F10_Login.loginValue.BumonCD                     '��ЃR�[�h
        _shoriId = frmH11F21_ShiireSokatuList.shoriId                             '����ID
        _userId = frmC01F10_Login.loginValue.TantoCD                        '���[�U�h�c
        _printKbn = frmH11F21_ShiireSokatuList.printKbn

    End Sub

    '---------------------------------------------------------------------------------
    '�t�F�b�`�f�[�^
    '---------------------------------------------------------------------------------
    Private Sub H11R21_ShiireSokatuList_FetchData(sender As Object, eArgs As FetchEventArgs) Handles MyBase.FetchData
        Try

            '�y�[�W�w�b�_
            txt�敪�w�b�_.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����_�敪"))
            txt�d���N�x�w�b�_.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����_�d���N�x"))
            txt�쐬���w�b�_.Text = _comLogc.getSysDdate
            '�y�[�W�̓v���p�e�B�ōs���Ă���

            '����
            txt�d����R�[�h.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�d����R�[�h"))
            txt�d���於.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�d���於"))
            fld�Q������.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�Q���d������"))
            fld�R������.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�R���d������"))
            fld�S������.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�S���d������"))
            fld�T������.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�T���d������"))
            fld���ʌv.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�Q���d������")) _
                            + _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�R���d������")) _
                            + _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�S���d������")) _
                            * _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�T���d������"))

            fld�Q�����z.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�Q���d�����z"))
            fld�R�����z.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�R���d�����z"))
            fld�S�����z.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�S���d�����z"))
            fld�T�����z.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�T���d�����z"))
            fld���z�v.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�Q���d�����z")) _
                            + _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�R���d�����z")) _
                            + _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�S���d�����z")) _
                            * _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�T���d�����z"))

            iKensuYoko = 0
            If _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�Q������")) = 0 Then
                dcTanka2 = 0
            Else
                dcTanka2 = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�Q���d���P��")) _
                         / _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�Q������"))
                iKensuYoko = iKensuYoko + 1
                '�c�W�v
                dcTanka2Sum = dcTanka2Sum + dcTanka2
                iTanka2KensuTate = iTanka2KensuTate + 1
            End If
            fld�Q�����ϒP��.Value = dcTanka2
            If _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�R������")) = 0 Then
                dcTanka3 = 0
            Else
                dcTanka3 = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�R���d���P��")) _
                         / _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�R������"))
                iKensuYoko = iKensuYoko + 1
                '�c�W�v
                dcTanka3Sum = dcTanka3Sum + dcTanka3
                iTanka3KensuTate = iTanka3KensuTate + 1
            End If
            fld�R�����ϒP��.Value = dcTanka3
            If _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�S������")) = 0 Then
                dcTanka4 = 0
            Else
                dcTanka4 = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�S���d���P��")) _
                         / _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�S������"))
                iKensuYoko = iKensuYoko + 1
                '�c�W�v
                dcTanka4Sum = dcTanka4Sum + dcTanka4
                iTanka4KensuTate = iTanka4KensuTate + 1
            End If
            fld�S�����ϒP��.Value = dcTanka4
            If _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�T������")) = 0 Then
                dcTanka5 = 0
            Else
                dcTanka5 = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�T���d���P��")) _
                         / _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�T������"))
                iKensuYoko = iKensuYoko + 1
                '�c�W�v
                dcTanka5Sum = dcTanka5Sum + dcTanka5
                iTanka5KensuTate = iTanka5KensuTate + 1
            End If
            fld�S�����ϒP��.Value = dcTanka5
            '���ϒP���v
            If iKensuYoko = 0 Then
                fld���ϒP���v.Value = 0
            Else
                fld���ϒP���v.Value = (dcTanka2 + dcTanka3 + dcTanka4 + dcTanka5) / iKensuYoko
            End If

            '�y�[�W�t�b�^
            txt��Ж�.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("��Ж�"))

            '���|�[�g�t�b�^
            dcSuryoKei = dcSuryoKei + CType(fld���ʌv.Value, Decimal)
            dcKingakuKei = dcSuryoKei + CType(fld���z�v.Value, Decimal)
            If iTanka2KensuTate = 0 Then
                fld�Q�����ϒP�������v.Value = 0
            Else
                fld�Q�����ϒP�������v.Value = dcTanka2Sum / iTanka2KensuTate
            End If
            If iTanka3KensuTate = 0 Then
                fld�R�����ϒP�������v.Value = 0
            Else
                fld�R�����ϒP�������v.Value = dcTanka3Sum / iTanka3KensuTate
            End If
            If iTanka4KensuTate = 0 Then
                fld�S�����ϒP�������v.Value = 0
            Else
                fld�S�����ϒP�������v.Value = dcTanka4Sum / iTanka4KensuTate
            End If
            If iTanka5KensuTate = 0 Then
                fld�T�����ϒP�������v.Value = 0
            Else
                fld�T�����ϒP�������v.Value = dcTanka5Sum / iTanka5KensuTate
            End If
            If (iTanka2KensuTate + iTanka3KensuTate + iTanka4KensuTate + iTanka5KensuTate) = 0 Then
                fld���ϒP�������v.Value = 0
            Else
                fld���ϒP�������v.Value = (dcTanka2Sum + dcTanka3Sum + dcTanka4Sum + dcTanka5Sum) _
                                    / (iTanka2KensuTate + iTanka3KensuTate + iTanka4KensuTate + iTanka5KensuTate)
            End If

            rowIdx = rowIdx + 1
            eArgs.EOF = False
        Catch ie As IndexOutOfRangeException
            eArgs.EOF = True '�f�[�^����
        End Try

    End Sub

    '---------------------------------------------------------------------------------
    '���|�[�g�G���h
    '---------------------------------------------------------------------------------
    Private Sub H11R21_ShiireSokatuList_ReportEnd(sender As Object, e As EventArgs) Handles MyBase.ReportEnd

        '���엚�����O�쐬
        Call _comLogc.Insert_L01_ProcLog(_companyCd, DBNull.Value, _shoriId, CommonConst.PROGRAM_REPORT, CommonConst.STATUS_NORMAL,
                                                DBNull.Value, txt�d���N�x�w�b�_.Text, DBNull.Value, _printKbn, DBNull.Value,
                                                dcSuryoKei, dcKingakuKei, dcTankaKei, DBNull.Value, DBNull.Value, _userId)


    End Sub

End Class
