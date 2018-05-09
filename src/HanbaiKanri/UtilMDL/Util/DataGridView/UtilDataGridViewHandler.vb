Imports System.Windows.Forms.DataGridView

Namespace DataGridView

    '===============================================================================
    '
    '  ���[�e�B���e�B�N���X
    '    �i�N���X���j    UtilDataGridViewHandler
    '    �i�����@�\���j      DataGridView�R���g���[���̐���@�\��񋟂���
    '    �i�{MDL�g�p�O��j   ���ɂȂ�
    '    �i���l�j            
    '
    '===============================================================================
    '  ����  ���O          ��  �t      �}�[�N      ���e
    '-------------------------------------------------------------------------------
    '  (1)   Jun.Takagi    2006/05/01              �V�K
    '-------------------------------------------------------------------------------
    Public Class UtilDataGridViewHandler

        '===============================================================================
        '�����o�[�ϐ���`
        '===============================================================================
        Private _grid As Windows.Forms.DataGridView
        '>--2006/11/10 ADD -STR- A.Yamazaki
        '===============================================================================
        '�񋓌^��`
        '===============================================================================
        Public Enum chkType '�Z�����̓`�F�b�N�^�C�v
            ''' <summary>
            ''' ���t�^�P��yy/MM/dd
            ''' </summary>
            ''' <remarks></remarks>
            Date1 = 1
            ''' <summary>
            ''' ���t�^�Q��yyyy/MM/dd
            ''' </summary>
            ''' <remarks></remarks>
            Date2 = 2
            ''' <summary>
            ''' ���l��123456789
            ''' </summary>
            ''' <remarks></remarks>
            Num = 3
            ''' <summary>
            ''' ���l�i�}�C�i�X���j��-123 123
            ''' </summary>
            ''' <remarks></remarks>
            Num_M = 4
            ''' <summary>
            ''' ���z�i�J���}�ҏW�j��123,456,789
            ''' </summary>
            ''' <remarks></remarks>
            Cur = 5
            ''' <summary>
            ''' ���p�p����ABCDabcd1234
            ''' </summary>
            ''' <remarks></remarks>
            Hankaku = 6

        End Enum

        '===============================================================================
        '���̓`�F�b�N�p�萔
        '===============================================================================
        Private Shared NUM_CHARS As Char() = New Char(10) {"0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c, "."c}
        Private Shared NUM_MINS_CHARS As Char() = New Char(11) {"0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c, "-"c, "."c}
        Private Shared HANKAKU_CHARS As Char() = New Char(61) {"0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c, _
                                                      "a"c, "b"c, "c"c, "d"c, "e"c, "f"c, "g"c, "h"c, "i"c, "j"c, "k"c, "l"c, "m"c, "n"c, "o"c, "p"c, "q"c, "r"c, "s"c, "t"c, "u"c, "v"c, "w"c, "x"c, "y"c, "z"c, _
                                                      "A"c, "B"c, "C"c, "D"c, "E"c, "F"c, "G"c, "H"c, "I"c, "J"c, "K"c, "L"c, "M"c, "N"c, "O"c, "P"c, "Q"c, "R"c, "S"c, "T"c, "U"c, "V"c, "W"c, "X"c, "Y"c, "Z"c}
        '<--2006/11/10 ADD -END- A.Yamazaki
        '===============================================================================
        '�v���p�e�B(�A�N�Z�T)
        '===============================================================================

        '===============================================================================
        ' �R���X�g���N�^
        '   �����̓p�����^   �FprmTargetGrid    ����Ώ�DataGridView
        '===============================================================================
        ''' <summary>
        ''' DataGridView�n���h���̃C���X�^���X�𐶐�����
        ''' </summary>
        ''' <param name="prmTargetGrid">����ΏۂƂȂ�DataGridView</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal prmTargetGrid As Windows.Forms.DataGridView)

            _grid = prmTargetGrid

        End Sub

        '-------------------------------------------------------------------------------
        '   �f�[�^�N���A
        '   �i�����T�v�j�f�[�^�s������������(�ꗗ��0�s�Ƃ���)
        '   �����̓p�����^   �FprmDataColName   �f�[�^�Z�b�g��̗�
        '   �@�@�@�@�@�@�@�@ �FprmRow           �Ώۍs�̃C���f�b�N�X(0�`)
        '   �����\�b�h�߂�l �F�Ȃ�
        '   �����l�@�@       �FDataGridView�ƃo�C���h����Ă���ADataSet��̃f�[�^��ON��ݒ肷��
        '                                               2006.05.01 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �f�[�^�s������������(�ꗗ��0�s�Ƃ���)
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub clearRow()

            If Not _grid.DataSource Is Nothing Then
                Call CType(_grid.DataSource, System.Data.DataSet).Tables(0).Rows.Clear()
            End If

        End Sub

        '-------------------------------------------------------------------------------
        '   �`�F�b�N�{�b�N�XON
        '   �i�����T�v�j�`�F�b�N�{�b�N�X�^�̗�f�[�^���`�F�b�N�{�b�N�XON�Ƃ���
        '   �����̓p�����^   �FprmDataColName   �f�[�^�Z�b�g��̗�
        '   �@�@�@�@�@�@�@�@ �FprmRow           �Ώۍs�̃C���f�b�N�X(0�`)
        '   �����\�b�h�߂�l �F�Ȃ�
        '   �����l�@�@       �FDataGridView�ƃo�C���h����Ă���ADataSet��̃f�[�^��ON��ݒ肷��
        '                                               2006.05.01 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �`�F�b�N�{�b�N�X�^�̗�f�[�^���`�F�b�N�{�b�N�XON�Ƃ���
        ''' </summary>
        ''' <param name="prmDataColName">�f�[�^�Z�b�g��̗�</param>
        ''' <param name="prmRow">�Ώۍs�̃C���f�b�N�X(0�`)</param>
        ''' <remarks></remarks>
        Public Sub checkBoxOn(ByVal prmDataColName As String, ByVal prmRow As Integer)
            Dim cell As Object = CType(_grid.DataSource, System.Data.DataSet).Tables(0).Rows(prmRow)(prmDataColName)
            If cell.GetType().ToString.Equals("System.Int16") Or _
               cell.GetType().ToString.Equals("System.Int32") Or _
               cell.GetType().ToString.Equals("System.Int64") Then
                CType(_grid.DataSource, System.Data.DataSet).Tables(0).Rows(prmRow)(prmDataColName) = 1
            ElseIf cell.GetType().ToString.Equals("System.String") Then
                CType(_grid.DataSource, System.Data.DataSet).Tables(0).Rows(prmRow)(prmDataColName) = "1"
            ElseIf cell.GetType().ToString.Equals("System.Boolean") Then
                CType(_grid.DataSource, System.Data.DataSet).Tables(0).Rows(prmRow)(prmDataColName) = True
            End If
        End Sub

        '-------------------------------------------------------------------------------
        '   �`�F�b�N�{�b�N�XOFF
        '   �i�����T�v�j�`�F�b�N�{�b�N�X�^�̗�f�[�^���`�F�b�N�{�b�N�XOFF�Ƃ���
        '   �����̓p�����^   �FprmDataColName   �f�[�^�Z�b�g��̗�
        '   �@�@�@�@�@�@�@�@ �FprmRow           �Ώۍs�̃C���f�b�N�X(0�`)
        '   �����\�b�h�߂�l �F�Ȃ�
        '   �����l�@�@       �FDataGridView�ƃo�C���h����Ă���ADataSet��̃f�[�^��ON��ݒ肷��
        '                                               2006.05.01 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �`�F�b�N�{�b�N�X�^�̗�f�[�^���`�F�b�N�{�b�N�XOFF�Ƃ���
        ''' </summary>
        ''' <param name="prmDataColName">�f�[�^�Z�b�g��̗�</param>
        ''' <param name="prmRow">�Ώۍs�̃C���f�b�N�X(0�`)</param>
        ''' <remarks></remarks>
        Public Sub checkBoxOff(ByVal prmDataColName As String, ByVal prmRow As Integer)
            Dim cell As Object = CType(_grid.DataSource, System.Data.DataSet).Tables(0).Rows(prmRow)(prmDataColName)
            If cell.GetType().ToString.Equals("System.Int16") Or _
               cell.GetType().ToString.Equals("System.Int32") Or _
               cell.GetType().ToString.Equals("System.Int64") Then
                CType(_grid.DataSource, System.Data.DataSet).Tables(0).Rows(prmRow)(prmDataColName) = 0
            ElseIf cell.GetType().ToString.Equals("System.String") Then
                CType(_grid.DataSource, System.Data.DataSet).Tables(0).Rows(prmRow)(prmDataColName) = "0"
            ElseIf cell.GetType().ToString.Equals("System.Boolean") Then
                CType(_grid.DataSource, System.Data.DataSet).Tables(0).Rows(prmRow)(prmDataColName) = False
            End If
        End Sub

        '-------------------------------------------------------------------------------
        '   �Z���擾
        '   �i�����T�v�j�Z���I�u�W�F�N�g���擾����
        '   �����̓p�����^   �FprmColName   �O���b�h��̗�
        '   �@�@�@�@�@�@�@�@ �FprmRow       �Ώۍs�̃C���f�b�N�X(0�`)
        '   �����\�b�h�߂�l �F�Ȃ�
        '   �����l�@�@       �FDataGridView���Cell���̂��̂��擾
        '                                               2006.05.01 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �Z���I�u�W�F�N�g���擾����(DataGridView���Cell���̂��̂��擾)
        ''' </summary>
        ''' <param name="prmColName">�O���b�h��̗�</param>
        ''' <param name="prmRow">�Ώۍs�̃C���f�b�N�X(0�`)</param>
        ''' <returns>�Z���I�u�W�F�N�g</returns>
        ''' <remarks></remarks>
        Public Function getCell(ByVal prmColName As String, ByVal prmRow As Integer) As Windows.Forms.DataGridViewCell
            Return _grid.Rows(prmRow).Cells(prmColName)
        End Function

        '-------------------------------------------------------------------------------
        '   �Z���f�[�^�擾
        '   �i�����T�v�j�Z���Ɋi�[����Ă���f�[�^���擾����
        '   �����̓p�����^   �FprmDataColName   �f�[�^�Z�b�g��̗�
        '   �@�@�@�@�@�@�@�@ �FprmRow           �Ώۍs�̃C���f�b�N�X(0�`)
        '   �����\�b�h�߂�l �F�i�[�f�[�^(String�^�ŕԋp)
        '   �����l�@�@       �FDataGridView�ƃo�C���h����Ă���ADataSet��̃f�[�^����擾����
        '                                               2006.06.05 Updated By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �Z���Ɋi�[����Ă���f�[�^���擾����
        ''' </summary>
        ''' <param name="prmDataColName">�f�[�^�Z�b�g��̗�</param>
        ''' <param name="prmRow">�Ώۍs�̃C���f�b�N�X(0�`)</param>
        ''' <returns>DataSet��̗�f�[�^</returns>
        ''' <remarks></remarks>
        Public Function getCellData(ByVal prmDataColName As String, ByVal prmRow As Integer) As String
            Dim cell As Object = CType(_grid.DataSource, System.Data.DataSet).Tables(0).Rows(prmRow)(prmDataColName)
            If IsDBNull(cell) Then
                Return ""
            Else
                Return cell
            End If
        End Function

        '-------------------------------------------------------------------------------
        '   �Z���f�[�^�擾
        '   �i�����T�v�j�Z���Ɋi�[����Ă���f�[�^���擾����
        '   �����̓p�����^   �FprmDataColIdx    �f�[�^�Z�b�g��̗�C���f�b�N�X(0�`)
        '   �@�@�@�@�@�@�@�@ �FprmRow           �Ώۍs�̃C���f�b�N�X(0�`)
        '   �����\�b�h�߂�l �F�i�[�f�[�^(String�^�ŕԋp)
        '   �����l�@�@       �FDataGridView�ƃo�C���h����Ă���ADataSet��̃f�[�^����擾����
        '                                               2006.06.08 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �Z���Ɋi�[����Ă���f�[�^���擾����
        ''' </summary>
        ''' <param name="prmDataColIdx">�f�[�^�Z�b�g��̗�C���f�b�N�X(0�`)</param>
        ''' <param name="prmRow">�Ώۍs�̃C���f�b�N�X(0�`)</param>
        ''' <returns>DataSet��̗�f�[�^</returns>
        ''' <remarks></remarks>
        Public Function getCellData(ByVal prmDataColIdx As Integer, ByVal prmRow As Integer) As String
            Dim cell As Object = CType(_grid.DataSource, System.Data.DataSet).Tables(0).Rows(prmRow)(prmDataColIdx)
            If IsDBNull(cell) Then
                Return ""
            Else
                Return cell
            End If
        End Function

        '-------------------------------------------------------------------------------
        '   �Z���f�[�^�ݒ�
        '   �i�����T�v�j�Z���Ɋi�[����Ă���f�[�^��ݒ肷��
        '   �����̓p�����^   �FprmDataColName   �f�[�^�Z�b�g��̗�
        '   �@�@�@�@�@�@�@�@ �FprmRow           �Ώۍs�̃C���f�b�N�X(0�`)
        '   �@�@�@�@�@�@�@�@ �FprmVal           �i�[����f�[�^
        '   �����\�b�h�߂�l �F�Ȃ�
        '   �����l�@�@       �FDataGridView�ƃo�C���h����Ă���ADataSet��̃f�[�^�֐ݒ肷��
        '                                               2006.05.01 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �Z���Ɋi�[����Ă���f�[�^��ݒ肷��
        ''' </summary>
        ''' <param name="prmDataColName">�f�[�^�Z�b�g��̗�</param>
        ''' <param name="prmRow">�Ώۍs�̃C���f�b�N�X(0�`)</param>
        ''' <param name="prmVal">�i�[����f�[�^</param>
        ''' <remarks></remarks>
        Public Sub setCellData(ByVal prmDataColName As String, ByVal prmRow As Integer, ByVal prmVal As Object)
            CType(_grid.DataSource, System.Data.DataSet).Tables(0).Rows(prmRow)(prmDataColName) = prmVal
        End Sub

        '-------------------------------------------------------------------------------
        '   �J�����g�Z���ݒ�
        '   �i�����T�v�j�J�����g�Z����ݒ肷��
        '   �����̓p�����^   �FprmColName       �O���b�h��̗�
        '   �@�@�@�@�@�@�@�@ �FprmRow           �Ώۍs�̃C���f�b�N�X(0�`)
        '   �����\�b�h�߂�l �F�Ȃ�
        '                                               2006.05.11 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �J�����g�Z����ݒ肷��
        ''' </summary>
        ''' <param name="prmColName">�O���b�h��̗�</param>
        ''' <param name="prmRow">�Ώۍs�̃C���f�b�N�X(0�`)</param>
        ''' <remarks></remarks>
        Public Sub setCurrentCell(ByVal prmColName As String, ByVal prmRow As Integer)
            _grid.CurrentCell = Me.getCell(prmColName, prmRow)
        End Sub
        '2010.08.18 add by takagi
        Public Structure dgvErrSet
            Public onErr As Boolean
            Public row As Integer
            Public colName As String
        End Structure
        Public Function readyForErrSet(ByVal prmErrRow As Integer, ByVal prmColName As String) As dgvErrSet
            Dim ret As dgvErrSet
            ret.onErr = True
            ret.row = prmErrRow
            ret.colName = prmColName
            Return ret
        End Function
        Public Sub setCurrentCell(ByRef prmRefCell As dgvErrSet)
            If prmRefCell.onErr Then
                _grid.CurrentCell = Me.getCell(prmRefCell.colName, prmRefCell.row)
                prmRefCell.onErr = False
            End If
        End Sub
        '2010.08.18 add by takagi

        '-------------------------------------------------------------------------------
        '   �ő�s���擾
        '   �i�����T�v�j�O���b�h�ɕ\������Ă���f�[�^�̍ő�s���擾
        '   �����̓p�����^   �F�Ȃ�
        '   �����\�b�h�߂�l �F�s��
        '                                               2006.05.29 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �O���b�h�ɕ\������Ă���f�[�^�̍ő�s���擾
        ''' </summary>
        ''' <returns>�ő�s</returns>
        ''' <remarks></remarks>
        Public Function getMaxRow() As Integer
            '-->2010.11.15 chg by takagi
            'Return CType(_grid.DataSource, DataSet).Tables(0).Rows.Count
            If _grid.DataSource Is Nothing Then Return 0
            Return CType(_grid.DataSource, DataSet).Tables(0).Rows.Count
            '<--2010.11.15 chg by takagi
        End Function

        '-------------------------------------------------------------------------------
        '   �񃍃b�N
        '   �i�����T�v�j���ǂݎ���p�ɂ���
        '   �����̓p�����^   �FprmColName       �O���b�h��̗�
        '   �����\�b�h�߂�l �F�Ȃ�
        '                                               2006.05.01 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' ������b�N����(�ǎ��p)
        ''' </summary>
        ''' <param name="prmColName">�O���b�h��̗�</param>
        ''' <remarks></remarks>
        Public Sub colRock(ByVal prmColName As String)
            _grid.Columns(prmColName).ReadOnly = True
        End Sub

        '-------------------------------------------------------------------------------
        '   ��A�����b�N
        '   �i�����T�v�j��̓ǂݎ���p���������A�ҏW�\�ɂ���
        '   �����̓p�����^   �FprmColName       �O���b�h��̗�
        '   �����\�b�h�߂�l �F�Ȃ�
        '                                               2006.05.01 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' ��̃��b�N����������(�ҏW�\)
        ''' </summary>
        ''' <param name="prmColName">�O���b�h��̗�</param>
        ''' <remarks></remarks>
        Public Sub colUnRock(ByVal prmColName As String)
            _grid.Columns(prmColName).ReadOnly = False
        End Sub

        '-------------------------------------------------------------------------------
        '   ��w�i�F�ύX
        '   �i�����T�v�j��̔w�i�F��ύX����
        '   �����̓p�����^   �FprmColName       �O���b�h��̗�
        '                    �FprmBackColor     �w�i�F
        '                    �FprmForeColor     �O�i�F
        '                    �FprmSelBackColor  �I�����̔w�i�F
        '                    �FprmSelForeColor  �I�����̑O�i�F
        '   �����\�b�h�߂�l �F�Ȃ�
        '                                               2006.05.01 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' ��̔w�i�F��ύX����
        ''' </summary>
        ''' <param name="prmColName">�O���b�h��̗�</param>
        ''' <param name="prmBackColor">�w�i�F</param>
        ''' <remarks></remarks>
        Public Sub colChengeColor(ByVal prmColName As String, _
                                  ByVal prmBackColor As Drawing.Color)
            Try
                _grid.Columns(prmColName).DefaultCellStyle.BackColor = prmBackColor
            Catch ex As ArgumentOutOfRangeException
            End Try
        End Sub
        ''' <summary>
        ''' ��̔w�i�F��ύX����
        ''' </summary>
        ''' <param name="prmColName">�O���b�h��̗�</param>
        ''' <param name="prmBackColor">�w�i�F</param>
        ''' <param name="prmForeColor">�O�i�F</param>
        ''' <remarks></remarks>
        Public Sub colChengeColor(ByVal prmColName As String, _
                                  ByVal prmBackColor As Drawing.Color, _
                                  ByVal prmForeColor As Drawing.Color)
            Try
                _grid.Columns(prmColName).DefaultCellStyle.BackColor = prmBackColor
                _grid.Columns(prmColName).DefaultCellStyle.ForeColor = prmForeColor
            Catch ex As ArgumentOutOfRangeException
            End Try
        End Sub
        ''' <summary>
        ''' ��̔w�i�F��ύX����
        ''' </summary>
        ''' <param name="prmColName">�O���b�h��̗�</param>
        ''' <param name="prmBackColor">�w�i�F</param>
        ''' <param name="prmForeColor">�O�i�F</param>
        ''' <param name="prmSelBackColor">�I�����̔w�i�F</param>
        ''' <param name="prmSelForeColor">�I�����̑O�i�F</param>
        ''' <remarks></remarks>
        Public Sub colChengeColor(ByVal prmColName As String, _
                                  ByVal prmBackColor As Drawing.Color, _
                                  ByVal prmForeColor As Drawing.Color, _
                                  ByVal prmSelBackColor As Drawing.Color, _
                                  ByVal prmSelForeColor As Drawing.Color)
            Try
                _grid.Columns(prmColName).DefaultCellStyle.BackColor = prmBackColor
                _grid.Columns(prmColName).DefaultCellStyle.SelectionBackColor = prmSelBackColor
                _grid.Columns(prmColName).DefaultCellStyle.ForeColor = prmForeColor
                _grid.Columns(prmColName).DefaultCellStyle.SelectionForeColor = prmSelForeColor
            Catch ex As ArgumentOutOfRangeException
            End Try
        End Sub

        '-------------------------------------------------------------------------------
        '   ��w�i�F�擾
        '   �i�����T�v�j��̔w�i�F���擾����
        '   �����̓p�����^   �FprmColName          �O���b�h��̗�
        '                    �FprmRefBackColor     �w�i�F
        '                    �FprmRefForeColor     �O�i�F
        '                    �FprmRefSelBackColor  �I�����̔w�i�F
        '                    �FprmRefSelForeColor  �I�����̑O�i�F
        '   �����\�b�h�߂�l �F�Ȃ�
        '                                               2006.05.19 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' ��̔w�i�F���擾����
        ''' </summary>
        ''' <param name="prmColName">�O���b�h��̗�</param>
        ''' <param name="prmRefBackColor">�w�i�F</param>
        ''' <param name="prmRefForeColor">�O�i�F</param>
        ''' <param name="prmRefSelBackColor">�I�����̔w�i�F</param>
        ''' <param name="prmRefSelForeColor">�I�����̑O�i�F</param>
        ''' <remarks></remarks>
        Public Sub colGetColor(ByVal prmColName As String, _
                               ByRef prmRefBackColor As Drawing.Color, _
                               ByRef prmRefForeColor As Drawing.Color, _
                               ByRef prmRefSelBackColor As Drawing.Color, _
                               ByRef prmRefSelForeColor As Drawing.Color)
            Try
                prmRefBackColor = _grid.Columns(prmColName).DefaultCellStyle.BackColor
                prmRefSelBackColor = _grid.Columns(prmColName).DefaultCellStyle.SelectionBackColor
                prmRefForeColor = _grid.Columns(prmColName).DefaultCellStyle.ForeColor
                prmRefSelForeColor = _grid.Columns(prmColName).DefaultCellStyle.SelectionForeColor
            Catch ex As ArgumentOutOfRangeException
            End Try
        End Sub

        '-------------------------------------------------------------------------------
        '   �s�w�i�F�ύX
        '   �i�����T�v�j�s�̔w�i�F��ύX����
        '   �����̓p�����^   �FprmRowIdx        �Ώۍs�̃C���f�b�N�X(0�`)
        '                    �FprmBackColor     �w�i�F
        '                    �FprmForeColor     �O�i�F
        '                    �FprmSelBackColor  �I�����̔w�i�F
        '                    �FprmSelForeColor  �I�����̑O�i�F
        '   �����\�b�h�߂�l �F�Ȃ�
        '                                               2006.05.11 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �s�̔w�i�F��ύX����
        ''' </summary>
        ''' <param name="prmRowIdx">�Ώۍs�̃C���f�b�N�X</param>
        ''' <param name="prmBackColor">�w�i�F</param>
        ''' <remarks></remarks>
        Public Sub rowChengeColor(ByVal prmRowIdx As Integer, _
                                  ByVal prmBackColor As Drawing.Color)
            Try
                _grid.Rows(prmRowIdx).DefaultCellStyle.BackColor = prmBackColor
            Catch ex As ArgumentOutOfRangeException
            End Try
        End Sub
        ''' <summary>
        ''' �s�̔w�i�F��ύX����
        ''' </summary>
        ''' <param name="prmRowIdx">�Ώۍs�̃C���f�b�N�X</param>
        ''' <param name="prmBackColor">�w�i�F</param>
        ''' <param name="prmForeColor">�O�i�F</param>
        ''' <remarks></remarks>
        Public Sub rowChengeColor(ByVal prmRowIdx As Integer, _
                                  ByVal prmBackColor As Drawing.Color, _
                                  ByVal prmForeColor As Drawing.Color)
            Try
                _grid.Rows(prmRowIdx).DefaultCellStyle.BackColor = prmBackColor
                _grid.Rows(prmRowIdx).DefaultCellStyle.ForeColor = prmForeColor
            Catch ex As ArgumentOutOfRangeException
            End Try
        End Sub
        ''' <summary>
        ''' �s�̔w�i�F��ύX����
        ''' </summary>
        ''' <param name="prmRowIdx">�Ώۍs�̃C���f�b�N�X</param>
        ''' <param name="prmBackColor">�w�i�F</param>
        ''' <param name="prmForeColor">�O�i�F</param>
        ''' <param name="prmSelBackColor">�I�����̔w�i�F</param>
        ''' <param name="prmSelForeColor">�I�����̑O�i�F</param>
        ''' <remarks></remarks>
        Public Sub rowChengeColor(ByVal prmRowIdx As Integer, _
                                  ByVal prmBackColor As Drawing.Color, _
                                  ByVal prmForeColor As Drawing.Color, _
                                  ByVal prmSelBackColor As Drawing.Color, _
                                  ByVal prmSelForeColor As Drawing.Color)
            Try
                _grid.Rows(prmRowIdx).DefaultCellStyle.BackColor = prmBackColor
                _grid.Rows(prmRowIdx).DefaultCellStyle.SelectionBackColor = prmSelBackColor
                _grid.Rows(prmRowIdx).DefaultCellStyle.ForeColor = prmForeColor
                _grid.Rows(prmRowIdx).DefaultCellStyle.SelectionForeColor = prmSelForeColor
            Catch ex As ArgumentOutOfRangeException
            End Try
        End Sub

        '-------------------------------------------------------------------------------
        '   �s�w�i�F�擾
        '   �i�����T�v�j�s�̔w�i�F���擾����
        '   �����̓p�����^   �FprmRowIdx        �Ώۍs�̃C���f�b�N�X(0�`)
        '                    �FprmRefBackColor     �w�i�F
        '                    �FprmRefForeColor     �O�i�F
        '                    �FprmRefSelBackColor  �I�����̔w�i�F
        '                    �FprmRefSelForeColor  �I�����̑O�i�F
        '   �����\�b�h�߂�l �F�Ȃ�
        '                                               2006.05.19 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �s�̔w�i�F���擾����
        ''' </summary>
        ''' <param name="prmRowIdx">�Ώۍs�̃C���f�b�N�X</param>
        ''' <param name="prmRefBackColor">�w�i�F</param>
        ''' <param name="prmRefForeColor">�O�i�F</param>
        ''' <param name="prmRefSelBackColor">�I�����̔w�i�F</param>
        ''' <param name="prmRefSelForeColor">�I�����̑O�i�F</param>
        ''' <remarks></remarks>
        Public Sub rowGetColor(ByVal prmRowIdx As Integer, _
                                  ByRef prmRefBackColor As Drawing.Color, _
                                  ByRef prmRefForeColor As Drawing.Color, _
                                  ByRef prmRefSelBackColor As Drawing.Color, _
                                  ByRef prmRefSelForeColor As Drawing.Color)
            Try
                prmRefBackColor = _grid.Rows(prmRowIdx).DefaultCellStyle.BackColor
                prmRefSelBackColor = _grid.Rows(prmRowIdx).DefaultCellStyle.SelectionBackColor
                prmRefForeColor = _grid.Rows(prmRowIdx).DefaultCellStyle.ForeColor
                prmRefSelForeColor = _grid.Rows(prmRowIdx).DefaultCellStyle.SelectionForeColor
            Catch ex As ArgumentOutOfRangeException
            End Try
        End Sub

        '-------------------------------------------------------------------------------
        '   �I���s�w�i�F�ݒ�
        '   �i�����T�v�j�I���s���w��̐F�֕ύX���A�I�������ƂȂ�s�̐F���f�t�H���g�֖߂�
        '   �����̓p�����^   �FprmNewRowIdx        �I���s�̃C���f�b�N�X(0�`)
        '                    �FprmOldRowIdx        �I�������s�̃C���f�b�N�X(0�`)
        '                    �FprmRefBackColor     �w�i�F
        '                    �FprmRefForeColor     �O�i�F
        '                    �FprmRefSelBackColor  �I�����̔w�i�F
        '                    �FprmRefSelForeColor  �I�����̑O�i�F
        '   �����\�b�h�߂�l �F�Ȃ�
        '                                               2006.05.24 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        Public Sub setSelectionRowColor(ByVal prmNewRowIdx As Integer, _
                                        ByVal prmOldRowIdx As Integer, _
                                        ByVal prmBackColor As Drawing.Color)
            Try
                Me.setDefaultCellColor(DEFCLR_B, prmNewRowIdx, prmOldRowIdx)
                _grid.Rows(prmNewRowIdx).DefaultCellStyle.BackColor = prmBackColor
            Catch ex As ArgumentOutOfRangeException
            End Try

        End Sub
        Public Sub setSelectionRowColor(ByVal prmNewRowIdx As Integer, _
                                        ByVal prmOldRowIdx As Integer, _
                                        ByVal prmBackColor As Drawing.Color, _
                                        ByVal prmForeColor As Drawing.Color)
            Try
                Me.setDefaultCellColor(DEFCLR_BF, prmNewRowIdx, prmOldRowIdx)
                _grid.Rows(prmNewRowIdx).DefaultCellStyle.BackColor = prmBackColor
                _grid.Rows(prmNewRowIdx).DefaultCellStyle.ForeColor = prmForeColor
            Catch ex As ArgumentOutOfRangeException
            End Try
        End Sub
        Public Sub setSelectionRowColor(ByVal prmNewRowIdx As Integer, _
                                        ByVal prmOldRowIdx As Integer, _
                                        ByVal prmBackColor As Drawing.Color, _
                                        ByVal prmForeColor As Drawing.Color, _
                                        ByVal prmSelBackColor As Drawing.Color, _
                                        ByVal prmSelForeColor As Drawing.Color)
            Try
                Me.setDefaultCellColor(DEFCLR_BFS, prmNewRowIdx, prmOldRowIdx)
                _grid.Rows(prmNewRowIdx).DefaultCellStyle.BackColor = prmBackColor
                _grid.Rows(prmNewRowIdx).DefaultCellStyle.ForeColor = prmForeColor
                _grid.Rows(prmNewRowIdx).DefaultCellStyle.SelectionBackColor = prmSelBackColor
                _grid.Rows(prmNewRowIdx).DefaultCellStyle.SelectionForeColor = prmSelForeColor
            Catch ex As ArgumentOutOfRangeException
            End Try
        End Sub
        '�������\�b�h
        Private Const DEFCLR_B As Short = 0     '�w�i�F�̂�
        Private Const DEFCLR_BF As Short = 1    '�w�i�F���O�i�F�̂�
        Private Const DEFCLR_BFS As Short = 2   '�w�i�F�ƑO�i�F�ƑI�����w�i�F�ƑI�����O�i�F
        Private Sub setDefaultCellColor(ByVal prmKbn As Short, _
                                        ByVal prmNewRowIdx As Integer, _
                                        ByVal prmOldRowIdx As Integer)
            If prmKbn = DEFCLR_B Or prmKbn = DEFCLR_BF Or prmKbn = DEFCLR_BFS Then
                _grid.Rows(prmOldRowIdx).DefaultCellStyle.BackColor = _grid.Rows(prmNewRowIdx).DefaultCellStyle.BackColor
            End If
            If prmKbn = DEFCLR_BF Or prmKbn = DEFCLR_BFS Then
                _grid.Rows(prmOldRowIdx).DefaultCellStyle.ForeColor = _grid.Rows(prmNewRowIdx).DefaultCellStyle.ForeColor
            End If
            If prmKbn = DEFCLR_BFS Then
                _grid.Rows(prmOldRowIdx).DefaultCellStyle.SelectionBackColor = _grid.Rows(prmNewRowIdx).DefaultCellStyle.SelectionBackColor
                _grid.Rows(prmOldRowIdx).DefaultCellStyle.SelectionForeColor = _grid.Rows(prmNewRowIdx).DefaultCellStyle.SelectionForeColor
            End If
        End Sub

        '-------------------------------------------------------------------------------
        '   �{�^���N���b�N�s�擾
        '   �i�����T�v�j�{�^���^�̗�ɂ����āA�N���b�N���ꂽ�s�̃C���f�b�N�X��ԋp����
        '   �����̓p�����^   �Fe(DataGridViewCellEventArgs) CellContentClick�C�x���g�̃C�x���g�I�u�W�F�N�g
        '   �@�@�@�@�@�@�@�@ �FprmRefRowIdx                 �����{�^���sIdx(0�`)
        '   �����\�b�h�߂�l �FTrue/False       �{�^���N���b�N����Ă��邩�ۂ�
        '   �����l�@�@       �FCellContentClick�C�x���g�ŌĂяo�����ƁB
        '                    �F�{�^���N���b�N�Ŗ����ꍇ�����s�����ׁA
        '                    �F�{�^���N���b�N�̏ꍇ�̂݃��\�b�h�߂�l��True��ԋp����B
        '                                               2006.05.11 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �{�^���^�̗�ɂ����āA�N���b�N���ꂽ�s�̃C���f�b�N�X���擾
        ''' </summary>
        ''' <param name="e">CellContentClick�C�x���g�̃C�x���g�I�u�W�F�N�g</param>
        ''' <param name="prmRefRowIdx">�����{�^���sIdx(0�`)</param>
        ''' <returns>True/False�F�{�^���N���b�N����Ă��邩�ۂ�</returns>
        ''' <remarks></remarks>
        Public Function getClickBtn(ByVal e As System.Windows.Forms.DataGridViewCellEventArgs, _
                               ByRef prmRefRowIdx As Integer) As Boolean
            If Not (TypeOf _grid.Columns(e.ColumnIndex) Is Windows.Forms.DataGridViewButtonColumn _
               AndAlso e.RowIndex <> -1) Then
                '�{�^���N���b�N�C�x���g�ł͂Ȃ�
                Return False
            End If

            prmRefRowIdx = e.RowIndex
            Return True

        End Function

        '-------------------------------------------------------------------------------
        '   �R���{�{�b�N�X�s�ǉ�
        '   �i�����T�v�j�R���{�{�b�N�X�^�̗�փf�[�^��1���ǉ�����
        '   �����̓p�����^   �FprmColName   �O���b�h��̗�
        '   �@�@�@�@�@�@�@�@ �FprmData      �R���{�{�b�N�X�f�[�^��VO(UtilDgvCboVO)
        '   �����\�b�h�߂�l �F�Ȃ�
        '                                               2006.05.12 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �R���{�{�b�N�X��փf�[�^��1���ǉ�����
        ''' </summary>
        ''' <param name="prmColName">�O���b�h��̗�</param>
        ''' <param name="prmData">�R���{�{�b�N�X�f�[�^��VO(UtilDgvCboVO)</param>
        ''' <remarks></remarks>
        Public Sub addItem(ByVal prmColName As String, ByVal prmData As UtilDgvCboVO)
            Try
                If prmData Is Nothing Then
                    Throw (New UsrDefException("UtilDgvCboVO�̃C���X�^���X���ݒ肳��Ă��܂���"))
                End If
                '���݂̃R���{����DataTable���擾
                Dim dt As DataTable = CType(CType(_grid.Columns(prmColName), Windows.Forms.DataGridViewComboBoxColumn).DataSource, DataTable)
                Dim dsp As String = ""
                Dim val As String = ""
                If dt Is Nothing Then
                    '���݂̃R���{�̓f�[�^�����Ȃ̂�
                    dt = New DataTable()    '�f�[�^�e�[�u���𐶐�
                    dsp = prmData.name      'DisplayMenber��ݒ�
                    val = prmData.code      'ValueMenber��ݒ�
                Else
                    '���݂�DisplayMember/ValueMember���擾
                    dsp = CType(_grid.Columns(prmColName), Windows.Forms.DataGridViewComboBoxColumn).DisplayMember
                    val = CType(_grid.Columns(prmColName), Windows.Forms.DataGridViewComboBoxColumn).ValueMember
                End If

                'DataTable��VO��ǉ�
                Call addRow(dt, dsp, val, prmData)

                'DataTable���R���{�ɖ߂�
                CType(_grid.Columns(prmColName), Windows.Forms.DataGridViewComboBoxColumn).DataSource = dt
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        '�������\�b�h�FDataTable�̍ŏI�s��VO�̍s��}������
        Private Sub addRow(ByRef dt As DataTable, _
                                       ByVal DisplayMember As String, _
                                       ByVal ValueMember As String _
                                       , ByVal prmData As UtilDgvCboVO)
            Try
                Dim newRow As DataRow = dt.NewRow
                newRow(DisplayMember) = prmData.name
                newRow(ValueMember) = prmData.code

                '����DataTable�̍ŏI�s��VO��}��
                dt.Rows.InsertAt(newRow, dt.Rows.Count)

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        '-------------------------------------------------------------------------------
        '   �R���{�{�b�N�X�s�f�[�^�o�C���h
        '   �i�����T�v�j�R���{�{�b�N�X�^�̗�փf�[�^�𕡐����ǉ�����
        '   �����̓p�����^   �FprmColName       �O���b�h��̗�
        '   �@�@�@�@�@�@�@�@ �FprmDataSet       �ݒ肷��DataSet(prmTblName�ȗ�����Index=0�̃f�[�^�e�[�u�����g�p)
        '   �@�@�@�@�@�@�@�@ �FprmNonSelRowFlg  �擪�ɋ�s(���I���s)��݂��邩�ǂ����̃t���O(�݂���ꍇ�F���Y�s��I�����̓R�[�h��""�Ƃ���)
        '   �@�@�@�@�@�@�@�@ �FprmDisplayMember DataSet��̕\�����̂��i�[���Ă����̗񖼏�
        '   �@�@�@�@�@�@�@�@ �FprmValueMember   DataSet��̃R�[�h���i�[���Ă����̗񖼏�
        '   �@�@�@�@�@�@�@�@ �FprmTblName       DataSet��̎g�p����TBL�����w��
        '   �����\�b�h�߂�l �F�Ȃ�
        '   �����l�@�@       �FForm_Load�C�x���g�ȂǃR���{�ݒ莞�ɌĂяo�����ƁB
        '                                               2006.05.12 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �R���{�{�b�N�X�^�̗�փf�[�^�𕡐����ǉ�����
        ''' </summary>
        ''' <param name="prmColName">�O���b�h��̗�</param>
        ''' <param name="prmDataSet">�ݒ肷��DataSet(prmTblName�ȗ�����Index=0�̃f�[�^�e�[�u�����g�p)</param>
        ''' <param name="prmNonSelRowFlg">�擪�ɋ�s(���I���s)��݂��邩�ǂ����̃t���O(�݂���ꍇ�F���Y�s��I�����̓R�[�h��""�Ƃ���)</param>
        ''' <param name="prmDisplayMember">DataSet��̕\�����̂��i�[���Ă����̗񖼏�</param>
        ''' <param name="prmValueMember">DataSet��̃R�[�h���i�[���Ă����̗񖼏�</param>
        ''' <param name="prmTblName">DataSet��̎g�p����TBL�����w��</param>
        ''' <remarks></remarks>
        Public Sub setCboData(ByVal prmColName As String, ByVal prmDataSet As DataSet, _
                              Optional ByVal prmNonSelRowFlg As Boolean = False, _
                              Optional ByVal prmDisplayMember As String = "����", _
                              Optional ByVal prmValueMember As String = "�R�[�h", _
                              Optional ByVal prmTblName As String = "")
            Try

                Dim dt As DataTable = New DataTable()

                If prmTblName.Equals("") Then
                    '0�Ԗڂ�TBL���g�p
                    dt = prmDataSet.Tables(0)
                    If prmNonSelRowFlg Then             '��s��ݒ肷�邩
                        Call addNonSelectRow(dt, prmDisplayMember, prmValueMember)
                    End If
                Else
                    '�w���TBL���̂�TBL���g�p
                    dt = prmDataSet.Tables(prmTblName)
                    If prmNonSelRowFlg Then             '��s��ݒ肷�邩
                        Call addNonSelectRow(dt, prmDisplayMember, prmValueMember)
                    End If
                End If

                CType(_grid.Columns(prmColName), Windows.Forms.DataGridViewComboBoxColumn).DataSource = dt
                CType(_grid.Columns(prmColName), Windows.Forms.DataGridViewComboBoxColumn).DisplayMember = prmDisplayMember
                CType(_grid.Columns(prmColName), Windows.Forms.DataGridViewComboBoxColumn).ValueMember = prmValueMember

            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        '�������\�b�h�FDataTable�̐擪�s�ɋ�s��݂���
        Private Sub addNonSelectRow(ByRef dt As DataTable, _
                                       ByVal DisplayMember As String, _
                                       ByVal ValueMember As String)
            Try
                Dim newRow As DataRow = dt.NewRow
                newRow(DisplayMember) = ""
                newRow(ValueMember) = ""

                '����DataTable�̐擪�ɋ�s��}��
                dt.Rows.InsertAt(newRow, 0)

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        '-------------------------------------------------------------------------------
        '   �R���{�{�b�N�X�s�I��
        '   �i�����T�v�j�R���{�{�b�N�X�^�̗��̃R���{��I��������
        '   �����̓p�����^   �FprmRowIdx        �Ώۍs�̃C���f�b�N�X(0�`)
        '   �@�@�@�@�@�@�@�@ �FprmColName       �O���b�h��̗�
        '   �@�@�@�@�@�@�@�@ �FprmCode          �R�[�h
        '   �����\�b�h�߂�l �F�Ȃ�
        '   �����l�@�@       �F�R�[�h��������Ȃ��ꍇ�͖��I���Ƃ���B
        '                                               2006.05.12 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �R���{�{�b�N�X�^�̗��̃R���{��I��������@�R�[�h��������Ȃ��ꍇ�͖��I���Ƃ���
        ''' </summary>
        ''' <param name="prmRowIdx">�Ώۍs�̃C���f�b�N�X(0�`)</param>
        ''' <param name="prmColName">�O���b�h��̗�</param>
        ''' <param name="prmCode">�R�[�h</param>
        ''' <remarks></remarks>
        Public Sub selectItem(ByVal prmRowIdx As Integer, ByVal prmColName As String, ByVal prmCode As String)
            Try
                '���݂̃R���{����DataTable���擾
                Dim dt As DataTable = CType(CType(_grid.Columns(prmColName), Windows.Forms.DataGridViewComboBoxColumn).DataSource, DataTable)
                Dim dsp As String = ""
                Dim val As String = ""
                If dt Is Nothing Then
                    '���݂̃R���{�̓f�[�^�����Ȃ̂ŏ�������
                    Return
                Else
                    '���݂�DisplayMember/ValueMember���擾
                    dsp = CType(_grid.Columns(prmColName), Windows.Forms.DataGridViewComboBoxColumn).DisplayMember
                    val = CType(_grid.Columns(prmColName), Windows.Forms.DataGridViewComboBoxColumn).ValueMember
                End If

                Dim hitFlg As Boolean = False
                For i As Integer = 0 To dt.Rows.Count - 1 'i�̓R���{�̒���index������
                    If dt.Rows(i)(val).ToString.Equals(prmCode) Then
                        '��v
                        CType(Me.getCell(prmColName, prmRowIdx), Windows.Forms.DataGridViewComboBoxCell).Value = prmCode
                        hitFlg = True
                        Continue For
                    End If
                Next
                If Not hitFlg Then
                    '������Ȃ��̂Ŗ��I��
                    CType(Me.getCell(prmColName, prmRowIdx), Windows.Forms.DataGridViewComboBoxCell).Value = Nothing
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        '-------------------------------------------------------------------------------
        '   �R���{�{�b�N�X��̑I��l�R�[�h�擾
        '   �i�����T�v�j���ݑI������Ă��鍀�ڂ̃R�[�h���擾����
        '   �����̓p�����^   �FprmRowIdx    �s�ԍ�
        '                    �FprmColName   ��
        '   �����\�b�h�߂�l �F�I��l(�R�[�h)
        '   �����l           �F���I���̂΂����A""��ԋp
        '                                               2006.05.12 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �R���{�{�b�N�X��̑I��l�R�[�h�擾
        ''' </summary>
        ''' <param name="prmRowIdx">�s�ԍ�</param>
        ''' <param name="prmColName">��</param>
        ''' <returns>�I��l(�R�[�h)�@���I���̂΂����A""��ԋp</returns>
        ''' <remarks></remarks>
        Public Function getCode(ByVal prmRowIdx As Integer, ByVal prmColName As String) As String

            Dim ret As String = CType(Me.getCell(prmColName, prmRowIdx), Windows.Forms.DataGridViewComboBoxCell).Value
            If ret Is Nothing Then
                ret = ""
            End If
            Return ret

        End Function

        '-------------------------------------------------------------------------------
        '   �\�����擾
        '   �i�����T�v�j���ݑI������Ă��鍀�ڂ̕\�����̂��擾����
        '   �����̓p�����^   �F�Ȃ�
        '   �����\�b�h�߂�l �F�I��l(�\������)
        '   ��������O       �F�Ȃ�
        '   �����l           �F���I���̂΂����A""��ԋp
        '                                               2006.05.12 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �\�����擾�@���ݑI������Ă��鍀�ڂ̕\�����̂��擾����@���I���̂΂����A""��ԋp
        ''' </summary>
        ''' <param name="prmRowIdx">�s�ԍ�</param>
        ''' <param name="prmColName">��̖���</param>
        ''' <returns>�I��l(�\������)</returns>
        ''' <remarks></remarks>
        Public Function getName(ByVal prmRowIdx As Integer, ByVal prmColName As String) As String
            Try
                '���݂̃R���{����DataTable���擾
                Dim dt As DataTable = CType(CType(_grid.Columns(prmColName), Windows.Forms.DataGridViewComboBoxColumn).DataSource, DataTable)
                Dim dsp As String = ""
                Dim val As String = ""
                If dt Is Nothing Then
                    '���݂̃R���{�̓f�[�^�����Ȃ̂ŏ�������
                    Return ""
                Else
                    '���݂�DisplayMember/ValueMember���擾
                    dsp = CType(_grid.Columns(prmColName), Windows.Forms.DataGridViewComboBoxColumn).DisplayMember
                    val = CType(_grid.Columns(prmColName), Windows.Forms.DataGridViewComboBoxColumn).ValueMember
                End If

                For i As Integer = 0 To dt.Rows.Count - 1 'i�̓R���{�̒���index������
                    If dt.Rows(i)(val).ToString.Equals(CType(Me.getCell(prmColName, prmRowIdx), Windows.Forms.DataGridViewComboBoxCell).Value) Then
                        '��v
                        Return dt.Rows(i)(dsp)
                    End If
                Next
                Return "" '������Ȃ����Ƃ͖����͂�
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        '>--2006/11/10 ADD -STR- A.Yamazaki
        '-------------------------------------------------------------------------------
        '   �f�[�^�O���b�h�r���[�̃Z�����͂𐧌�����
        '   �i�����T�v�j�f�[�^�O���b�h�r���[�Z���̓��͐������s��
        '   �����̓p�����^   �Fprmsender    �Ăяo�����iEditingControlShowing�j�C�x���g�́usender�v�p�����[�^
        '                    �Fprme         �Ăяo�����iEditingControlShowing�j�C�x���g�́ue�v�p�����[�^
        '                    �FprmchkType�@ �`�F�b�N���@
        '                    �Fprmchkchr�@  ���͉\������i�ėp�`�F�b�N�̏ꍇ�Ɏg�p�j
        '   �����\�b�h�߂�l �F�`�F�b�N�K�v���iVO�j
        '   ��������O       �F�Ȃ�
        '   �����l           �F
        '                                               2006.11.09 Created By Akiyoshi.Yamazaki
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �f�[�^�O���b�h�r���[�̃Z�����͂𐧌�����
        ''' </summary>
        ''' <param name="prmsender">�Ăяo�����iEditingControlShowing�j�C�x���g�́usender�v�p�����[�^</param>
        ''' <param name="prme">�Ăяo�����iEditingControlShowing�j�C�x���g�́ue�v�p�����[�^</param>
        ''' <param name="prmchkType">�`�F�b�N���@</param>
        ''' <returns>�`�F�b�N�K�v���iVO�j</returns>
        ''' <remarks></remarks>
        Public Function chkCell(ByVal prmsender As Object _
                                , ByVal prme As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs _
                                , ByVal prmchkType As chkType) As UtilDgvChkCellVO


            'DataGridViewTextBoxEditingControl�̃C�x���g����������ׂ̕ϐ�
            Dim editingControl As DataGridViewTextBoxEditingControl
            Dim befData As String   '�Z���ҏW�O�f�[�^

            '�ҏW�O�̃f�[�^���擾
            If _grid.CurrentCell.Value IsNot Nothing Then
                befData = _grid.CurrentCell.Value.ToString
            Else
                befData = ""
            End If

            '�]���ȕ�������菜��
            prme.Control.Text = Replace(prme.Control.Text, "/", "")
            prme.Control.Text = Replace(prme.Control.Text, ",", "")

            'DGV�̃e�L�X�g�{�b�N�X�R���g���[���擾
            editingControl = TryCast(prme.Control, DataGridViewTextBoxEditingControl)

            If editingControl IsNot Nothing Then
                'DGV�Z���̃L�[�v���X�n���h���ɓ��̓`�F�b�N�̃C�x���g��ǉ�
                If prmchkType = chkType.Date1 Then
                    '���t�`�F�b�N�P
                    AddHandler editingControl.KeyPress, AddressOf ChkDgv_Date_KeyPress

                ElseIf prmchkType = chkType.Date2 Then
                    '���t�`�F�b�N�Q
                    AddHandler editingControl.KeyPress, AddressOf ChkDgv_Date2_KeyPress

                ElseIf prmchkType = chkType.Num Then
                    '���l�`�F�b�N
                    AddHandler editingControl.KeyPress, AddressOf ChkDgv_Num_KeyPress

                ElseIf prmchkType = chkType.Num_M Then
                    '���l�i�}�C�i�X�j�`�F�b�N
                    AddHandler editingControl.KeyPress, AddressOf ChkDgv_NumM_KeyPress

                ElseIf prmchkType = chkType.Cur Then
                    '���z�`�F�b�N
                    AddHandler editingControl.KeyPress, AddressOf ChkDgv_Num_KeyPress

                ElseIf prmchkType = chkType.Hankaku Then
                    '���p�`�F�b�N
                    AddHandler editingControl.KeyPress, AddressOf ChkDgv_Hankaku_KeyPress

                End If
            End If

            '�`�F�b�N�ɕK�v�ȏ���VO�Ɋi�[����
            Dim chkVO As New UtilDgvChkCellVO(befData, editingControl, prmchkType)

            '�`�F�b�N�ɕK�v�ȏ����i�[����VO��ԋp
            Return chkVO

        End Function
        '-------------------------------------------------------------------------------
        '   �f�[�^�O���b�h�r���[�Z���̓��͐�����̌㏈��
        '   �i�����T�v�j�f�[�^�O���b�h�r���[�Z���̓��͐����Ŋ֘A�Â����C�x���g��������A�l�̍ŏI�`�F�b�N���s��
        '   �����̓p�����^   �FprmchkVO    �`�F�b�N���i�[VO(�uEditingControlShowing�v�C�x���g�Ŏ擾)
        '   �����\�b�h�߂�l �F�Ȃ�
        '   ��������O       �F�Ȃ�
        '   �����l           �F�{���\�b�h��DGV�C�x���g�́uCellEndEdit�v����Ăяo��
        '                                               2006.11.09 Created By Akiyoshi.Yamazaki
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �f�[�^�O���b�h�r���[�Z���̓��͐�����̌㏈
        ''' </summary>
        ''' <param name="prmchkVO">�`�F�b�N���i�[VO</param>
        ''' <remarks></remarks>
        Public Sub AfterchkCell(ByVal prmchkVO As UtilDgvChkCellVO)

            If prmchkVO.EditingControl IsNot Nothing Then
                '���̓`�F�b�N������
                If prmchkVO.chkType = chkType.Date1 Then
                    '���t�`�F�b�N�P
                    RemoveHandler prmchkVO.EditingControl.KeyPress, AddressOf ChkDgv_Date_KeyPress

                ElseIf prmchkVO.chkType = chkType.Date2 Then
                    '���t�`�F�b�N�Q
                    RemoveHandler prmchkVO.EditingControl.KeyPress, AddressOf ChkDgv_Date2_KeyPress

                ElseIf prmchkVO.chkType = chkType.Num Then
                    '���l�`�F�b�N
                    RemoveHandler prmchkVO.EditingControl.KeyPress, AddressOf ChkDgv_Num_KeyPress

                ElseIf prmchkVO.chkType = chkType.Num_M Then
                    '���l�i�}�C�i�X�j�`�F�b�N
                    RemoveHandler prmchkVO.EditingControl.KeyPress, AddressOf ChkDgv_NumM_KeyPress

                ElseIf prmchkVO.chkType = chkType.Cur Then
                    '���z�`�F�b�N
                    RemoveHandler prmchkVO.EditingControl.KeyPress, AddressOf ChkDgv_Num_KeyPress

                ElseIf prmchkVO.chkType = chkType.Hankaku Then
                    '���p�`�F�b�N
                    RemoveHandler prmchkVO.EditingControl.KeyPress, AddressOf ChkDgv_Hankaku_KeyPress

                End If

                prmchkVO.EditingControl = Nothing
            End If

            '�Z���ɓ��͂�����ꍇ�͍ŏI�`�F�b�N(�`�F�b�N�~�̏ꍇ�͌��̒l�ɖ߂�)
            If IsExistString(_grid.CurrentCell.Value.ToString) = True Then

                If prmchkVO.chkType = chkType.Date1 Then
                    '���t�`�F�b�N�P
                    Dim datewk As String
                    '���t�X���b�V���ϊ������t�Ó����`�F�b�N
                    datewk = convertDateSlash(Replace(_grid.CurrentCell.Value.ToString, "/", ""))
                    If IsExistString(datewk) = True Then
                        _grid.CurrentCell.Value = datewk
                    Else
                        _grid.CurrentCell.Value = prmchkVO.befData
                    End If

                ElseIf prmchkVO.chkType = chkType.Date2 Then
                    '���t�`�F�b�N�Q
                    Dim datewk As String
                    '���t�X���b�V���ϊ������t�Ó����`�F�b�N
                    If Len(Replace(_grid.CurrentCell.Value.ToString, "/", "")) = 8 Then
                        datewk = convertDateSlash(Replace(_grid.CurrentCell.Value.ToString, "/", ""))
                        If IsExistString(datewk) = True Then
                            _grid.CurrentCell.Value = datewk
                        Else
                            _grid.CurrentCell.Value = prmchkVO.befData
                        End If
                    Else
                        _grid.CurrentCell.Value = prmchkVO.befData
                    End If

                ElseIf prmchkVO.chkType = chkType.Num Then
                    '���l�`�F�b�N
                    Dim permitChars As Char() = NUM_CHARS
                    Dim lidx As Integer = 0
                    For lidx = 1 To Len(_grid.CurrentCell.Value.ToString)
                        If Not hasPermitChars(Mid(_grid.CurrentCell.Value.ToString, lidx, 1), permitChars) Then
                            '2010/11.08 �݌Ɍv��p�ύX start nakazawa
                            '_grid.CurrentCell.Value = prmchkVO.befData
                            '2010/11.08 �݌Ɍv��p�ύX end nakazawa
                        End If
                    Next lidx

                ElseIf prmchkVO.chkType = chkType.Num_M Then
                    '���l�i�}�C�i�X�j�`�F�b�N
                    Dim permitChars As Char() = NUM_MINS_CHARS
                    Dim minsPos As Integer
                    '�}�C�i�X�̈ʒu�擾
                    minsPos = InStr(_grid.CurrentCell.Value.ToString, "-")
                    '�}�C�i�X�̏ꍇ�P�����ڂɂȂ��ꍇ�̓G���[
                    If minsPos > 0 Then
                        If minsPos <> 1 Then
                            _grid.CurrentCell.Value = prmchkVO.befData
                        End If
                    End If

                    '���l�`�F�b�N
                    Dim lidx As Integer = 0
                    For lidx = 1 To Len(_grid.CurrentCell.Value.ToString)
                        If Not hasPermitChars(Mid(_grid.CurrentCell.Value.ToString, lidx, 1), permitChars) Then
                            _grid.CurrentCell.Value = prmchkVO.befData
                        End If
                    Next lidx

                ElseIf prmchkVO.chkType = chkType.Cur Then
                    '���z�`�F�b�N
                    Dim permitChars As Char() = NUM_CHARS

                    Dim lidx As Integer = 0
                    For lidx = 1 To Len(_grid.CurrentCell.Value.ToString)
                        If Not hasPermitChars(Mid(_grid.CurrentCell.Value.ToString, lidx, 1), permitChars) Then
                            _grid.CurrentCell.Value = prmchkVO.befData
                            Exit Sub
                        End If
                    Next lidx
                    '�J���}�ҏW
                    _grid.CurrentCell.Value = Format(CDec(_grid.CurrentCell.Value.ToString), "###,#")

                ElseIf prmchkVO.chkType = chkType.Hankaku Then
                    '���p�`�F�b�N
                    Dim permitChars As Char() = HANKAKU_CHARS

                    Dim lidx As Integer = 0
                    For lidx = 1 To Len(_grid.CurrentCell.Value.ToString)
                        If Not hasPermitChars(Mid(_grid.CurrentCell.Value.ToString, lidx, 1), permitChars) Then
                            _grid.CurrentCell.Value = prmchkVO.befData
                            Exit Sub
                        End If
                    Next lidx

                End If

            End If
        End Sub
        '-------------------------------------------------------------------------------
        '  �f�[�^�O���b�h�Z�����t�^�iYYMMDD�j�̓��̓`�F�b�N�p�@�L�[�v���X�C�x���g
        '   �i�����T�v�j���t�ȊO�̕��������͕s�ɂ���
        '   �����̓p�����^�F�Ȃ�
        '   �����\�b�h�߂�l�@�F�Ȃ�
        '                                               2006.11.09 Created By Akiyoshi.Yamazaki
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' ���t�^�̓��̓`�F�b�N
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub ChkDgv_Date_KeyPress(ByVal sender As Object, _
                                                       ByVal e As System.Windows.Forms.KeyPressEventArgs)

            Const MaxLen As Short = 6   '���͉\������

            Dim myBox As TextBox = CType(sender, TextBox)
            Dim permitChars As Char() = NUM_CHARS

            'CTRL+C���͗L��
            If Char.IsControl(e.KeyChar) Then
                Return
            End If

            If Not hasPermitChars(e.KeyChar, permitChars) Then
                e.Handled = True    '�����Ă��Ȃ������͓��͋֎~
            Else
                '�����Ă��镶���ł��A���͂ł��邩���肷��
                'YY/MM/DD�`���p�̃`�F�b�N
                Dim KeyAscii As Integer = Asc(e.KeyChar) '���̃u���b�N�Ŏg���E�s�t���O�i�O�ɂ���Γ��͖����j

                '�I�𔽓]�����ōő啶���Ȃ�A����ȏ�̓��͕͂s���Ƃ���B
                If CType(sender, TextBox).SelectionLength = 0 Then
                    If CType(sender, TextBox).Text.Length >= MaxLen Then
                        KeyAscii = 0
                    End If
                End If

                '12 3 456  �J�[�\���ʒu�F�P�ڂ̂l�̑O�͂Q�B
                'YY[M]MDD�@�ɓ��͂���ꍇ�̃`�F�b�N 
                If myBox.SelectionStart = 2 Then
                    If KeyAscii <> Asc("0") And KeyAscii <> Asc("1") Then
                        '�O�ƂP�ȊO�͖���
                        KeyAscii = 0
                    End If

                    '123 4 56 �@�J�[�\���ʒu�F�Q�ڂ̂l�̑O�͂R
                    'YYM[M]DD�@�ɓ��͂���ꍇ�̃`�F�b�N 
                ElseIf myBox.SelectionStart = 3 Then

                    '�O�̕������P�Ȃ�A�O�C�P�C�Q�̂݋���
                    If myBox.Text.ToString.Substring(2, 1).Equals("1") Then
                        If KeyAscii <> Asc("0") And KeyAscii <> Asc("1") And KeyAscii <> Asc("2") Then
                            '�O�C�P�C�Q�ȊO�͖���
                            KeyAscii = 0
                        End If
                    End If

                    '�O�̕������O�Ȃ�A�P�`�X�̂݋���
                    If myBox.Text.ToString.Substring(2, 1).Equals("0") Then
                        If KeyAscii <> Asc("1") And KeyAscii <> Asc("2") And KeyAscii <> Asc("3") And _
                           KeyAscii <> Asc("4") And KeyAscii <> Asc("5") And KeyAscii <> Asc("6") And _
                           KeyAscii <> Asc("7") And KeyAscii <> Asc("8") And KeyAscii <> Asc("9") Then
                            KeyAscii = 0
                        End If
                    End If

                    '1234 5 6�@�@�J�[�\���ʒu�F�P�ڂ̂c�̑O�͂S
                    'YYMM[D]D�@�ɓ��͂���ꍇ�̃`�F�b�N 
                ElseIf myBox.SelectionStart = 4 Then
                    If KeyAscii <> Asc("0") And KeyAscii <> Asc("1") And KeyAscii <> Asc("2") And KeyAscii <> Asc("3") Then
                        KeyAscii = 0
                    End If

                    '12345 6�@ �J�[�\���ʒu�F�Q�ڂ̂c�̑O�͂T
                    'YYMMD[D]�@�ɓ��͂���ꍇ�̃`�F�b�N 
                ElseIf myBox.SelectionStart = 5 Then
                    '�O�̕������R�Ȃ�A�O�C�P�̂݋���
                    If myBox.Text.ToString.Substring(4, 1).Equals("3") Then
                        If KeyAscii <> Asc("0") And KeyAscii <> Asc("1") Then
                            KeyAscii = 0
                        End If
                    End If

                    '�O�̕������O�Ȃ�A�P�`�X�ƂO�̂݋���
                    If myBox.Text.ToString.Substring(4, 1).Equals("0") Then
                        If KeyAscii <> Asc("1") And KeyAscii <> Asc("2") And KeyAscii <> Asc("3") And _
                           KeyAscii <> Asc("4") And KeyAscii <> Asc("5") And KeyAscii <> Asc("6") And _
                           KeyAscii <> Asc("7") And KeyAscii <> Asc("8") And KeyAscii <> Asc("9") Then
                            KeyAscii = 0
                        End If
                    End If
                End If

                '����������
                If KeyAscii = 0 Then
                    e.Handled = True    '�������ς񂾂��Ƃɂ���i�����͖��������̂œ��͂������ƂȂ�j
                Else
                    e.Handled = False   '�����͏��������̂ŗL���ƂȂ���͂����B
                End If
            End If
        End Sub
        '-------------------------------------------------------------------------------
        '  �f�[�^�O���b�h�Z�����t�^�iYYYYMMDD�j�̓��̓`�F�b�N�p�@�L�[�v���X�C�x���g
        '   �i�����T�v�j���t�ȊO�̕��������͕s�ɂ���
        '   �����̓p�����^�F�Ȃ�
        '   �����\�b�h�߂�l�@�F�Ȃ�
        '                                               2006.11.09 Created By Akiyoshi.Yamazaki
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' ���t�^�̓��̓`�F�b�N
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub ChkDgv_Date2_KeyPress(ByVal sender As Object, _
                                                       ByVal e As System.Windows.Forms.KeyPressEventArgs)

            Const MaxLen As Short = 8   '���͉\������

            Dim myBox As TextBox = CType(sender, TextBox)
            Dim permitChars As Char() = NUM_CHARS

            'CTRL+C���͗L��
            If Char.IsControl(e.KeyChar) Then
                Return
            End If

            If Not hasPermitChars(e.KeyChar, permitChars) Then
                e.Handled = True    '�����Ă��Ȃ������͓��͋֎~
            Else
                '�����Ă��镶���ł��A���͂ł��邩���肷��
                'YYYYMMDD�`���p�̃`�F�b�N
                Dim KeyAscii As Integer = Asc(e.KeyChar) '���̃u���b�N�Ŏg���E�s�t���O�i�O�ɂ���Γ��͖����j

                '�I�𔽓]�����ōő啶���Ȃ�A����ȏ�̓��͕͂s���Ƃ���B
                If CType(sender, TextBox).SelectionLength = 0 Then
                    If CType(sender, TextBox).Text.Length >= MaxLen Then
                        KeyAscii = 0
                    End If
                End If

                ' 1 2345678 �J�[�\���ʒu�F�P�ڂ̂x�̑O�͂O�B
                '[Y]YYYMMDD�@�ɓ��͂���ꍇ�̃`�F�b�N 
                If myBox.SelectionStart = 0 Then
                    If KeyAscii <> Asc("1") And KeyAscii <> Asc("2") Then
                        '�P�ƂQ�ȊO�͖���
                        KeyAscii = 0
                    End If

                    '1234 5 678 �J�[�\���ʒu�F�P�ڂ̂l�̑O�͂S�B
                    'YYYY[M]MDD�@�ɓ��͂���ꍇ�̃`�F�b�N 
                ElseIf myBox.SelectionStart = 4 Then
                    If KeyAscii <> Asc("0") And KeyAscii <> Asc("1") Then
                        '�O�ƂP�ȊO�͖���
                        KeyAscii = 0
                    End If

                    '12345 6 78�@�J�[�\���ʒu�F�Q�ڂ̂l�̑O�͂T
                    'YYYYM[M]DD�@�ɓ��͂���ꍇ�̃`�F�b�N  
                ElseIf myBox.SelectionStart = 5 Then

                    '�O�̕������P�Ȃ�A�O�C�P�C�Q�̂݋���
                    If myBox.Text.ToString.Substring(4, 1).Equals("1") Then
                        If KeyAscii <> Asc("0") And KeyAscii <> Asc("1") And KeyAscii <> Asc("2") Then
                            '�O�C�P�C�Q�ȊO�͖���
                            KeyAscii = 0
                        End If
                    End If

                    '�O�̕������O�Ȃ�A�P�`�X�̂݋���
                    If myBox.Text.ToString.Substring(4, 1).Equals("0") Then
                        If KeyAscii <> Asc("1") And KeyAscii <> Asc("2") And KeyAscii <> Asc("3") And _
                           KeyAscii <> Asc("4") And KeyAscii <> Asc("5") And KeyAscii <> Asc("6") And _
                           KeyAscii <> Asc("7") And KeyAscii <> Asc("8") And KeyAscii <> Asc("9") Then
                            KeyAscii = 0
                        End If
                    End If

                    '123456 7 8�@�@�J�[�\���ʒu�F�P�ڂ̂c�̑O�͂U
                    'YYYYMM[D]D�@�ɓ��͂���ꍇ�̃`�F�b�N 
                ElseIf myBox.SelectionStart = 6 Then
                    If KeyAscii <> Asc("0") And KeyAscii <> Asc("1") And KeyAscii <> Asc("2") And KeyAscii <> Asc("3") Then
                        KeyAscii = 0
                    End If

                    '1234567 8 �J�[�\���ʒu�F�Q�ڂ̂c�̑O�͂V
                    'YYYYMMD[D]�@�ɓ��͂���ꍇ�̃`�F�b�N 
                ElseIf myBox.SelectionStart = 7 Then
                    '�O�̕������R�Ȃ�A�O�C�P�̂݋���
                    If myBox.Text.ToString.Substring(6, 1).Equals("3") Then
                        If KeyAscii <> Asc("0") And KeyAscii <> Asc("1") Then
                            KeyAscii = 0
                        End If
                    End If

                    '�O�̕������O�Ȃ�A�P�`�X�ƂO�̂݋���
                    If myBox.Text.ToString.Substring(6, 1).Equals("0") Then
                        If KeyAscii <> Asc("1") And KeyAscii <> Asc("2") And KeyAscii <> Asc("3") And _
                           KeyAscii <> Asc("4") And KeyAscii <> Asc("5") And KeyAscii <> Asc("6") And _
                           KeyAscii <> Asc("7") And KeyAscii <> Asc("8") And KeyAscii <> Asc("9") Then
                            KeyAscii = 0
                        End If
                    End If
                End If

                '����������
                If KeyAscii = 0 Then
                    e.Handled = True    '�������ς񂾂��Ƃɂ���i�����͖��������̂œ��͂������ƂȂ�j
                Else
                    e.Handled = False   '�����͏��������̂ŗL���ƂȂ���͂����B
                End If
            End If
        End Sub
        '-------------------------------------------------------------------------------
        '  �f�[�^�O���b�h�Z�����l�^�@�L�[�v���X�C�x���g
        '   �i�����T�v�j���l�ȊO�̕��������͕s�ɂ���
        '   �����̓p�����^�F�Ȃ�
        '   �����\�b�h�߂�l�@�F�Ȃ�
        '                                               2006.11.09 Created By Akiyoshi.Yamazaki
        '-------------------------------------------------------------------------------
        Public Shared Sub ChkDgv_Num_KeyPress(ByVal sender As Object _
                                           , ByVal e As System.Windows.Forms.KeyPressEventArgs)

            Dim permitChars As Char() = NUM_CHARS

            If Char.IsControl(e.KeyChar) Then
                Return                      'Ctrl+C�Ȃǂ͏������Ȃ�
            End If

            If Not hasPermitChars(e.KeyChar, permitChars) Then
                e.Handled = True                            '�����Ă��Ȃ������͓��͋֎~ 
            Else
                Dim KeyAscii As Integer = Asc(e.KeyChar)    '���͂������Ă��镶���ł��`�F�b�N���o����

                '����������
                If KeyAscii = 0 Then
                    e.Handled = True    '�������ς񂾂��Ƃɂ���i�����͖��������̂œ��͂������ƂȂ�j
                Else
                    e.Handled = False   '�����͏��������̂ŗL���ƂȂ���͂����B
                End If
            End If
        End Sub
        '-------------------------------------------------------------------------------
        '  �f�[�^�O���b�h�Z�����l�^(�}�C�i�X�����e)�@�L�[�v���X�C�x���g
        '   �i�����T�v�j���l�ȊO�̕��������͕s�ɂ���
        '   �����̓p�����^�F�Ȃ�
        '   �����\�b�h�߂�l�@�F�Ȃ�
        '                                               2006.11.09 Created By Akiyoshi.Yamazaki
        '-------------------------------------------------------------------------------
        Public Shared Sub ChkDgv_NumM_KeyPress(ByVal sender As Object _
                                           , ByVal e As System.Windows.Forms.KeyPressEventArgs)

            Dim permitChars As Char() = NUM_MINS_CHARS
            Dim myBox As TextBox = CType(sender, TextBox)

            If Char.IsControl(e.KeyChar) Then
                Return                      'Ctrl+C�Ȃǂ͏������Ȃ�
            End If

            If Not hasPermitChars(e.KeyChar, permitChars) Then
                e.Handled = True                            '�����Ă��Ȃ������͓��͋֎~ 
            Else
                Dim KeyAscii As Integer = Asc(e.KeyChar)    '���͂������Ă��镶���ł��`�F�b�N���o����

                If myBox.SelectionStart <> 0 Then
                    If KeyAscii = Asc("-") Then
                        '�擪�����ȊO���}�C�i�X�͕s��
                        KeyAscii = 0
                    End If
                End If

                '����������
                If KeyAscii = 0 Then
                    e.Handled = True    '�������ς񂾂��Ƃɂ���i�����͖��������̂œ��͂������ƂȂ�j
                Else
                    e.Handled = False   '�����͏��������̂ŗL���ƂȂ���͂����B
                End If
            End If
        End Sub
        '-------------------------------------------------------------------------------
        '  �f�[�^�O���b�h�Z�����p�p���^�@�L�[�v���X�C�x���g
        '   �i�����T�v�j���p�p�����ȊO�̕��������͕s�ɂ���
        '   �����̓p�����^�F�Ȃ�
        '   �����\�b�h�߂�l�@�F�Ȃ�
        '                                               2006.11.09 Created By Akiyoshi.Yamazaki
        '-------------------------------------------------------------------------------
        Public Shared Sub ChkDgv_Hankaku_KeyPress(ByVal sender As Object _
                                           , ByVal e As System.Windows.Forms.KeyPressEventArgs)

            Dim permitChars As Char() = HANKAKU_CHARS

            If Char.IsControl(e.KeyChar) Then
                Return                      'Ctrl+C�Ȃǂ͏������Ȃ�
            End If

            If Not hasPermitChars(e.KeyChar, permitChars) Then
                e.Handled = True                            '�����Ă��Ȃ������͓��͋֎~ 
            Else
                Dim KeyAscii As Integer = Asc(e.KeyChar)    '���͂������Ă��镶���ł��`�F�b�N���o����

                '����������
                If KeyAscii = 0 Then
                    e.Handled = True    '�������ς񂾂��Ƃɂ���i�����͖��������̂œ��͂������ƂȂ�j
                Else
                    e.Handled = False   '�����͏��������̂ŗL���ƂȂ���͂����B
                End If
            End If
        End Sub
        '-------------------------------------------------------------------------------
        'hasPermitChars�@���͉\�����̃`�F�b�N�p
        '-------------------------------------------------------------------------------
        Private Shared Function hasPermitChars(ByVal chTarget As Char, ByVal chPermits As Char()) As Boolean
            For Each ch As Char In chPermits
                If chTarget = ch Then
                    Return True
                End If
            Next ch
        End Function
        '-------------------------------------------------------------------------------
        '  ���t�X���b�V���ϊ��֐�
        '   �i�����T�v�jyyyyMMdd �� yyyy/MM/dd or yyyy/MM/dd �� yyyyMMdd
        '               yyMMdd   �� yy/MM/dd   or yy/MM/dd   �� yyMMdd
        '   �����̓p�����^�@�@�F�Ȃ�
        '   �����\�b�h�߂�l�@�F�ϊ��㕶����i���t�Ƃ��Ă��������ꍇ�͋�("")��Ԃ��܂��B�j
        '                                               2006.11.10 Created By Akiyoshi.Yamazaki
        '-------------------------------------------------------------------------------
        Private Shared Function convertDateSlash(ByVal prmstrDate As String) As String
            Dim w_Date As System.DateTime
            Dim w_Str As String = prmstrDate

            If IsExistString(w_Str) = False Then
                Return ""
            End If

            If w_Str.IndexOf("/", 0) = -1 Then
                '"/"�������ꍇ
                Try
                    If Len(prmstrDate) > 6 Then
                        w_Date = DateTime.ParseExact(prmstrDate, "yyyyMMdd", New System.Globalization.CultureInfo("ja-JP"))
                        Return w_Date.ToString("yyyy/MM/dd")
                    Else
                        w_Date = DateTime.ParseExact(prmstrDate, "yyMMdd", New System.Globalization.CultureInfo("ja-JP"))
                        Return w_Date.ToString("yy/MM/dd")
                    End If
                Catch
                    Return ""
                End Try
            Else
                Try
                    If Len(prmstrDate) > 8 Then
                        w_Date = DateTime.ParseExact(prmstrDate, "yyyy/MM/dd", New System.Globalization.CultureInfo("ja-JP"))
                        Return w_Date.ToString("yyyyMMdd")
                    Else
                        w_Date = DateTime.ParseExact(prmstrDate, "yy/MM/dd", New System.Globalization.CultureInfo("ja-JP"))
                        Return w_Date.ToString("yyMMdd")
                    End If
                Catch
                    Return ""
                End Try
            End If
        End Function
        '-------------------------------------------------------------------------------
        '  �󔒔���
        '   �i�����T�v�j�����񂪋󔒂��𔻒肷��
        '   �����̓p�����^�@�@�F�Ȃ�
        '   �����\�b�h�߂�l�@�FTrue=�󔒂ł͖���, False=��
        '                                               2006.11.10 Created By Akiyoshi.Yamazaki
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �󔒔���
        ''' </summary>
        ''' <returns>True=�󔒂ł͖���, False=��</returns>
        ''' <remarks></remarks>
        Private Shared Function IsExistString(ByVal prmstrDate As String) As Boolean
            'Nothing����
            If IsNothing(prmstrDate) = True Then
                Return False
            End If
            '�󕶎�����
            If prmstrDate Is String.Empty Then
                Return False
            End If
            '""��������
            If "".Equals(prmstrDate.Trim) Then
                Return False
            End If
            Return True
        End Function
        '<--2006/11/10 ADD -END- A.Yamazaki

        '-------------------------------------------------------------------------------
        '  �^�u�L�[����������
        '   �i�����T�v�j�^�u�L�[�������A�s�ړ��iUP�L�[�ADOWN�L�[����j����
        '               �擪�y�эŏI�s�ł͑O��̃R���g���[���Ɉړ�����
        '   �����̓p�����^�@�@�FprmForm    �t�H�[�J�X������s���t�H�[��
        '                     �FprmEvent   KeyPress�C�x���g
        '   �����\�b�h�߂�l�@�F�Ȃ�
        '                                               2018.03.07 Created By Yuichi.Kanno
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �^�u�L�[���������� �^�u�L�[�������A�s�ړ��iUP�L�[�ADOWN�L�[����j����
        ''' </summary>
        ''' <param name="prmForm">�t�H�[�J�X������s���t�H�[��</param>
        ''' <param name="prmEvent">KeyPress�C�x���g</param>
        ''' <remarks></remarks>
        Public Sub gridTabKeyDown(ByVal prmForm As Form, ByVal prmEvent As System.Windows.Forms.KeyEventArgs)

            If prmEvent.KeyData = Keys.Tab Then
                '�����L�[��Tab�̏ꍇ

                Dim idx As Integer
                '�ꗗ�I���s�C���f�b�N�X�̎擾
                For Each c As DataGridViewRow In _grid.SelectedRows
                    idx = c.Index
                    Exit For
                Next c

                '�ꗗ�̍ŏI�s�̏ꍇ
                If idx = _grid.RowCount - 1 Then
                    '���̃R���g���[���փt�H�[�J�X�ړ�
                    prmForm.SelectNextControl(prmForm.ActiveControl, True, True, True, True)
                Else
                    'DOWN�L�[����������
                    SendKeys.Send("{DOWN}")
                End If

                'Tab�L�[����������
                prmEvent.Handled = True

            ElseIf (prmEvent.Modifiers And Keys.Shift) = Keys.Shift Then
                If prmEvent.KeyCode = Keys.Tab Then
                    '�����L�[��Shift + Tab�̏ꍇ

                    Dim idx As Integer
                    '�ꗗ�I���s�C���f�b�N�X�̎擾
                    For Each c As DataGridViewRow In _grid.SelectedRows
                        idx = c.Index
                        Exit For
                    Next c

                    '�ꗗ�̐擪�s�̏ꍇ
                    If idx = 0 Then
                        '�O�̃R���g���[���փt�H�[�J�X�ړ�
                        prmForm.SelectNextControl(prmForm.ActiveControl, False, True, True, True)
                    Else
                        'UP�L�[����������
                        SendKeys.Send("{UP}")
                    End If

                    'Shift + Tab�L�[����������
                    prmEvent.Handled = True
                End If
            End If

        End Sub

    End Class

    '===============================================================================
    '
    '  ���[�e�B���e�B�N���X
    '    �i�N���X���j    UtilDgvCboVO
    '    �i�����@�\���j      UtilDataGridViewHandler�ɓn���R���{�{�b�N�X�f�[�^�̘g���(Beans)
    '    �i�{MDL�g�p�O��j   UtilDataGridViewHandler�Ƒ΂Ŏg�p����
    '    �i���l�j            ��L�g�p�O����UtilDataGridViewHandler�Ɠ���SRC��ɒ�`
    '
    '===============================================================================
    '  ����  ���O          ��  �t      �}�[�N      ���e
    '-------------------------------------------------------------------------------
    '  (1)   Jun.Takagi    2006/05/12              �V�K
    '-------------------------------------------------------------------------------
    Public Class UtilDgvCboVO
        '===============================================================================
        '�����o�[�ϐ���`
        '===============================================================================
        Private _code As String
        Private _name As String

        '===============================================================================
        '�v���p�e�B(�A�N�Z�T)
        '===============================================================================
        Public ReadOnly Property code()
            'Geter--------
            Get
                code = _code
            End Get
        End Property
        Public ReadOnly Property name()
            'Geter--------
            Get
                name = _name
            End Get
        End Property

        '===============================================================================
        ' �R���X�g���N�^
        '   �����̓p�����^   �FprmCode    ���̃C���X�^���X���\�����ڂ̃R�[�h
        '                   �FprmName    ���̃C���X�^���X���\�����ڂ̕\������
        '===============================================================================
        ''' <summary>
        ''' �O���b�h�n���h���ւ̎󂯓n���f�[�^���C���X�^���X������
        ''' </summary>
        ''' <param name="prmCode">�R�[�h</param>
        ''' <param name="prmName">����</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal prmCode As String, ByVal prmName As String)
            _code = prmCode
            _name = prmName
        End Sub

        '===============================================================================
        ' �I�[�o�[���C�h���\�b�h
        '   �i�����T�v�j�R���{�{�b�N�X�ɕ\������J�������w��
        '===============================================================================
        ''' <summary>
        ''' �R���{�{�b�N�X�ɕ\�����镶���������킷
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overrides Function ToString() As String
            ToString = _name '�\�����̂�ԋp
        End Function

    End Class
    '>--2006/11/10 ADD -STR- A.Yamazaki
    '===============================================================================
    '
    '  ���[�e�B���e�B�N���X
    '    �i�N���X���j    UtilDgvchkCellVO
    '    �i�����@�\���j      UtilDataGridViewHandler�ɓn���Z���̃`�F�b�N���̘g���(Beans)
    '    �i�{MDL�g�p�O��j   UtilDataGridViewHandler�Ƒ΂Ŏg�p����
    '    �i���l�j            ��L�g�p�O����UtilDataGridViewHandler�Ɠ���SRC��ɒ�`
    '
    '===============================================================================
    '  ����  ���O          ��  �t      �}�[�N      ���e
    '-------------------------------------------------------------------------------
    '  (1)   A.Yamazaki    2006/11/10              �V�K
    '-------------------------------------------------------------------------------
    Public Class UtilDgvChkCellVO
        '===============================================================================
        '�����o�[�ϐ���`
        '===============================================================================
        Private _befData As String  '�ҏW�O�f�[�^
        Private _EditingControl As DataGridViewTextBoxEditingControl    'DataGridViewTextBoxEditingControl�̃C�x���g����������ׂ̕ϐ�
        Private _chkType As Integer '�Z���`�F�b�N���@
        '===============================================================================
        '�v���p�e�B(�A�N�Z�T)
        '===============================================================================
        Public Property befData() As String
            'Geter--------
            Get
                befData = _befData
            End Get
            'Setter-------
            Set(ByVal Value As String)
                _befData = Value
            End Set
        End Property
        Public Property EditingControl() As DataGridViewTextBoxEditingControl
            'Geter--------
            Get
                EditingControl = _EditingControl
            End Get
            'Setter-------
            Set(ByVal Value As DataGridViewTextBoxEditingControl)
                _EditingControl = Value
            End Set
        End Property
        Public Property chkType() As Integer
            'Geter--------
            Get
                chkType = _chkType
            End Get
            'Setter-------
            Set(ByVal Value As Integer)
                _chkType = Value
            End Set
        End Property
        '===============================================================================
        ' �R���X�g���N�^
        '   �����̓p�����^   �FprmbefData    �ҏW�O�f�[�^
        '                    �FprmEditingControl    �ҏW�Z���̃R���g���[��
        '                  �@�FprmchkType    �`�F�b�N���@
        '===============================================================================
        ''' <summary>
        ''' �O���b�h�n���h���ւ̎󂯓n���f�[�^���C���X�^���X������
        ''' </summary>
        ''' <param name="prmbefData">�ҏW�O�f�[�^</param>
        ''' <param name="prmEditingControl">�ҏW�Z���̃R���g���[��</param>
        ''' <param name="prmchkType">�`�F�b�N���@</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal prmbefData As String, ByVal prmEditingControl As DataGridViewTextBoxEditingControl, ByVal prmchkType As Integer)
            _befData = prmbefData
            _EditingControl = prmEditingControl
            _chkType = prmchkType
        End Sub

        '===============================================================================
        ' �I�[�o�[���C�h���\�b�h
        '   �i�����T�v�jVO�̊i�[�f�[�^��\������
        '===============================================================================
        ''' <summary>
        ''' VO�S�̂�\�����镶���������킷
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overrides Function ToString() As String
            ToString = _EditingControl.Text '�\�����̂�ԋp
        End Function

    End Class
    '<--2006/11/10 ADD -END- A.Yamazaki
End Namespace
