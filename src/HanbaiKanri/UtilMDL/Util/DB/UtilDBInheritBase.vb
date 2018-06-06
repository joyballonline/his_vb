Namespace DB
    '===============================================================================
    '
    '  ���[�e�B���e�B�N���X
    '    �i�N���X���j    UtilDBInheritBase
    '    �i�����@�\���j     UtilDBIf�ɂ��DB�A�N�Z�X�@�\��񋟂���
    '    �i�{MDL�g�p�O��j  �{�N���X�͌p�����Ƃ��邱�Ƃ�O��Ƃ��邽�߁A
    '                       �p�����ăT�u�N���X���`���邱�ƁB
    '    �i���l�j           �EUtilDBIf�C���^�[�t�F�[�X������
    '                       �E�{�N���X�̃C���X�^���X���͍s���Ȃ�
    '                           �˃T�u�N���X���C���X�^���X�����邱��
    '                       �EMustOverride�ȃ����o�[��Overrides���邱��
    '
    '===============================================================================
    '  ����  ���O          ��  �t      �}�[�N      ���e
    '-------------------------------------------------------------------------------
    '  (1)   Jun.Takagi    2006/05/23              �V�K
    '  (2)   Jun.Takagi    2010/08/26              SystemInfo�e�[�u������̎擾�ɑΉ�
    '-------------------------------------------------------------------------------
    Public MustInherit Class UtilDBInheritBase
        Implements UtilDBIf
        '===============================================================================
        '�����o�[�ϐ���`
        '===============================================================================
        '�Ȃ�


        '===============================================================================
        '�v���p�e�B(�A�N�Z�T)
        '===============================================================================

        '�g�����U�N�V�������J���Ă��邩�ǂ����̃X�e�[�^�X��߂�
        ''' <summary>
        ''' �g�����U�N�V�������J���Ă��邩�ǂ����̃X�e�[�^�X��߂�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public MustOverride ReadOnly Property isTransactionOpen() As Boolean Implements UtilDBIf.isTransactionOpen

        '===============================================================================
        ' �R���X�g���N�^
        '===============================================================================
        '���ۃN���X�ł���ב��݂��Ȃ�

        '-------------------------------------------------------------------------------
        '   DB�ؒf
        '   �i�����T�v�jDB�ڑ����N���[�Y����
        '   �����̓p�����^  �F�Ȃ�
        '   �����\�b�h�߂�l�F�Ȃ�
        '                                               2006.05.23 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' DB�ڑ����N���[�Y����
        ''' </summary>
        ''' <remarks></remarks>
        Public MustOverride Sub close() Implements UtilDBIf.close

        '-------------------------------------------------------------------------------
        '   �g�����U�N�V�����J�n
        '   �i�����T�v)�@�g�����U�N�V�������J�n����
        '   �����̓p�����^  �F�Ȃ�
        '   �����\�b�h�߂�l�F�Ȃ�
        '                                               2006.05.23 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �g�����U�N�V�������J�n����
        ''' </summary>
        ''' <remarks></remarks>
        Public MustOverride Sub beginTran() Implements UtilDBIf.beginTran

        '-------------------------------------------------------------------------------
        '   �g�����U�N�V�����I��
        '   �i�����T�v)�@�g�����U�N�V������Commit����
        '   �����̓p�����^  �F�Ȃ�
        '   �����\�b�h�߂�l�F�Ȃ�
        '                                               2006.05.23 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �g�����U�N�V������Commit����
        ''' </summary>
        ''' <remarks></remarks>
        Public MustOverride Sub commitTran() Implements UtilDBIf.commitTran

        '-------------------------------------------------------------------------------
        '   �g�����U�N�V�����j��
        '   �i�����T�v)�@�g�����U�N�V������Rollback����
        '   �����̓p�����^  �F�Ȃ�
        '   �����\�b�h�߂�l�F�Ȃ�
        '                                               2006.05.23 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �g�����U�N�V������Rollback����
        ''' </summary>
        ''' <remarks></remarks>
        Public MustOverride Sub rollbackTran() Implements UtilDBIf.rollbackTran

        '-------------------------------------------------------------------------------
        '   Select�����s
        '   �i�����T�v�jSelect���𔭍s���ADataSet��ԋp����
        '   �����̓p�����^  �FprmSQL        Select��
        '                  �FprmTblName     �ԋp�����DataSet��TBL����
        '                  �F<prmRefRecCnt> �擾����
        '   �����\�b�h�߂�l�FDataSet
        '   �����l          �F�ԋp����DataSet��prmTblName��TBL���̂Ŋi�[
        '                                               2006.05.23 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' Select���𔭍s���ADataSet��ԋp����
        ''' </summary>
        ''' <param name="prmSQL">Select��</param>
        ''' <param name="prmTblName">�ԋp�����DataSet��TBL����</param>
        ''' <param name="prmRefRecCnt">�擾����</param>
        ''' <returns>DataSet</returns>
        ''' <remarks></remarks>
        Public MustOverride Function selectDB(ByVal prmSQL As String, _
                                              ByVal prmTblName As String, _
                                              Optional ByRef prmRefRecCnt As Integer = 0) _
                                                                           As System.Data.DataSet _
                                                                           Implements UtilDBIf.selectDB

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
        '                                               2006.06.16 Created By Jun.Takagi
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
        Public MustOverride Function selectDB(ByVal prmSQL As String, _
                                              ByVal prmParameters As List(Of UtilDBPrm), _
                                              ByVal prmTblName As String, _
                                              Optional ByRef prmRefRecCnt As Integer = 0) _
                                                                             As System.Data.DataSet _
                                                                             Implements UtilDBIf.selectDB

        '-------------------------------------------------------------------------------
        '   �X�VSQL�����s
        '   �i�����T�v�jInsert/Update/Delete���𔭍s����
        '   �����̓p�����^  �FprmSQL        SQL��
        '   �����\�b�h�߂�l�F�Ȃ�
        '                                               2006.05.23 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' Insert/Update/Delete���𔭍s����
        ''' </summary>
        ''' <param name="prmSQL">SQL��</param>
        ''' <remarks></remarks>
        Public MustOverride Sub executeDB(ByVal prmSQL As String) Implements UtilDBIf.executeDB

        '-------------------------------------------------------------------------------
        '   �X�VSQL�����s
        '   �i�����T�v�jInsert/Update/Delete���𔭍s����
        '   �����̓p�����^  �FprmSQL                SQL��
        '                   �FprmRefAffectedRows    �e�����󂯂��s��
        '   �����\�b�h�߂�l�F�Ȃ�
        '                                               2006.06.23 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' ���s�nSQL�𔭍s����i�e�����������t���j
        ''' </summary>
        ''' <param name="prmSQL">���s����SQL��</param>
        ''' <param name="prmRefAffectedRows">�e�����󂯂��s��</param>
        ''' <remarks>���R�[�h�Z�b�g�𐶐����Ȃ�SQL(INSERT/UPDATE/DELETE�cetc)�𔭍s����B</remarks>
        Public MustOverride Sub executeDB(ByVal prmSQL As String, ByRef prmRefAffectedRows As Integer) _
                                                                           Implements UtilDBIf.executeDB

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
        '                                               2006.06.16 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �u���p�����[�^�t�����s�nSQL�𔭍s���� (�ڍׂ͎g�p��Q��)
        ''' </summary>
        ''' <param name="prmSQL">�p�����[�^�t��SQL��</param>
        ''' <param name="prmRefParameters">�u���p�����[�^���X�g</param>
        ''' <remarks>�X�g�A�h���s�Ȃǂ�z��(����ȊO�����s�\)</remarks>
        Public MustOverride Sub executeDB(ByVal prmSQL As String, _
                                          ByRef prmRefParameters As List(Of UtilDBPrm)) _
                                                                   Implements UtilDBIf.executeDB

        '-------------------------------------------------------------------------------
        '   �V���O���N�H�[�g������
        '   �i�����T�v�j�V���O���N�H�[�g���u''�v�ɒu�����ĕԋp
        '   �����̓p�����^  �FprmSQL     Select��
        '   �����\�b�h�߂�l�F�u����SQL������
        '                                               2006.05.23 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �V���O���N�H�[�g������ �V���O���N�H�[�g���u''�v�ɒu�����ĕԋp
        ''' </summary>
        ''' <param name="prmSQL">Select��</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shadows Function rmSQ(ByVal prmSQL As String) As String Implements UtilDBIf.rmSQ
            Return prmSQL.Replace("'", "''")
        End Function

        '-------------------------------------------------------------------------------
        '   Null�˕�����
        '                                               2006.05.23 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' Null�˕�����
        ''' </summary>
        ''' <param name="prmField"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function rmNullStr(ByVal prmField As Object) As String Implements UtilDBIf.rmNullStr
            Dim ret As String = ""
            Try
                If prmField Is Nothing Then
                ElseIf IsDBNull(prmField) Then
                Else
                    ret = CStr(prmField)
                End If
            Catch ex As Exception
            End Try
            Return ret
        End Function

        '-------------------------------------------------------------------------------
        '   Null��Short
        '                                               2006.05.23 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' Null��Short
        ''' </summary>
        ''' <param name="prmField"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function rmNullShort(ByVal prmField As Object) As Short Implements UtilDBIf.rmNullShort
            Dim ret As Short = 0
            Try
                If prmField Is Nothing Then
                ElseIf IsDBNull(prmField) Then
                ElseIf Not IsNumeric(prmField) Then
                Else
                    ret = CShort(prmField)
                End If
            Catch ex As Exception
            End Try
            Return ret
        End Function

        '-------------------------------------------------------------------------------
        '   Null��Integer
        '                                               2006.05.23 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' Null��Integer
        ''' </summary>
        ''' <param name="prmField"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function rmNullInt(ByVal prmField As Object) As Integer Implements UtilDBIf.rmNullInt
            Dim ret As Integer = 0
            Try
                If prmField Is Nothing Then
                ElseIf IsDBNull(prmField) Then
                ElseIf Not IsNumeric(prmField) Then
                Else
                    ret = CInt(prmField)
                End If
            Catch ex As Exception
            End Try
            Return ret
        End Function

        '-------------------------------------------------------------------------------
        '   Null��Long
        '                                               2006.05.23 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' Null��Long
        ''' </summary>
        ''' <param name="prmField"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function rmNullLong(ByVal prmField As Object) As Long Implements UtilDBIf.rmNullLong
            Dim ret As Long = 0
            Try
                If prmField Is Nothing Then
                ElseIf IsDBNull(prmField) Then
                ElseIf Not IsNumeric(prmField) Then
                Else
                    ret = CLng(prmField)
                End If
            Catch ex As Exception
            End Try
            Return ret
        End Function

        '-------------------------------------------------------------------------------
        '   Null��Double
        '                                               2006.05.23 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' Null��Double
        ''' </summary>
        ''' <param name="prmField"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function rmNullDouble(ByVal prmField As Object) As Double Implements UtilDBIf.rmNullDouble
            Dim ret As Double = 0
            Try
                If prmField Is Nothing Then
                ElseIf IsDBNull(prmField) Then
                ElseIf Not IsNumeric(prmField) Then
                Else
                    ret = CDbl(prmField)
                End If
            Catch ex As Exception
            End Try
            Return ret
        End Function

        '-------------------------------------------------------------------------------
        '   Null�˓��t������l
        '                                               2006.05.23 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' Null�˓��t������l
        ''' </summary>
        ''' <param name="prmField"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function rmNullDate(ByVal prmField As Object, _
                                   Optional ByVal prmFormatStr As String = "yyyy/MM/dd HH:mm:ss" _
                                   ) As String Implements UtilDBIf.rmNullDate
            Dim ret As String = ""
            Try
                If prmField Is Nothing Then
                ElseIf IsDBNull(prmField) Then
                ElseIf Not IsDate(prmField) Then
                Else
                    ret = (CDate(prmField)).ToString(prmFormatStr)
                End If
            Catch ex As Exception
            End Try
            Return ret
        End Function
    End Class

End Namespace
