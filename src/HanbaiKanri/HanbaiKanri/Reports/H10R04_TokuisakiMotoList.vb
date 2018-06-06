'===============================================================================
'
'�@�J�l�L�g�c���X�l
'�@�@�i�V�X�e�����j�̔��Ǘ�
'�@�@�i�����@�\���j���Ӑ挳��
'    �i�t�H�[��ID�jH10R04
'
'===============================================================================
'�@�����@���O�@�@�@�@�@���@�t       �}�[�N      ���e
'-------------------------------------------------------------------------------
'�@(1)   ���        2018/03/12                 �V�K              
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
    Private _comLogc As CommonLogic                         '���ʏ����p
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
    '�T�u���|�[�g
    Private rptSub As H10R04S_TokuisakiMotoList

    Private Const RS As String = "RecSet"                   '���R�[�h�Z�b�g�e�[�u��
    Private Const RS2 As String = "RecSet2"                   '���R�[�h�Z�b�g�e�[�u��
    Private Const ITAKU_INFO_NO As String = "�o�͂��Ȃ�"
    Private Const ITAKU_ZAN_YES As String = "�ϑ��c�w�肠��"
    Private Const ITAKU_ZAN_NO As String = "�ϑ��c�w��Ȃ�"


    '-------------------------------------------------------------------------------
    '�v���p�e�B�錾
    '-------------------------------------------------------------------------------
    Public Shared ReadOnly Property parentDb() As UtilDBIf
        Get
            Return _db
        End Get
    End Property


    '---------------------------------------------------------------------------------
    '���|�[�g��\
    '---------------------------------------------------------------------------------
    Public Sub runReport() Implements RepSectionIF.runReport
        Dim v As New PreviewForm
        v.viewer.Document = Me.Document
        Me.Run()
        If frmH10F04_TokuisakiMotoList.printKbn = CommonConst.REPORT_PREVIEW Then
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
        _shoriId = frmH10F04_TokuisakiMotoList.shoriId                         '����ID
        _userId = frmC01F10_Login.loginValue.TantoCD                        '���[�U�h�c
        _printKbn = frmH10F04_TokuisakiMotoList.printKbn

    End Sub

    '---------------------------------------------------------------------------------
    '�t�F�b�`�f�[�^
    '---------------------------------------------------------------------------------
    Private Sub H10R04_TokuisakiMotoList_FetchData(sender As Object, eArgs As FetchEventArgs) Handles MyBase.FetchData
        Try

            '�O���[�s���O�p
            fld������.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("������R�[�h"))
            _seikyusakiCd = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("������R�[�h"))
            _hidukeFrom = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����_���tFrom"))
            _hidukeTo = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����_���tTo"))
            _itakuInfo = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����_�ϑ��c���"))

            '�y�[�W�w�b�_
            txt�敪�w�b�_.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����_�敪"))
            txt��������N���w�b�_.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����_�N��"))
            txt���t�w�b�_.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����_���t"))
            txt�쐬���w�b�_.Text = _comLogc.getSysDdate
            txt�o�͏��w�b�_.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����_�o�͏�"))
            jyokenBotonn = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����_���O�p"))
            '�y�[�W�̓v���p�e�B�ōs���Ă���

            '������w�b�_
            txt������R�[�h.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("������R�[�h"))
            txt�����於.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�����於"))

            '�c���v�Z�@�O���ׂ̎c���{����z�{����Ł|�����z
            If _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�擾���e�[�u��")).Equals("1") Then
                _zandaka = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�c��"))
            Else
                _zandaka = _zandaka + _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("����z")) _
                                    + _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�����")) _
                                    - _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�����z"))

            End If

            '����
            If _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�擾���e�[�u��")).Equals("1") Then
                '���c��
                txt�����.Text = ""
                txt�`�[�ԍ�.Text = ""
                txt�敪.Text = ""
                txt�o�א於.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�o�א�"))
                txt�ϑ��c����\��.Text = ""
                txt���i�����E�v.Text = ""
                txt����.Text = ""
                txt�P��.Text = ""
                txt�P��.Text = ""

                '������t�b�^�W�v�p�i�W�v�̓v���p�e�B�ōs���Ă���j
                fld����z.Value = 0
                fld�����.Value = 0
                fld�����z.Value = 0
                fld�c��.Value = _zandaka

            ElseIf _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�擾���e�[�u��")).Equals("2") Then
                '�����{�^����
                If Not _denpyoBango.Equals(_db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�`�[�ԍ�"))) Then
                    txt�����.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�����"))
                    txt�`�[�ԍ�.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�`�[�ԍ�"))
                    txt�敪.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�敪"))
                    txt�o�א於.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�o�א�"))
                Else
                    '�d�����\��
                    txt�����.Text = ""
                    txt�`�[�ԍ�.Text = ""
                    txt�敪.Text = ""
                    txt�o�א於.Text = ""
                End If
                txt�ϑ��c����\��.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�ϑ��c����\��"))
                txt���i�����E�v.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("���i�����E�v"))
                txt����.Text = Decimal.Parse(_ds.Tables(RS).Rows(rowIdx)("����")).ToString("#,##0.00")
                txt�P��.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�P��"))
                txt�P��.Text = Decimal.Parse(_ds.Tables(RS).Rows(rowIdx)("�P��")).ToString("#,##0.00")

                '������t�b�^�W�v�p�i�W�v�̓v���p�e�B�ōs���Ă���j
                fld����z.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����z"))
                fld�����.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�����"))
                fld�����z.Value = 0
                fld�c��.Value = _zandaka
            Else
                '������{�^����
                If Not _denpyoBango.Equals(_db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�`�[�ԍ�"))) Then
                    txt�����.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�����"))
                    txt�`�[�ԍ�.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�`�[�ԍ�"))
                    txt�敪.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�敪"))
                Else
                    '�d�����\��
                    txt�����.Text = ""
                    txt�`�[�ԍ�.Text = ""
                    txt�敪.Text = ""
                End If
                txt�o�א於.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�o�א�"))
                txt�ϑ��c����\��.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�ϑ��c����\��"))
                txt���i�����E�v.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("���i�����E�v"))
                txt����.Text = ""
                txt�P��.Text = ""
                txt�P��.Text = ""
                txt����z.Text = ""
                txt�����.Text = ""

                '������t�b�^�W�v�p�i�W�v�̓v���p�e�B�ōs���Ă���j
                fld����z.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����z"))
                fld�����.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�����"))
                fld�����z.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�����z"))
                fld�c��.Value = _zandaka
            End If
            txt�ϑ���.Text = ""

            '�ϑ��c���̕\��
            If _itakuInfo.Equals(ITAKU_INFO_NO) Then
                txt�ϑ��c����\��.Visible = False
                lbl�ϑ��c����.Visible = False
                SubReport1.Visible = False
                _itakuZanStr = ITAKU_ZAN_NO
            Else
                txt�ϑ��c����\��.Visible = True
                lbl�ϑ��c����.Visible = True
                SubReport1.Visible = True
                _itakuZanStr = ITAKU_ZAN_YES
            End If

            '���ׂ̏��
            If _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�擾���e�[�u��")).Equals("1") Then
                Line4.Visible = False
            ElseIf Not _denpyoBango.Equals(_db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�`�[�ԍ�"))) Then
                Line4.Visible = True
            Else
                Line4.Visible = False
            End If
            _denpyoBango = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�`�[�ԍ�"))

            '�y�[�W�t�b�^
            txt��Ж�.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("��Ж�"))

            '���|�[�g�t�b�^

            rowIdx = rowIdx + 1

            '���O�p
            iMeisaiCnt = rowIdx

            eArgs.EOF = False
        Catch ie As IndexOutOfRangeException
            eArgs.EOF = True '�f�[�^����
        End Try

    End Sub


    '---------------------------------------------------------------------------------
    'Detail_Format�i�y�[�W�t�b�^�[�̕����ݒ�j
    '---------------------------------------------------------------------------------
    Private Sub Detail_Format(sender As Object, e As EventArgs) Handles Detail.Format

    End Sub

    '---------------------------------------------------------------------------------
    '�y�[�W�w�b�_�[�i�y�[�W�t�b�^�[�̕����ݒ�j
    '---------------------------------------------------------------------------------
    Private Sub PageHeader_Format(sender As Object, e As EventArgs) Handles PageHeader.Format

    End Sub

    '---------------------------------------------------------------------------------
    '���|�[�g�G���h
    '---------------------------------------------------------------------------------
    Private Sub H10R04_TokuisakiMotoList_ReportEnd(sender As Object, e As EventArgs) Handles MyBase.ReportEnd

        '���엚�����O�쐬
        Call _comLogc.Insert_L01_ProcLog(_companyCd, DBNull.Value, _shoriId, CommonConst.PROGRAM_REPORT, CommonConst.STATUS_NORMAL,
                                                jyokenBotonn, txt��������N���w�b�_.Text, txt���t�w�b�_.Text, _printKbn, _itakuZanStr,
                                                iMeisaiCnt, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _userId)


    End Sub


    '---------------------------------------------------------------------------------
    '�O���[�v�w�b�_
    '---------------------------------------------------------------------------------
    Private Sub gh������_Format(sender As Object, e As EventArgs) Handles gh������.Format

        '�T�u���|�[�g
        Dim sSql As String = ""
        Dim rc As Integer = 0

        sSql = sSql & "SELECT "
        sSql = sSql & "  t15.����          �ϑ���"
        sSql = sSql & " ,t15.�ϑ��`��      �`�[�ԍ�"
        sSql = sSql & " ,'�ϑ�'            �敪"
        sSql = sSql & " ,t15.�o�א於      �o�א�"
        sSql = sSql & " ,t16.���i�� || COALESCE(t16.�׎p�`��,'') ���i��"       'NVL
        sSql = sSql & " ,t16.�ϑ��c��      �c����"
        sSql = sSql & " ,t16.�P��          �P��"
        sSql = sSql & " ,t16.���P��"
        sSql = sSql & " ,t16.�ϑ��c���z    �����z"
        sSql = sSql & " FROM t15_itakhd t15"      '�ϑ���{
        sSql = sSql & " LEFT JOIN t16_itakdt As t16    On t16.��ЃR�[�h   = t15.��ЃR�[�h"
        sSql = sSql & "                               And t16.�ϑ��`��     = t15.�ϑ��`��"
        sSql = sSql & " WHERE t15.��ЃR�[�h   = '" & _companyCd & "'"
        sSql = sSql & "   AND t15.����敪     = '0'"
        sSql = sSql & "   AND t15.������R�[�h = '" & _seikyusakiCd & "'"
        sSql = sSql & "   AND TO_CHAR(t15.����,'YYYY/MM/DD') >= '" & _hidukeFrom & "'"
        sSql = sSql & "   AND TO_CHAR(t15.����,'YYYY/MM/DD') <= '" & _hidukeTo & "'"
        sSql = sSql & " ORDER BY t15.����, t15.�ϑ��`��, t16.�s��"

        Dim ds As DataSet = _db.selectDB(sSql, RS2, rc)

        rptSub.DataSource = ds
        rptSub.DataMember = RS2
        SubReport1.Report = rptSub

    End Sub

    '---------------------------------------------------------------------------------
    '���|�[�g�X�^�[�g
    '---------------------------------------------------------------------------------
    Private Sub H10R04_TokuisakiMotoList_ReportStart(sender As Object, e As EventArgs) Handles MyBase.ReportStart
        rptSub = New H10R04S_TokuisakiMotoList()
    End Sub

End Class
