Public Class clsCityO
    Inherits clsDatabaseObjectO
#Region "Declarations"

#End Region

#Region "Properties"

#End Region

#Region "Methods"
    Public Sub New()
        Me.TableName = "tblCity"
        Me.TableIDFieldName = "City_ID"
        Me.TableNameFieldName = "City_Name"

    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String
        Return "City_Name"
    End Function
    Protected Overrides Function OnGetInsertValues() As String
        Return clsDBStrings.SingleQot(Name)
    End Function
    Protected Overrides Function OnGetUpdateValues() As String
        Return "City_Name=" & clsDBStrings.SingleQot(Name)
    End Function
    Protected Overrides Function OnRefreshFields(ByRef rdrReader As System.Data.SqlClient.SqlDataReader) As Boolean
        Me.Name = CStr(rdrReader.Item("City_Name").ToString())
    End Function

#End Region

End Class


Public Class clsCityS
    Inherits clsDatabaseObjectS
    Public Sub New()
        Me.TableName = "tblCity"
        Me.TableIDFieldName = "City_ID"
        Me.TableNameFieldName = "City_Name"

    End Sub
End Class