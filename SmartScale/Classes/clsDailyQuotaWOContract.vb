Public Class clsDailyQuotaWOContarctO
    Inherits clsDatabaseObjectO
#Region "Declarations"
    Private mDateQuotaDate As Date
    Private mintCustomerID As Integer
    Private mdecQuantity As Decimal
    Private mblnFinished As Boolean

#End Region

#Region "Properties"


    Public Property QuotaDate() As Date
        Get
            Return mDateQuotaDate
        End Get
        Set(ByVal Value As Date)
            mDateQuotaDate = Value
        End Set
    End Property
    Public Property CustomerID() As Integer
        Get
            Return mintCustomerID
        End Get
        Set(ByVal Value As Integer)
            mintCustomerID = Value
        End Set
    End Property
    Public Property Quantity() As Decimal
        Get
            Return mdecQuantity
        End Get
        Set(ByVal value As Decimal)
            mdecQuantity = value
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



#End Region

#Region "Methods"
    Public Sub New()
        Me.TableName = "tblDailyQuotaWithOutContract"
        Me.TableIDFieldName = "DailyQuotaID"
        Me.TableNameFieldName = ""

    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String
        Return "QuotaDate,CustomerID,Quantity,finish"
    End Function
    Protected Overrides Function OnGetInsertValues() As String
        Return clsDBStrings.ToDBDate(QuotaDate) _
         & "," & clsDBStrings.SingleQot(CustomerID) _
         & "," & (Quantity) _
         & "," & clsDBStrings.ToDBBoolean(Finished)



    End Function
    Protected Overrides Function OnGetUpdateValues() As String
        Return "QuotaDate=" & clsDBStrings.ToDBDate(QuotaDate) _
         & ",CustomerID=" & clsDBStrings.SingleQot(CustomerID) _
         & ",Quantity=" & (Quantity) _
         & ",finish=" & clsDBStrings.ToDBBoolean(Finished)
    End Function
    Protected Overrides Function OnRefreshFields(ByRef rdrReader As System.Data.SqlClient.SqlDataReader) As Boolean

        Me.QuotaDate = CType(rdrReader.Item("QuotaDate").ToString(), Date)
        Me.CustomerID = CType(rdrReader.Item("CustomerID").ToString(), String)
        Me.Quantity = CType(rdrReader.Item("Quantity").ToString(), Decimal)
        Me.Finished = CType(rdrReader.Item("finish").ToString(), Boolean)
    End Function
#End Region
End Class
Public Class clsDailyQuotaWOContractS
    Inherits clsDatabaseObjectS
    Public Sub New()
        Me.TableName = "tblDailyQuotaWithOutContract"
        Me.TableIDFieldName = "DailyQuotaID"
        Me.TableNameFieldName = ""

    End Sub
End Class
