Public Class clsProtypeO
    Inherits clsDatabaseObjectO
#Region "Declarations"

#End Region

#Region "Properties"

#End Region

#Region "Methods"
    Public Sub New()
        Me.TableName = "tblProtype"
        Me.TableIDFieldName = "ProtypeID"
        Me.TableNameFieldName = "ProtypeName"

    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String
        Return "ProTypeName"
    End Function
    Protected Overrides Function OnGetInsertValues() As String
        Return clsDBStrings.SingleQot(Name)
    End Function
    Protected Overrides Function OnGetUpdateValues() As String
        Return "ProtypeName=" & clsDBStrings.SingleQot(Name)
    End Function
    Protected Overrides Function OnRefreshFields(ByRef rdrReader As System.Data.SqlClient.SqlDataReader) As Boolean
        Me.Name = CStr(rdrReader.Item("ProtypeName").ToString())
    End Function

#End Region

End Class


Public Class clsProtypeS
    Inherits clsDatabaseObjectS
    Public Sub New()
        Me.TableName = "tblProtype"
        Me.TableIDFieldName = "ProtypeID"
        Me.TableNameFieldName = "ProtypeName"

    End Sub
End Class
