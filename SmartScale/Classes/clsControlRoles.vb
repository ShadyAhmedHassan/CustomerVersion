Public Class clstblControlRolesO
    Inherits clsDatabaseObjectO
#Region "Declarations"
    Private mintControlID As Integer
    Private mintRoleID As Integer

#End Region
#Region "Properties"
    Public Property ControlID() As Integer
        Get
            Return mintControlID
        End Get
        Set(ByVal Value As Integer)
            mintControlID = Value
        End Set
    End Property
    Public Property RoleID() As Integer
        Get
            Return mintRoleID
        End Get
        Set(ByVal Value As Integer)
            mintRoleID = Value
        End Set
    End Property

#End Region

#Region "Methods"
    Public Sub New()
        Me.TableName = "tblControlRoles"
        Me.TableIDFieldName = "Controls_RolesID"
        Me.TableNameFieldName = ""
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String
        Return "ControlID,RoleID"
    End Function
    Protected Overrides Function OnGetInsertValues() As String
        Return (ControlID) _
        & "," & (RoleID)

    End Function
    Protected Overrides Function OnGetUpdateValues() As String
        Return "ControlID=" & (ControlID) _
        & ",RoleID=" & (RoleID)

    End Function
    'Protected Overrides Function OnRefreshFields(ByRef ds As DataSet) As Boolean
    '    Me.ControlID = CType(ds.Tables(0).Rows(0).Item("ControlID").ToString(), Integer)
    '    Me.RoleID = CType(ds.Tables(0).Rows(0).Item("RoleID").ToString(), Integer)
    'End Function
#End Region

End Class
Public Class clstblControlRolesS
    Inherits clsDatabaseObjectS
    Public Sub New()
        Me.TableName = "tblControlRoles"
        Me.TableIDFieldName = "Controls_RolesID"
        Me.TableNameFieldName = ""
    End Sub
End Class