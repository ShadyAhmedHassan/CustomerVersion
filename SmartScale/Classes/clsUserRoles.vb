Public Class clstblUserRolesO
    Inherits clsDatabaseObjectO
#Region "Declarations"
    Private mintRoleID As Integer
    Private mintUserID As Integer

#End Region
#Region "Properties"
    Public Property RoleID() As Integer
        Get
            Return mintRoleID
        End Get
        Set(ByVal Value As Integer)
            mintRoleID = Value
        End Set
    End Property
    Public Property UserID() As Integer
        Get
            Return mintUserID
        End Get
        Set(ByVal Value As Integer)
            mintUserID = Value
        End Set
    End Property

#End Region

#Region "Methods"
    Public Sub New()
        Me.TableName = "tblUserRoles"
        Me.TableIDFieldName = "Role_UserID"
        Me.TableNameFieldName = ""
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String
        Return "RoleID,UserID"
    End Function
    Protected Overrides Function OnGetInsertValues() As String
        Return (RoleID) _
        & "," & (UserID)

    End Function
    Protected Overrides Function OnGetUpdateValues() As String
        Return "RoleID=" & (RoleID) _
        & ",UserID=" & (UserID)

    End Function
    'Protected Overrides Function OnRefreshFields(ByRef ds As dataset) As Boolean
    '    Me.RoleID = CType(ds.Tables(0).Rows(0).item("RoleID").ToString(), Integer)
    '    Me.UserID = CType(ds.Tables(0).Rows(0).item("UserID").ToString(), Integer)

    'End Function


#End Region

End Class
Public Class clstblUserRolesS
    Inherits clsDatabaseObjectS
    Public Sub New()
        Me.TableName = "tblUserRoles"
        Me.TableIDFieldName = "Role_UserID"
        Me.TableNameFieldName = ""
    End Sub
End Class