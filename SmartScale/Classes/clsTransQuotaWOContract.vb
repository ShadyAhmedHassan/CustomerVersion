Public Class clsTransQuotaWOContractO
    Inherits clsDatabaseObjectO

#Region "Declarations"

    Private mintTransID As Integer
    Private mintQuotaID As Integer
    Private mdecQuantity As Decimal

#End Region

#Region "Properties"

    Public Property TransID() As Integer
        Get
            Return mintTransID
        End Get
        Set(ByVal Value As Integer)
            mintTransID = Value
        End Set
    End Property

    Public Property QuotaID() As Integer
        Get
            Return mintQuotaID
        End Get
        Set(ByVal Value As Integer)
            mintQuotaID = Value
        End Set
    End Property

    Public Property Quantity() As Decimal
        Get
            Return mdecQuantity
        End Get
        Set(ByVal Value As Decimal)
            mdecQuantity = Value
        End Set
    End Property

#End Region

#Region "Methods"
    Public Sub New()
        Me.TableName = "tblTransQuotaWithOutContract"
        Me.TableIDFieldName = "TransQuotaWOContractID"
        Me.TableNameFieldName = ""

    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String
        Return "TransID,QuotaID,Quantity"
    End Function
    Protected Overrides Function OnGetInsertValues() As String
        Return (TransID) _
         & "," & (QuotaID) _
         & "," & (Quantity)

    End Function
    Protected Overrides Function OnGetUpdateValues() As String
        Return "TransID=" & (TransID) _
         & ",QuotaID=" & (QuotaID) _
         & ",Quantity=" & (Quantity)


    End Function
    Protected Overrides Function OnRefreshFields(ByRef rdrReader As System.Data.SqlClient.SqlDataReader) As Boolean
        Me.TransID = CType(rdrReader.Item("TransID").ToString(), Integer)
        Me.QuotaID = CType(rdrReader.Item("QuotaID").ToString(), Integer)
        Me.Quantity = CType(rdrReader.Item("Quantity").ToString(), Decimal)

    End Function
#End Region
End Class
Public Class clsTransQuotaWOContractS
    Inherits clsDatabaseObjectS
    Public Sub New()
        Me.TableName = "tblTransQuotaWithOutContract"
        Me.TableIDFieldName = "TransQuotaWOContractID"
        Me.TableNameFieldName = ""

    End Sub
End Class

