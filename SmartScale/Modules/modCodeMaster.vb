Option Explicit On
Module ModCodeMaster

    Public MYpass As Boolean

    Public Sub Main()
        'On Error Resume Next
        glogLogFile = New Kernel.clsLogFile(CombineFilePath("Error.log"))
        Call InitDLLLogging()

        'gconConnection = New DBLib.clsDBConnection("Data Source=IT\SQLEXPRESS;Initial Catalog=SmartScaleDB_1;User ID=amr;Password=123")
        gconConnection = New DBLib.clsDBConnection(GetConString(Application.StartupPath & "\ConnString.txt")) '"Data Source=TH\SQLEXPRESS;Initial Catalog=SmartScaleDB_1;User ID=TH;Password=123")

        If Not gconConnection.Open() Then
            Call MsgBox("Unable to connect to database connection due to the following error:" _
                        & vbCrLf & glogLogFile.LastLogData, MsgBoxStyle.Critical)
            Call AppTerminate()
        End If

        Call InitDLLDBConnection()

        Application.EnableVisualStyles()

        gobjCurrentLogin = New clsLogin

        If DoLogin() Then
            'gAdo = New ClsAdo("IT", "SmartScaleDB_1", "Amr", "123")
            gAdo = New ClsAdo(GetServerName(), GetDataBaseName(), GetUserID(), GetPassword())

            'gfrmMain = New cfrmMain
            'Call gfrmMain.ShowDialog()
            gfrmChoose = New cfrmChoose
            gfrmChoose.ShowDialog()

        Else
            ' Call TerminateApplication()
        End If

    End Sub

    Private Sub InitDLLLogging()
        Dim objDLL As Object
        On Error Resume Next

        objDLL = New DBLib.clsDLL
        objDLL.LogFile = glogLogFile
        objDLL = Nothing

        'objDLL = New clsDLL
        'objDLL.LogFile = glogLogFile
        'objDLL = Nothing
    End Sub

    Private Sub InitDLLDBConnection()
        Dim objDLL, objDllLib As Object
        On Error Resume Next
        objDLL = New DBLib.clsDLL
        'objDllLib = New RokyLib.clsDLL
        objDLL.DatabaseConnection = gconConnection
        'objDllLib.DatabaseConnection = gconConnection
        objDLL = Nothing
        objDllLib = Nothing
    End Sub

    Public Function CombineFilePath(ByVal strFileName As String) As String
        On Error Resume Next
        Return System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), strFileName)
    End Function

    Public Sub AppTerminate()
        On Error Resume Next

        If gconConnection IsNot Nothing Then
            gconConnection = Nothing
        End If

        If glogLogFile IsNot Nothing Then
            Call glogLogFile.Close()
            glogLogFile = Nothing
        End If

        End
    End Sub

    Public Sub TerminateApplication()
        On Error Resume Next
        gfrmMain = Nothing
        gobjCurrentLogin = Nothing
        Application.Exit()
    End Sub

End Module

