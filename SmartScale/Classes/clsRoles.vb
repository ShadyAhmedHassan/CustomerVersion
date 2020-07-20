Public Class clstblRolesO
    Inherits clsDatabaseObjectO
#Region "Declarations"

    Private mintLevelID As Integer
#End Region
#Region "Properties"
    Public Property LevelID() As Integer
        Get
            Return mintLevelID
        End Get
        Set(ByVal Value As Integer)
            mintLevelID = Value
        End Set
    End Property
#End Region

#Region "Methods"
    Public Sub New()
        Me.TableName = "tblRoles"
        Me.TableIDFieldName = "RoleID"
        Me.TableNameFieldName = "RoleName"

    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String
        Return "RoleName"
    End Function
    Protected Overrides Function OnGetInsertValues() As String
        Return clsDBStrings.SingleQot(Name)


    End Function
    Protected Overrides Function OnGetUpdateValues() As String
        Return "RoleName=" & clsDBStrings.SingleQot(Name)


    End Function
    'Protected Overrides Function OnRefreshFields(ByRef ds As dataset) As Boolean
    '    Me.Name = CStr(ds.Tables(0).Rows(0).item("RoleName").ToString())
    '    Me.LevelID = CType(ds.Tables(0).Rows(0).item("LevelID").ToString(), Integer)

    'End Function


#End Region

End Class
Public Class clstblRolesS
    Inherits clsDatabaseObjectS
    Public Sub New()
        Me.TableName = "tblRoles"
        Me.TableIDFieldName = "RoleID"
        Me.TableNameFieldName = "RoleName"

    End Sub
End Class