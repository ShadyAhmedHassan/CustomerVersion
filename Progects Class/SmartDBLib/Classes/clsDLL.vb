Public Class clsDLL
    Public Property DatabaseConnection() As DBLib.clsDBConnection
        Get
            'On Error Resume Next
            Return gconConnection
        End Get
        Set(ByVal value As DBLib.clsDBConnection)
            gconConnection = value
        End Set
    End Property

    Public Property LogFile() As Object
        Get
            'On Error Resume Next
            Return glogLogFile
        End Get
        Set(ByVal value As Object)
            'On Error Resume Next
            glogLogFile = value
        End Set
    End Property
End Class
