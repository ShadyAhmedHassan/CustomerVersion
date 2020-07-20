Option Explicit On
Module modCodeShare
    Public gfrmMain As cfrmMain
    Public gfrmChoose As cfrmChoose
    Public gfrmAddNew As cfrmAddNew
    Public gfrmReports As cfrmReports
    Public gfrmAllReports As frmAllReports
    Public gfrmEdit As frmEdit
    Public gfrmEditRet As frmEditReturns
    Public gfrmEditFirstScale As frmEditFirstScale
    Public gfrmManualEditReport As ManualEditReport
    Public gfrmCustQnty As frmCustQnty
    Public gfrmrptCustQnty As frmrptCustQnty
    Public gfrmReturns As frmReturns
    Public gfrmCollectedMotions As cfrmCollectedMotions


#Region "Login"
    Public gobjCurrentLogin As clsLogin
#End Region

#Region "dataBaseConection"
    Public GObjConection As DBLib.clsDBConnection


#End Region


#Region "Sound Effects"
    Public Const gstrSOUND_EFFECTS_RELATIVE_PATH As String = "Sound"
    Public Const gstrTRUCK_MESSAGES_SOUND_EFFECTS_RELATIVE_PATH As String = gstrSOUND_EFFECTS_RELATIVE_PATH & "\Truck Messages"

    ''' <summary>
    ''' Returns Truck Messages sound effects' path by combining application startup path and gstrTRUCK_MESSAGES_SOUND_EFFECTS_RELATIVE_PATH
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TruckMessagesPath()
        Get
            On Error Resume Next
            Return IO.Path.Combine(Application.StartupPath, gstrTRUCK_MESSAGES_SOUND_EFFECTS_RELATIVE_PATH)
        End Get
    End Property

    Public Function GetTruckMessageFullPath(ByVal strMessage As String) As String
        On Error Resume Next
        Return IO.Path.Combine(TruckMessagesPath, strMessage & ".wav")
    End Function
#End Region

    Public GObjCarO As New clsCarO
    Public GObjDataBaseS As New DBLib.clsDatabaseObjectS
    Public glogLogFile As Kernel.clsLogFile
    Public gconConnection As DBLib.clsDBConnection

End Module
