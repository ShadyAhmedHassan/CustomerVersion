Public Class clsDriverO
    Inherits clsDatabaseObjectO

#Region "Declarations"
    Private mstrDriver_License_No As String
    Private mintScaleID As Integer

#End Region

#Region "Properties"
    Public Property Driver_License_No() As String
        Get
            Return mstrDriver_License_No
        End Get
        Set(ByVal Value As String)
            mstrDriver_License_No = Value
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
        Me.TableName = "tblDriver"
        Me.TableIDFieldName = "Driver_ID"
        Me.TableNameFieldName = "Driver_Name"


    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()

    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String
        Return "Driver_Name,Driver_License_No,ScaleID"
    End Function
    Protected Overrides Function OnGetInsertValues() As String
        Return clsDBStrings.SingleQot(Name) _
        & "," & clsDBStrings.SingleQot(Driver_License_No) _
        & "," & (ScaleID)

    End Function
    Protected Overrides Function OnGetUpdateValues() As String
        Return "Driver_Name=" & clsDBStrings.SingleQot(Name) _
                & ",Driver_License_No=" & clsDBStrings.SingleQot(Driver_License_No) _
                & ",ScaleID=" & ScaleID

    End Function
    Protected Overrides Function OnRefreshFields(ByRef rdrReader As System.Data.SqlClient.SqlDataReader) As Boolean
        Me.Name = CStr(rdrReader.Item("Driver_Name").ToString())
        Me.Driver_License_No = CType(rdrReader.Item("Driver_License_No").ToString(), String)
        Me.ScaleID = CType(rdrReader.Item("ScaleID").ToString(), Integer)
    End Function


#End Region

End Class
Public Class clsDriverS
    Inherits clsDatabaseObjectS
    Public Sub New()
        Me.TableName = "tblDriver"
        Me.TableIDFieldName = "Driver_ID"
        Me.TableNameFieldName = "Driver_Name"

    End Sub
End Class