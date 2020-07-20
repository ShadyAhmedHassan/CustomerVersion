Public Class clsSlipO
    Inherits clsDatabaseObjectO
#Region "Declarations"
    Private mdecSlip_MinRange As Decimal
    Private mdecSlip_MaxRange As Decimal
    Private mdecSlip_Rate As Decimal
    Private mblnSlip_IsDeleted As Boolean

#End Region

#Region "Properties"
    Public Property Slip_MinRange() As Decimal
        Get
            Return mdecSlip_MinRange
        End Get
        Set(ByVal Value As Decimal)
            mdecSlip_MinRange = Value
        End Set
    End Property
    Public Property Slip_MaxRange() As Decimal
        Get
            Return mdecSlip_MaxRange
        End Get
        Set(ByVal Value As Decimal)
            mdecSlip_MaxRange = Value
        End Set
    End Property
    Public Property Slip_Rate() As Decimal
        Get
            Return mdecSlip_Rate
        End Get
        Set(ByVal Value As Decimal)
            mdecSlip_Rate = Value
        End Set
    End Property
    Public Property Slip_IsDeleted() As Boolean
        Get
            Return mblnSlip_IsDeleted
        End Get
        Set(ByVal Value As Boolean)
            mblnSlip_IsDeleted = Value
        End Set
    End Property

#End Region

#Region "Methods"
    Public Sub New()
        Me.TableName = "tblSlip"
        Me.TableIDFieldName = "Slip_ID"
        Me.TableNameFieldName = "Slip_Name"

    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Protected Overrides Function OnAfterRefresh() As Boolean
        Return True
    End Function
    Protected Overrides Function OnGetInsertFieldNames() As String
        Return "Slip_Name,Slip_MinRange,Slip_MaxRange,Slip_Rate,Slip_IsDeleted"
    End Function
    Protected Overrides Function OnGetInsertValues() As String
        Return clsDBStrings.SingleQot(Name) _
        & "," & (Slip_MinRange) _
        & "," & (Slip_MaxRange) _
        & "," & (Slip_Rate) _
        & "," & clsDBStrings.ToDBBoolean(Slip_IsDeleted)

    End Function
    Protected Overrides Function OnGetUpdateValues() As String
        Return "Slip_Name=" & clsDBStrings.SingleQot(Name) _
        & ",Slip_MinRange=" & (Slip_MinRange) _
        & ",Slip_MaxRange=" & (Slip_MaxRange) _
        & ",Slip_Rate=" & (Slip_Rate) _
        & ",Slip_IsDeleted=" & clsDBStrings.ToDBBoolean(Slip_IsDeleted)

    End Function
    Protected Overrides Function OnRefreshFields(ByRef rdrReader As System.Data.SqlClient.SqlDataReader) As Boolean
        Me.Name = CStr(rdrReader.Item("Slip_Name").ToString())

        If rdrReader.Item("Slip_MinRange").ToString <> Nothing Then
            Me.Slip_MinRange = CType(rdrReader.Item("Slip_MinRange").ToString(), Decimal)
        End If


        If rdrReader.Item("Slip_MaxRange").ToString <> Nothing Then
            Me.Slip_MaxRange = CType(rdrReader.Item("Slip_MaxRange").ToString(), Decimal)
        End If

        If rdrReader.Item("Slip_Rate").ToString <> Nothing Then
            Me.Slip_Rate = CType(rdrReader.Item("Slip_Rate").ToString(), Decimal)
        End If

        ' Me.Slip_IsDeleted = CType(rdrReader.Item("Slip_IsDeleted").ToString(), Boolean)

    End Function


#End Region

End Class
Public Class clsSlipS
    Inherits clsDatabaseObjectS
    Public Sub New()
        Me.TableName = "tblSlip"
        Me.TableIDFieldName = "Slip_ID"
        Me.TableNameFieldName = "Slip_Name"
    End Sub
End Class