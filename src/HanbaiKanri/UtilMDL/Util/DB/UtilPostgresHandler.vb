Imports Npgsql
Imports NpgsqlTypes

Namespace DB
    '===============================================================================
    '
    '  ���[�e�B���e�B�N���X
    '    �i�N���X���j    UtilPostgresHandler
    '    �i�����@�\���j     Npgsql�ɂ��DB�A�N�Z�X�@�\��񋟂���
    '    �i�{MDL�g�p�O��j  UtilDBInheritBase/UtilDBIf���v���W�F�N�g�Ɏ�荞��ł��邱��
    '                       Npgsql.dll���Q�Ɛݒ肵�Ă��邱��
    '    �i���l�j           UtilDBInheritBase���p��
    '                       UtilDBIf�C���^�[�t�F�[�X��(UtilDBInheritBase�ɂ�)����
    '
    '===============================================================================
    '  ����  ���O          ��  �t      �}�[�N      ���e
    '-------------------------------------------------------------------------------
    '  (1)   Laevigata, Inc.    2014/02/20              �V�K
    '-------------------------------------------------------------------------------
    Public Class UtilPostgresHandler
        Inherits UtilDBInheritBase

        '===============================================================================
        '�����o�[�ϐ���`
        '===============================================================================
        Private _con As NpgsqlConnection    '�R�l�N�V����
        Private _cmd As NpgsqlCommand       '�R�}���h
        Private _adp As NpgsqlDataAdapter   '�A�_�v�^
        Private _tran As NpgsqlTransaction  '�g�����U�N�V����
        Private _tranFlg As Boolean = False '�g�����U�N�V�������t���O

        '===============================================================================
        '�v���p�e�B(�A�N�Z�T)
        '===============================================================================
        Public ReadOnly Property con() As NpgsqlConnection
            'Geter--------
            Get
                con = _con
            End Get
            'Setter-------
            '�Ȃ�
        End Property
        Public Overrides ReadOnly Property isTransactionOpen() As Boolean
            'Geter--------
            Get
                isTransactionOpen = _tranFlg
            End Get
            'Setter-------
            '�Ȃ�
        End Property

        '===============================================================================
        ' �R���X�g���N�^
        '   �i�����T�v�j�p�����[�^���DB�R�l�N�V������V�K��������
        '   �����̓p�����^   �F  prmSvAdr      �T�[�o�[�A�h���X
        '                    �F  prmPortNo     �|�[�g�ԍ�
        '                    �F  prmDbNm       �f�[�^�x�[�X��
        '                    �F  prmUserId     ���[�U�[ID
        '                    �F  prmPswd       �p�X���[�h
        '                    �F  <prmTimeout>  ���sSQL�̃^�C���A�E�g�ݒ�(�ȗ����͊����30�b/0�ݒ莞�͉i�v�ҋ@)
        '                                               2014.02.20 Created By Laevigata, Inc.
        '===============================================================================
        ''' <summary>
        ''' �R���X�g���N�^ UDL�t�@�C�����DB�R�l�N�V������V�K��������
        ''' </summary>
        ''' <param name="prmSvAdr">�T�[�o�[�A�h���X</param>
        ''' <param name="prmPortNo">�|�[�g�ԍ�</param>
        ''' <param name="prmDbNm">�f�[�^�x�[�X��</param>
        ''' <param name="prmUserId">���[�U�[ID</param>
        ''' <param name="prmPswd">�p�X���[�h</param>
        ''' <param name="prmTimeout">���sSQL�̃^�C���A�E�g�ݒ�(�ȗ����͊����30�b/0�ݒ莞�͉i�v�ҋ@)</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal prmSvAdr As String, ByVal prmPortNo As String, ByVal prmDbNm As String, ByVal prmUserId As String, ByVal prmPswd As String, Optional ByVal prmTimeout As Short = -1)
            Try
                Call initInstance(prmSvAdr, prmPortNo, prmDbNm, prmUserId, prmPswd, prmTimeout)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        '===============================================================================
        ' �R���X�g���N�^
        '   �i�����T�v�j�T�u�N���X�p�̃R���X�g���N�^�B�C���X�^���X������initInstance���Ăяo������
        '===============================================================================
        ''' <summary>
        ''' �R���X�g���N�^ �T�u�N���X�p�̃R���X�g���N�^�B�C���X�^���X������initInstance���Ăяo������
        ''' </summary>
        ''' <remarks></remarks>
        Protected Sub New()
            '�����Ȃ�
        End Sub

        '===============================================================================
        ' �T�u���[�`��[�T�u�N���X�p]
        '   �i�����T�v�j�p�����[�^���DB�R�l�N�V������V�K��������
        '   �����̓p�����^   �F  prmSvAdr      �T�[�o�[�A�h���X
        '                    �F  prmPortNo     �|�[�g�ԍ�
        '                    �F  prmDbNm       �f�[�^�x�[�X��
        '                    �F  prmUserId     ���[�U�[ID
        '                    �F  prmPswd       �p�X���[�h
        '                    �F  <prmTimeout>  ���sSQL�̃^�C���A�E�g�ݒ�(�ȗ����͊����30�b/0�ݒ莞�͉i�v�ҋ@)
        '                                               2014.02.20 Created By Laevigata, Inc.
        '===============================================================================
        ''' <summary>
        ''' �T�u���[�`��[�T�u�N���X�p] UDL�t�@�C�����DB�R�l�N�V������V�K��������
        ''' </summary>
        ''' <param name="prmSvAdr">�T�[�o�[�A�h���X</param>
        ''' <param name="prmPortNo">�|�[�g�ԍ�</param>
        ''' <param name="prmDbNm">�f�[�^�x�[�X��</param>
        ''' <param name="prmUserId">���[�U�[ID</param>
        ''' <param name="prmPswd">�p�X���[�h</param>
        ''' <param name="prmTimeout">���sSQL�̃^�C���A�E�g�ݒ�(�ȗ����͊����30�b/0�ݒ莞�͉i�v�ҋ@)</param>
        ''' <remarks></remarks>
        Protected Sub initInstance(ByVal prmSvAdr As String, ByVal prmPortNo As String, ByVal prmDbNm As String, ByVal prmUserId As String, ByVal prmPswd As String, Optional ByVal prmTimeout As Short = -1)
            Try
                _con = New NpgsqlConnection()
                _con.ConnectionString = "Server=" & prmSvAdr & ";Port=" & prmPortNo & ";Database=" & prmDbNm & ";Encoding=UNICODE;User Id=" & prmUserId & ";Password=" & prmPswd & ";"
                Try
                    _con.Open()
                Catch lex As Exception
                    Throw New Exception("�f�[�^�x�[�X�̐ڑ��Ɏ��s���܂����B" & ControlChars.NewLine &
                                        ControlChars.Tab & "�@ " & lex.Message, lex)
                End Try


                _adp = New NpgsqlDataAdapter()
                _cmd = New NpgsqlCommand()
                _cmd.Connection = _con
                If prmTimeout > -1 Then 'Optional�����ȗ��̏ꍇ�͋K��l��30�b���K�p�����
                    _cmd.CommandTimeout = prmTimeout '0�̏ꍇ�̓^�C���A�E�g����(�i�v�ҋ@)�ƂȂ�̂Œ��ӂ̂��ƁI
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Debug.WriteLine(ex.StackTrace)
                Throw ex
            End Try

        End Sub

        '===============================================================================
        ' �f�X�g���N�^
        '   �i�����T�v�jDB�R�l�N�V�����̏I���������{
        '===============================================================================
        ''' <summary>
        ''' �f�X�g���N�^ DB�R�l�N�V�����̏I���������{
        ''' </summary>
        ''' <remarks></remarks>
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            Try
                If _con IsNot Nothing Then
                    If _con.State = ConnectionState.Open Then
                        _con.Close()
                    End If
                End If
            Catch ex As Exception
            Finally
                _con = Nothing
                _adp = Nothing
                _cmd = Nothing
            End Try

        End Sub

        '-------------------------------------------------------------------------------
        '   DB�ؒf
        '   �i�����T�v�jDB�ڑ����N���[�Y����
        '   �����̓p�����^  �F�Ȃ�
        '   �����\�b�h�߂�l�F�Ȃ�
        '                                               2014.02.20 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' DB�ؒf 
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub close()
            Try
                If _con.State = ConnectionState.Open Then
                    _con.Close()
                End If
            Catch ex As Exception
            Finally
                _con.Dispose()
            End Try

        End Sub

        '-------------------------------------------------------------------------------
        '   �g�����U�N�V�����J�n
        '   �i�����T�v)�@�g�����U�N�V�������J�n����
        '   �����̓p�����^  �F�Ȃ�
        '   �����\�b�h�߂�l�F�Ȃ�
        '                                               2014.02.20 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �g�����U�N�V�����J�n
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub beginTran()
            Try
                _tran = _con.BeginTransaction()
                _cmd.Transaction = _tran
                _tranFlg = True
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Debug.WriteLine(ex.StackTrace)
                Throw ex
            End Try
        End Sub

        '-------------------------------------------------------------------------------
        '   �g�����U�N�V�����I��
        '   �i�����T�v)�@�g�����U�N�V������Commit����
        '   �����̓p�����^  �F�Ȃ�
        '   �����\�b�h�߂�l�F�Ȃ�
        '                                               2014.02.20 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �g�����U�N�V�����I�� �g�����U�N�V������Commit����
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub commitTran()
            Try
                _tran.Commit()
                _tranFlg = False
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Debug.WriteLine(ex.StackTrace)
                Throw ex
            End Try
        End Sub

        '-------------------------------------------------------------------------------
        '   �g�����U�N�V�����j��
        '   �i�����T�v)�@�g�����U�N�V������Rollback����
        '   �����̓p�����^  �F�Ȃ�
        '   �����\�b�h�߂�l�F�Ȃ�
        '                                               2014.02.20 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �g�����U�N�V�����j�� �g�����U�N�V������Rollback����
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub rollbackTran()
            Try
                _tran.Rollback()
                _tranFlg = False
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Debug.WriteLine(ex.StackTrace)
                Throw ex
            End Try
        End Sub

        '-------------------------------------------------------------------------------
        '   Select�����s
        '   �i�����T�v�jSelect���𔭍s���ADataSet��ԋp����
        '   �����̓p�����^  �FprmSQL        Select��
        '                  �FprmTblName     �ԋp�����DataSet��TBL����
        '                  �F<prmRefRecCnt> �擾����
        '   �����\�b�h�߂�l�FDataSet
        '   �����l          �F�ԋp����DataSet��prmTblName��TBL���̂Ŋi�[
        '                                               2014.02.20 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' Select�����s Select���𔭍s���ADataSet��ԋp���� �ԋp����DataSet��prmTblName��TBL���̂Ŋi�[
        ''' </summary>
        ''' <param name="prmSQL">Select��</param>
        ''' <param name="prmTblName">�ԋp�����DataSet��TBL����</param>
        ''' <param name="prmRefRecCnt">�擾����</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overrides Function selectDB(ByVal prmSQL As String, _
                                           ByVal prmTblName As String, _
                                           Optional ByRef prmRefRecCnt As Integer = 0) As DataSet
            Try

                _cmd.CommandText = prmSQL
                _adp.SelectCommand = _cmd
                Dim ds As DataSet = New DataSet()

                'Select�����s
                Call _adp.Fill(ds, prmTblName)

                '�擾����
                prmRefRecCnt = ds.Tables(0).Rows.Count

                '�߂�l�ݒ�
                selectDB = ds

            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Debug.WriteLine(ex.StackTrace)
                Throw ex
            End Try

        End Function

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
        '                                               2014.02.20 Created By Laevigata, Inc.
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
        Public Overrides Function selectDB(ByVal prmSQL As String, _
                                               ByVal prmParameters As List(Of UtilDBPrm), _
                                               ByVal prmTblName As String, _
                                               Optional ByRef prmRefRecCnt As Integer = 0) As DataSet
            Try

                _cmd.CommandText = prmSQL
                _cmd.Parameters.Clear()
                If prmParameters Is Nothing Then

                    '�p�����^�Ȃ�
                    If InStr(prmSQL, "?") > 0 Then
                        '�u���p�����^�����݂���̂Ƀp�����^���X�g����̂��Ă��Ȃ��̂ŃG���[�Ƃ���
                        Throw New UsrDefException("�u���p�����[�^���ݒ肳��Ă��܂���B")
                    End If

                Else

                    '�p�����^����
                    Dim p As NpgsqlParameter = Nothing
                    For i As Integer = 0 To prmParameters.Count - 1      '��̃p�����^����Loop
                        Dim typePrm As NpgsqlDbType = Nothing               '�^
                        Select Case prmParameters(i).type
                            Case UtilDBPrm.parameterType.tBoolean
                                typePrm = NpgsqlDbType.Boolean
                            Case UtilDBPrm.parameterType.tDate
                                typePrm = NpgsqlDbType.Date
                            Case UtilDBPrm.parameterType.tNumber
                                typePrm = NpgsqlDbType.Numeric
                            Case UtilDBPrm.parameterType.tVarchar
                                typePrm = NpgsqlDbType.Char
                            Case Else
                                Throw New UsrDefException("�u���p�����[�^�̐ݒ肪����Ă��܂��B")
                        End Select
                        Dim wkName As String = CStr(i)                   '����
                        p = _cmd.Parameters.Add(wkName, typePrm)
                        p.Value = prmParameters(i).value                 '�l
                        p.Direction = prmParameters(i).systemDirection   '����
                        p.Size = prmParameters(i).size                   '�T�C�Y
                    Next

                End If

                _adp.SelectCommand = _cmd
                Dim ds As DataSet = New DataSet()

                'Select�����s
                Call _adp.Fill(ds, prmTblName)

                '�擾����
                prmRefRecCnt = ds.Tables(0).Rows.Count

                '�߂�l�ݒ�
                selectDB = ds

            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Debug.WriteLine(ex.StackTrace)
                Throw ex
            End Try

        End Function

        '-------------------------------------------------------------------------------
        '   �X�VSQL�����s
        '   �i�����T�v�jInsert/Update/Delete���𔭍s����
        '   �����̓p�����^  �FprmSQL        Select��
        '   �����\�b�h�߂�l�F�Ȃ�
        '                                               2014.02.20 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �X�VSQL�����s Insert/Update/Delete���𔭍s����
        ''' </summary>
        ''' <param name="prmSQL">SQL</param>
        ''' <remarks></remarks>
        Public Overrides Sub executeDB(ByVal prmSQL As String)
            Try
                _cmd.CommandText = prmSQL
                _cmd.ExecuteNonQuery()

            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Debug.WriteLine(ex.StackTrace)
                Throw ex
            End Try

        End Sub

        '-------------------------------------------------------------------------------
        '   �X�VSQL�����s
        '   �i�����T�v�jInsert/Update/Delete���𔭍s����
        '   �����̓p�����^  �FprmSQL                SQL��
        '                   �FprmRefAffectedRows    �e�����󂯂��s��
        '   �����\�b�h�߂�l�F�Ȃ�
        '                                               2014.02.20 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' ���s�nSQL�𔭍s����i�e�����������t���j
        ''' </summary>
        ''' <param name="prmSQL">���s����SQL��</param>
        ''' <param name="prmRefAffectedRows">�e�����󂯂��s��</param>
        ''' <remarks>���R�[�h�Z�b�g�𐶐����Ȃ�SQL(INSERT/UPDATE/DELETE�cetc)�𔭍s����B</remarks>
        Public Overrides Sub executeDB(ByVal prmSQL As String, ByRef prmRefAffectedRows As Integer)
            Try
                _cmd.CommandText = prmSQL
                prmRefAffectedRows = _cmd.ExecuteNonQuery

            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Debug.WriteLine(ex.StackTrace)
                Throw ex
            End Try
        End Sub

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
        '                                               2014.02.20 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �u���p�����[�^�t�����s�nSQL�𔭍s���� (�ڍׂ͎g�p��Q��)
        ''' </summary>
        ''' <param name="prmSQL">�p�����[�^�t��SQL��</param>
        ''' <param name="prmRefParameters">�u���p�����[�^���X�g</param>
        ''' <remarks>�X�g�A�h���s�Ȃǂ�z��(����ȊO�����s�\)</remarks>
        Public Overrides Sub executeDB(ByVal prmSQL As String, _
                                       ByRef prmRefParameters As List(Of UtilDBPrm))
            Try

                '�p�����^��ݒ�
                _cmd.Parameters.Clear()
                If prmRefParameters Is Nothing Then

                    '�p�����^�Ȃ�
                    If InStr(prmSQL, "?") > 0 Then
                        '�u���p�����^�����݂���̂Ƀp�����^���X�g����̂��Ă��Ȃ��̂ŃG���[�Ƃ���
                        Throw New UsrDefException("�u���p�����[�^���ݒ肳��Ă��܂���B")
                    End If

                Else

                    '�p�����^����
                    Dim p As NpgsqlParameter = Nothing
                    For i As Integer = 0 To prmRefParameters.Count - 1      '��̃p�����^����Loop
                        Dim typePrm As NpgsqlDbType = Nothing                  '�^
                        Select Case prmRefParameters(i).type
                            Case UtilDBPrm.parameterType.tBoolean
                                typePrm = NpgsqlDbType.Boolean
                            Case UtilDBPrm.parameterType.tDate
                                typePrm = NpgsqlDbType.Date
                            Case UtilDBPrm.parameterType.tNumber
                                typePrm = NpgsqlDbType.Numeric
                            Case UtilDBPrm.parameterType.tVarchar
                                typePrm = NpgsqlDbType.Char
                            Case Else
                                Throw New UsrDefException("�u���p�����[�^�̐ݒ肪����Ă��܂��B")
                        End Select
                        Dim wkName As String = CStr(i)                      '����
                        p = _cmd.Parameters.Add(wkName, typePrm)
                        p.Value = prmRefParameters(i).value                 '�l
                        p.Direction = prmRefParameters(i).systemDirection   '����
                        p.Size = prmRefParameters(i).size                   '�T�C�Y
                        prmRefParameters(i).refParameter = p                '�|�C���^(�N�G�����s��̌���[�p�����^�l]�擾�Ɏg�p)
                    Next

                End If

                '�N�G�����s
                _cmd.CommandText = prmSQL
                _cmd.ExecuteNonQuery()

            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Debug.WriteLine(ex.StackTrace)
                Throw ex
            End Try

        End Sub

    End Class

End Namespace

