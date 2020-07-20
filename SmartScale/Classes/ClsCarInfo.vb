Public Class clsCarInfoO

    Inherits clsDatabaseObjectO

#Region "Declarations"


    Private mintTruckBoard_City_ID As clsCityO
    Private mintScaleID As Integer

#End Region

#Region "Properties"

    Public Property TruckBoard_City_ID() As clsCityO
        Get
            Return mintTruckBoard_City_ID
        End Get
        Set(ByVal Value As clsCityO)
            mintTruckBoard_City_ID = Value
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
        Me.TableName = "tblCarInfo"
        Me.TableIDFieldName = "Car_Info_ID"
        Me.TableNameFieldName = ""
        mintTruckBoard_City_ID = New clsCityO

    End Sub
    Protected Overrides Sub Finalize()

        mintTruckBoard_City_ID = Nothing

        MyBase.Finalize()
    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String
        Return "TruckBoard_No,TruckBoard_City_ID,ScaleID"

    End Function
    Protected Overrides Function OnGetInsertValues() As String
        Return clsDBStrings.SingleQot(Name) _
                 & "," & (TruckBoard_City_ID.ID) _
                 & "," & (ScaleID)

    End Function
    Protected Overrides Function OnGetUpdateValues() As String

        Return "TruckBoard_No=" & clsDBStrings.SingleQot(Name) _
              & ",TruckBoard_City_ID=" & (TruckBoard_City_ID.ID) _
                & ",ScaleID=" & ScaleID


    End Function
    Protected Overrides Function OnRefreshFields(ByRef rdrReader As System.Data.SqlClient.SqlDataReader) As Boolean

        Me.Name = CType(rdrReader.Item("TruckBoard_No").ToString(), String)
        Me.TruckBoard_City_ID.ID = CType(rdrReader.Item("TruckBoard_City_ID").ToString(), Integer)
        Me.ScaleID = CType(rdrReader.Item("ScaleID").ToString(), Integer)

    End Function

#End Region

End Class

Public Class clsCarInfoS

    Inherits clsDatabaseObjectS

    Public Sub New()
        Me.TableName = "tblCarInfo"
        Me.TableIDFieldName = "Car_Info_ID"
        Me.TableNameFieldName = ""
    End Sub

End Class