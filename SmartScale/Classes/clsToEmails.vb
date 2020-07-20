Public Class clsToEmails
    Inherits clsDatabaseObjectO
#Region "Declarations"
    Private mstrEmail As String
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

    

#End Region

#Region "Methods"
    Public Sub New()
        Me.TableName = "tblEmails"
        Me.TableIDFieldName = "ID"
    End Sub
    Protected Overrides Sub Finalize()

        MyBase.Finalize()
    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String

        Return "[Email]"


    End Function
    Protected Overrides Function OnGetInsertValues() As String

        Return clsDBStrings.SingleQot(Email)


    End Function
    Protected Overrides Function OnGetUpdateValues() As String

        Return "Email=" & clsDBStrings.SingleQot(Email)
    End Function
    Protected Overrides Function OnRefreshFields(ByRef rdrReader As System.Data.SqlClient.SqlDataReader) As Boolean
        Me.Email = CStr(rdrReader.Item("Email").ToString())
        
    End Function


#End Region

End Class
Public Class clsEmailSS
    Inherits clsDatabaseObjectS
    Public Sub New()
        Me.TableName = "tblEmails"
        Me.TableIDFieldName = "ID"


    End Sub
End Class

