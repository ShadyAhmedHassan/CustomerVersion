Public Class clsContractO
    Inherits clsDatabaseObjectO
#Region "Declarations"

    Private mstrContractNO As String
    Private mDateContract As Date
    Private mintCustomerID As String
    Private mintParentGood As String
    Private mdecAllQuantity As Decimal
    Private mdecQuantityRatio As Decimal
    Private mdateDeliveryMonth As Date
    Private mstrNotes As String
    Private mblnFinished As Boolean
    Private mdecOutGoingQuantity As Decimal
#End Region

#Region "Properties"

    Public Property ContractNO() As String
        Get
            Return mstrContractNO
        End Get
        Set(ByVal Value As String)
            mstrContractNO = Value
        End Set
    End Property

    Public Property DateContract() As Date
        Get
            Return mDateContract
        End Get
        Set(ByVal Value As Date)
            mDateContract = Value
        End Set
    End Property
    Public Property CustomerID() As String
        Get
            Return mintCustomerID
        End Get
        Set(ByVal Value As String)
            mintCustomerID = Value
        End Set
    End Property
    Public Property ParentGood() As String
        Get
            Return mintParentGood
        End Get
        Set(ByVal value As String)
            mintParentGood = value
        End Set
    End Property
    Public Property AllQuantity() As Decimal
        Get
            Return mdecAllQuantity
        End Get
        Set(ByVal value As Decimal)
            mdecAllQuantity = value
        End Set
    End Property

    Public Property QuantityRatio() As Decimal
        Get
            Return mdecQuantityRatio
        End Get
        Set(ByVal value As Decimal)
            mdecQuantityRatio = value
        End Set
    End Property

    Public Property DeliveryMonth() As Date
        Get
            Return mdateDeliveryMonth
        End Get
        Set(ByVal Value As Date)
            mdateDeliveryMonth = Value
        End Set
    End Property

    Public Property Notes() As String
        Get
            Return mstrNotes
        End Get
        Set(ByVal Value As String)
            mstrNotes = Value
        End Set
    End Property

    Public Property Finished() As Boolean
        Get
            Return mblnFinished
        End Get
        Set(ByVal Value As Boolean)
            mblnFinished = Value
        End Set
    End Property

    Public Property OutGoingQuantity() As Decimal
        Get
            Return mdecOutGoingQuantity
        End Get
        Set(ByVal value As Decimal)
            mdecOutGoingQuantity = value
        End Set
    End Property



#End Region

#Region "Methods"
    Public Sub New()
        Me.TableName = "tblContracts"
        Me.TableIDFieldName = "ContractID"
        Me.TableNameFieldName = ""

    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String
        Return "ContractNO,DateContract,CustomerID,ParentGood,AllQuantity,QuantityRatio,DeliveryMonth,Notes,Finished,OutGoingQuantity"
    End Function
    Protected Overrides Function OnGetInsertValues() As String
        Return clsDBStrings.SingleQot(ContractNO) _
         & "," & clsDBStrings.ToDBDate(DateContract) _
         & "," & clsDBStrings.SingleQot(CustomerID) _
         & "," & clsDBStrings.SingleQot(ParentGood) _
         & "," & (AllQuantity) _
         & "," & (QuantityRatio) _
         & "," & clsDBStrings.ToDBDate(DeliveryMonth) _
         & "," & clsDBStrings.SingleQot(Notes) _
         & "," & clsDBStrings.ToDBBoolean(Finished) _
         & "," & clsDBStrings.SingleQot(OutGoingQuantity)


    End Function
    Protected Overrides Function OnGetUpdateValues() As String
        Return "ContractNO=" & clsDBStrings.SingleQot(ContractNO) _
         & ",DateContract=" & clsDBStrings.ToDBDate(DateContract) _
         & ",CustomerID=" & clsDBStrings.SingleQot(CustomerID) _
         & ",ParentGood=" & clsDBStrings.SingleQot(ParentGood) _
         & ",AllQuantity=" & (AllQuantity) _
         & ",QuantityRatio=" & (QuantityRatio) _
         & ",DeliveryMonth=" & clsDBStrings.ToDBDate(DeliveryMonth) _
         & ",Notes=" & clsDBStrings.SingleQot(Notes) _
         & ",Finished=" & clsDBStrings.ToDBBoolean(Finished) _
         & ",OutGoingQuantity=" & clsDBStrings.SingleQot(OutGoingQuantity)
    End Function
    Protected Overrides Function OnRefreshFields(ByRef rdrReader As System.Data.SqlClient.SqlDataReader) As Boolean
        Me.ContractNO = CType(rdrReader.Item("ContractNO").ToString(), String)
        Me.DateContract = CType(rdrReader.Item("DateContract").ToString(), Date)
        Me.CustomerID = CType(rdrReader.Item("CustomerID").ToString(), String)
        Me.ParentGood = CType(rdrReader.Item("ParentGood").ToString(), String)
        Me.AllQuantity = CType(rdrReader.Item("AllQuantity").ToString(), Decimal)
        Me.QuantityRatio = CType(rdrReader.Item("QuantityRatio").ToString(), Decimal)
        Me.DeliveryMonth = CType(rdrReader.Item("DeliveryMonth").ToString(), Date)
        Me.Notes = CType(rdrReader.Item("Notes").ToString(), String)
        Me.Finished = CType(rdrReader.Item("Finished").ToString(), Boolean)
        Me.OutGoingQuantity = CType(rdrReader.Item("OutGoingQuantity").ToString(), Decimal)
    End Function
#End Region
End Class
Public Class clsContractS
    Inherits clsDatabaseObjectS
    Public Sub New()
        Me.TableName = "tblContracts"
        Me.TableIDFieldName = "ContractID"
        Me.TableNameFieldName = ""

    End Sub
End Class
