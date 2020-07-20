Public Class clsGoodtypeO
    Inherits clsDatabaseObjectO
#Region "Declarations"

#End Region

#Region "Properties"

#End Region

#Region "Methods"
    Public Sub New()
        Me.TableName = "tblGoodtype"
        Me.TableIDFieldName = "GoodtypeID"
        Me.TableNameFieldName = "GoodtypeName"

    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String
        Return "GoodtypeName"
    End Function
    Protected Overrides Function OnGetInsertValues() As String
        Return clsDBStrings.SingleQot(Name)
    End Function
    Protected Overrides Function OnGetUpdateValues() As String
        Return "GoodtypeName=" & clsDBStrings.SingleQot(Name)
    End Function
    Protected Overrides Function OnRefreshFields(ByRef rdrReader As System.Data.SqlClient.SqlDataReader) As Boolean
        Me.Name = CStr(rdrReader.Item("GoodtypeName").ToString())
    End Function

#End Region

End Class


Public Class clsGoodtypeS
    Inherits clsDatabaseObjectS
    Public Sub New()
        Me.TableName = "tblGoodtype"
        Me.TableIDFieldName = "GoodtypeID"
        Me.TableNameFieldName = "GoodtypeName"

    End Sub
End Class
