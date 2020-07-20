Public Class clsLocationO
    Inherits clsDatabaseObjectO
#Region "Declarations"



  
#End Region

#Region "Properties"

    

       
#End Region

#Region "Methods"
    Public Sub New()
        Me.TableName = "tblLocation"
        Me.TableIDFieldName = "LocationID"
        Me.TableNameFieldName = "LocationName"



    End Sub
    Protected Overrides Sub Finalize()
      
    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String
        'Return "CarBoard_No,CarBoard_City_ID,ScaleID" ',Dealer_ID,Car_Info_ID"
    End Function
    Protected Overrides Function OnGetInsertValues() As String
       


    End Function
    Protected Overrides Function OnGetUpdateValues() As String

       

    End Function
    Protected Overrides Function OnRefreshFields(ByRef rdrReader As System.Data.SqlClient.SqlDataReader) As Boolean

        'Me.Name = CStr(rdrReader.Item("LocationName").ToString())

    End Function


#End Region

End Class
Public Class clsLocationS
    Inherits clsDatabaseObjectS
    Public Sub New()
        Me.TableName = "tblLocation"
        Me.TableIDFieldName = "LocationID"
        Me.TableNameFieldName = "LocationName"
    End Sub


End Class