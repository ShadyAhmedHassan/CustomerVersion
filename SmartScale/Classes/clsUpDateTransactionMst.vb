Public Class clsUpDateTransactionMstO
    Inherits clsDatabaseObjectO
#Region "Declarations"
    Dim mdatUpDateDate As String
    Dim mintUserID As Integer
#End Region

#Region "Properties"
    Public Property UpDateDate() As String
        Get
            Return mdatUpDateDate
        End Get
        Set(ByVal Value As String)
            mdatUpDateDate = Value
        End Set
    End Property

    Public Property User_ID() As Integer
        Get
            Return mintUserID
        End Get
        Set(ByVal Value As Integer)
            mintUserID = Value
        End Set
    End Property

#End Region
#Region "Methods"
    Public Sub New()
        Me.TableName = "tblUpDateTransactionsMst"
        Me.TableIDFieldName = "UpDateTransID"
        Me.TableNameFieldName = ""

    End Sub

    Protected Overrides Sub Finalize()

        mdatUpDateDate = Nothing
        mintUserID = Nothing

        MyBase.Finalize()
    End Sub

    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function

    Protected Overrides Function OnGetInsertFieldNames() As String
        Return "UpDateDate,UserID"
    End Function

    Protected Overrides Function OnGetInsertValues() As String
        Return clsDBStrings.SingleQot(UpDateDate) _
        & "," & User_ID
        
    End Function

    Protected Overrides Function OnGetUpdateValues() As String
        Return "UpDateDate=" & clsDBStrings.SingleQot(UpDateDate) _
        & ",UserID=" & (User_ID)

    End Function

    Protected Overrides Function OnRefreshFields(ByRef rdrReader As System.Data.SqlClient.SqlDataReader) As Boolean
        Me.UpDateDate = CType(rdrReader.Item("UpDateDate").ToString(), String)
        Me.User_ID = CType(rdrReader.Item("UserID").ToString(), Integer)
    End Function

#End Region

End Class
Public Class clsUpDateTransactionMsts
    Inherits clsDatabaseObjectS
    Public Sub New()
        Me.TableName = "tblUpDateTransactionsMst"
        Me.TableIDFieldName = "UpDateTransID"
        Me.TableNameFieldName = ""
    End Sub
End Class
