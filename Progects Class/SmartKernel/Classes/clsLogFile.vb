Public Class clsLogFile
    Private mstrFileName As String = ""
    Private mblnIsOpen As Boolean = False
    Private mstmFile As System.IO.FileStream = Nothing
    Private mblnOnFirstLogWriteHeader As Boolean = False
    Private mblnFirstWrite As Boolean = True
    Private mdatInitDateTime As Date
    Private mstrLastLogData As String = ""

    Public Sub New()
        On Error Resume Next
        mdatInitDateTime = Now
    End Sub
    Public Sub New(ByVal strFileName As String)
        Me.New()
        On Error Resume Next
        Call Me.Open(strFileName)
    End Sub

    Protected Overrides Sub Finalize()
        Me.Close()
        MyBase.Finalize()
    End Sub

    Public Property FileName() As String
        Get
            On Error Resume Next
            Return mstrFileName
        End Get
        Set(ByVal value As String)
            On Error Resume Next
            mstrFileName = value
        End Set
    End Property

    Public Property OnFirstLogWriteHeader() As Boolean
        Get
            On Error Resume Next
            Return mblnOnFirstLogWriteHeader
        End Get
        Set(ByVal value As Boolean)
            On Error Resume Next
            mblnOnFirstLogWriteHeader = value
        End Set
    End Property

    Public ReadOnly Property InitDateTime() As Date
        Get
            Return mdatInitDateTime
        End Get
    End Property

    Public Function Open(Optional ByVal strFileName As String = "") As Boolean
        On Error Resume Next
        If (strFileName.Length() > 0) Then
            'Optional parameter is set
            'Change filename property to the new file name
            Me.FileName = strFileName
        End If

        Call Me.Close()

        Call Err.Clear()
        mstmFile = New IO.FileStream(Me.FileName, IO.FileMode.Append)

        'If Err.Number = 0 Then
        '    mblnIsOpen = True
        'Else
        '    mblnIsOpen = False
        'End If

        mblnIsOpen = (Err.Number = 0)   'returns true if no errors
        Return mblnIsOpen
    End Function

    Public ReadOnly Property IsOpen() As Boolean
        Get
            On Error Resume Next
            Return mblnIsOpen
        End Get
    End Property

    Public Function Close() As Boolean
        On Error Resume Next

        If mstmFile IsNot Nothing Then
            Call mstmFile.Flush()
            Call mstmFile.Close()
            Call mstmFile.Dispose()
            mstmFile = Nothing
        End If

        mblnIsOpen = False
        Return True
    End Function

    Public Function ReOpen() As Boolean
        On Error Resume Next
        Call Me.Close()
        Return Me.Open(Me.FileName)
    End Function

    Private Function Write(ByVal strData As String, Optional ByVal blnLogDateTime As Boolean = True) As Boolean
        Dim strHeader As String = ""
        Dim abytData As Byte()
        On Error Resume Next
        If Not Me.IsOpen Then
            Return False
        End If
        mstrLastLogData = strData

        If mblnFirstWrite Then
            mblnFirstWrite = False
            If Me.OnFirstLogWriteHeader Then
                strHeader = vbCrLf & "-----------------------------------------------------------" _
                        & vbCrLf & "SmartKernel Log" _
                        & vbCrLf & "First time log at " & Now.ToString("dd/MM/yyyy HH:mm:ss") _
                        & vbCrLf & "Module Loaded at " & mdatInitDateTime.ToString("dd/MM/yyyy HH:mm:ss") _
                        & vbCrLf & "-----------------------------------------------------------" _
                        & vbCrLf

            End If
        End If

        If blnLogDateTime Then
            strData = Now.ToString("dd/MM/yyyy HH:mm:ss") & " " & strData
        End If

        Call Err.Clear()
        abytData = System.Text.Encoding.Default.GetBytes(strHeader & strData)
        Call mstmFile.Write(abytData, 0, abytData.Length)
        Call mstmFile.Flush()
        Write = (Err.Number = 0)

        ReDim abytData(0)   'Clear memory -- leave at least 1 byte
    End Function
    Public Function WriteLine(ByVal strLine As String, Optional ByVal blnLogDateTime As Boolean = True) As Boolean
        On Error Resume Next
        Return Me.Write(strLine & vbCrLf, blnLogDateTime)
    End Function

    Public ReadOnly Property LastLogData() As String
        Get
            On Error Resume Next
            Return mstrLastLogData
        End Get
    End Property

    Public Sub OnErrorWrite(ByVal strModuleName As String, ByVal strFunctionName As String, ByVal objError As ErrObject, Optional ByVal strAdditionalInfo As String = "")
        OnErrorWriteToLog(Me, strModuleName, strFunctionName, objError, strAdditionalInfo)
    End Sub

    Public Shared Sub OnErrorWriteToLog(ByRef objLog As clsLogFile, ByVal strModuleName As String, ByVal strFunctionName As String, ByVal objError As ErrObject, Optional ByVal strAdditionalInfo As String = "")
        If objLog Is Nothing Then Exit Sub

        Dim intErrorNumber As Integer = objError.Number

        If intErrorNumber = 0 Then Exit Sub
        OnErrorWriteToLog(objLog, strModuleName, strFunctionName, objError.Description, intErrorNumber, strAdditionalInfo)
    End Sub
    Public Shared Sub OnErrorWriteToLog(ByRef objLog As clsLogFile, ByVal strModuleName As String, ByVal strFunctionName As String, ByVal strErrorMessage As String, Optional ByVal intErrorNumber As Integer = 0, Optional ByVal strAdditionalInfo As String = "")
        On Error Resume Next
        Call objLog.WriteLine("at " & strModuleName & "::" & strFunctionName & " " & strErrorMessage _
                                    & IIf(strAdditionalInfo = "", "", vbCrLf & "Additional Info:" & strAdditionalInfo), True)

        On Error GoTo 0
        If intErrorNumber <> 0 Then
            Err.Number = intErrorNumber
        End If

    End Sub
End Class
