Public Class clsDealerO
    Inherits clsDatabaseObjectO
#Region "Declarations"

    Private mstrdealerID As String
    Private mintType As Integer
    Private mstrDealer_Shot_Name As String

#End Region

#Region "Properties"

    Public Overridable Property ID() As String
        Get
            Return MyBase.ID
        End Get
        Set(ByVal value As String)
            MyBase.ID = value
        End Set
    End Property

    Public Property DealerID() As String
        Get
            Return mstrdealerID
        End Get
        Set(ByVal value As String)

            mstrdealerID = value
        End Set
    End Property

    Public Property Type() As Integer
        Get
            Return mintType
        End Get
        Set(ByVal value As Integer)
            mintType = value
        End Set
    End Property

    Public Property DealerShortName() As String
        Get
            Return mstrDealer_Shot_Name
        End Get
        Set(ByVal value As String)
            mstrDealer_Shot_Name = value
        End Set

    End Property

#End Region

#Region "Methods"
    Public Sub New()

        Me.TableName = "tblDealer"
        Me.TableIDFieldName = "Dealer_ID"
        Me.TableNameFieldName = "Dealer_Name"

    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String
        Return "Dealer_ID,Dealer_Name,Type,Dealer_Short_Name"
    End Function
    Protected Overrides Function OnGetInsertValues() As String
        Return clsDBStrings.SingleQot(DealerID) _
                & "," & clsDBStrings.SingleQot(Name) _
                & "," & (Type) _
                & "," & clsDBStrings.SingleQot(DealerShortName)
    End Function
    Protected Overrides Function OnGetUpdateValues() As String
        Return "Dealer_ID=" & clsDBStrings.SingleQot(DealerID) _
               & ",Dealer_Name=" & clsDBStrings.SingleQot(Name) _
               & ",Type=" & (Type) _
               & ",Dealer_Short_Name=" & clsDBStrings.SingleQot(DealerShortName)

    End Function
    Protected Overrides Function OnRefreshFields(ByRef rdrReader As System.Data.SqlClient.SqlDataReader) As Boolean

        Me.DealerID = CType(rdrReader.Item("Dealer_ID").ToString, String)
        Me.Name = CType(rdrReader.Item("Dealer_Name").ToString, String)
        Me.Type = CType(rdrReader.Item("Type").ToString, Integer)
        Me.DealerShortName = CType(rdrReader.Item("Dealer_Short_Name").ToString, String)

    End Function

#End Region

End Class
Public Class clsDealerS
    Inherits clsDatabaseObjectS
    Public Sub New()
        Me.TableName = "tblDealer"
        Me.TableIDFieldName = "Dealer_ID"
        Me.TableNameFieldName = "Dealer_Name"

    End Sub
End Class