Imports UtilMDL.Log


Namespace DB
    '===============================================================================
    '
    '  ���[�e�B���e�B�N���X
    '    �i�N���X���j    UtilOleDBDebugger
    '    �i�����@�\���j     ���O�o�͊g���@�\��������DB�A�N�Z�X(OLE DB)�񋟂���
    '    �i�{MDL�g�p�O��j  UtilLogDebugger���v���W�F�N�g�Ɏ�荞�܂�Ă��邱��
    '                       UtilDBInheritBase/UtilDBIf���v���W�F�N�g�Ɏ�荞�܂�Ă��邱��
    '    �i���l�j           UtilDBInheritBase���p��
    '                       UtilDBIf�C���^�[�t�F�[�X��(UtilDBInheritBase�ɂ�)����
    '
    '===============================================================================
    '  ����  ���O          ��  �t      �}�[�N      ���e
    '-------------------------------------------------------------------------------
    '  (1)   Jun.Takagi    2006/04/25              �V�K
    '  (2)   Jun.Takagi    2006/05/23              UtilDBInheritBase���p�����Ƃ���
    '  (3)   Jun.Takagi    2010/08/26              SystemInfo�e�[�u������̎擾�ɑΉ�
    '                                              �ڑ���DB���o��
    '-------------------------------------------------------------------------------
    Public Class UtilOleDBDebugger
        Inherits UtilDBInheritBase

        '===============================================================================
        '�����o�[�萔��`
        '===============================================================================
        Private _logger As UtilLogDebugger      '���O�f�o�b�K
        Private _hd As UtilOleDBHandler         'DB�n���h��

        '===============================================================================
        '�����o�[�ϐ���`
        '===============================================================================
        '�Ȃ�

        '===============================================================================
        '�v���p�e�B(�A�N�Z�T)
        '===============================================================================
        Public Property debugFlg() As Boolean '�f�o�b�O���[�h
            'Geter--------
            Get
                Return _logger.debugFlg
            End Get
            'Setter-------
            Set(ByVal Value As Boolean)
                _logger.debugFlg = Value
            End Set
        End Property
        Public Property consoleWrite() As Boolean '�R���\�[���o�͂��邩�ǂ���
            'Geter--------
            Get
                Return _logger.consoleWrite
            End Get
            'Setter-------
            Set(ByVal Value As Boolean)
                _logger.consoleWrite = Value
            End Set
        End Property
        '2006.05.23 add by takagi
        Public Overrides ReadOnly Property isTransactionOpen() As Boolean '�g�����U�N�V�������J���Ă��邩�ǂ���
            Get
                Return _hd.isTransactionOpen
            End Get
        End Property

        '===============================================================================
        ' �R���X�g���N�^
        '   �����̓p�����^   �F  prmUDLFileNm        UDL�t�@�C����(�t���p�X)
        '                       prmFileNm           Log�t�@�C����(�t���p�X)
        '                       prmDebugFlg         �f�o�b�O���[�h
        '                       <prmConsoleWrite>   �R���\�[���o�͂��邩�ǂ���
        '                                               2010.08.26 Updated By Jun.Takagi
        '===============================================================================
        ''' <summary>
        ''' �R���X�g���N�^
        ''' </summary>
        ''' <param name="prmUDLFileNm">UDL�t�@�C����(�t���p�X)</param>
        ''' <param name="prmFileNm">Log�t�@�C����(�t���p�X)</param>
        ''' <param name="prmDebugFlg">�f�o�b�O���[�h</param>
        ''' <param name="prmConsoleWrite">�R���\�[���o�͂��邩�ǂ���</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal prmUDLFileNm As String, _
                       ByVal prmFileNm As String, _
                       ByVal prmDebugFlg As Boolean, _
                       Optional ByVal prmConsoleWrite As Boolean = True)

            _logger = New UtilLogDebugger(prmFileNm, prmDebugFlg, prmConsoleWrite)
            Try
                _hd = New UtilOleDBHandler(prmUDLFileNm)
                '-->2010.08.26 upd by takagi #�ڑ���DB�o��
                '_logger.writeLine(UtilLogDebugger.LOG_DEBUG, "�f�[�^�x�[�X�ڑ�����")
                Dim r As IO.StreamReader = New IO.StreamReader(prmUDLFileNm, System.Text.Encoding.Default)
                Dim conStr As String = ""
                Try : conStr = r.ReadToEnd()
                Finally : r.Close()
                End Try
                _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "HashCode:" & Me.GetHashCode & " �f�[�^�x�[�X�ڑ������FConnectionString=[" & conStr & "]")
                '<--2010.08.26 upd by takagi #�ڑ���DB�o��
            Catch ex As Exception
                '-->2010.08.26 upd by takagi #�ڑ���DB�o��
                '_logger.writeLine(UtilLogDebugger.LOG_ERR, "�f�[�^�x�[�X�ڑ����s", ex.Message & ControlChars.NewLine & ex.StackTrace)
                _logger.writeLine(UtilLogDebugger.LOG_ERR, "HashCode:" & Me.GetHashCode & " �f�[�^�x�[�X�ڑ����s", ex.Message & ControlChars.NewLine & ex.StackTrace)
                '<--2010.08.26 upd by takagi #�ڑ���DB�o��
                Throw ex
            End Try

        End Sub

        '-------------------------------------------------------------------------------
        '   DB�ؒf
        '   �i�����T�v�jDB�ڑ����N���[�Y����
        '   �����̓p�����^  �F�Ȃ�
        '   �����\�b�h�߂�l�F�Ȃ�
        '                                               2006.05.23 Updated By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' DB�ؒf
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub close()
            Try
                _hd.close()
                '-->2010.08.26 upd by takagi #�ڑ���DB�o��
                '_logger.writeLine(UtilLogDebugger.LOG_DEBUG, "�f�[�^�x�[�X�ؒf")
                _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "HashCode:" & Me.GetHashCode & " �f�[�^�x�[�X�ؒf")
                '<--2010.08.26 upd by takagi #�ڑ���DB�o��
            Catch ex As Exception
                '-->2010.08.26 upd by takagi #�ڑ���DB�o��
                '_logger.writeLine(UtilLogDebugger.LOG_ERR, "�f�[�^�x�[�X�ؒf���s", ex.Message & ControlChars.NewLine & ex.StackTrace)
                _logger.writeLine(UtilLogDebugger.LOG_ERR, "HashCode:" & Me.GetHashCode & " �f�[�^�x�[�X�ؒf���s", ex.Message & ControlChars.NewLine & ex.StackTrace)
                '<--2010.08.26 upd by takagi #�ڑ���DB�o��
                Throw ex
            End Try

        End Sub

        '-------------------------------------------------------------------------------
        '   �g�����U�N�V�����J�n
        '   �i�����T�v) �g�����U�N�V�������J�n����
        '   �����̓p�����^  �F�Ȃ�
        '   �����\�b�h�߂�l�F�Ȃ�
        '                                               2006.05.23 Updated By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �g�����U�N�V�����J�n
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub beginTran()
            Try
                _hd.beginTran()
                '-->2010.08.26 upd by takagi #�ڑ���DB�o��
                '_logger.writeLine(UtilLogDebugger.LOG_DEBUG, "beginTran")
                _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "HashCode:" & Me.GetHashCode & " beginTran")
                '<--2010.08.26 upd by takagi #�ڑ���DB�o��
            Catch ex As Exception
                '-->2010.08.26 upd by takagi #�ڑ���DB�o��
                '_logger.writeLine(UtilLogDebugger.LOG_ERR, "beginTran���s", ex.Message & ControlChars.NewLine & ex.StackTrace)
                _logger.writeLine(UtilLogDebugger.LOG_ERR, "HashCode:" & Me.GetHashCode & " beginTran���s", ex.Message & ControlChars.NewLine & ex.StackTrace)
                '<--2010.08.26 upd by takagi #�ڑ���DB�o��
                Throw ex
            End Try
        End Sub

        '-------------------------------------------------------------------------------
        '   �g�����U�N�V�����I��
        '   �i�����T�v) �g�����U�N�V������Commit����
        '   �����̓p�����^  �F�Ȃ�
        '   �����\�b�h�߂�l�F�Ȃ�
        '                                               2006.05.23 Updated By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �g�����U�N�V�����I��
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub commitTran()
            Try
                _hd.commitTran()
                '-->2010.08.26 upd by takagi #�ڑ���DB�o��
                '_logger.writeLine(UtilLogDebugger.LOG_DEBUG, "commitTran")
                _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "HashCode:" & Me.GetHashCode & " commitTran")
                '<--2010.08.26 upd by takagi #�ڑ���DB�o��
            Catch ex As Exception
                '-->2010.08.26 upd by takagi #�ڑ���DB�o��
                '_logger.writeLine(UtilLogDebugger.LOG_ERR, "commitTran���s", ex.Message & ControlChars.NewLine & ex.StackTrace)
                _logger.writeLine(UtilLogDebugger.LOG_ERR, "HashCode:" & Me.GetHashCode & " commitTran���s", ex.Message & ControlChars.NewLine & ex.StackTrace)
                '<--2010.08.26 upd by takagi #�ڑ���DB�o��
                Throw ex
            End Try
        End Sub

        '-------------------------------------------------------------------------------
        '   �g�����U�N�V�����j��
        '   �i�����T�v) �g�����U�N�V������Rollback����
        '   �����̓p�����^  �F�Ȃ�
        '   �����\�b�h�߂�l�F�Ȃ�
        '                                               2006.05.23 Updated By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �g�����U�N�V�����j��
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub rollbackTran()
            Try
                _hd.rollbackTran()
                '-->2010.08.26 upd by takagi #�ڑ���DB�o��
                '_logger.writeLine(UtilLogDebugger.LOG_DEBUG, "rollbackTran")
                _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "HashCode:" & Me.GetHashCode & " rollbackTran")
                '<--2010.08.26 upd by takagi #�ڑ���DB�o��
            Catch ex As Exception
                '-->2010.08.26 upd by takagi #�ڑ���DB�o��
                '_logger.writeLine(UtilLogDebugger.LOG_ERR, "rollbackTran���s", ex.Message & ControlChars.NewLine & ex.StackTrace)
                _logger.writeLine(UtilLogDebugger.LOG_ERR, "HashCode:" & Me.GetHashCode & " rollbackTran���s", ex.Message & ControlChars.NewLine & ex.StackTrace)
                '<--2010.08.26 upd by takagi #�ڑ���DB�o��
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
        '                                               2006.05.23 Updated By Jun.Takagi
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
            Dim ds As DataSet
            Try
                ds = _hd.selectDB(prmSQL, prmTblName, prmRefRecCnt)
                '-->2010.08.26 upd by takagi #�ڑ���DB�o��
                '_logger.writeLine(UtilLogDebugger.LOG_DEBUG, "�f�[�^�擾�����F" & prmRefRecCnt & "��", prmSQL)
                _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "HashCode:" & Me.GetHashCode & " �f�[�^�擾�����F" & prmRefRecCnt & "��", prmSQL)
                '<--2010.08.26 upd by takagi #�ڑ���DB�o��
            Catch ex As Exception
                '-->2010.08.26 upd by takagi #�ڑ���DB�o��
                '_logger.writeLine(UtilLogDebugger.LOG_ERR, "�f�[�^�擾���s�F" & ex.Message & ControlChars.NewLine & ex.StackTrace, prmSQL)
                _logger.writeLine(UtilLogDebugger.LOG_ERR, "HashCode:" & Me.GetHashCode & " �f�[�^�擾���s�F" & ex.Message & ControlChars.NewLine & ex.StackTrace, prmSQL)
                '<--2010.08.26 upd by takagi #�ڑ���DB�o��
                Throw ex
            End Try
            Return ds
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
        Public Overrides Function selectDB(ByVal prmSQL As String, _
                                               ByVal prmParameters As List(Of UtilDBPrm), _
                                               ByVal prmTblName As String, _
                                               Optional ByRef prmRefRecCnt As Integer = 0) As DataSet
            Dim ds As DataSet
            Try
                Dim outWk As String = ""
                For i As Integer = 0 To prmParameters.Count - 1
                    If Not "".Equals(outWk) Then
                        outWk = outWk & " , "
                    End If
                    Select Case prmParameters(i).type
                        Case UtilDBPrm.parameterType.tDate
                            outWk = outWk & "#" & CStr(prmParameters(i).value) & "#"
                        Case UtilDBPrm.parameterType.tVarchar
                            outWk = outWk & "'" & CStr(prmParameters(i).value) & "'"
                        Case Else
                            outWk = outWk & "" & CStr(prmParameters(i).value) & ""
                    End Select
                Next
                Try
                    ds = _hd.selectDB(prmSQL, prmParameters, prmTblName, prmRefRecCnt)
                    '-->2010.08.26 upd by takagi #�ڑ���DB�o��
                    '_logger.writeLine(UtilLogDebugger.LOG_DEBUG, "�f�[�^�擾�����F" & prmRefRecCnt & "��", prmSQL & " {�p�����[�^�F" & outWk & "}")
                    _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "HashCode:" & Me.GetHashCode & " �f�[�^�擾�����F" & prmRefRecCnt & "��", prmSQL & " {�p�����[�^�F" & outWk & "}")
                    '<--2010.08.26 upd by takagi #�ڑ���DB�o��
                Catch ex As Exception
                    '-->2010.08.26 upd by takagi #�ڑ���DB�o��
                    '_logger.writeLine(UtilLogDebugger.LOG_ERR, "�f�[�^�擾���s�F" & ex.Message & ControlChars.NewLine & ex.StackTrace, prmSQL & " {�p�����[�^�F" & outWk & "}")
                    _logger.writeLine(UtilLogDebugger.LOG_ERR, "HashCode:" & Me.GetHashCode & " �f�[�^�擾���s�F" & ex.Message & ControlChars.NewLine & ex.StackTrace, prmSQL & " {�p�����[�^�F" & outWk & "}")
                    '<--2010.08.26 upd by takagi #�ڑ���DB�o��
                    Throw ex
                End Try
            Catch ex As Exception
                Throw ex
            End Try
            Return ds
        End Function

        '-------------------------------------------------------------------------------
        '   �X�VSQL�����s
        '   �i�����T�v�jInsert/Update/Delete���𔭍s����
        '   �����̓p�����^  �FprmSQL        SQL��
        '   �����\�b�h�߂�l�F�Ȃ�
        '                                               2006.05.23 Updated By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �X�VSQL�����s Insert/Update/Delete���𔭍s����
        ''' </summary>
        ''' <param name="prmSQL">SQL</param>
        ''' <remarks></remarks>
        Public Overrides Sub executeDB(ByVal prmSQL As String)
            Try
                _hd.executeDB(prmSQL)
                '-->2010.08.26 upd by takagi #�ڑ���DB�o��
                '_logger.writeLine(UtilLogDebugger.LOG_DEBUG, "�f�[�^�X�V����", prmSQL)
                _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "HashCode:" & Me.GetHashCode & " �f�[�^�X�V����", prmSQL)
                '<--2010.08.26 upd by takagi #�ڑ���DB�o��
            Catch ex As Exception
                '-->2010.08.26 upd by takagi #�ڑ���DB�o��
                '_logger.writeLine(UtilLogDebugger.LOG_ERR, "�f�[�^�X�V���s�F" & ex.Message & ControlChars.NewLine & ex.StackTrace, prmSQL)
                _logger.writeLine(UtilLogDebugger.LOG_ERR, "HashCode:" & Me.GetHashCode & " �f�[�^�X�V���s�F" & ex.Message & ControlChars.NewLine & ex.StackTrace, prmSQL)
                '<--2010.08.26 upd by takagi #�ڑ���DB�o��
                Throw ex
            End Try
        End Sub

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
        Public Overrides Sub executeDB(ByVal prmSQL As String, ByRef prmRefAffectedRows As Integer)
            Try
                _hd.executeDB(prmSQL, prmRefAffectedRows)
                '-->2010.08.26 upd by takagi #�ڑ���DB�o��
                '_logger.writeLine(UtilLogDebugger.LOG_DEBUG, "�f�[�^�X�V����(�X�V�����F" & prmRefAffectedRows.ToString & "��)", prmSQL)
                _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "HashCode:" & Me.GetHashCode & " �f�[�^�X�V����(�X�V�����F" & prmRefAffectedRows.ToString & "��)", prmSQL)
                '<--2010.08.26 upd by takagi #�ڑ���DB�o��
            Catch ex As Exception
                '-->2010.08.26 upd by takagi #�ڑ���DB�o��
                '_logger.writeLine(UtilLogDebugger.LOG_ERR, "�f�[�^�X�V���s�F" & ex.Message & ControlChars.NewLine & ex.StackTrace, prmSQL)
                _logger.writeLine(UtilLogDebugger.LOG_ERR, "HashCode:" & Me.GetHashCode & " �f�[�^�X�V���s�F" & ex.Message & ControlChars.NewLine & ex.StackTrace, prmSQL)
                '<--2010.08.26 upd by takagi #�ڑ���DB�o��
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
        '                                               2006.06.16 Created By Jun.Takagi
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
                Dim outWk As String = ""
                For i As Integer = 0 To prmRefParameters.Count - 1
                    If Not "".Equals(outWk) Then
                        outWk = outWk & " , "
                    End If
                    Select Case prmRefParameters(i).type
                        Case UtilDBPrm.parameterType.tDate
                            outWk = outWk & "#" & CStr(prmRefParameters(i).value) & "#"
                        Case UtilDBPrm.parameterType.tVarchar
                            outWk = outWk & "'" & CStr(prmRefParameters(i).value) & "'"
                        Case Else
                            outWk = outWk & "" & CStr(prmRefParameters(i).value) & ""
                    End Select
                Next
                Try
                    _hd.executeDB(prmSQL, prmRefParameters)
                    '-->2010.08.26 upd by takagi #�ڑ���DB�o��
                    '_logger.writeLine(UtilLogDebugger.LOG_DEBUG, "�f�[�^�X�V����", prmSQL & " {�p�����[�^�F" & outWk & "}")
                    _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "HashCode:" & Me.GetHashCode & " �f�[�^�X�V����", prmSQL & " {�p�����[�^�F" & outWk & "}")
                    '<--2010.08.26 upd by takagi #�ڑ���DB�o��
                Catch ex As Exception
                    '-->2010.08.26 upd by takagi #�ڑ���DB�o��
                    '_logger.writeLine(UtilLogDebugger.LOG_ERR, "�f�[�^�X�V���s�F" & ex.Message & ControlChars.NewLine & ex.StackTrace, prmSQL & " {�p�����[�^�F" & outWk & "}")
                    _logger.writeLine(UtilLogDebugger.LOG_ERR, "HashCode:" & Me.GetHashCode & " �f�[�^�X�V���s�F" & ex.Message & ControlChars.NewLine & ex.StackTrace, prmSQL & " {�p�����[�^�F" & outWk & "}")
                    '<--2010.08.26 upd by takagi #�ڑ���DB�o��
                    Throw ex
                End Try
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        '-------------------------------------------------------------------------------
        '   SystemInfo�擾
        '   �i�����T�v�j�@SystemInfo����l���擾����
        '   �����̓p�����^   �FprmFixKey        �Œ�L�[
        '                      prmVariableKey   �σL�[
        '   �����\�b�h�߂�l �FSystemInfo���R�[�h
        '                                               2010.08.26 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        Public Overrides Function getSystemInfo(ByVal prmFixKey As String, ByVal prmVariableKey As String) As UtilDBIf.sysinfoRec
            Dim rec As UtilDBIf.sysinfoRec
            Try
                rec = _hd.getSystemInfo(prmFixKey, prmVariableKey)
                _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "HashCode:" & Me.GetHashCode & " SystemInfo�擾�����F�Œ�L�[=[" & prmFixKey & "] �σL�[=[" & prmVariableKey & "]")
            Catch ex As Exception
                _logger.writeLine(UtilLogDebugger.LOG_ERR, "HashCode:" & Me.GetHashCode & " SystemInfo�擾���s�F" & ex.ToString)
                Throw ex
            End Try
            Return rec
        End Function

        '-------------------------------------------------------------------------------
        '   SystemInfo�擾
        '   �i�����T�v�j�@SystemInfo����l���擾����
        '   �����̓p�����^   �FprmFixKey        �Œ�L�[
        '   �����\�b�h�߂�l �FSystemInfo���R�[�h
        '                                               2010.08.26 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        Public Overrides Function getSystemInfo(ByVal prmFixKey As String) As UtilDBIf.sysinfoRec()
            Dim rec() As UtilDBIf.sysinfoRec
            Try
                rec = _hd.getSystemInfo(prmFixKey)
                _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "HashCode:" & Me.GetHashCode & " SystemInfo�擾�����F�Œ�L�[=[" & prmFixKey & "] " & rec.Length & "��")
            Catch ex As Exception
                _logger.writeLine(UtilLogDebugger.LOG_ERR, "HashCode:" & Me.GetHashCode & " SystemInfo�擾���s�F" & ex.ToString)
                Throw ex
            End Try
            Return rec
        End Function

        '-------------------------------------------------------------------------------
        '   SystemInfo�擾
        '   �i�����T�v�j�@SystemInfo����l���擾����
        '   �����̓p�����^   �F�Ȃ�
        '   �����\�b�h�߂�l �FSystemInfo���R�[�h
        '                                               2010.08.26 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        Public Overrides Function getSystemInfo() As UtilDBIf.sysinfoRec()
            Dim rec() As UtilDBIf.sysinfoRec
            Try
                rec = _hd.getSystemInfo()
                _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "HashCode:" & Me.GetHashCode & " SystemInfo�擾�����F" & rec.Length & "��")
            Catch ex As Exception
                _logger.writeLine(UtilLogDebugger.LOG_ERR, "HashCode:" & Me.GetHashCode & " SystemInfo�擾���s�F" & ex.ToString)
                Throw ex
            End Try
            Return rec
        End Function

        '-------------------------------------------------------------------------------
        '   SystemInfo�擾
        '   �i�����T�v�j�@SystemInfo����FixKey���X�g���擾����
        '   �����̓p�����^   �F�Ȃ�
        '   �����\�b�h�߂�l �FSystemInfo���R�[�h
        '                                               2010.08.26 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        Public Overrides Function getSystemInfoFixKeies() As UtilDBIf.sysinfoRec()
            Dim rec() As UtilDBIf.sysinfoRec
            Try
                rec = _hd.getSystemInfoFixKeies()
                _logger.writeLine(UtilLogDebugger.LOG_DEBUG, "HashCode:" & Me.GetHashCode & " SystemInfoFixKeies�擾�����F" & rec.Length & "��")
            Catch ex As Exception
                _logger.writeLine(UtilLogDebugger.LOG_ERR, "HashCode:" & Me.GetHashCode & " SystemInfoFixKeies�擾���s�F" & ex.ToString)
                Throw ex
            End Try
            Return rec
        End Function

    End Class
End Namespace
