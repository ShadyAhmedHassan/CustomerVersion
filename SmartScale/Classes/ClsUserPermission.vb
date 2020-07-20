Public Class ClsUserPermissionO
    Inherits clsDatabaseObjectO
#Region "Declarations"

    Private mobjUser_ID As clsUserO
    Private mobjPermission_ID As clsPermissionO
    Private mobjAllow As Boolean

#End Region
#Region "Properties"
    Public Property User_ID() As clsUserO
        Get
            Return mobjUser_ID
        End Get
        Set(ByVal Value As clsUserO)
            mobjUser_ID = Value
        End Set
    End Property
    Public Property Permission_ID() As clsPermissionO
        Get
            Return mobjPermission_ID
        End Get
        Set(ByVal Value As clsPermissionO)
            mobjPermission_ID = Value
        End Set
    End Property
    Public Property Allow() As Boolean
        Get
            Return mobjAllow
        End Get
        Set(ByVal Value As Boolean)
            mobjAllow = Value
        End Set
    End Property

#End Region

#Region "Methods"
    Public Sub New()
        Me.TableName = "tblUserPermission"
        Me.TableIDFieldName = "User_Permission_ID"
        mobjUser_ID = New clsUserO
        mobjPermission_ID = New clsPermissionO
        Me.TableNameFieldName = ""
    End Sub
    Protected Overrides Sub Finalize()
        mobjUser_ID = Nothing
        mobjPermission_ID = Nothing
        MyBase.Finalize()
    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Call Me.User_ID.Refresh()
        Call Me.Permission_ID.Refresh()
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String
        Return "User_ID,Permission_ID,Allow"
    End Function
    Protected Overrides Function OnGetInsertValues() As String
        Return (User_ID.ID) _
        & "," & (Permission_ID.ID) _
        & ",'" & (Allow) & "'"

    End Function
    Protected Overrides Function OnGetUpdateValues() As String
        Return "User_ID=" & (User_ID.ID) _
        & ",Permission_ID=" & (Permission_ID.ID) _
        & ",Allow='" & (Allow) & "'"

    End Function
    Protected Overrides Function OnRefreshFields(ByRef rdrReader As System.Data.SqlClient.SqlDataReader) As Boolean
        Me.User_ID.ID = CInt(rdrReader.Item("User_ID").ToString())
        Me.Permission_ID.ID = CInt(rdrReader.Item("Permission_ID").ToString())
        Me.Allow = CType(rdrReader.Item("Allow").ToString(), Boolean)

    End Function


#End Region

End Class
Public Class clsUserPermissionS
    Inherits clsDatabaseObjectS
    Public Sub New()
        Me.TableName = "tblUserPermission"
        Me.TableIDFieldName = "User_Permission_ID"
        Me.TableNameFieldName = ""
    End Sub
End Class