Imports System.Globalization
Imports System.Text.RegularExpressions
Imports System.Windows.Forms
'===============================================================================
'
'  ���[�e�B���e�B�N���X
'    �i�N���X���j    UtilClass
'    �i�����@�\���j      ���[�e�B���e�B���\�b�h�Q
'    �i�{MDL�g�p�O��j   ���ɂȂ�
'    �i���l�j            ���\�b�h�P�ʂł̈ڐA���\�Ƃ��邽�߁AImports�錾��
'                        �s�킸�A���S�C�����O��Ԃ��g�p�̂���
'
'===============================================================================
'  ����  ���O          ��  �t      �}�[�N      ���e
'-------------------------------------------------------------------------------
'  (1)   Laevigata, Inc.    2006/05/01              �V�K
'  (2)   Laevigata, Inc.    2010/08/26              �G���[���b�Z�[�W�擾(getErrDetail)�ɔ��������ǉ�
'-------------------------------------------------------------------------------
Public Class UtilClass
    Public Shared Sub main()
    End Sub

    '-------------------------------------------------------------------------------
    '�R���X�g���N�^
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' �R���X�g���N�^
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub New() '�C���X�^���X����}��
        '��������
    End Sub

    '-------------------------------------------------------------------------------
    '   �A�v���P�[�V�������s�p�X���擾
    '   �i�����T�v�j�A�v���P�[�V�������s�p�X��ԋp����
    '   �����̓p�����^�FprmAssembly  �A�Z���u��
    '   �����\�b�h�߂�l�@�F�擾�A�v���P�[�V�������s�p�X
    '                                               2006.05.01 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' �A�v���P�[�V�������s�p�X���擾
    ''' </summary>
    ''' <param name="prmAssembly">�A�Z���u��</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getAppPath(ByVal prmAssembly As System.Reflection.Assembly) As String
        Return System.IO.Path.GetDirectoryName(prmAssembly.Location)
    End Function

    '-------------------------------------------------------------------------------
    '   �A�v���P�[�V�������̂��擾
    '   �i�����T�v�j�A�v���P�[�V�������̂�ԋp����
    '   �����̓p�����^�FprmAssembly  �A�Z���u��
    '   �����\�b�h�߂�l�@�F�擾�A�v���P�[�V��������
    '                                               2006.05.01 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' �A�v���P�[�V�������̂��擾
    ''' </summary>
    ''' <param name="prmAssembly">�A�Z���u��</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getAppName(ByVal prmAssembly As System.Reflection.Assembly) As String
        Return prmAssembly.GetName().Name
    End Function

    '-------------------------------------------------------------------------------
    '   �A�v���P�[�V������Version���擾
    '   �i�����T�v�j�v���W�F�N�g�̃v���p�e�B�̃A�v���P�[�V�����^�u-�A�Z���u�����{�^������
    '   �@�@�@�@�@�@�N�������A�Z���u���̃t�@�C���o�[�W������ԋp����
    '   �����̓p�����^�FprmAssembly  �A�Z���u��
    '   �����\�b�h�߂�l�@�F�擾Version
    '                                               2006.05.22 Updated By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' �A�v���P�[�V������Version���擾
    ''' </summary>
    ''' <param name="prmAssembly">�A�Z���u��</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getAppVersion(ByVal prmAssembly As System.Reflection.Assembly) As String

        '�A�Z���u���̃o�[�W���������擾����
        Dim v As System.Diagnostics.FileVersionInfo
        v = (System.Diagnostics.FileVersionInfo.GetVersionInfo(prmAssembly.Location))
        '-->2006.05.22 chg start by Laevigata, Inc.
        'Return v.ProductMajorPart & "." & v.ProductMinorPart & "." & v.ProductBuildPart & "." & v.ProductPrivatePart
        'Return v.ProductMajorPart & "." & v.ProductMinorPart & "." & String.Format("{0:00}", v.ProductPrivatePart)
        Return v.ProductMajorPart & "." & v.ProductMinorPart & "." & v.ProductBuildPart & "." & v.ProductPrivatePart
        '<--2006.05.22 chg end by Laevigata, Inc.

    End Function

    '-------------------------------------------------------------------------------
    '�@ �t�@�C�����݃`�F�b�N
    '   �i�����T�v�j�����̃t�@�C�������݂��邩�ǂ����𔻒�
    '   �����̓p�����^�FprmDir  ����t�@�C���t���p�X������
    '   �����\�b�h�߂�l�@�FTrue/False
    '                                               2006.05.01 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' �t�@�C�����݃`�F�b�N �����̃t�@�C�������݂��邩�ǂ����𔻒�
    ''' </summary>
    ''' <param name="prmFile">����t�@�C���t���p�X������</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Public Shared Function isFileExists(ByVal prmFile As String) As Boolean
        Return System.IO.File.Exists(prmFile)
    End Function

    '-------------------------------------------------------------------------------
    '�@ �t�H���_���݃`�F�b�N
    '   �i�����T�v�j�����̃f�B���N�g�������݂��邩�ǂ����𔻒�
    '   �����̓p�����^�FprmDir  ����f�B���N�g��������
    '   �����\�b�h�߂�l�@�FTrue/False
    '                                               2006.05.01 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' �t�H���_���݃`�F�b�N �����̃f�B���N�g�������݂��邩�ǂ����𔻒�
    ''' </summary>
    ''' <param name="prmDir">����f�B���N�g��������</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Public Shared Function isDirExists(ByVal prmDir As String) As Boolean
        Return System.IO.Directory.Exists(prmDir)
    End Function

    '-------------------------------------------------------------------------------
    '�@ �G���[���b�Z�[�W�擾
    '   �i�����T�v�jException�̏ڍ׃��b�Z�[�W���擾����
    '   �����̓p�����^�FprmException  ���b�Z�[�W���擾�����O
    '   �����\�b�h�߂�l�@�F�ҏW�ς݃G���[���b�Z�[�W
    '                                               20010.08.26 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' �G���[���b�Z�[�W�擾 Exception�̏ڍ׃��b�Z�[�W���擾����
    ''' </summary>
    ''' <param name="prmException">���b�Z�[�W���擾�����O</param>
    ''' <returns>�ҏW�ς݃G���[���b�Z�[�W</returns>
    ''' <remarks></remarks>
    Public Shared Function getErrDetail(ByVal prmException As Exception) As String
        Dim wkSorce As String = prmException.TargetSite.DeclaringType.FullName
        wkSorce = wkSorce.Replace(prmException.Source & ".", "")
        '-->2010.08.26 upd by Laevigata, Inc. #���������ǉ�
        'Return "���b�Z�[�W" & ControlChars.Tab & "�F " & prmException.Message & ControlChars.NewLine & _
        '       "������" & ControlChars.Tab & "�F " & prmException.Source & ControlChars.NewLine & _
        '       "�����ӏ�" & ControlChars.Tab & "�F " & wkSorce & " [ " & prmException.TargetSite.ToString & " ]"
        Return "���b�Z�[�W" & ControlChars.Tab & "�F " & prmException.Message & ControlChars.NewLine &
               "������" & ControlChars.Tab & "�F " & prmException.Source & ControlChars.NewLine &
               "�����ӏ�" & ControlChars.Tab & "�F " & wkSorce & " [ " & prmException.TargetSite.ToString & " ]" & ControlChars.NewLine &
               "��������" & ControlChars.Tab & "�F " & Now.ToString("G")
        '<--2010.08.26 upd by Laevigata, Inc. #���������ǉ�
    End Function

    '-------------------------------------------------------------------------------
    '�@ �t�H�[�J�X�J��
    '   �i�����T�v�j���̃R���g���[���փt�H�[�J�X�ړ����s��
    '   �����̓p�����^�FprmForm    �t�H�[�J�X������s���t�H�[��
    '                   prmEvent   KeyPress�C�x���g
    '   �����\�b�h�߂�l�@�F�Ȃ�
    '                                               2006.05.10 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' �t�H�[�J�X�J�� ���̃R���g���[���փt�H�[�J�X�ړ����s��
    ''' </summary>
    ''' <param name="prmForm">�t�H�[�J�X������s���t�H�[��</param>
    ''' <param name="prmEvent">KeyPress�C�x���g</param>
    ''' <remarks></remarks>
    Public Shared Sub moveNextFocus(ByVal prmForm As Form, ByVal prmEvent As System.Windows.Forms.KeyPressEventArgs)
        Try
            '�����L�[��Enter�̏ꍇ�A���̃R���g���[���փt�H�[�J�X�ړ�
            If prmEvent.KeyChar = Chr(Keys.Enter) Then
                prmForm.SelectNextControl(prmForm.ActiveControl, True, True, True, True)
                prmEvent.Handled = True '�L�[�����Ɋւ��鏈�����I���������Ƃ�.NET Framework�ɒʒm(Beep�����Ȃ�)
            End If

        Catch ex As Exception
            Debug.WriteLine("moveNextFocus�ŃG���[���������܂����B�F" & ex.Message)
            Debug.WriteLine(ex.StackTrace)
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@ ������f�[�^���擾(�S�p�E���p�Ή�)
    '   �i�����T�v�j�n���ꂽ������̒��������߂�(�S�p1����=2�C���p1����=1�Ōv�Z)
    '   �����̓p�����^�FsPrmStr �Ώە�����
    '   �����\�b�h�߂�l�@�F������f�[�^��(�o�C�g�P��)
    '                                               2006.05.10 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' ������f�[�^���擾(�S�p�E���p�Ή�) �n���ꂽ������̒��������߂�(�S�p1����=2�C���p1����=1�Ōv�Z)
    ''' </summary>
    ''' <param name="prmStr">�Ώە�����</param>
    ''' <returns>������f�[�^��(�o�C�g�P��)</returns>
    ''' <remarks></remarks>
    Public Shared Function getLenB(ByVal prmStr As String) As Short

        'Shift JIS�ɕϊ������Ƃ��ɕK�v�ȃo�C�g����Ԃ�
        Return System.Text.Encoding.GetEncoding("shift_jis").GetByteCount(prmStr)

    End Function

    '-------------------------------------------------------------------------------
    '�@ �f�[�^���擾(�S�p�E���p�Ή�)
    '   �i�����T�v�j��������w�肳�ꂽ�����ɕҏW����
    '               �����񁄃f�[�^�� �F ���ߕ��؂�̂�
    '               �����񁃃f�[�^�� �F �s�����X�y�[�X�l��
    '   �����̓p�����^�FprmStr(�Ώە�����)
    '                 �FprmLen(�w��f�[�^�� �c �o�C�g�P��)
    '   �����\�b�h�߂�l�@�F�ҏW������
    '                                               2006.05.10 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' �f�[�^���擾(�S�p�E���p�Ή�) ��������w�肳�ꂽ�����ɕҏW����. �����񁄃f�[�^�� �F ���ߕ��؂�̂�.  �����񁃃f�[�^�� �F �s�����X�y�[�X�l��.
    ''' </summary>
    ''' <param name="prmStr">�Ώە�����</param>
    ''' <param name="prmLen">�w��f�[�^�� �c �o�C�g�P��</param>
    ''' <returns>�ҏW������</returns>
    ''' <remarks></remarks>
    Public Shared Function setDataLen(ByVal prmStr As String, ByVal prmLen As Integer) As String
        Dim ret As String
        Const encodType As String = "shift_jis"
        If System.Text.Encoding.GetEncoding(encodType).GetByteCount(prmStr) > prmLen Then
            Dim wkStr As String = prmStr
            While System.Text.Encoding.GetEncoding(encodType).GetByteCount(wkStr) > prmLen
                wkStr = wkStr.Substring(0, wkStr.Length - 1)
            End While
            If System.Text.Encoding.GetEncoding(encodType).GetByteCount(wkStr) < prmLen Then
                wkStr = wkStr & Space(1)
            End If
            ret = wkStr
        Else
            ret = prmStr & Space(prmLen - System.Text.Encoding.GetEncoding(encodType).GetByteCount(prmStr))
        End If
        Return ret
    End Function

    '-------------------------------------------------------------------------------
    '�@ �S�p�E���p���݃`�F�b�N
    '   �i�����T�v�j�����񒆂ɔ��p�E�S�p�����݂��Ă��邩�ǂ����𔻒�
    '   �����̓p�����^�FprmStr(�Ώە�����j
    '   �����\�b�h�߂�l�@�FTRUE(�S�p���p���݂���j�^FALSE(���p�S�p���݂Ȃ�)
    '                                               2006.05.10 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' �S�p�E���p���݃`�F�b�N �����񒆂ɔ��p�E�S�p�����݂��Ă��邩�ǂ����𔻒�
    ''' </summary>
    ''' <param name="prmStr">�Ώە�����</param>
    ''' <returns>TRUE(�S�p���p���݂���j�^FALSE(���p�S�p���݂Ȃ�)</returns>
    ''' <remarks></remarks>
    Public Shared Function isSharedNWStr(ByVal prmStr As String) As Boolean
        Dim wCnt As Short
        Dim nCnt As Short
        For i As Integer = 0 To prmStr.Length - 1
            If System.Text.Encoding.GetEncoding("shift_jis").GetByteCount(prmStr.Substring(i, 1)) > 1 Then
                wCnt = wCnt + 1
            Else
                nCnt = nCnt + 1
            End If
        Next
        Dim ret As Boolean
        If wCnt <> 0 And nCnt <> 0 Then
            ret = True
        Else
            ret = False
        End If
        Return ret
    End Function

    '-------------------------------------------------------------------------------
    '�@ ���p�̂݃`�F�b�N
    '   �i�����T�v�j�����񒆂ɑS�p�����݂��Ă��邩�ǂ����𔻒�
    '   �����̓p�����^�FprmStr(�Ώە�����j
    '   �����\�b�h�߂�l�@�FTRUE(���p�̂݁j�^FALSE(�S�p����)
    '                                               2006.05.10 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' ���p�̂݃`�F�b�N �����񒆂ɑS�p�����݂��Ă��邩�ǂ����𔻒�
    ''' </summary>
    ''' <param name="prmStr">�Ώە�����</param>
    ''' <returns>TRUE(���p�̂݁j�^FALSE(�S�p����)</returns>
    ''' <remarks></remarks>
    Public Shared Function isOnlyNStr(ByVal prmStr As String) As Boolean
        Dim wCnt As Short
        Dim nCnt As Short
        For i As Integer = 0 To prmStr.Length - 1
            If System.Text.Encoding.GetEncoding("shift_jis").GetByteCount(prmStr.Substring(i, 1)) > 1 Then
                wCnt = wCnt + 1
            Else
                nCnt = nCnt + 1
            End If
        Next
        Dim ret As Boolean
        If wCnt <> 0 Then
            ret = False
        Else
            ret = True
        End If
        Return ret
    End Function

    '-------------------------------------------------------------------------------
    '�@�f�B���N�g��/�t�@�C��������
    '   �i�����T�v�j�t���p�X�̃t�@�C�������f�B���N�g��&�t�@�C�����ɕ�������
    '   �����̓p�����^�Fi prmFullPath �t���p�X
    '                 �Fo prmPath     �f�B���N�g��
    '                 �Fo prmFile     �t�@�C����
    '                                               2006.05.10 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' �f�B���N�g���E�t�@�C�������� �t���p�X�̃t�@�C�������f�B���N�g���E�t�@�C�����ɕ�������
    ''' </summary>
    ''' <param name="prmFullPath">�t���p�X</param>
    ''' <param name="prmRefPath">�f�B���N�g��</param>
    ''' <param name="prmRefFile">�t�@�C����</param>
    ''' <remarks></remarks>
    Public Shared Sub dividePathAndFile(ByVal prmFullPath As String, ByRef prmRefPath As String, ByRef prmRefFile As String)
        Dim devPos As Integer
        devPos = InStrRev(prmFullPath.Replace("/", "\"), "\")

        If devPos <= 0 Then
            prmRefFile = prmFullPath
        Else
            prmRefFile = prmFullPath.Substring(devPos)
            prmRefPath = prmFullPath.Substring(0, devPos - 1)
        End If
    End Sub

    '-------------------------------------------------------------------------------
    '�@�؎̂�
    '   �i�����T�v�j���̓p�����^�̐��l��؂�̂Ă��ĕԋp
    '   �����̓p�����^�Fi num �p�����^
    '   �����\�b�h�߂�l�@�F�����l
    '                                               2006.05.10 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' �؎̂ā@���̓p�����^�̐��l��؂�̂Ă��ĕԋp
    ''' </summary>
    ''' <param name="prmNum">�p�����^</param>
    ''' <returns>�����l</returns>
    ''' <remarks></remarks>
    Public Shared Function roundDown(ByVal prmNum As Double) As Integer
        Return Fix(prmNum)
    End Function

    '-------------------------------------------------------------------------------
    '�@�؎̂�
    '   �i�����T�v�j���̓p�����^�̐��l��؂�̂Ă��ĕԋp
    '   �����̓p�����^�Fi prmNum    �p�����^
    '               �@�Fi prmDigit  ���s��
    '   �����\�b�h�߂�l�@�F�����l
    '   �����l      �@�F0.15��������Q�ʂŐ؎̂Ă̏ꍇ�AprmDigit�� 2 �Ŏ��s
    '  �@�@�@       �@�F1520��S�̈ʂŐ؎̂Ă̏ꍇ�AprmDigit�� -3 �Ŏ��s
    '                                               2006.07.10 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' �؎̂ā@���̓p�����^�̐��l��؎̂Ă��ĕԋp
    ''' </summary>
    ''' <param name="prmNum">�p�����^</param>
    ''' <param name="prmDigit">���s��(������P�ʂŐ؎̂Ă̏ꍇ[1]�A��̈ʂ̏ꍇ[-1])</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function roundDown(ByVal prmNum As Double, ByVal prmDigit As Short) As Double
        If prmDigit = 0 Then
            Throw New UsrDefException("prmDigit�p�����^���s���ł��B0�ȊO�̒l��ݒ肵�Ă��������B")
        End If

        Dim wkDigit As Short
        If prmDigit > 0 Then
            '�����Ŏ��s
            wkDigit = prmDigit
        Else
            '�����Ŏ��s
            wkDigit = prmDigit * (-1)
            prmNum = Fix(prmNum)
        End If
        Dim multiple As Double = 1
        For i As Integer = wkDigit - 1 To 1 Step -1
            multiple = multiple * 10
        Next

        Dim ret As Double = 0
        If prmDigit > 0 Then
            '�����Ŏ��s
            prmNum = prmNum * multiple
            ret = (Fix(prmNum)) / multiple
        Else
            '�����Ŏ��s
            prmNum = prmNum / (multiple * 10)
            ret = (Fix(prmNum)) * (multiple * 10)
        End If
        Return ret
    End Function

    '-------------------------------------------------------------------------------
    '�@�؂�グ
    '   �i�����T�v�j���̓p�����^�̐��l��؂�グ���ĕԋp
    '   �����̓p�����^�Fi prmNum �p�����^
    '   �����\�b�h�߂�l�@�F�����l
    '                                               2006.05.10 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' �؂�グ�@���̓p�����^�̐��l��؂�グ���ĕԋp
    ''' </summary>
    ''' <param name="prmNum">�p�����^</param>
    ''' <returns>�����l</returns>
    ''' <remarks></remarks>
    Public Shared Function roundUp(ByVal prmNum As Double) As Integer
        Return Int(System.Math.Abs(prmNum) * -1) * (Math.Sign(prmNum) * -1)
    End Function

    '-------------------------------------------------------------------------------
    '�@�؂�グ
    '   �i�����T�v�j���̓p�����^�̐��l��؂�グ���ĕԋp
    '   �����̓p�����^�Fi prmNum    �p�����^
    '               �@�Fi prmDigit  ���s��
    '   �����\�b�h�߂�l�@�F�����l
    '   �����l      �@�F0.15��������Q�ʂŐ؂�グ�̏ꍇ�AprmDigit�� 2 �Ŏ��s
    '  �@�@�@       �@�F1520��S�̈ʂŐ؂�グ�̏ꍇ�AprmDigit�� -3 �Ŏ��s
    '                                               2006.07.10 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' �؂�グ�@���̓p�����^�̐��l��؂�グ���ĕԋp
    ''' </summary>
    ''' <param name="prmNum">�p�����^</param>
    ''' <param name="prmDigit">���s��(������P�ʂŐ؂�グ�̏ꍇ[1]�A��̈ʂ̏ꍇ[-1])</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function roundUp(ByVal prmNum As Double, ByVal prmDigit As Short) As Double
        If prmDigit = 0 Then
            Throw New UsrDefException("prmDigit�p�����^���s���ł��B0�ȊO�̒l��ݒ肵�Ă��������B")
        End If

        Dim wkDigit As Short
        If prmDigit > 0 Then
            '�����Ŏ��s
            wkDigit = prmDigit
        Else
            '�����Ŏ��s
            wkDigit = prmDigit * (-1)
            prmNum = Fix(prmNum)
        End If
        Dim multiple As Double = 1
        For i As Integer = wkDigit - 1 To 1 Step -1
            multiple = multiple * 10
        Next

        Dim ret As Double = 0
        If prmDigit > 0 Then
            '�����Ŏ��s
            prmNum = prmNum * multiple
            ret = (Int(System.Math.Abs(prmNum) * -1) * (Math.Sign(prmNum) * -1)) / multiple
        Else
            '�����Ŏ��s
            prmNum = prmNum / (multiple * 10)
            ret = (Int(System.Math.Abs(prmNum) * -1) * (Math.Sign(prmNum) * -1)) * (multiple * 10)
        End If
        Return ret
    End Function

    '-------------------------------------------------------------------------------
    '�@�l�̌ܓ�
    '   �i�����T�v�j���̓p�����^�̐��l���l�̌ܓ����ĕԋp
    '   �����̓p�����^�Fi num �p�����^
    '   �����\�b�h�߂�l�@�F�����l
    '                                               2006.05.10 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' �l�̌ܓ��@���̓p�����^�̐��l���l�̌ܓ����ĕԋp
    ''' </summary>
    ''' <param name="prmNum">�p�����^</param>
    ''' <returns>�����l</returns>
    ''' <remarks></remarks>
    Public Shared Function roundOff(ByVal prmNum As Double) As Integer
        Return Fix(prmNum + (0.5 * Math.Sign(prmNum)))
    End Function

    '-------------------------------------------------------------------------------
    '�@�l�̌ܓ�
    '   �i�����T�v�j���̓p�����^�̐��l���w�茅�Ŏl�̌ܓ����ĕԋp
    '   �����̓p�����^�Fi prmNum    �p�����^
    '               �@�Fi prmDigit  ���s��
    '   �����\�b�h�߂�l�@�F�����l
    '   �����l      �@�F0.15��������Q�ʂŎl�̌ܓ��̏ꍇ�AprmDigit�� 2 �Ŏ��s
    '  �@�@�@       �@�F1520��S�̈ʂŎl�̌ܓ��̏ꍇ�AprmDigit�� -3 �Ŏ��s
    '                                               2006.07.10 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' �l�̌ܓ��@���̓p�����^�̐��l���l�̌ܓ����ĕԋp
    ''' </summary>
    ''' <param name="prmNum">�p�����^</param>
    ''' <param name="prmDigit">���s��(������P�ʂŎl�̌ܓ��̏ꍇ[1]�A��̈ʂ̏ꍇ[-1])</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function roundOff(ByVal prmNum As Double, ByVal prmDigit As Short) As Double
        If prmDigit = 0 Then
            Throw New UsrDefException("prmDigit�p�����^���s���ł��B0�ȊO�̒l��ݒ肵�Ă��������B")
        End If

        Dim wkDigit As Short
        If prmDigit > 0 Then
            '�����Ŏ��s
            wkDigit = prmDigit
        Else
            '�����Ŏ��s
            wkDigit = prmDigit * (-1)
            prmNum = Fix(prmNum)
        End If
        Dim multiple As Double = 1
        For i As Integer = wkDigit - 1 To 1 Step -1
            multiple = multiple * 10
        Next

        Dim ret As Double = 0
        If prmDigit > 0 Then
            '�����Ŏ��s
            prmNum = prmNum * multiple
            ret = Fix((prmNum + (0.5 * Math.Sign(prmNum)))) / multiple
        Else
            '�����Ŏ��s
            prmNum = prmNum / (multiple * 10)
            ret = Fix((prmNum + (0.5 * Math.Sign(prmNum)))) * (multiple * 10)
        End If
        Return ret
    End Function

    '-------------------------------------------------------------------------------
    '�@�R���g���[���S�I����Ԑ���
    '   �i�����T�v�j���̓p�����^�̃R���g���[����S�I����ԂƂ���
    '   �����̓p�����^�Fi prmObj �ΏۃR���g���[��(TextBox,MskedTextBox��z��)
    '   �����\�b�h�߂�l�@�F�Ȃ�
    '   ���g�p��@�@�@�F     Private Sub Text1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Text1.GotFocus
    '                            Call UtilClass.selAll(Text1)
    '                        End Sub
    '                                               2006.06.09 Updated By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' �R���g���[���S�I����Ԑ����@���̓p�����^�̃R���g���[����S�I����ԂƂ���
    ''' </summary>
    ''' <param name="prmRefObj">�ΏۃR���g���[��(TextBox,MskedTextBox��z��)</param>
    ''' <remarks></remarks>
    Public Shared Sub selAll(ByRef prmRefObj As Object)
        Try

            '�e�L�X�g�{�b�N�X�֕ϊ�
            Dim wkText As TextBox = CType(prmRefObj, TextBox)
            'wkText.SelectionStart = 0
            'wkText.SelectionLength = wkText.Text.Length
            wkText.SelectAll()
        Catch ex As Exception
            Try
                '�}�X�N�h�e�L�X�g�{�b�N�X�֒u��
                Dim wkMaskedText As MaskedTextBox = CType(prmRefObj, MaskedTextBox)
                'wkMaskedText.SelectionStart = 0
                'wkMaskedText.SelectionLength = wkMaskedText.Text.Length
                wkMaskedText.SelectAll()
            Catch ex2 As Exception
                'TextBox�ł�MaskedTextBox�ł��Ȃ��ꍇ�������Ȃ�
            End Try
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '  �[�����擾
    '   �i�����T�v�j���s�[���̃R���s���[�^�����擾����
    '   �����̓p�����^�F�Ȃ�
    '   �����\�b�h�߂�l�@�F�[����
    '                                               2006.05.19 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' ���s�[���̃R���s���[�^�����擾����
    ''' </summary>
    ''' <returns>�[����</returns>
    ''' <remarks></remarks>
    Public Shared Function getComputerName() As String
        Return System.Net.Dns.GetHostName
    End Function

    '-------------------------------------------------------------------------------
    '  IP�擾
    '   �i�����T�v�j���s�[����IP���擾����
    '   �����̓p�����^�F�Ȃ�
    '   �����\�b�h�߂�l�@�FIP
    '                                               2006.05.19 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' ���s�[����IP���擾����
    ''' </summary>
    ''' <returns>IP</returns>
    ''' <remarks></remarks>
    Public Shared Function getComputerIP() As String
        Dim cn As String = System.Net.Dns.GetHostName
        Dim ipInfo As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(cn)
        Dim ipInfoAddress As System.Net.IPAddress = ipInfo.AddressList(0)
        Return ipInfoAddress.ToString
    End Function

    '-------------------------------------------------------------------------------
    '  �[�����擾(IP����擾)
    '   �i�����T�v�jIP����[�������擾����
    '   �����̓p�����^�FprmIP   IP
    '   �����\�b�h�߂�l�@�F�[����
    '   �����l�@�@�@�@�FDNS���t�������T�|�[�g���Ă��邱�ƁB ��NSL-LAN�̓T�|�[�g�O
    '                                               2006.05.19 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' IP����[�������擾����
    ''' </summary>
    ''' <param name="prmIP">IP</param>
    ''' <returns>�[����</returns>
    ''' <remarks>DNS���t�������T�|�[�g���Ă��邱�ƁB</remarks>
    Public Shared Function getComputerNameFromIP(ByVal prmIP As String) As String
        Dim hostInfo As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(prmIP)
        Return hostInfo.HostName
    End Function

    '-------------------------------------------------------------------------------
    '  IP�擾(�[��������擾)
    '   �i�����T�v�j�[��������IP���擾����
    '   �����̓p�����^�FprmComputerName   �[����
    '   �����\�b�h�߂�l�@�FIP
    '                                               2006.05.19 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' �[��������IP���擾����
    ''' </summary>
    ''' <param name="prmComputerName">�[����</param>
    ''' <returns>IP</returns>
    ''' <remarks></remarks>
    Public Shared Function getComputerIPFromName(ByVal prmComputerName As String) As String
        Dim ipInfo As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(prmComputerName)
        Dim ipInfoAddress As System.Net.IPAddress = ipInfo.AddressList(0)
        Return ipInfoAddress.ToString
    End Function

    '-------------------------------------------------------------------------------
    '  Boolean�^���擾����
    '   �i�����T�v�jBoolean��Type��ԋp����
    '   �����̓p�����^�@�@�F�Ȃ�
    '   �����\�b�h�߂�l�@�F�擾Type
    '                                               2006.05.30 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' Boolean��Type��ԋp����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getBln() As Type
        Return Type.GetType("System.Boolean")
    End Function

    '-------------------------------------------------------------------------------
    '  String�^���擾����
    '   �i�����T�v�jString��Type��ԋp����
    '   �����̓p�����^�@�@�F�Ȃ�
    '   �����\�b�h�߂�l�@�F�擾Type
    '                                               2006.05.30 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' String��Type��ԋp����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getStr() As Type
        Return Type.GetType("System.String")
    End Function

    '-------------------------------------------------------------------------------
    '  Short�^���擾����
    '   �i�����T�v�jShort��Type��ԋp����
    '   �����̓p�����^�@�@�F�Ȃ�
    '   �����\�b�h�߂�l�@�F�擾Type
    '                                               2006.05.30 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' Short��Type��ԋp����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getSho() As Type
        Return Type.GetType("System.Int16")
    End Function

    '-------------------------------------------------------------------------------
    '  Integer�^���擾����
    '   �i�����T�v�jInteger��Type��ԋp����
    '   �����̓p�����^�@�@�F�Ȃ�
    '   �����\�b�h�߂�l�@�F�擾Type
    '                                               2006.05.30 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' Integer��Type��ԋp����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getInt() As Type
        Return Type.GetType("System.Int32")
    End Function

    '-------------------------------------------------------------------------------
    '  Long�^���擾����
    '   �i�����T�v�jLong��Type��ԋp����
    '   �����̓p�����^�@�@�F�Ȃ�
    '   �����\�b�h�߂�l�@�F�擾Type
    '                                               2006.05.30 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' Long��Type��ԋp����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getLng() As Type
        Return Type.GetType("System.Int64")
    End Function

    '-------------------------------------------------------------------------------
    '  Single�^���擾����
    '   �i�����T�v�jSingle��Type��ԋp����
    '   �����̓p�����^�@�@�F�Ȃ�
    '   �����\�b�h�߂�l�@�F�擾Type
    '                                               2006.05.30 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' Single��Type��ԋp����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getSgl() As Type
        Return Type.GetType("System.Single")
    End Function

    '-------------------------------------------------------------------------------
    '  Double�^���擾����
    '   �i�����T�v�jDouble��Type��ԋp����
    '   �����̓p�����^�@�@�F�Ȃ�
    '   �����\�b�h�߂�l�@�F�擾Type
    '                                               2006.05.30 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' Double��Type��ԋp����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getDbl() As Type
        Return Type.GetType("System.Double")
    End Function

    '-------------------------------------------------------------------------------
    '  DateTime�^���擾����
    '   �i�����T�v�jDateTime��Type��ԋp����
    '   �����̓p�����^�@�@�F�Ȃ�
    '   �����\�b�h�߂�l�@�F�擾Type
    '                                               2006.05.30 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' DateTime��Type��ԋp����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getDte() As Type
        Return Type.GetType("System.DateTime")
    End Function

    '-------------------------------------------------------------------------------
    '  Object�^���擾����
    '   �i�����T�v�jObject��Type��ԋp����
    '   �����̓p�����^�@�@�F�Ȃ�
    '   �����\�b�h�߂�l�@�F�擾Type
    '                                               2006.05.30 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' Object��Type��ԋp����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getObj() As Type
        Return Type.GetType("System.Object")
    End Function

    '-------------------------------------------------------------------------------
    '  Byte�^���擾����
    '   �i�����T�v�jByte��Type��ԋp����
    '   �����̓p�����^�@�@�F�Ȃ�
    '   �����\�b�h�߂�l�@�F�擾Type
    '                                               2006.05.30 Created By Laevigata, Inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' Byte��Type��ԋp����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getBte() As Type
        Return Type.GetType("System.Byte")
    End Function
    '-------------------------------------------------------------------------------
    '  ���t�X���b�V���ϊ��֐�
    '   �i�����T�v�jyyyyMMdd �� yyyy/MM/dd or yyyy/MM/dd �� yyyyMMdd
    '               yyMMdd   �� yy/MM/dd   or yy/MM/dd   �� yyMMdd
    '   �����̓p�����^�@�@�F�Ȃ�
    '   �����\�b�h�߂�l�@�F�ϊ��㕶����i���t�Ƃ��Ă��������ꍇ�͋�("")��Ԃ��܂��B�j
    '                                               2006.11.10 Created By Laevigata inc.
    '-------------------------------------------------------------------------------
    Public Shared Function convertDateSlash(ByVal prmstrDate As String) As String
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
    '                                               2006.11.10 Created By Laevigata inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' �󔒔���
    ''' </summary>
    ''' <returns>True=�󔒂ł͖���, False=��</returns>
    ''' <remarks></remarks>
    Public Shared Function IsExistString(ByVal prmstrDate As String) As Boolean
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

    '-------------------------------------------------------------------------------
    '  �f�[�^�X�V���̓����t�H�[�}�b�g
    '   �i�����T�v�j���t�\���͊��ɍ��킹�邽�߁A�o�^���͓��{�`���ɂ���
    '   �����̓p�����^�@�@�FDatetime
    '   �����\�b�h�߂�l�@�FDatetime
    '                                               2019.02.07 Created By Laevigata inc.
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' �󔒔���
    ''' </summary>
    ''' <returns>True=�󔒂ł͖���, False=��</returns>
    ''' <remarks></remarks>
    Public Shared Function jaDatetimeFormat(ByVal prmDate As DateTime) As String

        '���{�̓��t�`���ɂ���
        Return Format(prmDate, "yyyy/MM/dd HH:mm:ss").ToString

    End Function

    'String�^��Date����{�̌`���ɒ���
    Public Shared Function strFormatDate(ByVal prmDate As String, Optional ByRef prmFormat As String = "yyyy/MM/dd") As String

        'PC�̃J���`���[���擾���A����ɉ�����String����Datetime���쐬
        Dim ci As New System.Globalization.CultureInfo(CultureInfo.CurrentCulture.Name.ToString)
        Dim dateFormat As DateTime = DateTime.Parse(prmDate, ci, System.Globalization.DateTimeStyles.AssumeLocal)

        '���{�̌`���ɏ���������
        Return dateFormat.ToString(prmFormat)
    End Function

    'Datetime�^����{�̌`���ɒ���
    Public Shared Function formatDatetime(ByVal prmDatetime As DateTime) As String

        'PC�̃J���`���[���擾���A����ɉ�����String����Datetime���쐬
        Dim ciCurrent As New System.Globalization.CultureInfo(CultureInfo.CurrentCulture.Name.ToString)
        Dim dateFormat As DateTime = DateTime.Parse(prmDatetime.ToString, ciCurrent, System.Globalization.DateTimeStyles.AssumeLocal)

        Dim changeFormat As String = dateFormat.ToString("yyyy/MM/dd HH:mm:ss")

        Dim ciJP As New System.Globalization.CultureInfo("ja-JP")
        Dim rtnDatetime As DateTime = DateTime.Parse(changeFormat, ciJP, System.Globalization.DateTimeStyles.AssumeLocal)


        '���{�̌`���ɏ���������
        Return changeFormat
    End Function

    '���z�t�H�[�}�b�g�i�o�^�̍ۂ̏����_�w��q�j����{�̌`���ɍ��킹��
    '����؂�L���͊O���i�����_�݂̂̃t�H�[�}�b�g�ɕϊ����Ă���j
    Public Shared Function formatNumber(ByVal prmVal As Decimal) As String

        Dim nfi As NumberFormatInfo = New CultureInfo("ja-JP", False).NumberFormat

        '���{�̌`���ɏ���������
        Return prmVal.ToString("F3", nfi)
    End Function

    'sql�Ŏ��s���镶���񂩂�V���O���N�H�[�e�[�V�����𕶎��R�[�h�ɂ���
    Public Shared Function escapeSql(ByVal prmSql As String) As String
        Dim sql As String = prmSql

        If prmSql IsNot Nothing Then
            sql = sql.Replace("'"c, "''") '�V���O���N�H�[�e�[�V������u��

            'Return Regex.Escape(sql)
            Return sql

        End If

        Return sql
    End Function

End Class
