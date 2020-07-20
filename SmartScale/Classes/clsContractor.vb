Public Class clsContractorO
    Inherits clsDatabaseObjectO
#Region "Declarations"

    Private mstrNotes As String

#End Region
#Region "Properties"
    Public Property Notes() As String
        Get
            Return mstrNotes
        End Get
        Set(ByVal Value As String)
            mstrNotes = Value
        End Set
    End Property
    

#End Region

#Region "Methods"
    Public Sub New()
        Me.TableName = "tblContractor"
        Me.TableIDFieldName = "Contractor_ID"
        Me.TableNameFieldName = "Contractor_Name"
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String
        Return "Contractor_Name,Notes"
    End Function
    Protected Overrides Function OnGetInsertValues() As String
        Return "'" & (Me.Name) _
        & "','" & (Notes) & "'"

    End Function
    Protected Overrides Function OnGetUpdateValues() As String
        Return "Contractor_Name=" & clsDBStrings.SingleQot(Me.Name) _
        & ",Notes=" & clsDBStrings.SingleQot(Notes)

    End Function
    'Protected Overrides Function OnRefreshFields(ByRef ds As dataset) As Boolean
    '    Me.RoleID = CType(ds.Tables(0).Rows(0).item("RoleID").ToString(), Integer)
    '    Me.UserID = CType(ds.Tables(0).Rows(0).item("UserID").ToString(), Integer)

    'End Function


#End Region

End Class
Public Class clsContractorS
    Inherits clsDatabaseObjectS
    Public Sub New()
        Me.TableName = "tblContractor"
        Me.TableIDFieldName = "Contractor_ID"
        Me.TableNameFieldName = "Contractor_Name"
    End Sub
End Class