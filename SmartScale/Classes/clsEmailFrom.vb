Public Class clsEmailFrom
    Inherits clsDatabaseObjectO
#Region "Declarations"
    Private mstrEmail As String
    Private mstrPassword As String
    Private mstrPort As String
    Private mblnEnableSsl As Boolean
    Private mstrHost As String
    Private mblnUseDefaultCredentials As Boolean
    Private mstrDisplayName As String
#End Region

#Region "Properties"
    Public Property Email() As String
        Get
            Return mstrEmail
        End Get
        Set(ByVal value As String)
            mstrEmail = value
        End Set
    End Property

    Public Property Password() As String
        Get
            Return mstrPassword
        End Get
        Set(ByVal Value As String)
            mstrPassword = Value
        End Set
    End Property
   
    Public Property EnableSsl() As Boolean
        Get
            Return mblnEnableSsl
        End Get
        Set(ByVal Value As Boolean)
            mblnEnableSsl = Value
        End Set
    End Property

    Public Property UseDefaultCredentials() As Boolean
        Get
            Return mblnUseDefaultCredentials
        End Get
        Set(ByVal Value As Boolean)
            mblnUseDefaultCredentials = Value
        End Set
    End Property


    Public Property Port() As String
        Get
            Return mstrPort
        End Get
        Set(ByVal Value As String)
            mstrPort = Value
        End Set
    End Property

    Public Property Host() As String
        Get
            Return mstrHost
        End Get
        Set(ByVal Value As String)
            mstrHost = Value
        End Set
    End Property

    Public Property DisplayName() As String
        Get
            Return mstrDisplayName
        End Get
        Set(ByVal Value As String)
            mstrDisplayName = Value
        End Set
    End Property

#End Region

#Region "Methods"
    Public Sub New()
        Me.TableName = "tblFromEmail"
        Me.TableIDFieldName = "ID"
    End Sub
    Protected Overrides Sub Finalize()

        MyBase.Finalize()
    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String

        Return "[Email],[Password],[Port],[EnableSsl],[Host],[DisplayName],[UseDefaultCredentials]"


    End Function
    Protected Overrides Function OnGetInsertValues() As String

        Return clsDBStrings.SingleQot(Email) _
               & "," & clsDBStrings.SingleQot(Password) _
               & "," & clsDBStrings.SingleQot(Port) _
               & "," & clsDBStrings.ToDBBoolean(EnableSsl) _
               & "," & clsDBStrings.SingleQot(Host) _
                 & "," & clsDBStrings.SingleQot(DisplayName) _
                    & "," & clsDBStrings.ToDBBoolean(UseDefaultCredentials)


    End Function
    Protected Overrides Function OnGetUpdateValues() As String

        Return "Email=" & clsDBStrings.SingleQot(Email) _
                      & ",Password=" & clsDBStrings.SingleQot(Password) _
                      & ",Port=" & clsDBStrings.SingleQot(Port) _
                      & ",EnableSsl=" & clsDBStrings.ToDBBoolean(EnableSsl) _
                      & ",Host=" & clsDBStrings.SingleQot(Host) _
                      & ",DisplayName=" & clsDBStrings.SingleQot(DisplayName) _
                      & ",UseDefaultCredentials=" & clsDBStrings.ToDBBoolean(UseDefaultCredentials)
    End Function
    Protected Overrides Function OnRefreshFields(ByRef rdrReader As System.Data.SqlClient.SqlDataReader) As Boolean
        Me.Email = CStr(rdrReader.Item("Email").ToString())
        Me.Password = CType(rdrReader.Item("Password").ToString(), String)
        Me.Port = CType(rdrReader.Item("Port").ToString(), String)
        Me.EnableSsl = CType(rdrReader.Item("EnableSsl").ToString, Boolean)
        Me.Host = CType(rdrReader.Item("Host").ToString(), String)
        Me.DisplayName = CType(rdrReader.Item("DisplayName").ToString(), String)
        Me.UseDefaultCredentials = CType(rdrReader.Item("UseDefaultCredentials").ToString, Boolean)
    End Function


#End Region

End Class
Public Class clsFromEmailS
    Inherits clsDatabaseObjectS
    Public Sub New()
        Me.TableName = "tblFromEmail"
        Me.TableIDFieldName = "ID"


    End Sub
End Class
