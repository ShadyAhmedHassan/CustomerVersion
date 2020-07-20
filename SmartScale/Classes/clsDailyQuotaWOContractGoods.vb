Public Class clsDailyQuotaWOContarctGoodO
    Inherits clsDatabaseObjectO
#Region "Declarations"

    Private mintDailyQuotaID As Integer
    Private mintGoodID As String

#End Region

#Region "Properties"

    Public Property GoodID() As String
        Get
            Return mintGoodID
        End Get
        Set(ByVal Value As String)
            mintGoodID = Value
        End Set
    End Property

    Public Property DailyQuotaID() As Integer
        Get
            Return mintDailyQuotaID
        End Get
        Set(ByVal Value As Integer)
            mintDailyQuotaID = Value
        End Set
    End Property
    



#End Region

#Region "Methods"
    Public Sub New()
        Me.TableName = "tblDailyQuotaWOContractGoods"
        Me.TableIDFieldName = "DailyQuotaGoodID"
        Me.TableNameFieldName = ""

    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String
        Return "DailyQuotaID,GoodID"
    End Function
    Protected Overrides Function OnGetInsertValues() As String
        Return DailyQuotaID _
         & "," & clsDBStrings.SingleQot(GoodID)



    End Function
    Protected Overrides Function OnGetUpdateValues() As String
        Return "DailyQuotaID=" & (DailyQuotaID) _
         & ",GoodID=" & clsDBStrings.SingleQot(GoodID)
    End Function
    Protected Overrides Function OnRefreshFields(ByRef rdrReader As System.Data.SqlClient.SqlDataReader) As Boolean

        Me.DailyQuotaID = CType(rdrReader.Item("DailyQuotaID").ToString(), Integer)
        Me.GoodID = rdrReader.Item("GoodID").ToString()
    End Function
#End Region
End Class
Public Class clsDailyQuotaWOContarctGoodS
    Inherits clsDatabaseObjectS
    Public Sub New()
        Me.TableName = "tblDailyQuotaWOContractGoods"
        Me.TableIDFieldName = "DailyQuotaGoodID"
        Me.TableNameFieldName = ""

    End Sub
End Class

