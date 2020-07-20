Public Class clsScaleO
    Inherits clsDatabaseObjectO
#Region "Declarations"
    Private mstrScale_Port As String
    Private mblnScale_IsDeleted As Boolean
    Private mstrScale_IP As String
    Private mstrScale_PortNo As Integer
    Private mintcountTo As Integer

    Private mintLocation As clsLocationO
#End Region

#Region "Properties"
    Public Property Scale_Port() As String
        Get
            Return mstrScale_Port
        End Get
        Set(ByVal Value As String)
            mstrScale_Port = Value
        End Set
    End Property
    Public Property Scale_IsDeleted() As Boolean
        Get
            Return mblnScale_IsDeleted
        End Get
        Set(ByVal Value As Boolean)
            mblnScale_IsDeleted = Value
        End Set
    End Property
    Public Property IP() As String
        Get
            Return mstrScale_IP
        End Get
        Set(ByVal value As String)
            mstrScale_IP = value
        End Set
    End Property
    Public Property PortNo() As Integer
        Get
            Return mstrScale_PortNo
        End Get
        Set(ByVal value As Integer)
            mstrScale_PortNo = value
        End Set
    End Property

    Public Property countTo() As Integer
        Get
            Return mintcountTo
        End Get
        Set(ByVal value As Integer)
            mintcountTo = value
        End Set
    End Property

    Public Property LocatinID() As clsLocationO
        Get
            Return mintLocation
        End Get
        Set(ByVal value As clsLocationO)
            mintLocation = value
        End Set
    End Property

#End Region

#Region "Methods"
    Public Sub New()
        Me.TableName = "tblScale"
        Me.TableIDFieldName = "Scale_ID"
        Me.TableNameFieldName = "Scale_Name"

        mintLocation = New clsLocationO
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String
        Return "Scale_Name,Scale_Port,Scale_IsDeleted,Scale_IP,Scale_PortNo,countTo,Location"
    End Function
    Protected Overrides Function OnGetInsertValues() As String
        Return clsDBStrings.SingleQot(Name) _
        & "," & clsDBStrings.SingleQot(Scale_Port) _
        & "," & clsDBStrings.ToDBBoolean(Scale_IsDeleted) _
        & "," & clsDBStrings.SingleQot(IP) _
        & "," & (PortNo) _
        & "," & (countTo) _
         & "," & (LocatinID.ID)
    End Function
    Protected Overrides Function OnGetUpdateValues() As String
        Return "Scale_Name=" & clsDBStrings.SingleQot(Name) _
        & ",Scale_Port=" & clsDBStrings.SingleQot(Scale_Port) _
        & ",Scale_IsDeleted=" & clsDBStrings.ToDBBoolean(Scale_IsDeleted) _
        & ",Scale_IP=" & clsDBStrings.SingleQot(IP) _
        & ",Scale_PortNo=" & (PortNo) _
        & ",countTo=" & (countTo) _
        & ",Location=" & (LocatinID.ID)
    End Function
    Protected Overrides Function OnRefreshFields(ByRef rdrReader As System.Data.SqlClient.SqlDataReader) As Boolean
        Me.Name = CStr(rdrReader.Item("Scale_Name").ToString())
        Me.Scale_Port = CType(rdrReader.Item("Scale_Port").ToString(), String)
        Me.Scale_IsDeleted = CType(rdrReader.Item("Scale_IsDeleted").ToString(), Boolean)
        Me.IP = CType(rdrReader.Item("Scale_IP").ToString(), String)
        Me.PortNo = CType(rdrReader.Item("Scale_PortNo").ToString(), Integer)
        Me.countTo = CType(rdrReader.Item("countTo").ToString(), Integer)
        Me.LocatinID.ID = CType(rdrReader.Item("Location").ToString(), Integer)
    End Function


#End Region

End Class
Public Class clsScaleS
    Inherits clsDatabaseObjectS
    Public Sub New()
        Me.TableName = "tblScale"
        Me.TableIDFieldName = "Scale_ID"
        Me.TableNameFieldName = "Scale_Name"

    End Sub
End Class