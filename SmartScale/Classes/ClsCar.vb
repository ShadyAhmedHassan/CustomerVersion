Public Class clsCarO
    Inherits clsDatabaseObjectO
#Region "Declarations"

    Private mintCarBoard_City_ID As clsCityO
    '   Private mintCarBoard_No As String
    Private mintDealer_ID As clsDealerO
    Private mintCar_Info_ID As clsCarInfoO
    Private mintScaleID As Integer
#End Region

#Region "Properties"
   
    Public Property CarBoard_City_ID() As clsCityO
        Get
            Return mintCarBoard_City_ID
        End Get
        Set(ByVal Value As clsCityO)
            mintCarBoard_City_ID = Value
        End Set
    End Property
 
    Public Property Dealer_ID() As clsDealerO
        Get
            Return mintDealer_ID
        End Get
        Set(ByVal value As clsDealerO)
            mintDealer_ID = value
        End Set
    End Property

    Public Property Car_Info_ID() As clsCarInfoO
        Get
            Return mintCar_Info_ID
        End Get
        Set(ByVal value As clsCarInfoO)
            mintCar_Info_ID = value
        End Set
    End Property

    Public Property ScaleID() As Integer
        Get
            Return mintScaleID
        End Get
        Set(ByVal value As Integer)
            mintScaleID = value
        End Set
    End Property

#End Region

#Region "Methods"
    Public Sub New()
        Me.TableName = "tblCar"
        Me.TableIDFieldName = "Car_ID"
        Me.TableNameFieldName = ""

        mintCarBoard_City_ID = New clsCityO
        mintCar_Info_ID = New clsCarInfoO
        mintDealer_ID = New clsDealerO
    End Sub
    Protected Overrides Sub Finalize()
        mintCarBoard_City_ID = Nothing
        mintCar_Info_ID = Nothing
        mintDealer_ID = Nothing
        MyBase.Finalize()
    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String
        Return "CarBoard_No,CarBoard_City_ID,ScaleID" ',Dealer_ID,Car_Info_ID"
    End Function
    Protected Overrides Function OnGetInsertValues() As String
        Return clsDBStrings.SingleQot(Name) _
        & "," & (CarBoard_City_ID.ID) _
        & "," & (ScaleID)
        '& "," & (Dealer_ID.ID) _
        '& "" & (Car_Info_ID.ID)


    End Function
    Protected Overrides Function OnGetUpdateValues() As String

        Return "CarBoard_No=" & clsDBStrings.SingleQot(Name) _
        & ",CarBoard_City_ID=" & (CarBoard_City_ID.ID) _
        & ",ScaleID=" & (ScaleID)
        '& ",Dealer_ID=" & (Dealer_ID.ID) _
        '& ",Car_Info_ID=" & (Car_Info_ID.ID)

    End Function
    Protected Overrides Function OnRefreshFields(ByRef rdrReader As System.Data.SqlClient.SqlDataReader) As Boolean

        Me.Name = CType(rdrReader.Item("CarBoard_No").ToString(), String)
        Me.CarBoard_City_ID.ID = CType(rdrReader.Item("CarBoard_City_ID").ToString(), Integer)
        Me.ScaleID = CType(rdrReader.Item("ScaleID").ToString(), Integer)
        ' Me.Dealer_ID.ID = CType(rdrReader.Item("Dealer_ID").ToString(), Integer)
        'Me.Car_Info_ID.ID = CType(rdrReader.Item("Car_Info_ID").ToString(), Integer)

    End Function


#End Region

End Class
Public Class clsCarS
    Inherits clsDatabaseObjectS
    Public Sub New()
        Me.TableName = "tblCar"
        Me.TableIDFieldName = "Car_ID"
        Me.TableNameFieldName = ""
    End Sub


End Class