Public Class ClsPorts
    Inherits clsDatabaseObjectO

#Region "Methods"
    Public Sub New()
        Me.TableName = "tblPorts"
        Me.TableIDFieldName = "Port_CD"
        Me.TableNameFieldName = "Port_Name"

    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String
        Return "Port_CD,Port_Name"
    End Function
    Protected Overrides Function OnGetInsertValues() As String
        Return (ID) _
           & "," & clsDBStrings.SingleQot(Name)

    End Function
    Protected Overrides Function OnGetUpdateValues() As String
        Return "Port_CD=" & (Me.ID) _
        & ",Port_Name=" & clsDBStrings.SingleQot(Name)

    End Function
    Protected Overrides Function OnRefreshFields(ByRef rdrReader As System.Data.SqlClient.SqlDataReader) As Boolean
        Me.Name = CStr(rdrReader.Item("Port_Name").ToString())

    End Function


#End Region
End Class

Public Class ClsPortsS
    Inherits clsDatabaseObjectS
    Public Sub New()
        Me.TableName = "tblPorts"
        Me.TableIDFieldName = "Port_CD"
        Me.TableNameFieldName = "Port_Name"

    End Sub
End Class
