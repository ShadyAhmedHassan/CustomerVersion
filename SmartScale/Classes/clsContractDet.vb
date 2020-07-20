Public Class clsContractDetO
    Inherits clsDatabaseObjectO

#Region "Declarations"

    Private mintContractID As Integer
    Private mintGoodID As String
    Private mdecQuantity As Decimal
    Private mdecOutgoingQuantity As Decimal
#End Region

#Region "Properties"

    Public Property ContractID() As Integer
        Get
            Return mintContractID
        End Get
        Set(ByVal Value As Integer)
            mintContractID = Value
        End Set
    End Property

    Public Property GoodID() As String
        Get
            Return mintGoodID
        End Get
        Set(ByVal Value As String)
            mintGoodID = Value
        End Set
    End Property
    'Public Property Quantity() As Decimal
    '    Get
    '        Return mdecQuantity
    '    End Get
    '    Set(ByVal Value As Decimal)
    '        mdecQuantity = Value
    '    End Set
    'End Property

    'Public Property OutGoingQuantity() As Decimal
    '    Get
    '        Return mdecOutgoingQuantity
    '    End Get
    '    Set(ByVal value As Decimal)
    '        mdecOutgoingQuantity = value
    '    End Set
    'End Property

#End Region

#Region "Methods"
    Public Sub New()
        Me.TableName = "tblContractDet"
        Me.TableIDFieldName = "ContractDetID"
        Me.TableNameFieldName = ""

    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String
        Return "ContractID,GoodID"
    End Function
    Protected Overrides Function OnGetInsertValues() As String
        Return (ContractID) _
         & "," & clsDBStrings.SingleQot(GoodID) _
         '& "," & (Quantity) _
        '& "," & (OutGoingQuantity)

    End Function
    Protected Overrides Function OnGetUpdateValues() As String
        Return "ContractID=" & (ContractID) _
         & ",GoodID=" & clsDBStrings.SingleQot(GoodID) _
         '& ",Quantity=" & (Quantity) _
        '& ",OutgoingQuantity=" & (OutGoingQuantity)

    End Function
    Protected Overrides Function OnRefreshFields(ByRef rdrReader As System.Data.SqlClient.SqlDataReader) As Boolean
        Me.ContractID = CType(rdrReader.Item("ContractID").ToString(), Integer)
        Me.GoodID = CType(rdrReader.Item("GoodID").ToString(), String)
        'Me.Quantity = CType(rdrReader.Item("Quantity").ToString(), Decimal)
        'Me.OutGoingQuantity = CType(rdrReader.Item("OutgoingQuantity").ToString(), String)

    End Function
#End Region
End Class
Public Class clsContractDetS
    Inherits clsDatabaseObjectS
    Public Sub New()
        Me.TableName = "tblContractDet"
        Me.TableIDFieldName = "ContractDetID"
        Me.TableNameFieldName = ""

    End Sub
End Class

