Public Class clsCountryO
    Inherits clsDatabaseObjectO
#Region "Declarations"

#End Region

#Region "Properties"

#End Region

#Region "Methods"
    Public Sub New()
        Me.TableName = "tblCountry"
        Me.TableIDFieldName = "Country_ID"
        Me.TableNameFieldName = "Country_Name"

    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String
        Return "Country_Name"
    End Function
    Protected Overrides Function OnGetInsertValues() As String
        Return clsDBStrings.SingleQot(Name)

    End Function
    Protected Overrides Function OnGetUpdateValues() As String
        Return "Country_Name=" & clsDBStrings.SingleQot(Name)

    End Function
    Protected Overrides Function OnRefreshFields(ByRef rdrReader As System.Data.SqlClient.SqlDataReader) As Boolean
        Me.Name = CStr(rdrReader.Item("Country_Name").ToString())

    End Function


#End Region

End Class
Public Class clsCountryS
    Inherits clsDatabaseObjectS
    Public Sub New()
        Me.TableName = "tblCountry"
        Me.TableIDFieldName = "Country_ID"
        Me.TableNameFieldName = "Country_Name"

    End Sub
End Class