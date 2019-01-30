Namespace DB

    '===============================================================================
    '
    '  ���[�e�B���e�B�N���X
    '    �i�N���X���j    UtilDBIf
    '    �i�����@�\���j     Util.DB�ɂ��DB�A�N�Z�X�@�\��I/F��񋟂���
    '    �i�{MDL�g�p�O��j  UtilDBIf�C���^�[�t�F�[�X����������������DB�A�N�Z�X�N���X��
    '                       �v���W�F�N�g�Ɏ�荞��ł��邱��
    '    �i���l�j           UtilDBIf�C���^�[�t�F�[�X���`
    '
    '===============================================================================
    '  ����  ���O          ��  �t      �}�[�N      ���e
    '-------------------------------------------------------------------------------
    '  (1)   Laevigata, Inc.    2006/05/10              �V�K
    '  (2)   Laevigata, Inc.    2006/06/16              �u���p�����[�^�N�G���Ή�
    '  (3)   Laevigata, Inc.    2010/08/26              SystemInfo�e�[�u������̎擾�ɑΉ�
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' Util.DB�ɂ��DB�A�N�Z�X�@�\��I/F��񋟂���
    ''' </summary>
    ''' <remarks>�{I/F���������Ă���Util.DB��DB�A�N�Z�X�N���X�̃C���^�[�t�F�[�X��񋟂���B</remarks>
    Public Interface UtilDBIf

        'SystemInfo�\����
        Structure sysinfoRec                    '2010.08.26 add by Laevigata, Inc. #SystemInfo
            Public fixKey As String
            Public variableKey As String
            Public stringValue1 As String
            Public stringValue2 As String
            Public stringValue3 As String
            Public numericValue1 As String      '�{�����l�^����NULL�̎�舵�����l�����ĈӐ}�I�ɕ����^�Ƃ���
            Public numericValue2 As String      '�{�����l�^����NULL�̎�舵�����l�����ĈӐ}�I�ɕ����^�Ƃ���
            Public numericValue3 As String      '�{�����l�^����NULL�̎�舵�����l�����ĈӐ}�I�ɕ����^�Ƃ���
            Public dateValue1 As String         '�{�����t�^����NULL�̎�舵�����l�����ĈӐ}�I�ɕ����^�Ƃ���
            Public dateValue2 As String         '�{�����t�^����NULL�̎�舵�����l�����ĈӐ}�I�ɕ����^�Ƃ���
            Public dateValue3 As String         '�{�����t�^����NULL�̎�舵�����l�����ĈӐ}�I�ɕ����^�Ƃ���
            Public UpdDate As String            '�{�����t�^����NULL�̎�舵�����l�����ĈӐ}�I�ɕ����^�Ƃ���
            Public Memo As String
        End Structure

        '===============================================================================
        '�v���p�e�B(�A�N�Z�T)
        '===============================================================================
        ''' <summary>
        ''' �g�����U�N�V���������ǂ���������
        ''' </summary>
        ''' <value>�Ȃ�</value>
        ''' <returns>True/False</returns>
        ''' <remarks>�g�����U�N�V�������J�n����Ă���ꍇ��True��߂��B</remarks>
        ReadOnly Property isTransactionOpen() As Boolean

        '-------------------------------------------------------------------------------
        '   DB�ؒf
        '   �i�����T�v�jDB�ڑ����N���[�Y����
        '   �����̓p�����^  �F�Ȃ�
        '   �����\�b�h�߂�l�F�Ȃ�
        '                                               2006.05.23 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' DB�ؒf
        ''' </summary>
        ''' <remarks>DB�R�l�N�V��������A�R�l�N�V�����I�u�W�F�N�g��j������B</remarks>
        Sub close()

        '-------------------------------------------------------------------------------
        '   �g�����U�N�V�����J�n
        '   �i�����T�v)�@�g�����U�N�V�������J�n����
        '   �����̓p�����^  �F�Ȃ�
        '   �����\�b�h�߂�l�F�Ȃ�
        '                                               2006.05.23 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �g�����U�N�V�����J�n
        ''' </summary>
        ''' <remarks>�g�����U�N�V�������J�n����B</remarks>
        Sub beginTran()

        '-------------------------------------------------------------------------------
        '   �g�����U�N�V�����I��
        '   �i�����T�v)�@�g�����U�N�V������Commit����
        '   �����̓p�����^  �F�Ȃ�
        '   �����\�b�h�߂�l�F�Ȃ�
        '                                               2006.05.23 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �g�����U�N�V�����m��
        ''' </summary>
        ''' <remarks>Commit�𔭍s���A�g�����U�N�V���������B</remarks>
        Sub commitTran()

        '-------------------------------------------------------------------------------
        '   �g�����U�N�V�����j��
        '   �i�����T�v)�@�g�����U�N�V������Rollback����
        '   �����̓p�����^  �F�Ȃ�
        '   �����\�b�h�߂�l�F�Ȃ�
        '                                               2006.05.23 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �g�����U�N�V�����j��
        ''' </summary>
        ''' <remarks>RollBack�𔭍s���A�g�����U�N�V���������B</remarks>
        Sub rollbackTran()

        '-------------------------------------------------------------------------------
        '   Select�����s
        '   �i�����T�v�jSelect���𔭍s���ADataSet��ԋp����
        '   �����̓p�����^  �FprmSQL        Select��
        '                  �FprmTblName     �ԋp�����DataSet��TBL����
        '                  �F<prmRefRecCnt> �擾����
        '   �����\�b�h�߂�l�FDataSet
        '   �����l          �F�ԋp����DataSet��prmTblName��TBL���̂Ŋi�[
        '                                               2006.05.23 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' SELECT���𔭍s����
        ''' </summary>
        ''' <param name="prmSQL">���s����SELECT��</param>
        ''' <param name="prmTblName">�ԋp�����DataSet��TABLE����</param>
        ''' <param name="prmRefRecCnt">�ȗ��\�FSELECT���̎擾���R�[�h����</param>
        ''' <returns>�擾�������R�[�h�Z�b�g��DataSet�I�u�W�F�N�g�Ƃ��ĕԋp</returns>
        ''' <remarks>SELECT���𔭍s���A���R�[�h�Z�b�g���擾����B�擾�������R�[�h�Z�b�g��DataSet�I�u�W�F�N�g�Ƃ��ĕԋp����B</remarks>
        Function selectDB(ByVal prmSQL As String, ByVal prmTblName As String, Optional ByRef prmRefRecCnt As Integer = 0) As DataSet

        '-------------------------------------------------------------------------------
        '   Select�����s
        '   �i�����T�v�j�u���p�����[�^�t��Select���𔭍s���ADataSet��ԋp����
        '   �����̓p�����^  �FprmSQL            �p�����[�^�t��Select��(�u���p�����^�́u?�v)
        '                   �FprmParameters     �u���p�����[�^���X�g
        '                   �FprmTblName        �ԋp�����DataSet��TBL����
        '                   �F<prmRefRecCnt>    �擾����
        '   �����\�b�h�߂�l�FDataSet
        '   �����l          �F�ԋp����DataSet��prmTblName��TBL���̂Ŋi�[
        '   ���g�p��
        '                     Dim rtnCnt As Integer = 0
        '                     Dim listPrm As List(Of UtilDBPrm) = New List(Of UtilDBPrm)
        '                         listPrm.Add(New UtilDBPrm(1, , UtilDBPrm.parameterType.tNumber))
        '                         listPrm.Add(New UtilDBPrm(4, , UtilDBPrm.parameterType.tNumber))
        '                     Dim ds As DataSet = _db.selectDB("select * from test where col2 in (?,?)", listPrm, "RS", rtnCnt)
        '                         If rtnCnt > 0 Then
        '                             With ds.Tables("RS")
        '                                 For i As Integer = 0 To rtnCnt - 1
        '                                     Debug.WriteLine(.Rows(i)("col1") & "|" & .Rows(i)("col2") & "|" & .Rows(i)("col3"))
        '                                 Next
        '                             End With
        '                         End If
        '                                               2006.06.16 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �u���p�����[�^�t��SELECT���𔭍s���� (�ڍׂ͎g�p��Q��)
        ''' </summary>
        ''' <param name="prmSQL">�p�����[�^�t��Select��(�u���p�����^�́u?�v)</param>
        ''' <param name="prmParameters">�u���p�����[�^���X�g</param>
        ''' <param name="prmTblName">�ԋp�����DataSet��TABLE����</param>
        ''' <param name="prmRefRecCnt">�ȗ��\�FSELECT���̎擾���R�[�h����</param>
        ''' <returns>�擾�������R�[�h�Z�b�g��DataSet�I�u�W�F�N�g�Ƃ��ĕԋp</returns>
        ''' <remarks>SELECT���𔭍s���A���R�[�h�Z�b�g���擾����B�擾�������R�[�h�Z�b�g��DataSet�I�u�W�F�N�g�Ƃ��ĕԋp����B</remarks>
        Function selectDB(ByVal prmSQL As String,
                          ByVal prmParameters As List(Of UtilDBPrm),
                          ByVal prmTblName As String,
                          Optional ByRef prmRefRecCnt As Integer = 0) As DataSet

        '-------------------------------------------------------------------------------
        '   �X�VSQL�����s
        '   �i�����T�v�jInsert/Update/Delete���𔭍s����
        '   �����̓p�����^  �FprmSQL        SQL��
        '   �����\�b�h�߂�l�F�Ȃ�
        '                                               2006.05.23 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' ���s�nSQL�𔭍s����
        ''' </summary>
        ''' <param name="prmSQL">���s����SQL��</param>
        ''' <remarks>���R�[�h�Z�b�g�𐶐����Ȃ�SQL(INSERT/UPDATE/DELETE�cetc)�𔭍s����B</remarks>
        Sub executeDB(ByVal prmSQL As String)

        '-------------------------------------------------------------------------------
        '   �X�VSQL�����s
        '   �i�����T�v�jInsert/Update/Delete���𔭍s����
        '   �����̓p�����^  �FprmSQL                SQL��
        '                   �FprmRefAffectedRows    �e�����󂯂��s��
        '   �����\�b�h�߂�l�F�Ȃ�
        '                                               2006.06.23 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' ���s�nSQL�𔭍s����i�e�����������t���j
        ''' </summary>
        ''' <param name="prmSQL">���s����SQL��</param>
        ''' <param name="prmRefAffectedRows">�e�����󂯂��s��</param>
        ''' <remarks>���R�[�h�Z�b�g�𐶐����Ȃ�SQL(INSERT/UPDATE/DELETE�cetc)�𔭍s����B</remarks>
        Sub executeDB(ByVal prmSQL As String, ByRef prmRefAffectedRows As Integer)

        '-------------------------------------------------------------------------------
        '   �X�VSQL�����s
        '   �i�����T�v�j�u���p�����[�^�t�����s�nSQL�𔭍s����
        '   �����̓p�����^  �FprmSQL        SQL��
        '                   �FprmParameters �u���p�����[�^���X�g
        '   �����\�b�h�߂�l�F�Ȃ�
        '   ���g�p��
        '                     '�p�����^�ݒ�
        '                     Dim listPrm As List(Of UtilDBPrm) = New List(Of UtilDBPrm)
        '                     listPrm.Add(New UtilDBPrm(Nothing, 255, UtilDBPrm.parameterType.tVarchar, UtilDBPrm.parameterDirection.dReturn)) '�߂�l
        '                     listPrm.Add(New UtilDBPrm(10, , UtilDBPrm.parameterType.tNumber, UtilDBPrm.parameterDirection.dInput))           '�@
        '                     listPrm.Add(New UtilDBPrm(Nothing, , UtilDBPrm.parameterType.tNumber, UtilDBPrm.parameterDirection.dOutput))     '�A
        '                     listPrm.Add(New UtilDBPrm(30, , UtilDBPrm.parameterType.tNumber, UtilDBPrm.parameterDirection.dInputOutput))     '�B
        '                     listPrm.Add(New UtilDBPrm(Nothing, , UtilDBPrm.parameterType.tDate, UtilDBPrm.parameterDirection.dOutput))       '�C
        '                     listPrm.Add(New UtilDBPrm("�u���p�����^�N�G�����s�e�X�g", _
        '                                                       14, UtilDBPrm.parameterType.tVarchar, UtilDBPrm.parameterDirection.dInput))    '�D
        '                     '���s
        '                     _db.executeDB("BEGIN ? := TESTFUNC(?,?,?,?,?); END;", listPrm)
        '
        '                     '���ʊm�F
        '                     Debug.WriteLine("�߂�l=" & listPrm(0).value)
        '                     Debug.WriteLine("prm1  =" & listPrm(1).value)
        '                     Debug.WriteLine("prm2  =" & listPrm(2).value)
        '                     Debug.WriteLine("prm3  =" & listPrm(3).value)
        '                     Debug.WriteLine("prm4  =" & listPrm(4).value)
        '                     Debug.WriteLine("prm5  =" & listPrm(5).value)
        '
        '
        '                     ===���s�X�g�A�h==========================
        '                     CREATE OR REPLACE FUNCTION TESTFUNC(
        '                     	 INPRM 		IN		NUMBER
        '                     	,OUTPRM		OUT		NUMBER
        '                     	,INOUTPRM	IN	OUT	NUMBER
        '                     	,DTPRM		OUT		DATE
        '                     	,VCPRM		IN		VARCHAR
        '                     )
        '                     RETURN VARCHAR2 
        '                     IS
        '                     	WK	DATE;
        '                     BEGIN
        '                         INOUTPRM := INOUTPRM * 2;             --INOUTPRM���Q�{
        '                         OUTPRM := INPRM + 1;                  --INPRM�ɂP��������OUTPRM�ɐݒ�
        '                         SELECT SYSDATE INTO DTPRM FROM DUAL;  --DTPRM�ɃV�X�e�����t��ݒ�
        '                         RETURN VCPRM || '�����s���܂����B';   --�߂�l��VCPRM�{����ݒ�
        '                     END;
        '                     /
        '                     =========================================
        '                                               2006.06.16 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �u���p�����[�^�t�����s�nSQL�𔭍s���� (�ڍׂ͎g�p��Q��)
        ''' </summary>
        ''' <param name="prmSQL">�p�����[�^�t��SQL��</param>
        ''' <param name="prmRefParameters">�u���p�����[�^���X�g</param>
        ''' <remarks>�X�g�A�h���s�Ȃǂ�z��(����ȊO�����s�\)</remarks>
        Sub executeDB(ByVal prmSQL As String, ByRef prmRefParameters As List(Of UtilDBPrm))

        '-------------------------------------------------------------------------------
        '   �V���O���N�H�[�g������
        '   �i�����T�v�j�V���O���N�H�[�g���u''�v�ɒu�����ĕԋp
        '   �����̓p�����^  �FprmSQL     Select��
        '   �����\�b�h�߂�l�F�u����SQL������
        '                                               2006.05.23 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �V���O���N�H�[�e�[�V����������
        ''' </summary>
        ''' <param name="prmSQL">�u���Ώ�SQL������</param>
        ''' <returns>�u�����SQL������</returns>
        ''' <remarks>�V���O���N�H�[�e�[�V�����𕶎��񉻂��ASQL�C���W�F�N�V�����΍􂨂�уf�[�^�o�^�\�Ƃ���B</remarks>
        Function rmSQ(ByVal prmSQL As String) As String

        '-------------------------------------------------------------------------------
        '   Null�˕�����
        '                                               2006.05.23 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' Null�̕�����u��
        ''' </summary>
        ''' <param name="prmField">�t�B�[���h�l</param>
        ''' <returns>�u���㕶����</returns>
        ''' <remarks>Null�̃t�B�[���h�l��""(����0������)�֒u������B</remarks>
        Function rmNullStr(ByVal prmField As Object) As String

        '-------------------------------------------------------------------------------
        '   Null��Short
        '                                               2006.05.23 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' Null�̐��l�u��(Short�^)
        ''' </summary>
        ''' <param name="prmField">�t�B�[���h�l</param>
        ''' <returns>�u���㐔�l</returns>
        ''' <remarks>Null�̃t�B�[���h�l��Short�^���l�֒u������B</remarks>
        Function rmNullShort(ByVal prmField As Object) As Short

        '-------------------------------------------------------------------------------
        '   Null��Integer
        '                                               2006.05.23 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' Null�̐��l�u��(Integer�^)
        ''' </summary>
        ''' <param name="prmField">�t�B�[���h�l</param>
        ''' <returns>�u���㐔�l</returns>
        ''' <remarks>Null�̃t�B�[���h�l��Integer�^���l�֒u������B</remarks>
        Function rmNullInt(ByVal prmField As Object) As Integer

        '-------------------------------------------------------------------------------
        '   Null��Long
        '                                               2006.05.23 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' Null�̐��l�u��(Long�^)
        ''' </summary>
        ''' <param name="prmField">�t�B�[���h�l</param>
        ''' <returns>�u���㐔�l</returns>
        ''' <remarks>Null�̃t�B�[���h�l��Long�^���l�֒u������B</remarks>
        Function rmNullLong(ByVal prmField As Object) As Long

        '-------------------------------------------------------------------------------
        '   Null��Double
        '                                               2006.05.23 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' Null�̐��l�u��(Double�^)
        ''' </summary>
        ''' <param name="prmField">�t�B�[���h�l</param>
        ''' <returns>�u���㐔�l</returns>
        ''' <remarks>Null�̃t�B�[���h�l��Double�^���l�֒u������B</remarks>
        Function rmNullDouble(ByVal prmField As Object) As Double

        '-------------------------------------------------------------------------------
        '   Null�˓��t������l
        '                                               2006.05.23 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' Null�̓��t������u��
        ''' </summary>
        ''' <param name="prmField">�t�B�[���h�l</param>
        ''' <param name="prmFormatStr">���t����</param>
        ''' <returns>�u������t������</returns>
        ''' <remarks>Null�̃t�B�[���h�l����t�t�H�[�}�b�g�̕�����֒u������B</remarks>
        Function rmNullDate(ByVal prmField As Object,
                            Optional ByVal prmFormatStr As String = "yyyy/MM/dd HH:mm:ss"
                            ) As String
    End Interface




    '===============================================================================
    '
    '  ���[�e�B���e�B�N���X
    '    �i�N���X���j    UtilDBPrm
    '    �i�����@�\���j     Util.DB�ɂ��DB�A�N�Z�X�ɂ����āA�u���p�����^�N�G���̃f�[�^�g���
    '    �i�{MDL�g�p�O��j  UtilDBIf�C���^�[�t�F�[�X����������������DB�A�N�Z�X�N���X��
    '                       �v���W�F�N�g�Ɏ�荞��ł��邱��
    '    �i���l�j           
    '
    '===============================================================================
    '  ����  ���O          ��  �t      �}�[�N      ���e
    '-------------------------------------------------------------------------------
    '  (1)   Laevigata, Inc.    2006/06/16              �V�K
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' Util.DB�ɂ��DB�A�N�Z�X�ɂ����āA�u���p�����^�N�G���̃f�[�^�g���
    ''' </summary>
    ''' <remarks></remarks>
    Public Class UtilDBPrm

        '===============================================================================
        '�񋓑̒�`
        '===============================================================================
        ''' <summary>
        ''' �p�����[�^�^�C�v(�^)
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum parameterType As Short
            ''' <summary>
            ''' �_���l
            ''' </summary>
            ''' <remarks></remarks>
            tBoolean = 0
            ''' <summary>
            ''' �ϒ�������l
            ''' </summary>
            ''' <remarks></remarks>
            tVarchar = 1
            ''' <summary>
            ''' ���t�l
            ''' </summary>
            ''' <remarks></remarks>
            tDate = 2
            ''' <summary>
            ''' ���l
            ''' </summary>
            ''' <remarks></remarks>
            tNumber = 3
        End Enum

        ''' <summary>
        ''' �p�����[�^�f�B���N�V����(I/O)
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum parameterDirection As Short
            ''' <summary>
            ''' IN�p�����[�^
            ''' </summary>
            ''' <remarks></remarks>
            dInput = 0
            ''' <summary>
            ''' OUT�p�����[�^
            ''' </summary>
            ''' <remarks></remarks>
            dOutput = 1
            ''' <summary>
            ''' IN/OUT�p�����[�^
            ''' </summary>
            ''' <remarks></remarks>
            dInputOutput = 2
            ''' <summary>
            ''' �߂�l
            ''' </summary>
            ''' <remarks></remarks>
            dReturn = 3
        End Enum

        '===============================================================================
        '�����o�[�ϐ���`
        '===============================================================================
        Private _value As Object                                            '�l
        Private _type As parameterType                                      '�^
        Private _direction As parameterDirection                            '����
        Private _size As Short                                              '�T�C�Y
        Private _refParameter As System.Data.Common.DbParameter = Nothing   '�ݒ��̃p�����^�|�C���^(���s��̌���[�p�����^�l]�擾�Ɏg�p)

        '===============================================================================
        '�v���p�e�B(�A�N�Z�T)
        '===============================================================================
        ''' <summary>
        ''' �l
        ''' </summary>
        ''' <returns>�p�����[�^�l</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property value() As Object
            Get
                If _refParameter Is Nothing Then
                    '�p�����[�^�ݒ�O
                    Select Case _type
                        Case parameterType.tBoolean
                            Return CBool(_value)
                        Case parameterType.tDate
                            Return CDate(_value)
                        Case parameterType.tNumber
                            Return CDec(_value)
                        Case parameterType.tVarchar
                            Return CStr(_value)
                        Case Else
                            Return Nothing
                    End Select
                Else
                    '�p�����[�^�ݒ��
                    Select Case _type
                        Case parameterType.tBoolean
                            Return CBool(_refParameter.Value)
                        Case parameterType.tDate
                            Return CDate(_refParameter.Value)
                        Case parameterType.tNumber
                            Return CDec(_refParameter.Value)
                        Case parameterType.tVarchar
                            Return CStr(_refParameter.Value)
                        Case Else
                            Return Nothing
                    End Select
                End If
            End Get
        End Property
        ''' <summary>
        ''' �^�C�v(�^)
        ''' </summary>
        ''' <returns>�p�����[�^�^�C�v</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property type() As parameterType
            Get
                Return _type
            End Get
        End Property
        ''' <summary>
        ''' �f�B���N�V����(I/O)[UtilMDL.DB.UtilDBPrm.parameterDirection�Ƃ��ĕԋp]
        ''' </summary>
        ''' <returns>�p�����[�^�f�B���N�V����(����)</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property direction() As parameterDirection
            Get
                Return _direction
            End Get
        End Property
        ''' <summary>
        ''' �f�B���N�V����(I/O)[System.Data.ParameterDirection�Ƃ��ĕԋp]
        ''' </summary>
        ''' <returns>�p�����[�^�f�B���N�V����(����)</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property systemDirection() As System.Data.ParameterDirection
            Get
                Dim ret As System.Data.ParameterDirection
                Select Case _direction
                    Case parameterDirection.dInput
                        ret = Data.ParameterDirection.Input
                    Case parameterDirection.dInputOutput
                        ret = Data.ParameterDirection.InputOutput
                    Case parameterDirection.dOutput
                        ret = Data.ParameterDirection.Output
                    Case parameterDirection.dReturn
                        ret = Data.ParameterDirection.ReturnValue
                    Case Else
                        ret = Nothing
                End Select
                Return ret
            End Get
        End Property
        ''' <summary>
        ''' �T�C�Y
        ''' </summary>
        ''' <returns>�p�����[�^�T�C�Y</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property size() As Short
            Get
                Return _size
            End Get
        End Property
        ''' <summary>
        ''' �ݒ��̃p�����^�|�C���^(���s��̌���[�p�����^�l]�擾�Ɏg�p)
        ''' </summary>
        ''' <value>�|�C���^</value>
        ''' <remarks></remarks>
        Public WriteOnly Property refParameter() As System.Data.Common.DbParameter
            Set(ByVal value As System.Data.Common.DbParameter)
                _refParameter = value
            End Set
        End Property

        '===============================================================================
        ' �R���X�g���N�^
        '   �����̓p�����^   �F prmValue        �p�����[�^�l
        '                       prmSize         �T�C�Y
        '                       prmType         �p�����[�^�^�C�v(�^)
        '                       prmDirection    �p�����[�^�f�B���N�V����(I/O)
        '                                               2006.06.19 Created By Laevigata, Inc.
        '===============================================================================
        ''' <summary>
        ''' �R���X�g���N�^
        ''' </summary>
        ''' <param name="prmValue">�p�����[�^�l</param>
        ''' <param name="prmSize">�T�C�Y</param>
        ''' <param name="prmType">�p�����[�^�^�C�v(�^)</param>
        ''' <param name="prmDirection">�p�����[�^�f�B���N�V����(I/O)</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal prmValue As Object, _
                               Optional ByVal prmSize As Short = 0, _
                               Optional ByVal prmType As parameterType = parameterType.tVarchar, _
                               Optional ByVal prmDirection As parameterDirection = parameterDirection.dInput)

            '�p�����^�`�F�b�N
            If (prmType <> parameterType.tBoolean And _
                prmType <> parameterType.tVarchar And _
                prmType <> parameterType.tDate And _
                prmType <> parameterType.tNumber) Then
                Throw New UsrDefException("�u���p�����[�^�̐ݒ肪����Ă��܂��B" & ControlChars.NewLine & _
                                          "prmType�ɂ�parameterType�萔���g�p���ĉ������B")
            End If
            If (prmDirection <> parameterDirection.dInput And _
                prmDirection <> parameterDirection.dOutput And _
                prmDirection <> parameterDirection.dInputOutput And _
                prmDirection <> parameterDirection.dReturn) Then
                Throw New UsrDefException("�u���p�����[�^�̐ݒ肪����Ă��܂��B" & ControlChars.NewLine & _
                                          "prmDirection�ɂ�parameterDirection�萔���g�p���ĉ������B")
            End If
            If prmType = parameterType.tVarchar And _
               prmSize = 0 Then
                Throw New UsrDefException("�u���p�����[�^�̐ݒ肪����Ă��܂��B" & ControlChars.NewLine & _
                                          "prmType��parameterType.tVarchar�萔���g�p���Ă���ꍇ�A" & ControlChars.NewLine & _
                                          "������prmSize���w�肵�ĉ������B")
            End If
            _value = prmValue
            _type = prmType
            _direction = prmDirection
            _size = prmSize

        End Sub

    End Class

End Namespace
