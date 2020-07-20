Public Class clsPermissionO
    Inherits clsDatabaseObjectO



#Region "Methods"
    Public Sub New()
        Me.TableName = "tblPermission"
        Me.TableIDFieldName = "Permission_ID"
        Me.TableNameFieldName = "Permission_Name"
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String
        Return "Permission_Name"
    End Function
    Protected Overrides Function OnGetInsertValues() As String
        Return (Name)

    End Function
    Protected Overrides Function OnGetUpdateValues() As String
        Return "Permission_Name=" & (Name)

    End Function
    Protected Overrides Function OnRefreshFields(ByRef rdrReader As System.Data.SqlClient.SqlDataReader) As Boolean
        Me.Name = CType(rdrReader.Item("Permission_Name").ToString(), String)

    End Function


#End Region

End Class
Public Class clsPermissionS
    Inherits clsDatabaseObjectS
    Public Sub New()
        Me.TableName = "tblPermission"
        Me.TableIDFieldName = "Permission_ID"
        Me.TableNameFieldName = ""
    End Sub
End Class